---
title: "Use the Full-Text Indexing Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.fulltextindexingwizard.selecttablecolumns.f1"
  - "sql12.swb.fulltextindexingwizard.welcome.f1"
  - "sql12.swb.fulltextindexingwizard.selectacatalog.f1"
  - "sql12.swb.fulltextindexingwizard.progress.f1"
  - "sql12.swb.fulltextindexingwizard.selectorcreatepopschedules.f1"
  - "sql12.swb.fulltextindexingwizard.selectatableorview.f1"
  - "sql12.swb.fulltextindexingwizard.selectchangetracking.f1"
  - "sql12.swb.fulltextindexingwizard.selectanindex.f1"
  - "sql12.swb.fulltextindexingwizard.summary.f1"
helpviewer_keywords: 
  - "Full-Text Indexing Wizard"
  - "full-text search [SQL Server], Full-Text Indexing Wizard"
ms.assetid: 3e9d9605-6525-4781-9168-fdaa06db3459
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Use the Full-Text Indexing Wizard
  The Full-Text Indexing Wizard walks you through a series of steps designed to help you create a full-text index.  
  
#### To use the Full-Text Indexing wizard  
  
1.  In Object Explorer, right-click the table on which you want to create a full-text index, point to **Full-Text index**, and then click **Define Full-Text Index**.  
  
     **Unique Index**  
     Select an index from the drop down list. The index must be a single-key-column, unique, non-nullable index. Select the smallest unique key index for the full-text unique key. For best performance, a clustered index is recommended.  
  
     **Available Columns**  
     To include a column in the index, select the check box next to the column name. Columns that are not eligible for full-text indexing appear in grey with their check boxes disabled.  
  
     **Language for Word Breaker**  
     Select a language from the drop-down list. This choice will be used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to identify the correct word breakers for the index. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses word breakers to identify word boundaries in the full-text indexed data.  
  
     **Type Column**  
     Select the name of the column that holds the document type of column being full-text indexed.  
  
     The  **Type Column** is only enabled when the column named in the **Available Columns** column is of type `varbinary(max)` or `image`.  
  
     **Statistical Semantics**  
     Select whether to enable semantic indexing for the selected column. For more information, see [Semantic Search &#40;SQL Server&#41;](semantic-search-sql-server.md).  
  
     If you select a **Language** prior to selecting **Statistical Semantics**, and the selected language does not have an associated Semantic Language Model, then the **Statistical Semantics** checkbox is disabled. If you select **Statistical Semantics** prior to selecting a **Language**, the languages available in the drop-down combo box will be restricted to those for which there is Semantic Language Model support.  
  
2.  Select the change tracking options.  
  
     **Automatically**  
     Select this radio button to have the full-text index updated automatically as changes occur to the underlying data.  
  
     **Manually**  
     Select this radio button if you do not want the full-text index to be updated automatically as changes occur to the underlying data. Changes to the underlying data are maintained. However, to apply the changes to the full-text index you must start or schedule this process manually.  
  
     **Do not track changes**  
     Select this radio button if you do not want the full-text index to be updated with changes to the underlying data.  
  
     **Start full population when index is created**  
     Select this radio button to kick off a full population at the successful completion of this wizard. This will consist of creating the full-text index structure in the catalog and populating it with full-text indexed data.  
  
3.  Select the catalog, index filegroup and stoplist.  
  
     **Select full-text catalog**  
     Select a full-text catalog from the list. The default catalog for the database will be the selected item by default in the list. If no catalogs are available, the list will be disabled, and the **Create a new catalog** checkbox will be checked and disabled.  
  
    |||  
    |-|-|  
    |**Create a new catalog**|Select to create a new full-text catalog.|  
  
     **Name**  
     Enter a name for the new full-text catalog.  
  
     **Set as default catalog**  
     Select to make the catalog the default for this database.  
  
     **Accent sensitivity**  
     Specify whether the new catalog will be accent-sensitive or accent-insensitive. If the database is accent-sensitive, **Sensitive** will be selected by default.  
  
     **Select index filegroup**  
     Specify the filegroup on which to create the full-text index.  
  
     Select one of the following values:  
  
    |Value|Description|  
    |-----------|-----------------|  
    |**\<default>**|If the table or view is not partitioned, select to use the same filegroup as the underlying table or view. If the table or view is partitioned, the primary filegroup is used.|  
    |**PRIMARY**|Select to use the primary filegroup for the new full-text index.|  
    |*user-specified default filegroup*|If a user-defined default stoplist exists, select its name from the list to use that filegroup for the new full-text index.|  
  
     **Select full-text stoplist**  
     Specify a stoplist to use for the full-text index, or disable stoplist use.  
  
     In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions, stopwords are managed in databases using objects called stoplists. A *stoplist* is a list of stopwords that, when associated with a full-text index, is applied to full-text queries on that index. For more information, see [Configure and Manage Stopwords and Stoplists for Full-Text Search](configure-and-manage-stopwords-and-stoplists-for-full-text-search.md).  
  
     Select one of the following values:  
  
    |Value|Description|  
    |-----------|-----------------|  
    |**\<system>**|Select to use the system stoplist on the new full-text index. This is the default|  
    |**\<off>**|Select to disable stoplists for the new full-text index.|  
    |*user-defined-stoplist-name*|The list displays the name of each user-defined stoplist, if any, that has been created on the database. Select any user-defined stoplist to use for the new full-text index.|  
  
4.  Optionally, define the population schedule. Indexing operations will begin immediately unless they have been scheduled for future execution. Schedules will be created immediately, although they will not run until their scheduled time.  
  
     **New Table Schedule**  
     Define a population schedule for a table.  
  
     **New Catalog Schedule**  
     Define a population schedule for a full-text catalog.  
  
     **Edit**  
     Edit a schedule.  
  
     **Delete**  
     Delete a schedule.  
  
5.  View or control the progress of the Full-Text Indexing Wizard.  
  
     **Stop**  
     Interrupts the current operation and prevents subsequent full-text operations from being performed by the wizard during this session.  
  
     **Report**  
     When all of the operations have finished executing, click this button to access a report on the operations performed. You can view the report, print it to a file, copy it to the clipboard, or e-mail the report.  
  
  
