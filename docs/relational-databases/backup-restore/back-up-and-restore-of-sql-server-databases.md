---
title: Back up and Restore of SQL Server Databases
description: This article describes the benefits of backing up SQL Server databases and introduces backup and restore strategies and security considerations.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/27/2026
ms.service: sql
ms.subservice: backup-restore
ms.topic: concept-article
ms.update-cycle: 1825-days
helpviewer_keywords:
  - "disaster recovery [SQL Server], see restoring [SQL Server]"
  - "backups [SQL Server]"
  - "restoring databases [SQL Server]"
  - "backup [SQL Server], see backing up [SQL Server]"
  - "databases [SQL Server], restoring"
  - "backing up databases [SQL Server]"
  - "restore [SQL Server], see restoring [SQL Server]"
  - "disaster recovery [SQL Server], see backing up [SQL Server]"
  - "backing up [SQL Server]"
  - "Database Engine [SQL Server], backups"
  - "databases [SQL Server], backups"
---
# Back up and restore SQL Server databases

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes the benefits of backing up [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] databases, introduces basic backup and restore terms, and covers backup and restore strategies and security considerations for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!NOTE]  
> This article introduces SQL Server backups. For specific steps to back up SQL Server databases, see [Creating backups](#creating-backups).

The SQL Server backup and restore component provides an essential safeguard for critical data stored in your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] databases. To minimize the risk of catastrophic data loss, back up your databases regularly to preserve modifications to your data. A well-planned backup and restore strategy helps protect databases against data loss caused by many types of failure. Test your strategy by restoring a set of backups and then recovering your database, so you're ready to respond to a disaster.

In addition to local storage, SQL Server also supports backup to and restore from Azure Blob Storage. For more information, see [SQL Server backup and restore with Azure Blob Storage](sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md). For database files stored using Azure Blob Storage, [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] provides the option to use Azure snapshots for nearly instantaneous backups and faster restores. For more information, see [File-snapshot backups for database files in Azure](file-snapshot-backups-for-database-files-in-azure.md). Azure also offers an enterprise-class backup solution for SQL Server running in Azure VMs. A fully managed backup solution, it supports Always On availability groups, long-term retention, point-in-time recovery, and central management and monitoring. For more information, see [About SQL Server backup on Azure VMs](/azure/backup/backup-azure-sql-database).

## Why back up?

- Backing up your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] databases, running test restore procedures on your backups, and storing copies of backups in a safe, off-site location protects you from potentially catastrophic data loss. **Backing up is the only way to protect your data.**

  With valid backups of a database, you can recover your data from many failures, such as:

  - Media failure.

  - User errors, for example, dropping a table by mistake.

  - Hardware failures, for example, a damaged disk drive or permanent loss of a server.

  - Natural disasters. By using SQL Server Backup to Azure Blob Storage, you can create an off-site backup in a different region from your on-premises location, to use if a natural disaster affects your on-premises location.

- Additionally, backups of a database are useful for routine administrative purposes, such as copying a database from one server to another, setting up [!INCLUDE [ssHADR](../../includes/sshadr-md.md)] or database mirroring, and archiving.

## Glossary of backup terms

| Term | Definition |
| --- | --- |
| **back up** *[verb]* | The process of creating a **backup** *[noun]* by copying data records from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database or log records from its transaction log. |
| **backup** *[noun]* | A copy of data that you can use to restore and recover the data after a failure. Backups of a database can also be used to restore a copy the database to a new location. |
| **backup device** | A disk or tape device to which SQL Server backups are written and from which they can be restored. SQL Server backups can also be written to an Azure Blob Storage, and **URL** format is used to specify the destination and the name of the backup file. For more information, see [SQL Server backup and restore with Azure Blob Storage](sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md). |
| **backup media** | One or more tapes or disk files to which one or more backups have been written. |
| **data backup** | A backup of data in a complete database (a database backup), a partial database (a partial backup), or a set of data files or filegroups (a file backup). |
| **database backup** | A backup of a database. Full database backups represent the whole database at the time the backup finished. Differential database backups contain only changes made to the database since its most recent full database backup. |
| **differential backup** | A data backup that is based on the latest full backup of a complete or partial database or a set of data files or filegroups (the differential base) and that contains only the data that has changed since that base. |
| **full backup** | A data backup that contains all the data in a specific database or set of filegroups or files, and also enough log to allow for recovering that data. |
| **log backup** | A backup of transaction logs that includes all log records that weren't backed up in a previous log backup (full recovery model). |
| **recover** | To return a database to a stable and consistent state. |
| **recovery** | A phase of database startup or of a restore with recovery that brings the database into a transaction-consistent state. |
| **recovery model** | A database property that controls transaction log maintenance on a database. There are three recovery models: basic, full, and bulk-logged. A database's recovery model determines its backup and restore requirements. |
| **restore** | A multiphase process that copies all the data and log pages from a specified [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup to a specified database, and then rolls forward all the transactions that are logged in the backup by applying logged changes to bring the data forward in time. |

## Backup and restore strategies

You must customize backup and restore strategies to your environment and available resources. Reliable recovery requires a backup and restore strategy. A well-designed strategy balances the business requirements for maximum data availability and minimum data loss against the cost of maintaining and storing backups.

A backup and restore strategy contains a backup portion and a restore portion. The backup portion defines the type and frequency of backups, the type and speed of the hardware they require, how to test backups, and where and how to store backup media (including security considerations). The restore portion defines who's responsible for performing restores, how to perform restores to meet your goals for database availability and minimum data loss, and how to test restores.

An effective backup and restore strategy requires careful planning, implementation, and testing. Testing is required. You don't have a backup strategy until you successfully restore backups in every combination included in your restore strategy and test each restored database for physical consistency. Consider several factors, including:

- Your organization's goals regarding your production databases, especially the requirements for availability and protecting data from loss or damage.

- The nature of each database: its size, its usage patterns, the nature of its content, the requirements for its data, and so on.

- Constraints on resources, such as hardware, personnel, space for storing backup media, the physical security of the stored media, and so on.

## Best practice recommendations

Don't grant the accounts that perform backup or restore operations more privileges than necessary. For more information, see [backup](../../t-sql/statements/backup-transact-sql.md#permissions) and [restore](../../t-sql/statements/restore-statements-transact-sql.md#permissions) for specific permission details. [Encrypt](../security/encryption/transparent-data-encryption.md) database backups and, if possible, [compress](backup-compression-sql-server.md) them.

Use consistent file extensions to make backups easier to identify and manage. SQL Server doesn't require or enforce these extensions, but consistency helps with operational tasks such as configuring antivirus exclusions for backup files. For more information, see [Configure antivirus software to work with SQL Server](/troubleshoot/sql/database-engine/security/antivirus-and-sql-server).

- Database backup files should have the `.BAK` extension.
- Log backup files should have the `.TRN` extension.

### Use separate storage

Place your database backups in a separate physical location or on a separate device from the database files. When the physical drive that stores your databases fails or crashes, recovery depends on your ability to access the separate drive or remote device that stored the backups. You can create several logical volumes or partitions from the same physical disk drive. Carefully review the disk partition and logical volume layouts before you choose a storage location for the backups.

### Choose appropriate recovery model

Backup and restore operations occur within the context of a [recovery model](recovery-models-sql-server.md). A recovery model is a database property that controls how the transaction log is managed. Therefore, a database's recovery model determines what types of backup and restore scenarios the database supports, and the size of its transaction log backups. Typically, a database uses either the simple recovery model or the full recovery model. You can augment the full recovery model by switching to the bulk-logged recovery model before bulk operations. For an introduction to these recovery models and how they affect transaction log management, see [the transaction log](../logs/the-transaction-log-sql-server.md).

The best choice of database recovery model depends on your business requirements. To avoid transaction log management and simplify backup and restore, use the simple recovery model. To minimize work-loss exposure at the cost of administrative overhead, use the full recovery model. To minimize the effect on log size during bulk-logged operations while still allowing recovery of those operations, use the bulk-logged recovery model. For information about the effect of recovery models on backup and restore, see [Backup overview (SQL Server)](backup-overview-sql-server.md).

### Design your backup strategy

After you select a recovery model that meets your business requirements for a specific database, plan and implement a matching backup strategy. The best backup strategy depends on several factors. The following factors are especially important:

- How many hours a day do applications need to access the database?

  If there's a predictable off-peak period, you should schedule full database backups for that period.

- How frequently are changes and updates likely to occur?

  If changes are frequent, consider:

  - Under the simple recovery model, you can schedule differential backups between full database backups. A differential backup captures only the changes since the last full database backup.

  - Under the full recovery model, you can schedule frequent log backups. Scheduling differential backups between full backups can reduce restore time by reducing the number of log backups you have to restore after restoring the data.

- Are changes likely to occur in only a small part of the database, or in a large part?

  For a large database in which changes are concentrated in a subset of the files or filegroups, partial backups or full file backups can be useful. For more information, see [Partial Backups (SQL Server)](partial-backups-sql-server.md) and [Full File Backups (SQL Server)](full-file-backups-sql-server.md).

- How much disk space does a full database backup require?
- How far in the past does your business require to maintain backups?

  Ensure you have a proper backup schedule that matches the application's needs and business requirements. As backups age, the risk of data loss grows unless you have a way to regenerate all the data up to the point of failure. Before you dispose of old backups because of storage limits, consider whether you need recovery that far in the past.

### Estimate the size of a full database backup

Before you implement a backup and restore strategy, estimate how much disk space a full database backup uses. The backup operation copies the data in the database to the backup file. The backup contains only the actual data in the database, not any unused space. Therefore, the backup is usually smaller than the database itself. To estimate the size of a full database backup, use the `sp_spaceused` system stored procedure. For more information, see [sp_spaceused](../system-stored-procedures/sp-spaceused-transact-sql.md).

### Schedule backups

A backup operation has minimal effect on running transactions, so you can run backups during regular operations. You can perform a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup with minimal effect on production workloads.

> [!NOTE]  
> For information about concurrency restrictions during backup, see [Backup overview (SQL Server)](backup-overview-sql-server.md).

After you decide what types of backups you need and how often to perform each type, schedule regular backups as part of a database maintenance plan. For information about maintenance plans and how to create them for database backups and log backups, see [Use the Maintenance Plan Wizard](../maintenance-plans/use-the-maintenance-plan-wizard.md).

### Test your backups

You don't have a restore strategy until you test your backups. Thoroughly test your backup strategy for each database by restoring a copy of the database onto a test system. You must test restoring every type of backup that you intend to use. After you restore the backup, run `DBCC CHECKDB` against the database to confirm the backup media isn't damaged.

### Verify media stability and consistency

Use the verification options provided by the backup utilities (`BACKUP` T-SQL command, SQL Server Maintenance Plans, your backup software or solution, and so on). For an example, see [RESTORE Statements - VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md).

Use advanced features such as `BACKUP CHECKSUM` to detect problems with the backup media itself. For more information, see [Possible Media Errors During Backup and Restore (SQL Server)](possible-media-errors-during-backup-and-restore-sql-server.md).

### Document backup/restore strategy

Document your backup and restore procedures, and keep a copy of the documentation in your run book.

You should also maintain an operations manual for each database. This operations manual should document the location of the backups, backup device names (if any), and the time required to restore the test backups.

## Security risk of restoring backups from untrusted sources

[!INCLUDE [backup-restore-security-risk](../../includes/backup-restore-security-risk.md)]

## Monitor progress with XEvent

Backup and restore operations can take a long time because of the size of a database and the complexity of the operations involved. When issues arise with either operation, use the `backup_restore_progress_trace` extended event to monitor progress live. For more information about extended events, see [Extended Events overview](../extended-events/extended-events.md).

  > [!WARNING]  
  > The `backup_restore_progress_trace` extended event can cause performance issues and consume a large amount of disk space. Use it for short periods, exercise caution, and test thoroughly before using it in production.

```sql
-- Create the backup_restore_progress_trace extended event session
CREATE EVENT SESSION [BackupRestoreTrace] ON SERVER
ADD EVENT sqlserver.backup_restore_progress_trace
ADD TARGET package0.event_file (SET filename = N'BackupRestoreTrace')
WITH
(
    MAX_MEMORY = 4096 KB,
    EVENT_RETENTION_MODE = ALLOW_SINGLE_EVENT_LOSS,
    MAX_DISPATCH_LATENCY = 5 SECONDS,
    MAX_EVENT_SIZE = 0 KB,
    MEMORY_PARTITION_MODE = NONE,
    TRACK_CAUSALITY = OFF,
    STARTUP_STATE = OFF
);
GO

-- Start the event session
ALTER EVENT SESSION [BackupRestoreTrace] ON SERVER
STATE = START;
GO

-- Stop the event session
ALTER EVENT SESSION [BackupRestoreTrace] ON SERVER
STATE = STOP;
GO
```

### Sample output from Extended Event

:::image type="content" source="media/back-up-and-restore-of-sql-server-databases/backup-xevent-example.png" alt-text="Screenshot of an example of back up xevent output.":::

:::image type="content" source="media/back-up-and-restore-of-sql-server-databases/restore-xevent-example.png" alt-text="Screenshot of an example of back up xevent output, continued.":::

## More about backup tasks

- [Create a maintenance plan](../maintenance-plans/create-a-maintenance-plan.md)
- [Create a SQL Server Agent job in SQL Server Management Studio](/ssms/agent/create-a-job)
- [Configure schedule for SQL Server Agent job](/ssms/agent/schedule-a-job)

<a id="working-with-backup-devices-and-backup-media"></a>

## Work with backup devices and backup media

- [Define a logical backup device for a disk file (SQL Server)](define-a-logical-backup-device-for-a-disk-file-sql-server.md)
- [Define a logical backup device for a tape drive (SQL Server)](define-a-logical-backup-device-for-a-tape-drive-sql-server.md)
- [Specify a disk or tape backup destination (SQL Server)](specify-a-disk-or-tape-as-a-backup-destination-sql-server.md)
- [Delete a backup device (SQL Server)](delete-a-backup-device-sql-server.md)
- [Set the expiration date on a backup (SQL Server)](set-the-expiration-date-on-a-backup-sql-server.md)
- [View the contents of a backup tape or file (SQL Server)](view-the-contents-of-a-backup-tape-or-file-sql-server.md)
- [View the data and log files in a backup set (SQL Server)](view-the-data-and-log-files-in-a-backup-set-sql-server.md)
- [View the properties and contents of a logical backup device (SQL Server)](view-the-properties-and-contents-of-a-logical-backup-device-sql-server.md)
- [Restore a backup from a device (SQL Server)](restore-a-backup-from-a-device-sql-server.md)

<a id="creating-backups"></a>

## Create backups

For partial or copy-only backups, use the [!INCLUDE [tsql](../../includes/tsql-md.md)] [BACKUP](../../t-sql/statements/backup-transact-sql.md) statement with the `PARTIAL` or `COPY_ONLY` option, respectively.

<a id="using-ssms"></a>

### Use SSMS

- [Create a full database backup](create-a-full-database-backup-sql-server.md)
- [Back up a transaction log](back-up-a-transaction-log-sql-server.md)
- [Back up files and filegroups](back-up-files-and-filegroups-sql-server.md)
- [Create a differential database backup (SQL Server)](create-a-differential-database-backup-sql-server.md)

<a id="using-t-sql"></a>

### Use T-SQL

- [Use Resource Governor to limit CPU usage by backup compression](use-resource-governor-to-limit-cpu-usage-by-backup-compression-transact-sql.md)
- [Back up the transaction log when the database is damaged (SQL Server)](back-up-the-transaction-log-when-the-database-is-damaged-sql-server.md)
- [Enable or disable backup checksums during backup or restore (SQL Server)](enable-or-disable-backup-checksums-during-backup-or-restore-sql-server.md)
- [Specify backup or restore to continue or stop after error](specify-if-backup-or-restore-continues-or-stops-after-error.md)

## Restore data backups

<a id="using-ssms"></a>

### Use SSMS

- [Restore a database backup using SSMS](restore-a-database-backup-using-ssms.md)
- [Restore a database to a new location (SQL Server)](restore-a-database-to-a-new-location-sql-server.md)
- [Restore a differential database backup (SQL Server)](restore-a-differential-database-backup-sql-server.md)
- [Restore files and filegroups (SQL Server)](restore-files-and-filegroups-sql-server.md)

<a id="using-t-sql"></a>

### Use T-SQL

- [Restore a database backup under the simple recovery model](restore-a-database-backup-under-the-simple-recovery-model-transact-sql.md)
- [Restore database to point of failure - full recovery](restore-database-to-point-of-failure-full-recovery.md)
- [Restore files and filegroups over existing files (SQL Server)](restore-files-and-filegroups-over-existing-files-sql-server.md)
- [Restore files to a new location (SQL Server)](restore-files-to-a-new-location-sql-server.md)
- [Restore the master database](restore-the-master-database-transact-sql.md)

## Restore transaction logs (full recovery model)

<a id="using-ssms"></a>

### Use SSMS

- [Restore a database to a marked transaction (SQL Server Management Studio)](restore-a-database-to-a-marked-transaction-sql-server-management-studio.md)
- [Restore a transaction log backup (SQL Server)](restore-a-transaction-log-backup-sql-server.md)
- [Restore a SQL Server database to a point in time (full recovery model)](restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md)

<a id="using-t-sql"></a>

### Use T-SQL

- [Restore a SQL Server database to a point in time (full recovery model)](restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md)
- [Restart an interrupted restore operation](restart-an-interrupted-restore-operation-transact-sql.md)
- [Recover a database without restoring data](recover-a-database-without-restoring-data-transact-sql.md)

## Related content

- [Backup overview (SQL Server)](backup-overview-sql-server.md)
- [Restore and recovery overview (SQL Server)](restore-and-recovery-overview-sql-server.md)
- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [RESTORE Statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
- [Backup and Restore of Analysis Services Databases](/analysis-services/multidimensional-models/backup-and-restore-of-analysis-services-databases)
- [Back up and restore full-text catalogs and indexes](../search/back-up-and-restore-full-text-catalogs-and-indexes.md)
- [Back up and restore replicated databases](../replication/administration/back-up-and-restore-replicated-databases.md)
- [The transaction log](../logs/the-transaction-log-sql-server.md)
- [Recovery models (SQL Server)](recovery-models-sql-server.md)
- [Media sets, media families, and backup sets (SQL Server)](media-sets-media-families-and-backup-sets-sql-server.md)
