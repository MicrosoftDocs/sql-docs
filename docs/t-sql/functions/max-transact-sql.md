---
title: "MAX (Transact-SQL)"
description: MAX returns the maximum of all values of the specified expression in a group.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/18/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "MAX"
  - "MAX_TSQL"
helpviewer_keywords:
  - "MAX function [Transact-SQL]"
  - "values [SQL Server], maximum"
  - "maximum values [SQL Server]"
dev_langs:
  - TSQL
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# MAX (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Returns the maximum of all values of the specified expression in a group. Can be followed by the [OVER](../queries/select-over-clause-transact-sql.md) clause.

> [!NOTE]  
> To get the maximum of all values of multiple expressions inline, see [Logical functions - GREATEST](logical-functions-greatest-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Aggregation function syntax:

```syntaxsql
MAX ( [ ALL | DISTINCT ] expression )
```

Analytic function syntax:

```syntaxsql
MAX ( [ ALL ] expression) OVER ( [ <partition_by_clause> ] [ <order_by_clause> ] )
```

## Arguments

#### ALL

Applies the aggregate function to all values. `ALL` is the default.

#### DISTINCT

Specifies that each unique value is considered. `DISTINCT` isn't meaningful with `MAX`, and is available for ISO compatibility only.

#### *expression*

A constant, column name, or function, and any combination of arithmetic, bitwise, and string operators. `MAX` can be used with **numeric**, **char**, **nchar**, **varchar**, **nvarchar**, **uniqueidentifier**, or **datetime** columns, but not with **bit** columns. Aggregate functions and subqueries aren't permitted.

For more information, see [Expressions](../language-elements/expressions-transact-sql.md).

#### OVER ( [ *partition_by_clause* ] [ *order_by_clause* ] )

*partition_by_clause* divides the result set produced by the `FROM` clause into partitions to which the function is applied. If not specified, the function treats all rows of the query result set as a single group.

*order_by_clause* determines the logical order in which the operation is performed.

For more information, see [SELECT - OVER clause](../queries/select-over-clause-transact-sql.md).

## Return types

Returns a value same as *expression*.

## Remarks

`MAX` ignores any null values.

`MAX` returns `NULL` when there's no row to select.

For character columns, `MAX` finds the highest value in the collating sequence.

`MAX` is a deterministic function when used without the `OVER` and `ORDER BY` clauses. It's nondeterministic when specified with the `OVER` and `ORDER BY` clauses. For more information, see [Deterministic and nondeterministic functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).

`MAX` operates on a set of rows. To obtain the maximum value for a set of values inline, see [Logical functions - GREATEST](logical-functions-greatest-transact-sql.md).

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Basic example

The following example returns the highest (maximum) tax rate.

```sql
SELECT MAX(TaxRate)
FROM Sales.SalesTaxRate;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
-------------------
19.60
Warning, null value eliminated from aggregate.
```

### B. Use the OVER clause

The following example uses the `MIN`, `MAX`, `AVG`, and `COUNT` functions with the `OVER` clause to provide aggregated values for each department in the `HumanResources.Department` table.

```sql
SELECT DISTINCT Name,
                MIN(Rate) OVER (PARTITION BY edh.DepartmentID) AS MinSalary,
                MAX(Rate) OVER (PARTITION BY edh.DepartmentID) AS MaxSalary,
                AVG(Rate) OVER (PARTITION BY edh.DepartmentID) AS AvgSalary,
                COUNT(edh.BusinessEntityID) OVER (PARTITION BY edh.DepartmentID) AS EmployeesPerDept
FROM HumanResources.EmployeePayHistory AS eph
     INNER JOIN HumanResources.EmployeeDepartmentHistory AS edh
         ON eph.BusinessEntityID = edh.BusinessEntityID
     INNER JOIN HumanResources.Department AS d
         ON d.DepartmentID = edh.DepartmentID
WHERE edh.EndDate IS NULL
ORDER BY Name;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

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
```

### C. Use MAX with character data

The following example returns the database name that sorts using the database name alphabetically. The example uses `WHERE database_id < 5`, to select the system databases.

```sql
SELECT MAX(name)
FROM sys.databases
WHERE database_id < 5;
```

The last system database is `tempdb`.

## Related content

- [Aggregate functions (Transact-SQL)](aggregate-functions-transact-sql.md)
- [Logical functions - GREATEST (Transact-SQL)](logical-functions-greatest-transact-sql.md)
- [SELECT - OVER clause (Transact-SQL)](../queries/select-over-clause-transact-sql.md)
