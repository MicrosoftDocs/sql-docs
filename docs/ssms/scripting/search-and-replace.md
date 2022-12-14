---
title: "Search and Replace"
description: Learn about the four different ways to find and replace text, each of which displays its own version of the Find and Replace dialog box. The Find and Replace option settings affect all these ways of searching. 
ms.custom: seo-lt-2019
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: ssms
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
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Search and Replace
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
  There are several different ways to find and replace text. On the **Edit** menu, **Find and Replace** offers four choices: **Quick Find**, **Quick Replace**, **Find in Files**, or **Replace in Files**. Each of these opens versions of the **Find and Replace** dialog box. You can also search without a dialog box by using incremental search keyboard shortcut keys. These techniques allow you to control the scope of find and replace, and choose the method of reviewing search matches and replacements.  
  
 You should consider the following when you search and replace text:  
  
-   Options set in the **Find and Replace** dialog box affect all the searches. These options include **Match case**, **Match whole word**, **Search up**, **Search hidden text**, **Wildcards**, **Regular Expressions**, **Look in All Open Documents**, and **Look in Current Project**. All options are not available in all versions of the **Find and Replace** dialog box.  
  
-   **Undo** is available only for documents left open after a replace operation.  
  
-   **Undo** for a **Replace All** operation that spans more than one file is considered a single, bulk action across all the affected files. That is, you cannot undo the change in some files while retaining the change in other files.  
  
 Generally, you cannot search items with graphical views.  
  
## See Also  
 [Search an Active Document Incrementally](./search-an-active-document-incrementally.md)   
 [Search Documents Interactively](./search-documents-interactively.md)   
 [Search Documents Using Results Lists](./search-documents-using-results-lists.md)   
 [Search Text with Wildcards](./search-text-with-wildcards.md)   
 [Search Text with Regular Expressions](./search-text-with-regular-expressions.md)  
  
