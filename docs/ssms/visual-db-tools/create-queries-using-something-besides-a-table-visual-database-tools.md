---
title: "Create Queries using Something Besides a Table | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "user-defined functions [SQL Server], queries"
  - "queries [SQL Server], creating"
ms.assetid: 8e4a1f0a-8a42-4733-be8d-e21d6dbddb33
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Create Queries using Something Besides a Table (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
Whenever you write a retrieval query, you articulate what columns you want, what rows you want, and where the query processor should find the original data. Typically, this original data consists of a table or several tables joined together. But the original data can come from sources other than tables. In fact, it can come from views, queries, synonyms, or user-defined functions that return a table.  
  
## Using a View in Place of a Table  
You can select rows from a view. For example, suppose the database includes a view called "ExpensiveBooks," in which each row describes a title whose price exceeds 19.99. The view definition might look like this:  
  
```  
SELECT *  
FROM titles  
WHERE price > 19.99  
```  
  
You can select the expensive psychology books merely by selecting the psychology books from the ExpensiveBooks view. The resulting SQL might look like this:  
  
```  
SELECT *  
FROM ExpensiveBooks  
WHERE type = 'psychology'  
```  
  
Similarly, a view can participate in a JOIN operation. For example, you can find the sales of expensive books merely by joining the sales table to the ExpensiveBooks view. The resulting SQL might look like this:  
  
```  
SELECT *  
FROM sales   
         INNER JOIN   
         ExpensiveBooks   
         ON sales.title_id   
         =  ExpensiveBooks.title_id  
```  
  
For more information about adding a view to a query, see [Add Tables to Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/add-tables-to-queries-visual-database-tools.md).  
  
## Using a Query in Place of a Table  
You can select rows from a query. For example, suppose you have already written a query retrieving titles and identifiers of the coauthored books - the books with more than one author. The SQL might look like this:  
  
```  
SELECT   
     titles.title_id, title, type  
FROM   
     titleauthor   
         INNER JOIN  
         titles   
         ON titleauthor.title_id   
         =  titles.title_id   
GROUP BY   
     titles.title_id, title, type  
HAVING COUNT(*) > 1  
```  
  
You can then write another query that builds on this result. For example, you can write a query that retrieves the coauthored psychology books. To write this new query, you can use the existing query as the source of the new query's data. The resulting SQL might look like this:  
  
```  
SELECT   
    title  
FROM   
    (  
    SELECT   
        titles.title_id,   
        title,   
        type  
    FROM   
        titleauthor   
            INNER JOIN  
            titles   
            ON titleauthor.title_id   
            =  titles.title_id   
    GROUP BY   
        titles.title_id,   
        title,   
        type  
    HAVING COUNT(*) > 1  
    )   
    co_authored_books  
WHERE     type = 'psychology'  
```  
  
The emphasized text shows the existing query used as the source of the new query's data. Note that the new query uses an alias ("co_authored_books") for the existing query. For more information about aliases, see [Create Table Aliases &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/create-table-aliases-visual-database-tools.md) and [Create Column Aliases &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/create-column-aliases-visual-database-tools.md).  
  
Similarly, a query can participate in a JOIN operation. For example, you can find the sales of expensive coauthored books merely by joining the ExpensiveBooks view to the query retrieving the coauthored books. The resulting SQL might look like this:  
  
```  
SELECT   
    ExpensiveBooks.title  
FROM   
    ExpensiveBooks   
        INNER JOIN  
        (  
        SELECT   
            titles.title_id,   
            title,   
            type  
        FROM   
            titleauthor   
                INNER JOIN  
                titles   
                ON titleauthor.title_id   
                =  titles.title_id   
        GROUP BY   
            titles.title_id,   
            title,   
            type  
        HAVING COUNT(*) > 1  
        )  
```  
  
For more information about adding a query to a query, see [Add Tables to Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/add-tables-to-queries-visual-database-tools.md).  
  
## Using a User-Defined Function in Place of a Table  
In SQL Server 2000 or higher, you can create a user-defined function that returns a table. Such functions are useful for performing complex or procedural logic.  
  
For example, suppose the employee table contains an additional column, employee.manager_emp_id, and that a foreign key exists from manager_emp_id to employee.emp_id. Within each row of the employee table, the manager_emp_id column indicates the employee's boss. More precisely, it indicates the employee's boss's emp_id. You can create a user-defined function that returns a table containing one row for each employee working within a particular high-level manager's organizational hierarchy. You might call the function fn_GetWholeTeam, and design it to take an input variable - the emp_id of the manager whose team you want to retrieve.  
  
You can write a query that uses the fn_GetWholeTeam function as a source of data. The resulting SQL might look like this:  
  
```  
SELECT *   
FROM   
     fn_GetWholeTeam ('VPA30890F')  
```  
  
"VPA30890F" is the emp_id of the manager whose organization you want to retrieve. For more information about adding a user-defined function to a query, see [Add Tables to Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/add-tables-to-queries-visual-database-tools.md). For a complete description of user-defined functions, see [User-Defined Functions](https://msdn.microsoft.com/d7ddafab-f5a6-44b0-81d5-ba96425aada4).  
  
