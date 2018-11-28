---
title: "Estimate Memory Requirements for Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "12/02/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 5c5cc1fc-1fdf-4562-9443-272ad9ab5ba8
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Estimate Memory Requirements for Memory-Optimized Tables
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

Memory-optimized tables require that sufficient memory exist to keep all of the rows and indexes in memory. Because memory is a finite resource, it is important that you understand and manage memory usage on your system. The topics in this section cover common memory use and management scenarios.

Whether you are creating a new memory-optimized table or migrating an existing disk-based table to an [!INCLUDE[hek_2](../../includes/hek-2-md.md)] memory-optimized table, it is important to have a reasonable estimate of each table's memory needs so you can provision the server with sufficient memory. This section describes how to estimate the amount of memory that you need to hold data for a memory-optimized table.  
  
If you are contemplating migrating from disk-based tables to memory-optimized tables, before you proceed in this topic, see the topic [Determining if a Table or Stored Procedure Should Be Ported to In-Memory OLTP](../../relational-databases/in-memory-oltp/determining-if-a-table-or-stored-procedure-should-be-ported-to-in-memory-oltp.md) for guidance on which tables are best to migrate. All the topics under [Migrating to In-Memory OLTP](../../relational-databases/in-memory-oltp/migrating-to-in-memory-oltp.md) provide guidance on migrating from disk-based to memory-optimized tables. 
  
## Basic Guidance for Estimating Memory Requirements

Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], there is no limit on the size of memory-optimized tables, though the tables do need to fit in memory.  In [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] the supported data size is 256GB for SCHEMA_AND_DATA tables.

The size of a memory-optimized table corresponds to the size of data plus some overhead for row headers. When migrating a disk-based table to memory-optimized, the size of the memory-optimized table will roughly correspond to the size of the clustered index or heap of the original disk-based table.

Indexes on memory-optimized tables tend to be smaller than nonclustered indexes on disk-based tables. The size of nonclustered indexes is in the order of `[primary key size] * [row count]`. The size of hash indexes is `[bucket count] * 8 bytes`. 

When there is an active workload, additional memory is needed to account for row versioning and various operations. How much memory is needed in practice depends on the workload, but to be safe the recommendation is to start with two times the expected size of memory-optimized tables and indexes, and observe what are the memory requirements in practice. The overhead for row versioning always depends on the characteristics of the workload - especially long-running transactions increase the overhead. For most workloads using larger databases (e.g., >100GB), overhead tends to be limited (25% or less).

  
## Detailed Computation of Memory Requirements 
  
- [Example memory-optimized table](../../relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md#bkmk_ExampleTable)  
  
- [Memory for the table](../../relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md#bkmk_MemoryForTable)  
  
- [Memory for indexes](../../relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md#bkmk_IndexMeemory)  
  
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

  WITH (memory_optimized = on)  
);
GO  
```  

Using this schema we will determine the minimum memory needed for this memory-optimized table.  
  
###  <a name="bkmk_MemoryForTable"></a> Memory for the table  

A memory-optimized table row is comprised of three parts:
  
- **Timestamps**   
    Row header/timestamps = 24 bytes.  
  
- **Index pointers**   
    For each hash index in the table, each row has an 8-byte address pointer to the next row in the index.  Since there are 4 indexes, each row will allocate 32 bytes for index pointers (an 8 byte pointer for each index).  
  
- **Data**   
    The size of the data portion of the row is determined by summing the type size for each data column.  In our table we have five 4-byte integers, three 50-byte character columns, and one 30-byte character column.  Therefore the data portion of each row is 4 + 4 + 4 + 4 + 4 + 50 + 50 + 30 + 50 or 200 bytes.  
  
The following is a size computation for 5,000,000 (5 million) rows in a memory-optimized table. The total memory used by data rows is estimated as follows:  
  
#### Memory for the table's rows  
  
From the above calculations, the size of each row in the memory-optimized table is 24 + 32 + 200, or 256 bytes.  Since we have 5 million rows, the table will consume 5,000,000 * 256 bytes, or 1,280,000,000 bytes - approximately 1.28 GB.  
  
###  <a name="bkmk_IndexMeemory"></a> Memory for indexes  

#### Memory for each hash index  
  
Each hash index is a hash array of 8-byte address pointers.  The size of the array is best determined by the number of unique index values for that index - e.g., the number of unique Col2 values is a good starting point for the array size for the t1c2_index. A hash array that is too big wastes memory.  A hash array that is too small slows performance since there will be too many collisions by index values that hash to the same index.  
  
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
  
If you are migrating a disk-based table you can use the following to determine the number of unique values for the index t1c2_index.  
  
```sql
SELECT COUNT(DISTINCT [Col2])  
  FROM t_hk;
```  
  
If you are creating a new table, you'll need to estimate the array size or gather data from your testing prior to deployment.  
  
For information on how hash indexes work in [!INCLUDE[hek_2](../../includes/hek-2-md.md)] memory-optimized tables, see [Hash Indexes](https://msdn.microsoft.com/library/f4bdc9c1-7922-4fac-8183-d11ec58fec4e).  
  
#### Setting the hash index array size  
  
The hash array size is set by `(bucket_count= value)` where `value` is an integer value greater than zero. If `value` is not a power of 2, the actual bucket_count is rounded up to the next closest power of 2.  In our example table, (bucket_count = 5000000), since 5,000,000 is not a power of 2, the actual bucket count rounds up to 8,388,608 (2^23).  You must use this number, not 5,000,000 when calculating memory needed by the hash array.  
  
Thus, in our example, the memory needed for each hash array is:  
  
8,388,608 * 8 = 2^23 \* 8 = 2^23 \* 2^3 = 2^26 = 67,108,864 or approximately 64 MB.  
  
Since we have three hash indexes, the memory needed for the hash indexes is 3 * 64MB = 192MB.  
  
#### Memory for non-clustered indexes  
  
Non-clustered indexes are implemented as BTrees with the inner nodes containing the index value and pointers to subsequent nodes.  Leaf nodes contain the index value and a pointer to the table row in memory.  
  
Unlike hash indexes, non-clustered indexes do not have a fixed bucket size. The index grows and shrinks dynamically with the data.  
  
Memory needed by non-clustered indexes can be computed as follows:  
  
- **Memory allocated to non-leaf nodes**   
    For a typical configuration, the memory allocated to non-leaf nodes is a small percentage of the overall memory taken by the index. This is so small it can safely be ignored.  
  
- **Memory for leaf nodes**   
    The leaf nodes have one row for each unique key in the table that points to the data rows with that unique key.  If you have multiple rows with the same key (i.e., you have a non-unique non-clustered index), there is only one row in the index leaf node that points to one of the rows with the other rows linked to each other.  Thus, the total memory required can be approximated by:
  - memoryForNonClusteredIndex = (pointerSize + sum(keyColumnDataTypeSizes)) * rowsWithUniqueKeys  
  
 Non-clustered indexes are best when used for range lookups, as exemplified by the following query:  
  
```sql  
SELECT * FRON t_hk  
   WHERE c2 > 5;  
```  
  
###  <a name="bkmk_MemoryForRowVersions"></a> Memory for row versioning

To avoid locks, In-Memory OLTP uses optimistic concurrency when updating or deleting rows. This means that when a row is updated, an additional version of the row is created. In addition, deletes are logical - the existing row is marked as deleted, but not removed immediately. The system keeps old row versions (including deleted rows) available until all transactions that could possibly use the version have finished execution. 
  
Because there may be a number of additional rows in memory at any time waiting for the garbage collection cycle to release their memory, you must have sufficient memory to accommodate these additional rows.  
  
The number of additional rows can be estimated by computing the peak number of row updates and deletions per second, then multiplying that by the number of seconds the longest transaction takes (minimum of 1).  
  
That value is then multiplied by the row size to get the number of bytes you need for row versioning.  
  
`rowVersions = durationOfLongestTransctoinInSeconds * peakNumberOfRowUpdatesOrDeletesPerSecond`  
  
Memory needs for stale rows is then estimated by multiplying the number of stale rows by the size of a memory-optimized table row (See [Memory for the table](../../relational-databases/in-memory-oltp/estimate-memory-requirements-for-memory-optimized-tables.md#bkmk_MemoryForTable) above).  
  
`memoryForRowVersions = rowVersions * rowSize`  
  
###  <a name="bkmk_TableVariables"></a> Memory for table variables
  
Memory used for a table variable is released only when the table variable goes out of scope. Deleted rows, including rows deleted as part of an update, from a table variable are not subject to garbage collection. No memory is released until the table variable exits scope.  
  
Table variables defined in a large SQL batch, as opposed to a procedure scope, which are used in many transactions, can consume a lot of memory. Because they are not garbage collected, deleted rows in a table variable can consume a lot memory and degrade performance since read operations need to scan past the deleted rows.  
  
###  <a name="bkmk_MemoryForGrowth"></a> Memory for growth

The above calculations estimate your memory needs for the table as it currently exists. In addition to this memory, you need to estimate the growth of the table and provide sufficient memory to accommodate that growth.  For example, if you anticipate 10% growth then you need to multiple the results from above by 1.1 to get the total memory needed for your table.  
  
## See Also

[Migrating to In-Memory OLTP](../../relational-databases/in-memory-oltp/migrating-to-in-memory-oltp.md)  

