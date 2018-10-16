---
title: "Full File Backups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "full backups [SQL Server]"
  - "backing up [SQL Server], files or filegroups"
  - "backups [SQL Server], files or filegroups"
  - "full recovery model [SQL Server], full file backups"
  - "file backups [SQL Server], full"
  - "files [SQL Server], backing up"
  - "filegroups [SQL Server], backing up"
  - "file backups [SQL Server]"
ms.assetid: a716bf8d-0c5a-490d-aadd-597b3b0fac0c
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Full File Backups (SQL Server)
  This topic is relevant for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] databases that contain multiple files or filegroups.  
  
 The files in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database can be backed up and restored individually. Also, you can specify a whole filegroup instead of specifying each constituent file individually. Note that if any file in a filegroup is offline (for example, because the file is being restored), the whole filegroup is offline and cannot be backed up.  
  
 File backups of read-only filegroups can be combined with partial backups. Partial backups include all the read/write filegroups and, optionally, one or more read-only filegroups. For more information, see [Partial Backups &#40;SQL Server&#41;](partial-backups-sql-server.md).  
  
 A file backup can serve as the *differential base* for differential file backups. For more information, see [Differential Backups &#40;SQL Server&#41;](differential-backups-sql-server.md).  
  
> [!NOTE]  
>  Full file backups are typically called *file backups*, except when they are being explicitly compared with *differential file backups*.  
  
 **In This Topic:**  
  
-   [Benefits of File Backups](#Benefits)  
  
-   [Disadvantages of File Backups](#Disadvantages)  
  
-   [Overview of File Backups](#Overview)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="Benefits"></a> Benefits of File Backups  
 File backups offer the following advantages over database backups:  
  
-   Using file backups can increase the speed of recovery by letting you restore only damaged files, without restoring the rest of the database.  
  
     For example, if a database consists of several files that are located on different disks and one disk fails, only the file on the failed disk has to be restored. The damaged file can be quickly restored, and recovery is faster than it would be for an entire database.  
  
-   File backups increase flexibility in scheduling and media handling over full database backups, which for very large databases can become unmanageable. The increased flexibility of file or filegroup backups is also useful for large databases that contain data that has varying update characteristics.  
  
##  <a name="Disadvantages"></a> Disadvantages of File Backups  
  
-   The primary disadvantage of file backups compared to full database backups is the additional administrative complexity. Maintaining and keeping track of a complete set of these backups can be a time-consuming task that might outweigh the space requirements of full database backups.  
  
-   A media failure can make a complete database unrecoverable if a damaged file lacks a backup. You must therefore maintain a complete set of file backups, and, for the full/bulk-logged recovery model, one or more log backups covering minimally the interval between the first full file backup and last full file backup.  
  
##  <a name="Overview"></a> Overview of File Backups  
 A full file backup backs up all the data in one or more files or filegroups. By default, file backups contain enough log records to roll forward the file to the end of the backup operation.  
  
 Backing up a read-only file or filegroup is the same for every recovery model. Under the full recovery model, a complete set of full file backups, together with enough log backups to span all the file backups, is the equivalent of a full database backup.  
  
 Only one file backup operation can occur at a time. You can back up multiple files in one operation, but this might extend the recovery time if you only have to restore a single file. This is because to locate that file, the whole backup is read.  
  
> [!NOTE]  
>  Individual files can be restored from a database backup; however, locating and restoring a file takes longer from a database backup than from a file backup.  
  
### File Backups and the Simple Recovery Model  
 Under the simple recovery model, read/write files must all be backed up together. This makes sure that the database can be restored to a consistent point in time. Instead of individually specifying each read/write file or filegroup, use the READ_WRITE_FILEGROUPS option. This option backs up all the read/write filegroups in the database. A backup that is created by specifying READ_WRITE_FILEGROUPS is known as a partial backup. For more information, see [Partial Backups &#40;SQL Server&#41;](partial-backups-sql-server.md).  
  
### File Backups and the Full Recovery Model  
 Under the full recovery model, you must back up the transaction log, regardless of the rest of your backup strategy. A complete set of full file backups, together with enough log backups to span all the file backups from the start of the first file backup, is the equivalent of a full database backup.  
  
 Restoring a database using just file and log backups can be complex. Therefore, if it is possible, it is a best practice to perform a full database backup and start the log backups before the first file backup. The following illustration shows a strategy in which a full database backup is taken (at time t1) soon after the database is created (at time t0). This first database backup enables transaction log backups to start. Transaction log backups are scheduled to occur at set intervals. File backups occur at whatever interval best meets the business requirements for the database. This illustration shows each of the four filegroups being backed up one at a time. The order in which they are backed up (A, C, B, A) reflects the business requirements of the database.  
  
 ![Strategy combining database, file, and log backups](../../database-engine/media/bnr-rmfull-3-fulldb-filegrps-log-backups.gif "Strategy combining database, file, and log backups")  
  
> [!NOTE]  
>  Under the full recovery model, you must roll forward the transaction log when restoring a read/write file backup to make sure that the file is consistent with the rest of the database. To avoid rolling forward a lot of transaction log backups, consider using differential file backups. For more information, see [Differential Backups &#40;SQL Server&#41;](differential-backups-sql-server.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To create a file or filegroup backup**  
  
-   [Back Up Files and Filegroups &#40;SQL Server&#41;](back-up-files-and-filegroups-sql-server.md)  
  
-   <xref:Microsoft.SqlServer.Management.Smo.Backup.SqlBackup%2A> (SMO)  
  
> [!NOTE]  
>  File backups are not supported by the Maintenance Plan Wizard.  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql)   
 [Backup Overview &#40;SQL Server&#41;](backup-overview-sql-server.md)   
 [Backup and Restore: Interoperability and Coexistence &#40;SQL Server&#41;](backup-and-restore-interoperability-and-coexistence-sql-server.md)   
 [Differential Backups &#40;SQL Server&#41;](differential-backups-sql-server.md)   
 [File Restores &#40;Simple Recovery Model&#41;](file-restores-simple-recovery-model.md)   
 [File Restores &#40;Full Recovery Model&#41;](file-restores-full-recovery-model.md)   
 [Online Restore &#40;SQL Server&#41;](online-restore-sql-server.md)   
 [Piecemeal Restores &#40;SQL Server&#41;](piecemeal-restores-sql-server.md)  
  
  
