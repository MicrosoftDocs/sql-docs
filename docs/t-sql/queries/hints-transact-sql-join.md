---
description: "Hints (Transact-SQL) - Join"
title: "Join Hints (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "Join Hint"
  - "Join_Hint_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "HASH join hint"
  - "REMOTE join hint"
  - "LOOP join hint"
  - "join hints"
  - "MERGE join hint"
  - "hints [SQL Server], join"
ms.assetid: 09069f4a-f2e3-4717-80e1-c0110058efc4
author: VanMSFT
ms.author: vanto
---
# Hints (Transact-SQL) - Join
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Join hints specify that the query optimizer enforce a join strategy between two tables in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. For general information about joins and join syntax, see [FROM &#40;Transact-SQL&#41;](../../t-sql/queries/from-transact-sql.md).  
  
> [!CAUTION]  
>  Because the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] query optimizer typically selects the best execution plan for a query, we recommend that hints be used only as a last resort by experienced developers and database administrators.
  
#### Applies to
  
- [DELETE](../../t-sql/statements/delete-transact-sql.md)  
  
- [SELECT](../../t-sql/queries/select-transact-sql.md)  
  
- [UPDATE](../../t-sql/queries/update-transact-sql.md)  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
<join_hint> ::=   
     { LOOP | HASH | MERGE | REMOTE }  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

LOOP \| HASH \| MERGE

Specifies that the join in the query should use looping, hashing, or merging. Using LOOP |HASH | MERGE JOIN enforces a particular join between two tables. LOOP cannot be specified together with RIGHT or FULL as a join type. For more information, see [Joins](../../relational-databases/performance/joins.md).

REMOTE

Specifies that the join operation is performed on the site of the right table. This is useful when the left table is a local table and the right table is a remote table. REMOTE should be used only when the left table has fewer rows than the right table.  

If the right table is local, the join is performed locally. If both tables are remote but from different data sources, REMOTE causes the join to be performed on the site of the right table. If both tables are remote tables from the same data source, REMOTE is not required.  

REMOTE cannot be used when one of the values being compared in the join predicate is cast to a different collation using the COLLATE clause.  

REMOTE can be used only for INNER JOIN operations.  
  
## Remarks

Join hints are specified in the FROM clause of a query. Join hints enforce a join strategy between two tables. If a join hint is specified for any two tables, the query optimizer automatically enforces the join order for all joined tables in the query, based on the position of the ON keywords. When a CROSS JOIN is used without the ON clause, parentheses can be used to indicate the join order.  
  
## Examples  
  
### A. Using HASH

The following example specifies that the `JOIN` operation in the query is performed by a `HASH` join. The example uses the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```sql
SELECT p.Name, pr.ProductReviewID  
FROM Production.Product AS p  
LEFT OUTER HASH JOIN Production.ProductReview AS pr  
ON p.ProductID = pr.ProductID  
ORDER BY ProductReviewID DESC;  
```  
  
### B. Using LOOP  
 The following example specifies that the `JOIN` operation in the query is performed by a `LOOP` join. The example uses the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```sql
DELETE FROM Sales.SalesPersonQuotaHistory   
FROM Sales.SalesPersonQuotaHistory AS spqh  
    INNER LOOP JOIN Sales.SalesPerson AS sp  
    ON spqh.SalesPersonID = sp.SalesPersonID  
WHERE sp.SalesYTD > 2500000.00;  
GO  
```  
  
### C. Using MERGE  
 The following example specifies that the `JOIN` operation in the query is performed by a `MERGE` join. The example uses the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```sql
SELECT poh.PurchaseOrderID, poh.OrderDate, pod.ProductID, pod.DueDate, poh.VendorID   
FROM Purchasing.PurchaseOrderHeader AS poh  
INNER MERGE JOIN Purchasing.PurchaseOrderDetail AS pod   
    ON poh.PurchaseOrderID = pod.PurchaseOrderID;  
GO  
```  
  
## See Also  
[Hints &#40;Transact-SQL&#41;](../../t-sql/queries/hints-transact-sql.md)  
  
  
