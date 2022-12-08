---
description: "Create Queries (Visual Database Tools)"
title: Create Queries
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "queries [SQL Server], creating"
ms.assetid: 696a080d-848f-44d3-a918-e29bafaab85a
author: markingmyname
ms.author: maghan
ms.reviewer: 

---
# Create Queries (Visual Database Tools)
[!INCLUDE[SQL Server](../../includes/applies-to-version/sqlserver.md)]
Queries allow you to retrieve data from the tables and views in your database. You create and work with queries in **Query and View Designer**, which is composed of four panes: the [Diagram Pane](../../ssms/visual-db-tools/diagram-pane-visual-database-tools.md), the [SQL Pane](../../ssms/visual-db-tools/sql-pane-visual-database-tools.md), the [Criteria Pane](../../ssms/visual-db-tools/criteria-pane-visual-database-tools.md), and the [Results Pane](../../ssms/visual-db-tools/results-pane-visual-database-tools.md).  
  
### To create a new query  
  
1.  In **Object Explorer**, expand the **Tables** node for the database you want to query. Right-click the table you want to query and click **Open Table**.  
  
2.  To add more tables to the query, on the Query Designer menu, select **Add Table**.  
  
    > [!NOTE]  
    > If you do not see the **Diagram**, **SQL**, **Criteria**, or **Results** panes, from the Query Designer menu, point to **Pane** and click the pane you want to open.  
  
3.  In the **Add Table** dialog box, select the tables you want to query and click **Add** for each one.  
  
4.  Once you have added all the tables you want to query, click **Close**.  
  
    To add more tables later, right-click the open space in the **Diagram** pane and from the shortcut menu click **Add Table**.  
  
5.  In the **Diagram Pane**, check the boxes in the table-valued objects for each column you want to query.  
  
6.  From the Query Designer menu, choose **Execute SQL** to run your query.  
  
To further refine your query, you can change the SQL code in the **SQL Pane** or choose options such as sort order and column aliases in the **Criteria Pane.**  
  
## See Also  
[Save Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/save-queries-visual-database-tools.md)  
[Types of Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/types-of-queries-visual-database-tools.md)  
[Specify Search Criteria &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/specify-search-criteria-visual-database-tools.md)  
[Summarize Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/summarize-query-results-visual-database-tools.md)  
[Perform Basic Operations with Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/perform-basic-operations-with-queries-visual-database-tools.md)  
  
