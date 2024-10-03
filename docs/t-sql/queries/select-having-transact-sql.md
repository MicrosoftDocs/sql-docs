---
title: "HAVING (Transact-SQL)"
description: "SELECT - HAVING (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "01/21/2020"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
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
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=fabric"
---
# SELECT - HAVING (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

  Specifies a search condition for a group or an aggregate. HAVING can be used only with the SELECT statement. HAVING is typically used with a GROUP BY clause. When GROUP BY is not used, there is an implicit single, aggregated group.   
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
[ HAVING <search condition> ]  
```  
  
## Arguments
\<search_condition>
 Specifies one or more predicates for groups and/or aggregates to meet. For more information about search conditions and predicates, see [Search Condition &#40;Transact-SQL&#41;](../../t-sql/queries/search-condition-transact-sql.md).  
  
 The **text**, **image**, and **ntext** data types cannot be used in a HAVING clause.  
  
## Examples  
 The following example that uses a simple `HAVING` clause retrieves the total for each `SalesOrderID` from the `SalesOrderDetail` table that exceeds `$100000.00`.  
  
```sql
USE AdventureWorks2022;  
GO  
SELECT SalesOrderID, SUM(LineTotal) AS SubTotal  
FROM Sales.SalesOrderDetail  
GROUP BY SalesOrderID  
HAVING SUM(LineTotal) > 100000.00  
ORDER BY SalesOrderID ;  
```  
  
## Examples: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example uses a `HAVING` clause to retrieve the total `SalesAmount` that exceeds `80000` for each `OrderDateKey` from the `FactInternetSales` table.  
  
```sql
-- Uses AdventureWorks  
  
SELECT OrderDateKey, SUM(SalesAmount) AS TotalSales   
FROM FactInternetSales  
GROUP BY OrderDateKey   
HAVING SUM(SalesAmount) > 80000  
ORDER BY OrderDateKey;  
```  
  
## See Also  
 [GROUP BY &#40;Transact-SQL&#41;](../../t-sql/queries/select-group-by-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)  
  
  

