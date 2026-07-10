---
title: "go-mssqldb Transactions"
description: "Use transactions with the go-mssqldb driver, including isolation levels, deadlock handling, savepoints, and production patterns."
author: dlevy-msft
ms.author: dlevy
ms.date: 04/25/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Transactions with go-mssqldb

Transactions group multiple operations into an atomic unit. Either all operations succeed (commit) or none of them take effect (rollback). This article covers how to use transactions with the `go-mssqldb` driver, including isolation levels, error handling, and patterns for production applications.

Examples in this article run against the [AdventureWorks2025](../../samples/adventureworks-install-configure.md) sample database. Write-oriented examples target `HumanResources.Department`, `Production.ProductCategory`, `Production.ProductSubcategory`, and `Production.ProductInventory`.

## Start a transaction

Use `db.BeginTx` to start a transaction. The returned `*sql.Tx` pins a single connection from the pool for the lifetime of the transaction:

```go
tx, err := db.BeginTx(ctx, nil) // nil uses the default isolation level
if err != nil {
    return err
}
defer tx.Rollback() // No-op if tx.Commit() succeeds first.

_, err = tx.ExecContext(ctx, "INSERT INTO HumanResources.Department (Name, GroupName) VALUES (@p1, @p2)",
    sql.Named("p1", name),
    sql.Named("p2", groupName))
if err != nil {
    return err
}

return tx.Commit()
```

> [!IMPORTANT]
> Always call `defer tx.Rollback()` immediately after `BeginTx`. If `Commit()` succeeds, the deferred `Rollback()` is a no-op. If any error occurs before `Commit()`, the deferred `Rollback()` ensures the transaction doesn't remain open and leak the connection back to the pool in an uncommitted state.

## Isolation levels

SQL Server supports several isolation levels that control how concurrent transactions interact. Set the isolation level in `sql.TxOptions`:

```go
tx, err := db.BeginTx(ctx, &sql.TxOptions{
    Isolation: sql.LevelReadCommitted,
})
```

### Isolation level comparison

| Isolation level | Dirty reads | Nonrepeatable reads | Phantom reads | Performance impact | Use when |
| --- | --- | --- | --- | --- | --- |
| `sql.LevelReadUncommitted` | Yes | Yes | Yes | Lowest overhead | Approximate counts, monitoring dashboards. Data accuracy isn't critical. |
| `sql.LevelReadCommitted` | No | Yes | Yes | **Default.** Good for most workloads. | General OLTP workloads. The default and recommended starting point. |
| `sql.LevelRepeatableRead` | No | No | Yes | Moderate. Holds locks longer. | Reads that must see consistent values for the same rows within the transaction. |
| `sql.LevelSerializable` | No | No | No | Highest. Range locks block concurrent inserts. | Financial transactions, inventory management, anywhere phantom reads are unacceptable. |
| `sql.LevelSnapshot` | No | No | No | Uses row versioning in `tempdb`. No blocking. | Read-heavy workloads that need point-in-time consistency without blocking writers. |

> [!NOTE]
> `sql.LevelSnapshot` requires snapshot isolation to be enabled on the database: `ALTER DATABASE AdventureWorks2025 SET ALLOW_SNAPSHOT_ISOLATION ON`.

### Example: Read committed vs. serializable

Specify the isolation level in `sql.TxOptions`:

```go
// Read Committed (default) - suitable for most operations.
tx1, err := db.BeginTx(ctx, &sql.TxOptions{
    Isolation: sql.LevelReadCommitted,
})

// Serializable - prevents phantom reads in financial calculations.
tx2, err := db.BeginTx(ctx, &sql.TxOptions{
    Isolation: sql.LevelSerializable,
})
```

## Read-only routing

The driver doesn't support `sql.TxOptions.ReadOnly`. If you pass `ReadOnly: true`, `BeginTx` returns an error.

For AlwaysOn read-only routing, set `applicationintent=ReadOnly` in the connection string when you open the connection:

```text
sqlserver://listener.example.com?database=AdventureWorks2025&applicationintent=ReadOnly
```

This is a connection-level setting. It can route eligible sessions to a readable secondary, but it doesn't make an existing transaction read-only. Use a dedicated read-only connection or least-privilege credentials for workloads that must not write data.

## Error handling in transactions

Handle errors inside transactions carefully. When any statement fails, the transaction must be rolled back entirely. Don't attempt to continue with other statements after an error:

```go
func createCategory(ctx context.Context, db *sql.DB, category Category) error {
    tx, err := db.BeginTx(ctx, nil)
    if err != nil {
        return fmt.Errorf("begin transaction: %w", err)
    }
    defer tx.Rollback()

    var categoryId int64
    err = tx.QueryRowContext(ctx, `
        INSERT INTO Production.ProductCategory (Name)
        OUTPUT INSERTED.ProductCategoryID
        VALUES (@name)`,
        sql.Named("name", category.Name)).Scan(&categoryId)
    if err != nil {
        return fmt.Errorf("insert category: %w", err)
    }

    // Insert subcategories under the new category.
    for _, sub := range category.Subcategories {
        _, err = tx.ExecContext(ctx, `
            INSERT INTO Production.ProductSubcategory (ProductCategoryID, Name)
            VALUES (@catId, @name)`,
            sql.Named("catId", categoryId),
            sql.Named("name", sub.Name))
        if err != nil {
            return fmt.Errorf("insert subcategory %s: %w", sub.Name, err)
        }
    }

    if err = tx.Commit(); err != nil {
        return fmt.Errorf("commit category: %w", err)
    }
    return nil
}
```

## Savepoints

Savepoints create intermediate rollback points within a transaction. SQL Server supports savepoints natively. Since Go's `database/sql` package doesn't expose savepoints directly, execute them as raw SQL through the transaction:

```go
func createCategoryWithOptionalSubcategory(ctx context.Context, db *sql.DB, category Category) error {
    tx, err := db.BeginTx(ctx, nil)
    if err != nil {
        return err
    }
    defer tx.Rollback()

    err = tx.QueryRowContext(ctx,
        "INSERT INTO Production.ProductCategory (Name) OUTPUT INSERTED.ProductCategoryID VALUES (@p1)",
        sql.Named("p1", category.Name)).Scan(&category.Id)
    if err != nil {
        return err
    }

    // Try to add a subcategory. If it fails, roll back only the subcategory part.
    _, err = tx.ExecContext(ctx, "SAVE TRANSACTION AddSubcategory")
    if err != nil {
        return err
    }

    _, err = tx.ExecContext(ctx,
        "INSERT INTO Production.ProductSubcategory (ProductCategoryID, Name) VALUES (@p1, @p2)",
        sql.Named("p1", category.Id),
        sql.Named("p2", category.DefaultSubcategory))
    if err != nil {
        // Roll back only the subcategory; the category insert is preserved.
        _, rbErr := tx.ExecContext(ctx, "ROLLBACK TRANSACTION AddSubcategory")
        if rbErr != nil {
            return fmt.Errorf("rollback savepoint: %w (original: %w)", rbErr, err)
        }
        log.Printf("Subcategory %q failed, proceeding without it: %v",
            category.DefaultSubcategory, err)
    }

    return tx.Commit()
}
```

> [!NOTE]
> `SAVE TRANSACTION <name>` creates the savepoint. `ROLLBACK TRANSACTION <name>` rolls back to that savepoint without ending the outer transaction. `ROLLBACK` with no name rolls back the entire transaction.

## Deadlock handling

SQL Server resolves deadlocks by terminating one of the competing transactions (the *deadlock victim*) and returning error 1205. The terminated transaction is rolled back automatically by the server.

### Detect and retry deadlocks

Check for error 1205 and retry the entire transaction with a short delay:

```go
import (
    "errors"
    "fmt"
    "time"

    "github.com/microsoft/go-mssqldb"
)

func isDeadlock(err error) bool {
    var mssqlErr mssql.Error
    return errors.As(err, &mssqlErr) && mssqlErr.Number == 1205
}

func withDeadlockRetry(ctx context.Context, db *sql.DB, maxRetries int,
    fn func(ctx context.Context, tx *sql.Tx) error) error {

    for attempt := 0; attempt < maxRetries; attempt++ {
        tx, err := db.BeginTx(ctx, nil)
        if err != nil {
            return err
        }

        err = fn(ctx, tx)
        if err != nil {
            tx.Rollback()
            if isDeadlock(err) && attempt < maxRetries-1 {
                // Wait briefly before retrying.
                delay := time.Duration(attempt+1) * 50 * time.Millisecond
                select {
                case <-ctx.Done():
                    return ctx.Err()
                case <-time.After(delay):
                }
                continue
            }
            return err
        }

        if err = tx.Commit(); err != nil {
            if isDeadlock(err) && attempt < maxRetries-1 {
                delay := time.Duration(attempt+1) * 50 * time.Millisecond
                select {
                case <-ctx.Done():
                    return ctx.Err()
                case <-time.After(delay):
                }
                continue
            }
            return err
        }
        return nil
    }
    return fmt.Errorf("transaction failed after %d deadlock retries", maxRetries)
}
```

### Use the deadlock retry wrapper

Pass a transaction function to the retry wrapper:

```go
err := withDeadlockRetry(ctx, db, 3, func(ctx context.Context, tx *sql.Tx) error {
    _, err := tx.ExecContext(ctx,
        "UPDATE Production.ProductInventory SET Quantity = Quantity - @qty WHERE ProductID = @pid AND LocationID = @lid",
        sql.Named("qty", orderQty),
        sql.Named("pid", productId),
        sql.Named("lid", locationId))
    return err
})
```

### Reduce deadlocks

| Strategy | How it helps |
| --- | --- |
| Access tables in consistent order | When all transactions lock Table A before Table B, circular waits can't occur. |
| Keep transactions short | Shorter transactions hold locks for less time, reducing the window for conflicts. |
| Use the lowest sufficient isolation level | `ReadCommitted` holds fewer locks than `Serializable`. |
| Add appropriate indexes | Index-targeted updates lock fewer rows than table scans. |
| Avoid user interaction during transactions | Never wait for user input between `BeginTx` and `Commit`. |

Retrying is the correct response in application code, but repeated deadlocks on the same query indicate a design problem. Use the SQL Server deadlock graph (captured through Extended Events or the system health session) to identify the competing statements and lock types, then apply the strategies in the preceding table. For a full walkthrough of deadlock analysis and prevention, see the [Deadlocks guide](../../relational-databases/sql-server-deadlocks-guide.md).

## Transactions and connection pinning

A transaction pins a single connection from the pool until `Commit()` or `Rollback()` is called. During this time, no other goroutine can use that connection.

**Implications:**

- Long-running transactions reduce the effective pool size. If you have `MaxOpenConns=25` and 20 open transactions, only 5 connections are available for other work.
- A forgotten `Rollback()` leaks the connection permanently.
- Context cancellation on the transaction's context rolls back the transaction and returns the connection to the pool.

```go
// Set a deadline to prevent transactions from running indefinitely.
ctx, cancel := context.WithTimeout(context.Background(), 30*time.Second)
defer cancel()

tx, err := db.BeginTx(ctx, nil)
if err != nil {
    return err
}
defer tx.Rollback()
```

## Concurrent transactions

Each goroutine should create its own transaction. Never share a `*sql.Tx` across goroutines because `*sql.Tx` is unsafe for concurrent use:

```go
// CORRECT: Each goroutine gets its own transaction.
var g errgroup.Group
for _, item := range items {
    item := item
    g.Go(func() error {
        tx, err := db.BeginTx(ctx, nil)
        if err != nil {
            return err
        }
        defer tx.Rollback()

        _, err = tx.ExecContext(ctx,
            "UPDATE Production.ProductInventory SET Quantity = Quantity - 1 WHERE ProductID = @p1 AND LocationID = @p2",
            sql.Named("p1", item.ProductId),
            sql.Named("p2", item.LocationId))
        if err != nil {
            return err
        }
        return tx.Commit()
    })
}
return g.Wait()
```

## Distributed transactions

The `go-mssqldb` driver doesn't support distributed transactions (XA transactions or `System.Transactions` equivalents). If you need to coordinate work across multiple databases:

- Use a saga pattern with compensating actions.
- Consolidate operations into a single database when possible.
- Use linked servers with `BEGIN DISTRIBUTED TRANSACTION` from Transact-SQL (T-SQL) if both databases are SQL Server.

## Transaction checklist

| Area | Recommendation |
| --- | --- |
| Rollback safety | Always `defer tx.Rollback()` immediately after `BeginTx`. |
| Isolation level | Start with `ReadCommitted` (the default). Escalate only when needed. |
| Deadlocks | Wrap transactional code in a retry loop. Access tables in a consistent order. |
| Duration | Keep transactions as short as possible. Set context deadlines. |
| Concurrency | Never share a `*sql.Tx` across goroutines. |
| Savepoints | Use `SAVE TRANSACTION` and `ROLLBACK TRANSACTION <name>` for partial rollback. |
| Pool impact | Monitor `db.Stats().InUse` to detect connection leaks from uncommitted transactions. |

## Related content

- [Error handling and retry patterns](error-handling.md)
- [Queries and statements](queries-statements.md)
- [Connection pooling](connection-pooling.md)
- [Concurrent programming](concurrent-programming.md)
