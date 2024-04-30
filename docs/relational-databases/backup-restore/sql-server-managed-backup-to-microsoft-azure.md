---
title: "SQL Server managed backup to Microsoft Azure"
description: SQL Server Managed Backup to Microsoft Azure manages and automates SQL Server backups to Microsoft Azure Blob storage.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 04/18/2024
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
---
# SQL Server managed backup to Microsoft Azure

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-windows-only.md)]

[!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] manages and automates SQL Server backups to Microsoft Azure Blob storage. You can choose to allow SQL Server to determine the backup schedule based on the transaction workload of your database, or use advanced options to define a schedule. The retention settings determine how long the backups are stored in Azure Blob storage. [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] supports point in time restore for the retention time period specified.

> [!NOTE]  
> In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], the procedures and underlying behavior of [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] have changed. For more information, see [Migrate managed backup settings](migrate-sql-server-2014-managed-backup-settings-to-sql-server-2016.md).

[!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] is recommended for SQL Server instances running on Microsoft Azure virtual machines.

## Benefits

Currently automating backups for multiple databases requires developing a backup strategy, writing custom code, and scheduling backups. Using [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)], you can create a backup plan by specifying only the retention period and storage location. Although advanced settings are available, they aren't required. [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] schedules, performs, and maintains the backups.

[!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] can be configured at the database level or at the SQL Server instance level. When configured at the instance level, any new databases are also backed up automatically. Settings at the database level can be used to override instance level defaults on an individual case.

You can also encrypt the backups for added security, and you can set up a custom schedule to control when the backups are taken. For more information on the benefits of using Microsoft Azure Blob storage for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backups, see [SQL Server backup and restore with Azure Blob Storage](sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md).

## <a id="Prereqs"></a> Prerequisites

Microsoft Azure Storage is used by [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] to store the backup files. The following prerequisites are required:

| Prerequisite | Description |
| --- | --- |
| **Microsoft Azure Account** | You can get started with Azure with a [free trial](https://azure.microsoft.com/pricing/free-trial/) before exploring [purchase options](https://azure.microsoft.com/pricing/purchase-options/). |
| **Azure Storage account** | The backups are stored in Azure Blob Storage associated with an Azure storage account. For step-by-step instructions to create a storage account, see [Create a storage account](/azure/storage/common/storage-account-create). |
| **Blob container** | Blobs are organized in containers. You specify the target container for the backup files. You can create a container in the [Azure Management Portal](https://portal.azure.com/), or you use the `New-AzureStorageContainer`[Azure PowerShell](/powershell/azure/) command. |
| **Shared access signature (SAS)** | Access to the target container is controlled by a Shared Access Signature (SAS). For an overview of SAS, see [Grant limited access to Azure Storage resources using shared access signatures (SAS)](/azure/storage/common/storage-sas-overview). You can create a SAS token in code or with the `New-AzureStorageContainerSASToken` PowerShell command. For a PowerShell script that simplifies this process, see [Simplifying creation of SQL Credentials with Shared Access Signature ( SAS ) tokens on Azure Storage with PowerShell](/archive/blogs/sqlcat/simplifying-creation-of-sql-credentials-with-shared-access-signature-sas-tokens-on-azure-storage-with-powershell). The SAS token can be stored in a **SQL Credential** for use with [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)]. |
| **SQL Server Agent** | SQL Server Agent must be running for [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] to work. Consider setting the startup option to automatic. |

## Components

Transact-SQL is the main interface to interact with [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)]. System stored procedures are used for enabling, configuring, and monitoring [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)]. System functions are used to retrieve existing configuration settings, parameter values, and backup file information. Extended events are used to surface errors and warnings. Alert mechanisms are enabled through SQL Agent jobs and SQL Server Policy Based Management. The following list of objects include a description of their functionality in relation to [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)].

PowerShell cmdlets are also available to configure [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)]. SQL Server Management Studio supports restoring backups created by [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] by using the **Restore Database** task.

| System object | Description |
| --- | --- |
| `msdb` | Stores the metadata, backup history for all the backups created by [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)]. |
| [managed_backup.sp_backup_config_basic](../system-stored-procedures/managed-backup-sp-backup-config-basic-transact-sql.md) | Enables [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)]. |
| [managed_backup.sp_backup_config_advanced](../system-stored-procedures/managed-backup-sp-backup-config-advanced-transact-sql.md) | Configures advanced settings for [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)], such as encryption. |
| [managed_backup.sp_backup_config_schedule](../system-stored-procedures/managed-backup-sp-backup-config-schedule-transact-sql.md) | Creates a custom schedule for [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)]. |
| [managed_backup.sp_ backup_master_switch](../system-stored-procedures/managed-backup-sp-backup-master-switch-transact-sql.md) | Pauses and resumes [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)]. |
| [managed_backup.sp_set_parameter](../system-stored-procedures/managed-backup-sp-set-parameter-transact-sql.md) | Enables and configures monitoring for [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)]. Examples: enabling extended events, mail settings for notifications. |
| [managed_backup.sp_backup_on_demand](../system-stored-procedures/managed-backup-sp-backup-on-demand-transact-sql.md) | Performs an ad hoc backup for a database that is enabled to use [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] without breaking the log chain. |
| [managed_backup.fn_backup_db_config](../system-functions/managed-backup-fn-backup-db-config-transact-sql.md) | Returns the current [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] status and configuration values for a database, or for all the databases on the instance. |
| [managed_backup.fn_is_master_switch_on](../system-functions/managed-backup-fn-is-master-switch-on-transact-sql.md) | Returns the status of the master switch. |
| [managed_backup.sp_get_backup_diagnostics](../system-stored-procedures/managed-backup-sp-get-backup-diagnostics-transact-sql.md) | Returns the events logged by Extended Events. |
| [managed_backup.fn_get_parameter](../system-functions/managed-backup-fn-get-parameter-transact-sql.md) | Returns the current values for backup system settings such as monitoring and mail settings for alerts. |
| [managed_backup.fn_available_backups](../system-functions/managed-backup-fn-available-backups-transact-sql.md) | Retrieves available backups for a specified database or for all the databases in an instance. |
| [managed_backup.fn_get_current_xevent_settings](../system-functions/managed-backup-fn-get-current-xevent-settings-transact-sql.md) | Returns the current extended event settings. |
| [managed_backup.fn_get_health_status](../system-functions/managed-backup-fn-get-health-status-transact-sql.md) | Returns the aggregated counts of errors logged by Extended Events for a specified period. |

## Backup strategy

The following sections describe a backup strategy for [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)].

### Backup scheduling

You can specify a custom backup schedule using the system stored procedure [managed_backup.sp_backup_config_schedule](../system-stored-procedures/managed-backup-sp-backup-config-schedule-transact-sql.md). If you don't specify a custom schedule, the type of backups scheduled and the backup frequency is determined based on the workload of the database. The retention period settings are used to determine the length of time a backup file should be retained in the storage and the ability to recover the database to a point-in-time within the retention period.

### Backup file naming conventions

[!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] uses the container that you specify, so you have control over the name of the container. For the backup files, non availability databases are named using the following convention: The name is created using the first 40 characters of the database name, the database GUID without the `-`, and the timestamp. The underscore character is inserted between segments as separators. The `.bak` file extension is used for full backup and `.log` for log backups. For databases in an availability group (AG), in addition to the file naming convention described previously, the AG database GUID is added after the 40 characters of the database name. The AG database GUID value is the value for group_database_id in `sys.databases`.

### Full database backup

[!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] agent schedules a full database backup if any of the following conditions is true.

- A database is [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] enabled for the first time, or when [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] is enabled with default settings at the instance level.

- The log growth since last full database backup is equal to or larger than 1 GB.

- The maximum time interval of one week has passed since the last full database backup.

- The log chain is broken. [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] periodically checks to see whether the log chain is intact by comparing the first and last LSNs of the backup files. If there's break in the log chain for any reason, [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] schedules a full database backup. The most common reason for log chain breaks is probably a backup command issued using Transact-SQL or through the Backup task in SQL Server Management Studio. Other common scenarios include accidental deletion of the backup log files, or accidental overwrites of backups.

### Transaction log backup

[!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] schedules a log backup if any of the following conditions is true:

- No log backup history can be found. This is usually true when [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] is enabled for the first time.

- The transaction log space used is 5 MB or larger.

- The maximum time interval of 2 hours since the last log backup is reached.

- Anytime the transaction log backup is lagging behind a full database backup. The goal is to keep the log chain ahead of full backup.

## Retention period settings

When enabling backup, you must set the retention period in days: The minimum is 1 day, and maximum is 90 days.

[!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] based on the retention period settings, assesses the ability to recover to a point in time in the specified time, to determine what backup files to keep and identifying the backup files to delete. The backup_finish_date of the backup is used to determine and match the time specified by the retention period settings.

## Considerations

For a database, if there's an existing full database backup job running, then [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] waits for the current job to be completed before doing another full database backup for the same database. Similarly, only one transaction log backup can be running at a given time. However, a full database backup and a transaction log backup can run concurrently. Failures are logged as Extended Events.

If more than 10 concurrent full database backups are scheduled, a warning is issued through the debug channel of Extended Events. [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] then maintains a priority queue for the remaining databases that require a backup until the all backups are scheduled and completed.

> [!NOTE]  
> SQL Server managed backup isn't supported with proxy servers.
>

## <a id="support_limits"></a> Supportability

The following support limitations and considerations are specific to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]:

- Backup of `master`, `model`, and `msdb` system databases is supported. Backup of `tempdb` isn't supported.

- All recovery models are supported (Full, Bulk-logged, and Simple).

- [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] agent only supports database full and log backups. File backup automation isn't supported.

- The Microsoft Azure Blob Storage is the only supported backup storage option. Backups to disk or tape aren't supported.

- [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] uses the Backup to Block Blob feature. The maximum size of a block blob is 200 GB. But by utilizing striping, the maximum size of an individual backup can be up to 12 TB. If your backup requirements exceed this limit, consider using compression, and test the backup file size before setting up [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)]. You can either test by backing up to a local disk or manually backing up to Microsoft Azure storage using the `BACKUP TO URL` Transact-SQL statement. For more information, see [SQL Server backup to URL for Microsoft Azure Blob Storage](sql-server-backup-to-url.md).

- [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] might have some limitations when it's configured with other technologies supporting backup, high availability, or disaster recovery.

- Backups of databases in an availability group are [copy-only backups](copy-only-backups-sql-server.md).

- Backups of databases on secondary replica's in an availabiity group need to be readable in order to stripe to multiple files. 

## Related content

- [Enable SQL Server managed backup to Azure](enable-sql-server-managed-backup-to-microsoft-azure.md)
- [Configure advanced options for SQL Server managed backup to Microsoft Azure](configure-advanced-options-for-sql-server-managed-backup-to-microsoft-azure.md)
- [Disable SQL Server Managed Backup to Microsoft Azure](disable-sql-server-managed-backup-to-microsoft-azure.md)
- [Back up and restore: System databases (SQL Server)](back-up-and-restore-of-system-databases-sql-server.md)
- [Back Up and Restore of SQL Server Databases](back-up-and-restore-of-sql-server-databases.md)
