---
title: "sys.query_store_query_text (Transact-SQL)"
description: sys.query_store_query_text (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/01/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "SYS.QUERY_STORE_QUERY_TEXT"
  - "QUERY_STORE_QUERY_TEXT"
  - "SYS.QUERY_STORE_QUERY_TEXT_TSQL"
  - "QUERY_STORE_QUERY_TEXT_TSQL"
helpviewer_keywords:
  - "sys.query_store_query_text catalog view"
  - "query_store_query_text catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || =azure-sqldw-latest || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.query_store_query_text (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricsqldb.md)]

Contains the [!INCLUDE [tsql](../../includes/tsql-md.md)] text and the SQL handle of the query.

| Column name | Data type | Description |
| --- | --- | --- |
| `query_text_id` | **bigint** | Primary key. |
| `query_sql_text` | **nvarchar(max)** | SQL text of the query, as provided by the user. Includes whitespaces, hints, and comments. Comments and spaces before and after the query text are ignored. Comments and spaces inside text aren't ignored. |
| `statement_sql_handle` | **varbinary(64)** | SQL handle of the individual query. |
| `is_part_of_encrypted_module` <sup>1</sup> | **bit** | Query text is a part of an encrypted module. |
| `has_restricted_text` <sup>1</sup> | **bit** | Query text contains a password or other unmentionable words. |

<sup>1</sup> Azure Synapse Analytics always returns zero (`0`).

## Permissions

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions require `VIEW SERVER STATE` permission on the server.

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions require `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Related content

- [sys.database_query_store_options (Transact-SQL)](sys-database-query-store-options-transact-sql.md)
- [sys.query_context_settings (Transact-SQL)](sys-query-context-settings-transact-sql.md)
- [sys.query_store_plan (Transact-SQL)](sys-query-store-plan-transact-sql.md)
- [sys.query_store_query (Transact-SQL)](sys-query-store-query-transact-sql.md)
- [sys.query_store_runtime_stats (Transact-SQL)](sys-query-store-runtime-stats-transact-sql.md)
- [sys.query_store_wait_stats (Transact-SQL)](sys-query-store-wait-stats-transact-sql.md)
- [sys.query_store_runtime_stats_interval (Transact-SQL)](sys-query-store-runtime-stats-interval-transact-sql.md)
- [Monitor performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Query Store stored procedures (Transact-SQL)](../system-stored-procedures/query-store-stored-procedures-transact-sql.md)
- [sys.fn_stmt_sql_handle_from_sql_stmt (Transact-SQL)](../system-functions/sys-fn-stmt-sql-handle-from-sql-stmt-transact-sql.md)

