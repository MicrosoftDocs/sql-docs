---
description: "Sort Rows (Visual Database Tools)"
title: Sort Rows
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "sorting rows [SQL Server]"
  - "sorting query results [SQL Server]"
ms.assetid: 780ef467-f96e-4373-8235-6dacbedb05a2
author: markingmyname
ms.author: maghan
ms.reviewer: 

---
# Sort Rows (Visual Database Tools)
[!INCLUDE[SQL Server](../../includes/applies-to-version/sqlserver.md)]
You can order the rows in a query result. That is, you can name a particular column or set of columns whose values determine the order of rows in the result set.  
  
> [!NOTE]  
> The sort order is determined in part by the column's collation sequence. You can change the collation sequence in the [Collation Dialog Box](../../ssms/visual-db-tools/collation-dialog-box-visual-database-tools.md).  
  
There are several ways in which you can sort query results:  
  
-   **You can arrange rows in ascending or descending order** By default, SQL uses order-by columns to arrange rows in ascending order. For example, to arrange the book titles by ascending price, simply sort the rows by the price column. The resulting SQL might look like this:  
  
    ```  
    SELECT *  
    FROM titles  
    ORDER BY price  
    ```  
  
    On the other hand, if you want to arrange the titles with the more expensive books first, you can explicitly specify a highest-first ordering. That is, you indicate that the result rows should be arranged by descending values of the price column. The resulting SQL might look like this:  
  
    ```  
    SELECT *  
    FROM titles  
    ORDER BY price DESC  
    ```  
  
-   **You can sort by multiple columns** For example, you can create a result set with one row for each author, ordering first by state and then by city. The resulting SQL might look like this:  
  
    ```  
    SELECT *  
    FROM authors   
    ORDER BY state, city  
    ```  
  
-   **You can sort by columns not appearing in the result set** For example, you can create a result set with the most expensive titles first, even though the prices do not appear. The resulting SQL might look like this:  
  
    ```  
    SELECT title_id, title  
    FROM titles  
    ORDER BY price DESC  
    ```  
  
-   **You can sort by derived columns** For example, you can create a result set in which each row contains a book title - with the books that pay the highest royalty per copy appearing first. The resulting SQL might look like this:  
  
    ```  
    SELECT title, price * royalty / 100 as royalty_per_unit  
    FROM titles  
    ORDER BY royalty_per_unit DESC  
    ```  
  
    (The formula for calculating the royalty that each book earns per copy is emphasized.)  
  
    To calculate a derived column, you can use SQL syntax, as in the preceding example, or you can use a user-defined function that returns a scalar value. For more information about user-defined functions, see the SQL Server documentation.  
  
-   **You can sort grouped rows** For example; you can create a result set in which each row describes a city, plus the number of authors in that city - with the cities containing many authors appearing first. The resulting SQL might look like this:  
  
    ```  
    SELECT city, state, COUNT(*)  
    FROM authors  
    GROUP BY city, state  
    ORDER BY COUNT(*) DESC, state  
    ```  
  
    Notice that the query uses `state` as a secondary sort column. Thus, if two states have the same number of authors, those states will appear in alphabetical order.  
  
-   **You can sort using international data** That is; you can sort a column using collating conventions that differ from the default conventions for that column. For example, you can write a query that retrieves all the book titles by Jaime Patiño. To display the titles in alphabetical order, you use a Spanish collating sequence for the title column. The resulting SQL might look like this:  
  
    ```  
    SELECT title  
    FROM   
        authors   
        INNER JOIN   
            titleauthor   
            ON authors.au_id   
            =  titleauthor.au_id   
            INNER JOIN  
                titles   
                ON titleauthor.title_id   
                =  titles.title_id   
    WHERE   
         au_fname = 'Jaime' AND   
         au_lname = 'Patiño'  
    ORDER BY   
         title COLLATE SQL_Spanish_Pref_CP1_CI_AS  
    ```  
  
## See Also  
[Sort and Group Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/sort-and-group-query-results-visual-database-tools.md)  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
  
