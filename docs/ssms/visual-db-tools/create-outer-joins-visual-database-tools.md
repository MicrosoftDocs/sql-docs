---
description: "Create Outer Joins (Visual Database Tools)"
title: Create Outer Joins
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "outer joins"
  - "joins [SQL Server], outer"
ms.assetid: 18de47b1-f936-427d-b852-fe6d20334f71
author: markingmyname
ms.author: maghan
ms.reviewer: 

---
# Create Outer Joins (Visual Database Tools)
[!INCLUDE[SQL Server](../../includes/applies-to-version/sqlserver.md)]
By default, the [Query and View Designer](../../ssms/visual-db-tools/query-and-view-designer-tools-visual-database-tools.md) creates an inner join between tables. Inner joins eliminate the rows that do not match with a row from the other table. Outer joins, however, return all rows from at least one of the tables or views mentioned in the FROM clause, as long as those rows meet any WHERE or HAVING search conditions. If you want to include data rows in the result set that do not have a match in the joined table, you can create an outer join.  
  
When you create an outer join, the order in which tables appear in the SQL statement (as reflected in the SQL pane) is significant. The first table you add becomes the "left" table and the second table becomes the "right" table. (The actual order in which the tables appear in the [Diagram pane](../../ssms/visual-db-tools/diagram-pane-visual-database-tools.md) is not significant.) When you specify a left or right outer join, you are referring to the order in which the tables were added to the query and to the order in which they appear in the SQL statement in the [SQL pane](../../ssms/visual-db-tools/sql-pane-visual-database-tools.md).  
  
### To create an outer join  
  
1.  Create the join, either automatically or manually. For details, see [Join Tables Automatically &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/join-tables-automatically-visual-database-tools.md) or [Join Tables Manually &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/join-tables-manually-visual-database-tools.md).  
  
2.  Select the join line in the Diagram pane, and then from the **Query Designer** menu, choose **Select All Rows from \<tablename\>**, selecting the command that includes the table whose extra rows you want to include.  
  
    -   Choose the first table to create a left outer join.  
  
    -   Choose the second table to create a right outer join.  
  
    -   Choose both tables to create a full outer join.  
  
When you specify an outer join, the Query and View Designer modifies the join line to indicate an outer join.  
  
In addition, the Query and View Designer modifies the SQL statement in the SQL pane to reflect the change in join type, as shown in the following statement:  
  
```  
SELECT employee.job_id, employee.emp_id,  
   employee.fname, employee.minit, jobs.job_desc  
FROM employee LEFT OUTER JOIN jobs ON   
    employee.job_id = jobs.job_id  
```  
  
Because an outer join includes unmatched rows, you can use it to find rows that violate foreign key constraints. To do so, you create an outer join and then add a search condition to find rows in which the primary key column of the rightmost table is null. For example, the following outer join finds rows in the `employee` table that do not have corresponding rows in the `jobs` table:  
  
```  
SELECT employee.emp_id, employee.job_id  
FROM employee LEFT OUTER JOIN jobs   
   ON employee.job_id = jobs.job_id  
WHERE (jobs.job_id IS NULL)  
```  
  
## See Also  
[Query with Joins &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/query-with-joins-visual-database-tools.md)  
[Join Dialog Box &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/join-dialog-box-visual-database-tools.md)  
  
