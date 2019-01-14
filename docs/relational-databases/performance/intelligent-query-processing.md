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

The **Intelligent query processing** feature family includes features with broad impact that improve the performance of existing workloads with minimal implementation effort.

![Intelligent Query Processing Features](./media/3_IQPFeatureFamily2.png)

## Adaptive Query Processing

The adaptive query processing feature family includes query processing improvements that adapt optimization strategies to your application workload's runtime conditions. These improvements included: 
- Batch mode adaptive joins
- Memory grant feedback
- Interleaved execution for multi-statement table-valued functions (MSTVFs)

### Batch mode adaptive joins

This feature allows your plan to dynamically switch to a better join strategy during execution using a single cached plan.

For more information on batch mode adaptive joins, see [Adaptive query processing in SQL databases](../../relational-databases/performance/adaptive-query-processing.md).

### Row and batch mode memory grant feedback

> [!NOTE]
> Row mode memory grant feedback is a public preview feature.  

This feature recalculates the actual memory required for a query and then updates the grant value for the cached plan, reducing excessive memory grants that impact concurrency and fixing underestimated memory grants that cause expensive spills to disk.

For more information on memory grant feedback, see [Adaptive query processing in SQL databases](../../relational-databases/performance/adaptive-query-processing.md).

### Interleaved execution for multi-statement table-valued functions (MSTVFs)

With interleaved execution, the actual row counts from the function are used to make better-informed downstream query plan decisions. For more information on multi-statement table-valued functions (MSTVFs), see [Table-Valued Functions](../../relational-databases/user-defined-functions/create-user-defined-functions-database-engine.md#TVF).

For more information on interleaved execution, see [Adaptive query processing in SQL databases](../../relational-databases/performance/adaptive-query-processing.md).

## Table variable deferred compilation

> [!NOTE]
> Table variable deferred compilation is a public preview feature.  

Table variable deferred compilation improves plan quality and overall performance for queries referencing table variables. During optimization and initial compilation, this feature will propagate cardinality estimates that are based on actual table variable row counts.  This accurate row count information will be used for optimizing downstream plan operations.

With table variable deferred compilation, compilation of a statement that references a table variable is deferred until the first actual execution of the statement. This deferred compilation behavior is identical to the behavior of temporary tables, and this change results in the use of actual cardinality instead of the original one-row guess. To enable the public preview of table variable deferred compilation in Azure SQL Database, enable database compatibility level 150 for the database you are connected to when executing the query.

For more information, see [Table variable deferred compilation](../../t-sql/data-types/table-transact-sql.md#table-variable-deferred-compilation ).

## Scalar UDF inlining

> [!NOTE]
> Scalar UDF inlining is a public preview feature.  

Scalar UDF inlining automatically transforms [scalar user-defined functions (UDF)](../../relational-databases/user-defined-functions/create-user-defined-functions-database-engine.md#Scalar) into relational expressions and embeds them in the calling SQL query, thereby improving the performance of workloads that leverage scalar UDFs. Scalar UDF inlining facilitates cost-based optimization of operations inside UDFs, and results in efficient plans that are set-oriented and parallel as opposed to inefficient, iterative, serial execution plans. This feature is enabled by default under database compatibility level 150.

For more information, see [Scalar UDF Inlining](../user-defined-functions/scalar-udf-inlining.md).

## Approximate query processing

> [!NOTE]
> APPROX_COUNT_DISTINCT is a public preview feature.  

Approximate query processing is a new family of features that are designed to provide aggregations across large data sets where responsiveness is more critical than absolute precision.  An example might be calculating a COUNT(DISTINCT()) across 10 billion rows, for display on a dashboard.  In this case, absolute precision is not important, but responsiveness is critical. The new APPROX_COUNT_DISTINCT aggregate function returns the approximate number of unique non-null values in a group.

For more information, see [APPROX_COUNT_DISTINCT (Transact-SQL)](../../t-sql/functions/approx-count-distinct-transact-sql.md).

## Batch Mode on Rowstore 

> [!NOTE]
> Batch Mode on Rowstore is a public preview feature.  

### Background

[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] introduced a new feature for accelerating analytical workloads: columnstore indexes. We have expanded the use cases and improved the performance of columnstore indexes in each subsequent release. Until now, we have surfaced and documented all these capabilities as a single feature: You create columnstore indexes on your tables, and your analytical workload "just goes faster". Under the covers, however, there are two related but distinct sets of technologies:
- **Columnstore** indexes allow analytical queries to access only the data in the columns they need. The columnstore format also allows much more effective compression than what you get with page compression in traditional "rowstore" indexes. 
- **Batch mode** processing allows query operators to process data more efficiently by working on a batch of rows at a time, instead of one row at a time. A number of other scalability improvements are tied to batch mode processing. For more information on batch mode, see [Execution Modes](../../relational-databases/query-processing-architecture-guide.md#execution-modes).

The two sets of features work together to improve I/O and CPU utilization:
- Columnstore indexes allow more of your data to fit in memory, and thus reduce the need for I/O.
- Batch mode processing uses CPU more efficiently.

The two technologies take advantage of each other whenever possible. For example, batch mode aggregates can be evaluated as part of a columnstore index scan. We also process columnstore data compressed using run-length encoding much more efficiently with batch mode joins and batch mode aggregates. 
 
The two features are already usable independently: you can get row mode plans that use columnstore indexes, and you can get batch mode plans that use only rowstore indexes. But since in most cases you get the best results when the two features are used together, SQL's query optimizer has until now considered batch mode processing only for queries that involve at least one table with a columnstore index.

For some applications, columnstore indexes are not a viable option because the application uses some other feature that is not supported with columnstore indexes (triggers are not supported on tables with clustered columnstore indexes, for example). More importantly, columnstore indexes adds overhead for DELETE and UPDATE statements, because in-place modifications are not compatible with columnstore compression. For some hybrid transactional-analytical workloads, the benefit that columnstore indexes would bring for analytical queries is outweighed by the overhead on a workload's transactional aspects. Such scenarios can get improved CPU utilization from batch mode processing alone, which is why the **batch mode on rowstore** feature will consider batch mode for all queries, regardless of the indexes involved.

### What workloads may benefit from batch mode on rowstore

The following workloads may benefit from batch mode on rowstore:
1. A significant part of the workload consists of analytical queries (as a rule of thumb, queries with operators such as joins or aggregates processing hundreds of thousands of rows or more), **AND**
2. The workload is CPU bound (if the bottleneck is IO, it is still recommended to consider a columnstore index, if possible), **AND**
3. Creating a columnstore index adds too much overhead to the transactional part of your workload **OR** creating a columnstore index is not feasible because your application depends on a feature that is not yet supported with columnstore indexes.

> [!NOTE]
> Batch mode on rowstore can only help by reducing CPU consumption. If your bottleneck is IO-related, and data is not already cached ("cold" cache), batch mode on rowstore will NOT improve elapsed time. Similarly, if there is not enough memory on the machine to cache all data, a performance improvement is unlikely.

### What changes with batch mode on rowstore

Other than moving to compatibility level 150, you don't have to change anything on your side in order to enable batch mode on rowstore for candidate workloads.

Even if a query does not involve any table with a columnstore index, the query processor now uses heuristics to decide whether to consider batch mode. The heuristics consist of:
1. An initial check of table sizes, operators used, and estimated cardinalities in the input query.
2. Additional checkpoints, as the optimizer discovers new, cheaper plans for the query. If these alternative plans do not make significant use of batch mode, the optimizer will stop exploring batch mode alternatives.

If batch mode on rowstore is used, in the query execution plan you will see the actual execution mode as "batch mode" used by the scan operator for on-disk heaps and B-tree indexes.  This batch mode scan  can evaluate batch mode bitmap filters.  You may also see other batch mode operators in the plan, such as hash joins, hash-based aggregates, sorts, window aggregates, filters, concatenation, and compute scalar operators.

### Remarks

1. There is no guarantee that query plans will use batch mode. The query optimizer may decide that batch mode does not look beneficial for the query. 
2. As the query optimizer's search space is changing, there is no guarantee that if you get a row mode plan, it will be the same as the plan you get in a lower compatibility level. There is also no guarantee that if you get a batch mode plan, it will be the same as the plan you'd get with a columnstore index. 
3. Plans may also change in subtle ways for queries that mix columnstore and rowstore indexes, because of the new batch mode rowstore scan.
4. Current limitations for the new batch mode on rowstore scan: It will not kick in for in-memory OLTP tables, or for any index other than on-disk heaps and B-trees. It will also not kick in if a LOB column is fetched or filtered. This limitation includes sparse column sets, and XML columns.
5. There are queries for which batch mode is not used even with columnstore indexes (for example queries involving cursors), and these same exclusions extend to batch mode on rowstore as well.

### Configuring batch mode on rowstore

The BATCH_MODE_ON_ROWSTORE database scoped configuration is on by default and can be used to disable batch mode on rowstore without requiring a change in database compatibility level:

```sql
-- Disabling batch mode on rowstore
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_ON_ROWSTORE = OFF;

-- Enabling batch mode on rowstore
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_ON_ROWSTORE = ON;
```

You can disable batch mode on rowstore via database scoped configuration but still override the setting at the query level using the ALLOW_BATCH_MODE query hint. The following example enables batch mode on rowstore even with the feature disabled via database scoped configuration:

```sql
SELECT [Tax Rate], [Lineage Key], [Salesperson Key], SUM(Quantity) AS SUM_QTY, SUM([Unit Price]) AS SUM_BASE_PRICE, COUNT(*) AS COUNT_ORDER
FROM Fact.OrderHistoryExtended
WHERE [Order Date Key]<=DATEADD(dd, -73, '2015-11-13')
GROUP BY [Tax Rate], [Lineage Key], [Salesperson Key]
ORDER BY [Tax Rate], [Lineage Key], [Salesperson Key]
OPTION(RECOMPILE, USE HINT('ALLOW_BATCH_MODE'));
```

You can also disable batch mode on rowstore for a specific query by using the DISALLOW_BATCH_MODE query hint. For example:

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
[Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md)    
[Showplan Logical and Physical Operators Reference](../../relational-databases/showplan-logical-and-physical-operators-reference.md)    
[Joins](../../relational-databases/performance/joins.md)    
[Demonstrating Adaptive Query Processing](https://github.com/joesackmsft/Conferences/blob/master/Data_AMP_Detroit_2017/Demos/AQP_Demo_ReadMe.md)       
[Demonstrating Intelligent QP](https://github.com/joesackmsft/Conferences/blob/master/IQPDemos/IQP_Demo_ReadMe.md)   
