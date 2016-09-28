---
title: "sys.pdw_nodes_indexes (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c79c6290-7330-4341-b5a1-3c60cf7675c0
caps.latest.revision: 6
author: BarbKess
---
# sys.pdw_nodes_indexes (SQL Server PDW)
Returns indexes for SQL Server PDW.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|object_id|**int**|id of the object to which this index belongs.||  
|name|**sysname**|Name of the index. Name is unique only within the object. NULL = Heap||  
|index_id|**int**|id of the index. index_id is unique only within the object.<br /><br />0 = Heap<br /><br />1 = Clustered index<br /><br />> 1 = Non-clustered index||  
|type|**tinyint**|Type of index:<br /><br />0 = Heap<br /><br />1 = Clustered<br /><br />2 = Non-clustered<br /><br />5 = Clustered xVelocity memory optimized columnstore index|  
|type_desc|**nvarchar(60)**|Description of index type:<br /><br />HEAP<br /><br />CLUSTERED<br /><br />NONCLUSTERED<br /><br />CLUSTERED   COLUMNSTORE||  
|is_unique|**bit**|0 = Index is not unique.|Always 0.|  
|data_space_id|**int**|id of the data space for this index. Data space is either a filegroup or partition scheme.<br /><br />0 = object_id is a table-valued function.||  
|ignore_dup_key|**bit**|0 = IGNORE_DUP_KEY is OFF.|Always 0.|  
|is_primary_key|**bit**|1 = Index is part of a PRIMARY KEY constraint.|Always 0.|  
|is_unique_constraint|**bit**|1 = Index is part of a UNIQUE constraint.|Always 0.|  
|fill_factor|**tinyint**|> 0 = FILLFACTOR percentage used when the index was created or rebuilt.<br /><br />0 = Default value|Always 0.|  
|is_padded|**bit**|0 = PADINDEX is OFF.|Always 0.|  
|is_disabled|**bit**|1 = Index is disabled.<br /><br />0 = Index is not disabled.||  
|is_hypothetical|**bit**|0 = Index is not hypothetical.|Always 0.|  
|allow_row_locks|**bit**|1 = Index allows row locks.|Always 1.|  
|allow_page_locks|**bit**|1 = Index allows page locks.|Always 1.|  
|has_filter|**bit**|0 = Index does not have a filter.|Always 0.|  
|filter_definition|**nvarchar(max)**|Expression for the subset of rows included in the filtered index.|Always NULL.|  
|pdw_node_id|**int**|Unique identifier of a SQL Server PDW node.|NOT NULL|  
  
## Permissions  
Requires CONTROL SERVER permission.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
