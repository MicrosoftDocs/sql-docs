---
title: "Tail-Log Backups (SQL Server) | Microsoft Docs"
description: In SQL Server, a tail-log backup captures any log records that have not yet been backed up to prevent data loss and to keep the log chain intact.
ms.custom: ""
ms.date: "08/01/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "backing up [SQL Server], tail of log"
  - "transaction log backups [SQL Server], tail-log backups"
  - "NO_TRUNCATE clause"
  - "backups [SQL Server], log backups"
  - "tail-log backups"
  - "backups [SQL Server], tail-log backups"
ms.assetid: 313ddaf6-ec54-4a81-a104-7ffa9533ca58
author: MashaMSFT
ms.author: mathoma
---
# Tail-Log Backups (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic is relevant only for backup and restore of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases that are using the full or bulk-logged recovery models.  
  
 A *tail-log backup* captures any log records that have not yet been backed up (the *tail of the log*) to prevent work loss and to keep the log chain intact. Before you can recover a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database to its latest point in time, you must back up the tail of its transaction log. The tail-log backup will be the last backup of interest in the recovery plan for the database.  
  
> [!NOTE]  
> Not all restore scenarios require a tail-log backup. You do not need a tail-log backup if the recovery point is contained in an earlier log backup. Also, a tail-log backup is unnecessary if you are moving or replacing (overwriting) a database and do not need to restore it to a point of time after its most recent backup.  
  
   ##  <a name="TailLogScenarios"></a> Scenarios That Require a Tail-Log Backup  
 We recommend that you take a tail-log backup in the following scenarios:  
  
-   If the database is online and you plan to perform a restore operation on the database, begin by backing up the tail of the log. To avoid an error for an online database, you must use the ... WITH NORECOVERY option of the [BACKUP](../../t-sql/statements/backup-transact-sql.md) [!INCLUDE[tsql](../../includes/tsql-md.md)] statement.  
  
-   If a database is offline and fails to start and you need to restore the database, first back up the tail of the log. Because no transactions can occur at this time, using the WITH NORECOVERY is optional.  
  
-   If a database is damaged, try to take a tail-log backup by using the WITH CONTINUE_AFTER_ERROR option of the BACKUP statement.  
  
     On a damaged database backing up the tail of the log can succeed only if the log files are undamaged, the database is in a state that supports tail-log backups, and the database does not contain any bulk-logged changes. If a tail-log backup cannot be created, any transactions committed after the latest log backup are lost.  
  
 The following table summarizes the BACKUP NORECOVERY and CONTINUE_AFTER_ERROR options.  
  
|BACKUP LOG option|Comments|  
|-----------------------|--------------|  
|NORECOVERY|Use NORECOVERY whenever you intend to continue with a restore operation on the database. NORECOVERY takes the database into the restoring state. This guarantees that the database does not change after the tail-log backup. The log will be truncated unless the NO_TRUNCATE option or COPY_ONLY option is also specified.<br /><br /> **Important:** Avoid using NO_TRUNCATE, except when the database is damaged. You may need to put the database into [single-user mode](../../relational-databases/databases/set-a-database-to-single-user-mode.md) to obtain exclusive access before performing the restore with NORECOVERY. After the restore, set the database back to multi-user mode. |  
|CONTINUE_AFTER_ERROR|Use CONTINUE_AFTER_ERROR only if you are backing up the tail of a damaged database.<br /><br /> When you use back up the tail of the log on a damaged database, some of the metadata ordinarily captured in log backups might be unavailable. For more information, see [Tail-Log Backups That Have Incomplete Backup Metadata](#IncompleteMetadata), in this topic.|  
  
##  <a name="IncompleteMetadata"></a> Tail-Log backups that have incomplete backup metadata  
 Tail log backups capture the tail of the log even if the database is offline, damaged, or missing data files. This might cause incomplete metadata from the restore information commands and **msdb**. However, only the metadata is incomplete; the captured log is complete and usable.  
  
 If a tail-log backup has incomplete metadata, in the [backupset](../../relational-databases/system-tables/backupset-transact-sql.md) table, **has_incomplete_metadata** is set to **1**. Also, in the output of [RESTORE HEADERONLY](../../t-sql/statements/restore-statements-headeronly-transact-sql.md), **HasIncompleteMetadata** is set to **1**.  
  
 If the metadata in a tail-log backup is incomplete, the [backupfilegroup](../../relational-databases/system-tables/backupfilegroup-transact-sql.md) table will be missing most of the information about filegroups at the time of the tail-log backup. Most of the **backupfilegroup** table columns are NULL; the only meaningful columns are as follows:  
  
-   **backup_set_id**  
-   **filegroup_id**  
-   **type**  
-   **type_desc**  
-   **is_readonly**  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 To create a tail-log backup, see [Back Up the Transaction Log When the Database Is Damaged &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-the-transaction-log-when-the-database-is-damaged-sql-server.md).  
  
 To restore a transaction log backup, see [Restore a Transaction Log Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-transaction-log-backup-sql-server.md).  
    
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)   
 [Copy-Only Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/copy-only-backups-sql-server.md)   
 [Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/transaction-log-backups-sql-server.md)   
 [Apply Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/apply-transaction-log-backups-sql-server.md)    
 [SQL Server Transaction Log Architecture and Management Guide](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md)
  
