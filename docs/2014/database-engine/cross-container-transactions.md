---
title: "Cross-Container Transactions | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 5d84b51a-ec17-4c5c-b80e-9e994fc8ae80
author: stevestein
ms.author: sstein
manager: craigg
---
# Cross-Container Transactions
  Cross-container transactions are either implicit or explicit user transactions that include calls to natively-compiled stored procedures or operations on memory-optimized tables.  
  
 In [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], calls to stored procedures do not initiate a transaction. Executions of natively compiled procedures in autocommit mode (not in the context of a user transaction) are not considered cross-container transactions.  
  
 Any interpreted query that references memory-optimized tables is considered a part of a cross-container transaction, whether executed from an explicit or implicit transaction or in auto-commit mode.  
  
##  <a name="isolation"></a> Isolation of Individual Operations  
 Each [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] transaction has an isolation level. The default isolation level is Read Committed. To use a different isolation level, you can set the isolation level using [SET TRANSACTION ISOLATION LEVEL &#40;Transact-SQL&#41;](/sql/t-sql/statements/set-transaction-isolation-level-transact-sql).  
  
 It is often necessary to perform operations on memory-optimized tables at a different isolation level than operations on disk-based tables. In a transaction, it is possible to set a different isolation level for a collection of statements or for an individual read operation.  
  
### Specifying the Isolation Level of Individual Operations  
 To set a different isolation level for a set of statements in a transaction, you can use `SET TRANSACTION ISOLATION LEVEL`. The following example of a transaction uses the serializable isolation level as default. The insert and select operations on t3, t2, and t1 are executed under repeatable read isolation.  
  
```tsql  
set transaction isolation level serializable  
go  
  
begin transaction  
 ......  
  set transaction isolation level repeatable read  
  
  insert t3 select * from t1 join t2 on t1.id=t2.id  
  
  set transaction isolation level serializable  
 ......  
commit  
```  
  
 To set an isolation level for individual read operations that is different from the transaction default, you can use a table hint (for example, serializable). Every select corresponds to a read operation and every update and every delete corresponds to a read, because the row always needs to be read before it can be updated or deleted. Insert operations do not have an isolation level, because writes are always isolated in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. In the following example, the default isolation level for the transaction is read committed, but table t1 is accessed under serializable and t2 under snapshot isolation.  
  
```tsql  
set transaction isolation level read committed  
go  
  
begin transaction  
 ......  
  
  insert t3 select * from t1 (serializable) join t2 (snapshot) on t1.id=t2.id  
  
  ......  
commit  
```  
  
### Isolation Semantics for Individual Operations  
 A serializable transaction T is executed in complete isolation. It is as if every other transaction has either committed before T started, or is started after T committed. It becomes more complex when different operations in a transaction have different isolation levels.  
  
 The general semantics of the transaction isolation levels in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], along with the implications on locking, is explained in [SET TRANSACTION ISOLATION LEVEL &#40;Transact-SQL&#41;](/sql/t-sql/statements/set-transaction-isolation-level-transact-sql).  
  
 For cross-container transactions where different operations may have different isolation levels, you need to understand the semantics of isolation of individual read operations. Write operations are always isolated. Writes in different transactions cannot impact each other.  
  
 A data read operation returns a number of rows that satisfy a filter condition.  
  
 Reads are performed as part of a transaction T. Isolation levels for read operations can be understood in terms of,  
  
 Commit Status  
 Commit status refers to whether the data read is guaranteed to be committed.  
  
 (Transactional) Consistency  
 Transactional consistency for a set of reads refers to whether the row versions read are all guaranteed to include updates from precisely the same set of transactions.  
  
 Stability guarantees the system gives to transaction T about the data read.  
 Stability refers to whether the transaction's reads are repeatable. That is, if the reads were repeated would they return the same rows and row versions?  
  
 Certain guarantees refer to the logical end time of the transaction. In general, the logical end time is the time the transaction is committed to the database. If memory-optimized tables are accessed by the transaction, the logical end time is technically the beginning of the validation phase. (For more information, see the transaction lifetime discussion in [Transactions in Memory-Optimized Tables](../relational-databases/in-memory-oltp/memory-optimized-tables.md).  
  
 Regardless of isolation level, a transaction (T) always sees its own updates:  
  
 READ UNCOMMITTED  
 The data read may neither be committed, consistent, or stable. However, it will include earlier write operations done by T.  
  
 READ COMMITTED  
 The data read will be committed.  
  
 SNAPSHOT  
 All read operations performed by T under snapshot isolation have the same logical read time, which is the beginning of the transaction. The data read is guaranteed to be committed and consistent as of the logical read time. Repeating a read as of the original read time is guaranteed to return the same result.  
  
 REPEATABLE READ  
 The data read is guaranteed to be committed and stable up to the logical end time of the transaction.  
  
 SERIALIZABLE  
 All guarantees of REPEATABLE READ plus phantom avoidance and transactional consistency with respect to all serializable read operations performed by T. Phantom avoidance means that the scan operation can only return additional rows that were written by T, but no rows that were written by other transactions.  
  
 Consider the following transaction,  
  
```tsql  
set transaction isolation level read committed  
go  
  
begin transaction  
  -- remove all rows from t3; the related read operation is performed under read committed   
  -- isolation, as this is the default for the transaction  
  delete from t3  
  
  -- copy the contents from t1 to t3; the read on t1 is performed under the serializable   
  -- isolation level  
  insert t3 select * from t1 (serializable)  
  
  -- compare t3 and t1; note: the result set may not be empty, as rows may have been added   
  -- by other transaction before this select, due to the read committed isolation level  
  select * from t3 except t1  
  
  -- compare t1 and t3; note: the result set is empty, as no rows have been added to t1   
  -- since its contents were copied to t1, due to the serializable isolation level  
  select * from t1 except t3  
commit  
```  
  
 This transaction deletes all rows from t3 under read committed isolation, copies all rows from t1 to t3 under serializable isolation, and then compares t1 and t3. Some rows [not in t1] may have been added to t3 since the table was emptied. No rows were added to t1 as the copy was serializable.  
  
 Although the read from t1 at the end of the transaction is syntactically read committed, it is effectively serializable, because the same read was performed earlier in the transaction under serializable isolation: serializability guarantees that if the read is performed at any later point in the transaction, the same rows are returned.  
  
## Cross-Container Transactions and Isolation Levels  
 A cross-container transaction can be seen as having two sides: a disk-based side (for operations on disk-based tables) and a memory-optimized side (for operations on memory-optimized tables). These two sides may have different isolation levels. In fact, individual read operations on each side may have different isolation levels.  
  
 The disk-based side of a given transaction T reaches a certain isolation level X if one of the following conditions is met:  
  
-   It starts in X. That is, the session default was X, either because you executed `SET TRANSACTION ISOLATION LEVEL`, or it is the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] default.  
  
-   During the transaction, the default isolation level is changed to X using `SET TRANSACTION ISOLATION LEVEL`.  
  
-   A read operation on a disk-based table is executed under isolation level X, using the syntax `WITH (X)`.  
  
 The memory-optimized side of T reaches an isolation level Y if during execution of T, any read operation on a memory-optimized table or any natively compiled stored procedure is executed under isolation level Y.  
  
 Consider the following transaction as an example. Here, t1 and t2 are disk-based tables and t3 and t4 are memory-optimized tables.  
  
 The disk-based side of the transaction reaches the isolation level read committed, because it starts in that level. The disk-based side also reaches repeatable read, because the first read operation is executed under that isolation level. The delete at the end of the transaction is executed under read committed isolation level, and so does not introduce a new isolation level.  
  
 The memory-optimized side of the transaction can reach one of two levels: if condition1 is true, it reaches serializable, while if it is false, the memory-optimized side reaches only snapshot isolation.  
  
```tsql  
set transaction isolation level read committed  
go  
  
begin transaction  
  select * from t1 (repeatableread)  
  
  if condition1 begin  
    insert t3 select * from t4 (serializable)  
  end  
  else begin  
    insert t3 select * from t4 (snapshot)  
  end  
  
  delete from t1  
commit  
```  
  
### Supported Isolation Levels for Cross-Container Transactions  
 There are limitations on the isolation levels used with operations on memory-optimized tables in cross-container transactions.  
  
 Memory-optimized tables support the isolation levels SNAPSHOT, REPEATABLE READ, and SERIALIZABLE. For autocommit transactions, memory-optimized tables support the isolation level READ COMMITTED.  
  
 The following scenarios are supported:  
  
-   READ UNCOMMITTED, READ COMMITTED, and READ_COMMITTED_SNAPSHOT cross-container transactions can access memory-optimized tables under SNAPSHOT, REPEATABLE READ, and SERIALIZABLE isolation. The READ COMMITTED guarantee holds for the transaction; all rows read by the transaction have been committed to the database.  
  
-   REPEATABLE READ and SERIALIZABLE transactions can access memory-optimized tables under SNAPSHOT isolation.  
  
## Read-only Cross-Container Transactions  
 Most read-only transactions in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] are rolled back at commit time. Because there are no changes to commit to the database, the system simply frees the resources used by the transaction. For read-only disk-based transactions, all locks taken by the transaction are released at this time. For read-only memory-optimized transactions that span a single natively compiled procedure execution, no validation is performed.  
  
 Cross-container, read-only transactions in autocommit mode are simply rolled back at the end of the transaction. No validation is performed.  
  
 Explicit or implicit cross-container, read-only transactions perform validation at commit time if the transaction accesses memory-optimized tables under REPEATABLE READ or SERIALIZABLE isolation. For details about validation see the section on Conflict Detection, Validation, and Commit Dependency Checks in [Transactions in Memory-Optimized Tables](../relational-databases/in-memory-oltp/memory-optimized-tables.md).  
  
## See Also  
 [Understanding Transactions on Memory-Optimized Tables](../../2014/database-engine/understanding-transactions-on-memory-optimized-tables.md)   
 [Guidelines for Transaction Isolation Levels with Memory-Optimized Tables](../../2014/database-engine/guidelines-for-transaction-isolation-levels-with-memory-optimized-tables.md)   
 [Guidelines for Retry Logic for Transactions on Memory-Optimized Tables](../../2014/database-engine/guidelines-for-retry-logic-for-transactions-on-memory-optimized-tables.md)  
  
  
