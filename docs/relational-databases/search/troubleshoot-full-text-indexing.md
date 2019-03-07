---
title: "Troubleshoot Full-Text Indexing | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "search, sql-database"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "indexes [full-text search]"
  - "troubleshooting [SQL Server], full-text search"
  - "troubleshooting [full-text search]"
ms.assetid: 964c43a8-5019-4179-82aa-63cd0ef592ef
author: douglaslMS
ms.author: douglasl
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Troubleshoot Full-Text Indexing
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
     
##  <a name="failure"></a> Troubleshoot Full-Text Indexing Failures  
 While populating or maintaining a full-text index, the full-text indexer, for reasons described below, might fail to index one or more rows. These row-level errors do not prevent the population from completing. The indexer skips these rows, which means that you are not able to query for content contained in these rows.  
  
 Indexing failures can occur when:  
  
-   The indexer cannot find or load a filter or word breaker component. This failure can occur if the table row contains a document format or content in a language that has not been registered with the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This failure can also happen if the registered word breaker or filter component was not signed or failed signature verification when it was being loaded.  
  
-   A component, such as a word breaker or filter, fails, and returns an error to the indexer. This failure can happen if the document being indexed is corrupt and the filter is unable to extract text from the document. This failure can also occur when a component is unable to handle the content of a single row above a certain size, due to memory limits on the full-text filter daemon host (fdhost.exe).  
  
 For each row-level failure, the crawl log contains details on the reason for the failure. The error counts are summarized at the end of a full or incremental population.  
  
 There are other failures that can impact the indexing process itself and prevent the population from completing:  
  
-   The full-text index exceeds the limit for the number of rows that can be contained in a full-text catalog.  
  
-   A clustered index or full-text key index on the table being indexed gets altered, dropped, or rebuilt.  
  
-   A hardware failure or disk corruption results in the corruption of the full-text catalog.  
  
-   A file group that contains the table being full-text indexed goes offline, or is made read-only.  
  
 Examine the crawl log at the end of any significant full-text index population operation, or when you find that a population did not complete.  
  
### Unsigned Components  
 By default, the full-text indexer requires the filters and word breakers that it loads to be signed. If they are not signed, which is the case sometimes when custom components are installed, you must configure the full-text indexer to ignore signature verification.  
  
> [!IMPORTANT]  
>  Ignoring signature verification makes the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] less secure. We recommend that you sign any components that you implement or ensure that any components that you acquire are signed. For information about signing components, see [sp_fulltext_service &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-fulltext-service-transact-sql.md).  
  
  
##  <a name="state"></a> Full-Text Index in Inconsistent State after Transaction Log Restored  
 When restoring the transaction log of a database, you might see a warning indicating that the full-text index is not in a consistent state. The reason for this is that the full-text index on a table was modified after the database was backed up. To bring the full-text index to a consistent state, you must run a full population (crawl) on the table. For more information, see [Populate Full-Text Indexes](../../relational-databases/search/populate-full-text-indexes.md).  
  
  
## See Also  
 [ALTER FULLTEXT CATALOG &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-catalog-transact-sql.md)   
 [Populate Full-Text Indexes](../../relational-databases/search/populate-full-text-indexes.md)  
  
  
