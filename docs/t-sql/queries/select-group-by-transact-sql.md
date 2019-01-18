---
title: "GROUP BY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "GROUP"
  - "CUBE"
  - "ROLLUP"
  - "GROUPING_SETS_TSQL"
  - "GROUP BY"
  - "GROUPING SETS"
  - "GROUP_BY_TSQL"
  - "GROUP_TSQL"
  - "CUBE_TSQL"
  - "ROLLUP_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "GROUP BY clause, about GROUP BY clause"
  - "dividing tables into groups"
  - "GROUPING SETS"
  - "GROUP BY clause"
  - "table groups [SQL Server]"
  - "groups [SQL Server], tables divided into groups"
  - "summary values [SQL Server]"
ms.assetid: 40075914-6385-4692-b4a5-62fe44ae6cb6
author: shkale-msft
ms.author: shkale
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SELECT - GROUP BY- Transact-SQL
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

A SELECT statement clause that divides the query result into groups of rows, usually for the purpose of performing one or more aggregations on each group. The SELECT statement returns one row per group.  
  
## Syntax  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
```  
-- Syntax for SQL Server and Azure SQL Database   
-- ISO-Compliant Syntax  
  
GROUP BY {
      column-expression  
    | ROLLUP ( <group_by_expression> [ ,...n ] )  
    | CUBE ( <group_by_expression> [ ,...n ] )  
    | GROUPING SETS ( <grouping_set> [ ,...n ]  )  
    | () --calculates the grand total 
} [ ,...n ] 
 
<group_by_expression> ::=  
      column-expression  
    | ( column-expression [ ,...n ] )    
   
<grouping_set> ::=  
      () --calculates the grand total  
    | <grouping_set_item>  
    | ( <grouping_set_item> [ ,...n ] )  
  
<grouping_set_item> ::=  
      <group_by_expression>  
    | ROLLUP ( <group_by_expression> [ ,...n ] )  
    | CUBE ( <group_by_expression> [ ,...n ] )  
  

-- For backward compatibility only.
-- Non-ISO-Compliant Syntax for SQL Server and Azure SQL Database 
  
GROUP BY 
      [ ALL ] column-expression [ ,...n ] 
    | column-expression [ ,...n ] [ WITH { CUBE | ROLLUP } ]   

```  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
GROUP BY {
      column-name [ WITH (DISTRIBUTED_AGG) ]  
    | column-expression
} [ ,...n ]

```  
  
## Arguments 
 
### *column-expression*  
Specifies a column or a non-aggregate calculation on a column. This column can belong to a table, derived table, or view. The column must appear in the FROM clause of the SELECT statement, but is not required to appear in the SELECT list. 

For valid expressions, see [expression](~/t-sql/language-elements/expressions-transact-sql.md).    

The column must appear in the FROM clause of the SELECT statement, but is not required to appear in the SELECT list. However, each table or view column in any nonaggregate expression in the \<select> list must be included in the GROUP BY list:  
  
The following statements are allowed:  
  
```sql  
SELECT ColumnA, ColumnB FROM T GROUP BY ColumnA, ColumnB;  
SELECT ColumnA + ColumnB FROM T GROUP BY ColumnA, ColumnB;  
SELECT ColumnA + ColumnB FROM T GROUP BY ColumnA + ColumnB;  
SELECT ColumnA + ColumnB + constant FROM T GROUP BY ColumnA, ColumnB;  
```  
  
The following statements are not allowed:  
  
```sql  
SELECT ColumnA, ColumnB FROM T GROUP BY ColumnA + ColumnB;  
SELECT ColumnA + constant + ColumnB FROM T GROUP BY ColumnA + ColumnB;  
```  

The column expression cannot contain:

- A column alias that is defined in the SELECT list. It can use a column alias for a derived table that is defined in the FROM clause.
- A column of type **text**, **ntext**, or **image**. However, you can use a column of text, ntext, or image as an argument to a function that returns a value of a valid data type. For example, the expression can use SUBSTRING() and CAST(). This also applies to expressions in the HAVING clause.
- xml data type methods. It can include a user-defined function that uses xml data type methods. It can include a computed column that uses xml data type methods. 
- A subquery. Error 144 is returned. 
- A column from an indexed view. 
 
### GROUP BY *column-expression* [ ,...n ] 

Groups the SELECT statement results according to the values in a list of one or more column expressions. 

For example, this query creates a Sales table with columns for Country, Region, and Sales. It inserts four rows and two of the rows have matching values for Country and Region.  

```sql
CREATE TABLE Sales ( Country varchar(50), Region varchar(50), Sales int );

INSERT INTO sales VALUES (N'Canada', N'Alberta', 100);
INSERT INTO sales VALUES (N'Canada', N'British Columbia', 200);
INSERT INTO sales VALUES (N'Canada', N'British Columbia', 300);
INSERT INTO sales VALUES (N'United States', N'Montana', 100);
```
The Sales table contains these rows:

| Country | Region | Sales |
|---------|--------|-------|
| Canada | Alberta | 100 |
| Canada | British Columbia | 200 |
| Canada | British Columbia | 300 |
| United States | Montana | 100 |

This next query groups Country and Region and returns the aggregate sum for each combination of values.  
 
```sql 
SELECT Country, Region, SUM(sales) AS TotalSales
FROM Sales
GROUP BY Country, Region;
```
The query result has 3 rows since there are 3  combinations of values for Country and Region. The TotalSales for Canada and British Columbia is the sum of two rows. 

| Country | Region | TotalSales |
|---------|--------|-------|
| Canada | Alberta | 100 |
| Canada | British Columbia | 500 |
| United States | Montana | 100 |

### GROUP BY ROLLUP

Creates a group for each combination of column expressions. In addition, it "rolls up" the results into subtotals and grand totals. To do this, it moves from right to left decreasing the number of column expressions over which it creates groups and the aggregation(s). 

The column order affects the ROLLUP output and can affect the number of rows in the result set.  

For example, `GROUP BY ROLLUP (col1, col2, col3, col4)` creates groups for each combination of column expressions in the following lists.  

- col1, col2, col3, col4 
- col1, col2, col3, NULL
- col1, col2, NULL, NULL
- col1, NULL, NULL, NULL
- NULL, NULL, NULL, NULL --This is the grand total

Using the table from the previous example, this code runs a GROUP BY ROLLUP operation instead of a simple GROUP BY.

```sql
SELECT Country, Region, SUM(Sales) AS TotalSales
FROM Sales
GROUP BY ROLLUP (Country, Region);
```

The query result has the same aggregations as the simple GROUP BY without the ROLLUP. In addition, it creates subtotals for each value of Country. Finally, it gives a grand total for all rows. The result looks like this:

| Country | Region | TotalSales |
| :------ | :----- | ---------: |
| Canada | Alberta | 100 |
| Canada | British Columbia | 500 |
| Canada | NULL | 600 |
| United States | Montana | 100 |
| United States | NULL | 100 |
| NULL | NULL | 700 |

### GROUP BY CUBE ( )  

GROUP BY CUBE creates groups for all possible combinations of columns. For GROUP BY CUBE (a, b) the results has groups for unique values of (a, b), (NULL, b), (a, NULL), and (NULL, NULL).

Using the table from the previous examples, this code runs a GROUP BY CUBE operation on Country and Region. 

```sql
SELECT Country, Region, SUM(Sales) AS TotalSales
FROM Sales
GROUP BY CUBE (Country, Region);
```

The query result has groups for unique values of (Country, Region), (NULL, Region), (Country, NULL), and (NULL, NULL). The results look like this:

| Country | Region | TotalSales |
|---------|--------|-------|
| Canada | Alberta | 100 |
| NULL | Alberta | 100 |
| Canada | British Columbia | 500 |
| NULL | British Columbia | 500 |
| United States | Montana | 100 |
| NULL | Montana | 100 |
| NULL | NULL | 700
| Canada | NULL | 600 |
| United States | NULL | 100 |
   
 ### GROUP BY GROUPING SETS ( )  
 
The GROUPING SETS option gives you the ability to combine multiple GROUP BY clauses into one GROUP BY clause. The results are the equivalent of UNION ALL of the specified groups. 

For example, `GROUP BY ROLLUP (Country, Region)` and `GROUP BY GROUPING SETS ( ROLLUP (Country, Region) )` return the same results. 

When GROUPING SETS has two or more elements, the results are a union of the elements. This example returns the union of the ROLLUP and CUBE results for Country and Region.

```sql
SELECT Country, Region, SUM(Sales) AS TotalSales
FROM Sales
GROUP BY GROUPING SETS ( ROLLUP (Country, Region), CUBE (Country, Region) );
```

The results are the same as this query that returns a union of the two GROUP BY statements.

```sql
SELECT Country, Region, SUM(Sales) AS TotalSales
FROM Sales
GROUP BY ROLLUP (Country, Region)
UNION ALL
SELECT Country, Region, SUM(Sales) AS TotalSales
FROM Sales
GROUP BY CUBE (Country, Region);
```

SQL does not consolidate duplicate groups generated for a GROUPING SETS list. For example, in `GROUP BY ( (), CUBE (Country, Region) )`, both elements return a row for the grand total and both rows will be listed in the results. 

 ### GROUP BY ()  
Specifies the empty group which generates the grand total. This is useful as one of the elements of a GROUPING SET. For example, this statement gives the total sales for each country and then gives the grand-total for all countries.

```sql
SELECT Country, SUM(Sales) AS TotalSales
FROM Sales
GROUP BY GROUPING SETS ( Country, () );
```

### GROUP BY [ ALL ] column-expression [ ,...n ] 

Applies to: SQL Server and Azure SQL Database

NOTE: This syntax is provided for backward compatibility only. It will be removed in a future version. Avoid using this syntax in new development work, and plan to modify applications that currently use this syntax.

Specifies to include all groups in the results regardless of whether they meet the search criteria in the WHERE clause. Groups that don't meet the search criteria have NULL for the aggregation. 

GROUP BY ALL:
- Is not supported in queries that access remote tables if there is also a WHERE clause in the query.
- Will fail on columns that have the FILESTREAM attribute.
  
### WITH (DISTRIBUTED_AGG)
Applies to: Azure SQL Data Warehouse and Parallel Data Warehouse

The DISTRIBUTED_AGG query hint forces the massively parallel processing (MPP) system to redistribute a table on a specific column before performing an aggregation. Only one column in the GROUP BY clause can have a DISTRIBUTED_AGG query hint. After the query finishes, the redistributed table is dropped. The original table is not changed.  

NOTE: The DISTRIBUTED_AGG query hint is provided for backwards compatibility with earlier Parallel Data Warehouse versions and will not improve performance for most queries. By default, MPP already redistributes data as necessary to improve performance for aggregations. 
  
## General Remarks

### How GROUP BY interacts with the SELECT statement
SELECT list:
- Vector aggregates. If aggregate functions are included in the SELECT list, GROUP BY calculates a summary value for each group. These are known as vector aggregates. 
- Distinct aggregates. The aggregates AVG (DISTINCT *column_name*), COUNT (DISTINCT *column_name*), and SUM (DISTINCT *column_name*) are supported with ROLLUP, CUBE, and GROUPING SETS.
  
WHERE clause:
- SQL removes Rows that do not meet the conditions in the WHERE clause before any grouping operation is performed.  
  
HAVING clause:
- SQL uses the having clause to filter groups in the result set. 
  
ORDER BY clause:
- Use the ORDER BY clause to order the result set. The GROUP BY clause does not order the result set. 
  
NULL values:
- If a grouping column contains NULL values, all NULL values are considered equal and they are collected into a single group.   
  
## Limitations and Restrictions

Applies to: SQL Server (starting with 2008) and Azure SQL Data Warehouse

### Maximum capacity

For a GROUP BY clause that uses ROLLUP, CUBE, or GROUPING SETS, the maximum number of expressions is 32. The maximum number of groups is 4096 (2<sup>12</sup>). The following examples fail because the GROUP BY clause has more than 4096 groups.  
 
-   The following example generates 4097 (2<sup>12</sup> + 1) grouping sets and will fail.  
  
    ```sql
    GROUP BY GROUPING SETS( CUBE(a1, ..., a12), b )  
    ```  
  
-   The following example generates 4097 (2<sup>12</sup> + 1) groups and will fail. Both `CUBE ()` and the `()` grouping set produce a grand total row and duplicate grouping sets are not eliminated.  
  
    ```sql
    GROUP BY GROUPING SETS( CUBE(a1, ..., a12), ())  
    ```  

-   This example uses the backwards compatible syntax. It generates 8192 (2<sup>13</sup>) grouping sets and will fail.  
  
    ```sql
    GROUP BY CUBE (a1, ..., a13)   
    GROUP BY a1, ..., a13 WITH CUBE   
    ```    
	For backwards compatible GROUP BY clauses that do not contain CUBE or ROLLUP, the number of group by items is limited by the GROUP BY column sizes, the aggregated columns, and the aggregate values involved in the query. This limit originates from the limit of 8,060 bytes on the intermediate worktable that is needed to hold intermediate query results. A maximum of 12 grouping expressions is permitted when CUBE or ROLLUP is specified.

### Support for ISO and ANSI SQL-2006 GROUP BY Features

The GROUP BY clause supports all GROUP BY features that are included in the SQL-2006 standard with the following syntax exceptions:  
  
-   Grouping sets are not allowed in the GROUP BY clause unless they are part of an explicit GROUPING SETS list. For example, `GROUP BY Column1, (Column2, ...ColumnN`) is allowed in the standard but not in Transact-SQL.  Transact-SQL supports `GROUP BY C1, GROUPING SETS ((Column2, ...ColumnN))` and `GROUP BY Column1, Column2, ... ColumnN`, which are semantically equivalent. These are semantically equivalent to the previous `GROUP BY` example. This is to avoid the possibility that `GROUP BY Column1, (Column2, ...ColumnN`) might be misinterpreted as `GROUP BY C1, GROUPING SETS ((Column2, ...ColumnN))`, which are not semantically equivalent.  
  
-   Grouping sets are not allowed inside grouping sets. For example, `GROUP BY GROUPING SETS (A1, A2,...An, GROUPING SETS (C1, C2, ...Cn))` is allowed in the SQL-2006 standard but not in Transact-SQL. Transact-SQL allows `GROUP BY GROUPING SETS( A1, A2,...An, C1, C2, ...Cn )` or `GROUP BY GROUPING SETS( (A1), (A2), ... (An), (C1), (C2), ... (Cn) )`, which are semantically equivalent to the first GROUP BY example and have a more clear syntax.  
  
-   GROUP BY [ALL/DISTINCT] is only allowed in a simple GROUP BY clause that contains column expressions. It is not allowed with the GROUPING SETS, ROLLUP, CUBE, WITH CUBE or WITH ROLLUP constructs. ALL is the default and is implicit. It is also only allowed in the backwards compatible syntax.
  
### Comparison of Supported GROUP BY Features  
 The following table describes the GROUP BY features that are supported based upon SQL versions and database compatibility level.  
  
|Feature|SQL Server Integration Services|SQL Server compatibility level 100 or higher|SQL Server 2008 or later with compatibility level 90.|  
|-------------|-------------------------------------|--------------------------------------------------|-----------------------------------------------------------|  
|DISTINCT aggregates|Not supported for WITH CUBE or WITH ROLLUP.|Supported for WITH CUBE, WITH ROLLUP, GROUPING SETS, CUBE, or ROLLUP.|Same as compatibility level 100.|  
|User-defined function with CUBE or ROLLUP name in the GROUP BY clause|User-defined function **dbo.cube(**_arg1_**,**_...argN_**)** or **dbo.rollup(**_arg1_**,**..._argN_**)** in the GROUP BY clause is allowed.<br /><br /> For example: `SELECT SUM (x) FROM T  GROUP BY dbo.cube(y);`|User-defined function **dbo.cube (**_arg1_**,**...argN**)** or **dbo.rollup(**arg1**,**_...argN_**)** in the GROUP BY clause is not allowed.<br /><br /> For example: `SELECT SUM (x) FROM T  GROUP BY dbo.cube(y);`<br /><br /> The following error message is returned: "Incorrect syntax near the keyword 'cube'&#124;'rollup'."<br /><br /> To avoid this problem, replace `dbo.cube` with `[dbo].[cube]` or `dbo.rollup` with `[dbo].[rollup]`.<br /><br /> The following example is allowed: `SELECT SUM (x) FROM T  GROUP BY [dbo].[cube](y);`|User-defined function **dbo.cube (**_arg1_**,**_...argN_) or **dbo.rollup(**_arg1_**,**_...argN_**)** in the GROUP BY clause is allowed<br /><br /> For example: `SELECT SUM (x) FROM T  GROUP BY dbo.cube(y);`|  
|GROUPING SETS|Not supported|Supported|Supported|  
|CUBE|Not supported|Supported|Not supported|  
|ROLLUP|Not supported|Supported|Not supported|  
|Grand total, such as GROUP BY ()|Not supported|Supported|Supported|  
|GROUPING_ID function|Not supported|Supported|Supported|  
|GROUPING function|Supported|Supported|Supported|  
|WITH CUBE|Supported|Supported|Supported|  
|WITH ROLLUP|Supported|Supported|Supported|  
|WITH CUBE or WITH ROLLUP "duplicate" grouping removal|Supported|Supported|Supported| 
 
  
## Examples  
  
### A. Use a simple GROUP BY clause  
 The following example retrieves the total for each `SalesOrderID` from the `SalesOrderDetail` table. This example uses AdventureWorks.  
  
```sql  
SELECT SalesOrderID, SUM(LineTotal) AS SubTotal  
FROM Sales.SalesOrderDetail AS sod  
GROUP BY SalesOrderID  
ORDER BY SalesOrderID;  
```  
  
### B. Use a GROUP BY clause with multiple tables  
 The following example retrieves the number of employees for each `City` from the `Address` table joined to the `EmployeeAddress` table. This example uses AdventureWorks. 
  
```sql  
SELECT a.City, COUNT(bea.AddressID) EmployeeCount  
FROM Person.BusinessEntityAddress AS bea   
    INNER JOIN Person.Address AS a  
        ON bea.AddressID = a.AddressID  
GROUP BY a.City  
ORDER BY a.City;  
```  
  
### C. Use a GROUP BY clause with an expression  
 The following example retrieves the total sales for each year by using the `DATEPART` function. The same expression must be present in both the `SELECT` list and `GROUP BY` clause.  
  
```sql  
SELECT DATEPART(yyyy,OrderDate) AS N'Year'  
    ,SUM(TotalDue) AS N'Total Order Amount'  
FROM Sales.SalesOrderHeader  
GROUP BY DATEPART(yyyy,OrderDate)  
ORDER BY DATEPART(yyyy,OrderDate);  
```  
  
### D. Use a GROUP BY clause with a HAVING clause  
 The following example uses the `HAVING` clause to specify which of the groups generated in the `GROUP BY` clause should be included in the result set.  
  
```sql  
SELECT DATEPART(yyyy,OrderDate) AS N'Year'  
    ,SUM(TotalDue) AS N'Total Order Amount'  
FROM Sales.SalesOrderHeader  
GROUP BY DATEPART(yyyy,OrderDate)  
HAVING DATEPART(yyyy,OrderDate) >= N'2003'  
ORDER BY DATEPART(yyyy,OrderDate);  
```  
  
## Examples: SQL Data Warehouse and Parallel Data Warehouse  
  
### E. Basic use of the GROUP BY clause  
 The following example finds the total amount for all sales on each day. One row containing the sum of all sales is returned for each day.  
  
```sql  
-- Uses AdventureWorksDW  
  
SELECT OrderDateKey, SUM(SalesAmount) AS TotalSales FROM FactInternetSales  
GROUP BY OrderDateKey ORDER BY OrderDateKey;  
```  
  
### F. Basic use of the DISTRIBUTED_AGG hint  
 This example uses the DISTRIBUTED_AGG query hint to force the appliance to shuffle the table on the `CustomerKey` column before performing the aggregation.  
  
```sql  
-- Uses AdventureWorksDW  
  
SELECT CustomerKey, SUM(SalesAmount) AS sas  
FROM FactInternetSales  
GROUP BY CustomerKey WITH (DISTRIBUTED_AGG)  
ORDER BY CustomerKey DESC;  
```  
  
### G. Syntax Variations for GROUP BY  
 When the select list has no aggregations, each column in the select list must be included in the GROUP BY list. Computed columns in the select list can be listed, but are not required, in the GROUP BY list. These are examples of syntactically valid SELECT statements:  
  
```sql  
-- Uses AdventureWorks  
  
SELECT LastName, FirstName FROM DimCustomer GROUP BY LastName, FirstName;  
SELECT NumberCarsOwned FROM DimCustomer GROUP BY YearlyIncome, NumberCarsOwned;  
SELECT (SalesAmount + TaxAmt + Freight) AS TotalCost FROM FactInternetSales GROUP BY SalesAmount, TaxAmt, Freight;  
SELECT SalesAmount, SalesAmount*1.10 SalesTax FROM FactInternetSales GROUP BY SalesAmount;  
SELECT SalesAmount FROM FactInternetSales GROUP BY SalesAmount, SalesAmount*1.10;  
```  
  
### H. Using a GROUP BY with multiple GROUP BY expressions  
 The following example groups results using multiple `GROUP BY` criteria. If, within each `OrderDateKey` group, there are subgroups that can be differentiated by `DueDateKey`, a new grouping will be defined for the result set.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT OrderDateKey, DueDateKey, SUM(SalesAmount) AS TotalSales   
FROM FactInternetSales
GROUP BY OrderDateKey, DueDateKey   
ORDER BY OrderDateKey;  
```  
  
### I. Using a GROUP BY clause with a HAVING clause  
 The following example uses the `HAVING` clause to specify the groups generated in the `GROUP BY` clause that should be included in the result set. Only those groups with order dates in 2004 or later will be included in the results.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT OrderDateKey, SUM(SalesAmount) AS TotalSales   
FROM FactInternetSales  
GROUP BY OrderDateKey   
HAVING OrderDateKey > 20040000   
ORDER BY OrderDateKey;  
```  
  
## See Also  
 [GROUPING_ID &#40;Transact-SQL&#41;](~/t-sql/functions/grouping-id-transact-sql.md)   
 [GROUPING &#40;Transact-SQL&#41;](~/t-sql/functions/grouping-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](~/t-sql/queries/select-transact-sql.md)   
 [SELECT Clause &#40;Transact-SQL&#41;](~/t-sql/queries/select-clause-transact-sql.md)  
  
  




