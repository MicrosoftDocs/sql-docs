---
title: "HAVING (Transact-SQL)"
description: "SELECT - HAVING (Transact-SQL)"
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
  - "HAVING"
  - "HAVING_TSQL"
helpviewer_keywords:
  - "restricting conditions for result set"
  - "search conditions [SQL Server], HAVING clause"
  - "HAVING clause"
  - "HAVING clause, about HAVING clause"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# SELECT - HAVING clause (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Specifies a search condition for a group or an aggregate. You can use `HAVING` only with the `SELECT` statement. Typically, you use `HAVING` with a `GROUP BY` clause. When you don't use `GROUP BY`, there's an implicit single, aggregated group.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
[ HAVING <search condition> ]
```

## Arguments

#### \<search_condition>

Specifies one or more predicates for groups and aggregates that the groups need to meet. For more information about search conditions and predicates, see [Search condition](search-condition-transact-sql.md).

You can't use the **text**, **image**, and **ntext** data types in a `HAVING` clause.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Retrieve total for each sales order

The following example uses a `HAVING` clause to retrieve the total for each `SalesOrderID` from the `SalesOrderDetail` table that exceeds `$100000.00`.

```sql
USE AdventureWorks2025;
GO

SELECT SalesOrderID,
       SUM(LineTotal) AS SubTotal
FROM Sales.SalesOrderDetail
GROUP BY SalesOrderID
HAVING SUM(LineTotal) > 100000.00
ORDER BY SalesOrderID;
```

## Examples: Azure Synapse Analytics and Analytics Platform System (PDW)

### B. Retrieve total sales exceeding a given value

The following example uses a `HAVING` clause to retrieve the total `SalesAmount` that exceeds `80000` for each `OrderDateKey` from the `FactInternetSales` table.

```sql
-- Uses AdventureWorks
SELECT OrderDateKey,
       SUM(SalesAmount) AS TotalSales
FROM FactInternetSales
GROUP BY OrderDateKey
HAVING SUM(SalesAmount) > 80000
ORDER BY OrderDateKey;
```

## Related content

- [SELECT - GROUP BY clause (Transact-SQL)](select-group-by-transact-sql.md)
- [WHERE (Transact-SQL)](where-transact-sql.md)
