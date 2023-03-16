---
title: "sys.dm_exec_query_plan_stats (Transact-SQL)"
description: sys.dm_exec_query_plan_stats (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "02/24/2023"
ms.service: sql
ms.subservice: system-objects
ms.topic: conceptual
f1_keywords:
  - "sys.dm_exec_query_plan_stats"
  - "sys.dm_exec_query_plan_stats_TSQL"
  - "dm_exec_query_plan_stats_TSQL"
  - "dm_exec_query_plan_stats"
helpviewer_keywords:
  - "sys.dm_exec_query_plan_stats management view"
dev_langs:
  - "TSQL"
---
# sys.dm_exec_query_plan_stats (Transact-SQL)

[!INCLUDE [sqlserver2019-asdb-asdbmi](../../includes/applies-to-version/sqlserver2019-asdb-asdbmi.md)]

Returns the equivalent of the last known actual execution plan for a previously cached query plan.

## Syntax

```syntaxsql
sys.dm_exec_query_plan_stats ( plan_handle )
```

## Arguments

#### *plan_handle*

A token that uniquely identifies a query execution plan for a batch that has executed and its plan resides in the plan cache, or is currently executing. *plan_handle* is **varbinary(64)**.

The *plan_handle* can be obtained from the following dynamic management objects:

- [sys.dm_exec_cached_plans (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md)
- [sys.dm_exec_query_stats (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)
- [sys.dm_exec_requests (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)
- [sys.dm_exec_procedure_stats (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-exec-procedure-stats-transact-sql.md)
- [sys.dm_exec_trigger_stats (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-exec-trigger-stats-transact-sql.md)

## Table returned

| Column name | Data type | Description |
| --- | --- | --- |
| **dbid** | **smallint** | ID of the context database that was in effect when the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement corresponding to this plan was compiled. For ad hoc and prepared SQL statements, the ID of the database where the statements were compiled.<br /><br />Column is nullable. |
| **objectid** | **int** | ID of the object (for example, stored procedure or user-defined function) for this query plan. For ad hoc and prepared batches, this column is **null**.<br /><br />Column is nullable. |
| **number** | **smallint** | Numbered stored procedure integer. For example, a group of procedures for the **orders** application may be named **orderproc;1**, **orderproc;2**, and so on. For ad hoc and prepared batches, this column is **null**.<br /><br />Column is nullable. |
| **encrypted** | **bit** | Indicates whether the corresponding stored procedure is encrypted.<br /><br />0 = not encrypted<br /><br />1 = encrypted<br /><br />Column isn't nullable. |
| **query_plan** | **xml** | Contains the last known runtime Showplan representation of the actual query execution plan that is specified with *plan_handle*. The Showplan is in XML format. One plan is generated for each batch that contains, for example ad hoc [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, stored procedure calls, and user-defined function calls.<br /><br />Column is nullable. |

## Remarks

This is an opt-in feature. To enable at the server level, use [Trace Flag 2451](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf2451). To enable at the database level, use the LAST_QUERY_PLAN_STATS option in [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).

This system function works under the *lightweight* query execution statistics profiling infrastructure. For more information, see [Query Profiling Infrastructure](../../relational-databases/performance/query-profiling-infrastructure.md).

The Showplan output by `sys.dm_exec_query_plan_stats` contains the following information:

- All the compile-time information found in the cached plan
- Runtime information such as the actual number of rows per operator, the total query CPU time and execution time, spill warnings, actual DOP, the maximum used memory and granted memory

Under the following conditions, a Showplan output *equivalent to an actual execution plan* is returned in the `query_plan` column of the returned table for `sys.dm_exec_query_plan_stats`:

- The plan can be found in [sys.dm_exec_cached_plans](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md).

    **AND**

- The query being executed is complex or resource consuming.

Under the following conditions, a *simplified* <sup>1</sup> Showplan output is returned in the `query_plan` column of the returned table for `sys.dm_exec_query_plan_stats`:

- The plan can be found in [sys.dm_exec_cached_plans](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md).

    **AND**

- The query is simple enough, usually categorized as part of an OLTP workload.

<sup>1</sup> Refers to a Showplan that only contains the root node operator (SELECT).

Under the following conditions, *no output is returned* from `sys.dm_exec_query_plan_stats`:

- The query plan that is specified by using `plan_handle` has been evicted from the plan cache.

    **OR**

- The query plan wasn't cacheable in the first place. For more information, see [Execution Plan Caching and Reuse](../../relational-databases/query-processing-architecture-guide.md#execution-plan-caching-and-reuse).

> [!NOTE]  
> A limitation in the number of nested levels allowed in the **xml** data type, means that `sys.dm_exec_query_plan` cannot return query plans that meet or exceed 128 levels of nested elements. In earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this condition prevented the query plan from returning and generates [error 6335](../errors-events/database-engine-events-and-errors-6000-to-6999.md). In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 2 and later versions, the `query_plan` column returns NULL.

## Permissions

 Requires `VIEW SERVER STATE` permission on the server.

### Permissions for SQL Server 2022 and later

Requires VIEW SERVER PERFORMANCE STATE permission on the server.

## Examples

### A. Look at last known actual query execution plan for a specific cached plan

The following example queries `sys.dm_exec_cached_plans` to find the interesting plan and copy its `plan_handle` from the output.

```sql
SELECT * FROM sys.dm_exec_cached_plans;
GO
```

Then, to obtain the last known actual query execution plan, use the copied `plan_handle` with system function `sys.dm_exec_query_plan_stats`.

```sql
SELECT * FROM sys.dm_exec_query_plan_stats(< copied plan_handle >);
GO
```

### B. Look at last known actual query execution plan for all cached plans

```sql
SELECT *
FROM sys.dm_exec_cached_plans AS cp
CROSS APPLY sys.dm_exec_sql_text(plan_handle) AS st
CROSS APPLY sys.dm_exec_query_plan_stats(plan_handle) AS qps;
GO
```

### C. Look at last known actual query execution plan for a specific cached plan and query text

```sql
SELECT *
FROM sys.dm_exec_cached_plans AS cp
CROSS APPLY sys.dm_exec_sql_text(plan_handle) AS st
CROSS APPLY sys.dm_exec_query_plan_stats(plan_handle) AS qps
WHERE st.text LIKE 'SELECT * FROM Person.Person%';
GO
```

### D. Look at cached events for trigger

```sql
SELECT *
FROM sys.dm_exec_cached_plans
CROSS APPLY sys.dm_exec_query_plan_stats(plan_handle)
WHERE objtype ='Trigger';
GO
```

## See also

- [Trace Flags](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)
- [Dynamic Management Views and Functions (Transact-SQL)](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
- [Execution Related Dynamic Management Views (Transact-SQL)](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)
