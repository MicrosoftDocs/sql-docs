---
title: "go-mssqldb Performance Tuning"
description: "Performance tuning guidance for the go-mssqldb driver, including pool configuration, packet size, prepared statements, and bulk copy."
author: dlevy-msft
ms.author: dlevy
ms.date: 04/30/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Performance tuning with go-mssqldb

This article provides guidance on optimizing the performance of Go applications that use the `go-mssqldb` driver with SQL Server.

## Start with the highest-impact changes

Most applications don't need driver-specific tuning on day one. Start with these steps before you change packet size, add prepared statements everywhere, or tune bulk options:

1. Set a bounded connection pool size that matches your server limits.
1. Add context timeouts so blocked SQL calls don't pin connections and make the app appear frozen under load.
1. Fix slow queries, missing indexes, and unnecessary round trips in SQL Server.
1. Benchmark the workload before and after each change.

For many services, connection pool settings and query shape matter more than packet size or statement preparation.

## Connection pool tuning

The `database/sql` connection pool is the most impactful performance lever. Under-provisioned pools cause goroutines to block waiting for connections, while over-provisioned pools waste server resources.

```go
db.SetMaxOpenConns(25)     // Match your workload concurrency
db.SetMaxIdleConns(10)     // Keep warm connections ready
db.SetConnMaxLifetime(5 * time.Minute)  // Recycle connections periodically
db.SetConnMaxIdleTime(1 * time.Minute)  // Close stale idle connections
```

Monitor `db.Stats().WaitCount` and `db.Stats().WaitDuration` to detect pool contention. For more details, see [Connection pooling](connection-pooling.md).

## Increase packet size

The default TDS packet size is 4,096 bytes. For workloads that transfer large result sets or bulk data, increasing the packet size reduces the number of network round trips.  

```text
sqlserver://<user>:<password>@<server>?database=AdventureWorks2025&packet+size=16384
```

Valid range: 512 to 32,767. Values of 8,192 or 16,384 are common for high-throughput scenarios.  

Keep the default packet size unless benchmark data shows that network transfer dominates the workload. For OLTP-style traffic with small queries and rows, larger packets often add complexity without a meaningful gain.

## Use prepared statements

Prepared statements avoid repeated query parsing and plan compilation on the server. Use `db.PrepareContext` when you run the same query many times with different parameters:

```go
stmt, err := db.PrepareContext(ctx,
    "SELECT TOP (1) FirstName + ' ' + LastName AS Name FROM Sales.vSalesPerson WHERE CountryRegionName = @p1")
if err != nil {
    log.Fatal(err)
}
defer stmt.Close()

for _, loc := range locations {
    var name string
    stmt.QueryRowContext(ctx, loc).Scan(&name)
}
```

> [!TIP]
> Close prepared statements by using `defer stmt.Close()` to avoid consuming server-side prepared statement handles.

Don't prepare every statement by default. They help most when the same statement runs repeatedly in a hot path. For one-off queries, `QueryContext` or `ExecContext` is usually more straightforward and fast enough.

## Use bulk copy for large inserts

Individual `INSERT` statements are slow for large data loads. Bulk copy streams data directly to the server, bypassing the query processor:

```go
stmt, err := txn.Prepare(mssql.CopyIn("MyTable", mssql.BulkOptions{
    Tablock:      true,
    RowsPerBatch: 5000,
}, "Col1", "Col2"))
```

For details, see [Bulk operations](bulk-operations.md).

## Use varchar when appropriate

By default, the code sends `string` parameters as `nvarchar` (Unicode). If your columns use `varchar`, the server might perform implicit conversion and skip indexes. Use `mssql.VarChar` to send `varchar` parameters.

```go
db.QueryContext(ctx, "SELECT * FROM Production.Product WHERE ProductNumber = @p1",
    mssql.VarChar("FR-R92B-58"))
```

## Use context timeouts

Set context deadlines on individual queries to prevent blocked SQL calls from pinning connections and stalling callers.

```go
ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
defer cancel()

rows, err := db.QueryContext(ctx, "SELECT * FROM LargeTable")
```

## Close resources promptly

Open `*sql.Rows`, `*sql.Tx`, and `*sql.Conn` objects pin a connection from the pool. Always close them as soon as possible.

```go
rows, err := db.QueryContext(ctx, query)
if err != nil {
    return err
}
defer rows.Close()

for rows.Next() {
    // process
}
return rows.Err()
```

## Reduce round trips

- **Batch queries** into a single call when possible: `"SELECT ...; SELECT ...;"`.
- **Use `OUTPUT` clauses** instead of separate `SELECT SCOPE_IDENTITY()` calls.
- **Use table-valued parameters** to send multiple rows in a single call instead of looping over individual inserts.

## Performance checklist

| Area | Recommendation |
| --- | --- |
| Pool | Set `MaxOpenConns` based on server limits and workload concurrency. |
| Pool | Set `MaxIdleConns` to at least half of `MaxOpenConns`. |
| Network | Change `packet size` only after benchmarking large transfers or bulk loads. |
| Queries | Use prepared statements only for statements that repeat often. |
| Queries | Use `mssql.VarChar` for `varchar` columns to avoid implicit conversions. |
| Large loads | Use bulk copy (`mssql.CopyIn`) for batch inserts. |
| Resources | Close `Rows`, `Tx`, and `Conn` objects promptly. |
| Timeouts | Set context deadlines on all queries and statements. |
| Read-heavy | Use `ApplicationIntent=ReadOnly` for read replicas. |
| Benchmarks | Use `testing.B` to measure before and after optimization. |
| Monitoring | Enable Query Store and use SSMS reports or Query Performance Insight. |
| Monitoring | Export `db.Stats()` to Prometheus or OpenTelemetry. |

## Benchmark database operations

Use Go's `testing.B` to measure the performance of database operations and validate optimization changes:

```go
func BenchmarkInsertSingle(b *testing.B) {
    db := setupDB(b)
    ctx := context.Background()

    b.ResetTimer()
    for i := 0; i < b.N; i++ {
        _, err := db.ExecContext(ctx,
            "INSERT INTO BenchTable (Name) VALUES (@p1)",
            sql.Named("p1", fmt.Sprintf("bench-%d", i)))
        if err != nil {
            b.Fatal(err)
        }
    }
}

func BenchmarkInsertBulk(b *testing.B) {
    db := setupDB(b)
    ctx := context.Background()

    b.ResetTimer()
    for i := 0; i < b.N; i++ {
        txn, err := db.BeginTx(ctx, nil)
        if err != nil {
            b.Fatal(err)
        }
        stmt, err := txn.Prepare(mssql.CopyIn("BenchTable",
            mssql.BulkOptions{}, "Name"))
        if err != nil {
            b.Fatal(err)
        }
        for j := 0; j < 1000; j++ {
            if _, err := stmt.Exec(fmt.Sprintf("bench-%d-%d", i, j)); err != nil {
                b.Fatal(err)
            }
        }
        if _, err := stmt.Exec(); err != nil {
            b.Fatal(err)
        }
        stmt.Close()
        if err := txn.Commit(); err != nil {
            b.Fatal(err)
        }
    }
}
```

Run benchmarks with:

```bash
go test -bench=BenchmarkInsert -benchmem -count=5
```

> [!TIP]
> Use `-count=5` or higher to get statistically meaningful results. Use **[benchstat](https://golang.org/x/perf/cmd/benchstat)** to compare benchmark results before and after a change.

## Use read-only routing

If your SQL Server environment has an availability group with readable secondaries, route read-only queries to the secondary replica by setting `ApplicationIntent=ReadOnly` in the connection string:

```text
sqlserver://<user>:<password>@mylistener?database=AdventureWorks2025&ApplicationIntent=ReadOnly
```

Create separate `*sql.DB` instances for read and write workloads:

```go
writeDB, err := sql.Open("sqlserver",
    "sqlserver://<user>:<password>@mylistener?database=AdventureWorks2025")
if err != nil {
    log.Fatal(err)
}

readDB, err := sql.Open("sqlserver",
    "sqlserver://<user>:<password>@mylistener?database=AdventureWorks2025&ApplicationIntent=ReadOnly")
if err != nil {
    log.Fatal(err)
}

// Use readDB for reports, dashboards, and analytics.
// Use writeDB for inserts, updates, and deletes.
```

## Query plan analysis

Use `SET SHOWPLAN_XML` to retrieve the execution plan for a query without running it. This method helps you identify table scans, missing indexes, and expensive operations:

```go
func getQueryPlan(ctx context.Context, db *sql.DB, query string) (string, error) {
    // Use a dedicated connection so SHOWPLAN mode doesn't affect other queries.
    conn, err := db.Conn(ctx)
    if err != nil {
        return "", err
    }
    defer conn.Close()

    // Enable SHOWPLAN_XML mode.
    _, err = conn.ExecContext(ctx, "SET SHOWPLAN_XML ON")
    if err != nil {
        return "", err
    }

    var planXML string
    err = conn.QueryRowContext(ctx, query).Scan(&planXML)
    if err != nil {
        return "", err
    }

    // Disable SHOWPLAN_XML mode.
    _, _ = conn.ExecContext(ctx, "SET SHOWPLAN_XML OFF")

    return planXML, nil
}
```

> [!WARNING]
> `SET SHOWPLAN_XML ON` affects the entire connection. Always use `db.Conn(ctx)` to isolate SHOWPLAN mode to a dedicated connection.

## Use Query Store to find slow queries

Go benchmarks and client-side timing tell you how long a query takes from your application's perspective, but that number combines network latency, server execution time, and client processing. [Query Store](/sql/relational-databases/performance/monitoring-performance-by-using-the-query-store) captures execution plans and runtime statistics on the server, so you can see exactly how SQL Server executed each query, how often it ran, and how its performance changed over time.

Query Store is especially useful for identifying parameter sniffing, plan regressions, and queries that consume the most server resources. Enable it on your database if it isn't already enabled:

```sql
ALTER DATABASE [AdventureWorks2025] SET QUERY_STORE = ON;
```

Once enabled, you can review performance data in several ways:

- **SQL Server Management Studio (SSMS)**: Expand your database in Object Explorer, open the **Query Store** folder, and use the built-in reports like **Top Resource Consuming Queries**, **Regressed Queries**, and **Overall Resource Consumption**.
- **Azure portal**: For Azure SQL Database, open the **Query Performance Insight** blade to see the top resource-consuming queries without installing any tools.
- **Transact-SQL (T-SQL)**: Query the `sys.query_store_runtime_stats` and `sys.query_store_plan` catalog views directly from your Go application if you need programmatic access.

> [!TIP]
> Query Store persists data across server restarts, so you can analyze performance trends over days or weeks. Use the **Regressed Queries** report to quickly spot queries that got slower after a schema or code change.

## Use the Performance Dashboard

The [Performance Dashboard](/sql/relational-databases/performance/performance-dashboard) is a built-in SSMS report that gives a real-time overview of SQL Server health. Right-click the server instance in SSMS Object Explorer and select **Reports** > **Standard Reports** > **Performance Dashboard**.

The dashboard shows:

- Current waits and bottlenecks.
- Recent expensive queries.
- CPU, I/O, and memory usage trends.
- Active user requests and blocked sessions.

The Performance Dashboard is useful during development and load testing to quickly spot problems without writing any diagnostic queries.

## Monitor server-side metrics from Go

If you need to expose SQL Server performance data to a monitoring system like Prometheus or OpenTelemetry from your Go application, query dynamic management views (DMVs) directly:

### Most expensive queries

Retrieve the top 10 queries ranked by average elapsed time:

```go
rows, err := db.QueryContext(ctx, `
    SELECT TOP 10
        qs.total_elapsed_time / qs.execution_count AS avg_elapsed_us,
        qs.execution_count,
        qs.total_logical_reads / qs.execution_count AS avg_reads,
        SUBSTRING(st.text, (qs.statement_start_offset/2)+1,
            ((CASE qs.statement_end_offset
                WHEN -1 THEN DATALENGTH(st.text)
                ELSE qs.statement_end_offset
            END - qs.statement_start_offset)/2)+1) AS query_text
    FROM sys.dm_exec_query_stats AS qs
    CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) AS st
    ORDER BY avg_elapsed_us DESC`)
```

### Current active requests

List all currently executing requests on the server, excluding your own session:

```go
rows, err := db.QueryContext(ctx, `
    SELECT
        r.session_id,
        r.status,
        r.wait_type,
        r.cpu_time,
        r.logical_reads,
        t.text AS query_text
    FROM sys.dm_exec_requests AS r
    CROSS APPLY sys.dm_exec_sql_text(r.sql_handle) AS t
    WHERE r.session_id != @@SPID`)
```

## Memory-efficient patterns for large results

### Stream rows without accumulating

Process rows one at a time instead of loading the entire result set into a slice:

```go
rows, err := db.QueryContext(ctx, "SELECT Id, Data FROM BigTable")
if err != nil {
    return err
}
defer rows.Close()

for rows.Next() {
    var id int
    var data string
    if err := rows.Scan(&id, &data); err != nil {
        return err
    }
    // Process immediately, don't append to a slice.
    process(id, data)
}
return rows.Err()
```

### Batch processing with keyset pagination

Break large table scans into manageable chunks to limit memory usage and avoid holding a connection for extended periods:

```go
func processBatched(ctx context.Context, db *sql.DB, batchSize int) error {
    var lastID int
    for {
        rows, err := db.QueryContext(ctx, `
            SELECT TOP(@batch) Id, Data FROM BigTable
            WHERE Id > @lastID ORDER BY Id`,
            sql.Named("batch", batchSize),
            sql.Named("lastID", lastID))
        if err != nil {
            return err
        }

        var count int
        for rows.Next() {
            var id int
            var data string
            if err := rows.Scan(&id, &data); err != nil {
                return err
            }
            process(id, data)
            lastID = id
            count++
        }
        if err := rows.Err(); err != nil {
            return err
        }
        rows.Close()

        if count < batchSize {
            break // No more rows.
        }
    }
    return nil
}
```

## Related content

- [Query Store](/sql/relational-databases/performance/monitoring-performance-by-using-the-query-store)
- [Performance Dashboard](/sql/relational-databases/performance/performance-dashboard)
- [Connection pooling with go-mssqldb](connection-pooling.md)
- [Bulk operations with go-mssqldb](bulk-operations.md)
- [Concurrent programming with go-mssqldb](concurrent-programming.md)
- [go-mssqldb data type mappings](data-type-mappings.md)
- [Use go-mssqldb with Azure SQL Database](azure-sql.md)
- [go-mssqldb connection options](connection-options.md)
