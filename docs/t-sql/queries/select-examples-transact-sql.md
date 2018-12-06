---
title: "SELECT Examples (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "parentheses [SQL Server]"
  - "GROUP BY clause, SELECT statement"
  - "query hints [SQL Server]"
  - "ALL keyword"
  - "ROLLUP operator"
  - "SELECT statement [SQL Server], examples"
  - "correlated subqueries, SELECT statement"
  - "SELECT INTO statement"
  - "ORDER BY clause [Transact-SQL]"
  - "GROUPING function"
  - "index hints [SQL Server]"
  - "HAVING clause, SELECT statement"
  - "DISTINCT keyword"
  - "CUBE operator"
  - "UNION operator [SQL Server]"
  - "computed sums"
  - "WHERE clause, SELECT statement"
ms.assetid: 9b9caa3d-e7d0-42e1-b60b-a5572142186c
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# SELECT Examples (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  This topic provides examples of using the [SELECT](../../t-sql/queries/select-transact-sql.md) statement.  
  
## A. Using SELECT to retrieve rows and columns  
 The following example shows three code examples. This first code example returns all rows (no WHERE clause is specified) and all columns (using the `*`) from the `Product` table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
 [!code-sql[Select#SelectExamples1](../../t-sql/queries/codesnippet/tsql/select-examples-transact_1.sql)]  
  
 This example returns all rows (no WHERE clause is specified), and only a subset of the columns (`Name`, `ProductNumber`, `ListPrice`) from the `Product` table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. Additionally, a column heading is added.  
  
 [!code-sql[Select#SelectExamples2](../../t-sql/queries/codesnippet/tsql/select-examples-transact_2.sql)]  
  
 This example returns only the rows for `Product` that have a product line of `R` and that have days to manufacture that is less than `4`.  
  
 [!code-sql[Select#SelectExamples3](../../t-sql/queries/codesnippet/tsql/select-examples-transact_3.sql)]  
  
## B. Using SELECT with column headings and calculations  
 The following examples return all rows from the `Product` table. The first example returns total sales and the discounts for each product. In the second example, the total revenue is calculated for each product.  
  
 [!code-sql[Select#SelectExamples4](../../t-sql/queries/codesnippet/tsql/select-examples-transact_4.sql)]  
  
 This is the query that calculates the revenue for each product in each sales order.  
  
 [!code-sql[Select#SelectExamples5](../../t-sql/queries/codesnippet/tsql/select-examples-transact_5.sql)]  
  
## C. Using DISTINCT with SELECT  
 The following example uses `DISTINCT` to prevent the retrieval of duplicate titles.  
  
 [!code-sql[Select#SelectExamples6](../../t-sql/queries/codesnippet/tsql/select-examples-transact_6.sql)]  
  
## D. Creating tables with SELECT INTO  
 The following first example creates a temporary table named `#Bicycles` in `tempdb`.  
  
 [!code-sql[Select#SelectExamples7](../../t-sql/queries/codesnippet/tsql/select-examples-transact_7.sql)]  
  
 This second example creates the permanent table `NewProducts`.  
  
 [!code-sql[Select#SelectExamples8](../../t-sql/queries/codesnippet/tsql/select-examples-transact_8.sql)]  
  
## E. Using correlated subqueries  
 The following example shows queries that are semantically equivalent and illustrates the difference between using the `EXISTS` keyword and the `IN` keyword. Both are examples of a valid subquery that retrieves one instance of each product name for which the product model is a long sleeve logo jersey, and the `ProductModelID` numbers match between the `Product` and `ProductModel` tables.  
  
 [!code-sql[Select#SelectExamples9](../../t-sql/queries/codesnippet/tsql/select-examples-transact_9.sql)]  
  
 The following example uses `IN` in a correlated, or repeating, subquery. This is a query that depends on the outer query for its values. The query is executed repeatedly, one time for each row that may be selected by the outer query. This query retrieves one instance of the first and last name of each employee for which the bonus in the `SalesPerson` table is `5000.00` and for which the employee identification numbers match in the `Employee` and `SalesPerson` tables.  
  
 [!code-sql[Select#SelectExamples10](../../t-sql/queries/codesnippet/tsql/select-examples-transact_10.sql)]  
  
 The previous subquery in this statement cannot be evaluated independently of the outer query. It requires a value for `Employee.EmployeeID`, but this value changes as the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] examines different rows in `Employee`.  
  
 A correlated subquery can also be used in the `HAVING` clause of an outer query. This example finds the product models for which the maximum list price is more than twice the average for the model.  
  
 [!code-sql[Select#SelectExamples11](../../t-sql/queries/codesnippet/tsql/select-examples-transact_11.sql)]  
  
 This example uses two correlated subqueries to find the names of employees who have sold a particular product.  
  
 [!code-sql[Select#SelectExamples12](../../t-sql/queries/codesnippet/tsql/select-examples-transact_12.sql)]  
  
## F. Using GROUP BY  
 The following example finds the total of each sales order in the database.  
  
 [!code-sql[Select#SelectExamples13](../../t-sql/queries/codesnippet/tsql/select-examples-transact_13.sql)]  
  
 Because of the `GROUP BY` clause, only one row containing the sum of all sales is returned for each sales order.  
  
## G. Using GROUP BY with multiple groups  
 The following example finds the average price and the sum of year-to-date sales, grouped by product ID and special offer ID.  
  
 [!code-sql[Select#SelectExamples14](../../t-sql/queries/codesnippet/tsql/select-examples-transact_14.sql)]  
  
## H. Using GROUP BY and WHERE  
 The following example puts the results into groups after retrieving only the rows with list prices greater than `$1000`.  
  
 [!code-sql[Select#SelectExamples15](../../t-sql/queries/codesnippet/tsql/select-examples-transact_15.sql)]  
  
## I. Using GROUP BY with an expression  
 The following example groups by an expression. You can group by an expression if the expression does not include aggregate functions.  
  
 [!code-sql[Select#SelectExamples16](../../t-sql/queries/codesnippet/tsql/select-examples-transact_16.sql)]  
  
## J. Using GROUP BY with ORDER BY  
 The following example finds the average price of each type of product and orders the results by average price.  
  
 [!code-sql[Select#SelectExamples18](../../t-sql/queries/codesnippet/tsql/select-examples-transact_17.sql)]  
  
## K. Using the HAVING clause  
 The first example that follows shows a `HAVING` clause with an aggregate function. It groups the rows in the `SalesOrderDetail` table by product ID and eliminates products whose average order quantities are five or less. The second example shows a `HAVING` clause without aggregate functions.  
  
 [!code-sql[Select#SelectExamples19](../../t-sql/queries/codesnippet/tsql/select-examples-transact_18.sql)]  
  
 This query uses the `LIKE` clause in the `HAVING` clause.  
  
```sql
USE AdventureWorks2012 ;  
GO  
SELECT SalesOrderID, CarrierTrackingNumber   
FROM Sales.SalesOrderDetail  
GROUP BY SalesOrderID, CarrierTrackingNumber  
HAVING CarrierTrackingNumber LIKE '4BD%'  
ORDER BY SalesOrderID ;  
GO  
```  
  
## L. Using HAVING and GROUP BY  
 The following example shows using `GROUP BY`, `HAVING`, `WHERE`, and `ORDER BY` clauses in one `SELECT` statement. It produces groups and summary values but does so after eliminating the products with prices over $25 and average order quantities under 5. It also organizes the results by `ProductID`.  
  
 [!code-sql[Select#SelectExamples21](../../t-sql/queries/codesnippet/tsql/select-examples-transact_19.sql)]  
  
## M. Using HAVING with SUM and AVG  
 The following example groups the `SalesOrderDetail` table by product ID and includes only those groups of products that have orders totaling more than `$1000000.00` and whose average order quantities are less than `3`.  
  
 [!code-sql[Select#SelectExamples22](../../t-sql/queries/codesnippet/tsql/select-examples-transact_20.sql)]  
  
 To see the products that have had total sales greater than `$2000000.00`, use this query:  
  
 [!code-sql[Select#SelectExamples23](../../t-sql/queries/codesnippet/tsql/select-examples-transact_21.sql)]  
  
 If you want to make sure there are at least one thousand five hundred items involved in the calculations for each product, use `HAVING COUNT(*) > 1500` to eliminate the products that return totals for fewer than `1500` items sold. The query looks like this:  
  
 [!code-sql[Select#SelectExamples24](../../t-sql/queries/codesnippet/tsql/select-examples-transact_22.sql)]  
  
## N. Using the INDEX optimizer hint  
 The following example shows two ways to use the `INDEX` optimizer hint. The first example shows how to force the optimizer to use a nonclustered index to retrieve rows from a table, and the second example forces a table scan by using an index of 0.  
  
 [!code-sql[Select#SelectExamples45](../../t-sql/queries/codesnippet/tsql/select-examples-transact_23.sql)]  
  
## M. Using OPTION and the GROUP hints  
 The following example shows how the `OPTION (GROUP)` clause is used with a `GROUP BY` clause.  
  
 [!code-sql[Select#SelectExamples46](../../t-sql/queries/codesnippet/tsql/select-examples-transact_24.sql)]  
  
## O. Using the UNION query hint  
 The following example uses the `MERGE UNION` query hint.  
  
 [!code-sql[Select#SelectExamples47](../../t-sql/queries/codesnippet/tsql/select-examples-transact_25.sql)]  
  
## P. Using a simple UNION  
 In the following example, the result set includes the contents of the `ProductModelID` and `Name` columns of both the `ProductModel` and `Gloves` tables.  
  
 [!code-sql[Select#SelectExamples48](../../t-sql/queries/codesnippet/tsql/select-examples-transact_26.sql)]  
  
## Q. Using SELECT INTO with UNION  
 In the following example, the `INTO` clause in the second `SELECT` statement specifies that the table named `ProductResults` holds the final result set of the union of the designated columns of the `ProductModel` and `Gloves` tables. Note that the `Gloves` table is created in the first `SELECT` statement.  
  
 [!code-sql[Select#SelectExamples49](../../t-sql/queries/codesnippet/tsql/select-examples-transact_27.sql)]  
  
## R. Using UNION of two SELECT statements with ORDER BY  
 The order of certain parameters used with the UNION clause is important. The following example shows the incorrect and correct use of `UNION` in two `SELECT` statements in which a column is to be renamed in the output.  
  
 [!code-sql[Select#SelectExamples50](../../t-sql/queries/codesnippet/tsql/select-examples-transact_28.sql)]  
  
## S. Using UNION of three SELECT statements to show the effects of ALL and parentheses  
 The following examples use `UNION` to combine the results of three tables that all have the same 5 rows of data. The first example uses `UNION ALL` to show the duplicated records, and returns all 15 rows. The second example uses `UNION` without `ALL` to eliminate the duplicate rows from the combined results of the three `SELECT` statements, and returns 5 rows.  
  
 The third example uses `ALL` with the first `UNION` and parentheses enclose the second `UNION` that is not using `ALL`. The second `UNION` is processed first because it is in parentheses, and returns 5 rows because the `ALL` option is not used and the duplicates are removed. These 5 rows are combined with the results of the first `SELECT` by using the `UNION ALL` keywords. This does not remove the duplicates between the two sets of 5 rows. The final result has 10 rows.  
  
 [!code-sql[Select#SelectExamples51](../../t-sql/queries/codesnippet/tsql/select-examples-transact_29.sql)]  
  
## See Also  
 [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md)   
 [CREATE VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/create-view-transact-sql.md)   
 [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)   
 [EXECUTE &#40;Transact-SQL&#41;](../../t-sql/language-elements/execute-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)   
 [LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md)   
 [UNION &#40;Transact-SQL&#41;](../../t-sql/language-elements/set-operators-union-transact-sql.md)   
 [EXCEPT and INTERSECT &#40;Transact-SQL&#41;](../../t-sql/language-elements/set-operators-except-and-intersect-transact-sql.md)   
 [UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)   
 [PathName &#40;Transact-SQL&#41;](../../relational-databases/system-functions/pathname-transact-sql.md)   
 [INTO Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-into-clause-transact-sql.md)  
  
  
