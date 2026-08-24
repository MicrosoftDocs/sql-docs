---
title: "KEY_COLUMN_USAGE (Transact-SQL)"
description: "KEY_COLUMN_USAGE (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "KEY_COLUMN_USAGE_TSQL"
  - "KEY_COLUMN_USAGE"
helpviewer_keywords:
  - "INFORMATION_SCHEMA.KEY_COLUMN_USAGE view"
  - "KEY_COLUMN_USAGE view"
dev_langs:
  - "TSQL"
---
# KEY_COLUMN_USAGE (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Returns one row for each column that is constrained as a key in the current database. This information schema view returns information about the objects to which the current user has permissions.  
  
 To retrieve information from these views, specify the fully qualified name of **INFORMATION_SCHEMA**.*view_name*.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**CONSTRAINT_CATALOG**|**nvarchar(**128**)**|Constraint qualifier.|  
|**CONSTRAINT_SCHEMA**|**nvarchar(**128**)**|Name of schema that contains the constraint.<br /><br /> **Important:** Don't use INFORMATION_SCHEMA views to determine the schema of an object. INFORMATION_SCHEMA views only represent a subset of the metadata of an object. The only reliable way to find the schema of an object is to query the `sys.objects` catalog view.|  
|**CONSTRAINT_NAME**|**nvarchar(**128**)**|Constraint name.|  
|**TABLE_CATALOG**|**nvarchar(**128**)**|Table qualifier.|  
|**TABLE_SCHEMA**|**nvarchar(**128**)**|Name of schema that contains the table.<br /><br /> **Important:** Don't use INFORMATION_SCHEMA views to determine the schema of an object. INFORMATION_SCHEMA views only represent a subset of the metadata of an object. The only reliable way to find the schema of an object is to query the `sys.objects` catalog view.|  
|**TABLE_NAME**|**nvarchar(**128**)**|Table name.|  
|**COLUMN_NAME**|**nvarchar(**128**)**|Column name.|  
|**ORDINAL_POSITION**|**int**|Column ordinal position.|  
  
## Related content

- [Transact-SQL reference (Database Engine)](../../t-sql/language-reference.md)
- [System information schema views (Transact-SQL)](system-information-schema-views-transact-sql.md)
- [sys.columns (Transact-SQL)](../system-catalog-views/sys-columns-transact-sql.md)
- [sys.indexes (Transact-SQL)](../system-catalog-views/sys-indexes-transact-sql.md)
- [sys.objects (Transact-SQL)](../system-catalog-views/sys-objects-transact-sql.md)
- [sys.foreign_keys (Transact-SQL)](../system-catalog-views/sys-foreign-keys-transact-sql.md)
- [sys.key_constraints (Transact-SQL)](../system-catalog-views/sys-key-constraints-transact-sql.md)
