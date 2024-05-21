---
title: "sp_help_fulltext_columns (Transact-SQL)"
description: sp_help_fulltext_columns returns the columns designated for full-text indexing.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_fulltext_columns"
  - "sp_help_fulltext_columns_TSQL"
helpviewer_keywords:
  - "sp_help_fulltext_columns"
dev_langs:
  - "TSQL"
---
# sp_help_fulltext_columns (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the columns designated for full-text indexing.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use the [sys.fulltext_index_columns](../system-catalog-views/sys-fulltext-index-columns-transact-sql.md) catalog view instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_fulltext_columns
    [ [ @table_name = ] N'table_name' ]
    [ , [ @column_name = ] N'column_name' ]
[ ; ]
```

## Arguments

#### [ @table_name = ] N'*table_name*'

The one- or two-part table name for which full-text index information is requested. *@table_name* is **nvarchar(517)**, with a default of `NULL`.

If *@table_name* is omitted, full-text index column information is retrieved for every full-text indexed table.

#### [ @column_name = ] N'*column_name*'

The name of the column for which full-text index metadata is requested. *@column_name* is **sysname**, with a default of `NULL`. If *@column_name* is omitted or is `NULL`, full-text column information is returned for every full-text indexed column for *@table_name*.

If *@table_name* is also omitted or is `NULL`, full-text index column information is returned for every full-text indexed column for all tables in the database.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `TABLE_OWNER` | **sysname** | Table owner. The owner is the name of the database user that created the table. |
| `TABLE_ID` | **int** | ID of the table. |
| `TABLE_NAME` | **sysname** | Name of the table. |
| `FULLTEXT_COLUMN_NAME` | **sysname** | Column in a full-text indexed table that is designated for indexing. |
| `FULLTEXT_COLID` | **int** | Column ID of the full-text indexed column. |
| `FULLTEXT_BLOBTP_COLNAME` | **sysname** | Column in a full-text indexed table that specifies the document type of the full-text indexed column. This value is only applicable when the full-text indexed column is a **varbinary(max)** or **image** column. |
| `FULLTEXT_BLOBTP_COLID` | **int** | Column ID of the document type column. This value is only applicable when the full-text indexed column is a **varbinary(max)** or **image** column. |
| `FULLTEXT_LANGUAGE` | **sysname** | Language used for the full-text search of the column. |

## Permissions

Execute permissions default to members of the **public** role.

## Examples

The following example returns information about the columns that have been designated for full-text indexing in the `Document` table.

```sql
USE AdventureWorks2022;
GO
EXEC sp_help_fulltext_columns 'Production.Document';
GO
```

## Related content

- [COLUMNPROPERTY (Transact-SQL)](../../t-sql/functions/columnproperty-transact-sql.md)
- [sp_fulltext_column (Transact-SQL)](sp-fulltext-column-transact-sql.md)
- [sp_help_fulltext_columns_cursor (Transact-SQL)](sp-help-fulltext-columns-cursor-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
