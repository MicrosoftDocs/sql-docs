---
title: "Backup Overview (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
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
ms.assetid: 09a6e0c2-d8fd-453f-9aac-4ff24a97dc1f
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Backup Overview (SQL Server)
  This topic introduces the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup component. Backing up your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database is essential for protecting your data. This discussion covers backup types, and backup restrictions. The topic also introduces [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup devices and backup media.  
  
 **In this Topic:**  
  
-   [Components and Concepts](#TermsAndDefinitions)  
  
-   [Backup Compression](#BackupCompression)  
  
-   [Restrictions on Backup Operations in SQL Server](#Restrictions)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="TermsAndDefinitions"></a> Components and Concepts  
 back up [verb]  
 Copies the data or log records from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database or its transaction log to a backup device, such as a disk, to create a data backup or log backup.  
  
 backup [noun]  
 A copy of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data that can be used to restore and recover the data after a failure. A backup of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data is created at the level of a database or one or more of its files or filegroups. Table-level backups cannot be created. In addition to data backups, the full recovery model requires creating backups of the transaction log.  
  
 [recovery model](recovery-models-sql-server.md)  
 A database property that controls transaction log maintenance on a database. Three recovery models exist: simple, full, and bulk-logged. The recovery model of database determines its backup and restore requirements.  
  
 [restore](restore-and-recovery-overview-sql-server.md)  
 A multi-phase process that copies all the data and log pages from a specified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup to a specified database, and then rolls forward all the transactions that are logged in the backup by applying logged changes to bring the data forward in time.  
  
 **Types of Backups**  
  
 [copy-only backup](copy-only-backups-sql-server.md)  
 A special-use backup that is independent of the regular sequence of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups.  
  
 data backup  
 A backup of data in a complete database (a database backup), a partial database (a partial backup), or a set of data files or filegroups (a file backup).  
  
 [database backup](full-database-backups-sql-server.md)  
 A backup of a database. Full database backups represent the whole database at the time the backup finished. Differential database backups contain only changes made to the database since its most recent full database backup.  
  
 [differential backup](full-database-backups-sql-server.md)  
 A data backup that is based on the latest full backup of a complete or partial database or a set of data files or filegroups (the *differential base*) and that contains only the data extents that have changed since the differential base.  
  
 A differential partial backup records only the data extents that have changed in the filegroups since the previous partial backup, known as the base for the differential.  
  
 full backup  
 A data backup that contains all the data in a specific database or set of filegroups or files, and also enough log to allow for recovering that data.  
  
 [log backup](transaction-log-backups-sql-server.md)  
 A backup of transaction logs that includes all log records that were not backed up in a previous log backup. (full recovery model)  
  
 [file backup](full-file-backups-sql-server.md)  
 A backup of one or more database files or filegroups.  
  
 [partial backup](partial-backups-sql-server.md)  
 Contains data from only some of the filegroups in a database, including the data in the primary filegroup, every read/write filegroup, and any optionally-specified read-only files.  
  
 **Backup Media Terms and Definitions**  
  
 [backup device](backup-devices-sql-server.md)  
 A disk or tape device to which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups are written and from which they can be restored. SQL Server backups can also be written to a Windows Azure Blob storage service, and **URL** format is used to specify the destination and the name of the backup file.. For more information, see [SQL Server Backup and Restore with Windows Azure Blob Storage Service](sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md).  
  
 [backup media](media-sets-media-families-and-backup-sets-sql-server.md)  
 One or more tapes or disk files to which one or more backup have been written.  
  
 [backup set](media-sets-media-families-and-backup-sets-sql-server.md)  
 The backup content that is added to a media set by a successful backup operation.  
  
 [media family](media-sets-media-families-and-backup-sets-sql-server.md)  
 Backups created on a single nonmirrored device or a set of mirrored devices in a media set  
  
 [media set](media-sets-media-families-and-backup-sets-sql-server.md)  
 An ordered collection of backup media, tapes or disk files, to which one or more backup operations have written using a fixed type and number of backup devices.  
  
 [mirrored media set](mirrored-backup-media-sets-sql-server.md)  
 Multiple copies (mirrors) of a media set.  
  
##  <a name="BackupCompression"></a> Backup Compression  
 [!INCLUDE[ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)] and later versions support compressing backups, and [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions can restore a compressed backup. For more information, see [Backup Compression &#40;SQL Server&#41;](backup-compression-sql-server.md).  
  
##  <a name="Restrictions"></a> Restrictions on Backup Operations in SQL Server  
 Backup can occur while the database is online and being used. However, the following restrictions exist.  
  
### Offline Data Cannot Be Backed Up  
 Any backup operation that implicitly or explicitly references data that is offline fails. Some typical examples include the following:  
  
-   You request a full database backup, but one filegroup of the database is offline. Because all filegroups are implicitly included in a full database backup, this operation fails.  
  
     To back up this database, you can use a file backup and specify only the filegroups that are online.  
  
-   You request a partial backup, but a read/write filegroup is offline. Because all read/write filegroups are required for a partial backup, the operation fails.  
  
-   You request a file backup of specific files, but one of the files is not online. The operation fails. To back up the online files, you can omit the offline file from the file list and repeat the operation.  
  
 Typically, a log backup succeeds even if one or more data files are unavailable. However, if any file contains bulk-logged changes made under the bulk-logged recovery model, all the files must be online for the backup to succeed.  
  
### Concurrency Restrictions During Backup  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses an online backup process to allow for a database backup while the database is still being used. During a backup, most operations are possible; for example, INSERT, UPDATE, or DELETE statements are allowed during a backup operation. However, if you try to start a backup operation while a database file is being created or deleted, the backup operation waits until the create or delete operation is finished or the backup times out.  
  
 Operations that cannot run during a database backup or transaction log backup include the following:  
  
-   File-management operations such as the ALTER DATABASE statement with either the ADD FILE or REMOVE FILE options.  
  
-   Shrink database or shrink file operations. This includes auto-shrink operations.  
  
-   If you try to create or delete a database file while a backup operation is in progress, the create or delete operation fails.  
  
 If a backup operation overlaps with a file-management operation or shrink operation, a conflict occurs. Regardless of which of the conflicting operation began first, the second operation waits for the lock set by the first operation to time out. (The time-out period is controlled by a session time-out setting.) If the lock is released during the time-out period, the second operation continues. If the lock times out, the second operation fails.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To work with backup devices and backup media**  
  
-   [Define a Logical Backup Device for a Disk File &#40;SQL Server&#41;](define-a-logical-backup-device-for-a-disk-file-sql-server.md)  
  
-   [Define a Logical Backup Device for a Tape Drive &#40;SQL Server&#41;](define-a-logical-backup-device-for-a-tape-drive-sql-server.md)  
  
-   [Specify a Disk or Tape As a Backup Destination &#40;SQL Server&#41;](specify-a-disk-or-tape-as-a-backup-destination-sql-server.md)  
  
-   [Delete a Backup Device &#40;SQL Server&#41;](delete-a-backup-device-sql-server.md)  
  
-   [Set the Expiration Date on a Backup &#40;SQL Server&#41;](set-the-expiration-date-on-a-backup-sql-server.md)  
  
-   [View the Contents of a Backup Tape or File &#40;SQL Server&#41;](view-the-contents-of-a-backup-tape-or-file-sql-server.md)  
  
-   [View the Data and Log Files in a Backup Set &#40;SQL Server&#41;](view-the-data-and-log-files-in-a-backup-set-sql-server.md)  
  
-   [View the Properties and Contents of a Logical Backup Device &#40;SQL Server&#41;](view-the-properties-and-contents-of-a-logical-backup-device-sql-server.md)  
  
-   [Restore a Backup from a Device &#40;SQL Server&#41;](restore-a-backup-from-a-device-sql-server.md)  
  
-   [Tutorial: SQL Server Backup and Restore to Windows Azure Blob Storage Service](../tutorial-sql-server-backup-and-restore-to-azure-blob-storage-service.md)  
  
 **To create a backup**  
  
> [!NOTE]  
>  For partial or copy-only backups, you must use the [!INCLUDE[tsql](../../includes/tsql-md.md)][BACKUP](/sql/t-sql/statements/backup-transact-sql) statement with the PARTIAL or COPY_ONLY option, respectively.  
  
-   [Create a Full Database Backup &#40;SQL Server&#41;](create-a-full-database-backup-sql-server.md)  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](back-up-a-transaction-log-sql-server.md)  
  
-   [Back Up Files and Filegroups &#40;SQL Server&#41;](back-up-files-and-filegroups-sql-server.md)  
  
-   [Create a Differential Database Backup &#40;SQL Server&#41;](create-a-differential-database-backup-sql-server.md)  
  
-   [Back Up the Transaction Log When the Database Is Damaged &#40;SQL Server&#41;](back-up-the-transaction-log-when-the-database-is-damaged-sql-server.md)  
  
-   [Enable or Disable Backup Checksums During Backup or Restore &#40;SQL Server&#41;](enable-or-disable-backup-checksums-during-backup-or-restore-sql-server.md)  
  
-   [Specify Whether a Backup or Restore Operation Continues or Stops After Encountering an Error &#40;SQL Server&#41;](specify-if-backup-or-restore-continues-or-stops-after-error.md)  
  
-   [Use Resource Governor to Limit CPU Usage by Backup Compression &#40;Transact-SQL&#41;](use-resource-governor-to-limit-cpu-usage-by-backup-compression-transact-sql.md)  
  
-   [Tutorial: SQL Server Backup and Restore to Windows Azure Blob Storage Service](../tutorial-sql-server-backup-and-restore-to-azure-blob-storage-service.md)  
  
## See Also  
 [Back Up and Restore of SQL Server Databases](back-up-and-restore-of-sql-server-databases.md)   
 [Restore and Recovery Overview &#40;SQL Server&#41;](restore-and-recovery-overview-sql-server.md)   
 [Maintenance Plans](../maintenance-plans/maintenance-plans.md)   
 [The Transaction Log &#40;SQL Server&#41;](../logs/the-transaction-log-sql-server.md)   
 [Recovery Models &#40;SQL Server&#41;](recovery-models-sql-server.md)  
  
  
