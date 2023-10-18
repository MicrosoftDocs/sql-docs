---
title: "Backup overview (SQL Server)"
description: Learn about the SQL Server backup component, including backup types and restrictions, and also backup devices and backup media.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 10/05/2023
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords:
  - "tables [SQL Server], backing up data"
  - "backups [SQL Server]"
  - "database backups [SQL Server]"
  - "backup types [SQL Server]"
  - "data backups [SQL Server]"
  - "backing up tables [SQL Server]"
  - "database restores [SQL Server], backups"
  - "backing up [SQL Server], about backing up"
  - "restoring [SQL Server], backup types"
  - "backups [SQL Server], about"
  - "backups [SQL Server], table-level backups unsupported"
---
# Backup overview (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article introduces the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup component. Backing up your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database is essential for protecting your data. This discussion covers backup types, and backup restrictions. The topic also introduces [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup devices and backup media.

## Terms

- **back up [verb]**: Copies the data or log records from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database or its transaction log to a backup device, such as a disk, to create a data backup or log backup.

- **backup [noun]**: A copy of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data that can be used to restore and recover the data after a failure. A backup of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data is created at the level of a database or one or more of its files or filegroups. Table-level backups cannot be created. In addition to data backups, the full recovery model requires creating backups of the transaction log.

- **[recovery model](recovery-models-sql-server.md)**: A database property that controls transaction log maintenance on a database. Three recovery models exist: simple, full, and bulk-logged. The recovery model of database determines its backup and restore requirements.

- **[restore](restore-and-recovery-overview-sql-server.md)**: A multi-phase process that copies all the data and log pages from a specified [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup to a specified database, and then rolls forward all the transactions that are logged in the backup by applying logged changes to bring the data forward in time.

## Types of backups

- **[copy-only backup](copy-only-backups-sql-server.md)**: A special-use backup that is independent of the regular sequence of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backups.

- **data backup**: A backup of data in a complete database (a database backup), a partial database (a partial backup), or a set of data files or filegroups (a file backup).

- **[database backup](full-database-backups-sql-server.md)**: A backup of a database. Full database backups represent the whole database at the time the backup finished. Differential database backups contain only changes made to the database since its most recent full database backup.

- **[differential backup](differential-backups-sql-server.md)**: A data backup that is based on the latest full backup of a complete or partial database or a set of data files or filegroups (the *differential base*) and that contains only the data extents that have changed since the differential base.

  A differential partial backup records only the data extents that have changed in the filegroups since the previous partial backup, known as the base for the differential.

- **full backup**: A data backup that contains all the data in a specific database or set of filegroups or files, and also enough log to allow for recovering that data.

- **[log backup](transaction-log-backups-sql-server.md)**: A backup of transaction logs that includes all log records that were not backed up in a previous log backup (full recovery model).

- **[file backup](full-file-backups-sql-server.md)**: A backup of one or more database files or filegroups.

- **[partial backup](partial-backups-sql-server.md)**: Contains data from only some of the filegroups in a database, including the data in the primary filegroup, every read/write filegroup, and any optionally specified read-only files.

## Backup media terms and definitions

- **[backup device](backup-devices-sql-server.md)**: A disk or tape device to which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backups are written and from which they can be restored. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backups can also be written to Azure Blob Storage, and **URL** format is used to specify the destination and the name of the backup file.. For more information, see [SQL Server Backup and Restore with Microsoft Azure Blob Storage](sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md).

- **[backup media](media-sets-media-families-and-backup-sets-sql-server.md)**: One or more tapes or disk files to which one or more backup have been written.

- **[backup set](media-sets-media-families-and-backup-sets-sql-server.md)**: The backup content that is added to a media set by a successful backup operation.

- **[media family](media-sets-media-families-and-backup-sets-sql-server.md)**: Backups created on a single non-mirrored device or a set of mirrored devices in a media set.

- **[media set](media-sets-media-families-and-backup-sets-sql-server.md)**: An ordered collection of backup media, tapes or disk files, to which one or more backup operations have written using a fixed type and number of backup devices.

- **[mirrored media set](mirrored-backup-media-sets-sql-server.md)**: Multiple copies (mirrors) of a media set.

## <a id="BackupCompression"></a> Backup compression

[!INCLUDE [ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)] edition and later versions support compressing backups, and [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions can restore a compressed backup. [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Standard edition and later versions support compressing backups, and restoring compressed backups. For more information, see [Backup compression (SQL Server)](backup-compression-sql-server.md).

## <a id="Restrictions"></a> Backup operations restrictions

Backup can occur while the database is online and being used. However, the following restrictions exist:

### Cannot back up offline data

Any backup operation that implicitly or explicitly references data that is offline fails. Some typical examples include the following:

- You request a full database backup, but one filegroup of the database is offline. Because all filegroups are implicitly included in a full database backup, this operation fails.

  To back up this database, you can use a file backup and specify only the filegroups that are online.

- You request a partial backup, but a read/write filegroup is offline. Because all read/write filegroups are required for a partial backup, the operation fails.

- You request a file backup of specific files, but one of the files is not online. The operation fails. To back up the online files, you can omit the offline file from the file list and repeat the operation.

Typically, a log backup succeeds even if one or more data files are unavailable. However, if any file contains bulk-logged changes made under the bulk-logged recovery model, all the files must be online for the backup to succeed.

### Concurrency restrictions

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses an online backup process to allow for a database backup while the database is still being used. During a backup, most operations are possible; for example, INSERT, UPDATE, or DELETE statements are allowed during a backup operation. However, if you try to start a backup operation while a database file is being created or deleted, the backup operation waits until the create or delete operation is finished or the backup times out.

Operations that cannot run during a database backup or transaction log backup include the following:

- File-management operations such as the `ALTER DATABASE` statement with either the `ADD FILE` or `REMOVE FILE` options.

- Shrink database or shrink file operations. This includes auto-shrink operations.

- If you try to create or delete a database file while a backup operation is in progress, the create or delete operation fails.

If a backup operation overlaps with a file-management operation or shrink operation, a conflict occurs. Regardless of which of the conflicting operations began first, the second operation waits for the lock set by the first operation to time out. (The time-out period is controlled by a session time-out setting.) If the lock is released during the time-out period, the second operation continues. If the lock times out, the second operation fails.

## <a id="RelatedTasks"></a> Related tasks

### Backup devices and backup media

- [Define a Logical Backup Device for a Disk File (SQL Server)](define-a-logical-backup-device-for-a-disk-file-sql-server.md)
- [Define a Logical Backup Device for a Tape Drive (SQL Server)](define-a-logical-backup-device-for-a-tape-drive-sql-server.md)
- [Specify a disk or tape backup destination (SQL Server)](specify-a-disk-or-tape-as-a-backup-destination-sql-server.md)
- [Delete a Backup Device (SQL Server)](delete-a-backup-device-sql-server.md)
- [Set the Expiration Date on a Backup (SQL Server)](set-the-expiration-date-on-a-backup-sql-server.md)
- [View the contents of a backup tape or file (SQL Server)](view-the-contents-of-a-backup-tape-or-file-sql-server.md)
- [View the data and log files in a backup set (SQL Server)](view-the-data-and-log-files-in-a-backup-set-sql-server.md)
- [View the Properties and Contents of a Logical Backup Device (SQL Server)](view-the-properties-and-contents-of-a-logical-backup-device-sql-server.md)
- [Restore a Backup from a Device (SQL Server)](restore-a-backup-from-a-device-sql-server.md)
- [Tutorial: SQL Server Backup and Restore to Azure Blob Storage](~/relational-databases/tutorial-sql-server-backup-and-restore-to-azure-blob-storage-service.md)

### Create a backup

> [!NOTE]  
> For partial or copy-only backups, you must use the [!INCLUDE [tsql](../../includes/tsql-md.md)] [BACKUP](../../t-sql/statements/backup-transact-sql.md) statement with the `PARTIAL` or `COPY_ONLY` option, respectively.

- [Create a Full Database Backup](create-a-full-database-backup-sql-server.md)
- [Back Up a Transaction Log](back-up-a-transaction-log-sql-server.md)
- [Back Up Files and Filegroups](back-up-files-and-filegroups-sql-server.md)
- [Create a Differential Database Backup (SQL Server)](create-a-differential-database-backup-sql-server.md)
- [Back Up the Transaction Log When the Database Is Damaged (SQL Server)](back-up-the-transaction-log-when-the-database-is-damaged-sql-server.md)
- [Enable or disable backup checksums during backup or restore (SQL Server)](enable-or-disable-backup-checksums-during-backup-or-restore-sql-server.md)
- [Specify backup or restore to continue or stop after error](specify-if-backup-or-restore-continues-or-stops-after-error.md)
- [Use Resource Governor to Limit CPU Usage by Backup Compression (Transact-SQL)](use-resource-governor-to-limit-cpu-usage-by-backup-compression-transact-sql.md)
- [Quickstart: SQL backup and restore to Azure Blob Storage](../tutorial-sql-server-backup-and-restore-to-azure-blob-storage-service.md)

## Related content

- [Back Up and Restore of SQL Server Databases](back-up-and-restore-of-sql-server-databases.md)
- [Restore and recovery overview (SQL Server)](restore-and-recovery-overview-sql-server.md)
- [Maintenance plans](../maintenance-plans/maintenance-plans.md)
- [The transaction log](../logs/the-transaction-log-sql-server.md)
- [Recovery Models (SQL Server)](recovery-models-sql-server.md)
