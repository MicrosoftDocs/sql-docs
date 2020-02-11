---
title: "Create Make Table Queries (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "queries [SQL Server], types"
  - "table creation [SQL Server], Make Table query"
  - "inserting tables"
  - "Make Table query"
  - "adding tables"
ms.assetid: 4493cffa-7b2d-4c24-8ef0-d49329198972
author: stevestein
ms.author: sstein
manager: craigg
---
# Create Make Table Queries (Visual Database Tools)
  You can copy rows into a new table using a Make Table query, which is useful for creating subsets of data to work with or copying the contents of a table from one database to another. A Make Table query is similar to an Insert Results query but creates a new table to copy rows into.  
  
 When you create a Make Table query, you specify:  
  
-   The name of the new database table (the destination table).  
  
-   The table or tables to copy rows from (the source table). You can copy from a single table or from joined tables.  
  
-   The columns in the source table whose contents you want to copy.  
  
-   Sort order, if you want to copy the rows in a particular order.  
  
-   Search conditions to define the rows you want to copy.  
  
-   Group By options, if you want to copy only summary information.  
  
 For example, the following query creates a new table called `uk`_`customers` and copies information from the `customers` table to it:  
  
```  
SELECT *   
INTO uk_customers  
FROM customers  
WHERE country = 'UK'  
```  
  
 In order to use a Make Table query successfully:  
  
-   Your database must support the SELECT...INTO syntax.  
  
-   You must have permission to create a table in the target database.  
  
### To create a Make Table query  
  
1.  Add the source table or tables to the Diagram pane.  
  
2.  From the **Query Designer** menu, point to **Change Type**, and then click **Make Table**.  
  
3.  In the **Make Table** dialog box, type the name of the destination table. The Query and View Designer does not check whether the name is already in use or whether you have permission to create the table.  
  
     To create a destination table in another database, specify a fully qualified table name including the name of the target database, the owner (if required), and the name of the table.  
  
4.  Specify the columns to copy by adding them to the query. For details, see [Add Columns to Queries &#40;Visual Database Tools&#41;](visual-database-tools.md). Columns will be copied only if you add them to the query. To copy entire rows, choose **\* (All Columns)**.  
  
     The Query and View Designer adds the columns you choose to the **Column** column of the Criteria pane.  
  
5.  If you want to copy rows in a particular order, specify a sort order. For details, see **Sorting and Grouping Query Results**.  
  
6.  Specify the rows to copy by entering search conditions. For details, see [Specify Search Criteria &#40;Visual Database Tools&#41;](specify-search-criteria-visual-database-tools.md).  
  
     If you do not specify a search condition, all rows from the source table will be copied to the destination table.  
  
    > [!NOTE]  
    >  When you add a column to search to the Criteria pane, the Query and View Designer also adds it to the list of columns to copy. If you want to use a column for searching but not copy it, clear the check box next to the column name in the rectangle representing the table or table-structured object.  
  
7.  If you want to copy summary information, specify Group By options. For details, see [Summarize Query Results &#40;Visual Database Tools&#41;](summarize-query-results-visual-database-tools.md).  
  
 When you execute a Make Table query, no results are reported in the [Results Pane](results-pane-visual-database-tools.md). Instead, a message appears indicating how many rows were copied.  
  
## See Also  
 [Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](design-queries-and-views-how-to-topics-visual-database-tools.md)   
 [Types of Queries &#40;Visual Database Tools&#41;](types-of-queries-visual-database-tools.md)  
  
  
