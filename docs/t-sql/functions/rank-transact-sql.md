---
title: RANK (Transact-SQL)
description: RANK returns the rank of each row within the partition of a result set.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest, wiassaf
ms.date: 06/13/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "RANK"
  - "RANK_TSQL"
helpviewer_keywords:
  - "row ranking [SQL Server]"
  - "tied rows [SQL Server]"
  - "ranking rows"
  - "RANK function [Transact-SQL]"
dev_langs:
  - TSQL
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# RANK (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Returns the rank of each row within the partition of a result set. The rank of a row is one plus the number of ranks that come before the row in question.

`ROW_NUMBER` and `RANK` are similar. `ROW_NUMBER` numbers all rows sequentially (for example 1, 2, 3, 4, 5). `RANK` provides the same numeric value for ties (for example 1, 2, 2, 4, 5).

> [!NOTE]  
> `RANK` is a temporary value calculated when the query is run. To persist numbers in a table, see [IDENTITY (Property)](../statements/create-table-transact-sql-identity-property.md) and [CREATE SEQUENCE](../statements/create-sequence-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
RANK ( ) OVER ( [ partition_by_clause ] order_by_clause )
```

## Arguments

#### OVER ( [ *partition_by_clause* ] *order_by_clause* )

The *partition_by_clause* divides the result set produced by the `FROM` clause into partitions to which the function is applied. If not specified, the function treats all rows of the query result set as a single group.

The *order_by_clause* determines the order of the data before the function is applied. The *order_by_clause* is required. The `<rows or range clause>` of the `OVER` clause can't be specified for the `RANK` function. For more information, see [SELECT - OVER clause](../queries/select-over-clause-transact-sql.md).

## Return types

**bigint**

## Remarks

If two or more rows tie for a rank, each tied row receives the same rank. For example, if the two top salespeople have the same `SalesYTD` value, they're both ranked one. The salesperson with the next highest `SalesYTD` is ranked number three, because there are two rows that are ranked higher. Therefore, the `RANK` function doesn't always return consecutive integers.

The sort order that is used for the whole query determines the order in which the rows appear in a result set.

`RANK` is nondeterministic. For more information, see [Deterministic and nondeterministic functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).

For window function performance tuning guidance, see [OVER() Performance considerations](../queries/select-over-clause-transact-sql.md#performance-considerations).

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Rank rows within a partition

The following example ranks the products in inventory the specified inventory locations according to their quantities. The result set is partitioned by `LocationID` and logically ordered by `Quantity`. In location 3, products 494 and 495 have the same quantity. Because they're tied, they're both ranked one.

```sql
USE AdventureWorks2025;
GO

SELECT i.ProductID,
       p.Name,
       i.LocationID,
       i.Quantity,
       RANK() OVER (PARTITION BY i.LocationID ORDER BY i.Quantity DESC) AS Rank
FROM Production.ProductInventory AS i
     INNER JOIN Production.Product AS p
         ON i.ProductID = p.ProductID
WHERE i.LocationID BETWEEN 3 AND 4
ORDER BY i.LocationID;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
ProductID   Name                   LocationID   Quantity Rank
----------- ---------------------- ------------ -------- ----
494         Paint - Silver         3            49       1
495         Paint - Blue           3            49       1
493         Paint - Red            3            41       3
496         Paint - Yellow         3            30       4
492         Paint - Black          3            17       5
495         Paint - Blue           4            35       1
496         Paint - Yellow         4            25       2
493         Paint - Red            4            24       3
492         Paint - Black          4            14       4
494         Paint - Silver         4            12       5
```

### B. Rank all rows in a result set

The following example returns the top 10 employees ranked by their salary. Because a `PARTITION BY` clause isn't specified, the `RANK` function is applied to all rows in the result set.

```sql
USE AdventureWorks2025;
GO

SELECT TOP (10) BusinessEntityID,
                Rate,
                RANK() OVER (ORDER BY Rate DESC) AS RankBySalary
FROM HumanResources.EmployeePayHistory AS eph1
WHERE RateChangeDate = (
    SELECT MAX(RateChangeDate)
    FROM HumanResources.EmployeePayHistory AS eph2
    WHERE eph1.BusinessEntityID = eph2.BusinessEntityID
)
ORDER BY BusinessEntityID;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
BusinessEntityID Rate                  RankBySalary
---------------- --------------------- --------------------
1                125.50                1
2                63.4615               4
3                43.2692               11
4                29.8462               28
5                32.6923               22
6                32.6923               22
7                50.4808               6
8                40.8654               14
9                40.8654               14
10               42.4808               13
```

## Examples: Azure Synapse Analytics and Analytics Platform System (PDW)

### C: Ranking rows within a partition

The following example ranks the sales representatives in each sales territory according to their total sales. The rowset is partitioned by `SalesTerritoryGroup` and sorted by `SalesAmountQuota`.

```sql
-- Uses AdventureWorks
SELECT e.LastName,
       st.SalesTerritoryGroup,
       SUM(sq.SalesAmountQuota) AS TotalSales,
       RANK() OVER (PARTITION BY st.SalesTerritoryGroup ORDER BY SUM(sq.SalesAmountQuota) DESC) AS RankResult
FROM dbo.DimEmployee AS e
     INNER JOIN dbo.FactSalesQuota AS sq
         ON e.EmployeeKey = sq.EmployeeKey
     INNER JOIN dbo.DimSalesTerritory AS st
         ON e.SalesTerritoryKey = st.SalesTerritoryKey
WHERE e.SalesPersonFlag = 1
      AND st.SalesTerritoryGroup <> N'NA'
GROUP BY e.LastName, st.SalesTerritoryGroup;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
LastName           SalesTerritoryGroup  TotalSales   RankResult
------------------ -------------------- ------------ -----------
Pak                Europe               10514000.00  1
Varkey Chudukatil  Europe               5557000.00   2
Valdez             Europe               2287000.00   3
Carson             North America        12198000.00  1
Mitchell           North America        11786000.00  2
Blythe             North America        11162000.00  3
Reiter             North America        8541000.00   4
Ito                North America        7804000.00   5
Saraiva            North America        7098000.00   6
Vargas             North America        4365000.00   7
Campbell           North America        4025000.00   8
Ansman-Wolfe       North America        3551000.00   9
Mensa-Annan        North America        2753000.00   10
Tsoflias           Pacific              1687000.00   1
```

## Related content

- [SELECT - OVER clause (Transact-SQL)](../queries/select-over-clause-transact-sql.md)
- [DENSE_RANK (Transact-SQL)](dense-rank-transact-sql.md)
- [ROW_NUMBER (Transact-SQL)](row-number-transact-sql.md)
- [NTILE (Transact-SQL)](ntile-transact-sql.md)
- [Ranking Functions (Transact-SQL)](ranking-functions-transact-sql.md)
