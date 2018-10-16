---
title: "Query Builder | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.querybuilder.f1"
helpviewer_keywords: 
  - "Query Builder dialog box"
ms.assetid: 780752c9-6e3c-4f44-aaff-4f4d5e5a45c5
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Query Builder
  Use the **Query Builder** dialog box to create a query for use in the Execute SQL task, the OLE DB source and the OLE DB destination, and the Lookup transformation.  
  
 You can use Query Builder to perform the following tasks:  
  
-   **Working with a graphical representation of a query or with SQL commands** Query Builder includes a pane that displays your query graphically and a pane that displays the SQL text of your query. You can work in either the graphical pane or the text pane. Query Builder synchronizes the views so that they are always current.  
  
-   **Joining related tables** If you add more than one table to your query, Query Builder automatically determines how the tables are related and constructs the appropriate join command.  
  
-   **Querying or updating databases** You can use Query Builder to return data by using Transact-SQL SELECT statements and to create queries that update, add, or delete records in a database.  
  
-   **Viewing and editing results immediately** You can run your query and work with a recordset in a grid that allows you to scroll through and edit records in the database.  
  
 The graphical tools in the **Query Builder** dialog box let you construct queries using drag-and-drop operations. By default, the Query Builder dialog box constructs SELECT queries, but you can also build INSERT, UPDATE, or DELETE queries. All types of SQL statements can be parsed and run in the **Query Builder** dialog box. For more information about SQL statements in packages, see [Integration Services &#40;SSIS&#41; Queries](integration-services-ssis-queries.md).  
  
 To learn more about the Transact-SQL language and its syntax, see [Transact-SQL Reference &#40;Database Engine&#41;](/sql/t-sql/language-reference).  
  
 You can also use variables in a query to provide values to an input parameter, to capture values of output parameters, and to store return codes. To learn more about using variables in the queries that packages use, see [Execute SQL Task](control-flow/execute-sql-task.md), [OLE DB Source](data-flow/ole-db-source.md), and [Integration Services &#40;SSIS&#41; Queries](integration-services-ssis-queries.md). To learn more about using variables in the Execute SQL Task, see [Parameters and Return Codes in the Execute SQL Task](../../2014/integration-services/parameters-and-return-codes-in-the-execute-sql-task.md) and [Result Sets in the Execute SQL Task](../../2014/integration-services/result-sets-in-the-execute-sql-task.md).  
  
 The Lookup and Fuzzy lookup transformations can also use variables with parameters and return codes. The information about the OLE DB source applies to these two transformations also.  
  
## Options  
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
  
## See Also  
 [Execute SQL Task](control-flow/execute-sql-task.md)   
 [OLE DB Source](data-flow/ole-db-source.md)   
 [OLE DB Destination](data-flow/ole-db-destination.md)   
 [Lookup Transformation](data-flow/transformations/lookup-transformation.md)   
 [Integration Services &#40;SSIS&#41; Queries](integration-services-ssis-queries.md)   
 [MERGE in Integration Services Packages](control-flow/merge-in-integration-services-packages.md)  
  
  
