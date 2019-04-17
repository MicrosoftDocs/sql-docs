---
title: "Example: Piecemeal Restore of Database (Full Recovery Model) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "full recovery model [SQL Server], RESTORE example"
  - "piecemeal restores [SQL Server], full recovery model"
  - "restore sequences [SQL Server], piecemeal"
ms.assetid: 0a84892d-2f7a-4e77-b2d0-d68b95595210
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Example: Piecemeal Restore of Database (Full Recovery Model)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  A piecemeal restore sequence restores and recovers a database in stages at the filegroup level, beginning with the primary and all read-write, secondary filegroups.  
  
 In this example, database `adb` is restored to a new computer after a disaster. The database is using the full recovery model; therefore, before the restore starts, a tail-log backup must be taken of the database. Before the disaster, all the filegroups are online. Filegroup `B` is read-only. All of the secondary filegroups must be restored, but they are restored in order of importance: `A` (highest), `C`, and lastly `B`. In this example, there are four log backups, including the tail-log backup.  
  
## Tail-Log Backup  
 Before restoring the database, the database administrator must back up the tail of the log. Because the database is damaged, creating the tail-log backup requires using the NO_TRUNCATE option:  
  
```  
BACKUP LOG adb TO tailLogBackup WITH NORECOVERY, NO_TRUNCATE  
```  
  
 The tail-log backup is the last backup that is applied in the following restore sequences.  
  
## Restore Sequences  
  
> [!NOTE]  
>  The syntax for an online restore sequence is the same as for an offline restore sequence.  
  
1.  Partial restore of the primary and secondary filegroup `A`.  
  
    ```  
    RESTORE DATABASE adb FILEGROUP='Primary' FROM backup1   
       WITH PARTIAL, NORECOVERY  
    RESTORE DATABASE adb FILEGROUP='A' FROM backup2   
       WITH NORECOVERY  
    RESTORE LOG adb FROM log_backup3 WITH NORECOVERY  
    RESTORE LOG adb FROM log_backup4 WITH NORECOVERY  
    RESTORE LOG adb FROM log_backup5 WITH NORECOVERY  
    RESTORE LOG adb FROM tailLogBackup WITH RECOVERY  
    ```  
  
2.  Online restore of filegroup `C`.  
  
     At this point, the primary filegroup and secondary filegroup `A` are online. All the files in filegroups `B` and `C` are recovery pending, and the filegroups are offline.  
  
     Messages from the last `RESTORE LOG` statement in step 1 indicate that rollback of transactions that involve filegroup `C` was deferred, because this filegroup is not available. Regular operations can continue, but locks are held by these transactions and log truncation will not occur until the rollback can complete.  
  
     In the second restore sequence, the database administrator restores filegroup `C`:  
  
    ```  
    RESTORE DATABASE adb FILEGROUP='C' FROM backup2a WITH NORECOVERY  
    RESTORE LOG adb FROM log_backup3 WITH NORECOVERY  
    RESTORE LOG adb FROM log_backup4 WITH NORECOVERY  
    RESTORE LOG adb FROM log_backup5 WITH NORECOVERY  
    RESTORE LOG adb FROM tailLogBackup WITH RECOVERY  
    ```  
  
     At this point the primary and filegroups `A` and `C` are online. Files in filegroup `B` remain recovery pending, with the filegroup offline. Deferred transactions have been resolved, and log truncation occurs.  
  
3.  Online restore of filegroup `B`.  
  
     In the third restore sequence, the database administrator restores filegroup `B`. The backup of filegroup `B` was taken after the filegroup became read-only; therefore, it does not have to be rolled forward during recovery.  
  
    ```  
    RESTORE DATABASE adb FILEGROUP='B' FROM backup2b WITH RECOVERY  
    ```  
  
     All filegroups are now online.  
  
## Additional Examples  
  
-   [Example: Piecemeal Restore of Database &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/example-piecemeal-restore-of-database-simple-recovery-model.md)  
  
-   [Example: Piecemeal Restore of Only Some Filegroups &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/example-piecemeal-restore-of-only-some-filegroups-simple-recovery-model.md)  
  
-   [Example: Online Restore of a Read-Only File &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/example-online-restore-of-a-read-only-file-simple-recovery-model.md)  
  
-   [Example: Piecemeal Restore of Only Some Filegroups &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-piecemeal-restore-of-only-some-filegroups-full-recovery-model.md)  
  
-   [Example: Online Restore of a Read-Write File &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-online-restore-of-a-read-write-file-full-recovery-model.md)  
  
-   [Example: Online Restore of a Read-Only File &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-online-restore-of-a-read-only-file-full-recovery-model.md)  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [Online Restore &#40;SQL Server&#41;](../../relational-databases/backup-restore/online-restore-sql-server.md)   
 [Apply Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/apply-transaction-log-backups-sql-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Piecemeal Restores &#40;SQL Server&#41;](../../relational-databases/backup-restore/piecemeal-restores-sql-server.md)  
  
  
