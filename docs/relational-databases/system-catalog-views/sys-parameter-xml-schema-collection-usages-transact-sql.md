---
title: "sys.parameter_xml_schema_collection_usages (Transact-SQL)"
description: sys.parameter_xml_schema_collection_usages (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.parameter_xml_schema_collection_usages"
  - "parameter_xml_schema_collection_usages"
  - "parameter_xml_schema_collection_usages_TSQL"
  - "sys.parameter_xml_schema_collection_usages_TSQL"
helpviewer_keywords:
  - "sys.parameter_xml_schema_collection_usages catalog view"
dev_langs:
  - "TSQL"
ms.assetid: bffb91a3-492c-4375-bd2a-db8fc1a3ace4
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.parameter_xml_schema_collection_usages (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

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
  
  
