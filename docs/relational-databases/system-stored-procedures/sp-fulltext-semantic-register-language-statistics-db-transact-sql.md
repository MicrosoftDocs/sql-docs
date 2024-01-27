---
title: "sp_fulltext_semantic_register_language_statistics_db (Transact-SQL)"
description: Registers a pre-populated Semantic Language Statistics database in the current instance of SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/07/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_fulltext_semantic_register_language_statistics_db"
  - "sp_fulltext_semantic_register_language_statistics_db_TSQL"
helpviewer_keywords:
  - "sp_fulltext_semantic_register_language_statistics_db"
dev_langs:
  - "TSQL"
---
# sp_fulltext_semantic_register_language_statistics_db (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Registers a pre-populated Semantic Language Statistics database in the current instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

You can initiate semantic extraction only after you have attached this language statistics database and registered it by using this stored procedure. You only need to perform this task once for each instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_fulltext_semantic_register_language_statistics_db [ @dbname = ] N'dbname'
[ ; ]
```

## Arguments

#### [ @dbname = ] N'*dbname*'

The name of the Semantic Language Statistics database to be registered for the current instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The database must already be attached. *@dbname* is **sysname**, and can't be `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

The Semantic Language Statistics database contains language-related statistics that are required for semantic processing of textual content.

`sp_fulltext_semantic_register_language_statistics_db` performs the following steps:

1. Checks that the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is a version that supports semantic processing.

1. Checks that the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't already have a Semantic Language Statistics database defined.

1. Checks that the database is a valid Semantic Language Statistics database.

1. Sets permissions on the Semantic Language Statistics database to restrict access to the database by users.

1. Inserts the metadata that defines the name of the Semantic Language Statistics database for the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

1. Inserts the metadata that defines the mappings between the installed Semantic Language Statistics database and the internal Language Model tables.

1. Checks to ensure that the database is ready to be used.

For more information, see [Install and Configure Semantic Search](../search/install-and-configure-semantic-search.md).

## Metadata

For information about the Semantic Language Statistics database installed on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], query the catalog view [sys.fulltext_semantic_language_statistics_database (Transact-SQL)](../system-catalog-views/sys-fulltext-semantic-language-statistics-database-transact-sql.md).

## Permissions

Requires CONTROL SERVER permissions.

## Examples

The following example shows how to register the Semantic Language Statistics database by calling `sp_fulltext_semantic_register_language_statistics_db`.

```sql
EXEC sp_fulltext_semantic_register_language_statistics_db
    @dbname = 'semanticsDb';
GO
```

## Related content

- [Install and Configure Semantic Search](../search/install-and-configure-semantic-search.md)
