---
title: "sys.dm_column_store_object_pool (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: a8a58ca7-0a7d-4786-bfd9-e8894bd345dd
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_column_store_object_pool (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

 Returns counts of different types of object memory pool usage for columnstore index objects.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|`database_id`|`int`|ID of the database. This is unique within an instance of a SQL Server database or an Azure SQL database server. |  
|`object_id`|`int`|ID of the object. The object is one of the object_types. | 
|`index_id`|`int`|ID of the columnstore index.|  
|`partition_number`|`bigint`|1-based partition number within the index or heap. Every table or view has at least one partition.| 
|`column_id`|`int`|ID of the columnstore column. This is NULL for DELETE_BITMAP.| 
|`row_group_id`|`int`|ID of the rowgroup.|
|`object_type`|`smallint`|1 = COLUMN_SEGMENT<br /><br /> 2 = COLUMN_SEGMENT_PRIMARY_DICTIONARY<br /><br /> 3 = COLUMN_SEGMENT_SECONDARY_DICTIONARY<br /><br /> 4 = COLUMN_SEGMENT_BULKINSERT_DICTIONARY<br /><br /> 5 = COLUMN_SEGMENT_DELETE_BITMAP|  
|`object_type_desc`|`nvarchar(60)`|COLUMN_SEGMENT - A column segment. `object_id` is the segment ID. A segment stores all the values for one column within one rowgroup. For example, if a table has 10 columns, there are 10 column segments per rowgroup. <br /><br /> COLUMN_SEGMENT_PRIMARY_DICTIONARY - A global dictionary that contains lookup information for all of the column segments in the table.<br /><br /> COLUMN_SEGMENT_SECONDARY_DICTIONARY - A local dictionary associated with one column.<br /><br /> COLUMN_SEGMENT_BULKINSERT_DICTIONARY - Another representation of the global dictionary. This provides an inverse look up of value to dictionary_id. Used for creating compressed segments as part of Tuple Mover or Bulk Load.<br /><br /> COLUMN_SEGMENT_DELETE_BITMAP - A bitmap that tracks segment deletes. There is one delete bitmap per partition.|  
|`access_count`|`int`|Number of read or write accesses to this object.|  
|`memory_used_in_bytes`|`bigint`|Memory used by this object in the object pool.|  
|`object_load_time`|`datetime`|Clock-time for when object_id was brought into the object pool.|  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.   
On [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)], requires the `VIEW DATABASE STATE` permission in the database.   
 
## See Also  
  
 [Index Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/index-related-dynamic-management-views-and-functions-transact-sql.md)   
 [sys.dm_db_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md)   
 [sys.dm_db_index_operational_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-operational-stats-transact-sql.md)   
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [Monitor and Tune for Performance](../../relational-databases/performance/monitor-and-tune-for-performance.md)  
  
  
