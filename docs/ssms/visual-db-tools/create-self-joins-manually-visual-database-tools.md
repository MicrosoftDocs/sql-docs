---
title: "Create Self-Joins Manually (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "self-joins"
  - "manual joins [SQL Server]"
  - "joins [SQL Server], self"
ms.assetid: 910ed516-cb84-481b-95d0-cba3e89afdba
author: "markingmyname"
ms.author: "maghan"
manager: craigg

---
# Create Self-Joins Manually (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
You can join a table to itself even if the table does not have a reflexive relationship in the database. For example, you can use a self-join to find pairs of authors living in the same city.  
  
As with any join, a self-join requires at least two tables. The difference is that, instead of adding a second table to the query, you add a second instance of the same table. That way, you can compare a column in the first instance of the table to the same column in the second instance, which allows you to compare the values in a column to each other. The [Query and View Designer](../../ssms/visual-db-tools/query-and-view-designer-tools-visual-database-tools.md) assigns an alias to the second instance of the table.  
  
For example, if you are creating a self-join to find all pairs of authors within Berkeley, you compare the `city` column in the first instance of the table against the `city` column in the second instance. The resulting query might look like the following:  
  
```  
SELECT   
         authors.au_fname,   
         authors.au_lname,   
         authors1.au_fname AS Expr2,   
         authors1.au_lname AS Expr3  
      FROM   
         authors   
            INNER JOIN  
            authors authors1   
               ON authors.city   
                = authors1.city  
      WHERE  
         authors.city = 'Berkeley'  
```  
  
Creating a self-join often requires multiple join conditions. To understand why, consider the result of the preceding query:  
  
```  
Cheryl Carson       Cheryl Carson  
   Abraham Bennet      Abraham Bennet  
   Cheryl Carson       Abraham Bennet  
   Abraham Bennet      Cheryl Carson  
```  
  
The first row is useless; it indicates that Cheryl Carson lives in the same city as Cheryl Carson. The second row is equally useless. To eliminate this useless data, you add another condition retaining only those result rows in which the two author names describe different authors. The resulting query might look like this:  
  
```  
SELECT   
         authors.au_fname,   
         authors.au_lname,   
         authors1.au_fname AS Expr2,   
         authors1.au_lname AS Expr3  
      FROM   
         authors   
            INNER JOIN  
            authors authors1   
               ON authors.city   
                = authors1.city  
               AND authors.au_id  
                <> authors1.au_id  
      WHERE  
         authors.city = 'Berkeley'  
```  
  
The result set is improved:  
  
```  
Cheryl Carson       Abraham Bennet  
   Abraham Bennet      Cheryl Carson  
```  
  
But the two result rows are redundant. The first says Carson lives in the same city as Bennet, and the second says the Bennet lives in the same city as Carson. To eliminate this redundancy, you can alter the second join condition from "not equals" to "less than." The resulting query might look like this:  
  
```  
SELECT   
         authors.au_fname,   
         authors.au_lname,   
         authors1.au_fname AS Expr2,   
         authors1.au_lname AS Expr3  
      FROM   
         authors   
            INNER JOIN  
            authors authors1   
               ON authors.city   
                = authors1.city  
               AND authors.au_id  
                < authors1.au_id  
      WHERE  
         authors.city = 'Berkeley'  
```  
  
And the result set looks like this:  
  
```  
Cheryl Carson       Abraham Bennet  
```  
  
### To create a self-join manually  
  
1.  Add to the [Diagram pane](../../ssms/visual-db-tools/diagram-pane-visual-database-tools.md) the table or table-valued object you want to work with.  
  
2.  Add the same table again, so that the Diagram pane shows the same table or table-valued object twice within the Diagram pane.  
  
    The Query and View Designer assigns an alias to the second instance by adding a sequential number to the table name. In addition, the Query and View Designer creates a join line between the two occurrences of the table or table-valued object within the Diagram pane.  
  
3.  Right-click the join line and choose **Properties** from the shortcut menu.  
  
4.  In the Properties window click **Join Condition and Type** and click the **ellipses (...)** to the right of the property.  
  
5.  In the [Join Dialog Box](../../ssms/visual-db-tools/join-dialog-box-visual-database-tools.md) change the comparison operator between the primary keys as required. For example, you might change the operator to less than (<).  
  
6.  Create the additional join condition (for example, authors.zip = authors1.zip) by dragging the name of the primary join column in the first occurrence of the table or table-valued object and dropping it on the corresponding column in the second occurrence.  
  
7.  Specify other options for the query such as output columns, search conditions, and sort order.  
  
## See Also  
[Create Self-Joins Automatically &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/create-self-joins-automatically-visual-database-tools.md)  
[Query with Joins &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/query-with-joins-visual-database-tools.md)  
  
