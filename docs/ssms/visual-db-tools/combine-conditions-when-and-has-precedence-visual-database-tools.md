---
description: "Combine Conditions When AND Has Precedence (Visual Database Tools)"
title: Combine Conditions When AND Has Precedence
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "search conditions [SQL Server], combining"
  - "precedence [SQL Server], Criteria pane"
  - "search criteria [SQL Server], combining conditions"
  - "combining search conditions"
  - "AND, Criteria pane"
ms.assetid: 450eb2eb-6ea3-405b-8dd2-1ff926c016e7
author: markingmyname
ms.author: maghan
ms.reviewer: 
ms.custom: seo-lt-2019
ms.date: 01/19/2017
---

# Combine Conditions When AND Has Precedence (Visual Database Tools)

[!INCLUDE[SQL Server](../../includes/applies-to-version/sqlserver.md)]

To combine conditions with AND, you add the column to the query twice--once for each condition. To combine conditions with OR, you put the first one in the Filter column and additional conditions into an **Or...** column.  
  
For example, imagine that you want to find either employees who have been with the company for more than five years in lower-level jobs or employees with middle-level jobs regardless of their hire date. This query requires three conditions, two of them linked with AND:  
  
-   Employees with a hire date earlier than five years ago AND with a job level of 100.  
  
    -or-  
  
-   Employees with a job level of 200.  
  
## To combine conditions when AND has precedence  
  
1.  In the [Criteria pane](../../ssms/visual-db-tools/criteria-pane-visual-database-tools.md), add the data columns you want to search. If you want to search the same column using two or more conditions linked with AND, you must add the data column name to the grid once for each value you want to search.  
  
2.  In the **Filter** column, enter all the conditions that you want to link with AND. For example, to link conditions with AND that search the `hire_date` and `job_lvl` columns, enter the values `< '1/1/91'` and `= 100`, respectively, in the Filter column.  
  
    These grid entries produce the following WHERE clause in the statement in the [SQL Pane](../../ssms/visual-db-tools/sql-pane-visual-database-tools.md):  
  
    ```  
    WHERE (hire_date < '01/01/91') AND  
      (job_lvl = 100)  
    ```  
  
3.  In the **Or...** grid column, enter conditions that you want to link with OR. For example, to add a condition that searches for another value in the `job_lvl` column, enter an additional value in the **Or...** column, such as `= 200`.  
  
    Adding a value in the **Or...** column adds another condition to the WHERE clause in the statement in the SQL pane:  
  
    ```  
    WHERE (hire_date < '01/01/91' ) AND  
      (job_lvl = 100) OR   
      (job_lvl = 200)  
    ```  
  
## See Also

[Combine Conditions When OR Has Precedence](../../ssms/visual-db-tools/combine-conditions-when-or-has-precedence-visual-database-tools.md)  
[Conventions for Combining Search Conditions in the Criteria Pane](../../ssms/visual-db-tools/conventions-combine-search-conditions-in-criteria-pane-visual-db-tools.md)  
[Rules for Entering Search Values](../../ssms/visual-db-tools/rules-for-entering-search-values-visual-database-tools.md)  
[Specify Search Criteria](../../ssms/visual-db-tools/specify-search-criteria-visual-database-tools.md)
