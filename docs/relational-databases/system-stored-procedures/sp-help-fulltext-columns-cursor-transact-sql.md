---
title: "sp_help_fulltext_columns_cursor (Transact-SQL)"
description: Uses a cursor to return the columns designated for full-text indexing.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_fulltext_columns_cursor"
  - "sp_help_fulltext_columns_cursor_TSQL"
helpviewer_keywords:
  - "sp_help_fulltext_columns_cursor"
dev_langs:
  - "TSQL"
---
# sp_help_fulltext_columns_cursor (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Uses a cursor to return the columns designated for full-text indexing.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use the [sys.fulltext_index_columns](../system-catalog-views/sys-fulltext-index-columns-transact-sql.md) catalog view instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_fulltext_columns_cursor
    [ @cursor_return = ] cursor_return OUTPUT
    [ , [ @table_name = ] N'table_name' ]
    [ , [ @column_name = ] N'column_name' ]
[ ; ]
```

## Arguments

#### [ @cursor_return = ] *cursor_return* OUTPUT

*@cursor_return* is an OUTPUT parameter of type **int**. The resulting cursor is a read-only, scrollable, dynamic cursor.

#### [ @table_name = ] N'*table_name*'

The one- or two-part table name for which full-text index information is requested. *@table_name* is **nvarchar(517)**, with a default of `NULL`.

If *@table_name* is omitted, full-text index column information is retrieved for every full-text indexed table.

#### [ @column_name = ] N'*column_name*'

The name of the column for which full-text index metadata is desired. *@column_name* is **sysname**, with a default of `NULL`. If *@column_name* is omitted or is `NULL`, full-text column information is returned for every full-text indexed column for *@table_name*.

If *@table_name* is also omitted or is `NULL`, full-text index column information is returned for every full-text indexed column for all tables in the database.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `TABLE_OWNER` | **sysname** | Table owner. The owner is the name of the database user that created the table. |
| `TABLE_ID` | **int** | ID of the table. |
| `TABLE_NAME` | **sysname** | Table name. |
| `FULLTEXT_COLUMN_NAME` | **sysname** | Column in a full-text indexed table that is designated for indexing. |
| `FULLTEXT_COLID` | **int** | Column ID of the full-text indexed column. |
| `FULLTEXT_BLOBTP_COLNAME` | **sysname** | Column in a full-text indexed table that specifies the document type of the full-text indexed column. This value is only applicable when the full-text indexed column is a **varbinary(max)** or **image** column. |
| `FULLTEXT_BLOBTP_COLID` | **int** | Column ID of the document type column. This value is only applicable when the full-text indexed column is a **varbinary(max)** or **image** column. |
| `FULLTEXT_LANGUAGE` | **sysname** | Language used for the full-text search of the column. |

## Permissions

Execute permissions default to members of the **public** role.

## Examples

The following example returns information about the columns that are designated for full-text indexing in all of the tables in the database.

```sql
USE AdventureWorks2022;
GO
DECLARE @mycursor CURSOR;
EXEC sp_help_fulltext_columns_cursor @mycursor OUTPUT
FETCH NEXT FROM @mycursor;
WHILE (@@FETCH_STATUS <> -1)
   BEGIN
      FETCH NEXT FROM @mycursor;
   END;
CLOSE @mycursor;
DEALLOCATE @mycursor;
GO
```

## Related content

- [COLUMNPROPERTY (Transact-SQL)](../../t-sql/functions/columnproperty-transact-sql.md)
- [sp_fulltext_column (Transact-SQL)](sp-fulltext-column-transact-sql.md)
- [sp_help_fulltext_columns (Transact-SQL)](sp-help-fulltext-columns-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
