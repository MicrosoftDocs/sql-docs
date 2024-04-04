---
title: "sp_fulltext_keymappings (Transact-SQL)"
description: Returns mappings between document identifiers (DocIds) and full-text key values.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/07/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_fulltext_keymappings_TSQL"
  - "sp_fulltext_keymappings"
helpviewer_keywords:
  - "full-text indexes [SQL Server], key column"
  - "sp_fulltext_keymappings"
  - "full-text indexes [SQL Server], troubleshooting"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_fulltext_keymappings (Transact-SQL)

[!INCLUDE [sql-asdbmi-pdw](../../includes/applies-to-version/sql-asdbmi-pdw.md)]

Returns mappings between document identifiers (DocIds) and full-text key values. The DocId column contains values for a **bigint** integer that maps to a particular full-text key value in a full-text indexed table. DocId values that satisfy a search condition are passed from the Full-Text Engine to the Database Engine, where they are mapped to full-text key values from the base table being queried. The full-text key column is a unique index that is required on one column of the table.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_fulltext_keymappings { table_id | table_id , docId | table_id , NULL , key }
[ ; ]
```

## Arguments

#### *table_id*

The object ID of the full-text indexed table. If you specify an invalid *table_id*, an error is returned. For information about obtaining the object ID of a table, see [OBJECT_ID (Transact-SQL)](../../t-sql/functions/object-id-transact-sql.md).

#### *docId*

An internal document identifier (DocId) that corresponds to the key value. An invalid *docId* value returns no results.

#### *key*

The full-text key value from the specified table. An invalid *key* value returns no results. For information about full-text key values, see [Manage Full-Text Indexes](../search/create-and-manage-full-text-indexes.md).

## Return code values

None.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `DocId` | **bigint** | An internal document identifier (DocId) column that corresponds to the key value. |
| `Key` | <sup>1</sup> | The full-text key value from the specified table.<br /><br />If no full-text keys exist in the mapping table, an empty rowset is returned. |

<sup>1</sup> The data type for Key is same as the data type of the full-text key column in the base table.

## Permissions

This function is public and doesn't require any special permissions.

## Remarks

The following table describes the effect of using one, two, or three parameters.

| This parameter list... | Has this result... |
| --- | --- |
| *table_id* | When invoked with only the *table_id* parameter, `sp_fulltext_keymappings` returns all full-text key (Key) values from the specified base table, along with the DocId that corresponds to each key. This includes keys that are pending delete.<br /><br />This function is useful for troubleshooting various issues. It's useful for seeing the full-text index content when the selected full-text key isn't of an integer data type. This involves joining the results of `sp_fulltext_keymappings` with the results of `sys.dm_fts_index_keywords_by_document`. For more information, see [sys.dm_fts_index_keywords_by_document (Transact-SQL)](../system-dynamic-management-views/sys-dm-fts-index-keywords-by-document-transact-sql.md).<br /><br />In general, however, we recommend that, if possible, you execute `sp_fulltext_keymappings` with parameters that specify a specific full-text key or DocId. This is much more efficient than returning an entire key map, especially for a large table for which the performance cost of returning the entire key map might be substantial. |
| *table_id*, *docId* | If only the *table_id* and *docId* are specified, *docId* must be non-NULL and specify a valid DocId in the specified table. This function is useful to isolate the custom full-text key from the base table that corresponds to the DocId of a particular full-text index. |
| *table_id*, NULL, *key* | If three parameters are present, the second parameter must be NULL, and *key* must be non-NULL and specify a valid full-text key value from the specified table. This function is useful in isolating the DocId that corresponds to a particular full-text key from the base table. |

An error is returned under any of the following conditions:

- You specify an invalid *table_id*
- The table isn't full-text indexed
- `NULL` is encountered for a parameter that may be non-null.

## Examples

> [!NOTE]  
> The examples in this section use the `Production.ProductReview` table of the [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database. You can create this index by executing the example provided for the `ProductReview` table in [CREATE FULLTEXT INDEX (Transact-SQL)](../../t-sql/statements/create-fulltext-index-transact-sql.md).

### A. Obtain all the Key and DocId values

The following example uses a [DECLARE](../../t-sql/language-elements/declare-local-variable-transact-sql.md) statement to create a local variable, `@table_id` and to assign the ID of the `ProductReview` table as its value. The example executes `sp_fulltext_keymappings` specifying `@table_id` for the *table_id* parameter.

> [!NOTE]  
> Using `sp_fulltext_keymappings` with only the *table_id* parameter is suitable for small tables.

```sql
USE AdventureWorks2022;
GO
DECLARE @table_id int = OBJECT_ID(N'Production.ProductReview');
EXEC sp_fulltext_keymappings @table_id;
GO
```

This example returns all the DocIds and full-text keys from the table, as follows:

| TABLE | docId | key |
| --- | --- | --- |
| `1` | `1` | `1` |
| `2` | `2` | `2` |
| `3` | `3` | `3` |
| `4` | `4` | `4` |

### B. Obtain the DocId value for a specific Key value

The following example uses a DECLARE statement to create a local variable, `@table_id`, and to assign the ID of the `ProductReview` table as its value. The example executes `sp_fulltext_keymappings` specifying `@table_id` for the *table_id* parameter, NULL for the *docId* parameter, and 4 for the *key* parameter.

> [!NOTE]  
> Using `sp_fulltext_keymappings` with only the *table_id* parameteris suitable for small tables.

```sql
USE AdventureWorks2022;
GO

DECLARE @table_id int = OBJECT_ID(N'Production.ProductReview');

EXEC sp_fulltext_keymappings @table_id, NULL, 4;
GO
```

This example returns the following results.

| TABLE | docId | key |
| --- | --- | --- |
| `4` | `4` | `4` |

## Related content

- [Full-Text Search and Semantic Search stored procedures (Transact-SQL)](full-text-search-and-semantic-search-stored-procedures-transact-sql.md)
