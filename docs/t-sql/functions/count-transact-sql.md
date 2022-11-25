---
title: "COUNT (Transact-SQL)"
description: "COUNT (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/21/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "COUNT_TSQL"
  - "COUNT"
helpviewer_keywords:
  - "totals [SQL Server], COUNT function"
  - "totals [SQL Server]"
  - "counting items in group"
  - "groups [SQL Server], number of items in"
  - "number of group items"
  - "COUNT function [Transact-SQL]"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# COUNT (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function returns the number of items found in a group. `COUNT` operates like the [COUNT_BIG](../../t-sql/functions/count-big-transact-sql.md) function. These functions differ only in the data types of their return values. `COUNT` always returns an **int** data type value. `COUNT_BIG` always returns a **bigint** data type value.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

### Aggregation function syntax

```syntaxsql
COUNT ( { [ [ ALL | DISTINCT ] expression ] | * } )
```

### Analytic function syntax

```syntaxsql
COUNT ( [ ALL ]  { expression | * } ) OVER ( [ <partition_by_clause> ] )
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### ALL

Applies the aggregate function to all values. ALL serves as the default.

#### DISTINCT

Specifies that `COUNT` returns the number of unique nonnull values.

#### *expression*

An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any type, except **image**, **ntext**, or **text**. `COUNT` doesn't support aggregate functions or subqueries in an expression.

#### *

Specifies that `COUNT` should count all rows to determine the total table row count to return. `COUNT(*)` takes no parameters and doesn't support the use of DISTINCT. `COUNT(*)` doesn't require an *expression* parameter because by definition, it doesn't use information about any particular column. `COUNT(*)` returns the number of rows in a specified table, and it preserves duplicate rows. It counts each row separately. This includes rows that contain null values.

#### OVER ( [ *partition_by_clause* ] [ *order_by_clause* ] [ *ROW_or_RANGE_clause* ] )

The *partition_by_clause* divides the result set produced by the `FROM` clause into partitions to which the `COUNT` function is applied. If not specified, the function treats all rows of the query result set as a single group. The *order_by_clause* determines the logical order of the operation. See [OVER clause (Transact-SQL)](../../t-sql/queries/select-over-clause-transact-sql.md) for more information.

## Return types

- **int NOT NULL** when `ANSI_WARNINGS` is `ON`, however SQL Server will always treat `COUNT` expressions as `int NULL` in metadata, unless wrapped in `ISNULL`.

- **int NULL** when `ANSI_WARNINGS` is `OFF`.

## Remarks

- `COUNT(*)` without `GROUP BY` returns the cardinality (number of rows) in the resultset. This includes rows comprised of all-`NULL` values and duplicates.
- `COUNT(*)` with `GROUP BY` returns the number of rows in each group. This includes `NULL` values and duplicates.
- `COUNT(ALL <expression>)` evaluates *expression* for each row in a group, and returns the number of nonnull values.
- `COUNT(DISTINCT *expression*)` evaluates *expression* for each row in a group, and returns the number of unique, nonnull values.

`COUNT` is a deterministic function when used *without* the OVER and ORDER BY clauses. It is nondeterministic when used ***with*** the OVER and ORDER BY clauses. For more information, see [Deterministic and nondeterministic functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).

### `ARITHABORT` and `ANSI_WARNINGS`

When `COUNT` has a return value exceeding the maximum value of **int** (that is, 2<sup>31</sup>-1 or 2,147,483,647), the `COUNT` function will fail due to an integer overflow. When `COUNT` overflows and *both* the `ARITHABORT` and `ANSI_WARNINGS` options are `OFF`, `COUNT` will return `NULL`. Otherwise, when *either* of `ARITHABORT` or `ANSI_WARNINGS` are `ON`, the query will abort and the arithmetic overflow error `Msg 8115, Level 16, State 2; Arithmetic overflow error converting expression to data type int.` will be raised. To correctly handle these large results, use `COUNT_BIG` instead, which returns **bigint**.

When both ``ARITHABORT`` and `ANSI_WARNINGS` are `ON`, you can safely wrap `COUNT` call-sites in `ISNULL( <count-expr>, 0 )` to coerce the expression's type to `int NOT NULL` instead of `int NULL`. Wrapping `COUNT` in `ISNULL` means any overflow error will be silently suppressed, which should be considered for correctness.

## Examples

### A. Use COUNT and DISTINCT

This example returns the number of different titles that an [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)] employee can hold.

```sql
SELECT COUNT(DISTINCT Title)
FROM HumanResources.Employee;
GO
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
-----------
67
  
(1 row(s) affected)
```

### B. Use COUNT(*)

This example returns the total number of [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)] employees.

```sql
SELECT COUNT(*)
FROM HumanResources.Employee;
GO
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
-----------
290
  
(1 row(s) affected)
```

### C. Use COUNT(*) with other aggregates

This example shows that `COUNT(*)` works with other aggregate functions in the `SELECT` list. The example uses the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.

```sql
SELECT COUNT(*), AVG(Bonus)
FROM Sales.SalesPerson
WHERE SalesQuota > 25000;
GO
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
----------- ---------------------
14            3472.1428
  
(1 row(s) affected)
```

### D. Use the OVER clause

This example uses the `MIN`, `MAX`, `AVG` and `COUNT` functions with the `OVER` clause, to return aggregated values for each department in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database `HumanResources.Department` table.

```sql
SELECT DISTINCT Name
    , MIN(Rate) OVER (PARTITION BY edh.DepartmentID) AS MinSalary
    , MAX(Rate) OVER (PARTITION BY edh.DepartmentID) AS MaxSalary
    , AVG(Rate) OVER (PARTITION BY edh.DepartmentID) AS AvgSalary
    , COUNT(edh.BusinessEntityID) OVER (PARTITION BY edh.DepartmentID) AS EmployeesPerDept
FROM HumanResources.EmployeePayHistory AS eph
JOIN HumanResources.EmployeeDepartmentHistory AS edh
    ON eph.BusinessEntityID = edh.BusinessEntityID
JOIN HumanResources.Department AS d
ON d.DepartmentID = edh.DepartmentID
WHERE edh.EndDate IS NULL
ORDER BY Name;
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
Name                          MinSalary             MaxSalary             AvgSalary             EmployeesPerDept
----------------------------- --------------------- --------------------- --------------------- ----------------
Document Control              10.25                 17.7885               14.3884               5
Engineering                   32.6923               63.4615               40.1442               6
Executive                     39.06                 125.50                68.3034               4
Facilities and Maintenance    9.25                  24.0385               13.0316               7
Finance                       13.4615               43.2692               23.935                10
Human Resources               13.9423               27.1394               18.0248               6
Information Services          27.4038               50.4808               34.1586               10
Marketing                     13.4615               37.50                 18.4318               11
Production                    6.50                  84.1346               13.5537               195
Production Control            8.62                  24.5192               16.7746               8
Purchasing                    9.86                  30.00                 18.0202               14
Quality Assurance             10.5769               28.8462               15.4647               6
Research and Development      40.8654               50.4808               43.6731               4
Sales                         23.0769               72.1154               29.9719               18
Shipping and Receiving        9.00                  19.2308               10.8718               6
Tool Design                   8.62                  29.8462               23.5054               6
  
(16 row(s) affected)
```

## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

### E. Use COUNT and DISTINCT

This example returns the number of different titles that an employee of a specific company can hold.

```sql
USE ssawPDW;
  
SELECT COUNT(DISTINCT Title)
FROM dbo.DimEmployee;
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
-----------
67
```

### F. Use COUNT(*)

This example returns the total number of rows in the `dbo.DimEmployee` table.

```sql
USE ssawPDW;
  
SELECT COUNT(*)
FROM dbo.DimEmployee;
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
-------------
296
```

### G. Use COUNT(*) with other aggregates

This example combines `COUNT(*)` with other aggregate functions in the `SELECT` list. It returns the number of sales representatives with an annual sales quota greater than $500,000, and the average sales quota of those sales representatives.

```sql
USE ssawPDW;
  
SELECT COUNT(EmployeeKey) AS TotalCount, AVG(SalesAmountQuota) AS [Average Sales Quota]
FROM dbo.FactSalesQuota
WHERE SalesAmountQuota > 500000 AND CalendarYear = 2001;
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
TotalCount  Average Sales Quota
----------  -------------------
10          683800.0000
```

### H. Use COUNT with HAVING

This example uses `COUNT` with the `HAVING` clause to return the departments of a company, each of which has more than 15 employees.

```sql
USE ssawPDW;
  
SELECT DepartmentName,
    COUNT(EmployeeKey)AS EmployeesInDept
FROM dbo.DimEmployee
GROUP BY DepartmentName
HAVING COUNT(EmployeeKey) > 15;
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
DepartmentName  EmployeesInDept
--------------  ---------------
Sales           18
Production      179
```

### I. Use COUNT with OVER

This example uses `COUNT` with the `OVER` clause, to return the number of products contained in each of the specified sales orders.

```sql
USE ssawPDW;
  
SELECT DISTINCT COUNT(ProductKey) OVER(PARTITION BY SalesOrderNumber) AS ProductCount
    , SalesOrderNumber
FROM dbo.FactInternetSales
WHERE SalesOrderNumber IN (N'SO53115',N'SO55981');
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
ProductCount   SalesOrderID
------------   -----------------
3              SO53115
1              SO55981
```

## See also

- [Aggregate Functions (Transact-SQL)](../../t-sql/functions/aggregate-functions-transact-sql.md)
- [COUNT_BIG (Transact-SQL)](../../t-sql/functions/count-big-transact-sql.md)
- [OVER Clause (Transact-SQL)](../../t-sql/queries/select-over-clause-transact-sql.md)
