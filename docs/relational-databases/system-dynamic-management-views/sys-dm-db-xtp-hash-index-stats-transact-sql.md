---
title: "sys.dm_db_xtp_hash_index_stats (Transact-SQL)"
description: For In-Memory OLTP tables, sys.dm_db_xtp_hash_index_stats helps you understand bucket counts hash indexes in memory-optimized tables.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/02/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_xtp_hash_index_stats"
  - "sys.dm_db_xtp_hash_index_stats_TSQL"
  - "dm_db_xtp_hash_index_stats"
  - "dm_db_xtp_hash_index_stats_TSQL"
helpviewer_keywords:
  - "sys.dm_db_xtp_hash_index_stats (dynamic management view)"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_xtp_hash_index_stats (Transact-SQL)
[!INCLUDE[sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  These statistics are useful for understanding and tuning the bucket counts for [hash indexes in memory-optimized tables](../sql-server-index-design-guide.md#hash_index). It can also be used to detect cases where the index key has many duplicates.  
  
 A large average chain length indicates that many rows are hashed to the same bucket. This could happen because:  
  
-   If the number of empty buckets is low or the average and maximum chain lengths are similar, it is likely that the total bucket count is too low. This causes many different index keys to hash to the same bucket.  
  
-   If the number of empty buckets is high or the maximum chain length is high relative to the average chain length, it is likely that there are many rows with duplicate index key values or there is a skew in the key values. All rows with the same index key value hash to the same bucket, hence there is a long chain length in that bucket.  
  
Long chain lengths can significantly impact the performance of all DML operations on individual rows, including SELECT and INSERT. Short chain lengths along with a high empty bucket count are in indication of a bucket_count that is too high. This decreases the performance of index scans.  
  
> [!WARNING]
> This DMV scans the entire table. So, if there are large tables in your database, `sys.dm_db_xtp_hash_index_stats` may take a long time run.  
  
For more information, see [Hash Indexes for Memory-Optimized Tables](../../relational-databases/sql-server-index-design-guide.md#hash_index).  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|object_id|**int**|The object ID of parent table.|  
|xtp_object_id|**bigint**|ID of the memory-optimized table.|  
|index_id|**int**|The index ID.|  
|total_bucket_count|**bigint**|The total number of hash buckets in the index.|  
|empty_bucket_count|**bigint**|The number of empty hash buckets in the index.|  
|avg_chain_length|**bigint**|The average length of the row chains over all the hash buckets in the index.|  
|max_chain_length|**bigint**|The maximum length of the row chains in the hash buckets.|  
|xtp_object_id|**bigint**|The [!INCLUDE[inmemory](../../includes/inmemory-md.md)] object ID that corresponds to the memory-optimized table.|  
  
## Permissions  
 Requires VIEW DATABASE STATE permission on the server.  

## Examples  
  
### A. Troubleshoot hash index bucket count

The following query can be used to troubleshoot the hash index bucket count of an existing table. The query returns statistics about percentage of empty buckets and chain length for all hash indexes on user tables.

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
  FROM sys.dm_db_xtp_hash_index_stats as h   
  INNER JOIN sys.indexes as i  
            ON h.object_id = i.object_id  
           AND h.index_id  = i.index_id  
    INNER JOIN sys.memory_optimized_tables_internal_attributes ia ON h.xtp_object_id=ia.xtp_object_id
    INNER JOIN sys.tables t on h.object_id=t.object_id
  WHERE ia.type=1
  ORDER BY [table], [index];  
``` 

For details on how to interpret the results of this query, see [Troubleshooting Hash Indexes for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/hash-indexes-for-memory-optimized-tables.md) .  

### B. Hash index statistics for internal tables

Certain features use internal tables that leverage hash indexes, for example columnstore indexes on memory-optimized tables. The following query returns stats for hash indexes on internal tables that are linked to user tables.

```sql
  SELECT  
    QUOTENAME(SCHEMA_NAME(t.schema_id)) + N'.' + QUOTENAME(OBJECT_NAME(h.object_id)) as [user_table],
    ia.type_desc as [internal_table_type],
    i.name                   as [index],   
    h.total_bucket_count,  
    h.empty_bucket_count,  
    h.avg_chain_length,   
    h.max_chain_length  
  FROM sys.dm_db_xtp_hash_index_stats as h   
  INNER JOIN sys.indexes as i  
            ON h.object_id = i.object_id  
           AND h.index_id  = i.index_id  
    INNER JOIN sys.memory_optimized_tables_internal_attributes ia ON h.xtp_object_id=ia.xtp_object_id
    INNER JOIN sys.tables t on h.object_id=t.object_id
  WHERE ia.type!=1
  ORDER BY [user_table], [internal_table_type], [index]; 
```

Note that the BUCKET_COUNT of index on internal tables cannot be changed, thus the output of this query should be considered informative only. No action is required.  

This query is not expected to return any rows unless you are using a feature that leverages hash indexes on internal tables. The following memory-optimized table contains a columnstore index. After creating this table, you will see hash indexes on internal tables.

```sql
  CREATE TABLE dbo.table_columnstore
  (
      c1 INT NOT NULL PRIMARY KEY NONCLUSTERED,
      INDEX ix_columnstore CLUSTERED COLUMNSTORE
  ) WITH (MEMORY_OPTIMIZED=ON);
```

## See also

- [Introduction to Memory-Optimized Tables](../in-memory-oltp/introduction-to-memory-optimized-tables.md)
- [Memory-Optimized Table Dynamic Management Views](../../relational-databases/system-dynamic-management-views/memory-optimized-table-dynamic-management-views-transact-sql.md)
- [SQL Server and Azure SQL index architecture and design guide: Hash index design guidelines](../sql-server-index-design-guide.md#hash_index)

## Next steps 

- [[!INCLUDE[inmemory](../../includes/inmemory-md.md)] Overview and Usage Scenarios](../in-memory-oltp/overview-and-usage-scenarios.md)
- [Optimize performance by using in-memory technologies in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/in-memory-oltp-overview)
- [Troubleshooting Hash Indexes for Memory-Optimized Tables](../in-memory-oltp/hash-indexes-for-memory-optimized-tables.md)
