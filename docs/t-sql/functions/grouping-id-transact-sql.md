---
title: "GROUPING_ID (Transact-SQL)"
description: GROUPING_ID is a function that computes the level of grouping, in a SELECT, HAVING, or ORDER BY clause.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 09/06/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "GROUPING_ID_TSQL"
  - "GROUPING_ID"
helpviewer_keywords:
  - "GROUP BY clause, GROUPING_ID"
  - "GROUPING_ID function"
dev_langs:
  - "TSQL"
---
# GROUPING_ID (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

`GROUPING_ID` Is a function that computes the level of grouping. `GROUPING_ID` can be used only in the `SELECT <select>` list, `HAVING`, or `ORDER BY` clauses when `GROUP BY` is specified.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
GROUPING_ID ( <column_expression> [ , ...n ] )
```

## Arguments

#### \<column_expression>

A *column_expression* in a [SELECT - GROUP BY](../queries/select-group-by-transact-sql.md) clause.

## Return types

**int**

## Remarks

The `GROUPING_ID <column_expression>` must exactly match the expression in the `GROUP BY` list. For example, if you're grouping by `DATEPART (yyyy, <column name>)`, use `GROUPING_ID (DATEPART (yyyy, <column name>))`; or if you're grouping by `<column name>`, use `GROUPING_ID (<column name>)`.

## Compare GROUPING_ID() to GROUPING()

`GROUPING_ID (<column_expression> [ , ...n ])` inputs the equivalent of the `GROUPING (<column_expression>)` return for each column in its column list in each output row, as a string of ones and zeros. `GROUPING_ID` interprets that string as a base-2 number and returns the equivalent integer.

For example, consider the following statement:

```sql
SELECT a, b, c, SUM(d),
GROUPING_ID(a, b, c)
FROM T
GROUP BY <group_by_list>
```

This table shows the `GROUPING_ID()` input and output values.

| Columns aggregated | GROUPING_ID (a, b, c) input = GROUPING(a) + GROUPING(b) + GROUPING(c) | GROUPING_ID() output |
| --- | --- | --- |
| `a` | `100` | `4` |
| `b` | `010` | `2` |
| `c` | `001` | `1` |
| `ab` | `110` | `6` |
| `ac` | `101` | `5` |
| `bc` | `011` | `3` |
| `abc` | `111` | `7` |

## Technical definition of GROUPING_ID()

Each `GROUPING_ID` argument must be an element of the `GROUP BY` list. `GROUPING_ID()` returns an integer bitmap whose lowest *n* bits might be *lit*. A lit bit indicates the corresponding argument isn't a grouping column for the given output row. The lowest-order bit corresponds to argument *n*, and the *n*-1<sup>th</sup> lowest-order bit corresponds to argument 1.

## GROUPING_ID() equivalents

For a single grouping query, `GROUPING (<column_expression>)` is equivalent to `GROUPING_ID (<column_expression>)`, and both return `0`.

For example, the following statements are equivalent:

### Statement A

```sql
SELECT GROUPING_ID(A, B)
FROM T
GROUP BY CUBE(A, B)
```

### Statement B

```sql
SELECT 3 FROM T GROUP BY ()
UNION ALL
SELECT 1 FROM T GROUP BY A
UNION ALL
SELECT 2 FROM T GROUP BY B
UNION ALL
SELECT 0 FROM T GROUP BY A, B
```

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Use GROUPING_ID to identify grouping levels

The following example returns the count of employees by `Name` and `Title`, and company total in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database. `GROUPING_ID()` is used to create a value for each row in the `Title` column that identifies its level of aggregation.

```sql
SELECT D.Name,
    CASE 
        WHEN GROUPING_ID(D.Name, E.JobTitle) = 0 THEN E.JobTitle
        WHEN GROUPING_ID(D.Name, E.JobTitle) = 1 THEN N'Total: ' + D.Name
        WHEN GROUPING_ID(D.Name, E.JobTitle) = 3 THEN N'Company Total:'
        ELSE N'Unknown'
    END AS N'Job Title',
    COUNT(E.BusinessEntityID) AS N'Employee Count'
FROM HumanResources.Employee E
INNER JOIN HumanResources.EmployeeDepartmentHistory DH
    ON E.BusinessEntityID = DH.BusinessEntityID
INNER JOIN HumanResources.Department D
    ON D.DepartmentID = DH.DepartmentID
WHERE DH.EndDate IS NULL
    AND D.DepartmentID IN (12, 14)
GROUP BY ROLLUP(D.Name, E.JobTitle);
```

### B. Use GROUPING_ID to filter a result set

#### Basic example

In the following code, to return only the rows that have a count of employees by title, remove the comment characters from `HAVING GROUPING_ID(D.Name, E.JobTitle) = 0;`. To return only rows with a count of employees by department, remove the comment characters from `HAVING GROUPING_ID(D.Name, E.JobTitle) = 1;`.

```sql
SELECT D.Name,
    E.JobTitle,
    GROUPING_ID(D.Name, E.JobTitle) AS [Grouping Level],
    COUNT(E.BusinessEntityID) AS [Employee Count]
FROM HumanResources.Employee AS E
INNER JOIN HumanResources.EmployeeDepartmentHistory AS DH
    ON E.BusinessEntityID = DH.BusinessEntityID
INNER JOIN HumanResources.Department AS D
    ON D.DepartmentID = DH.DepartmentID
WHERE DH.EndDate IS NULL
    AND D.DepartmentID IN (12, 14)
GROUP BY ROLLUP(D.Name, E.JobTitle)
-- HAVING GROUPING_ID(D.Name, E.JobTitle) = 0; -- All titles
-- HAVING GROUPING_ID(D.Name, E.JobTitle) = 1; -- Group by Name;
```

Here's the unfiltered result set.

| Name | Title | Grouping Level | Employee Count | Name |
| --- | --- | --- | --- | --- |
| Document Control | Control Specialist | 0 | 2 | Document Control |
| Document Control | Document Control Assistant | 0 | 2 | Document Control |
| Document Control | Document Control Manager | 0 | 1 | Document Control |
| Document Control | `NULL` | 1 | 5 | Document Control |
| Facilities and Maintenance | Facilities Administrative Assistant | 0 | 1 | Facilities and Maintenance |
| Facilities and Maintenance | Facilities Manager | 0 | 1 | Facilities and Maintenance |
| Facilities and Maintenance | Janitor | 0 | 4 | Facilities and Maintenance |
| Facilities and Maintenance | Maintenance Supervisor | 0 | 1 | Facilities and Maintenance |
| Facilities and Maintenance | `NULL` | 1 | 7 | Facilities and Maintenance |
| `NULL` | `NULL` | 3 | 12 | `NULL` |

#### Complex example

The following example uses `GROUPING_ID()` to filter a result set that contains multiple grouping levels by grouping level. Similar code can be used to create a view that has several grouping levels, and a stored procedure that calls the view by passing a parameter that filters the view by grouping level.

```sql
DECLARE @Grouping NVARCHAR(50);
DECLARE @GroupingLevel SMALLINT;

SET @Grouping = N'CountryRegionCode Total';

SELECT @GroupingLevel = (
    CASE @Grouping
        WHEN N'Grand Total' THEN 15
        WHEN N'SalesPerson Total' THEN 14
        WHEN N'Store Total' THEN 13
        WHEN N'Store SalesPerson Total' THEN 12
        WHEN N'CountryRegionCode Total' THEN 11
        WHEN N'Group Total' THEN 7
        ELSE N'Unknown'
    END
);

SELECT T.[Group],
    T.CountryRegionCode,
    S.Name AS N'Store',
    (
        SELECT P.FirstName + ' ' + P.LastName
        FROM Person.Person AS P
        WHERE P.BusinessEntityID = H.SalesPersonID
    ) AS N'Sales Person',
    SUM(TotalDue) AS N'TotalSold',
    CAST(GROUPING(T.[Group]) AS CHAR(1)) + CAST(GROUPING(T.CountryRegionCode) AS CHAR(1)) + CAST(GROUPING(S.Name) AS CHAR(1)) + CAST(GROUPING(H.SalesPersonID) AS CHAR(1)) AS N'GROUPING base-2',
    GROUPING_ID((T.[Group]), (T.CountryRegionCode), (S.Name), (H.SalesPersonID)) AS N'GROUPING_ID',
    CASE 
        WHEN GROUPING_ID((T.[Group]), (T.CountryRegionCode), (S.Name), (H.SalesPersonID)) = 15
            THEN N'Grand Total'
        WHEN GROUPING_ID((T.[Group]), (T.CountryRegionCode), (S.Name), (H.SalesPersonID)) = 14
            THEN N'SalesPerson Total'
        WHEN GROUPING_ID((T.[Group]), (T.CountryRegionCode), (S.Name), (H.SalesPersonID)) = 13
            THEN N'Store Total'
        WHEN GROUPING_ID((T.[Group]), (T.CountryRegionCode), (S.Name), (H.SalesPersonID)) = 12
            THEN N'Store SalesPerson Total'
        WHEN GROUPING_ID((T.[Group]), (T.CountryRegionCode), (S.Name), (H.SalesPersonID)) = 11
            THEN N'CountryRegionCode Total'
        WHEN GROUPING_ID((T.[Group]), (T.CountryRegionCode), (S.Name), (H.SalesPersonID)) = 7
            THEN N'Group Total'
        ELSE N'Error'
    END AS N'Level'
FROM Sales.Customer AS C
INNER JOIN Sales.Store AS S
    ON C.StoreID = S.BusinessEntityID
INNER JOIN Sales.SalesTerritory AS T
    ON C.TerritoryID = T.TerritoryID
INNER JOIN Sales.SalesOrderHeader AS H
    ON C.CustomerID = H.CustomerID
GROUP BY GROUPING SETS(
    (S.Name,H.SalesPersonID),
    (H.SalesPersonID), 
    (S.Name), 
    (T.[Group]),
    (T.CountryRegionCode),
    ()
)
HAVING GROUPING_ID(
    (T.[Group]),
    (T.CountryRegionCode),
    (S.Name),
    (H.SalesPersonID)
) = @GroupingLevel
ORDER BY
    GROUPING_ID(S.Name, H.SalesPersonID),
    GROUPING_ID(
        (T.[Group]),
        (T.CountryRegionCode),
        (S.Name),
        (H.SalesPersonID)
    ) ASC;
```

### C. Use GROUPING_ID() with ROLLUP and CUBE to identify grouping levels

The code in the following examples shows using `GROUPING()` to compute the `Bit Vector(base-2)` column. `GROUPING_ID()` is used to compute the corresponding `Integer Equivalent` column. The column order in the `GROUPING_ID()` function is the opposite of the column order of the columns that are concatenated by the `GROUPING()` function.

In these examples, `GROUPING_ID()` is used to create a value for each row in the `Grouping Level` column to identify the level of grouping. Grouping levels aren't always a consecutive list of integers that start with 1 (0, 1, 2, ...*n*).

> [!NOTE]  
> `GROUPING` and `GROUPING_ID` can be used in a `HAVING` clause to filter a result set.

#### ROLLUP example

In this example, all grouping levels don't appear as they do in the following `CUBE` example. If the order of the columns in the `ROLLUP` list is changed, the level values in the `Grouping Level` column also have to be changed.

```sql
SELECT DATEPART(yyyy, OrderDate) AS N'Year',
    DATEPART(mm, OrderDate) AS N'Month',
    DATEPART(dd, OrderDate) AS N'Day',
    SUM(TotalDue) AS N'Total Due',
    CAST(GROUPING(DATEPART(dd, OrderDate)) AS CHAR(1)) + CAST(GROUPING(DATEPART(mm, OrderDate)) AS CHAR(1)) + CAST(GROUPING(DATEPART(yyyy, OrderDate)) AS CHAR(1)) AS N'Bit Vector(base-2)',
    GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) AS N'Integer Equivalent',
    CASE 
        WHEN GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) = 0
            THEN N'Year Month Day'
        WHEN GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) = 1
            THEN N'Year Month'
        WHEN GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) = 2
            THEN N'not used'
        WHEN GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) = 3
            THEN N'Year'
        WHEN GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) = 4
            THEN N'not used'
        WHEN GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) = 5
            THEN N'not used'
        WHEN GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) = 6
            THEN N'not used'
        WHEN GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) = 7
            THEN N'Grand Total'
        ELSE N'Error'
    END AS N'Grouping Level'
FROM Sales.SalesOrderHeader
WHERE DATEPART(yyyy, OrderDate) IN (N'2007', N'2008')
    AND DATEPART(mm, OrderDate) IN (1, 2)
    AND DATEPART(dd, OrderDate) IN (1, 2)
GROUP BY ROLLUP(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate))
ORDER BY GROUPING_ID(DATEPART(mm, OrderDate), DATEPART(yyyy, OrderDate), DATEPART(dd, OrderDate)),
    DATEPART(yyyy, OrderDate),
    DATEPART(mm, OrderDate),
    DATEPART(dd, OrderDate);
```

Here's a partial result set.

| Year | Month | Day | Total Due | Bit Vector (base-2) | Integer Equivalent | Grouping Level |
| --- | --- | --- | --- | --- | --- | --- |
| 2007 | 1 | 1 | 1497452.6066 | 000 | 0 | Year Month Day |
| 2007 | 1 | 2 | 21772.3494 | 000 | 0 | Year Month Day |
| 2007 | 2 | 1 | 2705653.5913 | 000 | 0 | Year Month Day |
| 2007 | 2 | 2 | 21684.4068 | 000 | 0 | Year Month Day |
| 2008 | 1 | 1 | 1908122.0967 | 000 | 0 | Year Month Day |
| 2008 | 1 | 2 | 46458.0691 | 000 | 0 | Year Month Day |
| 2008 | 2 | 1 | 3108771.9729 | 000 | 0 | Year Month Day |
| 2008 | 2 | 2 | 54598.5488 | 000 | 0 | Year Month Day |
| 2007 | 1 | `NULL` | 1519224.956 | 100 | 1 | Year Month |
| 2007 | 2 | `NULL` | 2727337.9981 | 100 | 1 | Year Month |
| 2008 | 1 | `NULL` | 1954580.1658 | 100 | 1 | Year Month |
| 2008 | 2 | `NULL` | 3163370.5217 | 100 | 1 | Year Month |
| 2007 | `NULL` | `NULL` | 4246562.9541 | 110 | 3 | Year |
| 2008 | `NULL` | `NULL` | 5117950.6875 | 110 | 3 | Year |
| `NULL` | `NULL` | `NULL` | 9364513.6416 | 111 | 7 | Grand Total |

#### CUBE example

In this example, the `GROUPING_ID()` function is used to create a value for each row in the `Grouping Level` column to identify the level of grouping.

Unlike `ROLLUP` in the previous example, `CUBE` outputs all grouping levels. If the order of the columns in the `CUBE` list is changed, the level values in the `Grouping Level` column also have to be changed.

```sql
SELECT DATEPART(yyyy, OrderDate) AS N'Year',
    DATEPART(mm, OrderDate) AS N'Month',
    DATEPART(dd, OrderDate) AS N'Day',
    SUM(TotalDue) AS N'Total Due',
    CAST(GROUPING(DATEPART(dd, OrderDate)) AS CHAR(1)) + CAST(GROUPING(DATEPART(mm, OrderDate)) AS CHAR(1)) + CAST(GROUPING(DATEPART(yyyy, OrderDate)) AS CHAR(1)) AS N'Bit Vector(base-2)',
    GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) AS N'Integer Equivalent',
    CASE 
        WHEN GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) = 0
            THEN N'Year Month Day'
        WHEN GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) = 1
            THEN N'Year Month'
        WHEN GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) = 2
            THEN N'Year Day'
        WHEN GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) = 3
            THEN N'Year'
        WHEN GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) = 4
            THEN N'Month Day'
        WHEN GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) = 5
            THEN N'Month'
        WHEN GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) = 6
            THEN N'Day'
        WHEN GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)) = 7
            THEN N'Grand Total'
        ELSE N'Error'
    END AS N'Grouping Level'
FROM Sales.SalesOrderHeader
WHERE DATEPART(yyyy, OrderDate) IN (N'2007', N'2008')
    AND DATEPART(mm, OrderDate) IN (1, 2)
    AND DATEPART(dd, OrderDate) IN (1, 2)
GROUP BY CUBE(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate))
ORDER BY GROUPING_ID(DATEPART(yyyy, OrderDate), DATEPART(mm, OrderDate), DATEPART(dd, OrderDate)),
    DATEPART(yyyy, OrderDate),
    DATEPART(mm, OrderDate),
    DATEPART(dd, OrderDate);
```

Here's a partial result set.

| Year | Month | Day | Total Due | Bit Vector (base-2) | Integer Equivalent | Grouping Level |
| --- | --- | --- | --- | --- | --- | --- |
| 2007 | 1 | 1 | 1497452.6066 | 000 | 0 | Year Month Day |
| 2007 | 1 | 2 | 21772.3494 | 000 | 0 | Year Month Day |
| 2007 | 2 | 1 | 2705653.5913 | 000 | 0 | Year Month Day |
| 2007 | 2 | 2 | 21684.4068 | 000 | 0 | Year Month Day |
| 2008 | 1 | 1 | 1908122.0967 | 000 | 0 | Year Month Day |
| 2008 | 1 | 2 | 46458.0691 | 000 | 0 | Year Month Day |
| 2008 | 2 | 1 | 3108771.9729 | 000 | 0 | Year Month Day |
| 2008 | 2 | 2 | 54598.5488 | 000 | 0 | Year Month Day |
| 2007 | 1 | `NULL` | 1519224.956 | 100 | 1 | Year Month |
| 2007 | 2 | `NULL` | 2727337.9981 | 100 | 1 | Year Month |
| 2008 | 1 | `NULL` | 1954580.1658 | 100 | 1 | Year Month |
| 2008 | 2 | `NULL` | 3163370.5217 | 100 | 1 | Year Month |
| 2007 | `NULL` | 1 | 4203106.1979 | 010 | 2 | Year Day |
| 2007 | `NULL` | 2 | 43456.7562 | 010 | 2 | Year Day |
| 2008 | `NULL` | 1 | 5016894.0696 | 010 | 2 | Year Day |
| 2008 | `NULL` | 2 | 101056.6179 | 010 | 2 | Year Day |
| 2007 | `NULL` | `NULL` | 4246562.9541 | 110 | 3 | Year |
| 2008 | `NULL` | `NULL` | 5117950.6875 | 110 | 3 | Year |
| `NULL` | 1 | 1 | 3405574.7033 | 001 | 4 | Month Day |
| `NULL` | 1 | 2 | 68230.4185 | 001 | 4 | Month Day |
| `NULL` | 2 | 1 | 5814425.5642 | 001 | 4 | Month Day |
| `NULL` | 2 | 2 | 76282.9556 | 001 | 4 | Month Day |
| `NULL` | 1 | `NULL` | 3473805.1218 | 101 | 5 | Month |
| `NULL` | 2 | `NULL` | 5890708.5198 | 101 | 5 | Month |
| `NULL` | `NULL` | 1 | 9220000.2675 | 011 | 6 | Day |
| `NULL` | `NULL` | 2 | 144513.3741 | 011 | 6 | Day |
| `NULL` | `NULL` | `NULL` | 9364513.6416 | 111 | 7 | Grand Total |

## Related content

- [GROUPING (Transact-SQL)](grouping-transact-sql.md)
- [SELECT - GROUP BY (Transact-SQL)](../queries/select-group-by-transact-sql.md)
