---
title: sys.fulltext_index_catalog_usages (Transact-SQL)
description: sys.fulltext_index_catalog_usages returns a row for each full-text catalog to full-text index reference.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/09/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.fulltext_index_catalog_usages_TSQL"
  - "sys.fulltext_index_catalog_usages"
  - "fulltext_index_catalog_usages_TSQL"
  - "fulltext_index_catalog_usages"
helpviewer_keywords:
  - "sys.fulltext_index_catalog_usages catalog view"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.fulltext_index_catalog_usages (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Returns a row for each full-text catalog to full-text index reference.

| Column name | Data type | Description |
| --- | --- | --- |
| `object_id` | **int** | ID of the full-text indexed table. Unique within the database. |
| `index_id` | **int** | ID of full-text index. |
| `fulltext_catalog_id` | **int** | ID of full-text catalog. |

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)]

## Related content

- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Data Spaces (Transact-SQL)](data-spaces-transact-sql.md)
