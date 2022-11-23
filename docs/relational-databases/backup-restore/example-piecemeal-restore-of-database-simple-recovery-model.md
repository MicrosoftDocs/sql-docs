---
title: "Piecemeal restore: simple recovery model"
description: This example shows a piecemeal restore in SQL Server of a database to a new computer using the simple recovery model.
ms.custom: seo-lt-2019
ms.date: "12/17/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "piecemeal restores [SQL Server], simple recovery model"
  - "restore sequences [SQL Server], piecemeal"
  - "simple recovery model [SQL Server], RESTORE examples"
ms.assetid: 9834b14a-4e56-4654-b190-c2a38624b6b4
author: MashaMSFT
ms.author: mathoma
---
# Example: Piecemeal Restore of Database (Simple Recovery Model)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  A piecemeal restore sequence restores and recovers a database in stages at the filegroup level, starting with the primary and all read/write, secondary filegroups.  
  
 In this example, database `adb` is restored to a new computer after a disaster. The database is using the simple recovery model. Before the disaster, all the filegroups are online. Filegroups `A` and `C` are read/write, and filegroup `B` is read-only. Filegroup `B` became read-only before the most recent partial backup, which contains the primary filegroup and the read/write secondary filegroups, `A` and `C`. After filegroup `B` became read-only, a separate file backup of filegroup `B` was taken.  
  
## Restore Sequences  
  
1.  Partial restore of the primary and filegroups `A` and `C`.  
  
    ```  
    RESTORE DATABASE adb FILEGROUP='A',FILEGROUP='C'   
       FROM partial_backup   
       WITH PARTIAL, RECOVERY;  
  
    ```  
  
     At this point, the primary and filegroups `A` and `C` are online. All files in filegroup `B` are recovery pending, and the filegroup is offline.  
  
2.  Online restore of filegroup `B`.  
  
    ```  
    RESTORE DATABASE adb FILEGROUP='B' FROM backup WITH RECOVERY;  
  
    ```  
  
     All filegroups are now online.  
  
## Additional Examples  
  
-   [Example: Piecemeal Restore of Only Some Filegroups &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/example-piecemeal-restore-of-only-some-filegroups-simple-recovery-model.md)  
  
-   [Example: Online Restore of a Read-Only File &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/example-online-restore-of-a-read-only-file-simple-recovery-model.md)  
  
-   [Example: Piecemeal Restore of Database &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-piecemeal-restore-of-database-full-recovery-model.md)  
  
-   [Example: Piecemeal Restore of Only Some Filegroups &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-piecemeal-restore-of-only-some-filegroups-full-recovery-model.md)  
  
-   [Example: Online Restore of a Read-Write File &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-online-restore-of-a-read-write-file-full-recovery-model.md)  
  
-   [Example: Online Restore of a Read-Only File &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-online-restore-of-a-read-only-file-full-recovery-model.md)  
  
## See Also  
 [Online Restore &#40;SQL Server&#41;](../../relational-databases/backup-restore/online-restore-sql-server.md)   
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Piecemeal Restores &#40;SQL Server&#41;](../../relational-databases/backup-restore/piecemeal-restores-sql-server.md)  
  
  
