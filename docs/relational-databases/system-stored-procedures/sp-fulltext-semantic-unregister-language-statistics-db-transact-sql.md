---
title: "sp_fulltext_semantic_unregister_language_statistics_db (Transact-SQL)"
description: Unregisters an existing Semantic Language Statistics database from the current instance of SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/07/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_fulltext_semantic_unregister_language_statistics_db_TSQL"
  - "sp_fulltext_semantic_unregister_language_statistics_db"
helpviewer_keywords:
  - "sp_fulltext_semantic_unregister_language_statistics_db"
dev_langs:
  - "TSQL"
---
# sp_fulltext_semantic_unregister_language_statistics_db (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Unregisters an existing Semantic Language Statistics database from the current instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and deletes any associated metadata.

This statement doesn't detach the database or remove the physical database file from the file system. After you unregister the database, you can detach it and delete the physical database file.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_fulltext_semantic_unregister_language_statistics_db
[ ; ]
```

## Arguments

This procedure doesn't require any arguments. Since an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] only has one semantic language statistics database, it isn't necessary to identify the database.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

When a Semantic Language Statistics database is unregistered, all the metadata associated with it is also removed.

`sp_fulltext_semantic_unregister_language_statistics_db` performs the following steps:

1. Checks that there are no semantic populations in progress for the current instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

1. Removes all metadata associated with the specified Semantic Language Statistics database.

For more information, see [Install and Configure Semantic Search](../search/install-and-configure-semantic-search.md).

## Metadata

For information about the Semantic Language Statistics database installed on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], query the catalog view [sys.fulltext_semantic_language_statistics_database](../system-catalog-views/sys-fulltext-semantic-language-statistics-database-transact-sql.md).

## Permissions

Requires CONTROL SERVER permissions.

## Examples

The following example shows how to unregister the Semantic Language Statistics database by calling `sp_fulltext_semantic_unregister_language_statistics_db`.

```sql
EXEC sp_fulltext_semantic_unregister_language_statistics_db;
GO
```

## Related content

- [Install and Configure Semantic Search](../search/install-and-configure-semantic-search.md)
