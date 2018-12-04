---
title: "Supported Query Types (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "Delete query"
  - "queries [SQL Server], types"
  - "Update query"
  - "Query Designer [SQL Server], query types"
  - "Criteria pane"
  - "Insert Values query"
  - "Select query"
  - "Make Table query"
  - "Insert Results query"
  - "Diagram pane [Visual Database Tools]"
  - "View Designer, query types"
ms.assetid: 72b9116c-c128-4078-a78d-257a2955a3f6
author: stevestein
ms.author: sstein
manager: craigg
---
# Supported Query Types (Visual Database Tools)
  You can create the following types of queries in the Diagram and Criteria panes (the graphical panes) of the [Query and View Designer](visual-database-tools.md):  
  
-   **Select query** Retrieves data from one or more tables or views. This type of query creates an SQL SELECT statement.  
  
-   **Insert Results** Creates new rows by copying existing rows from one table into another, or into the same table as new rows. This type of query creates an SQL INSERT INTO...SELECT statement.  
  
-   **Insert Values** Creates a new row and inserts values into specified columns. This type of query creates an SQL INSERT INTO...VALUES statement.  
  
-   **Update query** Changes the values of individual columns in one or more existing rows in a table. This type of query creates an SQL UPDATE...SET statement.  
  
-   **Delete query** Removes one or more rows from a table. This type of query creates an SQL DELETE statement.  
  
    > [!NOTE]  
    >  A Delete query removes entire rows from the table. If you want to delete values from individual data columns, use an Update query.  
  
-   **Make Table query** Creates a new table and creates rows in it by copying the results of a query into it. This type of query creates an SQL SELECT...INTO statement.  
  
 In addition to the queries you can create using the graphical panes, you can enter any SQL statement into the SQL pane, such as Union queries.  
  
 When you create queries using SQL statements that cannot be represented in the graphical panes, the Query and View Designer dims those panes to indicate that they do not reflect the query you are creating. However, the dimmed panes are still active and, in many cases, you can make changes to the query in those panes. If the changes you make result in a query that can be represented in the graphical panes, those panes are no longer dimmed.  
  
## See Also  
 [Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](design-queries-and-views-how-to-topics-visual-database-tools.md)   
 [Types of Queries &#40;Visual Database Tools&#41;](types-of-queries-visual-database-tools.md)  
  
  
