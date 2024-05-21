---
title: "sp_help_fulltext_tables_cursor (Transact-SQL)"
description: sp_help_fulltext_tables_cursor uses a cursor to return a list of tables that are registered for full-text indexing.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 12/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_fulltext_tables_cursor"
  - "sp_help_fulltext_tables_cursor_TSQL"
helpviewer_keywords:
  - "sp_help_fulltext_tables_cursor"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_help_fulltext_tables_cursor (Transact-SQL)

[!INCLUDE [sql-asdbmi-pdw](../../includes/applies-to-version/sql-asdbmi-pdw.md)]

Uses a cursor to return a list of tables that are registered for full-text indexing.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use the new `sys.fulltext_indexes` catalog view instead. For more information, see [sys.fulltext_indexes (Transact-SQL)](../system-catalog-views/sys-fulltext-indexes-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_fulltext_tables_cursor
     [ @cursor_return = ] cursor_return OUTPUT
     [ , [ @fulltext_catalog_name = ] N'fulltext_catalog_name' ]
     [ , [ @table_name = ] N'table_name' ]
[ ; ]
```

## Arguments

#### [ @cursor_return = ] *cursor_return* OUTPUT

The output variable of type **cursor**. The cursor is a read-only, scrollable, dynamic cursor.

#### [ @fulltext_catalog_name = ] N'*fulltext_catalog_name*'

The name of the full-text catalog. *@fulltext_catalog_name* is **sysname**, with a default of `NULL`.

- If *@fulltext_catalog_name* is omitted or is `NULL`, all full-text indexed tables associated with the database are returned.

- If *@fulltext_catalog_name* is specified, but *@table_name* is omitted or is `NULL`, the full-text index information is retrieved for every full-text indexed table associated with this catalog.

- If both *@fulltext_catalog_name* and *@table_name* are specified, a row is returned if *@table_name* is associated with *@fulltext_catalog_name*; otherwise, an error is raised.

#### [ @table_name = ] N'*table_name*'

The one-part or two-part table name for which the full-text metadata is requested. *@table_name* is **nvarchar(517)**, with a default value of `NULL`. If only *@table_name* is specified, only the row relevant to *@table_name* is returned.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `TABLE_OWNER` | **sysname** | Table owner. This is the name of the database user that created the table. |
| `TABLE_NAME` | **sysname** | Table name. |
| `FULLTEXT_KEY_INDEX_NAME` | **sysname** | Index imposing the `UNIQUE` constraint on the column designated as the unique key column. |
| `FULLTEXT_KEY_COLID` | **int** | Column ID of the unique index identified by `FULLTEXT_KEY_INDEX_NAME`. |
| `FULLTEXT_INDEX_ACTIVE` | **int** | Specifies whether columns marked for full-text indexing in this table are eligible for queries:<br /><br />`0` = Inactive<br />1 = Active |
| `FULLTEXT_CATALOG_NAME` | **sysname** | Full-text catalog in which the full-text index data resides. |

## Permissions

Execute permissions default to members of the **public** role.

## Examples

The following example returns the names of the full-text indexed tables associated with the `Cat_Desc` full-text catalog.

```sql
USE AdventureWorks2022;
GO
DECLARE @mycursor CURSOR;
EXEC sp_help_fulltext_tables_cursor @mycursor OUTPUT, 'Cat_Desc';
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

- [INDEXPROPERTY (Transact-SQL)](../../t-sql/functions/indexproperty-transact-sql.md)
- [OBJECTPROPERTY (Transact-SQL)](../../t-sql/functions/objectproperty-transact-sql.md)
- [sp_fulltext_table (Transact-SQL)](sp-fulltext-table-transact-sql.md)
- [sp_help_fulltext_tables (Transact-SQL)](sp-help-fulltext-tables-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
