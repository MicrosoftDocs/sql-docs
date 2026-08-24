---
title: "Transactions with Memory-Optimized Tables"
description: Learn about transactions for memory-optimized tables and natively compiled stored procedures and how they differ from transactions for disk-based tables.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 01/07/2026
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: concept-article
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Transactions with memory-optimized tables

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes all the aspects of transactions that are specific to memory-optimized tables and natively compiled stored procedures.

The transaction isolation levels in SQL Server apply differently to memory-optimized tables versus disk-based tables, and the underlying mechanisms are different. An understanding of the differences helps the programmer design a high throughput system. The goal of transaction integrity is shared in all cases.

For error conditions specific to transactions on memory-optimized tables, see the [Conflict Detection and Retry Logic](#conflict-detection-and-retry-logic) section.

For general information, see [SET TRANSACTION ISOLATION LEVEL](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md).

## Pessimistic versus optimistic

The functional differences between memory-optimized tables and disk-based tables come from pessimistic versus optimistic approaches to transaction integrity. Memory-optimized tables use the optimistic approach:

- Pessimistic approach uses locks to block potential conflicts before they occur. Lock are taken when the statement is executed, and released when the transaction is committed.

- Optimistic approach detects conflicts as they occur, and performs validation checks at commit time.
  - Error 1205, a deadlock, can't occur for a memory-optimized table.

The optimistic approach has less overhead and is usually more efficient, partly because transaction conflicts are uncommon in most applications. The main functional difference between the pessimistic and optimistic approaches is that if a conflict occurs, in the pessimistic approach you wait, while in the optimistic approach one of the transactions fails and needs to be retried by the client. The functional differences are bigger when the `REPEATABLE READ` isolation level is in force, and are biggest for the `SERIALIZABLE` level.

## Transaction initiation modes

SQL Server uses the following modes for transaction initiation:

- **Autocommit**. A simple query or DML statement implicitly opens a transaction at the start, and the end of the statement implicitly commits the transaction. **Autocommit** is the default.

  In autocommit mode, you usually don't need to code a table hint about the transaction isolation level on the memory-optimized table in the `FROM` clause.

- **Explicit**. Your Transact-SQL contains the code `BEGIN TRANSACTION`, along with an eventual `COMMIT TRANSACTION`. You can corral two or more statements into the same transaction.

  In explicit mode, you must either use the database option `MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT` or code a table hint about the transaction isolation level on the memory-optimized table in the `FROM` clause.

- **Implicit**. Initiated when `SET IMPLICIT_TRANSACTION ON` is in force. This option implicitly performs the equivalent of an explicit `BEGIN TRANSACTION` before each `UPDATE` statement if `@@TRANCOUNT` is `0`. Therefore, it's up to your T-SQL code to eventually issue an explicit `COMMIT TRANSACTION`.

- **ATOMIC block**. All statements in `ATOMIC` blocks always run as part of a single transaction. Either the actions of the atomic block as a whole are committed on success, or the actions are all rolled back when a failure occurs. Each natively compiled stored procedure requires an `ATOMIC` block.

### Code example with explicit mode

The following interpreted Transact-SQL script uses:

- An explicit transaction.
- A memory-optimized table, named `dbo.Order_mo`.
- The `READ COMMITTED` transaction isolation level context.

Therefore, you need to add a table hint on the memory-optimized table. The hint must be for `SNAPSHOT` or an even more isolating level. In the case of the code example, the hint is `WITH (SNAPSHOT)`. If you remove this hint, the script encounters error 41368, for which an automated retry isn't appropriate:

#### Error 41368

Accessing memory optimized tables by using the `READ COMMITTED` isolation level is supported only for autocommit transactions. It's not supported for explicit or implicit transactions. Provide a supported isolation level for the memory-optimized table by using a table hint, such as `WITH (SNAPSHOT)`.

```sql
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

BEGIN TRANSACTION; -- Explicit transaction.

-- Order_mo is a memory-optimized table.
SELECT *
FROM dbo.Order_mo AS o WITH (SNAPSHOT) -- Table hint.
     INNER JOIN dbo.Customer AS c
         ON c.CustomerId = o.CustomerId;

COMMIT TRANSACTION;
```

You can avoid the need for the `WITH (SNAPSHOT)` hint by using the database option `MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT`. When you set this option to `ON`, access to a memory-optimized table under a lower isolation level automatically elevates to `SNAPSHOT` isolation.

```sql
ALTER DATABASE CURRENT
    SET MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = ON;
```

## Row versioning

Memory-optimized tables use a highly sophisticated row versioning system that makes the optimistic approach efficient, even at the most strict isolation level of `SERIALIZABLE`. For more information, see [Introduction to Memory-Optimized Tables](introduction-to-memory-optimized-tables.md).

Disk-based tables indirectly have a row versioning system when `READ_COMMITTED_SNAPSHOT` or the `SNAPSHOT` isolation level is in effect. This system is based on `tempdb`, while memory-optimized data structures have row versioning built in, for maximum efficiency.

## Isolation levels

The following table lists the possible levels of transaction isolation, in sequence from least isolation to most. For details about conflicts that can occur and retry logic to deal with these conflicts, see [Conflict Detection and Retry Logic](#conflict-detection-and-retry-logic).

| Isolation Level | Description |
| --- | --- |
| `READ UNCOMMITTED` | Not available: you can't access memory-optimized tables under Read Uncommitted isolation. You can still access memory-optimized tables under `SNAPSHOT` isolation if you set the session-level `TRANSACTION ISOLATION LEVEL` to `READ UNCOMMITTED`, by using the `WITH (SNAPSHOT)` table hint or setting the database setting `MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT` to `ON`. |
| `READ COMMITTED` | Supported for memory-optimized tables only when the autocommit mode is in effect. You can still access memory-optimized tables under `SNAPSHOT` isolation if you set the session-level `TRANSACTION ISOLATION LEVEL` to `READ COMMITTED`, by using the `WITH (SNAPSHOT)` table hint or setting the database setting `MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT` to `ON`.<br /><br />If you set the database option `READ_COMMITTED_SNAPSHOT` to `ON`, you can't access both a memory-optimized and a disk-based table under `READ COMMITTED` isolation in the same statement. |
| `SNAPSHOT` | Supported for memory-optimized tables.<br /><br />Internally `SNAPSHOT` is the least demanding transaction isolation level for memory-optimized tables.<br /><br />`SNAPSHOT` uses fewer system resources than does `REPEATABLE READ` or `SERIALIZABLE`. |
| `REPEATABLE READ` | Supported for memory-optimized tables. The guarantee provided by `REPEATABLE READ` isolation is that, at commit time, no concurrent transaction has updated any of the rows read by this transaction.<br /><br />Because of the optimistic model, concurrent transactions aren't prevented from updating rows read by this transaction. Instead, at commit time, this transaction validated that `REPEATABLE READ` isolation hasn't been violated. If it has, this transaction is rolled back and must be retried. |
| `SERIALIZABLE` | Supported for memory-optimized tables.<br /><br />Named *Serializable* because the isolation is so strict that it's almost a bit like having the transactions run in series rather than concurrently. |

## Transaction phases and lifetime

When a memory-optimized table is involved, the lifetime of a transaction goes through the phases shown in the following image:

:::image type="content" source="media/transactions-with-memory-optimized-tables/memory-optimized-transactions.png" alt-text="Diagram showing in-memory transaction lifetime.":::

Descriptions of the phases follow.

### Phase 1 of 3: Regular processing

- This phase executes all queries and DML statements in the query.

- During this phase, the statements see the version of the memory-optimized tables as of the logical start time of the transaction.

### Phase 2 of 3: Validation

- The validation phase begins by assigning the end time, which marks the transaction as logically complete. This completion makes all changes of the transaction visible to other transactions that take a dependency on this transaction. The dependent transactions can't commit until this transaction successfully commits. In addition, transactions that hold such dependencies can't return result sets to the client, to ensure the client only sees data that is successfully committed to the database.

- This phase comprises the repeatable read and serializable validation. For repeatable read validation, it checks whether the transaction updated any of the rows it read. For serializable validation, it checks whether the transaction inserted a row into any data range it scanned. Per the table in [Isolation Levels and Conflicts](#isolation-levels), both repeatable read and serializable validation can happen when using snapshot isolation, to validate consistency of unique and foreign key constraints.

### Phase 3 of 3: Commit processing

- During the commit phase, the process writes the changes to durable tables to the log, and the process writes the log to disk. Then, the process returns control to the client.

- After commit processing completes, the process notifies all dependent transactions that they can commit.

As always, keep your transactional units of work as minimal and brief as is valid for your data needs.

## Conflict detection and retry logic

Two kinds of transaction-related error conditions can cause a transaction to fail and roll back. In most cases, you need to retry the transaction after such a failure, similar to when a deadlock occurs.

- **Conflicts between concurrent transactions**. These conflicts, including update conflicts and validation failures, can happen because of transaction isolation level violations or constraint violations.

- **Dependency failures**. These failures result from transactions that you depend on failing to commit, or from the number of dependencies growing too large.

The following error conditions can cause transactions to fail when they access memory-optimized tables.

| Error code | Description | Cause |
| --- | --- | --- |
| `41302` | Attempted to update a row that was updated in a different transaction since the start of the present transaction. | This error condition occurs if two concurrent transactions attempt to update or delete the same row at the same time. One of the two transactions receives this error message and needs to be retried. |
| `41305` | Repeatable read validation failure. A row read from a memory-optimized table this transaction has been updated by another transaction that committed before the commit of this transaction. | This error can occur when using `REPEATABLE READ` or `SERIALIZABLE` isolation, and also if the actions of a concurrent transaction cause violation of a `FOREIGN KEY` constraint.<br /><br />Such concurrent violation of foreign key constraints is rare, and typically indicates an issue with the application logic or with data entry. However, the error can also occur if there's no index on the columns involved with the `FOREIGN KEY` constraint. Therefore, always create an index on foreign key columns in a memory-optimized table.<br /><br />For more detailed considerations about validation failures caused by foreign key violations, see [Considerations around validation errors 41305 and 41325 on memory optimized tables with foreign keys](/archive/blogs/sqlcat/considerations-around-validation-errors-41305-and-41325-on-memory-optimized-tables-with-foreign-keys) by the SQL Server Customer Advisory Team. |
| `41325` | Serializable validation failure. A new row was inserted into a range that was scanned earlier by the present transaction. We call this a phantom row. | This error can occur when using `SERIALIZABLE` isolation, and also if the actions of a concurrent transaction cause violation of a `PRIMARY KEY`, `UNIQUE`, or `FOREIGN KEY` constraint.<br /><br />Such concurrent constraint violation is rare, and typically indicates an issue with the application logic or data entry. However, similar to repeatable read validation failures, this error can also occur if there's a `FOREIGN KEY` constraint with no index on the columns involved. |
| `41301` | Dependency failure: a dependency was taken on another transaction that later failed to commit. | This transaction (`Tx1`) took a dependency on another transaction (`Tx2`) while that transaction (`Tx2`) was in its validation or commit processing phase, by reading data that was written by `Tx2`. `Tx2` then failed to commit. The most common causes for `Tx2` to fail to commit are repeatable read (41305) and serializable (41325) validation failures. A less common cause is log IO failure. |
| `41823` and `41840` | Quota for user data in memory-optimized tables and table variables was reached. | Error 41823 applies to SQL Server Express, Web, and Standard editions, and single databases in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. Error 41840 applies to elastic pools in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].<br /><br />In most cases, these errors indicate that the maximum user data size was reached. To resolve the error, delete data from memory-optimized tables. However, rare cases exist where this error is transient. Retry when first encountering these errors.<br /><br />Like the other errors in this list, errors 41823 and 41840 cause the active transaction to abort. |
| `41839` | Transaction exceeded the maximum number of commit dependencies. | There's a limit on the number of transactions a given transaction (`Tx1`) can depend on. Those transactions are the outgoing dependencies. In addition, there's a limit on the number of transactions that can depend on a given transaction (`Tx1`). These transactions are the incoming dependencies. The limit for both is 8.<br /><br />The most common case for this failure is where a large number of read transactions access data written by a single write transaction. The likelihood of hitting this condition increases if the read transactions all perform large scans of the same data and if validation or commit processing of the write transaction takes long. For example, the write transaction performs large scans under serializable isolation (which increases the length of the validation phase) or the transaction log is placed on a slow log IO device (which increases the length of commit processing). If the read transactions perform large scans and they're expected to access only few rows, an index might be missing. Similarly, if the write transaction uses serializable isolation and is performing large scans but is expected to access only few rows, this condition also indicates a missing index.<br /><br />You can lift the limit on number of commit dependencies by using trace flag `9926`. Use this trace flag only if you're still hitting this error condition after confirming that there are no missing indexes, as it could mask these issues in the above-mentioned cases. Another caution is that complex dependency graphs, where each transaction has a large number of incoming and outgoing dependencies, and individual transactions have many layers of dependencies, can lead to inefficiencies in the system.<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]. Later versions of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] don't have a limit on the number of commit dependencies. |

### Retry logic

When a transaction fails due to any of the previously mentioned conditions, retry the transaction.

You can implement retry logic on the client or server side. Implement retry logic on the client side for better efficiency. This approach also helps you handle result sets returned by the transaction before the failure occurs.

#### Retry T-SQL code example

Use server-side retry logic with T-SQL only for transactions that don't return result sets to the client. Otherwise, retries can potentially return extra result sets to the client.

The following interpreted T-SQL script shows what retry logic can look like for errors associated with transaction conflicts involving memory-optimized tables.

```sql
-- Retry logic, in Transact-SQL.
DROP PROCEDURE If Exists usp_update_salesorder_dates;
GO

CREATE PROCEDURE usp_update_salesorder_dates
AS
BEGIN
    DECLARE @retry AS INT = 10;

    WHILE (@retry > 0)
    BEGIN

        BEGIN TRY
            BEGIN TRANSACTION;

            UPDATE dbo.SalesOrder_mo WITH (SNAPSHOT)
            SET OrderDate = GETUTCDATE()
            WHERE CustomerId = 42;

            UPDATE dbo.SalesOrder_mo WITH (SNAPSHOT)
            SET OrderDate = GETUTCDATE()
            WHERE CustomerId = 43;

            COMMIT TRANSACTION;

            SET @retry = 0; -- Stops the loop.
        END TRY

        BEGIN CATCH
            SET @retry - = 1;

            IF (@retry > 0
                AND ERROR_NUMBER() IN (41302, 41305, 41325, 41301, 41823, 41840, 41839, 1205))
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

    END -- While loop
END
GO

-- EXECUTE usp_update_salesorder_dates;
```

## Cross-container transaction

A transaction is a cross-container transaction if it:

- Accesses a memory-optimized table from interpreted Transact-SQL.
- Executes a native proc when a transaction is already open (`XACT_STATE() = 1`).

The term "cross-container" comes from the fact that the transaction runs across two transaction management containers. One container manages disk-based tables, and the other manages memory-optimized tables.

Within a single cross-container transaction, you can use different isolation levels for accessing disk-based and memory-optimized tables. You express this difference through explicit table hints such as `WITH (SERIALIZABLE)` or through the database option `MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT`. This option implicitly elevates the isolation level for memory-optimized table to snapshot if the `TRANSACTION ISOLATION LEVEL` is configured as `READ COMMITTED` or `READ UNCOMMITTED`.

In the following Transact-SQL code example:

- The disk-based table, `Table_D1`, is accessed using the `READ COMMITTED` isolation level.
- The memory-optimized table `Table_MO7` is accessed using the `SERIALIZABLE` isolation level. `Table_MO6` doesn't have a specific associated isolation level, since inserts are always consistent and executed essentially under serializable isolation.

```sql
-- Different isolation levels for
-- disk-based tables versus memory-optimized tables,
-- within one explicit transaction.
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO

BEGIN TRANSACTION;

-- Table_D1 is a traditional disk-based table, accessed using READ COMMITTED isolation.
SELECT *
FROM Table_D1;

-- Table_MO6 and Table_MO7 are memory-optimized tables.
-- Table_MO7 is accessed using SERIALIZABLE isolation,
-- while Table_MO6 does not have a specific isolation level.

INSERT INTO Table_MO6
SELECT *
FROM Table_MO7 WITH (SERIALIZABLE);

COMMIT TRANSACTION;
```

## Limitations

- Cross-database transactions aren't supported for memory-optimized tables. If a transaction accesses a memory-optimized table, the transaction can't access any other database, except for:

  - `tempdb` database.
  - Read-only access from the `master` database.

- Distributed transactions aren't supported: When you use `BEGIN DISTRIBUTED TRANSACTION`, the transaction can't access a memory-optimized table.

## Natively compiled stored procedures

- In a native proc, the `ATOMIC` block must declare the transaction isolation level for the whole block, such as:

  `... BEGIN ATOMIC WITH (TRANSACTION ISOLATION LEVEL = SNAPSHOT, ...) ...`

- You can't include explicit transaction control statements within the body of a native stored procedure. Statements like `BEGIN TRANSACTION` and `ROLLBACK TRANSACTION` are disallowed.

- For more information about transaction control with `ATOMIC` blocks, see [Atomic Blocks in Native Procedures](atomic-blocks-in-native-procedures.md).

## Related content

- [SET IMPLICIT_TRANSACTIONS (Transact-SQL)](../../t-sql/statements/set-implicit-transactions-transact-sql.md)
- [sys.sp_getapplock (Transact-SQL)](../system-stored-procedures/sp-getapplock-transact-sql.md)
- [Row Versioning-based Isolation Levels in the Database Engine](/previous-versions/sql/sql-server-2008-r2/ms177404(v=sql.105))
- [Control Transaction Durability](../logs/control-transaction-durability.md)
