---
title: "Back up and Restore of SQL Server Databases"
description: This article describes the benefits of backing up SQL Server databases and introduces backup and restore strategies and security considerations.
author: MashaMSFT
ms.author: mathoma
ms.date: 12/05/2025
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
# Back up and restore of SQL Server databases

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes the benefits of backing up [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] databases, describes basic backup and restore terms, and introduces backup and restore strategies for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and security considerations for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup and restore.

> This article introduces SQL Server backups. For specific steps to back up SQL Server databases, see [Creating backups](#creating-backups).

The SQL Server backup and restore component provides an essential safeguard for protecting critical data stored in your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] databases. To minimize the risk of catastrophic data loss, you need to back up your databases to preserve modifications to your data on a regular basis. A well-planned backup and restore strategy helps protect databases against data loss caused by a variety of failures. Test your strategy by restoring a set of backups and then recovering your database to prepare you to respond effectively to a disaster.

In addition to local storage for storing the backups, SQL Server also supports backup to and restore from Azure Blob Storage. For more information, see [SQL Server backup and restore with Microsoft Azure Blob Storage](sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md). For database files stored using Azure Blob Storage, [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] provides the option to use Azure snapshots for nearly instantaneous backups and faster restores. For more information, see [File-snapshot backups for database files in Azure](file-snapshot-backups-for-database-files-in-azure.md). Azure also offers an enterprise-class backup solution for SQL Server running in Azure VMs. A fully managed backup solution, it supports Always On availability groups, long-term retention, point-in-time recovery, and central management and monitoring. For more information, see [About SQL Server backup on Azure VMs](/azure/backup/backup-azure-sql-database).

## Why back up?

- Backing up your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] databases, running test restores procedures on your backups, and storing copies of backups in a safe, off-site location protects you from potentially catastrophic data loss. **Backing up is the only way to protect your data.**

     With valid backups of a database, you can recover your data from many failures, such as:

  - Media failure.
  - User errors, for example, dropping a table by mistake.
  - Hardware failures, for example, a damaged disk drive or permanent loss of a server.
  - Natural disasters. By using SQL Server Backup to Azure Blob Storage, you can create an off-site backup in a different region than your on-premises location, to use in the event of a natural disaster affecting your on-premises location.

- Additionally, backups of a database are useful for routine administrative purposes, such as copying a database from one server to another, setting up [!INCLUDE [ssHADR](../../includes/sshadr-md.md)] or database mirroring, and archiving.

## Glossary of backup terms

 **back up** [verb]  
 The process of creating a **backup [noun]** by copying data records from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database or log records from its transaction log.

 **backup** [noun]  
 A copy of data that you can use to restore and recover the data after a failure. Backups of a database can also be used to restore a copy the database to a new location.

**backup** device  
 A disk or tape device to which SQL Server backups are written and from which they can be restored. SQL Server backups can also be written to an Azure Blob Storage, and **URL** format is used to specify the destination and the name of the backup file. For more information, see [SQL Server backup and restore with Microsoft Azure Blob Storage](sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md).

**backup media**  
 One or more tapes or disk files to which one or more backups have been written.

**data backup**  
 A backup of data in a complete database (a database backup), a partial database (a partial backup), or a set of data files or filegroups (a file backup).

**database backup**  
 A backup of a database. Full database backups represent the whole database at the time the backup finished. Differential database backups contain only changes made to the database since its most recent full database backup.

**differential backup**  
 A data backup that is based on the latest full backup of a complete or partial database or a set of data files or filegroups (the differential base) and that contains only the data that has changed since that base.

**full backup**  
 A data backup that contains all the data in a specific database or set of filegroups or files, and also enough log to allow for recovering that data.

**log backup**  
 A backup of transaction logs that includes all log records that weren't backed up in a previous log backup (full recovery model).

**recover**  
 To return a database to a stable and consistent state.

**recovery**  
 A phase of database startup or of a restore with recovery that brings the database into a transaction-consistent state.

**recovery model**  
 A database property that controls transaction log maintenance on a database. There are three recovery models: simple, full, and bulk-logged. A database's recovery model determines its backup and restore requirements.

**restore**  
 A multiphase process that copies all the data and log pages from a specified [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup to a specified database, and then rolls forward all the transactions that are logged in the backup by applying logged changes to bring the data forward in time.

## Backup and restore strategies

Backing up and restoring data must be customized to a particular environment and must work with the available resources. Therefore, a reliable use of backup and restore for recovery requires a backup and restore strategy. A well-designed backup and restore strategy balances the business requirements for maximum data availability and minimum data loss while considering the cost of maintaining and storing backups.

A backup and restore strategy contains a backup portion and a restore portion. The backup part of the strategy defines the type and frequency of backups, the nature and speed of the hardware they require, how backups are to be tested, and where and how backup media is to be stored (including security considerations). The restore part of the strategy defines who's responsible for performing restores, how restores should be performed to meet your goals for database availability and minimizing data loss, and how restores are tested.

Designing an effective backup and restore strategy requires careful planning, implementation, and testing. Testing is required: you don't have a backup strategy until you've successfully restored backups in all the combinations that are included in your restore strategy and have tested the restored database for physical consistency. You must consider a variety of factors. These include:

- Your organization's goals regarding your production databases, especially the requirements for availability and protecting data from loss or damage.

- The nature of each database: its size, its usage patterns, the nature of its content, the requirements for its data, and so on.

- Constraints on resources, such as hardware, personnel, space for storing backup media, the physical security of the stored media, and so on.

## Best practice recommendations

The accounts that perform backup or restore operations shouldn't be granted more privileges than necessary. Review [backup](../../t-sql/statements/backup-transact-sql.md#permissions) and [restore](../../t-sql/statements/restore-statements-transact-sql.md#permissions) for specific permission details. It's recommended that backups are [encrypted](../security/encryption/transparent-data-encryption.md) and, if possible, [compressed](backup-compression-sql-server.md).

Use consistent file extensions to make backups easier to identify and manage. SQL Server doesn't require or enforce these extensions, but following a convention helps with operational tasks such as configuring antivirus exclusions for backup files:

- Database backup files should have the `.BAK` extension.
- Log backup files should have the `.TRN` extension.

### Use Separate Storage

> [!IMPORTANT]  
> Ensure that you place your database backups on a separate physical location or device from the database files. When your physical drive that stores your databases malfunctions or crashes, recoverability depends on the ability to access the separate drive or remote device that stored the backups in order to perform a restore. Keep in mind that you could create several logical volumes or partitions from a same physical disk drive. Carefully study the disk partition and logical volume layouts before choosing a storage location for the backups.

### Choose appropriate recovery model

Backup and restore operations occur within the context of a [recovery model](recovery-models-sql-server.md). A recovery model is a database property that controls how the transaction log is managed. Thus, a database's recovery model determines what types of backups and restore scenarios are supported for the database, and what size the transaction log backups would be. Typically, a database uses either the simple recovery model or the full recovery model. You can augment the full recovery model by switching to the bulk-logged recovery model before bulk operations. For an introduction to these recovery models and how they affect transaction log management, see [the transaction log](../logs/the-transaction-log-sql-server.md).

The best choice of recovery model for the database depends on your business requirements. To avoid transaction log management and simplify backup and restore, use the simple recovery model. To minimize work-loss exposure at the cost of administrative overhead, use the full recovery model. To minimize impact on log size during bulk-logged operations while at the same time allowing for recoverability of those operations, use bulk-logged recovery model. For information about the effect of recovery models on backup and restore, see [Backup overview](backup-overview-sql-server.md).

### Design your backup strategy

 After you've selected a recovery model that meets your business requirements for a specific database, you have to plan and implement a corresponding backup strategy. The optimal backup strategy depends on a variety of factors, of which the following are especially significant:

- How many hours a day do applications have to access the database?

     If there's a predictable off-peak period, we recommend that you schedule full database backups for that period.

- How frequently are changes and updates likely to occur?

     If changes are frequent, consider the following:

  - Under the simple recovery model, consider scheduling differential backups between full database backups. A differential backup captures only the changes since the last full database backup.

  - Under the full recovery model, you should schedule frequent log backups. Scheduling differential backups between full backups can reduce restore time by reducing the number of log backups you have to restore after restoring the data.

- Are changes likely to occur in only a small part of the database or in a large part of the database?

     For a large database in which changes are concentrated in a part of the files or filegroups, partial backups and or full file backups can be useful. For more information, see [Partial backups (SQL Server)](partial-backups-sql-server.md) and [Full file backups (SQL Server)](full-file-backups-sql-server.md).

- How much disk space will a full database backup require?
- How far in the past does your business require to maintain backups?

    Make sure you have a proper backup schedule established according to the needs of the application and business requirements. As the backups get old, the risk of data loss is higher unless you have a way to regenerate all the data till the point of failure. Before you choose to dispose of old backups due to storage resource limitations, consider if recoverability is required that far in the past.

### Estimate the size of a full database backup

Before you implement a backup and restore strategy, you should estimate how much disk space a full database backup will use. The backup operation copies the data in the database to the backup file. The backup contains only the actual data in the database and not any unused space. Therefore, the backup is usually smaller than the database itself. You can estimate the size of a full database backup by using the `sp_spaceused` system stored procedure. For more information, see [sp_spaceused (Transact-SQL)](../system-stored-procedures/sp-spaceused-transact-sql.md).

### Schedule backups

Performing a backup operation has minimal effect on transactions that are running; therefore, you can run backup operations during regular operations. You can perform a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup with minimal effect on production workloads.

> For information about concurrency restrictions during backup, see [Backup overview (SQL Server)](backup-overview-sql-server.md).

After you decide what types of backups you require and how frequently you have to perform each type, we recommend that you schedule regular backups as part of a database maintenance plan for the database. For information about maintenance plans and how to create them for database backups and log backups, see [Use the Maintenance Plan Wizard](../maintenance-plans/use-the-maintenance-plan-wizard.md).

### Test your backups

You don't have a restore strategy until you test your backups. It's very important to thoroughly test your backup strategy for each of your databases by restoring a copy of the database onto a test system. You must test restoring every type of backup that you intend to use. We also recommend that once you restore the backup, you perform database consistency checks via DBCC CHECKDB of the database to validate the backup media wasn't damaged.

### Verify media stability and consistency

Use the verification options provided by the backup utilities (`BACKUP` T-SQL command, SQL Server Maintenance Plans, your backup software or solution, etc.). For an example, see [RESTORE statements - VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md).

Use advanced features like `BACKUP CHECKSUM` to detect problems with the backup media itself. For more information, see [Possible media errors during backup and restore (SQL Server)](possible-media-errors-during-backup-and-restore-sql-server.md)

### Document backup/restore strategy

We recommend that you document your backup and restore procedures and keep a copy of the documentation in your run book.
We also recommend that you maintain an operations manual for each database. This operations manual should document the location of the backups, backup device names (if any), and the amount of time that is required to restore the test backups.

## Security risk of restoring backups from untrusted sources

[!INCLUDE [backup-restore-security-risk](../../includes/backup-restore-security-risk.md)]

## Monitor progress with XEvent

Backup and restore operations can take a considerable amount of time due to the size of a database and the complexity of the operations involved. When issues arise with either operation, you can use the `backup_restore_progress_trace` extended event to monitor progress live. For more information about extended events, see [Extended Events overview](../extended-events/extended-events.md).

  > [!WARNING]  
  > Using the `backup_restore_progress_trace` extended event can cause a performance issue and consume a significant amount of disk space. Use for short periods of time, exercise caution, and test thoroughly before implementing in production.

```sql
-- Create the backup_restore_progress_trace extended event session
CREATE EVENT SESSION [BackupRestoreTrace] ON SERVER
ADD EVENT sqlserver.backup_restore_progress_trace
ADD TARGET package0.event_file(SET filename=N'BackupRestoreTrace')
WITH (MAX_MEMORY=4096 KB,EVENT_RETENTION_MODE=ALLOW_SINGLE_EVENT_LOSS,MAX_DISPATCH_LATENCY=5 SECONDS,MAX_EVENT_SIZE=0 KB,MEMORY_PARTITION_MODE=NONE,TRACK_CAUSALITY=OFF,STARTUP_STATE=OFF)
GO

-- Start the event session
ALTER EVENT SESSION [BackupRestoreTrace]
ON SERVER
STATE = start;
GO

-- Stop the event session
ALTER EVENT SESSION [BackupRestoreTrace]
ON SERVER
STATE = stop;
GO
```

### Sample output from extended event

![Screenshot of an example of back up xevent output.](media/back-up-and-restore-of-sql-server-databases/backup-xevent-example.png)
![Screenshot of an example of back up xevent output, continued.](media/back-up-and-restore-of-sql-server-databases/restore-xevent-example.png)

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

> [!NOTE]  
> For partial or copy-only backups, you must use the [!INCLUDE [tsql](../../includes/tsql-md.md)] [BACKUP](../../t-sql/statements/backup-transact-sql.md) statement with the `PARTIAL` or `COPY_ONLY` option, respectively.

<a id="using-ssms"></a>

### Use SSMS

- [Create a Full Database Backup (SQL Server)](create-a-full-database-backup-sql-server.md)

- [Back up a transaction log](back-up-a-transaction-log-sql-server.md)

- [Back Up Files and Filegroups](back-up-files-and-filegroups-sql-server.md)

- [Create a Differential Database Backup (SQL Server)](create-a-differential-database-backup-sql-server.md)

<a id="using-t-sql"></a>

### Use T-SQL

- [Use Resource Governor to Limit CPU Usage by Backup Compression (Transact-SQL)](use-resource-governor-to-limit-cpu-usage-by-backup-compression-transact-sql.md)

- [Back Up the Transaction Log When the Database Is Damaged (SQL Server)](back-up-the-transaction-log-when-the-database-is-damaged-sql-server.md)

- [Enable or disable backup checksums during backup or restore (SQL Server)](enable-or-disable-backup-checksums-during-backup-or-restore-sql-server.md)

- [Specify backup or restore to continue or stop after error](specify-if-backup-or-restore-continues-or-stops-after-error.md)

## Restore data backups

<a id="using-ssms"></a>

### Use SSMS

- [Restore a Database Backup Using SSMS](restore-a-database-backup-using-ssms.md)

- [Restore a database to a new location (SQL Server)](restore-a-database-to-a-new-location-sql-server.md)

- [Restore a differential database backup (SQL Server)](restore-a-differential-database-backup-sql-server.md)

- [Restore Files and Filegroups (SQL Server)](restore-files-and-filegroups-sql-server.md)

<a id="using-t-sql"></a>

### Use T-SQL

- [Restore a database backup under the simple recovery model (Transact-SQL)](restore-a-database-backup-under-the-simple-recovery-model-transact-sql.md)

- [Restore Database to Point of Failure - Full Recovery](restore-database-to-point-of-failure-full-recovery.md)

- [Restore Files and Filegroups over Existing Files (SQL Server)](restore-files-and-filegroups-over-existing-files-sql-server.md)

- [Restore Files to a New Location (SQL Server)](restore-files-to-a-new-location-sql-server.md)

- [Restore the master database (Transact-SQL)](restore-the-master-database-transact-sql.md)

## Restore transaction logs (full recovery model)

<a id="using-ssms"></a>

### Use SSMS

- [Restore a database to a marked transaction (SQL Server Management Studio)](restore-a-database-to-a-marked-transaction-sql-server-management-studio.md)

- [Restore a Transaction Log Backup (SQL Server)](restore-a-transaction-log-backup-sql-server.md)

- [Restore a SQL Server Database to a Point in Time (Full Recovery Model)](restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md)

 ### Using T-SQL

- [Restore a SQL Server Database to a Point in Time (Full Recovery Model)](restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md)

- [Restart an interrupted restore operation (Transact-SQL)](restart-an-interrupted-restore-operation-transact-sql.md)

- [Recover a database without restoring data (Transact-SQL)](recover-a-database-without-restoring-data-transact-sql.md)

## Related content

- [Backup overview (SQL Server)](backup-overview-sql-server.md)
- [Restore and recovery overview (SQL Server)](restore-and-recovery-overview-sql-server.md)
- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [RESTORE Statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
- [Backup and Restore of Analysis Services Databases](/analysis-services/multidimensional-models/backup-and-restore-of-analysis-services-databases)
- [Back Up and Restore Full-Text Catalogs and Indexes](../search/back-up-and-restore-full-text-catalogs-and-indexes.md)
- [Back Up and Restore Replicated Databases](../replication/administration/back-up-and-restore-replicated-databases.md)
- [The transaction log](../logs/the-transaction-log-sql-server.md)
- [Recovery models (SQL Server)](recovery-models-sql-server.md)
- [Media sets, media families, and backup sets (SQL Server)](media-sets-media-families-and-backup-sets-sql-server.md)
