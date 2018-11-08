---
title: "Transaction Isolation Levels | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 8a6a82bf-273c-40ab-a101-46bd3615db8a
author: stevestein
ms.author: sstein
manager: craigg
---
# Transaction Isolation Levels
  The following isolation levels are supported for transactions that access memory-optimized tables.  
  
-   SNAPSHOT  
  
-   REPEATABLE READ  
  
-   SERIALIZABLE  
  
-   READ COMMITTED  
  
 The transaction isolation level can be specified as part of the atomic block of a natively compiled stored procedure. For more information, see [CREATE PROCEDURE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-procedure-transact-sql). When accessing memory-optimized tables from interpreted [!INCLUDE[tsql](../includes/tsql-md.md)], the isolation level can be specified using table-level hints.  
  
 You must specify the transaction isolation level when you define a natively compiled stored procedure. You must specify the isolation level in table hints when accessing memory-optimized tables from user transactions in interpreted [!INCLUDE[tsql](../includes/tsql-md.md)]. For more information, see [Guidelines for Transaction Isolation Levels with Memory-Optimized Tables](../relational-databases/in-memory-oltp/memory-optimized-tables.md).  
  
 The isolation level READ COMMITTED is supported for memory-optimized tables with autocommit transactions. READ COMMITTED is not valid in user transactions or in an atomic block. READ COMMITTED is not supported with explicit or implicit user transactions. Isolation level READ_COMMITTED_SNAPSHOT is supported for memory-optimized tables with autocommit transactions and only if the query does not access any disk-based tables. In addition, transactions that are started using interpreted [!INCLUDE[tsql](../includes/tsql-md.md)] with SNAPSHOT isolation cannot access memory-optimized tables. Transactions that are use interpreted [!INCLUDE[tsql](../includes/tsql-md.md)] with either REPEATABLE READ or SERIALIZABLE isolation must access memory-optimized tables using SNAPSHOT isolation. For more information about this scenario, see [Cross-Container Transactions](cross-container-transactions.md).  
  
 READ COMMITTED is the default isolation level in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. When the isolation level of the session is READ COMMITED (or lower), you can do one of the following:  
  
-   Explicitly use a higher isolation level hint for accessing the memory-optimized table (for example, WITH (SNAPSHOT)).  
  
-   Specify the `MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT` set option, which will set the isolation level for memory-optimized tables to SNAPSHOT (as if you included WITH(SNAPSHOT) hints to every memory-optimized table). For more information about `MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT`, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-set-options).  
  
 Alternatively, if the isolation level of the session is READ COMMITTED, you can use autocommit transactions.  
  
 SNAPSHOT transactions started in interpreted [!INCLUDE[tsql](../includes/tsql-md.md)] cannot access memory-optimized tables.  
  
 The transaction isolation levels supported for memory-optimized tables provide the same logical guarantees as disk-based tables. The mechanism used for providing isolation level guarantees is different.  
  
 For disk-based tables, most isolation level guarantees are implemented using locking, which prevent conflicts through blocking. For memory-optimized tables, the guarantees are enforced using a conflict detection mechanism, which avoids the need to take locks. The exception is SNAPSHOT isolation on disk-based tables. This is implemented similarly to SNAPSHOT isolation on memory-optimized tables using a conflict detection mechanism.  
  
 SNAPSHOT  
 This isolation level specifies that data read by any statement in a transaction will be the transactionally consistent version of the data that existed at the start of the transaction. The transaction can only recognize data modifications that were committed before the start of the transaction. Data modifications made by other transactions after the start of the current transaction are not visible to statements executing in the current transaction. The statements in a transaction get a snapshot of the committed data as it existed at the start of the transaction.  
  
 Write operations (updates, inserts and deletes) are always fully isolated from other transactions. Therefore, the write operations in a SNAPSHOT transaction can conflict with write operations by other transactions. When the current transaction attempts to update or delete a row that has been updated or deleted by another transaction that committed after the current transaction started, the transaction terminates with the following error message.  
  
 Error 41302. The current transaction attempted to update a record in table X that has been updated since this transaction started. The transaction was aborted.  
  
 When the current transaction attempts to insert a row with the same primary key value as a row that was inserted by another transaction that committed before the current transaction, there will be a failure to commit with the following error message.  
  
 Error 41325. The current transaction failed to commit due to a serializable validation failure.  
  
 If a transaction writes to a table that is dropped before the transaction commits, the transaction terminates with the following error message:  
  
 Error 41305. The current transaction failed to commit due to a repeatable read validation failure.  
  
 REPEATABLE READ  
 This isolation level includes the guarantees given by SNAPSHOT isolation level. In addition, REPEATABLE READ guarantees that for any row that is read by the transaction, at the time the transaction commits the row has not been changed by any other transaction. Every read operation in the transaction is repeatable up to the end of the transaction.  
  
 If the current transaction has read any row that has then been updated by another transaction that has committed before the current transaction, the commit fails with the following error message.  
  
 Error 41305. The current transaction failed to commit due to a repeatable read validation failure.  
  
 SERIALIZABLE  
 This isolation level includes the guarantees given by REPEATABLE READ. No phantom rows have appeared between the snapshot and the end of the transaction. Phantom rows match the filter condition of a select, update, or delete.  
  
 The transaction is executed as if there are no concurrent transactions. All actions virtually occur at a single serialization point (commit time).  
  
 If any of these guarantees is violated, the transaction fails to commit with the following error message:  
  
 Error 41325. The current transaction failed to commit due to a serializable validation failure.  
  
## See Also  
 [Understanding Transactions on Memory-Optimized Tables](../../2014/database-engine/understanding-transactions-on-memory-optimized-tables.md)   
 [Guidelines for Transaction Isolation Levels with Memory-Optimized Tables](../relational-databases/in-memory-oltp/memory-optimized-tables.md)   
 [Guidelines for Retry Logic for Transactions on Memory-Optimized Tables](../../2014/database-engine/guidelines-for-retry-logic-for-transactions-on-memory-optimized-tables.md)  
  
  
