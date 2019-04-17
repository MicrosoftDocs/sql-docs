---
title: "Manage and Monitor Semantic Search | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "semantic search [SQL Server], managing"
  - "semantic search [SQL Server], monitoring"
ms.assetid: eb5c3b29-da70-42aa-aa97-7d35a3f1eb98
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Manage and Monitor Semantic Search
  Describes the process of semantic indexing and the tasks related to managing and monitoring the indexes.  
  
##  <a name="HowToMonitorStatus"></a> How To: Check the Status of Semantic Indexing  
 **Is the first phase of semantic indexing complete?**  
 Query the dynamic management view, [sys.dm_fts_index_population &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-index-population-transact-sql), and check the **status** and **status_description** columns.  
  
 The first phase of indexing includes the population of the full-text keyword index and the semantic key phrase index, as well as the extraction of document similarity data.  
  
```sql  
USE database_name  
GO  
  
SELECT * FROM sys.dm_fts_index_population WHERE table_id = OBJECT_ID('table_name')  
GO  
```  
  
 **Is the second phase of semantic indexing complete?**  
 Query the dynamic management view, [sys.dm_fts_semantic_similarity_population &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-semantic-similarity-population-transact-sql), and check the **status** and **status_description** columns..  
  
 The second phase of indexing includes the population of the semantic document similarity index.  
  
```wql  
USE database_name  
GO  
  
SELECT * FROM sys.dm_fts_semantic_similarity_population WHERE table_id = OBJECT_ID('table_name')  
GO  
```  
  
##  <a name="HowToCheckSize"></a> How To: Check the Size of the Semantic Indexes  
 **What is the logical size of a semantic key phrase index or a semantic document similarity index?**  
 Query the dynamic management view, [sys.dm_db_fts_index_physical_stats &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-fts-index-physical-stats-transact-sql).  
  
 The logical size is displayed in number of index pages.  
  
```sql  
USE database_name  
GO  
  
SELECT * FROM sys.dm_db_fts_index_physical_stats WHERE object_id = OBJECT_ID('table_name')  
GO  
```  
  
 **What is the total size of the full-text and semantic indexes for a full-text catalog?**  
 Query the **IndexSize** property of the [FULLTEXTCATALOGPROPERTY &#40;Transact-SQL&#41;](/sql/t-sql/functions/fulltextcatalogproperty-transact-sql) metadata function.  
  
```sql  
SELECT FULLTEXTCATALOGPROPERTY('catalog_name', 'IndexSize')  
GO  
```  
  
 **How many items are indexed in the full-text and semantic indexes for a full-text catalog?**  
 Query the **ItemCount** property of the [FULLTEXTCATALOGPROPERTY &#40;Transact-SQL&#41;](/sql/t-sql/functions/fulltextcatalogproperty-transact-sql) metadata function.  
  
```sql  
SELECT FULLTEXTCATALOGPROPERTY('catalog_name', 'ItemCount')  
GO  
```  
  
##  <a name="HowToForcePopulation"></a> How To: Force the Population of the Semantic Indexes  
 You can force the population of full-text and semantic indexes by using the START/STOP/PAUSE or RESUME POPULATION clause with the same syntax and behavior that is described for full-text indexes. For more information, see [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-fulltext-index-transact-sql) and [Populate Full-Text Indexes](../indexes/indexes.md).  
  
 Since semantic indexing is dependent on full-text indexing, semantic indexes are only populated when the associated full-text indexes are populated.  
  
 **Example: Start a full population of full-text and semantic indexes**  
  
 The following example starts full population of both full-text and semantic indexes by altering an existing full-text index on the **Production.Document** table in the AdventureWorks2012 sample database.  
  
```vb  
USE AdventureWorks2012  
GO  
  
ALTER FULLTEXT INDEX ON Production.Document  
    START FULL POPULATION  
GO  
```  
  
##  <a name="HowToDisableIndexing"></a> How To: Disable or Re-enable Semantic Indexing  
 You can enable or disable full-text or semantic indexing by using the ENABLE/DISABLE clause with the same syntax and behavior that is described for full-text indexes. For more information, see [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-fulltext-index-transact-sql).  
  
 When semantic indexing is disabled and suspended, queries over semantic data continue to work successfully and to return previously indexed data. This behavior is not consistent with the behavior of Full-Text Search.  
  
```sql  
-- To disable semantic indexing on a table  
USE database_name  
GO  
  
ALTER FULLTEXT INDEX ON table_name DISABLE  
GO  
  
-- To re-enable semantic indexing on a table  
USE database_name  
GO  
  
ALTER FULLTEXT INDEX ON table_name ENABLE  
GO  
```  
  
##  <a name="SemanticIndexing"></a> Phases of Semantic Indexing  
 Semantic Search indexes two kinds of data for each column on which it is enabled:  
  
1.  **Key phrases**  
  
2.  **Document similarity**  
  
 Semantic indexing occurs in two phases, in conjunction with full-text indexing:  
  
1.  **Phase 1**. The full-text keyword index and the semantic key phrase index are populated in parallel at the same time. The data required to index document similarity is also extracted at this time.  
  
2.  **Phase 2**. The semantic document similarity index is then populated. This index depends on both indexes that were populated in the preceding phase.  
  
##  <a name="BestPracticeUnderstand"></a>   
##  <a name="ProblemNotPopulated"></a> Problem: Semantic Indexes Are Not Populated  
 **Are the associated full-text indexes populated?**  
 Since semantic indexing is dependent on full-text indexing, semantic indexes are only populated when the associated full-text indexes are populated.  
  
 **Are full-text search and semantic search properly installed and configured?**  
 For more information, see [Install and Configure Semantic Search](install-and-configure-semantic-search.md).  
  
 **Is the FDHOST service not available, or is there another condition that would cause full-text indexing to fail?**  
 For more information, see [Troubleshoot Full-Text Indexing](troubleshoot-full-text-indexing.md).  
  
  
