---
title: "FIRST_VALUE (Transact-SQL)"
description: Returns the first value in an ordered set of values.
author: markingmyname
ms.author: maghan
ms.reviewer: kendalv, randolphwest
ms.date: 01/04/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "FIRST_VALUE_TSQL"
  - "FIRST_VALUE"
helpviewer_keywords:
  - "FIRST_VALUE function"
  - "analytic functions, FIRST_VALUE"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# FIRST_VALUE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-edge-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-edge-fabricse-fabricdw.md)]

Returns the first value in an ordered set of values.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
FIRST_VALUE ( [ scalar_expression ] ) [ IGNORE NULLS | RESPECT NULLS ]
    OVER ( [ partition_by_clause ] order_by_clause [ rows_range_clause ] )
```

## Arguments

#### *scalar_expression*

The value to be returned. *scalar_expression* can be a column, subquery, or other arbitrary expression that results in a single value. Other analytic functions aren't permitted.

#### [ IGNORE NULLS | RESPECT NULLS ]

**Applies to**: SQL Server (starting with [!INCLUDE [sssql22](../../includes/sssql22-md.md)]), Azure SQL Database, Azure SQL Managed Instance, [!INCLUDE [ssazurede-md](../../includes/ssazurede-md.md)]

`IGNORE NULLS` - Ignore null values in the dataset when computing the first value over a partition.

`RESPECT NULLS` - Respect null values in the dataset when computing first value over a partition. `RESPECT NULLS` is the default behavior if a NULLS option isn't specified.

For more information on this argument in [!INCLUDE [ssazurede-md](../../includes/ssazurede-md.md)], see [Imputing missing values](/azure/azure-sql-edge/imputing-missing-values/).

#### OVER ( [ *partition_by_clause* ] *order_by_clause* [ *rows_range_clause* ] )

The *partition_by_clause* divides the result set produced by the FROM clause into partitions to which the function is applied. If not specified, the function treats all rows of the query result set as a single group.

The *order_by_clause* determines the logical order in which the operation is performed. The *order_by_clause* is required.

The *rows_range_clause* further limits the rows within the partition by specifying start and end points.

For more information, see [OVER Clause (Transact-SQL)](../queries/select-over-clause-transact-sql.md).

## Return types

The same type as *scalar_expression*.

## Remarks

`FIRST_VALUE` is nondeterministic. For more information, see [Deterministic and nondeterministic functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).

## Examples

### A. Use FIRST_VALUE over a query result set

The following example uses `FIRST_VALUE` to return the name of the product that is the least expensive in a given product category.

```sql
USE AdventureWorks2022;
GO

SELECT Name,
    ListPrice,
    FIRST_VALUE(Name) OVER (
        ORDER BY ListPrice ASC
    ) AS LeastExpensive
FROM Production.Product
WHERE ProductSubcategoryID = 37;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
Name                    ListPrice             LeastExpensive
----------------------- --------------------- --------------------
Patch Kit/8 Patches     2.29                  Patch Kit/8 Patches
Road Tire Tube          3.99                  Patch Kit/8 Patches
Touring Tire Tube       4.99                  Patch Kit/8 Patches
Mountain Tire Tube      4.99                  Patch Kit/8 Patches
LL Road Tire            21.49                 Patch Kit/8 Patches
ML Road Tire            24.99                 Patch Kit/8 Patches
LL Mountain Tire        24.99                 Patch Kit/8 Patches
Touring Tire            28.99                 Patch Kit/8 Patches
ML Mountain Tire        29.99                 Patch Kit/8 Patches
HL Road Tire            32.60                 Patch Kit/8 Patches
HL Mountain Tire        35.00                 Patch Kit/8 Patches
```

### B. Use FIRST_VALUE over partitions

The following example uses `FIRST_VALUE` to return the employee with the fewest number of vacation hours compared to other employees with the same job title. The `PARTITION BY` clause partitions the employees by job title and the `FIRST_VALUE` function is applied to each partition independently. The `ORDER BY` clause specified in the `OVER` clause determines the logical order in which the `FIRST_VALUE` function is applied to the rows in each partition. The `ROWS UNBOUNDED PRECEDING` clause specifies the starting point of the window is the first row of each partition.

```sql
USE AdventureWorks2022;
GO

SELECT JobTitle,
    LastName,
    VacationHours,
    FIRST_VALUE(LastName) OVER (
        PARTITION BY JobTitle ORDER BY VacationHours ASC ROWS UNBOUNDED PRECEDING
    ) AS FewestVacationHours
FROM HumanResources.Employee AS e
INNER JOIN Person.Person AS p
    ON e.BusinessEntityID = p.BusinessEntityID
ORDER BY JobTitle;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
JobTitle                            LastName                  VacationHours FewestVacationHours
----------------------------------- ------------------------- ------------- -------------------
Accountant                          Moreland                  58            Moreland
Accountant                          Seamans                   59            Moreland
Accounts Manager                    Liu                       57            Liu
Accounts Payable Specialist         Tomic                     63            Tomic
Accounts Payable Specialist         Sheperdigian              64            Tomic
Accounts Receivable Specialist      Poe                       60            Poe
Accounts Receivable Specialist      Spoon                     61            Poe
Accounts Receivable Specialist      Walton                    62            Poe
```

## Related content

- [LAST_VALUE (Transact-SQL)](last-value-transact-sql.md)
- [SELECT - OVER Clause (Transact-SQL)](../queries/select-over-clause-transact-sql.md)
