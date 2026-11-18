---
title: "queryinsights.sql_pool_insights (Transact-SQL)"
description: The queryinsights.sql_pool_insights in Microsoft Fabric provides actionable insights into the configuration, pressure, and vnode utilization of SQL pools.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mariyaali
ms.date: 11/18/2026
ms.service: sql
ms.topic: reference
f1_keywords:
  - "queryinsights.sql_pool_insights"
  - "queryinsights.sql_pool_insights_TSQL"
helpviewer_keywords:
  - "queryinsights.sql_pool_insights system view"
  - "queryinsights.sql_pool_insights"
  - "query insights sql_pool_insights"
  - "SQL pools"
dev_langs:
  - TSQL
monikerRange: "=fabric"
---
# queryinsights.sql_pool_insights (Transact-SQL)

[!INCLUDE [Fabric SE DW](../../includes/applies-to-version/fabric-se-dw.md)]

  The `queryinsights.sql_pool_insights` in [!INCLUDE [fabric](../../includes/fabric.md)] Data Warehouse monitors resource allocation, tracks configuration changes, identifies periods when pools are under pressure, and shows how many virtual nodes (vnodes) each pool used compared to its limit.

| Column name                | Data type        | Description                                                                 |
| ---                        | ---              | ---                                                                         |
| `sql_pool_name` | **nvarchar(128)**| Name of the SQL pool.                                                       |
| `timestamp` | **datetime2**    | Timestamp when the health check or capacity change took place.      |
| `max_resource_percentage` | **int**          | Maximum resource percentage allocated to the pool.                          |
| `is_optimized_for_reads` | **bit**          | Indicates if the pool is configured for read-optimized workloads.            |
| `current_workspace_capacity` | **nvarchar(16)**| Capacity currently used by the workspace.                               |
| `is_pool_under_pressure` | **bit**          | Indicates if the pool is under pressure.                                    |
| `vnodes_used` | **int** | Highest number of vnodes the pool used during the reporting interval, across all warehouses and SQL analytics endpoints in the workspace that use the pool. `NULL` when vnode data isn't available for the interval. |
| `max_vnodes_available` | **int** | Maximum number of vnodes the pool can use, based on the workspace capacity and `max_resource_percentage`. `NULL` when vnode data isn't available. |
| `includes_cache_vnodes` | **bit** | `1` when `vnodes_used` includes vnodes kept warm for caching. |

## Remarks

In Fabric Data Warehouse, resource isolation is enforced between `SELECT` and `NON SELECT` pools, preventing contention. Two pools are present by default:

  - `SELECT`: Handles read (`SELECT`) queries, optimized for analytics and reporting.
  - `NON SELECT`: Handles data modification (`INSERT`, `UPDATE`, `DELETE`), optimized for ETL and ingestion.

### Event-based reporting

- The database logs new records when pool configuration, workspace capacity, or pressure state changes.
- The database logs pressure state changes if the pressure is sustained for **1 minute** or longer.
- The SQL Database Engine logs events only when the warehouse is active. If there's no activity on the warehouse, events pause and resume once activity is detected. This condition means that during periods of inactivity, event logging can have gaps until the warehouse becomes active again.

## Permissions

You need access to a SQL analytics endpoint or warehouse within a Fabric Capacity workspace with Contributor or higher permissions, or Viewer with Monitor permissions.

## Examples

Use this view to correlate query performance problems with pool pressure and configuration changes. Visualize periods of pressure by using window functions or external tools. Some examples follow:

### A. Periods of pressure in the last 24 hours

Show periods when the `SELECT` pool was under pressure in the last 24 hours:

```sql
-- Show periods when the SELECT pool was under pressure in the last 24 hours
SELECT sql_pool_name, timestamp, is_pool_under_pressure
FROM queryinsights.sql_pool_insights
WHERE sql_pool_name = 'SELECT'
  AND timestamp >= DATEADD(hour, -24, GETDATE())
  AND is_pool_under_pressure = 1
ORDER BY timestamp DESC;
```

### B. Visualize pressure trends

Calculate consecutive pressure periods and gaps by using window functions:

```sql
-- Calculate consecutive pressure periods and gaps using window functions
SELECT sql_pool_name,
       timestamp,
       is_pool_under_pressure,
       LAG(timestamp) OVER (PARTITION BY sql_pool_name ORDER BY timestamp) AS previous_event,
       DATEDIFF(minute, LAG(timestamp) OVER (PARTITION BY sql_pool_name ORDER BY timestamp), timestamp) AS minutes_since_last_event
FROM queryinsights.sql_pool_insights
WHERE sql_pool_name = 'SELECT'
ORDER BY timestamp;
```

### C. Calculate vnode utilization

Show how close each pool came to its vnode limit in the last 24 hours:

```sql
SELECT
    sql_pool_name,
    [timestamp],
    vnodes_used,
    max_vnodes_available,
    CAST(100.0 * vnodes_used / NULLIF(max_vnodes_available, 0) AS decimal(5,1)) AS utilization_pct,
    includes_cache_vnodes,
    is_pool_under_pressure
FROM queryinsights.sql_pool_insights
WHERE vnodes_used IS NOT NULL
  AND [timestamp] >= DATEADD(DAY, -1, SYSUTCDATETIME())
ORDER BY sql_pool_name, [timestamp];
```

### D. Find queries that ran during high utilization

Match pool rows to queries by time. This shows which queries ran while the pool was busy. It doesn't show how many vnodes each query used.

```sql
WITH busy AS (
    SELECT sql_pool_name, [timestamp]
    FROM queryinsights.sql_pool_insights
    WHERE vnodes_used >= 0.9 * max_vnodes_available
      AND [timestamp] >= DATEADD(DAY, -1, SYSUTCDATETIME())
)
SELECT DISTINCT
    b.sql_pool_name,
    b.[timestamp] AS busy_at,
    r.distributed_statement_id,
    r.start_time,
    r.end_time,
    r.total_elapsed_time_ms,
    r.login_name,
    LEFT(r.command, 200) AS command_text
FROM busy AS b
JOIN queryinsights.exec_requests_history AS r
    ON r.sql_pool_name = b.sql_pool_name
   AND b.[timestamp] BETWEEN r.start_time AND r.end_time
ORDER BY b.[timestamp] DESC, r.total_elapsed_time_ms DESC;
```

## Next step

> [!div class="nextstepaction"]
> [Query insights in Microsoft Fabric](/fabric/data-warehouse/query-insights)

## Related content

- [Monitor connections, sessions, and requests using DMVs](/fabric/data-warehouse/monitor-using-dmv)
- [queryinsights.exec_requests_history (Transact-SQL)](queryinsights-exec-requests-history-transact-sql.md)
- [queryinsights.exec_sessions_history (Transact-SQL)](queryinsights-exec-sessions-history-transact-sql.md)
- [queryinsights.long_running_queries (Transact-SQL)](queryinsights-long-running-queries-transact-sql.md)
- [queryinsights.frequently_run_queries (Transact-SQL)](queryinsights-frequently-run-queries-transact-sql.md)
