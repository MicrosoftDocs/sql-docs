---
title: "sys.xml_schema_namespaces (Transact-SQL)"
description: sys.xml_schema_namespaces (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.xml_schema_namespaces_TSQL"
  - "sys.xml_schema_namespaces"
  - "xml_schema_namespaces"
  - "xml_schema_namespaces_TSQL"
helpviewer_keywords:
  - "sys.xml_schema_namespaces catalog view"
dev_langs:
  - "TSQL"
---
# sys.xml_schema_namespaces (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a row per XSD-defined XML namespace. The following tuples are unique: **collection_id**, **namespace_id**, and **collection_id**, and **name**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**xml_collection_id**|**int**|ID of the XML schema collection that contains this namespace.|  
|**name**|**nvarchar(4000)**|Name of XML namespace. Blank **name** indicates no target namespace.|  
|**xml_namespace_id**|**int**|1-based ordinal that uniquely identifies the XML namespace in the database.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Related content

- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [XML Schemas (XML Type System) Catalog Views (Transact-SQL)](xml-schemas-xml-type-system-catalog-views-transact-sql.md)
