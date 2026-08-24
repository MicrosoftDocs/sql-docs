---
title: "sys.column_store_row_groups (Transact-SQL)"
description: sys.column_store_row_groups (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dfurman
ms.date: 01/05/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.column_store_row_groups_TSQL"
  - "column_store_row_groups"
  - "sys.column_store_row_groups"
  - "column_store_row_groups_TSQL"
  - "deleted bitmap"
helpviewer_keywords:
  - "sys.column_store_row_groups catalog view"
dev_langs:
  - "TSQL"
---

# sys.column_store_row_groups (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Provides columnstore index information on a per-segment basis.

For clustered columnstore indexes, `sys.column_store_row_groups` has a column for the total number of rows physically stored (including those marked as deleted) and a column for the number of rows marked as deleted. Use `sys.column_store_row_groups` to determine which row groups have a high percentage of deleted rows and should be rebuilt.

| Column name | Data type | Description |
| --- | --- | --- |
| `object_id` | **int** | The ID of the table on which this index is defined. |
| `index_id` | **int** | The ID of the columnstore index. |
| `partition_number` | **int** | The table partition that holds the row group identified by `row_group_id`. Use `partition_number` to join `sys.partitions`. |
| `row_group_id` | **int** | The row group number associated with this row group. This number is unique within the partition.<br /><br />-1 = tail of a memory-optimized table. |
| `delta_store_hobt_id` | **bigint** | The `hobt_id` for an `OPEN` row group in the delta store.<br /><br />NULL if the row group isn't in the delta store.<br /><br />NULL for the tail of a memory-optimized table. |
| `state` | **tinyint** | A number describing the row group state.<br /><br />0 = `INVISIBLE`<br /><br />1 = `OPEN`<br /><br />2 = `CLOSED`<br /><br />3 = `COMPRESSED`<br /><br />4 = `TOMBSTONE` |
| `state_description` | **nvarchar(60)** | Description of the state of the row group:<br /><br />`INVISIBLE` - A hidden compressed segment in the process of being built from data in a delta store. Read actions use the delta store until the invisible compressed segment is completed. Then the new segment is made visible, and the source delta store is removed.<br /><br />`OPEN` - A read/write row group that's accepting new rows. An open row group is still in rowstore format and isn't compressed to columnstore format.<br /><br />`CLOSED` - A row group that is filled, but not yet compressed by the tuple mover process.<br /><br />`COMPRESSED` - A row group that is filled and compressed. |
| `total_rows` | **bigint** | Total rows physically stored in the row group. Deleted rows might still be stored. The maximum number of rows in a row group is 1,048,576. |
| `deleted_rows` | **bigint** | Total rows in the row group that are marked deleted but remain stored. This value is always `0` for delta row groups.<br /><br />For nonclustered columnstore indexes, this value doesn't include deleted rows stored in the delete buffer. For more information, and to find the number of deleted rows in the delete buffer, see [sys.internal_partitions](sys-internal-partitions-transact-sql.md). |
| `size_in_bytes` | **bigint** | Size in bytes of all the data in this row group (not including metadata or shared dictionaries), for both delta store and columnstore rowgroups. |

## Remarks

Returns one row for each columnstore row group for each partition of each table that has a clustered or nonclustered columnstore index.

Use `sys.column_store_row_groups` to find out how many rows are in the row group and the size of the row group.

When the number of deleted rows in a row group grows to a large percentage of the total rows, the table becomes less efficient. Rebuild the columnstore index to reduce the size of the table, reducing the disk I/O required to read the table. To rebuild the columnstore index, use the `REBUILD` clause of the `ALTER INDEX` statement.

The updatable columnstore first inserts new data into an open rowgroup, which is in rowstore format, and is also sometimes referred to as a delta table. Once an open rowgroup is full, its state changes to `CLOSED`. A closed rowgroup is compressed into columnstore format by the tuple mover and the state changes to `COMPRESSED`. The tuple mover is a background process that periodically wakes up and checks whether there are any closed rowgroups that are ready to compress into a columnstore rowgroup. The tuple mover also deallocates any rowgroups in which every row is deleted. Deallocated rowgroups are marked as `TOMBSTONE`. To run tuple mover immediately, use the `REORGANIZE` clause of the `ALTER INDEX` statement.

When a columnstore row group fills, it's compressed, and stops accepting new rows. When you delete rows from a compressed group, they remain but are marked as deleted. Updates to a compressed group are implemented as a delete from the compressed group, and an insert to an open group.

## Permissions

Returns information for a table if the user has `VIEW DEFINITION` permission on the table.

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata visibility configuration](../security/metadata-visibility-configuration.md).

## Examples

The following example joins the `sys.column_store_row_groups` view and other system views to return information about clustered columnstore indexes. The `percent_full` column is an estimate of the efficiency of the row group.

```sql
SELECT i.object_id,
       OBJECT_SCHEMA_NAME(i.object_id) AS schema_name,
       OBJECT_NAME(i.object_id) AS table_name,
       i.name AS index_name,
       i.type_desc AS index_type_desc,
       rg.partition_number,
       rg.row_group_id,
       rg.state_description,
       rg.total_rows,
       rg.deleted_rows,
       rg.size_in_bytes,
       100 * (rg.total_rows - ISNULL(rg.deleted_rows, 0)) / total_rows AS percent_full
FROM sys.indexes AS i
     INNER JOIN sys.column_store_row_groups AS rg
         ON i.object_id = rg.object_id
        AND i.index_id = rg.index_id
WHERE INDEXPROPERTY(i.object_id, i.name, 'IsClustered') = 1
ORDER BY schema_name, table_name, index_name, row_group_id;
```

For more information, see [Check the fragmentation of a columnstore index](../indexes/reorganize-and-rebuild-indexes.md#check-the-fragmentation-of-a-columnstore-index).

## Related content

- [Columnstore indexes: overview](../indexes/columnstore-indexes-overview.md)
- [sys.dm_db_column_store_row_group_physical_stats (Transact-SQL)](../system-dynamic-management-objects/sys-dm-db-column-store-row-group-physical-stats-transact-sql.md)
- [sys.column_store_dictionaries (Transact-SQL)](sys-column-store-dictionaries-transact-sql.md)
- [sys.column_store_segments (Transact-SQL)](sys-column-store-segments-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](querying-the-sql-server-system-catalog-faq.yml)
