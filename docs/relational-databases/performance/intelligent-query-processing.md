---
title: "Intelligent Query Processing"
description: "Intelligent query processing features to improve query performance in SQL Server, Azure SQL Managed Instance, and Azure SQL Database."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: derekw, randolphwest
ms.date: 07/09/2025
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
ms.custom:
  - ignite-2024
  - build-2025
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---

# Intelligent query processing in SQL databases

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The intelligent query processing (IQP) feature family includes features with broad impact that improve the performance of existing workloads with minimal implementation effort to adopt. The following graphic details the family of IQP features and when they were first introduced for SQL Server. All IQP features are available in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. Some features depend on the database's compatibility level.

:::image type="content" source="media/iqp-feature-family.svg" alt-text="Diagram of the Intelligent Query Processing family of features and when they were first introduced to SQL Server.":::

Watch this video for an overview of intelligent query processing:

&nbsp;

> [!VIDEO https://learn-video.azurefd.net/vod/player?show=data-exposed&ep=how-sql-is-getting-smarter-intelligent-query-processing-2017-to-today-data-exposed]

For demos and sample code of intelligent query processing (IQP) features on GitHub, visit [https://aka.ms/IQPDemos](https://aka.ms/IQPDemos).

You can make workloads automatically eligible for intelligent query processing by enabling the applicable database compatibility level for the database. You can set this using [!INCLUDE [tsql](../../includes/tsql-md.md)]. For example:

```sql
ALTER DATABASE [WideWorldImportersDW] SET COMPATIBILITY_LEVEL = 170;
```

The following table details all intelligent query processing features, along with any requirement they have for database compatibility level. For complete details on all IQP features, including release notes and more in-depth descriptions, see [Intelligent query processing features in detail](intelligent-query-processing-details.md).

<a id="sql2025"></a>

## IQP features for Azure SQL Database and SQL Server 2025 Preview

| IQP Feature | Supported in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] | Supported in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] | Description |
| --- | --- | --- | --- |
| [Optional parameter plan optimization (OPPO)](optional-parameter-optimization.md) | Yes | Yes, starting with [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)] with compatibility level 170 | Leverages the adaptive plan optimization (Multiplan) infrastructure that was introduced with the Parameter Sensitive Plan Optimization (PSPO) improvement, which generates multiple plans from a single statement. The feature can choose a more optimal plan at runtime based on whether a parameter is `NULL OR NOT NULL`, which improves performance for queries that could otherwise default to suboptimal performance for such query patterns. |
| [Cardinality estimation (CE) feedback for expressions](intelligent-query-processing-ce-feedback-for-expressions.md) | Yes | Yes, starting with [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)] with compatibility level 160 | Extends CE feedback to improve cardinality estimates for repeating expressions across queries by learning from previous executions and automatically applying appropriate CE model choices to future executions of those expressions |
| [OPTIMIZED_SP_EXECUTESQL](../system-stored-procedures/sp-executesql-transact-sql.md#optimized_sp_executesql) | Yes | Yes, starting with [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)] | Effectively reduce the impact of compilation storms. A compilation storm is a situation where a large number of queries is being compiled simultaneously, leading to performance issues and resource contention. Enable this feature to allow invocations of `sp_executesql` to behave like objects such as stored procedures and triggers from a compilation perspective. |

<a id="sql2022"></a>

## IQP features for Azure SQL Database and SQL Server 2022

| IQP Feature | Supported in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] | Supported in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions | Description |
| --- | --- | --- | --- |
| [Adaptive Joins (Batch Mode)](intelligent-query-processing-details.md#batch-mode-adaptive-joins) | Yes, starting with database compatibility level 140 | Yes, starting in [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] with database compatibility level 140 | Adaptive joins dynamically select a join type during runtime based on actual input rows. |
| [Approximate Count Distinct](intelligent-query-processing-details.md#approximate-query-processing) | Yes | Yes, starting in [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] | Provide approximate `COUNT DISTINCT` for big data scenarios with the benefit of high performance and a low memory footprint. |
| [Approximate Percentile](intelligent-query-processing-details.md#approximate-query-processing) | Yes, starting with database compatibility level 110 | Yes, starting in [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] with compatibility level 110 | Quickly compute percentiles for a large dataset with acceptable rank-based error bounds to help make rapid decisions by using approximate percentile aggregate functions. |
| [Batch Mode on Rowstore](intelligent-query-processing-details.md#batch-mode-on-rowstore) | Yes, starting with database compatibility level 150 | Yes, starting in [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] with compatibility level 150 | Provide batch mode for CPU-bound relational DW workloads without requiring columnstore indexes. |
| [Cardinality estimation (CE) feedback](intelligent-query-processing-cardinality-estimation-feedback.md) | Yes, starting with database compatibility level 160 | Yes, starting in [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] with compatibility level 160 | Automatically adjusts cardinality estimates for repeating queries to optimize workloads where inefficient CE assumptions cause poor query performance. CE feedback will identify and use a model assumption that better fits a given query and data distribution to improve query execution plan quality. |
| [Degree of parallelism (DOP) feedback](intelligent-query-processing-degree-parallelism-feedback.md) | Yes, starting with database compatibility level 160 | Yes, starting with database compatibility level 160 | Automatically adjusts degree of parallelism for repeating queries to optimize for workloads where inefficient parallelism can cause performance issues. Requires Query Store to be enabled. |
| [Interleaved Execution](intelligent-query-processing-details.md#interleaved-execution-for-mstvfs) | Yes, starting with database compatibility level 140 | Yes, starting in [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] with database compatibility level 140 | Uses the actual cardinality of the multi-statement table valued function encountered on first compilation instead of a fixed guess. |
| [Memory grant feedback (Batch Mode)](intelligent-query-processing-memory-grant-feedback.md#batch-mode-memory-grant-feedback) | Yes, starting with database compatibility level 140 | Yes, starting in [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] with database compatibility level 140 | If a batch mode query has operations that spill to disk, add more memory for consecutive executions. If a query wastes > 50% of the memory allocated to it, reduce the memory grant size for consecutive executions. |
| [Memory grant feedback (Row Mode)](intelligent-query-processing-memory-grant-feedback.md#row-mode-memory-grant-feedback) | Yes, starting with database compatibility level 150 | Yes, starting in [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] with database compatibility level 150 | If a row mode query has operations that spill to disk, add more memory for consecutive executions. If a query wastes > 50% of the memory allocated to it, reduce the memory grant size for consecutive executions. |
| [Memory grant feedback (Percentile)](intelligent-query-processing-memory-grant-feedback.md#percentile-and-persistence-mode-memory-grant-feedback) | Yes, enabled on all databases | Yes, starting with [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)]) with database compatibility level 140 | Addresses existing limitations of memory grant feedback in a non-intrusive way by incorporating past query execution to refine feedback. |
| [Memory Grant feedback persistence](intelligent-query-processing-memory-grant-feedback.md#percentile-and-persistence-mode-memory-grant-feedback) | Yes, enabled on all databases | Yes, starting with [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)]) with database compatibility level 140 | Provides new functionality to persist memory grant feedback. Requires Query Store to be enabled for the database and in `READ_WRITE` mode. |
| [CE feedback persistence](intelligent-query-processing-cardinality-estimation-feedback.md#persistence-for-cardinality-estimation-ce-feedback) | Yes, starting with database compatibility level 160 | Yes, starting with [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)]) with database compatibility level 160 | Requires Query Store to be enabled for the database and in `READ_WRITE` mode. |
| [Optimized plan forcing with Query Store](optimized-plan-forcing-query-store.md) | Yes | Yes, starting with [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)]). | Reduces compilation overhead for repeating forced queries. For more information, see [Optimized plan forcing with Query Store](optimized-plan-forcing-query-store.md). |
| [Scalar UDF Inlining](intelligent-query-processing-details.md#scalar-udf-inlining) | Yes, starting with database compatibility level 150 | Yes, starting in [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] with database compatibility level 150 | Scalar UDFs are transformed into equivalent relational expressions that are "inlined" into the calling query, often resulting in significant performance gains. |
| [Parameter Sensitive Plan optimization](parameter-sensitive-plan-optimization.md) | Yes, starting with database compatibility level 160 | Yes, starting in [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] with database compatibility level 160 | Parameter Sensitive Plan optimization addresses the scenario where a single cached plan for a parameterized query isn't optimal for all possible incoming parameter values, for example non-uniform data distributions. |
| [Table Variable Deferred Compilation](intelligent-query-processing-details.md#table-variable-deferred-compilation) | Yes, starting with database compatibility level 150 | Yes, starting in [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] with database compatibility level 150 | Uses the actual cardinality of the table variable encountered on first compilation instead of a fixed guess. |

<a id="sqlmi"></a>

## IQP features for Azure SQL Managed Instance

| IQP Feature | Supported in [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)] | Description |
| --- | --- | --- | --- |
| [Adaptive Joins (Batch Mode)](intelligent-query-processing-details.md#batch-mode-adaptive-joins) | Yes, starting with database compatibility level 140 | Adaptive joins dynamically select a join type during runtime based on actual input rows. |
| [Approximate Count Distinct](intelligent-query-processing-details.md#approximate-query-processing) | Yes | Provide approximate `COUNT DISTINCT` for big data scenarios with the benefit of high performance and a low memory footprint. |
| [Approximate Percentile](intelligent-query-processing-details.md#approximate-query-processing) | Yes, starting with database compatibility level 110 | Quickly compute percentiles for a large dataset with acceptable rank-based error bounds to help make rapid decisions by using approximate percentile aggregate functions. |
| [Batch Mode on Rowstore](intelligent-query-processing-details.md#batch-mode-on-rowstore) | Yes, starting with database compatibility level 150 | Provide batch mode for CPU-bound relational DW workloads without requiring columnstore indexes. |
| [Cardinality estimation (CE) feedback](intelligent-query-processing-cardinality-estimation-feedback.md) | Yes, starting with database compatibility level 160 | Automatically adjusts cardinality estimates for repeating queries to optimize workloads where inefficient CE assumptions cause poor query performance. CE feedback will identify and use a model assumption that better fits a given query and data distribution to improve query execution plan quality. |
| [Degree of parallelism (DOP) feedback](intelligent-query-processing-degree-parallelism-feedback.md) | Yes, starting with database compatibility level 160 in Azure SQL Managed Instance with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy). No, for the [SQL Server 2022 update policy](/azure/azure-sql/managed-instance/update-policy#sql-server-2022-update-policy). | Automatically adjusts degree of parallelism for repeating queries to optimize for workloads where inefficient parallelism can cause performance issues. Requires Query Store to be enabled. |
| [Interleaved Execution](intelligent-query-processing-details.md#interleaved-execution-for-mstvfs) | Yes, starting with database compatibility level 140 | Uses the actual cardinality of the multi-statement table valued function encountered on first compilation instead of a fixed guess. |
| [Memory grant feedback (Batch Mode)](intelligent-query-processing-memory-grant-feedback.md#batch-mode-memory-grant-feedback) | Yes, starting with database compatibility level 140 | If a batch mode query has operations that spill to disk, add more memory for consecutive executions. If a query wastes > 50% of the memory allocated to it, reduce the memory grant size for consecutive executions. |
| [Memory grant feedback (Row Mode)](intelligent-query-processing-memory-grant-feedback.md#row-mode-memory-grant-feedback) | Yes, starting with database compatibility level 150 | If a row mode query has operations that spill to disk, add more memory for consecutive executions. If a query wastes > 50% of the memory allocated to it, reduce the memory grant size for consecutive executions. |
| [Memory grant feedback (Percentile)](intelligent-query-processing-memory-grant-feedback.md#percentile-and-persistence-mode-memory-grant-feedback) | Yes, starting with database compatibility level 160 | Addresses existing limitations of memory grant feedback in a non-intrusive way by incorporating past query execution to refine feedback. |
| [Memory Grant, CE, and DOP feedback persistence](intelligent-query-processing-memory-grant-feedback.md#percentile-and-persistence-mode-memory-grant-feedback) | Yes, starting with database compatibility level 160 | Provides new functionality to persist memory grant feedback. CE and DOP feedback is always persisted. Requires Query Store to be enabled for the database and in `READ_WRITE` mode. |
| [Optimized plan forcing with Query Store](optimized-plan-forcing-query-store.md) | No <!--Yes, starting with database compatibility level 160--> | Reduces compilation overhead for repeating forced queries. For more information, see [Optimized plan forcing with Query Store](optimized-plan-forcing-query-store.md). |
| [Scalar UDF Inlining](intelligent-query-processing-details.md#scalar-udf-inlining) | Yes, starting with database compatibility level 150 | Scalar UDFs are transformed into equivalent relational expressions that are "inlined" into the calling query, often resulting in significant performance gains. |
| [Parameter Sensitive Plan optimization](parameter-sensitive-plan-optimization.md) | Yes, starting with database compatibility level 160 | Parameter Sensitivity Plan Optimization addresses the scenario where a single cached plan for a parameterized query isn't optimal for all possible incoming parameter values, for example non-uniform data distributions. |
| [Table Variable Deferred Compilation](intelligent-query-processing-details.md#table-variable-deferred-compilation) | Yes, starting with database compatibility level 150 | Uses the actual cardinality of the table variable encountered on first compilation instead of a fixed guess. |

<a id="sql2019"></a>

## IQP features for SQL Server 2019

| IQP feature | Supported in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] | Description |
| --- | --- | --- |
| [Adaptive Joins (Batch Mode)](intelligent-query-processing-details.md#batch-mode-adaptive-joins) | Yes, starting in [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] with database compatibility level 140 | Adaptive joins dynamically select a join type during runtime based on actual input rows. |
| [Approximate Count Distinct](intelligent-query-processing-details.md#approximate-query-processing) | Yes | Provide approximate `COUNT DISTINCT` for big data scenarios with the benefit of high performance and a low memory footprint. |
| [Batch Mode on Rowstore](intelligent-query-processing-details.md#batch-mode-on-rowstore) | Yes, starting with database compatibility level 150 | Provide batch mode for CPU-bound relational DW workloads without requiring columnstore indexes. |
| [Interleaved Execution](intelligent-query-processing-details.md#interleaved-execution-for-mstvfs) | Yes, starting with database compatibility level 140 | Use the actual cardinality of the multi-statement table valued function encountered on first compilation instead of a fixed guess. |
| [Memory grant feedback (Batch Mode)](intelligent-query-processing-memory-grant-feedback.md#batch-mode-memory-grant-feedback) | Yes, starting with database compatibility level 140 | If a batch mode query has operations that spill to disk, add more memory for consecutive executions. If a query wastes > 50% of the memory allocated to it, reduce the memory grant size for consecutive executions. |
| [Memory grant feedback (Row Mode)](intelligent-query-processing-memory-grant-feedback.md#row-mode-memory-grant-feedback) | Yes, starting with database compatibility level 150 | If a row mode query has operations that spill to disk, add more memory for consecutive executions. If a query wastes > 50% of the memory allocated to it, reduce the memory grant size for consecutive executions. |
| [Scalar UDF Inlining](intelligent-query-processing-details.md#scalar-udf-inlining) | Yes, starting with database compatibility level 150 | Scalar UDFs are transformed into equivalent relational expressions that are "inlined" into the calling query, often resulting in significant performance gains. |
| [Table Variable Deferred Compilation](intelligent-query-processing-details.md#table-variable-deferred-compilation) | Yes, starting with database compatibility level 150 | Use the actual cardinality of the table variable encountered on first compilation instead of a fixed guess. |

<a id="sql2019"></a>

## IQP features for SQL Server 2017

| IQP feature | Supported in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] | Description |
| --- | --- | --- |
| [Adaptive Joins (Batch Mode)](intelligent-query-processing-details.md#batch-mode-adaptive-joins) | Yes, starting in [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] with database compatibility level 140 | Adaptive joins dynamically select a join type during runtime based on actual input rows. |
| [Approximate Count Distinct](intelligent-query-processing-details.md#approximate-query-processing) | Yes | Provide approximate `COUNT DISTINCT` for big data scenarios with the benefit of high performance and a low memory footprint. |
| [Interleaved Execution](intelligent-query-processing-details.md#interleaved-execution-for-mstvfs) | Yes, starting with database compatibility level 140 | Use the actual cardinality of the multi-statement table valued function encountered on first compilation instead of a fixed guess. |
| [Memory grant feedback (Batch Mode)](intelligent-query-processing-memory-grant-feedback.md#batch-mode-memory-grant-feedback) | Yes, starting with database compatibility level 140 | If a batch mode query has operations that spill to disk, add more memory for consecutive executions. If a query wastes > 50% of the memory allocated to it, reduce the memory grant size for consecutive executions. |

## Query Store requirement

Several of the suite of [intelligent query processing features](intelligent-query-processing.md) require the [Query Store](monitoring-performance-by-using-the-query-store.md) to be enabled in order to benefit the user database. To enable the Query Store, see [Enable the Query Store](monitoring-performance-by-using-the-query-store.md#Enabling).

| IQP feature | Requires Query Store to be enabled and `READ_WRITE` |
| --- | --- |
| [Adaptive Joins (Batch Mode)](intelligent-query-processing-details.md#batch-mode-adaptive-joins) | No |
| [Approximate Count Distinct](intelligent-query-processing-details.md#approximate-query-processing) | No |
| [Approximate Percentile](intelligent-query-processing-details.md#approximate-query-processing) | No |
| [Batch Mode on Rowstore](intelligent-query-processing-details.md#batch-mode-on-rowstore) | No |
| [Cardinality estimation (CE) feedback](intelligent-query-processing-cardinality-estimation-feedback.md) | Yes |
| [Degree of parallelism (DOP) feedback](intelligent-query-processing-degree-parallelism-feedback.md) | Yes |
| [Interleaved Execution](intelligent-query-processing-details.md#interleaved-execution-for-mstvfs) | No |
| [Memory grant feedback (Batch Mode)](intelligent-query-processing-memory-grant-feedback.md#batch-mode-memory-grant-feedback) | No |
| [Memory grant feedback (Row Mode)](intelligent-query-processing-memory-grant-feedback.md#row-mode-memory-grant-feedback) | No |
| [Memory grant feedback (Percentile and Persistence mode)](intelligent-query-processing-memory-grant-feedback.md#percentile-and-persistence-mode-memory-grant-feedback) | Yes |
| [Optimized plan forcing with Query Store](optimized-plan-forcing-query-store.md) | Yes |
| [Scalar UDF Inlining](intelligent-query-processing-details.md#scalar-udf-inlining) | No |
| [Parameter Sensitive Plan optimization](parameter-sensitive-plan-optimization.md) | No, but recommended |
| [Table Variable Deferred Compilation](intelligent-query-processing-details.md#table-variable-deferred-compilation) | No |

## Related content

- [Intelligent query processing features in detail](intelligent-query-processing-details.md)
- [Joins (SQL Server)](joins.md)
- [Execution modes](../query-processing-architecture-guide.md#execution-modes)
- [Query processing architecture guide](../query-processing-architecture-guide.md)
- [Logical and physical showplan operator reference](../showplan-logical-and-physical-operators-reference.md)
- [What's new in SQL Server 2017](../../sql-server/what-s-new-in-sql-server-2017.md)
- [What's new in SQL Server 2019](../../sql-server/what-s-new-in-sql-server-2019.md)
- [What's new in SQL Server 2022](../../sql-server/what-s-new-in-sql-server-2022.md)
- [Memory grant feedback](intelligent-query-processing-memory-grant-feedback.md)
- [Demonstrating Intelligent Query Processing](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/intelligent-query-processing)
- [Constant Folding and Expression Evaluation](../query-processing-architecture-guide.md#constant-folding-and-expression-evaluation)
- [Intelligent query processing demos on GitHub](https://aka.ms/IQPDemos)
- [Performance Center for SQL Server Database Engine and Azure SQL Database](performance-center-for-sql-server-database-engine-and-azure-sql-database.md)
- [Monitor performance by using the Query Store](monitoring-performance-by-using-the-query-store.md)
- [Best practices for monitoring workloads with Query Store](best-practice-with-the-query-store.md)
