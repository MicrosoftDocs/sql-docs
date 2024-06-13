---
title: Memory grant feedback
description: Learn about Memory grant feedback, part of the Intelligent Query Processing (IQP) feature set.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: derekw
ms.date: 06/11/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
f1_keywords:
  - "memory grant feedback"
helpviewer_keywords:
  - "memory grant feedback"
---
# Memory grant feedback

**Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later, [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] 

Sometimes a query executes with a memory grant that is too large or too small. If the memory grant is too large, we inhibit parallelism on the server. If it's too small, we might spill to disk, which is a costly operation. Memory grant feedback attempts to remember the memory needs of a prior execution (with percentile feedback, multiple past executions). Based on this historical query information, memory grant feedback adjusts the grant given to the query accordingly for subsequent executions.

This feature has been released in three waves. Batch mode memory grant feedback, followed by row mode memory grant feedback, and [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] introduced memory grant feedback on-disk persistence using the Query Store, and an improved algorithm known as percentile grant.

> [!NOTE]
> For other query feedback features, see [Cardinality estimation (CE) feedback](intelligent-query-processing-cardinality-estimation-feedback.md) and [Degree of parallelism (DOP) feedback](intelligent-query-processing-degree-parallelism-feedback.md).

## Batch mode memory grant feedback

**Applies to:** [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]), [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] (Starting with database compatibility level 140)

A query's execution plan includes the minimum required memory needed for execution and the ideal memory grant size to have all rows fit in memory. Performance suffers when memory grant sizes are incorrectly sized. Excessive grants result in wasted memory and reduced concurrency. Insufficient memory grants cause expensive spills to disk. By addressing repeating workloads, batch mode memory grant feedback recalculates the actual memory required for a query and then updates the grant value for the cached plan. When an identical query statement is executed, the query uses the revised memory grant size, reducing excessive memory grants that impact concurrency and fixing underestimated memory grants that cause expensive spills to disk.

The following graph shows one example of using batch mode adaptive memory grant feedback. For the first execution of the query, duration was **88 seconds** due to high spills:

```sql
DECLARE @EndTime datetime = '2016-09-22 00:00:00.000';
DECLARE @StartTime datetime = '2016-09-15 00:00:00.000';

SELECT TOP 10 hash_unique_bigint_id
FROM dbo.TelemetryDS
WHERE Timestamp BETWEEN @StartTime AND @EndTime
GROUP BY hash_unique_bigint_id
ORDER BY MAX(max_elapsed_time_microsec) DESC;
```

:::image type="content" source="media/2_AQPGraphHighSpills.png" alt-text="Graph of granted versus spilled MBs of memory, indicating high spills.":::

With memory grant feedback enabled, for the second execution, duration is **1 second** (down from 88 seconds), spills are removed entirely, and the grant is higher:

:::image type="content" source="media/3_AQPGraphNoSpills.png" alt-text="Graph of granted versus spilled MBs of memory, indicating no spills.":::

### Memory grant feedback sizing

For an excessive memory grant condition, if the granted memory is more than two times the size of the actual used memory, memory grant feedback will recalculate the memory grant and update the cached plan. Plans with memory grants under 1 MB won't be recalculated for overages.

For an insufficiently sized memory grant condition that result in a spill to disk for batch mode operators, memory grant feedback will trigger a recalculation of the memory grant. Spill events are reported to memory grant feedback and can be surfaced via the `spilling_report_to_memory_grant_feedback` extended event. This event returns the node ID from the plan and spilled data size of that node.

The adjusted memory grant shows up in the actual (post-execution) plan via the `GrantedMemory` property.

You can see this property in the root operator of the graphical showplan or in the showplan XML output:

```xml
<MemoryGrantInfo SerialRequiredMemory="1024" SerialDesiredMemory="10336" RequiredMemory="1024" DesiredMemory="10336" RequestedMemory="10336" GrantWaitTime="0" GrantedMemory="10336" MaxUsedMemory="9920" MaxQueryMemory="725864" />
```

To have your workloads automatically eligible for this improvement, enable compatibility level 140 for the database.

Example:

```sql
ALTER DATABASE [WideWorldImportersDW] SET COMPATIBILITY_LEVEL = 140;
```

### Memory grant feedback and parameter sensitive scenarios

Different parameter values might also require different query plans in order to remain optimal. This type of query is defined as "parameter-sensitive."

For parameter-sensitive plans, memory grant feedback will disable itself on a query if it has unstable memory requirements. The memory grant feedback feature is disabled after several repeated runs of the query and this can be observed by monitoring the `memory_grant_feedback_loop_disabled` extended event. This condition is mitigated with the [persistence and percentile mode for memory grant feedback](#percentile-and-persistence-mode-memory-grant-feedback) introduced in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. The persistence feature of memory grant feedback requires the Query Store to be enabled in the database and set to "read write" mode.

For more information about parameter sniffing and parameter sensitivity, see the [Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md#parameter-sensitivity).

#### Memory grant feedback caching

Feedback can be stored in the cached plan for a single execution. It's the consecutive executions of that statement, however, that benefit from the memory grant feedback adjustments. This feature applies to repeated execution of statements. Memory grant feedback will change only the cached plan. Prior to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], changes weren't captured in the Query Store.

Feedback isn't persisted if the plan is evicted from cache. Feedback will also be lost if there's a failover. A statement using `OPTION (RECOMPILE)` creates a new plan and doesn't cache it. Since it isn't cached, no memory grant feedback is produced, and it isn't stored for that compilation and execution. However, if an equivalent statement (that is, with the same query hash) that did **not** use `OPTION (RECOMPILE)` was cached and then re-executed, the second and later consecutive executions can benefit from memory grant feedback.

#### Track memory grant feedback activity

You can track memory grant feedback events using the `memory_grant_updated_by_feedback` extended event. This event tracks the current execution count history, the number of times the plan has been updated by memory grant feedback, the ideal additional memory grant before modification and the ideal additional memory grant after memory grant feedback has modified the cached plan.

#### Memory grant feedback, resource governor and query hints

The actual memory granted honors the query memory limit determined by the resource governor or query hint.

#### Disable batch mode memory grant feedback without changing the compatibility level

Memory grant feedback can be disabled at the database or statement scope while still maintaining database compatibility level 140 and higher. To disable batch mode memory grant feedback for all query executions originating from the database, execute the SQL statements below within the context of the applicable database:

```sql
-- SQL Server 2017
ALTER DATABASE SCOPED CONFIGURATION SET DISABLE_BATCH_MODE_MEMORY_GRANT_FEEDBACK = ON;

-- Starting with SQL Server 2019, and in Azure SQL Database
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_MEMORY_GRANT_FEEDBACK = OFF;
```

When enabled, this setting will appear as enabled in [sys.database_scoped_configurations](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md).

To re-enable batch mode memory grant feedback for all query executions originating from the database, execute the SQL statements within the context of the applicable database:

```sql
-- SQL Server 2017
ALTER DATABASE SCOPED CONFIGURATION SET DISABLE_BATCH_MODE_MEMORY_GRANT_FEEDBACK = OFF;

-- Azure SQL Database, SQL Server 2019 and higher
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_MEMORY_GRANT_FEEDBACK = ON;
```

You can also disable batch mode memory grant feedback for a specific query by designating `DISABLE_BATCH_MODE_MEMORY_GRANT_FEEDBACK` as a [USE HINT query hint](../../t-sql/queries/hints-transact-sql-query.md#use_hint). For example:

```sql
SELECT * FROM Person.Address
WHERE City = 'SEATTLE' AND PostalCode = 98104
OPTION (USE HINT ('DISABLE_BATCH_MODE_MEMORY_GRANT_FEEDBACK'));
```

A USE HINT query hint takes precedence over a [database scoped configuration](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md) or trace flag setting.

## Row mode memory grant feedback

**Applies to:** [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] (Starting with database compatibility level 150)

Row mode memory grant feedback expands on the batch mode memory grant feedback feature by adjusting memory grant sizes for both batch and row mode operators.

To enable row mode memory grant feedback in Azure SQL Database, enable database compatibility level 150 or higher for the database you're connected to when executing the query.

Example:

```sql
ALTER DATABASE [<database name>] SET COMPATIBILITY_LEVEL = 150;
```

As with batch mode memory grant feedback, row mode memory grant feedback activity is visible via the `memory_grant_updated_by_feedback` XEvent. We're also introducing two new query execution plan attributes for better visibility into the current state of a memory grant feedback operation for both row and batch mode.

Memory grant feedback doesn't require the Query Store, however, the persistence improvements introduced in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] require the Query Store to be enabled for the database and in a "read write" state. For more information on persistence, see [Percentile and persistence mode memory grant feedback](#percentile-and-persistence-mode-memory-grant-feedback) later in this article.

Row mode memory grant feedback activity is visible via the `memory_grant_updated_by_feedback` extended event.

Starting with row mode memory grant feedback, two new query plan attributes is shown for actual post-execution plans: `IsMemoryGrantFeedbackAdjusted` and `LastRequestedMemory`, which are added to the `MemoryGrantInfo` query plan XML element.

- The `LastRequestedMemory` attribute shows the granted memory in Kilobytes (KB) from the prior query execution.
- The `IsMemoryGrantFeedbackAdjusted` attribute allows you to check the state of memory grant feedback for the statement within an actual query execution plan.

Values surfaced in this attribute are as follows:

| `IsMemoryGrantFeedbackAdjusted` Value | Description |
|---|---|
| No: First Execution | Memory grant feedback doesn't adjust memory for the first compile and associated execution.  |
| No: Accurate Grant | If there's no spill to disk and the statement uses at least 50% of the granted memory, then memory grant feedback isn't triggered. |
| No: Feedback disabled | If memory grant feedback is continually triggered and fluctuates between memory-increase and memory-decrease operations, the database engine will disable memory grant feedback for the statement. |
| Yes: Adjusting | Memory grant feedback has been applied and might be further adjusted for the next execution. |
| Yes: Percentile Adjusting | Memory grant feedback is being applied using the percentile grant algorithm, which looks at more history than only the most recent execution. |
| Yes: Stable | Memory grant feedback has been applied and granted memory is now stable, meaning that what was last granted for the previous execution is what was granted for the current execution. |

## Percentile and persistence mode memory grant feedback

**Applies to:** [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]), [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] (Currently, persistence only)

This feature was introduced in [!INCLUDE [ssSQL22](../../includes/sssql22-md.md)], however this performance enhancement is available for queries that operate in the database compatibility level 140 (introduced in SQL Server 2017) or higher, or the `QUERY_OPTIMIZER_COMPATIBILITY_LEVEL_n` hint of 140 and higher, and when Query Store is enabled for the database and is in a "read write" state.

 - Percentile memory grant feedback is enabled by default in [!INCLUDE [ssSQL22](../../includes/sssql22-md.md)], but has no effect if Query Store is not enabled or when Query Store is not in a "read write" state.
 - Persistence for memory grant, CE, and DOP feedback is on by default in [!INCLUDE [ssSQL22](../../includes/sssql22-md.md)], but has no effect when Query Store is not enabled or when Query Store is not in a "read write" state.
 - Percentile and persistence for memory grant feedback is available in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and enabled by default on all databases, both existing and new. 
 - Percentile and persistence for memory grant feedback is not currently available in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)].

It's recommended that you have a performance baseline for your workload before the feature is enabled for your database. The baseline numbers will help you determine if you're getting the intended benefit from the feature.

Memory grant feedback (MGF) is an existing feature that adjusts the size of the memory allocated for a query based on past performance. However, the initial phases of this project only stored the memory grant adjustment with the plan in the cache – if a plan is evicted from the cache, the feedback process must start again, resulting in poor performance the first few times a query is executed after eviction. The new solution is to persist the grant information with the other query information in the Query Store so that the benefits last across cache evictions. Memory grant feedback persistence and percentile address existing limitations of memory grant feedback in a non-intrusive way.

Additionally, the grant size adjustments only accounted for the most recently used grant. So, if a parameterized query or workload requires significantly varying memory grant sizes with each execution, the most recent grant information could be inaccurate. It could be out of step with the actual needs of the query being executed. Memory grant feedback in this scenario is unhelpful to performance because we're always adjusting memory based on the last used grant value. The next image shows the behavior possible with memory grant feedback without percentile and persistence mode.

:::image type="content" source="media/memory-grant-feedback-without-percentile-and-persistence-mode.svg" alt-text="Graph of granted versus actual needed memory behavior in Memory Grant feedback without percentile and persistence mode memory grant feedback.":::

As you can see, in this unusual but possible query behavior, the oscillation between the actual needed and granted memory amounts results in wasted and insufficient memory if the query execution itself alternates in terms of the amount of memory. In this scenario, memory grant feedback disables itself, recognizing it's doing more harm than good.

Using a percentile-based calculation over recent history of the query, instead of simply the last execution, we can smooth the grant size values based on past execution usage history and try to optimize for minimizing spills. For example, the same alternating workload would see the following memory grant behavior:

:::image type="content" source="media/memory-grant-feedback-with-percentile-and-persistence-mode.svg" alt-text="Graph of granted versus actual needed memory behavior in Memory Grant feedback with percentile and persistence mode memory grant feedback.":::

The query optimizer uses a high percentile of past memory grant sizing requirements for executions of the cached plan to calculate memory grant sizes, using data persisted in the Query Store. The percentile adjustment, which will perform the memory grant adjustments is based on the recent history of executions. Over time, the memory grant given reduces spills and wasted memory.

Persistence also applies to [DOP feedback](intelligent-query-processing-degree-parallelism-feedback.md) and [CE feedback](intelligent-query-processing-cardinality-estimation-feedback.md).

## Enable and disable memory grant feedback features

### Disable row mode memory grant feedback without changing the compatibility level

Row mode memory grant feedback can be disabled at the database or statement scope while still maintaining database compatibility level 150 and higher. To disable row mode memory grant feedback for all query executions originating from the database, execute the SQL statements within the context of the applicable database:

```sql
ALTER DATABASE SCOPED CONFIGURATION SET ROW_MODE_MEMORY_GRANT_FEEDBACK = OFF;
```

To re-enable row mode memory grant feedback for all query executions originating from the database, execute the following within the context of the applicable database:

```sql
ALTER DATABASE SCOPED CONFIGURATION SET ROW_MODE_MEMORY_GRANT_FEEDBACK = ON;
```

You can also disable row mode memory grant feedback for a specific query by designating `DISABLE_ROW_MODE_MEMORY_GRANT_FEEDBACK` as a [USE HINT query hint](../../t-sql/queries/hints-transact-sql-query.md#use_hint). For example:

```sql
SELECT * FROM Person.Address
WHERE City = 'SEATTLE' AND PostalCode = 98104
OPTION (USE HINT ('DISABLE_ROW_MODE_MEMORY_GRANT_FEEDBACK'));
```

A USE HINT query hint takes precedence over a [database scoped configuration](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md) or trace flag setting.

### Enable memory grant feedback persistence and percentile

Persistence and percentile feedback are [enabled by default](#percentile-and-persistence-mode-memory-grant-feedback) in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssSQL22](../../includes/sssql22-md.md)].

Use database compatibility level 140 or higher for the database you're connected to when executing the query. You can change this via [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md):

`ALTER DATABASE <DATABASE NAME> SET COMPATIBILITY LEVEL = 140; -- OR HIGHER`

The Query Store must be enabled for every database where the persistence portion of this feature is used.

### Disable percentile

To disable memory grant feedback percentile for all query executions originating from the database, execute the following within the context of the applicable database:

`ALTER DATABASE SCOPED CONFIGURATION SET MEMORY_GRANT_FEEDBACK_PERCENTILE_GRANT = OFF;`

The default setting for `MEMORY_GRANT_FEEDBACK_PERCENTILE_GRANT` is `ON`.

### Disable persistence

To disable memory grant feedback persistence for all query executions originating from the database.

Execute the following within the context of the applicable database:

`ALTER DATABASE SCOPED CONFIGURATION SET MEMORY_GRANT_FEEDBACK_PERSISTENCE = OFF;`

Disabling memory grant feedback persistence will also remove existing collected feedback.

The default setting for `MEMORY_GRANT_FEEDBACK_PERSISTENCE` is `ON`.

## Considerations for memory grant feedback

You can view your current settings by querying [sys.database_scoped_configurations](../system-catalog-views/sys-database-scoped-configurations-transact-sql.md).

> [!NOTE]  
> This feature won't work if both `BATCH_MODE_MEMORY_GRANT_FEEDBACK` and `ROW_MODE_MEMORY_GRANT_FEEDBACK` are set to `OFF`.

Given feedback data is now persisted in the Query Store, there's some increase in the Query Store usage requirements.

Percentile-based memory grant errs on the side of reducing spills. Because it's no longer based on the last execution-only but on an observation of the several past executions, this could increase memory usage for oscillating workloads with wide variance in memory grant requirements between executions.

Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], when Query Store for secondary replicas is enabled, memory grant feedback is replica-aware for secondary replicas in availability groups. Memory grant feedback can apply feedback differently on a primary replica and on a secondary replica. However, memory grant feedback is not persisted on secondary replicas, and on failover, the memory grant feedback from the old primary replica is applied to the new primary replica. Any feedback applied to the secondary replica when it becomes the primary replica is lost. For more information, see [Query Store for secondary replicas](query-store-for-secondary-replicas.md).

## Related content

- [Intelligent query processing in SQL databases](intelligent-query-processing.md)
- [Intelligent query processing features in detail](intelligent-query-processing-details.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Monitor and Tune for Performance](monitor-and-tune-for-performance.md)
- [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)
