---
title: "sys.dm_xtp_system_memory_consumers (Transact-SQL)"
description: Reports system level memory consumers for In-Memory OLTP.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/27/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom: event-tier1-build-2022
f1_keywords:
  - "dm_xtp_system_memory_consumers"
  - "sys.dm_xtp_system_memory_consumers_TSQL"
  - "sys.dm_xtp_system_memory_consumers"
  - "dm_xtp_system_memory_consumers_TSQL"
helpviewer_keywords:
  - "sys.dm_xtp_system_memory_consumers dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 9eb0dd82-7920-42e0-9e50-7ce6e7ecee8b
---
# sys.dm_xtp_system_memory_consumers (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Reports system level memory consumers for [!INCLUDE[inmemory](../../includes/inmemory-md.md)]. The memory for these consumers comes either from the default pool (when the allocation is in the context of a user thread) or from the internal pool (if the allocation is in the context of a system thread).

```sql
SELECT * 
FROM sys.dm_xtp_system_memory_consumers;
```

For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](../in-memory-oltp/overview-and-usage-scenarios.md).

|Column name|Type|Description|
|-----------------|----------|-----------------|
|memory_consumer_id|**bigint**|Internal ID for memory consumer.|
|memory_consumer_type|**int**|An integer that represents the type of the memory consumer with one of the following values:<br /><br />0 - Should not be displayed. Aggregates memory usage of two or more consumers.<br /><br />1 - LOOKASIDE: Tracks memory consumption for a system lookaside.<br /><br />2 - VARHEAP: Tracks memory consumption for a variable-length heap.<br /><br />4 - IO page pool: Tracks memory consumption for a system page pool used for IO operations.|
|memory_consumer_type_desc|**nvarchar(16)**|The description of the type of memory consumer:<br /><br />0 - Should not be displayed<br /><br />1 - LOOKASIDE<br /><br />2 - VARHEAP<br /><br />4 - PGPOOL|
|memory_consumer_desc|**nvarchar(64)**|Description of the memory consumer instance:<br /><br />VARHEAP:<br />System heap. General purpose. Currently only used to allocate garbage collection work items.<br />-OR-<br />Lookaside heap. Used by lookasides when the number of items contained in the lookaside list reaches a predetermined cap (usually around 5,000 items).<br /><br />PGPOOL: For IO system pools there are three different sizes: System 4K page pool, System 64 K page pool, and System 256 K page pool.|
|lookaside_id|**bigint**|The ID of the thread-local, lookaside memory provider.|
|pagepool_id|**bigint**|The ID of the thread-local, page pool memory provider.|
|allocated_bytes|**bigint**|Number of bytes reserved for this consumer.|
|used_bytes|**bigint**|Bytes used by this consumer. Applies only to varheap memory consumers.<br /><br />Starting with [!INCLUDE [sqlserver2022](../../includes/sssql22-md.md)], applies to varheap and lookaside memory consumers.|
|allocation_count|**int**|Number of allocations.<br /><br />Starting with [!INCLUDE [sqlserver2022](../../includes/sssql22-md.md)], the number of allocations from the system heap for lookaside memory consumers.|
|partition_count|**int**|Internal use only.|
|sizeclass_count|**int**|Internal use only.|
|min_sizeclass|**int**|Internal use only.|
|max_sizeclass|**int**|Internal use only.|
|memory_consumer_address|**varbinary**|Internal address of the consumer.|

## Permissions

Requires VIEW SERVER STATE permissions on the server.

## User scenario

This example outputs the system memory consumers for the SQL Server instance:

```sql
SELECT memory_consumer_type_desc,
       memory_consumer_desc,
       allocated_bytes / 1024 as allocated_bytes_kb, 
       used_bytes / 1024 as used_bytes_kb,
       allocation_count
FROM sys.dm_xtp_system_memory_consumers;
```

 The output shows all memory consumers at system level. For example, there are consumers for transaction look aside.

```output
memory_consumer_type_desc                memory_consumer_desc                           allocated_bytes_kb   used_bytes_kb        allocation_count
-------------------------------          ---------------------                          -------------------  --------------       ----------------
VARHEAP                                  Lookaside heap                                 0                    0                    0
VARHEAP                                  System heap                                    768                  0                    2
LOOKASIDE                                GC transaction map entry                       64                   64                   910
LOOKASIDE                                Redo transaction map entry                     128                  128                  1260
LOOKASIDE                                Recovery table cache entry                     448                  448                  8192
LOOKASIDE                                Transaction recent rows                        3264                 3264                 4444
LOOKASIDE                                Range cursor                                   0                    0                    0
LOOKASIDE                                Hash cursor                                    3200                 3200                 11070
LOOKASIDE                                Transaction save-point set entry               0                    0                    0
LOOKASIDE                                Transaction partially-inserted rows set        704                  704                  1287
LOOKASIDE                                Transaction constraint set                     576                  576                  1940
LOOKASIDE                                Transaction save-point set                     0                    0                    0
LOOKASIDE                                Transaction write set                          704                  704                  672
LOOKASIDE                                Transaction scan set                           320                  320                  156
LOOKASIDE                                Transaction read set                           704                  704                  343
LOOKASIDE                                Transaction                                    4288                 4288                 1459
PGPOOL                                   System 256K page pool                          5120                 5120                 20
PGPOOL                                   System 64K page pool                           0                    0                    0
PGPOOL                                   System 4K page pool                            24                   24                   6
```

To see the total memory consumed by system allocators:

```sql
SELECT SUM(allocated_bytes) / (1024 * 1024) AS total_allocated_MB,
       SUM(used_bytes) / (1024 * 1024) AS total_used_MB
FROM sys.dm_xtp_system_memory_consumers;
```

Here is the result set.

```output
total_allocated_MB   total_used_MB
-------------------- --------------------
2                    2
```

## See also

- [Memory-Optimized Table Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/memory-optimized-table-dynamic-management-views-transact-sql.md)
