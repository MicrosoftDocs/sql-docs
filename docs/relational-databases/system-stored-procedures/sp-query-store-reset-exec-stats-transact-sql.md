---
title: "sp_query_store_reset_exec_stats (Transact-SQL)"
description: "Clears the runtime stats for a specific query plan from the Query Store."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/31/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_query_store_reset_exec_stats_TSQL"
  - "sys.sp_query_store_reset_exec_stats_TSQL"
  - "sys.sp_query_store_reset_exec_stats"
  - "sp_query_store_reset_exec_stats"
helpviewer_keywords:
  - "sp_query_store_reset_exec_stats"
  - "sys.sp_query_store_reset_exec_stats"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_query_store_reset_exec_stats (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Clears the runtime stats for a specific query plan from the Query Store. If you [enable Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md), `sp_query_store_reset_exec_stats` can only be executed against the primary replica. The procedure's scope applies to the entire replica set.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_query_store_reset_exec_stats [ @plan_id = ] plan_id
[ ; ]
```

## Arguments

#### [ @plan_id = ] *plan_id*

The ID of the query plan to be cleared. *@plan_id* is **bigint**, with no default.

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

After you identify the *plan_id* that you want to clear the statistics, use the following example to delete the execution stats for a specific query plan. This example deletes the execution stats for plan number 3.

```sql
EXEC sp_query_store_reset_exec_stats 3;
```

## Related content

- [sp_query_store_force_plan (Transact-SQL)](sp-query-store-force-plan-transact-sql.md)
- [sp_query_store_remove_query (Transact-SQL)](sp-query-store-remove-query-transact-sql.md)
- [sp_query_store_unforce_plan (Transact-SQL)](sp-query-store-unforce-plan-transact-sql.md)
- [sp_query_store_remove_plan (Transact-SQL)](sp-query-store-remove-plan-transact-sql.md)
- [sp_query_store_flush_db (Transact-SQL)](sp-query-store-flush-db-transact-sql.md)
- [Query Store Catalog Views (Transact-SQL)](../system-catalog-views/query-store-catalog-views-transact-sql.md)
- [Monitoring Performance By Using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
