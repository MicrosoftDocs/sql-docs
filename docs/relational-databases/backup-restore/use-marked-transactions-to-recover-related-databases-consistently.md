---
title: "Use Marked Transactions to Recover Related Databases Consistently | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "transaction marks [SQL Server]"
  - "marked transactions [SQL Server]"
  - "database restores [SQL Server], inserting transaction marks for"
  - "recovery [SQL Server], related databases"
  - "restoring databases [SQL Server], related database recovery"
  - "database restores [SQL Server], related databases"
  - "marked transactions [SQL Server], creating"
  - "BEGIN TRAN...WITH MARK statement"
  - "two-phase commit"
ms.assetid: 50a73574-1a69-448e-83dd-9abcc7cb7e1a
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Use Marked Transactions to Recover Related Databases Consistently
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  This topic is relevant only for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases that are using the full or bulk-logged recovery models.  
  
 When you make related updates to two or more databases, *related databases*, you can use transaction marks to recover them to a logically consistent point. However, this recovery loses any transaction that is committed after the mark that was used as the recovery point. Marking transactions is suitable only when you are testing related databases or when you are willing to lose recently committed transactions.  
  
 Routinely marking related transactions in every related database establishes a series of common recovery points in the databases. The transaction marks are recorded in the transaction log and included in log backups. In the event of a disaster, you can restore each of the databases to the same transaction mark to recover them to a consistent point.  
  
> [!NOTE]  
>  Log backups on the different databases can be created independently of each other and do not have to be simultaneous.  
  
 Recovering related databases in the following scenarios requires that you have already marked transactions in every related database:  
  
-   One or more transaction logs are destroyed. You have to restore the set of databases to a consistent state at the time of your last log backup.  
  
-   You have to restore the entire set of databases to a mutually consistent state at some earlier point in time.  
  
> [!IMPORTANT]  
>  You can recover related databases only to a marked transaction, not to a specific point in time.  
  
 For information about how to create marking transactions, see "Creating the Marked Transactions," later in this topic.  
  
## Typical Scenario for Using Marked Transactions  
 A typical scenario for using marked transactions includes the following steps:  
  
1.  Create a full or differential database backup of each of the related databases.  
  
2.  Mark a transaction block in all the databases.  
  
3.  Back up the transaction log for all the databases.  
  
4.  Restore database backups WITH NORECOVERY.  
  
5.  Restore logs WITH STOPATMARK.  
  
## Considerations for Using Marked Transactions  
 Before inserting named marks into the transaction log, consider the following:  
  
-   Because transaction marks consume log space, use them only for transactions that play a significant role in the database recovery strategy.  
  
-   After a marked transaction commits, a row is inserted in the [logmarkhistory](../../relational-databases/system-tables/logmarkhistory-transact-sql.md) table in **msdb**.  
  
-   If a marked transaction spans multiple databases on the same database server or on different servers, the marks must be recorded in the logs of all the affected databases.  
  
## Creating the Marked Transactions  
 To create a marked transaction, use the [BEGIN TRANSACTION](../../t-sql/language-elements/begin-transaction-transact-sql.md) statement and the WITH MARK [*description*] clause. The optional *description* is a textual description of the mark. A mark name for the transaction is required. A mark name can be reused. The transaction log records the mark name, description, database, user, datetime information, and the log sequence number (LSN). The datetime information is used along with the mark name to uniquely identify the mark.  
  
 **To create marked transactions in a set of databases:**  
  
1.  Name the transaction in the BEGIN TRAN statement and use the WITH MARK clause  
  
     You can nest the statement BEGIN TRAN *new_mark_name* WITH MARK within an existing transaction. The value of *new_mark_name* is the mark name for the transaction, even if the transaction possesses a transaction name.  
  
    > [!NOTE]  
    >  If you issue a second nested BEGIN TRAN...WITH MARK, that statement is skipped but causes a warning message.  
  
2.  Run an update against all of the databases in the set.  
  
     The mark for a specific transaction is inserted into transaction logs only on the server instance where the BEGIN TRAN...WITH MARK statement is executed. The transaction mark is placed in the transaction log of every database updated by the marked transaction on that server instance. If the databases reside on different server instances, identical marks must be created on each of the server instances.  
  
### Examples  
 The following example restores the transaction log to the mark in the marked transaction named `ListPriceUpdate`.  
  
```sql  
USE AdventureWorks  
GO  
BEGIN TRANSACTION ListPriceUpdate  
   WITH MARK 'UPDATE Product list prices';  
GO  
  
UPDATE Production.Product  
   SET ListPrice = ListPrice * 1.10  
   WHERE ProductNumber LIKE 'BK-%';  
GO  
  
COMMIT TRANSACTION ListPriceUpdate;  
GO  
  
-- Time passes. Regular database   
-- and log backups are taken.  
-- An error occurs in the database.  
USE master  
GO  
  
RESTORE DATABASE AdventureWorks  
FROM AdventureWorksBackups  
WITH FILE = 3, NORECOVERY;  
GO  
  
RESTORE LOG AdventureWorks  
   FROM AdventureWorksBackups   
   WITH FILE = 4,  
   RECOVERY,   
   STOPATMARK = 'ListPriceUpdate';  
```  
  
## Forcing a Mark to Spread to Other Servers  
 A transaction mark name is not automatically distributed to another server as the transaction spreads there. To force the mark to spread to the other servers, a stored procedure must be written that contains a BEGIN TRAN *name* WITH MARK statement. That stored procedure must then be executed on the remote server under the scope of the transaction in the originating server.  
  
 For example, consider a partitioned database that exists on multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. On each instance is a database named `coyote`. First, in every database, create a stored procedure, for example, `sp_SetMark`.  
  
```sql  
CREATE PROCEDURE sp_SetMark  
@name nvarchar (128)  
AS  
BEGIN TRANSACTION @name WITH MARK  
UPDATE coyote.dbo.Marks SET one = 1  
COMMIT TRANSACTION;  
GO  
```  
  
 Next, create stored procedure `sp_MarkAll` containing a transaction that places a mark in every database. `sp_MarkAll` can be run from any of the instances.  
  
```sql  
CREATE PROCEDURE sp_MarkAll  
@name nvarchar (128)  
AS  
BEGIN TRANSACTION  
EXEC instance0.coyote.dbo.sp_SetMark @name  
EXEC instance1.coyote.dbo.sp_SetMark @name  
EXEC instance2.coyote.dbo.sp_SetMark @name  
COMMIT TRANSACTION;  
GO  
```  
  
### Two-Phase Commit  
 Committing a distributed transaction occurs in two phases: prepare and commit. When a marked transaction is committed, the commit log record for each database in the marked transaction is placed in the log at a point where there are no in-doubt transactions in any of the logs. At this point, it is guaranteed that there are no transactions that appear as committed in one log, but not committed in another log.  
  
 The following steps accomplish this during the commit of a marked transaction:  
  
1.  Prepare phase of a marking transaction stalls all new prepares and commits.  
  
2.  Only commits of already prepared transactions are allowed to continue.  
  
3.  Marking transaction then waits for all prepared transactions to drain (with time-out).  
  
4.  Marked transaction is prepared and committed.  
  
5.  The stall of new prepares and commits is removed.  
  
 The stalls generated by marked transactions that span multiple databases can reduce the transaction processing performance of the server.  
  
 We recommend that you do not run concurrent marked transactions. It is rare but possible for the commit of a distributed marked transaction to deadlock with other distributed marked transactions that are committing at the same time. When this happens, the marking transaction is chosen as the deadlock victim and is rolled back. When this error occurs, the application can retry the marked transaction. When multiple marked transactions try to commit concurrently, there is a higher probability of deadlock.  
  
## Recovering to a Marked Transaction  
 For information about how to recover a database that contains marked transactions to or just before a particular mark, see [Recovery of Related  Databases That Contain Marked Transaction](../../relational-databases/backup-restore/recovery-of-related-databases-that-contain-marked-transaction.md).  
  
## See Also  
 [BEGIN DISTRIBUTED TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-distributed-transaction-transact-sql.md)   
 [Back Up and Restore of System Databases &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md)   
 [BEGIN TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-transaction-transact-sql.md)   
 [Apply Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/apply-transaction-log-backups-sql-server.md)   
 [Full Database Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/full-database-backups-sql-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Recovery of Related  Databases That Contain Marked Transaction](../../relational-databases/backup-restore/recovery-of-related-databases-that-contain-marked-transaction.md)  
  
  
