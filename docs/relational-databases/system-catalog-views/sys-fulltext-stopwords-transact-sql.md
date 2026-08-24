---
title: sys.fulltext_stopwords (Transact-SQL)
description: sys.fulltext_stopwords contains a row per stopword for all stoplists in the database.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/09/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.fulltext_stopwords_TSQL"
  - "sys.fulltext_stopwords"
  - "fulltext_stopwords_TSQL"
  - "fulltext_stopwords"
helpviewer_keywords:
  - "sys.fulltext_stopwords catalog view"
  - "stoplists [full-text search]"
  - "full-text search [SQL Server], stopwords"
  - "stopwords [full-text search]"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.fulltext_stopwords (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Contains a row per stopword for all stoplists in the database.

| Column name | Data type | Description |
| --- | --- | --- |
| `stoplist_id` | **int** | ID of the stoplist to which `stopword` belongs. This ID is unique within the database. |
| `stopword` | **nvarchar(64)** | The term to be considered for a stop-word match. |
| `language` | **sysname** | Either the value of the alias in [sys.fulltext_languages](sys-fulltext-languages-transact-sql.md) corresponding to the value of the locale identifier (LCID), or the string representation of the numeric LCID. |
| `language_id` | **int** | LCID used for word breaking. |

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)]

## Related content

- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Object catalog views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [Configure and manage stopwords and stoplists for Full-Text Search](../search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)
- [sys.fulltext_stoplists (Transact-SQL)](sys-fulltext-stoplists-transact-sql.md)
- [sys.fulltext_system_stopwords (Transact-SQL)](sys-fulltext-system-stopwords-transact-sql.md)
