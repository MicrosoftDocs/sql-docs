---
title: "sp_help_fulltext_catalogs (Transact-SQL)"
description: Returns the ID, name, root directory, status, and number of full-text indexed tables for the specified full-text catalog.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_fulltext_catalogs_TSQL"
  - "sp_help_fulltext_catalogs"
helpviewer_keywords:
  - "sp_help_fulltext_catalogs"
dev_langs:
  - "TSQL"
---
# sp_help_fulltext_catalogs (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the ID, name, root directory, status, and number of full-text indexed tables for the specified full-text catalog.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use the [sys.fulltext_catalogs](../system-catalog-views/sys-fulltext-catalogs-transact-sql.md) catalog view instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_fulltext_catalogs [ [ @fulltext_catalog_name = ] N'fulltext_catalog_name' ]
[ ; ]
```

## Arguments

#### [ @fulltext_catalog_name = ] N'*fulltext_catalog_name*'

The name of the full-text catalog. *@fulltext_catalog_name* is **sysname**, with a default of `NULL`. If this parameter is omitted or has the value `NULL`, information about all full-text catalogs associated with the current database is returned.

## Return code values

`0` (success) or `1` (failure).

## Result set

This table shows the result set, which is ordered by `fulltext_catalog_id`.

| Column name | Data type | Description |
| --- | --- | --- |
| `fulltext_catalog_id` | **smallint** | Full-text catalog identifier. |
| `NAME` | **sysname** | Name of the full-text catalog. |
| `PATH` | **nvarchar(260)** | This clause has no effect. |
| `STATUS` | **int** | Full-text index population status of the catalog:<br /><br />`0` = Idle<br />`1` = Full population in progress<br />`2` = Paused<br />`3` = Throttled<br />`4` = Recovering<br />`5` = Shutdown<br />`6` = Incremental population in progress<br />`7` = Building index<br />`8` = Disk is full. Paused<br />`9` = Change tracking<br /><br />`NULL` = User doesn't have `VIEW` permission on the full-text catalog, or database isn't full-text enabled, or full-text component not installed. |
| `NUMBER_FULLTEXT_TABLES` | **int** | Number of full-text indexed tables associated with the catalog. |

## Permissions

Execute permissions default to members of the **public** role.

## Examples

The following example returns information about the `Cat_Desc` full-text catalog.

```sql
USE AdventureWorks2022;
GO
EXEC sp_help_fulltext_catalogs 'Cat_Desc';
GO
```

## Related content

- [FULLTEXTCATALOGPROPERTY (Transact-SQL)](../../t-sql/functions/fulltextcatalogproperty-transact-sql.md)
- [sp_fulltext_catalog (Transact-SQL)](sp-fulltext-catalog-transact-sql.md)
- [sp_help_fulltext_catalogs_cursor (Transact-SQL)](sp-help-fulltext-catalogs-cursor-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
