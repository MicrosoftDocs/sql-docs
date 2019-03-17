---
title: "Search and Replace | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "match case [SQL Server]"
  - "undo operations"
  - "searches [SQL Server Management Studio]"
  - "replacing text"
  - "quick search and replaces [SQL Server Management Studio]"
  - "Query Editor [SQL Server Management Studio], undo"
  - "Query Editor [SQL Server Management Studio], searching"
  - "regular expressions [SQL Server Management Studio]"
  - "finding text [SQL Server Management Studio]"
  - "text [SQL Server], search and replace operations"
  - "finding [SQL Server Management Studio]"
  - "locating text"
  - "Query Editor [SQL Server Management Studio], replacing text"
  - "Find and Replace dialog box"
  - "wildcard options [SQL Server Management Studio]"
  - "match whole word [SQL Server]"
  - "searches [SQL Server Management Studio], replacing"
ms.assetid: 3641c7b3-3e3e-4ddd-af82-c15b50004f94
author: MightyPen
ms.author: genemi
manager: craigg
---
# Search and Replace
  There are several different ways to find and replace text. On the **Edit** menu, **Find and Replace** offers four choices: **Quick Find**, **Quick Replace**, **Find in Files**, or **Replace in Files**. Each of these opens versions of the **Find and Replace** dialog box. You can also search without a dialog box by using incremental search keyboard shortcut keys. These techniques allow you to control the scope of find and replace, and choose the method of reviewing search matches and replacements.  
  
 You should consider the following when you search and replace text:  
  
-   Options set in the **Find and Replace** dialog box affect all the searches. These options include **Match case**, **Match whole word**, **Search up**, **Search hidden text**, **Wildcards**, **Regular Expressions**, **Look in All Open Documents**, and **Look in Current Project**. All options are not available in all versions of the **Find and Replace** dialog box.  
  
-   **Undo** is available only for documents left open after a replace operation.  
  
-   **Undo** for a **Replace All** operation that spans more than one file is considered a single, bulk action across all the affected files. That is, you cannot undo the change in some files while retaining the change in other files.  
  
 Generally, you cannot search items with graphical views.  
  
## See Also  
 [Search an Active Document Incrementally](search-an-active-document-incrementally.md)   
 [Search Documents Interactively](search-documents-interactively.md)   
 [Search Documents Using Results Lists](search-documents-using-results-lists.md)   
 [Search Text with Wildcards](search-text-with-wildcards.md)   
 [Search Text with Regular Expressions](search-text-with-regular-expressions.md)  
  
  
