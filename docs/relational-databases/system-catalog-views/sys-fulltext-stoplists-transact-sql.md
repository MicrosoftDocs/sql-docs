---
title: sys.fulltext_stoplists (Transact-SQL)
description: sys.fulltext_stoplists contains a row per full-text stoplist in the database.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/09/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.fulltext_stoplists_TSQL"
  - "sys.fulltext_stoplists"
  - "fulltext_stoplists_TSQL"
  - "fulltext_stoplists"
helpviewer_keywords:
  - "sys.fulltext_stoplists catalog view"
  - "stoplists [full-text search]"
  - "full-text search [SQL Server], stoplists"
  - "stopwords [full-text search]"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.fulltext_stoplists (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Contains a row per full-text stoplist in the database.

| Column name | Data type | Description |
| --- | --- | --- |
| `stoplist_id` | **int** | ID of the stoplist, unique within the database. |
| `name` | **sysname** | Name of the stoplist. |
| `create_date` | **datetime** | Date that stoplist was created. |
| `modify_date` | **datetime** | Date that stoplist was last modified using any `ALTER` statement. |
| `principal_id` | **int** | ID of the database principal that owns the stoplist. |

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)]

## Related content

- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Object catalog views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [sys.fulltext_system_stopwords (Transact-SQL)](sys-fulltext-system-stopwords-transact-sql.md)
- [sys.fulltext_stopwords (Transact-SQL)](sys-fulltext-stopwords-transact-sql.md)
- [Configure and manage stopwords and stoplists for Full-Text Search](../search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)
- [CREATE FULLTEXT STOPLIST (Transact-SQL)](../../t-sql/statements/create-fulltext-stoplist-transact-sql.md)
- [ALTER FULLTEXT STOPLIST (Transact-SQL)](../../t-sql/statements/alter-fulltext-stoplist-transact-sql.md)
- [DROP FULLTEXT STOPLIST (Transact-SQL)](../../t-sql/statements/drop-fulltext-stoplist-transact-sql.md)
