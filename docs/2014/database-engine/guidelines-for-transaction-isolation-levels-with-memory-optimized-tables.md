---
title: "Guidelines for Transaction Isolation Levels with Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: e365e9ca-c34b-44ae-840c-10e599fa614f
author: stevestein
ms.author: sstein
manager: craigg
---
# Guidelines for Transaction Isolation Levels with Memory-Optimized Tables
  In many scenarios, you must specify the transaction isolation level. Transaction isolation for memory-optimized tables differs from disk-based tables.  
  
 Requirements for specifying transaction isolation level:  
  
-   TRANSACTION ISOLATION LEVEL is a required option for the ATOMIC block comprising the content of a natively compiled stored procedure.  
  
-   Because of restrictions on isolation level use in cross-container transactions, uses of memory-optimized tables in interpreted [!INCLUDE[tsql](../includes/tsql-md.md)] must often be accompanied by a table hint specifying the isolation level used to access the table. For more information about isolation level hints and cross-container transactions, see [Transaction Isolation Levels](../../2014/database-engine/transaction-isolation-levels.md).  
  
-   The desired transaction isolation level must be explicitly declared. It is not possible to use locking hints (such as XLOCK) to guarantee the isolation of certain rows or tables in the transaction.  
  
-   The application accessing the database should implement retry logic to deal with errors resulting from transaction-dooming conflicts, validation failures, and commit-dependency failures. Note that commit dependency failures can occur even with read-only transactions.  
  
-   Long-running transactions should be avoided with memory-optimized tables. Such transactions increase the likelihood of conflicts and subsequent transaction terminations. A long-running transaction also defers garbage collection. The longer a transaction runs, the longer In-Memory OLTP keeps recently deleted row versions, which can decrease lookup performance for new transactions.  
  
 Disk-based tables typically rely on locking and blocking for transaction isolation. Memory-optimized tables rely on multi-versioning and conflict detection to guarantee isolation. For details, see the section on Conflict Detection, Validation, and Commit Dependency Checks in [Transactions in Memory-Optimized Tables](../relational-databases/in-memory-oltp/memory-optimized-tables.md).  
  
 Disk-based tables do allow multi-versioning with the isolation levels SNAPSHOT and READ_COMMITTED_SNAPSHOT. For memory-optimized tables all isolation levels are multi-version based, including REPEATABLE READ and SERIALIZABLE.  
  
## Types of Transactions  
 Every query in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] runs in the context of a transaction.  
  
 There are three types of transactions in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]:  
  
-   Autocommit transactions. If there is no active transaction context and implicit transactions are not set to ON in the session, each query has its own transaction context. The transaction starts when the statement starts execution, and completes when the statement finishes.  
  
-   Explicit transactions. The user starts the transaction through an explicit BEGIN TRAN or BEGIN ATOMIC. The transaction is completed following the corresponding COMMIT and ROLLBACK or END (in the case of an atomic block).  
  
-   Implicit transactions. When the option IMPLICIT_TRANSACTIONS is set to ON, a transaction is started implicitly whenever the user executes a statement and there is no active transaction context. The transaction is completed through an explicit COMMIT and ROLLBACK.  
  
## Baseline READ COMMITTED Isolation  
 READ COMMITTED is the default isolation level in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 The isolation level READ COMMITTED guarantees that transactions do not see any uncommitted data from changes outside the current transaction. In other words, the transaction only reads data which has either been committed to the database, or has been changed by the current transaction.  
  
 All isolation levels supported for memory-optimized tables provide the read committed guarantee. Therefore, if the transaction does not require stronger guarantees, you can use any of the isolation levels supported for memory-optimized tables. SNAPSHOT uses the fewest system resources, compared to other isolation levels.  
  
 The guarantee provided by the SNAPSHOT isolation level (the lowest level of isolation supported for memory-optimized tables) includes the guarantees of READ COMMITTED. Every statement in the transaction reads the same, consistent version of the database. Not only are all the rows read by the transaction committed to the database, also all the read operations see the set of changes made by the same set of transactions.  
  
 **Guideline**: If only the READ COMMITTED isolation guarantee is required, use SNAPSHOT isolation with natively compiled stored procedures and for accessing memory-optimized tables through interpreted [!INCLUDE[tsql](../includes/tsql-md.md)].  
  
 For autocommit transactions, the isolation level READ COMMITTED is implicitly mapped to SNAPSHOT for memory-optimized tables. Therefore, if the TRANSACTION ISOLATION LEVEL session setting is set to READ COMMITTED, it is not necessary to specify the isolation level through a table hint when accessing memory-optimized tables.  
  
 The following autocommit transaction example shows a join between a memory-optimized table Customers and a regular table [Order History], as part of an ad hoc batch:  
  
```sql  
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;  
GO  
SELECT *   
FROM dbo.Customers AS c   
LEFT JOIN dbo.[Order History] AS oh   
    ON c.customer_id = oh.customer_id;  
```  
  
 The following explicit or implicit transactions example shows the same join, but this time in an explicit user transaction. The memory-optimized table Customers is accessed under snapshot isolation, as indicated through the table hint WITH (SNAPSHOT), and the regular table [Order History] is accessed under read committed isolation:  
  
```sql  
SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
GO  
BEGIN TRAN  
SELECT * FROM dbo.Customers c with (SNAPSHOT)   
LEFT JOIN dbo.[Order History] oh   
    ON c.customer_id=oh.customer_id  
...  
COMMIT  
```  
  
### Operational Differences  
 Besides the read committed guarantee, there are also two key implementation details that applications using disk-based tables may rely on. Be aware of the following when converting a disk-based table that is accessed using READ COMMITTED isolation to a memory-optimized table that is accessed using SNAPSHOT isolation:  
  
-   The implementation of the READ COMMITTED isolation level for disk-based tables (assuming READ_COMMITTED_SNAPSHOT is OFF) uses locks to prevent conflicts between readers and writers. When a writer starts updating a row, it takes a lock and does not release the lock until the transaction is committed. Any read operations are blocked and will wait for the write transaction to commit.  
  
     Some applications may assume readers always wait for writers to commit, particularly if there is any synchronization between the two transactions in the application tier.  
  
     **Guideline:** Applications cannot rely on blocking behavior. If an application needs synchronization between concurrent transactions, such logic can be implemented in the application tier or in the database tier, through [sp_getapplock &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-getapplock-transact-sql).  
  
-   In transactions that use READ COMMITTED isolation, each statement sees the most recent version of the rows in the database. Therefore, subsequent statements see changes in the state of the database.  
  
     Polling a table using a WHILE loop until a new row has been found is an example of an application pattern that uses this assumption. With each iteration of the loop, the query will see the latest updates in the database.  
  
     **Guideline:** If an application needs to poll a memory-optimized table to obtain the most recent rows written to the table, move the polling loop outside the scope of the transaction.  
  
     The following is an example application pattern that uses this assumption. Polling a table using a WHILE loop until a new row is found. In each loop iteration, the query will access the latest updates in the database.  
  
 The following example script polls a table t1 until it has a row. It then removes a single row from the table for further processing.  
  
 Notice that the polling logic needs to be outside the scope of the transaction, as it is using snapshot isolation to access table t1. Using polling logic inside the scope of a transaction would create a long-running transaction, which is a bad practice.  
  
```sql  
-- poll table  
WHILE NOT EXISTS (SELECT 1 FROM dbo.t1)  
BEGIN   
  -- if empty, wait and poll again  
  WAITFOR DELAY '00:00:01'  
END  
  
BEGIN TRANSACTION  
  DECLARE @id int  
  SELECT TOP 1 @id=id FROM dbo.t1 WITH (SNAPSHOT)  
  DELETE FROM dbo.t1 WITH (SNAPSHOT) WHERE id=@id  
  
  -- insert processing based on @id  
COMMIT  
```  
  
## Locking Table Hints  
 Locking hints ([Table Hints &#40;Transact-SQL&#41;](/sql/t-sql/queries/hints-transact-sql-table)) such as HOLDLOCK and XLOCK can be used with disk-based tables to have [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] take more locks than are required for the specified isolation level.  
  
 Memory-optimized tables do not use locks. Higher isolation levels such as REPEATABLE READ and SERIALIZABLE can be used to declare the desired guarantees.  
  
 Locking hints are not supported. Instead, declare the required guarantees through the transaction isolation levels. (NOLOCK is supported because [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] does not take locks on memory-optimized tables. Note that, in contrast to disk-based tables, NOLOCK does not imply READ UNCOMMITTED behavior for memory-optimized tables.)  
  
## See Also  
 [Understanding Transactions on Memory-Optimized Tables](../../2014/database-engine/understanding-transactions-on-memory-optimized-tables.md)   
 [Guidelines for Retry Logic for Transactions on Memory-Optimized Tables](../../2014/database-engine/guidelines-for-retry-logic-for-transactions-on-memory-optimized-tables.md)   
 [Transaction Isolation Levels](../../2014/database-engine/transaction-isolation-levels.md)  
  
  
