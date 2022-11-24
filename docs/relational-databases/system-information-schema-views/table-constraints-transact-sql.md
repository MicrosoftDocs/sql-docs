---
description: "TABLE_CONSTRAINTS (Transact-SQL)"
title: "TABLE_CONSTRAINTS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "TABLE_CONSTRAINTS"
  - "TABLE_CONSTRAINTS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "INFORMATION_SCHEMA.TABLE_CONSTRAINTS view"
  - "TABLE_CONSTRAINTS view"
ms.assetid: 687f3284-2849-4853-8a5c-fc936deceae0
author: markingmyname
ms.author: maghan
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# TABLE_CONSTRAINTS (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns one row for each table constraint in the current database. This information schema view returns information about the objects to which the current user has permissions.  
  
 To retrieve information from these views, specify the fully qualified name of **INFORMATION_SCHEMA**.*view_name*.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**CONSTRAINT_CATALOG**|**nvarchar(**128**)**|Constraint qualifier.|  
|**CONSTRAINT_SCHEMA**|**nvarchar(**128**)**|Name of schema that contains the constraint.<br /><br /> **Important:** The only reliable way to find the schema of an object is to query the `sys.objects` catalog view.|  
|**CONSTRAINT_NAME**|**sysname**|Constraint name.|  
|**TABLE_CATALOG**|**nvarchar(**128**)**|Table qualifier.|  
|**TABLE_SCHEMA**|**nvarchar(**128**)**|Name of schema that contains the table.<br /><br /> **Important:** The only reliable way to find the schema of an object is to query the `sys.objects` catalog view.|  
|**TABLE_NAME**|**sysname**|Table name.|  
|**CONSTRAINT_TYPE**|**varchar(**11**)**|Type of constraint:<br /><br /> CHECK<br /><br /> UNIQUE<br /><br /> PRIMARY KEY<br /><br /> FOREIGN KEY|  
|**IS_DEFERRABLE**|**varchar(**2**)**|Specifies whether constraint checking is deferrable. Always returns NO.|  
|**INITIALLY_DEFERRED**|**varchar(**2**)**|Specifies whether constraint checking is at first deferred. Always returns NO.|  
  
## See Also  
 [System Views &#40;Transact-SQL&#41;](../../t-sql/language-reference.md)   
 [Information Schema Views &#40;Transact-SQL&#41;](~/relational-databases/system-information-schema-views/system-information-schema-views-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.key_constraints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-key-constraints-transact-sql.md)   
 [sys.check_constraints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-check-constraints-transact-sql.md)   
 [sys.tables &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-tables-transact-sql.md)  
  
