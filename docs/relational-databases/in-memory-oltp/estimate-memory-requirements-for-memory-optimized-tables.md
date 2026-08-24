---
title: "Memory requirements - memory-optimized tables"
description: Learn about memory use and management scenarios for memory-optimized tables in SQL Server, which require sufficient memory for all the rows and indexes.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/18/2025
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: how-to
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Estimate Memory Requirements for Memory-Optimized Tables

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Memory-optimized tables require that sufficient memory exist to keep all of the rows and indexes in memory. Because memory is a finite resource, it's important that you understand and manage memory usage on your system. The topics in this section cover common memory use and management scenarios.

It's important to have a reasonable estimate of each memory-optimized table's memory needs so you can provision the server with sufficient memory. That applies to both new tables, and tables migrated from disk-based tables. This section describes how to estimate the amount of memory that you need to hold data for a memory-optimized table. 

If you're considering a migration from disk-based tables to memory-optimized tables, see [Determining if a Table or Stored Procedure Should Be Ported to In-Memory OLTP](../../relational-databases/in-memory-oltp/determining-if-a-table-or-stored-procedure-should-be-ported-to-in-memory-oltp.md) for guidance on which tables are best to migrate. All the topics under [Migrating to In-Memory OLTP](./plan-your-adoption-of-in-memory-oltp-features-in-sql-server.md) provide guidance on migrating from disk-based to memory-optimized tables. 
  
## Basic Guidance for Estimating Memory Requirements

In [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later versions, there's no limit on the size of memory-optimized tables, though the tables do need to fit in memory. In [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], the supported data size is 256 GB for SCHEMA_AND_DATA tables.

The size of a memory-optimized table corresponds to the size of data plus some overhead for row headers. The size of the memory-optimized table roughly corresponds to the size of the clustered index or heap of the original disk-based table.

Indexes on memory-optimized tables tend to be smaller than nonclustered indexes on disk-based tables. The size of nonclustered indexes is in the order of `[primary key size] * [row count]`. The size of hash indexes is `[bucket count] * 8 bytes`. 

When there's an active workload, extra memory is needed to account for row versioning and various operations. The required amount of memory depends on the workload, but to be safe the recommendation is to start with two times the expected size of memory-optimized tables and indexes, and observe the actual memory consumption. The overhead for row versioning always depends on the characteristics of the workload - especially long-running transactions increase the overhead. For most workloads using larger databases (for example, greater than 100 GB), overhead tends to be limited (25 percent or less).

For more information about potential memory overhead in the In-Memory OLTP engine, see [Memory fragmentation](#memory-fragmentation).

  
## Detailed Computation of Memory Requirements 
  
- [Example memory-optimized table](../../relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md#bkmk_ExampleTable)  
  
- [Memory for the table](../../relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md#bkmk_MemoryForTable)  
  
- [Memory for indexes](../../relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md#bkmk_IndexMemory)  
  
- [Memory for row versioning](../../relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md#bkmk_MemoryForRowVersions)  
  
- [Memory for table variables](../../relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md#bkmk_TableVariables)  
  
- [Memory for growth](../../relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md#bkmk_MemoryForGrowth)  
  
###  <a name="bkmk_ExampleTable"></a> Example memory-optimized table  

Consider the following memory-optimized table schema:
  
```sql  
CREATE TABLE t_hk
(  
  col1 int NOT NULL  PRIMARY KEY NONCLUSTERED,  

  col2 int NOT NULL  INDEX t1c2_index   
      HASH WITH (bucket_count = 5000000),  

  col3 int NOT NULL  INDEX t1c3_index   
      HASH WITH (bucket_count = 5000000),  

  col4 int NOT NULL  INDEX t1c4_index   
      HASH WITH (bucket_count = 5000000),  

  col5 int NOT NULL  INDEX t1c5_index NONCLUSTERED,  

  col6 char (50) NOT NULL,  
  col7 char (50) NOT NULL,   
  col8 char (30) NOT NULL,   
  col9 char (50) NOT NULL  

)   WITH (memory_optimized = on)  ;
GO  
```  

Using this schema, let's determine the minimum memory needed for this memory-optimized table. 
  
###  <a name="bkmk_MemoryForTable"></a> Memory for the table  

A memory-optimized table row has three parts:
  
- **Timestamps**   
    Row header/timestamps = 24 bytes. 
  
- **Index pointers**   
    For each hash index in the table, each row has an 8-byte address pointer to the next row in the index. Since there are four indexes, each row allocates 32 bytes for index pointers (an 8-byte pointer for each index). 
  
- **Data**   
    The size of the data portion of the row is determined by summing the type size for each data column. In our table we have five 4-byte integers, three 50-byte character columns, and one 30-byte character column. Therefore the data portion of each row is 4 + 4 + 4 + 4 + 4 + 50 + 50 + 30 + 50 or 200 bytes. 
  
The following is a size computation for 5,000,000 (5 million) rows in a memory-optimized table. The total memory used by data rows is estimated as follows:  
  
#### Memory for the table's rows  
  
From the above calculations, the size of each row in the memory-optimized table is 24 + 32 + 200, or 256 bytes. Since we have 5 million rows, the table consumes 5,000,000 * 256 bytes, or 1,280,000,000 bytes - approximately 1.28 GB. 
  
###  <a name="bkmk_IndexMemory"></a> Memory for indexes  

#### Memory for each hash index  
  
Each hash index is a hash array of 8-byte address pointers. The size of the array is best determined by the number of unique index values for that index. In the current example, the number of unique Col2 values is a good starting point for the array size for the t1c2_index. A hash array that is too large wastes memory. A hash array that is too small slows performance since there are too many collisions by index values that hash to the same index entry.
  
Hash indexes achieve very fast equality lookups such as:  
  
```sql  
SELECT * FROM t_hk  
   WHERE Col2 = 3;
```  
  
Nonclustered indexes are faster for range lookups such as:  
  
```sql  
SELECT * FROM t_hk  
   WHERE Col2 >= 3;
```  
  
If you're migrating a disk-based table you can use the following to determine the number of unique values for the index t1c2_index. 
  
```sql
SELECT COUNT(DISTINCT [Col2])  
  FROM t_hk;
```  
  
If you're creating a new table, you need to estimate the array size or gather data from your testing prior to deployment. 
  
For information on how hash indexes work in [!INCLUDE[inmemory](../../includes/inmemory-md.md)] memory-optimized tables, see [Hash Indexes](/previous-versions/sql/sql-server-2016/dn133190(v=sql.130)). 
  
#### Setting the hash index array size  
  
The hash array size is set by `(bucket_count= value)` where `value` is an integer value greater than zero. If `value` isn't a power of 2, the actual bucket_count is rounded up to the next closest power of 2. In our example table, (bucket_count = 5000000), since 5,000,000 isn't a power of 2, the actual bucket count rounds up to 8,388,608 (2^23). You must use this number, not 5,000,000 when calculating memory needed by the hash array. 
  
Thus, in our example, the memory needed for each hash array is:  
  
8,388,608 * 8 = 2^23 \* 8 = 2^23 \* 2^3 = 2^26 = 67,108,864 or approximately 64 MB. 
  
Since we have three hash indexes, the memory needed for the hash indexes is 3 * 64 MB = 192 MB. 
  
#### Memory for nonclustered indexes  
  
Nonclustered indexes are implemented as Bw-trees with the inner nodes containing the index value and pointers to subsequent nodes. Leaf nodes contain the index value and a pointer to the table row in memory.

Unlike hash indexes, nonclustered indexes don't have a fixed bucket size. The index grows and shrinks dynamically with the data. 
  
Memory needed by nonclustered indexes can be computed as follows:  
  
- **Memory allocated to nonleaf nodes**   
    For a typical configuration, the memory allocated to nonleaf nodes is a small percentage of the overall memory taken by the index. This is so small it can safely be ignored. 
  
- **Memory for leaf nodes**   
    The leaf nodes have one row for each unique key in the table that points to the data rows with that unique key. If you have multiple rows with the same key (that is, you have a nonunique nonclustered index), there's only one row in the index leaf node that points to one of the rows with the other rows linked to each other. Thus, the total memory required can be approximated by:
  - memoryForNonClusteredIndex = (pointerSize + sum(keyColumnDataTypeSizes)) * rowsWithUniqueKeys  
  
 Nonclustered indexes are best when used for range lookups, as exemplified by the following query:  
  
```sql  
SELECT * FROM t_hk  
   WHERE c2 > 5;  
```  
  
###  <a name="bkmk_MemoryForRowVersions"></a> Memory for row versioning

To avoid locks, In-Memory OLTP uses optimistic concurrency when updating or deleting rows. This means that when a row is updated, another version of the row is created. In addition, deletes are logical - the existing row is marked as deleted, but not removed immediately. The system keeps old row versions (including deleted rows) available until all transactions that could possibly use the version finish execution. 
  
Because there may be many more rows in memory at any time waiting for the garbage collection cycle to release their memory, you must have sufficient memory to accommodate these other rows. 
  
The number of extra rows can be estimated by computing the peak number of row updates and deletions per second, then multiplying that by the number of seconds the longest transaction takes (minimum of 1). 
  
That value is then multiplied by the row size to get the number of bytes you need for row versioning. 
  
`rowVersions = durationOfLongestTransactionInSeconds * peakNumberOfRowUpdatesOrDeletesPerSecond`  
  
Memory needs for stale rows is then estimated by multiplying the number of stale rows by the size of a memory-optimized table row. For more information, see [Memory for the table](../../relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md#bkmk_MemoryForTable).
  
`memoryForRowVersions = rowVersions * rowSize`  
  
###  <a name="bkmk_TableVariables"></a> Memory for table variables
  
Memory used for a table variable is released only when the table variable goes out of scope. Deleted rows, including rows deleted as part of an update, from a table variable aren't subject to garbage collection. No memory is released until the table variable exits scope. 
  
Table variables defined in a large SQL batch instead of in a stored procedure, and used in many transactions can consume a large amount of memory. Because they aren't garbage collected, deleted rows in a table variable can consume a lot memory and degrade performance since read operations need to scan past the deleted rows. 
  
###  <a name="bkmk_MemoryForGrowth"></a> Memory for growth

The previous calculations estimate your memory needs for the table as it currently exists. In addition to this memory, you need to estimate the growth of the table and provide sufficient memory to accommodate that growth. For example, if you anticipate 10% growth then you need to multiple the previous results by 1.1 to get the total memory needed for your table. 
  

## Memory fragmentation

To avoid the overhead of memory allocation calls and to improve performance, the In-Memory OLTP engine always requests memory from the SQL Server Operating System (SQLOS) using 64-KB blocks, referred to as superblocks.

Each superblock contains memory allocations only within a specific size range, referred to as sizeclass. For example, superblock **A** might have memory allocations in the 1-16 byte sizeclass, while superblock **B** might have memory allocations in the 17-32 byte sizeclass, and so on.

By default, superblocks are also partitioned by logical CPU. That means that for each logical CPU, there is a separate set of superblocks, further broken down by sizeclass. This reduces memory allocation contention among requests executing on different CPUs.

When the In-Memory OLTP engine makes a new memory allocation, it first attempts to find free memory in an existing superblock for the requested sizeclass and for the CPU processing the request. If this attempt is successful, the value in the `used_bytes` column in [sys.dm_xtp_system_memory_consumers](../system-dynamic-management-views/sys-dm-xtp-system-memory-consumers-transact-sql.md) for a specific memory consumer increases by the requested memory size, but the value in the `allocated_bytes` column remains the same.

If there is no free memory in existing superblocks, a new superblock is allocated and value in the `used_bytes` increases by the requested memory size, while the value in the `allocated_bytes` column increases by 64 KB.

Over time, as memory in superblocks is allocated and deallocated, the total amount of memory consumed by the In-Memory OLTP engine might become significantly larger than the amount of used memory. In other words, memory can become fragmented.

[Garbage collection](in-memory-oltp-garbage-collection.md) might reduce the used memory, but it only reduces the allocated memory if one or more superblocks become empty and are deallocated. This applies to both automatic and forced garbage collection using the [sys.sp_xtp_force_gc](..//system-stored-procedures/sys-sp-xtp-force-gc-transact-sql.md) system stored procedure.

If the In-Memory OLTP engine memory fragmentation and allocated memory usage become higher than expected, you can enable [trace flag 9898](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf9898). This changes superblock partitioning scheme from per-CPU to per-NUMA node, reducing the total number of superblocks and the potential for high memory fragmentation.

This optimization is more relevant for large machines with many logical CPUs. The tradeoff of this optimization is a potential increase in memory allocation contention resulting from fewer superblocks, which might reduce the overall workload throughput. Depending on workload patterns, throughput reduction from using per-NUMA memory partitioning may or may not be noticeable.

## Related content

- [Plan your adoption of In-Memory OLTP Features in SQL Server](plan-your-adoption-of-in-memory-oltp-features-in-sql-server.md)
