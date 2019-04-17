---
title: "Recovery of Related  Databases That Contain Marked Transaction | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "transaction logs [SQL Server], marks"
  - "STOPBEFOREMARK option [RESTORE statement]"
  - "STOPATMARK option [RESTORE statement]"
  - "point in time recovery [SQL Server]"
  - "restoring databases [SQL Server], point in time"
  - "recovery [SQL Server], databases"
  - "restoring [SQL Server], point in time"
  - "transactions [SQL Server], recovering to a mark"
  - "database recovery [SQL Server]"
  - "marked transactions [SQL Server], restoring"
  - "database restores [SQL Server], point in time"
ms.assetid: 77a0d9c0-978a-4891-8b0d-a4256c81c3f8
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Recovery of Related  Databases That Contain Marked Transaction
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic is relevant only for databases that contain marked transactions and that use the full or bulk-logged recovery models.  
  
 For information about the requirements for restoring to a specific recovery point, see [Restore a SQL Server Database to a Point in Time &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md).  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports inserting named marks into the transaction log to allow recovery to that specific mark. Log marks are transaction specific and are inserted only if their associated transaction commits. As a result, marks can be tied to specific work, and you can recover to a point that includes or excludes this work.  
  
 Before you insert named marks into the transaction log, consider the following:  
  
-   Because transaction marks consume log space, use them only for transactions that play a significant role in the database recovery strategy.  
  
-   After a marked transaction commits, a row is inserted in the [logmarkhistory](../../relational-databases/system-tables/logmarkhistory-transact-sql.md) table in **msdb**.  
  
-   If a marked transaction spans multiple databases on the same database server or on different servers, the marks must be recorded in the logs of all the affected databases. For more information, see [Use Marked Transactions to Recover Related Databases Consistently &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/use-marked-transactions-to-recover-related-databases-consistently.md).  
  
> [!NOTE]  
>  For information about how to mark transactions, see [Use Marked Transactions to Recover Related Databases Consistently &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/use-marked-transactions-to-recover-related-databases-consistently.md).  
  
## Transact-SQL Syntax for Inserting Named Marks into a Transaction Log  
 To insert marks into the transaction logs, use the [BEGIN TRANSACTION](../../t-sql/language-elements/begin-transaction-transact-sql.md) statement and the WITH MARK [*description*] clause. The mark is named the same as the transaction. The optional *description* is a textual description of the mark, not the mark name. For example, the name of both the transaction and the mark that is created in the following `BEGIN TRANSACTION` statement is `Tx1`:  
  
```wmimof  
BEGIN TRANSACTION Tx1 WITH MARK 'not the mark name, just a description'    
```  
  
 The transaction log records the mark name (transaction name), description, database, user, **datetime** information, and the log sequence number (LSN). The **datetime** information is used with the mark name to uniquely identify the mark.  
  
 For information about how to insert a mark into a transaction that spans multiple databases, see [Use Marked Transactions to Recover Related Databases Consistently &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/use-marked-transactions-to-recover-related-databases-consistently.md).  
  
## Transact-SQL Syntax for Recovering to a Mark  
 When you target a marked transaction by using a[RESTORE LOG](../../t-sql/statements/restore-statements-transact-sql.md)statement, you can use one the following clauses to stop at or immediately before the mark:  
  
-   Use the WITH STOPATMARK = **'**_<mark_name>_**'** clause to specify that the marked transaction is the recovery point.  
  
     STOPATMARK rolls forward to the mark and includes the marked transaction in the roll forward.  
  
-   Use the WITH STOPBEFOREMARK = **'**_<mark_name>_**'** clause to specify that the log record that is immediately before the mark is the recovery point.  
  
     STOPBEFOREMARK rolls forward to the mark and excludes marked the transaction from the roll forward.  
  
 The STOPATMARK and STOPBEFOREMARK options both support an optional AFTER *datetime* clause. When *datetime* is used, mark names do not have to be unique.  
  
 If AFTER *datetime* is omitted, roll forward stops at the first mark that has the specified name. If AFTER *datetime* is specified, roll forward stops at the first mark that has the specified name, exactly at or after *datetime*.  
  
> [!NOTE]  
>  As in all point-in-time restore operations, recovering to a mark is disallowed when the database is undergoing operations that are bulk-logged.  
  
 **To restore to a marked transaction**  
  
 [Restore a Database to a Marked Transaction &#40;SQL Server Management Studio&#41;](../../relational-databases/backup-restore/restore-a-database-to-a-marked-transaction-sql-server-management-studio.md)  
  
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)  
  
### Preparing the Log Backups  
 For this example, an appropriate backup strategy for these related databases would be the following:  
  
1.  Use the full recovery model for both databases.  
  
2.  Create a full backup of each database.  
  
     The databases can be backed up sequentially or simultaneously.  
  
3.  Before backing up the transaction log, mark a transaction that executes in all databases. For information about how to create the marked transactions, see [Use Marked Transactions to Recover Related Databases Consistently &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/use-marked-transactions-to-recover-related-databases-consistently.md).  
  
4.  Back up the transaction log on each database.  
  
### Recovering the Database to a Marked Transaction  
 **To restore the backup**  
  
1.  Create [tail-log backups](../../relational-databases/backup-restore/tail-log-backups-sql-server.md) of the undamaged databases, if possible.  
  
2.  Restore the most recent full database backup of each database.  
  
3.  Identify the most recent marked transaction that is available in all of the transaction log backups. This information is stored in the **logmarkhistory** table in the **msdb** database on each server.  
  
4.  Identify the log backups for all related databases that contain this mark.  
  
5.  Restore each log backup, stopping at the marked transaction.  
  
6.  Recover each database.  
  
## See Also  
 [BEGIN TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-transaction-transact-sql.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Apply Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/apply-transaction-log-backups-sql-server.md)   
 [Use Marked Transactions to Recover Related Databases Consistently &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/use-marked-transactions-to-recover-related-databases-consistently.md)   
 [Restore and Recovery Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md)   
 [Restore a SQL Server Database to a Point in Time &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md)   
 [Plan and Perform Restore Sequences &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/plan-and-perform-restore-sequences-full-recovery-model.md)  
  
  
