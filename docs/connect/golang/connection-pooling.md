---
title: "go-mssqldb Connection Pooling"
description: "Configure the database/sql connection pool for the go-mssqldb driver."
author: dlevy-msft
ms.author: dlevy
ms.date: 04/30/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Connection pooling with go-mssqldb

The `go-mssqldb` driver uses the built-in connection pool provided by Go's `database/sql` package. Every `sql.DB` instance maintains a pool of idle connections that are reused automatically. This article explains how to configure the pool for your workload.

## How the pool works

When you call `db.QueryContext`, `db.ExecContext`, or any other database method:

1. The pool attempts to find an idle connection.
1. If no idle connection is available and the pool hasn't reached its maximum size, a new connection is created.
1. If the pool is at maximum capacity, the call blocks until a connection becomes available.
1. After the operation completes, the connection is returned to the pool.

## Pool configuration methods

Configure the pool using methods on `*sql.DB`:

| Method | Description |
| --- | --- |
| `db.SetMaxOpenConns(n)` | Maximum number of open connections (in use + idle). Default: `0` (unlimited). |
| `db.SetMaxIdleConns(n)` | Maximum number of idle connections in the pool. Default: `2`. |
| `db.SetConnMaxLifetime(d)` | Maximum total time a connection can be reused. Default: `0` (no limit). |
| `db.SetConnMaxIdleTime(d)` | Maximum time a connection can sit idle before it's closed. Default: `0` (no limit). |

### Example

Configure the pool immediately after opening the database:

```go
db, err := sql.Open("sqlserver", connString)
if err != nil {
    log.Fatal(err)
}

db.SetMaxOpenConns(25)
db.SetMaxIdleConns(10)
db.SetConnMaxLifetime(5 * time.Minute)
db.SetConnMaxIdleTime(1 * time.Minute)
```

Treat these values as a starting point, not a universal default. For many services, the first useful step is to set bounded `MaxOpenConns` and `MaxIdleConns`, then add lifetime and idle limits only when your deployment path can leave you with stale or unevenly distributed connections.

## Recommended settings

| Scenario | MaxOpen | MaxIdle | MaxLifetime | MaxIdleTime |
| --- | --- | --- | --- | --- |
| Web application on a stable SQL Server network path | 25 | 10 | 0 | 0 |
| Web application through Azure SQL, a gateway, or a load balancer | 25 | 10 | 5 minutes | 1 minute |
| High-throughput service | 50-100 | 25 | 5 minutes | 30 seconds |
| Background job / CLI tool | 5 | 2 | 0 | 0 |
| Azure SQL Database (Basic/Standard) | 10-20 | 5 | 5 minutes | 1 minute |

> [!TIP]
> Set `MaxOpenConns` below the connection limit of your SQL Server instance or Azure SQL tier. Exceeding the server's maximum concurrent connections causes login failures for all clients.

Short `ConnMaxLifetime` and `ConnMaxIdleTime` values reduce the chance of stale connections after failover or gateway recycling, but they also increase connection churn. If your app connects directly to a stable SQL Server instance and you aren't seeing stale-connection failures, leaving both values at `0` is reasonable.

## Monitor pool statistics

Use `db.Stats()` to read current pool statistics:

```go
stats := db.Stats()
fmt.Printf("Open: %d, InUse: %d, Idle: %d\n",
    stats.OpenConnections, stats.InUse, stats.Idle)
fmt.Printf("WaitCount: %d, WaitDuration: %v\n",
    stats.WaitCount, stats.WaitDuration)
```

Key fields:

| Field | Description |
| --- | --- |
| `OpenConnections` | Total open connections (in use + idle). |
| `InUse` | Connections currently checked out by callers. |
| `Idle` | Connections waiting in the pool. |
| `WaitCount` | Total number of times a caller had to wait for a connection. |
| `WaitDuration` | Total cumulative wait time. |

If `WaitCount` is growing steadily, don't increase `MaxOpenConns` automatically. First verify that rows, transactions, and dedicated connections are being closed promptly, and confirm that the server can support a larger pool.

## SessionInitSQL

Use `SessionInitSQL` to run a SQL statement on every new connection as it enters the pool. This feature is useful for setting session-level options:

```go
import (
    "database/sql"
    "github.com/microsoft/go-mssqldb"
    "github.com/microsoft/go-mssqldb/msdsn"
)

config := msdsn.Config{
    Host:     "<server>",
    Port:     1433,
    Database: "AdventureWorks2025",
}

connector := mssql.NewConnectorConfig(config)
connector.SessionInitSQL = "SET ANSI_NULLS ON; SET QUOTED_IDENTIFIER ON"
db := sql.OpenDB(connector)
```

## Connection pinning

Certain operations pin a connection so it isn't returned to the pool until the operation ends:

- **Transactions** (`db.BeginTx`) - The connection is pinned until `Commit()` or `Rollback()` is called.
- **Single connections** (`db.Conn`) - The connection is pinned until `conn.Close()` is called.
- **Open rows** (`db.QueryContext`) - The connection is pinned until `rows.Close()` is called.

Always close these resources promptly to avoid starving the pool.

## Detect and resolve pool exhaustion

Pool exhaustion occurs when all connections are in use and the pool has reached `MaxOpenConns`. New callers block until a connection is returned. Symptoms include high latency, goroutine buildup, and eventual context deadline errors.

### Monitor for exhaustion

Poll `db.Stats()` periodically and alert when contention is detected:

```go
func monitorPool(ctx context.Context, db *sql.DB, interval time.Duration) {
    ticker := time.NewTicker(interval)
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
                log.Printf("POOL CONTENTION: %d new waits, avg wait %v, open=%d, inUse=%d, idle=%d",
                    newWaits, stats.WaitDuration/time.Duration(stats.WaitCount),
                    stats.OpenConnections, stats.InUse, stats.Idle)
            }
        }
    }
}
```

### Common causes and solutions

| Cause | Symptom | Solution |
| --- | --- | --- |
| `MaxOpenConns` too low for workload | `WaitCount` grows steadily. | Increase `MaxOpenConns`. |
| Rows not closed in error paths | `InUse` grows, `Idle` stays at 0. | Use `defer rows.Close()` immediately after `QueryContext`. |
| Long-running transactions | `InUse` stays high. | Keep transactions short. Use context timeouts. |
| `db.Conn` used unnecessarily | `InUse` higher than expected. | Only use `db.Conn` when you need session-scoped state (temp tables). |
| `MaxOpenConns` not set (unlimited) | Hundreds of open connections under load. | Always set `MaxOpenConns` to a bounded value. |

## Health checks and stale connection detection

The pool doesn't actively validate idle connections. A connection that sat idle while the server recycled it fails on the next use. Configure `ConnMaxLifetime` and `ConnMaxIdleTime` to rotate connections before they become stale:

```go
// Rotate connections every 5 minutes to stay compatible
// with load balancers and Azure SQL failover.
db.SetConnMaxLifetime(5 * time.Minute)

// Close connections that have been idle for over 1 minute
// to reduce the number of stale connections.
db.SetConnMaxIdleTime(1 * time.Minute)
```

> [!NOTE]
> When `ConnMaxIdleTime` closes idle connections proactively, SQL Server logs might show connections being dropped. This is expected behavior, not a connection leak. If your DBA reports unexpected connection closures, verify that the `ConnMaxIdleTime` setting aligns with the team's monitoring expectations.

If your application connects through a load balancer or Azure SQL with geo-replication, set `ConnMaxLifetime` to 5 minutes or less. This setting ensures connections are redistributed across replicas after a failover.

### Validate connectivity on startup

Always call `db.PingContext` after opening the database to confirm the connection string is correct and the server is reachable:

```go
db, err := sql.Open("sqlserver", connString)
if err != nil {
    log.Fatal(err)
}

ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
defer cancel()
if err := db.PingContext(ctx); err != nil {
    log.Fatalf("Cannot connect to database: %v", err)
}
```

## Export pool metrics

Expose pool statistics to your monitoring system by periodically reading `db.Stats()`:

### Prometheus example

Register gauges that track pool statistics and update them periodically:

```go
import "github.com/prometheus/client_golang/prometheus"

var (
    dbOpenConns = prometheus.NewGauge(prometheus.GaugeOpts{
        Name: "db_open_connections",
        Help: "Number of open database connections.",
    })
    dbInUseConns = prometheus.NewGauge(prometheus.GaugeOpts{
        Name: "db_in_use_connections",
        Help: "Number of connections currently in use.",
    })
    dbWaitCount = prometheus.NewCounter(prometheus.CounterOpts{
        Name: "db_wait_count_total",
        Help: "Total number of times a caller waited for a connection.",
    })
    dbWaitDuration = prometheus.NewCounter(prometheus.CounterOpts{
        Name: "db_wait_duration_seconds_total",
        Help: "Total wait time for a connection.",
    })
)

func init() {
    prometheus.MustRegister(dbOpenConns, dbInUseConns, dbWaitCount, dbWaitDuration)
}

func recordPoolMetrics(ctx context.Context, db *sql.DB) {
    ticker := time.NewTicker(10 * time.Second)
    defer ticker.Stop()

    var lastWaitCount int64
    var lastWaitDuration time.Duration
    for {
        select {
        case <-ctx.Done():
            return
        case <-ticker.C:
            stats := db.Stats()
            dbOpenConns.Set(float64(stats.OpenConnections))
            dbInUseConns.Set(float64(stats.InUse))
            dbWaitCount.Add(float64(stats.WaitCount - lastWaitCount))
            dbWaitDuration.Add((stats.WaitDuration - lastWaitDuration).Seconds())
            lastWaitCount = stats.WaitCount
            lastWaitDuration = stats.WaitDuration
        }
    }
}
```

## Pool configuration checklist

| Area | Recommendation |
| --- | --- |
| `MaxOpenConns` | Always set to a bounded value. Match it to your workload concurrency, below the server's connection limit. |
| `MaxIdleConns` | Set to at least half of `MaxOpenConns`. Too few idle connections cause frequent reconnection overhead. |
| `ConnMaxLifetime` | Set to 5 minutes for Azure SQL or load-balanced environments. Prevents stale connection buildup. |
| `ConnMaxIdleTime` | Set to 30-60 seconds to close connections that are no longer needed. |
| Monitoring | Poll `db.Stats()` and alert on `WaitCount` growth. |
| Resource cleanup | Always `defer rows.Close()`, `defer tx.Rollback()`, and `defer conn.Close()`. |
| Startup validation | Call `db.PingContext` after `sql.Open` to verify connectivity. |

## Related content

- [Performance tuning](performance-tuning.md)
- [Concurrent programming](concurrent-programming.md)
- [Connection options](connection-options.md)
- [Azure SQL Database](azure-sql.md)
- [Troubleshooting](troubleshooting.md)
