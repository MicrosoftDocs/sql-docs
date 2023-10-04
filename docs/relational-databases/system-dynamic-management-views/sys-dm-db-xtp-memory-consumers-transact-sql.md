---
title: "sys.dm_db_xtp_memory_consumers (Transact-SQL)"
description: dm_db_xtp_memory_consumers returns data on database-level memory consumers that the database engine uses for In-Memory OLTP.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/22/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_xtp_memory_consumers"
  - "dm_db_xtp_memory_consumers"
  - "dm_db_xtp_memory_consumers_TSQL"
  - "sys.dm_db_xtp_memory_consumers_stats_TSQL"
helpviewer_keywords:
  - "sys.dm_db_xtp_memory_consumers dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sys.dm_db_xtp_memory_consumers (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Reports the database-level memory consumers in the [!INCLUDE [inmemory](../../includes/inmemory-md.md)] database engine. The view returns a row for each memory consumer that the database engine uses. Use this DMV to see how the memory is distributed across different internal objects.

For more information, see [[!INCLUDE [inmemory](../../includes/inmemory-md.md)] (In-Memory Optimization)](../in-memory-oltp/overview-and-usage-scenarios.md).

| Column name | Data type | Description |
| --- | --- | --- |
| `memory_consumer_id` | **bigint** | ID (internal) of the memory consumer. |
| `memory_consumer_type` | **int** | The type of memory consumer:<br /><br />0 = Aggregation. (Aggregates memory usage of two or more consumers. It shouldn't be displayed.)<br /><br /> 2 = VARHEAP (Tracks memory consumption for a variable-length heap.)<br /><br />3 = HASH (Tracks memory consumption for an index.)<br /><br />5 = DB page pool (Tracks memory consumption for a database page pool used for runtime operations. For example, table variables and some serializable scans. There is only one memory consumer of this type per database.) |
| `memory_consumer_type_desc` | **nvarchar(64)** | Type of memory consumer: VARHEAP, HASH, or PGPOOL.<br /><br />0 - (It shouldn't be displayed.)<br /><br />2 - VARHEAP<br /><br />3 - HASH<br /><br />5 - PGPOOL |
| `memory_consumer_desc` | **nvarchar(64)** | Description of the memory consumer instance:<br /><br />VARHEAP:<br />- Database heap. Used to allocate user data for a database (rows).<br />- Database System heap. Used to allocate database data that will be included in memory dumps, and don't include user data.<br />- Range index heap. Private heap used by range index to allocate BW pages.<br /><br />HASH: No description since the object_id indicates the table and the index_id the hash index itself.<br /><br />PGPOOL: For the database, there is only one 64-KB page pool. |
| `object_id` | **bigint** | The object ID to which the allocated memory is attributed. A negative value for system objects. |
| `xtp_object_id` | **bigint** | The [!INCLUDE [inmemory](../../includes/inmemory-md.md)] object ID that corresponds to the memory-optimized table. |
| `index_id` | **int** | The index ID of the consumer (if any). NULL for base tables. |
| `allocated_bytes` | **bigint** | Number of bytes reserved for this consumer. |
| `used_bytes` | **bigint** | Bytes used by this consumer. Applies only to VARHEAP. |
| `allocation_count` | **int** | Number of allocations. |
| `partition_count` | **int** | Internal use only. |
| `sizeclass_count` | **int** | Internal use only. |
| `min_sizeclass` | **int** | Internal use only. |
| `max_sizeclass` | **int** | Internal use only. |
| `memory_consumer_address` | **varbinary** | Internal address of the consumer. For internal use only. |

## Remarks

In the output, the allocators at database levels refer to user tables, indexes, and system tables. VARHEAP with `object_id` = `NULL` refers to memory allocated to tables with variable length columns.

When a memory-optimized table has a columnstore index, the system uses some internal tables, which consume some memory, to track data for the columnstore index. For details about these internal tables and sample queries showing their memory consumption, see [sys.memory_optimized_tables_internal_attributes (Transact-SQL)](../system-catalog-views/sys-memory-optimized-tables-internal-attributes-transact-sql.md).

## Permissions

All rows are returned if you have VIEW DATABASE STATE permission on the current database. Otherwise, an empty rowset is returned.

If you don't have VIEW DATABASE permission, all columns are returned for rows in tables that you have SELECT permission on.

On [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions, system tables are returned only for users with VIEW DATABASE STATE permission.

For [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, you require VIEW DATABASE PERFORMANCE STATE permission on the database.

## Examples

#### Query memory consumers in the current database

```sql
SELECT CONVERT(CHAR(10), OBJECT_NAME(object_id)) AS Name,
    CONVERT(CHAR(10), memory_consumer_type_desc) AS memory_consumer_type_desc,
    object_id,
    index_id,
    allocated_bytes,
    used_bytes
FROM sys.dm_db_xtp_memory_consumers;
```

Here is the output with a subset of columns. The allocators at database levels refer to user tables, indexes, and system tables. The VARHEAP with `object_id` = `NULL` (last row) refers to memory allocated to data rows of the tables (in the example here, it is `t1`). The allocated bytes, when converted to MB, is 1340 MB.

```output
Name       memory_consumer_type_desc object_id   index_id    allocated_bytes      used_bytes
---------- ------------------------- ----------- ----------- -------------------- --------------------
t3         HASH                      629577281   2           8388608              8388608
t2         HASH                      597577167   2           8388608              8388608
t1         HASH                      565577053   2           1048576              1048576
NULL       HASH                      -6          1           2048                 2048
NULL       VARHEAP                   -6          NULL        0                    0
NULL       HASH                      -5          3           8192                 8192
NULL       HASH                      -5          2           8192                 8192
NULL       HASH                      -5          1           8192                 8192
NULL       HASH                      -4          1           2048                 2048
NULL       VARHEAP                   -4          NULL        0                    0
NULL       HASH                      -3          1           2048                 2048
NULL       HASH                      -2          2           8192                 8192
NULL       HASH                      -2          1           8192                 8192
NULL       VARHEAP                   -2          NULL        196608               26496
NULL       HASH                      0           1           2048                 2048
NULL       PGPOOL                    0           NULL        0                    0
NULL       VARHEAP                   NULL        NULL        1405943808           1231220560

(17 row(s) affected)
```

The total memory allocated and used from this DMV is same as the object level in [sys.dm_db_xtp_table_memory_stats (Transact-SQL)](sys-dm-db-xtp-table-memory-stats-transact-sql.md).

```sql
SELECT sum(allocated_bytes) / (1024 * 1024) AS total_allocated_MB,
    sum(used_bytes) / (1024 * 1024) AS total_used_MB
FROM sys.dm_db_xtp_memory_consumers;
```

```output
total_allocated_MB   total_used_MB
-------------------- --------------------
1358                 1191
```

## Next steps

- [Introduction to Memory-Optimized Tables](../in-memory-oltp/introduction-to-memory-optimized-tables.md)
- [Memory-Optimized Table Dynamic Management Views](memory-optimized-table-dynamic-management-views-transact-sql.md)
- [[!INCLUDE [inmemory](../../includes/inmemory-md.md)] Overview and Usage Scenarios](../in-memory-oltp/overview-and-usage-scenarios.md)
- [Optimize performance by using in-memory technologies in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/in-memory-oltp-overview)
