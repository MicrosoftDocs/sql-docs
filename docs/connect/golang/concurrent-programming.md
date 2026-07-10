---
title: "go-mssqldb Concurrent Programming"
description: "Use goroutines, worker pools, and parallel queries safely with the go-mssqldb driver for SQL Server."
author: dlevy-msft
ms.author: dlevy
ms.date: 04/25/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Concurrent programming with go-mssqldb

Go applications commonly use goroutines to handle concurrent work. The `go-mssqldb` driver and the `database/sql` package are designed for concurrent use, but you must follow specific patterns to avoid connection leaks, data races, and pool starvation. This article covers goroutine safety, worker pools, and graceful shutdown patterns.

## Goroutine safety

### sql.DB is safe for concurrent use

A `*sql.DB` instance is safe to use from multiple goroutines simultaneously. It manages an internal connection pool and handles synchronization:

```go
// CORRECT: Share a single *sql.DB across all goroutines.
var db *sql.DB

func main() {
    var err error
    db, err = sql.Open("sqlserver", connString)
    if err != nil {
        log.Fatal(err)
    }
    defer db.Close()

    http.HandleFunc("/employees", listEmployees) // Each request runs in its own goroutine.
    log.Fatal(http.ListenAndServe(":8080", nil))
}
```

> [!WARNING]
> Don't create a new `sql.Open` per request or per goroutine. Each `sql.Open` call creates a separate connection pool. Creating pools per request wastes resources and can exhaust server-side connection limits quickly.

### sql.Rows, sql.Tx, and sql.Conn are unsafe for concurrent use

These types represent a single connection and must be used from one goroutine at a time:

```go
// WRONG: Sharing rows across goroutines causes data races.
rows, _ := db.QueryContext(ctx, "SELECT BusinessEntityID, FirstName + ' ' + LastName FROM Sales.vSalesPerson")
go func() { rows.Next() }() // DATA RACE
go func() { rows.Next() }() // DATA RACE

// CORRECT: Process rows in the goroutine that created them.
rows, _ := db.QueryContext(ctx, "SELECT BusinessEntityID, FirstName + ' ' + LastName FROM Sales.vSalesPerson")
defer rows.Close()
for rows.Next() {
    // Process in this goroutine only.
}
```

## Worker pool pattern

When you need to process many items concurrently (for example, updating thousands of records), use a fixed-size worker pool. This approach limits concurrency to prevent pool exhaustion and server overload:

```go
import "sync"

func updateEmployeeLocations(ctx context.Context, db *sql.DB, updates []EmployeeUpdate) error {
    const maxWorkers = 10
    sem := make(chan struct{}, maxWorkers)
    var mu sync.Mutex
    var firstErr error

    var wg sync.WaitGroup
    for _, u := range updates {
        select {
        case <-ctx.Done():
            return ctx.Err()
        case sem <- struct{}{}: // Acquire a worker slot.
        }

        wg.Add(1)
        go func(u EmployeeUpdate) {
            defer wg.Done()
            defer func() { <-sem }() // Release the worker slot.

            _, err := db.ExecContext(ctx,
                "UPDATE HumanResources.Department SET GroupName = @grp WHERE DepartmentID = @id",
                sql.Named("grp", u.GroupName),
                sql.Named("id", u.Id))
            if err != nil {
                mu.Lock()
                if firstErr == nil {
                    firstErr = err
                }
                mu.Unlock()
            }
        }(u)
    }

    wg.Wait()
    return firstErr
}
```

### Use errgroup for worker pools

The `golang.org/x/sync/errgroup` package simplifies worker pools with built-in error propagation and context cancellation:

```go
import "golang.org/x/sync/errgroup"

func updateDepartmentGroups(ctx context.Context, db *sql.DB, updates []DepartmentUpdate) error {
    g, ctx := errgroup.WithContext(ctx)
    g.SetLimit(10) // Maximum concurrent goroutines.

    for _, u := range updates {
        u := u
        g.Go(func() error {
            _, err := db.ExecContext(ctx,
                "UPDATE HumanResources.Department SET GroupName = @grp WHERE DepartmentID = @id",
                sql.Named("grp", u.GroupName),
                sql.Named("id", u.Id))
            return err
        })
    }

    return g.Wait()
}
```

> [!TIP]
> Set the errgroup limit to a value lower than `MaxOpenConns`. If the worker count equals the pool size, the workers consume all connections and leave no room for health checks or other queries.

## Parallel queries

Run independent queries concurrently to reduce total latency:

```go
func getDashboardData(ctx context.Context, db *sql.DB) (*Dashboard, error) {
    g, ctx := errgroup.WithContext(ctx)

    var orderCount int
    var customerCount int
    var revenue float64

    g.Go(func() error {
        return db.QueryRowContext(ctx,
            "SELECT COUNT(*) FROM Sales.SalesOrderHeader WHERE OrderDate >= DATEADD(day, -7, GETUTCDATE())").
            Scan(&orderCount)
    })

    g.Go(func() error {
        return db.QueryRowContext(ctx,
            "SELECT COUNT(DISTINCT CustomerID) FROM Sales.SalesOrderHeader WHERE OrderDate >= DATEADD(day, -7, GETUTCDATE())").
            Scan(&customerCount)
    })

    g.Go(func() error {
        return db.QueryRowContext(ctx,
            "SELECT ISNULL(SUM(TotalDue), 0) FROM Sales.SalesOrderHeader WHERE OrderDate >= DATEADD(day, -7, GETUTCDATE())").
            Scan(&revenue)
    })

    if err := g.Wait(); err != nil {
        return nil, err
    }

    return &Dashboard{
        OrderCount:    orderCount,
        CustomerCount: customerCount,
        WeeklyRevenue: revenue,
    }, nil
}
```

## Batch processing with controlled concurrency

For large batch operations (importing data, syncing records), combine batching with concurrency to maximize throughput:

```go
func importRecords(ctx context.Context, db *sql.DB, records []Record) error {
    const batchSize = 100
    const maxWorkers = 5

    g, ctx := errgroup.WithContext(ctx)
    g.SetLimit(maxWorkers)

    for i := 0; i < len(records); i += batchSize {
        end := i + batchSize
        if end > len(records) {
            end = len(records)
        }
        batch := records[i:end]

        g.Go(func() error {
            return insertBatch(ctx, db, batch)
        })
    }

    return g.Wait()
}

func insertBatch(ctx context.Context, db *sql.DB, batch []Record) error {
    tx, err := db.BeginTx(ctx, nil)
    if err != nil {
        return err
    }
    defer tx.Rollback()

    stmt, err := tx.Prepare(mssql.CopyIn("Production.ScrapReason", mssql.BulkOptions{}, "Name"))
    if err != nil {
        return err
    }

    for _, r := range batch {
        if _, err := stmt.Exec(r.Name); err != nil {
            return err
        }
    }

    if _, err := stmt.Exec(); err != nil {
        return err
    }
    if err := stmt.Close(); err != nil {
        return err
    }

    return tx.Commit()
}
```

## Graceful shutdown

When your application receives a shutdown signal, drain active database operations before closing the pool. Abruptly calling `db.Close()` cancels in-flight queries and can leave the server with orphaned sessions.

```go
import (
    "context"
    "database/sql"
    "log"
    "net/http"
    "os"
    "os/signal"
    "syscall"
    "time"
)

func main() {
    db, err := sql.Open("sqlserver", connString)
    if err != nil {
        log.Fatal(err)
    }

    srv := &http.Server{Addr: ":8080"}

    // Run the server in a goroutine.
    go func() {
        if err := srv.ListenAndServe(); err != http.ErrServerClosed {
            log.Fatalf("HTTP server error: %v", err)
        }
    }()

    // Wait for interrupt signal.
    quit := make(chan os.Signal, 1)
    signal.Notify(quit, syscall.SIGINT, syscall.SIGTERM)
    <-quit
    log.Println("Shutting down...")

    // Give in-flight HTTP requests up to 30 seconds to complete.
    shutdownCtx, cancel := context.WithTimeout(context.Background(), 30*time.Second)
    defer cancel()
    if err := srv.Shutdown(shutdownCtx); err != nil {
        log.Printf("HTTP shutdown error: %v", err)
    }

    // Close the database pool after HTTP handlers have drained.
    // This waits for any remaining connections to be returned.
    if err := db.Close(); err != nil {
        log.Printf("Database close error: %v", err)
    }

    log.Println("Shutdown complete.")
}
```

> [!IMPORTANT]
> Close the database pool _after_ your HTTP server (or other request router) finishes draining. If you close the pool first, in-flight handlers get broken connection errors.

## Connection pool sizing for concurrent workloads

Size the connection pool based on the expected concurrency of your application, not on the total number of goroutines:

| Application type | Recommended `MaxOpenConns` | Rationale |
| --- | --- | --- |
| HTTP API, low concurrency | 10-25 | Matches typical concurrent request count. |
| HTTP API, high concurrency | 25-50 | More connections for parallel handlers. |
| Background worker (batch) | 5-10 per worker pool | Each worker needs its own connection. |
| Mixed (API + background jobs) | Sum of API + worker needs | Ensure each subsystem has enough headroom. |

```go
db.SetMaxOpenConns(25)    // Total connections across all goroutines.
db.SetMaxIdleConns(10)    // Keep warm connections ready for bursts.
db.SetConnMaxLifetime(5 * time.Minute) // Rotate connections for load balancer compatibility.
```

> [!TIP]
> Always set `MaxOpenConns`. The default (`0`) is unlimited. An unlimited pool under load can open hundreds of connections and overwhelm the server, especially on Azure SQL where connection limits are tier-dependent.

## Avoid common concurrency mistakes

### Don't share *sql.Rows across goroutines

Passing a `*sql.Rows` value to multiple goroutines causes data races:

```go
// WRONG: rows is consumed by two goroutines.
rows, _ := db.QueryContext(ctx, "SELECT BusinessEntityID, FirstName + ' ' + LastName FROM Sales.vSalesPerson")
go processRows(rows)
go processRows(rows) // Race condition.
```

### Don't forget to close rows in loops

Leaking `*sql.Rows` inside a loop exhausts the connection pool:

```go
// WRONG: Rows leak when the loop starts a new iteration.
for _, location := range locations {
    rows, _ := db.QueryContext(ctx,
        "SELECT FirstName + ' ' + LastName FROM Sales.vSalesPerson WHERE CountryRegionName = @p1",
        sql.Named("p1", location))
    for rows.Next() {
        // Process...
    }
    // rows.Close() never called if an error occurs.
}

// CORRECT: Use a helper function with defer.
for _, location := range locations {
    if err := processLocation(ctx, db, location); err != nil {
        return err
    }
}

func processLocation(ctx context.Context, db *sql.DB, location string) error {
    rows, err := db.QueryContext(ctx,
        "SELECT FirstName + ' ' + LastName FROM Sales.vSalesPerson WHERE CountryRegionName = @p1",
        sql.Named("p1", location))
    if err != nil {
        return err
    }
    defer rows.Close() // Guaranteed cleanup.
    for rows.Next() {
        // Process...
    }
    return rows.Err()
}
```

### Don't use db.Conn unless you need connection affinity

`db.Conn(ctx)` pins a specific connection. If you use it unnecessarily, you reduce the effective pool size:

```go
// WRONG: Unnecessary pinning.
conn, _ := db.Conn(ctx)
defer conn.Close()
conn.QueryContext(ctx, "SELECT 1") // Use db.QueryContext instead.

// CORRECT: Use db.Conn only for temp tables or session-scoped state.
conn, _ := db.Conn(ctx)
defer conn.Close()
conn.ExecContext(ctx, "CREATE TABLE #Temp (Id INT)")
conn.ExecContext(ctx, "INSERT INTO #Temp VALUES (1)")
conn.QueryContext(ctx, "SELECT * FROM #Temp")
```

## Concurrency checklist

| Area | Recommendation |
| --- | --- |
| Pool sharing | Create one `*sql.DB` instance for the entire application. |
| Goroutine safety | Don't share `*sql.Rows`, `*sql.Tx`, or `*sql.Conn` across goroutines. |
| Worker pools | Use `errgroup.SetLimit` or a semaphore channel to control concurrency. |
| Pool sizing | Set `MaxOpenConns` lower than the server connection limit. |
| Graceful shutdown | Drain HTTP handlers before closing the database pool. |
| Resource cleanup | Always `defer rows.Close()` and `defer tx.Rollback()` in the goroutine that created them. |
| Parallel queries | Run independent queries concurrently to reduce latency for aggregation pages. |

## Related content

- [Connection pooling](connection-pooling.md)
- [Error handling and retry patterns](error-handling.md)
- [Transactions](transactions.md)
- [Performance tuning](performance-tuning.md)
