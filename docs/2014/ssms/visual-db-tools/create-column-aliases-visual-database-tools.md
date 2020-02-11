---
title: "Create Column Aliases (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "columns [SQL Server], aliases"
  - "aliases [SQL Server], columns"
ms.assetid: e2e1c166-8ea7-47a2-b6a7-e419bf0fa3bb
author: stevestein
ms.author: sstein
manager: craigg
---
# Create Column Aliases (Visual Database Tools)
  You can create aliases for column names to make it easier to work with column names, calculations, and summary values. For example, you can create a column alias to:  
  
-   Create a column name, such as "Total Amount," for an expression such as `(quantity * unit_price)` or for an aggregate function.  
  
-   Create a shortened form of a column name, such as `"d_id"` for `"discounts.stor_id."`  
  
 After you have defined a column alias, you can use the alias in a Select query to specify query output.  
  
### To create a column alias  
  
1.  In the **Criteria Pane**, locate the row containing the data column for which you want to create an alias, and if necessary, mark it for output. If the data column is not already in the grid, add it.  
  
2.  In the **Alias** column for that row, enter the alias. The alias must follow all naming conventions for SQL. If the alias name you enter contains spaces, the Query and View Designer automatically puts delimiters around it.  
  
## See Also  
 [Add Columns to Queries &#40;Visual Database Tools&#41;](visual-database-tools.md)   
 [Sort and Group Query Results &#40;Visual Database Tools&#41;](sort-and-group-query-results-visual-database-tools.md)   
 [Summarize Query Results &#40;Visual Database Tools&#41;](summarize-query-results-visual-database-tools.md)   
 [Perform Basic Operations with Queries &#40;Visual Database Tools&#41;](perform-basic-operations-with-queries-visual-database-tools.md)  
  
  
