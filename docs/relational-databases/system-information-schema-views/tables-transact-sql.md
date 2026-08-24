---
title: TABLES (Transact-SQL)
description: "TABLES (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "05/20/2019"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "TABLES_TSQL"
  - "TABLES"
helpviewer_keywords:
  - "TABLES view"
  - "INFORMATION_SCHEMA.TABLES view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# TABLES (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Returns one row for each table or view in the current database for which the current user has permissions.  
  
To retrieve information from these views, specify the fully qualified name of **INFORMATION_SCHEMA**.*view_name*.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**TABLE_CATALOG**|**nvarchar(**128**)**|Table qualifier.|  
|**TABLE_SCHEMA**|**nvarchar(**128**)**|Name of schema that contains the table.<br /><br /> **Important:** The only reliable way to find the schema of an object is to query the `sys.objects` catalog view. INFORMATION_SCHEMA views could be incomplete since they are not updated for all new features.|  
|**TABLE_NAME**|**sysname**|Table or view name.|  
|**TABLE_TYPE**|**varchar(**10**)**|Type of table. Can be VIEW or BASE TABLE.|  
  
## Related content

- [Transact-SQL reference (Database Engine)](../../t-sql/language-reference.md)
- [System information schema views (Transact-SQL)](system-information-schema-views-transact-sql.md)
- [sys.objects (Transact-SQL)](../system-catalog-views/sys-objects-transact-sql.md)
- [sys.tables (Transact-SQL)](../system-catalog-views/sys-tables-transact-sql.md)
