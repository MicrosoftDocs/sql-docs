---
title: "COUNT_BIG (Transact-SQL)"
description: "COUNT_BIG (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/19/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "COUNT_BIG_TSQL"
  - "COUNT_BIG"
helpviewer_keywords:
  - "totals [SQL Server], COUNT_BIG function"
  - "counting items in group"
  - "groups [SQL Server], number of items in"
  - "number of group items"
  - "COUNT_BIG function"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# COUNT_BIG (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

This function returns the number of items found in a group. `COUNT_BIG` operates like the [COUNT](count-transact-sql.md) function. These functions differ only in the data types of their return values. `COUNT_BIG` always returns a **bigint** data type value. `COUNT` always returns an **int** data type value.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Aggregation function syntax:

```syntaxsql
COUNT_BIG ( { [ [ ALL | DISTINCT ] expression ] | * } )
```

Analytic function syntax:

```syntaxsql
COUNT_BIG ( { [ ALL ] expression | * } ) OVER ( [ <partition_by_clause> ] )
```

## Arguments

#### ALL

Applies the aggregate function to all values. `ALL` serves as the default.

#### DISTINCT

Specifies that `COUNT_BIG` returns the number of unique non-null values.

#### *expression*

An [expression](../language-elements/expressions-transact-sql.md) of any type. `COUNT_BIG` doesn't support aggregate functions or subqueries in an expression.

#### \*

Specifies that `COUNT_BIG` should count all rows to determine the total table row count to return. `COUNT_BIG(*)` takes no parameters and doesn't support the use of `DISTINCT`. `COUNT_BIG(*)` doesn't require an *expression* parameter because by definition, it doesn't use information about any particular column. `COUNT_BIG(*)` returns the number of rows in a specified table, and it preserves duplicate rows. It counts each row separately, including rows that contain null values.

#### OVER ( [ *partition_by_clause* ] [ *order_by_clause* ] )

The *partition_by_clause* divides the result set produced by the `FROM` clause into partitions to which the `COUNT_BIG` function is applied. If you don't specify the *partition_by_clause*, the function treats all rows of the query result set as a single group. The *order_by_clause* determines the logical order of the operation. For more information, see [OVER clause](../queries/select-over-clause-transact-sql.md).

## Return types

**bigint**

## Remarks

`COUNT_BIG(*)` returns the number of items in a group. This count includes `NULL` values and duplicates.

`COUNT_BIG(ALL <expression>)` evaluates *expression* for each row in a group, and returns the number of non-null values.

`COUNT_BIG(DISTINCT <expression>)` evaluates *expression* for each row in a group, and returns the number of unique, non-null values.

### Deterministic and nondeterministic usage

`COUNT_BIG` is a *deterministic* function when used *without* the `OVER` and `ORDER BY` clauses.

`COUNT_BIG` is *nondeterministic* when used *with* the `OVER` and `ORDER BY` clauses.

| Uses `OVER` and `ORDER BY` clauses | Deterministic |
| --- | --- |
| No | Yes |
| Yes | No |

For more information, see [Deterministic and nondeterministic functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Use COUNT_BIG and DISTINCT

This example returns the number of different job titles in the `HumanResources.Employee` table that an employee can hold.

```sql
SELECT COUNT_BIG(DISTINCT JobTitle)
FROM HumanResources.Employee;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
-----------
67
```

### B. Use COUNT_BIG(*)

This example returns the total number of employees in the `HumanResources.Employee` table.

```sql
SELECT COUNT_BIG(*)
FROM HumanResources.Employee;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
-----------
290
```

### C. Use COUNT_BIG(*) with other aggregates

This example shows that `COUNT_BIG(*)` works with other aggregate functions in the `SELECT` list.

```sql
SELECT COUNT_BIG(*), AVG(Bonus)
FROM Sales.SalesPerson
WHERE SalesQuota > 25000;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
------ ---------------------
14     3472.1428
```

### D. Use the OVER clause

This example uses the `MIN`, `MAX`, `AVG`, and `COUNT_BIG` functions with the `OVER` clause to return aggregated values for each department in the `HumanResources.Department` table.

```sql
SELECT DISTINCT d.Name,
                MIN(eph.Rate) OVER (PARTITION BY edh.DepartmentID) AS MinSalary,
                MAX(eph.Rate) OVER (PARTITION BY edh.DepartmentID) AS MaxSalary,
                AVG(eph.Rate) OVER (PARTITION BY edh.DepartmentID) AS AvgSalary,
                COUNT_BIG(edh.BusinessEntityID) OVER (PARTITION BY edh.DepartmentID) AS EmployeesPerDept
FROM HumanResources.EmployeePayHistory AS eph
     INNER JOIN HumanResources.EmployeeDepartmentHistory AS edh
         ON eph.BusinessEntityID = edh.BusinessEntityID
     INNER JOIN HumanResources.Department AS d
         ON d.DepartmentID = edh.DepartmentID
WHERE edh.EndDate IS NULL
ORDER BY d.Name;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Name                         MinSalary   MaxSalary   AvgSalary   EmployeesPerDept
---------------------------- ----------- ----------- ----------- -----------------
Document Control             10.25       17.7885     14.3884     5
Engineering                  32.6923     63.4615     40.1442     6
Executive                    39.06       125.50      68.3034     4
Facilities and Maintenance   9.25        24.0385     13.0316     7
Finance                      13.4615     43.2692     23.935      10
Human Resources              13.9423     27.1394     18.0248     6
Information Services         27.4038     50.4808     34.1586     10
Marketing                    13.4615     37.50       18.4318     11
Production                   6.50        84.1346     13.5537     195
Production Control           8.62        24.5192     16.7746     8
Purchasing                   9.86        30.00       18.0202     14
Quality Assurance            10.5769     28.8462     15.4647     6
Research and Development     40.8654     50.4808     43.6731     4
Sales                        23.0769     72.1154     29.9719     18
Shipping and Receiving       9.00        19.2308     10.8718     6
Tool Design                  8.62        29.8462     23.5054     6
```

## Examples: Azure Synapse Analytics and Analytics Platform System (PDW)

### E. Use COUNT_BIG and DISTINCT

This example returns the number of different titles that an employee of a specific company can hold.

```sql
USE ssawPDW;
SELECT COUNT_BIG(DISTINCT Title)
FROM dbo.DimEmployee;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
-----------
67
```

### F. Use COUNT_BIG(*)

This example returns the total number of rows in the `dbo.DimEmployee` table.

```sql
USE ssawPDW;
SELECT COUNT_BIG(*)
FROM dbo.DimEmployee;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
-------------
296
```

### G. Use COUNT_BIG(*) with other aggregates

This example combines `COUNT_BIG(*)` with other aggregate functions in the `SELECT` list. It returns the number of sales representatives with an annual sales quota greater than $500,000, and the average sales quota of those sales representatives.

```sql
USE ssawPDW;
SELECT COUNT_BIG(EmployeeKey) AS TotalCount,
       AVG(SalesAmountQuota) AS [Average Sales Quota]
FROM dbo.FactSalesQuota
WHERE SalesAmountQuota > 500000
      AND CalendarYear = 2001;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
TotalCount  Average Sales Quota
----------  -------------------
10          683800.0000
```

### H. Use COUNT_BIG with HAVING

This example uses `COUNT_BIG` with the `HAVING` clause to return the departments of a company, each of which has more than 15 employees.

```sql
USE ssawPDW;
SELECT DepartmentName,
       COUNT_BIG(EmployeeKey) AS EmployeesInDept
FROM dbo.DimEmployee
GROUP BY DepartmentName
HAVING COUNT_BIG(EmployeeKey) > 15;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
DepartmentName  EmployeesInDept
--------------  ---------------
Sales           18
Production      179
```

### I. Use COUNT_BIG with OVER

This example uses `COUNT_BIG` with the `OVER` clause, to return the number of products contained in each of the specified sales orders.

```sql
USE ssawPDW;
SELECT DISTINCT COUNT_BIG(ProductKey) OVER (PARTITION BY SalesOrderNumber) AS ProductCount,
                SalesOrderNumber
FROM dbo.FactInternetSales
WHERE SalesOrderNumber IN (N'SO53115', N'SO55981');
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
ProductCount   SalesOrderID
------------   -----------------
3              SO53115
1              SO55981
```

## Related content

- [Aggregate Functions (Transact-SQL)](aggregate-functions-transact-sql.md)
- [COUNT (Transact-SQL)](count-transact-sql.md)
- [int, bigint, smallint, and tinyint](../data-types/int-bigint-smallint-and-tinyint-transact-sql.md)
