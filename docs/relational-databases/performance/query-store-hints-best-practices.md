---
title: "Query Store Hints Best Practices"
description: "Best practices for the Query Store hints feature, which helps you to shape query plans without changing application code."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/25/2025
ms.service: sql
ms.subservice: performance
ms.topic: best-practice
ms.custom:
  - ignite-2024
  - build-2025
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || >=sql-server-ver16 || >=sql-server-linux-ver16 || =fabric"
---

# Query Store hints best practices

[!INCLUDE [SQL Server 2022 Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sqlserver2022-asdb-asmi-fabricsqldb.md)]

This article details best practices for using [Query Store hints](query-store-hints.md). Query Store hints enable shaping query plan shapes without modifying application code.

- For more information on configuring and administering with the Query Store, see [Monitor performance by using the Query Store](monitoring-performance-by-using-the-query-store.md).
- For information on discovering actionable information and tune performance with the Query Store, see [Tune performance with the Query Store](tune-performance-with-the-query-store.md).
- For general best practices on the Query Store, see [Best practices for monitoring workloads with Query Store](best-practice-with-the-query-store.md).

## Use cases for Query Store hints

Consider the following use cases as ideal of Query Store hints. For more information, see [When to use Query Store hints](query-store-hints.md#when-to-use-query-store-hints).

> [!CAUTION]  
> Because the SQL Server Query Optimizer typically selects the best execution plan for a query, we recommend only using hints as a last resort for experienced developers and database administrators. For more information, see [Query hints](../../t-sql/queries/hints-transact-sql-query.md).

### When code cannot be changed

Using Query Store hints allows you to influence the execution plans of queries without changing application code or database objects. No other feature allows for you to apply query hints quickly and easily.

You can use Query Store hints, for example to benefit extract-transform-load (ETL) workloads, without redeploying code. Learn how to improve bulk loading with Query Store hints with this 14-minute video:

> [!VIDEO https://channel9.msdn.com/Shows/data-exposed/using-query-store-hints-to-optimize-memory-grants-improving-performance/player?WT.mc_id=dataexposed-c9-niner]

Query Store hints are lightweight query tuning methods, but if a query becomes problematic it should be addressed with more substantial code changes. If you're regularly finding the need to apply Query Store hints to a query, consider a larger query rewrite. The SQL Server Query Optimizer typically selects the best execution plan for a query. We recommend only using hints as a last resort for experienced developers and database administrators.

For information on which query hints can be applied, see [Supported query hints](../system-stored-procedures/sys-sp-query-store-set-hints-transact-sql.md#supported-query-hints).

### Under high transaction load or with mission critical code

If code changes are impractical because of high uptime requirements or transactional load, Query Store hints can apply query hints to existing query workloads quickly. Adding and removing Query Store hints is easy.

Query Store hints can be added and removed to batches of queries to adjust performance for windows timed for bursts of exceptional workload.

### As a replacement for plan guides

Previous to Query Store hints, a developer would have to rely on [plan guides](plan-guides.md) to accomplish similar tasks, which can be complex to use. Query Store hints are integrated with Query Store features of SQL Server Management Studio (SSMS), for visual exploration of queries.

With plan guides, searching through all plans using query snippets is necessary. The Query Store hints feature doesn't require exact matching queries to impact the resulting query plan. Query Store hints can be applied to a `query_id` in the Query Store dataset.

Query Store hints override hard-coded statement-level hints and existing plan guides.

### Consider a newer compatibility level

Query Store hints can be a valuable method when a newer database compatibility level isn't available to you due to vendor specification or larger testing delays, for example. When a higher compatibility level is available to a database, consider upgrading the database compatibility level of an individual query to take advantage of the latest performance optimizations and features of SQL Server.

For example, if you have a [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] instance with a database in compatibility level 140, you can still use Query Store hints to run individual queries in compatibility level 160. You could use the following hint:

```sql
EXEC sys.sp_query_store_set_hints @query_id= 39, @query_hints = N'OPTION(USE HINT(''QUERY_OPTIMIZER_COMPATIBILITY_LEVEL_160''))';
```

For a complete tutorial, see [Query Store hints Examples](query-store-hints.md#examples).

### Consider an older compatibility level after upgrade

Another case where Query Store hints can help is where queries can't be modified directly after a SQL Server instance migration or upgrade. Use Query Store hints to apply a prior compatibility level for a query until it can be rewritten or otherwise addressed to perform well in the latest compatibility level. Identify outlier queries that regressed with a higher compatibility level using the [Query Store's regressed queries report](monitoring-performance-by-using-the-query-store.md#Regressed), using the [Query Tuning Assistant](upgrade-dbcompat-using-qta.md) tool during a migration, or other query-level application telemetry. For more information on the differences between compatibility levels, review the [Differences between compatibility levels](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md#differences-between-compatibility-levels).

After performance testing the new compatibility level and deploying Query Store hints in this way, you can upgrade the entire database's compatibility level while keeping key problematic queries on the prior compatibility level, without any code changes.

### Block future execution of problematic queries

You can use the `ABORT_QUERY_EXECUTION` query hint to block future execution of known problematic queries, for example nonessential queries causing high resource consumption and affecting critical application workloads.

> [!NOTE]  
> At this time, the [ABORT_QUERY_EXECUTION](/sql/t-sql/queries/hints-transact-sql-query?view=azuresqldb-current&preserve-view=true#use_hint_abort_query_execution) (preview) query hint is available only in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)].

For example, to block future execution of `query_id` 39, execute [sys.sp_query_store_set_hints](../system-stored-procedures/sys-sp-query-store-set-hints-transact-sql.md) as follows:

```sql
EXEC sys.sp_query_store_set_hints
     @query_id = 39,
     @query_hints = N'OPTION (USE HINT (''ABORT_QUERY_EXECUTION''))';
```

For more information, see Query Store hint [examples](query-store-hints.md#examples).

The following considerations apply:

- When you specify this hint for a query, an attempt to execute the query fails with error 8778, severity 16, *Query execution has been aborted because the ABORT_QUERY_EXECUTION hint was specified.*
- To unblock a query, you can clear the hint by passing the `query_id` value to the `@query_id` parameter in the [sys.sp_query_store_clear_hints](../system-stored-procedures/sys-sp-query-store-clear-hints-transact-sql.md) stored procedure.
- You can use system views to find queries in Query Store that are blocked, as in the following example query:

  ```sql
  SELECT qsh.query_id,
         q.query_hash,
         qt.query_sql_text
  FROM sys.query_store_query_hints AS qsh
  INNER JOIN sys.query_store_query AS q
  ON qsh.query_id = q.query_id
  INNER JOIN sys.query_store_query_text AS qt
  ON q.query_text_id = qt.query_text_id
  WHERE UPPER(qsh.query_hint_text) LIKE '%ABORT[_]QUERY[_]EXECUTION%'
  ```

- To get the `query_id` value, at least one query execution must be recorded in Query Store. This execution doesn't have to be successful. This means that future execution of timed out or canceled queries can be blocked.

- If you need to block or unblock all queries with a specific query hash, consider using an automation script. For example, [dbo.sp_query_store_modify_hints_by_query_hash](https://github.com/microsoft/sql-server-samples/blob/master/samples/features/query-store/sp_query_store_modify_hints_by_query_hash.sql) is a sample stored procedure that calls the `sys.sp_query_store_set_hints` or `sys.sp_query_store_clear_hints` system stored procedure in a loop for all `query_id` values matching a query hash.
- If a query is already executing when you block it, its execution continues. You can use the [KILL](../../t-sql/language-elements/kill-transact-sql.md) statement to abort the query.
  - Execution of killed queries isn't recorded in Query Store. If the query isn't yet in Query Store, you need to let the query complete or time out to get a `query_id` that you can block.
- When a query is blocked by the `ABORT_QUERY_EXECUTION` hint, the `execution_type` and `execution_type_desc` columns in the [sys.query_store_runtime_stats](../system-catalog-views/sys-query-store-runtime-stats-transact-sql.md) view are set to 4 and **Exception** respectively.
- As with all Query Store hints, you need to have the `ALTER` permission on the database to set and clear the `ABORT_QUERY_EXECUTION` hint.

## Query Store hints considerations

Consider the following scenarios when deploying Query Store hints.

### Data distribution changes

Plan guides, forced plans via the Query Store, and Query Store hints override the optimizer's decision making. The Query Store hint might be beneficial now, but not in the future. For example, if a Query Store hint helps a query in previous data distribution, it might be counter-productive if large-scale DML operations change the data. A new data distribution might cause the optimizer to make a better decision than the hint. This scenario is the most common consequence of forcing plan behavior.

### Regularly reevaluate your Query Store hints strategy

Reevaluate your existing Query Store hints strategy in the following cases:

- After known large data distribution changes.
- When the resources available to the database change. For example, when the compute size of your Azure SQL Database, SQL Managed Instance, or SQL Server virtual machine changes.
- Where plan fixing has become long-lived. Query Store hints are best used for short-term fixes.
- Unexpected performance regressions.

### Broad impact potential

Query Store hints affect all executions of the query, regardless of parameter set, source application, user, or result set. In the case of accidentally performance regression, Query Store hints created with [sys.sp_query_store_set_hints](../system-stored-procedures/sys-sp-query-store-set-hints-transact-sql.md) can be easily removed with [sys.sp_query_store_clear_hints](../system-stored-procedures/sys-sp-query-store-clear-hints-transact-sql.md).

Carefully load test changes for mission critical or sensitive systems before applying Query Store hints in production.

### Forced parameterization and the RECOMPILE hint are not supported

Applying the `RECOMPILE` query hint with Query Store hints isn't supported when the database option [PARAMETERIZATION is set to FORCED](../../t-sql/statements/alter-database-transact-sql-set-options.md#parameterization_option-). For more information, see [Guidelines for Using Forced Parameterization](../../relational-databases/query-processing-architecture-guide.md#forced-parameterization).

The `RECOMPILE` hint isn't compatible with forced parameterization set at the database level. If the database uses forced parameterization, and the `RECOMPILE` hint is part of the hints string set in Query Store for a query, the Database Engine ignores the `RECOMPILE` hint and applies other hints if specified. Additionally, starting in July 2022 in Azure SQL Database, a warning (error code 12461) is issued stating that the `RECOMPILE` hint was ignored.

For information on which query hints can be applied, see [Supported query hints](../system-stored-procedures/sys-sp-query-store-set-hints-transact-sql.md#supported-query-hints).

Caution

Purging Query Store data will lead to the deletion of all query hints configured, ensure taking backup before purging data.

## Related content

- [Save an Execution Plan in XML Format](save-an-execution-plan-in-xml-format.md)
- [Display and save execution plans](display-and-save-execution-plans.md)
- [Configure the max degree of parallelism (MAXDOP) in Azure SQL Database](/azure/azure-sql/database/configure-max-degree-of-parallelism)
