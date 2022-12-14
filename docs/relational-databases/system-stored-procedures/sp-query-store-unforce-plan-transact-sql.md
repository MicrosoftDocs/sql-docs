---
title: "sp_query_store_unforce_plan (Transact-SQL)"
description: "sp_query_store_unforce_plan (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: 09/30/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "SP_QUERY_STORE_UNFORCE_PLAN_TSQL"
  - "SP_QUERY_STORE_UNFORCE_PLAN"
  - "SYS.SP_QUERY_STORE_UNFORCE_PLAN"
  - "SYS.SP_QUERY_STORE_UNFORCE_PLAN_TSQL"
helpviewer_keywords:
  - "sys.sp_query_store_unforce_plan"
  - "sp_query_store_unforce_plan"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_query_store_unforce_plan (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

  Enables unforcing a previously forced plan for a particular query.

 :::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_query_store_unforce_plan 
    [ @query_id = ] query_id , 
    [ @plan_id = ] plan_id, 
    [ @force_plan_scope = ] replica_group_id [;]
```

## Arguments

#### [ @query_id = ] query_id
 Is the `query_id` of the query. `query_id` is a **bigint**, with no default.

#### [ @plan_id = ] plan_id
 Is the `plan_id` of the query plan that will no longer be enforced. `plan_id` is a **bigint**, with no default.

#### [ @force_plan_scope = ] replica_group_id

 You can force and unforce plans on a secondary replica when [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md) is enabled. Execute `sp_query_store_force_plan` and `sp_query_store_unforce_plan` on the secondary replica. The optional *force_plan_scope* argument defaults only to the local replica, but you can optionally specify a `replica_group_id` referencing [sys.query_store_plan_forcing_locations](../system-catalog-views/sys-query-store-plan-forcing-locations-transact-sql.md).

## Return Code Values

 0 (success) or 1 (failure)

## Remarks

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

 After you identify the `query_id` and `plan_id` that you want to unforce, use the following example to unforce the plan.

```sql
EXEC sp_query_store_unforce_plan 3, 3;
```

## Next steps

- [sys.query_store_replicas (Transct-SQL)](../system-catalog-views/sys-query-store-replicas.md)
- [sys.query_store_plan_forcing_locations (Transact-SQL)](../system-catalog-views/sys-query-store-plan-forcing-locations-transact-sql.md)
- [sp_query_store_force_plan (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-force-plan-transact-sql.md)
- [sp_query_store_remove_plan (Transct-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-remove-plan-transct-sql.md)
- [sp_query_store_remove_query (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-remove-query-transact-sql.md)
- [sp_query_store_reset_exec_stats (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-reset-exec-stats-transact-sql.md)
- [sp_query_store_flush_db (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-flush-db-transact-sql.md)
- [Query Store Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/query-store-catalog-views-transact-sql.md)
- [Monitoring Performance by using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [Best Practice with the Query Store](../../relational-databases/performance/best-practice-with-the-query-store.md#CheckForced)
