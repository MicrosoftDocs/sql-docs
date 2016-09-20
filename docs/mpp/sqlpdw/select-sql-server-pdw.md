---
title: "SELECT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 2ddf705e-0152-4430-a523-3f3caacf521a
caps.latest.revision: 48
author: BarbKess
---
# SELECT (SQL Server PDW)
SQL query statement that retrieves data from tables or views in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
[ WITH <common_table_expression> [ ,...n ] ]  
SELECT <select_criteria>  
[;]  
  
<select_criteria> ::=  
    [ TOP ( top_expression ) ]   
    [ ALL | DISTINCT ]   
    { * | column_name | expression } [ ,...n ]   
    [ FROM { table_source } [ ,...n ] ]  
    [ WHERE <search_condition> ]   
    [ GROUP BY <group_by_clause> ]   
    [ HAVING <search_condition> ]   
    [ ORDER BY <order_by_expression> ]  
    [ OPTION ( <query_option> [ ,...n ] ) ]  
```  
  
## Arguments  
*common_table_expression*  
Specifies a temporary named result set, known as a common table expression (CTE). For more information, see [WITH common_table_expression &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/with-common-table-expression-sql-server-pdw.md).  
  
TOP ( *top_expression* )  
Returns only a specified top set of rows from the query result set. *top_expression* is a number.  
  
For more information, see [TOP &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/top-sql-server-pdw.md).  
  
**ALL** | DISTINCT  
All specifies that duplicate rows can appear in the results. DISTINCT specifies that only unique rows will be returned in the results.  
  
{ \* | *column_name* | *expression* } [ ,... *n*]  
The list of one or more columns or expressions to be selected for the result set.  
  
\*  
Returns all columns from all tables and views in the FROM clause; the columns are returned in the order in which they exist in the table or view.  
  
*column_name*  
*column_name* is an expression. The maximum number of expressions that can be specified in the select list is 4096. For information on specifying a different name for a column, see [Aliasing &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/aliasing-sql-server-pdw.md).  
  
*expression*  
*expression* can be a variable or a function that is used without a FROM clause. For example, `SELECT @@VERSION`, `SELECT CURRENT_TIMESTAMP`.  
  
FROM { <table_source> } [ **,**...*n* ]  
Limits the scope of the result set to a set of tables or views. For more information, see [FROM &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/from-sql-server-pdw.md).  
  
WHERE *< search_condition >*  
Limits the scope of the returned values based on conditions. For more information, see [WHERE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/where-sql-server-pdw.md).  
  
GROUP BY *< group_by_clause >*  
Defines a grouping for the result set. For more information, see [GROUP BY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/group-by-sql-server-pdw.md).  
  
HAVING *< search_condition >*  
Specifies requirements on the group or aggregate. For more information, see [HAVING &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/having-sql-server-pdw.md).  
  
ORDER BY *< order_by_expression >*  
Orders the result set. For more information, see [ORDER BY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/order-by-sql-server-pdw.md).  
  
OPTION ( <query_option> [ ,...*n* ] )  
Specifies a query option, which is either a query label or a query join hint. A query label is useful for locating a query within the Admin Console. A query hint might be useful to improve query performance when SQL Server, running on the Compute nodes, does not generate an optimal query plan. For more information, see [OPTION &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/option-sql-server-pdw.md).  
  
## Permissions  
Requires SELECT permission on the table or view.  
  
## General Remarks  
Queries on columnstore tables run in memory as much as possible to achieve maximum performance. SQL Server PDW has pre-set resource governor settings that moderate the memory available for in-memory queries. If a query does not have enough in-memory resources, SQL Server PDW uses disk resources to complete the query without failing.  
  
Use the UNION operator between queries to combine their results into one result set. For more information, see [UNION &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/union-sql-server-pdw.md).  
  
The order of the clauses in the SELECT statement is significant. Any one of the optional clauses can be omitted, but optional clauses must appear in the appropriate order when they are used.  
  
SQL Server PDW raises an exception and rolls back the current running statement if a SELECT statement produces a result row or an intermediate work table row that exceeds 8,060 bytes.  
  
### Processing Order of WHERE, GROUP BY, HAVING Clauses  
The following steps show the processing order for a SELECT statement with a WHERE clause, a GROUP BY clause, and a HAVING clause:  
  
1.  The FROM clause returns an initial result set.  
  
2.  The WHERE clause excludes rows not meeting its search condition.  
  
3.  The GROUP BY clause collects the selected rows into one group for each unique value in the GROUP BY clause.  
  
4.  Aggregate functions specified in the SELECT list calculate summary values for each group.  
  
5.  The HAVING clause additionally excludes rows not meeting its search condition.  
  
## Limitations and Restrictions  
SELECT statements cannot contain distinct aggregates on constant expressions. For example, the following is not supported: `SELECT COUNT ( DISTINCT 3 ) FROM Table A`.  
  
## Locking  
Takes a shared lock at some level of detail on the table being queried.  
  
## Examples  
  
### A. Using SELECT to retrieve rows and columns  
This section shows three code examples. This first code example returns all rows (no WHERE clause is specified) and all columns (using the `*`) from the `DimEmployee` table in the **AdventureWorksPDW2012** database.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT *  
FROM DimEmployee  
ORDER BY LastName;  
```  
  
This next example using table aliasing to achieve the same result.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT e.*  
FROM DimEmployee AS e  
ORDER BY LastName;  
```  
  
This example returns all rows (no WHERE clause is specified) and a subset of the columns (`FirstName`, `LastName`, `StartDate`) from the `DimEmployee` table in the **AdventureWorksPDW2012** database. The third column heading is renamed to `FirstDay`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, StartDate AS FirstDay  
FROM DimEmployee   
ORDER BY LastName;  
```  
  
This example returns only the rows for `DimEmployee` that have an `EndDate` that is not NULL and a `MaritalStatus` of ‘M’ (married).  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, StartDate AS FirstDay  
FROM DimEmployee   
WHERE EndDate IS NOT NULL   
AND MaritalStatus = 'M'  
ORDER BY LastName;  
```  
  
### B. Using SELECT with column headings and calculations  
The following example returns all rows from the `DimEmployee` table, and calculates the gross pay for each employee based on their `BaseRate` and a 40 hour work week.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, BaseRate, BaseRate * 40 AS GrossPay  
FROM DimEmployee  
ORDER BY LastName;  
```  
  
### C. Using DISTINCT with SELECT  
The following example uses `DISTINCT` to generate a list of all unique titles in the `DimEmployee` table.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT DISTINCT Title  
FROM DimEmployee  
ORDER BY Title;  
```  
  
### D. Using GROUP BY  
The following example finds the total amount for all sales on each day.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT OrderDateKey, SUM(SalesAmount) AS TotalSales  
FROM FactInternetSales  
GROUP BY OrderDateKey  
ORDER BY OrderDateKey;  
```  
  
Because of the `GROUP BY` clause, only one row containing the sum of all sales is returned for each day.  
  
### E. Using GROUP BY with multiple groups  
The following example finds the average price and the sum of Internet sales for each day, grouped by order date and the promotion key.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT OrderDateKey, PromotionKey, AVG(SalesAmount) AS AvgSales, SUM(SalesAmount) AS TotalSales  
FROM FactInternetSales  
GROUP BY OrderDateKey, PromotionKey  
ORDER BY OrderDateKey;  
```  
  
### F. Using GROUP BY and WHERE  
The following example puts the results into groups after retrieving only the rows with order dates later than August 1, 2002.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT OrderDateKey, SUM(SalesAmount) AS TotalSales  
FROM FactInternetSales  
WHERE OrderDateKey > '20020801'  
GROUP BY OrderDateKey  
ORDER BY OrderDateKey;  
```  
  
### G. Using GROUP BY with an expression  
The following example groups by an expression. You can group by an expression if the expression does not include aggregate functions.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT SUM(SalesAmount) AS TotalSales  
FROM FactInternetSales  
GROUP BY (OrderDateKey * 10);  
```  
  
### H. Using GROUP BY with ORDER BY  
The following example finds the sum of sales per day, and orders by the day.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT OrderDateKey, SUM(SalesAmount) AS TotalSales  
FROM FactInternetSales  
GROUP BY OrderDateKey  
ORDER BY OrderDateKey;  
```  
  
### I. Using the HAVING clause  
This query uses the `HAVING` clause to restrict results.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT OrderDateKey, SUM(SalesAmount) AS TotalSales  
FROM FactInternetSales  
GROUP BY OrderDateKey  
HAVING OrderDateKey > 20010000  
ORDER BY OrderDateKey;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
