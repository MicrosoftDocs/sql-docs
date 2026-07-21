---
title: "Full-Text Search DDL, Functions, Stored Procedures, and Views"
description: "Full-Text Search DDL, Functions, Stored Procedures, and Views"
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/19/2026
ms.service: sql
ms.subservice: search
ms.topic: reference
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Full-Text Search DDL, functions, stored procedures, and views

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Lists the Transact-SQL statements and the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database objects that support full-text search, including the property search feature.

This list doesn't include deprecated objects.

For the list of database objects that support semantic search, see [Semantic Search DDL, Functions, Stored Procedures, and Views](semantic-search-ddl-functions-stored-procedures-and-views.md).

<a id="ddl"></a>

## Transact-SQL Data Definition Language (DDL) statements

Use these statements to define, modify, and remove full-text search components such as catalogs, indexes, stoplists, and property lists. Typically, you use these statements when preparing a database or table for full-text search or when updating its configuration.

| Component | Create | Alter | Drop |
| --- | --- | --- | --- |
| **Full-text catalog**: Define a logical container for full-text indexes. | [CREATE](../../t-sql/statements/create-fulltext-catalog-transact-sql.md) | [ALTER](../../t-sql/statements/alter-fulltext-catalog-transact-sql.md) | [DROP](../../t-sql/statements/drop-fulltext-catalog-transact-sql.md) |
| **Full-text index**: Define on a table column to enable full-text querying. | [CREATE](../../t-sql/statements/create-fulltext-index-transact-sql.md) | [ALTER](../../t-sql/statements/alter-fulltext-index-transact-sql.md) | [DROP](../../t-sql/statements/drop-fulltext-index-transact-sql.md) |
| **Full-text stoplist**: Define a list of words to be ignored during full-text indexing. | [CREATE](../../t-sql/statements/create-fulltext-stoplist-transact-sql.md) | [ALTER](../../t-sql/statements/alter-fulltext-stoplist-transact-sql.md) | [DROP](../../t-sql/statements/drop-fulltext-stoplist-transact-sql.md) |
| **Search property list**: Defines a mapping of document property names to MIME types for use in property search. | [CREATE](../../t-sql/statements/create-search-property-list-transact-sql.md) | [ALTER](../../t-sql/statements/alter-search-property-list-transact-sql.md) | [DROP](../../t-sql/statements/drop-search-property-list-transact-sql.md) |

<a id="func"></a>

## System predicates and functions

Full-text search extends Transact-SQL with predicates and table-valued functions that you can use to perform sophisticated text matching in queries.

| Object | Description |
| --- | --- |
| [CONTAINS](../../t-sql/queries/contains-transact-sql.md) | Predicate that tests whether specified text values meet full-text criteria (for example, words or phrases). |
| [CONTAINSTABLE](../system-functions/containstable-transact-sql.md) | Table-valued function returning key values and rank of qualifying rows for a full-text query. |
| [FREETEXT](../../t-sql/queries/freetext-transact-sql.md) | Predicate that matches text values against a natural-language search phrase. |
| [FREETEXTTABLE](../system-functions/freetexttable-transact-sql.md) | Table-valued function similar to `FREETEXT`, returning ranking information. |

<a id="meta"></a>

## System metadata functions

These functions return metadata values related to full-text search or object properties. They're also used more broadly throughout SQL Server metadata queries.

| Object | Description |
| --- | --- |
| [COLUMNPROPERTY](../../t-sql/functions/columnproperty-transact-sql.md) | Returns information about a column's properties (for example, whether it is computed). |
| [FULLTEXTCATALOGPROPERTY](../../t-sql/functions/fulltextcatalogproperty-transact-sql.md) | Returns property values for a full-text catalog. |
| [FULLTEXTSERVICEPROPERTY](../../t-sql/functions/fulltextserviceproperty-transact-sql.md) | Returns values for full-text service configuration. |
| [INDEXPROPERTY](../../t-sql/functions/indexproperty-transact-sql.md) | Returns information about an index. |
| [OBJECTPROPERTY](../../t-sql/functions/objectproperty-transact-sql.md) | Returns information about database objects. |
| [OBJECTPROPERTYEX](../../t-sql/functions/objectpropertyex-transact-sql.md) | Similar to `OBJECTPROPERTY` with extended property types. |
| [SERVERPROPERTY](../../t-sql/functions/serverproperty-transact-sql.md) | Returns information about the SQL Server instance. |

<a id="proc"></a>

## System stored procedures

These stored procedures support management and diagnostic operations for full-text search subsystems.

| Object | Description |
| --- | --- |
| [sp_fulltext_keymappings](../system-stored-procedures/sp-fulltext-keymappings-transact-sql.md) | Reports mappings between full-text keys and index entries. This information is useful for understanding how key values relate to full-text indexes. |
| [sp_fulltext_load_thesaurus_file](../system-stored-procedures/sp-fulltext-load-thesaurus-file-transact-sql.md) | Loads a thesaurus file for full-text search. Thesaurus files provide synonyms and expansions. |
| [sp_fulltext_pendingchanges](../system-stored-procedures/sp-fulltext-pendingchanges-transact-sql.md) | Returns size or estimated status of pending full-text index changes. |
| [sp_fulltext_service](../system-stored-procedures/sp-fulltext-service-transact-sql.md) | Provides administrative control over the full-text search service settings. |
| [sp_help_fulltext_system_components](../system-stored-procedures/sp-help-fulltext-system-components-transact-sql.md) | Lists full-text search system components and their status. |

<a id="cat"></a>

## System catalog views

These views expose full-text search metadata defined in the database schema.

| Object | Description |
| --- | --- |
| [sys.fulltext_catalogs](../system-catalog-views/sys-fulltext-catalogs-transact-sql.md) | Lists full-text catalogs defined in the database. |
| [sys.fulltext_document_types](../system-catalog-views/sys-fulltext-document-types-transact-sql.md) | Shows document types registered for full-text indexing. |
| [sys.fulltext_index_catalog_usages](../system-catalog-views/sys-fulltext-index-catalog-usages-transact-sql.md) | Shows associations between full-text indexes and catalogs. |
| [sys.fulltext_index_columns](../system-catalog-views/sys-fulltext-index-columns-transact-sql.md) | Lists the columns participating in full-text indexes. |
| [sys.fulltext_index_fragments](../system-catalog-views/sys-fulltext-index-fragments-transact-sql.md) | Provides fragmentation metadata for full-text indexes. |
| [sys.fulltext_indexes](../system-catalog-views/sys-fulltext-indexes-transact-sql.md) | Lists tables that have a full-text index and key index information. |
| [sys.fulltext_languages](../system-catalog-views/sys-fulltext-languages-transact-sql.md) | Lists languages supported for full-text indexing. |
| [sys.fulltext_stoplists](../system-catalog-views/sys-fulltext-stoplists-transact-sql.md) | Lists stoplists available in the database. |
| [sys.fulltext_stopwords](../system-catalog-views/sys-fulltext-stopwords-transact-sql.md) | Lists stopwords defined in custom stoplists. |
| [sys.fulltext_system_stopwords](../system-catalog-views/sys-fulltext-system-stopwords-transact-sql.md) | Lists system stopwords built into SQL Server. |
| [sys.registered_search_properties](../system-catalog-views/sys-registered-search-properties-transact-sql.md) | Lists search properties registered for property search. |
| [sys.registered_search_property_lists](../system-catalog-views/sys-registered-search-property-lists-transact-sql.md) | Lists property lists used by property search. |

<a id="dmv"></a>

## System dynamic management views

These DMVs provide real-time monitoring and internal status information related to full-text indexing and population.

| Object | Description |
| --- | --- |
| [sys.dm_fts_active_catalogs](../system-dynamic-management-views/sys-dm-fts-active-catalogs-transact-sql.md) | Shows full-text catalogs currently active. |
| [sys.dm_fts_fdhosts](../system-dynamic-management-views/sys-dm-fts-fdhosts-transact-sql.md) | Details host processes supporting full-text indexing. |
| [sys.dm_fts_index_keywords](../system-dynamic-management-views/sys-dm-fts-index-keywords-transact-sql.md) | Lists keywords stored in full-text indexes. |
| [sys.dm_fts_index_keywords_by_document](../system-dynamic-management-views/sys-dm-fts-index-keywords-by-document-transact-sql.md) | Keywords mapped by document. |
| [sys.dm_fts_index_keywords_by_property](../system-dynamic-management-views/sys-dm-fts-index-keywords-by-property-transact-sql.md) | Keywords mapped by registered property. |
| [sys.dm_fts_index_population](../system-dynamic-management-views/sys-dm-fts-index-population-transact-sql.md) | Tracks population status of full-text indexes. |
| [sys.dm_fts_memory_buffers](../system-dynamic-management-views/sys-dm-fts-memory-buffers-transact-sql.md) | Shows memory buffer usage for full-text indexing. |
| [sys.dm_fts_memory_pools](../system-dynamic-management-views/sys-dm-fts-memory-pools-transact-sql.md) | Provides memory pool statistics for full-text search. |
| [sys.dm_fts_outstanding_batches](../system-dynamic-management-views/sys-dm-fts-outstanding-batches-transact-sql.md) | Reports outstanding index update batches. |
| [sys.dm_fts_parser](../system-dynamic-management-views/sys-dm-fts-parser-transact-sql.md) | Examines how text is parsed into tokens for full-text indexing. |
| [sys.dm_fts_population_ranges](../../relational-databases/system-dynamic-management-views/sys-dm-fts-population-ranges-transact-sql.md) | Shows ranges of data being processed during index population. |
