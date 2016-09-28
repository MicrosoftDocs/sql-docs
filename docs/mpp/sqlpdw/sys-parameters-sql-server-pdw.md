---
title: "sys.parameters (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 0f56274b-593d-42bb-8ab5-81f6930820b3
caps.latest.revision: 7
author: BarbKess
---
# sys.parameters (SQL Server PDW)
Contains a row for each parameter of a SQL Server PDW object that accepts parameters.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**object_id**|**int**|id of the object to which this parameter belongs.|  
|**name**|**sysname**|Name of the parameter. Is unique within the object.<br /><br />If the object is a scalar function, the parameter name is an empty string in the row representing the return value.|  
|**parameter_id**|**int**|id of the parameter. Is unique within the object.<br /><br />If the object is a scalar function, **parameter_id** = 0 represents the return value.|  
|**system_type_id**|**tinyint**|id of the system type of the parameter.|  
|**user_type_id**|**int**|id of the parameter type.<br /><br />To return the name of the type, join to the [sys.types &#40;SQL Server PDW&#41;](../sqlpdw/sys-types-sql-server-pdw.md) catalog view on this column.|  
|**max_length**|**smallint**|Maximum length of the parameter, in bytes.<br /><br />Value = -1 when the column data type is **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml**.|  
|**precision**|**tinyint**|Precision of the parameter if numeric-based; otherwise, 0.|  
|**scale**|**tinyint**|Scale of the parameter if numeric-based; otherwise, 0.|  
|**is_output**|**bit**|1 = Parameter is RETURN; otherwise, 0.|  
|**is_cursor_ref**|**bit**|Always 0.|  
|**has_default_value**|**bit**|Always 0.|  
|**is_xml_document**|**bit**|Always 0.|  
|**default_value**|**sql_variant**|Always NULL.|  
|**xml_collection_id**|**int**|Always 0.|  
|**is_readonly**|**int**|Always 0.|  
  
## Permissions  
Users can see information about objects for which they have some type of permission.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
[CREATE PROCEDURE &#40;SQL Server PDW&#41;](../sqlpdw/create-procedure-sql-server-pdw.md)  
  
