---
title: "Integration Services (SSIS) Queries | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.querybuilder.f1"
helpviewer_keywords: 
  - "Query Builder [Integration Services]"
  - "queries [Integration Services]"
  - "statements [Integration Services]"
  - "queries [Integration Services], about queries in packages"
ms.assetid: 8822bd29-4575-46c8-92a0-1a39bc2604c1
author: janinezhang
ms.author: janinez
manager: craigg
---
# Integration Services (SSIS) Queries

[!INCLUDE[ssis-appliesto](../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The Execute SQL task, the OLE DB source, the OLE DB destination, and the Lookup transformation can use SQL queries. In the Execute SQL task, the SQL statements can create, update, and delete database objects and data; run stored procedures; and perform SELECT statements. In the OLE DB source and the Lookup transformation, the SQL statements are typically SELECT statements or EXEC statements. The latter most frequently run stored procedures that return result sets.  
  
 A query can be parsed to establish whether it is valid. When parsing a query that uses a connection to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], the query is parsed, executed, and the execution outcome (success or failure) is assigned to the parsing outcome. If the query uses a connection to a data other than [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], the statement is parsed only.  
  
You can provide the SQL statement in the following ways:
1.   Enter it directly in the designer.
2.   Specify a connection to a file contains the statement.
3.   Specify a variable that contains the statement.  
  
## Direct Input SQL  
 Query Builder is available in the user interface for the Execute SQL task, the OLE DB source, the OLE DB destination, and the Lookup transformation. Query Builder offers the following advantages:  
  
-   Work visually or with SQL commands.  
  
     Query Builder includes graphical panes that compose your query visually and a text pane that displays the SQL text of your query. You can work in either the graphical or text panes. Query Builder synchronizes the views so that the query text and graphical representation always match.  
  
-   Join related tables.  
  
     If you add more than one table to your query, Query Builder automatically determines how the tables are related and constructs the appropriate join command.  
  
-   Query or update databases.  
  
     You can use Query Builder to return data using Transact-SQL SELECT statements, or to create queries that update, add, or delete records in a database.  
  
-   View and edit results immediately.  
  
     You can execute your query and work with a recordset in a grid that lets you scroll through and edit records in the database.  
  
 Although Query Builder is visually limited to creating SELECT queries, you can type the SQL for other types of statements such as DELETE and UPDATE statements in the text pane. The graphical pane is automatically updated to reflect the SQL statement that you typed.  
  
 You can also provide direct input by typing the query in the task or data flow component dialog box or the Properties window.  
  
 For more information, see [Query Builder](https://msdn.microsoft.com/library/780752c9-6e3c-4f44-aaff-4f4d5e5a45c5).  
  
## SQL in Files  
 The SQL statement for the Execute SQL task can also reside in a separate file. For example, you can write queries using tools such as the Query Editor in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], save the query to a file, and then read the query from the file when running a package. The file can contain only the SQL statements to run and comments. To use a SQL statement stored in a file, you must provide a file connection that specifies the file name and location. For more information, see [File Connection Manager](../integration-services/connection-manager/file-connection-manager.md).  
  
## SQL in Variables  
 If the source of the SQL statement in the Execute SQL task is a variable, you provide the name of the variable that contains the query. The Value property of the variable contains the query text. You set the ValueType property of the variable to a string data type and then type or copy the SQL statement into the Value property. For more information, see [Integration Services &#40;SSIS&#41; Variables](../integration-services/integration-services-ssis-variables.md) and [Use Variables in Packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787).  

## Query Builder dialog box
Use the **Query Builder** dialog box to create a query for use in the Execute SQL task, the OLE DB source and the OLE DB destination, and the Lookup transformation.  
  
 You can use Query Builder to perform the following tasks:  
  
-   **Working with a graphical representation of a query or with SQL commands** Query Builder includes a pane that displays your query graphically and a pane that displays the SQL text of your query. You can work in either the graphical pane or the text pane. Query Builder synchronizes the views so that they are always current.  
  
-   **Joining related tables** If you add more than one table to your query, Query Builder automatically determines how the tables are related and constructs the appropriate join command.  
  
-   **Querying or updating databases** You can use Query Builder to return data by using Transact-SQL SELECT statements and to create queries that update, add, or delete records in a database.  
  
-   **Viewing and editing results immediately** You can run your query and work with a recordset in a grid that allows you to scroll through and edit records in the database.  
  
 The graphical tools in the **Query Builder** dialog box let you construct queries using drag-and-drop operations. By default, the Query Builder dialog box constructs SELECT queries, but you can also build INSERT, UPDATE, or DELETE queries. All types of SQL statements can be parsed and run in the **Query Builder** dialog box. For more information about SQL statements in packages, see [Integration Services &#40;SSIS&#41; Queries](../integration-services/integration-services-ssis-queries.md).  
  
 To learn more about the Transact-SQL language and its syntax, see [Transact-SQL Reference &#40;Database Engine&#41;](../t-sql/transact-sql-reference-database-engine.md).  
  
 You can also use variables in a query to provide values to an input parameter, to capture values of output parameters, and to store return codes. To learn more about using variables in the queries that packages use, see [Execute SQL Task](../integration-services/control-flow/execute-sql-task.md), [OLE DB Source](../integration-services/data-flow/ole-db-source.md), and [Integration Services &#40;SSIS&#41; Queries](../integration-services/integration-services-ssis-queries.md). To learn more about using variables in the Execute SQL Task, see [Parameters and Return Codes in the Execute SQL Task](https://msdn.microsoft.com/library/a3ca65e8-65cf-4272-9a81-765a706b8663) and [Result Sets in the Execute SQL Task](https://msdn.microsoft.com/library/62605b63-d43b-49e8-a863-e154011e6109).  
  
 The Lookup and Fuzzy lookup transformations can also use variables with parameters and return codes. The information about the OLE DB source applies to these two transformations also.  
  
### Options  
 **Toolbar**  
 Use the toolbar to manage datasets, select panes to display, and control query functions.  
  
|Value|Description|  
|-----------|-----------------|  
|**Show/Hide Diagram Pane**|Shows or hides the **Diagram** pane.|  
|**Show/Hide Grid Pane**|Shows or hides the **Grid** pane.|  
|**Show/Hide SQL Pane**|Shows or hides the **SQL** pane.|  
|**Show/Hide Results Pane**|Shows or hides the **Results** pane.|  
|**Run**|Runs the query. Results are displayed in the result pane.|  
|**Verify SQL**|Verifies that the SQL statement is valid.|  
|**Sort Ascending**|Sorts output rows on the selected column in the grid pane, in ascending order.|  
|**Sort Descending**|Sorts output rows on the selected column in the grid pane, in descending order.|  
|**Remove Filter**|Select a column name in the grid pane, and then click **Remove Filter** to remove sort criteria for the column.|  
|**Use Group By**|Adds GROUP BY functionality to the query.|  
|**Add Table**|Adds a new table to the query.|  
  
 **Query Definition**  
 The query definition provides a toolbar and panes in which to define and test the query.  
  
|Pane|Description|  
|----------|-----------------|  
|**Diagram** pane|Displays the query in a diagram. The diagram shows the tables included in the query, and how they are joined. Select or clear the check box next to a column in a table to add or remove it from the query output.<br /><br /> When you add tables to the query, Query Builder creates joins between tables based on tables, depending on the keys in the table. To add a join, drag a field from one table onto a field in another table. To manage a join, right-click the join, and then select a menu option.<br /><br /> Right-click the **Diagram** pane to add or remove tables, select all the tables, and show or hide panes.|  
|**Grid** pane|Displays the query in a grid. You can use this pane to add to and remove columns from the query and change the settings for each column.|  
|**SQL** pane|Displays the query as SQL text. Changes made in the **Diagram** pane and the **Grid** pane will appear here, and changes made here will appear in the **Diagram** pane and the **Grid** pane.|  
|**Results** pane|Displays the results of the query when you click **Run** on the toolbar.| 

  
