---
title: Known Issues
titleSuffix: Azure SQL Managed Instance
description: Learn about the currently known issues with Azure SQL Managed Instance, and their possible workarounds or resolutions.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 03/03/2026
ms.service: azure-sql-managed-instance
ms.subservice: service-overview
ms.topic: troubleshooting-known-issue
---
# Known issues with Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article lists the currently known issues with [Azure SQL Managed Instance](https://azure.microsoft.com/updates/?product=sql-database&query=sql%20managed%20instance), and their resolution date or possible workaround. To learn more about Azure SQL Managed Instance, see [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md), and [What's new in Azure SQL Managed Instance?](doc-changes-updates-release-notes-whats-new.md)

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Known issues

| Issue | Date discovered | Status | Date resolved |
| --- | --- | --- | --- |
| [Misleading error message when connecting to a read replica using invalid credentials](#misleading-error-message-when-connecting-to-a-read-replica-using-invalid-credentials) | February 2026 |
| [Modifying backup retention period for the free offer](#modifying-backup-retention-period-for-the-free-offer) | June 2025 | Has workaround | |
| [Login to read-secondary failed due to long wait on "HADR_DATABASE_WAIT_FOR_TRANSITION_TO_VERSIONING"](#login-to-read-secondary-failed-due-to-long-wait-on-hadr_database_wait_for_transition_to_versioning) | April 2025 | Has workaround | |
| [Interim guidance on 2024 time zone updates for Paraguay](#interim-guidance-on-2024-time-zone-updates-for-paraguay) | March 2025 | Resolved | February 2026 |
| [Error 8992 when running DBCC CHECKDB on a SQL Server database that originated from SQL Managed Instance](#error-8992-when-running-dbcc-checkdb-on-a-sql-server-database-that-originated-from-sql-managed-instance) | March 2025 | Has workaround | |
| [Differential backups aren't taken when an instance is linked to SQL Server](#differential-backups-arent-taken-when-an-instance-is-linked-to-sql-server) | Sept 2024 | By design | |
| [List of long-term backups in Azure portal shows backup files for active and deleted databases with the same name](#list-of-long-term-backups-in-azure-portal-shows-backup-files-for-active-and-deleted-databases-with-the-same-name) | Mar 2024 | Has workaround | |
| [Temporary instance inaccessibility using the failover group listener during scaling operation](#temporary-instance-inaccessibility-using-the-failover-group-listener-during-scaling-operation) | Jan 2024 | Resolved | April 2025 |
| [The event_file target of the system_health event session isn't accessible](#the-event_file-target-of-the-system_health-event-session-isnt-accessible) | Dec 2023 | Partially resolved | May 2025 |
| [Procedure sp_send_dbmail might fail when @query parameter is used](#procedure-sp_send_dbmail-might-fail-when-query-parameter-is-used) | Dec 2023 | Has workaround | |
| [Increased number of system logins used for transactional replication](#increased-number-of-system-logins-used-for-transactional-replication) | Dec 2022 | No resolution | |
| [msdb table for manual backups doesn't preserve the username](#msdb-table-for-manual-backups-doesnt-preserve-the-username) | Nov 2022 | Resolved | Aug 2023 |
| [When you use SQL Server authentication, usernames with '@' aren't supported](#when-you-use-sql-server-authentication-usernames-with-at-sign-arent-supported) | Oct 2021 | Resolved | Feb 2022 |
| [Misleading error message on Azure portal suggesting recreation of the Service Principal](#misleading-error-message-on-azure-portal-suggesting-recreation-of-the-service-principal) | Sep 2021 | | Oct 2021 |
| [Changing the connection type doesn't affect connections through the failover group endpoint](#changing-the-connection-type-doesnt-affect-connections-through-the-failover-group-endpoint) | Jan 2021 | Resolved | Nov 2025 |
| [Distributed transactions can be executed after removing SQL managed instance from Server Trust Group](#distributed-transactions-can-be-executed-after-removing-sql-managed-instance-from-server-trust-group) | Oct 2020 | Has workaround | |
| [Can't create SQL Managed Instance with the same name as logical server previously deleted](#cant-create-sql-managed-instance-with-the-same-name-as-logical-server-previously-deleted) | Aug 2020 | Has workaround | |
| [Service Principal can't access Microsoft Entra ID and AKV](#service-principal-cant-access-azure-ad-and-akv) | Aug 2020 | Has workaround | |
| [Restoring manual backup without CHECKSUM might fail](#restoring-manual-backup-without-checksum-might-fail) | May 2020 | Resolved | June 2020 |
| [Permissions on resource group not applied to SQL Managed Instance](#permissions-on-resource-group-not-applied-to-sql-managed-instance) | Feb 2020 | Resolved | Nov 2020 |
| [Microsoft Entra logins and users aren't supported in SSDT](#azure-ad-logins-and-users-arent-supported-in-ssdt) | Nov 2019 | No Workaround | |
| [Wrong error returned while trying to remove a file that isn't empty](#wrong-error-returned-while-trying-to-remove-a-file-that-isnt-empty) | Oct 2019 | Resolved | August 2020 |
| [Change service tier and create instance operations are blocked by ongoing database restore](#change-service-tier-and-create-instance-operations-are-blocked-by-ongoing-database-restore) | Sep 2019 | Has workaround | |
| [Resource Governor on a readable secondary replica needs reconfiguration after failover](#resource-governor-on-a-readable-secondary-replica-needs-reconfiguration-after-failover) | Sep 2019 | Has workaround | |
| [Cross-database Service Broker dialogs need reinitialization after service tier upgrade](#cross-database-service-broker-dialogs-need-reinitialization-after-service-tier-upgrade) | Aug 2019 | Resolved | January 2020 |
| [Impersonation of Microsoft Entra login types isn't supported](#impersonation-of-azure-ad-login-types-isnt-supported) | Jul 2019 | No Workaround | |
| [Transactional replication must be reconfigured after geo-failover](#transactional-replication-must-be-reconfigured-after-geo-failover) | Mar 2019 | No Workaround | |
| [Exceeding storage space with small database files](#exceeding-storage-space-with-small-database-files) | | Has workaround | |
| [GUID values shown instead of database names](#guid-values-shown-instead-of-database-names) | | Has workaround | |
| [Error logs aren't persisted](#error-logs-arent-persisted) | | No Workaround | |
| [CLR modules and linked servers sometimes can't reference a local IP address](#clr-modules-and-linked-servers-sometimes-cant-reference-a-local-ip-address) | | Has workaround | |
| Database consistency not verified using `DBCC CHECKDB` after restore database from Azure Blob Storage. | | Resolved | Nov 2019 |
| Point-in-time database restore from Business Critical tier to General Purpose tier doesn't succeed if source database contains in-memory OLTP objects. | | Resolved | Oct 2019 |
| Database mail feature with external (non-Azure) mail servers using secure connection | | Resolved | Oct 2019 |
| Contained databases not supported in SQL Managed Instance | | Resolved | Aug 2019 |

## Has workaround

### Modifying backup retention period for the free offer

You can only modify the backup retention policy of your databases in the free SQL managed instance by using [REST API](/rest/api/sql/managed-backup-short-term-retention-policies), [PowerShell](/powershell/module/az.sql/set-azsqlinstancedatabasebackupshorttermretentionpolicy), and [Azure CLI](/cli/azure/sql/midb/short-term-retention-policy) commands. You can't modify the backup retention policy through the [Azure portal](https://portal.azure.com).

### Login to read-secondary failed due to long wait on "HADR_DATABASE_WAIT_FOR_TRANSITION_TO_VERSIONING"

You might see this error as an exception for the **Microsoft OLE DB Driver 19 for SQL Server** driver when you try to connect to a read-only secondary replica of a [failover group](failover-group-sql-mi.md), or a database replicated through the [Managed Instance link](managed-instance-link-feature-overview.md).

This error occurs when the secondary replica isn't available for logins because row versions are missing for transactions that were in-flight when a secondary replica restarted or recycled, whether for maintenance or due to a failover. When an instance restarts or fails over, the versioning data stored in `tempdb` is cleared. Secondary read queries are aborted if there are long-running active transactions that started before the failover or restart.

To resolve this issue, roll back or commit active transactions on the primary replica. To avoid this error, minimize long-running write transactions on the primary replica.


### Error 8992 when running DBCC CHECKDB on a SQL Server database that originated from SQL Managed Instance

You might see the following error when you run the `DBCC CHECKDB` command on a SQL Server 2022 database after you delete an index, or a table with an index, and the database originated from Azure SQL Managed Instance, such as after restoring a backup file, or from the [SQL Managed Instance link feature](managed-instance-link-feature-overview.md):

```output
Msg 8992, Level 16, State 1, Line <Line_Number>
Check Catalog Msg 3853, State 1: Attribute (%ls) of row (%ls) in sys.sysrowsetrefs does not have a matching row (%ls) in sys.indexes.
```

To work around the issue, first drop the index, or the table with the index, from the source database in Azure SQL Managed Instance, and then restore, or link, the database to SQL Server 2022 again. If recreating the database from the source Azure SQL Managed Instance isn't possible, contact Microsoft support to help resolve this issue.

> [!CAUTION]  
> If you create a partitioned index on a table after dropping an index as described in this scenario, the table becomes inaccessible.

### List of long-term backups in Azure portal shows backup files for active and deleted databases with the same name

Long-term backups can be listed and managed on Azure portal page for an Azure SQL Managed Instance on the *Backups* tab. The page lists active or deleted databases, basic information about their long-term backups, and link for managing backups. When you select the *Manage* link, a new side pane opens with list of backups. Due to an issue with the filtering logic, the list shows backups for both active database and deleted databases with the same name. This requires a special attention when selecting backups for deletion, to avoid deleting backups for a wrong database.

**Workaround**: Use displayed *Backup time (UTC)* information in the list to differentiate backups belonging to databases with the same name that existed on the instance at different periods. Alternatively, use PowerShell commands [Get-AzSqlInstanceDatabaseLongTermRetentionBackup](/powershell/module/az.sql/get-azsqlinstancedatabaselongtermretentionbackup) and [Remove-AzSqlInstanceDatabaseLongTermRetentionBackup](/powershell/module/az.sql/remove-azsqlinstancedatabaselongtermretentionbackup), or CLI commands [az sql midb ltr-backup list](/cli/azure/sql/midb/ltr-backup?view=azure-cli-latest&preserve-view=true#az-sql-midb-ltr-backup-list) and [az sql midb ltr-backup delete](/cli/azure/sql/midb/ltr-backup?view=azure-cli-latest&preserve-view=true#az-sql-midb-ltr-backup-delete) to manage long-term backups using the *DatabaseState* parameter and *DatabaseDeletionTime* return value to filter backups for a database.

<a id="procedure-sp_send_dbmail-may-fail-when-query-parameter-is-used-on-nov22fw-enabled-managed-instances"></a>

### Procedure sp_send_dbmail might fail when *@query* parameter is used

The `sp_send_dbmail` stored procedure might fail when the `@query` parameter is used. Failures happen when the stored procedure is executed under a **sysadmin** account.

This problem is caused by a known bug related to how `sp_send_dbmail` uses impersonation.

**Workaround**: Make sure you call `sp_send_dbmail` under an appropriate custom account you create, and not under a **sysadmin** account.

Here's an example of how you can create a dedicated account and modify existing objects that send email via `sp_send_dbmail`.

```sql
USE [msdb];
GO

-- Step 1: Create a user mapped to a login to specify as a runtime user.
CREATE USER [user_name] FOR LOGIN [login_name];
GO

EXECUTE msdb.dbo.sp_update_jobstep
    @job_name = N'db_mail_sending_job',
    @step_id = db_mail_sending_job_id,
    @database_user_name = N'user_name';
GO

-- Step 2: Grant DB Mail permissions to the user who created it.
ALTER ROLE [DatabaseMailUserRole] ADD MEMBER [user_name];
GO

-- Step 3: If the database of the job step is not msdb, the permission error cannot be avoided even if it is a member of the role, so set it to msdb.
EXECUTE msdb.dbo.sp_update_jobstep
    @job_name = N'db_mail_sending_job',
    @step_id = db_mail_sending_job_id,
    @database_name = N'msdb';
GO

-- Step 4: Set a principal in the email profile
EXECUTE msdb.dbo.sysmail_add_principalprofile_sp
    @principal_name = N'user_name',
    @profile_name = N'profile_name',
    @is_default = 0;
GO
```

<a id="procedure-sp_send_dbmail-may-transiently-fail-when-query-parameter-is-used"></a>

### Distributed transactions can be executed after removing SQL managed instance from Server Trust Group

[Server Trust Groups](server-trust-group-overview.md) are used to establish trust between managed instances that is prerequisite for executing [distributed transactions](../database/elastic-transactions-overview.md). After removing the SQL managed instance from Server Trust Group or deleting the group, you might still be able to execute distributed transactions. To make sure that distributed transactions are disabled, perform a [user-initiated manual failover](user-initiated-failover.md) on the SQL managed instance.

### Can't create SQL Managed Instance with the same name as logical server previously deleted

When you create a [logical server in Azure](../database/logical-servers.md) for Azure SQL Database or a SQL Managed Instance, the system creates a DNS record for `<name>.database.windows.com`. This DNS record must be unique. If you create a logical server for SQL Database and then delete it, the name remains reserved for seven days. During this period, you can't create a SQL Managed Instance with the same name as the deleted logical server. As a workaround, use a different name for the SQL Managed Instance, or create a support ticket to release the logical server name.

<a id="service-principal-cant-access-azure-ad-and-akv"></a>

### Service Principal can't access Microsoft Entra ID and AKV

In some circumstances, an issue exists with the Service Principal used to access Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) and Azure Key Vault (AKV) services. As a result, this issue impacts usage of Microsoft Entra authentication and transparent data encryption (TDE) with SQL Managed Instance. You might experience this problem as an intermittent connectivity issue, or not being able to run statements such are `CREATE LOGIN/USER FROM EXTERNAL PROVIDER` or `EXECUTE AS LOGIN/USER`. Setting up TDE with customer-managed key on a new Azure SQL Managed Instance might also not work in some circumstances.

**Workaround**: To prevent this issue from occurring on your SQL Managed Instance, before executing any update commands, or in case you already experienced this issue after update commands, go to the **Overview page** of your SQL managed instance in the Azure portal. Under **Settings**, select **Microsoft Entra ID** to access the SQL Managed Instance [Microsoft Entra ID admin page](../database/authentication-aad-configure.md#azure-sql-managed-instance). Look for the following error message:

```output
Managed Instance needs a Service Principal to access Microsoft Entra ID. Click here to create a Service Principal.
```

If you encounter this error message, select it, and follow the step-by-step instructions provided until this error is resolved.

### Wrong error returned while trying to remove a file that isn't empty

SQL Server and SQL Managed Instance [don't allow a user to drop a file that isn't empty](/sql/relational-databases/databases/delete-data-or-log-files-from-a-database#Prerequisites). If you try to remove a nonempty data file by using an `ALTER DATABASE REMOVE FILE` statement, the error:

```output
Msg 5042 - The file '<file_name>' cannot be removed because it is not empty` isn't immediately returned. SQL Managed Instance keeps trying to drop the file, and the operation fails after 30 minutes with `Internal server error.
```

### Change service tier and create instance operations are blocked by ongoing database restore

An ongoing `RESTORE` statement, a Data Migration Service migration process, and built-in point-in-time restore can block updates to a service tier, or resizing the existing instance and creating new instances, until the restore process finishes.

The restore process blocks these operations on the managed instances and instance pools in the same subnet where the restore process is running. The instances in instance pools aren't affected. Create or change service tier operations don't fail or time out. They proceed once the restore process is completed or canceled.

**Workaround**: Wait until the restore process finishes, or cancel the restore process if the creation or update-service-tier operation has higher priority.

<a id="resource-governor-on-a-readable-secondary-replica-needs-to-be-reconfigured-after-its-failover"></a>

### Resource Governor on a readable secondary replica needs reconfiguration after failover

The [Resource governor](/sql/relational-databases/resource-governor/resource-governor) feature that enables you to limit the resources assigned to the user workload might incorrectly classify some user workloads after failover or a user-initiated change of service tier (for example, the change of max vCore or max instance storage size).

**Workaround**: Run `ALTER RESOURCE GOVERNOR RECONFIGURE` periodically or as part of a SQL Agent job that executes the SQL task when the readable secondary replica starts if you're using [Resource governor](/sql/relational-databases/resource-governor/resource-governor).

<a id="cross-database-service-broker-dialogs-must-be-reinitialized-after-service-tier-upgrade"></a>

### Exceeding storage space with small database files

`CREATE DATABASE`, `ALTER DATABASE ADD FILE`, and `RESTORE DATABASE` statements might fail because the instance reaches the Azure Storage limit on the General Purpose service tier, but not the [Next-gen General Purpose service tier upgrade](service-tiers-next-gen-general-purpose-use.md) or Business Critical service tier.

Each General Purpose instance of SQL Managed Instance has up to 35 TB of storage reserved for Azure Premium Disk space. Each database file is placed on a separate physical disk. Disk sizes can be 128 GB, 256 GB, 512 GB, 1 TB, or 4 TB. You aren't charged for unused space on the disk, but the total sum of Azure Premium Disk sizes can't exceed 35 TB. In some cases, a SQL managed instance that doesn't need 8 TB in total might exceed the 35 TB Azure limit on storage size due to internal fragmentation.

For example, a General Purpose instance of SQL Managed Instance might have one large file that's 1.2 TB in size placed on a 4-TB disk. It also might have 248 files that are 1 GB each and that are placed on separate 128-GB disks. In this example:

- The total allocated disk storage size is 1 x 4 TB + 248 x 128 GB = 35 TB.
- The total reserved space for databases on the instance is 1 x 1.2 TB + 248 x 1 GB = 1.4 TB.

This example illustrates that under certain circumstances, due to a specific distribution of files, an instance of SQL Managed Instance might reach the 35-TB limit that's reserved for an attached Azure Premium Disk, when you might not expect it.

In this example, existing databases continue to work and can grow without any problem as long as new files aren't added. You can't create or restore new databases because there isn't enough space for new disk drives, even if the total size of all databases doesn't reach the instance size limit. The error that's returned isn't clear.

You can [identify the number of remaining files](https://medium.com/azure-sqldb-managed-instance/how-many-files-you-can-create-in-general-purpose-azure-sql-managed-instance-e1c7c32886c1) by using system views. If you reach this limit, try to [empty and delete some of the smaller files by using the DBCC SHRINKFILE statement](/sql/t-sql/database-console-commands/dbcc-shrinkfile-transact-sql?view=azuresqlmi-current&preserve-view=true#d-emptying-a-file) or switch to the [Business Critical tier, which doesn't have this limit](resource-limits.md#service-tier-characteristics).

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

## No resolution

### Misleading error message when connecting to a read replica using invalid credentials

When you attempt to connect to a Business Critical tier instance's read-secondary replica by using `ApplicationIntent=ReadOnly` and invalid credentials, the instance reports an error indicating that the `master` database is read-only. The instance doesn't correctly report that the credentials are invalid.

### Differential backups aren't taken when an instance is linked to SQL Server

When you configure a [link](managed-instance-link-feature-overview.md) between SQL Server and Azure SQL Managed Instance, automated full and transaction log backups are taken on the SQL managed instance, whether or not it's in the primary role. However, differential backups aren't currently taken, when can lead to longer than expected restore times.

### Increased number of system logins used for transactional replication

Azure SQL Managed Instance service creates a system login for purposes of transactional replication. You can find this login in SSMS (in **Object Explorer** > **Security** > **Logins**) or in the `sys.syslogins` system view. The login name format looks like `DBxCy\WF-abcde01234QWERT`, and the login has the **public** server role. Under certain conditions, this login is recreated, and due to an internal issue, the previous login isn't deleted. This fault can lead to an increase in the number of logins. These logins don't represent a security threat, and you can safely ignore them. Don't delete these logins, because at least one of them is being used for transactional replication.

<a id="azure-ad-logins-and-users-arent-supported-in-ssdt"></a>

### Microsoft Entra logins and users aren't supported in SSDT

SQL Server Data Tools don't fully support Microsoft Entra logins and users.

<a id="impersonation-of-azure-ad-login-types-isnt-supported"></a>

### Impersonation of Microsoft Entra login types isn't supported

Impersonation using `EXECUTE AS USER` or `EXECUTE AS LOGIN` isn't supported for the following Microsoft Entra principals:

- Aliased Microsoft Entra users. In this case, the operation returns error `15517`.
- Microsoft Entra logins and users based on Microsoft Entra applications or service principals. In this case, the operation returns errors `15517` and `15406`.

### Transactional replication must be reconfigured after geo-failover

If you enable transactional replication on a database in a failover group, the SQL Managed Instance administrator must clean up all publications on the old primary and reconfigure them on the new primary after a failover to another region occurs. For more information, see [Replication](transact-sql-tsql-differences-sql-server.md#replication).

### Error logs aren't persisted

Error logs that are available in SQL Managed Instance aren't persisted, and their size isn't included in the maximum storage limit. Error logs might be automatically erased if failover occurs. Gaps might exist in the error log history because SQL Managed Instance was moved several times on several virtual machines.

## Resolved

### Interim guidance on 2024 time zone updates for Paraguay

**(Resolved in February 2026)**

On October 14, 2024, the Paraguayan government announced a permanent change to the time zone policy. Paraguay now remains on Daylight Saving Time (DST) year-round, effectively adopting UTC-3 as its standard time. As a result, clocks did not advance by 60 minutes at 12:00 a.m. on March 23, 2025, as previously scheduled. This change affects the Paraguay Standard time zone. Microsoft has released related [Windows updates in February and March 2025](https://techcommunity.microsoft.com/blog/dstblog/paraguay-2025-time-zone-update-now-available/4386720). SQL managed instances using the affected time zone reflect this change, and align to to the new UTC-3 offset.

### Changing the connection type doesn't affect connections through the failover group endpoint

**(Resolved in November 2025)**

If an instance participates in a [failover group](failover-group-sql-mi.md), changing the instance's [connection type](connection-types-overview.md) doesn't take effect for the connections established through the failover group listener endpoint.

### Temporary instance inaccessibility using the failover group listener during scaling operation

**(Resolved in April 2025)**

Scaling SQL managed instance sometimes requires moving the instance to a different virtual cluster, along with the associated service-maintained DNS records. If the SQL managed instance participates in a failover group, the DNS record corresponding to its associated failover group listener (read-write listener, if the instance is the current geo-primary read-only listener, if the instance is the current geo-secondary) is moved to the new virtual cluster.

In the current scaling operation design, the listener DNS records are removed from the originating virtual cluster before it fully migrates the SQL managed instance to the new virtual cluster. In some situations, this design leads to prolonged time during which the instance's IP address can't be resolved by using the listener. During this time, a SQL client attempting to access the instance being scaled by using the listener endpoint can expect login failures with the following error message:

```output
Error 40532: Cannot open server "xxx.xxx.xxx.xxx" requested by the login. The login failed. (Microsoft SQL Server, Error: 40532).
```

The issue will be addressed through scaling operation redesign.

<a id="msdb-table-for-manual-backups-doesnt-preserve-the-username"></a>

### Table for manual backups in msdb doesn't preserve the username

**(Resolved in August 2023)** The recently introduced support for auto backups in `msdb` doesn't currently contain username information.

<a id="when-you-use-sql-server-authentication-usernames-with-at-sign-arent-supported"></a>
<a id="when-using-sql-server-authentication-usernames-with--arent-supported"></a>

### When you use SQL Server authentication, usernames with '@' aren't supported

Usernames that contain the `@` symbol in the middle (for example, `abc@xy`) can't sign in using SQL Server authentication.

### Restoring manual backup without CHECKSUM might fail

**(Resolved in June 2020)** In certain circumstances, restoring manual backup of databases that you made on a SQL managed instance without `CHECKSUM` might fail. In such cases, retry restoring the backup until you're successful.

**Workaround**: Take manual backups of databases on SQL managed instances with `CHECKSUM` enabled.

### Permissions on resource group not applied to SQL Managed Instance

When you apply the SQL Managed Instance Contributor Azure role to a resource group (RG), it doesn't apply to SQL Managed Instance and has no effect.

**Workaround**: Set up a SQL Managed Instance Contributor role for users at the subscription level.

<a id="query-parameter-not-supported-in-sp_send_db_mail"></a>

### Misleading error message on Azure portal suggesting recreation of the Service Principal

The **Active Directory admin** page of Azure portal for Azure SQL Managed Instance might show the following error message, even though Service Principal already exists:

```output
Managed Instance needs a Service Principal to access Microsoft Entra ID. Click here to create a Service Principal.
```

You can neglect this error message if Service Principal for the SQL managed instance already exists, and/or Microsoft Entra authentication on the SQL managed instance works.

To check whether Service Principal exists, navigate to the **Enterprise applications** page on the Azure portal, choose **Managed Identities** from the **Application type** dropdown list, select **Apply**, and type the name of the SQL managed instance in the search box. If the instance name shows up in the result list, Service Principal already exists and no further actions are needed.

If you already followed the instructions from the error message and selected the link, the Service Principal of the SQL managed instance is recreated. In that case, assign Microsoft Entra ID read permissions to the newly created Service Principal in order for Microsoft Entra authentication to work properly. You can also run this step with Azure PowerShell by following the [instructions](../database/authentication-aad-configure.md?tabs=azure-powershell#assign-microsoft-graph-permissions).

<a id="the-event_file-target-of-the-system_health-event-session-is-not-accessible"></a>

### The event_file target of the system_health event session isn't accessible

**(Partially resolved in May 2025)** When you attempt to read the contents of the `event_file` target of the `system_health` event session, you get error 40538, "A valid URL beginning with `https://` is required as value for any filepath specified."

Originally, this issue occurred in SQL Server Management Studio (SSMS), or when reading the session data using the [sys.fn_xe_file_target_read_file](/sql/relational-databases/system-functions/sys-fn-xe-file-target-read-file-transact-sql) function.

In May 2025, this issue was resolved for reading session data from SSMS. The issue isn't resolved when reading event data by using the `sys.fn_xe_file_target_read_file` function.

This change in behavior is an unintended consequence of a required security fix. You can work around this issue by creating your own equivalent of the `system_health` session with an `event_file` target in Azure Blob Storage. For more information, including a T-SQL script to create the `system_health` session that can be modified to create your own equivalent of `system_health`, see [Use the system_health session](/sql/relational-databases/extended-events/use-the-system-health-session).

### Cross-database Service Broker dialogs need reinitialization after service tier upgrade

**(Resolved in January 2020)** Cross-database Service Broker dialogs stop delivering the messages to the services in other databases after change service tier operation. The messages *aren't lost*, and they can be found in the sender queue. Any change of vCores or instance storage size in SQL Managed Instance causes a `service_broke_guid` value in [sys.databases](/sql/relational-databases/system-catalog-views/sys-databases-transact-sql) view to change for all databases. Any `DIALOG` created using a [BEGIN DIALOG](/sql/t-sql/statements/begin-dialog-conversation-transact-sql) statement that references Service Brokers in other databases stops delivering messages to the target service.

**Workaround**: Stop any activity that uses cross-database Service Broker dialog conversations before updating a service tier, and reinitialize them afterward. If undelivered messages remain after a service tier change, read the messages from the source queue and resend them to the target queue.

## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/sql/sql-server/sql-server-docs-contribute).

## Related content

- For a list of SQL Managed Instance updates and improvements, see [SQL Managed Instance service updates](https://azure.microsoft.com/updates/?product=sql-database&query=sql%20managed%20instance).

- For updates and improvements to all Azure services, see [Service updates](https://azure.microsoft.com/updates).
