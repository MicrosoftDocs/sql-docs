---
title: "Transactions with Memory-Optimized Tables"
description: Learn about transactions for memory-optimized tables and natively compiled stored procedures and how they differ from transactions for disk-based tables.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "01/16/2018"
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.assetid: ba6f1a15-8b69-4ca6-9f44-f5e3f2962bc5
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Transactions with Memory-Optimized Tables
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes all the aspects of transactions that are specific to memory-optimized tables and natively compiled stored procedures.  
  
The transaction isolation levels in SQL Server apply differently to memory-optimized tables versus disk-based tables, and the underlying mechanisms are different. An understanding of the differences helps the programmer design a high throughput system. The goal of transaction integrity is shared in all cases.  

For error conditions specific to transactions on memory-optimized tables, jump to the section [Conflict Detection and Retry Logic](#conflict-detection-and-retry-logic).
  
For general information, see [SET TRANSACTION ISOLATION LEVEL (Transact-SQL)](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md).  
  
## Pessimistic versus Optimistic  
  
The functional differences are due to pessimistic versus optimistic approaches to transaction integrity. Memory-optimized tables use the optimistic approach:  
  
- Pessimistic approach uses locks to block potential conflicts before they occur. Lock are taken when the statement is executed, and released when the transaction is committed.  
  
- Optimistic approach detects conflicts as they occur, and performs validation checks at commit time.  
  - Error 1205, a deadlock, cannot occur for a memory-optimized table.  
  
The optimistic approach is less overhead and is usually more efficient, partly because transaction conflicts are uncommon in most applications. The main functional difference between the pessimistic and optimistic approaches is that if a conflict occurs, in the pessimistic approach you wait, while in the optimistic approach one of the transactions fails and needs to be retried by the client. The functional differences are bigger when the REPEATABLE READ isolation level is in force, and are biggest for the SERIALIZABLE level.  
  
## Transaction Initiation Modes  
  
SQL Server has the following modes for transaction initiation:  
  
- **Autocommit** - The start of a simple query or DML statement implicitly opens a transaction, and the end of the statement implicitly commits the transaction. **Autocommit** is the default.  
  - In autocommit mode, usually you are not required to code a table hint about the transaction isolation level on the memory-optimized table in the FROM clause.  
  
- **Explicit** - Your Transact-SQL contains the code BEGIN TRANSACTION, along with an eventual COMMIT TRANSACTION. Two or more statements can be corralled into the same transaction.  
  - In explicit mode, you must either use the database option MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT or code a table hint about the transaction isolation level on the memory-optimized table in the FROM clause.  
  
- **Implicit** - When SET IMPLICIT_TRANSACTION ON is in force. Perhaps a better name would have been IMPLICIT_BEGIN_TRANSACTION, because all this option does is implicitly perform the equivalent of an explicit BEGIN TRANSACTION before each UPDATE statement if 0 = @@trancount. Therefore it is up to your T-SQL code to eventually issue an explicit COMMIT TRANSACTION.   
  
- **ATOMIC BLOCK** - All statements in ATOMIC blocks always run as part of a single transaction. Either the actions of the atomic block as a whole are committed on success, or the actions are all rolled back when a failure occurs. Each natively compiled stored procedure requires an ATOMIC block.  
  
### Code Example with Explicit Mode  
  
The following interpreted Transact-SQL script uses:  
  
- An explicit transaction.
- A memory-optimized table, named dbo.Order_mo.
- The READ COMMITTED transaction isolation level context.  
  
Therefore it is necessary to have a table hint on the memory-optimized table. The hint must be for SNAPSHOT or an even more isolating level. In the case of the code example, the hint is WITH (SNAPSHOT). If this hint is removed, the script would suffer an error 41368, for which an automated retry would be inappropriate:  

#### Error 41368

Accessing memory optimized tables using the READ COMMITTED isolation level is supported only for autocommit transactions. It is not supported for explicit or implicit transactions. Provide a supported isolation level for the memory-optimized table using a table hint, such as WITH (SNAPSHOT).

```sql
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;  
GO  

BEGIN TRANSACTION;  -- Explicit transaction.  

-- Order_mo  is a memory-optimized table.  
SELECT * FROM  
           dbo.Order_mo  as o  WITH (SNAPSHOT)  -- Table hint.  
      JOIN dbo.Customer  as c  on c.CustomerId = o.CustomerId;  
COMMIT TRANSACTION;
```

The need for the `WITH (SNAPSHOT)` hint can be avoided through the use of the database option `MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT`. When this option is set to `ON`, access to a memory-optimized table under a lower isolation level is automatically elevated to SNAPSHOT isolation.  

```sql
ALTER DATABASE CURRENT
    SET MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = ON;
```

## Row Versioning  
  
Memory-optimized tables use a highly sophisticated row versioning system that makes the optimistic approach efficient, even at the most strict isolation level of SERIALIZABLE. For details see [Introduction to Memory-Optimized Tables](../../relational-databases/in-memory-oltp/introduction-to-memory-optimized-tables.md).  
  
Disk-based tables indirectly have a row versioning system when READ_COMMITTED_SNAPSHOT or the SNAPSHOT isolation level is in effect. This system is based on tempdb, while memory-optimized data structures have row versioning built in, for maximum efficiency.  
  
## Isolation Levels 
  
The following table lists the possible levels of transaction isolation, in sequence from least isolation to most. For details about conflicts that can occur and retry logic to deal with these conflicts, see [Conflict Detection and Retry Logic](#conflict-detection-and-retry-logic). 
  
| Isolation Level | Description |   
| :-- | :-- |   
| READ UNCOMMITTED | Not available: memory-optimized tables cannot be accessed under Read Uncommitted isolation. It is still possible to access memory-optimized tables under SNAPSHOT isolation if the session-level TRANSACTION ISOLATION LEVEL is set to READ UNCOMMITTED, by using the WITH (SNAPSHOT) table hint or setting the database setting MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT to ON. | 
| READ COMMITTED | Supported for memory-optimized tables only when the autocommit mode is in effect. It is still possible to access memory-optimized tables under SNAPSHOT isolation if the session-level TRANSACTION ISOLATION LEVEL is set to READ COMMITTED, by using the WITH (SNAPSHOT) table hint or setting the database setting MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT to ON.<br/><br/>If the database option READ_COMMITTED_SNAPSHOT is set to ON, it is not allowed to access both a memory-optimized and a disk-based table under READ COMMITTED isolation in the same statement. |  
| SNAPSHOT | Supported for memory-optimized tables. <br/><br/> Internally SNAPSHOT is the least demanding transaction isolation level for memory-optimized tables. <br/><br/> SNAPSHOT uses fewer system resources than does REPEATABLE READ or SERIALIZABLE. |  
| REPEATABLE READ | Supported for memory-optimized tables. The guarantee provided by REPEATABLE READ isolation is that, at commit time, no concurrent transaction has updated any of the rows read by this transaction. <br/><br/> Because of the optimistic model, concurrent transactions are not prevented from updating rows read by this transaction. Instead, at commit time this transaction validated that REPEATABLE READ isolation has not been violated. If it has, this transaction is rolled back and must be retried. | 
| SERIALIZABLE | Supported for memory-optimized tables. <br/><br/> Named *Serializable* because the isolation is so strict that it is almost a bit like having the transactions run in series rather than concurrently. | 

## Transaction Phases and Lifetime  
  
When a memory-optimized table is involved, the lifetime of a transaction progresses through the phases as displayed in the following image:
  
![hekaton_transactions](../../relational-databases/in-memory-oltp/media/hekaton-transactions.gif)  
  
Descriptions of the phases follow.  
  
#### Regular Processing: Phase 1 (of 3)  
  
- This phase is composed of the execution of all queries and DML statements in the query.  
- During this phase, the statements see the version of the memory-optimized tables as of the logical start time of the transaction.  
  
#### Validation: Phase 2 (of 3)  
  
- The validation phase begins by assigning the end time, thereby marking the transaction as logically complete. This completion makes all changes of the transaction visible to other transactions which take a dependency on this transaction. The dependent transactions are not allowed to commit until this transaction has successfully committed. In addition, transactions which hold such dependencies are not allowed to return result sets to the client, to ensure the client only sees data that has been successfully committed to the database.  
- This phase comprises the repeatable read and serializable validation. For repeatable read validation, it checks whether any of the rows read by the transaction has since been updated. For serializable validation it checks whether any row has been inserted into any data range scanned by this transaction. Per the table in [Isolation Levels and Conflicts](#isolation-levels), both repeatable read and serializable validation can happen when using snapshot isolation, to validate consistency of unique and foreign key constraints.  
  
#### Commit Processing: Phase 3 (of 3)  
  
- During the commit phase, the changes to durable tables are written to the log, and the log is written to disk. Then control is returned to the client.  
- After commit processing completes, all dependent transactions are notified that they can commit.  
  
As always, you should try to keep your transactional units of work as minimal and brief as is valid for your data needs.  
  
## Conflict Detection and Retry Logic 

There are two kinds of transaction-related error conditions that cause a transaction to fail and roll back. In most cases, once such a failure occurs, the transaction needs to be retried, similar to when a deadlock occurs.
- Conflicts between concurrent transactions. These are update conflicts and validation failures, and can be due to transaction isolation level violations or constraint violations.
- Dependency failures. These result from transactions that you depend on failing to commit, or from the number of dependencies growing too large.

The following are the error conditions that can cause transactions to fail when they access memory-optimized tables.

| Error Code | Description | Cause |
| :-- | :-- | :-- |
| **41302** | Attempted to update a row that was updated in a different transaction since the start of the present transaction. | This error condition occurs if two concurrent transactions attempt to update or delete the same row at the same time. One of the two transactions receives this error message and will need to be retried. <br/><br/>  | 
| **41305**| Repeatable read validation failure. A row read from a memory-optimized table this transaction has been updated by another transaction that has committed before the commit of this transaction. | This error can occur when using REPEATABLE READ or SERIALIZABLE isolation, and also if the actions of a concurrent transaction cause violation of a FOREIGN KEY constraint. <br/><br/>Such concurrent violation of foreign key constraints is rare, and typically indicates an issue with the application logic or with data entry. However, the error can also occur if there is no index on the columns involved with the FOREIGN KEY constraint. Therefore, the guidance is to always create an index on foreign key columns in a memory-optimized table. <br/><br/> For more detailed considerations about validation failures caused by foreign key violations, see [this blog post](/archive/blogs/sqlcat/considerations-around-validation-errors-41305-and-41325-on-memory-optimized-tables-with-foreign-keys) by the SQL Server Customer Advisory Team. |  
| **41325** | Serializable validation failure. A new row was inserted into a range that was scanned earlier by the present transaction. We call this a phantom row. | This error can occur when using SERIALIZABLE isolation, and also if the actions of a concurrent transaction cause violation of a PRIMARY KEY, UNIQUE, or FOREIGN KEY constraint. <br/><br/> Such concurrent constraint violation is rare, and typically indicates an issue with the application logic or data entry. However, similar to repeatable read validation failures, this error can also occur if there is a FOREIGN KEY constraint with no index on the columns involved. |  
| **41301** | Dependency failure: a dependency was taken on another transaction that later failed to commit. | This transaction (Tx1) took a dependency on another transaction (Tx2) while that transaction (Tx2) was in its validation or commit processing phase, by reading data that was written by Tx2. Tx2 subsequently failed to commit. Most common causes for Tx2 to fail to commit are repeatable read (41305) and serializable (41325) validation failures; a less common cause is log IO failure. |
| **41823** and **41840** | Quota for user data in memory-optimized tables and table variables was reached. | Error 41823 applies to SQL Server Express/Web/Standard Edition, as well as single databases in [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)]. Error 41840 applies to elastic pools in [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)]. <br/><br/> In most cases these errors indicate that the maximum user data size was reached, and the way to resolve the error is to delete data from memory-optimized tables. However, there are rare cases where this error is transient. We therefore recommend to retry when first encountering these errors.<br/><br/> Like the other errors in this list, errors 41823 and 41840 cause the active transaction to abort. |
| **41839** | Transaction exceeded the maximum number of commit dependencies. |**Applies to:** [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]. Later versions of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] and [!INCLUDE[sssdsfull](../../includes/sssdsfull-md.md)] do not have a limit on the number of commit dependencies.<br/><br/> There is a limit on the number of transactions a given transaction (Tx1) can depend on. Those transactions are the outgoing dependencies. In addition, there is a limit on the number of transactions that can depend on a given transaction (Tx1). These transactions are the incoming dependencies. The limit for both is 8. <br/><br/> The most common case for this failure is where there is a large number of read transactions accessing data written by a single write transaction. The likelihood of hitting this condition increases if the read transactions are all performing large scans of the same data and if validation or commit processing of the write transaction takes long, for example the write transaction performs large scans under serializable isolation (increases length of the validation phase) or the transaction log is placed on a slow log IO device (increases length of commit processing). If the read transactions are performing large scans and they are expected to access only few rows, an index might be missing. Similarly, if the write transaction uses serializable isolation and is performing large scans but is expected to access only few rows, this is also an indication of a missing index. <br/><br/> The limit on number of commit dependencies can be lifted by using Trace Flag **9926**. Use this trace flag only if you are still hitting this error condition after confirming that there are no missing indexes, as it could mask these issues in the above-mentioned cases. Another caution is that complex dependency graphs, where each transaction has a large number of incoming as well as outgoing dependencies, and individual transactions have many layers of dependencies, can lead to inefficiencies in the system.  |
  
### Retry Logic 

When a transaction fails due to any of the above-mentioned conditions, the transaction should be retried.
  
Retry logic can be implemented at the client or server side. The general recommendation is to implement retry logic on the client side, as it is more efficient, and allows you to deal with result sets returned by the transaction before the failure occurs.  
  
#### Retry T-SQL Code Example  
  
Server-side retry logic using T-SQL should only be used for transactions that do not return result sets to the client. Otherwise, retries can potentially result in additional result sets beyond those anticipated being returned to the client.  
  
The following interpreted T-SQL script illustrates what retry logic can look like for the errors associated with transaction conflicts involving memory-optimized tables.

```sql
-- Retry logic, in Transact-SQL.
DROP PROCEDURE If Exists usp_update_salesorder_dates;
GO

CREATE PROCEDURE usp_update_salesorder_dates
AS
BEGIN
    DECLARE @retry INT = 10;

    WHILE (@retry > 0)
    BEGIN
        BEGIN TRY
            BEGIN TRANSACTION;

            UPDATE dbo.SalesOrder_mo WITH (SNAPSHOT)
                set OrderDate = GetUtcDate()
                where CustomerId = 42;

            UPDATE dbo.SalesOrder_mo WITH (SNAPSHOT)
                set OrderDate = GetUtcDate()
                where CustomerId = 43;

            COMMIT TRANSACTION;

            SET @retry = 0;  -- //Stops the loop.
        END TRY

        BEGIN CATCH
            SET @retry -= 1;

            IF (@retry > 0 AND
                ERROR_NUMBER() in (41302, 41305, 41325, 41301, 41823, 41840, 41839, 1205)
                )
            BEGIN
                IF XACT_STATE() = -1
                    ROLLBACK TRANSACTION;

                WAITFOR DELAY '00:00:00.001';
            END
            ELSE
            BEGIN
                PRINT 'Suffered an error for which Retry is inappropriate.';
                THROW;
            END
        END CATCH

    END -- //While loop
END;
GO

--  EXECUTE usp_update_salesorder_dates;
```

## Cross-Container Transaction  
  
A transaction is called a cross-container transaction if it:  
  
- Accesses a memory-optimized table from interpreted Transact-SQL; or  
- Executes a native proc when a transaction is already open (XACT_STATE() = 1). 

The term "cross-container" derives from the fact that the transaction runs across the two transaction management containers, one for disk-based tables and one for memory-optimized tables.  
  
Within a single cross-container transaction, different isolation levels can be used for accessing disk-based and memory-optimized tables. This difference is expressed through explicit table hints such as WITH (SERIALIZABLE) or through the database option MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT, which implicitly elevates the isolation level for memory-optimized table to snapshot if the TRANSACTION ISOLATION LEVEL is configured as READ COMMITTED or READ UNCOMMITTED.  
  
In the following Transact-SQL code example:  
  
- The disk-based table, Table_D1, is accessed using the READ COMMITTED isolation level.  
- The memory-optimized table Table_MO7 is accessed using the SERIALIZABLE isolation level. Table_MO6 does not have a specific associated isolation level, since inserts are always consistent and executed essentially under serializable isolation.  


```sql
-- Different isolation levels for
-- disk-based tables versus memory-optimized tables,
-- within one explicit transaction.

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
go

BEGIN TRANSACTION;

    -- Table_D1 is a traditional disk-based table, accessed using READ COMMITTED isolation.

    SELECT * FROM Table_D1;


    -- Table_MO6 and Table_MO7 are memory-optimized tables.
    -- Table_MO7 is accessed using SERIALIZABLE isolation,
    --   while Table_MO6 does not have a specific isolation level.

    INSERT Table_MO6
        SELECT * FROM Table_MO7 WITH (SERIALIZABLE);

COMMIT TRANSACTION;
go
```

## Limitations  
  
- Cross-database transactions are not supported for memory-optimized tables. If a transaction accesses a memory-optimized table, the transaction cannot access any other database, except for:  
  - tempdb database.  
  - Read-only from the master database.  
  
- Distributed transactions are not supported: When BEGIN DISTRIBUTED TRANSACTION  is used, the transaction cannot access a memory-optimized table.  
  
## Natively Compiled Stored Procedures  
  
- In a native proc, the ATOMIC block must declare the transaction isolation level for the whole block, such as:  
  - `... BEGIN ATOMIC WITH (TRANSACTION ISOLATION LEVEL = SNAPSHOT, ...) ...`  
  
- No explicit transaction control statements are allowed within the body of a native proc. BEGIN TRANSACTION, ROLLBACK TRANSACTION, and so on, are all disallowed.  
  
- For more information about transaction control with ATOMIC blocks, see [Atomic Blocks](atomic-blocks-in-native-procedures.md)  
  
## Other Transaction Links  
  
- [SET IMPLICIT_TRANSACTIONS](../../t-sql/statements/set-implicit-transactions-transact-sql.md)  
  
- [sp_getapplock (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-getapplock-transact-sql.md)  
  
- [Row Versioning-based Isolation Levels in the Database Engine](/previous-versions/sql/sql-server-2008-r2/ms177404(v=sql.105))  
  
- [Control Transaction Durability](../../relational-databases/logs/control-transaction-durability.md)
