---
title: "sys.pdw_nodes_column_store_row_groups (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 74f34ac4-251a-46c4-87f1-9a182448963c
caps.latest.revision: 16
author: BarbKess
---
# sys.pdw_nodes_column_store_row_groups (SQL Server PDW)
Provides clustered columnstore index information on a per-segment basis to help the administrator make system management decisions in SQL Server PDW. **sys.pdw_nodes_column_store_row_groups** has a column for the total number of rows physically stored (including those marked as deleted) and a column for the number of rows marked as deleted. Use **sys.pdw_nodes_column_store_row_groups** to determine which row groups have a high percentage of deleted rows and should be rebuilt.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**object_id**|**int**|ID of the underlying table. This is the physical table on the Compute node, not the object_id for the logical table on the Control node. For example, object_id does not match with the object_id in sys.tables.<br /><br />To join with sys.tables, use sys.pdw_index_mappings.|  
|**index_id**|**int**|ID of the clustered columnstore index on *object_id* table.|  
|**partition_number**|**int**|ID of the table partition that holds row group *row_group_id*. You can use *partition_number* to join this DMV to sys.partitions.|  
|**row_group_id**|**int**|ID of this row group. This is unique within the partition.|  
|**dellta_store_hobt_id**|**bigint**|The hobt_id for delta row groups, or NULL if the row group type is not delta. A delta row group is a read/write row group that is accepting new records. A delta row group has the **OPEN** status. A delta row group is still in rowstore format and has not been compressed to columnstore format.|  
|**state**|**tinyint**|ID number associated with the state_description.<br /><br />1 = OPEN<br /><br />2 = CLOSED<br /><br />3 = COMPRESSED|  
|**state_desccription**|**nvarchar(60)**|Description of the persistent state of the row group:<br /><br />OPEN – A read/write row group that is accepting new records. An open row group is still in rowstore format and has not been compressed to columnstore format.<br /><br />CLOSED – A row group that has been filled, but not yet compressed by the tuple mover process.<br /><br />COMPRESSED – A row group that has filled and compressed.|  
|**total_rows**|**bigint**|Total rows physically stored in the row group. Some may have been deleted but they are still stored. The maximum number of rows in a row group is 1,048,576 (hexadecimal FFFFF).|  
|**deleted_rows**|**bigint**|Number of rows physically stored in the row group that are marked for deletion.<br /><br />Always 0 for DELTA row groups.|  
|**size_in_bytes**|**int**|Combined size, in bytes, of all the pages in this row group. This size does not include the size required to store metadata or shared dictionaries.|  
|**pdw_node_id**|**int**|Unique id of a SQL Server PDW node.|  
  
## Remarks  
Returns one row for each columnstore row group for each table having a clustered or nonclustered columnstore index.  
  
Use **sys.pdw_nodes_column_store_row_groups** to determine the number of rows included in the row group and the size of the row group.  
  
When the number of deleted rows in a row group grows to a large percentage of the total rows, the table becomes less efficient. Rebuild the columnstore index to reduce the size of the table, reducing the disk I/O required to read the table. To rebuild the columnstore index use the **REBUILD** option of the **ALTER INDEX** statement.  
  
The updateable columnstore first inserts new data into an **OPEN** rowgroup, which is in rowstore format, and is also sometimes referred to as a delta table.  Once an open rowgroup is full, its state changes to **CLOSED**. A closed rowgroup is compressed into columnstore format by the tuple mover and the state changes to **COMPRESSED**.  The tuple mover is a background process that periodically wakes up and checks whether there are any closed rowgroups that are ready to compress into a columnstore rowgroup.  The tuple mover also deallocates any rowgroups in which every row has been deleted. Deallocated rowgroups are marked as **RETIRED**. To run tuple mover immediately, use the **REORGANIZE** option of the **ALTER INDEX** statement.  
  
When a columnstore row group has filled, it is compressed, and stops accepting new rows. When rows are deleted from a compressed group, they remain but are marked as deleted. Updates to a compressed group are implemented as a delete from the compressed group, and an insert to an open group.  
  
## Permissions  
Requires **VIEW SERVER STATE** permission.  
  
## Examples  
The following example joins the **sys.pdw_nodes_column_store_row_groups** table to other system tables to return information about specific tables. The calculated `PercentFull` column is an estimate of the efficiency of the row group. To find information on a single table remove the comment hyphens in front of the WHERE clause and provide a table name.  
  
```  
SELECT IndexMap.object_id,   
  object_name(IndexMap.object_id) AS LogicalTableName,   
  i.name AS LogicalIndexName, IndexMap.index_id, NI.type_desc,   
  IndexMap.physical_name AS PhyIndexNameFromIMap,   
  CSRowGroups.*,  
  100*(ISNULL(deleted_rows,0))/total_rows AS PercentDeletedRows   
FROM sys.tables AS t  
JOIN sys.indexes AS i  
    ON t.object_id = i.object_id  
JOIN sys.pdw_index_mappings AS IndexMap  
    ON i.object_id = IndexMap.object_id  
    AND i.index_id = IndexMap.index_id  
JOIN sys.pdw_nodes_indexes AS NI  
    ON IndexMap.physical_name = NI.name  
    AND IndexMap.index_id = NI.index_id  
JOIN sys.pdw_nodes_column_store_row_groups AS CSRowGroups  
    ON CSRowGroups.object_id = NI.object_id   
    AND CSRowGroups.pdw_node_id = NI.pdw_node_id  
AND CSRowGroups.index_id = NI.index_id      
--WHERE t.name = '<table_name>'   
ORDER BY object_name(i.object_id), i.name, IndexMap.physical_name, pdw_node_id;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
[Clustered Columnstore Indexes &#40;SQL Server PDW&#41;](../sqlpdw/clustered-columnstore-indexes-sql-server-pdw.md)  
[CREATE COLUMNSTORE INDEX &#40;SQL Server PDW&#41;](../sqlpdw/create-columnstore-index-sql-server-pdw.md)  
[sys.pdw_nodes_column_store_segments &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-nodes-column-store-segments-sql-server-pdw.md)  
[sys.pdw_nodes_column_store_dictionaries &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-nodes-column-store-dictionaries-sql-server-pdw.md)  
  
