---
title: "Search Documents Interactively"
description: Learn how to use the Find and Replace dialog box to search one or more open files or windows, pausing after each match to review what was found, in its context. You can also perform a bulk find operation and review the search matches in report format.
ms.custom: seo-lt-2019
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: ssms
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "interactive searches [SQL Server Management Studio]"
  - "searches [SQL Server Management Studio], interactive"
  - "Query Editor [SQL Server Management Studio], interactive search"
ms.assetid: dae65ac5-67af-45c6-a6e0-952fea26d680
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Search Documents Interactively
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
  Using the **Find and Replace** dialog box, you can search one or more open files or windows and move through the search matches one by one. This technique allows you to review each individual search match in the context of the text around the match. You also have the option of performing bulk find operations and reviewing search matches in report format using the **Find and Replace** dialog box.  
  
### To search all open documents  
  
1.  Open the items you wish to search.  
  
2.  On the **Edit** menu, point to **Find and Replace,** and then click **QuickFind**.  
  
3.  In the **Find and Replace** text box, enter the text to search for.  
  
4.  In the **Look in** list, select **All Open Documents**.  
  
    > [!NOTE]  
    >  Certain open files might not be searched when **All Open Documents** is selected. Only files currently open in a textual view, such as Code view, are included in searches. Files in Designer view are not included in searches.  
  
5.  Select additional search options to increase the accuracy of the search.  
  
6.  Click **Find Next** to start the search, and continue clicking **Find Next** until the last file has been searched.  
  
 When the search passes the beginning or end of the document, a message is displayed in the status bar. A message box appears when the search reaches the starting point of the search.  
  
#### To replace in all active files interactively  
  
1.  On the **Edit** menu, point to **Find and Replace**, and then click **QuickReplace**.  
  
2.  In the **Find what** box, enter the text to search for.  
  
3.  In the **Replace with** box, enter the text to replace the search text.  
  
4.  In the **Look in** list, select **All Open Documents**.  
  
5.  Click **Replace**, and continue clicking **Replace** until the last match in the last file has been replaced. Click **Find Next** to skip a match you do not want to replace.  
  
     -or-  
  
     Choose **Replace All** to replace all matches. A message box appears, listing the total number of replacements.  
  
 The **Replace All** command replaces all search matches, including those you have skipped with the **Find Next** button. To cancel **Replace All**, click **Undo** from the **Edit** menu before closing any of the files.  
  
## See Also  
 [Search an Active Document Incrementally](./search-an-active-document-incrementally.md)   
 [Search and Replace](./search-and-replace.md)   
 [Search Documents Using Results Lists](./search-documents-using-results-lists.md)   
 [Search Text with Wildcards](./search-text-with-wildcards.md)   
 [Search Text with Regular Expressions](./search-text-with-regular-expressions.md)  
  
