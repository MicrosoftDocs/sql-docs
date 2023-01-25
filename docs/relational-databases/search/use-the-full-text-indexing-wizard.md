---
description: "Use the Full-Text Indexing Wizard"
title: "Use the Full-Text Indexing Wizard | Microsoft Docs"
ms.date: "08/19/2016"
ms.service: sql
ms.subservice: search
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.fulltextindexingwizard.welcome.f1"
  - "sql13.swb.fulltextindexingwizard.selectorcreatepopschedules.f1"
  - "sql13.swb.fulltextindexingwizard.progress.f1"
  - "sql13.swb.fulltextindexingwizard.selectchangetracking.f1"
  - "sql13.swb.fulltextindexingwizard.selectacatalog.f1"
  - "sql13.swb.fulltextindexingwizard.selectatableorview.f1"
  - "sql13.swb.fulltextindexingwizard.selectanindex.f1"
  - "sql13.swb.fulltextindexingwizard.summary.f1"
  - "sql13.swb.fulltextindexingwizard.selecttablecolumns.f1"
helpviewer_keywords: 
  - "Full-Text Indexing Wizard"
  - "full-text search [SQL Server], Full-Text Indexing Wizard"
ms.assetid: 3e9d9605-6525-4781-9168-fdaa06db3459
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Use the Full-Text Indexing Wizard
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  The Full-Text Indexing Wizard in SSMS walks you through a series of steps designed to help you create a full-text index.  
  
## Create a  Full-Text index 

1. In Object Explorer, right-click the table on which you want to create a full-text index, point to **Full-Text index**, and then click **Define Full-Text Index**. This action launches the Wizard in a separate window.
   Click Next 
  
2. **Unique Index.**  Select an index from the drop down list. The index must be a single-key-column, unique, non-nullable index. Select the smallest unique key index for the full-text unique key. For best performance, a clustered index is recommended.  
  
3.  **Available Columns.** Check the box next to all column names for columns you want to include.  check box next to the column name. Ineligible columns are greyed out and their check boxes disabled.  
  
4. **Language for Word Breaker.** Select a language from the drop-down list. This choice will be used  to identify the correct word breakers for the index. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses word breakers to identify word boundaries in the full-text indexed data.  
  
5.  **Type Column.** Select the name of the column that holds the document type of column being full-text indexed.  

    > [!NOTE]  
    > The **Type Column** is enabled only when the column named in the **Available Columns** column is of type **varbinary(max)** or **image**.  
  
6. **Statistical Semantics.** Select whether to enable semantic indexing for the selected column. For more information, see [Semantic Search &#40;SQL Server&#41;](../../relational-databases/search/semantic-search-sql-server.md).  
  
   > [!NOTE]  
   > If your selected language does not have an associated Semantic Language Model, then the **Statistical Semantics** checkbox is not enabled. If you select **Statistical Semantics** prior to selecting a **Language**, the languages available in the drop-down combo box will be restricted to those for which there is Semantic Language Model support.  
   >
   > Semantic Search is **not available for Azure SQL Database.** The Statistical Semantics option does not appear when running this Wizard on an Azure SQL Database.
  
7. Select the change tracking options.  
  
     **Automatically**  
     Select this radio button to have the full-text index updated automatically as changes occur to the underlying data.  
  
     **Manually**  
     Select this radio button if you do not want the full-text index to be updated automatically as changes occur to the underlying data. Changes to the underlying data are maintained. However, to apply the changes to the full-text index you must start or schedule this process manually.  
  
     **Do not track changes**  
     Select this radio button if you do not want the full-text index to be updated with changes to the underlying data.  
  
8.  Start full population when index is created (Available only when you Do not track changes).
  
     Select this radio button to kick off a full population at the successful completion of this wizard. This will consist of creating the full-text index structure in the catalog and populating it with full-text indexed data.  
     
     Click Next
  
## Catalog, Index Filegroup and Stoplist   
  
9.  **Select full-text catalog**  

     **Select a catalog:** Select a full-text catalog from the list. The default catalog for the database will be the selected item by default in the list. If no catalogs are available, the list will be disabled, and the **Create a new catalog** checkbox will be checked and disabled.  
  
  OR
  
 10. **Create a new catalog**
 - Select full-text catalog.  
  
    a. **Name**  
     Enter a name for your new full-text catalog.  
  
     b. **Set as default catalog**  
     Select to make the catalog the default for this database.  
  
     c. **Accent sensitivity**  
     Specify whether the new catalog will be accent-sensitive or accent-insensitive. If the database is accent-sensitive, **Sensitive** is selected by default.  
  
     d. **Select index filegroup**  
     Specify the filegroup on which to create the full-text index.  
  
     e. Select a value:
       
      |Value|Description|  
      |-----------|-----------------|
      |**\<default\>**| If the table or view is not partitioned, select to use the same filegroup as the underlying table or view. If the       table or view is partitioned, the primary filegroup is used|
      |**PRIMARY**|Select to use the primary filegroup for the new full-text index.|
      *user-specified default filegroup*|If a user-defined default stoplist exists, select its name from the list to use that filegroup       for the new full-text index.|
  
     
 11. **Select full-text stoplist**  
     Specify a stoplist to use for the full-text index, or disable stoplist use.  
  
     Stopwords are managed in databases using objects called stoplists. A *stoplist* is a list of stopwords that, when associated with a full-text index, is applied to full-text queries on that index. For more information, see [Configure and Manage Stopwords and Stoplists for Full-Text Search](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md).  
  
     Select one of the following values:  
  
   |Value|Description|  
   |-----------|-----------------|  
   |**\<system\>**|Select to use the system stoplist on the new full-text index. This is the default|  
   |**\<off\>**|Select to disable stoplists for the new full-text index.|  
   |*user-defined-stoplist-name*|The list displays the name of each user-defined stoplist, if any, that has been created on the database. Select any user-defined stoplist to use for the new full-text index.|  
  
   Click Next
  
11. Optionally, SQL Server only, define the population schedule. Indexing operations will begin immediately unless they have been scheduled for future execution. Schedules will be created immediately, although they will not run until their scheduled time.  
  
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
  
  
