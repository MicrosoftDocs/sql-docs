---
title: "File Restores (Full Recovery Model) | Microsoft Docs"
description: A file restore in SQL Server is a single restore sequence that copies, rolls forward, and recovers one or more data files without restoring the whole database.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "file restores [SQL Server]"
  - "full recovery model [SQL Server], performing restores"
  - "restoring files [SQL Server], Transact-SQL restore sequence"
  - "restoring files [SQL Server]"
  - "file restores [SQL Server], full recovery model"
  - "restoring files [SQL Server], full recovery model"
  - "Transact-SQL restore sequence"
  - "file restores [SQL Server], Transact-SQL restore sequence"
ms.assetid: d2236a2a-4cf1-4c3f-b542-f73f6096e15c
author: MashaMSFT
ms.author: mathoma
---
# File Restores (Full Recovery Model)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This topic is relevant only for databases that contain multiple files or filegroups under the full or bulk-load recovery model.  
  
 In a file restore, the goal is to restore one or more damaged files without restoring the whole database. A file restore scenario consists of a single restore sequence that copies, rolls forward, and recovers the appropriate data  
  
 If the filegroup that is being restored is read/write, an unbroken chain of log backups must be applied after the last data or differential backup is restored. This brings the filegroup forward to the log records in the current active log records in the log file. The recovery point is typically near the end of log, but not necessarily.  
  
 If the filegroup that is being restored is read-only, usually applying log backups is unnecessary and is skipped. If the backup was taken after the file became read-only, that is the last backup to restore. Roll forward stops at the target point.  
  
 The file-restore scenarios are as follows:  
  
-   Offline file restore  
  
     In an *offline file restore*, the database is offline while damaged files or filegroups are restored. At the end of the restore sequence, the database comes online.  
  
     All editions of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] support offline file restore.  
  
-   Online file restore  
  
     In an *online file restore*, if database is online at restore time, it remains online during the file restore. However, each filegroup in which a file is being restored is offline during the restore operation. After all the files in an offline filegroup are recovered, the filegroup is automatically brought online.  
  
     For information about support for online page and file restore, see [Editions and supported features of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md). For more information about online restores, see [Online Restore (SQL Server)](../../relational-databases/backup-restore/online-restore-sql-server.md).
  
    > [!TIP]  
    >  If you want the database to be offline for a file restore, take the database offline before you start the restore sequence by executing the following [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql-set-options.md) statement: ALTER DATABASE *database_name* SET OFFLINE.  
  
  
##  <a name="Overview"></a> Restoring Damaged Files from File Backups  
  
1.  Before restoring one or more damaged files, attempt to create a [tail-log backup](../../relational-databases/backup-restore/tail-log-backups-sql-server.md).  
  
     If the log has been damaged, a tail-log backup cannot be created, and you must restore the whole database.  
  
     For information about how to back up a transaction log, see [Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/transaction-log-backups-sql-server.md).  
  
    > [!IMPORTANT]  
    >  For an offline file restore, you must always take a tail-log backup before the file restore. For an online file restore, you must always take the log backup after the file restore. This log backup is necessary to allow for the file to be recovered to a state consistent with the rest of the database.  
  
2.  Restore each damaged file from the most recent file backup of that file.  
  
3.  Restore the most recent differential file backup, if any, for each restored file.  
  
4.  Restore transaction log backups in sequence, starting with the backup that covers the oldest of the restored files and ending with the tail-log backup created in step 1.  
  
     You must restore the transaction log backups that were created after the file backups to bring the database to a consistent state. The transaction log backups can be rolled forward quickly, because only the changes that apply to the restored files are applied. Restoring individual files can be better than restoring the whole database, because undamaged files are not copied and then rolled forward. However, the whole chain of log backups still has to be read.  
  
5.  Recover the database.  

> [!NOTE]  
>  File backups can be used to restore the database to an earlier point in time. To do this, you must restore a complete set of file backups, and then restore transaction log backups in sequence to reach a target point that is after the end of the most recent restored file backup. For more information about point-in-time recovery, see [Restore a SQL Server Database to a Point in Time &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md).  
  
## Transact-SQL Restore Sequence for an Offline File Restore (Full Recovery Model)  
 A file restore scenario consists of a single restore sequence that copies, rolls forward, and recovers the appropriate data.  
  
 This section shows the essential [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md) options for a file-restore sequence. Syntax and details that are not relevant to this purpose are omitted.  
  
 The following sample restore sequence shows an offline restore of two secondary files, `A` and `B`, using WITH NORECOVERY. Next, two log backups are applied with NORECOVERY, followed with the tail-log backup, and this is restored using WITH RECOVERY.  
  
> [!NOTE]  
>  The following sample restore sequence starts by taking the file offline and then creates a tail-log backup.  
  
```  
--Take the file offline.  
ALTER DATABASE database_name MODIFY FILE SET OFFLINE;  
-- Back up the currently active transaction log.  
BACKUP LOG database_name  
   TO <tail_log_backup>  
   WITH NORECOVERY;  
GO   
-- Restore the files.  
RESTORE DATABASE database_name FILE=name   
   FROM <file_backup_of_file_A>   
   WITH NORECOVERY;  
RESTORE DATABASE database_name FILE=<name> ......  
   FROM <file_backup_of_file_B>   
   WITH NORECOVERY;  
-- Restore the log backups.  
RESTORE LOG database_name FROM <log_backup>   
   WITH NORECOVERY;  
RESTORE LOG database_name FROM <log_backup>   
   WITH NORECOVERY;  
RESTORE LOG database_name FROM <tail_log_backup>   
   WITH RECOVERY;  
```  
  
## Examples  
  
-   [Example: Online Restore of a Read-Write File &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-online-restore-of-a-read-write-file-full-recovery-model.md)  
  
-   [Example: Online Restore of a Read-Only File &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-online-restore-of-a-read-only-file-full-recovery-model.md)  
  
-   [Example: Offline Restore of Primary and One Other Filegroup &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-offline-restore-of-primary-and-one-other-filegroup-full-recovery-model.md)  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To restore files and filegroups**  
  
-   [Restore Files to a New Location &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-files-to-a-new-location-sql-server.md)  
  
-   [Restore Files and Filegroups &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-files-and-filegroups-sql-server.md)  
  
-   <xref:Microsoft.SqlServer.Management.Smo.Restore.SqlRestore%2A> (SMO)  
  
  
## See Also  
 [Backup and Restore: Interoperability and Coexistence &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-and-restore-interoperability-and-coexistence-sql-server.md)   
 [Differential Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/differential-backups-sql-server.md)   
 [Full File Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/full-file-backups-sql-server.md)   
 [Backup Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-overview-sql-server.md)   
 [Restore and Recovery Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Complete Database Restores &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-simple-recovery-model.md)   
 [Piecemeal Restores &#40;SQL Server&#41;](../../relational-databases/backup-restore/piecemeal-restores-sql-server.md)  
  
