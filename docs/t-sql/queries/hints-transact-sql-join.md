---
title: "Join hints (Transact-SQL)"
description: Join hints specify that the query optimizer enforce a join strategy between two tables in SQL Server.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 06/07/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
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
---
# Join hints (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

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
     { LOOP | HASH | MERGE | REMOTE }
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### { LOOP | HASH | MERGE }

Specifies that the join in the query should use looping, hashing, or merging. Using `LOOP`, `HASH`, or `MERGE JOIN` enforces a particular join between two tables. `LOOP` can't be specified together with `RIGHT` or `FULL` as a join type. For more information, see [Joins](../../relational-databases/performance/joins.md).

#### REMOTE

Specifies that the join operation is performed on the site of the right table. This is useful when the left table is a local table and the right table is a remote table. `REMOTE` should be used only when the left table has fewer rows than the right table.

If the right table is local, the join is performed locally. If both tables are remote but from different data sources, `REMOTE` causes the join to be performed on the site of the right table. If both tables are remote tables from the same data source, `REMOTE` isn't required.

`REMOTE` can't be used when one of the values being compared in the join predicate is cast to a different collation using the `COLLATE` clause.

`REMOTE` can be used only for `INNER JOIN` operations.

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

## Related content

- [Hints (Transact-SQL)](hints-transact-sql.md)
