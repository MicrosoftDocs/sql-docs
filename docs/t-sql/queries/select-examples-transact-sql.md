---
title: "SELECT examples (Transact-SQL)"
description: "Examples of the SELECT Transact-SQL statement in the Database Engine."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 11/01/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
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
dev_langs:
  - "TSQL"
---
# SELECT examples (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article provides examples of using the [SELECT](../../t-sql/queries/select-transact-sql.md) statement.

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

## A. Use SELECT to retrieve rows and columns

The following example shows three code examples. This first code example returns all rows (no WHERE clause is specified) and all columns (using the `*`) from the `Product` table in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_1.sql":::

This example returns all rows (no WHERE clause is specified), and only a subset of the columns (`Name`, `ProductNumber`, `ListPrice`) from the `Product` table in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. Additionally, a column heading is added.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_2.sql":::

This example returns only the rows for `Product` that have a product line of `R` and that have days to manufacture that is less than `4`.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_3.sql":::

## B. Use SELECT with column headings and calculations

The following examples return all rows from the `Product` table. The first example returns total sales and the discounts for each product. In the second example, the total revenue is calculated for each product.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_4.sql":::

This is the query that calculates the revenue for each product in each sales order.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_5.sql":::

## C. Use DISTINCT with SELECT

The following example uses `DISTINCT` to prevent the retrieval of duplicate titles.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_6.sql":::

## D. Create tables with SELECT INTO

The following first example creates a temporary table named `#Bicycles` in `tempdb`.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_7.sql":::

This second example creates the permanent table `NewProducts`.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_8.sql":::

## E. Use correlated subqueries

A correlated subquery is a query that depends on the outer query for its values. This query can be executed repeatedly, one time for each row that could be selected by the outer query.

The first example shows queries that are semantically equivalent to illustrate the difference between using the `EXISTS` keyword and the `IN` keyword. Both are examples of a valid subquery that retrieves one instance of each product name for which the product model is a long sleeve logo jersey, and the `ProductModelID` numbers match between the `Product` and `ProductModel` tables.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_9.sql":::

The next example uses `IN` and retrieves one instance of the first name and family name of each employee for which the bonus in the `SalesPerson` table is `5000.00`, and for which the employee identification numbers match in the `Employee` and `SalesPerson` tables.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_10.sql":::

The previous subquery in this statement can't be evaluated independently of the outer query. It requires a value for `Employee.EmployeeID`, but this value changes as the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] examines different rows in `Employee`.

A correlated subquery can also be used in the `HAVING` clause of an outer query. This example finds the product models for which the maximum list price is more than twice the average for the model.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_11.sql":::

This example uses two correlated subqueries to find the names of employees who sold a particular product.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_12.sql":::

## F. Use GROUP BY

The following example finds the total of each sales order in the database.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_13.sql":::

Because of the `GROUP BY` clause, only one row containing the sum of all sales is returned for each sales order.

## G. Use GROUP BY with multiple groups

The following example finds the average price and the sum of year-to-date sales, grouped by product ID and special offer ID.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_14.sql":::

## H. Use GROUP BY and WHERE

The following example puts the results into groups after retrieving only the rows with list prices greater than `$1000`.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_15.sql":::

## I. Use GROUP BY with an expression

The following example groups by an expression. You can group by an expression if the expression doesn't include aggregate functions.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_16.sql":::

## J. Use GROUP BY with ORDER BY

The following example finds the average price of each type of product and orders the results by average price.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_17.sql":::

## K. Use the HAVING clause

The first example that follows shows a `HAVING` clause with an aggregate function. It groups the rows in the `SalesOrderDetail` table by product ID and eliminates products whose average order quantities are five or less. The second example shows a `HAVING` clause without aggregate functions.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_18.sql":::

This query uses the `LIKE` clause in the `HAVING` clause.

```sql
USE AdventureWorks2022;
GO
SELECT SalesOrderID, CarrierTrackingNumber
FROM Sales.SalesOrderDetail
GROUP BY SalesOrderID, CarrierTrackingNumber
HAVING CarrierTrackingNumber LIKE '4BD%'
ORDER BY SalesOrderID ;
GO
```

## L. Use HAVING and GROUP BY

The following example shows using `GROUP BY`, `HAVING`, `WHERE`, and `ORDER BY` clauses in one `SELECT` statement. It produces groups and summary values but does so after eliminating the products with prices over $25 and average order quantities under 5. It also organizes the results by `ProductID`.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_19.sql":::

## M. Use HAVING with SUM and AVG

The following example groups the `SalesOrderDetail` table by product ID and includes only those groups of products that have orders totaling more than `$1000000.00` and whose average order quantities are less than `3`.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_20.sql":::

To see the products with total sales greater than `$2000000.00`, use this query:

:::code language="sql" source="codesnippet/tsql/select-examples-transact_21.sql":::

If you want to make sure there are at least 1,500 items involved in the calculations for each product, use `HAVING COUNT(*) > 1500` to eliminate the products that return totals for fewer than `1500` items sold. The query looks like this:

:::code language="sql" source="codesnippet/tsql/select-examples-transact_22.sql":::

## N. Use the INDEX optimizer hint

The following example shows two ways to use the `INDEX` optimizer hint. The first example shows how to force the optimizer to use a nonclustered index to retrieve rows from a table. The second example forces a table scan by using an index of 0.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_23.sql":::

## M. Use OPTION and the GROUP hints

The following example shows how the `OPTION (GROUP)` clause is used with a `GROUP BY` clause.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_24.sql":::

## O. Use the UNION query hint

The following example uses the `MERGE UNION` query hint.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_25.sql":::

## P. Use a UNION

In the following example, the result set includes the contents of the `ProductModelID` and `Name` columns of both the `ProductModel` and `Gloves` tables.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_26.sql":::

## Q. Use SELECT INTO with UNION

In the following example, the `INTO` clause in the second `SELECT` statement specifies that the table named `ProductResults` holds the final result set of the union of the designated columns of the `ProductModel` and `Gloves` tables. The `Gloves` table is created in the first `SELECT` statement.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_27.sql":::

## R. Use UNION of two SELECT statements with ORDER BY

The order of certain parameters used with the UNION clause is important. The following example shows the incorrect and correct use of `UNION` in two `SELECT` statements in which a column is to be renamed in the output.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_28.sql":::

## S. Use UNION of three SELECT statements to show the effects of ALL and parentheses

The following examples use `UNION` to combine the results of three tables that all have the same five rows of data. The first example uses `UNION ALL` to show the duplicated records, and returns all 15 rows. The second example uses `UNION` without `ALL` to eliminate the duplicate rows from the combined results of the three `SELECT` statements, and returns five rows.

The third example uses `ALL` with the first `UNION` and parentheses enclose the second `UNION` that isn't using `ALL`. The second `UNION` is processed first because it's in parentheses, and returns five rows because the `ALL` option isn't used and the duplicates are removed. These five rows are combined with the results of the first `SELECT` by using the `UNION ALL` keywords. This example doesn't remove the duplicates between the two sets of five rows. The final result has 10 rows.

:::code language="sql" source="codesnippet/tsql/select-examples-transact_29.sql":::

## Related content

- [CREATE TRIGGER (Transact-SQL)](../statements/create-trigger-transact-sql.md)
- [CREATE VIEW (Transact-SQL)](../statements/create-view-transact-sql.md)
- [DELETE (Transact-SQL)](../statements/delete-transact-sql.md)
- [EXECUTE (Transact-SQL)](../language-elements/execute-transact-sql.md)
- [Expressions (Transact-SQL)](../language-elements/expressions-transact-sql.md)
- [INSERT (Transact-SQL)](../statements/insert-transact-sql.md)
- [LIKE (Transact-SQL)](../language-elements/like-transact-sql.md)
- [Set Operators - UNION (Transact-SQL)](../language-elements/set-operators-union-transact-sql.md)
- [Set Operators - EXCEPT and INTERSECT (Transact-SQL)](../language-elements/set-operators-except-and-intersect-transact-sql.md)
- [UPDATE (Transact-SQL)](update-transact-sql.md)
- [WHERE (Transact-SQL)](where-transact-sql.md)
- [PathName (Transact-SQL)](../../relational-databases/system-functions/pathname-transact-sql.md)
- [SELECT - INTO Clause (Transact-SQL)](select-into-clause-transact-sql.md)
