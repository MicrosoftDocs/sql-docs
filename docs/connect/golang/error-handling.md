---
title: "go-mssqldb Error Handling and Retry Patterns"
description: "Handle errors, classify transient faults, and implement retry logic in Go applications using the go-mssqldb driver for SQL Server."
author: dlevy-msft
ms.author: dlevy
ms.date: 07/08/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Error handling and retry patterns with go-mssqldb

Production Go applications need structured error handling to distinguish between transient failures that you can retry and permanent errors that require human intervention. This article covers error classification, retry patterns, and resilience strategies for the `go-mssqldb` driver.

## SQL Server error structure

When SQL Server returns an error, the `go-mssqldb` driver wraps it in a `mssql.Error` struct. Use a type assertion to access the structured error fields:

```go
import (
    "database/sql"
    "errors"
    "fmt"

    mssql "github.com/microsoft/go-mssqldb"
)

func handleError(err error) {
    var mssqlErr mssql.Error
    if errors.As(err, &mssqlErr) {
        fmt.Printf("Number:  %d\n", mssqlErr.Number)
        fmt.Printf("State:   %d\n", mssqlErr.State)
        fmt.Printf("Class:   %d\n", mssqlErr.Class)
        fmt.Printf("Message: %s\n", mssqlErr.Message)
        fmt.Printf("Server:  %s\n", mssqlErr.ServerName)
        fmt.Printf("Proc:    %s\n", mssqlErr.ProcName)
        fmt.Printf("Line:    %d\n", mssqlErr.LineNo)
    }
}
```

### Error fields

| Field | Type | Description |
| --- | --- | --- |
| `Number` | `int32` | SQL Server error number. Maps to `sys.messages`. |
| `State` | `uint8` | Error state. Provides additional context for the same error number. |
| `Class` | `uint8` | Severity level (0-25). Severity 11-16 are user-correctable. Severity 17+ indicate resource or system problems. |
| `Message` | `string` | Human-readable error text from the server. |
| `ServerName` | `string` | Name of the SQL Server instance that raised the error. |
| `ProcName` | `string` | Stored procedure or function name where the error occurred. Empty for ad hoc queries. |
| `LineNo` | `int32` | Line number in the Transact-SQL (T-SQL) batch or stored procedure. |

### Severity levels

| Severity range | Meaning | Action |
| --- | --- | --- |
| 0-10 | Informational messages | No error. Log if useful. |
| 11-16 | User-correctable errors | Fix the query, parameters, or permissions. |
| 17-19 | Resource errors | Retry. The server might be under load or out of resources. |
| 20-25 | Fatal errors | The connection is broken. Reconnect and retry. |

## Classify errors as transient or permanent

Transient errors are temporary conditions that resolve on their own, such as network blips, connection throttling, or brief resource contention. Permanent errors require code or configuration changes.

### Common transient error numbers

Use the following shared catalog as the canonical list of transient connection-establishment and request-path transport errors:

[!INCLUDE [transient-connection-errors](../includes/transient-connection-errors.md)]

The following `isTransient` function shows one Go implementation pattern for retry classification. Treat the shared catalog above as the source of truth, and keep your code lookup aligned with it.

```go
// isTransient returns true if the error is a transient SQL Server error
// that is likely to succeed on retry.
func isTransient(err error) bool {
    var mssqlErr mssql.Error
    if !errors.As(err, &mssqlErr) {
        // Network errors, context deadlines, and connection resets
        // are also transient.
        return isNetworkError(err)
    }

    if isTransientSQLNumber(mssqlErr.Number) {
        return true
    }

    // Severity 17-19 indicates resource issues that are typically transient.
    return mssqlErr.Class >= 17 && mssqlErr.Class <= 19
}

// Keep this lookup synchronized with the shared transient catalog above.
var transientSQLNumbers = map[int32]struct{}{
    64:    {}, // Transport/connection error.
    1205:  {}, // Deadlock victim.
    40197: {}, // Service error processing request.
    40501: {}, // Service is currently busy.
    40613: {}, // Database is currently unavailable.
    49918: {}, // Cannot process request: not enough resources.
    49919: {}, // Cannot process create/update request.
    49920: {}, // Cannot process request: too many operations.
}

func isTransientSQLNumber(number int32) bool {
    _, ok := transientSQLNumbers[number]
    return ok
}
```

If you experience configuration and quota errors, fix the underlying capacity, database, or network configuration before retrying. Examples include:

- `40544` (database size quota)
- `4060` (cannot open database)
- `40615` (firewall rule)

### Detect network errors

Network-level errors don't produce `mssql.Error` values. Check for common Go network error types:

```go
import (
    "context"
    "errors"
    "net"
    "io"
)

func isNetworkError(err error) bool {
    if err == nil {
        return false
    }

    // Context deadline exceeded or canceled
    if errors.Is(err, context.DeadlineExceeded) {
        return true
    }

    // Connection reset or broken pipe
    var netErr *net.OpError
    if errors.As(err, &netErr) {
        return true
    }

    // Unexpected EOF (server dropped the connection)
    if errors.Is(err, io.ErrUnexpectedEOF) || errors.Is(err, io.EOF) {
        return true
    }

    return false
}
```

## Implement retry with exponential backoff

Retry transient errors with increasing delays between attempts. This approach gives the server time to recover and avoids overwhelming it with rapid retries.

```go
import (
    "context"
    "database/sql"
    "errors"
    "fmt"
    "log"
    "math"
    "math/rand"
    "time"

    mssql "github.com/microsoft/go-mssqldb"
)

// RetryConfig controls retry behavior.
type RetryConfig struct {
    MaxAttempts int           // Maximum number of attempts (including the first).
    BaseDelay   time.Duration // Initial delay before the first retry.
    MaxDelay    time.Duration // Upper bound on delay between retries.
}

// DefaultRetryConfig provides sensible defaults for SQL Server workloads.
var DefaultRetryConfig = RetryConfig{
    MaxAttempts: 5,
    BaseDelay:   100 * time.Millisecond,
    MaxDelay:    10 * time.Second,
}

// RetryFunc executes fn with retries for transient errors.
func RetryFunc(ctx context.Context, cfg RetryConfig, fn func(ctx context.Context) error) error {
    var lastErr error
    for attempt := 0; attempt < cfg.MaxAttempts; attempt++ {
        lastErr = fn(ctx)
        if lastErr == nil {
            return nil
        }

        if !isTransient(lastErr) {
            return lastErr // Permanent error, don't retry.
        }

        if attempt == cfg.MaxAttempts-1 {
            break // Last attempt, don't sleep.
        }

        delay := calculateDelay(attempt, cfg.BaseDelay, cfg.MaxDelay)

        select {
        case <-ctx.Done():
            return ctx.Err()
        case <-time.After(delay):
        }
    }
    return lastErr
}

func calculateDelay(attempt int, baseDelay, maxDelay time.Duration) time.Duration {
    // Exponential backoff: base * 2^attempt
    delay := time.Duration(float64(baseDelay) * math.Pow(2, float64(attempt)))
    if delay > maxDelay {
        delay = maxDelay
    }
    // Add jitter: +/- 25% to avoid thundering herd
    jitter := time.Duration(rand.Int63n(int64(delay) / 2))
    return delay/2 + jitter
}

// isTransient classifies retryable SQL Server errors.
// For a fuller example, see "Classify errors as transient or permanent" earlier in this article.
func isTransient(err error) bool {
    var mssqlErr mssql.Error
    if !errors.As(err, &mssqlErr) {
        return errors.Is(err, context.DeadlineExceeded)
    }

    switch mssqlErr.Number {
    case 1205, 40197, 40501, 40613, 49918, 49919, 49920:
        return true
    }

    return mssqlErr.Class >= 17 && mssqlErr.Class <= 19
}

// getEmployeeCount wraps a query with automatic retry.
func getEmployeeCount(ctx context.Context, db *sql.DB) (int, error) {
    var count int
    err := RetryFunc(ctx, DefaultRetryConfig, func(ctx context.Context) error {
        // This query executes on every retry attempt until success or exhaustion.
        return db.QueryRowContext(ctx, "SELECT COUNT(*) FROM HumanResources.Employee").Scan(&count)
    })
    return count, err
}

// Example call site (assumes db is already initialized).
func example(ctx context.Context, db *sql.DB) {
    queryCtx, cancel := context.WithTimeout(ctx, 15*time.Second)
    defer cancel()

    count, err := getEmployeeCount(queryCtx, db)
    if err != nil {
        log.Fatal(err)
    }
    fmt.Printf("Employee count: %d\n", count)
}
```

## Handle deadlocks

Deadlocks (error 1205) are the most common transient error in multi-user applications. SQL Server automatically terminates one of the competing sessions and returns error 1205 to the victim.

### Detect a deadlock

Check whether a SQL Server error is a deadlock (error 1205).

```go
func isDeadlock(err error) bool {
    var mssqlErr mssql.Error
    if errors.As(err, &mssqlErr) {
        return mssqlErr.Number == 1205
    }
    return false
}
```

### Retry transactions after deadlocks

When a deadlock occurs inside a transaction, the server rolls back the entire transaction. You must retry the complete transaction, not just the failed statement:

```go
func transferInventory(ctx context.Context, db *sql.DB, productID, fromLocationID, toLocationID int, qty int) error {
    return RetryFunc(ctx, DefaultRetryConfig, func(ctx context.Context) error {
        tx, err := db.BeginTx(ctx, &sql.TxOptions{
            Isolation: sql.LevelReadCommitted,
        })
        if err != nil {
            return err
        }
        defer tx.Rollback()

        _, err = tx.ExecContext(ctx,
            "UPDATE Production.ProductInventory SET Quantity = Quantity - @qty WHERE ProductID = @pid AND LocationID = @lid",
            sql.Named("qty", qty),
            sql.Named("pid", productID),
            sql.Named("lid", fromLocationID))
        if err != nil {
            return err
        }

        _, err = tx.ExecContext(ctx,
            "UPDATE Production.ProductInventory SET Quantity = Quantity + @qty WHERE ProductID = @pid AND LocationID = @lid",
            sql.Named("qty", qty),
            sql.Named("pid", productID),
            sql.Named("lid", toLocationID))
        if err != nil {
            return err
        }

        return tx.Commit()
    })
}
```

> [!TIP]
> Reduce deadlocks by accessing tables in a consistent order across all transactions and keeping transactions short.

Retrying is the correct response in application code, but repeated deadlocks on the same query indicate a design problem. Use the SQL Server deadlock graph (captured through Extended Events or the system health session) to identify the competing statements and lock types. For a full walkthrough of deadlock analysis and prevention, see the [Deadlocks guide](../../relational-databases/sql-server-deadlocks-guide.md). For deadlock handling strategies specific to transactions, see [Deadlock handling](transactions.md#deadlock-handling).

## Handle connection pool exhaustion

When all connections in the pool are in use and `MaxOpenConns` is reached, new callers block until a connection becomes available or the context deadline expires. This situation manifests as slow requests or context deadline errors, not as explicit pool exhaustion errors.

### Detect pool pressure

Monitor pool statistics and alert when wait counts increase.

```go
func monitorPool(ctx context.Context, db *sql.DB) {
    ticker := time.NewTicker(10 * time.Second)
    defer ticker.Stop()

    var lastWaitCount int64
    for {
        select {
        case <-ctx.Done():
            return
        case <-ticker.C:
            stats := db.Stats()
            newWaits := stats.WaitCount - lastWaitCount
            lastWaitCount = stats.WaitCount

            if newWaits > 0 {
                log.Printf("Pool pressure: open=%d inUse=%d idle=%d newWaits=%d waitDuration=%v",
                    stats.OpenConnections, stats.InUse, stats.Idle,
                    newWaits, stats.WaitDuration)
            }
        }
    }
}
```

### Common causes and solutions

| Symptom | Cause | Solution |
| --- | --- | --- |
| `WaitCount` increases steadily | `MaxOpenConns` is too low | Increase `MaxOpenConns` to match your concurrency. |
| `InUse` equals `MaxOpenConns` for extended periods | Connections aren't returned to the pool | Close `*sql.Rows`, commit or rollback `*sql.Tx`, and close `*sql.Conn` promptly. |
| `OpenConnections` keeps growing | Connections leak faster than `MaxIdleConns` can recycle | Set `ConnMaxLifetime` and `ConnMaxIdleTime` to bound connection age. |
| Context deadline exceeded during queries | Pool is saturated and callers wait too long | Increase pool size, reduce query execution time, or add query timeouts. |

## Handle specific SQL Server errors

### Constraint violations

Unique key and foreign key violations are permanent errors that indicate a logic problem in the application:

```go
func isUniqueViolation(err error) bool {
    var mssqlErr mssql.Error
    if errors.As(err, &mssqlErr) {
        return mssqlErr.Number == 2627 || // Unique constraint violation
            mssqlErr.Number == 2601       // Unique index violation
    }
    return false
}

func isForeignKeyViolation(err error) bool {
    var mssqlErr mssql.Error
    if errors.As(err, &mssqlErr) {
        return mssqlErr.Number == 547 // FK constraint violation
    }
    return false
}
```

### Upsert pattern with conflict detection

Use a `MERGE` statement to insert or update a row atomically:

```go
func upsertDepartment(ctx context.Context, db *sql.DB, id int, name, groupName string) error {
    _, err := db.ExecContext(ctx, `
        MERGE INTO HumanResources.Department AS target
        USING (SELECT @id AS DepartmentID, @name AS Name, @grp AS GroupName) AS source
        ON target.DepartmentID = source.DepartmentID
        WHEN MATCHED THEN
            UPDATE SET Name = source.Name, GroupName = source.GroupName
        WHEN NOT MATCHED THEN
            INSERT (Name, GroupName) VALUES (source.Name, source.GroupName);`,
        sql.Named("id", id),
        sql.Named("name", name),
        sql.Named("grp", groupName))
    return err
}
```

### Permission errors

Detect common permission-denied error numbers to provide a clear message to callers:

```go
func isPermissionError(err error) bool {
    var mssqlErr mssql.Error
    if errors.As(err, &mssqlErr) {
        return mssqlErr.Number == 229 ||   // SELECT permission denied
            mssqlErr.Number == 230 ||       // Column permission denied
            mssqlErr.Number == 262 ||       // CREATE permission denied
            mssqlErr.Number == 300 ||       // VIEW permission denied
            mssqlErr.Number == 15247        // User doesn't have permission
    }
    return false
}
```

## Handle sql.ErrNoRows

`sql.ErrNoRows` isn't a SQL Server error. The `QueryRowContext.Scan` method returns it when the query returns no rows. Handle it explicitly to distinguish "not found" from actual errors:

```go
func getEmployee(ctx context.Context, db *sql.DB, id int) (*Employee, error) {
    var emp Employee
    err := db.QueryRowContext(ctx,
        "SELECT TOP (1) BusinessEntityID, FirstName + ' ' + LastName AS Name, CountryRegionName AS Location FROM Sales.vSalesPerson WHERE BusinessEntityID = @p1",
        sql.Named("p1", id)).Scan(&emp.Id, &emp.Name, &emp.Location)

    if errors.Is(err, sql.ErrNoRows) {
        return nil, nil // Not found, not an error.
    }
    if err != nil {
        return nil, fmt.Errorf("query employee %d: %w", id, err)
    }
    return &emp, nil
}
```

## Wrap errors with context

Add context to errors so callers can understand where the failure occurred:

```go
func getEmployeesByLocation(ctx context.Context, db *sql.DB, location string) ([]Employee, error) {
    rows, err := db.QueryContext(ctx,
        "SELECT BusinessEntityID, FirstName + ' ' + LastName AS Name, CountryRegionName AS Location FROM Sales.vSalesPerson WHERE CountryRegionName = @p1",
        sql.Named("p1", location))
    if err != nil {
        return nil, fmt.Errorf("query employees by location %q: %w", location, err)
    }
    defer rows.Close()

    var employees []Employee
    for rows.Next() {
        var emp Employee
        if err := rows.Scan(&emp.Id, &emp.Name, &emp.Location); err != nil {
            return nil, fmt.Errorf("scan employee row: %w", err)
        }
        employees = append(employees, emp)
    }
    if err := rows.Err(); err != nil {
        return nil, fmt.Errorf("iterate employee rows: %w", err)
    }
    return employees, nil
}
```

Using `%w` preserves the error chain so callers can still use `errors.As` and `errors.Is` to inspect the underlying error.

## Error handling checklist

| Area | Recommendation |
| --- | --- |
| Type assertion | Declare `var mssqlErr mssql.Error`, then use `errors.As(err, &mssqlErr)` to access SQL Server error fields. |
| Transient detection | Classify errors by number and severity before deciding whether to retry. |
| Retry logic | Use exponential backoff with jitter. Set a maximum attempt count and an overall timeout via context. |
| Deadlocks | Retry the entire transaction, not individual statements. Reduce deadlocks by accessing tables consistently. |
| Pool exhaustion | Monitor `db.Stats()` and set context deadlines on all database calls. |
| ErrNoRows | Handle `sql.ErrNoRows` explicitly for `QueryRowContext`. It's not a server error. |
| Error wrapping | Use `fmt.Errorf` with `%w` to add context while preserving the error chain. |
| Constraint violations | Check for error numbers 2627, 2601 (unique), and 547 (foreign key) to handle conflicts gracefully. |

## Related content

- [Transactions](transactions.md)
- [Connection pooling](connection-pooling.md)
- [Troubleshooting](troubleshooting.md)
- [Performance tuning](performance-tuning.md)
