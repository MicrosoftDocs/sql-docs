---
title: Known issues
titleSuffix: Azure SQL Managed Instance
description: Learn about the currently known issues with Azure SQL Managed Instance, and their possible workarounds or resolutions.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest, mathoma
ms.date: 04/17/2024
ms.service: sql-managed-instance
ms.subservice: service-overview
ms.topic: conceptual
ms.custom:
  - references_regions
---
# Known issues with Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article lists the currently known issues with [Azure SQL Managed Instance](https://azure.microsoft.com/updates/?product=sql-database&query=sql%20managed%20instance), and their resolution date or possible workaround. To learn more about Azure SQL Managed Instance, see [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md), and [What's new in Azure SQL Managed Instance?](doc-changes-updates-release-notes-whats-new.md)

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Known issues

| Issue | Date discovered | Status | Date resolved |
| --- | --- | --- | --- |
| [List of long-term backups in Azure portal shows backup files for active and deleted databases with the same name](#list-of-long-term-backups-in-azure-portal-shows-backup-files-for-active-and-deleted-databases-with-the-same-name) | Mar 2024 | Has Workaround | |
| [Temporary instance inaccessibility using the failover group listener during scaling operation](#temporary-instance-inaccessibility-using-the-failover-group-listener-during-scaling-operation) | Jan 2024 | No resolution | |
| [The event_file target of the system_health event session is not accessible](#the-event_file-target-of-the-system_health-event-session-is-not-accessible) | Dec 2023 | Has Workaround | |
| [Procedure sp_send_dbmail might fail when @query parameter is used on Nov22FW enabled managed instances](#procedure-sp_send_dbmail-may-fail-when-query-parameter-is-used-on-nov22fw-enabled-managed-instances) | Dec 2023 | Has Workaround | |
| [Increased number of system logins used for transactional replication](#increased-number-of-system-logins-used-for-transactional-replication) | Dec 2022 | No resolution | |
| [msdb table for manual backups doesn't preserve the username](#msdb-table-for-manual-backups-doesnt-preserve-the-username) | Nov 2022 | No resolution | |
| [Interim guidance on 2022 time zone updates for Chile](#interim-guidance-on-2022-time-zone-updates-for-chile) | Aug 2022 | Has Workaround | |
| [Querying external table fails with 'not supported' error message](#querying-external-table-fails-with-not-supported-error-message) | Jan 2022 | Resolved | Sep 2022 |
| [When using SQL Server authentication, usernames with '@' aren't supported](#when-using-sql-server-authentication-usernames-with--arent-supported) | Oct 2021 | Resolved | Feb 2022 |
| [Misleading error message on Azure portal suggesting recreation of the Service Principal](#misleading-error-message-on-azure-portal-suggesting-recreation-of-the-service-principal) | Sep 2021 | | Oct 2021 |
| [Changing the connection type doesn't affect connections through the failover group endpoint](#changing-the-connection-type-doesnt-affect-connections-through-the-failover-group-endpoint) | Jan 2021 | Has Workaround | |
| [Procedure sp_send_dbmail might transiently fail when @query parameter is used](#procedure-sp_send_dbmail-may-transiently-fail-when-query-parameter-is-used) | Jan 2021 | Resolved | March 2022 |
| [Distributed transactions can be executed after removing managed instance from Server Trust Group](#distributed-transactions-can-be-executed-after-removing-managed-instance-from-server-trust-group) | Oct 2020 | Has Workaround | |
| [Distributed transactions can't be executed after managed instance scaling operation](#distributed-transactions-cant-be-executed-after-managed-instance-scaling-operation) | Oct 2020 | Resolved | May 2021 |
| [Can't create SQL Managed Instance with the same name as logical server previously deleted](#cant-create-sql-managed-instance-with-the-same-name-as-logical-server-previously-deleted) | Aug 2020 | Has Workaround | |
| [Service Principal can't access Microsoft Entra ID and AKV](#service-principal-cant-access-azure-ad-and-akv) | Aug 2020 | Has Workaround | |
| [Restoring manual backup without CHECKSUM might fail](#restoring-manual-backup-without-checksum-might-fail) | May 2020 | Resolved | June 2020 |
| [Agent becomes unresponsive upon modifying, disabling, or enabling existing jobs](#agent-becomes-unresponsive-upon-modifying-disabling-or-enabling-existing-jobs) | May 2020 | Resolved | June 2020 |
| [Permissions on resource group not applied to SQL Managed Instance](#permissions-on-resource-group-not-applied-to-sql-managed-instance) | Feb 2020 | Resolved | Nov 2020 |
| [Limitation of manual failover via portal for failover groups](#limitation-of-manual-failover-via-portal-for-failover-groups) | Jan 2020 | Has Workaround | |
| [SQL Agent roles need explicit EXECUTE permissions for nonsysadmin logins](#sql-agent-roles-need-explicit-execute-permissions-for-non-sysadmin-logins) | Dec 2019 | Resolved | Sep 2022 |
| [SQL Agent jobs can be interrupted by Agent process restart](#sql-agent-jobs-can-be-interrupted-by-agent-process-restart) | Dec 2019 | Resolved | Mar 2020 |
| [Microsoft Entra logins and users aren't supported in SSDT](#azure-ad-logins-and-users-arent-supported-in-ssdt) | Nov 2019 | No Workaround | |
| [In-memory OLTP memory limits aren't applied](#in-memory-oltp-memory-limits-arent-applied) | Oct 2019 | Has Workaround | |
| [Wrong error returned while trying to remove a file that isn't empty](#wrong-error-returned-while-trying-to-remove-a-file-that-isnt-empty) | Oct 2019 | Resolved | August 2020 |
| [Change service tier and create instance operations are blocked by ongoing database restore](#change-service-tier-and-create-instance-operations-are-blocked-by-ongoing-database-restore) | Sep 2019 | Has Workaround | |
| [Resource Governor on Business Critical service tier might need to be reconfigured after failover](#resource-governor-on-business-critical-service-tier-might-need-to-be-reconfigured-after-failover) | Sep 2019 | Has Workaround | |
| [Cross-database Service Broker dialogs must be reinitialized after service tier upgrade](#cross-database-service-broker-dialogs-must-be-reinitialized-after-service-tier-upgrade) | Aug 2019 | Has Workaround | |
| [Impersonation of Microsoft Entra login types isn't supported](#impersonation-of-azure-ad-login-types-isnt-supported) | Jul 2019 | No Workaround | |
| [@query parameter not supported in sp_send_db_mail](#query-parameter-not-supported-in-sp_send_db_mail) | Apr 2019 | Resolved | Jan 2021 |
| [Transactional replication must be reconfigured after geo-failover](#transactional-replication-must-be-reconfigured-after-geo-failover) | Mar 2019 | No Workaround | |
| [tempdb structure and content is re-created](#tempdb-structure-and-content-is-re-created) | | No Workaround | |
| [Exceeding storage space with small database files](#exceeding-storage-space-with-small-database-files) | | Has Workaround | |
| [GUID values shown instead of database names](#guid-values-shown-instead-of-database-names) | | Has Workaround | |
| [Error logs aren't persisted](#error-logs-arent-persisted) | | No Workaround | |
| [Transaction scope on two databases within the same instance isn't supported](#transaction-scope-on-two-databases-within-the-same-instance-isnt-supported) | | Has Workaround | Mar 2020 |
| [CLR modules and linked servers sometimes can't reference a local IP address](#clr-modules-and-linked-servers-sometimes-cant-reference-a-local-ip-address) | | Has Workaround | |
| Database consistency not verified using `DBCC CHECKDB` after restore database from Azure Blob Storage. | | Resolved | Nov 2019 |
| Point-in-time database restore from Business Critical tier to General Purpose tier will not succeed if source database contains in-memory OLTP objects. | | Resolved | Oct 2019 |
| Database mail feature with external (non-Azure) mail servers using secure connection | | Resolved | Oct 2019 |
| Contained databases not supported in SQL Managed Instance | | Resolved | Aug 2019 |

## Has workaround

### List of long-term backups in Azure portal shows backup files for active and deleted databases with the same name

Long-term backups can be listed and managed on Azure portal page for an Azure SQL Managed Instance on _Backups_ tab. The page lists active or deleted databases, basic information about their long-term backups, and link for managing backups. Clicking on the _Manage_ link opens a new side blade with list of backups. Due to an issue with the filtering logic, the list shows backups for both active database and deleted databases with the same name. This requires a special attention when selecting backups for deletion, to avoid deleting backups for a wrong database.

**Workaround**: Use displayed _Backup time (UTC)_ information in the list to differentiate backups belonging to databases with the same name that existed on the instance at different periods. Alternatively, use PowerShell commands [Get-AzSqlInstanceDatabaseLongTermRetentionBackup](/powershell/module/az.sql/get-azsqlinstancedatabaselongtermretentionbackup) and [Remove-AzSqlInstanceDatabaseLongTermRetentionBackup](/powershell/module/az.sql/remove-azsqlinstancedatabaselongtermretentionbackup), or CLI commands [az sql midb ltr-backup list](/cli/azure/sql/midb/ltr-backup#az-sql-midb-ltr-backup-list) and [az sql midb ltr-backup delete](/cli/azure/sql/midb/ltr-backup#az-sql-midb-ltr-backup-delete) to manage long-term backups using _DatabaseState_ parameter and _DatabaseDeletionTime_ return value to filter backups for a database.

### The event_file target of the system_health event session is not accessible

When you attempt to read the contents of the `event_file` target of the `system_health` event session, you get error 40538, "A valid URL beginning with 'https://' is required as value for any filepath specified." This occurs in SQL Server Management Studio, or when reading the session data using the [sys.fn_xe_file_target_read_file](/sql/relational-databases/system-functions/sys-fn-xe-file-target-read-file-transact-sql) function.

This change in behavior is an unintended consequence of a recent required security fix. We are investigating the feasibility of an additional change that would allow customers to continue using the `system_health` session on Azure SQL Managed Instance securely. In the meantime, customers can work around this issue by creating their own equivalent of the `system_health` session with an `event_file` target in Azure blob storage. For more information, including a T-SQL script to create the `system_health` session that can be modified to create your own equivalent of `system_health`, see [Use the system_health session](/sql/relational-databases/extended-events/use-the-system-health-session).

### <a id="procedure-sp_send_dbmail-may-fail-when-query-parameter-is-used-on-nov22fw-enabled-managed-instances"></a> Procedure sp_send_dbmail might fail when @query parameter

Procedure `sp_send_dbmail` might fail when `@query` parameter is used.. Failures happen when the stored procedure is executed under sysadmin account.

This problem is caused by a known bug related to how `sp_send_dbmail` is using impersonation.

**Workaround**: Make sure you call `sp_send_dbmail` under appropriate custom account you've created, and not under sysadmin account.

Here's an example of how you can create a dedicated account and modify existing objects that are sending email via `sp_send_dbmail`.

```sql
USE [msdb]
GO

-- Step 1: Create a user mapped to a login to specify as a runtime user.
CREATE USER [user_name] FOR LOGIN [login_name]
GO
EXEC msdb.dbo.sp_update_jobstep @job_name=N'db_mail_sending_job', @step_id=db_mail_sending_job_id , @database_user_name=N'user_name'
GO

-- Step 2: Grant DB Mail permissions to the user who created it.
ALTER ROLE [DatabaseMailUserRole] ADD MEMBER [user_name]
GO

-- Step 3: If the database of the job step is not msdb, the permission error cannot be avoided even if it is a member of the role, so set it to msdb.
EXEC msdb.dbo.sp_update_jobstep @job_name=N'db_mail_sending_job', @step_id=db_mail_sending_job_id , @database_name=N'msdb'
GO 

-- Step 4: Set a principal in the email profile
EXEC msdb.dbo.sysmail_add_principalprofile_sp @principal_name=N'user_name', @profile_name=N'profile_name', @is_default=0
GO
```

### Interim guidance on 2022 time zone updates for Chile

On August 8, 2022, the Chilean government made an official announcement about a Daylight-Saving Time (DST) [time zone change](https://techcommunity.microsoft.com/t5/daylight-saving-time-time-zone/interim-guidance-on-2022-time-zone-updates-for-chile/ba-p/3598290). Starting at 12:00 a.m. Saturday, September 10, 2022, until 12:00 a.m. Saturday, April 1, 2023, the official time will advance 60 minutes. The change affects the following three time zones:  **Pacific SA Standard Time**, **Easter Island Standard Time** and **Magallanes Standard Time**. Azure SQL Managed Instances using affected time zones don't reflect the changes [until Microsoft releases an OS update](https://techcommunity.microsoft.com/t5/daylight-saving-time-time-zone/interim-guidance-on-2022-time-zone-updates-for-chile/ba-p/3598290) to support this, and Azure SQL Managed Instance service absorbs the update on the OS level.

**Workaround**: If you need to alter affected time zones for your managed instances, be aware of the [limitations](timezones-overview.md#limitations) and follow the guidance from the documentation.

### Changing the connection type doesn't affect connections through the failover group endpoint

If an instance participates in a [failover group](failover-group-sql-mi.md), changing the instance's [connection type](connection-types-overview.md) doesn't take effect for the connections established through the failover group listener endpoint.

**Workaround**: Drop and recreate failover group after changing the connection type.

### <a id="procedure-sp_send_dbmail-may-transiently-fail-when-query-parameter-is-used"></a> Procedure sp_send_dbmail might transiently fail when @query parameter is used

Procedure `sp_send_dbmail` might transiently fail when `@query` parameter is used. When this issue occurs, every second execution of procedure `sp_send_dbmail` fails with error `Msg 22050, Level 16, State 1` and message `Failed to initialize sqlcmd library with error number -2147467259`. To be able to see this error properly, the procedure should be called with default value 0 for the parameter `@exclude_query_output`, otherwise the error isn't be propagated.

This problem is caused by a known bug related to how `sp_send_dbmail` is using impersonation and connection pooling.

To work around this issue wrap code for sending email into a retry logic that relies on output parameter `@mailitem_id`. If the execution fails, then parameter value is NULL, indicating `sp_send_dbmail` should be called one more time to successfully send an email. Here is an example this retry logic:

```sql
CREATE PROCEDURE send_dbmail_with_retry AS
BEGIN
    DECLARE @miid INT
    EXEC msdb.dbo.sp_send_dbmail
        @recipients = 'name@mail.com', @subject = 'Subject', @query = 'select * from dbo.test_table',
        @profile_name ='AzureManagedInstance_dbmail_profile', @execute_query_database = 'testdb',
        @mailitem_id = @miid OUTPUT

    -- If sp_send_dbmail returned NULL @mailidem_id then retry sending email.
    --
    IF (@miid is NULL)
    EXEC msdb.dbo.sp_send_dbmail
        @recipients = 'name@mail.com', @subject = 'Subject', @query = 'select * from dbo.test_table',
        @profile_name ='AzureManagedInstance_dbmail_profile', @execute_query_database = 'testdb',
END
```

### Distributed transactions can be executed after removing managed instance from Server Trust Group

[Server Trust Groups](../managed-instance/server-trust-group-overview.md) are used to establish trust between managed instances that is prerequisite for executing [distributed transactions](../database/elastic-transactions-overview.md). After removing managed instance from Server Trust Group or deleting the group, you still might be able to execute distributed transactions. There's a workaround you can apply to be sure that distributed transactions are disabled and that is [user-initiated manual failover](../managed-instance/user-initiated-failover.md) on the managed instance.

### Distributed transactions can't be executed after managed instance scaling operation

SQL Managed Instance scaling operations that include changing service tier or number of vCores will reset Server Trust Group settings on the backend and disable running [distributed transactions](../database/elastic-transactions-overview.md). As a workaround, delete and create new [Server Trust Group](../managed-instance/server-trust-group-overview.md) on Azure portal.

### Can't create SQL Managed Instance with the same name as logical server previously deleted

A DNS record of `<name>.database.windows.com` is created when you create a [logical server in Azure](../database/logical-servers.md) for Azure SQL Database, and when you create a SQL Managed Instance. The DNS record must be unique. As such, if you create a logical server for SQL Database and then delete it, there's a threshold period of seven days before the name is released from the records. In that period, a SQL Managed Instance can't be created with the same name as the deleted logical server. As a workaround, use a different name for the SQL Managed Instance, or create a support ticket to release the logical server name.

<a name='service-principal-cant-access-azure-ad-and-akv'></a>

### Service Principal can't access Microsoft Entra ID and AKV

In some circumstances, there might exist an issue with Service Principal used to access Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) and Azure Key Vault (AKV) services. As a result, this issue impacts usage of Microsoft Entra authentication and transparent data encryption (TDE) with SQL Managed Instance. This might be experienced as an intermittent connectivity issue, or not being able to run statements such are `CREATE LOGIN/USER FROM EXTERNAL PROVIDER` or `EXECUTE AS LOGIN/USER`. Setting up TDE with customer-managed key on a new Azure SQL Managed Instance might also not work in some circumstances.

**Workaround**: To prevent this issue from occurring on your SQL Managed Instance before executing any update commands, or in case you have already experienced this issue after update commands, go to Azure portal, access SQL Managed Instance [Active Directory admin page](../database/authentication-aad-configure.md?tabs=azure-powershell#azure-portal). Verify if you can see the error message "Managed Instance needs a Service Principal to access Microsoft Entra ID. Click here to create a Service Principal". In case you've encountered this error message, select it, and follow the step-by-step instructions provided until this error have been resolved.

### Limitation of manual failover via portal for failover groups

If a failover group spans across instances in different Azure subscriptions or resource groups, manual failover can't be initiated from the primary instance in the failover group.

**Workaround**: Initiate failover via the portal from the geo-secondary instance.

### SQL Agent roles need explicit EXECUTE permissions for non-sysadmin logins

If non-sysadmin logins are added to any [SQL Agent fixed database roles](/sql/ssms/agent/sql-server-agent-fixed-database-roles), there exists an issue in which explicit EXECUTE permissions need to be granted to three stored procedures in the `master` database for these logins to work. If this issue is encountered, the error message `The EXECUTE permission was denied on the object <object_name> (Microsoft SQL Server, Error: 229)` is shown.

**Workaround**: Once you add logins to a SQL Agent fixed database role (SQLAgentUserRole, SQLAgentReaderRole, or SQLAgentOperatorRole), for each of the logins added to these roles, execute the following T-SQL script to explicitly grant EXECUTE permissions to the stored procedures listed.

```sql
USE [master];
GO

CREATE USER [login_name] FOR LOGIN [login_name];
GO

GRANT EXECUTE ON master.dbo.xp_sqlagent_enum_jobs TO [login_name];
GRANT EXECUTE ON master.dbo.xp_sqlagent_is_starting TO [login_name];
GRANT EXECUTE ON master.dbo.xp_sqlagent_notify TO [login_name];
```

### In-memory OLTP memory limits aren't applied

The Business Critical service tier doesn't correctly apply [max memory limits for memory-optimized objects](../managed-instance/resource-limits.md#in-memory-oltp-available-space) in some cases. SQL Managed Instance might enable workload to use more memory for in-memory OLTP operations, which can affect availability and stability of the instance. In-memory OLTP queries that are reaching the limits might not fail immediately. The queries that use more in-memory OLTP memory fail sooner if they reach the [limits](../managed-instance/resource-limits.md#in-memory-oltp-available-space).

**Workaround**: [Monitor in-memory OLTP storage usage](in-memory-oltp-monitor-space.md) using [SQL Server Management Studio](/sql/relational-databases/in-memory-oltp/monitor-and-troubleshoot-memory-usage#bkmk_Monitoring) to ensure that the workload isn't using more than the available memory. Increase the memory limits that depend on the number of vCores, or optimize your workload to use less memory.

### Wrong error returned while trying to remove a file that isn't empty

SQL Server and SQL Managed Instance [don't allow a user to drop a file that isn't empty](/sql/relational-databases/databases/delete-data-or-log-files-from-a-database#Prerequisites). If you try to remove a nonempty data file using an `ALTER DATABASE REMOVE FILE` statement, the error `Msg 5042 – The file '<file_name>' cannot be removed because it is not empty` isn't immediately returned. SQL Managed Instance will keep trying to drop the file, and the operation will fail after 30 minutes with `Internal server error`.

### Change service tier and create instance operations are blocked by ongoing database restore

An ongoing `RESTORE` statement, a Data Migration Service migration process, and built-in point-in-time restore, will block updating a service tier or resize of the existing instance and creating new instances until the restore process finishes.

The restore process blocks these operations on the managed instances and instance pools in the same subnet where the restore process is running. The instances in instance pools aren't affected. Create or change service tier operations don't fail or time out. They proceed once the restore process is completed or canceled.

**Workaround**: Wait until the restore process finishes, or cancel the restore process if the creation or update-service-tier operation has higher priority.

### Resource Governor on Business Critical service tier might need to be reconfigured after failover

The [Resource Governor](/sql/relational-databases/resource-governor/resource-governor) feature that enables you to limit the resources assigned to the user workload might incorrectly classify some user workload after failover or a user-initiated change of service tier (for example, the change of max vCore or max instance storage size).

**Workaround**: Run `ALTER RESOURCE GOVERNOR RECONFIGURE` periodically or as part of a SQL Agent job that executes the SQL task when the instance starts if you are using [Resource Governor](/sql/relational-databases/resource-governor/resource-governor).

### Cross-database Service Broker dialogs must be reinitialized after service tier upgrade

Cross-database Service Broker dialogs will stop delivering the messages to the services in other databases after change service tier operation. The messages *aren't lost*, and they can be found in the sender queue. Any change of vCores or instance storage size in SQL Managed Instance causes a `service_broke_guid` value in [sys.databases](/sql/relational-databases/system-catalog-views/sys-databases-transact-sql) view to be changed for all databases. Any `DIALOG` created using a [BEGIN DIALOG](/sql/t-sql/statements/begin-dialog-conversation-transact-sql) statement that references Service Brokers in other database stops delivering messages to the target service.

**Workaround**: Stop any activity that uses cross-database Service Broker dialog conversations before updating a service tier, and reinitialize them afterward. If there are remaining messages that are undelivered after a service tier change, read the messages from the source queue and resend them to the target queue.

### Exceeding storage space with small database files

`CREATE DATABASE`, `ALTER DATABASE ADD FILE`, and `RESTORE DATABASE` statements might fail because the instance can reach the Azure Storage limit.

Each General Purpose instance of SQL Managed Instance has up to 35 TB of storage reserved for Azure Premium Disk space. Each database file is placed on a separate physical disk. Disk sizes can be 128 GB, 256 GB, 512 GB, 1 TB, or 4 TB. Unused space on the disk isn't charged, but the total sum of Azure Premium Disk sizes can't exceed 35 TB. In some cases, a managed instance that doesn't need 8 TB in total might exceed the 35 TB Azure limit on storage size due to internal fragmentation.

For example, a General Purpose instance of SQL Managed Instance might have one large file that's 1.2 TB in size placed on a 4-TB disk. It also might have 248 files that are 1 GB each and that are placed on separate 128-GB disks. In this example:

- The total allocated disk storage size is 1 x 4 TB + 248 x 128 GB = 35 TB.
- The total reserved space for databases on the instance is 1 x 1.2 TB + 248 x 1 GB = 1.4 TB.

This example illustrates that under certain circumstances, due to a specific distribution of files, an instance of SQL Managed Instance might reach the 35-TB limit that's reserved for an attached Azure Premium Disk, when you might not expect it to.

In this example, existing databases continue to work and can grow without any problem as long as new files aren't added. New databases can't be created or restored because there isn't enough space for new disk drives, even if the total size of all databases doesn't reach the instance size limit. The error that's returned in that case isn't clear.

You can [identify the number of remaining files](https://medium.com/azure-sqldb-managed-instance/how-many-files-you-can-create-in-general-purpose-azure-sql-managed-instance-e1c7c32886c1) by using system views. If you reach this limit, try to [empty and delete some of the smaller files by using the DBCC SHRINKFILE statement](/sql/t-sql/database-console-commands/dbcc-shrinkfile-transact-sql#d-emptying-a-file) or switch to the [Business Critical tier, which doesn't have this limit](../managed-instance/resource-limits.md#service-tier-characteristics).

### GUID values shown instead of database names

Several system views, performance counters, error messages, XEvents, and error log entries display GUID database identifiers instead of the actual database names. Don't rely on these GUID identifiers because they might be replaced with actual database names in the future.

**Workaround**: Use `sys.databases` view to resolve the actual database name from the physical database name, specified in the form of GUID database identifiers:

```sql
SELECT name AS ActualDatabaseName,
    physical_database_name AS GUIDDatabaseIdentifier
FROM sys.databases
WHERE database_id > 4;
```

### CLR modules and linked servers sometimes can't reference a local IP address

CLR modules in SQL Managed Instance and linked servers or distributed queries that reference a current instance sometimes can't resolve the IP of a local instance. This error is a transient issue.

### Transaction scope on two databases within the same instance isn't supported

**(Resolved in March 2020)** The `TransactionScope` class in .NET doesn't work if two queries are sent to two databases within the same instance under the same transaction scope:

```csharp
using (var scope = new TransactionScope())
{
    using (var conn1 = new SqlConnection("Server=quickstartbmi.neu15011648751ff.database.windows.net;Database=b;User ID=myuser;Password=mypassword;Encrypt=true"))
    {
        conn1.Open();
        SqlCommand cmd1 = conn1.CreateCommand();
        cmd1.CommandText = string.Format("insert into T1 values(1)");
        cmd1.ExecuteNonQuery();
    }

    using (var conn2 = new SqlConnection("Server=quickstartbmi.neu15011648751ff.database.windows.net;Database=b;User ID=myuser;Password=mypassword;Encrypt=true"))
    {
        conn2.Open();
        var cmd2 = conn2.CreateCommand();
        cmd2.CommandText = string.Format("insert into b.dbo.T2 values(2)");        cmd2.ExecuteNonQuery();
    }

    scope.Complete();
}
```

**Workaround (not needed since March 2020)**: Use [SqlConnection.ChangeDatabase(String)](/dotnet/api/system.data.sqlclient.sqlconnection.changedatabase) to use another database in a connection context instead of using two connections.

## No resolution

### Increased number of system logins used for transactional replication

Azure SQL Managed Instance service is creating system login for purposes of transactional replication. This login can be found in SSMS (in **Object explorer**, under **Security**, **Logins**) or in system view `sys.syslogins`. Login name format looks like `'DBxCy\WF-abcde01234QWERT'`, and the login has public server role. Under certain conditions, this login is recreated, and due to a fault in the system previous login isn't deleted. This can lead to increased number of logins. These logins don't represent a security threat. They can be safely ignored. These logins shouldn't be deleted because at least one of them is being used for transactional replication.

### <a id="msdb-table-for-manual-backups-doesnt-preserve-the-username"></a> Table for manual backups in msdb doesn't preserve the username

We recently introduced support for auto backups in `msdb`, but the table doesn't currently contain username information.

<a name='azure-ad-logins-and-users-arent-supported-in-ssdt'></a>

### Microsoft Entra logins and users aren't supported in SSDT

SQL Server Data Tools don't fully support Microsoft Entra logins and users.

<a name='impersonation-of-azure-ad-login-types-isnt-supported'></a>

### Impersonation of Microsoft Entra login types isn't supported

Impersonation using `EXECUTE AS USER` or `EXECUTE AS LOGIN` of the following Microsoft Entra principals isn't supported:

- Aliased Microsoft Entra users. The following error is returned in this case: `15517`.
- Microsoft Entra logins and users based on Microsoft Entra applications or service principals. The following errors are returned in this case: `15517` and `15406`.

### Transactional replication must be reconfigured after geo-failover

If transactional replication is enabled on a database in a failover group, the SQL Managed Instance administrator must clean up all publications on the old primary and reconfigure them on the new primary after a failover to another region occurs. For more information, see [Replication](../managed-instance/transact-sql-tsql-differences-sql-server.md#replication).

### `tempdb` structure and content is re-created

The `tempdb` database is always split into 12 data files, and the file structure can't be changed. The maximum size per file can't be changed, and new files can't be added to `tempdb`. The `tempdb` database is always re-created as an empty database when the instance starts or fails over, and any changes made in `tempdb` aren't be preserved.

### Error logs aren't persisted

Error logs that are available in SQL Managed Instance aren't persisted, and their size isn't included in the maximum storage limit. Error logs might be automatically erased if failover occurs. There might be gaps in the error log history because SQL Managed Instance was moved several times on several virtual machines.

### Temporary instance inaccessibility using the failover group listener during scaling operation

Scaling managed instance sometimes requires moving the instance to a different virtual cluster, along with the associated service-maintained DNS records. If the managed instance participates in a failover group, the DNS record corresponding to its associated failover group listener (read-write listener, if the instance is the current geo-primary read-only listener, if the instance is the current geo-secondary) is moved to the new virtual cluster.

In the current scaling operation design, the listener DNS records are removed from the originating virtual cluster before the managed instance itself is fully migrated to the new virtual cluster, which in some situations can lead to prolonged time during which the instance's IP address can't be resolved using the listener. During this time, a SQL client attempting to access the instance being scaled using the listener endpoint can expect login failures with the following error message: "**Error 40532**: Cannot open server "xxx.xxx.xxx.xxx" requested by the login. The login failed. (Microsoft SQL Server, Error: 40532)".

The issue will be addressed through scaling operation redesign.

## Resolved

### <a id="querying-external-table-fails-with-not-supported-error-message"></a> Query on external table fails with not supported error message

Querying external table might fail with generic error message "Queries over external tables are not supported with the current service tier or performance level of this database. Consider upgrading the service tier or performance level of the database". The only type of external table supported in Azure SQL Managed Instance are PolyBase external tables (in preview). To allow queries on PolyBase external tables, you need to enable PolyBase on managed instance by running `sp_configure` command.

External tables related to [Elastic Query](../database/elastic-query-overview.md) feature of Azure SQL Database [aren't supported](../database/features-comparison.md#features-of-sql-database-and-sql-managed-instance) in SQL Managed Instance, but creating and querying them wasn't explicitly blocked. With support for PolyBase external tables, new checks have been introduced, blocking querying of *any* type of external table in managed instance unless PolyBase is enabled.

If you're using unsupported Elastic Query external tables to query data in Azure SQL Database or Azure Synapse from your managed instance, you should use Linked Server feature instead. To establish Linked Server connection from SQL Managed Instance to SQL Database, follow instructions from [this article](https://techcommunity.microsoft.com/t5/azure-database-support-blog/lesson-learned-63-it-is-possible-to-create-linked-server-in/ba-p/369168). To establish Linked Server connection from SQL Managed Instance to SQL Synapse, check [step-by-step instructions](https://devblogs.microsoft.com/azure-sql/linked-server-to-synapse-sql-to-implement-polybase-like-scenarios-in-managed-instance/#how-to-use-linked-servers). Since configuring and testing Linked Server connection takes some time, you can use a workaround as a temporary solution to enable querying external tables related to Elastic Query feature:

**Workaround**: Execute the following commands (once per instance) that enable queries on external tables:

```sql
sp_configure 'polybase enabled', 1;
GO

RECONFIGURE;
GO
```

### When using SQL Server authentication, usernames with '@' aren't supported

Usernames that contain the '@' symbol in the middle (for example, `'abc@xy'`) aren't able to sign in using SQL Server authentication.

### Restoring manual backup without CHECKSUM might fail

In certain circumstances manual backup of databases that was made on a managed instance without CHECKSUM might fail to be restored. In such cases, retry restoring the backup until you're successful.

**Workaround**: Take manual backups of databases on managed instances with CHECKSUM enabled.

### Agent becomes unresponsive upon modifying, disabling, or enabling existing jobs

In certain circumstances, modifying, disabling, or enabling an existing job can cause the agent to become unresponsive. The issue is automatically mitigated upon detection, resulting in a restart of the agent process.

### Permissions on resource group not applied to SQL Managed Instance

When the SQL Managed Instance Contributor Azure role is applied to a resource group (RG), it's not applied to SQL Managed Instance and has no effect.

**Workaround**: Set up a SQL Managed Instance Contributor role for users at the subscription level.

### SQL Agent jobs can be interrupted by Agent process restart

**(Resolved in March 2020)** SQL Agent creates a new session each time a job is started, gradually increasing memory consumption. To avoid hitting the internal memory limit, which would block execution of scheduled jobs, Agent process is restarted once its memory consumption reaches threshold. It might result in interrupting execution of jobs running at the moment of restart.

### <a id="query-parameter-not-supported-in-sp_send_db_mail"></a> @query parameter not supported in sp_send_db_mail

The `@query` parameter in the [sp_send_db_mail](/sql/relational-databases/system-stored-procedures/sp-send-dbmail-transact-sql) procedure doesn't work.

### Misleading error message on Azure portal suggesting recreation of the Service Principal

The **Active Directory admin** page of Azure portal for Azure SQL Managed Instance might show the following error message, even though Service Principal already exists:

"Managed Instance needs a Service Principal to access Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)). Click here to create a Service Principal"

You can neglect this error message if Service Principal for the managed instance already exists, and/or Microsoft Entra authentication on the managed instance works.

To check whether Service Principal exists, navigate to the **Enterprise applications** page on the Azure portal, choose **Managed Identities** from the **Application type** dropdown list, select **Apply**, and type the name of the managed instance in the search box. If the instance name shows up in the result list, Service Principal already exists and no further actions are needed.

If you already followed the instructions from the error message and selected the link from the error message, Service Principal of the managed instance has been recreated. In that case, assign Microsoft Entra ID read permissions to the newly created Service Principal in order for Microsoft Entra authentication to work properly. This can be done via Azure PowerShell by following [instructions](../database/authentication-aad-configure.md?tabs=azure-powershell#powershell).

## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/sql/sql-server/sql-server-docs-contribute).

## Related content

- For a list of SQL Managed Instance updates and improvements, see [SQL Managed Instance service updates](https://azure.microsoft.com/updates/?product=sql-database&query=sql%20managed%20instance).

- For updates and improvements to all Azure services, see [Service updates](https://azure.microsoft.com/updates).
