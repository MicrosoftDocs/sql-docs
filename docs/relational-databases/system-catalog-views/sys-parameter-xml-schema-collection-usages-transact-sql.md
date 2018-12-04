---
title: "sys.parameter_xml_schema_collection_usages (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.parameter_xml_schema_collection_usages"
  - "parameter_xml_schema_collection_usages"
  - "parameter_xml_schema_collection_usages_TSQL"
  - "sys.parameter_xml_schema_collection_usages_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.parameter_xml_schema_collection_usages catalog view"
ms.assetid: bffb91a3-492c-4375-bd2a-db8fc1a3ace4
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.parameter_xml_schema_collection_usages (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns a row for each parameter that is validated by an XML schema.  
  
 |Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|The ID of the object to which this parameter belongs.|  
|**parameter_id**|**int**|The ID of the parameter.  Is unique within the object.|  
|**xml_collection_id**|**int**|The ID of the XML schema collection that contains the validating XML schema namespace of the parameter.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [XML Schemas &#40;XML Type System&#41; Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/xml-schemas-xml-type-system-catalog-views-transact-sql.md)  
  
  
