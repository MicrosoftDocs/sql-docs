---
title: "sys.dm_db_vector_indexes (Transact-SQL)"
description: sys.dm_db_vector_indexes provides real-time insights into vector index health and performance for monitoring and diagnostics.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: pookam, randolphwest, wiassaf
ms.date: 09/22/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
ms.custom:
  - ignite-2025
ai-usage: ai-assisted
f1_keywords:
  - "sys.dm_db_vector_indexes"
  - "sys.dm_db_vector_indexes_TSQL"
  - "dm_db_vector_indexes"
  - "dm_db_vector_indexes_TSQL"
helpviewer_keywords:
  - "sys.dm_db_vector_indexes dynamic management view"
  - "vector indexes [SQL Server], monitoring"
dev_langs:
  - TSQL
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# sys.dm_db_vector_indexes (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

The `sys.dm_db_vector_indexes` dynamic management view returns real-time information about vector index health and background maintenance. Use this view to monitor DiskANN graph catch-up operations and identify vector indexes that might require attention.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

| Column name | Data type | Description |
| --- | --- | --- |
| `object_id` | **int** | Object ID of the table that contains the vector index. |
| `index_id` | **int** | ID of the vector index. |
| `graph_catchup_pending_percent` | **decimal(10,2)** | Approximate percentage of changes waiting to be incorporated into the DiskANN graph. The value moves toward zero as background maintenance processes the pending changes. The value can be `NULL` if no graph catch-up task has executed. |
| `quantized_keys_used_percent` | **decimal(10,2)** | Percentage of the quantized key space used by the vector index. |
| `last_background_task_execution_time` | **datetime2** | Time when the most recent background maintenance task executed. `NULL` if no background task has executed. |
| `last_background_task_succeeded` | **bit** | Success status of the most recent background maintenance task. `1` indicates success, `0` indicates failure, and `NULL` indicates that no background task has executed. |
| `last_background_task_duration_seconds` | **bigint** | Duration of the most recent background maintenance task, in seconds. `NULL` if no background task has executed. |
| `last_background_task_processed_inserts` | **bigint** | Number of inserts processed by the most recent background maintenance task. `NULL` if no background task has executed. |
| `last_background_task_processed_deletes` | **bigint** | Number of deletes processed by the most recent background maintenance task. `NULL` if no background task has executed. |
| `last_background_task_error_message` | **nvarchar(max)** | Error message reported by the most recent background maintenance task. `NULL` if no error was reported or no background task has executed. |

## Remarks

This view returns one row for each vector index in the current database.

Vector indexes use asynchronous background maintenance to incorporate data modifications, including inserts, updates, and deletes, into the DiskANN graph. The `graph_catchup_pending_percent` column shows the approximate percentage of changes that are still waiting to be incorporated into the graph.

### Feature availability

The [vector data type](../../t-sql/data-types/vector-data-type.md) and [vector functions](../../t-sql/functions/vector-functions-transact-sql.md) are generally available in SQL Server 2025, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric.

[Vector indexes](../../t-sql/statements/create-vector-index-transact-sql.md) are generally available in Azure SQL Database, SQL database in Fabric, and Azure SQL Managed Instance configured with the **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy?view=azuresql-mi&preserve-view=true).

Vector indexes are preview features in SQL Server 2025 and Azure SQL Managed Instance configured with the **SQL Server 2025** update policy.

### Analyze graph_catchup_pending_percent

The `graph_catchup_pending_percent` column indicates the approximate percentage of data changes that aren't yet incorporated into the DiskANN graph.

When you insert, update, or delete rows in a table with a vector index, the changes aren't immediately incorporated into the graph. The changes are queued and processed asynchronously by a background maintenance task.

A temporary nonzero value after data modifications is expected. The percentage moves toward zero as background maintenance incorporates pending changes into the graph.

A value of zero indicates that graph maintenance has caught up. A NULL value can indicate that no graph catch-up task has executed yet.

### Impact on VECTOR_SEARCH queries

`VECTOR_SEARCH` continues to operate while graph catch-up is in progress. It uses the current DiskANN graph together with changes that aren't yet fully incorporated into the graph.

This behavior means:

- `VECTOR_SEARCH` can still consider recently inserted or updated rows.
- Rows that aren't fully incorporated into the graph can't take full advantage of graph navigation.
- Search performance or recall can be affected while a large graph catch-up backlog is being processed.
- Search quality and performance are most predictable when `graph_catchup_pending_percent` is zero or remains consistently low.

### Interpret graph catch-up values

There's no universal threshold for a high graph catch-up percentage. An appropriate threshold depends on the workload, rate of data modification, database configuration, and expected maintenance interval.

During batch loading or periods of high DML activity, expect a temporary increase in graph catch-up percentage. Monitor whether the value moves toward zero after the workload decreases.

Investigate the vector index if you observe any of the following conditions:

- **Sustained graph catch-up backlog:** `graph_catchup_pending_percent` remains elevated and doesn't move toward zero.
- **Background task failure:** `last_background_task_succeeded` is 0.
- **Background task error:** `last_background_task_error_message` contains an error.
- **Reduced recall:** `VECTOR_SEARCH` returns fewer relevant results than expected.
- **Unexpected performance degradation:** Vector search latency increases while the graph catch-up backlog remains elevated.

`NULL` values for the background task columns don't necessarily indicate a failure. They can indicate that no background maintenance task has executed yet.

### When to rebuild a vector index

Consider rebuilding a vector index when you observe measurable performance or recall degradation. Don't rebuild an index based only on a temporary nonzero `graph_catchup_pending_percent` value.

Scenarios in which rebuilding might be appropriate include:

- **Significant recall degradation:** Vector search returns fewer relevant results than expected after graph catch-up completes.
- **Large-scale data replacement:** Most or all embeddings are replaced, such as when data is re-embedded with a different model.
- **Persistent maintenance problems:** The graph catch-up percentage doesn't decrease and background maintenance repeatedly fails.

For more information, see [Data quality and maintenance guidance for vector indexes](../../t-sql/statements/create-vector-index-transact-sql.md#data-quality-and-maintenance-guidance-for-vector-indexes).

## Permissions

Requires `VIEW DATABASE STATE` permission on the database.

## Examples

### A. Monitor all vector indexes

The following query returns graph catch-up and background maintenance information for all vector indexes in the current database:

```sql
SELECT
    DB_NAME() AS database_name,
    OBJECT_SCHEMA_NAME(v.object_id) AS schema_name,
    OBJECT_NAME(v.object_id) AS table_name,
    i.name AS vector_index_name,
    v.graph_catchup_pending_percent,
    v.quantized_keys_used_percent,
    v.last_background_task_execution_time,
    v.last_background_task_succeeded,
    v.last_background_task_duration_seconds,
    v.last_background_task_processed_inserts,
    v.last_background_task_processed_deletes,
    v.last_background_task_error_message
FROM sys.dm_db_vector_indexes AS v
INNER JOIN sys.indexes AS i
    ON i.object_id = v.object_id
    AND i.index_id = v.index_id
ORDER BY
    v.graph_catchup_pending_percent DESC,
    schema_name,
    table_name,
    vector_index_name;
```

### B. Monitor a specific table

The following query returns vector index maintenance information for the `dbo.Articles` table:

```sql
SELECT
    OBJECT_SCHEMA_NAME(v.object_id) AS schema_name,
    OBJECT_NAME(v.object_id) AS table_name,
    i.name AS vector_index_name,
    v.graph_catchup_pending_percent,
    v.last_background_task_execution_time,
    v.last_background_task_succeeded,
    v.last_background_task_error_message
FROM sys.dm_db_vector_indexes AS v
INNER JOIN sys.indexes AS i
    ON i.object_id = v.object_id
    AND i.index_id = v.index_id
WHERE v.object_id = OBJECT_ID(N'dbo.Articles');
```

<a id="b-identify-indexes-needing-attention"></a>

### C. Identify indexes that might require attention

The following example uses 15 percent as an illustrative monitoring threshold. Select a threshold appropriate for your workload:

```sql
DECLARE @GraphCatchupThreshold decimal(10,2) = 15.0;

SELECT
    OBJECT_SCHEMA_NAME(v.object_id) AS schema_name,
    OBJECT_NAME(v.object_id) AS table_name,
    i.name AS vector_index_name,
    v.graph_catchup_pending_percent,
    v.last_background_task_execution_time,
    v.last_background_task_succeeded,
    v.last_background_task_error_message
FROM sys.dm_db_vector_indexes AS v
INNER JOIN sys.indexes AS i
    ON i.object_id = v.object_id
    AND i.index_id = v.index_id
WHERE
    v.graph_catchup_pending_percent > @GraphCatchupThreshold
    OR v.last_background_task_succeeded = 0
    OR v.last_background_task_error_message IS NOT NULL
ORDER BY v.graph_catchup_pending_percent DESC;
```

## Related content

- [CREATE VECTOR INDEX (Transact-SQL)](../../t-sql/statements/create-vector-index-transact-sql.md)
- [VECTOR_SEARCH (Transact-SQL)](../../t-sql/functions/vector-search-transact-sql.md)