---
title: Using PIVOT and UNPIVOT
description: Learn about the Transact-SQL PIVOT and UNPIVOT relational operators. Use these operators on SELECT statements to change a table-valued expression into another table.
author: VanMSFT
ms.author: vanto
ms.reviewer: wiassaf, randolphwest
ms.date: 10/01/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "PIVOT_TSQL"
helpviewer_keywords:
  - "FROM clause, UNPIVOT operator"
  - "unpivoting tables"
  - "table pivoting [SQL Server]"
  - "UNPIVOT operator"
  - "crosstab query"
  - "PIVOT operator"
  - "rotating table-valued expressions"
  - "pivoting tables"
  - "FROM clause, PIVOT operator"
  - "rotating columns"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# FROM - Using PIVOT and UNPIVOT

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

You can use the `PIVOT` and `UNPIVOT` relational operators to change a table-valued expression into another table. `PIVOT` rotates a table-valued expression by turning the unique values from one column in the expression into multiple columns in the output. `PIVOT` also runs aggregations where they're required on any remaining column values that are wanted in the final output. `UNPIVOT` carries out the opposite operation to `PIVOT`, by rotating columns of a table-valued expression into column values.

The syntax for `PIVOT` is easier and more readable than the syntax that might otherwise be specified in a complex series of `SELECT...CASE` statements. For a complete description of the syntax for `PIVOT`, see [FROM clause](from-transact-sql.md).

> [!NOTE]
> Repeated use of `PIVOT`/`UNPIVOT` within a single T-SQL statement can negatively impact query performance.

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

## Syntax

This section summarizes how to use the `PIVOT` and `UNPIVOT` operator.

Syntax for the `PIVOT` operator.

```syntaxsql
SELECT [ <non-pivoted column> [ AS <column name> ] , ]
    ...
    [ <first pivoted column> [ AS <column name> ] ,
    [ <second pivoted column> [ AS <column name> ] , ]
    ...
    [ <last pivoted column> [ AS <column name> ] ] ]
FROM
    ( <SELECT query that produces the data> )
    AS <alias for the source query>
PIVOT
(
    <aggregation function> ( <column being aggregated> )
FOR <column that contains the values that become column headers>
    IN ( <first pivoted column>
         , <second pivoted column>
         , ... <last pivoted column> )
) AS <alias for the pivot table>
[ <optional ORDER BY clause> ]
[ ; ]
```

Syntax for the `UNPIVOT` operator.

```syntaxsql
SELECT [ <non-pivoted column> [ AS <column name> ] , ]
    ...
    [ <output column for names of the pivot columns> [ AS <column name> ] , ]
    [ <new output column created for values in result of the source query> [ AS <column name> ] ]
FROM
    ( <SELECT query that produces the data> )
    AS <alias for the source query>
UNPIVOT
(
    <new output column created for values in result of the source query>
FOR <output column for names of the pivot columns>
    IN ( <first pivoted column>
         , <second pivoted column>
         , ... <last pivoted column> )
)
[ <optional ORDER BY clause> ]
[ ; ]
```

## Remarks

The column identifiers in the `UNPIVOT` clause follow the catalog collation.

- For [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the collation is always `SQL_Latin1_General_CP1_CI_AS`.

- For [!INCLUDE [ssNoVersion_md](../../includes/ssnoversion-md.md)] partially contained databases, the collation is always `Latin1_General_100_CI_AS_KS_WS_SC`.

If the column is combined with other columns, then a collate clause (`COLLATE DATABASE_DEFAULT`) is required to avoid conflicts.

In [!INCLUDE [fabric](../../includes/fabric.md)] and [!INCLUDE [ssazuresynapse_md](../../includes/ssazuresynapse-md.md)] pools, queries with `PIVOT` operator fail if there's a `GROUP BY` on the nonpivot column output by `PIVOT`. As a workaround, remove the nonpivot column from the `GROUP BY`. Query results are the same, as this `GROUP BY` clause is a duplicate.

## Basic PIVOT example

The following code example produces a two-column table that has four rows.

```sql
USE AdventureWorks2022;
GO

SELECT DaysToManufacture, AVG(StandardCost) AS AverageCost
FROM Production.Product
GROUP BY DaysToManufacture;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
DaysToManufacture  AverageCost
------------------ ------------
0                  5.0885
1                  223.88
2                  359.1082
4                  949.4105
```

No products are defined with a value of `3` for `DaysToManufacture`.

The following code displays the same result, pivoted so that the `DaysToManufacture` values become the column headings. A column is provided for three (`[3]`) days, even though the results are `NULL`.

```sql
-- Pivot table with one row and five columns
SELECT 'AverageCost' AS CostSortedByProductionDays,
    [0], [1], [2], [3], [4]
FROM (
    SELECT DaysToManufacture,
        StandardCost
    FROM Production.Product
) AS SourceTable
PIVOT (
    AVG(StandardCost) FOR DaysToManufacture IN
    ([0], [1], [2], [3], [4])
) AS PivotTable;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
CostSortedByProductionDays  0           1           2           3           4
--------------------------- ----------- ----------- ----------- ----------- -----------
AverageCost                 5.0885      223.88      359.1082    NULL        949.4105
```

## Complex PIVOT example

A common scenario where `PIVOT` can be useful is when you want to generate cross-tabulation reports to give a summary of the data. For example, suppose you want to query the `PurchaseOrderHeader` table in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] sample database to determine the number of purchase orders placed by certain employees. The following query provides this report, ordered by vendor.

```sql
USE AdventureWorks2022;
GO

SELECT VendorID,
    [250] AS Emp1,
    [251] AS Emp2,
    [256] AS Emp3,
    [257] AS Emp4,
    [260] AS Emp5
FROM
(
    SELECT PurchaseOrderID,
    EmployeeID, VendorID
    FROM Purchasing.PurchaseOrderHeader
) p
PIVOT
(
    COUNT (PurchaseOrderID)
    FOR EmployeeID IN ([250], [251], [256], [257], [260])
) AS pvt
ORDER BY pvt.VendorID;
```

Here's a partial result set.

```output
VendorID    Emp1        Emp2        Emp3        Emp4        Emp5
----------- ----------- ----------- ----------- ----------- -----------
1492        2           5           4           4           4
1494        2           5           4           5           4
1496        2           4           4           5           5
1498        2           5           4           4           4
1500        3           4           4           5           4
```

The results returned by this subselect statement are pivoted on the `EmployeeID` column.

```sql
SELECT PurchaseOrderID,
    EmployeeID,
    VendorID
FROM PurchaseOrderHeader;
```

The unique values returned by the `EmployeeID` column become fields in the final result set. As such, there's a column for each `EmployeeID` number specified in the pivot clause, which are employees `250`, `251`, `256`, `257`, and `260` in this example. The `PurchaseOrderID` column serves as the value column, against which the columns returned in the final output, which are called the grouping columns, are grouped. In this case, the grouping columns are aggregated by the `COUNT` function. A warning message appears that indicates that any null values appearing in the `PurchaseOrderID` column weren't considered when computing the `COUNT` for each employee.

> [!IMPORTANT]  
> When aggregate functions are used with `PIVOT`, the presence of any null values in the value column aren't considered when computing an aggregation.

## UNPIVOT example

`UNPIVOT` carries out almost the reverse operation of `PIVOT`, by rotating columns into rows. Suppose the table produced in the previous example is stored in the database as `pvt`, and you want to rotate the column identifiers `Emp1`, `Emp2`, `Emp3`, `Emp4`, and `Emp5` into row values that correspond to a particular vendor. As such, you must identify two extra columns.

The column that contains the column values that you're rotating (`Emp1`, `Emp2`, and so on) is called `Employee`, and the column that holds the values that currently exist under the columns being rotated, is called `Orders`. These columns correspond to the *pivot_column* and *value_column*, respectively, in the [!INCLUDE [tsql](../../includes/tsql-md.md)] definition. Here's the query.

```sql
-- Create the table and insert values as portrayed in the previous example.
CREATE TABLE pvt (
    VendorID INT,
    Emp1 INT,
    Emp2 INT,
    Emp3 INT,
    Emp4 INT,
    Emp5 INT);
GO

INSERT INTO pvt
VALUES (1, 4, 3, 5, 4, 4);

INSERT INTO pvt
VALUES (2, 4, 1, 5, 5, 5);

INSERT INTO pvt
VALUES (3, 4, 3, 5, 4, 4);

INSERT INTO pvt
VALUES (4, 4, 2, 5, 5, 4);

INSERT INTO pvt
VALUES (5, 5, 1, 5, 5, 5);
GO

-- Unpivot the table.
SELECT VendorID, Employee, Orders
FROM (
    SELECT VendorID, Emp1, Emp2, Emp3, Emp4, Emp5
    FROM pvt
) p
UNPIVOT
(
    Orders FOR Employee IN (Emp1, Emp2, Emp3, Emp4, Emp5)
) AS unpvt;
GO
```

Here's a partial result set.

```output
VendorID    Employee    Orders
----------- ----------- ------
1            Emp1       4
1            Emp2       3
1            Emp3       5
1            Emp4       4
1            Emp5       4
2            Emp1       4
2            Emp2       1
2            Emp3       5
2            Emp4       5
2            Emp5       5
```

`UNPIVOT` isn't the exact reverse of `PIVOT`. `PIVOT` carries out an aggregation and merges possible multiple rows into a single row in the output. `UNPIVOT` doesn't reproduce the original table-valued expression result, because rows have been merged. Also, `NULL` values in the input of `UNPIVOT` disappear in the output. When the values disappear, it shows that there might have been original `NULL` values in the input before the `PIVOT` operation.

The `Sales.vSalesPersonSalesByFiscalYears` view in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database uses `PIVOT` to return the total sales for each salesperson, for each fiscal year. To script the view in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], in **Object Explorer**, locate the view under the **Views** folder for the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. Right-click the view name, and then select **Script View as**.

## Related content

- [FROM clause (Transact-SQL)](from-transact-sql.md)
- [CASE (Transact-SQL)](../language-elements/case-transact-sql.md)
