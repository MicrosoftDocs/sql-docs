---
title: "Specify Multiple Search Conditions for One Column (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "search criteria [SQL Server], multiple conditions"
  - "multiple search conditions"
  - "search conditions [SQL Server], multiple"
  - "OR operator"
  - "AND, Criteria pane"
ms.assetid: 2c006e36-56b1-4992-89b4-c6c0b19808f3
author: stevestein
ms.author: sstein
manager: craigg
---
# Specify Multiple Search Conditions for One Column (Visual Database Tools)
  In some instances, you might want to apply a number of search conditions to the same data column. For example, you might want to:  
  
-   Search for several different names in an `employee` table or for employees who are in different salary ranges. This type of search requires an OR condition.  
  
-   Search for a book title that both starts with the word "The" and contains the word "Cook." This type of search requires an AND condition.  
  
> [!NOTE]  
>  The information in this topic applies to search conditions in both the WHERE and HAVING clauses of a query. The examples focus on creating WHERE clauses, but the principles apply to both types of search conditions.  
  
 To search for alternative values in the same data column, you specify an OR condition. To search for values that meet several conditions, you specify an AND condition.  
  
## Specifying an OR Condition  
 Using an OR condition enables you to specify several alternative values to search for in a column. This option expands the scope of the search and can return more rows than searching for a single value.  
  
> [!TIP]  
>  You can often use the IN operator instead to search for multiple values in the same data column.  
  
#### To specify an OR condition  
  
1.  In the [Criteria Pane](visual-database-tools.md), add the column to search.  
  
2.  In the **Filter** column for the data column you just added, specify the first condition.  
  
3.  In the **Or...** column for the same data column, specify the second condition.  
  
 The Query and View Designer creates a WHERE clause that contains an OR condition such as the following:  
  
```  
SELECT fname, lname  
FROM employees  
WHERE (salary < 30000) OR (salary > 100000)  
```  
  
## Specifying an AND Condition  
 Using an AND condition enables you to specify that values in a column must meet two (or more) conditions for the row to be included in the result set. This option narrows the scope of the search and usually returns fewer rows than searching for a single value.  
  
> [!TIP]  
>  If you are searching for a range of values, you can use the BETWEEN operator instead of linking two conditions with AND.  
  
#### To specify an AND condition  
  
1.  In the Criteria pane, add the column to search.  
  
2.  In the **Filter** column for the data column you just added, specify the first condition.  
  
3.  Add the same data column to the Criteria pane again, placing it in an empty row of the grid.  
  
4.  In the **Filter** column for the second instance of the data column, specify the second condition.  
  
 The Query Designer creates a WHERE clause that contains an AND condition such as the following:  
  
```  
SELECT title_id, title  
FROM titles  
WHERE (title LIKE '%Cook%') AND   
  (title LIKE '%Recipe%')  
```  
  
## See Also  
 [Conventions for Combining Search Conditions in the Criteria Pane &#40;Visual Database Tools&#41;](conventions-combine-search-conditions-in-criteria-pane-visual-db-tools.md)   
 [Specify Search Criteria &#40;Visual Database Tools&#41;](specify-search-criteria-visual-database-tools.md)  
  
  
