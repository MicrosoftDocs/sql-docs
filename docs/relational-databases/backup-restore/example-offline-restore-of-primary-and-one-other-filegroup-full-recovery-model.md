---
title: "Example Offline Restore: Primary and 1 Filegroup (Full Recovery Model) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "full recovery model [SQL Server], RESTORE example"
  - "offline restores [SQL Server]"
  - "restore sequences [SQL Server], offline"
ms.assetid: 7d6c50eb-dc84-4d66-855a-0b5f1bd89737
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Example: Offline Restore of Primary and One Other Filegroup (Full Recovery Model)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  This topic is relevant only for databases under the full recovery model that contain multiple filegroups.  
  
 In this example, a database named `adb` contains three filegroups. Filegroups `A` and `C` are read/write, and filegroup `B` is read-only. The primary filegroup and filegroup `B` are damaged, but filegroups `A` and `C` are intact. Before the disaster, all the filegroups were online.  
  
 The database administrator decides to restore and recover the primary filegroup and filegroup `B`. The database is using the full recovery model; therefore, before the restore starts, a tail-log backup must be taken of the database. When the database comes on line, Filegroups `A` and `C` are automatically brought online.  
  
> [!NOTE]  
>  The offline restore sequence has fewer steps than an online restore of a read-only file. For an example, see [Example: Online Restore of a Read-Only File &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-online-restore-of-a-read-only-file-full-recovery-model.md). However, the whole database is offline for the duration of the sequence.  
  
## Tail-Log Backup  
 Before restoring the database, the database administrator must back up the tail of the log. Because the database is damaged, creating the tail-log backup requires using the NO_TRUNCATE option:  
  
```  
BACKUP LOG adb TO tailLogBackup   
   WITH NORECOVERY, NO_TRUNCATE  
```  
  
 The tail-log backup is the last backup that is applied in the following restore sequences.  
  
## Restore Sequence  
 To restore the primary filegroup and filegroup `B`, the database administrator uses a restore sequence without the PARTIAL option, as follows:  
  
```  
RESTORE DATABASE adb FILEGROUP='Primary' FROM backup1   
WITH NORECOVERY  
RESTORE DATABASE adb FILEGROUP='B' FROM backup2   
WITH NORECOVERY  
RESTORE LOG adb FROM backup3 WITH NORECOVERY  
RESTORE LOG adb FROM backup4 WITH NORECOVERY  
RESTORE LOG adb FROM backup5 WITH NORECOVERY  
RESTORE LOG adb FROM tailLogBackup WITH RECOVERY  
```  
  
 The files that are not restored are automatically brought online. All the filegroups are now online.  
  
## See Also  
 [Online Restore &#40;SQL Server&#41;](../../relational-databases/backup-restore/online-restore-sql-server.md)   
 [Piecemeal Restores &#40;SQL Server&#41;](../../relational-databases/backup-restore/piecemeal-restores-sql-server.md)   
 [File Restores &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/file-restores-full-recovery-model.md)   
 [Apply Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/apply-transaction-log-backups-sql-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)  
  
  
