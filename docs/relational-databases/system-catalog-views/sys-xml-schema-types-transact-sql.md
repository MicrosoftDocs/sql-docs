---
title: "sys.xml_schema_types (Transact-SQL)"
description: sys.xml_schema_types (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.xml_schema_types_TSQL"
  - "xml_schema_types_TSQL"
  - "sys.xml_schema_types"
  - "xml_schema_types"
helpviewer_keywords:
  - "sys.xml_schema_types catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 441ba49d-f778-4fa1-98c4-ced375a01a34
---
# sys.xml_schema_types (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a row per XML schema component that is a Type, **symbol_space** of **T**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<inherited columns>**||Inherits columns from [sys.xml_schema_components](../../relational-databases/system-catalog-views/sys-xml-schema-components-transact-sql.md).|  
|**is_abstract**|**bit**|1 = Type is an abstract type. All instances of an element of this type must use **xsi:type** to indicate a derived type that is not abstract.<br /><br /> 0 = Type is not abstract. (default)|  
|**allows_mixed_content**|**bit**|1 = Mixed content is allowed<br /><br /> 0 = Mixed content is not allowed. (default)|  
|**is_extension_blocked**|**bit**|1 = Replacement with an extension of the type is blocked in instances when the block attribute on the **complexType** definition or the **blockDefault** attribute of the ancestor \<schema> element information item is set to "extension" or "#all".<br /><br /> 0 =Replacement with extension is not blocked.|  
|**is_restriction_blocked**|**bit**|1 = Replacement with a restriction of the type is blocked in instances when the block attribute on the **complexType** definition or the **blockDefault** attribute of the ancestor \<schema> element information item is set to "restriction" or "#all".<br /><br /> 0 = Replacement with restriction is not blocked. (default)|  
|**is_final_extension**|**bit**|1 = Derivation by extension of the type is blocked when the final attribute on the **complexType** definition or the **finalDefault** attribute of the ancestor \<schema> element information item is set to "extension" or "#all".<br /><br /> 0 = Extension is allowed. (default)|  
|**is_final_restriction**|**bit**|1 = Derivation by restriction of the type is blocked when the final attribute on the simple or **complexType** definition or the **finalDefault** attribute of the ancestor \<schema> element information item is set to "restriction" or "#all".<br /><br /> 0 = Restriction is allowed. (default)|  
|**is_final_list_member**|**bit**|1 = This simple type cannot be used as the item type in a list.<br /><br /> 0 = This type is a complex type, or it can be used as list item type. (default)|  
|**is_final_union_member**|**bit**|1 = This simple type cannot be used as the member type of a union type.<br /><br /> 0 = This type is a complex type. or it can be used as union member type. (default)|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [XML Schemas &#40;XML Type System&#41; Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/xml-schemas-xml-type-system-catalog-views-transact-sql.md)  
  
  
