---
title: "Intelligent query processing"
description: "Intelligent query processing features to improve query performance in SQL Server, Azure SQL Managed Instance, and Azure SQL Database."
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
ms.date: 10/12/2022
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Intelligent query processing in SQL databases

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The intelligent query processing (IQP) feature family includes features with broad impact that improve the performance of existing workloads with minimal implementation effort to adopt. The following graphic details the family of IQP features and when they were first introduced for SQL Server. All IQP features are available in [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. Some features depend on the database's compatibility level.

:::image type="content" source="./media/iqp-feature-family.svg" alt-text="A diagram of the Intelligent Query Processing family of features and when they were first introduced to SQL Server.":::

Watch this 6-minute video for an overview of intelligent query processing:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Overview-Intelligent-Query-processing-in-SQL-Server-2019/player?WT.mc_id=dataexposed-c9-niner]

For demos and sample code of intelligent query processing (IQP) features on GitHub, visit [https://aka.ms/IQPDemos](https://aka.ms/IQPDemos).

You can make workloads automatically eligible for intelligent query processing by enabling the applicable database compatibility level for the database. You can set this using [!INCLUDE[tsql](../../includes/tsql-md.md)]. For example:

```sql
ALTER DATABASE [WideWorldImportersDW] SET COMPATIBILITY_LEVEL = 150;
```

The following table details all intelligent query processing features, along with any requirement they have for database compatibility level. For complete details on all IQP features, including release notes and more in-depth descriptions, see [Intelligent query processing (IQP) feature details](intelligent-query-processing-details.md).

## <a id="sql2022"></a> IQP features for [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)], [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]

| **IQP Feature** | **Supported in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]** | **Supported in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]** |**Description** |
| ---------------- | ------- | ------- | ---------------- |
| [Adaptive Joins (Batch Mode)](intelligent-query-processing-details.md#batch-mode-adaptive-joins) | Yes, starting with database compatibility level 140| Yes, starting in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] with database compatibility level 140|Adaptive joins dynamically select a join type during runtime based on actual input rows.|
| [Approximate Count Distinct](intelligent-query-processing-details.md#approximate-query-processing) | Yes| Yes, starting in [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]|Provide approximate COUNT DISTINCT for big data scenarios with the benefit of high performance and a low memory footprint. |
| [Approximate Percentile](intelligent-query-processing-details.md#approximate-query-processing) | Yes, starting with database compatibility level 110| Yes, starting in [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)] with compatibility level 110|Quickly compute percentiles for a large dataset with acceptable rank-based error bounds to help make rapid decisions by using approximate percentile aggregate functions.|
| [Batch Mode on Rowstore](intelligent-query-processing-details.md#batch-mode-on-rowstore) | Yes, starting with database compatibility level 150| Yes, starting in [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] with compatibility level 150|Provide batch mode for CPU-bound relational DW workloads without requiring columnstore indexes.  |
| [Cardinality estimation (CE) feedback](intelligent-query-processing-feedback.md#cardinality-estimation-ce-feedback) | Yes, in Preview, starting with database compatibility level 160 | Yes, starting in [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)] with compatibility level 160 | Automatically adjusts cardinality estimates for repeating queries to optimize workloads where inefficient CE assumptions cause poor query performance. CE feedback will identify and use a model assumption that better fits a given query and data distribution to improve query execution plan quality. |
| [Degrees of Parallelism (DOP) feedback](intelligent-query-processing-details.md#dop-feedback) | No <!--Yes, starting with database compatibility level 160--> | Yes, starting in [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)] with compatibility level 160|Automatically adjusts degree of parallelism for repeating queries to optimize for workloads where inefficient parallelism can cause performance issues. Requires Query Store to be enabled.|
| [Interleaved Execution](intelligent-query-processing-details.md#interleaved-execution-for-mstvfs) | Yes, starting with database compatibility level 140| Yes, starting in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] with database compatibility level 140|Uses the actual cardinality of the multi-statement table valued function encountered on first compilation instead of a fixed guess.|
| [Memory grant feedback (Batch Mode)](intelligent-query-processing-feedback.md#batch-mode-memory-grant-feedback) | Yes, starting with database compatibility level 140| Yes, starting in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] with database compatibility level 140|If a batch mode query has operations that spill to disk, add more memory for consecutive executions. If a query wastes > 50% of the memory allocated to it, reduce the memory grant size for consecutive executions.|
| [Memory grant feedback (Row Mode)](intelligent-query-processing-feedback.md#row-mode-memory-grant-feedback) | Yes, starting with database compatibility level 150| Yes, starting in [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] with database compatibility level 150|If a row mode query has operations that spill to disk, add more memory for consecutive executions. If a query wastes > 50% of the memory allocated to it, reduce the memory grant size for consecutive executions.|
| [Memory grant feedback (Percentile)](intelligent-query-processing-feedback.md#percentile-and-persistence-mode-memory-grant-feedback) | No <!--Yes, starting with database compatibility level 160-->| Yes, starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)]) with database compatibility level 140 | Addresses existing limitations of memory grant feedback in a non-intrusive way by incorporating past query execution to refine feedback. |
| [Memory Grant, CE, and DOP feedback persistence](intelligent-query-processing-feedback.md#percentile-and-persistence-mode-memory-grant-feedback) | No <!--Yes, starting with database compatibility level 160-->| Yes, starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)]) with database compatibility level 140 | Provides new functionality to persist memory grant feedback. CE and DOP feedback is always persisted. Requires Query Store to be enabled for the database and in READ_WRITE mode. |
| [Optimized plan forcing](optimized-plan-forcing-query-store.md) | No <!--Yes, starting with database compatibility level 160--> | Yes, starting with [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)]). | Reduces compilation overhead for repeating forced queries. For more information, see [Optimized plan forcing with Query Store](optimized-plan-forcing-query-store.md). |
| [Scalar UDF Inlining](intelligent-query-processing-details.md#scalar-udf-inlining) | Yes, starting with database compatibility level 150 | Yes, starting in [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] with database compatibility level 150|Scalar UDFs are transformed into equivalent relational expressions that are "inlined" into the calling query, often resulting in significant performance gains.|
| [Parameter Sensitivity Plan Optimization](parameter-sensitivity-plan-optimization.md) | No <!--Yes, starting with database compatibility level 160-->| Yes, (Starting in [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)]) with database compatibility level 160 | Parameter Sensitivity Plan Optimization addresses the scenario where a single cached plan for a parameterized query is not optimal for all possible incoming parameter values, for example non-uniform data distributions. |
| [Table Variable Deferred Compilation](intelligent-query-processing-details.md#table-variable-deferred-compilation) | Yes, starting with database compatibility level 150 | Yes, starting in [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] with database compatibility level 150 | Uses the actual cardinality of the table variable encountered on first compilation instead of a fixed guess.|

## <a id="sql2019"></a> IQP features for [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)]

| **IQP Feature** | **Supported in [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)]** |**Description** |
| ---------------- | ------- | ------- |
| [Adaptive Joins (Batch Mode)](intelligent-query-processing-details.md#batch-mode-adaptive-joins) | Yes, starting in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] with database compatibility level 140|Adaptive joins dynamically select a join type during runtime based on actual input rows.|
| [Approximate Count Distinct](intelligent-query-processing-details.md#approximate-query-processing) | Yes| Provide approximate COUNT DISTINCT for big data scenarios with the benefit of high performance and a low memory footprint. |
| [Batch Mode on Rowstore](intelligent-query-processing-details.md#batch-mode-on-rowstore) | Yes, starting with database compatibility level 150| Provide batch mode for CPU-bound relational DW workloads without requiring columnstore indexes.  |
| [Interleaved Execution](intelligent-query-processing-details.md#interleaved-execution-for-mstvfs) | Yes, starting with database compatibility level 140| Use the actual cardinality of the multi-statement table valued function encountered on first compilation instead of a fixed guess.|
| [Memory grant feedback (Batch Mode)](intelligent-query-processing-feedback.md#batch-mode-memory-grant-feedback) | Yes, starting with database compatibility level 140| If a batch mode query has operations that spill to disk, add more memory for consecutive executions. If a query wastes > 50% of the memory allocated to it, reduce the memory grant size for consecutive executions.|
| [Memory grant feedback (Row Mode)](intelligent-query-processing-feedback.md#row-mode-memory-grant-feedback) | Yes, starting with database compatibility level 150| If a row mode query has operations that spill to disk, add more memory for consecutive executions. If a query wastes > 50% of the memory allocated to it, reduce the memory grant size for consecutive executions.|
| [Scalar UDF Inlining](intelligent-query-processing-details.md#scalar-udf-inlining) | Yes, starting with database compatibility level 150 | Scalar UDFs are transformed into equivalent relational expressions that are "inlined" into the calling query, often resulting in significant performance gains.|
| [Table Variable Deferred Compilation](intelligent-query-processing-details.md#table-variable-deferred-compilation) | Yes, starting with database compatibility level 150 | Use the actual cardinality of the table variable encountered on first compilation instead of a fixed guess.|

## <a id="sql2019"></a> IQP features for [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)]

| **IQP Feature** | **Supported in [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)]** |**Description** |
| ---------------- | ------- | ------- | 
| [Adaptive Joins (Batch Mode)](intelligent-query-processing-details.md#batch-mode-adaptive-joins) | Yes, starting in [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] with database compatibility level 140|Adaptive joins dynamically select a join type during runtime based on actual input rows.|
| [Approximate Count Distinct](intelligent-query-processing-details.md#approximate-query-processing) | Yes| Provide approximate COUNT DISTINCT for big data scenarios with the benefit of high performance and a low memory footprint. |
| [Interleaved Execution](intelligent-query-processing-details.md#interleaved-execution-for-mstvfs) | Yes, starting with database compatibility level 140| Use the actual cardinality of the multi-statement table valued function encountered on first compilation instead of a fixed guess.|
| [Memory grant feedback (Batch Mode)](intelligent-query-processing-feedback.md#batch-mode-memory-grant-feedback) | Yes, starting with database compatibility level 140| If a batch mode query has operations that spill to disk, add more memory for consecutive executions. If a query wastes > 50% of the memory allocated to it, reduce the memory grant size for consecutive executions.|


## Query Store requirement

Several of the suite of [intelligent query processing features](intelligent-query-processing.md) require the [Query Store](monitoring-performance-by-using-the-query-store.md) to be enabled in order to benefit the user database. To enable the Query Store, see [Enable the Query Store](monitoring-performance-by-using-the-query-store.md#Enabling).

| **IQP Feature** | **Requires Query Store to be enabled and READ_WRITE**|
| ---------------- | ------- | 
| [Adaptive Joins (Batch Mode)](intelligent-query-processing-details.md#batch-mode-adaptive-joins) |  No |
| [Approximate Count Distinct](intelligent-query-processing-details.md#approximate-query-processing) | No |
| [Approximate Percentile](intelligent-query-processing-details.md#approximate-query-processing) | No |
| [Batch Mode on Rowstore](intelligent-query-processing-details.md#batch-mode-on-rowstore) | No |
| [Cardinality estimation (CE) feedback](intelligent-query-processing-feedback.md#cardinality-estimation-ce-feedback) | Yes |
| [Degrees of Parallelism (DOP) feedback](intelligent-query-processing-details.md#dop-feedback) | Yes | 
| [Interleaved Execution](intelligent-query-processing-details.md#interleaved-execution-for-mstvfs) | No |
| [Memory grant feedback (Batch Mode)](intelligent-query-processing-feedback.md#batch-mode-memory-grant-feedback) | Partially |
| [Memory grant feedback (Row Mode)](intelligent-query-processing-feedback.md#row-mode-memory-grant-feedback) | Partially |
| [Memory grant feedback (Percentile)](intelligent-query-processing-feedback.md#percentile-and-persistence-mode-memory-grant-feedback) | Yes |
| [Memory Grant, CE, and DOP feedback persistence](intelligent-query-processing-feedback.md#percentile-and-persistence-mode-memory-grant-feedback) | No |
| [Optimized plan forcing](optimized-plan-forcing-query-store.md) | Yes |
| [Scalar UDF Inlining](intelligent-query-processing-details.md#scalar-udf-inlining) | No | 
| [Parameter Sensitivity Plan Optimization](parameter-sensitivity-plan-optimization.md) | No, but recommended |
| [Table Variable Deferred Compilation](intelligent-query-processing-details.md#table-variable-deferred-compilation) | No |

## See also

For complete details on all IQP features, including release notes and more in-depth descriptions, see [Intelligent query processing (IQP) feature details](intelligent-query-processing-details.md).

- [Joins](../../relational-databases/performance/joins.md)
- [Execution modes](../../relational-databases/query-processing-architecture-guide.md#execution-modes)
- [Query processing architecture guide](../../relational-databases/query-processing-architecture-guide.md)
- [Showplan logical and physical operators reference](../../relational-databases/showplan-logical-and-physical-operators-reference.md)
- [What's new in SQL Server 2017](../../sql-server/what-s-new-in-sql-server-2017.md)
- [What's new in SQL Server 2019](../../sql-server/what-s-new-in-sql-server-2019.md)
- [What's new in SQL Server 2022](../../sql-server/what-s-new-in-sql-server-2022.md)

## Next steps

- [Query processing feedback](intelligent-query-processing-feedback.md)
- [Demonstrating Intelligent Query Processing](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/intelligent-query-processing)
- [Constant Folding and Expression Evaluation](../query-processing-architecture-guide.md#constant-folding-and-expression-evaluation)
- [Intelligent query processing demos on GitHub](https://aka.ms/IQPDemos)
- [Performance Center for SQL Server Database Engine and Azure SQL Database](../../relational-databases/performance/performance-center-for-sql-server-database-engine-and-azure-sql-database.md)
- [Monitor performance by using the Query Store](monitoring-performance-by-using-the-query-store.md)
- [Best practices with Query Store](best-practice-with-the-query-store.md)
