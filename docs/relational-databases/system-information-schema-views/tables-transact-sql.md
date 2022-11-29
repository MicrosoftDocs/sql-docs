---
title: TABLES (Transact-SQL)
description: "TABLES (Transact-SQL)"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "TABLES_TSQL"
  - "TABLES"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "TABLES view"
  - "INFORMATION_SCHEMA.TABLES view"
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: ""
ms.date: "05/20/2019"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# TABLES (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Returns one row for each table or view in the current database for which the current user has permissions.  
  
To retrieve information from these views, specify the fully qualified name of **INFORMATION_SCHEMA**.*view_name*.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**TABLE_CATALOG**|**nvarchar(**128**)**|Table qualifier.|  
|**TABLE_SCHEMA**|**nvarchar(**128**)**|Name of schema that contains the table.<br /><br /> **Important:** The only reliable way to find the schema of an object is to query the `sys.objects` catalog view. INFORMATION_SCHEMA views could be incomplete since they are not updated for all new features.|  
|**TABLE_NAME**|**sysname**|Table or view name.|  
|**TABLE_TYPE**|**varchar(**10**)**|Type of table. Can be VIEW or BASE TABLE.|  
  
## See Also  
 [System Views &#40;Transact-SQL&#41;](../../t-sql/language-reference.md)   
 [Information Schema Views &#40;Transact-SQL&#41;](~/relational-databases/system-information-schema-views/system-information-schema-views-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.tables &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-tables-transact-sql.md)  
  
