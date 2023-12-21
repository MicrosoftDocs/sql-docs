---
title: "sys.query_store_query (Transact-SQL)"
description: Contains information about the query and its associated overall aggregated runtime execution statistics.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/16/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "QUERY_STORE_QUERY"
  - "SYS.QUERY_STORE_QUERY_TSQL"
  - "SYS.QUERY_STORE_QUERY"
  - "QUERY_STORE_QUERY_TSQL"
helpviewer_keywords:
  - "query_store_query catalog view"
  - "sys.query_store_query catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || =azure-sqldw-latest || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sys.query_store_query (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

Contains information about the query and its associated overall aggregated runtime execution statistics.

| Column name | Data type | Description |
| --- | --- | --- |
| `query_id` | **bigint** | Primary key. |
| `query_text_id` | **bigint** | Foreign key. Joins to [sys.query_store_query_text (Transact-SQL)](sys-query-store-query-text-transact-sql.md) |
| `context_settings_id` <sup>1</sup> | **bigint** | Foreign key. Joins to [sys.query_context_settings (Transact-SQL)](sys-query-context-settings-transact-sql.md). |
| `object_id` <sup>2</sup> | **bigint** | ID of the database object that the query is part of (stored procedure, trigger, CLR UDF/UDAgg, etc.). `0` if the query isn't executed as part of a database object (ad hoc query). |
| `batch_sql_handle` <sup>3</sup> | **varbinary(64)** | ID of the statement batch the query is part of. Populated only if query references temporary tables or table variables. |
| `query_hash` | **binary(8)** | Zobrist hash over the shape of the individual query, based on the bound (input) logical query tree. Query hints aren't included as part of the hash. |
| `is_internal_query` <sup>2</sup> | **bit** | The query was generated internally. |
| `query_parameterization_type` <sup>2</sup> | **tinyint** | Type of parameterization:<br /><br />`0` - None<br />`1` - User<br />`2` - Simple<br />`3` - Forced |
| `query_parameterization_type_desc` <sup>4</sup> | **nvarchar(60)** | Textual description for the parameterization type. |
| `initial_compile_start_time` | **datetimeoffset** | Compile start time. |
| `last_compile_start_time` | **datetimeoffset** | Compile start time. |
| `last_execution_time` | **datetimeoffset** | Last execution time refers to the last end time of the query/plan. |
| `last_compile_batch_sql_handle` | **varbinary(64)** | Handle of the last SQL batch in which query was used last time. It can be provided as input to [sys.dm_exec_sql_text (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md) to get the full text of the batch. |
| `last_compile_batch_offset_start` <sup>2</sup> | **bigint** | Information that can be provided to `sys.dm_exec_sql_text` along with `last_compile_batch_sql_handle`. |
| `last_compile_batch_offset_end` <sup>2</sup> | **bigint** | Information that can be provided to `sys.dm_exec_sql_text` along with `last_compile_batch_sql_handle`. |
| `count_compiles` <sup>1</sup> | **bigint** | Compilation statistics. |
| `avg_compile_duration` | **float** | Compilation statistics in microseconds. |
| `last_compile_duration` | **bigint** | Compilation statistics in microseconds. |
| `avg_bind_duration` <sup>2</sup> | **float** | Binding statistics in microseconds. |
| `last_bind_duration` <sup>2</sup> | **bigint** | Binding statistics. |
| `avg_bind_cpu_time` <sup>2</sup> | **float** | Binding statistics. |
| `last_bind_cpu_time` <sup>2</sup> | **bigint** | Binding statistics. |
| `avg_optimize_duration` | **float** | Optimization statistics in microseconds. |
| `last_optimize_duration` | **bigint** | Optimization statistics. |
| `avg_optimize_cpu_time` <sup>2</sup> | **float** | Optimization statistics in microseconds. |
| `last_optimize_cpu_time` <sup>2</sup> | **bigint** | Optimization statistics. |
| `avg_compile_memory_kb` <sup>2</sup> | **float** | Compile memory statistics. |
| `last_compile_memory_kb` <sup>2</sup> | **bigint** | Compile memory statistics. |
| `max_compile_memory_kb` <sup>2</sup> | **bigint** | Compile memory statistics. |
| `is_clouddb_internal_query` <sup>2</sup> | **bit** | Always `0` in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on-premises. |

<sup>1</sup> Azure Synapse Analytics always returns one (`1`).

<sup>2</sup> Azure Synapse Analytics always returns zero (`0`).

<sup>3</sup> Azure Synapse Analytics always returns `NULL`.

<sup>4</sup> Azure Synapse Analytics always returns `None`.

## Permissions

Requires the **VIEW DATABASE STATE** permission.

## Related content

- [sys.database_query_store_options (Transact-SQL)](sys-database-query-store-options-transact-sql.md)
- [sys.query_context_settings (Transact-SQL)](sys-query-context-settings-transact-sql.md)
- [sys.query_store_plan (Transact-SQL)](sys-query-store-plan-transact-sql.md)
- [sys.query_store_query_text (Transact-SQL)](sys-query-store-query-text-transact-sql.md)
- [sys.query_store_wait_stats (Transact-SQL)](sys-query-store-wait-stats-transact-sql.md)
- [sys.query_store_runtime_stats (Transact-SQL)](sys-query-store-runtime-stats-transact-sql.md)
- [sys.query_store_runtime_stats_interval (Transact-SQL)](sys-query-store-runtime-stats-interval-transact-sql.md)
- [sys.fn_stmt_sql_handle_from_sql_stmt (Transact-SQL)](../system-functions/sys-fn-stmt-sql-handle-from-sql-stmt-transact-sql.md)
- [Query Store hints](../performance/query-store-hints.md)
- [Monitor performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Query Store stored procedures (Transact-SQL)](../system-stored-procedures/query-store-stored-procedures-transact-sql.md)
