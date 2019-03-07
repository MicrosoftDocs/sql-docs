---
title: "File Restores (Simple Recovery Model) | Microsoft Docs"
ms.custom: ""
ms.date: "03/24/2017"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "file restores [SQL Server]"
  - "simple recovery model [SQL Server]"
  - "restoring files [SQL Server], Transact-SQL restore sequence"
  - "restoring files [SQL Server]"
  - "Transact-SQL restore sequence"
  - "restoring files [SQL Server], simple recovery model"
  - "file restores [SQL Server], simple recovery model"
  - "file restores [SQL Server], Transact-SQL restore sequence"
ms.assetid: b6d07386-7c6f-4cc6-be32-93289adbd3d6
author: mashamsft
ms.author: mathoma
manager: craigg
---
# File Restores (Simple Recovery Model)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  This topic is relevant only for simple-model databases that contain at least one read-only secondary filegroup.  
  
 In a file restore, the goal is to restore one or more damaged files without restoring the whole database. Under the simple recovery model, file backups are supported only for read-only files. The primary filegroup and read/write secondary filegroups are always restored together, by restoring a database or partial backup.  
  
 The file-restore scenarios are as follows:  
  
-   Offline file restore  
  
     In an *offline file restore*, the database is offline while damaged files or filegroups are restored. At the end of the restore sequence, the database comes online.  
  
     All editions of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] support offline file restore.  
  
-   Online file restore  
  
     In an *online file restore*, if database is online at restore time, it remains online during the file restore. However, each filegroup in which a file is being restored is offline during the restore operation. After all the files in an offline filegroup are recovered, the filegroup is automatically brought online.  
  
     For information about support for online page and file restore, see [Database Engine Features and Tasks](https://msdn.microsoft.com/library/d9efe145-3306-4d61-bd77-e2af43e19c34). For more information about online restores, see [Online Restore &#40;SQL Server&#41;](../../relational-databases/backup-restore/online-restore-sql-server.md).  
  
    > [!TIP]  
    >  If you want the database to be offline for a file restore, take the database offline before you start the restore sequence by executing the following [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql-set-options.md) statement: ALTER DATABASE *database_name* SET OFFLINE.  
  
 **In this Topic:**  
  
-   [Overview of File and Filegroup Restore Under the Simple Recovery Model](#Overview)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="Overview"></a> Overview of File and Filegroup Restore Under the Simple Recovery Model  
 A file restore scenario consists of a single restore sequence that copies, rolls forward, and recovers the appropriate data as follows:  
  
1.  Restore each damaged file from its most recent file backup.  
  
2.  Restore the most recent differential file backup for each restored file and recover the database.  
  
### Transact-SQL Steps for File Restore Sequence (Simple Recovery Model)  
 This section shows the essential [!INCLUDE[tsql](../../includes/tsql-md.md)][RESTORE](../../t-sql/statements/restore-statements-transact-sql.md) options for a simple file-restore sequence. Syntax and details that are not relevant to this purpose are omitted.  
  
 The restore sequence contains only two [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. The first statement restores a secondary file, file `A`, which is restored using WITH NORECOVERY. The second operation restores two other files, `B` and `C` which are restored using WITH RECOVERY from a different backup device:  
  
1.  RESTORE DATABASE *database* FILE **=**_name_of_file_A_  
  
     FROM *file_backup_of_file_A*  
  
     WITH NORECOVERY**;**  
  
2.  RESTORE DATABASE *database* FILE **=**_name_of_file_B_**,**_name_of_file_C_  
  
     FROM *file_backup_of_files_B_and_C*  
  
     WITH RECOVERY**;**  
  
### Examples  
  
-   [Example: Online Restore of a Read-Only File &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/example-online-restore-of-a-read-only-file-simple-recovery-model.md)  
  
-   [Example: Offline Restore of Primary and One Other Filegroup &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-offline-restore-of-primary-and-one-other-filegroup-full-recovery-model.md)  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To restore files and filegroups**  
  
-   [Restore Files and Filegroups over Existing Files &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-files-and-filegroups-over-existing-files-sql-server.md)  
  
-   [Restore Files and Filegroups &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-files-and-filegroups-sql-server.md)  
  
-   [Restore Files and Filegroups &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-files-and-filegroups-sql-server.md)  
  
-   [Restore.SqlRestore Method (Server) (SMO)](https://msdn.microsoft.com/library/microsoft.sqlserver.management.smo.restore.sqlrestore.aspx)   
  
## See Also  
 [Backup and Restore: Interoperability and Coexistence &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-and-restore-interoperability-and-coexistence-sql-server.md)   
 [Differential Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/differential-backups-sql-server.md)   
 [Full File Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/full-file-backups-sql-server.md)   
 [Backup Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-overview-sql-server.md)   
 [Restore and Recovery Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Complete Database Restores &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-simple-recovery-model.md)   
 [Piecemeal Restores &#40;SQL Server&#41;](../../relational-databases/backup-restore/piecemeal-restores-sql-server.md)  
  
  
