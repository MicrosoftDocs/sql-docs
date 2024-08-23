---
title: "sp_query_store_force_plan (Transact-SQL)"
description: "Enables forcing a particular plan for a particular query in the Query Store."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "SYS.SP_QUERY_STORE_FORCE_PLAN"
  - "SP_QUERY_STORE_FORCE_PLAN"
  - "SYS.SP_QUERY_STORE_FORCE_PLAN_TSQL"
  - "SP_QUERY_STORE_FORCE_PLAN_TSQL"
helpviewer_keywords:
  - "sys.sp_query_store_force_plan"
  - "sp_query_store_force_plan"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_query_store_force_plan (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Enables forcing a particular plan for a particular query in the Query Store.

When a plan is forced for a particular query, every time [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] encounters the query, it tries to force the plan in the Query Optimizer. If plan forcing fails, an Extended Event is fired and the Query Optimizer is instructed to optimize in the normal way.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_query_store_force_plan
    [ @query_id = ] query_id ,
    [ @plan_id = ] plan_id ,
    [ @disable_optimized_plan_forcing = ] disable_optimized_plan_forcing ,
    [ @force_plan_scope = ] 'replica_group_id'
[ ; ]
```

## Arguments

#### [ @query_id = ] *query_id*

The ID of the query. *@query_id* is **bigint**, with no default.

#### [ @plan_id = ] *plan_id*

The ID of the query plan to be forced. *@plan_id* is **bigint**, with no default.

#### [ @disable_optimized_plan_forcing = ] *disable_optimized_plan_forcing*

Indicates whether optimized plan forcing should be disabled. *@disable_optimized_plan_forcing* is **bit** with a default of `0`.

#### [ @force_plan_scope = ] '*replica_group_id*'

You can force plans on a secondary replica when [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md) is enabled. Execute `sp_query_store_force_plan` and `sp_query_store_unforce_plan` on the secondary replica. The optional *@force_plan_scope* argument defaults only to the local replica (primary or secondary), but you can optionally specify a *replica_group_id* referencing [sys.query_store_replicas](../system-catalog-views/sys-query-store-replicas.md).

## Return code values

`0` (success) or `1` (failure).

## Remarks

The resulting execution plan forced by this feature is the same or similar to the plan being forced. Because the resulting plan might not be identical to the plan specified by `sys.sp_query_store_force_plan`, the performance of the plans might vary. In rare cases, the performance difference might be significant and negative; in that case, the administrator must remove the forced plan.

Review forced plans on secondary replicas with [sys.query_store_plan_forcing_locations](../system-catalog-views/sys-query-store-plan-forcing-locations-transact-sql.md).

## Permissions

Requires the ALTER permission on the database.

## Examples

The following example returns information about the queries in the Query Store.

```sql
SELECT txt.query_text_id,
    txt.query_sql_text,
    pl.plan_id,
    qry.*
FROM sys.query_store_plan AS pl
INNER JOIN sys.query_store_query AS qry
    ON pl.query_id = qry.query_id
INNER JOIN sys.query_store_query_text AS txt
    ON qry.query_text_id = txt.query_text_id;
```

After you identify the *query_id* and *plan_id* that you want to force, use the following example to force the query to use a plan.

```sql
EXEC sp_query_store_force_plan
    @query_id = 3,
    @plan_id = 3;
```

Use [sys.query_store_plan_forcing_locations](../system-catalog-views/sys-query-store-plan-forcing-locations-transact-sql.md), joined with [sys.query_store_replicas](../system-catalog-views/sys-query-store-replicas.md), to retrieve [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md).

```sql
SELECT query_plan
FROM sys.query_store_plan AS qsp
INNER JOIN sys.query_store_plan_forcing_locations AS pfl
    ON pfl.query_id = qsp.query_id
INNER JOIN sys.query_store_replicas AS qsr
    ON qsr.replica_group_id = qsp.replica_group_id
WHERE qsr.replica_name = 'yourSecondaryReplicaName';
```

## Related content

- [sys.query_store_plan_forcing_locations (Transact-SQL)](../system-catalog-views/sys-query-store-plan-forcing-locations-transact-sql.md)
- [sys.query_store_replicas (Transact-SQL)](../system-catalog-views/sys-query-store-replicas.md)
- [sp_query_store_remove_plan (Transact-SQL)](sp-query-store-remove-plan-transact-sql.md)
- [sp_query_store_remove_query (Transact-SQL)](sp-query-store-remove-query-transact-sql.md)
- [sp_query_store_unforce_plan (Transact-SQL)](sp-query-store-unforce-plan-transact-sql.md)
- [Query Store catalog views (Transact-SQL)](../system-catalog-views/query-store-catalog-views-transact-sql.md)
- [Monitor performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
- [sp_query_store_reset_exec_stats (Transact-SQL)](sp-query-store-reset-exec-stats-transact-sql.md)
- [sp_query_store_flush_db (Transact-SQL)](sp-query-store-flush-db-transact-sql.md)
- [Best Practice with the Query Store](../performance/best-practice-with-the-query-store.md#CheckForced)
