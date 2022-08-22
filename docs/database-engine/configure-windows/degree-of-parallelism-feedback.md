---
title: Degree of parallelism (DOP) feedback
description: Learn about the degree of parallelism (DOP) feedback.
ms.prod: sql
ms.prod_service: high-availability
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "parallel queries [SQL Server]"
  - "processors [SQL Server], parallel queries"
  - "number of processors for parallel queries"
  - "degree of parallelism feedback"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: katsmith
ms.custom:
- contperf-fy20q4
- event-tier1-build-2022
ms.date: 08/23/2022
---

# Degree of parallelism (DOP) feedback

**Applies to:** [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later

[!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] introduced a new feature to called degree of parallelism (DOP) Feedback to improve query performance by identifying parallelism inefficiencies for repeating queries, based on elapsed time and waits. DOP feedback is part of the [intelligent query processing](../../relational-databases/performance/intelligent-query-processing.md) family of features, and addresses suboptimal usage of parallelism for repeating queries. This scenario helps with optimizing resource usage and improving scalability of workloads, when excessive parallelism can cause performance issues. Instead of incurring in the pains of an all-encompassing default or manual adjustments to each query, DOP Feedback self-adjusts DOP to avoid the issues described above.

Parallelism is often beneficial for reporting and analytical queries, or queries that otherwise handle large amounts of data. Conversely, OLTP-centric queries that are executed in parallel could experience performance issues when the time spent coordinating all threads outweighs the advantages of using a parallel plan. For more information, see [parallel plan execution](../../relational-databases/query-processing-architecture-guide.md#parallel-query-processing).

- To enable DOP feedback, enable the `DOP_FEEDBACK` database scoped configuration in a database.

- The Query Store must be enabled for every database where DOP feedback is used, and in the "Read write" state. Feedback will be persisted in the `sys.query_store_plan_feedback` catalog view when we reach a stable degree of parallelism feedback value.

- DOP feedback is available for queries that operate in the database compatibility level 160 (introduced with SQL Server 2022) or higher.

- Only verified feedback is persisted. If the adjusted DOP results in a performance regression, DOP feedback will go back to the last known good DOP. In this context, a user canceled query is also perceived as a regression. The DOP feedback does not recompile plans.

- Stable feedback is re-verified upon plan recompilation and may readjust up or down, but never above MAXDOP setting (including a MAXDOP hint).

- To disable DOP feedback at the database level, use the `ALTER DATABASE SCOPED CONFIGURATION SET DOP_FEEDBACK = OFF` database scoped configuration.

- To disable DOP feedback at the query level, use the `DISABLE_DOP_FEEDBACK` query hint.

### Feedback implementation

DOP Feedback will identify parallelism inefficiencies for repeating queries, based on elapsed time and waits. If parallelism usage is deemed inefficient, DOP Feedback will lower the DOP for the next execution of the query, from whatever is the configured DOP, and verify if it helps.

To assess query eligibility, the adjusted query elapsed time is measured over a few executions. The total elapsed time for each query is adjusted by ignoring Buffer Latch, Buffer IO, and Network IO waits which are external to the parallel query execution. The goal of the DOP Feedback feature is to increase overall concurrency and reduce waits significantly, even if it slightly increases query elapsed time.

Only verified feedback is persisted. If the adjusted DOP results in a performance regression, DOP feedback will go back to the last known good DOP. In this context, a user canceled query is also perceived as a regression. Note that DOP Feedback doesn't recompile plans.

### DOP feedback considerations

Minimum DOP for any query adjusted with DOP Feedback is 2. Serial executions are out of scope for DOP Feedback.

Feedback information can be tracked using the `sys.query_store_plan_feedback` catalog view.

If a query has a query plan forced through Query Store, DOP feedback can still be used for that query.

If a query uses the MAXDOP hint, either as a hard-coded query hints or through the Query Store hinting mechanism, and the MAXDOP hint is greater than 2, DOP Feedback will lower the DOP using the hinted value as the ceiling. For more information, see [Hints (Transact-SQL) - Query](../../t-sql/queries/hints-transact-sql-query.md) and [Query Store hints](../../relational-databases/performance/query-store-hints.md).

#### Extended events

The following XEs are available for the feature:

- `dop_feedback_eligible_query`: Occurs when the query plan becomes eligible for DOP feedback.  Additional events may fire if a recompile or SQL Server instance restart occurs.
- `dop_feedback_provided`: Occurs when a DOP feedback provided data for a given query.  This event contains baseline statistics when feedback provided for first time and previous feedback statistics when subsequent feedback is provided.
- `dop_feedback_validation`: Occurs when validation occurs for the query runtime stats against a baseline or previous feedback stats.
- `dop_feedback_stabilized`: Occurs when DOP feedback is stabilized for a query.  
- `dop_feedback_reverted`: Occurs when a DOP feedback is reverted.  The event will fire when feedback validation fails on the first feedback provided.  The system will revert back to no feedback state.
- `dop_feedback_analysis_stopped` : Occurs when the DOP feedback analysis is stopped for a query.

## See also  

- [Intelligent query processing](../../relational-databases/performance/intelligent-query-processing.md)  
- [Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md)
- [Trace Flags](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)  
- [Query Store hints](../../relational-databases/performance/query-store-hints.md)
- [Query Hints (Transact-SQL)](../../t-sql/queries/hints-transact-sql-query.md)
- [Hints (Transact-SQL) - Query](../../t-sql/queries/hints-transact-sql-query.md)
- [USE HINT query hint](../../t-sql/queries/hints-transact-sql-query.md#use_hint)
- [ALTER DATABASE SCOPED CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)
- [affinity mask Server Configuration Option](../../database-engine/configure-windows/affinity-mask-server-configuration-option.md)
- [Server Configuration Options (SQL Server)](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
- [Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md#DOP)
- [Thread and Task Architecture Guide](../../relational-databases/thread-and-task-architecture-guide.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [Set Index Options](../../relational-databases/indexes/set-index-options.md)

## Next steps

- [Configure the max degree of parallelism Server Configuration Option](../../../azure-sql/database/configure-max-degree-of-parallelism.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
- [Monitor and Tune for Performance](../../relational-databases/performance/monitor-and-tune-for-performance.md)
- [Configure Parallel Index Operations](../../relational-databases/indexes/configure-parallel-index-operations.md)
