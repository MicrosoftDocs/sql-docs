---
title: "sys.internal_partitions (Transact-SQL)"
description: sys.internal_partitions returns one row for each rowset that tracks internal data for columnstore indexes on disk-based tables.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman, randolphwest
ms.date: 06/20/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
ms.custom:
  - ignite-2025
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# sys.internal_partitions (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricsqldb.md)]

Returns one row for each rowset that tracks internal data for columnstore indexes on disk-based tables. These rowsets are internal to columnstore indexes and track deleted rows, rowgroup mappings, and delta store rowgroups. They track data for each table partition. Every table has at least one partition. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] re-creates the rowsets each time it rebuilds the columnstore index.

| Column name | Data type | Description |
| --- | --- | --- |
| `partition_id` | **bigint** | Partition ID for this partition. This is unique within a database. |
| `object_id` | **int** | Object ID for the table that contains the partition. |
| `index_id` | **int** | Index ID for the columnstore index defined on the table.<br /><br />1 = clustered columnstore index<br />2 = nonclustered columnstore index |
| `partition_number` | **int** | The partition number.<br /><br />1 = first partition of a partitioned table, or the single partition of a nonpartitioned table.<br /><br />2 = second partition, and so on. |
| `internal_object_type` | **tinyint** | Rowset objects that track internal data for the columnstore index.<br /><br />2 = `COLUMN_STORE_DELETE_BITMAP`<br />3 = `COLUMN_STORE_DELTA_STORE`<br />4 = `COLUMN_STORE_DELETE_BUFFER`<br />5 = `COLUMN_STORE_MAPPING_INDEX` |
| `internal_object_type_desc` | **nvarchar(60)** | `COLUMN_STORE_DELETE_BITMAP` - This bitmap index tracks rows that are marked as deleted from the columnstore. The bitmap is for every rowgroup since partitions can have rows in multiple rowgroups. These rows are still physically present and take up space in the columnstore.<br /><br />`COLUMN_STORE_DELTA_STORE` - Stores groups of rows, called rowgroups, that haven't been compressed into columnar storage. Each table partition can have zero or more deltastore rowgroups.<br /><br />`COLUMN_STORE_DELETE_BUFFER` - For maintaining deletes to updateable nonclustered columnstore indexes. When a query deletes a row from the underlying rowstore table, the delete buffer tracks the deletion from the columnstore. When the number of deleted rows exceeds 1,048,576, they're merged back into the delete bitmap by the Tuple Mover background thread or by an `ALTER INDEX ... REORGANIZE` operation. At any given point in time, the union of the delete bitmap and the delete buffer represents all deleted rows.<br /><br />`COLUMN_STORE_MAPPING_INDEX` - Used only when the clustered columnstore index has a secondary nonclustered index. This maps nonclustered index keys to the rowgroup and row ID in the columnstore. It only stores keys for rows that move to a different rowgroup. This occurs when a delta rowgroup is compressed into the columnstore, and when a merge operation merges rows from two different rowgroups. |
| `row_group_id` | **int** | ID for the deltastore rowgroup. Each table partition can have zero or more deltastore rowgroups. |
| `hobt_id` | **bigint** | ID of the internal rowset object (HoBT). Can be used in joins with other system views and functions such as [sys.dm_db_index_physical_stats()](../system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md) to get more information about the physical characteristics of the internal rowset. |
| `rows` | **bigint** | Approximate number of rows in this partition. |
| `data_compression` | **tinyint** | The compression type for each partition:<br /><br />0 = `NONE`<br />1 = `ROW`<br />2 = `PAGE` |
| `data_compression_desc` | **nvarchar(60)** | The compression type for each partition. Possible values for rowstore tables are `NONE`, `ROW`, and `PAGE`. Possible values for columnstore tables are `COLUMNSTORE` and `COLUMNSTORE_ARCHIVE`. |

## Permissions

Requires membership in the `public` role. For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

## Remarks

The [!INCLUDE [ssDE](../../includes/ssde-md.md)] re-creates new columnstore internal indexes each time it creates or rebuilds a columnstore index.

## Examples

### A. View all of the internal rowsets for a table

This example returns all of the internal columnstore rowsets for a table. You can also use the `hobt_id` column to join with other system views and functions and find more information about the specific rowset.

```sql
SELECT i.object_id,
       i.index_id,
       i.name,
       p.hobt_id,
       p.internal_object_type_id,
       p.internal_object_type_desc
FROM sys.internal_partitions AS p
     INNER JOIN sys.indexes AS i
         ON i.object_id = p.object_id
WHERE p.object_id = OBJECT_ID('<table name>');
```

## Related content

- [Object catalog views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Querying the SQL Server system catalog FAQ](querying-the-sql-server-system-catalog-faq.yml)
