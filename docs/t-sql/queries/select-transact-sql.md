---
title: SELECT (Transact-SQL)
description: SELECT (Transact-SQL)
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "SELECT_TSQL"
  - "SELECT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "retrieving rows"
  - "SELECT statement [SQL Server]"
  - "SELECT statement [SQL Server], about SELECT statement"
  - "row retrieval [SQL Server], SELECT statement"
  - "DML [SQL Server], SELECT statement"
  - "data manipulation language [SQL Server], SELECT statement"
  - "row retrieval [SQL Server]"
  - "queries [SQL Server], results"
author: VanMSFT
ms.author: vanto
ms.custom:
- event-tier1-build-2022
ms.date: "10/24/2017"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SELECT (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Retrieves rows from the database and enables the selection of one or many rows or columns from one or many tables in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The full syntax of the SELECT statement is complex, but the main clauses can be summarized as:  
  
[ WITH { [[ XMLNAMESPACES ,]](../../t-sql/xml/with-xmlnamespaces.md) [[ \<common_table_expression> ]](../../t-sql/queries/with-common-table-expression-transact-sql.md) } ]
  
 [SELECT *select_list*](../../t-sql/queries/select-clause-transact-sql.md) [ [INTO *new_table*](../../t-sql/queries/select-into-clause-transact-sql.md) ]  
  
 [ [FROM *table_source*](../../t-sql/queries/from-transact-sql.md) ] [ [WHERE *search_condition*](../../t-sql/queries/where-transact-sql.md) ]  
  
 [ [GROUP BY *group_by_expression*](../../t-sql/queries/select-group-by-transact-sql.md) ]  

 [ [WINDOW *window expression*](../../t-sql/queries/select-window-transact-sql.md)]
  
 [ [HAVING *search_condition*](../../t-sql/queries/select-having-transact-sql.md) ]  
  
 [ [ORDER BY *order_expression* [ ASC | DESC ] ](../../t-sql/queries/select-order-by-clause-transact-sql.md)]  
  
 The [UNION](../../t-sql/language-elements/set-operators-union-transact-sql.md), [EXCEPT, and INTERSECT](../../t-sql/language-elements/set-operators-except-and-intersect-transact-sql.md) operators can be used between queries to combine or compare their results into one result set.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- Syntax for SQL Server and Azure SQL Database  
  
<SELECT statement> ::=    
    [ WITH { [ XMLNAMESPACES ,] [ <common_table_expression> [,...n] ] } ]  
    <query_expression>   
    [ ORDER BY <order_by_expression> ] 
    [ <FOR Clause>]   
    [ OPTION ( <query_hint> [ ,...n ] ) ]   
<query_expression> ::=   
    { <query_specification> | ( <query_expression> ) }   
    [  { UNION [ ALL ] | EXCEPT | INTERSECT }  
        <query_specification> | ( <query_expression> ) [...n ] ]   
<query_specification> ::=   
SELECT [ ALL | DISTINCT ]   
    [TOP ( expression ) [PERCENT] [ WITH TIES ] ]   
    < select_list >   
    [ INTO new_table ]   
    [ FROM { <table_source> } [ ,...n ] ]   
    [ WHERE <search_condition> ]   
    [ <GROUP BY> ]   
    [ HAVING < search_condition > ]   
```  
  
```syntaxsql
-- Syntax for Azure Synapse Analytics and Parallel Data Warehouse  
  
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

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Remarks  
 Because of the complexity of the SELECT statement, detailed syntax elements and arguments are shown by clause:  

:::row:::
    :::column:::
        [WITH XMLNAMESPACES](../../t-sql/xml/with-xmlnamespaces.md)
    :::column-end:::
    :::column:::
        [HAVING](../../t-sql/queries/select-having-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [WITH common_table_expression](../../t-sql/queries/with-common-table-expression-transact-sql.md)
    :::column-end:::
    :::column:::
        [UNION](../../t-sql/language-elements/set-operators-union-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [SELECT Clause](../../t-sql/queries/select-clause-transact-sql.md)
    :::column-end:::
    :::column:::
        [EXCEPT and INTERSECT](../../t-sql/language-elements/set-operators-except-and-intersect-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [INTO Clause](../../t-sql/queries/select-into-clause-transact-sql.md)
    :::column-end:::
    :::column:::
        [ORDER BY](../../t-sql/queries/select-order-by-clause-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [FROM](../../t-sql/queries/from-transact-sql.md)
    :::column-end:::
    :::column:::
        [FOR Clause](../../t-sql/queries/select-for-clause-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [WHERE](../../t-sql/queries/where-transact-sql.md)
    :::column-end:::
    :::column:::
        [OPTION Clause](../../t-sql/queries/option-clause-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [GROUP BY](../../t-sql/queries/select-group-by-transact-sql.md)
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::

 The order of the clauses in the SELECT statement is significant. Any one of the optional clauses can be omitted, but when the optional clauses are used, they must appear in the appropriate order.  
  
 SELECT statements are permitted in user-defined functions only if the select lists of these statements contain expressions that assign values to variables that are local to the functions.  
  
 A four-part name constructed with the OPENDATASOURCE function as the server-name part can be used as a table source wherever a table name can appear in a SELECT statement. A four-part name cannot be specified for [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].  
  
 Some syntax restrictions apply to SELECT statements that involve remote tables.  
  
## Logical Processing Order of the SELECT statement  
 The following steps show the logical processing order, or binding order, for a SELECT statement. This order determines when the objects defined in one step are made available to the clauses in subsequent steps. For example, if the query processor can bind to (access) the tables or views defined in the FROM clause, these objects and their columns are made available to all subsequent steps. Conversely, because the SELECT clause is step 8, any column aliases or derived columns defined in that clause cannot be referenced by preceding clauses. However, they can be referenced by subsequent clauses such as the ORDER BY clause. The actual physical execution of the statement is determined by the query processor and the order may vary from this list.  
  
1.  FROM  
2.  ON  
3.  JOIN  
4.  WHERE  
5.  GROUP BY  
6.  WITH CUBE or WITH ROLLUP  
7.  HAVING  
8.  SELECT  
9. DISTINCT  
10. ORDER BY  
11. TOP  

> [!WARNING]
> The preceding sequence is usually true. However, there are uncommon cases where the sequence may differ.
>
> For example, suppose you have a clustered index on a view, and the view excludes some table rows, and the view's SELECT column list uses a CONVERT that changes a data type from *varchar* to *integer*. In this situation, the CONVERT may execute before the WHERE clause executes. Uncommon indeed. Often there is a way to modify your view to avoid the different sequence, if it matters in your case. 

## Permissions  
 Selecting data requires **SELECT** permission on the table or view, which could be inherited from a higher scope such as **SELECT** permission on the schema or **CONTROL** permission on the table. Or requires membership in the **db_datareader** or **db_owner** fixed database roles, or the **sysadmin** fixed server role. Creating a new table using **SELECT INTO** also requires both the **CREATE TABLE** permission, and the **ALTER SCHEMA** permission on the schema that owns the new table.  
  
## Examples:   
The following examples use the [!INCLUDE[ssawPDW](../../includes/ssawpdw-md.md)] database.
  
### A. Using SELECT to retrieve rows and columns  
 This section shows three code examples. This first code example returns all rows (no WHERE clause is specified) and all columns (using the `*`) from the `DimEmployee` table.  
  
```sql  
SELECT *  
FROM DimEmployee  
ORDER BY LastName;  
```  
  
 This next example using table aliasing to achieve the same result.  
  
```sql  
SELECT e.*  
FROM DimEmployee AS e  
ORDER BY LastName;  
```  
  
 This example returns all rows (no WHERE clause is specified) and a subset of the columns (`FirstName`, `LastName`, `StartDate`) from the `DimEmployee` table in the `AdventureWorksPDW2012` database. The third column heading is renamed to `FirstDay`.  
  
```sql  
SELECT FirstName, LastName, StartDate AS FirstDay  
FROM DimEmployee   
ORDER BY LastName;  
```  
  
 This example returns only the rows for `DimEmployee` that have an `EndDate` that is not NULL and a `MaritalStatus` of 'M' (married).  
  
```sql  
SELECT FirstName, LastName, StartDate AS FirstDay  
FROM DimEmployee   
WHERE EndDate IS NOT NULL   
AND MaritalStatus = 'M'  
ORDER BY LastName;  
```  
  
### B. Using SELECT with column headings and calculations  
 The following example returns all rows from the `DimEmployee` table, and calculates the gross pay for each employee based on their `BaseRate` and a 40-hour work week.  
  
```sql  
SELECT FirstName, LastName, BaseRate, BaseRate * 40 AS GrossPay  
FROM DimEmployee  
ORDER BY LastName;  
```  
  
### C. Using DISTINCT with SELECT  
 The following example uses `DISTINCT` to generate a list of all unique titles in the `DimEmployee` table.  
  
```sql  
SELECT DISTINCT Title  
FROM DimEmployee  
ORDER BY Title;  
```  
  
### D. Using GROUP BY  
 The following example finds the total amount for all sales on each day.  
  
```sql  
SELECT OrderDateKey, SUM(SalesAmount) AS TotalSales  
FROM FactInternetSales  
GROUP BY OrderDateKey  
ORDER BY OrderDateKey;  
```  
  
 Because of the `GROUP BY` clause, only one row containing the sum of all sales is returned for each day.  
  
### E. Using GROUP BY with multiple groups  
 The following example finds the average price and the sum of Internet sales for each day, grouped by order date and the promotion key.  
  
```sql  

SELECT OrderDateKey, PromotionKey, AVG(SalesAmount) AS AvgSales, SUM(SalesAmount) AS TotalSales  
FROM FactInternetSales  
GROUP BY OrderDateKey, PromotionKey  
ORDER BY OrderDateKey;   
```  
  
### F. Using GROUP BY and WHERE  
 The following example puts the results into groups after retrieving only the rows with order dates later than August 1, 2002.  
  
```sql  
SELECT OrderDateKey, SUM(SalesAmount) AS TotalSales  
FROM FactInternetSales  
WHERE OrderDateKey > '20020801'  
GROUP BY OrderDateKey  
ORDER BY OrderDateKey;  
```  
  
### G. Using GROUP BY with an expression  
 The following example groups by an expression. You can group by an expression if the expression does not include aggregate functions.  
  
```sql  
SELECT SUM(SalesAmount) AS TotalSales  
FROM FactInternetSales  
GROUP BY (OrderDateKey * 10);  
```  
  
### H. Using GROUP BY with ORDER BY  
 The following example finds the sum of sales per day, and orders by the day.  
  
```sql  
SELECT OrderDateKey, SUM(SalesAmount) AS TotalSales  
FROM FactInternetSales  
GROUP BY OrderDateKey  
ORDER BY OrderDateKey;  
```  
  
### I. Using the HAVING clause  
 This query uses the `HAVING` clause to restrict results.  
  
```sql  
SELECT OrderDateKey, SUM(SalesAmount) AS TotalSales  
FROM FactInternetSales  
GROUP BY OrderDateKey  
HAVING OrderDateKey > 20010000  
ORDER BY OrderDateKey;  
```  
  
## See Also  
 [SELECT Examples &#40;Transact-SQL&#41;](../../t-sql/queries/select-examples-transact-sql.md)  
 [Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql.md)
  
