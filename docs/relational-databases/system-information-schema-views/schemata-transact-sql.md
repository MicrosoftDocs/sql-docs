---
description: "SCHEMATA (Transact-SQL)"
title: "SCHEMATA (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/08/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "SCHEMATA_TSQL"
  - "SCHEMATA"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "INFORMATION_SCHEMA.SCHEMATA view"
  - "SCHEMATA view"
ms.assetid: 69617642-0f54-4b25-b62f-5f39c8909601
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SCHEMATA (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns one row for each schema in the current database. To retrieve information from these views, specify the fully qualified name of **INFORMATION_SCHEMA**.*view_name*. To retrieve information about all databases in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], query the [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**CATALOG_NAME**|**sysname**|Name of current database|  
|**SCHEMA_NAME**|**nvarchar(**128**)**|Returns the name of the schema.|  
|**SCHEMA_OWNER**|**nvarchar(**128**)**|Schema owner name.<br /><br /> **Important:** Don't use INFORMATION_SCHEMA views to determine the schema of an object. INFORMATION_SCHEMA views only represent a subset of the metadata of an object. The only reliable way to find the schema of an object is to query the `sys.objects` catalog view.|  
|**DEFAULT_CHARACTER_SET_CATALOG**|**varchar(**6**)**|Always returns NULL.|  
|**DEFAULT_CHARACTER_SET_SCHEMA**|**varchar(**3**)**|Always returns NULL.|  
|**DEFAULT_CHARACTER_SET_NAME**|**sysname**|Returns the name of the default character set.|  

**Example**  
The following example, returns information about the schemas in the master database:  
```sql  
SELECT * FROM master.INFORMATION_SCHEMA.SCHEMATA;
```  

## See Also  
 [System Views &#40;Transact-SQL&#41;](../../t-sql/language-reference.md)   
 [Information Schema Views &#40;Transact-SQL&#41;](~/relational-databases/system-information-schema-views/system-information-schema-views-transact-sql.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 [sys.schemas &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/schemas-catalog-views-sys-schemas.md)   
 [sys.syscharsets &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-syscharsets-transact-sql.md)  
  
