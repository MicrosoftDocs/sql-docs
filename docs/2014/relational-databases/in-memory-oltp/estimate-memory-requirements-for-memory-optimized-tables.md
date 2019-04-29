---
title: "Estimate Memory Requirements for Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 5c5cc1fc-1fdf-4562-9443-272ad9ab5ba8
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Estimate Memory Requirements for Memory-Optimized Tables
  Whether you are creating a new [!INCLUDE[hek_2](../../includes/hek-2-md.md)] memory-optimized table or migrating an existing disk-based table to a memory-optimized table, it is important to have a reasonable estimate of each table's memory needs so you can provision the server with sufficient memory. This section describes how to estimate the amount of memory that you need to hold data for a memory-optimized table.  
  
 If you are contemplating migrating from disk-based tables to memory-optimized tables, before you proceed in this topic, see the topic [Determining if a Table or Stored Procedure Should Be Ported to In-Memory OLTP](determining-if-a-table-or-stored-procedure-should-be-ported-to-in-memory-oltp.md) for guidance on which tables are best to migrate. All the topics under [Migrating to In-Memory OLTP](migrating-to-in-memory-oltp.md) provide guidance on migrating from disk-based to memory-optimized tables.  
  
## Sections in this topic  
  
-   [Example memory-optimized table](#bkmk_ExampleTable)  
  
-   [Memory for the table](#bkmk_MemoryForTable)  
  
-   [Memory for indexes](#bkmk_IndexMeemory)  
  
-   [Memory for row versioning](#bkmk_MemoryForRowVersions)  
  
-   [Memory for table variables](#bkmk_TableVariables)  
  
-   [Memory for growth](#bkmk_MemoryForGrowth)  
  
##  <a name="bkmk_ExampleTable"></a> Example memory-optimized table  
 Consider the following memory-optimized table schema:  
  
```sql  
  
CREATE TABLE t_hk (  
col1 int NOT NULL PRIMARY KEY NONCLUSTERED,  
col2 int NOT NULL INDEX t1c2_index   
     HASH WITH (bucket_count = 5000000),  
col3 int NOT NULL INDEX t1c3_index   
     HASH WITH (bucket_count = 5000000),  
col4 int NOT NULL INDEX t1c4_index   
     HASH WITH (bucket_count = 5000000),  
col5 int NOT NULL INDEX t1c5_index NONCLUSTERED,  
col6 char (50) NOT NULL,  
col7 char (50) NOT NULL,   
col8 char (30) NOT NULL,   
col9 char (50) NOT NULL  
     WITH (memory_optimized = on)  
GO  
  
```  
  
 Using this schema we will determine the minimum memory needed for this memory-optimized table.  
  
##  <a name="bkmk_MemoryForTable"></a> Memory for the table  
 A memory-optimized table row is comprised of three parts:  
  
-   **Timestamps**   
    Row header/timestamps = 24 bytes.  
  
-   **Index pointers**   
    For each hash index in the table, each row has an 8-byte address pointer to the next row in the index.  Since there are 4 indexes, each row will allocate 32 bytes for index pointers (an 8 byte pointer for each index).  
  
-   **Data**   
    The size of the data portion of the row is determined by summing the type size for each data column.  In our table we have five 4-byte integers, three 50-byte character columns, and one 30-byte character column.  Therefore the data portion of each row is 4 + 4 + 4 + 4 + 4 + 50 + 50 + 30 + 50 or 200 bytes.  
  
 The following is a size computation for 5,000,000 (5 million) rows in a memory-optimized table. The total memory used by data rows is estimated as follows:  
  
 **Memory for the table's rows**  
  
 From the above calculations, the size of each row in the memory-optimized table is 24 + 32 + 200, or 256 bytes.  Since we have 5 million rows, the table will consume 5,000,000 * 256 bytes, or 1,280,000,000 bytes - approximately 1.28 GB.  
  
##  <a name="bkmk_IndexMeemory"></a> Memory for indexes  
 **Memory for each hash index**  
  
 Each hash index is a hash array of 8-byte address pointers.  The size of the array is best determined by the number of unique index values for that index - e.g., the number of unique Col2 values is a good starting point for the array size for the t1c2_index. A hash array that is too big wastes memory.  A hash array that is too small slows performance since there will be too many collisions by index values that hash to the same index.  
  
 Hash indexes achieve very fast equality lookups such as:  
  
```sql  
  
SELECT * FROM t_hk  
   WHERE Col2 = 3  
  
```  
  
 Nonclustered indexes are faster for range lookups such as:  
  
```sql  
  
SELECT * FROM t_hk  
   WHERE Col2 >= 3  
  
```  
  
 If you are migrating a disk-based table you can use the following to determine the number of unique values for the index t1c2_index.  
  
```sql  
  
SELECT COUNT(DISTINCT [Col2])  
  FROM t_hk  
  
```  
  
 If you are creating a new table, you'll need to estimate the array size or gather data from your testing prior to deployment.  
  
 For information on how hash indexes work in [!INCLUDE[hek_2](../../includes/hek-2-md.md)] memory-optimized tables, see [Hash Indexes](../../database-engine/hash-indexes.md).  
  
 **Note:** You cannot change the hash index array size on the fly. To change the hash index array size you must drop the table, change the bucket_count value and recreate the table.  
  
 **Setting the hash index array size**  
  
 The hash array size is set by `(bucket_count= <value>)` where \<value> is an integer value greater than zero. If \<value> is not a power of 2, the actual bucket_count is rounded up to the next closest power of 2.  In our example table, (bucket_count = 5000000), since 5,000,000 is not a power of 2, the actual bucket count rounds up to 8,388,608 (2<sup>23</sup>).  You must use this number, not 5,000,000 when calculating memory needed by the hash array.  
  
 Thus, in our example, the memory needed for each hash array is:  
  
 8,388,608 * 8 = 2<sup>23</sup> \* 8 = 2<sup>23</sup> \* 2<sup>3</sup> = 2<sup>26</sup> = 67,108,864 or approximately 64 MB.  
  
 Since we have three hash indexes, the memory needed for the hash indexes is 3 * 64MB = 192MB.  
  
 **Memory for non-clustered indexes**  
  
 Non-clustered indexes are implemented as BTrees with the inner nodes containing the index value and pointers to subsequent nodes.  Leaf nodes contain the index value and a pointer to the table row in memory.  
  
 Unlike hash indexes, non-clustered indexes do not have a fixed bucket size. The index grows and shrinks dynamically with the data.  
  
 Memory needed by non-clustered indexes can be computed as follows:  
  
-   **Memory allocated to non-leaf nodes**   
    For a typical configuration, the memory allocated to non-leaf nodes is a small percentage of the overall memory taken by the index. This is so small it can safely be ignored.  
  
-   **Memory for leaf nodes**   
    The leaf nodes have one row for each unique key in the table that points to the data rows with that unique key.  If you have multiple rows with the same key (i.e., you have a non-unique non-clustered index), there is only one row in the index leaf node that points to one of the rows with the other rows linked to each other.  Thus, the total memory required can be approximated by:   
    memoryForNonClusteredIndex = (pointerSize + sum(keyColumnDataTypeSizes)) * rowsWithUniqueKeys  
  
 Non-clustered indexes are best when used for range lookups, as exemplified by the following query:  
  
```sql  
  
SELECT * FROM t_hk  
   WHERE c2 > 5  
```  
  
##  <a name="bkmk_MemoryForRowVersions"></a> Memory for row versioning  
 To avoid locks, In-Memory OLTP uses optimistic concurrency when updating or deleting rows. This means that when a row is updated, an additional version of the row is created. The system keeps the previous versions available until all transactions that could possibly use the version have finished execution. When a row is deleted, the system acts in a similar way to an update, keeping the version available until it is no longer necessary. Reads and inserts do not create additional row versions.  
  
 Because there may be a number of additional rows in memory at any time waiting for the garbage collection cycle to release their memory, you must have sufficient memory to accommodate these additional rows.  
  
 The number of additional rows can be estimated by computing the peak number of row updates and deletions per second, then multiplying that by the number of seconds the longest transaction takes (minimum of 1).  
  
 That value is then multiplied by the row size to get the number of bytes you need for row versioning.  
  
 `rowVersions = durationOfLongestTransactionInSeconds * peakNumberOfRowUpdatesOrDeletesPerSecond`  
  
 Memory needs for stale rows is then estimated by multiplying the number of stale rows by the size of a memory-optimized table row (see [Memory for the table](#bkmk_MemoryForTable) above).  
  
 `memoryForRowVersions = rowVersions * rowSize`  
  
##  <a name="bkmk_TableVariables"></a> Memory for table variables  
 Memory used for a table variable is released only when the table variable goes out of scope. Deleted rows, including rows deleted as part of an update, from a table variable are not subject to garbage collection. No memory is released until the table variable exits scope.  
  
 Table variables defined in a large SQL batch, as opposed to a procedure scope, which are used in many transactions, can consume a lot of memory. Because they are not garbage collected, deleted rows in a table variable can consume a lot memory and degrade performance since read operations need to scan past the deleted rows.  
  
##  <a name="bkmk_MemoryForGrowth"></a> Memory for growth  
 The above calculations estimate your memory needs for the table as it currently exists. In addition to this memory, you need to estimate the growth of the table and provide sufficient memory to accommodate that growth.  For example, if you anticipate 10% growth then you need to multiple the results from above by 1.1 to get the total memory needed for your table.  
  
## See Also  
 [Migrating to In-Memory OLTP](migrating-to-in-memory-oltp.md)  
  
  
