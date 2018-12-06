---
title: "sys.schemas (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "schemas_TSQL"
  - "sys.schemas_TSQL"
  - "schemas"
  - "sys.schemas"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.schemas catalog view"
ms.assetid: 29af5ce5-2af7-4103-8f08-3ec92603ba05
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Schemas Catalog Views - sys.schemas
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md)]

  Contains a row for each database schema.  
  
> [!NOTE]  
>  Database schemas are different from XML schemas, which are used to define the content model of XML documents.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the schema. Is unique within the database.|  
|**schema_id**|**int**|ID of the schema. Is unique within the database.|  
|**principal_id**|**int**|ID of the principal that owns this schema.|  
  
## Remarks  
 Database schemas act as namespaces or containers for objects, such as tables, views, procedures, and functions, that can be found in the **sys.objects** catalog view.  
  
## Permissions  
 Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Schemas Catalog Views &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/c516fb1c-b6ed-48ae-99c7-a78bc4336c8e)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)  
  
  
