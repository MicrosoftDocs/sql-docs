---
title: "sp_query_store_remove_query (Transact-SQL)"
description: "sp_query_store_remove_query (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: 09/30/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "SP_QUERY_STORE_REMOVE_QUERY"
  - "SP_QUERY_STORE_REMOVE_QUERY_TSQL"
  - "SYS.SP_QUERY_STORE_REMOVE_QUERY"
  - "SYS.SP_QUERY_STORE_REMOVE_QUERY_TSQL"
helpviewer_keywords:
  - "sys.sp_query_store_remove_query"
  - "sp_query_store_remove_query"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_query_store_remove_query (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

  Removes the query and all associated plans and runtime stats from the query store.

 :::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_query_store_remove_query [ @query_id = ] query_id [;]
```

## Arguments

#### [ @query_id = ] query_id

 The `query_id` of the query to be removed from the Query Store. `query_id` is a **bigint**, with no default.

## Return Code Values

 0 (success) or 1 (failure)

## Remarks

## Permissions

 Requires the **ALTER** permission on the database.

## Examples

 The following example returns information about the queries in the Query Store.

```sql
SELECT Txt.query_text_id, Txt.query_sql_text, Pl.plan_id, Qry.*
FROM sys.query_store_plan AS Pl
JOIN sys.query_store_query AS Qry
    ON Pl.query_id = Qry.query_id
JOIN sys.query_store_query_text AS Txt
    ON Qry.query_text_id = Txt.query_text_id ;
```

 After you identify the `query_id` that you want to delete, use the following example to delete the query.

```sql
EXEC sp_query_store_remove_query 3;
```

## Next steps

- [sp_query_store_force_plan (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-force-plan-transact-sql.md)
- [sp_query_store_remove_plan &#40;Transct-SQL&#41;](../../relational-databases/system-stored-procedures/sp-query-store-remove-plan-transct-sql.md)
- [sp_query_store_unforce_plan (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-unforce-plan-transact-sql.md)
- [sp_query_store_reset_exec_stats (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-reset-exec-stats-transact-sql.md)
- [sp_query_store_flush_db (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-flush-db-transact-sql.md)
- [Query Store Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/query-store-catalog-views-transact-sql.md)
- [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
