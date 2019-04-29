---
title: "Troubleshooting Common Performance Problems with Memory-Optimized Hash Indexes | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 1954a997-7585-4713-81fd-76d429b8d095
author: stevestein
ms.author: sstein
manager: craigg
---
# Troubleshooting Common Performance Problems with Memory-Optimized Hash Indexes
  This topic will focus on troubleshooting and working around common issues with hash indexes.  
  
## Search Requires a Subset of Hash Index Key Columns  
 **Issue:** Hash indexes require values for all index key columns in order to compute the hash value, and locate the corresponding rows in the hash table. Therefore, if a query includes equality predicates for only a subset of the index keys in the WHERE clause, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] cannot use an index seek to locate the rows corresponding to the predicates in the WHERE clause.  
  
 In contrast, ordered indexes like the disk-based nonclustered indexes and the memory-optimized nonclustered indexes support index seek on a subset of the index key columns, as long as they are the leading columns in the index.  
  
 **Symptom:** This results in a performance degradation, as [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] needs to execute full table scans rather than an index seek, which is typically a faster operation.  
  
 **How to troubleshoot:** Besides the performance degradation, inspection of the query plans will show a scan instead of an index seek. If the query is fairly simple, inspection of the query text and index definition will also show whether the search requires a subset of the index key columns.  
  
 Consider the following table and query:  
  
```sql  
CREATE TABLE [dbo].[od]  
(  
     o_id INT NOT NULL,  
     od_id INT NOT NULL,  
     p_id INT NOT NULL,  
     CONSTRAINT PK_od PRIMARY KEY NONCLUSTERED HASH (o_id, od_id) WITH (BUCKET_COUNT = 10000)  
)  
WITH (MEMORY_OPTIMIZED = ON)  
  
 SELECT p_id  
 FROM dbo.od  
 WHERE o_id=1  
```  
  
 The table has a hash index on the two columns (o_id, od_id), while the query has an equality predicate on (o_id). As the query has equality predicates on only a subset of the index key columns, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] cannot perform an index seek operation using PK_od; instead, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has to revert to a full index scan.  
  
 **Workarounds:** There are a number of possible workarounds. For example:  
  
-   Recreate the index as type nonclustered instead of nonclustered hash. The memory-optimized nonclustered index is ordered, and thus [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can perform an index seek on the leading index key columns. The resulting primary key definition for the example would be `constraint PK_od primary key nonclustered`.  
  
-   Change the current index key to match the columns in the WHERE clause.  
  
-   Add a new hash index that matches with the columns in the WHERE clause of the query. In the example, the resulting table definition would look at follows:  
  
    ```sql  
    CREATE TABLE dbo.od  
     ( o_id INT NOT NULL,  
     od_id INT NOT NULL,  
     p_id INT NOT NULL,  
  
     CONSTRAINT PK_od PRIMARY KEY   
     NONCLUSTERED HASH (o_id,od_id) WITH (BUCKET_COUNT=10000),  
  
     INDEX ix_o_id NONCLUSTERED HASH (o_id) WITH (BUCKET_COUNT=10000)  
  
     ) WITH (MEMORY_OPTIMIZED=ON)  
    ```  
  
 Note that a memory-optimized hash index does not perform optimally if there are a lot of duplicate rows for a given index key value: in the example, if the number of unique values for the column o_id is much smaller than the number of rows in the table, it would not be optimal to add an index on (o_id); instead, changing the type of the index PK_od from hash to nonclustered would be the better solution. For more information, see [Determining the Correct Bucket Count for Hash Indexes](../relational-databases/indexes/indexes.md).  
  
## See Also  
 [Indexes on Memory-Optimized Tables](../relational-databases/in-memory-oltp/memory-optimized-tables.md)  
  
  
