---
title: optimize for ad hoc workloads (server configuration option)
description: "Learn about the 'optimize for ad hoc workloads' option. Use it to improve SQL Server plan cache efficiency when workloads contain many single-use ad hoc batches."
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/28/2023
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "optimize for ad hoc workloads option"
---

# optimize for ad hoc workloads (server configuration option)

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi-pdw.md)]

The **optimize for ad hoc workloads** option is used to improve the efficiency of the plan cache for workloads that contain many single use ad hoc batches. When this option is set to `1`, the Database Engine stores a small compiled plan stub in the plan cache when a batch is compiled for the first time, instead of the full compiled plan. This option might help to relieve memory pressure by not allowing the plan cache to become filled with compiled plans that aren't reused. However, enabling this option might affect your ability to troubleshoot single-use plans.

The compiled plan stub allows the [!INCLUDE [ssDE](../../includes/ssde-md.md)] to recognize that this ad hoc batch was compiled previously, and only stores a compiled plan stub. When this batch is invoked (compiled or executed) again, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] compiles the batch, removes the compiled plan stub from the plan cache, and adds the full compiled plan to the plan cache.

You can find compiled plan stubs by querying the `sys.dm_exec_cached_plans` catalog view and looking for "Compiled Plan" in the `cacheobjtype` column. The stub has a unique `plan_handle`. The compiled plan stub doesn't have an execution plan associated with it, and querying for the plan handle doesn't return a graphical or XML showplan.

[Trace Flag 8032](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) reverts the cache limit parameters to the [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] RTM setting, which in general allows caches to be larger. Use this setting when frequently reused cache entries don't fit into the cache and when the **optimize for ad hoc workloads** option failed to resolve the problem with plan cache.

> [!WARNING]  
> Trace Flag 8032 can cause poor performance if large caches make less memory available for other memory consumers, such as the buffer pool.

## Remarks

Setting the **optimize for ad hoc workloads** option to `1` affects only new plans; plans that are already in the plan cache are unaffected.

To affect already cached query plans immediately, the plan cache needs to be cleared using [ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md), or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] has to restart.

## Recommendations

Avoid having a large number of single-use plans in the plan cache. Common causes include:

- Data types of query parameters that aren't consistently defined. This particularly applies to the length of strings but can apply to any data type that has a maxlength, a precision, or a scale. For example, if a parameter named `@Greeting` is passed as **nvarchar(10)** on one call and **nvarchar(20)** on the next call, separate plans are created for each parameter size.

- Queries that aren't parameterized. If a query has one or more parameters for which hard-coded values get submitted to the [!INCLUDE [ssde-md](../../includes/ssde-md.md)], a large number of query plans could exist for each query. Plans could exist for each combination of query parameter data types and lengths that were used.

If the number of single-use plans take a significant portion of [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] memory in an OLTP server, and these plans are ad hoc plans, use this server option to decrease memory usage with these objects.

If the **optimize for ad hoc workloads** option is enabled, you can't view execution plans for single-use queries, because only the plan stub is cached. Depending on your environment and workload, you might benefit from the following two features:

- The **[Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)** feature, introduced in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], helps you quickly find performance differences caused by query plan changes. Query Store is enabled by default on new databases in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions.

- **[Forced parameterization](../../relational-databases/query-processing-architecture-guide.md#forced-parameterization)** can improve the performance of certain databases by reducing the frequency of query compilations and recompilations. Databases that benefit from forced parameterization generally experience high volumes of concurrent queries from sources such as point-of-sale applications.

  Forced parameterization can cause performance issues due to parameter sensitivity. For more information, see [Investigate and resolve parameter-sensitive issues](/troubleshoot/sql/database-engine/performance/troubleshoot-high-cpu-usage-issues#step-5-investigate-and-resolve-parameter-sensitive-issues). For [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, you can also enable [Parameter Sensitive Plan optimization](../../relational-databases/performance/parameter-sensitive-plan-optimization.md).

## Examples

To find the number of single-use cached plans, run the following query:

```sql
SELECT objtype,
    cacheobjtype,
    SUM(refcounts) AS AllRefObjects,
    SUM(CAST(size_in_bytes AS BIGINT)) / 1024 / 1024 AS SizeInMB
FROM sys.dm_exec_cached_plans
WHERE objtype = 'Adhoc'
    AND usecounts = 1
GROUP BY objtype, cacheobjtype;
```

## Related content

- [sys.dm_exec_cached_plans (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md)
- [Server configuration options (SQL Server)](server-configuration-options-sql-server.md)
- [Monitor performance by using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [Query processing architecture guide](../../relational-databases/query-processing-architecture-guide.md)
- [Parameter Sensitive Plan optimization](../../relational-databases/performance/parameter-sensitive-plan-optimization.md)
