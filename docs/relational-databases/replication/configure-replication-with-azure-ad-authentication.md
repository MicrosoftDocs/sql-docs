---
description: "Learn to configure Transactional or Snapshot replication with Microsoft Entra authentication."
title: "Configure replication with Microsoft Entra authentication for SQL Server enabled by Azure Arc"
titleSuffix: SQL Server enabled by Azure Arc
ms.date: 03/14/2024
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: how-to
author: "MashaMSFT"
ms.author: "mathoma"
---
# Configure replication with Microsoft Entra authentication - SQL Server enabled by Azure Arc

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver2022.md)]

This article provides steps to configure Transactional and Snapshot replication by using authentication with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) for [Azure-Arc enabled SQL Server](../../sql-server/azure-arc/overview.md). 

## Overview

Microsoft Entra authentication support for replication was introduced in Cumulative Update 6 for SQL Server 2022, and made generally available in Cumulative Update 12. When you use Microsoft Entra authentication for replication, the only different step is the first step. Specifically, create a Microsoft Entra login, and grant sysadmin permissions.

After that, use the Microsoft Entra login in the replication stored procedures to configure Transactional or Snapshot replication as you normally would.

> [!NOTE]
> Starting with SQL Server 2022 CU 6, disable Microsoft Entra authentication for replication by using session trace flag 11561.


## Prerequisites

To configure replication with Microsoft Entra authentication, you must meet the following prerequisites: 

- Have SQL Server 2022 [enabled by Azure-Arc](../../sql-server/azure-arc/connect.md) starting with [Cumulative Update 6](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate6). 
- Configured Microsoft Entra authentication for every server in the replication topology. Review [Tutorial: Set up Microsoft Entra authentication for SQL Server](../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-setup-tutorial.md) to learn more. 
- [SQL Server Management Studio (SSMS) v19.1 or higher](../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../azure-data-studio/download-azure-data-studio.md). 
- The user connecting to the publisher and subscriber is a member of the **sysadmin** fixed server role.
- The connection must be encrypted using a certificate from a trusted Certificate Authority (CA) or a self-signed certificate.
   - If a self-signed certificate is used, it must be imported to the client machine and installed into the Trusted Certificates list for the client to trust the SQL Server. This requirement cannot be bypassed by selecting the **Trust server certificate** option in SQL Server Management Studio (SSMS) as it doesn't work with replication.


## Limitations

Configuring your replication with Microsoft Entra authentication currently has the following limitations: 

- It's currently only possible to configure replication using Transact-SQL (T-SQL) and the replication stored procedures, the Replication Wizard in SSMS v19.1 or higher, or Azure Data Studio. It's not currently possible to configure replication using RMO replication objects or other command line languages. 
- Every server in the replication topology must be on at least SQL Server 2022 CU 6. Previous versions of SQL Server aren't supported.

<a name='create-sql-login-from-azure-ad'></a>

## Create SQL login from Microsoft Entra ID

[Create the Microsoft Entra login](../../t-sql/statements/create-login-transact-sql.md), and grant it the `sysadmin` role. 

To create the Microsoft Entra login and assign it as a `sysadmin`, use the following Transact-SQL (T-SQL) command: 

```sql
USE master
CREATE LOGIN [login_name] FROM EXTERNAL PROVIDER
EXEC sp_addsrvrolemember @loginame='login_name', @rolename='sysadmin' 
```

For example, to add the login name for `newuser@tenant.com`, use this command: 

```sql
USE master
CREATE LOGIN [newuser@tenant.com] FROM EXTERNAL PROVIDER
EXEC sp_addsrvrolemember @loginame='newuser@tenant.com', @rolename='sysadmin' 
```

## Create Distribution database

Use [sp_adddistributiondb](../../relational-databases/system-stored-procedures/sp-adddistributiondb-transact-sql.md) to create the distribution database. 

The following is an example script to create your distribution database on your Distributor: 

```sql
EXEC sp_adddistributiondb @database = N'distribution_db', 
@data_folder = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER22\MSSQL\DATA', 
@log_folder = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER22\MSSQL\DATA', 
@log_file_size = 2, @min_distretention = 0, @max_distretention = 72, 
@history_retention = 48, @deletebatchsize_xact = 5000, 
@deletebatchsize_cmd = 2000, @security_mode = 1 
```

The following example creates the table `UIProperties` in the Distribution database, and sets the `SnapshotFolder` property so the snapshot agent knows where to write replication snapshots: 

```sql
USE [distribution_db] 
IF (not exists (SELECT * FROM sysobjects WHERE NAME = 'UIProperties' and TYPE = 'U ')) 
CREATE TABLE UIProperties(id int) 
IF (exists(SELECT * FROM::fn_listextendedproperty('SnapshotFolder', 'user', 'dbo', 'table', 'UIProperties', null, null))) 
EXEC sp_updateextendedproperty N'SnapshotFolder', N' C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER22\MSSQL\DATA', 
'user', dbo, 'table', 'UIProperties' 
ELSE 

EXEC sp_addextendedproperty N'SnapshotFolder', N' C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER22\MSSQL\DATA', 
'user', dbo, 'table', 'UIProperties' 
```

The following script configures the Publisher to use the Distributor database, and defines the AD user login, along with a password to be used for replication: 

```sql
EXEC sp_adddistpublisher @publisher = N'publisher_db', @distribution_db = N'distribution_db', 
@security_mode = 0, @login = N'newuser@tenant.com', @password = N'password', 
@working_directory = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER22\MSSQL\ReplData', 
@trusted = N'false', @thirdparty_flag = 0, @publisher_type = N'MSSQLSERVER' 
```

## Enable replication

Use [sp_replicationdboption](../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md) to enable replication on your Publisher database, such as `testdb`, as the following example: 

```sql
EXEC sp_replicationdboption @dbname = N'testdb', @optname = N'publish', @value = N'true' 
```

## Add the publication 

Use [sp_addpublication](../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md) to add the publication. 

You can configure transactional or snapshot replication. 

### [Transactional replication](#tab/transactional)

Follow these steps to create a Transactional replication.

First, configure the log reader agent: 

```sql
USE [AdventureWorksDB] 
EXEC [AdventureWorksDB].sys.sp_addlogreader_agent @job_login = null, @job_password = null, 
@publisher_security_mode = 2, @publisher_login = N'newuser@tenant.com', 
@publisher_password = N'<password>', @job_name = null 
GO 
```

Next, create the transactional publication: 

```sql
use [AdventureWorksDB] 
exec sp_addpublication @publication = N'AdvWorksProducTrans', 
@description = N'Publication of database ''AdventureWorksDB'' from Publisher 'N'publisher_db''.', 
@sync_method = N'concurrent', @retention = 0, @allow_push = N'true', @allow_pull = N'true', 
@allow_anonymous = N'false', @enabled_for_internet = N'false', @snapshot_in_defaultfolder = N'true', 
@compress_snapshot = N'false', @ftp_port = 21, @allow_subscription_copy = N'false', 
@add_to_active_directory = N'false', @repl_freq = N'continuous', @status = N'active', 
@independent_agent = N'true', @immediate_sync = N'false', @allow_sync_tran = N'true', 
@allow_queued_tran = N'true', @allow_dts = N'false', @replicate_ddl = 1, 
@allow_initialize_from_backup = N'false', @enabled_for_p2p = N'false', 
@enabled_for_het_sub = N'false', @conflict_policy = N'pub wins' 
```


Then, create the Snapshot Agent and store the snapshot files for the Publisher by using the Microsoft Entra login for the `@publisher_login` and defining a password for the Publisher:

```sql
use [AdventureWorksDB] 
exec sp_addpublication_snapshot @publication = N'AdvWorksProducTrans', @frequency_type = 1,
 @frequency_interval = 1, @frequency_relative_interval = 1, @frequency_recurrence_factor = 0, 
@frequency_subday = 8, @frequency_subday_interval = 1, @active_start_time_of_day = 0, 
@active_end_time_of_day = 235959, @active_start_date = 0, @active_end_date = 0, 
@job_login = null, @job_password = null, @publisher_security_mode = 2, 
@publisher_login = N'newuser@tenant.com', @publisher_password = N'<password>' 
```

Finally, add the article `TestPub` to the publication: 

```sql
use [AdventureWorksDB] 
exec sp_addarticle @publication = N'AdvWorksProducTrans', @article = N'testtable', 
@source_owner = N'dbo', @source_object = N'testtable', @type = N'logbased', 
@description = null, @creation_script = null, @pre_creation_cmd = N'drop', 
@schema_option = 0x000000000803509D, @identityrangemanagementoption = N'manual', 
@destination_table = N'testtable', @destination_owner = N'dbo', @vertical_partition = N'false' 
```

### [Snapshot replication](#tab/snapshot)

Follow these steps to create a Snapshot replication.

First, create the snapshot publication: 

```sql
USE [testdb] 

EXEC sp_addpublication @publication = N'AdvWorksProducTrans', 
@description = N'Publication of database ''testdb'' from Publisher ''publisher_db''.', 
@sync_method = N'native', @retention = 0, @allow_push = N'true', @allow_pull = N'true', 
@allow_anonymous = N'true', @enabled_for_internet = N'false', @snapshot_in_defaultfolder = N'true', 
@compress_snapshot = N'false', @ftp_port = 21, @allow_subscription_copy = N'false', 
@add_to_active_directory = N'false', @repl_freq = N'snapshot', @status = N'active', 
@independent_agent = N'true', @immediate_sync = N'true', @allow_sync_tran = N'false', 
@allow_queued_tran = N'false', @allow_dts = N'false', @replicate_ddl = 1 
```

Next, create the Snapshot Agent and store the snapshot files for the Publisher by using the Microsoft Entra login for the `@publisher_login` and defining a password for the Publisher:

```sql
EXEC sp_addpublication_snapshot @publication = N'TestPub', @frequency_type = 4, @frequency_interval = 1, 
@frequency_relative_interval = 1, @frequency_recurrence_factor = 0, @frequency_subday = 8, 
@frequency_subday_interval = 1, @active_start_time_of_day = 0, @active_end_time_of_day = 235959, 
@active_start_date = 0, @active_end_date = 0, @job_login = null, @job_password = null, 
@publisher_security_mode = 2, @publisher_login = N'newuser@tenant.com', 
@publisher_password = N'password' 
``` 

Finally, add the article `TestPub` to the publication: 

```sql
USE [testdb] 
EXEC sp_addarticle @publication = N'TestPub', @article = N'testtable', 
@source_owner = N'dbo', @source_object = N'testtable', @type = N'logbased', 
@description = null, @creation_script = null, @pre_creation_cmd = N'drop',
 @schema_option = 0x000000000803509D, @identityrangemanagementoption = N'manual', 
@destination_table = N'testtable', @destination_owner = N'dbo', @vertical_partition = N'false' 
```

---


## Create Subscription 

Use [sp_addsubscription](../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md) to add your Subscriber, and then use either [sp_addpushsubscription_agent](../../relational-databases/system-stored-procedures/sp-addpushsubscription-agent-transact-sql.md) on the Publisher to create a push subscription or [sp_addpullsubscription_agent](../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md) on the Subscriber to create a pull subscription. Use the Microsoft Entra login for the `@subscriber_login`. 

The following sample script adds the subscription: 

```sql
USE [testdb] 
EXEC sp_addsubscription @publication = N'testpub', @subscriber = N'<subscription_server>', 
@destination_db = N'testdb', @subscription_type = N'Push', @sync_type = N'automatic', 
@article = N'all', @update_mode = N'read only', @subscriber_type = 0 
```


The following sample script adds a push subscription agent at the Publisher: 

```sql
EXEC sp_addpushsubscription_agent @publication = N'testpub', @subscriber = N'<subscription server.', 
@subscriber_db = N'testdb', @job_login = null, @job_password = null, @subscriber_security_mode = 2, 
@subscriber_login = N'newuser@tenant.com', @subscriber_password = 'password', @frequency_type = 64, 
@frequency_interval = 0, @frequency_relative_interval = 0, @frequency_recurrence_factor = 0, 
@frequency_subday = 0, @frequency_subday_interval = 0, @active_start_time_of_day = 0, 
@active_end_time_of_day = 235959, @active_start_date = 20220406, @active_end_date = 99991231, @enabled_for_syncmgr = N'False', @dts_package_location = N'Distributor' 
```

## Replication stored procedures

The following parameters in these replication stored procedures were modified in CU 6 for SQL Server 2022 to support Microsoft Entra authentication for replication: 

- [sp_addpullsubscription_agent](../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md): `@distributor_security_mode`
- [sp_addpushsubscription_agent](../../relational-databases/system-stored-procedures/sp-addpushsubscription-agent-transact-sql.md): `@subscriber_security_mode`
- [sp_addmergepullsubscription_agent](../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-agent-transact-sql.md): `@publisher_security_mode`, `@distributor_security_mode`
- [sp_addmergepushsubscription_agent](../../relational-databases/system-stored-procedures/sp-addmergepushsubscription-agent-transact-sql.md): `@subscriber_security_mode`, `@publisher_security_mode`
- [sp_addlogreader_agent](../../relational-databases/system-stored-procedures/sp-addlogreader-agent-transact-sql.md): `@publisher_security_mode`
- [sp_changelogreader_agent](../../relational-databases/system-stored-procedures/sp-changelogreader-agent-transact-sql.md): `@publisher_security_mode`
- [sp_addpublication_snapshot](../../relational-databases/system-stored-procedures/sp-addpublication-snapshot-transact-sql.md): `@publisher_security_mode`
- [sp_changepublication_snapshot](../../relational-databases/system-stored-procedures/sp-changepublication-snapshot-transact-sql.md): `@publisher_security_mode`

The following values define the security modes for these stored procedures: 
- **0** specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. 
- **1** specifies Windows Authentication.  
- **2** specifies Microsoft Entra password authentication starting with SQL Server 2022 CU 6. 
- **3** specifies Microsoft Entra integrated authentication starting with SQL Server 2022 CU 6. 
- **4** specifies Microsoft Entra token authentication starting with SQL Server 2022 CU 6. 


## Next steps

To learn more, review [SQL Server Replication](sql-server-replication.md) and [Microsoft Entra authentication for SQL Server](../security/authentication-access/azure-ad-authentication-sql-server-overview.md)
