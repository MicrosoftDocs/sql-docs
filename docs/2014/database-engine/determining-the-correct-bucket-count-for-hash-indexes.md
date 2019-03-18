---
title: "Determining the Correct Bucket Count for Hash Indexes | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 6d1ac280-87db-4bd8-ad43-54353647d8b5
author: stevestein
ms.author: sstein
manager: craigg
---
# Determining the Correct Bucket Count for Hash Indexes
  You must specify a value for the `BUCKET_COUNT` parameter when you create the memory-optimized table. This topic makes recommendations for determining the appropriate value for the `BUCKET_COUNT` parameter. If you cannot determine the correct bucket count, use a nonclustered index instead.  An incorrect `BUCKET_COUNT` value, especially one that is too low, can significantly impact workload performance, as well as recovery time of the database. It is better to overestimate the bucket count.  
  
 Duplicate index keys can decrease performance with a hash index because the keys are hashed to the same bucket, causing that bucket's chain to increase.  
  
 For more information about nonclustered hash indexes, see [Hash Indexes](hash-indexes.md) and [Guidelines for Using Indexes on Memory-Optimized Tables](../relational-databases/in-memory-oltp/memory-optimized-tables.md).  
  
 One hash table is allocated for each hash index on a memory-optimized table. The size of the hash table allocated for an index is specified by the `BUCKET_COUNT` parameter in [CREATE TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-table-transact-sql) or [CREATE TYPE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-type-transact-sql). The bucket count will internally be rounded up to the next power of two. For example, specifying a bucket count of 300,000 will result in an actual bucket count of 524,288.  
  
 For links to an article and video on bucket count, see [How to determine the right bucket count for hash indexes (In-Memory OLTP)](https://www.mssqltips.com/sqlservertip/3104/determine-bucketcount-for-hash-indexes-for-sql-server-memory-optimized-tables/).  
  
## Recommendations  
 In most cases the bucket count should be between 1 and 2 times the number of distinct values in the index key. If the index key contains a lot of duplicate values, on average there are more than 10 rows for each index key value, use a nonclustered index instead  
  
 You may not always be able to predict how many values a particular index key may have or will have. Performance should be acceptable if the `BUCKET_COUNT` value is within 5 times of the actual number of key values.  
  
 To determine the number of unique index keys in existing data, use queries similar to the following examples:  
  
### Primary Key and Unique Indexes  
 Because the primary key index is unique, the number of distinct values in the key corresponds to the number of rows in the table. For an example primary key on (SalesOrderID, SalesOrderDetailID) in the table Sales.SalesOrderDetail in the AdventureWorks database, issue the following query to calculate the number of distinct primary key values, which corresponds to the number of rows in the table:  
  
```tsql  
SELECT COUNT(*) AS [row count]   
FROM Sales.SalesOrderDetail  
```  
  
 This query shows a row count of 121,317. Use a bucket count of 240,000 if the row count will not change significantly. Use a bucket count of 480,000 if the number of sales orders in the table is expected to quadruple.  
  
### Non-Unique Indexes  
 For other indexes, for example a multi-column index on (SpecialOfferID, ProductID), issue the following query to determine the number of unique index key values:  
  
```tsql  
SELECT COUNT(*) AS [SpecialOfferID_ProductID index key count]  
FROM   
   (SELECT DISTINCT SpecialOfferID, ProductID   
    FROM Sales.SalesOrderDetail) t  
```  
  
 This query returns an index key count for (SpecialOfferID, ProductID) of 484, indicating a that a nonclustered index should be used instead of a nonclustered hash index.  
  
### Determining the Number of Duplicates  
 To determine the average number of duplicate values for an index key value, divide the total number of rows by the number of unique index keys.  
  
 For the example index on (SpecialOfferID, ProductID), this leads to 121317 / 484 = 251. This means index key values have an average of 251, and thus this should be a nonclustered index.  
  
## Troubleshooting the Bucket Count  
 To troubleshoot bucket count issues in memory-optimized tables, use [sys.dm_db_xtp_hash_index_stats &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-xtp-hash-index-stats-transact-sql) to obtain statistics about the empty buckets and the length of row chains. The following query can be used to obtain statistics about all the hash indexes in the current database. The query can take several minutes to run if there are large tables in the database.  
  
```tsql  
SELECT   
   object_name(hs.object_id) AS 'object name',   
   i.name as 'index name',   
   hs.total_bucket_count,  
   hs.empty_bucket_count,  
   floor((cast(empty_bucket_count as float)/total_bucket_count) * 100) AS 'empty_bucket_percent',  
   hs.avg_chain_length,   
   hs.max_chain_length  
FROM sys.dm_db_xtp_hash_index_stats AS hs   
   JOIN sys.indexes AS i   
   ON hs.object_id=i.object_id AND hs.index_id=i.index_id  
```  
  
 The two key indicators of hash index health are:  
  
 *empty_bucket_percent*  
 *empty_bucket_percent* indicates the number of empty buckets in the hash index.  
  
 If *empty_bucket_percent* is less than 10 percent, the bucket count is likely to be too low. Ideally, the *empty_bucket_percent* should be 33 percent or greater. If the bucket count matches the number of index key values, about 1/3 of the buckets is empty, due to hash distribution.  
  
 *avg_chain_length*  
 *avg_chain_length* indicates the average length of the row chains in the hash buckets.  
  
 If *avg_chain_length* is greater than 10 and *empty_bucket_percent* is greater than 10 percent, there likely are many duplicate index key values and a nonclustered index would be more appropriate. An average chain length of 1 is ideal.  
  
 There are two factors that impact the chain length:  
  
1.  Duplicates; all duplicate rows are part of the same chain in the hash index.  
  
2.  Multiple key values map to the same bucket. The lower the bucket count, the more buckets that will have multiple values mapped to them.  
  
 As an example, consider the following table and script to insert sample rows in the table:  
  
```tsql  
CREATE TABLE [Sales].[SalesOrderHeader_test]  
(  
   [SalesOrderID] [uniqueidentifier] NOT NULL DEFAULT (newid()),  
   [OrderSequence] int NOT NULL,  
   [OrderDate] [datetime2](7) NOT NULL,  
   [Status] [tinyint] NOT NULL,  
  
PRIMARY KEY NONCLUSTERED HASH ([SalesOrderID]) WITH ( BUCKET_COUNT = 262144 ),  
INDEX IX_OrderSequence HASH (OrderSequence) WITH ( BUCKET_COUNT = 20000),  
INDEX IX_Status HASH ([Status]) WITH ( BUCKET_COUNT = 8),  
INDEX IX_OrderDate NONCLUSTERED ([OrderDate] ASC),  
)WITH ( MEMORY_OPTIMIZED = ON , DURABILITY = SCHEMA_AND_DATA )  
GO  
  
DECLARE @i int = 0  
BEGIN TRAN  
WHILE @i < 262144  
BEGIN  
   INSERT Sales.SalesOrderHeader_test (OrderSequence, OrderDate, [Status]) VALUES (@i, sysdatetime(), @i % 8)  
   SET @i += 1  
END  
COMMIT  
GO  
```  
  
 The script inserts 262,144 rows in the table. It inserts unique values in the primary key index and in IX_OrderSequence. It inserts a lot of duplicate values in the index IX_Status: the script only generates 8 distinct values.  
  
 The output of the BUCKET_COUNT troubleshooting query is as follows:  
  
|index name|total_bucket_count|empty_bucket_count|empty_bucket_percent|avg_chain_length|max_chain_length|  
|----------------|--------------------------|--------------------------|----------------------------|------------------------|------------------------|  
|IX_Status|8|4|50|65536|65536|  
|IX_OrderSequence|32768|13|0|8|26|  
|PK_SalesOrd_B14003C3F8FB3364|262144|96319|36|1|8|  
  
 Consider the three hash indexes on this table:  
  
-   IX_Status: 50 percent of the buckets are empty, which is good. However, the average chain length is very high (65,536). This indicates a large number of duplicate values. Therefore, using a nonclustered hash index is not appropriate in this case. A nonclustered index should be used instead.  
  
-   IX_OrderSequence: 0 percent of the buckets are empty, which is too low. In addition, the average chain length is 8. As the values in this index are unique, this means on average 8 values are mapped to each bucket. The bucket count should be increased. As the index key has 262,144 unique values, the bucket count should be at least 262,144. If future growth is expected, the number should be higher.  
  
-   Primary key index (PK__SalesOrder...): 36 percent of the buckets are empty, which is good. In addition the average chain length is 1, which is also good. No change needed.  
  
 For more information on troubleshooting issues with your memory-optimized hash indexes, see [Troubleshooting Common Performance Problems with Memory-Optimized Hash Indexes](../../2014/database-engine/troubleshooting-common-performance-problems-with-memory-optimized-hash-indexes.md).  
  
## Detailed Considerations for Further Optimization  
 This section outlines further considerations for optimizing the bucket count.  
  
 To achieve the best performance for hash indexes, balance the amount of memory allocated to the hash table and the number of distinct values in the index key. There is also a balance between the performance of point lookups and table scans:  
  
-   The higher the bucket count value, the more empty buckets there will be in the index. This has an impact on memory usage (8 bytes per bucket) and the performance of table scans, as each bucket is scanned as part of a table scan.  
  
-   The lower the bucket count, the more values are assigned to a single bucket. This decreases performance for point lookups and inserts, because [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] may need to traverse several values in a single bucket to find the value specified by the search predicate.  
  
 If the bucket count is significantly lower than the number of unique index keys, many values will map to each bucket. This degrades performance of most DML operations, particularly point lookups (lookups of individual index keys) and insert operations. For example, you may see poor performance of SELECT queries and, UPDATE and DELETE operations with equality predicates matching the index key columns in the WHERE clause. A low bucket count will also affect the recovery time of the database, as the indexes are recreated on database startup.  
  
### Duplicate Index Key Values  
 Duplicate values can increase the performance impact of hash collisions. This is usually not a problem if each index key has a low number of duplicates. But this can be a problem if the discrepancy between the number of unique index keys and the number of rows in the tables becomes very large.  
  
 All rows with the same index key will go into the same duplicate chain. If multiple index keys are in the same bucket due to a hash collision, index scanners always need to scan the full duplicate chain for the first value before they can locate the first row corresponding to the second value. Duplicate keys also make it more difficult for garbage collection to locate the row. For example, if there are 1,000 duplicates for any key and one of the rows is deleted, the garbage collector needs to scan the chain of 1,000 duplicates to unlink the row from the index. This is true even if the query that found the delete used a more efficient index (a primary key index) to locate the row, because the garbage collector needs to unlink from every index  
  
 For hash indexes, there are two ways to reduce the work caused by duplicate index key values:  
  
-   Use a nonclustered index instead. You can decrease the duplicates by adding columns to the index key without requiring any changes to the application.  
  
-   Specify a very high bucket count for the index. For example, 20-to-100 times the number of unique index keys. This will reduce hash collisions.  
  
### Small Tables  
 For smaller tables, memory utilization is usually not a concern, as the size of the index will be small compared to the overall size of the database.  
  
 You must now make a choice based on the kind of performance you want:  
  
-   If the performance-critical operations on the index are predominantly point lookups and/or insert operations, a higher bucket count would be appropriate to reduce the likelihood of hash collisions. Three times the number of rows or even more would be the best option.  
  
-   If full index scans are the predominant performance-critical operations, use a bucket count that is close to the actual number of index key values.  
  
### Big Tables  
 For large tables, memory utilization could become a concern. For example, with a 250 million row table that has 4 hash indexes, each with a bucket count of one billion, the overhead for the hash tables is 4 indexes * 1 billion buckets \* 8 bytes = 32 gigabytes of memory utilization. When choosing a bucket count of 250 million for each of the indexes, the total overhead for the hash tables will be 8 gigabytes. Note that this is in addition to the 8 bytes of memory usage each index adds to each individual row, which is 8 gigabytes in this scenario (4 indexes \* 8 bytes \* 250 million rows).  
  
 Full table scans are not usually in the performance-critical path for OLTP workloads. Therefore, the choice is between memory utilization versus performance of point lookup and insert operations:  
  
-   If memory utilization is a concern, choose a bucket count close to the number of index key values. The bucket count should not be significantly lower than the number of index key values, as this impacts most DML operations as well the time it takes to recover the database after server restart.  
  
-   When optimizing the performance for point lookups, a higher bucket count of two or even three times the number of unique index values would be appropriate. A higher bucket count would mean an increased memory utilization and an increase in the time required for a full index scan.  
  
## See Also  
 [Indexes on Memory-Optimized Tables](../../2014/database-engine/indexes-on-memory-optimized-tables.md)  
  
  
