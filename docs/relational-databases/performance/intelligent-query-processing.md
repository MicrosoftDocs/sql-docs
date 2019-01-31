---
title: "Intelligent query processing in Microsoft SQL databases | Microsoft Docs"
description: "Intelligent query processing features to improve query performance in SQL Server and Azure SQL Database."
ms.custom: ""
ms.date: 01/11/2019
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
author: "joesackmsft"
ms.author: "josack"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Intelligent query processing in SQL databases

[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

The intelligent query processing (QP) feature family includes features with broad impact. They improve the performance of existing workloads with minimal implementation effort. To automatically benefit from this feature family, move to the applicable database compatibility level.

![Intelligent query processing features](./media/3_IQPFeatureFamily.png)

## Adaptive query processing

The feature family for adaptive query processing includes query processing improvements that adapt optimization strategies to your application workload's runtime conditions. These improvements are included: 
- Batch mode adaptive joins.
- Memory grant feedback.
- Interleaved run for multi-statement table-valued functions (MSTVFs).

### Batch mode adaptive joins

With this feature, your plan can dynamically switch to a better join strategy during run by using a single cached plan.

For more information on batch mode adaptive joins, see [Adaptive query processing in SQL databases](../../relational-databases/performance/adaptive-query-processing.md).

### Row and batch mode memory grant feedback

> [!NOTE]
> Row mode memory grant feedback is a public preview feature.  

This feature recalculates the actual memory required for a query. Then it updates the grant value for the cached plan. It reduces excessive memory grants that impact concurrency. It also fixes underestimated memory grants that cause expensive spills to disk.

For more information on memory grant feedback, see [Adaptive query processing in SQL databases](../../relational-databases/performance/adaptive-query-processing.md).

### Interleaved run for MSTVFs

With interleaved run, the actual row counts from the function are used to make better-informed downstream query plan decisions. For more information on multi-statement table-valued functions (MSTVFs), see [Table-valued functions](../../relational-databases/user-defined-functions/create-user-defined-functions-database-engine.md#TVF).

For more information on interleaved run, see [Adaptive query processing in SQL databases](../../relational-databases/performance/adaptive-query-processing.md).

## Table variable deferred compilation

> [!NOTE]
> Table variable deferred compilation is a public preview feature.  

Table variable deferred compilation improves plan quality and overall performance for queries that reference table variables. During optimization and initial compilation, this feature propagates cardinality estimates that are based on actual table variable row counts. This accurate row count information optimizes downstream plan operations.

Table variable deferred compilation defers compilation of a statement that references a table variable until the first actual run of the statement. This deferred compilation behavior is the same as that of temporary tables. This change results in the use of actual cardinality instead of the original one-row guess. 

You can enable the public preview of table variable deferred compilation in Azure SQL Database. To do that, enable database compatibility level 150 for the database you're connected to when you run the query.

For more information, see [Table variable deferred compilation](../../t-sql/data-types/table-transact-sql.md#table-variable-deferred-compilation).

## Scalar UDF inlining

> [!NOTE]
> Scalar user-defined function (UDF) inlining is a public preview feature.  

Scalar UDF inlining automatically transforms [scalar UDFs](../../relational-databases/user-defined-functions/create-user-defined-functions-database-engine.md#Scalar) into relational expressions and embeds them in the calling SQL query. This transformation improves the performance of workloads that take advantage of scalar UDFs. Scalar UDF inlining facilitates cost-based optimization of operations inside UDFs. It results in efficient, set-oriented, parallel plans instead of inefficient, iterative, serial execution plans. This feature is enabled by default under database compatibility level 150.

For more information, see [Scalar UDF inlining](../user-defined-functions/scalar-udf-inlining.md).

## Approximate query processing

> [!NOTE]
> **APPROX_COUNT_DISTINCT** is a public preview feature.  

Approximate query processing is a new feature family that provides aggregations across large datasets where responsiveness is more critical than absolute precision. An example is calculating a **COUNT(DISTINCT())** across 10 billion rows, for display on a dashboard. In this case, absolute precision isn't important, but responsiveness is critical. The new **APPROX_COUNT_DISTINCT** aggregate function returns the approximate number of unique non-null values in a group.

For more information, see [APPROX_COUNT_DISTINCT (Transact-SQL)](../../t-sql/functions/approx-count-distinct-transact-sql.md).

## Batch mode on rowstore 

> [!NOTE]
> Batch mode on rowstore is a public preview feature.  

### Background

[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] introduced a new feature to accelerate analytical workloads: columnstore indexes. We expanded the use cases and improved the performance of columnstore indexes in each subsequent release. Until now, we surfaced and documented all these capabilities as a single feature. You create columnstore indexes on your tables. And your analytical workload goes faster. However, there are two related but distinct sets of technologies:
- With **Columnstore** indexes, analytical queries access only the data in the columns they need. Page compression in the columnstore format is also more effective than compression in traditional **rowstore** indexes. 
- With **Batch mode** processing, query operators process data more efficiently. They work on a batch of rows instead of one row at a time. A number of other scalability improvements are tied to batch mode processing. For more information on batch mode, see [Execution modes](../../relational-databases/query-processing-architecture-guide.md#execution-modes).

The two sets of features work together to improve input/output (I/O) and CPU utilization:
- By using columnstore indexes, more of your data fits in memory. That reduces the need for I/O.
- Batch mode processing uses CPU more efficiently.

The two technologies take advantage of each other whenever possible. For example, batch mode aggregates can be evaluated as part of a columnstore index scan. We also process columnstore data that's compressed by using run-length encoding much more efficiently with batch mode joins and batch mode aggregates. 
 
The two features are independently usable:
* You get row mode plans that use columnstore indexes.
* You get batch mode plans that use only rowstore indexes. 

In most cases, you get the best results when you use the two features together. Until now, SQL's query optimizer considered batch mode processing only for queries that involve at least one table with a columnstore index.

For some applications, columnstore indexes aren't a viable option. The application uses some other feature that isn't supported with columnstore indexes. For example, triggers aren't supported on tables with clustered columnstore indexes. In-place modifications aren't compatible with columnstore compression. More importantly, columnstore indexes add overhead for **DELETE** and **UPDATE** statements. 

For some hybrid transactional-analytical workloads, the overhead on a workload's transactional aspects outweighs the benefits of columnstore indexes. Such scenarios can improve CPU utilization from batch mode processing alone. That's why the batch mode on rowstore feature considers batch mode for all queries, regardless of the indexes involved.

### Workloads that might benefit from batch mode on rowstore

The following workloads might benefit from batch mode on rowstore:
1. A significant part of the workload consists of analytical queries. Usually, these queries have operators like joins or aggregates that process hundreds of thousands of rows or more, **and**
2. The workload is CPU bound. If the bottleneck is I/O, we still recommend that you consider a columnstore index, if possible, **and**
3. Creating a columnstore index adds too much overhead to the transactional part of your workload, **or**
4. Creating a columnstore index isn't feasible because your application depends on a feature that's not yet supported with columnstore indexes.

> [!NOTE]
> Batch mode on rowstore helps only by reducing CPU consumption. If your bottleneck is I/O-related, and data isn't already cached, **cold cache**, batch mode on rowstore won't improve elapsed time. Similarly, if there isn't enough memory on the machine to cache all the data, a performance improvement is unlikely.

### What changes with batch mode on rowstore?

Other than moving to compatibility level 150, you don't have to change anything on your side to enable batch mode on rowstore for candidate workloads.

Even if a query doesn't involve any table with a columnstore index, the query processor now uses heuristics to decide whether to consider batch mode. The heuristics consist of these checks:
1. An initial check of table sizes, operators used, and estimated cardinalities in the input query.
2. Additional checkpoints, as the optimizer discovers new, cheaper plans for the query. If these alternative plans don't make significant use of batch mode, the optimizer stops exploring batch mode alternatives.

If batch mode on rowstore is used, in the query run plan, you see the actual run mode as **batch mode**. It's used by the scan operator for on-disk heaps and B-tree indexes. This batch mode scan can evaluate batch mode bitmap filters. You might also see other batch mode operators in the plan. Examples are hash joins, hash-based aggregates, sorts, window aggregates, filters, concatenation, and compute scalar operators.

### Remarks

1. Query plans don't always use batch mode. The query optimizer might decide that batch mode isn't beneficial for the query. 
2. The query optimizer's search space is changing. So if you get a row mode plan, it might not be the same as the plan you get in a lower compatibility level. And if you get a batch mode plan, it might not be the same as the plan you get with a columnstore index. 
3. Plans might also change for queries that mix columnstore and rowstore indexes because of the new batch mode rowstore scan.
4. There are current limitations for the new batch mode on rowstore scan: 
    * It doesn't kick in for in-memory OLTP tables or for any index other than on-disk heaps and B-trees. 
    * It also won't kick in if a large object (LOB) column is fetched or filtered. This limitation includes sparse column sets and XML columns.
5. There are queries that batch mode isn't used for even with columnstore indexes. Examples are queries that involve cursors. These same exclusions also extend to batch mode on rowstore.

### Configure batch mode on rowstore

The **BATCH_MODE_ON_ROWSTORE** database scoped configuration is on by default. It disables batch mode on rowstore without requiring a change in database compatibility level:

```sql
-- Disabling batch mode on rowstore
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_ON_ROWSTORE = OFF;

-- Enabling batch mode on rowstore
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_ON_ROWSTORE = ON;
```

You can disable batch mode on rowstore via database scoped configuration. But you can still override the setting at the query level by using the **ALLOW_BATCH_MODE** query hint. The following example enables batch mode on rowstore even with the feature disabled via database scoped configuration:

```sql
SELECT [Tax Rate], [Lineage Key], [Salesperson Key], SUM(Quantity) AS SUM_QTY, SUM([Unit Price]) AS SUM_BASE_PRICE, COUNT(*) AS COUNT_ORDER
FROM Fact.OrderHistoryExtended
WHERE [Order Date Key]<=DATEADD(dd, -73, '2015-11-13')
GROUP BY [Tax Rate], [Lineage Key], [Salesperson Key]
ORDER BY [Tax Rate], [Lineage Key], [Salesperson Key]
OPTION(RECOMPILE, USE HINT('ALLOW_BATCH_MODE'));
```

You can also disable batch mode on rowstore for a specific query by using the **DISALLOW_BATCH_MODE** query hint. An example is as follows:

```sql
SELECT [Tax Rate], [Lineage Key], [Salesperson Key], SUM(Quantity) AS SUM_QTY, SUM([Unit Price]) AS SUM_BASE_PRICE, COUNT(*) AS COUNT_ORDER
FROM Fact.OrderHistoryExtended
WHERE [Order Date Key]<=DATEADD(dd, -73, '2015-11-13')
GROUP BY [Tax Rate], [Lineage Key], [Salesperson Key]
ORDER BY [Tax Rate], [Lineage Key], [Salesperson Key]
OPTION(RECOMPILE, USE HINT('DISALLOW_BATCH_MODE'));
```

## See also

[Performance Center for SQL Server Database Engine and Azure SQL Database](../../relational-databases/performance/performance-center-for-sql-server-database-engine-and-azure-sql-database.md)     
[Query processing architecture guide](../../relational-databases/query-processing-architecture-guide.md)    
[Showplan logical and physical operators reference](../../relational-databases/showplan-logical-and-physical-operators-reference.md)    
[Joins](../../relational-databases/performance/joins.md)    
[Demonstrating adaptive query processing](https://github.com/joesackmsft/Conferences/blob/master/Data_AMP_Detroit_2017/Demos/AQP_Demo_ReadMe.md)       
[Demonstrating intelligent QP](https://github.com/joesackmsft/Conferences/blob/master/IQPDemos/IQP_Demo_ReadMe.md)   
