---
title: "sp_query_store_unforce_plan (Transact-SQL)"
description: "Enables unforcing a previously forced plan for a particular query in the Query Store."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/31/2023
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

Enables unforcing a previously forced plan for a particular query in the Query Store.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_query_store_unforce_plan
    [ @query_id = ] query_id ,
    [ @plan_id = ] plan_id ,
    [ @force_plan_scope = ] 'replica_group_id'
[ ; ]
```

## Arguments

#### [ @query_id = ] *query_id*

The ID of the query. *@query_id* is **bigint**, with no default.

#### [ @plan_id = ] *plan_id*

The ID of the query plan that will no longer be enforced. *@plan_id* is **bigint**, with no default.

#### [ @force_plan_scope = ] '*replica_group_id*'

You can force and unforce plans on a secondary replica when [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md) is enabled. Execute `sp_query_store_force_plan` and `sp_query_store_unforce_plan` on the secondary replica. The optional *@force_plan_scope* argument defaults only to the local replica, but you can optionally specify a *replica_group_id* referencing [sys.query_store_plan_forcing_locations](../system-catalog-views/sys-query-store-plan-forcing-locations-transact-sql.md).

## Return code values

`0` (success) or `1` (failure).

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

After you identify the *query_id* and *plan_id* that you want to unforce, use the following example to unforce the plan.

```sql
EXEC sp_query_store_unforce_plan 3, 3;
```

## Related content

- [sys.query_store_replicas (Transact-SQL)](../system-catalog-views/sys-query-store-replicas.md)
- [sys.query_store_plan_forcing_locations (Transact-SQL)](../system-catalog-views/sys-query-store-plan-forcing-locations-transact-sql.md)
- [sp_query_store_force_plan (Transact-SQL)](sp-query-store-force-plan-transact-sql.md)
- [sp_query_store_remove_plan (Transact-SQL)](sp-query-store-remove-plan-transact-sql.md)
- [sp_query_store_remove_query (Transact-SQL)](sp-query-store-remove-query-transact-sql.md)
- [sp_query_store_reset_exec_stats (Transact-SQL)](sp-query-store-reset-exec-stats-transact-sql.md)
- [sp_query_store_flush_db (Transact-SQL)](sp-query-store-flush-db-transact-sql.md)
- [Query Store Catalog Views (Transact-SQL)](../system-catalog-views/query-store-catalog-views-transact-sql.md)
- [Monitoring Performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
- [Best Practice with the Query Store](../performance/best-practice-with-the-query-store.md#CheckForced)
