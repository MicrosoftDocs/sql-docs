---
title: sys.registered_search_property_lists (Transact-SQL)
description: sys.registered_search_property_lists contains a row for each search property list on the current database.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/09/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.registered_search_property_lists_TSQL"
  - "sys.registered_search_property_lists"
  - "registered_search_property_lists_TSQL"
  - "registered_search_property_lists"
helpviewer_keywords:
  - "sys.registered_search_property_lists catalog view"
  - "full-text search [SQL Server], search property lists"
  - "search property lists [SQL Server], viewing"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.registered_search_property_lists (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Contains a row for each search property list on the current database.

| Column name | Data type | Description |
| --- | --- | --- |
| `property_list_id` | **int** | ID of the property list. |
| `name` | **sysname** | Name of the property list. |
| `create_date` | **datetime** | Date the property list was created. |
| `modify_date` | **datetime** | Date the property list was last modified by any `ALTER` statement. |
| `principal_id` | **int** | Owner of the property list. |

## Remarks

For more information, see [Search Document Properties with Search Property Lists](../search/search-document-properties-with-search-property-lists.md).

## Permissions

Visibility of the metadata in search property lists is limited to those that you either own or on which you have been granted some `REFERENCE` permission.

The search property list owner can grant `REFERENCE` or `CONTROL` permissions on the list. Users with `CONTROL` permission can also grant `REFERENCE` permission to other users.

## Examples

The following example displays the ID and name of the search property lists in the [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.

```sql
USE AdventureWorks2025;
GO

SELECT property_list_id,
       name
FROM sys.registered_search_property_lists;
GO
```

## Related content

- [ALTER FULLTEXT INDEX (Transact-SQL)](../../t-sql/statements/alter-fulltext-index-transact-sql.md)
- [sys.fulltext_indexes (Transact-SQL)](sys-fulltext-indexes-transact-sql.md)
