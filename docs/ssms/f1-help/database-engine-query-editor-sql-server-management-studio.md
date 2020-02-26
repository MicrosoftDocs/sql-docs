---
title: Database Engine Query Editor
ms.prod: sql
ms.prod_service: sql-tools
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.tsqlquery.f1"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Query Editor [Database Engine]"
  - "Transact-SQL Editor See Query Editor [Database Engine]"
  - "Database Engine Query Editor See Query Editor [Database Engine]"
  - "Query Editor [Database Engine], Toolbar"
  - "editors [SQL Server Management Studio], Database Engine Query Editor"
  - "Query Editor [Database Engine], Features"
  - "SQL Server Management Studio [SQL Server], Database Engine Query Editor"
ms.assetid: 05cfae9b-96d5-4a35-a098-0bc3a548bcfc
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 02/27/2020
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# SSMS Query Editor (database engine)

[!INCLUDE[appliesto-ss-asdb-asdw-xxx-md.md](../../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

The Query Editor creates and runs scripts containing [!INCLUDE[tsql](../../includes/tsql-md.md)] statements and **sqlcmd** commands.

The Query Editor is one of four editors implemented in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For a description of the functionality implemented in the Query Editor and the main tasks you can perform using the editor, see [Query and Text Editors](../scripting/query-and-text-editors-sql-server-management-studio.md)

You can add the SQL Editor toolbar by selecting the **View** menu, selecting **Toolbars**, and then selecting **SQL Editor**. If you add the SQL Editor toolbar when no [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor windows are open, all the buttons are unavailable.

![New query
](media/database-engine-query-editor-sql-server-management-studio/new-query.png)

## Transact-SQL F1 Help

The Query Editor supports linking you to the reference topic for a specific [!INCLUDE[tsql](../../includes/tsql-md.md)] statement when you select F1. To do so, highlight the name of a Transact-SQL statement and then select F1. The help search engine then searches for a topic that has an F1 help attribute that matches the string you highlighted.

If the help search engine doesn't find a topic with an F1 help keyword that exactly matches the string you highlighted, then this topic is displayed. In that case, there are two approaches to finding the help you're looking for:

- Copy and paste the editor string you highlighted into the search tab of SQL Server Books Online and do a search.

- Highlight only the part of the Transact-SQL statement likely to match an F1 help keyword applied to a topic and select F1 again. The search engine requires an exact match between the string you highlighted and an F1 help keyword assigned to a topic. If the string you highlighted contains elements unique to your environment, such as column or parameter names, the search engine doesn't get a match. Examples of the strings to highlight include:

  - The name of a Transact-SQL statement, such as SELECT, CREATE DATABASE or BEGIN TRANSACTION.
  
  - The name of a built-in function, such as SERVERPROPERTY, or @@VERSION.
  
  - The name of a system stored procedure table, or view, such as sys.data_spaces or sp_tableoption.

## SQL Editor Toolbar

![Editor toolbar](media/database-engine-query-editor-sql-server-management-studio/editor-toolbar.png)

When the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor is open, the SQL Editor toolbar appears with the following buttons.

### Connect

Opens the **Connect to Server** dialog box. Use this dialog box to establish a connection to a server.

![Connect icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-connect.png)

### Change Connection

Opens the **Connect to Server** dialog box. Use this dialog box to establish a connection to a different server.

![Change Connection icon
](media/database-engine-query-editor-sql-server-management-studio/toolbar-change-connection.png)

### Available Databases

Change the connection to a different database on the same server.

![Available Databases icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-available-databases-2.png)

### Execute

Executes the selected code or, if no code is selected, executes all the code in the Query Editor.

![Execute icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-execute-2.png)

### Cancel Executing Query

Sends a cancellation request to the server. Some queries can't be canceled immediately, but must wait for a suitable cancellation condition. When transactions are canceled, delays might occur while transactions are rolled back.

![Cancel Execution Query icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-cancel-execute-query.png)

### Parse

Check the syntax of the selected code. If no code is selected, it checks the syntax of the all code in the Query Editor window.

![Parse icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-parse.png)

### Display Estimated Execution Plan

Requests a query execution plan from the query processor without actually executing the query, and displays the plan in the **Execution plan** window. This plan uses index statistics as an estimate of the number of rows that are expected to be returned during each part of the query execution. The actual query plan that is used can be different from the estimated execution plan. This can occur if the number of rows that are returned is significantly different from the estimate, and the query processor changes the plan to be more efficient.

![Display Estimated Execution Plan icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-display-estimated-execution-plan.png)

### Query Options

Opens the **Query Options** dialog box. Use this dialog box to configure the default options for query execution and for query results.

![Query Options icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-query-options.png)

### IntelliSense Enabled

Specifies whether IntelliSense functionality is available in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor.

![Intellisense Enabled icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-intellisense-enabled.png)

### Include Actual Execution Plan

Executes the query, returns the query results, and the execution plan that was used for the query. These appear as a graphical query plan in the ### Execution plan###  window.

![Include Actual Execution Plan icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-include-actual-execution-plan.png)

### Include Live Query Statistics

Provides real-time insights into the query execution process as the controls flow from one query plan operator to another.

![Include Live Query Statistics icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-include-live-query-statistics.png)

### Include Client Statistics

Includes a **Client Statistics** window that contains statistics about the query and about the network packets, and the elapsed time of the query.

![Include Client Statistics icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-include-client-statistics.png)

### Results to Text

Returns the query results as text in the **Results** window.

![Results to Text icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-results-to-text.png)

### Results to Grid

Returns the query results as one or more grids in the **Results** window.

![Results to Grid icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-results-to-grid.png)

### Results to File

When the query executes, the **Save Results** dialog box opens. In **Save In**, select the folder in which you want to save the file. In **File name**, type the name of the file, and then select **Save** to save the query results as a **Report** file that has the .rpt extension. For advanced options, click the down-arrow on the **Save** button, and then select **Save with Encoding**.

![Results to File icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-results-to-file.png)

### Comment out the selected lines

Makes the current line a comment by adding a comment operator (--) at the beginning of the line.

![Comment Selection icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-comment-selection.png)

### Uncomment the selected lines

Makes the current line an active source statement by removing any comment operator (--) at the beginning of the line.

![Uncomment Selection icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-uncomment-selection.png)

### Decrease Indent

Moves the text of the line to the left by removing blanks at the beginning of the line.

![Decrease Indent icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-decrease-indent.png)

### Increase Line Indent

Moves the text of the line to the right by adding blanks at the beginning of the line.

![Increase Line Indent icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-increase-indent.png)

### Specify Values for Template Parameters

Opens a dialog box that you can use to specify values for parameters in stored procedures and functions.

![Specify Values for Template Parameters icon](media/database-engine-query-editor-sql-server-management-studio/toolbar-specify-values-for-template-parameters.png)

## See also

[SQL Server Management Studio Keyboard Shortcuts](../../ssms/sql-server-management-studio-keyboard-shortcuts.md)