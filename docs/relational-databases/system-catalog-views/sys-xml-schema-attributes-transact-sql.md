---
title: "sys.xml_schema_attributes (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "xml_schema_attributes_TSQL"
  - "xml_schema_attributes"
  - "sys.xml_schema_attributes_TSQL"
  - "sys.xml_schema_attributes"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.xml_schema_attributes catalog view"
ms.assetid: dd0c98aa-5e72-4df6-96d9-482786c8dbb1
author: MightyPen
ms.author: genemi
manager: craigg
---
# sys.xml_schema_attributes (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a row per XML schema component that is an attribute, **symbol_space** of **A**.  

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<inherited columns>**|--|Inherits from [sys.xml_schema_components](../../relational-databases/system-catalog-views/sys-xml-schema-components-transact-sql.md).|  
|**is_default_fixed**|**bit**|1 = The default value is a fixed value. This value cannot be overridden in an XML instance.<br /><br /> 0 = The default value is not a fixed value for the attribute. (default)|  
|**must_be_qualified**|**bit**|1 = The attribute must be explicitly namespace qualified.<br /><br /> 0 = The attribute may be implicitly namespace qualified. (default)|  
|**default_value**|**nvarchar**<br /><br /> **(4000)**|Default value of the attribute. Is NULL if a default value is not supplied.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [XML Schemas &#40;XML Type System&#41; Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/xml-schemas-xml-type-system-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
