---
title: "sys.message_type_xml_schema_collection_usages (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "message_type_xml_schema_collection_usages_TSQL"
  - "sys.message_type_xml_schema_collection_usages_TSQL"
  - "sys.message_type_xml_schema_collection_usages"
  - "message_type_xml_schema_collection_usages"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.message_type_xml_schema_collection_usages catalog view"
ms.assetid: 544f61a1-c7b7-44b4-bf8d-980ba87d0665
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.message_type_xml_schema_collection_usages (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  This catalog view returns a row for each service message type that is validated by an XML schema collection.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**message_type_id**|**int**|The ID of the service message type. Not NULLABLE.|  
|**xml_collection_id**|**int**|The ID of the collection containing the validating XML schema namespace. Not NULLABLE.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
  
