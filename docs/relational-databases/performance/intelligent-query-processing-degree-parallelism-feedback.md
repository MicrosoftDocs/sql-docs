---
title: Degree of parallelism (DOP) feedback
description: Learn about Degree of parallelism (DOP) feedback, part of the Intelligent Query Processing (IQP) feature set.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: derekw
ms.date: 01/19/2024
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

# Degree of parallelism (DOP) feedback

**Applies to:** [!INCLUDE [sqlserver2022-and-later](../../includes/applies-to-version/sqlserver2022-and-later.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] introduced a new feature called degree of parallelism (DOP) feedback to improve query performance by identifying parallelism inefficiencies for repeating queries, based on elapsed time and waits. DOP feedback is part of the [intelligent query processing](../../relational-databases/performance/intelligent-query-processing.md) family of features, and addresses suboptimal usage of parallelism for repeating queries. This scenario helps with optimizing resource usage and improving scalability of workloads, when excessive parallelism can cause performance issues. 

Instead of incurring in the pains of an all-encompassing default or manual adjustments to each query, DOP feedback self-adjusts DOP to avoid these issues.

For other query feedback features, see [Memory grant feedback](intelligent-query-processing-memory-grant-feedback.md) and [Cardinality estimation (CE) feedback](intelligent-query-processing-cardinality-estimation-feedback.md).

## Degree of parallelism (DOP) feedback avoids excess parallelism

Instead of incurring in the pains of an all-encompassing default or manual adjustments to each query, DOP feedback self-adjusts DOP to avoid excess parallelism. If parallelism usage is deemed inefficient, DOP feedback lowers the DOP for the next execution of the query, from whatever is the configured DOP, and verify if it helps.

Parallelism is often beneficial for reporting and analytical queries, or queries that otherwise handle large amounts of data. Conversely, OLTP-centric queries that are executed in parallel could experience performance issues when the time spent coordinating all threads outweighs the advantages of using a parallel plan. For more information, see [parallel plan execution](../../relational-databases/query-processing-architecture-guide.md#parallel-query-processing).

- To enable DOP feedback, enable the `DOP_FEEDBACK` [database scoped configuration](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#dop_feedback---on--off-) in a database. For example, in the user database:

    ```sql
    ALTER DATABASE SCOPED CONFIGURATION SET DOP_FEEDBACK = ON;
    ```

- To disable DOP feedback at the database level, use the `DOP_FEEDBACK` [database scoped configuration](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#ce_feedback---on--off-). For example, in the user database:

    ```sql
    ALTER DATABASE SCOPED CONFIGURATION SET DOP_FEEDBACK = OFF;
    ```

- To disable DOP feedback at the query level, use the `DISABLE_DOP_FEEDBACK` query hint.

The Query Store must be enabled for every database where DOP feedback is used, and in the "Read write" state. Feedback will be persisted in the [sys.query_store_plan_feedback](../system-catalog-views/sys-query-store-plan-feedback.md) catalog view when we reach a stable degree of parallelism feedback value.

DOP feedback is available for queries that operate in the database compatibility level 160 (introduced with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]) or higher.

Only verified feedback is persisted. If the adjusted DOP results in a performance regression, DOP feedback will go back to the last known good DOP. In this context, a user canceled query is also perceived as a regression. The DOP feedback doesn't recompile plans.

Stable feedback is reverified upon plan recompilation and might readjust up or down, but never higher than the MAXDOP setting (including a MAXDOP hint).

Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], when Query Store for secondary replicas is enabled, DOP feedback is also replica-aware for secondary replicas in availability groups. DOP feedback can apply feedback differently on a primary replica and on a secondary replica. However, DOP feedback is not persisted on secondary replicas, and on failover, the DOP feedback from the old primary replica is not applied to the new primary replica. On failover, feedback applied to primary or secondary replicas is lost. For more information, see [Query Store for secondary replicas](query-store-for-secondary-replicas.md).

### Degree of parallelism (DOP) feedback implementation

Degree of parallelism (DOP) feedback identifies parallelism inefficiencies for repeating queries, based on elapsed time and waits. If parallelism usage is deemed inefficient, DOP feedback will lower the DOP for the next execution of the query, from whatever is the configured DOP, and verify if it helps.

To assess query eligibility, the adjusted query elapsed time is measured over a few executions. The total elapsed time for each query is adjusted by ignoring Buffer Latch, Buffer IO, and Network IO waits which are external to the parallel query execution. The goal of the DOP feedback feature is to increase overall concurrency and reduce waits significantly, even if it slightly increases query elapsed time.

Only verified feedback is persisted. If the adjusted DOP results in a performance regression, DOP feedback will go back to the last known good DOP. In this context, a user canceled query is also perceived as a regression.

> [!NOTE]  
> DOP feedback doesn't recompile plans.

### Degree of parallelism (DOP) feedback considerations

Minimum DOP for any query adjusted with DOP feedback is 2. Serial executions are out of scope for DOP feedback.

Feedback information can be tracked using the [sys.query_store_plan_feedback](../system-catalog-views/sys-query-store-plan-feedback.md) catalog view.

If a query has a query plan forced through Query Store, DOP feedback can still be used for that query.

Currently, DOP Feedback is not compatible with query hints. For more information, see [Hints (Transact-SQL) - Query](../../t-sql/queries/hints-transact-sql-query.md) and [Query Store hints](../../relational-databases/performance/query-store-hints.md).

#### <a id="extended-events-for-dop-feedback"></a> Extended events for degree of parallelism (DOP) feedback

The following XEs are available for degree of parallelism (DOP) feedback:

- `dop_feedback_eligible_query`: Occurs when the query plan becomes eligible for DOP feedback.  Additional events might fire if a recompile or SQL Server instance restart occurs.
- `dop_feedback_provided`: Occurs when a DOP feedback provided data for a given query.  This event contains baseline statistics when feedback provided for first time and previous feedback statistics when subsequent feedback is provided.
- `dop_feedback_validation`: Occurs when validation occurs for the query runtime stats against a baseline or previous feedback stats.
- `dop_feedback_stabilized`: Occurs when DOP feedback is stabilized for a query.  
- `dop_feedback_reverted`: Occurs when a DOP feedback is reverted.  The event fires when feedback validation fails on the first feedback provided.  The system will revert back to no feedback state.
- `dop_feedback_analysis_stopped` : Occurs when the DOP feedback analysis is stopped for a query.

## Persistence for degree of parallelism (DOP) feedback

**Applies to:** [!INCLUDE [sqlserver2022-and-later](../../includes/applies-to-version/sqlserver2022-and-later.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]
<!---[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)])  -->

If the DOP feedback mechanism finds that the new degree of parallelism is good, this optimization is persisted inside the query store and will be applied appropriately to a query for future executions.

This feature was introduced in [!INCLUDE [ssSQL22](../../includes/sssql22-md.md)], and is available for queries that operate in the database compatibility level 160 or higher, or the `QUERY_OPTIMIZER_COMPATIBILITY_LEVEL_n` hint of 160 and higher, and when Query Store is enabled for the database and is in a "read write" state.

## Related content

- Blog: [Intelligent Query Processing: degree of parallelism feedback](https://cloudblogs.microsoft.com/sqlserver/2022/10/20/intelligent-query-processing-degree-of-parallelism-feedback/)
- [Intelligent query processing in SQL databases](intelligent-query-processing.md)
- [Intelligent query processing features in detail](intelligent-query-processing-details.md)
- [Configure the max degree of parallelism (server configuration option)](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md)
- [Cardinality Estimation (SQL Server)](cardinality-estimation-sql-server.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Monitor and Tune for Performance](monitor-and-tune-for-performance.md)
- [Configure Parallel Index Operations](../indexes/configure-parallel-index-operations.md)
- [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)
