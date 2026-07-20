---
title: sys.fulltext_index_columns (Transact-SQL)
description: sys.fulltext_index_columns contains a row for each column that is part of a full-text index.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/09/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.fulltext_index_columns_TSQL"
  - "sys.fulltext_index_columns"
  - "fulltext_index_columns_TSQL"
  - "fulltext_index_columns"
helpviewer_keywords:
  - "sys.fulltext_index_columns catalog view"
  - "full-text indexes [SQL Server], columns"
  - "full-text indexes [SQL Server], properties"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.fulltext_index_columns (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Contains a row for each column that is part of a full-text index.

| Column name | Data type | Description |
| --- | --- | --- |
| `object_id` | **int** | ID of the object of which this is part. |
| `column_id` | **int** | ID of the column that is part of the full-text index. |
| `type_column_id` | **int** | ID of the type column that stores the user-supplied document file extension (`.doc`, `.xls`, and so forth) of the document in a given row. The type column is specified only for columns whose data requires filtering during full-text indexing. `NULL` if not applicable. For more information, see [Configure and manage filters](../search/configure-and-manage-filters-for-search.md). |
| `language_id` | **int** | LCID of language whose word breaker is used to index this full-text column.<br /><br />0 = Neutral.<br /><br />For more information, see [sys.fulltext_languages](sys-fulltext-languages-transact-sql.md). |
| `statistical_semantics` | **int** | 1 = This column has statistical semantics enabled in addition to full-text indexing. |

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)]

## Related content

- [Object catalog views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
