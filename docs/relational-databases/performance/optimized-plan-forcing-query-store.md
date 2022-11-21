---
title: "Optimized plan forcing with Query Store"
description: Learn about optimized plan forcing and optimization replay scripts in Query Store.
ms.custom:
- event-tier1-build-2022
ms.date: 07/25/2022
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Query Store"
dev_langs:
 - "TSQL"
author: thesqlsith
ms.author: derekw 
ms.reviewer: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-ver16||>=sql-server-linux-ver16||=azuresqldb-mi-current"
---

# Optimized plan forcing with Query Store

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

Query optimization is a multi-phased process of generating a "good-enough" query execution plan. In some cases, query compilation, a part of query optimization, can represent a large percentage of overall query execution time and consume significant system resources. Optimized plan forcing is part of the [intelligent query processing family of features](intelligent-query-processing.md). Optimized plan forcing reduces compilation overhead for repeating *forced* queries and requires the Query Store to be enabled and in "read write" mode. Once the query execution plan is generated, specific compilation steps are stored for reuse as an optimization replay script. An optimization replay script is stored as part of the compressed showplan XML in [Query Store](monitoring-performance-by-using-the-query-store.md), in a hidden `OptimizationReplay` attribute.

## Optimized plan forcing implementation

When a query first goes through the compilation process, a threshold based on estimation of the time spent in optimization (based on the query optimizer input tree) will determine whether an optimization replay script is created.

After compilation completes, several runtime metrics become available to assess whether the previous estimation was correct. If it's confirmed the threshold was crossed, the optimization replay script is eligible for persistence. These runtime metrics include the number of objects accessed, the number of joins, the number of optimization tasks executed during optimization, and the actual optimization time.

The potential benefit of using an optimization replay script is also compared to the overhead of storing the optimization replay script. An estimation of the relative time to replay the optimization replay script is compared with the time that was spent executing the normal optimization process, based on the number of optimization tasks stored in optimization replay script and the number of optimization tasks executed during normal compilation. If replaying the optimization replay script shows substantial benefit in reducing compilation time, the optimization replay script is persisted.

## Considerations

When the optimized plan forcing feature is enabled, the eligibility criteria for optimized plan forcing is:

1. Only query plans that go through full optimization are eligible, which can be verified by the presence of the `StatementOptmLevel="FULL"` property.
1. Statements with RECOMPILE hint and distributed queries are not eligible.

However, if the Query Store independently captures a query plan that was scoped out by optimized plan forcing, the optimization replay script will be created for a second recompilation of that same query, subject to default recompilation events. Learn more about recompilation in [Recompiling Execution Plans](../query-processing-architecture-guide.md#recompiling-execution-plans).

Even if an optimization replay script was generated, it might not be persisted in the Query Store if the Query Store configured capture policies criteria isn't met, notably the number of executions of that statement and its cumulated compile and execution times. In this case, the invalid optimization replay script will be removed from memory asynchronously.

## Enable and disable optimized plan forcing

You can enable or disable optimized plan forcing for a database. When optimized plan forcing is enabled for a database, you may disable it for individual queries using the `DISABLE_OPTIMIZED_PLAN_FORCING` query hint. You may also disable optimized plan forcing for a query plan which is forced in Query Store.

### Enable or disable optimized plan forcing for a database

Optimized plan forcing is enabled by default for new databases created in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and higher. The Query Store must be enabled for every database where optimized plan forcing is used. Upgraded instances with existing databases or databases restored from a lower version of SQL Server will have optimized plan forcing enabled by default.

To enable optimized plan forcing at the database level, use the `ALTER DATABASE SCOPED CONFIGURATION SET OPTIMIZED_PLAN_FORCING = ON` database scoped configuration. You must enable Query Store if it isn't already enabled. Find example code in [Example A](#a-enable-query-store-and-optimized-plan-forcing-for-a-database), or learn more about Query Store in [Monitor performance by using the Query Store](monitoring-performance-by-using-the-query-store.md).

To disable optimized plan forcing at the database level, use the `ALTER DATABASE SCOPED CONFIGURATION SET OPTIMIZED_PLAN_FORCING = OFF` database scoped configuration.

### Disable optimized plan forcing with a query hint

When the optimized plan forcing feature is enabled in a database, you can disable optimized plan forcing for an individual query by using the `DISABLE_OPTIMIZED_PLAN_FORCING` [query hint](../../t-sql/queries/hints-transact-sql-query.md).

Find an example of applying this query hint in [Example E](#e-disable-optimized-plan-forcing-for-a-query).

### Force a plan with Query Store, but disable optimized plan forcing

The [sp_query_store_force_plan](../system-stored-procedures/sp-query-store-force-plan-transact-sql.md) procedure includes a `disable_optimized_plan_forcing` parameter. In order to use this parameter, an additional parameter is required by the sp_query_store_force_plan stored procedure. The additional parameter is called `replica_group_id`. By default, the primary `replica_group_id` will have a value of one (*1*) even in the case where there are no configured secondary replicas.

Find an example of applying the appropriate parameters to the sp_query_store_force_plan stored procedure in [Example C](#c-force-a-plan-and-disable-optimized-plan-forcing-in-query-store).

The `sys.query_store_plan` catalog view includes columns that indicate if the plan has an associated optimization replay script, and adds a new state to existing failure reason column specific to associated optimization replay script. Learn more in [sys.query_store_plan (Transact-SQL)](../system-catalog-views/sys-query-store-plan-transact-sql.md).

## Examples

### A. Enable Query Store and optimized plan forcing for a database

The following code enables Query Store on a database, then enables optimized plan forcing on the database. Learn more about options enabling Query Store in [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md).

Before running the code, connect to the appropriate user database.

```sql
ALTER DATABASE CURRENT
SET QUERY_STORE = ON
    (
      OPERATION_MODE = READ_WRITE,
      CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = 90 ),
      DATA_FLUSH_INTERVAL_SECONDS = 900,
      QUERY_CAPTURE_MODE = AUTO,
      MAX_STORAGE_SIZE_MB = 1024,
      INTERVAL_LENGTH_MINUTES = 60
    );
GO


ALTER DATABASE SCOPED CONFIGURATION SET OPTIMIZED_PLAN_FORCING = ON;
GO
```

### B. Select all queries that have an optimization replay script

The following example code selects all query_ids that have an optimization replay script in Query Store. Connect to the appropriate user database before running the example code.

```sql
SELECT 
    q.query_id, t.query_sql_text, p.plan_id, TRY_CAST(p.query_plan as XML) as query_plan, 
    p.is_forced_plan, p.count_compiles
FROM sys.query_store_plan AS p
INNER JOIN sys.query_store_query AS q on p.query_id = q.query_id
INNER JOIN sys.query_store_query_text AS t
    ON q.query_text_id = t.query_text_id
WHERE p.has_compile_replay_script = 1;
GO
```

### C. Force a plan and disable optimized plan forcing in Query Store

The following code forces a plan in Query Store, but disables optimized plan forcing. Before running the following code, replace `@query_id` and `@plan_id` with a combination appropriate for your instance.  The sp_query_store_force_plan stored procedure will expect that the `@replica_group_id` parameter be passed in as the third parameter value when attempting to disabled optimized plan forcing in Query Store.  This can be used to disable optimized plan forcing for a particular forced plan on a specific replica.  A value of 1 - `@replica_group_id=1` will be used to disable the feature on the primary replica.

```sql
EXEC sp_query_store_force_plan @query_id=148, @plan_id=4, @replica_group_id=1, @disable_optimized_plan_forcing=1;
GO
```

Learn more in [sp_query_store_force_plan (Transact-SQL)](../system-stored-procedures/sp-query-store-force-plan-transact-sql.md).

### D. Select all queries where optimized plan forcing is disabled by Query Store

The following example queries all plans that have been forced in Query Store where `is_optimized_plan_forcing_disabled` has been set to `1`. Before running the code, connect to the appropriate user database.

```sql
SELECT 
    q.query_id, t.query_sql_text, p.plan_id, TRY_CAST(p.query_plan as XML) as query_plan, 
    p.is_forced_plan, p.count_compiles
FROM sys.query_store_plan AS p
INNER JOIN sys.query_store_query AS q on p.query_id = q.query_id
INNER JOIN sys.query_store_query_text AS t
    ON q.query_text_id = t.query_text_id
WHERE p.is_optimized_plan_forcing_disabled = 1;
GO
```

### E. Disable optimized plan forcing for a query

The following example disables optimized plan forcing for a query using the `DISABLE_OPTIMIZED_PLAN_FORCING` [query hint](../../t-sql/queries/hints-transact-sql-query.md). The example uses the [AdventureWorks sample database](../../samples/adventureworks-install-configure.md).

```sql
SELECT ProductID, OrderQty, SUM(LineTotal) AS Total  
FROM Sales.SalesOrderDetail  
WHERE UnitPrice < $5.00  
GROUP BY ProductID, OrderQty  
ORDER BY ProductID, OrderQty
OPTION(USE HINT('DISABLE_OPTIMIZED_PLAN_FORCING');
GO 
```

## Next steps

Learn more about Query Store and optimized plan forcing in the following articles:

- [sys.query_store_plan (Transact-SQL)](../system-catalog-views/sys-query-store-plan-transact-sql.md)
- [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)
- [sp_query_store_force_plan (Transact-SQL)](../system-stored-procedures/sp-query-store-force-plan-transact-sql.md)
- [Intelligent query processing in SQL databases](intelligent-query-processing.md)
