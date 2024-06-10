---
title: "sys.query_store_plan (Transact-SQL)"
description: The sys.query_store_plan view contains information about each execution plan associated with a query.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wiassaf
ms.date: 02/12/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "QUERY_STORE_PLAN_TSQL"
  - "SYS.QUERY_STORE_PLAN"
  - "SYS.QUERY_STORE_PLAN_TSQL"
  - "QUERY_STORE_PLAN"
helpviewer_keywords:
  - "query_store_plan catalog view"
  - "sys.query_store_plan catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || =azure-sqldw-latest || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sys.query_store_plan (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

Contains information about each execution plan associated with a query.

| Column name | Data type | Description |
| --- | --- | --- |
| `plan_id` | **bigint** | Primary key. |
| `query_id` | **bigint** | Foreign key. Joins to [sys.query_store_query (Transact-SQL)](../../relational-databases/system-catalog-views/sys-query-store-query-transact-sql.md). |
| `plan_group_id` | **bigint** | ID of the plan group. Cursor queries typically require multiple (populate and fetch) plans. Populate and fetch plans that are compiled together are in the same group.<br /><br />`0` means plan isn't in a group. |
| `engine_version` | **nvarchar(32)** | Version of the engine used to compile the plan in `<major>.<minor>.<build>.<revision>` format. |
| `compatibility_level` | **smallint** | Database compatibility level of the database referenced in the query. |
| `query_plan_hash` | **binary(8)** | MD5 hash of the individual plan. |
| `query_plan` | **nvarchar(max)** | Showplan XML for the query plan. |
| `is_online_index_plan` | **bit** | Plan was used during an online index build.<br /><br />**Note:** [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)] always returns `0`. |
| `is_trivial_plan` | **bit** | Plan is a trivial plan (output in stage 0 of query optimizer).<br /><br />**Note:** [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)] always returns `0`. |
| `is_parallel_plan` | **bit** | Plan is parallel.<br /><br />**Note:** [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)] always returns `1`. |
| `is_forced_plan` | **bit** | Plan is marked as forced when user executes stored procedure `sys.sp_query_store_force_plan`. The forcing mechanism *doesn't guarantee* that this exact plan will be used for the query referenced by `query_id`. Plan forcing causes query to be compiled again, and typically produces exactly the same or a similar plan to the plan referenced by `plan_id`. If plan forcing doesn't succeed, `force_failure_count` is incremented, and `last_force_failure_reason` is populated with the failure reason.<br /><br />**Note:** [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)] always returns `0`. |
| `is_natively_compiled` | **bit** | Plan includes natively compiled memory optimized procedures. (0 = `FALSE`, 1 = `TRUE`).<br /><br />**Note:** [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)] always returns `0`. |
| `force_failure_count` | **bigint** | Number of times that forcing this plan has failed. It can be incremented only when the query is recompiled (*not on every execution*). Resets to `0` every time `is_plan_forced` is changed from `FALSE` to `TRUE`.<br /><br />**Note:** [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)] always returns `0`. |
| `last_force_failure_reason` | **int** | Reason why plan forcing failed.<br /><br />0: no failure, otherwise error number of the error that caused the forcing to fail<br />3617: `COMPILATION_ABORTED_BY_CLIENT`<br />8637: `ONLINE_INDEX_BUILD`<br />8675: `OPTIMIZATION_REPLAY_FAILED`<br />8683: `INVALID_STARJOIN`<br />8684: `TIME_OUT`<br />8689: `NO_DB`<br />8690: `HINT_CONFLICT`<br />8691: `SETOPT_CONFLICT`<br />8694: `DQ_NO_FORCING_SUPPORTED`<br />8698: `NO_PLAN`<br />8712: `NO_INDEX`<br />8713: `VIEW_COMPILE_FAILED`<br />\<other value>: `GENERAL_FAILURE`<br /><br />**Note:** [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)] always returns `0`. |
| `last_force_failure_reason_desc` | **nvarchar(128)** | Textual description of `last_force_failure_reason`.<br /><br />`COMPILATION_ABORTED_BY_CLIENT`: client aborted query compilation before it completed<br />`ONLINE_INDEX_BUILD`: query tries to modify data while target table has an index that is being built online<br />`OPTIMIZATION_REPLAY_FAILED`: The optimization replay script failed to execute.<br />`INVALID_STARJOIN`: plan contains invalid StarJoin specification<br />`TIME_OUT`: Optimizer exceeded number of allowed operations while searching for plan specified by forced plan<br />`NO_DB`: A database specified in the plan doesn't exist<br />`HINT_CONFLICT`: Query can't be compiled because plan conflicts with a query hint<br />`DQ_NO_FORCING_SUPPORTED`: Can't execute query because plan conflicts with use of distributed query or full-text operations.<br />`NO_PLAN`: Query processor couldn't produce query plan, because forced plan couldn't be verified as valid for the query<br />`NO_INDEX`: Index specified in plan no longer exists<br />`VIEW_COMPILE_FAILED`: Couldn't force query plan because of a problem in an indexed view referenced in the plan<br />`GENERAL_FAILURE`: general forcing error (not covered with other reasons)<br /><br />**Note:** [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)] always returns `NONE`. |
| `count_compiles` | **bigint** | Plan compilation statistics. |
| `initial_compile_start_time` | **datetimeoffset** | Plan compilation statistics. |
| `last_compile_start_time` | **datetimeoffset** | Plan compilation statistics. |
| `last_execution_time` | **datetimeoffset** | Last execution time refers to the last end time of the query/plan. |
| `avg_compile_duration` | **float** | Plan compilation statistics, in microseconds. Divide by 1,000,000 to get seconds. |
| `last_compile_duration` | **bigint** | Plan compilation statistics, in microseconds. Divide by 1,000,000 to get seconds. |
| `plan_forcing_type` | **int** | **Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions<br /><br />Plan forcing type.<br /><br />0: `NONE`<br />1: `MANUAL`<br />2: `AUTO` |
| `plan_forcing_type_desc` | **nvarchar(60)** | **Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions<br /><br />Text description of `plan_forcing_type`.<br /><br />`NONE`: No plan forcing<br />`MANUAL`: Plan forced by user<br />`AUTO`: Plan forced by automatic tuning. |
| `has_compile_replay_script` | **bit** | **Applies to:** [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and later versions<br /><br />Indicates whether the plan has an optimization replay script associated with it:<br />0 = No optimization replay script (none or even invalid).<br />1 = optimization replay script recorded.<br /><br />Not applicable to [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)]. |
| `is_optimized_plan_forcing_disabled` | **bit** | **Applies to:** [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and later versions<br /><br />Indicates whether optimized plan forcing was disabled for the plan:<br />0 = disabled.<br />1 = not disabled.<br /><br />Not applicable to [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)]. |
| `plan_type` | **int** | **Applies to:** [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and later versions<br /><br />Plan type.<br />0: Compiled Plan<br />1: Dispatcher Plan<br />2: Query Variant Plan<br /><br />Not applicable to [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)]. |
| `plan_type_desc` | **nvarchar(120)** | **Applies to:** [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and later versions<br /><br />Text description of the plan type.<br />Compiled Plan: Indicates that the plan is a non-parameter sensitive plan optimized plan<br />Dispatcher Plan: Indicates that the plan is a parameter sensitive plan optimized dispatcher plan<br />Query Variant Plan: Indicates that the plan is a parameter sensitive plan optimized query variant plan<br /><br />Not applicable to [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)]. |

## Remarks

More than one plan can be forced when [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md) is enabled.

In [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)], using columns `has_compile_replay_script`, `is_optimized_plan_forcing_disabled`, `plan_type`, `plan_type_desc` results in an `Invalid Column Name` error as they aren't supported. See [Example B](#b-query-to-view-query-plan-results-in-azure-synapse-analytics) for an example of how to use `sys.query_store_plan` in [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)].

## Plan forcing limitations

Query Store has a mechanism to enforce Query Optimizer to use certain execution plan. However, there are some limitations that can prevent a plan to be enforced.

First, if the plan contains following constructions:

- Insert bulk statement
- Reference to an external table
- Distributed query or full-text operations
- Use of [elastic queries](/azure/azure-sql/database/elastic-query-overview)
- Dynamic or keyset cursors
- Invalid star join specification

> [!NOTE]  
> Azure SQL Database and SQL Server 2019 and later build versions support plan forcing for static and fast forward cursors.

Second, when objects that plan relies on, are no longer available:

- Database (if database, where plan originated, doesn't exist anymore)
- Index (no longer there or disabled)

Finally, problems with the plan itself:

- Not legal for query
- Query Optimizer exceeded number of allowed operations
- Incorrectly formed plan XML

## Permissions

Requires the `VIEW DATABASE STATE` permission.

## Examples

### A. Find the reason SQL Server couldn't force a plan via QDS

Pay attention to the `last_force_failure_reason_desc` and `force_failure_count` columns:

```sql
SELECT TOP 1000
    p.query_id,
    p.plan_id,
    p.last_force_failure_reason_desc,
    p.force_failure_count,
    p.last_compile_start_time,
    p.last_execution_time,
    q.last_bind_duration,
    q.query_parameterization_type_desc,
    q.context_settings_id,
    c.set_options,
    c.STATUS
FROM sys.query_store_plan p
INNER JOIN sys.query_store_query q
    ON p.query_id = q.query_id
INNER JOIN sys.query_context_settings c
    ON c.context_settings_id = q.context_settings_id
LEFT JOIN sys.query_store_query_text t
    ON q.query_text_id = t.query_text_id
WHERE p.is_forced_plan = 1
    AND p.last_force_failure_reason != 0;
```

### B. Query to view query plan results in Azure Synapse Analytics

Use the following sample query to find the 100 most recent execution plans in the Query Store in [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)].

```sql
SELECT TOP 100
    plan_id,
    query_id,
    plan_group_id,
    engine_version,
    compatibility_level,
    query_plan_hash,
    query_plan,
    is_online_index_plan,
    is_trivial_plan,
    is_parallel_plan,
    is_forced_plan,
    is_natively_compiled,
    force_failure_count,
    last_force_failure_reason,
    last_force_failure_reason_desc,
    count_compiles,
    initial_compile_start_time,
    last_compile_start_time,
    last_execution_time,
    avg_compile_duration,
    last_compile_duration,
    plan_forcing_type,
    plan_forcing_type_desc
FROM sys.query_store_plan
ORDER BY last_execution_time DESC;
```

## Related content

- [Monitor performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
- [sys.database_query_store_options (Transact-SQL)](sys-database-query-store-options-transact-sql.md)
- [sys.query_context_settings (Transact-SQL)](sys-query-context-settings-transact-sql.md)
- [sys.query_store_query (Transact-SQL)](sys-query-store-query-transact-sql.md)
- [sys.query_store_query_text (Transact-SQL)](sys-query-store-query-text-transact-sql.md)
- [sys.query_store_runtime_stats (Transact-SQL)](sys-query-store-runtime-stats-transact-sql.md)
- [sys.query_store_wait_stats (Transact-SQL)](sys-query-store-wait-stats-transact-sql.md)
- [sys.query_store_runtime_stats_interval (Transact-SQL)](sys-query-store-runtime-stats-interval-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Query Store stored procedures (Transact-SQL)](../system-stored-procedures/query-store-stored-procedures-transact-sql.md)
