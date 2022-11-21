---
title: "sp_query_store_remove_plan (Transact-SQL)"
description: "sp_query_store_remove_plan (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: 09/19/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "SYS.SP_QUERY_STORE_REMOVE_PLAN_TSQL"
  - "SP_QUERY_STORE_REMOVE_PLAN_TSQL"
  - "SP_QUERY_STORE_REMOVE_PLAN"
  - "SYS.SP_QUERY_STORE_REMOVE_PLAN"
helpviewer_keywords:
  - "sys.sp_query_store_remove_plan"
  - "sp_query_store_remove_plan"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_query_store_remove_plan (Transct-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

  Removes a single plan from the query store.

 :::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_query_store_remove_plan [ @plan_id = ] plan_id [;]
```

## Arguments

#### `[ @plan_id = ] plan_id`

 Is the id of the query plan to be removed. *plan_id* is a **bigint**, with no default.

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
    ON Qry.query_text_id = Txt.query_text_id;
```

 After you identify the `plan_id` that you want to delete, use the following example to delete a query plan.

```sql
EXEC sp_query_store_remove_plan 3;
```

## Next steps

- [sp_query_store_force_plan (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-force-plan-transact-sql.md)
- [sp_query_store_remove_query (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-remove-query-transact-sql.md)
- [sp_query_store_unforce_plan (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-unforce-plan-transact-sql.md)
- [sp_query_store_reset_exec_stats (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-reset-exec-stats-transact-sql.md)
- [sp_query_store_flush_db (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-flush-db-transact-sql.md)
- [Query Store Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/query-store-catalog-views-transact-sql.md)
- [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
