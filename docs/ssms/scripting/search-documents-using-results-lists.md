---
title: "Search Documents Using Results Lists | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.technology: scripting
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "searches [SQL Server Management Studio], result lists"
  - "result list searches [SQL Server Management Studio]"
  - "searches [SQL Server Management Studio], multiple files"
  - "Query Editor [SQL Server Management Studio], search multiple files"
ms.assetid: 275e1b6c-fbd0-4408-af77-35903f90657c
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Search Documents Using Results Lists
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  Using the **Find and Replace** dialog box, you can search and replace text in all files in a project or solution or in a file system folder, even when they are not open in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Matches from searches that were performed with the **Find and Replace** dialog box appear in the Find Results 1 and Find Results 2 windows, which allows you to view the exact text from the line containing the match.  
  
### To search in multiple files  
  
1.  On the **Edit** menu, point to **Find and Replace,** and then click **Find in Files**.  
  
2.  In the **Find what** text box, enter the text to search for.  
  
3.  In the **Look in** list, click **All Open Documents**, **Current Project**, **Entire Solution**, or type a directory path.  
  
4.  In the **File types** list, select one of the listed sets of file extensions or enter the extensions for the types of files to be searched, separated by semicolons. Use \*.\* to search all files in the directory listed in the **Look in** drop-down list.  
  
5.  Select from the remaining search options to improve the accuracy of the search.  
  
6.  Click **Find** to begin the search.  
  
 The matches for the search appear in the Find Results 1 window by default. You can browse the search matches by double-clicking each entry in the Find Results 1 window. To view the search results in a new window, select **Display in Find 2**.  
  
#### To replace across multiple files or folders  
  
1.  On the **Edit** menu, point to **Find and Replace,** and then click **Replace in Files**.  
  
2.  In the **Find what** text box, enter the text to search for.  
  
3.  In the **Replace with** box, enter the text to replace the search text.  
  
4.  In the **Look in** list, click **All Open Documents**, **Current Project**, **Entire Solution**, or type a directory path.  
  
5.  Click **Replace** to replace the current search match with the text in the **Replace with** box. You can skip a single match by clicking **Find Next** or skip an entire file clicking **Skip File**.  
  
     \- or -  
  
     Choose **Replace all** to replace all search matches with the text in the **Replace with** box. Select **Keep modified files open after Replace All** if you want to undo some of the replacements at another time.  
  
    > [!NOTE]  
    >  **Replace all** replaces all search matches, including those in files you have skipped with **Skip File** or **Find Next**. You can only use **Undo** for replacements made in files that remain open after the replacement operation.  
  
 Replacement information appears in the Find Results 1 window by default. You can browse replacements by double-clicking each entry in the Find Results 1 window.  
  
## See Also  
 [Search and Replace](../../relational-databases/scripting/search-and-replace.md)   
 [Search Documents Interactively](../../relational-databases/scripting/search-documents-interactively.md)   
 [Search Text with Wildcards](../../relational-databases/scripting/search-text-with-wildcards.md)   
 [Search Text with Regular Expressions](../../relational-databases/scripting/search-text-with-regular-expressions.md)  
  
  
