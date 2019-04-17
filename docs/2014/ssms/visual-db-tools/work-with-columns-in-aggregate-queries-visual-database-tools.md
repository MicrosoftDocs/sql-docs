---
title: "Work with Columns in Aggregate Queries (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "HAVING clause, query summary results"
  - "GROUP BY clause, query summary results"
  - "aggregate queries [SQL Server]"
  - "WHERE clause, query summary results"
ms.assetid: 1b82681f-3d4f-4b9a-bb1d-2060e44f2577
author: stevestein
ms.author: sstein
manager: craigg
---
# Work with Columns in Aggregate Queries (Visual Database Tools)
  When you create aggregate queries the [Query and View Designer](visual-database-tools.md) makes certain assumptions so that it can construct a valid query. For example, if you are creating an aggregate query and mark a data column for output, the Query and View Designer automatically makes the column part of the GROUP BY clause so that you do not inadvertently attempt to display the contents of an individual row in a summary.  
  
## Using Group By  
 The Query and View Designer uses the following guidelines for working with columns:  
  
-   When you choose the Group By option or add an aggregate function to a query, all columns marked for output or used for sorting are automatically added to the GROUP BY clause. Columns are not automatically added to the GROUP BY clause if they are already part of an aggregate function.  
  
     If you do not want a particular column to be part of the GROUP BY clause, you must manually change it by selecting a different option in the Group By column of the Criteria pane. However, the Query and View Designer will not prevent you from choosing an option that can result in a query that will not run.  
  
-   If you manually add a query output column to an aggregate function in either the Criteria or SQL pane, the Query and View Designer does not automatically remove other output columns from the query. Therefore, you must remove the remaining columns from the query output or make them part of the GROUP BY clause or of an aggregate function.  
  
 When you enter a search condition into the Filter column of the Criteria pane, the Query and View Designer follows these rules:  
  
-   If the **Group By** column of the grid is not displayed (because you have not yet specified an aggregate query), the search condition is placed into the WHERE clause.  
  
-   If you are already in an aggregate query and have selected the option **Where** in the **Group By** column, the search condition is placed into the WHERE clause.  
  
-   If the **Group By** column contains any value other than **Where**, the search condition is placed in the HAVING clause.  
  
## Using the HAVING and WHERE Clauses  
 The following principles describe how you can reference columns in an aggregate query in search conditions. In general, you can use a column in a search condition to filter the rows that should be summarized (a WHERE clause) or to determine which grouped results appear in the final output (a HAVING clause).  
  
-   Individual data columns can appear in either the WHERE or HAVING clause, depending on how they are used elsewhere in the query.  
  
-   WHERE clauses are used to select a subset of rows for summarizing and grouping and are thus applied before any grouping is done. Therefore, you can use a data column in a WHERE clause even if it is not part of the GROUP BY clause or contained in an aggregate function. For example, the following statement selects all titles that cost more than $10.00 and averages the price:  
  
    ```  
    SELECT AVG(price)  
    FROM titles  
    WHERE price > 10  
    ```  
  
-   If you create a search condition that involves a column also used in a GROUP BY clause or aggregate function, the search condition can appear as either a WHERE clause or a HAVING clause - you can decide which when you create the condition. For example, the following statement creates an average price for the titles for each publisher, then displays the average for the publishers in which the average price is greater than $10.00:  
  
    ```  
    SELECT pub_id, AVG(price)  
    FROM titles  
    GROUP BY pub_id  
    HAVING (AVG(price) > 10)  
    ```  
  
-   If you use an aggregate function in a search condition, the condition involves a summary and must therefore be part of the HAVING clause.  
  
## See Also  
 [Summarize Query Results &#40;Visual Database Tools&#41;](summarize-query-results-visual-database-tools.md)   
 [Sort and Group Query Results &#40;Visual Database Tools&#41;](sort-and-group-query-results-visual-database-tools.md)  
  
  
