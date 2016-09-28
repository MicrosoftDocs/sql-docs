---
title: "sys.index_columns (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 799eb689-50e0-427d-852b-5afccc2ab12d
caps.latest.revision: 13
author: BarbKess
---
# sys.index_columns (SQL Server PDW)
Contains one row per column that is part of a [sys.indexes](../sqlpdw/sys-indexes-sql-server-pdw.md) index or unordered table.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|object_id|**int**|id of the object on which the index is defined.||  
|index_id|**int**|id of the index in which the column is defined.||  
|index_column_id|**int**|id of the index column. index_column_id is unique only within index_id.||  
|column_id|**int**|id of the column in object_id.<br /><br />column_id is unique only within object_id.<br /><br />0 = Row Identifier (RID) in a nonclustered index.||  
|key_ordinal|**tinyint**|Ordinal (1-based) within set of key columns.<br /><br />0 = Not a key column.|Always > 0 for columns in rowstore indexes because all indexed columns are key columns.<br /><br />Always 0 for columns in clustered columnstore index columns because all columns are included columns.|  
|partition_ordinal|**tinyint**|1 = partition column.<br /><br />0 = Not the partition column.||  
|is_descending_key|**bit**|1 = rowstore index column is sorted in descending order.<br /><br />0 = rowstore index column is sorted in ascending order.<br /><br />0 = column is in a clustered columnstore index.||  
|is_included_column|**bit**|0 = Column is not an included column.<br /><br />1 = Column is an included column.|Always 0 for rowstore index columns because all indexed columns are key columns.<br /><br />Always 1 for clustered columnstore index columns because all columns are included columns.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
