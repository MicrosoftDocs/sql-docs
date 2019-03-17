---
title: "sys.pdw_nodes_column_store_row_groups (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.technology: data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 17a4c925-d4b5-46ee-9cd6-044f714e6f0e
author: ronortloff
ms.author: rortloff
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.pdw_nodes_column_store_row_groups (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  Provides clustered columnstore index information on a per-segment basis to help the administrator make system management decisions in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)]. **sys.pdw_nodes_column_store_row_groups** has a column for the total number of rows physically stored (including those marked as deleted) and a column for the number of rows marked as deleted. Use **sys.pdw_nodes_column_store_row_groups** to determine which row groups have a high percentage of deleted rows and should be rebuilt.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of the underlying table. This is the physical table on the Compute node, not the object_id for the logical table on the Control node. For example, object_id does not match with the object_id in sys.tables.<br /><br /> To join with sys.tables, use sys.pdw_index_mappings.|  
|**index_id**|**int**|ID of the clustered columnstore index on *object_id* table.|  
|**partition_number**|**int**|ID of the table partition that holds row group *row_group_id*. You can use *partition_number* to join this DMV to sys.partitions.|  
|**row_group_id**|**int**|ID of this row group. This is unique within the partition.|  
|**dellta_store_hobt_id**|**bigint**|The hobt_id for delta row groups, or NULL if the row group type is not delta. A delta row group is a read/write row group that is accepting new records. A delta row group has the **OPEN** status. A delta row group is still in rowstore format and has not been compressed to columnstore format.|  
|**state**|**tinyint**|ID number associated with the state_description.<br /><br /> 1 = OPEN<br /><br /> 2 = CLOSED<br /><br /> 3 = COMPRESSED|  
|**state_desccription**|**nvarchar(60)**|Description of the persistent state of the row group:<br /><br /> OPEN - A read/write row group that is accepting new records. An open row group is still in rowstore format and has not been compressed to columnstore format.<br /><br /> CLOSED - A row group that has been filled, but not yet compressed by the tuple mover process.<br /><br /> COMPRESSED - A row group that has filled and compressed.|  
|**total_rows**|**bigint**|Total rows physically stored in the row group. Some may have been deleted but they are still stored. The maximum number of rows in a row group is 1,048,576 (hexadecimal FFFFF).|  
|**deleted_rows**|**bigint**|Number of rows physically stored in the row group that are marked for deletion.<br /><br /> Always 0 for DELTA row groups.|  
|**size_in_bytes**|**int**|Combined size, in bytes, of all the pages in this row group. This size does not include the size required to store metadata or shared dictionaries.|  
|**pdw_node_id**|**int**|Unique id of a [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] node.|  
|**distribution_id**|**int**|Unique id of the distribution.|
  
## Remarks  
 Returns one row for each columnstore row group for each table having a clustered or nonclustered columnstore index.  
  
 Use **sys.pdw_nodes_column_store_row_groups** to determine the number of rows included in the row group and the size of the row group.  
  
 When the number of deleted rows in a row group grows to a large percentage of the total rows, the table becomes less efficient. Rebuild the columnstore index to reduce the size of the table, reducing the disk I/O required to read the table. To rebuild the columnstore index use the **REBUILD** option of the **ALTER INDEX** statement.  
  
 The updateable columnstore first inserts new data into an **OPEN** rowgroup, which is in rowstore format, and is also sometimes referred to as a delta table.  Once an open rowgroup is full, its state changes to **CLOSED**. A closed rowgroup is compressed into columnstore format by the tuple mover and the state changes to **COMPRESSED**.  The tuple mover is a background process that periodically wakes up and checks whether there are any closed rowgroups that are ready to compress into a columnstore rowgroup.  The tuple mover also deallocates any rowgroups in which every row has been deleted. Deallocated rowgroups are marked as **RETIRED**. To run tuple mover immediately, use the **REORGANIZE** option of the **ALTER INDEX** statement.  
  
 When a columnstore row group has filled, it is compressed, and stops accepting new rows. When rows are deleted from a compressed group, they remain but are marked as deleted. Updates to a compressed group are implemented as a delete from the compressed group, and an insert to an open group.  
  
## Permissions  
 Requires **VIEW SERVER STATE** permission.  
  
## Examples: [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
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

The following [!INCLUDE[ssSDW_md](../../includes/sssdw-md.md)] example counts the rows per partition for clustered column stores as well as how many rows are in Open, Closed, or Compressed Row groups:  

```
SELECT
    s.name AS [Schema Name]
    ,t.name AS [Table Name]
    ,rg.partition_number AS [Partition Number]
    ,SUM(rg.total_rows) AS [Total Rows]
    ,SUM(CASE WHEN rg.State = 1 THEN rg.Total_rows Else 0 END) AS [Rows in OPEN Row Groups]
    ,SUM(CASE WHEN rg.State = 2 THEN rg.Total_Rows ELSE 0 END) AS [Rows in Closed Row Groups]
    ,SUM(CASE WHEN rg.State = 3 THEN rg.Total_Rows ELSE 0 END) AS [Rows in COMPRESSED Row Groups]
FROM sys.pdw_nodes_column_store_row_groups rg
JOIN sys.pdw_nodes_tables pt
ON rg.object_id = pt.object_id AND rg.pdw_node_id = pt.pdw_node_id AND pt.distribution_id = rg.distribution_id
JOIN sys.pdw_table_mappings tm
ON pt.name = tm.physical_name
INNER JOIN sys.tables t
ON tm.object_id = t.object_id
INNER JOIN sys.schemas s
ON t.schema_id = s.schema_id
GROUP BY s.name, t.name, rg.partition_number
ORDER BY 1, 2
```
  
## See Also  
 [SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)   
 [CREATE COLUMNSTORE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-columnstore-index-transact-sql.md)   
 [sys.pdw_nodes_column_store_segments &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-nodes-column-store-segments-transact-sql.md)   
 [sys.pdw_nodes_column_store_dictionaries &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-nodes-column-store-dictionaries-transact-sql.md)  
  
  
