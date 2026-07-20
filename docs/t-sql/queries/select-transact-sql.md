---
title: "SELECT (Transact-SQL)"
description: The SELECT statement retrieves rows from the database and enables the selection of rows or columns from tables in the SQL Server Database Engine.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 02/02/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SELECT_TSQL"
  - "SELECT"
helpviewer_keywords:
  - "retrieving rows"
  - "SELECT statement [SQL Server]"
  - "SELECT statement [SQL Server], about SELECT statement"
  - "row retrieval [SQL Server], SELECT statement"
  - "DML [SQL Server], SELECT statement"
  - "data manipulation language [SQL Server], SELECT statement"
  - "row retrieval [SQL Server]"
  - "queries [SQL Server], results"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# SELECT (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Use the `SELECT` statement to retrieve rows from the database. `SELECT` lets you choose one or many rows or columns from one or many tables in the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)].

Because the full syntax `SELECT` statement is complex, detailed syntax elements and arguments are shown per clause. Refer to the [Syntax](#syntax) section for how these clauses work together.

- [WITH XMLNAMESPACES](../xml/with-xmlnamespaces.md)
- [WITH common_table_expression](with-common-table-expression-transact-sql.md)
- [SELECT clause](select-clause-transact-sql.md)
- [INTO clause](select-into-clause-transact-sql.md)
- [FROM clause (includes JOIN, APPLY, and PIVOT)](from-transact-sql.md)
- [WHERE clause](where-transact-sql.md)
- [GROUP BY clause](select-group-by-transact-sql.md)
- [HAVING clause](select-having-transact-sql.md)
- [WINDOW clause](select-window-transact-sql.md)
- [ORDER BY clause](select-order-by-clause-transact-sql.md)

The [UNION](../language-elements/set-operators-union-transact-sql.md), [EXCEPT](../language-elements/set-operators-except-and-intersect-transact-sql.md), and [INTERSECT](../language-elements/set-operators-except-and-intersect-transact-sql.md) operators can be used between queries to combine or compare their results into one result set.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server and Azure SQL Database:

```syntaxsql
<SELECT statement> ::=
    [ WITH { [ XMLNAMESPACES , ] [ <common_table_expression> [ , ...n ] ] } ]
    <query_expression>
    [ ORDER BY <order_by_expression> ]
    [ <FOR Clause> ]
    [ OPTION ( <query_hint> [ , ...n ] ) ]
<query_expression> ::=
    { <query_specification> | ( <query_expression> ) }
    [  { UNION [ ALL ] | EXCEPT | INTERSECT }
        <query_specification> | ( <query_expression> ) [ ...n ] ]
<query_specification> ::=
SELECT [ ALL | DISTINCT ]
    [ TOP ( expression ) [ PERCENT ] [ WITH TIES ] ]
    <select_list>
    [ INTO new_table ]
    [ FROM { <table_source> } [ , ...n ] ]
    [ WHERE <search_condition> ]
    [ <GROUP BY> ]
    [ HAVING <search_condition> ]
[ ; ]
```

Syntax for Azure Synapse Analytics, Analytics Platform System (PDW), and Microsoft Fabric:

```syntaxsql
[ WITH <common_table_expression> [ , ...n ] ]
SELECT <select_criteria>
[ ; ]

<select_criteria> ::=
    [ TOP ( top_expression ) ]
    [ ALL | DISTINCT ]
    { * | column_name | expression } [ , ...n ]
    [ FROM { table_source } [ , ...n ] ]
    [ WHERE <search_condition> ]
    [ GROUP BY <group_by_clause> ]
    [ HAVING <search_condition> ]
    [ ORDER BY <order_by_expression> ]
    [ OPTION ( <query_option> [ , ...n ] ) ]
```

## Remarks

The order of the clauses in the `SELECT` statement is significant. Any one of the optional clauses can be omitted, but when the optional clauses are used, they must appear in the appropriate order.

`SELECT` statements are permitted in user-defined functions only if the select lists of these statements contain expressions that assign values to variables that are local to the functions.

A four-part name constructed with the `OPENDATASOURCE` function as the server-name part can be used as a table source wherever a table name can appear in a `SELECT` statement. A four-part name can't be specified for [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Some syntax restrictions apply to `SELECT` statements that involve remote tables.

## Logical processing order of the SELECT statement

The following steps show the logical processing order, or binding order, for a `SELECT` statement. This order determines when the objects defined in one step are made available to the clauses in subsequent steps. For example, if the query processor can bind to (access) the tables or views defined in the `FROM` clause, these objects and their columns are made available to all subsequent steps. Conversely, because the `SELECT` clause is step 8, any column aliases or derived columns defined in that clause can't be referenced by preceding clauses. However, they can be referenced by subsequent clauses such as the `ORDER BY` clause. The query processor determines the actual physical execution of the statement, and the order might vary from this list.

1. `FROM`
1. `ON`
1. `JOIN`
1. `WHERE`
1. `GROUP BY`
1. `WITH CUBE` or `WITH ROLLUP`
1. `HAVING`
1. `SELECT`
1. `DISTINCT`
1. `ORDER BY`
1. `TOP`

> [!WARNING]  
> There are uncommon cases where the previous sequence might differ. Suppose you have a clustered index on a view, and the view excludes some table rows, and the view's `SELECT` column list uses a `CONVERT` that changes a data type from **varchar** to **int**. In this situation, the `CONVERT` can execute before the `WHERE` clause executes. Often there's a way to modify your view to avoid the different sequence, if it matters in your case.

## Permissions

Selecting data requires `SELECT` permission on the table or view, which could be inherited from a higher scope such as `SELECT` permission on the schema or `CONTROL` permission on the table. Or requires membership in the **db_datareader** or **db_owner** fixed database roles, or the **sysadmin** fixed server role. Creating a new table using `SELECT INTO` also requires both the `CREATE TABLE` permission, and the `ALTER SCHEMA` permission on the schema that owns the new table.

## Examples

The following examples use the [!INCLUDE [ssawPDW](../../includes/ssawpdw-md.md)] database.

### A. Use SELECT to retrieve rows and columns

This section shows three code examples. This first code example returns all rows (no `WHERE` clause is specified) and all columns (using the `*`) from the `DimEmployee` table.

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

This example returns all rows (no `WHERE` clause is specified) and a subset of the columns (`FirstName`, `LastName`, `StartDate`) from the `DimEmployee` table in the [!INCLUDE [ssawPDW](../../includes/ssawpdw-md.md)] database. The third column heading is renamed to `FirstDay`.

```sql
SELECT FirstName,
       LastName,
       StartDate AS FirstDay
FROM DimEmployee
ORDER BY LastName;
```

This example returns only the rows for `DimEmployee` that have an `EndDate` that isn't `NULL` and a `MaritalStatus` of `M` (married).

```sql
SELECT FirstName,
       LastName,
       StartDate AS FirstDay
FROM DimEmployee
WHERE EndDate IS NOT NULL
      AND MaritalStatus = 'M'
ORDER BY LastName;
```

### B. Use SELECT with column headings and calculations

The following example returns all rows from the `DimEmployee` table, and calculates the gross pay for each employee based on their `BaseRate` and a 40-hour work week.

```sql
SELECT FirstName,
       LastName,
       BaseRate,
       BaseRate * 40 AS GrossPay
FROM DimEmployee
ORDER BY LastName;
```

<a id="c-using-distinct-with-select"></a>

### C. Use DISTINCT with SELECT

The following example uses `DISTINCT` to generate a list of all unique titles in the `DimEmployee` table.

```sql
SELECT DISTINCT Title
FROM DimEmployee
ORDER BY Title;
```

### D. Use GROUP BY

The following example finds the total amount for all sales on each day.

```sql
SELECT OrderDateKey,
       SUM(SalesAmount) AS TotalSales
FROM FactInternetSales
GROUP BY OrderDateKey
ORDER BY OrderDateKey;
```

Because of the `GROUP BY` clause, only one row containing the sum of all sales is returned for each day.

### E. Use GROUP BY with multiple groups

The following example finds the average price and the sum of Internet sales for each day, grouped by order date and the promotion key.

```sql
SELECT OrderDateKey,
       PromotionKey,
       AVG(SalesAmount) AS AvgSales,
       SUM(SalesAmount) AS TotalSales
FROM FactInternetSales
GROUP BY OrderDateKey, PromotionKey
ORDER BY OrderDateKey;
```

### F. Use GROUP BY and WHERE

The following example puts the results into groups after retrieving only the rows with order dates later than August 1, 2002.

```sql
SELECT OrderDateKey,
       SUM(SalesAmount) AS TotalSales
FROM FactInternetSales
WHERE OrderDateKey > '20020801'
GROUP BY OrderDateKey
ORDER BY OrderDateKey;
```

### G. Use GROUP BY with an expression

The following example groups by an expression. You can group by an expression if the expression doesn't include aggregate functions.

```sql
SELECT SUM(SalesAmount) AS TotalSales
FROM FactInternetSales
GROUP BY (OrderDateKey * 10);
```

### H. Use GROUP BY with ORDER BY

The following example finds the sum of sales per day, and orders by the day.

```sql
SELECT OrderDateKey,
       SUM(SalesAmount) AS TotalSales
FROM FactInternetSales
GROUP BY OrderDateKey
ORDER BY OrderDateKey;
```

### I. Use the HAVING clause

This query uses the `HAVING` clause to restrict results.

```sql
SELECT OrderDateKey,
       SUM(SalesAmount) AS TotalSales
FROM FactInternetSales
GROUP BY OrderDateKey
HAVING OrderDateKey > 20010000
ORDER BY OrderDateKey;
```

## Related content

- [SELECT examples (Transact-SQL)](select-examples-transact-sql.md)
- [OPTION clause (Transact-SQL)](option-clause-transact-sql.md)
- [Hints (Transact-SQL)](hints-transact-sql.md)
