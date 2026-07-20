---
title: Manage and Monitor Semantic Search
description: Manage and monitor Semantic Search
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/20/2026
ms.service: sql
ms.subservice: search
ms.topic: how-to
helpviewer_keywords:
  - "semantic search [SQL Server], managing"
  - "semantic search [SQL Server], monitoring"
---
# Manage and monitor Semantic Search

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article covers how to check indexing status and size, force population, and disable or re-enable semantic indexing. Semantic indexing runs in two phases in conjunction with full-text indexing.

<a id="HowToMonitorStatus"></a>

## Check the status of semantic indexing

### Is the first phase of semantic indexing complete?

Query the [sys.dm_fts_index_population](../system-dynamic-management-objects/sys-dm-fts-index-population-transact-sql.md) dynamic management view, and check the `status` and `status_description` columns.

The first phase of indexing includes the population of the full-text keyword index and the semantic key phrase index, as well as the extraction of document similarity data.

```sql
USE database_name;
GO

SELECT *
FROM sys.dm_fts_index_population
WHERE table_id = OBJECT_ID('table_name');
GO
```

### Is the second phase of semantic indexing complete?

Query the [sys.dm_fts_semantic_similarity_population](../system-dynamic-management-objects/sys-dm-fts-semantic-similarity-population-transact-sql.md) dynamic management view, and check the `status` and `status_description` columns.

The second phase of indexing includes the population of the semantic document similarity index.

```sql
USE database_name;
GO

SELECT *
FROM sys.dm_fts_semantic_similarity_population
WHERE table_id = OBJECT_ID('table_name');
GO
```

<a id="HowToCheckSize"></a>

## Check the size of the semantic indexes

### What is the logical size of a semantic key phrase index or a semantic document similarity index?

Query the [sys.dm_db_fts_index_physical_stats](../system-dynamic-management-objects/sys-dm-db-fts-index-physical-stats-transact-sql.md) dynamic management view.

The logical size is displayed in number of index pages.

```sql
USE database_name;
GO

SELECT *
FROM sys.dm_db_fts_index_physical_stats
WHERE object_id = OBJECT_ID('table_name');
GO
```

### What is the total size of the full-text and semantic indexes for a full-text catalog?

Query the `IndexSize` property of the [FULLTEXTCATALOGPROPERTY](../../t-sql/functions/fulltextcatalogproperty-transact-sql.md) metadata function.

```sql
SELECT FULLTEXTCATALOGPROPERTY('catalog_name', 'IndexSize');
GO
```

### How many items are indexed in the full-text and semantic indexes for a full-text catalog?

Query the `ItemCount` property of the [FULLTEXTCATALOGPROPERTY](../../t-sql/functions/fulltextcatalogproperty-transact-sql.md) metadata function.

```sql
SELECT FULLTEXTCATALOGPROPERTY('catalog_name', 'ItemCount');
GO
```

<a id="HowToForcePopulation"></a>

## Force the population of the semantic indexes

You can force the population of full-text and semantic indexes by using the `START`, `STOP`, `PAUSE`, or `RESUME POPULATION` clause with the same syntax and behavior that is described for full-text indexes. For more information, see [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md) and [Populate Full-Text Indexes](populate-full-text-indexes.md).

Since semantic indexing is dependent on full-text indexing, semantic indexes are only populated when the associated full-text indexes are populated.

### Example: Start a full population of full-text and semantic indexes

The following example starts full population of both full-text and semantic indexes by altering an existing full-text index on the `Production.Document` table in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] sample database.

```sql
USE AdventureWorks2025;
GO

ALTER FULLTEXT INDEX ON Production.Document
    START FULL POPULATION;
GO
```

<a id="HowToDisableIndexing"></a>

## Disable or re-enable semantic indexing

You can enable or disable full-text or semantic indexing by using the `ENABLE` or `DISABLE` clause with the same syntax and behavior that is described for full-text indexes. For more information, see [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md).

When semantic indexing is disabled and suspended, queries over semantic data continue to work successfully and to return previously indexed data. This behavior isn't consistent with the behavior of Full-Text Search.

```sql
-- To disable semantic indexing on a table
USE database_name;
GO

ALTER FULLTEXT INDEX ON table_name DISABLE;
GO

-- To re-enable semantic indexing on a table
USE database_name;
GO

ALTER FULLTEXT INDEX ON table_name ENABLE;
GO
```

<a id="SemanticIndexing"></a>

## About the phases of semantic indexing

Semantic Search indexes two kinds of data for each column on which it's enabled:

1. **Key phrases**

1. **Document similarity**

Semantic indexing occurs in two phases, in conjunction with full-text indexing:

1. **Phase 1**. The full-text keyword index and the semantic key phrase index are populated in parallel at the same time. The data required to index document similarity is also extracted at this time.

1. **Phase 2**. The semantic document similarity index is then populated. This index depends on both indexes that were populated in the preceding phase.

<a id="BestPracticeUnderstand"></a>

<a id="ProblemNotPopulated"></a>

## Issue: Semantic indexes aren't populated

### Are the associated full-text indexes populated?

Since semantic indexing is dependent on full-text indexing, semantic indexes are only populated when the associated full-text indexes are populated.

### Are Full-Text Search and Semantic Search properly installed and configured?

For more information, see [Install and Configure Semantic Search](install-and-configure-semantic-search.md).

### Is the fdhost service not available, or is there another condition that would cause full-text indexing to fail?

For more information, see [Troubleshoot full-text indexing](troubleshoot-full-text-indexing.md).
