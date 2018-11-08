---
title: "Full-Text Index Properties (Columns Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.fulltextsearch.fulltextindexproperties.columns.f1"
ms.assetid: 75e52edb-0d07-4393-9345-8b5af4561e35
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Full-Text Index Properties (Columns Page)
  **To view or change the properties of a full-text index**  
  
-   [Manage Full-Text Indexes](../relational-databases/indexes/indexes.md)  
  
## UIElement List  
 **Unique index**  
 Select an index from the drop down list. The index must be a single-key-column, unique, non-nullable index.  
  
 **Select the eligible columns that will be full-text indexed**  
 The grid displays the table columns that are available for this full-text index. Columns that are currently full-text indexed are checked. Optionally, you can check additional columns that you want to be full-text indexed.  
  
> [!IMPORTANT]  
>  Ensure that at least one column is checked before you click OK.  
  
|||  
|-|-|  
|**Available Columns**|The column name.|  
|**Language for Word Breaker**|The language whose word breakers and stemmers perform linguistic analysis on all full-text indexed data.<br /><br /> For more information, see [Configure and Manage Word Breakers and Stemmers for Search](../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md) and [Choose a Language When Creating a Full-Text Index](../relational-databases/search/choose-a-language-when-creating-a-full-text-index.md).|  
|**Type**|The name of the table column that holds the document type of the selected column. This is a read-only property.|  
|**Statistical Semantics**|Select whether to enable semantic indexing for the selected column. For more information, see [Semantic Search &#40;SQL Server&#41;](../relational-databases/search/semantic-search-sql-server.md).<br /><br /> If you select a **Language** prior to selecting **Statistical Semantics**, and the selected language does not have an associated Semantic Language Model, then the **Statistical Semantics** checkbox is disabled. If you select **Statistical Semantics** prior to selecting a **Language**, the languages available in the drop-down combo box will be restricted to those for which there is Semantic Language Model support.|  
  
## See Also  
 [Populate Full-Text Indexes](../relational-databases/search/populate-full-text-indexes.md)  
  
  
