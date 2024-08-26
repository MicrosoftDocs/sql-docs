---
title: "Tune performance with the Query Store"
description: The Query Store can be used to discover and tune query performance in all SQL Server and Azure SQL platforms.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 06/07/2024
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords:
  - "Query Store performance tuning"
  - "Query Store, performance tuning"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =azure-sqldw-latest"
---
# Tune performance with the Query Store

[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Query Store feature provides you with power to discover and tune queries in your workload through the [!INCLUDE [ssmanstudiofull](../../includes/ssmanstudiofull-md.md)] visual interface and through T-SQL queries. This article details how you can take actionable information to improve query performance in your database, including how to identify queries based on their usage statistics and forcing plans. You can also use the [Query Store hints](query-store-hints.md) feature to identify queries and shape their query plans without changing application code.

- For more information on how this data is collected, see [How Query Store collects data](how-query-store-collects-data.md).
- For more information on configuring and administering with the Query Store, see [Monitoring performance by using the Query Store](monitoring-performance-by-using-the-query-store.md).
- For information about operating the Query Store in Azure [!INCLUDE [ssSDS](../../includes/sssds-md.md)], see [Operating the Query Store in Azure SQL Database](best-practice-with-the-query-store.md#Insight).

## Performance tuning sample queries

Query Store keeps a history of compilation and runtime metrics throughout query executions, allowing you to ask questions about your workload.

The following sample queries can be helpful in your performance baseline and query performance investigation:

#### Last queries executed on the database

The last *n* queries executed on the database within the last hour:

```sql
SELECT TOP 10 qt.query_sql_text,
    q.query_id,
    qt.query_text_id,
    p.plan_id,
    rs.last_execution_time
FROM sys.query_store_query_text AS qt
INNER JOIN sys.query_store_query AS q
    ON qt.query_text_id = q.query_text_id
INNER JOIN sys.query_store_plan AS p
    ON q.query_id = p.query_id
INNER JOIN sys.query_store_runtime_stats AS rs
    ON p.plan_id = rs.plan_id
WHERE rs.last_execution_time > DATEADD(HOUR, -1, GETUTCDATE())
ORDER BY rs.last_execution_time DESC;
```

#### Execution counts

Number of executions for each query within the last hour:

```sql
SELECT q.query_id,
    qt.query_text_id,
    qt.query_sql_text,
    SUM(rs.count_executions) AS total_execution_count
FROM sys.query_store_query_text AS qt
INNER JOIN sys.query_store_query AS q
    ON qt.query_text_id = q.query_text_id
INNER JOIN sys.query_store_plan AS p
    ON q.query_id = p.query_id
INNER JOIN sys.query_store_runtime_stats AS rs
    ON p.plan_id = rs.plan_id
WHERE rs.last_execution_time > DATEADD(HOUR, -1, GETUTCDATE())
GROUP BY q.query_id,
    qt.query_text_id,
    qt.query_sql_text
ORDER BY total_execution_count DESC;
```

#### Longest average execution time

The number of queries with the highest average duration within last hour:

```sql
SELECT TOP 10 ROUND(CONVERT(FLOAT, SUM(rs.avg_duration * rs.count_executions)) /
        NULLIF(SUM(rs.count_executions), 0), 2) avg_duration,
    SUM(rs.count_executions) AS total_execution_count,
    qt.query_sql_text,
    q.query_id,
    qt.query_text_id,
    p.plan_id,
    GETUTCDATE() AS CurrentUTCTime,
    MAX(rs.last_execution_time) AS last_execution_time
FROM sys.query_store_query_text AS qt
INNER JOIN sys.query_store_query AS q
    ON qt.query_text_id = q.query_text_id
INNER JOIN sys.query_store_plan AS p
    ON q.query_id = p.query_id
INNER JOIN sys.query_store_runtime_stats AS rs
    ON p.plan_id = rs.plan_id
WHERE rs.last_execution_time > DATEADD(HOUR, -1, GETUTCDATE())
GROUP BY qt.query_sql_text,
    q.query_id,
    qt.query_text_id,
    p.plan_id
ORDER BY avg_duration DESC;
```

#### Highest average physical I/O reads

The number of queries that had the biggest average physical I/O reads in last 24 hours, with corresponding average row count and execution count:

```sql
SELECT TOP 10 rs.avg_physical_io_reads,
    qt.query_sql_text,
    q.query_id,
    qt.query_text_id,
    p.plan_id,
    rs.runtime_stats_id,
    rsi.start_time,
    rsi.end_time,
    rs.avg_rowcount,
    rs.count_executions
FROM sys.query_store_query_text AS qt
INNER JOIN sys.query_store_query AS q
    ON qt.query_text_id = q.query_text_id
INNER JOIN sys.query_store_plan AS p
    ON q.query_id = p.query_id
INNER JOIN sys.query_store_runtime_stats AS rs
    ON p.plan_id = rs.plan_id
INNER JOIN sys.query_store_runtime_stats_interval AS rsi
    ON rsi.runtime_stats_interval_id = rs.runtime_stats_interval_id
WHERE rsi.start_time >= DATEADD(hour, -24, GETUTCDATE())
ORDER BY rs.avg_physical_io_reads DESC;
```

#### Queries with multiple plans

Queries with more than one plan are especially interesting, because they can be candidates for a regression in performance due to a change in plan choice.

The following query identifies the queries with the highest number of plans within the last hour:

```sql
SELECT q.query_id,
    object_name(object_id) AS ContainingObject,
    COUNT(*) AS QueryPlanCount,
    STRING_AGG(p.plan_id, ',') plan_ids,
    qt.query_sql_text
FROM sys.query_store_query_text AS qt
INNER JOIN sys.query_store_query AS q
    ON qt.query_text_id = q.query_text_id
INNER JOIN sys.query_store_plan AS p
    ON p.query_id = q.query_id
INNER JOIN sys.query_store_runtime_stats AS rs
    ON p.plan_id = rs.plan_id
WHERE rs.last_execution_time > DATEADD(HOUR, -1, GETUTCDATE())
GROUP BY OBJECT_NAME(object_id),
    q.query_id,
    qt.query_sql_text
HAVING COUNT(DISTINCT p.plan_id) > 1
ORDER BY QueryPlanCount DESC;
```

The following query identifies these queries along with all plans within the last hour:

```sql
WITH Query_MultPlans
AS (
    SELECT COUNT(*) AS QueryPlanCount,
        q.query_id
    FROM sys.query_store_query_text AS qt
    INNER JOIN sys.query_store_query AS q
        ON qt.query_text_id = q.query_text_id
    INNER JOIN sys.query_store_plan AS p
        ON p.query_id = q.query_id
    GROUP BY q.query_id
    HAVING COUNT(DISTINCT plan_id) > 1
)
SELECT q.query_id,
    object_name(object_id) AS ContainingObject,
    query_sql_text,
    p.plan_id,
    p.query_plan AS plan_xml,
    p.last_compile_start_time,
    p.last_execution_time
FROM Query_MultPlans AS qm
INNER JOIN sys.query_store_query AS q
    ON qm.query_id = q.query_id
INNER JOIN sys.query_store_plan AS p
    ON q.query_id = p.query_id
INNER JOIN sys.query_store_query_text qt
    ON qt.query_text_id = q.query_text_id
INNER JOIN sys.query_store_runtime_stats AS rs
    ON p.plan_id = rs.plan_id
WHERE rs.last_execution_time > DATEADD(HOUR, -1, GETUTCDATE())
ORDER BY q.query_id,
    p.plan_id;
```

#### Highest wait durations

This query returns the top 10 queries with the highest wait durations for the last hour:

```sql
SELECT TOP 10 qt.query_text_id,
    q.query_id,
    p.plan_id,
    sum(total_query_wait_time_ms) AS sum_total_wait_ms
FROM sys.query_store_wait_stats ws
INNER JOIN sys.query_store_plan p
    ON ws.plan_id = p.plan_id
INNER JOIN sys.query_store_query q
    ON p.query_id = q.query_id
INNER JOIN sys.query_store_query_text qt
    ON q.query_text_id = qt.query_text_id
INNER JOIN sys.query_store_runtime_stats AS rs
    ON p.plan_id = rs.plan_id
WHERE rs.last_execution_time > DATEADD(HOUR, -1, GETUTCDATE())
GROUP BY qt.query_text_id,
    q.query_id,
    p.plan_id
ORDER BY sum_total_wait_ms DESC;
```

> [!NOTE]  
> In Azure Synapse Analytics, the Query Store sample queries in this section are supported with the exception of wait stats, which aren't available in the Azure Synapse Analytics Query Store DMVs.

#### Queries that recently regressed in performance

The following query example returns all queries for which execution time doubled in the last 48 hours due to a plan choice change. This query compares all runtime stat intervals side by side:

```sql
SELECT qt.query_sql_text,
    q.query_id,
    qt.query_text_id,
    rs1.runtime_stats_id AS runtime_stats_id_1,
    rsi1.start_time AS interval_1,
    p1.plan_id AS plan_1,
    rs1.avg_duration AS avg_duration_1,
    rs2.avg_duration AS avg_duration_2,
    p2.plan_id AS plan_2,
    rsi2.start_time AS interval_2,
    rs2.runtime_stats_id AS runtime_stats_id_2
FROM sys.query_store_query_text AS qt
INNER JOIN sys.query_store_query AS q
    ON qt.query_text_id = q.query_text_id
INNER JOIN sys.query_store_plan AS p1
    ON q.query_id = p1.query_id
INNER JOIN sys.query_store_runtime_stats AS rs1
    ON p1.plan_id = rs1.plan_id
INNER JOIN sys.query_store_runtime_stats_interval AS rsi1
    ON rsi1.runtime_stats_interval_id = rs1.runtime_stats_interval_id
INNER JOIN sys.query_store_plan AS p2
    ON q.query_id = p2.query_id
INNER JOIN sys.query_store_runtime_stats AS rs2
    ON p2.plan_id = rs2.plan_id
INNER JOIN sys.query_store_runtime_stats_interval AS rsi2
    ON rsi2.runtime_stats_interval_id = rs2.runtime_stats_interval_id
WHERE rsi1.start_time > DATEADD(hour, -48, GETUTCDATE())
    AND rsi2.start_time > rsi1.start_time
    AND p1.plan_id <> p2.plan_id
    AND rs2.avg_duration > 2 * rs1.avg_duration
ORDER BY q.query_id,
    rsi1.start_time,
    rsi2.start_time;
```

If you want to see all performance regressions (not only regressions related to plan choice change), remove condition `AND p1.plan_id <> p2.plan_id` from the previous query.

#### Queries with historical regression in performance

When you want to compare recent execution to historical execution, the following query compares query execution based on period of execution. In this particular example, the query compares execution in recent period (1 hour) vs. history period (last day) and identifies those that introduced `additional_duration_workload`. This metric is calculated as a difference between recent average execution and history average execution multiplied by the number of recent executions. It represents how much extra duration these recent executions introduced, compared to the history:

```sql
--- "Recent" workload - last 1 hour
DECLARE @recent_start_time DATETIMEOFFSET;
DECLARE @recent_end_time DATETIMEOFFSET;

SET @recent_start_time = DATEADD(hour, - 1, SYSUTCDATETIME());
SET @recent_end_time = SYSUTCDATETIME();

--- "History" workload
DECLARE @history_start_time DATETIMEOFFSET;
DECLARE @history_end_time DATETIMEOFFSET;

SET @history_start_time = DATEADD(hour, - 24, SYSUTCDATETIME());
SET @history_end_time = SYSUTCDATETIME();

WITH hist AS (
    SELECT p.query_id query_id,
        ROUND(ROUND(CONVERT(FLOAT, SUM(rs.avg_duration * rs.count_executions)) * 0.001, 2), 2) AS total_duration,
        SUM(rs.count_executions) AS count_executions,
        COUNT(DISTINCT p.plan_id) AS num_plans
    FROM sys.query_store_runtime_stats AS rs
    INNER JOIN sys.query_store_plan AS p
        ON p.plan_id = rs.plan_id
    WHERE (
        rs.first_execution_time >= @history_start_time
        AND rs.last_execution_time < @history_end_time
    )
    OR (
        rs.first_execution_time <= @history_start_time
        AND rs.last_execution_time > @history_start_time
    )
    OR (
        rs.first_execution_time <= @history_end_time
        AND rs.last_execution_time > @history_end_time
    )
    GROUP BY p.query_id
),
recent AS (
    SELECT p.query_id query_id,
        ROUND(ROUND(CONVERT(FLOAT, SUM(rs.avg_duration * rs.count_executions)) * 0.001, 2), 2) AS total_duration,
        SUM(rs.count_executions) AS count_executions,
        COUNT(DISTINCT p.plan_id) AS num_plans
    FROM sys.query_store_runtime_stats AS rs
    INNER JOIN sys.query_store_plan AS p
        ON p.plan_id = rs.plan_id
    WHERE (
        rs.first_execution_time >= @recent_start_time
        AND rs.last_execution_time < @recent_end_time
    )
    OR (
        rs.first_execution_time <= @recent_start_time
        AND rs.last_execution_time > @recent_start_time
    )
    OR (
        rs.first_execution_time <= @recent_end_time
        AND rs.last_execution_time > @recent_end_time
    )
    GROUP BY p.query_id
    )
SELECT results.query_id AS query_id,
    results.query_text AS query_text,
    results.additional_duration_workload AS additional_duration_workload,
    results.total_duration_recent AS total_duration_recent,
    results.total_duration_hist AS total_duration_hist,
    ISNULL(results.count_executions_recent, 0) AS count_executions_recent,
    ISNULL(results.count_executions_hist, 0) AS count_executions_hist
FROM (
    SELECT hist.query_id AS query_id,
        qt.query_sql_text AS query_text,
        ROUND(CONVERT(FLOAT, recent.total_duration / recent.count_executions - hist.total_duration / hist.count_executions) * (recent.count_executions), 2) AS additional_duration_workload,
        ROUND(recent.total_duration, 2) AS total_duration_recent,
        ROUND(hist.total_duration, 2) AS total_duration_hist,
        recent.count_executions AS count_executions_recent,
        hist.count_executions AS count_executions_hist
    FROM hist
    INNER JOIN recent
        ON hist.query_id = recent.query_id
    INNER JOIN sys.query_store_query AS q
        ON q.query_id = hist.query_id
    INNER JOIN sys.query_store_query_text AS qt
        ON q.query_text_id = qt.query_text_id
) AS results
WHERE additional_duration_workload > 0
ORDER BY additional_duration_workload DESC
OPTION (MERGE JOIN);
```

### <a id="Stability"></a> Maintain query performance stability

For queries executed multiple times you might notice that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses different plans, resulting in different resource utilization and duration. With Query Store, you can detect when query performance regressed and determine the optimal plan within a period of interest. You can then force that optimal plan for future query execution.

You can also identify inconsistent query performance for a query with parameters (either autoparameterized or manually parameterized). Among different plans, you can identify the plan that is fast and optimal enough for all or most of the parameter values and force that plan. This keeps predictable performance for the wider set of user scenarios.

### Force a plan for a query (apply forcing policy)

When a plan is forced for a certain query, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] tries to force the plan in the optimizer. If plan forcing fails, an Extended Event is fired and the optimizer is instructed to optimize in the normal way.

```sql
EXEC sp_query_store_force_plan @query_id = 48, @plan_id = 49;
```

When you use `sp_query_store_force_plan`, you can only force plans recorded by Query Store as a plan for that query. In other words, the only plans available for a query are plans that were already used to execute that query while Query Store was active.

> [!NOTE]  
> Forcing plans in Query Store isn't supported in Azure Synapse Analytics.

#### <a id="ctp23"></a> Plan forcing support for fast forward and static cursors

In [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions, and Azure SQL Database (all deployment models), Query Store supports the ability to force query execution plans for fast forward and static [!INCLUDE [tsql](../../includes/tsql-md.md)] and API cursors. Forcing is supported via `sp_query_store_force_plan` or through [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Query Store reports.

### Remove plan forcing for a query

To rely again on the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] query optimizer to calculate the optimal query plan, use `sp_query_store_unforce_plan` to unforce the plan that was selected for the query.

```sql
EXEC sp_query_store_unforce_plan @query_id = 48, @plan_id = 49;
```

## Related content

- [Monitoring performance by using the Query Store](monitoring-performance-by-using-the-query-store.md)
- [Best Practice with the Query Store](best-practice-with-the-query-store.md)
- [Using the Query Store with In-Memory OLTP](using-the-query-store-with-in-memory-oltp.md)
- [Query Store Usage Scenarios](query-store-usage-scenarios.md)
- [How Query Store Collects Data](how-query-store-collects-data.md)
- [Query Store stored procedures (Transact-SQL)](../system-stored-procedures/query-store-stored-procedures-transact-sql.md)
- [Query Store catalog views (Transact-SQL)](../system-catalog-views/query-store-catalog-views-transact-sql.md)
- [Open Activity Monitor (SQL Server Management Studio)](../performance-monitor/open-activity-monitor-sql-server-management-studio.md)
- [Live Query Statistics](live-query-statistics.md)
- [Activity Monitor](../performance-monitor/activity-monitor.md)
- [sys.database_query_store_options (Transact-SQL)](../system-catalog-views/sys-database-query-store-options-transact-sql.md)
- [Monitor and Tune for Performance](monitor-and-tune-for-performance.md)
- [Performance Monitoring and Tuning Tools](performance-monitoring-and-tuning-tools.md)
- [Query Store hints](query-store-hints.md)
- [Tuning Database Using Workload from Query Store with the Database Engine Tuning Advisor](tuning-database-using-workload-from-query-store.md)
