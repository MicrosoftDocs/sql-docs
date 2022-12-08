---
title: "sys.column_store_row_groups (Transact-SQL)"
description: sys.column_store_row_groups (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "10/28/2020"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.column_store_row_groups_TSQL"
  - "column_store_row_groups"
  - "sys.column_store_row_groups"
  - "column_store_row_groups_TSQL"
  - "deleted bitmap"
helpviewer_keywords:
  - "sys.column_store_row_groups catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 76e7fef2-d1a4-4272-a2bb-5f5dcd84aedc
---
# sys.column_store_row_groups (Transact-SQL)
[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

  Provides clustered columnstore index information on a per-segment basis to help the administrator make system management decisions. **sys.column_store_row_groups** has a column for the total number of rows physically stored (including those marked as deleted) and a column for the number of rows marked as deleted. Use **sys.column_store_row_groups** to determine which row groups have a high percentage of deleted rows and should be rebuilt.  
   
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|int|The id of the table on which this index is defined.|  
|**index_id**|int|ID of the index for the table that has this columnstore index.|  
|**partition_number**|int|ID of the table partition that holds row group row_group_id. You can use partition_number to join this DMV to sys.partitions.|  
|**row_group_id**|int|The row group number associated with this row group. This is unique within the partition.<br /><br /> -1 = tail of an in-memory table.|  
|**delta_store_hobt_id**|bigint|The hobt_id for OPEN row group in the delta store.<br /><br /> NULL if the row group is not in the delta store.<br /><br /> NULL for the tail of an in-memory table.|  
|**state**|tinyint|ID number associated with the state_description.<br /><br /> 0 = INVISIBLE<br /><br /> 1 = OPEN<br /><br /> 2 = CLOSED<br /><br /> 3 = COMPRESSED <br /><br /> 4 = TOMBSTONE|  
|**state_description**|nvarchar(60)|Description of the persistent state of the row group:<br /><br /> INVISIBLE -A hidden compressed segment in the process of being built from data in a delta store. Read actions will use the delta store until the invisible compressed segment is completed. Then the new segment is made visible, and the source delta store is removed.<br /><br /> OPEN - A read/write row group that is accepting new records. An open row group is still in rowstore format and has not been compressed to columnstore format.<br /><br /> CLOSED - A row group that has been filled, but not yet compressed by the tuple mover process.<br /><br /> COMPRESSED - A row group that has filled and compressed.|  
|**total_rows**|bigint|Total rows physically stored in the row group. Some may have been deleted but they are still stored. The maximum number of rows in a row group is 1,048,576 (hexadecimal FFFFF).|  
|**deleted_rows**|bigint|Total rows in the row group marked deleted. This is always 0 for DELTA row groups.|  
|**size_in_bytes**|bigint|Size in bytes of all the data in this row group (not including metadata or shared dictionaries), for both DELTA and COLUMNSTORE rowgroups.|  
  
## Remarks  
 Returns one row for each columnstore row group for each table having a clustered or nonclustered columnstore index.  
  
 Use **sys.column_store_row_groups** to determine the number of rows included in the row group and the size of the row group.  
  
 When the number of deleted rows in a row group grows to a large percentage of the total rows, the table becomes less efficient. Rebuild the columnstore index to reduce the size of the table, reducing the disk I/O required to read the table. To rebuild the columnstore index use the **REBUILD** option of the **ALTER INDEX** statement.  
  
 The updateable columnstore first inserts new data into an **OPEN** rowgroup, which is in rowstore format, and is also sometimes referred to as a delta table.  Once an open rowgroup is full, its state changes to **CLOSED**. A closed rowgroup is compressed into columnstore format by the tuple mover and the state changes to **COMPRESSED**.  The tuple mover is a background process that periodically wakes up and checks whether there are any closed rowgroups that are ready to compress into a columnstore rowgroup.  The tuple mover also deallocates any rowgroups in which every row has been deleted. Deallocated rowgroups are marked as **TOMBSTONE**. To run tuple mover immediately, use the **REORGANIZE** option of the **ALTER INDEX** statement.  
  
 When a columnstore row group has filled, it is compressed, and stops accepting new rows. When rows are deleted from a compressed group, they remain but are marked as deleted. Updates to a compressed group are implemented as a delete from the compressed group, and an insert to an open group.  
  
## Permissions  
 Returns information for a table if the user has `VIEW DEFINITION` permission on the table.  
  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  
 The following example joins the **sys.column_store_row_groups** table to other system tables to return information about specific tables. The calculated `PercentFull` column is an estimate of the efficiency of the row group. To find information on a single table remove the comment hyphens in front of the **WHERE** clause and provide a table name.  
  
```sql  
SELECT i.object_id, object_name(i.object_id) AS TableName,   
i.name AS IndexName, i.index_id, i.type_desc,   
CSRowGroups.*,   
100*(total_rows - ISNULL(deleted_rows,0))/total_rows AS PercentFull    
FROM sys.indexes AS i  
JOIN sys.column_store_row_groups AS CSRowGroups  
    ON i.object_id = CSRowGroups.object_id  
AND i.index_id = CSRowGroups.index_id   
--WHERE object_name(i.object_id) = '<table_name>'   
ORDER BY object_name(i.object_id), i.name, row_group_id;  
```  
  
## See also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)   
 [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md)   
 [sys.all_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-all-columns-transact-sql.md)   
 [sys.computed_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-computed-columns-transact-sql.md)   
 [Columnstore Indexes Guide](~/relational-databases/indexes/columnstore-indexes-overview.md)     
 [sys.column_store_dictionaries &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-store-dictionaries-transact-sql.md)   
 [sys.column_store_segments &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-store-segments-transact-sql.md)  
  
  

