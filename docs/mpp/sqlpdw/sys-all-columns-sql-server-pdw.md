---
title: "sys.all_columns (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 6b314b64-a362-4c52-a821-775f94f13c09
caps.latest.revision: 13
author: BarbKess
---
# sys.all_columns (SQL Server PDW)
Shows columns for both user-defined and system objects.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|object_id|**int**|ID of the object to which this column belongs.||  
|name|**sysname**|Name of the column. Unique in object.||  
|column_id|**int**|ID of the column. Unique in object.||  
|system_type_id|**tinyint**|ID of the system type of the column.||  
|user_type_id|**int**|ID of the type of the column as defined by the user.||  
|max_length|**smallint**|Maximum length (in bytes) of the column.||  
|precision|**tinyint**|Precision of the column if numeric-based; otherwise, 0.||  
|scale|**tinyint**|Scale of column if numeric-based; otherwise, 0.||  
|collation_name|**sysname**|Name of the collation of the column if character-based; otherwise NULL.||  
|is_nullable|**bit**|1 = Column is nullable.||  
|is_ansi_padded|**bit**|1 = Column uses ANSI_PADDING ON behavior if character, binary, or variant.|Always 0.|  
|is_rowguidcol|**bit**|1 = Column is a declared ROWGUIDCOL.|Always 0.|  
|is_identity|**bit**|1 = Column has identity values.|Always 0.|  
|is_computed|**bit**|1 = Column is a computed column.|Always 0.|  
|is_filestream|**bit**|1 = Column is a FILESTREAM column.|Always 0.|  
|is_replicated|**bit**|1 = Column is replicated.|Always 0.|  
|is_non_sql_subscribed|**bit**|1 = Column has a non-SQL subscriber.|Always 0.|  
|is_merge_published|**bit**|1 = Column is merge-published.|Always 0.|  
|is_dts_replicated|**bit**|1 = Column is replicated by using SSIS.|Always 0.|  
|is_xml_document|**bit**|1 = Content is a complete XML document.|Always 0.|  
|xml_collection_id|**int**|0 = No XML schema collection.|Always 0.|  
|default_object_id|**int**|ID of the default object, 0 = No default.|Always 0.|  
|rule_object_id|**int**|ID of the stand-alone rule bound to the column; 0 = No stand-alone rule.|Always 0.|  
|is_sparse|**bit**|1 = Column is a sparse column.|Always 0.|  
|is_column_set|**bit**|1 = Column is a column set.|Always 0.|  
|distribution_ordinal|**tinyint**|Ordinal (1-based) within set of distribution.<br /><br />0 = Not a distribution column.|1 = This is the distribution column. 0 = This is not the distribution column.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SQL Server Catalog Views &#40;SQL Server PDW&#41;](../sqlpdw/sql-server-catalog-views-sql-server-pdw.md)  
[sys.columns &#40;SQL Server PDW&#41;](../sqlpdw/sys-columns-sql-server-pdw.md)  
  
