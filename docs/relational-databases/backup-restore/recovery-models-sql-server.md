---
title: "Recovery Models (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "07/16/2016"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "database backups [SQL Server], recovery models"
  - "bulk-logged recovery model [SQL Server]"
  - "recovery [SQL Server], recovery model"
  - "restoring transaction logs [SQL Server], recovery models"
  - "backing up databases [SQL Server], recovery models"
  - "recovery models [SQL Server], about"
  - "transaction log backups [SQL Server]"
  - "simple recovery model [SQL Server]"
  - "backups [SQL Server], recovery models"
  - "recovery models [SQL Server]"
  - "recovery models [SQL Server], transaction log management"
  - "database restores [SQL Server], recovery models"
  - "transaction log restores [SQL Server]"
  - "logs [SQL Server], recovery models"
  - "restoring databases [SQL Server], recovery models"
  - "full recovery model [SQL Server]"
  - "backing up transaction logs [SQL Server], recovery models"
ms.assetid: 8cfea566-8f89-4581-b30d-c53f1f2c79eb
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Recovery Models (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup and restore operations occur within the context of the recovery model of the database. Recovery models are designed to control transaction log maintenance. A *recovery model* is a database property that controls how transactions are logged, whether the transaction log requires (and allows) backing up, and what kinds of restore operations are available. Three recovery models exist: simple, full, and bulk-logged. Typically, a database uses the full recovery model or simple recovery model. A database can be switched to another recovery model at any time.  
  
 **In this Topic:**  
  
-   [Recovery Model Overview](#RMov)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="RMov"></a> Recovery Model Overview  
 The following table summarizes the three recovery models.  
  
|Recovery model|Description|Work loss exposure|Recover to point in time?|  
|--------------------|-----------------|------------------------|-------------------------------|  
|**Simple**|No log backups.<br /><br /> Automatically reclaims log space to keep space requirements small, essentially eliminating the need to manage the transaction log space. For information about database backups under the simple recovery model, see [Full Database Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/full-database-backups-sql-server.md).<br /><br /> Operations that require transaction log backups are not supported by the simple recovery model. The following features cannot be used in simple recovery mode:<br /><br /> -Log shipping<br /><br /> -Always On or Database mirroring<br /><br /> -Media recovery without data loss<br /><br /> -Point-in-time restores|Changes since the most recent backup are unprotected. In the event of a disaster, those changes must be redone.|Can recover only to the end of a backup. For more information, see [Complete Database Restores &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-simple-recovery-model.md). <br><br> For a more in depth explanation of the Simple recovery model, see [SQL Server Simple Recovery Model](https://www.mssqltips.com/sqlservertutorial/4/sql-server-simple-recovery-model/) provided by the folks at [MSSQLTips!](https://www.mssqltips.com)|  
|**Full**|Requires log backups.<br /><br /> No work is lost due to a lost or damaged data file.<br /><br /> Can recover to an arbitrary point in time (for example, prior to application or user error). For information about database backups under the full recovery model, see [Full Database Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/full-database-backups-sql-server.md) and [Complete Database Restores &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-full-recovery-model.md).|Normally none.<br /><br /> If the tail of the log is damaged, changes since the most recent log backup must be redone.|Can recover to a specific point in time, assuming that your backups are complete up to that point in time. For information about using log backups to restore to the point of failure, see [Restore a SQL Server Database to a Point in Time &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md).<br /><br /> Note: If you have two or more full-recovery-model databases that must be logically consistent, you may have to implement special procedures to make sure the recoverability of these databases. For more information, see [Recovery of Related  Databases That Contain Marked Transaction](../../relational-databases/backup-restore/recovery-of-related-databases-that-contain-marked-transaction.md).|  
|**Bulk logged**|Requires log backups.<br /><br /> An adjunct of the full recovery model that permits high-performance bulk copy operations.<br /><br /> Reduces log space usage by using minimal logging for most bulk operations. For information about operations that can be minimally logged, see [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md).<br /><br /> For information about database backups under the bulk-logged recovery model, see [Full Database Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/full-database-backups-sql-server.md) and [Complete Database Restores &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-full-recovery-model.md).|If the log is damaged or bulk-logged operations occurred since the most recent log backup, changes since that last backup must be redone.<br /><br /> Otherwise, no work is lost.|Can recover to the end of any backup. Point-in-time recovery is not supported.|  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [View or Change the Recovery Model of a Database &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-or-change-the-recovery-model-of-a-database-sql-server.md)  
  
-   [Troubleshoot a Full Transaction Log &#40;SQL Server Error 9002&#41;](../../relational-databases/logs/troubleshoot-a-full-transaction-log-sql-server-error-9002.md)  
  
## See Also  
 [backupset &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupset-transact-sql.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)   
 [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)   
 [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md)   
 [Automated Administration Tasks &#40;SQL Server Agent&#41;](https://msdn.microsoft.com/library/541ee5ac-2c9f-4b74-b4f0-13b7bd5920b0)   
 [Restore and Recovery Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md)  
  
  
