---
title: "Search and Replace | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.technology: scripting
ms.reviewer: ""
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
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Search and Replace
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  There are several different ways to find and replace text. On the **Edit** menu, **Find and Replace** offers four choices: **Quick Find**, **Quick Replace**, **Find in Files**, or **Replace in Files**. Each of these opens versions of the **Find and Replace** dialog box. You can also search without a dialog box by using incremental search keyboard shortcut keys. These techniques allow you to control the scope of find and replace, and choose the method of reviewing search matches and replacements.  
  
 You should consider the following when you search and replace text:  
  
-   Options set in the **Find and Replace** dialog box affect all the searches. These options include **Match case**, **Match whole word**, **Search up**, **Search hidden text**, **Wildcards**, **Regular Expressions**, **Look in All Open Documents**, and **Look in Current Project**. All options are not available in all versions of the **Find and Replace** dialog box.  
  
-   **Undo** is available only for documents left open after a replace operation.  
  
-   **Undo** for a **Replace All** operation that spans more than one file is considered a single, bulk action across all the affected files. That is, you cannot undo the change in some files while retaining the change in other files.  
  
 Generally, you cannot search items with graphical views.  
  
## See Also  
 [Search an Active Document Incrementally](../../relational-databases/scripting/search-an-active-document-incrementally.md)   
 [Search Documents Interactively](../../relational-databases/scripting/search-documents-interactively.md)   
 [Search Documents Using Results Lists](../../relational-databases/scripting/search-documents-using-results-lists.md)   
 [Search Text with Wildcards](../../relational-databases/scripting/search-text-with-wildcards.md)   
 [Search Text with Regular Expressions](../../relational-databases/scripting/search-text-with-regular-expressions.md)  
  
  
