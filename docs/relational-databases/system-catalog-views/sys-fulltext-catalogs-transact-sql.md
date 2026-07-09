---
title: sys.fulltext_catalogs (Transact-SQL)
description: sys.fulltext_catalogs contains a row for each full-text catalog.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/09/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.fulltext_catalogs_TSQL"
  - "sys.fulltext_catalogs"
  - "fulltext_catalogs_TSQL"
  - "fulltext_catalogs"
helpviewer_keywords:
  - "sys.fulltext_catalogs catalog view"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# sys.fulltext_catalogs (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Contains a row for each full-text catalog.

> [!NOTE]  
> The following columns will be removed in a future release of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]: `data_space_id`, `file_id`, and `path`. Don't use these columns in new development work, and modify applications that currently use any of these columns as soon as possible.

| Column name | Data type | Description |
| --- | --- | --- |
| `fulltext_catalog_id` | **int** | ID of the full-text catalog. Unique across the full-text catalogs in the database. |
| `name` | **sysname** | Name of the catalog. Unique within the database. |
| `path` | **nvarchar(260)** | Name of the catalog directory in the file system. |
| `is_default` | **bit** | The default full-text catalog.<br /><br />True = Default.<br /><br />False = Not default. |
| `is_accent_sensitivity_on` | **bit** | Accent-sensitivity setting of the catalog.<br /><br />True = Accent-sensitive.<br /><br />False = Not accent-sensitive. |
| `data_space_id` | **int** | Filegroup where this catalog was created. |
| `file_id` | **int** | File ID of the full-text file associated with the catalog. |
| `principal_id` | **int** | ID of the database principal that owns the full-text catalog. |
| `is_importing` | **bit** | Indicates whether the full-text catalog is being imported:<br /><br />0 = The catalog is being imported.<br /><br />1 = The catalog isn't being imported. |

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)]

## Related content

- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [CREATE FULLTEXT CATALOG (Transact-SQL)](../../t-sql/statements/create-fulltext-catalog-transact-sql.md)
- [ALTER FULLTEXT CATALOG (Transact-SQL)](../../t-sql/statements/alter-fulltext-catalog-transact-sql.md)
- [DROP FULLTEXT CATALOG (Transact-SQL)](../../t-sql/statements/drop-fulltext-catalog-transact-sql.md)
