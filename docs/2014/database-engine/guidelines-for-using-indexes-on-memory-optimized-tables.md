---
title: "Guidelines for Using Indexes on Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
helpviewer_keywords: 
  - "hash indexes"
ms.assetid: 16ef63a4-367a-46ac-917d-9eebc81ab29b
author: stevestein
ms.author: sstein
manager: craigg
---
# Guidelines for Using Indexes on Memory-Optimized Tables
  Indexes are used for efficiently accessing data in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tables. Specifying the right indexes can dramatically improve query performance. Consider, for example, the query:  
  
```tsql  
SELECT c1, c2 FROM t WHERE c1 = 1;  
```  
  
 If there is no index on column c1, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] will need to scan the entire table t, and then filter on the rows that satisfy the condition c1=1. However, if t has an index on column c1, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can seek directly on the value 1 and retrieve the rows.  
  
 When searching for records that have a specific value, or range of values, for one or more columns in the table, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can use an index on those columns to quickly locate the corresponding records. Both disk-based and memory-optimized tables benefit from indexes. There are, however, certain differences between index structures that need to be considered when using memory-optimized tables. (Indexes on memory-optimized tables are referred to as memory-optimized indexes.) Some of the key differences are:  
  
-   Memory-optimized indexes must be created with [CREATE TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-table-transact-sql). Disk-based indexes can be created with `CREATE TABLE` and `CREATE INDEX`.  
  
-   Memory-optimized indexes exist only in memory. Index structures are not persisted to disk and index operations are not logged in the transaction log. The index structure is created when the memory-optimized table is created in memory, both during CREATE TABLE and during database startup.  
  
-   Memory-optimized indexes are inherently covering. Covering means that all columns are virtually included in the index and bookmark lookups are not needed for memory-optimized tables. Rather than a reference to the primary key, memory-optimized indexes simply contain a memory pointer to the actual row in the table data structure.  
  
-   Fragmentation and fillfactor do not apply to memory-optimized indexes. In disk-based indexes, fragmentation refers to pages in the B-tree being written to disk out-of-order. Memory-optimized indexes are not written to or read from disk. Fillfactor in disk-based B-tree indexes refers to the degree to which the physical page structures are filled with actual data. The memory-optimized index structures do not have fixed-size pages.  
  
 There are two types of memory-optimized indexes:  
  
-   Nonclustered hash indexes, which are made for point lookups. For more information about hash indexes, see [Hash Indexes](hash-indexes.md).  
  
-   Nonclustered indexes, which are made for range scans and ordered scans.  
  
 With a hash index, data is accessed through an in-memory hash table. Hash indexes do not have pages and are always of a fixed size. However, a hash index can have empty hash buckets, which result in limited wasted space. The values returned from a query using a hash index are not sorted. Hash indexes are optimized for index seeks on equality predicates and also support full index scans.  
  
 Nonclustered indexes (not hash indexes) support everything that hash indexes supports plus seek operations on inequality predicates such as greater than or less than, as well as sort order. Rows can be retrieved according to the order specified with index creation. If the sort order of the index matches the sort order required for a particular query, for example if the index key matches the ORDER BY clause, there is no need to sort the rows as part of query execution. Memory-optimized nonclustered indexes are unidirectional; they do not support retrieving rows in a sort order that is the reverse of the sort order of the index. For example, for an index specified as (c1 ASC), it is not possible to scan the index in reverse order, as (c1 DESC).  
  
 Each index consumes memory. Hash indexes consume a fixed amount of memory, which is a function of the bucket count. For nonclustered indexes, memory consumption is a function of the row count and the size of the index key columns, with some additional overhead depending on the workload. Memory for memory-optimized indexes is in addition to and separate from the memory used to store rows in memory-optimized tables.  
  
 Duplicate key values always share the same hash bucket. If a hash index contains many duplicate key values, the resulting long hash chains will harm performance. Hash collisions, which occur in any hash index, will further reduce performance in this scenario. For that reason, if the number of unique index keys is at least 100 times smaller than the row count, you can reduce the risk of hash collisions by making the bucket count much larger (at least eight times the number of unique index keys; see [Determining the Correct Bucket Count for Hash Indexes](../../2014/database-engine/determining-the-correct-bucket-count-for-hash-indexes.md) for more information) or you can eliminate hash collisions entirely by using a nonclustered index.  
  
## Determining Which Indexes to Use for a Memory-Optimized Table  
 Each memory-optimized table must have at least one index. Note that each PRIMARY KEY constraint implicitly creates an index. Therefore, if a table has a primary key, it has an index. A primary key is a requirement for a durable memory-optimized table.  
  
 When querying a memory-optimized table, hash indexes perform better when the predicate clause contains only equality predicates. The predicate must include all columns in the hash index key. A hash index will revert to a scan given an inequality predicate.  
  
 A column in a memory-optimized table can be part of both a hash index and a nonclustered index.  
  
 When querying a memory-optimized table with inequality predicates, nonclustered indexes will perform better than nonclustered hash indexes.  
  
 The hash index requires a key (to hash) to seek into the index. If an index key consists of two columns and you only provide the first column, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] does not have a complete key to hash. This will result in an index scan query plan. Usage determines which columns should be indexed.  
  
 When a column in a nonclustered index has the same value in many rows (index key columns have a lot of duplicate values), performance can degrade for updates, inserts, and deletions.  One way to improve performance in this situation is to add another column to the nonclustered index.  
  
### Operations on memory-optimized and disk-based indexes.  
  
|Operation|Memory-optimized, nonclustered hash, index|Memory-optimized nonclustered index|Disk-based index|  
|---------------|-------------------------------------------------|------------------------------------------|-----------------------|  
|Index Scan, retrieve all table rows.|Yes|Yes|Yes|  
|Index seek on equality predicate(s) (=).|Yes<br /><br /> (Full key required.)|Yes <sup>1</sup>|Yes|  
|Index seek on inequality predicates (>, <, \<=, >=, BETWEEN).|No (results in an index scan)|Yes <sup>1</sup>|Yes|  
|Retrieve rows in a sort-order matching the index definition.|No|Yes|Yes|  
|Retrieve rows in a sort-order matching the reverse of the index definition.|No|No|Yes|  
  
 In the table, Yes means that the index can adequately service the request and No means that the index cannot be used successfully to satisfy the request.  
  
 <sup>1</sup> For a nonclustered memory-optimized index, the full key is not required to perform an index seek. Although, given the column order of the index key, a scan will occur if a value for a column comes after a missing column.  
  
## Index Count  
 A memory-optimized table can have up to 8 indexes, including the index created with the primary key.  
  
 Regarding the number of indexes created on a memory-optimized table, consider the following:  
  
-   Specify the indexes you need when you create the table. You cannot create an index for a memory-optimized table after the table is created. If you want to add an index to a memory-optimized table, drop and recreate that table.  
  
-   Do not create an index that you rarely use:  
  
     Garbage collection works best if all indexes on the table are frequently used. Rarely-used indexes may cause the garbage collection system to not perform optimally for old row versions.  
  
## Creating a Memory-Optimized Index: Code Samples  
 Column level hash index:  
  
```tsql  
CREATE TABLE t1   
   (c1 INT NOT NULL INDEX idx HASH WITH (BUCKET_COUNT = 100))   
   WITH (MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_ONLY)  
```  
  
 Table level hash index:  
  
```tsql  
CREATE TABLE t1_1   
   (c1 INT NOT NULL,   
   INDEX IDX HASH (c1) WITH (BUCKET_COUNT = 100))   
   WITH (MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_ONLY)  
```  
  
 Column level primary key hash index:  
  
```tsql  
CREATE TABLE t2   
   (c1 INT NOT NULL PRIMARY KEY NONCLUSTERED HASH WITH (BUCKET_COUNT = 100))   
   WITH (MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_AND_DATA)  
```  
  
 Table level primary key hash index:  
  
```tsql  
CREATE TABLE t2_2   
   (c1 INT NOT NULL,   
   PRIMARY KEY NONCLUSTERED HASH (c1) WITH (BUCKET_COUNT = 100))   
   WITH (MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_AND_DATA)  
```  
  
 Column level nonclustered index:  
  
```tsql  
CREATE TABLE t3   
   (c1 INT NOT NULL INDEX ID)   
   WITH (MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_ONLY)  
```  
  
 Table level nonclustered  index:  
  
```tsql  
CREATE TABLE t3_3   
   (c1 INT NOT NULL,   
   INDEX IDX NONCLUSTERED (c1))   
   WITH (MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_ONLY)  
```  
  
 Column level primary key nonclustered  index:  
  
```tsql  
CREATE TABLE t4   
   (c1 INT NOT NULL PRIMARY KEY NONCLUSTERED)   
   WITH (MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_AND_DATA)  
```  
  
 Table level primary key nonclustered index:  
  
```tsql  
CREATE TABLE t4_4   
   (c1 INT NOT NULL,   
   PRIMARY KEY NONCLUSTERED (c1))   
   WITH (MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_AND_DATA)  
```  
  
 Multicolumn index defined after columns are defined:  
  
```tsql  
create table t (  
       a int not null constraint ta primary key nonclustered,  
       b int not null,  
       c int not null,  
       d int not null,  
       index idx_t_b_c_d nonclustered (b, c asc, d desc)  
) with (memory_optimized = on, durability = SCHEMA_AND_DATA)  
go  
```  
  
## See Also  
 [Indexes on Memory-Optimized Tables](../relational-databases/in-memory-oltp/memory-optimized-tables.md)   
 [Determining the Correct Bucket Count for Hash Indexes](../../2014/database-engine/determining-the-correct-bucket-count-for-hash-indexes.md)   
 [Hash Indexes](hash-indexes.md)  
  
  
