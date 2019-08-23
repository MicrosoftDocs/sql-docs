---
title: "Online Restore (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "online restores [SQL Server]"
  - "online restores [SQL Server], about online restores"
ms.assetid: 7982a687-980a-4eb8-8e9f-6894148e7d8c
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Online Restore (SQL Server)
  Online restore is supported only on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Enterprise edition. In this edition, a file, page, or piecemeal restore is online by default. This topic is relevant for databases that contain multiple files or filegroups (and, under the simple recovery model, only for read-only filegroups).  
  
 Restoring data while the database is online is called an *online restore*. A database is considered to be online whenever the primary filegroup is online, even if one or more of its secondary filegroups are offline. Under any recovery model, you can restore a file that is offline while the database is online. Under the full recovery model, you can also restore pages while the database is online.  
  
> [!NOTE]  
>  Online restore occurs automatically on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Enterprise and requires no user action. If you do not want to use online restore, you can take a database offline before you start a restore. For more information, see [Taking a Database or File Offline](#taking_db_or_file_offline), later in this topic.  
  
 During an online file restore, any file being restored and its filegroup are offline. If any of these files is online when an online restore starts, the first restore statement takes the filegroup of the file offline. In contrast, during an online page restore, only the page is offline.  
  
 Every online restore scenario involves the following basic steps:  
  
1.  Restore the data.  
  
2.  Restore the log by using WITH RECOVERY for the last log restore. This brings the restored data online.  
  
 Occasionally, an uncommitted transaction cannot be rolled back because the data that is required by rollback is offline during startup. In this case, the transaction is deferred. For more information, see [Deferred Transactions &#40;SQL Server&#41;](deferred-transactions-sql-server.md).  
  
> [!NOTE]  
>  If the database is currently using the bulk-logged recovery model, we recommend that you switch to the full recovery model before you start an online restore. For more information, see [View or Change the Recovery Model of a Database &#40;SQL Server&#41;](view-or-change-the-recovery-model-of-a-database-sql-server.md).  
  
> [!IMPORTANT]  
>  If the backups were taken with multiple devices that were attached to the server, the same number of devices must be available during an online restore.  
  
## Log Backups for Online Restore  
 In an online restore, the recovery point is the point when the data being restored was taken offline or made read-only for the last time. The transaction log backups leading up to and including this recovery point must all be available. Generally, a log backup is required after that point to cover the recovery point for the file. The only exception is during an online restore of read-only data from a data backup that was taken after the data became read-only. In this case, you do not have to have a log backup.  
  
 Generally, you may take transaction log backups while the database is online, even after the start of the restore sequence. The timing of the last log backup depends on the properties of the file being restored:  
  
-   For an online read-only file, you can take the last log backup that is required for recovery before or during the first restore sequence. A read-only filegroup may not require log backups if a data or differential backup was taken after the filegroup became read-only.  
  
    > [!NOTE]  
    >  The preceding information also applies to every offline file.  
  
-   A special case exists for a read/write file that was online when the first restore statement was issued and that was then automatically taken offline by that restore statement. In this case, you must take a log backup during the first *restore sequence* (the sequence of one or more RESTORE statements that restore, roll forward, and recover data). Generally, this log backup must occur after you restore all the full backups and before you recover the data. However, if there are multiple file backups for a specific filegroup, the minimal point of log backup is the time after the filegroup is offline. This post-data-restore log backup captures the point at which the file was taken offline. The post-data-restore log backup is necessary because the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] cannot use online log for an online restore.  
  
    > [!NOTE]  
    >  Alternatively, you can manually take the file offline before the restore sequence. For more information, see "Taking a Database or File Offline" later in this topic.  
  
##  <a name="taking_db_or_file_offline"></a> Taking a Database or File Offline  
 If you do not want to use online restore, you can take the database offline before you start the restore sequence by using one of the following methods:  
  
-   Under any recovery model, you can take the database offline by using the following [ALTER DATABASE](/sql/t-sql/statements/alter-database-transact-sql) statement:  
  
     ALTER DATABASE *database_name* SET OFFLINE  
  
-   Alternatively, under the full recovery model, you can force a file or page restore to be offline, by using the following [BACKUP LOG](/sql/t-sql/statements/backup-transact-sql) statement put the database in to the restoring state:  
  
     BACKUP LOG *database_name* WITH NORECOVERY.  
  
 As long as a database remains offline, all restores are offline restores.  
  
## Examples  
  
> [!NOTE]  
>  The syntax for an online restore sequence is the same as for an offline restore sequence.  
  
-   [Example: Piecemeal Restore of Database &#40;Simple Recovery Model&#41;](example-piecemeal-restore-of-database-simple-recovery-model.md)  
  
-   [Example: Piecemeal Restore of Only Some Filegroups &#40;Simple Recovery Model&#41;](example-piecemeal-restore-of-only-some-filegroups-simple-recovery-model.md)  
  
-   [Example: Online Restore of a Read-Only File &#40;Simple Recovery Model&#41;](example-online-restore-of-a-read-only-file-simple-recovery-model.md)  
  
-   [Example: Piecemeal Restore of Database &#40;Full Recovery Model&#41;](example-piecemeal-restore-of-database-full-recovery-model.md)  
  
-   [Example: Piecemeal Restore of Only Some Filegroups &#40;Full Recovery Model&#41;](example-piecemeal-restore-of-only-some-filegroups-full-recovery-model.md)  
  
-   [Example: Online Restore of a Read-Write File &#40;Full Recovery Model&#41;](example-online-restore-of-a-read-write-file-full-recovery-model.md)  
  
-   [Example: Online Restore of a Read-Only File &#40;Full Recovery Model&#41;](example-online-restore-of-a-read-only-file-full-recovery-model.md)  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Restore Files and Filegroups &#40;SQL Server&#41;](restore-files-and-filegroups-sql-server.md)  
  
-   [Restore Pages &#40;SQL Server&#41;](restore-pages-sql-server.md)  
  
-   [Manage the suspect_pages Table &#40;SQL Server&#41;](manage-the-suspect-pages-table-sql-server.md)  
  
-   [Recover a Database Without Restoring Data &#40;Transact-SQL&#41;](recover-a-database-without-restoring-data-transact-sql.md)  
  
-   [Remove Defunct Filegroups &#40;SQL Server&#41;](remove-defunct-filegroups-sql-server.md)  
  
## See Also  
 [File Restores &#40;Full Recovery Model&#41;](file-restores-full-recovery-model.md)   
 [File Restores &#40;Simple Recovery Model&#41;](file-restores-simple-recovery-model.md)   
 [Restore Pages &#40;SQL Server&#41;](restore-pages-sql-server.md)   
 [Piecemeal Restores &#40;SQL Server&#41;](piecemeal-restores-sql-server.md)   
 [Restore and Recovery Overview &#40;SQL Server&#41;](restore-and-recovery-overview-sql-server.md)  
  
  
