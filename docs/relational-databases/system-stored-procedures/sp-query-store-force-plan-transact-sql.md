---
title: "sp_query_store_force_plan (Transact-SQL)"
description: "The sp_query_store_force_plan stored procedure forces a plan in the Query Store."
author: markingmyname
ms.author: maghan
ms.date: 10/14/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom: event-tier1-build-2022
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
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_query_store_force_plan (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Enables forcing a particular plan for a particular query.

 When a plan is forced for a particular query, every time [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] encounters the query, it tries to force the plan in the Query Optimizer. If plan forcing fails, an Extended Event is fired and the Query Optimizer is instructed to optimize in the normal way.

 :::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_query_store_force_plan
    [ @query_id = ] query_id ,
    [ @plan_id = ] plan_id ,
    [ @disable_optimized_plan_forcing = ] disable_optimized_plan_forcing ,
    [ @force_plan_scope = ] replica_group_id [;]
```

## Arguments

#### [ @query_id = ] query_id

 Is the id of the query. *query_id* is a **bigint**, with no default.

#### [ @plan_id = ] plan_id

 Is the id of the query plan to be forced. *plan_id* is a **bigint**, with no default.

#### [ @disable_optimized_plan_forcing = ] disable_optimized_plan_forcing

 Indicates whether optimized plan forcing should be disabled. `disable_optimized_plan_forcing` is a **bit** with a default of 0.

#### [ @force_plan_scope = ] replica_group_id

 You can force plans on a secondary replica when [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md) is enabled. Execute `sp_query_store_force_plan` and `sp_query_store_unforce_plan` on the secondary replica. The optional *force_plan_scope* argument defaults only to the local replica (primary or secondary), but you can optionally specify a `replica_group_id` referencing [sys.query_store_replicas](../system-catalog-views/sys-query-store-replicas.md). 

## Return code values

 0 (success) or 1 (failure)

## Remarks

  The resulting execution plan forced by this feature will be the same or similar to the plan being forced. Because the resulting plan may not be identical to the plan specified by `sys.sp_query_store_force_plan`, the performance of the plans may vary. In rare cases, the performance difference may be significant and negative; in that case, the administrator must remove the forced plan.

Review forced plans on secondary replicas with [sys.query_store_plan_forcing_locations](../system-catalog-views/sys-query-store-plan-forcing-locations-transact-sql.md).

## Permissions

 Requires the **ALTER** permission on the database.

## Examples

 The following example returns information about the queries in the query store.

```sql
SELECT Txt.query_text_id, Txt.query_sql_text, Pl.plan_id, Qry.*
FROM sys.query_store_plan AS Pl
JOIN sys.query_store_query AS Qry
    ON Pl.query_id = Qry.query_id
JOIN sys.query_store_query_text AS Txt
    ON Qry.query_text_id = Txt.query_text_id ;
```

 After you identify the `query_id` and `plan_id` that you want to force, use the following example to force the query to use a plan.

```sql
EXEC sp_query_store_force_plan @query_id = 3, @plan_id = 3;
```

Use [sys.query_store_plan_forcing_locations](../system-catalog-views/sys-query-store-plan-forcing-locations-transact-sql.md), joined with [sys.query_store_replicas](../system-catalog-views/sys-query-store-replicas.md), to retrieve [Query Store plans forced on all secondary replicas](../performance/query-store-for-secondary-replicas.md).

```sql
SELECT query_plan 
FROM sys.query_store_plan AS qsp
    INNER JOIN sys.query_store_plan_forcing_locations AS pfl 
        ON pfl.query_id = qsp.query_id 
    INNER JOIN sys.query_store_replicas AS qsr
        ON qsr.replica_group_id = qsp.replica_group_id
WHERE qsr.replica_name = 'yourSecondaryReplicaName';
```

## Next steps

Learn more about related concepts in the following articles:

- [sys.query_store_plan_forcing_locations (Transact-SQL)](../system-catalog-views/sys-query-store-plan-forcing-locations-transact-sql.md)
- [sys.query_store_replicas (Transact-SQL)](../system-catalog-views/sys-query-store-replicas.md)
- [sys.query_store_plan_forcing_locations (Transact-SQL)](../system-catalog-views/sys-query-store-plan-forcing-locations-transact-sql.md)
- [sp_query_store_remove_plan (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-remove-plan-transct-sql.md)
- [sp_query_store_remove_query (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-remove-query-transact-sql.md)
- [sp_query_store_unforce_plan (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-unforce-plan-transact-sql.md)
- [Query Store Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/query-store-catalog-views-transact-sql.md)
- [Monitoring Performance by using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [sp_query_store_reset_exec_stats (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-reset-exec-stats-transact-sql.md)
- [sp_query_store_flush_db (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-flush-db-transact-sql.md)
- [Best Practice with the Query Store](../../relational-databases/performance/best-practice-with-the-query-store.md#CheckForced)
