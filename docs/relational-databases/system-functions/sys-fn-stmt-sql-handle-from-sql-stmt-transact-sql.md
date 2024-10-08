---
title: "sys.fn_stmt_sql_handle_from_sql_stmt (Transact-SQL)"
description: Gets the stmt_sql_handle for a Transact-SQL statement under a given parameterization type (simple or forced).
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/08/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# sys.fn_stmt_sql_handle_from_sql_stmt (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Gets the `stmt_sql_handle` for a [!INCLUDE [tsql](../../includes/tsql-md.md)] statement under the given parameterization type (simple or forced). You can refer to queries stored in the Query Store by using their `stmt_sql_handle` when you know their text.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.fn_stmt_sql_handle_from_sql_stmt
(
    N'query_sql_text'
    , [ query_param_type ]
)
[ ; ]
```

## Arguments

#### *query_sql_text*

The text of the query in the query store that you want the handle of. *query_sql_text* is **nvarchar(max)** with no default.

#### *query_param_type*

The parameter type of the query. *query_param_type* is **tinyint**, with a default of `NULL`. Possible values are:

| Value | Description |
| --- | --- |
| `NULL` (default) | Defaults to `0` |
| 0 | None |
| 1 | User |
| 2 | Simple |
| 3 | Forced |

## Columns returned

The following table lists the columns that `sys.fn_stmt_sql_handle_from_sql_stmt` returns.

| Column name | Type | Description |
| --- | --- | --- |
| `statement_sql_handle` | **varbinary(64)** | The SQL handle. |
| `query_sql_text` | **nvarchar(max)** | The text of the [!INCLUDE [tsql](../../includes/tsql-md.md)] statement. |
| `query_parameterization_type` | **tinyint** | The query parameterization type. |

## Return code values

`0` (success) or `1` (failure).

## Permissions

Requires `EXECUTE` permission on the database, and `DELETE` permission on the query store catalog views.

## Examples

The following example executes a statement, and then uses `sys.fn_stmt_sql_handle_from_sql_stmt` to return the SQL handle of that statement.

```sql
SELECT *
FROM sys.databases;

SELECT *
FROM sys.fn_stmt_sql_handle_from_sql_stmt('SELECT * FROM sys.databases', NULL);
```

Use the function to correlate Query Store data with other dynamic management views. The following example:

```sql
SELECT qt.query_text_id,
       q.query_id,
       qt.query_sql_text,
       qt.statement_sql_handle,
       q.context_settings_id,
       qs.statement_context_id
FROM sys.query_store_query_text AS qt
     INNER JOIN sys.query_store_query AS q
         ON qt.query_text_id = q.query_text_id
CROSS APPLY sys.fn_stmt_sql_handle_from_sql_stmt(qt.query_sql_text, NULL) AS fn_handle_from_stmt
     INNER JOIN sys.dm_exec_query_stats AS qs
         ON fn_handle_from_stmt.statement_sql_handle = qs.statement_sql_handle;
```

## Related content

- [sp_query_store_force_plan (Transact-SQL)](../system-stored-procedures/sp-query-store-force-plan-transact-sql.md)
- [sp_query_store_remove_plan (Transact-SQL)](../system-stored-procedures/sp-query-store-remove-plan-transact-sql.md)
- [sp_query_store_unforce_plan (Transact-SQL)](../system-stored-procedures/sp-query-store-unforce-plan-transact-sql.md)
- [sp_query_store_reset_exec_stats (Transact-SQL)](../system-stored-procedures/sp-query-store-reset-exec-stats-transact-sql.md)
- [sp_query_store_flush_db (Transact-SQL)](../system-stored-procedures/sp-query-store-flush-db-transact-sql.md)
- [sp_query_store_remove_query (Transact-SQL)](../system-stored-procedures/sp-query-store-remove-query-transact-sql.md)
- [Query Store catalog views (Transact-SQL)](../system-catalog-views/query-store-catalog-views-transact-sql.md)
- [Monitor performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
