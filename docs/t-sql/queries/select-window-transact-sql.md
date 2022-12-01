---
description: "SELECT - WINDOW - Transact-SQL"
title: "WINDOW (Transact-SQL) | Microsoft Docs"
ms.custom:
- event-tier1-build-2022
ms.date: "05/24/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "WINDOW"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "WINDOW clause, about WINDOW clause"
  - "dividing tables into groups"
  - "WINDOW SETS"
  - "WINDOWS clause"
  - "table groups [SQL Server]"
  - "groups [SQL Server], tables divided into groups"
  - "summary values [SQL Server]"
author: thesqlsith 
ms.author: derekw
ms.reviewer: mikeray
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16"
---

# SELECT - WINDOW - (Transact-SQL)

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

Named window definition in the WINDOW clause determines the partitioning and ordering of a rowset before the window function which uses the window in OVER clause is applied.

> [!NOTE]
> The WINDOW clause requires database compatibility level 160 or higher. If your database compatibility level is lower than 160, SQL Server cannot execute queries with the WINDOW clause.
>
>You can check compatibility level in the sys.databases view or in database properties. You can change the compatibility level of a database with the following command:
>
>```sql
>ALTER DATABASE DatabaseName SET COMPATIBILITY_LEVEL = 160
>```

[Transact-SQL Syntax Conventions (Transact-SQL)](../language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
WINDOW window_name AS (
       [ reference_window_name ]   
       [ <PARTITION BY clause> ]  
       [ <ORDER BY clause> ]   
       [ <ROW or RANGE clause> ]  
      )  
  
<PARTITION BY clause> ::=  
PARTITION BY value_expression , ... [ n ]  
  
<ORDER BY clause> ::=  
ORDER BY order_by_expression  
    [ COLLATE collation_name ]   
    [ ASC | DESC ]   
    [ ,...n ]  
  
<ROW or RANGE clause> ::=  
{ ROWS | RANGE } <window frame extent>

```

## Arguments

#### window_name

Name of the defined window specification. This name will be used by the window functions in the OVER clause to refer the window specification. Window names must follow the rules for identifiers.

#### reference_window_name

Name of the window being referenced by the current window. The referenced window must be among the windows defined in the WINDOW clause.

The other arguments are:

- [PARTITION BY](select-over-clause-transact-sql.md#partition-by) that divides the query result set into partitions.

- [ORDER BY](select-over-clause-transact-sql.md#order-by) that defines the logical order of the rows within each partition of the result set.

-   [ROWS/RANGE](select-over-clause-transact-sql.md#rows-or-range) that limits the rows within the partition by specifying start and end points within the partition.

For more specific details about the arguments, see the [OVER Clause](select-over-clause-transact-sql.md)


## General remarks

More than one named window can be defined in the WINDOW clause.

Additional components can be added to a named window in the OVER clause by using the window_name followed by the additional specifications. However, the properties specified in WINDOW clause cannot be redefined in the OVER clause.

When a query uses multiple windows, one named window can reference another named window using the window_name. In this case, the referenced window_name must be specified in the window definition of the referencing window. A window component defined in one window cannot be redefined by another window referencing it.

Based on the order in which the windows are defined in the window clause, forward and backward window references are permitted. In other words, a window may use any other window defined in the `<window_expression>` that it is part of, as `reference_window_name`, irrespective of the order of which they are defined. Cyclic references and using multiple window references in a single window are not allowed.

The scope of the new window_name of a defined window contained in a `<window_expression>` consists of any window definitions that are part of the `<window_expression>`, together with the SELECT clause of the `<query_specification>` or `<SELECT statement>` that contains the window clause. If the `<window_expression>` is contained in a `<query_specification>` that is part of `<query_expression>` which is a simple table query, then the scope of the new window_name also includes the `<order_by_expression>`, if any, of that `<query_expression>`.

The restrictions for usage of window specifications in the OVER clause with the aggregate and analytic functions based on their semantics are applicable to WINDOW clause.

## Examples

### A. Specifying a window defined in the window clause

The following example query shows uses a named window in the OVER clause.

```sql
ALTER DATABASE AdventureWorks2012 SET Compatibility_level = 160;
GO

USE AdventureWorks2012;
GO

SELECT ROW_NUMBER() OVER win AS "Row Number",
    p.LastName, s.SalesYTD, a.PostalCode
FROM Sales.SalesPerson AS s
    INNER JOIN Person.Person AS p
        ON s.BusinessEntityID = p.BusinessEntityID
    INNER JOIN Person.Address AS a
        ON a.AddressID = p.BusinessEntityID
WHERE TerritoryID IS NOT NULL
    AND SalesYTD <> 0
WINDOW win AS (PARTITION BY PostalCode ORDER BY SalesYTD DESC)
ORDER BY PostalCode;
GO
```

The following query is the equivalent of the above query without using the WINDOW clause.

```sql
USE AdventureWorks2012;
GO

SELECT ROW_NUMBER() OVER (PARTITION BY PostalCode ORDER BY SalesYTD DESC) AS "Row Number",
p.LastName, s.SalesYTD, a.PostalCode
FROM Sales.SalesPerson AS s
    INNER JOIN Person.Person AS p
        ON s.BusinessEntityID = p.BusinessEntityID
    INNER JOIN Person.Address AS a
        ON a.AddressID = p.BusinessEntityID
WHERE TerritoryID IS NOT NULL
    AND SalesYTD <> 0
ORDER BY PostalCode;
GO
```

Here is the result set.

|**Row Number**|**LastName**|**SalesYTD**|**PostalCode**|
| :-- | :-- | :-- | :-- |  
| 1 | Mitchell | 4251368.5497 | 98027|
| 2 | Blythe | 3763178.1787 | 98027 |      
| 3 | Carson | 3189418.3662 | 98027 |
| 4 | Reiter | 2315185.611 | 98027 |
| 5 | Vargas | 1453719.4653 | 98027 |
| 6 | Ansman-Wolfe | 1352577.1325 | 98027 |
| 1 | Pak | 4116871.2277 | 98055 |
| 2 | Varkey Chudukatil | 3121616.3202 | 98055 |
| 3 | Saraiva | 2604540.7172 | 98055 |
| 4 | Ito | 2458535.6169 | 98055 |
| 5 | Valdez | 1827066.7118 | 98055 |
| 6 | Mensa-Annan | 1576562.1966 | 98055 |
| 7 | Campbell | 1573012.9383 | 98055 |
| 8 | Tsoflias | 1421810.9242 | 98055 |

### B. Specifying a single window in multiple over clauses

The following example shows defining a window specification and using it multiple times in an OVER clause.

```sql
ALTER DATABASE AdventureWorks2012 SET Compatibility_level = 160;
GO

USE AdventureWorks2012;
GO

SELECT SalesOrderID, ProductID, OrderQty
    ,SUM(OrderQty) OVER win AS Total
    ,AVG(OrderQty) OVER win AS "Avg"
    ,COUNT(OrderQty) OVER win AS "Count"
    ,MIN(OrderQty) OVER win AS "Min"
    ,MAX(OrderQty) OVER win AS "Max"
FROM Sales.SalesOrderDetail
WHERE SalesOrderID IN(43659,43664)
WINDOW win AS (PARTITION BY SalesOrderID);
GO
```

The following query is the equivalent of the above query without using the WINDOW clause.

```sql
USE AdventureWorks2012;
GO
SELECT SalesOrderID, ProductID, OrderQty
    ,SUM(OrderQty) OVER (PARTITION BY SalesOrderID) AS Total
    ,AVG(OrderQty) OVER (PARTITION BY SalesOrderID) AS "Avg"
    ,COUNT(OrderQty) OVER (PARTITION BY SalesOrderID) AS "Count"
    ,MIN(OrderQty) OVER (PARTITION BY SalesOrderID) AS "Min"
    ,MAX(OrderQty) OVER (PARTITION BY SalesOrderID) AS "Max"
    FROM Sales.SalesOrderDetail
WHERE SalesOrderID IN(43659,43664);
GO
```

Here is the result set.

| **SalesOrderID** | **ProductID** | **OrderQty** | **Total** | **Avg** | **Count** | **Min** | **Max** |
| :-- | :-- | :-- | :-- | :-- | :-- | :-- | :-- |
| 43659 | 776 | 1 | 26 | 2 | 12 | 1 | 6 |
| 43659 | 777 | 3 | 26 | 2 | 12 | 1 | 6 |
| 43659 | 778 | 1 | 26 | 2 | 12 | 1 | 6 |
| 43659 | 771 | 1 | 26 | 2 | 12 | 1 | 6 |
| 43659 | 772 | 1 | 26 | 2 | 12 | 1 | 6 |
| 43659 | 773 | 2 | 26 | 2 | 12 | 1 | 6 |
| 43659 | 774 | 1 | 26 | 2 | 12 | 1 | 6 |
| 43659 | 714 | 3 | 26 | 2 | 12 | 1 | 6 |
| 43659 | 716 | 1 | 26 | 2 | 12 | 1 | 6 |
| 43659 | 709 | 6 | 26 | 2 | 12 | 1 | 6 |
| 43659 | 712 | 2 | 26 | 2 | 12 | 1 | 6
| 43659 | 711 | 4 | 26 | 2 | 12 | 1 | 6 |
| 43664 | 772 | 1 | 14 | 1 | 8 | 1 | 4 |
| 43664 | 775 | 4 | 14 | 1 | 8 | 1 | 4 |
| 43664 | 714 | 1 | 14 | 1 | 8 | 1 | 4 |
| 43664 | 716 | 1 | 14 | 1 | 8 | 1 | 4 |
| 43664 | 777 | 2 | 14 | 1 | 8 | 1 | 4 |
| 43664 | 771 | 3 | 14 | 1 | 8 | 1 | 4 |
| 43664 | 773 | 1 | 14 | 1 | 8 | 1 | 4 |
| 43664 | 778 | 1 | 14 | 1 | 8 | 1 | 4 |

### C. Defining common specification in window clause

This example shows defining a common specification in a window and using it to define additional specifications in the OVER clause.

```sql
ALTER DATABASE AdventureWorks2012 SET Compatibility_level = 160;
GO

USE AdventureWorks2012;
GO

SELECT SalesOrderID AS OrderNumber, ProductID,
    OrderQty AS Qty,
    SUM(OrderQty) OVER win AS Total,
    AVG(OrderQty) OVER(win PARTITION BY SalesOrderID) AS Avg,
    COUNT(OrderQty) OVER(win ROWS BETWEEN UNBOUNDED PRECEDING AND 1 FOLLOWING) AS Count
FROM Sales.SalesOrderDetail
WHERE SalesOrderID IN(43659,43664) AND
    ProductID LIKE '71%'
WINDOW win AS (ORDER BY SalesOrderID, ProductID);
GO
```

The following query is the equivalent of the above query without using the WINDOW clause.

```sql
USE AdventureWorks2012;
GO

SELECT SalesOrderID AS OrderNumber, ProductID,
    OrderQty AS Qty,
    SUM(OrderQty) OVER (ORDER BY SalesOrderID, ProductID) AS Total,
    AVG(OrderQty) OVER(PARTITION BY SalesOrderID ORDER BY SalesOrderID, ProductID) AS Avg,
    COUNT(OrderQty) OVER(ORDER BY SalesOrderID, ProductID ROWS BETWEEN UNBOUNDED PRECEDING AND 1 FOLLOWING) AS Count
FROM Sales.SalesOrderDetail
WHERE SalesOrderID IN(43659,43664) AND
    ProductID LIKE '71%';
GO
```

Here is the result set.


| **OrderNumber** | **ProductID** | **OrderQty** | **Total** | **Avg** | **Count** |
| :-- | :-- | :-- | :-- | :-- | :-- | 
| 43659 | 711 | 4 | 4 | 4 | 2 | 
| 43659 | 712 | 2 | 6 | 3 | 3 | 
| 43659 | 714 | 3 | 9 | 3 | 4 |
| 43659 | 716 | 1 | 10 | 2 | 5 |
| 43664 | 714 | 1 | 11 | 1 | 6 |
| 43664 | 716 | 1 | 12 | 1 | 6 |

### D. Forward and backward window references

This example shows using named windows as forward and backward references when defining a new window in the WINDOW clause.

```sql
ALTER DATABASE AdventureWorks2012 SET Compatibility_level = 160;
GO

USE AdventureWorks2012;
GO

SELECT SalesOrderID AS OrderNumber, ProductID,
    OrderQty AS Qty,
    SUM(OrderQty) OVER win2 AS Total,
    AVG(OrderQty) OVER win1 AS Avg
FROM Sales.SalesOrderDetail
WHERE SalesOrderID IN(43659,43664) AND
    ProductID LIKE '71%'
WINDOW win1 AS (win3),
    win2 AS (ORDER BY SalesOrderID, ProductID),
    win3 AS (win2 PARTITION BY SalesOrderID);
GO
```

The following query is the equivalent of the above query without using the WINDOW clause.

```sql
USE AdventureWorks2012;
GO

SELECT SalesOrderID AS OrderNumber, ProductID,
    OrderQty AS Qty,
    SUM(OrderQty) OVER (ORDER BY SalesOrderID, ProductID) AS Total,
    AVG(OrderQty) OVER (PARTITION BY SalesOrderID ORDER BY SalesOrderID, ProductID) AS Avg
FROM Sales.SalesOrderDetail
WHERE SalesOrderID IN(43659,43664) AND
    ProductID LIKE '71%';
GO                                                                                     |
```

Here is the result set.

| **OrderNumber** | **ProductID** | **OrderQty** | **Total** | **Avg** |
| :-- | :-- | :-- | :-- | :-- |
| 43659 | 711 | 4 | 4 | 4 |
| 43659 | 712 | 2 | 6 | 3 |
| 43659 | 714 | 3 | 9 | 3 |
| 43659 | 716 | 1 | 10 | 2 |
| 43664 | 714 | 1 | 11 | 1 |
| 43664 | 716 | 1 | 12 | 1 |

## See also

- [Aggregate Functions (Transact-SQL)](../functions/aggregate-functions-transact-sql.md)
- [Analytic functions (Transact-SQL)](../functions/analytic-functions-transact-sql.md)
- [SELECT - OVER Clause (Transact-SQL)](select-over-clause-transact-sql.md)
- [SELECT &#40;Transact-SQL&#41;](~/t-sql/queries/select-transact-sql.md)
- [SELECT Clause &#40;Transact-SQL&#41;](~/t-sql/queries/select-clause-transact-sql.md)
