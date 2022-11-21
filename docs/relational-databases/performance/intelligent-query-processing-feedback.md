---
title: Query processing feedback features
description: Learn about query processing feedback features, part of the Intelligent Query Processing (IQP) feature set.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: katsmith
ms.date: 09/29/2022
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
f1_keywords:
  - "parallel queries [SQL Server]"
  - "processors [SQL Server], parallel queries"
  - "number of processors for parallel queries"
  - "degree of parallelism feedback"
helpviewer_keywords:
  - "parallel queries [SQL Server]"
  - "processors [SQL Server], parallel queries"
  - "number of processors for parallel queries"
  - "degree of parallelism feedback"
---

# Query processing feedback features

This article has in-depth descriptions of various intelligent query processing (IQP) feedback features. The query processing feedback features are part of the Intelligent query processing family of features. Query processing feedback is a process by which the query processor in SQL Server, Azure SQL Database, and Azure SQL Managed Instance uses historical data about a query's execution to decide if the query might receive help from one or more changes to the way it's compiled and executed. The performance data is collected in the [query store](tune-performance-with-the-query-store.md), with various suggestions to improve query execution. If successful, we persist these modifications to disk in memory and/or in the query store for future use. If the suggestions don't yield sufficient improvement, they're discarded, and the query continues to execute without that feedback.

The feedback features discussed in this article are:

- [Memory Grant feedback](#memory-grant-feedback)
    - [Batch mode](#batch-mode-memory-grant-feedback)
    - [Row mode](#row-mode-memory-grant-feedback)
    - [Percentile and persistence mode](#percentile-and-persistence-mode-memory-grant-feedback)
- [Degree of parallelism feedback](#degree-of-parallelism-dop-feedback)
- [Cardinality Estimation feedback](#cardinality-estimation-ce-feedback)

## Memory grant feedback

Sometimes a query executes with a memory grant that is too large or too small.  If the memory grant is too large, we inhibit parallelism on the server. If it's too small, we may spill to disk, which is a costly operation. Memory grant feedback attempts to remember the memory needs of a prior execution (starting in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], multiple executions) of a query and adjust the grant given to the query accordingly. This feature has been released in three waves. Batch mode memory grant feedback, followed by row mode memory grant feedback, and in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], we're introducing memory grant feedback on-disk persistence using the Query Store and an improved algorithm known as percentile grant.

### Batch mode memory grant feedback

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

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

:::image type="content" source="./media/2_AQPGraphHighSpills.png" alt-text="A graph of granted vs spilled MBs of memory, indicating high spills." lightbox="./media/2_AQPGraphHighSpills.png":::

With memory grant feedback enabled, for the second execution, duration is **1 second** (down from 88 seconds), spills are removed entirely, and the grant is higher:

:::image type="content" source="./media/3_AQPGraphNoSpills.png" alt-text="A graph of granted vs spilled MBs of memory, indicating no spills." lightbox="./media/3_AQPGraphNoSpills.png":::

#### Memory grant feedback sizing

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

#### Memory grant feedback and parameter sensitive scenarios

Different parameter values may also require different query plans in order to remain optimal. This type of query is defined as "parameter-sensitive."

For parameter-sensitive plans, memory grant feedback will disable itself on a query if it has unstable memory requirements. The memory grant feedback feature is disabled after several repeated runs of the query and this can be observed by monitoring the `memory_grant_feedback_loop_disabled` extended event. This condition is mitigated with the [persistence and percentile mode for memory grant feedback](#percentile-and-persistence-mode-memory-grant-feedback) introduced in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]. The persistence feature of memory grant feedback requires the Query Store to be enabled in the database and set to "read write" mode.

For more information about parameter sniffing and parameter sensitivity, see the [Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md#parameter-sensitivity).

#### Memory grant feedback caching

Feedback can be stored in the cached plan for a single execution. It's the consecutive executions of that statement, however, that benefit from the memory grant feedback adjustments. This feature applies to repeated execution of statements. Memory grant feedback will change only the cached plan. Prior to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], changes weren't captured in the Query Store.

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

### Row mode memory grant feedback

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

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
| Yes: Adjusting | Memory grant feedback has been applied and may be further adjusted for the next execution. |
| Yes: Stable | Memory grant feedback has been applied and granted memory is now stable, meaning that what was last granted for the previous execution is what was granted for the current execution. |

#### Disable row mode memory grant feedback without changing the compatibility level

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

### Percentile and persistence mode memory grant feedback

**Applies to:** [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later

This feature was introduced in [!INCLUDE[ssSQL22](../../includes/sssql22-md.md)], however this performance enhancement is available for queries that operate in the database compatibility level 140 (introduced in SQL Server 2017) or higher, or the QUERY_OPTIMIZER_COMPATIBILITY_LEVEL_n hint of 140 and higher, and when Query Store is enabled for the database and is in a "read write" state.

 - Percentile memory grant feedback is enabled by default in [!INCLUDE[ssSQL22](../../includes/sssql22-md.md)], but has no effect if when Query Store is not enabled and in a "read write" state.
 - Persistence for memory grant, CE, and DOP feedback is on by default in [!INCLUDE[ssSQL22](../../includes/sssql22-md.md)], but has no effect if when Query Store is not enabled and in a "read write" state.
 - Percentile memory grant feedback is not currently available in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], and [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)].
 - Persistence is not currently available in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)].

It's recommended that you have a performance baseline for your workload before the feature is enabled for your database. The baseline numbers will help you determine if you're getting the intended benefit from the feature.

Memory grant feedback (MGF) is an existing feature that adjusts the size of the memory allocated for a query based on past performance. However, the initial phases of this project only stored the memory grant adjustment with the plan in the cache – if a plan is evicted from the cache, the feedback process must start again, resulting in poor performance the first few times a query is executed after eviction. The new solution is to persist the grant information with the other query information in the Query Store so that the benefits last across cache evictions. Memory grant feedback persistence and percentile address existing limitations of memory grant feedback in a non-intrusive way.

Additionally, the grant size adjustments only accounted for the most recently used grant. So, if a parameterized query or workload requires significantly varying memory grant sizes with each execution, the most recent grant information could be inaccurate. It could be out of step with the actual needs of the query being executed. Memory grant feedback in this scenario is unhelpful to performance because we're always adjusting memory based on the last used grant value. The next image shows the behavior possible with memory grant feedback without percentile and persistence mode.

:::image type="content" source="./media/memory-grant-feedback-without-percentile-and-persistence-mode.svg" alt-text="A graph of granted vs actual needed memory behavior in Memory Grant feedback without percentile and persistence mode memory grant feedback.":::

As you can see, in this unusual but possible query behavior, the oscillation between the actual needed and granted memory amounts results in wasted and insufficient memory if the query execution itself alternates in terms of the amount of memory. In this scenario, memory grant feedback disables itself, recognizing it's doing more harm than good.

Using a percentile-based calculation over recent history of the query, instead of simply the last execution, we can smooth the grant size values based on past execution usage history and try to optimize for minimizing spills. For example, the same alternating workload would see the following memory grant behavior:

:::image type="content" source="./media/memory-grant-feedback-with-percentile-and-persistence-mode.svg" alt-text="A graph of granted vs actual needed memory behavior in Memory Grant feedback with percentile and persistence mode memory grant feedback." :::

The query optimizer uses a high percentile of past memory grant sizing requirements for executions of the cached plan to calculate memory grant sizes, using data persisted in the Query Store. The percentile adjustment, which will perform the memory grant adjustments is based on the recent history of executions. Over time, the memory grant given reduces spills and wasted memory.

Persistence also applies to [DOP feedback](#degree-of-parallelism-dop-feedback) and [CE feedback](#cardinality-estimation-ce-feedback), also detailed in this article.

#### Enable memory grant feedback: persistence and percentile

To enable memory grant feedback persistence and percentile, use database compatibility level 140 or higher for the database you're connected to when executing the query. Peristence and percentile feedback are [enabled by default](#percentile-and-persistence-mode-memory-grant-feedback).

`ALTER DATABASE <DATABASE NAME> SET COMPATIBILITY LEVEL = 140; -- OR HIGHER`

The Query Store must be enabled for every database where the persistence portion of this feature is used. 

#### Disable percentile

To disable memory grant feedback percentile for all query executions originating from the database, execute the following within the context of the applicable database:

`ALTER DATABASE SCOPED CONFIGURATION SET MEMORY_GRANT_FEEDBACK_PERCENTILE = OFF;`

The default setting for `MEMORY_GRANT_FEEDBACK_PERCENTILE` is `OFF`.

#### Disable persistence

To disable memory grant feedback persistence for all query executions originating from the database.

Execute the following within the context of the applicable database:

`ALTER DATABASE SCOPED CONFIGURATION SET MEMORY_GRANT_FEEDBACK_PERSISTENCE = OFF;`

Disabling memory grant feedback persistence will also remove existing collected feedback.

The default setting for `MEMORY_GRANT_FEEDBACK_PERSISTENCE` is `ON`.

#### Considerations for memory grant feedback

You can view your current settings by querying `sys.database_scoped_configurations`.

> [!NOTE]  
> That this feature won't work if both `BATCH_MODE_MEMORY_GRANT_FEEDBACK` and `ROW_MODE_MEMORY_GRANT_FEEDBACK` are set to OFF.

Given feedback data is now persisted in the Query Store, there's some increase in the Query Store usage requirements.

Percentile-based memory grant errs on the side of reducing spills. Because it's no longer based on the last execution-only but on an observation of the several past executions, this could increase memory usage for oscillating workloads with wide variance in memory grant requirements between executions.

Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], when Query Store for secondary replicas is enabled, memory grant feedback is replica-aware for secondary replicas in availability groups. Memory grant feedback can apply feedback differently on a primary replica and on a secondary replica. However, memory grant feedback is not persisted on secondary replicas, and on failover, the memory grant feedback from the old primary replica is applied to the new primary replica. Any feedback applied to the secondary replica when it becomes the primary replica is lost. For more information, see [Query Store for secondary replicas](query-store-for-secondary-replicas.md).

## Degree of parallelism (DOP) feedback

**Applies to:** [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later

[!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] introduced a new feature called degree of parallelism (DOP) feedback to improve query performance by identifying parallelism inefficiencies for repeating queries, based on elapsed time and waits. DOP feedback is part of the [intelligent query processing](../../relational-databases/performance/intelligent-query-processing.md) family of features, and addresses suboptimal usage of parallelism for repeating queries. This scenario helps with optimizing resource usage and improving scalability of workloads, when excessive parallelism can cause performance issues. Instead of incurring in the pains of an all-encompassing default or manual adjustments to each query, DOP feedback self-adjusts DOP to avoid the issues described above.

Instead of incurring in the pains of an all-encompassing default or manual adjustments to each query, DOP feedback self-adjusts DOP to avoid excess parallelism. If parallelism usage is deemed inefficient, DOP feedback lowers the DOP for the next execution of the query, from whatever is the configured DOP, and verify if it helps.

Parallelism is often beneficial for reporting and analytical queries, or queries that otherwise handle large amounts of data. Conversely, OLTP-centric queries that are executed in parallel could experience performance issues when the time spent coordinating all threads outweighs the advantages of using a parallel plan. For more information, see [parallel plan execution](../../relational-databases/query-processing-architecture-guide.md#parallel-query-processing).

- To enable DOP feedback, enable the `DOP_FEEDBACK` [database scoped configuration](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#dop_feedback---on--off-) in a database.

- The Query Store must be enabled for every database where DOP feedback is used, and in the "Read write" state. Feedback will be persisted in the [sys.query_store_plan_feedback](../system-catalog-views/sys-query-store-plan-feedback.md) catalog view when we reach a stable degree of parallelism feedback value.

- DOP feedback is available for queries that operate in the database compatibility level 160 (introduced with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]) or higher.

- Only verified feedback is persisted. If the adjusted DOP results in a performance regression, DOP feedback will go back to the last known good DOP. In this context, a user canceled query is also perceived as a regression. The DOP feedback doesn't recompile plans.

- Stable feedback is reverified upon plan recompilation and may readjust up or down, but never above MAXDOP setting (including a MAXDOP hint).

- To disable DOP feedback at the database level, use the `ALTER DATABASE SCOPED CONFIGURATION SET DOP_FEEDBACK = OFF` [database scoped configuration](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#dop_feedback---on--off-).

- To disable DOP feedback at the query level, use the `DISABLE_DOP_FEEDBACK` query hint.

- Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], when Query Store for secondary replicas is enabled, DOP feedback is also replica-aware for secondary replicas in availability groups. DOP feedback can apply feedback differently on a primary replica and on a secondary replica. However, DOP feedback is not persisted on secondary replicas, and on failover, the DOP feedback from the old primary replica is not applied to the new primary replica. On failover, feedback applied to primary or secondary replicas is lost. For more information, see [Query Store for secondary replicas](query-store-for-secondary-replicas.md).

### DOP feedback implementation

DOP feedback will identify parallelism inefficiencies for repeating queries, based on elapsed time and waits. If parallelism usage is deemed inefficient, DOP feedback will lower the DOP for the next execution of the query, from whatever is the configured DOP, and verify if it helps.

To assess query eligibility, the adjusted query elapsed time is measured over a few executions. The total elapsed time for each query is adjusted by ignoring Buffer Latch, Buffer IO, and Network IO waits which are external to the parallel query execution. The goal of the DOP feedback feature is to increase overall concurrency and reduce waits significantly, even if it slightly increases query elapsed time.

Only verified feedback is persisted. If the adjusted DOP results in a performance regression, DOP feedback will go back to the last known good DOP. In this context, a user canceled query is also perceived as a regression.

> [!NOTE]  
> DOP feedback doesn't recompile plans.

### DOP feedback considerations

Minimum DOP for any query adjusted with DOP feedback is 2. Serial executions are out of scope for DOP feedback.

Feedback information can be tracked using the [sys.query_store_plan_feedback](../system-catalog-views/sys-query-store-plan-feedback.md) catalog view.

If a query has a query plan forced through Query Store, DOP feedback can still be used for that query.

If a query uses the MAXDOP hint, either as a hard-coded query hints or through the Query Store hinting mechanism, and the MAXDOP hint is greater than 2, DOP feedback will lower the DOP using the hinted value as the ceiling. For more information, see [Hints (Transact-SQL) - Query](../../t-sql/queries/hints-transact-sql-query.md) and [Query Store hints](../../relational-databases/performance/query-store-hints.md).

#### Extended events for DOP feedback

The following XEs are available for the feature:

- `dop_feedback_eligible_query`: Occurs when the query plan becomes eligible for DOP feedback.  Additional events may fire if a recompile or SQL Server instance restart occurs.
- `dop_feedback_provided`: Occurs when a DOP feedback provided data for a given query.  This event contains baseline statistics when feedback provided for first time and previous feedback statistics when subsequent feedback is provided.
- `dop_feedback_validation`: Occurs when validation occurs for the query runtime stats against a baseline or previous feedback stats.
- `dop_feedback_stabilized`: Occurs when DOP feedback is stabilized for a query.  
- `dop_feedback_reverted`: Occurs when a DOP feedback is reverted.  The event will fire when feedback validation fails on the first feedback provided.  The system will revert back to no feedback state.
- `dop_feedback_analysis_stopped` : Occurs when the DOP feedback analysis is stopped for a query.

## Cardinality estimation (CE) feedback

**Applies to:** [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later. Currently in preview for [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)].

Starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)]), the Cardinality Estimation (CE) feedback is part of the [intelligent query processing family of features](intelligent-query-processing.md) and addresses suboptimal query execution plans for repeating queries when these issues result from incorrect CE model assumptions. This scenario helps with reducing regression risks related to the default CE when upgrading from older versions of the Database Engine.

Because no single set of CE models and assumptions can accommodate the vast array of customer workloads and data distributions, CE feedback provides an adaptable solution based on query runtime characteristics. CE feedback will identify and use a model assumption that better fits a given query and data distribution to improve query execution plan quality. Feedback is applied when significant model estimation errors resulting in performance drops are found.

- Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], when Query Store for secondary replicas is enabled, CE feedback is not replica-aware for secondary replicas in availability groups. CE feedback currently only benefits primary replicas. For more information, see [Query Store for secondary replicas](query-store-for-secondary-replicas.md).

### Understand Cardinality Estimation

Cardinality Estimation (CE) is how the Query Optimizer can estimate the total number of rows processed at each level of a query plan. Cardinality estimation in SQL Server is derived primarily from histograms created when indexes or statistics are created, either manually or automatically. Sometimes, SQL Server also uses constraint information and logical rewrites of queries to determine cardinality.

Different versions of the Database Engine use different CE model assumptions based on how data is distributed and queried. For more information, see [versions of the CE](cardinality-estimation-sql-server.md#versions-of-the-ce).

### CE feedback implementation

CE feedback learns which CE model assumptions are optimal over time and then apply the historically most correct assumption:

1. CE feedback **identifies** model-related assumptions and evaluates whether they're accurate for repeating queries.

1. If an assumption looks incorrect, a subsequent execution of the same query is tested with a query plan that adjusts the impactful CE model assumption and **verifies** if it helps.

1. If it improves plan quality, the old query plan is **replaced** with a query plan that uses the appropriate [USE HINT query hint](../../t-sql/queries/hints-transact-sql-query.md#l-using-use-hint) that adjusts the estimation model, implemented through the [Query Store hint](query-store-hints.md) mechanism.

Only verified feedback is persisted. CE feedback isn't used for that query if the adjusted model assumption results in a performance regression. In this context, a user canceled query is also perceived as a regression.

### CE feedback scenarios

CE feedback addresses perceived regression issues resulting from incorrect CE model assumptions when using the default CE (CE120 or higher) and can selectively use different model assumptions. The scenarios include Correlation, Join Containment, and Optimizer row goal.

#### Correlation

When the Query Optimizer estimates the selectivity of predicates on a given table or view, or the number of rows satisfying the said predicate, it uses **correlation model assumptions**. These assumptions can be that predicates are:

- **Fully independent** (default for CE70), where cardinality is calculated by multiplying the selectivities of all predicates.

- **Partially correlated** (default for CE120 and higher), where cardinality is calculated using a variation on exponential backoff, ordering the selectivities from most to the least selective predicate.

- **Fully correlated**, where cardinality is calculated by using the minimum selectivities for all predicates.

The following example uses partial correlation when the database compatibility is set to 120 or higher:

```sql
USE AdventureWorks2016_EXT;
GO
SELECT AddressID, AddressLine1, AddressLine2
FROM Person.Address
WHERE StateProvinceID = 79 AND City = N'Redmond';
GO
```

When the database compatibility is set to 160, and default correlation is used, CE feedback will attempt to move the correlation to the correct direction one step at a time based on whether the estimated cardinality was underestimated or overestimated compared to the actual number of rows. Use full correlation if an actual number of rows is greater than the estimated cardinality. Use full independence if an actual number of rows is smaller than the estimated cardinality.

For more information, see [versions of the CE](cardinality-estimation-sql-server.md#versions-of-the-ce).

#### Join Containment

When the Query Optimizer estimates the selectivity of join predicates and applicable filter predicates, it uses **containment model assumptions**. These assumptions are:

- **Simple containment** (default for CE70) assumes that join predicates are fully correlated, where filter selectivity is calculated first, and then the join selectivity is factored in.

- **Base containment** (default for CE120 and higher) assumes no correlation between join predicates and downstream filters,

where join selectivity is calculated first, and then the filter selectivity is factored in.

The following example uses base containment when the database compatibility is set to 120 or higher:

```sql
USE AdventureWorksDW2016_EXT;
GO
SELECT *
FROM dbo.FactCurrencyRate AS f
INNER JOIN dbo.DimDate AS d ON f.DateKey = d.DateKey
WHERE d.MonthNumberOfYear = 7 AND f.CurrencyKey = 3 AND f.AverageRate > 1;
GO
```

For more information, see [versions of the CE](cardinality-estimation-sql-server.md#versions-of-the-ce).

#### Optimizer row goal

When the Query Optimizer estimates the cardinality of an execution plan, it usually assumes that all qualifying rows from all tables have to be processed. However, some query patterns cause the Query Optimizer to search for a plan that will return a smaller number of rows to reduce I/O. If the query specifies a target number of rows (row goal) that may be expected at runtime by using a `TOP`, `IN` or `EXISTS` keywords, the `FAST` query hint, or a `SET ROWCOUNT` statement, that row goal is used as part of the query optimization process such as in the following example:

```sql
USE AdventureWorks2016_EXT;
GO
SELECT TOP 1 soh.*
FROM Sales.SalesOrderHeader AS soh
INNER JOIN Sales.SalesOrderDetail AS sod ON soh.SalesOrderID = sod.SalesOrderID;
GO
```

When the row goal plan is applied, the estimated number of rows in the query plan is reduced because the Query Optimizer assumes that a smaller number of rows will have to be processed in order to reach the row goal.

While row goal is a beneficial optimization strategy for certain query patterns, if data isn't uniformly distributed, more pages may be scanned than estimated, meaning that row goal becomes inefficient. CE feedback can disable the row goal scan and enable a seek when this inefficiency is detected.

In the execution plan, there is no attribute specific to CE feedback, but there will be an attribute listed for the Query Store hint. Look for the `QueryStoreStatementHintSource` to be `CE feedback`.

### Considerations for CE feedback

To enable CE feedback, enable database compatibility level 160 for the database you're connected to when executing the query. The Query Store must be enabled and in READ_WRITE mode for every database where CE feedback is used.

CE feedback activity is visible via the `query_feedback_analysis` and `query_feedback_validation` XEvents.

Hints set by CE feedback can be tracked using the [sys.query_store_query_hints](../system-catalog-views/sys-query-store-query-hints-transact-sql.md) catalog view.

Feedback information can be tracked using the [sys.query_store_plan_feedback](../system-catalog-views/sys-query-store-plan-feedback.md) catalog view.

To disable CE feedback at the database level, use the `ALTER DATABASE SCOPED CONFIGURATION SET CE_FEEDBACK = OFF` [database scoped configuration](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#ce_feedback---on--off-).

To disable CE feedback at the query level, use the `DISABLE_CE_FEEDBACK` query hint.

If a query has a query plan forced through Query Store, CE feedback won't be used for that query.

If a query uses hard-coded query hints or is using Query Store hints set by the user, CE feedback won't be used for that query. For more information, see [Hints (Transact-SQL) - Query](../../t-sql/queries/hints-transact-sql-query.md) and [Query Store hint](query-store-hints.md).

#### Feedback and reporting issues

For feedback or questions, email [CEFfeedback@microsoft.com](mailto:CEFfeedback@microsoft.com)

## Next steps

- [Intelligent query processing](intelligent-query-processing.md)
- [Intelligent query processing details](intelligent-query-processing-details.md)
- [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md)
- [Cardinality Estimates](../../relational-databases/performance/cardinality-estimation-sql-server.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Monitor and Tune for Performance](../../relational-databases/performance/monitor-and-tune-for-performance.md)
- [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md)
- [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)
