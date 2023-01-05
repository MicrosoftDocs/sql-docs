---
title: "sys.schemas (Transact-SQL)"
description: Schemas Catalog Views - sys.schemas
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "schemas_TSQL"
  - "sys.schemas_TSQL"
  - "schemas"
  - "sys.schemas"
helpviewer_keywords:
  - "sys.schemas catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 29af5ce5-2af7-4103-8f08-3ec92603ba05
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Schemas Catalog Views - sys.schemas
[!INCLUDE [sql-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdbmi-asa-pdw.md)]

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

Each schema has a an owner. The owner is a security [principal](../../relational-databases/security/authentication-access/principals-database-engine.md).
  
## Permissions  
 Requires membership in the **public** role.
  
## See Also  
[Principals](../../relational-databases/security/authentication-access/principals-database-engine.md)

[Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   

[Schemas Catalog Views &#40;Transact-SQL&#41;](./catalog-views-transact-sql.md)   

[sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)  
  
