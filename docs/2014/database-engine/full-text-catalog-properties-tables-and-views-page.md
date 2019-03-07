---
title: "Full-Text Catalog Properties (Tables and Views Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.fulltextsearch.ftcatalogproperties.tablesviews.f1"
ms.assetid: 2d45fcd2-0f0f-4167-9027-316d6696c106
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Full-Text Catalog Properties (Tables and Views Page)
  Use this dialog page to view or modify the tables and views that are assigned to the full-text catalog.  
  
## UIElement List  
 **All eligible table/view objects in this database**  
 Lists the tables and views that have a unique index defined on them, but are not already a part of the full-text catalog. To select a table or view and assign it to the catalog, select the items in the list box and press the "->" button.  
  
 **Table/view objects assigned to the catalog**  
 Lists the tables and views that are currently assigned to the full-text catalog  
  
## Selected Object Properties  
 **Selected object properties**  
 Displays the properties of the selected object in the list box of objects assigned to the catalog.  
  
 **Unique Index**  
 Lists the available unique indexes of the table or view.  
  
 **Table is full-text enabled**  
 Select this check box to enable the full-text index on the table. Clear the check box to disable the full-text index.  
  
## Eligible Columns Grid  
  
|||  
|-|-|  
|**Available Columns**|Displays all the columns that are full-text indexed. Select a check box to add a column to the full-text index.|  
|**Language for Word Breaker**|Displays the language of the word breaker.|  
|**Data Type Column**|Lists the name of the column in the table that holds the document type of the column listed in **Available Columns** if the column is a `varbinary(max)` or `image` column.|  
|**Statistical Semantics**|Select whether to enable semantic indexing for the selected column. For more information, see [Semantic Search &#40;SQL Server&#41;](../relational-databases/search/semantic-search-sql-server.md).<br /><br /> If you select a **Language** prior to selecting **Statistical Semantics**, and the selected language does not have an associated Semantic Language Model, then the **Statistical Semantics** checkbox is disabled. If you select **Statistical Semantics** prior to selecting a **Language**, the languages available in the drop-down combo box will be restricted to those for which there is Semantic Language Model support.|  
  
## Track Changes  
  
|||  
|-|-|  
|**Automatic**|The full-text index is automatically updated when the data in the underlying table is modified, added, or deleted.|  
|**Manual**|When data is modified, added, or deleted in the indexed data, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tracks the changes. When **Manual** change tracking is in effect, the index is not automatically updated with these changes. Instead, an administrator can apply the changes manually by using an [ALTER FULLTEXT INDEX ... START UPDATE POPULATION](/sql/t-sql/statements/alter-fulltext-index-transact-sql) statement.|  
|**Do not track change**|With this option in effect, changes to the indexed data in the catalog are not recorded. An administrator must build the index by using ALTER FULLTEXT INDEX with either FULL POPULATION or INCREMENTAL POPULATION.|  
  
## See Also  
 [CREATE FULLTEXT CATALOG &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-fulltext-catalog-transact-sql)   
 [ALTER FULLTEXT CATALOG &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-fulltext-catalog-transact-sql)   
 [Populate Full-Text Indexes](../relational-databases/indexes/indexes.md)  
  
  
