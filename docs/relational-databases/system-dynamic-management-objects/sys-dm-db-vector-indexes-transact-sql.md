---
title: "sys.dm_db_vector_indexes (Transact-SQL)"
description: sys.dm_db_vector_indexes provides real-time insights into vector index health and performance for monitoring and diagnostics.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: pookam, randolphwest, wiassaf
ms.date: 03/07/2026
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
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =fabric-sqldb"
---

# sys.dm_db_vector_indexes (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-fabricsqldb.md)]

Returns real-time insights into vector index health and performance. Use this view for monitoring vector index maintenance operations and identifying indexes that need attention.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

| Column name | Data type | Description |
| --- | --- | --- |
| `object_id` | **int** | Table object ID. |
| `index_id` | **int** | Index ID. |
| `approximate_staleness_percent` | **decimal(10,2)** | Percentage of changes pending index update. Higher values indicate more pending changes. |
| `quantized_keys_used_percent` | **decimal(10,2)** | Percentage of key space consumed by the index. |
| `last_background_task_time` | **datetime2** | Last background maintenance timestamp. Indicates when the last maintenance operation completed. |
| `last_background_task_succeeded` | **bit** | Success status of last maintenance task. 1 indicates success, 0 indicates failure. |
| `last_background_task_duration_seconds` | **bigint** | Duration of last maintenance task in seconds. |
| `last_background_task_processed_inserts` | **bigint** | Number of insert operations processed in the last maintenance task. |
| `last_background_task_processed_deletes` | **bigint** | Number of delete operations processed in the last maintenance task. |
| `last_background_task_error_message` | **nvarchar(max)** | Error message if the last maintenance task failed. NULL if the task succeeded. |

## Remarks

This view returns information for all vector indexes in the current database. Vector indexes perform background maintenance to incorporate DML changes (inserts, updates, deletes). The `approximate_staleness_percent` column indicates how many changes are pending incorporation into the index structure.

### Analyze approximate_staleness_percent

The `approximate_staleness_percent` column indicates what percentage of data changes haven't yet been processed by the background maintenance task that keeps your vector index up-to-date. When you insert, update, or delete rows in a table with a vector index, those changes don't immediately get incorporated into the DiskANN graph structure. Instead, the changes are queued and processed by a background maintenance task. The staleness percentage drops back toward 0% as the backlog is processed.

For example, if you have a table with 10,000 rows and a vector index, and you insert 500 new rows, the staleness percent is approximately 5% (500 pending changes out of 10,500 total rows). As the background maintenance processes these 500 inserts, the staleness percentage drops back toward zero.

#### Impact on VECTOR_SEARCH queries

`VECTOR_SEARCH` uses the current state of the DiskANN graph combined with pending changes that haven't been fully incorporated yet. This means:

- Even when staleness is higher than zero, your `VECTOR_SEARCH` queries still return results and include recently inserted or updated rows.
- However, the search algorithm can't leverage the full graph structure for optimal ranking until background maintenance completes.
- Ranking accuracy might be reduced. The similarity scores and ordering might be less optimal for rows not yet fully integrated into the index structure.

Search quality is best when all vectors are properly integrated into the graph and staleness percentage is zero.

#### Interpret staleness values

There's no universal threshold for "high" staleness because it depends on your workload pattern. Use these guidelines to interpret the values you see:

1. **During batch loads**: 20-30% staleness that drops to near zero within minutes is expected and normal.
1. **During regular operations**: 0-5% staleness indicates the background maintenance is keeping pace with your workload.

Investigate your index health if you encounter any of the following scenarios:

- **Sustained high staleness**: Values consistently above 10-15% during regular operations suggest the background maintenance can't keep up with your DML rate.
- **Reduced recall**: You notice a measurable drop in the relevance of `VECTOR_SEARCH` results.
- **Task failures**: The `last_background_task_succeeded` value is 0. The background process is encountering errors and can't update the index.

### When to rebuild a vector index

Consider rebuilding a vector index when you observe **performance or recall degradation**, not based solely on staleness percentage. Rebuild scenarios include:

- **Significant recall quality drop**: Vector search returns fewer relevant results than expected
- **Large-scale data replacement**: When most or all embeddings are replaced (for example, re-embedding with a new model)

For detailed guidance on data quality and maintenance scenarios, see [Data quality and maintenance guidance for vector indexes](../../t-sql/statements/create-vector-index-transact-sql.md#data-quality-and-maintenance-guidance-for-vector-indexes).

Monitor this metric to understand index maintenance patterns and identify indexes requiring attention.

## Permissions

Requires `VIEW DATABASE STATE` permission on the database.

## Examples

### A. Monitor all vector indexes

The following query monitors all vector indexes in the current database, showing staleness and maintenance status.

```sql
SELECT 
    DB_NAME() AS database_name,
    OBJECT_NAME(object_id) AS table_name,
    index_id,
    approximate_staleness_percent,
    last_background_task_succeeded
FROM sys.dm_db_vector_indexes
ORDER BY approximate_staleness_percent DESC;
```

### B. Identify indexes needing attention

The following query finds vector indexes with high staleness or recent maintenance failures.

```sql
SELECT 
    OBJECT_NAME(object_id) AS table_name,
    approximate_staleness_percent,
    last_background_task_error_message
FROM sys.dm_db_vector_indexes
WHERE 
    approximate_staleness_percent > 15.0  -- Example value, adjust based on your workload
    OR last_background_task_succeeded = 0  -- Recent failure
ORDER BY approximate_staleness_percent DESC;
```

## Related content

- [System dynamic management views](system-dynamic-management-objects.md)
- [Database related dynamic management views (Transact-SQL)](database-related-dynamic-management-views-transact-sql.md)
- [CREATE VECTOR INDEX (Transact-SQL)](../../t-sql/statements/create-vector-index-transact-sql.md)
- [VECTOR_SEARCH (Transact-SQL)](../../t-sql/functions/vector-search-transact-sql.md)
