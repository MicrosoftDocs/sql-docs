---
title: sys.fulltext_system_stopwords (Transact-SQL)
description: sys.fulltext_system_stopwords provides access to the system stoplist.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/09/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.fulltext_system_stopwords_TSQL"
  - "sys.fulltext_system_stopwords"
  - "fulltext_system_stopwords_TSQL"
  - "fulltext_system_stopwords"
helpviewer_keywords:
  - "sys.fulltext_system_stopwords catalog view"
  - "stoplists [full-text search]"
  - "full-text search [SQL Server], stopwords"
  - "stopwords [full-text search]"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.fulltext_system_stopwords (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Provides access to the system stoplist.

| Column name | Data type | Description |
| --- | --- | --- |
| `stopword` | **nvarchar(64)** | The term that is considered for a stop-word match. |
| `language_id` | **int** | Locale identifier (LCID) of the language. This LCID is used for word breaking. |

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)]

## Related content

- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Object catalog views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [sys.fulltext_stoplists (Transact-SQL)](sys-fulltext-stoplists-transact-sql.md)
- [sys.fulltext_stopwords (Transact-SQL)](sys-fulltext-stopwords-transact-sql.md)
- [Configure and manage stopwords and stoplists for Full-Text Search](../search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)
