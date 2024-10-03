---
title: "Full-Text Search and Semantic Search stored procedures (Transact-SQL)"
description: "Full-Text Search and Semantic Search stored procedures (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "full-text indexes [SQL Server], stored procedures"
  - "full-text search [SQL Server], stored procedures"
  - "full-text catalogs [SQL Server], stored procedures"
  - "system stored procedures [SQL Server], full-text search"
dev_langs:
  - "TSQL"
---
# Full-Text Search and Semantic Search stored procedures (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports the following system stored procedures that are used to implement and query full-text indexes and semantic indexes.

## Full-Text Search stored procedures

- [sp_fulltext_catalog](sp-fulltext-catalog-transact-sql.md)

  Creates and drops a full-text catalog, and starts and stops the indexing action for a catalog. Multiple full-text catalogs can be created for each database.

  [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE FULLTEXT CATALOG](../../t-sql/statements/create-fulltext-catalog-transact-sql.md), [ALTER FULLTEXT CATALOG](../../t-sql/statements/alter-fulltext-catalog-transact-sql.md), and [DROP FULLTEXT CATALOG](../../t-sql/statements/drop-fulltext-catalog-transact-sql.md) instead.

- [sp_fulltext_column](sp-fulltext-column-transact-sql.md)

  Specifies whether or not a particular column of a table participates in full-text indexing.

  [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md) instead.

- [sp_fulltext_database](sp-fulltext-database-transact-sql.md)

  Has no effect on full-text catalogs in [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions and is supported for backward compatibility only.

  [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

- [sp_fulltext_keymappings](sp-fulltext-keymappings-transact-sql.md)

  Returns mappings between document identifiers (`DocId`s) and full-text key values.

- [sp_fulltext_load_thesaurus_file](sp-fulltext-load-thesaurus-file-transact-sql.md)

  Parses and loads the data from an updated thesaurus file that corresponds to an LCID and causes recompilation of full-text queries that use the thesaurus.

- [sp_fulltext_pendingchanges](sp-fulltext-pendingchanges-transact-sql.md)

  Returns unprocessed changes, such as pending inserts, updates, and deletes, for a specified table that is using change tracking.

- [sp_fulltext_service](sp-fulltext-service-transact-sql.md)

  Changes the server properties of full-text search for SQL Server.

- [sp_fulltext_table](sp-fulltext-table-transact-sql.md)
  Marks or unmarks a table for full-text indexing.

  [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE FULLTEXT INDEX](../../t-sql/statements/create-fulltext-index-transact-sql.md), [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md), and [DROP FULLTEXT INDEX](../../t-sql/statements/drop-fulltext-index-transact-sql.md) instead.

- [sp_help_fulltext_catalog_components](sp-help-fulltext-catalog-components-transact-sql.md)

  Returns a list of all components (filters, word-breakers, and protocol handlers), used for all full-text catalogs in the current database.

  [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

- [sp_help_fulltext_catalogs](sp-help-fulltext-catalogs-transact-sql.md)

  Returns the ID, name, root directory, status, and number of full-text indexed tables for the specified full-text catalog.

  [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use the [sys.fulltext_catalogs](../system-catalog-views/sys-fulltext-catalogs-transact-sql.md) catalog view instead.

- [sp_help_fulltext_catalogs_cursor](sp-help-fulltext-catalogs-cursor-transact-sql.md)

  Uses a cursor to return the ID, name, root directory, status, and number of full-text indexed tables for the specified full-text catalog.

  [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use the [sys.fulltext_catalogs](../system-catalog-views/sys-fulltext-catalogs-transact-sql.md) catalog view instead.

- [sp_help_fulltext_columns](sp-help-fulltext-columns-transact-sql.md)

  Returns the columns designated for full-text indexing.

  [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use the [sys.fulltext_index_columns](../system-catalog-views/sys-fulltext-index-columns-transact-sql.md) catalog view instead.

- [sp_help_fulltext_columns_cursor](sp-help-fulltext-columns-cursor-transact-sql.md)

  Uses a cursor to return the columns designated for full-text indexing.

  [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use the [sys.fulltext_index_columns](../system-catalog-views/sys-fulltext-index-columns-transact-sql.md) catalog view instead.

- [sp_help_fulltext_system_components](sp-help-fulltext-system-components-transact-sql.md)

  Returns information for the registered word-breakers, filter, and protocol handlers, as well as a list of identifiers of databases and full-text catalogs that have used a specified component.

- [sp_help_fulltext_tables](sp-help-fulltext-tables-transact-sql.md)

  Returns a list of tables that are registered for full-text indexing.

- [sp_help_fulltext_tables_cursor](sp-help-fulltext-tables-cursor-transact-sql.md)

  Returns a list of tables that are registered for full-text indexing.

  [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use the [sys.fulltext_indexes](../system-catalog-views/sys-fulltext-indexes-transact-sql.md) catalog view instead.

## Semantic Search stored procedures

- [sp_fulltext_semantic_register_language_statistics_db](sp-fulltext-semantic-register-language-statistics-db-transact-sql.md)

  Registers a pre-populated Semantic Language Statistics database in the current instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- [sp_fulltext_semantic_unregister_language_statistics_db](sp-fulltext-semantic-unregister-language-statistics-db-transact-sql.md)

  Unregisters an existing Semantic Language Statistics database from the current instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and deletes any associated metadata.

## Related content

- [Full-Text Search and Semantic Search Catalog Views (Transact-SQL)](../system-catalog-views/full-text-search-and-semantic-search-catalog-views-transact-sql.md)
- [Full-Text and Semantic Search Dynamic Management Views - Functions](../system-dynamic-management-views/full-text-and-semantic-search-dynamic-management-views-functions.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Full-Text Search](../search/full-text-search.md)
