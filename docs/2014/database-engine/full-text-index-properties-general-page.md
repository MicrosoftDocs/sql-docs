---
title: "Full-Text Index Properties (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.fulltextsearch.fulltextindexproperties.general.f1"
ms.assetid: f4dff61c-8c2f-4ff9-abe4-70a34421448f
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Full-Text Index Properties (General Page)
  **To view or change the modifiable properties of a full-text index**  
  
-   [Manage Full-Text Indexes](../relational-databases/indexes/indexes.md)  
  
## UIElement List  
 **Full-Text Catalog**  
 Displays the name of the full-text catalog with which the full-text index is associated.  
  
 **Database**  
 Displays the name of the database in which the full-text index resides.  
  
 **Table**  
 Displays the name of the table on which the full-text index is defined.  
  
 **Full-Text Index Key**  
 Displays the name of the full-text index key, which is a unique index on a single column of the table.  
  
 **Table Full-Text Populate Status**  
 Displays the population status of the full-text indexed table.  
  
 The possible values are as follows:  
  
 0 = Idle.  
  
 1 = Full population is in progress.  
  
 2 = Incremental population is in progress.  
  
 3 = Propagation of tracked changes is in progress.  
  
 4 = Background update index is in progress, such as automatic change tracking.  
  
 5 = Full-text indexing is throttled or paused.  
  
 **Active Full-Text Index**  
 Indicates whether the table has an active full-text index.  
  
 1 = True  
  
 0 = False  
  
 **Full-Text Index Filegroup**  
 The filegroup to which the full-text index belongs.  
  
 **Full-Text Index Stoplist**  
 The stoplist that is currently associated with the full-text index. A stoplist is a list of [stopwords](../relational-databases/search/full-text-search.md). The stoplist associated with a full-text index, if any, is applied to full-text queries on that index. You can remove the stoplist from the index by selecting **\<OFF>** from the list, or you can select a different stoplist; **\<SYSTEM>** indicates the system stoplist.  
  
 **To create a stoplist**  
  
-   [Configure and Manage Stopwords and Stoplists for Full-Text Search](../relational-databases/search/full-text-search.md)  
  
 **Search Property List**  
 The search property list that is currently associated with the full-text index, if any. A search property list specifies a set of document properties that are included in the associated full-text index when it is populated. For more information, see [Search Document Properties with Search Property Lists](../relational-databases/search/search-document-properties-with-search-property-lists.md).  
  
 **\<Off>** indicates that there is currently no search property list associated with the index. You can remove the current search property list from the index by selecting **\<Off>** from the list, or you can select a different search property list from the list. Only search property lists in the current database are listed here.  
  
> [!NOTE]  
>  You can associate a given search property list with more than one full-text index in the same database.  
  
 **To create a search property list**  
  
-   [Search Document Properties with Search Property Lists](../relational-databases/search/search-document-properties-with-search-property-lists.md)  
  
 **Table Full-Text Item Count**  
 Indicates the number of rows that were full-text indexed successfully.  
  
 This property corresponds to the `TableFulltextItemCount` property returned by the OBJECTPROPERTYEX [!INCLUDE[tsql](../includes/tsql-md.md)] function.  
  
 **Table Full-Text Docs Processed**  
 Displays the number of rows that have been processed since the start of full-text indexing. In a table that is being indexed for full-text search, all the columns of one row are considered as part of one document to be indexed. Deleted rows are not counted.  
  
|||  
|-|-|  
|0|Indicates that full-text indexing is completed and there is no active population.|  
|> 0|For an active population, indicates the number of documents processed by insert or update operations since any of the following: a population, enabling of change tracking with background update index population (such as auto change tracking), changing the full-text index schema, rebuilding the full-text catalog, restarting the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], and so forth.|  
  
 **Table Full-Text Pending Changes**  
 Number of pending change tracking entries to process.  
  
 0 = change tracking is not enabled.  
  
 NULL = Table does not have a full-text index.  
  
 **Table Full-Text Fail Count**  
 The number of rows that full-text search did not index.  
  
 0 = The population has completed.  
  
 \>0 = One of the following:  
  
-   The number of documents that were not indexed since the start of full, incremental, or manual change tracking population.  
  
-   For change tracking with background update index, the number of rows that were not indexed since the start of the population, or the restart of the population. This could be caused by a schema change, rebuild of the catalog, server restart, and so on  
  
 **Full-Text Indexing Enabled**  
 Specifies whether full-text indexing is enabled.  
  
|||  
|-|-|  
|**True**|Enabled|  
|**False**|Disabled|  
  
 **Change Tracking**  
 Specifies whether the table has full-text change-tracking enabled, and if so, what kind. Full-text change tracking maintains a record of the rows that have been modified in a table or indexed view that has been set up for full-text indexing. These changes can be propagated to the full-text index.  
  
 The **Change Tracking** values are as follows:  
  
|||  
|-|-|  
|**Off**|The full-text index is not updated with changes to the underlying data.|  
|**Manual**|The full-text index is not updated automatically as changes occur to the underlying data. However, changes to the underlying data are maintained, and you can propagate them to the . full-text index either on a schedule using SQL Server Agent or manually.|  
|**Automatic**|The full-text index is updated automatically as changes occur to the underlying data in the base table.|  
  
 **Repopulate index**  
 Click to start a population on the full-text index on exiting the dialog box. Select one of the following types of population:  
  
|||  
|-|-|  
|**Full**|During a full population of a table, index entries are built for all the rows.|  
|**Incremental**|Incremental population updates the full-text index for rows added, deleted, or modified after the last population, or while the last population was in progress. Performing an incremental population requires that the base table contain a column of the `timestamp` data type.|  
|**Update**|The full-text index is updated whenever the data in the base table is modified.|  
  
## See Also  
 [Get Started with Full-Text Search](../relational-databases/search/get-started-with-full-text-search.md)  
  
  
