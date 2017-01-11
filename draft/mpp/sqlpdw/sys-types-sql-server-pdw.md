---
title: "sys.types (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5b1ae40f-c512-48ca-8b5d-57508e464d8c
caps.latest.revision: 10
author: BarbKess
---
# sys.types (SQL Server PDW)
Contains a row for each system type.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|name|**sysname**|Name of the type. Is unique within the schema.||  
|system_type_id|**tinyint**|ID of the internal system-type of the type.||  
|user_type_id|**int**|ID of the type. Is unique within the database. For system data types, see user_type_id = system_type_id.||  
|schema_id|**int**|ID of the schema to which the type belongs.||  
|principal_id|**int**|ID of the individual owner if different from schema owner. By default, schema-contained objects are owned by the schema owner. However, an alternate owner can be specified by using the ALTER AUTHORIZATION statement to change ownership.<br /><br />NULL if there is no alternate individual owner.||  
|max_length|**smallint**|Maximum length (in bytes) of the type.||  
|precision|**tinyint**|Maximum precision of the type if it is numeric-based; otherwise, 0.||  
|scale|**tinyint**|Maximum scale of the type if it is numeric-based; otherwise, 0.||  
|collation_name|**sysname**|Name of the collation of the type if it is character-based; otherwise, NULL.||  
|is_nullable|**bit**|Type is nullable.||  
|is_user_defined|**bit**|1 = User-defined type<br /><br />0 = SQL Server system data type|Always 0.|  
|is_assembly_type|**bit**|1 = Implementation of the type is defined in a CLR assembly.|Always 0.|  
|default_object_id|**int**|0 = No default exists.|Always 0.|  
|rule_object_id|**int**|0 = No rule exists.|Always 0.|  
|is_table_type|**bit**|Indicates that the type is a table.|Always 0.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
