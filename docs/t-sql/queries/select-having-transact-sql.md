---
title: "HAVING (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/28/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "HAVING"
  - "HAVING_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "restricting conditions for result set"
  - "search conditions [SQL Server], HAVING clause"
  - "HAVING clause"
  - "HAVING clause, about HAVING clause"
ms.assetid: 55650709-001e-42f4-902f-ead09a3c34af
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SELECT - HAVING (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Specifies a search condition for a group or an aggregate. HAVING can be used only with the SELECT statement. HAVING is typically used with a GROUP BY clause. When GROUP BY is not used, there is an implicit single, aggregated group.   
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
[ HAVING <search condition> ]  
```  
  
## Arguments  
\<search_condition>
 Specifies one or more predicates for groups and/or aggregates to meet. For more information about search conditions and predicates, see [Search Condition &#40;Transact-SQL&#41;](../../t-sql/queries/search-condition-transact-sql.md).  
  
 The **text**, **image**, and **ntext** data types cannot be used in a HAVING clause.  
  
## Examples  
 The following example that uses a simple `HAVING` clause retrieves the total for each `SalesOrderID` from the `SalesOrderDetail` table that exceeds `$100000.00`.  
  
```sql
USE AdventureWorks2012 ;  
GO  
SELECT SalesOrderID, SUM(LineTotal) AS SubTotal  
FROM Sales.SalesOrderDetail  
GROUP BY SalesOrderID  
HAVING SUM(LineTotal) > 100000.00  
ORDER BY SalesOrderID ;  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example uses a `HAVING` clause to retrieve the total for each `SalesAmount` from the `FactInternetSales` table when the `OrderDateKey` is in the year 2004 or later.  
  
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
  
  

