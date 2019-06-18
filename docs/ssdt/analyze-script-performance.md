---
title: "Analyze Script Performance | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.codeanalysis.configuring"
ms.assetid: f4bbdd31-12a5-4c57-b0fe-1c6683820f11
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# Analyze Script Performance
You can use the tools provided by SQL Server Data Tools to determine whether you can improve the performance of your query, stored procedures, or scripts. For example, by monitoring client statistics such as the response times for frequently used queries, you can determine whether changes to the query or indexes on the tables are required. Such statistics can include client execution time, query profile, and packets/bytes sent and received.  
  
In addition, certain performance problems are better addressed by analyzing the application queries and updates that the application submits to the database, and how these queries and updates interact with the data contained in the database and the database schema. Execution plans graphically display the data retrieval methods chosen by the SQL Server query optimizer, and show the execution cost of specific statements and queries. Thus they can help you understand how SQL Server will be processing your SQL query, and to determine what is causing performance slowdown.  
  
## Using Client Statistics  
When you run a script or query in the Transact\-SQL Editor, you can choose to collect client statistics such as application profile, network, and time statistics for the execution. Such metrics allow you to gauge the efficiency of your script, or benchmark different scripts.  
  
To toggle the gathering of client statistics, when the Transact\-SQL Editor is open, on the **Data** menu, point to **Transact\-SQL Editor**, click **Execution Settings** and **Include Client Statistics**. Alternatively, click the **Include Client Statistics** button (the fifth one from the right) on the Transact\-SQL Editor toolbar, or by right-clicking in the Transact\-SQL editor and then selecting **Execution Settings** and **Include Client Statistics**. Notice that in order to gather statistics for a query, you have to turn on this feature before executing it.  
  
If you turned on client statistics, the **Statistics** tab appears next to the **Message** tab upon query execution. If you turned off client statistics, the **Statistics** tab does not appear. Statistics from successive query executions are listed along with the average values.  
  
For more information on the statistics collected, see [Query Window Statistics Pane](https://msdn.microsoft.com/library/aa216969(SQL.80).aspx) and ["Client Statistics Tab" section of this topic](https://msdn.microsoft.com/library/aa833205.aspx).  
  
## Using Execution Plans  
Execution plans display how the database engine navigates tables and uses indexes to access or process the data for a query or other DML statement, such as an update. This graphical approach is very useful for understanding the performance characteristics of a query.  
  
Open a Transact\-SQL script that contains the queries you want to analyze into the Transact\-SQL editor. You can then highlight the code you want to review and choose to display an estimated execution plan by clicking the **Display Estimated Execution Plan** button on the editor toolbar. If you click **Display Estimated Execution Plan**, the Transact\-SQL queries or batches are not run. Instead, the script is parsed, and the query execution plan that the database engine would most probably use if the queries were actually executed is displayed.  
  
After the script is parsed or executed, click the **Execution plan** tab to see a graphical representation of execution plan output.  
  
The graphical execution plan output is read from right to left and from top to bottom. Each query in the batch that is analyzed is displayed, including the cost of each query as a percentage of the total cost of the batch. To view additional information such as cost and operation for each step, hover your mouse over the [logical and physical operator icons](https://msdn.microsoft.com/library/ms175913.aspx) in the graphical plan.  
  
To alter the display of the execution plan, right-click the **Execution plan** and select **Zoom In**, **Zoom Out**, **Custom Zoom**, or **Zoom to Fit**. **Zoom In** and **Zoom Out** allow you to magnify or reduce the execution plan by fixed amounts. **Custom Zoom** allows you to define your own display magnification, such as zooming at 80 percent.  **Zoom to Fit** adjusts the execution plan to fit the result pane.  
  
Execution plans can be saved and reopened later for examination. To do so, right-click the **Execution Plan**, and select **Save Execution Plan As**. After that, you can open the plan in Visual Studio just like opening any other kind of file.  
  
## Using Code Analysis  
You can use Code Analysis to discover potential issues in your scripts, such as design, naming and performance problems.  Rules for database projects are organized into predefined rule sets that target specific areas, and you can enable or disable any rule in the **Code Analysis** tab of the **Project Properties** property page. In the same tab, you can specify code analysis to be run automatically every time that a project is built, or whether warnings are treated as errors.  
  
To use Code Analysis manually, right-click your project in **Solution Explorer** and select **Run Code Analysis**. Code analysis warnings are listed in the **Error List** window. You can double-click a warning to navigate to the source code that contains the issue, and you can view additional information and possible corrections for a warning by using the **Show Error Help** contextual menu.  
  
For more information on Code Analysis, see [Analyzing Database Code to Improve Code Quality](https://msdn.microsoft.com/library/dd172133.aspx).  
  
