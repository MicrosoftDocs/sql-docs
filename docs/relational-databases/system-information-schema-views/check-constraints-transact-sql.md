---
title: CHECK_CONSTRAINTS (Transact-SQL)
description: "CHECK_CONSTRAINTS (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "CHECK_CONSTRAINTS"
  - "CHECK_CONSTRAINTS_TSQL"
helpviewer_keywords:
  - "CHECK_CONSTRAINTS view"
  - "INFORMATION_SCHEMA.CHECK_CONSTRAINTS view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# CHECK_CONSTRAINTS (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Returns one row for each CHECK constraint in the current database. This information schema view returns information about the objects to which the current user has permissions.  

To retrieve information from these views, specify the fully qualified name of **INFORMATION_SCHEMA**.*view_name*.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**CONSTRAINT_CATALOG**|**nvarchar(**128**)**|Constraint qualifier.|  
|**CONSTRAINT_SCHEMA**|**nvarchar(**128**)**|Name of the schema to which the constraint belongs.<br /><br /> **Important:** Don't use INFORMATION_SCHEMA views to determine the schema of an object. INFORMATION_SCHEMA views only represent a subset of the metadata of an object. The only reliable way to find the schema of an object is to query the `sys.objects` catalog view.|  
|**CONSTRAINT_NAME**|**sysname**|Constraint name.|  
|**CHECK_CLAUSE**|**nvarchar(**4000**)**|Actual text of the [!INCLUDE[tsql](../../includes/tsql-md.md)] definition statement.|  
  
## Related content

- [Transact-SQL reference (Database Engine)](../../t-sql/language-reference.md)
- [System information schema views (Transact-SQL)](system-information-schema-views-transact-sql.md)
- [sys.check_constraints (Transact-SQL)](../system-catalog-views/sys-check-constraints-transact-sql.md)
- [sys.objects (Transact-SQL)](../system-catalog-views/sys-objects-transact-sql.md)
