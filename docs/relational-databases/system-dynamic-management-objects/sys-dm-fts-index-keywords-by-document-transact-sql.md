---
title: sys.dm_fts_index_keywords_by_document (Transact-SQL)
description: Returns information about the document-level content of a full-text index associated with the specified table.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/09/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.dm_fts_index_keywords_by_document_TSQL"
  - "dm_fts_index_keywords_by_document_TSQL"
  - "sys.dm_fts_index_keywords_by_document"
  - "dm_fts_index_keywords_by_document"
helpviewer_keywords:
  - "full-text search [SQL Server], troubleshooting"
  - "sys.dm_fts_index_keywords_by_document dynamic management function"
  - "full-text search [SQL Server], viewing keywords"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.dm_fts_index_keywords_by_document (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Returns information about the document-level content of a full-text index associated with the specified table.

`sys.dm_fts_index_keywords_by_document` is a dynamic management function.

- To view higher-level full-text index information, see [sys.dm_fts_index_keywords](sys-dm-fts-index-keywords-transact-sql.md).

- To view information about property-level content related to a document property, see [sys.dm_fts_index_keywords_by_property](sys-dm-fts-index-keywords-by-property-transact-sql.md).

## Syntax

```syntaxsql
sys.dm_fts_index_keywords_by_document
(
    DB_ID('database_name')
    , OBJECT_ID('table_name')
)
```

## Arguments

#### DB_ID('*database_name*')

A call to the [DB_ID](../../t-sql/functions/db-id-transact-sql.md) function. `DB_ID` is **int**. This function accepts a database name and returns the database ID, which `sys.dm_fts_index_keywords_by_document` uses to find the specified database. If *database_name* is omitted, the current database ID is returned.

#### OBJECT_ID('*table_name*')

A call to the [OBJECT_ID](../../t-sql/functions/object-id-transact-sql.md) function. `OBJECT_ID` is **int**. This function accepts a table name and returns the table ID of the table containing the full-text index to inspect.

## Table returned

| Column name | Data type | Nullable | Description |
| --- | --- | --- | --- |
| `keyword` <sup>1</sup> | **varbinary(128)** | No | The hexadecimal representation of the keyword that is stored inside the full-text index. |
| `display_term` <sup>1</sup> | **nvarchar(4000)** | No | The human-readable format of the keyword. This format is derived from the internal format that is stored in the full-text index. |
| `column_id` | **int** | No | ID of the column from which the current keyword was full-text indexed. |
| `document_id` | **bigint** | Yes | ID of the document or row from which the current term was full-text indexed. This ID corresponds to the full-text key value of that document or row. |
| `occurrence_count` | **int** | Yes | Number of occurrences of the current keyword in the document or row that is indicated by `document_id`. When '*search_property_name*' is specified, `occurrence_count` displays only the number of occurrences of the current keyword in the specified search property within the document or row. |

<sup>1</sup> `0xFF` represents the special character that indicates the end of a file or dataset.

## Remarks

The information returned by `sys.dm_fts_index_keywords_by_document` is useful for finding out the following, among other things:

- The total number of keywords that a full-text index contains.
- Whether a keyword is part of a given document or row.
- How many times a keyword appears in the whole full-text index. That is, `SUM(occurrence_count) WHERE keyword = <keyword_value>`
- How many times a keyword appears in a given document or row.
- How many keywords a given document or row contains.

Also, you can also use the information provided by `sys.dm_fts_index_keywords_by_document` to retrieve all the keywords belonging to a given document or row.

When the full-text key column is an integer data type, as recommended, the `document_id` maps directly to the full-text key value in the base table.

In contrast, when the full-text key column uses a non-integer data type, `document_id` doesn't represent the full-text key in the base table. In this case, to identify the row in the base table that is returned by `dm_fts_index_keywords_by_document`, you need to join this view with the results returned by [sp_fulltext_keymappings](../system-stored-procedures/sp-fulltext-keymappings-transact-sql.md).

Before you can join them, you must store the output of the stored procedure in a temp table. Then you can join the `document_id` column of `dm_fts_index_keywords_by_document` with the `DocId` column that is returned by this stored procedure. A **timestamp** column can't receive values at insert time, because they are auto-generated by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Therefore, the **timestamp** column must be converted to **varbinary(8)** columns. The following example shows these steps. In this example, *@table_id* is the ID of your table, *database_name* is the name of your database, and *table_name* is the name of your table.

```sql
USE database_name;
GO

CREATE TABLE #MyTempTable
(
    docid INT PRIMARY KEY,
    [key] INT NOT NULL
);

DECLARE @db_id AS INT = DB_ID(N'database_name');
DECLARE @table_id AS INT = OBJECT_ID(N'table_name');

INSERT INTO #MyTempTable
EXECUTE sp_fulltext_keymappings @table_id;

SELECT *
FROM sys.dm_fts_index_keywords_by_document(@db_id, @table_id) AS kbd
     INNER JOIN #MyTempTable AS tt
         ON tt.[docid] = kbd.document_id;
GO
```

## Permissions

Requires `SELECT` permission on the columns covered by the full-text index and `CREATE FULLTEXT CATALOG` permissions.

## Examples

### A. Display full-text index content at the document level

The following example displays the content of the full-text index at the document level in the `HumanResources.JobCandidate` table of the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] sample database.

> [!NOTE]  
> You can create this index by executing the example provided for the `HumanResources.JobCandidate` table in [CREATE FULLTEXT INDEX](../../t-sql/statements/create-fulltext-index-transact-sql.md).

```sql
SELECT *
FROM sys.dm_fts_index_keywords_by_document(DB_ID('AdventureWorks2025'), OBJECT_ID('HumanResources.JobCandidate'));
GO
```

## Related content

- [Full-text and semantic search dynamic management views and functions](full-text-and-semantic-search-dynamic-management-views-functions.md)
- [Full-Text Search](../search/full-text-search.md)
- [sys.dm_fts_index_keywords (Transact-SQL)](sys-dm-fts-index-keywords-transact-sql.md)
- [sys.dm_fts_index_keywords_by_property (Transact-SQL)](sys-dm-fts-index-keywords-by-property-transact-sql.md)
- [sp_fulltext_keymappings (Transact-SQL)](../system-stored-procedures/sp-fulltext-keymappings-transact-sql.md)
- [Improve the Performance of Full-Text Indexes](../search/improve-the-performance-of-full-text-indexes.md)
