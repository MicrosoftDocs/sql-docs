---
title: "Query Store hints best practices"
description: "Best practices for the Query Store hints feature, which helps you to shape query plans without changing application code."
ms.custom:
- event-tier1-build-2022
ms.date: 08/01/2022
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
dev_langs:
 - "TSQL"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-ver16||>=sql-server-linux-ver16"
---
# Query Store hints best practices
[!INCLUDE [sqlserver2022-asdb-asdbmi](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

This article details best practices for using [Query Store hints](query-store-hints.md). Query Store hints enable shaping query plan shapes without modifying application code.

- For more information on configuring and administering with the Query Store, see [Monitoring performance by using the Query Store](monitoring-performance-by-using-the-query-store.md).
- For information on discovering actionable information and tune performance with the Query Store, see [Tuning performance by using the Query Store](tune-performance-with-the-query-store.md).

## Use cases for Query Store hints

Consider the following use cases as ideal of Query Store hints. For more information, see [When to use Query Store hints](query-store-hints.md#when-to-use-query-store-hints).

> [!CAUTION]
> Because the SQL Server Query Optimizer typically selects the best execution plan for a query, we recommend only using hints as a last resort for experienced developers and database administrators. For more information, see [Query Hints](../../t-sql/queries/hints-transact-sql-query.md).

### When code cannot be changed

Using Query Store hints allows you to influence the execution plans of queries without changing application code or database objects. No other feature allows for you to apply query hints quickly and easily. 

You can use Query Store hints, for example, to benefit ETLs without redeploying code. Learn how to improve bulk loading with Query Store hints with this 14-minute video:

> [!VIDEO https://channel9.msdn.com/Shows/data-exposed/using-query-store-hints-to-optimize-memory-grants-improving-performance/player?WT.mc_id=dataexposed-c9-niner]

Query Store hints are lightweight query tuning methods, but if a query becomes problematic it should be addressed with more substantial code changes. If you are regularly finding the need to apply Query Store hints to a query, consider a larger query rewrite. The SQL Server Query Optimizer typically selects the best execution plan for a query, we recommend only using hints as a last resort for experienced developers and database administrators. 

For information on which query hints can be applied, see [Supported query hints](../system-stored-procedures/sys-sp-query-store-set-hints-transact-sql.md#supported-query-hints).

### Under high transaction load or with mission critical code

If code changes are impractical because of high uptime requirements or transactional load, Query Store hints can apply query hints to existing query workloads quickly. Adding and removing Query Store hints is easy.

Query Store hints can be added and removed to batches of queries to adjust performance for windows timed for bursts of exceptional workload.

### As a replacement for plan guides

Previous to Query Store hints, a developer would have to rely on [plan guides](plan-guides.md) to accomplish similar tasks, which can be complex to use. Query Store hints are integrated with Query Store features of SQL Server Management Studio (SSMS), for visual exploration of queries. 

With plan guides, searching through all plans using query snippets is necessary. The Query Store hints feature does not require exact matching queries to impact the resulting query plan. Query Store hints can be applied to a `query_id` in the Query Store dataset. 

Query Store hints override hard-coded statement-level hints and existing plan guides. 

### Consider a newer compatibility level

Query Store hints can be a valuable method when a newer database compatibility level is not available to you due to vendor specification or larger testing delays, for example. When a higher compatibility level is available to a database, consider upgrading the database compatibility level of an individual query to take advantage of the latest performance optimizations and features of SQL Server.

For example, if you have a [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] instance with a database in compatibility level 140, you can still use Query Store hints to run individual queries in compatibility level 160. You could use the following hint:

```sql
EXEC sys.sp_query_store_set_hints @query_id= 39, @query_hints = N'OPTION(USE HINT(''QUERY_OPTIMIZER_COMPATIBILITY_LEVEL_160''))';
```

For a complete tutorial, see [Query Store hints Examples](query-store-hints.md#examples).

### Consider an older compatibility level after upgrade

Another case where Query Store hints can help is where queries cannot be modified directly after a SQL Server instance migration or upgrade. Use Query Store hints to apply a prior compatibility level for a query until it can be rewritten or otherwise addressed to perform well in the latest compatibility level. Identify outlier queries that have regressed in a higher compatibility level using the [Query Store's regressed queries report](monitoring-performance-by-using-the-query-store.md#Regressed), using the [Query Tuning Advisor](upgrade-dbcompat-using-qta.md) tool during a migration, or other query-level application telemetry. For more information on the differences between compatibility levels, review the [Differences between compatibility levels](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#differences-between-compatibility-levels).

After performance testing the new compatibility level and deploying Query Store hints in this way, you can upgrade the entire database's compatibility level while keeping key problematic queries on the prior compatibility level, without any code changes.

## Query Store hints considerations

Consider the following scenarios when deploying Query Store hints.

### Data distribution changes

Plan guides, forced plans via the Query Store, and Query Store hints override the optimizer's decision making. The Query Store hint may be beneficial now, but not in the future. For example, if a Query Store hint helps a query in previous data distribution, it may be counter-productive if large-scale DML operations change the data. A new data distribution may cause the optimizer to make a better decision than the hint. This scenario is the most common consequence of forcing plan behavior. 

### Regularly re-evaluate your Query Store hints strategy

Re-evaluate your existing Query Store hints strategy in the following cases:

 - After known large data distribution changes.
 - When the service level objective (SLO) of your Azure SQL Database or Managed Instance or virtual machine has changed.
 - Where plan fixing has become long-lived. Query Store hints are best used for short-term fixes.
 - Unexpected performance regressions.

### Broad impact potential

Query Store hints will affect all executions of the query, regardless of parameter set, source application, user, or result set. In the case of accidentally performance regression, Query Store hints created with [sys.sp_query_store_set_hints](../system-stored-procedures/sys-sp-query-store-set-hints-transact-sql.md) can be easily removed with [sys.sp_query_store_clear_hints](../system-stored-procedures/sys-sp-query-store-clear-hints-transact-sql.md).

Carefully load test changes for mission critical or sensitive systems before applying Query Store hints in production. 

### Forced parameterization and the RECOMPILE hint are not supported

Applying the RECOMPILE query hint with Query Store hints is not supported when the database option [PARAMETERIZATION is set to FORCED](../../t-sql/statements/alter-database-transact-sql-set-options.md#parameterization_option-). For more information, see [Guidelines for Using Forced Parameterization](../../relational-databases/query-processing-architecture-guide.md#forced-parameterization).

The RECOMPILE hint is not compatible with forced parameterization set at the database level. If the database has forced parameterization set, and the RECOMPILE hint is part of the hints string set in Query Store for a query, the Database Engine will ignore the RECOMPILE hint and will apply other hints if leveraged. Additionally, starting in July 2022 in Azure SQL Database, a warning (error code 12461) should be issued stating that the RECOMPILE hint was ignored.

For information on which query hints can be applied, see [Supported query hints](../system-stored-procedures/sys-sp-query-store-set-hints-transact-sql.md#supported-query-hints).

## See also

- [Query Store hints](query-store-hints.md)
- [sys.query_store_query_hints (Transact-SQL)](../system-catalog-views/sys-query-store-query-hints-transact-sql.md)   
- [sys.sp_query_store_set_hints (Transact-SQL)](../system-stored-procedures/sys-sp-query-store-set-hints-transact-sql.md)   
- [sys.sp_query_store_clear_hints (Transact-SQL)](../system-stored-procedures/sys-sp-query-store-clear-hints-transact-sql.md)   
- [Save an Execution Plan in XML Format](save-an-execution-plan-in-xml-format.md)
- [Display and Save Execution Plans](display-and-save-execution-plans.md)
- [Hints (Transact-SQL) - Query](../../t-sql/queries/hints-transact-sql-query.md)  

## Next steps

- [Best practices with Query Store](best-practice-with-the-query-store.md)
- [Monitor performance by using Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [Configure the max degree of parallelism (MAXDOP) in Azure SQL Database](/azure/azure-sql/database/configure-max-degree-of-parallelism)
- [Tune performance with the Query Store](tune-performance-with-the-query-store.md)
