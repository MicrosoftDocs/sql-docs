---
title: "Query and Text Editors (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.technology: scripting
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "Query Editor [SQL Server Management Studio]"
  - "Code Editor [SQL Server Management Studio], about Query Editor"
  - "Query Editor [SQL Server Management Studio], full screen mode"
  - "Query Editor [Database Engine], templates"
  - "full screen mode [SQL Server Management Studio]"
  - "SQL Server Management Studio [SQL Server], templates"
  - "writing scripts"
  - "modifying scripts"
  - "SQL Server Management Studio [SQL Server], query editor"
  - "Query Editor [SQL Server Management Studio], about Query Editor"
  - "writing queries"
  - "SQL Server Management Studio [SQL Server], editor"
  - "scripts [SQL Server], SQL Server Management Studio"
  - "queries [SQL Server], SQL Server Management Studio"
ms.assetid: 062051e4-4b77-4969-98ae-d2547c24ce3e
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Query and Text Editors (SQL Server Management Studio)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  You can use one of the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] editors to interactively edit and test a [!INCLUDE[tsql](../../includes/tsql-md.md)], MDX, DMX, or XML/A script, or to edit an XML or plain text file. Each editor is supported by a language-specific service that colors keywords, and checks for syntax and usage errors. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor includes a [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger that you can use to help fix problems in [!INCLUDE[tsql](../../includes/tsql-md.md)] code.  
  
## SQL Server Management Studio Editors  
 The four editors in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] share a common architecture. The text editor implements the base level of functionality, and can be used as a basic editor for text files. The other three editors, or query editors, extend this base of functionality by including a language service that defines the syntax of one of the languages supported in SQL Server. The query editors also implement varying levels of support for editor features such as IntelliSense and debugging. The query editors include the Database Engine Query Editor for use in building scripts containing Transact-SQL and XQuery statements, the MDX editor for the MDX language, the DMX editor for the DMX language, and the XML/A editor for the XML for Analysis language.  
  
## Common Components  
 All of the editors in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] share these components:  
  
 **Code Pane**  
 The area where you enter your queries or text. In the query editors, it contains the statement builder features available for your language. The text editing environment supports find and replace, bulk commenting, and custom fonts and colors.  
  
 You can set options that affect the behavior of text in the code pane as it relates to indenting, tabbing, dragging and dropping of text, and so forth. Query windows can be configured to operate as either tabs in the document window, or in separate documents.  
  
 **Selection Margin**  
 A column of white space between the margin indicator bar and the code text where you can click to select lines of text. You can hide or display the selection margin.  
  
 **Horizontal and Vertical Scroll Bars**  
 Allows you to scroll the code pane horizontally and vertically so that you can view the code that extends beyond the viewable edges of the code pane.  
  
 **Line Numbering**  
 Displays line numbers to the left of the text or code in the Editor. You can navigate to specific line numbers.  
  
 **Word Wrap**  
 Displays long lines of text or code as multiple lines, enabling you to see all the text on the line. Word wrap does not affect the way text appears when it is executed or printed. Word wrap is turned on from the **Tools**, **Options** dialog box, on either the Text Editor, All Languages, General page, or on a specific editor page.  
  
## Code Editor Components  
 The code editors contain these features in addition to the ones shared with the text and XML editors:  
  
 **Results**  
 This window is used to view the results of a query. The window can display the results in grid or in text, or the results can be directed to a file. Result grids can be displayed as separate tabbed windows.  
  
 **IntelliSense**  
 In the Editors, on the **Edit** menu, point to **IntelliSense**, to view the [!INCLUDE[msCoName](../../includes/msconame-md.md)] IntelliSense options.  
  
 **Color Coding**  
 Displays different colors for each type of syntax element, which improves the readability of complex statements.  
  
 **Code Outlining**  
 Displays code groups with outlining lines to the left of the code. Code groups can be collapsed and expanded to make it easier to review your code.  
  
 **Template**  
 Templates are files that include the basic structure of the statements needed to create objects in a database. They can be used to speed the authoring of scripts.  
  
 **Messages**  
 Displays errors, warnings, and informational messages that are returned by the server when a script is run. The list of messages does not change until the script is run again.  
  
 **Status Bar**  
 Displays system information that is associated with the Query Editor window, such as which instance the Query Editor is connected to.  
  
## Database Engine Query Editor Components  
 These components are only available in the Database Engine Query Editor:  
  
 **Debugger**  
 Enables you to pause the execution of code on specific statements. You can then view data and system information to help you find errors in the code.  
  
 **Error List**  
 Displays syntax and semantic errors found by IntelliSense. The list of errors changes dynamically as you edit [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts.  
  
 **Graphical Showplan**  
 Displays the logical steps built into the execution plan of a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement.  
  
 **Client Statistics**  
 Displays information about the query execution grouped into categories. When **Include Client Statistics** is selected from the **Query** menu, a **Client Statistics** window is displayed upon query execution. Statistics from successive query executions are listed along with the average values. Select **Reset Client Statistics** from the **Query** menu to reset the average.  
  
 **Code Snippets**  
 Templates you can use as a starting point when adding statements in the Database Engine Query Editor. You can insert the pre-defined snippets supplied with SQL Server, or add your own snippets.  
  
 **SQLCMD Mode**  
 Runs [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts that include the set of commands supported by the sqlcmd utility. For more information, see [sqlcmd How-to Topics](https://msdn.microsoft.com/library/dd7a2d2b-6327-4d77-ac5a-580d36073ad4).  
  
## Editor Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes how to view and use the basic features in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor.|[Database Engine Query Editor &#40;SQL Server Management Studio&#41;](../../relational-databases/scripting/database-engine-query-editor-sql-server-management-studio.md)|  
|Describes how to view and use the basic features in the MDX Query Editor.|[MDX Query Editor &#40;Analysis Services - Multidimensional Data&#41;](https://msdn.microsoft.com/library/777f2c23-1c1c-4b72-9d19-48a4866551f8)|  
|Describes how to view and use the basic features in the DMX Query Editor.|[DMX Query Editor &#40;Analysis Services - Data Mining&#41;](https://msdn.microsoft.com/library/7ac877a1-0f29-46b9-9a51-73b02172bef1)|  
|Describes how to view and use the basic features in the XML/A Editor.|[XML Editor &#40;SQL Server Management Studio&#41;](../../relational-databases/scripting/xml-editor-sql-server-management-studio.md)|  
|Describes how to configure options for the various editors, such as line numbering and IntelliSense options.|[Configure Editors &#40;SQL Server Management Studio&#41;](../../relational-databases/scripting/configure-editors-sql-server-management-studio.md)|  
|Describes the various ways you can open the editors in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].|[Open an Editor &#40;SQL Server Management Studio&#41;](../../relational-databases/scripting/open-an-editor-sql-server-management-studio.md)|  
|Describes how to manage the view mode, such as word wrap, splitting a window, or tabs.|[Manage the Editor and View Mode](../../relational-databases/scripting/manage-the-editor-and-view-mode.md)|  
|Describes how to set formatting options, such as hidden text or indentation.|[Manage Code Formatting](../../relational-databases/scripting/manage-code-formatting.md)|  
|Describes how to navigate through the text in an editor window by using features such as incremental search or go to.|[Navigate Code and Text](../../relational-databases/scripting/navigate-code-and-text.md)|  
|Describes how to set color coding options for various classes of syntax, which makes it easier to read complex statements.|[Color Coding in Query Editors](../../relational-databases/scripting/color-coding-in-query-editors.md)|  
|Describes how to use code outlining to hide parts of complex scripts that you are not currently working on.|[Code Outlining](../../relational-databases/scripting/code-outlining.md)|  
|Describes how to drag text from one location in a script and drop it in a new location.|[Drag and Drop Text](../../relational-databases/scripting/drag-and-drop-text.md)|  
|Describes how to do global search and replace, such as when changing column names.|[Search and Replace](../../relational-databases/scripting/search-and-replace.md)|  
|Describes how to set bookmarks in order to more easily find important pieces of code.|[Manage Bookmarks](../../relational-databases/scripting/manage-bookmarks.md)|  
|Describes how to print scripts or the results in a window or grid.|[Print Code and Results](../../relational-databases/scripting/print-code-and-results.md)|  
|Describes how to use the sqlcmd features in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor.|[Edit SQLCMD Scripts with Query Editor](../../relational-databases/scripting/edit-sqlcmd-scripts-with-query-editor.md)|  
|Describes how to use IntelliSense features such as auto-completing object names as you type them, or ensuring breakpoints are placed in valid locations.|[IntelliSense &#40;SQL Server Management Studio&#41;](../../relational-databases/scripting/intellisense-sql-server-management-studio.md)|  
|Describes how to use code snippets in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor. Snippets are templates for commonly used statements or blocks, and can be customized or extended to include site-specific snippets.|[Transact-SQL Code Snippets](../../relational-databases/scripting/transact-sql-code-snippets.md)|  
|Describes how to use the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger to step through code and view debugging information such as the values in variables and parameters.|[Transact-SQL Debugger](../../relational-databases/scripting/transact-sql-debugger.md)|  
|Describes how to set custom colors for different instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], and have those colors set as the background of the status bar in [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor windows.|[Status Bar &#40;Database Engine Query Editor&#41;](../../relational-databases/scripting/status-bar-database-engine-query-editor.md)|  
  
## See Also  
 [SQL Server Management Studio Keyboard Shortcuts](../../tools/sql-server-management-studio/sql-server-management-studio-keyboard-shortcuts.md)  
  
  
