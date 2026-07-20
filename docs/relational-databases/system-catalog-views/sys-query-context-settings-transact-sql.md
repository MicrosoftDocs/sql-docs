---
title: "sys.query_context_settings (Transact-SQL)"
description: sys.query_context_settings contains information about the semantics affecting context settings associated with a query.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/26/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "QUERY_CONTEXT_SETTINGS_TSQL"
  - "SYS.QUERY_CONTEXT_SETTINGS_TSQL"
  - "SYS.QUERY_CONTEXT_SETTINGS"
  - "QUERY_CONTEXT_SETTINGS"
helpviewer_keywords:
  - "sys.query_context_settings catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || =azure-sqldw-latest || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.query_context_settings (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricsqldb.md)]

Contains information about the semantics affecting context settings associated with a query. There are several context settings available in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that influence the query semantics (defining the correct result of the query). The same query text compiled under different settings might produce different results (depending on the underlying data).

| Column name | Data type | Description |
| --- | --- | --- |
| `context_settings_id` | **bigint** | Primary key. This value is exposed in Showplan XML for queries. |
| `set_options` | **varbinary(8)** | Bit mask reflecting state of several SET options. For more information, see [sys.dm_exec_plan_attributes](../system-dynamic-management-views/sys-dm-exec-plan-attributes-transact-sql.md). |
| `language_id` | **smallint** | The ID of the language. For more information, see [sys.syslanguages](../system-compatibility-views/sys-syslanguages-transact-sql.md). |
| `date_format` | **smallint** | The date format. For more information, see [SET DATEFORMAT](../../t-sql/statements/set-dateformat-transact-sql.md). |
| `date_first` | **tinyint** | The date first value. For more information, see [SET DATEFIRST](../../t-sql/statements/set-datefirst-transact-sql.md). |
| `status` | **varbinary(2)** | Bitmask field that indicates type of query or context in which query was executed.<br />Column value can be combination of multiple flags (expressed in hexadecimal):<br /><br />`0x0` - regular query (no specific flags)<br /><br />`0x1` - query that was executed through one of the cursor APIs stored procedures<br /><br />`0x2` - query for notification<br /><br />`0x4` - internal query<br /><br />`0x8` - auto parameterized query without universal parameterization<br /><br />`0x10` - cursor fetch refresh query<br /><br />`0x20` - query that's being used in cursor update requests<br /><br />`0x40` - initial result set is returned when a cursor is opened (Cursor Auto Fetch)<br /><br />`0x80` - encrypted query<br /><br />`0x100` - query in context of row-level security predicate |
| `required_cursor_options` | **int** | Cursor options specified by the user such as the cursor type. |
| `acceptable_cursor_options` | **int** | Cursor options that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] might implicitly convert to in order to support the execution of the statement. |
| `merge_action_type` | **smallint** | The type of trigger execution plan used as the result of a `MERGE` statement.<br /><br />0 indicates a non-trigger plan, a trigger plan that doesn't execute as the result of a `MERGE` statement, or a trigger plan that executes as the result of a `MERGE` statement that only specifies a `DELETE` action.<br /><br />1 indicates an `INSERT` trigger plan that runs as the result of a `MERGE` statement.<br /><br />2 indicates an `UPDATE` trigger plan that runs as the result of a `MERGE` statement.<br /><br />3 indicates a `DELETE` trigger plan that runs as the result of a `MERGE` statement containing a corresponding `INSERT` or `UPDATE` action.<br /><br />For nested triggers run by cascading actions, this value is the action of the `MERGE` statement that caused the cascade. |
| `default_schema_id` | **int** | ID of the default schema, which is used to resolve names that aren't fully qualified. |
| `is_replication_specific` | **bit** | Used for replication. |
| `is_contained` | **varbinary(1)** | 1 indicates a contained database. |

## Permissions

Requires the `VIEW DATABASE STATE` permission.

### Permissions for SQL Server 2022 and later

Requires the `VIEW DATABASE PERFORMANCE STATE` permission on the database.

## Related content

- [sys.database_query_store_options](sys-database-query-store-options-transact-sql.md)
- [sys.query_store_plan](sys-query-store-plan-transact-sql.md)
- [sys.query_store_query](sys-query-store-query-transact-sql.md)
- [sys.query_store_query_text](sys-query-store-query-text-transact-sql.md)
- [sys.query_store_runtime_stats](sys-query-store-runtime-stats-transact-sql.md)
- [sys.query_store_wait_stats](sys-query-store-wait-stats-transact-sql.md)
- [sys.query_store_runtime_stats_interval](sys-query-store-runtime-stats-interval-transact-sql.md)
- [Monitor performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Query Store stored procedures (Transact-SQL)](../system-stored-procedures/query-store-stored-procedures-transact-sql.md)
- [sys.fn_stmt_sql_handle_from_sql_stmt](../system-functions/sys-fn-stmt-sql-handle-from-sql-stmt-transact-sql.md)
