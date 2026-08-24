---
title: "COLUMN_DOMAIN_USAGE (Transact-SQL)"
description: "COLUMN_DOMAIN_USAGE (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "COLUMN_DOMAIN_USAGE_TSQL"
  - "COLUMN_DOMAIN_USAGE"
helpviewer_keywords:
  - "INFORMATION_SCHEMA.COLUMN_DOMAIN_USAGE view"
  - "COLUMN_DOMAIN_USAGE view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# COLUMN_DOMAIN_USAGE (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Returns one row for each column in the current database that has an alias data type. This information schema view returns information about the objects to which the current user has permissions.  
  
 To retrieve information from these views, specify the fully qualified name of **INFORMATION_SCHEMA**.*view_name*.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**DOMAIN_CATALOG**|**nvarchar(**128**)**|Database in which the alias data type exists.|  
|**DOMAIN_SCHEMA**|**nvarchar(**128**)**|Name of schema that contains the alias data type.<br /><br /> **Important:** Don't use INFORMATION_SCHEMA views to determine the schema of a data type. The only reliable way to find the schema of a type is to use the TYPEPROPERTY function.|  
|**DOMAIN_NAME**|**sysname**|Alias data type.|  
|**TABLE_CATALOG**|**nvarchar(**128**)**|Table qualifier.|  
|**TABLE_SCHEMA**|**nvarchar(**128**)**|Table owner.<br /><br /> **Important:** Don't use INFORMATION_SCHEMA views to determine the schema of an object. INFORMATION_SCHEMA views only represent a subset of the metadata of an object. The only reliable way to find the schema of an object is to query the `sys.objects` catalog view.|  
|**TABLE_NAME**|**sysname**|Table in which the alias  data type is used.|  
|**COLUMN_NAME**|**sysname**|Column using the alias data type.|  
  
## Related content

- [Transact-SQL reference (Database Engine)](../../t-sql/language-reference.md)
- [System information schema views (Transact-SQL)](system-information-schema-views-transact-sql.md)
- [sys.objects (Transact-SQL)](../system-catalog-views/sys-objects-transact-sql.md)
- [sys.types (Transact-SQL)](../system-catalog-views/sys-types-transact-sql.md)
