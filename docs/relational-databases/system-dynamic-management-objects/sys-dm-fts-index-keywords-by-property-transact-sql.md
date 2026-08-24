---
title: sys.dm_fts_index_keywords_by_property (Transact-SQL)
description: sys.dm_fts_index_keywords_by_property returns all property-related content in the full-text index of a given table.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/09/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "dm_fts_index_keywords_by_property"
  - "dm_fts_index_keywords_by_property_TSQL"
  - "sys.dm_fts_index_keywords_by_property"
  - "sys.dm_fts_index_keywords_by_property_TSQL"
helpviewer_keywords:
  - "full-text search [SQL Server], troubleshooting"
  - "search property lists [SQL Server], viewing keywords by property"
  - "full-text search [SQL Server], viewing keywords"
  - "sys.dm_fts_index_keywords_by_property dynamic management view"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.dm_fts_index_keywords_by_property (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Returns all property-related content in the full-text index of a given table. This includes all data that belongs to any property registered by the search property list associated with that full-text index.

`sys.dm_fts_index_keywords_by_property` is a dynamic management function that enables you to see what registered properties were emitted by IFilters at index time, as well as the exact content of every property in each indexed document.

- To view all document-level content (including property-related content), see [sys.dm_fts_index_keywords_by_document](sys-dm-fts-index-keywords-by-document-transact-sql.md).

- To view higher-level full-text index information, see [sys.dm_fts_index_keywords](sys-dm-fts-index-keywords-transact-sql.md).

For information about search property lists, see [Search Document Properties with Search Property Lists](../search/search-document-properties-with-search-property-lists.md).

## Syntax

```syntaxsql
sys.dm_fts_index_keywords_by_property
(
    DB_ID('database_name')
    , OBJECT_ID('table_name')
)
```

## Arguments

#### DB_ID('*database_name*')

A call to the [DB_ID](../../t-sql/functions/db-id-transact-sql.md) function. `DB_ID` is **int**. This function accepts a database name and returns the database ID, which `sys.dm_fts_index_keywords_by_property` uses to find the specified database. If *database_name* is omitted, the current database ID is returned.

#### OBJECT_ID('*table_name*')

A call to the [OBJECT_ID](../../t-sql/functions/object-id-transact-sql.md) function. `OBJECT_ID` is **int**. This function accepts a table name and returns the table ID of the table containing the full-text index to inspect.

## Table returned

| Column name | Data type | Nullable | Description |
| --- | --- | --- | --- |
| `keyword` <sup>1</sup> | **varbinary(128)** | No | The hexadecimal representation of the keyword that is stored inside the full-text index. |
| `display_term` <sup>1</sup> | **nvarchar(4000)** | No | The human-readable format of the keyword. This format is derived from the internal format that is stored in the full-text index. |
| `column_id` | **int** | No | ID of the column from which the current keyword was full-text indexed. |
| `document_id` | **bigint** | Yes | ID of the document or row from which the current term was full-text indexed. This ID corresponds to the full-text key value of that document or row. |
| `property_id` | **int** | Yes | Internal property ID of the search property within the full-text index of the table that you specified in the `OBJECT_ID` parameter. |

<sup>1</sup> `0xFF` represents the special character that indicates the end of a file or dataset.

When a given property is added to a search property list, the Full-Text Engine registers the property and assigns it an internal property ID that is specific to that property list. The internal property ID, which is an integer, is unique to a given search property list. If a given property is registered for multiple search property lists, a different internal property ID might be assigned for each search property list.

> [!NOTE]  
> The internal property ID is distinct from the property integer identifier that is specified when adding the property to the search property list. For more information, see [Search Document Properties with Search Property Lists](../search/search-document-properties-with-search-property-lists.md).

To view the association between `property_id` and the property name, see [sys.registered_search_properties](../system-catalog-views/sys-registered-search-properties-transact-sql.md).

## Remarks

This dynamic management view can answer questions such as the following:

- What content is stored on a given property for a given DocID?

- How common is a given property among the indexed documents?

- What documents actually contain a given property? This is useful if querying on a given search property doesn't return a document that you expected to find.

When the full-text key column is an integer data type, as recommended, the `document_id` maps directly to the full-text key value in the base table.

In contrast, when the full-text key column uses a non-integer data type, `document_id` doesn't represent the full-text key in the base table. In this case, to identify the row in the base table that is returned by `dm_fts_index_keywords_by_property`, you need to join this view with the results returned by [sp_fulltext_keymappings](../system-stored-procedures/sp-fulltext-keymappings-transact-sql.md).

Before you can join them, you must store the output of the stored procedure in a temp table. Then you can join the `document_id` column of `dm_fts_index_keywords_by_property` with the `DocId` column that is returned by this stored procedure. A **timestamp** column can't receive values at insert time, because they are auto-generated by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Therefore, the **timestamp** column must be converted to **varbinary(8)** columns. The following example shows these steps. In this example, *@table_id* is the ID of your table, *database_name* is the name of your database, and *table_name* is the name of your table.

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
FROM sys.dm_fts_index_keywords_by_property(@db_id, @table_id) AS kbd
     INNER JOIN #MyTempTable AS tt
         ON tt.[docid] = kbd.document_id;
GO
```

## Permissions

Requires `SELECT` permission on the columns covered by the full-text index and `CREATE FULLTEXT CATALOG` permissions.

## Examples

The following example returns keywords from the `Author` property in the full-text index of the `Production.Document` table of the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] sample database. The example uses the alias `KWBPOP` for the table returned by `sys.dm_fts_index_keywords_by_property` The example uses inner joins to combine columns from [sys.registered_search_properties](../system-catalog-views/sys-registered-search-properties-transact-sql.md) and [sys.fulltext_indexes](../system-catalog-views/sys-fulltext-indexes-transact-sql.md).

```sql
-- Once the full-text index is configured to support property searching
-- on the Author property, return any keywords indexed for this property.
USE AdventureWorks2025;
GO

SELECT KWBPOP.*
FROM sys.dm_fts_index_keywords_by_property(DB_ID(), OBJECT_ID('Production.Document')) AS KWBPOP
     INNER JOIN sys.registered_search_properties AS RSP
         ON ((KWBPOP.property_id = RSP.property_id)
         AND (RSP.property_name = 'Author'))
     INNER JOIN sys.fulltext_indexes AS FTI
         ON ((FTI.[OBJECT_ID] = OBJECT_ID('Production.Document'))
         AND (RSP.property_list_id = FTI.property_list_id));
GO
```

## Related content

- [Full-Text Search](../search/full-text-search.md)
- [Improve the performance of full-text indexes](../search/improve-the-performance-of-full-text-indexes.md)
- [sp_fulltext_keymappings (Transact-SQL)](../system-stored-procedures/sp-fulltext-keymappings-transact-sql.md)
- [sys.dm_fts_index_keywords_by_document (Transact-SQL)](sys-dm-fts-index-keywords-by-document-transact-sql.md)
- [sys.dm_fts_index_keywords (Transact-SQL)](sys-dm-fts-index-keywords-transact-sql.md)
- [sys.registered_search_properties (Transact-SQL)](../system-catalog-views/sys-registered-search-properties-transact-sql.md)
- [sys.registered_search_property_lists (Transact-SQL)](../system-catalog-views/sys-registered-search-property-lists-transact-sql.md)
- [Search document properties with search property lists](../search/search-document-properties-with-search-property-lists.md)
