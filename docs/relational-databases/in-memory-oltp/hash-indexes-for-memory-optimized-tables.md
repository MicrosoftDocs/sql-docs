---
title: "Troubleshooting Hash Indexes for Memory-Optimized Tables | Microsoft Docs"
ms.custom: ""
ms.date: "12/01/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: e922cc3a-3d6e-453b-8d32-f4b176e98488
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Troubleshooting Hash Indexes for Memory-Optimized Tables
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

## Prerequisite  
  
Important context information for understanding this article is available at:  
  
- [Indexes for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/indexes-for-memory-optimized-tables.md)
- [Hash Indexes for Memory-Optimized Tables](../../relational-databases/sql-server-index-design-guide.md#hash_index) 
 
## Practical numbers  
  
When creating a hash index for a memory-optimized table, the number of buckets needs to be specified at create time. In most cases the bucket count would ideally be between 1 and 2 times the number of distinct values in the index key. 

However, even if the **BUCKET_COUNT** is moderately below or above the preferred range, the performance of your hash index is likely to be tolerable or acceptable. 
At minimum, consider giving your hash index a **BUCKET_COUNT** roughly equal to the number of rows you predict your memory-optimized table will grow to have.  
Suppose your growing table has 2,000,000 rows, but the prediction is it will grow 10 times to 20,000,000 rows. Start with a bucket count that is 10 times the number of rows in the table. This gives you room for an increased quantity of rows.  
  
- Ideally you would increase the bucket count when the quantity of rows reaches the initial bucket count.  
- Even if the quantity of rows grows to 5 times larger than the bucket count, the performance is still good in most situations.  
  
Suppose a hash index has 10,000,000 distinct key values.  
  
- A bucket count of 2,000,000 would be about as low as you could accept. The degree of performance degradation could be tolerable.  
  
## Too many duplicate values in the index?  
  
If the hash indexed values have a high rate of duplicates, the hash buckets suffer longer chains.  
  
Assume you have the same SupportEvent table from the earlier T-SQL syntax code block. The following T-SQL code demonstrates how you can find and display the ratio of *all* values to *unique* values:  
  
```sql
-- Calculate ratio of:  Rows / Unique_Values.  
DECLARE @allValues float(8) = 0.0, @uniqueVals float(8) = 0.0;  
  
SELECT @allValues = Count(*) FROM SupportEvent;  
  
SELECT @uniqueVals = Count(*) FROM  
  (SELECT DISTINCT SupportEngineerName  
      FROM SupportEvent) as d;  
  
    -- If (All / Unique) >= 10.0, use a nonclustered index, not a hash.   
SELECT Cast((@allValues / @uniqueVals) as float) as [All_divby_Unique];  
go  
```
  
- A ratio of 10.0 or higher means a hash would be a poor type of index. Consider using a nonclustered index instead,   
  
## Troubleshooting hash index bucket count  
  
This section discusses how to troubleshoot the bucket count for your hash index.  
  
### Monitor statistics for chains and empty buckets  
  
You can monitor the statistical health of your hash indexes by running the following T-SQL SELECT. The SELECT uses the data management view (DMV) named **sys.dm_db_xtp_hash_index_stats**.  
  
```sql
SELECT  
  QUOTENAME(SCHEMA_NAME(t.schema_id)) + N'.' + QUOTENAME(OBJECT_NAME(h.object_id)) as [table],   
  i.name                   as [index],   
  h.total_bucket_count,  
  h.empty_bucket_count,  
    
  FLOOR((  
    CAST(h.empty_bucket_count as float) /  
      h.total_bucket_count) * 100)  
                            as [empty_bucket_percent],  
  h.avg_chain_length,   
  h.max_chain_length  
FROM  
        sys.dm_db_xtp_hash_index_stats  as h   
  JOIN sys.indexes                     as i  
          ON h.object_id = i.object_id  
          AND h.index_id  = i.index_id  
JOIN sys.memory_optimized_tables_internal_attributes ia ON h.xtp_object_id=ia.xtp_object_id
JOIN sys.tables t on h.object_id=t.object_id
WHERE ia.type=1
ORDER BY [table], [index];  
```
  
Compare the SELECT results to the following statistical guidelines:  
  
- Empty buckets:  
  - 33% is a good target value, but a larger percentage (even 90%) is usually fine.  
  - When the bucket count equals the number of distinct key values, approximately 33% of the buckets are empty.  
  - A value below 10% is too low.  
- Chains within buckets:  
  - An average chain length of 1 is ideal in case there are no duplicate index key values. Chain lengths up to 10 are usually acceptable.  
  - If the average chain length is greater than 10, and the empty bucket percent is greater than 10%, the data has so many duplicates that a hash index might not be the most appropriate type.  
  
### Demonstration of chains and empty buckets  
  
The following T-SQL code block gives you an easy way to test a `SELECT * FROM sys.dm_db_xtp_hash_index_stats;`. The code block completes in 1 minute. Here are the phases of the following code block:  
  
1. Creates a memory-optimized table that has a few hash indexes.  
2. Populates the table with thousands of rows.  
    a. A modulo operator is used to configure the rate of duplicate values in the StatusCode column.  
    b. The loop inserts 262,144 rows in approximately 1 minute.  
3. PRINTs a message asking you to run the earlier SELECT from **sys.dm_db_xtp_hash_index_stats**.  
  
```sql
DROP TABLE IF EXISTS SalesOrder_Mem;  
go  
  
  
CREATE TABLE SalesOrder_Mem  
(  
  SalesOrderId   uniqueidentifier  NOT NULL  DEFAULT newid(),  
  OrderSequence  int               NOT NULL,  
  OrderDate      datetime2(3)      NOT NULL,  
  StatusCode     tinyint           NOT NULL,  
  
  PRIMARY KEY NONCLUSTERED  
      HASH (SalesOrderId)  WITH (BUCKET_COUNT = 262144),  
  
  INDEX ix_OrderSequence  
      HASH (OrderSequence) WITH (BUCKET_COUNT = 20000),  
  
  INDEX ix_StatusCode  
      HASH (StatusCode)    WITH (BUCKET_COUNT = 8),  
  
  INDEX ix_OrderDate       NONCLUSTERED (OrderDate DESC)  
)  
  WITH (MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_AND_DATA)  
go  
  
--------------------  
  
SET NOCOUNT ON;  
  
-- Same as PK bucket_count.  68 seconds to complete.  
DECLARE @i int = 262144;  
  
BEGIN TRANSACTION;  
  
WHILE @i > 0  
BEGIN  
  
  INSERT SalesOrder_Mem  
      (OrderSequence, OrderDate, StatusCode)  
    Values  
      (@i, GetUtcDate(), @i % 8);  -- Modulo technique.  
  
  SET @i -= 1;  
END  
COMMIT TRANSACTION;  
  
PRINT 'Next, you should query:  sys.dm_db_xtp_hash_index_stats .';  
go  
```  
  
The preceding `INSERT` loop does the following:  
  
- Inserts unique values for the primary key index, and for *ix_OrderSequence*.  
- Inserts a couple hundred thousands rows which represent only 8 distinct values for `StatusCode`. Therefore there is a high rate of value duplication in index *ix_StatusCode*.  
  
For troubleshooting when the bucket count is not optimal, examine the following output of the SELECT from **sys.dm_db_xtp_hash_index_stats**. For these results we added `WHERE Object_Name(h.object_id) = 'SalesOrder_Mem'` to the SELECT copied from section D.1.  
  
Our `SELECT` results are displayed after the code, artificially split into two narrower results tables for better display.  
  
- Here are the results for *bucket count*.  
  
| IndexName | total_bucket_count | empty_bucket_count | EmptyBucketPercent |  
| :-------- | -----------------: | -----------------: | -----------------: |  
| ix_OrderSequence | 32768 | 13 | 0 |  
| ix_StatusCode | 8 | 4 | 50 |  
| PK_SalesOrd_B14003... | 262144 | 96525 | 36 |  
  
- Next are the results for *chain length*.  
  
| IndexName | avg_chain_length | max_chain_length |  
| :-------- | ---------------: | ---------------: |  
| ix_OrderSequence | 8 | 26 |  
| ix_StatusCode | 65536 | 65536 |  
| PK_SalesOrd_B14003... | 1 | 8 |  
  
Let us interpret the preceding results tables for the three hash indexes:  
  
*ix_StatusCode:*  
  
- 50% of the buckets are empty, which is good.  
- However, the average chain length is very high at 65536.  
  - This indicates a high rate of duplicate values.  
  - Therefore, using a hash index is not appropriate in this case. A nonclustered index should be used instead.  
  
*ix_OrderSequence:*  
  
- 0% of the buckets are empty, which is too low.  
- The average chain length is 8, even though all values in this index are unique.  
  - Therefore the bucket count should be increased, to reduce the average chain length closer to 2 or 3.  
- Because the index key has 262144 unique values, the bucket count should be at least 262144.  
  - If future growth is expected, the bucket count should be higher.  
  
*Primary key index (PK_SalesOrd_...):*  
  
- 36% of the buckets are empty, which is good.  
- The average chain length is 1, which is also good. No change is needed.  
  
### Balancing the trade-off  
  
OLTP workloads focus on individual rows. Full table scans are not usually in the performance critical path for OLTP workloads. Therefore, the trade-off you must balance is between **quantity of memory utilization** versus **performance of equality tests and insert operations**.  
  
**If memory utilization is the bigger concern:**  
  
- Choose a bucket count close to the number of index key records.  
- The bucket count should not be significantly lower than the number of index key values, as this impacts most DML operations as well the time it takes to recover the database after server restart.  
  
**If performance of equality tests is the bigger concern:**  
  
- A higher bucket count, of two or three times the number of unique index values, is appropriate. A higher count means:  
  - Faster retrievals when looking for one specific value.  
  - An increased memory utilization.  
  - An increase in the time required for a full scan of the hash index.  
  

##  <a name="Additional_Reading"></a> Additional reading  
 [Hash Indexes for Memory-Optimized Tables](../../relational-databases/sql-server-index-design-guide.md#hash_index)   
 [Nonclustered Indexes for Memory-Optimized Tables](../../relational-databases/sql-server-index-design-guide.md#inmem_nonclustered_index)  
