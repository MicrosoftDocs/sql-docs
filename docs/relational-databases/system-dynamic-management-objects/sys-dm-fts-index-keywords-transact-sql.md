---
title: sys.dm_fts_index_keywords (Transact-SQL)
description: sys.dm_fts_index_keywords (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/09/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "dm_fts_index_keywords"
  - "sys.dm_fts_index_keywords"
  - "sys.dm_fts_index_keywords_TSQL"
  - "dm_fts_index_keywords_TSQL"
helpviewer_keywords:
  - "sys.dm_fts_index_keywords dynamic management function"
  - "full-text search [SQL Server], viewing keywords"
  - "troubleshooting [SQL Server], full-text search"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.dm_fts_index_keywords (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Returns information about the content of a full-text index for the specified table.

`sys.dm_fts_index_keywords` is a dynamic management function.

> [!NOTE]  
> To view lower-level full-text index information, use the [sys.dm_fts_index_keywords_by_document](sys-dm-fts-index-keywords-by-document-transact-sql.md) dynamic management function at the document level.

## Syntax

```syntaxsql
sys.dm_fts_index_keywords
(
    DB_ID('database_name')
   , OBJECT_ID('table_name')
)
```

## Arguments

#### DB_ID('*database_name*')

A call to the [DB_ID](../../t-sql/functions/db-id-transact-sql.md) function. `DB_ID` is **int**. This function accepts a database name and returns the database ID, which `sys.dm_fts_index_keywords` uses to find the specified database. If *database_name* is omitted, the current database ID is returned.

#### OBJECT_ID('*table_name*')

A call to the [OBJECT_ID](../../t-sql/functions/object-id-transact-sql.md) function. `OBJECT_ID` is **int**. This function accepts a table name and returns the table ID of the table containing the full-text index to inspect.

## Table returned

| Column name | Data type | Nullable | Description |
| --- | --- | --- | --- |
| `keyword` <sup>1</sup> | **varbinary(128)** | No | The hexadecimal representation of the keyword stored inside the full-text index. |
| `display_term` <sup>2</sup> | **nvarchar(4000)** | No | The human-readable format of the keyword. This format is derived from the hexadecimal format. |
| `column_id` | **int** | No | ID of the column from which the current keyword was full-text indexed. |
| `document_count` | **bigint** | Yes | Number of documents or rows containing the current term. |

<sup>1</sup> `0xFF` represents the special character that indicates the end of a file or dataset.  
<sup>2</sup> The `display_term` value for `0xFF` is `END OF FILE`.

## Remarks

The information returned by `sys.dm_fts_index_keywords` is useful for finding out the following, among other things:

- Whether a keyword is part of the full-text index.

- How many documents or rows contain a given keyword.

- The most common keyword in the full-text index:

  - `document_count` of each *keyword_value* compared to the total `document_count`, the document count of 0xFF.

  - Typically, common keywords are likely to be appropriate to declare as stopwords.

The `document_count` returned by `sys.dm_fts_index_keywords` might be less accurate for a specific document than the count returned by `sys.dm_fts_index_keywords_by_document` or a `CONTAINS` query. This potential inaccuracy is estimated to be less than 1%. This inaccuracy can occur because a `document_id` might be counted twice when it continues across more than one row in the index fragment, or when it appears more than once in the same row. To obtain a more accurate count for a specific document, use `sys.dm_fts_index_keywords_by_document` or a `CONTAINS` query.

## Permissions

Requires membership in the **sysadmin** fixed server role.

## Examples

### A. Display high-level full-text index content

The following example displays information about the high-level content of the full-text index in the `HumanResources.JobCandidate` table.

```sql
SELECT *
FROM sys.dm_fts_index_keywords(DB_ID('AdventureWorks2025'), OBJECT_ID('HumanResources.JobCandidate'));
GO
```

## Related content

- [Full-text and semantic search dynamic management views and functions](full-text-and-semantic-search-dynamic-management-views-functions.md)
- [Full-Text Search](../search/full-text-search.md)
- [sys.dm_fts_index_keywords_by_document (Transact-SQL)](sys-dm-fts-index-keywords-by-document-transact-sql.md)
