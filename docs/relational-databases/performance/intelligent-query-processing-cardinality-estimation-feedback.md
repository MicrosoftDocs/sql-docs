---
title: Cardinality estimation feedback
description: Learn about query processing feedback features, part of the Intelligent Query Processing (IQP) feature set.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: derekw, randolphwest, wiassaf
ms.date: 06/07/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
f1_keywords:
  - "cardinality estimation feedback"
helpviewer_keywords:
  - "cardinality estimation feedback"
---
# Cardinality estimation (CE) feedback

**Applies to:** [!INCLUDE [sqlserver2022-and-later](../../includes/applies-to-version/sqlserver2022-and-later.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)].
<!---
Currently in preview for [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)].
-->

Starting with [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)], the Cardinality Estimation (CE) feedback is part of the [intelligent query processing family of features](intelligent-query-processing.md) and addresses suboptimal query execution plans for repeating queries when these issues result from incorrect CE model assumptions. This scenario helps with reducing regression risks related to the default CE when upgrading from older versions of the Database Engine.

Because no single set of CE models and assumptions can accommodate the vast array of customer workloads and data distributions, CE feedback provides an adaptable solution based on query runtime characteristics. CE feedback identifies and uses a model assumption that better fits a given query and data distribution to improve query execution plan quality. Currently, CE feedback can identify plan operators where the estimated number of rows and the actual number of rows are very different. Feedback is applied when significant model estimation errors occur, and there's a viable alternate model to try.

For other query feedback features, see [Memory grant feedback](intelligent-query-processing-memory-grant-feedback.md) and [Degree of parallelism (DOP) feedback](intelligent-query-processing-degree-parallelism-feedback.md).

## Understand cardinality estimation (CE) feedback

Cardinality estimation (CE) is how the Query Optimizer can estimate the total number of rows processed at each level of a query plan. Cardinality estimation in SQL Server is derived primarily from histograms created when indexes or statistics are created, either manually or automatically. Sometimes, SQL Server also uses constraint information and logical rewrites of queries to determine cardinality.

Different versions of the Database Engine use different CE model assumptions based on how data is distributed and queried. For more information, see [versions of the CE](cardinality-estimation-sql-server.md#versions-of-the-ce).

## Cardinality estimation (CE) feedback implementation

Cardinality estimation (CE) feedback learns which CE model assumptions are optimal over time, and then applies the historically most correct assumption:

1. CE feedback **identifies** model-related assumptions and evaluates whether they're accurate for repeating queries.

1. If an assumption looks incorrect, a subsequent execution of the same query is tested with a query plan that adjusts the impactful CE model assumption and **verifies** if it helps. We identify incorrectness by looking at actual vs. estimated rows from plan operators. Not all errors can be corrected by model variants available in CE feedback.

1. If it improves plan quality, the old query plan is **replaced** with a query plan that uses the appropriate [USE HINT query hint](../../t-sql/queries/hints-transact-sql-query.md#l-use-use-hint) that adjusts the estimation model, implemented through the [Query Store hint](query-store-hints.md) mechanism.

Only verified feedback is persisted. CE feedback isn't used for that query if the adjusted model assumption results in a performance regression. In this context, a user canceled query is also perceived as a regression.

### Cardinality estimation (CE) feedback scenarios

Cardinality estimation (CE) feedback addresses perceived regression issues resulting from incorrect CE model assumptions when using the default CE (CE120 or higher) and can selectively use different model assumptions. The scenarios include Correlation, Join Containment, and Optimizer row goal.

#### Cardinality estimation (CE) feedback correlation

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

When the database compatibility is set to 160, and default correlation is used, CE feedback attempts to move the correlation to the correct direction one step at a time based on whether the estimated cardinality was underestimated or overestimated compared to the actual number of rows. Use full correlation if an actual number of rows is greater than the estimated cardinality. Use full independence if an actual number of rows is smaller than the estimated cardinality.

For more information, see [versions of the CE](cardinality-estimation-sql-server.md#versions-of-the-ce).

#### Cardinality estimation (CE) feedback join containment

When the Query Optimizer estimates the selectivity of join predicates and applicable filter predicates, it uses **containment model assumptions**. These assumptions are:

- **Simple containment** (default for CE70) assumes that join predicates are fully correlated, where filter selectivity is calculated first, and then the join selectivity is factored in.

- **Base containment** (default for CE120 and higher) assumes no correlation between join predicates and downstream filters, where join selectivity is calculated first, and then the filter selectivity is factored in.

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

#### Cardinality estimation (CE) feedback and the query optimizer row goal

When the Query Optimizer estimates the cardinality of an execution plan, it usually assumes that all qualifying rows from all tables have to be processed. However, some query patterns cause the Query Optimizer to search for a plan that will return a smaller number of rows to reduce I/O. If the query specifies a target number of rows (row goal) that might be expected at runtime by using a `TOP`, `IN` or `EXISTS` keywords, the `FAST` query hint, or a `SET ROWCOUNT` statement, that row goal is used as part of the query optimization process such as in the following example:

```sql
USE AdventureWorks2016_EXT;
GO
SELECT TOP 1 soh.*
FROM Sales.SalesOrderHeader AS soh
INNER JOIN Sales.SalesOrderDetail AS sod ON soh.SalesOrderID = sod.SalesOrderID;
GO
```

When the row goal plan is applied, the estimated number of rows in the query plan is reduced because the Query Optimizer assumes that a smaller number of rows will have to be processed in order to reach the row goal.

While row goal is a beneficial optimization strategy for certain query patterns, if data isn't uniformly distributed, more pages might be scanned than estimated, meaning that row goal becomes inefficient. CE feedback can disable the row goal scan and enable a seek when this inefficiency is detected.

In the execution plan, there's no attribute specific to CE feedback, but there's an attribute listed for the Query Store hint. Look for the `QueryStoreStatementHintSource` to be `CE feedback`.

## Considerations for cardinality estimation (CE) feedback

- To enable cardinality estimation (CE) feedback, enable database compatibility level 160 for the database you're connected to when executing the query. The Query Store must be enabled and in READ_WRITE mode for every database where CE feedback is used.

- To disable CE feedback at the database level, use the `CE_FEEDBACK` [database scoped configuration](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#ce_feedback---on--off-). For example, in the user database:

   ```sql
   ALTER DATABASE SCOPED CONFIGURATION SET CE_FEEDBACK = OFF;
   ```

- To disable CE feedback at the query level, use the `DISABLE_CE_FEEDBACK` query hint.

CE feedback activity is visible via the `query_feedback_analysis` and `query_feedback_validation` XEvents.

Hints set by CE feedback can be tracked using the [sys.query_store_query_hints](../system-catalog-views/sys-query-store-query-hints-transact-sql.md) catalog view.

Feedback information can be tracked using the [sys.query_store_plan_feedback](../system-catalog-views/sys-query-store-plan-feedback.md) catalog view.

If a query has a query plan forced through Query Store, CE feedback isn't used for that query.

If a query uses hard-coded query hints or is using Query Store hints set by the user, CE feedback isn't used for that query. For more information, see [Query hints](../../t-sql/queries/hints-transact-sql-query.md) and [Query Store hint](query-store-hints.md).

Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], when Query Store for secondary replicas is enabled, CE feedback isn't replica-aware for secondary replicas in availability groups. CE feedback currently only benefits primary replicas. On failover, feedback applied to primary or secondary replicas is lost. For more information, see [Query Store for secondary replicas](query-store-for-secondary-replicas.md).

## Persistence for cardinality estimation (CE) feedback

**Applies to:** [!INCLUDE [sqlserver2022-and-later](../../includes/applies-to-version/sqlserver2022-and-later.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]
<!-- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]) -->

Cardinality estimation (CE) feedback can detect scenarios when the row goal optimization should be persisted, and keep this change by persisting it in the Query Store in the form of a Query Store hint. The new optimization is used for future executions of the query. CE feedback persists other scenarios outside of row goal optimization query patterns, as detailed in [feedback scenarios](#cardinality-estimation-ce-feedback-scenarios). CE feedback currently handles predicate selectivity scenarios that are used by the CE's correlation model, and join predicate scenarios that are handled by the CE's containment model.

This feature was introduced in [!INCLUDE [ssSQL22](../../includes/sssql22-md.md)], however this performance enhancement is available for queries that operate in the database compatibility level 160 or higher, or the `QUERY_OPTIMIZER_COMPATIBILITY_LEVEL_n` hint of 160 and higher, and when Query Store is enabled for the database and is in a "read write" state.

## Known issues with cardinality estimation (CE) feedback

| Issue | Date discovered | Status | Date resolved |
| --- | --- | --- | --- |
| Slow SQL Server performance after you apply Cumulative Update 8 for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] under certain conditions. You might encounter dramatic Plan Cache memory utilization along with unexpected increases in CPU utilization when CE feedback is enabled. | December 2023 | Resolved | April 22, 2024 (CU 12) |

### Known issues details

#### Slow SQL Server performance after you apply Cumulative Update 8 for SQL Server 2022 under certain conditions

Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Cumulative Update 8, SQL Server might exhibit unexpected increases in CPU and memory utilization. Additionally, an increase in RESOURCE_SEMAPHORE_QUERY_COMPILE waits might also be observed. You might also notice steady increases in the number of Plan Cache objects in use that approach the Plan Cache limits and manually clearing the Plan Cache with techniques like `ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE`, `DBCC FREESYSTEMCACHE`, or `DBCC FREEPROCCACHE` don't provide assistance. This behavior has only been observed by a few customers.

This issue doesn't affect all workloads, and depends on the number of different plans that were generated as well as the number of plans that were eligible for the CE feedback feature to engage. While CE feedback is analyzing plan operators for significant model misestimations, there's a scenario where a referenced plan can be dereferenced during this analysis phase. This situation prevents the plan from being removed from memory using the usual Least Recently Used (LRU) algorithm. The LRU mechanism one way that SQL Server enforces plan eviction policies. SQL Server also removes plans from memory if the system is under memory pressure. When SQL Server attempts to remove the plans that were dereferenced improperly, it's unable to remove those plans from the plan cache, which causes the cache to continue to grow. The growing cache might start to cause additional compilations that ultimately use more CPU and memory. For more information, see [Plan Cache Internals](/previous-versions/tn-archive/cc293624(v=technet.10)).

**Symptom**: The number of plan cache **entries in use** and are marked as **dirty** from either SQL Plans or Object Plans increases over time to 50,000 or more. If you observe plan cache entries that start to approach this level along with unexpected increases in CPU utilization, your system might be encountering this issue. A fix is provided with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Cumulative Update 12. See [KB5033663](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate12#2890724).

To monitor the number of plan cache entries that your system is using, the following examples can be used as a point in time view of the number of plan cache entries that exist. As an example, watching the number of plan cache entries that are marked as dirty, periodically over time is one way to monitor for this phenomenon.

```sql
SELECT
  CASE
    WHEN mce.[name] LIKE 'SQL Plan%' THEN 'SQL Plans'
    WHEN mce.[name] LIKE 'Object Plan%' THEN 'Object Plans'
    ELSE '[All other cache stores]'
  END AS PlanType,
  COUNT(*) AS [Number of plans marked to be removed]
FROM sys.dm_os_memory_cache_entries AS mce
LEFT OUTER JOIN sys.dm_exec_cached_plans AS ecp
  ON mce.memory_object_address = ecp.memory_object_address
WHERE mce.is_dirty = 1
AND ecp.bucketid is NULL
GROUP BY
  CASE
    WHEN mce.[name] LIKE 'SQL Plan%' THEN 'SQL Plans'
    WHEN mce.[name] LIKE 'Object Plan%' THEN 'Object Plans'
    ELSE '[All other cache stores]'
  END;
```

Another set of queries that also provide the same information as the previous example while also allowing you to observe additional performance metrics. Plan Cache hit ratios decrease, as well as the number of compilations in relation to the number of batch requests/sec. The following queries can be used to monitor your system over time. Keeping an eye on the **Cache Hit Ratio** (unanticipated dips), the **Cache Objects in use** (increases in the count to levels approaching 50,000 without decreasing) and a lower than expected **Batch Requests/sec** ratio as compared to a rise in **Compilations/sec**.

```sql
--SQL Plan (Adhoc and Prepared plans)
SELECT
    CASE
        WHEN [counter_name] = 'Cache Hit Ratio' THEN 'Cache Hit Ratio'
        WHEN [counter_name] = 'Cache Object Counts' THEN 'Cache Object Counts'
        WHEN [counter_name] = 'Cache Objects in use' THEN 'Cache Objects in use'
        WHEN [counter_name] = 'Cache Pages' THEN 'Cache Pages'
    END AS [SQLServer:Plan Cache (SQL Plans)],
    CASE
        WHEN [counter_name] = 'Cache Hit Ratio' THEN NULL
        ELSE FORMAT(cntr_value, '#,###')
    END AS [Counter Value],
    CASE
        WHEN [counter_name] = 'Cache Hit Ratio' THEN
            FORMAT(TRY_CONVERT(DECIMAL(5, 2), (cntr_value * 1.0 / NULLIF((SELECT cntr_value
        FROM sys.dm_os_performance_counters WHERE
        [object_name] LIKE '%:Plan Cache%' AND [counter_name] = 'Cache Hit Ratio Base'
        AND instance_name LIKE 'SQL Plan%'), 0))), '0.00%')
    END AS [SQL Plan Cache Hit Ratio]
FROM sys.dm_os_performance_counters
WHERE [object_name] LIKE '%:Plan Cache%'
    AND [counter_name] IN ('Cache Hit Ratio', 'Cache Object Counts', 'Cache Objects in use', 'Cache Pages')
    AND instance_name LIKE 'SQL Plan%'
ORDER BY [counter_name];

--Module/Stored procedure based plans
SELECT
    CASE
        WHEN [counter_name] = 'Cache Hit Ratio' THEN 'Cache Hit Ratio'
        WHEN [counter_name] = 'Cache Object Counts' THEN 'Cache Object Counts'
        WHEN [counter_name] = 'Cache Objects in use' THEN 'Cache Objects in use'
        WHEN [counter_name] = 'Cache Pages' THEN 'Cache Pages'
    END AS [SQLServer:Plan Cache (Object Plans)],
    CASE
        WHEN [counter_name] = 'Cache Hit Ratio' THEN NULL
        ELSE FORMAT(cntr_value, '#,###')
    END AS [Counter Value],
    CASE
        WHEN [counter_name] = 'Cache Hit Ratio' THEN
            FORMAT(TRY_CONVERT(DECIMAL(5, 2), (cntr_value * 1.0 / NULLIF((SELECT cntr_value
        FROM sys.dm_os_performance_counters WHERE
        [object_name] LIKE '%:Plan Cache%' AND [counter_name] = 'Cache Hit Ratio Base'
        AND instance_name LIKE 'Object Plan%'), 0))), '0.00%')
    END AS [SQL Plan Cache Hit Ratio]
FROM sys.dm_os_performance_counters
WHERE [object_name] LIKE '%:Plan Cache%'
    AND [counter_name] IN ('Cache Hit Ratio', 'Cache Object Counts', 'Cache Objects in use', 'Cache Pages')
    AND instance_name LIKE 'Object Plan%'
ORDER BY [counter_name];

SELECT
    CASE
        WHEN [counter_name] = 'Batch Requests/sec' THEN 'Batch Requests/sec'
        WHEN [counter_name] = 'SQL Compilations/sec' THEN 'SQL Compilations/sec'
    END AS [SQLServer:SQL Statistics],
    FORMAT(cntr_value, '#,###') AS [Counter Value]
FROM sys.dm_os_performance_counters
WHERE [object_name] LIKE '%:SQL Statistics%'
AND counter_name IN ('Batch Requests/sec', 'SQL Compilations/sec'
);
```

#### Workaround

If your system continues to experience the symptoms that were described previously, after applying Cumulative Update 12 [KB5033663](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate12#2890724), the CE feedback feature can be disabled at the database level.

To reclaim the plan cache memory taken up by this issue, a restart of the SQL Server instance is required. This restart action can be taken after the CE feedback feature is disabled. To disable CE feedback at the database level, use the `CE_FEEDBACK` [database scoped configuration](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#ce_feedback---on--off-). For example, in the user database:

```sql
ALTER DATABASE SCOPED CONFIGURATION SET CE_FEEDBACK = OFF;
```

## Feedback and reporting issues

For feedback or questions, email [CEFfeedback@microsoft.com](mailto:CEFfeedback@microsoft.com)

## Related content

- [Cardinality Estimation Feedback in SQL Server 2022](https://www.microsoft.com/en-us/sql-server/blog/2022/12/01/cardinality-estimation-feedback-in-sql-server-2022)
- [Intelligent query processing in SQL databases](intelligent-query-processing.md)
- [Intelligent query processing features in detail](intelligent-query-processing-details.md)
- [Cardinality Estimation (SQL Server)](cardinality-estimation-sql-server.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Monitor and Tune for Performance](monitor-and-tune-for-performance.md)
- [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)
