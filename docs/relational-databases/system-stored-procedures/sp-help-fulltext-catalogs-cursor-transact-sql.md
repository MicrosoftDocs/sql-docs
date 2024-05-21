---
title: "sp_help_fulltext_catalogs_cursor (Transact-SQL)"
description: Uses a cursor to return the ID, name, root directory, status, and number of full-text indexed tables for the specified full-text catalog.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_fulltext_catalogs_cursor"
  - "sp_help_fulltext_catalogs_cursor_TSQL"
helpviewer_keywords:
  - "sp_help_fulltext_catalogs_cursor"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_help_fulltext_catalogs_cursor (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Uses a cursor to return the ID, name, root directory, status, and number of full-text indexed tables for the specified full-text catalog.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use the [sys.fulltext_catalogs](../system-catalog-views/sys-fulltext-catalogs-transact-sql.md) catalog view instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_fulltext_catalogs_cursor
    [ @cursor_return = ] cursor_return OUTPUT
    [ , [ @fulltext_catalog_name = ] N'fulltext_catalog_name' ]
[ ; ]
```

## Arguments

#### [ @cursor_return = ] *cursor_return* OUTPUT

*@cursor_return* is an OUTPUT parameter of type **int**. The cursor is a read-only, scrollable, dynamic cursor.

#### [ @fulltext_catalog_name = ] N'*fulltext_catalog_name*'

The name of the full-text catalog. *@fulltext_catalog_name* is **sysname**, with a default of `NULL`. If this parameter is omitted or is `NULL`, information about all full-text catalogs associated with the current database is returned.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `fulltext_catalog_id` | **smallint** | Full-text catalog identifier. |
| `NAME` | **sysname** | Name of the full-text catalog. |
| `PATH` | **nvarchar(260)** | This clause has no effect. |
| `STATUS` | **int** | Full-text index population status of the catalog:<br /><br />`0` = Idle<br />`1` = Full population in progress<br />`2` = Paused<br />`3` = Throttled<br />`4` = Recovering<br />`5` = Shutdown<br />`6` = Incremental population in progress<br />`7` = Building index<br />`8` = Disk is full. Paused<br />`9` = Change tracking |
| `NUMBER_FULLTEXT_TABLES` | **int** | Number of full-text indexed tables associated with the catalog. |

## Permissions

Execute permissions default to the **public** role.

## Examples

The following example returns information about the `Cat_Desc` full-text catalog.

```sql
USE AdventureWorks2022;
GO
DECLARE @mycursor CURSOR;
EXEC sp_help_fulltext_catalogs_cursor @mycursor OUTPUT, 'Cat_Desc';
FETCH NEXT FROM @mycursor;
WHILE (@@FETCH_STATUS <> -1)
   BEGIN
      FETCH NEXT FROM @mycursor;
   END
CLOSE @mycursor;
DEALLOCATE @mycursor;
GO
```

## Related content

- [FULLTEXTCATALOGPROPERTY (Transact-SQL)](../../t-sql/functions/fulltextcatalogproperty-transact-sql.md)
- [sp_fulltext_catalog (Transact-SQL)](sp-fulltext-catalog-transact-sql.md)
- [sp_help_fulltext_catalogs (Transact-SQL)](sp-help-fulltext-catalogs-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
