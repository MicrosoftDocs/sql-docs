---
title: "Intelligent query processing details"
description: "Intelligent query processing features described in detail."
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
author: "MikeRayMSFT"
ms.author: "mikeray"
ms.reviewer: "wiassaf"
ms.custom:
- seo-dt-2019
- event-tier1-build-2022
ms.date: 11/01/2022
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Intelligent query processing features in detail

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article contains in-depth descriptions of various [intelligent query processing (IQP)](intelligent-query-processing.md) features, release notes, and more detail. The intelligent query processing (IQP) feature family includes features with broad impact that improve the performance of existing workloads with minimal implementation effort to adopt. 

You can make workloads automatically eligible for intelligent query processing by enabling the applicable database compatibility level for the database. You can set this using [!INCLUDE[tsql](../../includes/tsql-md.md)]. For example, to set a database's compatibility level to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]:

```sql
ALTER DATABASE [WideWorldImportersDW] SET COMPATIBILITY_LEVEL = 160;
```

All [IQP features](intelligent-query-processing.md) are available in [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], sometimes depending on each database's compatibility mode. For more information on changes introduced with new versions, see:

- [What's new in SQL Server 2017](../../sql-server/what-s-new-in-sql-server-2017.md)
- [What's new in SQL Server 2019](../../sql-server/what-s-new-in-sql-server-2019.md)
- [What's new in SQL Server 2022](../../sql-server/what-s-new-in-sql-server-2022.md)

## Batch mode Adaptive Joins

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

The batch mode Adaptive Joins feature enables the choice of a [Hash Join or Nested Loops Join](../../relational-databases/performance/joins.md) method to be deferred until **after** the first input has been scanned, by using a single cached plan. The Adaptive Join operator defines a threshold that is used to decide when to switch to a Nested Loops plan. Your plan can therefore dynamically switch to a better join strategy during execution.

For more information, including how to disable Adaptive joins without changing the compatibility level, see [Understanding Adaptive joins](../../relational-databases/performance/joins.md#adaptive).

## Interleaved execution for MSTVFs

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

With interleaved execution, the actual row counts from the function are used to make better-informed downstream query plan decisions. For more information on multi-statement table-valued functions (MSTVFs), see [Table-valued functions](../../relational-databases/user-defined-functions/create-user-defined-functions-database-engine.md#TVF).

Interleaved execution changes the unidirectional boundary between the optimization and execution phases for a single-query execution and enables plans to adapt based on the revised cardinality estimates. During optimization if the database engine encounters a candidate for interleaved execution which uses **multi-statement table-valued functions (MSTVFs)**, optimization will pause, execute the applicable subtree, capture accurate cardinality estimates, and then resume optimization for downstream operations.

MSTVFs have a fixed cardinality guess of 100 starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], and 1 for earlier [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions. Interleaved execution helps workload performance issues that are due to these fixed cardinality estimates associated with MSTVFs. For more information on MSTVFs, see [Create User-defined Functions (Database Engine)](../../relational-databases/user-defined-functions/create-user-defined-functions-database-engine.md#TVF).

The following image depicts a [Live Query Statistics](../../relational-databases/performance/live-query-statistics.md) output, a subset of an overall execution plan that shows the impact of fixed cardinality estimates from MSTVFs

 You can see the actual row flow vs. estimated rows. There are three noteworthy areas of the plan (flow is from right to left):

- The MSTVF Table Scan has a fixed estimate of 100 rows. For this example, however, there are 527,597 rows flowing through this MSTVF Table Scan, as seen in Live Query Statistics via the *527597 of 100* actual of estimated - so the fixed estimate is significantly skewed.
- For the Nested Loops operation, only 100 rows are assumed to be returned by the outer side of the join. Given the high number of rows actually being returned by the MSTVF, you are likely better off with a different join algorithm altogether.
- For the Hash Match operation, notice the small warning symbol, which in this case is indicating a spill to disk.

:::image type="content" source="./media/7_AQPFlowThreeAreas.png" alt-text="Graphic of an execution plan row flow versus estimated rows." lightbox="./media/7_AQPFlowThreeAreas.png":::

Contrast the prior plan with the actual plan generated with interleaved execution enabled:

:::image type="content" source="./media/8_AQPInterleavedEnabledPlan.png" alt-text="Graphic of Interleaved execution plan." lightbox="./media/8_AQPInterleavedEnabledPlan.png":::

- Notice that the MSTVF table scan now reflects an accurate cardinality estimate. Also notice the reordering of this table scan and the other operations.
- And regarding join algorithms, we have switched from a Nested Loop operation to a Hash Match operation instead, which is more optimal given the large number of rows involved.
- Also notice that we no longer have spill-warnings, as we're granting more memory based on the true row count flowing from the MSTVF table scan.

### Interleaved execution eligible statements

MSTVF referencing statements in interleaved execution must currently be read-only and not part of a data modification operation. Also, MSTVFs are not eligible for interleaved execution if they do not use runtime constants.

### Interleaved execution benefits

In general, the higher the skew between the estimated vs. actual number of rows, coupled with the number of downstream plan operations, the greater the performance impact.

In general, interleaved execution benefits queries where:

- There is a large skew between the estimated vs. actual number of rows for the intermediate result set (in this case, the MSTVF).
- And the overall query is sensitive to a change in the size of the intermediate result. This typically happens when there is a complex tree above that subtree in the query plan.
A simple `SELECT *` from an MSTVF will not benefit from interleaved execution.

### Interleaved execution overhead

The overhead should be minimal-to-none. MSTVFs were already being materialized prior to the introduction of interleaved execution, however the difference is that now we're now allowing deferred optimization and are then leveraging the cardinality estimate of the materialized row set.
As with any plan affecting changes, some plans could change such that with better cardinality for the subtree we get a worse plan for the query overall. Mitigation can include reverting the compatibility level or using Query Store to force the non-regressed version of the plan.

### Interleaved execution and consecutive executions

Once an interleaved execution plan is cached, the plan with the revised estimates on the first execution is used for consecutive executions without reinstantiating interleaved execution.

### Tracking interleaved execution activity

You can see usage attributes in the actual query execution plan:

| Execution Plan attribute | Description |
| --- | --- |
| ContainsInterleavedExecutionCandidates | Applies to the *QueryPlan* node. When *true*, means the plan contains interleaved execution candidates. |
| IsInterleavedExecuted | Attribute of the *RuntimeInformation* element under the RelOp for the TVF node. When *true*, means the operation was materialized as part of an interleaved execution operation. |

You can also track interleaved execution occurrences via the following extended events:

| xEvent | Description |
| ---- | --- |
| `interleaved_exec_status` | This event fires when interleaved execution is occurring. |
| `interleaved_exec_stats_update` | This event describes the cardinality estimates updated by interleaved execution. |
| `Interleaved_exec_disabled_reason` | This event fires when a query with a possible candidate for interleaved execution does not actually get interleaved execution. |

A query must be executed in order to allow interleaved execution to revise MSTVF cardinality estimates. However, the estimated execution plan still shows when there are interleaved execution candidates via the `ContainsInterleavedExecutionCandidates` showplan attribute.

### Interleaved execution caching

If a plan is cleared or evicted from cache, upon query execution there is a fresh compilation that uses interleaved execution.
A statement using `OPTION (RECOMPILE)` will create a new plan using interleaved execution and not cache it.

### Interleaved execution and query store interoperability

Plans using interleaved execution can be forced. The plan is the version that has corrected cardinality estimates based on initial execution.

### Disabling interleaved execution without changing the compatibility level

Interleaved execution can be disabled at the database or statement scope while still maintaining database compatibility level 140 and higher.  To disable interleaved execution for all query executions originating from the database, execute the following within the context of the applicable database:

```sql
-- SQL Server 2017
ALTER DATABASE SCOPED CONFIGURATION SET DISABLE_INTERLEAVED_EXECUTION_TVF = ON;

-- Starting with SQL Server 2019, and in Azure SQL Database
ALTER DATABASE SCOPED CONFIGURATION SET INTERLEAVED_EXECUTION_TVF = OFF;
```

When enabled, this setting will appear as enabled in [sys.database_scoped_configurations](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md).
To re-enable interleaved execution for all query executions originating from the database, execute the following within the context of the applicable database:

```sql
-- SQL Server 2017
ALTER DATABASE SCOPED CONFIGURATION SET DISABLE_INTERLEAVED_EXECUTION_TVF = OFF;

-- Starting with SQL Server 2019, and in Azure SQL Database
ALTER DATABASE SCOPED CONFIGURATION SET INTERLEAVED_EXECUTION_TVF = ON;
```

You can also disable interleaved execution for a specific query by designating `DISABLE_INTERLEAVED_EXECUTION_TVF` as a [USE HINT query hint](../../t-sql/queries/hints-transact-sql-query.md#use_hint). For example:

```sql
SELECT [fo].[Order Key], [fo].[Quantity], [foo].[OutlierEventQuantity]
FROM [Fact].[Order] AS [fo]
INNER JOIN [Fact].[WhatIfOutlierEventQuantity]('Mild Recession',
                            '1-01-2013',
                            '10-15-2014') AS [foo] ON [fo].[Order Key] = [foo].[Order Key]
                            AND [fo].[City Key] = [foo].[City Key]
                            AND [fo].[Customer Key] = [foo].[Customer Key]
                            AND [fo].[Stock Item Key] = [foo].[Stock Item Key]
                            AND [fo].[Order Date Key] = [foo].[Order Date Key]
                            AND [fo].[Picked Date Key] = [foo].[Picked Date Key]
                            AND [fo].[Salesperson Key] = [foo].[Salesperson Key]
                            AND [fo].[Picker Key] = [foo].[Picker Key]
OPTION (USE HINT('DISABLE_INTERLEAVED_EXECUTION_TVF'));
```

A USE HINT query hint takes precedence over a [database scoped configuration](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md) or trace flag setting.

## Scalar UDF inlining

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

Scalar UDF inlining automatically transforms [scalar UDFs](../../relational-databases/user-defined-functions/create-user-defined-functions-database-engine.md#Scalar) into relational expressions. It embeds them in the calling SQL query. This transformation improves the performance of workloads that take advantage of scalar UDFs. Scalar UDF inlining facilitates cost-based optimization of operations inside UDFs. The results are efficient, set-oriented, and parallel instead of inefficient, iterative, serial execution plans. This feature is enabled by default under database compatibility level 150 or higher.

For more information, see [Scalar UDF inlining](../user-defined-functions/scalar-udf-inlining.md).

## Table variable deferred compilation

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

**Table variable deferred compilation** improves plan quality and overall performance for queries referencing table variables. During optimization and initial plan compilation, this feature will propagate cardinality estimates that are based on actual table variable row counts. This exact row count information will then be used for optimizing downstream plan operations.

With table variable deferred compilation, compilation of a statement that references a table variable is deferred until the first actual execution of the statement. This deferred compilation behavior is identical to the behavior of temporary tables. This change results in the use of actual cardinality instead of the original one-row guess.

To enable table variable deferred compilation, enable database compatibility level 150 or higher for the database you're connected to when the query runs.

Table variable deferred compilation **doesn't** change any other characteristics of table variables. For example, this feature doesn't add column statistics to table variables.

Table variable deferred compilation **doesn't increase recompilation frequency**. Rather, it shifts where the initial compilation occurs. The resulting cached plan generates based on the initial deferred compilation table variable row count. The cached plan is reused by consecutive queries. It's reused until the plan is evicted or recompiled.

Table variable row count that is used for initial plan compilation represents a typical value might be different from a fixed row count guess. If it's different, downstream operations will benefit. Performance may not be improved by this feature if the table variable row count varies significantly across executions.

### Disabling table variable deferred compilation without changing the compatibility level

Disable table variable deferred compilation at the database or statement scope while still maintaining database compatibility level 150 and higher. To disable table variable deferred compilation for all query executions originating from the database, execute the following example within the context of the applicable database:

```sql
ALTER DATABASE SCOPED CONFIGURATION SET DEFERRED_COMPILATION_TV = OFF;
```

To re-enable table variable deferred compilation for all query executions originating from the database, execute the following example within the context of the applicable database:

```sql
ALTER DATABASE SCOPED CONFIGURATION SET DEFERRED_COMPILATION_TV = ON;
```

You can also disable table variable deferred compilation for a specific query by assigning DISABLE_DEFERRED_COMPILATION_TV as a USE HINT query hint.  For example:

```sql
DECLARE @LINEITEMS TABLE 
    (L_OrderKey INT NOT NULL,
     L_Quantity INT NOT NULL
    );

INSERT @LINEITEMS
SELECT L_OrderKey, L_Quantity
FROM dbo.lineitem
WHERE L_Quantity = 5;

SELECT O_OrderKey,
    O_CustKey,
    O_OrderStatus,
    L_QUANTITY
FROM    
    ORDERS,
    @LINEITEMS
WHERE    O_ORDERKEY    =    L_ORDERKEY
    AND O_OrderStatus = 'O'
OPTION (USE HINT('DISABLE_DEFERRED_COMPILATION_TV'));
```

## Parameter Sensitivity Plan Optimization

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)])

Parameter Sensitivity Plan (PSP) optimization is part of the Intelligent query processing family of features. It addresses the scenario where a single cached plan for a parameterized query is not optimal for all possible incoming parameter values. This is the case with non-uniform data distributions. For more information on PSP optimization, see [Parameter Sensitive Plan optimization](parameter-sensitivity-plan-optimization.md). For more information on surrounding topics of parameterization and parameter sensitivity, see [Parameter Sensitivity](../query-processing-architecture-guide.md#parameter-sensitivity) and [Parameters and Execution Plan Reuse](../query-processing-architecture-guide.md#parameters-and-execution-plan-reuse).

## Approximate query processing
Approximate query processing is a new feature family. It aggregates across large datasets where responsiveness is more critical than absolute precision. An example is calculating a `COUNT(DISTINCT())` across 10 billion rows, for display on a dashboard. In this case, absolute precision isn't important, but responsiveness is critical.

### Approximate Count Distinct

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

The new **APPROX_COUNT_DISTINCT** aggregate function returns the approximate number of unique non-null values in a group.

This feature is available starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], regardless of the compatibility level.

For more information, see [APPROX_COUNT_DISTINCT (Transact-SQL)](../../t-sql/functions/approx-count-distinct-transact-sql.md).

### Approximate Percentile
**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

These aggregate functions compute percentiles for a large dataset with acceptable rank-based error bounds to help make rapid decisions by using approximate percentile aggregate functions.

For more information, see [APPROX_PERCENTILE_DISC (Transact-SQL)](../../t-sql/functions/approx-percentile-disc-transact-sql.md) and [APPROX_PERCENTILE_CONT (Transact-SQL)](../../t-sql/functions/approx-percentile-cont-transact-sql.md) 

## Batch mode on rowstore

**Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

Batch mode on rowstore enables batch mode execution for analytic workloads without requiring columnstore indexes.  This feature supports batch mode execution and bitmap filters for on-disk heaps and B-tree indexes. Batch mode on rowstore enables support for all existing batch mode-enabled operators.

[!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]

### Background

[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] introduced a new feature to accelerate analytical workloads: columnstore indexes. The use cases and performance of columnstore indexes increased in each subsequent release of SQL Server. Creating columnstore indexes on tables can improve performance for analytical workloads. However, there are two related but distinct sets of technologies:

- With **columnstore** indexes, analytical queries access only the data in the columns they need. Page compression in the columnstore format is also more effective than compression in traditional **rowstore** indexes.
- With **batch mode** processing, query operators process data more efficiently. They work on a batch of rows instead of one row at a time. A number of other scalability improvements are tied to batch mode processing. For more information on batch mode, see [Execution modes](../../relational-databases/query-processing-architecture-guide.md#execution-modes).

The two sets of features work together to improve input/output (I/O) and CPU utilization:

- By using columnstore indexes, more of your data fits in memory. That reduces the I/O workload.
- Batch mode processing uses CPU more efficiently.

The two technologies take advantage of each other whenever possible. For example, batch mode aggregates can be evaluated as part of a columnstore index scan. Also columnstore data that's compressed is processed by using run-length encoding much more efficiently with batch mode joins and batch mode aggregates.

It is important to understand however, that the two features are independent:

- You can get row mode plans that use columnstore indexes.
- You can get batch mode plans that use only rowstore indexes.

You usually get the best results when you use the two features together. Before [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], the SQL Server query optimizer considered batch mode processing only for queries that involve at least one table with a columnstore index.

Columnstore indexes may not be appropriate for some applications. An application might use some other feature that isn't supported with columnstore indexes. For example, in-place modifications are not compatible with columnstore compression. Therefore, triggers aren't supported on tables with clustered columnstore indexes. More importantly, columnstore indexes add overhead for **DELETE** and **UPDATE** statements.

For some hybrid transactional-analytical workloads, the overhead of a transactional workload outweighs the benefits gained from using columnstore indexes. Such scenarios can benefit from improved CPU usage by employing batch mode processing alone. That is why the batch-mode-on-rowstore feature considers batch mode for all queries regardless of what type of indexes are involved.

### Workloads that might benefit from batch mode on rowstore

The following workloads might benefit from batch mode on rowstore:

- A significant part of the workload consists of analytical queries. Usually, these queries use operators like joins or aggregates that process hundreds of thousands of rows or more.
- The workload is CPU bound. If the bottleneck is I/O, it is still recommended that you consider a columnstore index, where possible.
- Creating a columnstore index adds too much overhead to the transactional part of your workload. Or, creating a columnstore index is not feasible because your application depends on a feature that's not yet supported with columnstore indexes.

> [!NOTE]
> Batch mode on rowstore helps only by reducing CPU consumption. If your bottleneck is I/O-related, and data isn't already cached ("cold" cache), batch mode on rowstore will not improve query elapsed time. Similarly, if there is no sufficient memory on the machine to cache all data, a performance improvement is unlikely.

### What changes with batch mode on rowstore?

Batch mode on rowstore requires database to compatibility level 150.

Even if a query does not access any tables with columnstore indexes, the query processor, using heuristics, will decide whether to consider batch mode. The heuristics consist of these checks:

1. An initial check of table sizes, operators used, and estimated cardinalities in the input query.
2. Additional checkpoints, as the optimizer discovers new, cheaper plans for the query. If these alternative plans don't make significant use of batch mode, the optimizer stops exploring batch mode alternatives.

If batch mode on rowstore is used, you see the actual run mode as **batch mode** in the query plan. The scan operator uses batch mode for on-disk heaps and B-tree indexes. This batch mode scan can evaluate batch mode bitmap filters. You might also see other batch mode operators in the plan. Examples are hash joins, hash-based aggregates, sorts, window aggregates, filters, concatenation, and compute scalar operators.

### Remarks

Query plans don't always use batch mode. The Query Optimizer might decide that batch mode isn't beneficial for the query.

The Query Optimizer search space is changing. So if you get a row mode plan, it might not be the same as the plan you get in a lower compatibility level. And if you get a batch mode plan, it might not be the same as the plan you get with a columnstore index.

Plans might also change for queries that mix columnstore and rowstore indexes because of the new batch mode rowstore scan.

There are current limitations for the new batch mode on rowstore scan:

- It won't kick in for in-memory OLTP tables or for any index other than on-disk heaps and B-trees.
- It also won't kick in if a large object (LOB) column is fetched or filtered. This limitation includes sparse column sets and XML columns.

There are queries that batch mode isn't used for even with columnstore indexes. Examples are queries that involve cursors. These same exclusions also extend to batch mode on rowstore.

### Configure batch mode on rowstore

The `BATCH_MODE_ON_ROWSTORE` [database scoped configuration](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#batch_mode_on_rowstore---on--off-) is ON by default.

You can disable batch mode on rowstore without changing the database compatibility level:

```sql
-- Disabling batch mode on rowstore
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_ON_ROWSTORE = OFF;

-- Enabling batch mode on rowstore
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_ON_ROWSTORE = ON;
```

You can disable batch mode on rowstore via [database scoped configuration](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md). But you can still override the setting at the query level by using the `ALLOW_BATCH_MODE` query hint. The following example enables batch mode on rowstore even with the feature disabled via database scoped configuration:

```sql
SELECT [Tax Rate], [Lineage Key], [Salesperson Key], SUM(Quantity) AS SUM_QTY, SUM([Unit Price]) AS SUM_BASE_PRICE, COUNT(*) AS COUNT_ORDER
FROM Fact.OrderHistoryExtended
WHERE [Order Date Key]<=DATEADD(dd, -73, '2015-11-13')
GROUP BY [Tax Rate], [Lineage Key], [Salesperson Key]
ORDER BY [Tax Rate], [Lineage Key], [Salesperson Key]
OPTION(RECOMPILE, USE HINT('ALLOW_BATCH_MODE'));
```

You can also disable batch mode on rowstore for a specific query by using the `DISALLOW_BATCH_MODE` query hint. See the following example:

```sql
SELECT [Tax Rate], [Lineage Key], [Salesperson Key], SUM(Quantity) AS SUM_QTY, SUM([Unit Price]) AS SUM_BASE_PRICE, COUNT(*) AS COUNT_ORDER
FROM Fact.OrderHistoryExtended
WHERE [Order Date Key]<=DATEADD(dd, -73, '2015-11-13')
GROUP BY [Tax Rate], [Lineage Key], [Salesperson Key]
ORDER BY [Tax Rate], [Lineage Key], [Salesperson Key]
OPTION(RECOMPILE, USE HINT('DISALLOW_BATCH_MODE'));
```

## Memory grant feedback

### Batch mode memory grant feedback

For information about Batch mode memory grant feedback, visit [Batch mode memory grant feedback](intelligent-query-processing-feedback.md#batch-mode-memory-grant-feedback).

### Row mode memory grant feedback

For information about row mode memory grant feedback, visit [Row mode memory grant feedback.](intelligent-query-processing-feedback.md#row-mode-memory-grant-feedback).

### Percentile and persistence mode memory grant feedback

For information about percentile and persistence mode memory grant feedback, visit [Percentile and persistence mode memory grant feedback](intelligent-query-processing-feedback.md#percentile-and-persistence-mode-memory-grant-feedback).

## <a id="dop-feedback"></a> Degree of parallelism (DOP) feedback

For information about DOP feedback, visit [Degree of parallelism (DOP) feedback](intelligent-query-processing-feedback.md#degree-of-parallelism-dop-feedback).

## Cardinality estimation (CE) feedback

For information about CE feedback, visit [Cardinality estimation (CE) feedback](intelligent-query-processing-feedback.md#cardinality-estimation-ce-feedback).

## Optimized plan forcing with Query Store

For information about optimized plan forcing with Query Store, visit [Optimized plan forcing with Query Store](optimized-plan-forcing-query-store.md).

## See also

- [Joins](../../relational-databases/performance/joins.md)
- [Execution modes](../../relational-databases/query-processing-architecture-guide.md#execution-modes)
- [Query processing architecture guide](../../relational-databases/query-processing-architecture-guide.md)
- [Showplan logical and physical operators reference](../../relational-databases/showplan-logical-and-physical-operators-reference.md)
- [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)
- [What's new in SQL Server 2017](../../sql-server/what-s-new-in-sql-server-2017.md)
- [What's new in SQL Server 2019](../../sql-server/what-s-new-in-sql-server-2019.md)
- [What's new in SQL Server 2022](../../sql-server/what-s-new-in-sql-server-2022.md)

## Next steps

- [Batch mode memory grant feedback](intelligent-query-processing-feedback.md#batch-mode-memory-grant-feedback)
- [Row mode memory grant feedback](intelligent-query-processing-feedback.md#row-mode-memory-grant-feedback)
- [Percentile and persistence mode memory grant feedback](intelligent-query-processing-feedback.md#percentile-and-persistence-mode-memory-grant-feedback)
- [Parameter Sensitivity Plan Optimization](parameter-sensitivity-plan-optimization.md)
- [Demonstrating Intelligent Query Processing](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/intelligent-query-processing)
- [Constant Folding and Expression Evaluation](../query-processing-architecture-guide.md#constant-folding-and-expression-evaluation)
- [Intelligent query processing demos on GitHub](https://aka.ms/IQPDemos)
- [Performance Center for SQL Server Database Engine and Azure SQL Database](../../relational-databases/performance/performance-center-for-sql-server-database-engine-and-azure-sql-database.md)
- [Monitor performance by using the Query Store](monitoring-performance-by-using-the-query-store.md)
- [Best practices with Query Store](best-practice-with-the-query-store.md)

