---
title: "sys.pdw_nodes_column_store_segments (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 440f5ba8-8188-40ac-a06c-fe340d6d6366
caps.latest.revision: 8
author: BarbKess
---
# sys.pdw_nodes_column_store_segments (SQL Server PDW)
Contains a row for each column in a columnstore index.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**partition_id**|**bigint**|Indicates the partition ID. Is unique within a database.|  
|**hobt_id**|**bigint**|ID of the heap or B-tree index (hobt) for the table that has this columnstore index.|  
|**column_id**|**int**|ID of the columnstore column.|  
|**segment_id**|**int**|ID of the column segment.|  
|**version**|**int**|Version of the column segment format.|  
|**encoding_type**|**int**|Type of encoding used for that segment.|  
|**row_count**|**int**|Number of rows in the row group.|  
|**has_nulls**|**int**|1 if the column segment has null values.|  
|**base_id**|**bigint**|Base value id if encoding type 1 is being used.  If encoding type 1 is not being used, base_id is set to 1.|  
|**magnitude**|**float**|Magnitude if encoding type 1 is being used.  If encoding type 1 is not being used, magnitude is set to 1.|  
|**primary__dictionary_id**|**int**|Id of primary dictionary.|  
|**secondary_dictionary_id**|**int**|Id of secondary dictionary. Returns -1 if there is no secondary dictionary.|  
|**min_data_id**|**bigint**|Minimum data id in the column segment.|  
|**max_data_id**|**bigint**|Maximum data id in the column segment.|  
|**null_value**|**bigint**|Value used to represent nulls.|  
|**on_disk_size**|**bigint**|Size of segment in bytes.|  
|**pdw_node_id**|**int**|Unique identifier of a SQL Server PDW note.|  
  
## Remarks  
The following query returns information about segments of a columnstore index.  
  
```Transact-SQL  
SELECT i.name, p.object_id, p.index_id, i.type_desc,   
    COUNT(*) AS number_of_segments  
FROM sys.column_store_segments AS s   
INNER JOIN sys.partitions AS p   
    ON s.hobt_id = p.hobt_id   
INNER JOIN sys.indexes AS i   
    ON p.object_id = i.object_id  
WHERE i.type = 6  
GROUP BY i.name, p.object_id, p.index_id, i.type_desc ;  
```  
  
Join sys.pdw_nodes_column_store_segments with other system tables to determine the row count and on-disk size of the segments.  
  
```  
SELECT o.name, css.hobt_id, css. column_id, css.pdw_node_id, css.row_count, css.on_disk_size  
FROM sys.pdw_nodes_column_store_segments AS css  
JOIN sys.pdw_nodes_partitions AS pnp  
    ON css.partition_id = pnp.partition_id  
JOIN sys.pdw_nodes_tables AS part  
    ON pnp.object_id = part.object_id   
    AND pnp.pdw_node_id = part.pdw_node_id  
JOIN sys.pdw_table_mappings AS TMap  
    ON part.name = TMap.physical_name  
JOIN sys.objects AS o  
    ON TMap.object_id = o.object_id  
ORDER BY css.hobt_id, css.column_id;  
```  
  
## Permissions  
Requires **VIEW SERVER STATE** permission.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
[Clustered Columnstore Indexes &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/clustered-columnstore-indexes-sql-server-pdw.md)  
[CREATE COLUMNSTORE INDEX &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-columnstore-index-sql-server-pdw.md)  
[sys.pdw_nodes_column_store_row_groups &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-nodes-column-store-row-groups-sql-server-pdw.md)  
[sys.pdw_nodes_column_store_dictionaries &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-nodes-column-store-dictionaries-sql-server-pdw.md)  
  
