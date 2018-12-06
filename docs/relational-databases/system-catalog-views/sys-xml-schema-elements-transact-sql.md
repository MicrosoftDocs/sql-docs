---
title: "sys.xml_schema_elements (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.xml_schema_elements"
  - "sys.xml_schema_elements_TSQL"
  - "xml_schema_elements"
  - "xml_schema_elements_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.xml_schema_elements catalog view"
ms.assetid: 190ed0cd-0c5e-4607-9db4-9e77cacf17d7
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# sys.xml_schema_elements (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a row per XML schema component that is a Type, **symbol_space** of **E**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<inherited columns>**|**--**|Inherits columns from [sys.xml_schema_components](../../relational-databases/system-catalog-views/sys-xml-schema-components-transact-sql.md).|  
|**is_default_fixed**|**bit**|1 = Default value is a fixed value. This value cannot be overridden in XML instance.<br /><br /> 0 = Default value is not a fixed value for the element. (default).|  
|**is_abstract**|**bit**|1 = Element is abstract and cannot be used in an instance document. A member of the substitution group of the element must appear in the instance document.<br /><br /> 0 = Element is not abstract. (default).|  
|**is_nillable**|**bit**|1 = Element is nillable.<br /><br /> 0 = Element is not nillable. (default)|  
|**must_be_qualified**|**bit**|1 = Element must be explicitly namespace qualified.<br /><br /> 0 = Element may be implicitly namespace qualified. (default)|  
|**is_extension_blocked**|**bit**|1 = Replacement with an instance of an extension type is blocked.<br /><br /> 0 = Replacement with extension type is allowed. (default)|  
|**is_restriction_blocked**|**bit**|1 = Replacement with an instance of a restriction type is blocked.<br /><br /> 0 = Replacement with restriction type is allowed. (default)|  
|**is_substitution_blocked**|**bit**|1 = Instance of a substitution group cannot be used.<br /><br /> 0 = Replacement with substitution group is permitted. (default)|  
|**is_final_extension**|**bit**|1 = Replacement with an instance of an extension type is disallowed.<br /><br /> 0 = Replacement in an instance of an extension type is allowed. (default)|  
|**is_final_restriction**|**bit**|1 = Replacement with an instance of a restriction type is disallowed.<br /><br /> 0 = Replacement in an instance of a restriction type is allowed. (default)|  
|**default_value**|**nvarchar (4000)**|Default value of the element. NULL if a default value is not supplied.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [XML Schemas &#40;XML Type System&#41; Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/xml-schemas-xml-type-system-catalog-views-transact-sql.md)  
  
  
