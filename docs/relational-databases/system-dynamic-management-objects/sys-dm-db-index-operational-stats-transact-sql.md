---
title: sys.dm_db_index_operational_stats (Transact-SQL)
description: sys.dm_db_index_operational_stats (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dfurman
ms.date: 07/08/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.dm_db_index_operational_stats_TSQL"
  - "sys.dm_db_index_operational_stats"
  - "dm_db_index_operational_stats_TSQL"
  - "dm_db_index_operational_stats"
helpviewer_keywords:
  - "sys.dm_db_index_operational_stats dynamic management function"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# sys.dm_db_index_operational_stats (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Returns the lower level data access, locking, and latching statistics for each partition of a table or index in a database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.dm_db_index_operational_stats (
    { database_id | NULL | 0 | DEFAULT }
    , { object_id | NULL | 0 | DEFAULT }
    , { index_id | 0 | NULL | -1 | DEFAULT }
    , { partition_number | NULL | 0 | DEFAULT }
)
```

## Arguments

#### { *database_id* | NULL | 0 | DEFAULT }

ID of the database. *database_id* is **smallint**. Valid inputs are the ID number of a database, `NULL`, `0`, or `DEFAULT`. The default is `0`. `NULL`, `0`, and `DEFAULT` are equivalent values in this context.

Specify `NULL` to return information for all databases in the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If you specify `NULL` for *database_id*, you must also specify `NULL` for *object_id*, *index_id*, and *partition_number*.

The built-in function [DB_ID](../../t-sql/functions/db-id-transact-sql.md) can be specified.

#### { *object_id* | NULL | 0 | DEFAULT }

Object ID of the table or view the index is on. *object_id* is **int**.

Valid inputs are the ID number of a table and view, `NULL`, `0`, or `DEFAULT`. The default is `0`. `NULL`, `0`, and `DEFAULT` are equivalent values in this context.

Specify `NULL` to return information for all tables and views in the specified database. If you specify `NULL` for *object_id*, you must also specify `NULL` for *index_id* and *partition_number*.

#### { *index_id* | 0 | NULL | -1 | DEFAULT }

ID of the index. *index_id* is **int**. Valid inputs are the ID number of an index, `0` if *object_id* is a heap, `NULL`, `-1`, or `DEFAULT`. The default is `-1`. `NULL`, `-1`, and `DEFAULT` are equivalent values in this context.

Specify `NULL` to return information for all indexes for a base table or view. If you specify `NULL` for *index_id*, you must also specify `NULL` for *partition_number*.

#### { *partition_number* | NULL | 0 | DEFAULT }

Partition number in the object. *partition_number* is **int**. Valid inputs are the *partition_number* of an index or heap, `NULL`, `0`, or `DEFAULT`. The default is `0`. `NULL`, `0`, and `DEFAULT` are equivalent values in this context.

Specify `NULL` to return information for all partitions of the index or heap.

*partition_number* is 1-based. A nonpartitioned index or heap has *partition_number* set to 1.

## Table returned

| Column name | Data type | Description |
| --- | --- | --- |
| `database_id` | **smallint** | Database ID.<br /><br />In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the values are unique within a single database or an elastic pool, but not within a logical server. |
| `object_id` | **int** | ID of the table or view. For more information, see [sys.objects](../system-catalog-views/sys-objects-transact-sql.md). |
| `index_id` | **int** | ID of the index or heap. For more information, see [sys.indexes](../system-catalog-views/sys-indexes-transact-sql.md). |
| `partition_number` | **int** | 1-based partition number within the index or heap. For more information, see [sys.partitions](../system-catalog-views/sys-partitions-transact-sql.md). |
| `hobt_id` | **bigint** | ID of the data heap or B-tree rowset that tracks internal data for a columnstore index.<br /><br />`NULL` - this isn't an internal columnstore rowset.<br /><br />For more information, see [sys.internal_partitions](../system-catalog-views/sys-internal-partitions-transact-sql.md). |
| `leaf_insert_count` | **bigint** | Cumulative count of leaf level inserts. For more information about index levels, see [Index architecture and design guide](../sql-server-index-design-guide.md). |
| `leaf_delete_count` | **bigint** | Cumulative count of leaf level deletes. `leaf_delete_count` is only incremented for deleted records that aren't marked as ghost first. For deleted records that are ghosted first, `leaf_ghost_count` is incremented instead. |
| `leaf_update_count` | **bigint** | Cumulative count of leaf level updates. |
| `leaf_ghost_count` | **bigint** | Cumulative count of leaf level rows that are marked as deleted, but not yet removed. This count doesn't include records that are immediately deleted without being marked as ghost. A [cleanup thread](../ghost-row-cleanup-process-guide.md) removes ghost rows at set intervals. This value doesn't include ghost rows that are retained because of an outstanding snapshot transaction. |
| `nonleaf_insert_count` | **bigint** | Cumulative count of inserts above the leaf level. Applies to B-tree indexes only. 0 for heaps or columnstore indexes. |
| `nonleaf_delete_count` | **bigint** | Cumulative count of deletes above the leaf level. Applies to B-tree indexes only. 0 for heaps or columnstore indexes. |
| `nonleaf_update_count` | **bigint** | Cumulative count of updates above the leaf level. Applies to B-tree indexes only. 0 for heaps or columnstore indexes. |
| `leaf_allocation_count` | **bigint** | Cumulative count of leaf level page allocations in the index or heap.<br /><br />For an index, a page allocation corresponds to a page split. |
| `nonleaf_allocation_count` | **bigint** | Cumulative count of page allocations caused by page splits above the leaf level. Applies to B-tree indexes only. 0 for heaps or columnstore indexes. |
| `leaf_page_merge_count` | **bigint** | Cumulative count of page merges at the leaf level. Always 0 for columnstore indexes. |
| `nonleaf_page_merge_count` | **bigint** | Cumulative count of page merges above the leaf level. Applies to B-tree indexes only. 0 for heaps or columnstore indexes. |
| `range_scan_count` | **bigint** | Cumulative count of range and table scans started on the index or heap. |
| `singleton_lookup_count` | **bigint** | Cumulative count of single row retrievals from the index or heap. |
| `forwarded_fetch_count` | **bigint** | Count of rows that were fetched through a forwarding record. Applies to heaps only, 0 for B-tree indexes. |
| `lob_fetch_in_pages` | **bigint** | Cumulative count of large object (LOB) pages retrieved from a `LOB_DATA` allocation unit. These pages contain data that is stored in columns of type **text**, **ntext**, **image**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, and **json**. For more information, see [Data types](../../t-sql/data-types/data-types-transact-sql.md). |
| `lob_fetch_in_bytes` | **bigint** | Cumulative count of LOB data bytes retrieved. |
| `lob_orphan_create_count` | **bigint** | Cumulative count of orphan LOB values created for bulk operations. Applies to heaps and B-tree clustered indexes only, 0 for nonclustered and columnstore indexes. |
| `lob_orphan_insert_count` | **bigint** | Cumulative count of orphan LOB values inserted during bulk operations. Applies to heaps and B-tree clustered indexes only, 0 for nonclustered and columnstore indexes. |
| `row_overflow_fetch_in_pages` | **bigint** | Cumulative count of row-overflow data pages retrieved from a `ROW_OVERFLOW_DATA` allocation unit.<br /><br />These pages contain data stored in columns of type `varchar(n)`, `nvarchar(n)`, `varbinary(n)`, and `sql_variant` for [large rows](../pages-and-extents-architecture-guide.md#large-row-support). |
| `row_overflow_fetch_in_bytes` | **bigint** | Cumulative count of row-overflow data bytes retrieved. |
| `column_value_push_off_row_count` | **bigint** | Cumulative count of column values for LOB data and row-overflow data that is pushed off-row to make an inserted or updated row fit within a page. |
| `column_value_pull_in_row_count` | **bigint** | Cumulative count of column values for LOB data and row-overflow data that is pulled in-row. This occurs when an update operation frees up space in a record and provides an opportunity to pull in one or more off-row values from a `LOB_DATA` or `ROW_OVERFLOW_DATA` allocation units to the `IN_ROW_DATA` allocation unit. |
| `row_lock_count` | **bigint** | Cumulative number of row locks requested. |
| `row_lock_wait_count` | **bigint** | Cumulative number of times the [!INCLUDE [ssDE](../../includes/ssde-md.md)] waited on a row lock. |
| `row_lock_wait_in_ms` | **bigint** | Total number of milliseconds the [!INCLUDE [ssDE](../../includes/ssde-md.md)] waited on a row lock. |
| `page_lock_count` | **bigint** | Cumulative number of page locks requested. |
| `page_lock_wait_count` | **bigint** | Cumulative number of times the [!INCLUDE [ssDE](../../includes/ssde-md.md)] waited on a page lock. |
| `page_lock_wait_in_ms` | **bigint** | Total number of milliseconds the [!INCLUDE [ssDE](../../includes/ssde-md.md)] waited on a page lock. |
| `index_lock_promotion_attempt_count` | **bigint** | Cumulative number of times the [!INCLUDE [ssDE](../../includes/ssde-md.md)] tried to escalate locks. |
| `index_lock_promotion_count` | **bigint** | Cumulative number of times the [!INCLUDE [ssDE](../../includes/ssde-md.md)] escalated locks. |
| `page_latch_wait_count` | **bigint** | Cumulative number of times the [!INCLUDE [ssDE](../../includes/ssde-md.md)] waited to acquire a latch. |
| `page_latch_wait_in_ms` | **bigint** | Cumulative number of milliseconds the [!INCLUDE [ssDE](../../includes/ssde-md.md)] waited to acquire a latch. |
| `page_io_latch_wait_count` | **bigint** | Cumulative number of times the [!INCLUDE [ssDE](../../includes/ssde-md.md)] waited on a page I/O latch. |
| `page_io_latch_wait_in_ms` | **bigint** | Cumulative number of milliseconds the [!INCLUDE [ssDE](../../includes/ssde-md.md)] waited on a page I/O latch. |
| `tree_page_latch_wait_count` | **bigint** | Subset of `page_latch_wait_count` that includes only the upper level B-tree pages. Always 0 for a heap or columnstore index. |
| `tree_page_latch_wait_in_ms` | **bigint** | Subset of `page_latch_wait_in_ms` that includes only the upper level B-tree pages. Always 0 for a heap or columnstore index. |
| `tree_page_io_latch_wait_count` | **bigint** | Subset of `page_io_latch_wait_count` that includes only the upper level B-tree pages. Always 0 for a heap or columnstore index. |
| `tree_page_io_latch_wait_in_ms` | **bigint** | Subset of `page_io_latch_wait_in_ms` that includes only the upper level B-tree pages. Always 0 for a heap or columnstore index. |
| `page_compression_attempt_count` | **bigint** | Number of pages that were evaluated for `PAGE` level compression for a specific partition of a table, index, or indexed view. Includes pages that weren't compressed because significant savings couldn't be achieved. Always 0 for columnstore indexes. |
| `page_compression_success_count` | **bigint** | Number of data pages that were compressed by using `PAGE` compression for specific partitions of a table, index, or indexed view. Always 0 for columnstore indexes. |
| `version_generated_inrow` | **bigint** | Cumulative count of in-row versions with payload generated in the heap or B-tree for an update, merge, or insert-over-ghost operation. An in-row version stores the old row image (or a diff) directly in the row, avoiding a trip to the version store. This count is a superset that includes versions counted by `insert_over_ghost_version_inrow`. For more information about in-row and off-row versions, see [Space used by the persistent version store (PVS)](../sql-server-transaction-locking-and-row-versioning-guide.md#space-used-by-the-persistent-version-store-pvs). |
| `version_generated_offrow` | **bigint** | Cumulative count of versions pushed to the off-row storage for a heap, B-tree, or LOB delete, update, merge, or insert-over-ghost operation. An off-row version is generated when the old row image can't be kept in-row. This count is a superset that includes versions counted by `ghost_version_offrow` and `insert_over_ghost_version_offrow`. |
| `ghost_version_inrow` | **bigint** | Cumulative count of times a delete or update (performed as a delete followed by an insert) marked the existing row as a ghost with in-row versioning information. The in-row version stores only a transaction timestamp and a zero-length payload, so that undoing the delete only requires unghosting the row. |
| `ghost_version_offrow` | **bigint** | Cumulative count of times a delete or update (performed as a delete followed by an insert) pushed the existing row or LOB column data to the off-row storage, leaving a stub in the row for versioning information. This counter is incremented together with `version_generated_offrow` during ghost operations. |
| `insert_over_ghost_version_inrow` | **bigint** | Cumulative count of in-row versions with payload generated for a B-tree insert-over-ghost operation. An insert-over-ghost occurs when a new row is inserted into the slot of a previously ghosted record, either from an explicit delete followed by an insert, or from an update or merge implemented as a delete followed by an insert. This counter is a subset of `version_generated_inrow`. |
| `insert_over_ghost_version_offrow` | **bigint** | Cumulative count of times the existing ghost row was pushed to the off-row storage during a B-tree insert-over-ghost operation, leaving a stub in the newly inserted row for versioning information. This counter is a subset of `version_generated_offrow`. |
| `compaction_attempt_count` | **bigint** | Cumulative count of index auto-compaction attempts. For more information, see [Automatic index compaction (preview)](../indexes/automatic-index-compaction.md). |
| `compaction_complete_count` | **bigint** | Cumulative count of completed index auto-compactions. |
| `compaction_skip_count` | **bigint** | Cumulative count of skipped index auto-compactions. For more information about skip reasons, see [Use an extended event to monitor compaction statistics](../indexes/automatic-index-compaction.md#use-an-extended-event-to-monitor-compaction-statistics). |
| `compaction_ineligible_count` | **bigint** | Cumulative count of compaction attempts skipped because a page was ineligible for auto-compaction. |
| `compaction_failure_count` | **bigint** | Cumulative count of failed compaction attempts. |
| `compaction_row_move_count` | **bigint** | Cumulative number of rows moved from one page to another as part of auto-compaction. |
| `compaction_page_deallocation_count` | **bigint** | Cumulative number of pages that were deallocated after moving all rows to another page. |

[!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]

## Remarks

This function doesn't return information about indexes on memory-optimized tables. For information about indexes on memory-optimized tables, see [sys.dm_db_xtp_index_stats](sys-dm-db-xtp-index-stats-transact-sql.md).

This function doesn't accept correlated parameters from `CROSS APPLY` and `OUTER APPLY`.

You can use `sys.dm_db_index_operational_stats` to track data read and write operation statistics, and lock, page latch, and page I/O latch statistics for a table, index, or partition. You can identify the tables, indexes, and partitions that are encountering significant activity or contention.

The statistics are provided at the partition level and are additive. This means that you can obtain index level or table level statistics by writing an aggregation query in T-SQL. For more information, see the [Index scans and seeks for all tables](#index-scans-and-seeks-for-all-tables) example.

To analyze read and write operation statistics for a table, index, or partition, use these columns:

- `leaf_insert_count`
- `leaf_delete_count`
- `leaf_update_count`
- `leaf_ghost_count`
- `range_scan_count`
- `singleton_lookup_count`

To identify latch contention, use these columns:

- `page_latch_wait_count`
- `page_latch_wait_in_ms`

To identify lock contention, use these columns:

- `row_lock_count`
- `page_lock_count`
- `row_lock_wait_in_ms`
- `page_lock_wait_in_ms`

To analyze the physical I/O statistics, use these columns:

- `page_io_latch_wait_count`
- `page_io_latch_wait_in_ms`

## Column remarks

The values in the columns `lob_fetch_in_pages` and `lob_fetch_in_bytes` can be greater than zero for nonclustered indexes that contain one or more LOB columns as included columns. For more information, see [Create indexes with included columns](../indexes/create-indexes-with-included-columns.md). Similarly, the values in the columns `row_overflow_fetch_in_pages` and `row_overflow_fetch_in_bytes` can be greater than 0 for nonclustered indexes if the index contains [large rows](../pages-and-extents-architecture-guide.md#large-row-support).

## How the counters in the metadata cache are reset

The data returned by `sys.dm_db_index_operational_stats` exists only as long as a metadata cache object that represents the heap or B-tree is available. This data isn't persistent. This means you can't use these counters to conclusively determine whether an index was used or not, or when the index was last used. Instead, use [sys.dm_db_index_usage_stats](sys-dm-db-index-usage-stats-transact-sql.md).

The values for each numeric column are set to zero whenever the metadata for the heap or B-tree is brought into the metadata cache. Statistics are accumulated until the cache object is removed from the metadata cache. An active heap or B-tree commonly has its metadata in the cache, and the cumulative counts reflects the activity since the [!INCLUDE [ssDE](../../includes/ssde-md.md)] instance was last started. The metadata for a less active heap or B-tree might move in and out of the cache as it's used, particularly if the [!INCLUDE [ssDE](../../includes/ssde-md.md)] instance is under memory pressure. As a result, index operational statistics might sometimes not be reflected in `sys.dm_db_index_operational_stats`. This isn't common.

Statistics are removed from the cache and are no longer reported by this function if a table or index is dropped, or if a partition is truncated. Other DDL operations against the index might cause the value of the statistics to be reset to zero.

<a id="using-system-functions-to-specify-parameter-values"></a>

## Use system functions to specify parameter values

You can use the [!INCLUDE [tsql](../../includes/tsql-md.md)] functions [DB_ID](../../t-sql/functions/db-id-transact-sql.md) and [OBJECT_ID](../../t-sql/functions/object-id-transact-sql.md) to specify a value for the *database_id* and *object_id* parameters. However, passing values that aren't valid to these functions might cause unintended results. Always make sure that a valid ID is returned when you use `DB_ID` or `OBJECT_ID`. For more information, see [Return information for a specified table](#return-information-for-a-specified-table).

## Permissions

Requires the following permissions:

- `CONTROL` permission on the specified object within the database

- `VIEW DATABASE STATE` or `VIEW DATABASE PERFORMANCE STATE` permission to return information about all objects within the specified database, when a value for `@object_id` isn't specified.

- `VIEW SERVER STATE` or `VIEW SERVER PERFORMANCE STATE` permission to return information about all databases, when a value for `@database_id` isn't specified.

Granting `VIEW DATABASE STATE` or `VIEW SERVER PERFORMANCE STATE` allows all objects in the database to be returned, regardless of any `CONTROL` permissions denied on specific objects.

Denying `VIEW DATABASE STATE` or `VIEW SERVER PERFORMANCE STATE` disallows all objects in the database to be returned, regardless of any `CONTROL` permissions granted on specific objects.

For more information, see [System dynamic management views and functions](system-dynamic-management-objects.md).

## Examples

### Return information for a specified table

The following example returns information for all indexes and partitions of the `Person.Address` table in the [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.

> [!IMPORTANT]  
> When you use the [!INCLUDE [tsql](../../includes/tsql-md.md)] functions `DB_ID` and `OBJECT_ID` to return a parameter value, always ensure that a valid ID is returned. If the database or object name can't be found, such as when they don't exist or are spelled incorrectly, both functions return `NULL`. The `sys.dm_db_index_operational_stats` function interprets `NULL` as a wildcard value that specifies all databases or all objects. Because this can be an unintentional operation, the examples in this section demonstrate the safe way to determine database and object IDs.

```sql
DECLARE @db_id AS INT = DB_ID(N'AdventureWorks2025');
DECLARE @object_id AS INT = OBJECT_ID(N'AdventureWorks2025.Person.Address');

SELECT *
FROM sys.dm_db_index_operational_stats(@db_id, @object_id, NULL, NULL)
WHERE @db_id IS NOT NULL
      AND @object_id IS NOT NULL;
```

### Return information for all tables and indexes

The following example returns information for all tables and indexes on a [!INCLUDE [ssDE](../../includes/ssde-md.md)] instance.

```sql
SELECT *
FROM sys.dm_db_index_operational_stats(NULL, NULL, NULL, NULL);
```

### Index scans and seeks for all tables

The following example aggregates partition level data to return index seek and scan statistics for all tables in the current database.

```sql
SELECT OBJECT_SCHEMA_NAME(object_id) AS schema_name,
       OBJECT_NAME(object_id) AS object_name,
       COUNT(DISTINCT(index_id)) AS index_count,
       COUNT(DISTINCT(partition_number)) AS partition_count,
       SUM(range_scan_count) AS index_scan_count,
       SUM(singleton_lookup_count) AS index_seek_count
FROM sys.dm_db_index_operational_stats(DB_ID(), DEFAULT, DEFAULT, DEFAULT)
GROUP BY OBJECT_SCHEMA_NAME(object_id),
         OBJECT_NAME(object_id)
ORDER BY schema_name, object_name;
```

## Related content

- [System dynamic management views and functions](system-dynamic-management-objects.md)
- [Index Related Dynamic Management Views and Functions (Transact-SQL)](index-related-dynamic-management-views-and-functions-transact-sql.md)
- [Monitor and Tune for Performance](../performance/monitor-and-tune-for-performance.md)
- [sys.dm_db_index_physical_stats (Transact-SQL)](sys-dm-db-index-physical-stats-transact-sql.md)
- [sys.dm_db_index_usage_stats (Transact-SQL)](sys-dm-db-index-usage-stats-transact-sql.md)
- [sys.dm_os_latch_stats (Transact-SQL)](sys-dm-os-latch-stats-transact-sql.md)
- [sys.dm_db_partition_stats (Transact-SQL)](sys-dm-db-partition-stats-transact-sql.md)
- [sys.allocation_units (Transact-SQL)](../system-catalog-views/sys-allocation-units-transact-sql.md)
- [sys.partitions (Transact-SQL)](../system-catalog-views/sys-partitions-transact-sql.md)
- [sys.indexes (Transact-SQL)](../system-catalog-views/sys-indexes-transact-sql.md)
