---
title: "Hash Indexes for Memory-Optimized Tables | Microsoft Docs"
ms.custom: 
  - "MSDN content"
  - "MSDN - SQL DB"
ms.date: "06/12/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.service: "sql-database"
ms.suite: ""
ms.technology: 
  - "database-engine-imoltp"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: e922cc3a-3d6e-453b-8d32-f4b176e98488
caps.latest.revision: 7
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Hash Indexes for Memory-Optimized Tables
[!INCLUDE[tsql-appliesto-ss2014-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2014-asdb-xxxx-xxx-md.md)]

  
This article describes the *hash* type of index that is available for a memory-optimized table. The article:  
  
- Provides short code examples to demonstrate the Transact-SQL syntax.  
- Describes the fundamentals of hash indexes.  
- Describes [how to estimate an appropriate bucket count](#configuring_bucket_count).  
- Describes how to design and manage your hash indexes.  
  
  
#### Prerequisite  
  
Important context information for understanding this article is available at:  
  
- [Indexes for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/indexes-for-memory-optimized-tables.md)  
  
  
  
## A. Syntax for memory-optimized indexes  
  
  
### A.1 Code sample for syntax  
  
This subsection contains a Transact-SQL code block that demonstrates the available syntaxes to create a hash index on a memory-optimized table:  
  
- The sample shows the hash index is declared inside the CREATE TABLE statement.  
  - You can instead declare the hash index in a separate [ALTER TABLE...ADD INDEX](#h3-b2-declaration-limitations) statement.  
  
  
  
    DROP TABLE IF EXISTS SupportEventHash;  
    go  
      
    CREATE TABLE SupportIncidentRating_Hash  
    (  
      SupportIncidentRatingId   int      not null   identity(1,1)  
        PRIMARY KEY NONCLUSTERED,  
      
      RatingLevel          int           not null,  
      
      SupportEngineerName  nvarchar(16)  not null,  
      Description          nvarchar(64)      null,  
      
      INDEX ix_hash_SupportEngineerName  
        HASH (SupportEngineerName) WITH (BUCKET_COUNT = 100000)  
    )  
      WITH (  
        MEMORY_OPTIMIZED = ON,  
        DURABILITY = SCHEMA_ONLY);  
    go  
  

To determine the right `BUCKET_COUNT` for your data, see [Configuring the hash index bucket count](#configuring_bucket_count). 
  
## B. Hash indexes  
  
  
### B.1 Performance basics  
  
The performance of a hash index is:  
  
- Excellent when the WHERE clause specifies an *exact* value for each column in the hash index key.  
- Poor when the WHERE clause looks for a *range* of values in the index key.  
- Poor when the WHERE clause specifies one specific value for the first column of a two column hash index key, but does not specify a value for the *second* column of the key.  
  
  
<a name="h3-b2-declaration-limitations"></a>  
  
### B.2 Declaration limitations  
  
A hash index can exist only on a memory-optimized table. It cannot exist on a disk-based table.  
  
A hash index can be declared as:  
  
- UNIQUE, or can default to Nonunique.  
- NONCLUSTERED, which is the default.   
  
  
Here is an example of the syntax to create a hash index outside of the CREATE TABLE statement:  
  
  
    ALTER TABLE MyTable_memop  
      ADD INDEX ix_hash_Column2 UNIQUE  
        HASH (Column2) WITH (BUCKET_COUNT = 64);  
  
  
  
### B.3 Buckets and hash function  
  
A hash index anchors its key values in what we call a *bucket* array:  
  
- Each bucket is 8 bytes, which are used to store the memory address of a link list of key entries.  
- Each entry is a value for an index key, plus the address of its corresponding row in the underlying memory-optimized table.  
  - Each entry points to the next entry in a link list of entries, all chained to the current bucket.  
  
  
The hashing algorithm tries to spread all the unique or distinct key values evenly among its buckets, but total evenness is an unreached ideal. All instances of any given key value are chained to the same bucket. The bucket might also have mixed in all instances of a different key value.  
  
- This mixture is called a *hash collision*. Collisions are common but are not ideal.  
- A realistic goal is for 30% of the buckets contain two different key values.  
  
  
You declare how many buckets a hash index shall have.  
  
- The lower the ratio of buckets to table rows or to distinct values, the longer the average bucket link list will be.  
- Short link lists perform faster than long link lists.  
  
  
SQL Server has one hash function it uses for all hash indexes:  
  
- The hash function is deterministic: given the same input key value, it consistently outputs the same bucket slot.  
- With repeated calls, the outputs of the hash function tend to form a Poisson or bell curve distribution, not a flat linear distribution.  
  
  
The interplay of the hash index and the buckets is summarized in the following image.  
  
  
![hekaton_tables_23d](../../relational-databases/in-memory-oltp/media/hekaton-tables-23d.png "Index keys, input into hash function, output is address of a hash bucket, which points to head of chain.")  
  
  
  
### B.4 Row versions and garbage collection  
  
  
In a memory-optimized table, when a row is affected by an SQL UPDATE, the table creates an updated version of the row. During the update transaction, other sessions might be able to read the older version of the row and thereby avoid the performance slowdown associated with a row lock.  
  
The hash index might also have different versions of its entries to accommodate the update.  
  
Later when the older versions are no longer needed, a garbage collection (GC) thread traverses the buckets and their link lists to clean away old entries. The GC thread performs better if the link list chain lengths are short.   
  
<a name="configuring_bucket_count"></a>
  
## C. Configuring the hash index bucket count  
  
The hash index bucket count is specified at index create time, and can be changed using the ALTER TABLE...ALTER INDEX REBUILD syntax.  
  
In most cases the bucket count would ideally be between 1 and 2 times the number of distinct values in the index key.   
You may not always be able to predict how many values a particular index key may have or will have. Performance is usually still good if the **BUCKET_COUNT** value is within 10 times of the actual number of key values, and overestimating is generally better than underestimating.  
  
Too *few* buckets has the following drawbacks:  
  
- More hash collisions of distinct key values.  
  - Each distinct value is forced to share the same bucket with a different distinct value.  
  - The average chain length per bucket grows.  
  - The longer the bucket chain, the slower the speed of equality lookups in the index and .  
  
Too *many* buckets has the following drawbacks:  
  
- Too high a bucket count might result in more empty buckets.  
  - Empty buckets impact the performance of full index scans. If those are performed regularly, consider picking a bucket count close to the number of distinct index key values.  
  - Empty buckets use memory, though each bucket uses only 8 bytes.  
    
  
> [!NOTE]
> Adding more buckets does nothing to reduce the chaining together of entries that share a duplicate value. The rate of value duplication is used to decide whether a hash is the appropriate index type, not to calculate the bucket count.  
  
  
  
### C.1 Practical numbers  
  
Even if the **BUCKET_COUNT** is moderately below or above the preferred range, the performance of your hash index is likely to be tolerable or acceptable. No crisis is created.  
  
Give your hash index a **BUCKET_COUNT** roughly equal to the number of rows you predict your memory-optimized table will grow to have.  
  
Suppose your growing table has 2,000,000 rows, but you predict the quantity will grow 10 times to 20,000,000 rows. Start with a bucket count that is 10 times the number of rows in the table. This gives you room for an increased quantity of rows.  
  
- Ideally you would increase the bucket count when the quantity of rows reaches the initial bucket count.  
- Even if the quantity of rows grows to 5 times larger than the bucket count, the performance is still good in most situations.  
  
Suppose a hash index has 10,000,000 distinct key values.  
  
- A bucket count of 2,000,000 would be about as low as you could accept. The degree of performance degradation could be tolerable.  
  
### C.2 Too many duplicate values in the index?  
  
If the hash indexed values have a high rate of duplicates, the hash buckets suffer longer chains.  
  
Assume you have the same SupportEvent table from the earlier T-SQL syntax code block. The following T-SQL code demonstrates how you can find and display the ratio of *all* values to *unique* values:  
  
        -- Calculate ratio of:  Rows / Unique_Values.  
    DECLARE @allValues float(8) = 0.0, @uniqueVals float(8) = 0.0;  
      
    SELECT @allValues = Count(*) FROM SupportEvent;  
      
    SELECT @uniqueVals = Count(*) FROM  
      (SELECT DISTINCT SupportEngineerName  
         FROM SupportEvent) as d;  
      
        -- If (All / Unique) >= 10.0, use a nonclustered index, not a hash.   
    SELECT Cast((@allValues / @uniqueVals) as float) as [All_divby_Unique];  
    go  
  
- A ratio of 10.0 or higher means a hash would be a poor type of index. Consider using a nonclustered index instead,   
  
## D. Troubleshooting hash index bucket count  
  
This section discusses how to troubleshoot the bucket count for your hash index.  
  
### D.1 Monitor statistics for chains and empty buckets  
  
You can monitor the statistical health of your hash indexes by running the following T-SQL SELECT. The SELECT uses the data management view (DMV) named **sys.dm_db_xtp_hash_index_stats**.  
  
  
```t-sql
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
  

  
### D.2 Demonstration of chains and empty buckets  
  
  
The following T-SQL code block gives you an easy way to test a `SELECT * FROM sys.dm_db_xtp_hash_index_stats;`. The code block completes in 1 minute. Here are the phases of the following code block:  
  
  
1. Creates a memory-optimized table that has a few hash indexes.  
2. Populates the table with thousands of rows.  
    a. A modulo operator is used to configure the rate of duplicate values in the StatusCode column.  
    b. The loop INSERTs 262144 rows in approximately 1 minute.  
3. PRINTs a message asking you to run the earlier SELECT from **sys.dm_db_xtp_hash_index_stats**.  
  
  
```t-sql
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
      
    SET NoCount ON;  
      
      -- Same as PK bucket_count.  68 seconds to complete.  
    DECLARE @i int = 262144;  
      
    BEGIN TRANSACTION;  
      
    WHILE @i > 0  
    Begin  
      
      INSERT SalesOrder_Mem  
          (OrderSequence, OrderDate, StatusCode)  
        Values  
          (@i, GetUtcDate(), @i % 8);  -- Modulo technique.  
      
      SET @i -= 1;  
    End  
    COMMIT TRANSACTION;  
      
    PRINT 'Next, you should query:  sys.dm_db_xtp_hash_index_stats .';  
    go  
```  
  
  
The preceding INSERT loop does the following:  
  
- Inserts unique values for the primary key index, and for ix_OrderSequence.  
- Inserts  a couple hundred thousands rows which represent only 8 distinct values for StatusCode. Therefore there is a high rate of value duplication in index ix_StatusCode.  
  
For troubleshooting when the bucket count is not optimal, examine the following output of the SELECT from **sys.dm_db_xtp_hash_index_stats**. For these results we added `WHERE Object_Name(h.object_id) = 'SalesOrder_Mem'` to the SELECT copied from section D.1.  
  
  
  
Our SELECT results are displayed after the code, artificially split into two narrower results tables for better display.  
  
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
  
  
### D.3 Balancing the trade-off  
  
OLTP workloads focus on individual rows. Full table scans are not usually in the performance critical path for OLTP workloads. Therefore, the trade-off you must balance is between:  
  
- Quantity of memory utilization; versus  
- Performance of equality tests, and of insert operations.  
  
  
*If memory utilization is the bigger concern:*  
  
- Choose a bucket count close to the number of index key records.  
- The bucket count should not be significantly lower than the number of index key values, as this impacts most DML operations as well the time it takes to recover the database after server restart.  
  
  
*If performance of equality tests is the bigger concern:*  
  
- A higher bucket count, of two or three times the number of unique index values, is appropriate. A higher count means:  
  - Faster retrievals when looking for one specific value.  
  - An increased memory utilization.  
  - An increase in the time required for a full scan of the hash index.  
  
  
  
  
## E. Strengths of hash indexes  
  
  
A hash index is preferable over a nonclustered index when:  
  
- Queries test the indexed column by use of a WHERE clause with an equality, as in the following:  
  
  
  
    SELECT col9 FROM TableZ  
        WHERE Z_Id = 2174;  
  
  
  
### E.1 Multi-column hash index keys  
  
  
Your two column index could be a nonclustered index or a hash index. Suppose the index columns are col1 and col2. Given the following SQL SELECT statement, only the nonclustered index would be useful to the query optimizer:  
  
  
    SELECT col1, col3  
        FROM MyTable_memop  
        WHERE col1 = 'dn';  
  
  
The hash index needs the WHERE clause to specify an equality test for each of the columns in its key. Else the hash index is not useful to the optimizer.  
  
Neither index type is useful if the WHERE clause specifies only the second column in the index key.  
