---
title: "Specify the TOP Clause in Queries (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "TOP clause, queries"
  - "percentage of rows returned [SQL Server]"
  - "number of rows"
  - "search conditions [SQL Server], TOP clause"
  - "restricting rows returned"
  - "search criteria [SQL Server], limiting rows returned"
  - "inspecting portion of results"
  - "limiting rows returned"
  - "search criteria [SQL Server], TOP clause"
ms.assetid: ba7d7c10-9bb3-4d9b-90b0-5fa94ecae59b
author: "markingmyname"
ms.author: "maghan"
manager: craigg

---
# Specify the TOP Clause in Queries (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
The TOP clause returns only the first *n* or *n percent* rows from a query. The TOP clause is useful when you want to inspect a portion of your results to find out if your query does what you expect it to do, without taking the resources necessary to return all of the query results.  
  
### To specify the TOP clause in queries  
  
1.  Open a query from Solution Explorer or create a new one.  
  
2.  From the **View** menu, click **Properties Window**.  
  
3.  In the **Properties Window**, locate and expand the **Top Specification** property.  
  
4.  Click the **(Top)** child property and set it to **Yes**.  
  
5.  In the **Expression** child property, type an expression that has a numeric result (for example, "10" or "2*5").  
  
6.  Click the **Percent** child property and indicate whether or not to treat the **Expression** property as a percentage of all rows returned (Yes) or as the absolute number of rows returned (No).  
  
7.  If your query uses the ORDER BY clause, click the **With Ties** child property and choose **Yes** to display all rows in a group if only part of the group is included or **No** to truncate them.  
  
As you work through the preceding procedure, notice that the TOP clause, displayed in the SQL pane, changes to reflect the current settings of the properties.  
  
> [!NOTE]  
> You can also change the values of the child properties of the **Top Specification** by editing the TOP clause in the SQL pane.  
  
## See Also  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
[Query Properties &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/query-properties-visual-database-tools.md)  
  
