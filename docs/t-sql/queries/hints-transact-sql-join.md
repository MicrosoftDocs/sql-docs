---
title: "Join hints (Transact-SQL)"
description: Join hints specify that the query optimizer enforce a join strategy between two tables in SQL Server.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest, wiassaf
ms.date: 03/20/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "Join Hint"
  - "Join_Hint_TSQL"
helpviewer_keywords:
  - "HASH join hint"
  - "REMOTE join hint"
  - "LOOP join hint"
  - "join hints"
  - "MERGE join hint"
  - "hints [SQL Server], join"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Join hints (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricse-fabricdw-fabricsqldb.md)]

Join hints specify that the query optimizer enforce a join strategy between two tables in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)]. For general information about joins and join syntax, see [FROM clause plus JOIN, APPLY, PIVOT](from-transact-sql.md).

> [!CAUTION]  
> Because the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] query optimizer typically selects the best execution plan for a query, we recommend that hints be used only as a last resort by experienced developers and database administrators.

#### Applies to

- [DELETE (Transact-SQL)](../statements/delete-transact-sql.md)
- [SELECT (Transact-SQL)](select-transact-sql.md)
- [UPDATE (Transact-SQL)](update-transact-sql.md)

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
<join_hint> ::=
     { LOOP | HASH | MERGE | REMOTE | REDUCE | REPLICATE | REDISTRIBUTE [(columns count)]}
```

## Arguments

#### { LOOP | HASH | MERGE }

***Applies to:***  [!INCLUDE [Azure SQL Database](../../includes/ssazure-sqldb.md)], [!INCLUDE [SQL Managed Instance](../../includes/ssazuremi-md.md)], [!INCLUDE [Fabric SQL analytics endpoint](../../includes/fabric-se.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], [!INCLUDE [fabric](../../includes/fabric.md)] [!INCLUDE [Fabric Warehouse](../../includes/fabric-dw.md)]

Specifies that the join in the query should use looping, hashing, or merging. Using `LOOP`, `HASH`, or `MERGE JOIN` enforces a particular join between two tables. `LOOP` can't be specified together with `RIGHT` or `FULL` as a join type. For more information, see [Joins](../../relational-databases/performance/joins.md).

#### REMOTE

***Applies to:*** [!INCLUDE [Azure SQL Database](../../includes/ssazure-sqldb.md)], [!INCLUDE [SQL Managed Instance](../../includes/ssazuremi-md.md)], [!INCLUDE [Fabric SQL analytics endpoint](../../includes/fabric-se.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]


Specifies that the join operation is performed on the site of the right table. This is useful when the left table is a local table and the right table is a remote table. `REMOTE` should be used only when the left table has fewer rows than the right table.

If the right table is local, the join is performed locally. If both tables are remote but from different data sources, `REMOTE` causes the join to be performed on the site of the right table. If both tables are remote tables from the same data source, `REMOTE` isn't required.

`REMOTE` can't be used when one of the values being compared in the join predicate is cast to a different collation using the `COLLATE` clause.

`REMOTE` can be used only for `INNER JOIN` operations.

#### REDUCE

***Applies to:*** [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

Reduces the number of rows to be moved for the table on the right side of the join in order to make two distribution incompatible tables compatible. The REDUCE hint is also called a semi-join hint.

#### REPLICATE

***Applies to:*** [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], [!INCLUDE [fabric](../../includes/fabric.md)] [!INCLUDE [Fabric Warehouse](../../includes/fabric-dw.md)]

Causes a broadcast move operation, where a specific table to be replicated across all distribution nodes.

- Using `REPLICATE` with a `INNER` or `LEFT` join, the broadcast move operation will replicate the right side of the join to all nodes.
- Similarly, while using `REPLICATE` with a `RIGHT` join, the broadcast move operation will replicate the left side of the join to all nodes.
- When using `REPLICATE` with a `FULL` join, an estimated plan cannot be created.

#### REDISTRIBUTE [(columns_count)]

***Applies to:*** [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

Forces two data sources to be distributed on columns specified in the JOIN clause. For a distributed table, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] performs a shuffle move on the first column of both tables For a replicated table, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] performs a trim move. To understand these move types, see the "DMS Query Plan Operations" section in the "Understanding Query Plans" article in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)]. This hint can improve performance when the query plan is using a broadcast move to resolve a distribution incompatible join.

***Applies to:*** [!INCLUDE [fabric](../../includes/fabric.md)] [!INCLUDE [Fabric Warehouse](../../includes/fabric-dw.md)]

The `REDISTRIBUTE` hint ensures two data sources are distributed based on `JOIN` clause columns. It handles multiple join conditions, specified by the first *n* columns in both tables, where *n* is the `column_count` argument. Redistributing data optimizes query performance by evenly spreading data across nodes during intermediate steps of execution.

The `(columns_count)` argument is only supported in [!INCLUDE [fabric](../../includes/fabric.md)] [!INCLUDE [Fabric Warehouse](../../includes/fabric-dw.md)].

 
## Remarks

Join hints are specified in the `FROM` clause of a query. Join hints enforce a join strategy between two tables. If a join hint is specified for any two tables, the query optimizer automatically enforces the join order for all joined tables in the query, based on the position of the `ON` keywords. When a `CROSS JOIN` is used without the `ON` clause, parentheses can be used to indicate the join order.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Use HASH

The following example specifies that the `JOIN` operation in the query is performed by a `HASH` join.

```sql
SELECT p.Name,
    pr.ProductReviewID
FROM Production.Product AS p
LEFT OUTER HASH JOIN Production.ProductReview AS pr
    ON p.ProductID = pr.ProductID
ORDER BY ProductReviewID DESC;
```

### B. Use LOOP

The following example specifies that the `JOIN` operation in the query is performed by a `LOOP` join.

```sql
DELETE
FROM Sales.SalesPersonQuotaHistory
FROM Sales.SalesPersonQuotaHistory AS spqh
INNER LOOP JOIN Sales.SalesPerson AS sp
    ON spqh.SalesPersonID = sp.SalesPersonID
WHERE sp.SalesYTD > 2500000.00;
GO
```

### C. Use MERGE

The following example specifies that the `JOIN` operation in the query is performed by a `MERGE` join.

```sql
SELECT poh.PurchaseOrderID,
    poh.OrderDate,
    pod.ProductID,
    pod.DueDate,
    poh.VendorID
FROM Purchasing.PurchaseOrderHeader AS poh
INNER MERGE JOIN Purchasing.PurchaseOrderDetail AS pod
    ON poh.PurchaseOrderID = pod.PurchaseOrderID;
GO
```

### D. REDUCE join hint example

The following example uses the `REDUCE` join hint to alter the processing of the derived table within the query. When using the `REDUCE` join hint in this query, the `fis.ProductKey` is projected, replicated and made distinct, and then joined to `DimProduct` during the shuffle of `DimProduct` on `ProductKey`. The resulting derived table is distributed on `fis.ProductKey`.

```sql
-- Uses AdventureWorks
  
SELECT SalesOrderNumber
FROM (
    SELECT fis.SalesOrderNumber,
        dp.ProductKey,
        dp.EnglishProductName
    FROM DimProduct AS dp
    INNER REDUCE JOIN FactInternetSales AS fis
        ON dp.ProductKey = fis.ProductKey
    ) AS dTable
ORDER BY SalesOrderNumber;
```

### E. REPLICATE join hint example

This next example shows the same query as the previous example, except that a `REPLICATE` join hint is used instead of the `REDUCE` join hint. Use of the `REPLICATE` hint causes the values in the `ProductKey` (joining) column from the `FactInternetSales` table to be replicated to all nodes. The `DimProduct` table is joined to the replicated version of those values.

```sql
-- Uses AdventureWorks

SELECT SalesOrderNumber
FROM (
    SELECT fis.SalesOrderNumber,
        dp.ProductKey,
        dp.EnglishProductName
    FROM DimProduct AS dp
    INNER REPLICATE JOIN FactInternetSales AS fis
        ON dp.ProductKey = fis.ProductKey
    ) AS dTable
ORDER BY SalesOrderNumber;
```

### F. Use the REDISTRIBUTE hint to guarantee a Shuffle move for a distribution incompatible join

The following query uses the `REDISTRIBUTE` query hint on a distribution incompatible join. This guarantees the query optimizer uses a Shuffle move in the query plan. This also guarantees the query plan won't use a Broadcast move, which moves a distributed table to a replicated table.

In the following example, the `REDISTRIBUTE` hint forces a Shuffle move on the `FactInternetSales` table because `ProductKey` is the distribution column for `DimProduct`, and isn't the distribution column for `FactInternetSales`.

```sql
-- Uses AdventureWorks
  
SELECT dp.ProductKey,
    fis.SalesOrderNumber,
    fis.TotalProductCost
FROM DimProduct AS dp
INNER REDISTRIBUTE JOIN FactInternetSales AS fis
    ON dp.ProductKey = fis.ProductKey;
```

### G. Use the columns count argument with the REDISTRIBUTE hint

The following query uses the `REDISTRIBUTE` query hint with the columns count argument, and the shuffle takes place across the first four columns of each table in the join. 

```sql
SELECT * FROM DA
INNER REDISTRIBUTE (4) JOIN DB
ON DA.a1 = DB.b1
```

## Related content

- [Hints (Transact-SQL)](hints-transact-sql.md)
