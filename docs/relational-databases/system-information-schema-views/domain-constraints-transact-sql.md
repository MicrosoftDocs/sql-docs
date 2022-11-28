---
description: "DOMAIN_CONSTRAINTS (Transact-SQL)"
title: "DOMAIN_CONSTRAINTS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "DOMAIN_CONSTRAINTS"
  - "DOMAIN_CONSTRAINTS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "INFORMATION_SCHEMA.DOMAIN_CONSTRAINTS view"
  - "DOMAIN_CONSTRAINTS view"
ms.assetid: 436c4480-f1e3-403f-b2bd-de04539afe3c
author: markingmyname
ms.author: maghan
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DOMAIN_CONSTRAINTS (Transact-SQL)
[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

  Returns one row for each alias data type in the current database that has a rule bound to it and that can be accessed by current user.  
  
 To retrieve information from these views, specify the fully qualified name of **INFORMATION_SCHEMA**.*view_name*.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**CONSTRAINT_CATALOG**|**nvarchar(**128**)**|Database in which the rule exists.|  
|**CONSTRAINT_SCHEMA**|**nvarchar(128)**|Name of schema that contains the constraint.<br /><br /> **Important:** Don't use INFORMATION_SCHEMA views to determine the schema of an object. INFORMATION_SCHEMA views only represent a subset of the metadata of an object. The only reliable way to find the schema of an object is to query the `sys.objects` catalog view.|  
|**CONSTRAINT_NAME**|**sysname**|Rule name.|  
|**DOMAIN_CATALOG**|**nvarchar(**128**)**|Database in which the alias data type exists.|  
|**DOMAIN_SCHEMA**|**nvarchar(**128**)**|Name of schema that contains the alias data type.<br /><br /> **Important:** Don't use INFORMATION_SCHEMA views to determine the schema of a data type. The only reliable way to find the schema of a type is to use the TYPEPROPERTY function.|  
|**DOMAIN_NAME**|**sysname**|Alias data type.|  
|**IS_DEFERRABLE**|**varchar(**2**)**|Specifies whether constraint checking is deferrable. Always returns NO.|  
|**INITIALLY_DEFERRED**|**varchar(**2**)**|Specifies whether constraint checking is at first deferred. Always returns NO.|  
  
## See Also  
 [System Views &#40;Transact-SQL&#41;](../../t-sql/language-reference.md)   
 [Information Schema Views &#40;Transact-SQL&#41;](~/relational-databases/system-information-schema-views/system-information-schema-views-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.types &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-types-transact-sql.md)  
  
