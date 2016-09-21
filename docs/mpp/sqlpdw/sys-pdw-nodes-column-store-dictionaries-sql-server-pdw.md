---
title: "sys.pdw_nodes_column_store_dictionaries (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 14aaa1d8-7b81-4dc8-a26e-c4e39718593c
caps.latest.revision: 9
author: BarbKess
---
# sys.pdw_nodes_column_store_dictionaries (SQL Server PDW)
Contains a row for each dictionary used in columnstore indexes. Dictionaries are used to encode some, but not all data types, therefore not all columns in a columnstore index have dictionaries. A dictionary can exist as a primary dictionary (for all segments) and possibly for other secondary dictionaries used for a subset of the column's segments.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**partition_id**|**bigint**|Indicates the partition ID. Is unique within a database.|  
|**hobt_id**|**bigint**|ID of the heap or B-tree index (hobt) for the table that has this columnstore index.|  
|**column_id**|**int**|ID of the columnstore column.|  
|**dictionary_id**|**int**|Id of the dictionary.|  
|**version**|**int**|Version of the dictionary format.|  
|**type**|**int**|Dictionary type:<br /><br />1 – Hash dictionary containing **int** values<br /><br />2 – Not used<br /><br />3 – Hash dictionary containing string values<br /><br />4 – Hash dictionary containing **float** values|  
|**last_id**|**int**|The last data id in the dictionary.|  
|**entry_count**|**bigint**|Number of entries in the dictionary.|  
|**on_disc_size**|**bigint**|Size of dictionary in bytes.|  
|**pdw_node_id**|**int**|Unique identifier of a SQL Server PDW node.|  
  
## Permissions  
Requires VIEW SERVER STATE permission.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
[Clustered Columnstore Indexes &#40;SQL Server PDW&#41;](../sqlpdw/clustered-columnstore-indexes-sql-server-pdw.md)  
[CREATE COLUMNSTORE INDEX &#40;SQL Server PDW&#41;](../sqlpdw/create-columnstore-index-sql-server-pdw.md)  
[sys.pdw_nodes_column_store_segments &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-nodes-column-store-segments-sql-server-pdw.md)  
[sys.pdw_nodes_column_store_row_groups &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-nodes-column-store-row-groups-sql-server-pdw.md)  
  
