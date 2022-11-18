---
title: Error List Window
description: Learn how to use the Error List to view the errors and warnings that Intellisense generates when you edit Transact-SQL scripts.
titleSuffix: T-SQL debugger
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "error list window"
  - "SQL Server Management Studio [SQL Server], error list window"
ms.assetid: fae6327d-e268-44ae-a474-4a8f8f843129
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 12/04/2019
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Transact-SQL Debugger - Error List Window

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The SQL Server Management Studio **Error List** displays the syntax and semantic errors that are generated from the IntelliSense code in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor.  

[!INCLUDE[ssms-old-versions](../../includes/ssms-old-versions.md)]

## Features of the Error List  

The **Error List** provides the following functionality:  
  
-   As you edit scripts, the **Error List** displays the errors and warnings produced by IntelliSense in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor.  
  
-   You can double-click any error message entry to focus on the tab for the script file that generated the error, and move to the error location.  
  
-   You can filter which entries you want to display, and which columns of information you want appear for each entry.  
  
-   After you fix an error, the error entry is removed from the **Error List**.  
  
-   When you close the tab for a [!INCLUDE[tsql](../../includes/tsql-md.md)] script file, the errors for that file are removed from the **Error List**.  
  
## Working with the Error List  
 To display the **Error List**, do one of the following:  
  
-   On the **View** menu, click **Error List**.  
  
-   Enter the keyboard shortcut CTRL+\\, CTRL+E.  
  
 After you open the **Error List**, you can customize your view by performing the following actions:  
  
-   To sort the list, click any column header. To sort again by an additional column, press and hold the SHIFT key, and then click another column header.  
  
-   To select which columns are displayed and which are hidden, select **Show Columns** from the shortcut menu.  
  
-   To change the order in which columns are displayed, drag any column header to the left or right.  
  
 The **Error List** does not link to additional information about specific errors.  
  
## Transact-SQL Errors in Management Studio  
 SQL Server Management Studio displays errors for [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts in the following locations:  
  
-   The **Error List** contains all syntax and semantic errors found by IntelliSense in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Editor. This list of errors is dynamically updated as you edit [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts. The list includes all errors that the editor has found in each [!INCLUDE[tsql](../../includes/tsql-md.md)] script. The editor does not stop parsing a file after encountering errors in a script. In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], IntelliSense in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Editor does not support all [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax elements. The **Error List** contains only errors from the [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax that is supported by IntelliSense.  
  
-   The **Messages** tab at the bottom of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window displays all errors and messages that are returned by the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] when a [!INCLUDE[tsql](../../includes/tsql-md.md)] script is executed. This list does not change until you execute the script again. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] stops parsing a batch after it finds one or two compile errors; therefore, the **Messages** tab might not list all errors in a script.  
  
 Sometimes errors are listed in both locations. For example, a script file might have a syntax error that is listed in the **Error List**. If you execute the script before you correct the error, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] parser can detect the same condition and return another copy of the error message in the **Messages** tab.  
  
> [!NOTE]  
>  The **Error List** only displays errors from the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor; it does not display errors from the MDX, DMX, or XML/A Editors. All MDX, DMX, and XML/A errors are displayed in the **Messages** tab in those editors.  
  
## UI element list  
 When the **Error List** is open, the information is displayed in the following columns:  
  
 **Default Order**  
 Displays an integer that indicates the order in which an entry was generated.  
  
 **Description**  
 Displays the text of the error entry. Lengthy descriptions wrap onto additional lines.  
  
 **File**  
 Displays the name of the script file that generated the error.  
  
 **Line**  
 Displays an integer that indicates which line of the code includes the error.  
  
 **Column**  
 Displays an integer that indicates the position of the error in the line of code.  
  
 **Project**  
 Displays the name of the project that includes the script file.  
