---
title: CHECK_CONSTRAINTS (Transact-SQL)
description: "CHECK_CONSTRAINTS (Transact-SQL)"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "CHECK_CONSTRAINTS"
  - "CHECK_CONSTRAINTS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CHECK_CONSTRAINTS view"
  - "INFORMATION_SCHEMA.CHECK_CONSTRAINTS view"
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: ""
ms.date: "03/15/2017"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# CHECK_CONSTRAINTS (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Returns one row for each CHECK constraint in the current database. This information schema view returns information about the objects to which the current user has permissions.  

To retrieve information from these views, specify the fully qualified name of **INFORMATION_SCHEMA**.*view_name*.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**CONSTRAINT_CATALOG**|**nvarchar(**128**)**|Constraint qualifier.|  
|**CONSTRAINT_SCHEMA**|**nvarchar(**128**)**|Name of the schema to which the constraint belongs.<br /><br /> **Important:** Don't use INFORMATION_SCHEMA views to determine the schema of an object. INFORMATION_SCHEMA views only represent a subset of the metadata of an object. The only reliable way to find the schema of an object is to query the `sys.objects` catalog view.|  
|**CONSTRAINT_NAME**|**sysname**|Constraint name.|  
|**CHECK_CLAUSE**|**nvarchar(**4000**)**|Actual text of the [!INCLUDE[tsql](../../includes/tsql-md.md)] definition statement.|  
  
## See Also

- [System Views &#40;Transact-SQL&#41;](../../t-sql/language-reference.md)   
- [Information Schema Views &#40;Transact-SQL&#41;](~/relational-databases/system-information-schema-views/system-information-schema-views-transact-sql.md)
- [sys.check_constraints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-check-constraints-transact-sql.md)
- [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)