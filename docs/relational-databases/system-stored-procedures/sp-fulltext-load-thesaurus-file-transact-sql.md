---
title: "sp_fulltext_load_thesaurus_file (Transact-SQL)"
description: Causes the server instance to parse and load the data from the thesaurus file that corresponds to the language whose LCID is specified.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/07/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_fulltext_load_thesaurus_file"
  - "sp_fulltext_load_thesaurus_file_TSQL"
helpviewer_keywords:
  - "sp_fulltext_load_thesaurus_file"
  - "full-text indexes [SQL Server], thesaurus files"
  - "thesaurus [full-text search], editing"
dev_langs:
  - "TSQL"
---
# sp_fulltext_load_thesaurus_file (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Causes the server instance to parse and load the data from the thesaurus file that corresponds to the language whose LCID is specified. This stored procedure is useful after updating a thesaurus file. Executing `sp_fulltext_load_thesaurus_file` causes recompilation of full-text queries that use the thesaurus of the specified LCID.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_fulltext_load_thesaurus_file
    [ @lcid = ] lcid
    [ , [ @loadOnlyIfNotLoaded = ] loadOnlyIfNotLoaded ]
[ ; ]
```

## Arguments

#### [ @lcid = ] *lcid*

Integer mapping the locale identifier (LCID) of the language for which you want to load the thesaurus XML definition. *@lcid* is **int**, with no default. To obtain the LCIDs of languages that are available on a server instance, use the [sys.fulltext_languages (Transact-SQL)](../system-catalog-views/sys-fulltext-languages-transact-sql.md) catalog view.

#### [ @loadOnlyIfNotLoaded = ] *loadOnlyIfNotLoaded*

Specifies whether the thesaurus file is loaded into the internal thesaurus tables even if it's already been loaded. *@loadOnlyIfNotLoaded* is **bit**, with a default of `0`, and the following possible values:

| Value | Definition |
| --- | --- |
| `0` | Load the thesaurus file regardless of whether it's already loaded. This is the default behavior of `sp_fulltext_load_thesaurus_file`. |
| `1` | Load the thesaurus file only if it isn't yet loaded. |

## Return code values

None.

## Result set

None.

## Remarks

Thesaurus files are automatically loaded by full-text queries that use the thesaurus. To avoid this first-time performance impact on full-text queries, we recommend that you execute `sp_fulltext_load_thesaurus_file`.

Use `sp_fulltext_service 'update_languages'` to update the list of languages registered with full-text search. For more information, see [sp_fulltext_service](sp-fulltext-service-transact-sql.md).

## Permissions

Only members of the **sysadmin** fixed server role or the system administrator can execute the `sp_fulltext_load_thesaurus_file` stored procedure.

Only system administrators can update, modify, or delete thesaurus files.

## Examples

### A. Load a thesaurus file even if it's already loaded

The following example parses and loads the English thesaurus file.

```sql
EXEC sys.sp_fulltext_load_thesaurus_file 1033;
```

### B. Load a thesaurus file only if it isn't yet loaded

The following example parses and loads the Arabic thesaurus file, unless it's already loaded.

```sql
EXEC sys.sp_fulltext_load_thesaurus_file 1025, @loadOnlyIfNotLoaded = 1;
```

## Related content

- [FULLTEXTSERVICEPROPERTY (Transact-SQL)](../../t-sql/functions/fulltextserviceproperty-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Configure and Manage Thesaurus Files for Full-Text Search](../search/configure-and-manage-thesaurus-files-for-full-text-search.md)
