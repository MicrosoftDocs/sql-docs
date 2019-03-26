---
title: "Database Engine Query Editor (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.technology: scripting
ms.reviewer: ""
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
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Database Engine Query Editor (SQL Server Management Studio)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  Use the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor to create and run scripts containing [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. The editor also supports running scripts that contain **sqlcmd** commands.  
  
## Transact-SQL F1 Help  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor supports linking you to the reference topic for a specific [!INCLUDE[tsql](../../includes/tsql-md.md)] statement when you select F1. To do so, highlight the name of a Transact-SQL statement and then select F1. The help search engine will then search for a topic that has an F1 help attribute that matches the string you highlighted.  
  
 If the help search engine does not find a topic with an F1 help keyword that exactly matches the string you highlighted, then this topic is displayed. In that case, there are two approaches to finding the help you are looking for:  
  
-   Copy and paste the editor string you highlighted into the search tab of SQL Server Books Online and do a search.  
  
-   Highlight only the part of the Transact-SQL statement likely to match an F1 help keyword applied to a topic and select F1 again. The search engine requires an exact match between the string you highlighted and an F1 help keyword assigned to a topic. If the string you highlighted contains elements unique to your environment, such as column or parameter names, the search engine will not get a match. Examples of the strings to highlight include:  
  
    -   The name of a Transact-SQL statement, such as SELECT, CREATE DATABASE or BEGIN TRANSACTION.  
  
    -   The name of a built-in function, such as SERVERPROPERTY, or @@VERSION.  
  
    -   The name of a system stored procedure table, or view, such as sys.data_spaces or sp_tableoption.  
  
## Working With the Database Engine Query Editor  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor is one of four editors implemented in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For a description of the functionality implemented in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor and the main tasks you can perform using the editor, see [Query and Text Editors &#40;SQL Server Management Studio&#41;](../../relational-databases/scripting/query-and-text-editors-sql-server-management-studio.md).  
  
## SQL Editor Toolbar  
 When the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor is open, the SQL Editor toolbar appears with the following buttons.  
  
 **Connect**  
 Opens the **Connect to Server** dialog box. Use this dialog box to establish a connection to a server.  
  
 **Disconnect**  
 Disconnects the current Query Editor from the server.  
  
 **Change Connection**  
 Opens the **Connect to Server** dialog box. Use this dialog box to establish a connection to a different server.  
  
 **New Query with Current Connection**  
 Opens a new Query Editor window and uses the connection information from the current Query Editor window.  
  
 **Available Databases**  
 Change the connection to a different database on the same server.  
  
 **Execute**  
 Executes the selected code or, if no code is selected, executes all the code in the Query Editor.  
  
 **Debug**  
 Enables the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger. This debugger supports debugging actions such as setting breakpoints, watching variables, and stepping through code.  
  
 **Cancel Executing Query**  
 Sends a cancellation request to the server. Some queries cannot be canceled immediately, but must wait for a suitable cancellation condition. When transactions are canceled, delays might occur while transactions are rolled back.  
  
 **Parse**  
 Check the syntax of the selected code. If no code is selected, checks the syntax of the all code in the Query Editor window.  
  
 **Display Estimated Execution Plan**  
 Requests a query execution plan from the query processor without actually executing the query, and displays the plan in the **Execution plan** window. This plan uses index statistics as an estimate of the number of rows that are expected to be returned during each part of the query execution. The actual query plan that is used can be different from the estimated execution plan. This can occur if the number of rows that are returned is significantly different from the estimate, and the query processor changes the plan to be more efficient.  
  
 **Query Options**  
 Opens the **Query Options** dialog box. Use this dialog box to configure the default options for query execution and for query results.  
  
 **IntelliSense Enabled**  
 Specifies whether IntelliSense functionality is available in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor.  
  
 **Include Actual Execution Plan**  
 Executes the query, returns the query results, and the execution plan that was used for the query. These appear as a graphical query plan in the **Execution plan** window.  
  
 **Include Client Statistics**  
 Includes a **Client Statistics** window that contains statistics about the query and about the network packets, and the elapsed time of the query.  
  
 **Results to Text**  
 Returns the query results as text in the **Results** window.  
  
 **Results to Grid**  
 Returns the query results as one or more grids in the **Results** window.  
  
 **Results to File**  
 When the query executes, the **Save Results** dialog box opens. In **Save In**, select the folder in which you want to save the file. In **File name**, type the name of the file, and then click **Save** to save the query results as a **Report** file that has the .rpt extension. For advanced options, click the down-arrow on the **Save** button, and then click **Save with Encoding**.  
  
 **Comment Selection**  
 Makes the current line a comment by adding a comment operator (--) at the beginning of the line.  
  
 **Uncomment Selection**  
 Makes the current line an active source statement by removing any comment operator (--) at the beginning of the line.  
  
 **Decrease Line Indent**  
 Moves the text of the line to the left by removing blanks at the beginning of the line.  
  
 **Increase Line Indent**  
 Moves the text of the line to the right by adding blanks at the beginning of the line.  
  
 **Specify Values for Template Parameters**  
 Opens a dialog box that you can use to specify values for parameters in stored procedures and functions.  
  
 You can also add the SQL Editor toolbar by selecting the **View** menu, selecting **Toolbars**, and then selecting **SQL Editor**. If you add the SQL Editor toolbar when no [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor windows are open, all the buttons are unavailable.  
  
## SQL Editor Toolbar  
 When a [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window is open, you can add the Debug toolbar by selecting the **View** menu, selecting **Toolbars**, and then selecting **Debug**. If you add the Debug toolbar when no [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor windows are open, all the buttons are unavailable.  
  
 **Continue**  
 Runs the code in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window until a breakpoint is encountered.  
  
 **Break All**  
 Sets the debugger to break all processes to which the debugger is attached when a break occurs.  
  
 **Stop Debugging**  
 Takes the selected [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window out of debug mode, and restores the standard execution mode.  
  
 **Show Next Statement**  
 Moves the cursor to the next statement to be executed.  
  
 **Step Into**  
 The next statement is run. If the statement invokes a Transact-SQL stored procedure, function, or trigger, the debugger displays a new **Query Editor** window that contains the code of the module. The window is in debug mode, and execution pauses on the first statement in the module. You can then move through the module, for example, by setting breakpoints or stepping through the code.  
  
 **Step Over**  
 The next statement is run. If the statement invokes a Transact-SQL stored procedure, function, or trigger, the module is run until it finishes and the results are returned to the calling code. If you are sure there are no errors in the module, you can step over it. Execution pauses on the statement that follows the call to the module.  
  
 **Step Out**  
 Step back to the next highest calling level (function, stored procedure, or trigger). Execution pauses on the statement that follows the call to the stored procedure, function, or trigger.  
  
 **Windows**  
 Opens either the **Breakpoint** window or the **Immediate** window.  
  
## See Also  
 [SQL Server Management Studio Keyboard Shortcuts](../../tools/sql-server-management-studio/sql-server-management-studio-keyboard-shortcuts.md)  
  
  
