---
title: "Use Transact-SQL Editor to Edit and Execute Scripts | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "SQL.DATA.TOOLS.SQLEDITOR"
ms.assetid: fa78e2cf-3c64-49f5-93cc-a3d50b1e7d05
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# Use Transact-SQL Editor to Edit and Execute Scripts
The Transact\-SQL Editor provides you with a rich editing and debugging experience when you are working with scripts. It is invoked when you use the **View Code** contextual menu to open a database entity in a connected database or a project. It is also automatically opened when you use the **New Query** contextual menu from the SQL Server Object Explorer, or add a new script object to a database project.  
  
If you are not connected to a database, but want to execute a query against it, you can also use the **New Query Connection** dialog box in the **SQL** -> **Transact\-SQL Editor** menu option to connect to a database and launch the Transact\-SQL Editor.  
  
The Transact\-SQL Editor contains a main **T-SQL** pane where you can write and edit Transact\-SQL scripts. The editor supports IntelliSense as well as color coding of syntax to improve the readability of complex statements. It also supports find and replace, bulk commenting, custom fonts and colors, and line numbering. You can also change the database which the script in the editor will be executed against. For more information, see [How to: Clone an Existing Database](../ssdt/how-to-clone-an-existing-database.md). The **Results** pane displays query results in a grid or text. You can also redirect query results to a file. The **Message** pane displays errors, warnings, and informational messages returned when a script is run. When client statistics is enabled, the **Statistics** pane displays information about the query execution grouped into categories. The **Execution Plan** pane displays the data retrieval methods chosen by SQL Server, and shows the execution cost of specific statements and queries.  
  
## In This Section  
  
|Topic|Description|  
|---------|---------------|  
|[How to: Outline and Add Snippets to Transact-SQL Script](../ssdt/how-to-outline-and-add-snippets-to-transact-sql-script.md)|Use the Snippet Picker to insert ready-made Transact\-SQL code to your query.|  
|[How to: Navigate Between Scripts](../ssdt/how-to-navigate-between-scripts.md)|Use Go-To-Definition and Find All Reference to navigate between scripts.|  
|[How to: Use Rename and Refactoring to Make Changes to your Database Objects](../ssdt/how-to-use-rename-and-refactoring-to-make-changes-to-your-database-objects.md)|Rename an object across all scripts and preview any changes.|  
|[How to: Execute a Partial Query](../ssdt/how-to-execute-a-partial-query.md)|Highlight a specific segment of the script and execute it as a single query.|  
|[How to: Debug Stored Procedures](../ssdt/how-to-debug-stored-procedures.md)|Create and debug a Transact\-SQL stored procedure by stepping into it.|  
|[Analyze Script Performance](../ssdt/analyze-script-performance.md)|Use execution plans, client statistics and code analysis to determine whether you can improve performance of your query, stored procedures, or scripts.|  
  
## See Also  
[How to: Create New Database Objects Using Queries](../ssdt/how-to-create-new-database-objects-using-queries.md)  
  
