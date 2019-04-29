---
title: "sys.xml_schema_wildcard_namespaces (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "xml_schema_wildcard_namespaces_TSQL"
  - "xml_schema_wildcard_namespaces"
  - "sys.xml_schema_wildcard_namespaces_TSQL"
  - "sys.xml_schema_wildcard_namespaces"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.xml_schema_wildcard_namespaces catalog view"
ms.assetid: a3caa932-41c7-48a9-9b2d-ff090afbb66b
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# sys.xml_schema_wildcard_namespaces (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a row per enumerated namespace for an XML schema wildcard.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**xml_component_id**|**int**|ID of the XML schema component (wildcard) to which this applies.|  
|**namespace**|**nvarchar(4000)**|Name or URI of the namespace that is used by the XML wildcard.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [XML Schemas &#40;XML Type System&#41; Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/xml-schemas-xml-type-system-catalog-views-transact-sql.md)  
  
  
