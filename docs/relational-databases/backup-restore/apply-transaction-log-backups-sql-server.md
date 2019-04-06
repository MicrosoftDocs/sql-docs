---
title: "Apply Transaction Log Backups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "08/14/2016"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "restoring [SQL Server], log backups"
  - "transaction log backups [SQL Server], applying backups"
  - "online restores [SQL Server], log backups"
  - "transaction log backups [SQL Server], quantity needed for restore sequence"
  - "backups [SQL Server], log backups"
ms.assetid: 9b12be51-5469-46f9-8e86-e938e10aa3a1
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Apply Transaction Log Backups (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The topic is relevant only for the full recovery model or bulk-logged recovery model.  
  
 This topic describes applying transaction log backups as part of restoring a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
 
  
##  <a name="Requirements"></a> Requirements for restoring transaction log backups  
 To apply a transaction log backup, the following requirements must be met:  
  
-   **Enough Log Backups for a Restore Sequence :** You must have enough log records backed up to complete a restore sequence. The necessary log backups, including the [tail-log backup](../../relational-databases/backup-restore/tail-log-backups-sql-server.md) where required, must be available before the start of the restore sequence.  
  
-   **Correct restore order:**  The immediately previous full database backup or differential database backup must be restored first. Then, all transaction logs that are created after that full or differential database backup must be restored in chronological order. If a transaction log backup in this log chain is lost or damaged, you can restore only transaction logs before the missing transaction log.  
  
-   **Database not yet recovered:**  The database cannot be recovered until after the final transaction log has been applied. If you recover the database after restoring one of the intermediate transaction log backups, that before the end of the log chain, you cannot restore the database past that point without restarting the complete restore sequence, starting with the full database backup.  
  
    > **TIP!** A best practice is to restore all the log backups (RESTORE LOG *database_name* WITH NORECOVERY). Then, after restoring the last log backup, recover the database in a separate operation (RESTORE DATABASE *database_name* WITH RECOVERY).  
  
##  <a name="RecoveryAndTlogs"></a> Recovery and transaction logs  
 When you finish the restore operation and recover the database, recovery rolls back all incomplete transactions. This is known as the *undo phase*. Rolling back is required to restore the integrity of the database. After rollback, the database goes online, and no more transaction log backups can be applied to the database.  
  
 For example, a series of transaction log backups contain a long-running transaction. The start of the transaction is recorded in the first transaction log backup, but the end of the transaction is recorded in the second transaction log backup. There is no record of a commit or rollback operation in the first transaction log backup. If a recovery operation runs when the first transaction log backup is applied, the long-running transaction is treated as incomplete, and data modifications recorded in the first transaction log backup for the transaction are rolled back. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not allow for the second transaction log backup to be applied after this point.  
  
> **NOTE:** In some circumstances, you can explicitly add a file during log restore.  
  
##  <a name="PITrestore"></a> Use log backups to restore to the failure point  
 Assume the following sequence of events.  
  
|Time|Event|  
|----------|-----------|  
|8:00 A.M.|Back up database to create a full database backup.|  
|Noon|Back up transaction log.|  
|4:00 P.M.|Back up transaction log.|  
|6:00 P.M.|Back up database to create a full database backup.|  
|8:00 P.M.|Back up transaction log.|  
|9:45 P.M.|Failure occurs.|  
  
> For an explanation of this example sequence of backups, see [Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/transaction-log-backups-sql-server.md).  
  
 To restore the database to its state at 9:45 P.M. (the point of failure), either of the following alternative procedures can be used:  

[!INCLUDE[Freshness](../includes/paragraph-content/fresh-note-steps-feedback.md)]

 **Alternative 1: Restore the database by using the most recent full database backup**  
  
1.  Create a tail-log backup of the currently active transaction log as of the point of failure.  
  
2.  Do not restore the 8:00 A.M. full database backup. Instead, restore the more recent 6:00 P.M. full database backup, and then apply the 8:00 P.M. log backup and the tail-log backup.  
  
 **Alternative 2: Restore the database by using an earlier full database backup**  
  
> This alternative process is useful if a problem prevents you from using the 6:00 P.M. full database backup. This process takes longer than restoring from the 6:00 P.M. full database backup.  
  
1.  Create a tail-log backup of the currently active transaction log as of the point of failure.  
  
2.  Restore the 8:00 A.M. full database backup, and then restore all four transaction log backups in sequence. This rolls forward all completed transactions up to 9:45 P.M.  
  
     This alternative points out the redundant security offered by maintaining a chain of transaction log backups across a series of full database backups.  
  
> In some cases, you can also use transaction logs to restore a database to a specific point in time. For more information, [Restore a SQL Server Database to a Point in Time &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md).  
  
##  <a name="RelatedTasks"></a> Related tasks  
 **To apply a transaction log backup**  
  
-   [Restore a Transaction Log Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-transaction-log-backup-sql-server.md)  
  
 **To restore to your recovery point**  
  
-   [Restore a Database to the Point of Failure Under the Full Recovery Model &#40;Transact-SQL&#41;](../../relational-databases/backup-restore/restore-database-to-point-of-failure-full-recovery.md)  
  
-   [Restore a SQL Server Database to a Point in Time &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md)  
  
-   <xref:Microsoft.SqlServer.Management.Smo.Restore.SqlRestore%2A> (SMO)  
  
-   [Recovery of Related  Databases That Contain Marked Transaction](../../relational-databases/backup-restore/recovery-of-related-databases-that-contain-marked-transaction.md)  
  
-   [Recover to a Log Sequence Number &#40;SQL Server&#41;](../../relational-databases/backup-restore/recover-to-a-log-sequence-number-sql-server.md)  
  
 **To recover a database after restoring backups using WITH NORECOVERY**  
  
-   [Recover a Database Without Restoring Data &#40;Transact-SQL&#41;](../../relational-databases/backup-restore/recover-a-database-without-restoring-data-transact-sql.md)  
  
## See also  
 [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md)  
  
  
