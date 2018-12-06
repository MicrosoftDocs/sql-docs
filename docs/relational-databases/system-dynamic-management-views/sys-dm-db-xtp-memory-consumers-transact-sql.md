---
title: "sys.dm_db_xtp_memory_consumers (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_db_xtp_memory_consumers"
  - "dm_db_xtp_memory_consumers"
  - "dm_db_xtp_memory_consumers_TSQL"
  - "sys.dm_db_xtp_memory_consumers_stats_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_db_xtp_memory_consumers dynamic management view"
ms.assetid: f7ab2eaf-e627-464d-91fe-0e170b3f37bc
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_xtp_memory_consumers (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2014-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2014-asdb-xxxx-xxx-md.md)]

  Reports the database-level memory consumers in the [!INCLUDE[hek_2](../../includes/hek-2-md.md)] database engine. The view returns a row for each memory consumer that the database engine uses. Use this DMV to see how the memory is distributed across different internal objects.  
  
 For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](../../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|memory_consumer_id|**bigint**|ID (internal) of the memory consumer.|  
|memory_consumer_type|**int**|The type of memory consumer:<br /><br /> 0=Aggregation. (Aggregates memory usage of two or more consumers. It should not be displayed.)<br /><br /> 2=VARHEAP (Tracks memory consumption for a variable-length heap.)<br /><br /> 3=HASH (Tracks memory consumption for an index.)<br /><br /> 5=DB page pool (Tracks memory consumption for a database page pool used for runtime operations. For example, table variables and some serializable scans. There is only one memory consumer of this type per database.)|  
|memory_consumer_type_desc|**nvarchar(64)**|Type of memory consumer: VARHEAP, HASH, or PGPOOL.<br /><br /> 0 - (It should not be displayed.)<br /><br /> 2 - VARHEAP<br /><br /> 3 - HASH<br /><br /> 5 - PGPOOL|  
|memory_consumer_desc|**nvarchar(64)**|Description of the memory consumer instance:<br /><br /> VARHEAP: <br />Database heap. Used to allocate user data for a database (rows).<br />Database System heap. Used to allocate database data that will be included in memory dumps and do not include user data.<br />Range index heap. Private heap used by range index to allocate BW pages.<br /><br /> HASH: No description since the object_id indicates the table and the index_id the hash index itself.<br /><br /> PGPOOL: For the database there is only one page pool Database 64K page pool.|  
|object_id|**bigint**|The object ID to which the allocated memory is attributed. A negative value for system objects.|  
|xtp_object_id|**bigint**|The object ID for the memory-optimized table.|  
|index_id|**int**|The index ID of the consumer (if any). NULL for base tables.|  
|allocated_bytes|**bigint**|Number of bytes reserved for this consumer.|  
|used_bytes|**bigint**|Bytes used by this consumer. Applies only to varheap.|  
|allocation_count|**int**|Number of allocations.|  
|partition_count|**int**|Internal use only.|  
|sizeclass_count|**int**|Internal use only.|  
|min_sizeclass|**int**|Internal use only.|  
|max_sizeclass|**int**|Internal use only.|  
|memory_consumer_address|**varbinary**|Internal address of the consumer. For internal use only.|  
|xtp_object_id|**bigint**|The in-memory OLTP object ID that corresponds to the memory-optimized table.|  
  
## Remarks  
 In the output, the allocators at database levels refer to user tables, indexes, and system tables. VARHEAP with object_id = NULL refers to memory allocated to tables with variable length columns.  
  
## Permissions  
 All rows are returned if you have VIEW DATABASE STATE permission on the current database. Otherwise, an empty rowset is returned.  
  
 If you do not have VIEW DATABASE permission, all columns will be returned for rows in tables that you have SELECT permission on.  
  
 System tables are returned only for users with VIEW DATABASE STATE permission.  
  
## General Remarks  
 When a memory-optimized table has a columnstore index, the system uses some internal tables, which consume some memory, to track data for the columnstore index. For details about these internal tables and sample queries showing their memory consumption see [sys.memory_optimized_tables_internal_attributes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-memory-optimized-tables-internal-attributes-transact-sql.md).
 
  
## Examples  
  
```  
-- memory consumers (database level)  
SELECT OBJECT_NAME(object_id), *   
FROM sys.dm_db_xtp_memory_consumers;  
```  
  
## User Scenario  
  
```  
-- memory consumers (database level)  
  
select  convert(char(10), object_name(object_id)) as Name,   
convert(char(10),memory_consumer_type_desc ) as memory_consumer_type_desc, object_id,index_id, allocated_bytes,  used_bytes   
from sys.dm_db_xtp_memory_consumers  
```  
  
 Here is the output with a subset of columns. The allocators at database levels refer to user tables, indexes, and system tables. The VARHEAP with object_id = NULL (last row) refers to memory allocated to data rows of the tables (in the example here, it is t1). The allocated bytes, when converted to MB, is 1340MB.  
  
```  
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
  
 The total memory allocated and used from this DMV is same as the object level in [sys.dm_db_xtp_table_memory_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-table-memory-stats-transact-sql.md).  
  
```  
select  sum(allocated_bytes)/(1024*1024) as total_allocated_MB,   
        sum(used_bytes)/(1024*1024) as total_used_MB  
from sys.dm_db_xtp_memory_consumers  
  
total_allocated_MB   total_used_MB  
-------------------- --------------------  
1358                 1191  
```  
  
## See Also  
 [Memory-Optimized Table Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/memory-optimized-table-dynamic-management-views-transact-sql.md)  
  
  
