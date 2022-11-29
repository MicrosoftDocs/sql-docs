---
title: "sys.dm_db_incremental_stats_properties (Transact-SQL)"
description: sys.dm_db_incremental_stats_properties (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "12/18/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_incremental_stats_properties"
  - "sys.dm_db_incremental_stats_properties_TSQL"
  - "dm_db_incremental_stats_properties"
  - "dm_db_incremental_stats_properties_TSQL"
helpviewer_keywords:
  - "sys.dm_db_incremental_stats_properties"
dev_langs:
  - "TSQL"
ms.assetid: aa0db893-34d1-419c-b008-224852e71307
---
# sys.dm_db_incremental_stats_properties (Transact-SQL)
[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

  Returns properties of incremental statistics for the specified database object (table) in the current [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. The use of `sys.dm_db_incremental_stats_properties` (which contains a partition number) is similar to `sys.dm_db_stats_properties` which is used for non-incremental statistics. 
  
  This function was introduced in [!INCLUDE[ssSQL14_md](../../includes/sssql14-md.md)] Service Pack 2 and [!INCLUDE[ssSQL15_md](../../includes/sssql16-md.md)] Service Pack 1.
  
## Syntax  
  
```  
sys.dm_db_incremental_stats_properties (object_id, stats_id)  
```  
  
## Arguments  
 *object_id*  
 Is the ID of the object in the current database for which properties of one of its incremental statistics is requested. *object_id* is **int**.  
  
 *stats_id*  
 Is the ID of statistics for the specified *object_id*. The statistics ID can be obtained from the [sys.stats](../../relational-databases/system-catalog-views/sys-stats-transact-sql.md) dynamic management view. *stats_id* is **int**.  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_id|**int**|ID of the object (table) for which to return the properties of the statistics object.|  
|stats_id|**int**|ID of the statistics object. Is unique within the table. For more information, see [sys.stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-stats-transact-sql.md).|
|partition_number|**int**|Number of the partition containing the portion of the table.|  
|last_updated|**datetime2**|Date and time the statistics object was last updated. For more information, see the [Remarks](#Remarks) section in this page.|  
|rows|**bigint**|Total number of rows in the table when statistics were last updated. If the statistics are filtered or correspond to a filtered index, the number of rows might be less than the number of rows in the table.|  
|rows_sampled|**bigint**|Total number of rows sampled for statistics calculations.|  
|steps|**int**|Number of steps in the histogram. For more information, see [DBCC SHOW_STATISTICS &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md).|  
|unfiltered_rows|**bigint**|Total number of rows in the table before applying the filter expression (for filtered statistics). If statistics are not filtered, unfiltered_rows is equal to the value returns in the rows column.|  
|modification_counter|**bigint**|Total number of modifications for the leading statistics column (the column on which the histogram is built) since the last time statistics were updated.<br /><br /> This column does not contain information for memory-optimized tables.|  
  
## <a name="Remarks"></a> Remarks  
 `sys.dm_db_incremental_stats_properties` returns an empty rowset under any of the following conditions:  
  
-   `object_id` or `stats_id` is NULL.   
-   The specified object is not found or does not correspond to a table with incremental statistics.  
-   The specified statistics ID does not correspond to existing statistics for the specified object ID.  
-   The current user does not have permissions to view the statistics object.
 
 This behavior allows for the safe usage of `sys.dm_db_incremental_stats_properties` when cross applied to rows in views such as `sys.objects` and `sys.stats`. This method can return properties for the statistics that correspond to each partition. To see the properties for the merged statistics combined across all partitions, use the sys.dm_db_stats_properties instead. 

Statistics update date is stored in the [statistics blob object](../../relational-databases/statistics/statistics.md#DefinitionQOStatistics) together with the [histogram](../../relational-databases/statistics/statistics.md#histogram) and [density vector](../../relational-databases/statistics/statistics.md#density), not in the metadata. When no data is read to generate statistics data, the statistics blob is not created, the date is not available, and the *last_updated* column is NULL. This is the case for filtered statistics for which the predicate does not return any rows, or for new empty tables.

## Permissions  
 Requires that the user has select permissions on statistics columns or the user owns the table or the user is a member of the `sysadmin` fixed server role, the `db_owner` fixed database role, or the `db_ddladmin` fixed database role.  
  
## Examples  

### A. Simple example
The following example returns the statistics for the `PartitionTable` table described in the topic [Create Partitioned Tables and Indexes](../../relational-databases/partitions/create-partitioned-tables-and-indexes.md).

```sql
SELECT * FROM sys.dm_db_incremental_stats_properties (object_id('PartitionTable'), 1);
``` 

For additional usage suggestions, see  [sys.dm_db_stats_properties](../../relational-databases/system-dynamic-management-views/sys-dm-db-stats-properties-transact-sql.md).
  
## See Also  
 [DBCC SHOW_STATISTICS &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)   
 [sys.stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-stats-transact-sql.md)   
 [Object Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/object-related-dynamic-management-views-and-functions-transact-sql.md)   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)  
 [sys.dm_db_stats_properties](../../relational-databases/system-dynamic-management-views/sys-dm-db-stats-properties-transact-sql.md)   
 [sys.dm_db_stats_histogram (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-stats-histogram-transact-sql.md) 
