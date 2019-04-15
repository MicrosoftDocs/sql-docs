---
title: "Example: Piecemeal Restore of Only Some Filegroups (Full Recovery Model) | Microsoft Docs"
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
ms.assetid: bced4b54-e819-472b-b784-c72e14e72a0b
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Example: Piecemeal Restore of Only Some Filegroups (Full Recovery Model)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  This topic is relevant for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases under the full recovery model that contain multiple files or filegroups.  
  
 A piecemeal restore sequence restores and recovers a database in stages at the filegroup level, starting with the primary and all read/write, secondary filegroups.  
  
 In this example, a database named `adb`, which uses the full recovery model, contains three filegroups. Filegroup `A` is read/write, and filegroup `B` and filegroup `C` are read-only. Initially, all of the filegroups are online.  
  
 The primary and filegroup `B` of database `adb` appear to be damaged. The primary filegroup is fairly small and can be restored quickly. The database administrator decides to restore them by using a piecemeal restore sequence. First, the primary filegroup and the subsequent transaction logs are restored the database is recovered.  
  
 The intact filegroups `A` and `C` contain critical data. Therefore, they will be recovered next to bring them online as quickly as possible. Finally, the damaged secondary filegroup, `B`, is restored and recovered.  
  
## Restore Sequences:  
  
> [!NOTE]  
>  The syntax for an online restore sequence is the same as for an offline restore sequence.  
  
1.  Create a tail log backup of database `adb`. This step is essential to make the intact filegroups `A` and `C` current with the recovery point of the database.  
  
    ```  
    BACKUP LOG adb TO tailLogBackup WITH NORECOVERY  
    ```  
  
2.  Partial restore of the primary filegroup.  
  
    ```  
    RESTORE DATABASE adb FILEGROUP='Primary' FROM backup   
    WITH PARTIAL, NORECOVERY  
    RESTORE LOG adb FROM log_backup1 WITH NORECOVERY  
    RESTORE LOG adb FROM log_backup2 WITH NORECOVERY  
    RESTORE LOG adb FROM log_backup3 WITH NORECOVERY  
    RESTORE LOG adb FROM tailLogBackup WITH RECOVERY  
    ```  
  
     At this point the primary is online. Files in filegroups `A`, `B`, and `C` are recovery pending, and the filegroups are offline.  
  
3.  Online restore of filegroups `A` and `C`.  
  
     Because their data is undamaged, these filegroups do not have to be restored from a backup, but they do have to be recovered to bring them online.  
  
     The database administrator recovers `A` and `C` immediately.  
  
    ```  
    RESTORE DATABASE adb FILEGROUP='A', FILEGROUP='C' WITH RECOVERY  
    ```  
  
     At this point the primary and filegroups `A` and `C` are online. Files in filegroup `B` remain recovery pending, with the filegroup offline.  
  
4.  Online restore of filegroup `B`.  
  
     Files in filegroup `B` are restored any time thereafter.  
  
    > [!NOTE]  
    >  The backup of filegroup `B` was taken after the filegroup became read-only; therefore, these files do not have to be rolled forward.  
  
    ```  
    RESTORE DATABASE adb FILEGROUP='B' FROM backup WITH RECOVERY  
    ```  
  
     All filegroups are now online.  
  
## Additional Examples  
  
-   [Example: Piecemeal Restore of Database &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/example-piecemeal-restore-of-database-simple-recovery-model.md)  
  
-   [Example: Piecemeal Restore of Only Some Filegroups &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/example-piecemeal-restore-of-only-some-filegroups-simple-recovery-model.md)  
  
-   [Example: Online Restore of a Read-Only File &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/example-online-restore-of-a-read-only-file-simple-recovery-model.md)  
  
-   [Example: Piecemeal Restore of Database &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-piecemeal-restore-of-database-full-recovery-model.md)  
  
-   [Example: Online Restore of a Read-Write File &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-online-restore-of-a-read-write-file-full-recovery-model.md)  
  
-   [Example: Online Restore of a Read-Only File &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-online-restore-of-a-read-only-file-full-recovery-model.md)  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [Online Restore &#40;SQL Server&#41;](../../relational-databases/backup-restore/online-restore-sql-server.md)   
 [Apply Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/apply-transaction-log-backups-sql-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Piecemeal Restores &#40;SQL Server&#41;](../../relational-databases/backup-restore/piecemeal-restores-sql-server.md)  
  
  
