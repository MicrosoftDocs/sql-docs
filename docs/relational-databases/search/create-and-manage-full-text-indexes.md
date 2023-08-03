---
title: "Create and manage full-text indexes"
description: This article describes how to create, populate, and manage full-text indexes in SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
ms.date: 12/05/2022
ms.service: sql
ms.subservice: search
ms.topic: conceptual
helpviewer_keywords:
  - "full-text indexes [SQL Server], about"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create and manage full-text indexes

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to create, populate, and manage full-text indexes in SQL Server.

## Create a full-text catalog

Before you can create a full-text index, you have to have a full-text catalog. The catalog is a virtual container for one or more full-text indexes. For more info, see [Create and Manage Full-Text Catalogs](../../relational-databases/search/create-and-manage-full-text-catalogs.md).

## <a id="tasks"></a> Create, alter, or drop a full-text index

- [CREATE FULLTEXT INDEX (Transact-SQL)](../../t-sql/statements/create-fulltext-index-transact-sql.md)
- [ALTER FULLTEXT INDEX (Transact-SQL)](../../t-sql/statements/alter-fulltext-index-transact-sql.md)
- [DROP FULLTEXT INDEX (Transact-SQL)](../../t-sql/statements/drop-fulltext-index-transact-sql.md)

## Populate a full-text index

The process of creating and maintaining a full-text index is called a *population* (also known as a *crawl*). There are three types of full-text index population:

- Full population
- Population based on change tracking
- Incremental population based on a timestamp.

For more information, see [Populate Full-Text Indexes](../../relational-databases/search/populate-full-text-indexes.md).

## <a id="view"></a> View the properties of a full-text index

### View the properties of a full-text index with Transact-SQL

| Catalog or Dynamic Management View | Description |
| --- | --- |
| [sys.fulltext_index_catalog_usages (Transact-SQL)](../../relational-databases/system-catalog-views/sys-fulltext-index-catalog-usages-transact-sql.md) | Returns a row for each full-text catalog to full-text index reference. |
| [sys.fulltext_index_columns (Transact-SQL)](../../relational-databases/system-catalog-views/sys-fulltext-index-columns-transact-sql.md) | Contains a row for each column that is part of a full-text index. |
| [sys.fulltext_index_fragments (Transact-SQL)](../../relational-databases/system-catalog-views/sys-fulltext-index-fragments-transact-sql.md) | A fulltext index uses internal tables called full-text index fragments to store the inverted index data. This view can be used to query the metadata about these fragments. This view contains a row for each full-text index fragment in every table that contains a full-text index. |
| [sys.fulltext_indexes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-fulltext-indexes-transact-sql.md) | Contains a row per full-text index of a tabular object. |
| [sys.dm_fts_index_keywords (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-transact-sql.md) | Returns information about the content of a full-text index for the specified table. |
| [sys.dm_fts_index_keywords_by_document (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-by-document-transact-sql.md) | Returns information about the document-level content of a full-text index for the specified table. A given keyword can appear in several documents. |
| [sys.dm_fts_index_population (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-population-transact-sql.md) | Returns information about the full-text index populations currently in progress. |

### View the properties of a full-text index with SQL Server Management Studio

> [!NOTE]  
> To view properties of full-text indexes for Azure SQL databases, use [Transact-SQL](#view-the-properties-of-a-full-text-index-with-transact-sql).

1. In SQL Server Management Studio, in Object Explorer, expand the server.

1. Expand **Databases**, and then expand the database that contains the full-text index.

1. Expand **Tables**.

1. Right-click the table on which the full-text index is defined, select **Full-Text index**, and on the **Full-Text index** context menu, select **Properties**. This opens the **Full-text index Properties** dialog box.

1. In the **Select a page** pane, you can select any of the following pages:

   | Page | Description |
   | --- | --- |
   | **General** | Displays basic properties of the full-text index. These include several modifiable properties and many unchangeable properties such as database name, table name, and the name of full-text key column. The modifiable properties are:<br /><br />**Full-Text Index Stoplist**<br /><br />**Full-Text Indexing Enabled**<br /><br />**Change Tracking**<br /><br />**Search Property List** |
   | **Columns** | Displays the table columns that are available for full-text indexing. The selected column or columns are full-text indexed. You can select as many of the available columns as you want to include in the full-text index. For more info, see [Populate Full-Text Indexes](populate-full-text-indexes.md). |
   | **Schedules** | Use this page to create or manage schedules for a SQL Server Agent job that starts an incremental table population for the full-text index populations. For more info, see [Populate Full-Text Indexes](../../relational-databases/search/populate-full-text-indexes.md).<br /><br />Note: After you exit the **Full-Text Index Properties** dialog box, any newly created schedule is associated with a SQL Server Agent job (Start Incremental Table Population on *database_name*.*table_name*). |

1. Select **OK** to save any changes and exit the **Full-text index Properties** dialog box.

## <a id="props"></a> View the properties of indexed tables and columns

Several [!INCLUDE[tsql](../../includes/tsql-md.md)] functions such as OBJECTPROPERTYEX can be used to obtain the value of various full-text indexing properties. This information is useful for administering and troubleshooting full-text search.

The following table lists the full-text properties related to indexed tables and columns and their related [!INCLUDE[tsql](../../includes/tsql-md.md)] functions.

| Property | Description | Function |
| --- | --- | --- |
| **FullTextTypeColumn** | TYPE COLUMN in the table that holds the document type information of the column. | [COLUMNPROPERTY](../../t-sql/functions/columnproperty-transact-sql.md) |
| **IsFulltextIndexed** | Whether a column has been enabled for full-text indexing. | COLUMNPROPERTY |
| **IsFulltextKey** | Whether the index is the full-text key for a table. | [INDEXPROPERTY](../../t-sql/functions/indexproperty-transact-sql.md) |
| **TableFulltextBackgroundUpdateIndexOn** | Whether a table has full-text background update indexing. | [OBJECTPROPERTYEX](../../t-sql/functions/objectpropertyex-transact-sql.md) |
| **TableFulltextCatalogId** | Full-text catalog ID in which the full-text index data for the table resides. | OBJECTPROPERTYEX |
| **TableFulltextChangeTrackingOn** | Whether a table has full-text change-tracking enabled. | OBJECTPROPERTYEX |
| **TableFulltextDocsProcessed** | Number of rows processed since the start of full-text indexing. | OBJECTPROPERTYEX |
| **TableFulltextFailCount** | Number of rows Full-Text Search didn't index. | OBJECTPROPERTYEX |
| **TableFulltextItemCount** | Number of rows that were successfully full-text indexed. | OBJECTPROPERTYEX |
| **TableFulltextKeyColumn** | The column ID of the full-text unique key column. | OBJECTPROPERTYEX |
| **TableFullTextMergeStatus** | Whether a table that has a full-text index is currently in merging. | OBJECTPROPERTYEX |
| **TableFulltextPendingChanges** | Number of pending change tracking entries to process. | OBJECTPROPERTYEX |
| **TableFulltextPopulateStatus** | Population status of a full-text table. | OBJECTPROPERTYEX |
| **TableHasActiveFulltextIndex** | Whether a table has an active full-text index. | OBJECTPROPERTYEX |

## <a id="key"></a> Get information about the full-text key column

Typically, the result of CONTAINSTABLE or FREETEXTTABLE rowset-valued functions need to be joined with the base table. In such cases, you need to know the unique key column name. You can inquire whether a given unique index is used as the full-text key, and you can obtain the identifier of the full-text key column.

### Determine whether a given unique index is used as the full-text key column

Use a [SELECT](../../t-sql/queries/select-transact-sql.md) statement to call the [INDEXPROPERTY](../../t-sql/functions/indexproperty-transact-sql.md) function. In the function call, use the OBJECT_ID function to convert the name of the table (*table_name*) into the table ID, specify the name of a unique index for the table, and specify the **IsFulltextKey** index property, as follows:

```sql
SELECT INDEXPROPERTY(OBJECT_ID('table_name'), 'index_name',  'IsFulltextKey');
```

This statement returns 1 if the index is used to enforce uniqueness of the full-text key column and 0 if it isn't.

#### Example

The following example inquires whether the `PK_Document_DocumentNode` index is used to enforce the uniqueness of the full-text key column, as follows:

```sql
USE AdventureWorks2019;
GO
SELECT INDEXPROPERTY(OBJECT_ID('Production.Document'), 'PK_Document_DocumentNode',  'IsFulltextKey');
```

This example returns 1 if the `PK_Document_DocumentNode` index is used to enforce uniqueness of the full-text key column. Otherwise, it returns 0 or NULL. NULL implies you're using an invalid index name, the index name doesn't correspond to the table, the table doesn't exist, or so forth.

### Find the identifier of the full-text key column

Each full-text enabled table has a column that is used to enforce unique rows for the table (the *unique* key column). The **TableFulltextKeyColumn** property, obtained from the OBJECTPROPERTYEX function, contains the column ID of the unique key column.

To obtain this identifier, you can use a SELECT statement to call the OBJECTPROPERTYEX function. Use the OBJECT_ID function to convert the name of the table (*table_name*) into the table ID and specify the **TableFulltextKeyColumn** property, as follows:

```sql
SELECT OBJECTPROPERTYEX(OBJECT_ID('table_name'), 'TableFulltextKeyColumn' ) AS 'Column Identifier';
```

#### Examples

The following example returns the identifier of the full-text key column or NULL. NULL implies that you're using an invalid index name, the index name doesn't correspond to the table, the table doesn't exist, or so forth.

```sql
USE AdventureWorks2019;
GO
SELECT OBJECTPROPERTYEX(OBJECT_ID('Production.Document'), 'TableFulltextKeyColumn');
GO
```

The following example shows how to use the identifier of the unique key column to obtain the name of the column.

```sql
USE AdventureWorks2019;
GO

DECLARE @key_column SYSNAME

SET @key_column = COL_NAME(OBJECT_ID('Production.Document'),
   OBJECTPROPERTYEX(OBJECT_ID('Production.Document'), 'TableFulltextKeyColumn'));

SELECT @key_column AS 'Unique Key Column';
GO
```

This example returns a result set column named `Unique Key Column`, which displays a single row containing the name of the unique key column of the Document table, DocumentNode. If this query contained an invalid index name, the index name didn't correspond to the table, the table didn't exist, and so forth, it would return NULL.

## Index varbinary(max) and XML columns

If a **varbinary(max)**, **varbinary**, or **xml** column is full-text indexed, it can be queried using the full-text predicates (CONTAINS and FREETEXT) and functions (CONTAINSTABLE and FREETEXTTABLE), like any other full-text indexed column.

### Index varbinary(max) or varbinary data

A single **varbinary(max)** or **varbinary** column can store many types of documents. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports any document type for which a filter is installed and available in the operative system. The document type of each document is identified by the file extension of the document. For example, for a .doc file extension, full-text search uses the filter that supports Microsoft Word documents. For a list of available document types, query the [sys.fulltext_document_types](../../relational-databases/system-catalog-views/sys-fulltext-document-types-transact-sql.md) catalog view.

The Full-Text Engine can use existing filters that are installed in the operating system. Before you can use operating-system filters, word breakers, and stemmers, you must load them in the server instance, as follows:

```sql
EXEC sp_fulltext_service @action = 'load_os_resources', @value = 1;
```

To create a full-text index on a **varbinary(max)** column, the Full-Text Engine needs access to the file extensions of the documents in the **varbinary(max)** column. This information must be stored in a table column, called a type column, that must be associated with the **varbinary(max)** column in the full-text index. When indexing a document, the Full-Text Engine uses the file extension in the type column to identify which filter to use.

### Index XML data

An **xml** data type column stores only XML documents and fragments, and only the XML filter is used for the documents. Therefore, a type column is unnecessary. On **xml** columns, the full-text index indexes the content of the XML elements, but ignores the XML markup. Attribute values are full-text indexed unless they are numeric values. Element tags are used as token boundaries. Well-formed XML or HTML documents and fragments containing multiple languages are supported.

For more info about indexing and querying on an **xml** column, see [Use Full-Text Search with XML Columns](../../relational-databases/xml/use-full-text-search-with-xml-columns.md).

## <a id="disable"></a> Disable or re-enable full-text indexing for a table

In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], all user-created databases are full-text enabled by default. Additionally, an individual table is automatically enabled for full-text indexing as soon as a full-text index is created on it and a column is added to the index. A table is automatically disabled for full-text indexing when the last column is dropped from its full-text index.

On a table that has a full-text index, you can manually disable or re-enable a table for full-text indexing using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

1. Expand the server group, expand **Databases**, and expand the database that contains the table you want to enable for full-text indexing.

1. Expand **Tables**, and right-click the table that you want to disable or re-enable for full-text indexing.

1. Select **Full-Text index**, and then select **Disable Full-Text index** or **Enable Full-Text index**.

## <a id="remove"></a> Remove a full-text index from a table

1. In Object Explorer, right-click the table that has the full-text index that you want to delete.

1. Select **Delete Full-Text index**.

1. When prompted, select **OK** to confirm that you want to delete the full-text index.

## Next steps

- [Full-Text Search](full-text-search.md)
- [Full-Text Search and Semantic Search Stored Procedures (Transact-SQL)](../system-stored-procedures/full-text-search-and-semantic-search-stored-procedures-transact-sql.md)
- [Create and Manage Full-Text Catalogs](create-and-manage-full-text-catalogs.md)
