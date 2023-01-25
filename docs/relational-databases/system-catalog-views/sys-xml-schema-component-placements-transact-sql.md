---
title: "sys.xml_schema_component_placements (Transact-SQL)"
description: sys.xml_schema_component_placements (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.xml_schema_component_placements"
  - "xml_schema_component_placements_TSQL"
  - "xml_schema_component_placements"
  - "sys.xml_schema_component_placements_TSQL"
helpviewer_keywords:
  - "sys.xml_schema_component_placements catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 2d3c8828-e4b3-423d-bf11-990464c1341b
---
# sys.xml_schema_component_placements (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a row per placement for XML schema components.  
   
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**xml_component_id**|**int**|ID of the XML schema component that owns this placement.|  
|**placement_id**|**int**|ID of the placement. This is unique within the owning XML schema component.|  
|**placed_xml_component_id**|**int**|ID of the placed XML schema component.|  
|**is_default_fixed**|**bit**|1 = The default value is a fixed value. This value cannot be overridden in an XML instance.<br /><br /> 0 = The value can be overridden.(default)|  
|**min_occurrences**|**int**|Minimum number of placed component occurs.|  
|**max_occurrences**|**int**|Maximum number of placed component occurs.|  
|**default_value**|**nvarchar (4000)**|Default value if one is supplied. Is NULL if a default value is not supplied.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [XML Schemas &#40;XML Type System&#41; Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/xml-schemas-xml-type-system-catalog-views-transact-sql.md)  
  
  
