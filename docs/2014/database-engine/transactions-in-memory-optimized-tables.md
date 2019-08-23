---
title: "Transactions in Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 2cd07d26-a1f1-4034-8d6f-f196eed1b763
author: stevestein
ms.author: sstein
manager: craigg
---
# Transactions in Memory-Optimized Tables
  Row versioning on disk-based tables (using SNAPSHOT isolation or READ_COMMITTED_SNAPSHOT) provides a form of optimistic concurrency control. Readers and writers do not block each other. With memory-optimized tables, writers do not block writers. With row versioning on disk-based tables, one transaction locks the row and concurrent transactions attempting to update the row are blocked. There is no locking with memory-optimized tables. Instead, if two transactions attempt to update the same row, a write/write conflict (error 41302) will occur.  
  
 Unlike disk-based tables, memory-optimized tables allow optimistic concurrency control with the higher isolation levels, REPEATABLE READ and SERIALIZABLE. Locks are not taken to enforce the isolation levels. Instead, at the end of the transaction validation ensures the repeatable read or serializability assumptions. If the assumptions are violated, the transaction is terminated. For more information, see [Transaction Isolation Levels](../../2014/database-engine/transaction-isolation-levels.md).  
  
 The important transaction semantics for memory-optimized tables are:  
  
-   Multi-versioning  
  
-   Snapshot-based transaction isolation  
  
-   Optimistic  
  
-   Conflict detection  
  
 Each of these semantics is explained in the following sections.  
  
## Multi-Versioning in Memory-Optimized Tables  
 Rows in memory-optimized tables can have different versions. Concurrent transactions access potentially different versions of the same row.  
  
 Memory-optimized table data is version-based. For any row there may be different row versions that are valid at different points in time. Disk-based tables maintain different row versions when READ_COMMITTED_SNAPSHOT or ALLOW_SNAPSHOT_ISOLATION is ON. Memory-optimized tables maintain different row versions, even if READ_COMMITTED_SNAPSHOT and ALLOW_SNAPSHOT_ISOLATION are OFF. The row versions of memory-optimized tables are not maintained in tempdb. Instead, the row versions are maintained in-line, as part of the memory-optimized data structures storing the rows in memory.  
  
## Snapshot-Based Transaction Isolation for Memory-Optimized Tables  
 All operations in a single transaction use the same transactionally-consistent snapshot of the memory-optimized tables. All transaction isolation for memory-optimized tables is snapshot-based. For example, a transaction using the serializable isolation level to access memory-optimized tables will perform all operations on the same transactionally consistent snapshot.  
  
 Transactions that access memory-optimized tables use this row versioning to obtain a transactionally consistent snapshot of the rows in the tables. The data read by any statement in the transaction will be the transactionally consistent version of the data that existed at the time the transaction started. Therefore, any modifications made by concurrently running transactions are not visible to statements in the current transaction.  
  
## Optimistic Concurrency Control for Memory-Optimized Tables  
 Conflicts and failures are rare and transactions on memory-optimized tables assume there are no conflicts with concurrent transactions and operations succeed. Transactions do not take locks or latches on memory-optimized table to guarantee transaction isolation. Writers do not block readers. Writers do not block writers. Instead, transactions proceed under the (optimistic) assumption that there will be no conflicts with other transactions. Not using locks and latches and not waiting for other transactions to finish processing the same rows improves performance.  
  
 In addition, if a transaction (TxA) reads rows that have been inserted or modified by another transaction (TxB) that is in the process of committing, it will optimistically assume the other transaction will commit rather than wait for the commit to occur. In this case, transaction TxA will take a commit dependency on transaction TxB.  
  
## Conflict Detection, Validation, and Commit Dependency Checks  
 [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] detects conflicts between concurrent transactions, as well as isolation level violations, and will doom one of the conflicting transactions. This transaction will need to be retried. (For more information, see [Guidelines for Retry Logic for Transactions on Memory-Optimized Tables](../relational-databases/in-memory-oltp/memory-optimized-tables.md).)  
  
 The system optimistically assumes there are no conflicts and no violations of transaction isolation. If any conflicts occur that may cause inconsistencies in the database or that may violate transaction isolation, these conflicts are detected, and the transaction is terminated.  
  
 If a conflict is detected, the transaction is terminated and the client needs to retry.  
  
 The following table summarizes the error conditions for transactions that accesses memory-optimized tables.  
  
### Error conditions for transactions accessing memory-optimized tables.  
  
|Error|Scenario|  
|-----------|--------------|  
|Write conflict. Attempting to update a record that has been updated since the transaction started.|UPDATE or DELETE a row that has been updated or deleted by a concurrent transaction.|  
|Repeatable read validation failure.|A row that was read by the transaction has changed (updated or deleted) since the transaction started. Repeatable read validation is typically occurs when using REPEATABLE READ and SERIALIZABLE transaction isolation levels.|  
|Serializable validation failure.|A new (phantom) row has been inserted in one of the scan ranges in the transaction, since the transaction started. The row would have been visible to the transaction if the row had been committed to the database before the transaction started. SERIALIZABLE validation typically occurs when using SERIALIZABLE isolation and validating PRIMARY KEY constraints.|  
|Commit dependency failure.|The transaction took a dependency on another transaction that failed to commit, either due to one of the failures in this table, an out-of-memory condition, or due to failure to commit to the transaction log. This failure can occur with both read/write and read-only transactions.|  
  
### Transaction Lifetime  
 The failures mentioned in the previous table can occur at different points during a transaction. The following figure illustrates the phases of a transaction that accesses memory-optimized tables.  
  
 ![Lifetime of a transaction.](../../2014/database-engine/media/hekaton-transactions.gif "Lifetime of a transaction.")  
Lifetime of a transaction that accesses memory-optimized tables.  
  
#### Regular Processing  
 During this phase, the user-issued [!INCLUDE[tsql](../includes/tsql-md.md)] statements are executed. Rows are read from the tables, and new row versions are written to the database. The transaction is isolated from all other concurrent transactions. The transaction uses the snapshot of the memory-optimized tables that exists at the start of the transaction.  
  
 Writes to the tables in this phase of the transaction are not yet visible to other transactions, with one exception: row updates and deletes are visible to update and delete operations in other transactions, in order to detect write conflicts.  
  
 If an update or delete operation sees that a row has been updated or deleted since the logical start of the transaction, the operation will fail with error 41302. The message for error 41302 is "The current transaction attempted to update a record in table X that has been updated since this transaction started. The transaction was aborted."  
  
 This error dooms the transaction (even if XACT_ABORT is OFF), meaning that the transaction will be rolled back when the user session ends. Doomed transactions cannot be committed and only support read operations that do not write to the log and do not access memory-optimized tables.  
  
#####  <a name="cd"></a> Commit Dependencies  
 During regular processing, a transaction can read rows written by other transactions that are in the validation or commit phase, but have not yet committed. The rows are visible because the logical end time of the transactions has been assigned at the start of the validation phase.  
  
 If a transaction reads such uncommitted rows, it will take a commit dependency on that transaction. This has two main implications:  
  
-   A transaction cannot commit until the transactions it depends on have committed. In other words, it cannot enter the commit phase, until all dependencies have cleared.  
  
-   In addition, result sets are not returned to the client until all dependencies have cleared. This prevents the client from observing uncommitted data.  
  
 If any of the dependent transactions fails to commit, there is a commit dependency failure. This means the transaction will fail to commit with error 41301 ("A previous transaction that the current transaction took a dependency on has aborted, and the current transaction can no longer commit.").  
  
#### Validation Phase  
 During the validation phase, the system validates that the assumptions necessary for the requested transaction isolation level were true between the logical start and logical end of the transaction.  
  
 At the start of the validation phase, the transaction is assigned a logical end time. The row versions written in the database become visible to other transactions at the logical end time. For more information, see [Commit Dependencies](#cd).  
  
##### Repeatable Read Validation  
 If the isolation level of the transaction is REPEATABLE READ or SERIALIZABLE, or if tables are accessed under REPEATABLE READ or SERIALIZABLE isolation (for more information, see the section on Isolation of Individual Operations in [Transaction Isolation Levels](../../2014/database-engine/transaction-isolation-levels.md)), the system validates that the reads are repeatable. This means it validates that the versions of the rows read by the transaction are still valid row versions at the logical end time of the transaction.  
  
 If any of the rows have been updated or changed, the transaction fails to commit with error 41305 ("The current transaction failed to commit due to a repeatable read validation failure.").  
  
 This error can also occur if a table is dropped after an insert, update, or delete operation and before the transaction commits. This applies only to insert, update, or delete operations in natively compiled stored procedures. Such write operations performed through interpreted [!INCLUDE[tsql](../includes/tsql-md.md)] cause the DROP TABLE statement to block and wait until the transaction commits.  
  
##### Serializable Validation  
 Serializable validation is performed in two cases:  
  
-   If the isolation level of the transaction is SERIALIZABLE or tables are accessed under SERIALIZABLE isolation.  
  
-   If rows are inserted in a unique index, such as the index created for a PRIMARY KEY constraint. The system validates that no rows with the same key have been concurrently inserted.  
  
 The system validates that no phantom rows have been written to the database. The read operations performed by the transaction are evaluated to determine that no new rows were inserted in the scan ranges of these read operations.  
  
 Insertion of a key in a unique index includes an implicit read operation, to determine that the key is not a duplicate. Serializable validation for unique indexes ensures these indexes cannot have duplicates in case two transactions concurrently insert the same key.  
  
 If phantom rows are detected, the transaction fails to commit with error 41325 ("The current transaction failed to commit due to a serializable validation failure.").  
  
#### Commit Processing  
 If validation succeeds and all transaction dependencies clear, the transaction enters the commit processing phase. During this phase the changes to durable tables are written to the log, and the log is written to disk, to ensure durability. Once the log record for the transaction has been written to disk, control is returned to the client.  
  
 All commit dependencies on this transaction are cleared, and all transactions that had been waiting for this transaction to commit can proceed.  
  
## Limitations  
  
-   Cross-database transactions are not supported with memory-optimized tables. Every transaction that accesses memory-optimized tables cannot access more than one database, with the exception of read-write access to tempdb and read-only access to the system database master.  
  
-   Distributed transactions are not supported with memory-optimized tables. Distributed transactions started with BEGIN DISTRIBUTED TRANSACTION cannot access memory-optimized tables.  
  
-   Memory-optimized tables do not support locking. Explicit locks through locking hints (such as TABLOCK, XLOCK, ROWLOCK) are not supported with memory-optimized tables.  
  
## See Also  
 [Understanding Transactions on Memory-Optimized Tables](../../2014/database-engine/understanding-transactions-on-memory-optimized-tables.md)  
  
  
