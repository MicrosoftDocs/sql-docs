---
title: "sys.xml_schema_wildcards (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.xml_schema_wildcards"
  - "sys.xml_schema_wildcards_TSQL"
  - "xml_schema_wildcards"
  - "xml_schema_wildcards_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.xml_schema_wildcards catalog view"
ms.assetid: 7cedfe9a-e99e-4777-8a28-98674b6e5cff
author: MightyPen
ms.author: genemi
manager: craigg
---
# sys.xml_schema_wildcards (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a row per XML schema component that is an Attribute-Wildcard (**kind** of **V**) or Element-Wildcard (**kind** of **W**), both with **symbol_space** of **N**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<inherited columns>**||Inherits columns from [sys.xml_schema_components](../../relational-databases/system-catalog-views/sys-xml-schema-components-transact-sql.md).|  
|**process_content**|**char(1)**|Indicates how contents are processed.<br /><br /> S = Strict validation (must validate)<br /><br /> L = Lax validation (validate if possible)<br /><br /> P = Skip validation|  
|**process_content_desc**|**nvarchar(60)**|Description of how contents are processed:<br /><br /> **STRICT_VALIDATION**<br /><br /> **LAX_VALIDATION**<br /><br /> **SKIP_VALIDATION**|  
|**disallow_namespaces**|**bit**|0 = Namespaces enumerated in [sys.xml_schema_wildcard_namespaces](../../relational-databases/system-catalog-views/sys-xml-schema-wildcard-namespaces-transact-sql.md) are the only ones allowed.<br /><br /> 1 = Namespaces are the only ones disallowed.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [XML Schemas &#40;XML Type System&#41; Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/xml-schemas-xml-type-system-catalog-views-transact-sql.md)  
  
  
