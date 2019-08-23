---
title: "Partial Backups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "full backups [SQL Server]"
  - "partial backups [SQL Server]"
  - "READ_WRITE_FILEGROUPS option"
  - "database backups [SQL Server], about backing up databases"
ms.assetid: fe6b6bb1-38d0-46c4-bab8-31df14e8999c
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Partial Backups (SQL Server)
  All [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] recovery models support partial backups, so this topic is relevant for all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases. However, partial backups are designed for use under the simple recovery model to improve flexibility for backing up very large databases that contain one or more read-only filegroups.  
  
 Partial backups are useful whenever you want to exclude read-only filegroups. A *partial backup* resembles a full database backup, but a partial backup does not contain all the filegroups. Instead, for a read-write database, a partial backup contains the data in the primary filegroup, every read-write filegroup, and, optionally, one or more read-only files. A partial backup of a read-only database contains only the primary filegroup.  
  
> [!NOTE]  
>  If a read-only database is changed to read/write after a partial backup, there might be read/write secondary filegroups that are not in the partial backup. In this case, if you try to take a differential partial backup, the backup fails. Before you can take a differential partial backup of the database, you must take another partial backup. The new partial backup contains every read/write secondary filegroup and can serve as the base for differential partial backups.  
  
 File backups of read-only filegroups can be combined with partial backups. For information about file backups, see [Full File Backups &#40;SQL Server&#41;](full-file-backups-sql-server.md).  
  
 A partial backup can serve as the *differential base* for differential partial backups. For more information, see [Differential Backups &#40;SQL Server&#41;](differential-backups-sql-server.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
> [!NOTE]  
>  Partial backups are not supported by [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or the Maintenance Plan Wizard.  
  
 **To create a partial backup**  
  
-   [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql) (READ_WRITE_FILEGROUPS; FILEGROUP option, if needed)  
  
 **To use a partial backup in a restore sequence**  
  
-   [Example: Piecemeal Restore of Database &#40;Simple Recovery Model&#41;](example-piecemeal-restore-of-database-simple-recovery-model.md)  
  
-   [Example: Piecemeal Restore of Only Some Filegroups &#40;Simple Recovery Model&#41;](example-piecemeal-restore-of-only-some-filegroups-simple-recovery-model.md)  
  
## See Also  
 [Backup Overview &#40;SQL Server&#41;](backup-overview-sql-server.md)   
 [File Restores &#40;Simple Recovery Model&#41;](file-restores-simple-recovery-model.md)   
 [Piecemeal Restores &#40;SQL Server&#41;](piecemeal-restores-sql-server.md)  
  
  
