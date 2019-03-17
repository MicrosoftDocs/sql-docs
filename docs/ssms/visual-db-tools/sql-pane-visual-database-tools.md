---
title: "SQL Pane (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "Query Designer [SQL Server], SQL pane"
  - "View Designer, SQL pane"
  - "SQL pane [Visual Database Tools]"
ms.assetid: dbabab18-0614-415b-a2ef-9bcd0d320d5c
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# SQL Pane (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
You can use the SQL pane to create your own SQL statement, or you can use the Criteria pane and Diagram pane to create the statement, in which case the SQL statements will be created in the SQL pane. As you build your query, the SQL pane automatically updates and reformats for easy readability.  
  
To open the SQL pane, first open Query and View Designer (with a database object selected in Server Explorer, from the **Database** menu, click **New Query**). Then from the **Query Designer** menu point to **Pane** and click **SQL**.  
  
In the SQL pane you can:  
  
-   Create new queries by entering SQL statements.  
  
-   Modify the SQL statement created by the Query and View Designer based on settings you make in the Diagram and Criteria panes.  
  
-   Enter statements that take advantage of features specific to the database you are using.  
  
> [!NOTE]  
> Be sure you know the rules for identifying database objects in the database you are using. For details, see the documentation for your database management system.  
  
## Statements in the SQL Pane  
You can edit the current query directly in the SQL pane. When you move to another pane, the Query and View Designer automatically formats your statement, and then changes the Diagram and Criteria panes to match your statement.  
  
If your statement cannot be represented in the Diagram and Criteria panes, and if those panes are visible, Query and View Designer displays an error and then offers you two choices:  
  
-   Ignore that the statement can not be represented in the Diagram and Criteria panes.  
  
-   Undo the change that can not be represented and revert to the most recent version of the SQL statement.  
  
If you choose to ignore that the statement can not be represented in the Diagram and Criteria panes, the Query and View Designer dims the other panes to indicate that they no longer reflect the contents of the SQL pane.  
  
You can continue to edit the statement and execute it as you would any SQL statement.  
  
> [!NOTE]  
> If you enter an SQL statement, but then make further changes to the query by changing the Diagram and Criteria panes, the Query and View Designer rebuilds and redisplays the SQL statement. In some cases, this action results in an SQL statement that is constructed differently from the one you originally entered (though it will always yield the same results). This difference is particularly likely when you are working with search conditions that involve several clauses linked with AND and OR.  
  
## See Also  
[Create Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/create-queries-visual-database-tools.md)  
[Run Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/run-queries-visual-database-tools.md)  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
[Diagram Pane &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/diagram-pane-visual-database-tools.md)  
[Criteria Pane &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/criteria-pane-visual-database-tools.md)  
[Results Pane &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/results-pane-visual-database-tools.md)  
  
