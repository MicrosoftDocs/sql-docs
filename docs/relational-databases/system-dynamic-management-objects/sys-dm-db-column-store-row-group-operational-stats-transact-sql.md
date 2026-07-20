---
title: "sys.dm_db_column_store_row_group_operational_stats (Transact-SQL)"
description: Returns current row-level I/O, locking, and access method activity for compressed rowgroups in a columnstore index.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/16/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.dm_db_column_store_row_group_operational_stats_TSQL"
  - "sys.dm_db_column_store_row_group_operational_stats"
  - "dm_db_column_store_row_group_operational_stats_TSQL"
  - "dm_db_column_store_row_group_operational_stats"
helpviewer_keywords:
  - "sys.dm_db_column_store_row_group_operational_stats dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.dm_db_column_store_row_group_operational_stats (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricsqldb.md)]

Returns current row-level I/O, locking, and access method activity for compressed rowgroups in a columnstore index. Use `sys.dm_db_column_store_row_group_operational_stats` to track the length of time a user query must wait to read or write to a compressed rowgroup or partition of a columnstore index, and identify rowgroups that are encountering significant I/O activity or hot spots.

In-memory columnstore indexes don't appear in this DMV.

| Column name | Data type | Nullable | Description |
| --- | --- | --- | --- |
| `object_id` | **int** | No | ID of the table with the columnstore index. |
| `index_id` | **int** | No | ID of the columnstore index. |
| `partition_number` | **int** | No | 1-based partition number within the index or heap. |
| `row_group_id` | **int** | No | ID of the rowgroup in the columnstore index. This is unique within a partition. |
| `index_scan_count` | **bigint** | No | Number of times the columnstore index partition was scanned. This is the same for all rowgroups in the partition. |
| `scan_count` | **bigint** | No | Number of scans through the rowgroup since the last SQL restart. |
| `delete_buffer_scan_count` | **bigint** | No | Number of times the delete buffer was used to determine deleted rows in this rowgroup. This includes accessing the in-memory hashtable and the underlying B-tree. |
| `row_group_lock_count` | **bigint** | No | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `row_group_lock_wait_count` | **bigint** | No | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `row_group_lock_wait_in_ms` | **bigint** | No | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `returned_row_count` | **bigint** | No | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `returned_aggregate_count` | **bigint** | No | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `returned_group_count` | **bigint** | No | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `input_groupby_row_count` | **bigint** | No | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `row_group_elimination_count` | **bigint** | No | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `rowgroup_lock_count` | **bigint** | N/A | Cumulative count of lock requests for this rowgroup since the last SQL Server restart. |
| `rowgroup_lock_wait_count` | **bigint** | N/A | Cumulative number of times the database engine waited on this rowgroup lock since the last SQL Server restart. |
| `rowgroup_lock_wait_in_ms` | **bigint** | N/A | Cumulative number of milliseconds the database engine waited on this rowgroup lock since the last SQL Server restart. |

[!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]

## Permissions

Requires the following permissions:

- `CONTROL` permission on the table specified by `object_id`.

- `VIEW DATABASE STATE` permission to return information about all objects within the database, by using the object wildcard `@object_id = NULL`.

- In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions, requires `VIEW DATABASE STATE` permission to return information about all objects within the database, by using the object wildcard `@object_id = NULL`.

- In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, requires `VIEW DATABASE PERFORMANCE STATE` permission on the database.

Granting `VIEW DATABASE [PERFORMANCE] STATE` allows all objects in the database to be returned, regardless of any `CONTROL` permissions denied on specific objects.

Denying `VIEW DATABASE [PERFORMANCE] STATE` disallows all objects in the database to be returned, regardless of any `CONTROL` permissions granted on specific objects. Also, when the database wildcard `@database_id = NULL` is specified, the database is omitted.

For more information, see [System dynamic management views](system-dynamic-management-objects.md).

## Related content

- [System dynamic management views](system-dynamic-management-objects.md)
- [Index Related Dynamic Management Views and Functions (Transact-SQL)](index-related-dynamic-management-views-and-functions-transact-sql.md)
- [Monitor and Tune for Performance](../performance/monitor-and-tune-for-performance.md)
- [sys.dm_db_index_physical_stats (Transact-SQL)](sys-dm-db-index-physical-stats-transact-sql.md)
- [sys.dm_db_index_usage_stats (Transact-SQL)](sys-dm-db-index-usage-stats-transact-sql.md)
- [sys.dm_os_latch_stats (Transact-SQL)](sys-dm-os-latch-stats-transact-sql.md)
- [sys.dm_db_partition_stats (Transact-SQL)](sys-dm-db-partition-stats-transact-sql.md)
- [sys.allocation_units (Transact-SQL)](../system-catalog-views/sys-allocation-units-transact-sql.md)
- [sys.indexes (Transact-SQL)](../system-catalog-views/sys-indexes-transact-sql.md)
