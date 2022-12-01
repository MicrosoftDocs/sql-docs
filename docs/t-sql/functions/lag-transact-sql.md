---
title: "LAG (Transact-SQL)"
description: "LAG (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "11/09/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "LAG_TSQL"
  - "LAG"
helpviewer_keywords:
  - "LAG function"
  - "analytic functions, LAG"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# LAG (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Accesses data from a previous row in the same result set without the use of a self-join starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]. LAG provides access to a row at a given physical offset that comes before the current row. Use this analytic function in a SELECT statement to compare values in the current row with values in a previous row.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
LAG (scalar_expression [,offset] [,default])  
    OVER ( [ partition_by_clause ] order_by_clause )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *scalar_expression*  
 The value to be returned based on the specified offset. It is an expression of any type that returns a single (scalar) value. *scalar_expression* cannot be an analytic function.  
  
 *offset*  
 The number of rows back from the current row from which to obtain a value. If not specified, the default is 1. *offset* can be a column, subquery, or other expression that evaluates to a positive integer or can be implicitly converted to **bigint**. *offset* cannot be a negative value or an analytic function.  
  
 *default*  
 The value to return when *offset* is beyond the scope of the partition. If a default value is not specified, NULL is returned. *default* can be a column, subquery, or other expression, but it cannot be an analytic function. *default* must be type-compatible with *scalar_expression*.  
  
 OVER **(** [ _partition\_by\_clause_ ] _order\_by\_clause_**)**  
 *partition_by_clause* divides the result set produced by the FROM clause into partitions to which the function is applied. If not specified, the function treats all rows of the query result set as a single group. *order_by_clause* determines the order of the data before the function is applied. If *partition_by_clause* is specified, it determines the order of the data in the partition. The *order_by_clause* is required. For more information, see [OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md).  
  
## Return Types  
 The data type of the specified *scalar_expression*. NULL is returned if *scalar_expression* is nullable or *default* is set to NULL.  
  
## General Remarks  
 LAG is nondeterministic. For more information, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).  
  
## Examples  
  
### A. Compare values between years  
 The following example uses the LAG function to return the difference in sales quotas for a specific employee over previous years. Notice that because there is no lag value available for the first row, the default of zero (0) is returned.  
  
```sql   
USE AdventureWorks2012;  
GO  
SELECT BusinessEntityID, YEAR(QuotaDate) AS SalesYear, SalesQuota AS CurrentQuota,   
       LAG(SalesQuota, 1,0) OVER (ORDER BY YEAR(QuotaDate)) AS PreviousQuota  
FROM Sales.SalesPersonQuotaHistory  
WHERE BusinessEntityID = 275 AND YEAR(QuotaDate) IN ('2005','2006');  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```    
BusinessEntityID SalesYear   CurrentQuota          PreviousQuota  
---------------- ----------- --------------------- ---------------------  
275              2005        367000.00             0.00  
275              2005        556000.00             367000.00  
275              2006        502000.00             556000.00  
275              2006        550000.00             502000.00  
275              2006        1429000.00            550000.00  
275              2006        1324000.00            1429000.00  
  
```  
  
### B. Compare values within partitions  
 The following example uses the LAG function to compare year-to-date sales between employees. The PARTITION BY clause is specified to divide the rows in the result set by sales territory. The LAG function is applied to each partition separately and computation restarts for each partition. The ORDER BY clause in the OVER clause orders the rows in each partition. The ORDER BY clause in the SELECT statement sorts the rows in the whole result set. Notice that because there is no lag value available for the first row of each partition, the default of zero (0) is returned.  
  
```sql   
USE AdventureWorks2012;  
GO  
SELECT TerritoryName, BusinessEntityID, SalesYTD,   
       LAG (SalesYTD, 1, 0) OVER (PARTITION BY TerritoryName ORDER BY SalesYTD DESC) AS PrevRepSales  
FROM Sales.vSalesPerson  
WHERE TerritoryName IN (N'Northwest', N'Canada')   
ORDER BY TerritoryName;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```    
TerritoryName            BusinessEntityID SalesYTD              PrevRepSales  
-----------------------  ---------------- --------------------- ---------------------  
Canada                   282              2604540.7172          0.00  
Canada                   278              1453719.4653          2604540.7172  
Northwest                284              1576562.1966          0.00  
Northwest                283              1573012.9383          1576562.1966  
Northwest                280              1352577.1325          1573012.9383  
  
```  
  
### C. Specifying arbitrary expressions  
 The following example demonstrates specifying a variety of arbitrary expressions in the LAG function syntax.  
  
```sql   
CREATE TABLE T (a INT, b INT, c INT);   
GO  
INSERT INTO T VALUES (1, 1, -3), (2, 2, 4), (3, 1, NULL), (4, 3, 1), (5, 2, NULL), (6, 1, 5);   
  
SELECT b, c,   
    LAG(2*c, b*(SELECT MIN(b) FROM T), -c/2.0) OVER (ORDER BY a) AS i  
FROM T;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
b           c           i  
----------- ----------- -----------  
1           -3          1  
2           4           -2  
1           NULL        8  
3           1           -6  
2           NULL        NULL  
1           5           NULL  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### D: Compare values between quarters  
 The following example demonstrates the LAG function. The query uses the LAG function to return the difference in sales quotas for a specific employee over previous calendar quarters. Notice that because there is no lag value available for the first row, the default of zero (0) is returned.  
  
```sql   
-- Uses AdventureWorks  
  
SELECT CalendarYear, CalendarQuarter, SalesAmountQuota AS SalesQuota,  
       LAG(SalesAmountQuota,1,0) OVER (ORDER BY CalendarYear, CalendarQuarter) AS PrevQuota,  
       SalesAmountQuota - LAG(SalesAmountQuota,1,0) OVER (ORDER BY CalendarYear, CalendarQuarter) AS Diff  
FROM dbo.FactSalesQuota  
WHERE EmployeeKey = 272 AND CalendarYear IN (2001, 2002)  
ORDER BY CalendarYear, CalendarQuarter;   
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
Year Quarter  SalesQuota  PrevQuota  Diff  
---- -------  ----------  ---------  -------------  
2001 3        28000.0000      0.0000   28000.0000  
2001 4         7000.0000  28000.0000  -21000.0000  
2001 1        91000.0000   7000.0000   84000.0000  
2002 2       140000.0000  91000.0000   49000.0000  
2002 3         7000.0000 140000.0000  -70000.0000  
2002 4       154000.0000   7000.0000   84000.0000
```  
  
## See Also  
 [LEAD &#40;Transact-SQL&#41;](../../t-sql/functions/lead-transact-sql.md)  
  
  


