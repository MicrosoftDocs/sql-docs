---
title: "sys.dm_db_xtp_memory_consumers (Transact-SQL)"
description: dm_db_xtp_memory_consumers returns data on database-level memory consumers that the database engine uses for In-Memory OLTP.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/05/2023
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

For more information, see [In-Memory OLTP overview and usage scenarios](../in-memory-oltp/overview-and-usage-scenarios.md).

> [!NOTE]  
> The output of this system dynamic management view may be different, depending on the version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] installed.

| Column name | Data type | Description |
| --- | --- | --- |
| `memory_consumer_id` | **bigint** | ID (internal) of the memory consumer. |
| `memory_consumer_type` | **int** | The type of memory consumer:<br /><br />0 = Aggregation. (Aggregates memory usage of two or more consumers. It shouldn't be displayed.)<br /><br />2 = `VARHEAP` (Tracks memory consumption for a variable-length heap.)<br /><br />3 = `HASH` (Tracks memory consumption for an index.)<br /><br />4 = `PGPOOL` (DB page pool: Tracks memory consumption for a database page pool used for runtime operations. For example, table variables and some serializable scans. There is only one memory consumer of this type per database.) |
| `memory_consumer_type_desc` | **nvarchar(64)** | Type of memory consumer: `VARHEAP`, `HASH`, or `PGPOOL`.<br /><br />0 - (Shouldn't be displayed)<br />2 - `VARHEAP`<br />3 - `HASH`<br />4 - `PGPOOL` |
| `memory_consumer_desc` | **nvarchar(64)** | Description of the memory consumer instance. For more information, review the table that follows. |
| `object_id` | **bigint** | The object ID to which the allocated memory is attributed. A negative value for system objects. |
| `xtp_object_id` | **bigint** | The [!INCLUDE [inmemory](../../includes/inmemory-md.md)] object ID that corresponds to the memory-optimized table. |
| `index_id` | **int** | The index ID of the consumer (if any). NULL for base tables. |
| `allocated_bytes` | **bigint** | Number of bytes reserved for this consumer. |
| `used_bytes` | **bigint** | Bytes used by this consumer. Applies only to `VARHEAP`. |
| `allocation_count` | **int** | Number of allocations. |
| `partition_count` | **int** | Internal use only. |
| `sizeclass_count` | **int** | Internal use only. |
| `min_sizeclass` | **int** | Internal use only. |
| `max_sizeclass` | **int** | Internal use only. |
| `memory_consumer_address` | **varbinary** | Internal address of the consumer. For internal use only. |

The following table describes the memory consumers specified in the `memory_consumer_type` column:

| Memory consumer | Description | Type |
| --- | --- | --- |
| `256K page pool` | Memory pool used during checkpoint activity. | `PGPOOL` |
| `4K page pool` | Memory pool used during checkpoint activity. | `PGPOOL` |
| `Checkpoint table` | Internal use only. | `VARHEAP` |
| `Ckpt file table` | Internal use only. | `VARHEAP` |
| `Ckpt file watermark table` | Internal use only. | `VARHEAP` |
| `Database internal heap` | Used to allocate database data that are included in memory dumps, and don't include user data. | `VARHEAP` |
| `Database user heap` | Used to allocate user data for a database (rows). | `VARHEAP` |
| `Encryption table` | Internal use only. | `VARHEAP` |
| `Hash index` | Tracks memory consumption for an index. The `object_id` indicates the table and the `index_id` of the hash index itself. | `HASH` |
| `Large Rows File table` | Internal use only. | `VARHEAP` |
| `LOB Page Allocator` | Heap memory used by large rows. | `VARHEAP` |
| `Logical range index partition table` | Internal use only. | `VARHEAP` |
| `Logical root fragment table` | Internal use only. | `VARHEAP` |
| `Logical Root table` | Internal use only. | `VARHEAP` |
| `Logical Sequence Object table` | Internal use only. | `VARHEAP` |
| `Physical range index partition table` | Internal use only. | `VARHEAP` |
| `Physical root fragment table` | Internal use only. | `VARHEAP` |
| `Physical Root table` | Internal use only. | `VARHEAP` |
| `Physical Sequence object table` | Internal use only. | `VARHEAP` |
| `Range index heap` | Private heap used by range index to allocate Bw-tree pages. | `VARHEAP` |
| `Storage internal heap` | Internal use only. | `VARHEAP` |
| `Storage user heap` | Internal use only. | `VARHEAP` |
| `Table heap` | Heap memory used by In-Memory tables. | `VARHEAP` |
| `Tail cache 256K page pool` | Internal use only. | `PGPOOL` |
| `Tx Segment table` | Internal use only. | `VARHEAP` |

## Remarks

In the output, the allocators at database levels refer to user tables, indexes, and system tables. `VARHEAP` with `object_id = NULL` refers to memory allocated to tables with variable length columns.

When a memory-optimized table has a columnstore index, the system uses some internal tables, which consume some memory, to track data for the columnstore index. For details about these internal tables and sample queries showing their memory consumption, see [sys.memory_optimized_tables_internal_attributes (Transact-SQL)](../system-catalog-views/sys-memory-optimized-tables-internal-attributes-transact-sql.md).

## Permissions

All rows are returned if you have VIEW DATABASE STATE permission on the current database. Otherwise, an empty rowset is returned.

If you don't have VIEW DATABASE permission, all columns are returned for rows in tables that you have SELECT permission on.

On [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions, system tables are returned only for users with VIEW DATABASE STATE permission.

For [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, you require VIEW DATABASE PERFORMANCE STATE permission on the database.

## Examples

#### Query memory consumers in the current database

Run the following query against the sample `WideWorldImporters` database, which contains memory-optimized tables:

```sql
SELECT CONVERT(CHAR(10), OBJECT_NAME(object_id)) AS Name,
    memory_consumer_type_desc,
    object_id,
    index_id,
    allocated_bytes,
    used_bytes
FROM sys.dm_db_xtp_memory_consumers;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)] The allocators at the database level refer to user tables, indexes, and system tables. The `VARHEAP` rows with `object_id` = `NULL` refer to memory allocated to data rows of the tables. The sum of the allocated bytes, when converted to MB, is 50 MB.

|Name|memory_consumer_type_desc|object_id|index_id|allocated_bytes|used_bytes|
|---|---|---|---|---|---|
|ColdRoomTe|VARHEAP|1179151246|3|196608|800|
|ColdRoomTe|VARHEAP|1179151246|2|196608|800|
|VehicleTem|VARHEAP|1243151474|2|10551296|1187936|
|ColdRoomTe|VARHEAP|1179151246|NULL|65536|384|
|memory_opt|VARHEAP|1211151360|2|131072|208|
|VehicleTem|VARHEAP|1243151474|NULL|33423360|32802112|
|memory_opt|VARHEAP|1211151360|NULL|0|0|
|VehicleTem|VARHEAP|1243151474|2|131072|160|
|VehicleTem|VARHEAP|1243151474|NULL|0|0|
|VehicleTem|VARHEAP|1243151474|NULL|0|0|
|NULL|VARHEAP|-15|1|131072|176|
|NULL|VARHEAP|-15|NULL|0|0|
|NULL|VARHEAP|-14|2|131072|192|
|NULL|VARHEAP|-14|1|131072|208|
|NULL|VARHEAP|-14|NULL|0|0|
|NULL|HASH|-13|1|2048|2048|
|NULL|VARHEAP|-13|NULL|0|0|
|NULL|HASH|-10|2|32768|32768|
|NULL|HASH|-10|1|32768|32768|
|NULL|VARHEAP|-10|NULL|131072|544|
|NULL|HASH|-11|1|32768|32768|
|NULL|VARHEAP|-11|NULL|196608|240|
|NULL|HASH|-12|1|8192|8192|
|NULL|VARHEAP|-12|NULL|196608|2160|
|NULL|HASH|-9|1|2048|2048|
|NULL|VARHEAP|-9|NULL|131072|1440|
|NULL|VARHEAP|-7|1|327680|976|
|NULL|VARHEAP|-7|NULL|131072|720|
|NULL|HASH|-3|1|2048|2048|
|NULL|VARHEAP|-3|NULL|0|0|
|NULL|HASH|0|2|8192|8192|
|NULL|HASH|0|1|32768|32768|
|NULL|VARHEAP|NULL|NULL|393216|13416|
|NULL|VARHEAP|NULL|NULL|1114112|4800|
|NULL|VARHEAP|NULL|NULL|196608|25184|
|NULL|VARHEAP|-15|1|262144|176|
|NULL|VARHEAP|-15|NULL|0|0|
|NULL|HASH|-7|1|32768|32768|
|NULL|VARHEAP|-7|NULL|65536|600|
|NULL|HASH|-3|1|2048|2048|
|NULL|VARHEAP|-3|NULL|0|0|
|NULL|HASH|0|1|32768|32768|
|NULL|VARHEAP|NULL|NULL|393216|11120|
|NULL|PGPOOL|0|NULL|262144|262144|
|NULL|PGPOOL|0|NULL|2883584|2621440|
|NULL|PGPOOL|0|NULL|49152|40960|
|NULL|VARHEAP|NULL|NULL|1179648|28624|
|NULL|VARHEAP|NULL|NULL|131072|512|

The total memory allocated and used from this DMV is same as the object level in [sys.dm_db_xtp_table_memory_stats](sys-dm-db-xtp-table-memory-stats-transact-sql.md).

```sql
SELECT SUM(allocated_bytes) / (1024 * 1024) AS total_allocated_MB,
    SUM(used_bytes) / (1024 * 1024) AS total_used_MB
FROM sys.dm_db_xtp_memory_consumers;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
total_allocated_MB total_used_MB
------------------ --------------------
50                 35
```

## Related content

- [Introduction to Memory-Optimized Tables](../in-memory-oltp/introduction-to-memory-optimized-tables.md)
- [Memory-Optimized Table Dynamic Management Views (Transact-SQL)](memory-optimized-table-dynamic-management-views-transact-sql.md)
- [In-Memory OLTP overview and usage scenarios](../in-memory-oltp/overview-and-usage-scenarios.md)
- [Optimize performance by using in-memory technologies in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/in-memory-oltp-overview)
