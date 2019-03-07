---
title: "SUM (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SUM"
  - "SUM_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "values [SQL Server], sum of all"
  - "SUM function [Transact-SQL]"
  - "values [SQL Server]"
  - "summation [SQL Server]"
  - "expressions [SQL Server], sum of all values"
  - "DISTINCT keyword"
  - "totals [SQL Server], SUM"
  - "summary values [SQL Server]"
ms.assetid: 9af94d0f-55d4-428f-a840-ec530160f379
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SUM (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the sum of all the values, or only the DISTINCT values, in the expression. SUM can be used with numeric columns only. Null values are ignored.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Aggregate Function Syntax    
SUM ( [ ALL | DISTINCT ] expression )  

-- Analytic Function Syntax   
SUM ([ ALL ] expression) OVER ( [ partition_by_clause ] order_by_clause)  
```  
  
## Arguments  
 ALL  
 Applies the aggregate function to all values. ALL is the default.  
  
 DISTINCT  
 Specifies that SUM returns the sum of unique values.  
  
 *expression*  
 Is a constant, column, or function, and any combination of arithmetic, bitwise, and string operators. *expression* is an expression of the exact numeric or approximate numeric data type category, except for the **bit** data type. Aggregate functions and subqueries are not permitted. For more information, see [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md).  
  
 OVER **(** [ _partition\_by\_clause_ ] _order\_by\_clause_**)**  
 *partition_by_clause* divides the result set produced by the FROM clause into partitions to which the function is applied. If not specified, the function treats all rows of the query result set as a single group. _order\_by\_clause_ determines the logical order in which the operation is performed. _order\_by\_clause_ is required. For more information, see [OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md).  
  
## Return Types  
 Returns the summation of all *expression* values in the most precise *expression* data type.  
  
|Expression result|Return type|  
|-----------------------|-----------------|  
|**tinyint**|**int**|  
|**smallint**|**int**|  
|**int**|**int**|  
|**bigint**|**bigint**|  
|**decimal** category (p, s)|**decimal(38, s)**|  
|**money** and **smallmoney** category|**money**|  
|**float** and **real** category|**float**|  
  
## Remarks  
 SUM is a deterministic function when used without the OVER and ORDER BY clauses. It is nondeterministic when specified with the OVER and ORDER BY clauses. For more information, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).  
  
## Examples  
  
### A. Using SUM to return summary data  
 The following examples show using the SUM function to return summary data in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```  
SELECT Color, SUM(ListPrice), SUM(StandardCost)  
FROM Production.Product  
WHERE Color IS NOT NULL   
    AND ListPrice != 0.00   
    AND Name LIKE 'Mountain%'  
GROUP BY Color  
ORDER BY Color;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
Color
--------------- --------------------- ---------------------
Black           27404.84              5214.9616
Silver          26462.84              14665.6792
White           19.00                 6.7926

(3 row(s) affected)
 ```  
  
### B. Using the OVER clause  
 The following example uses the SUM function with the OVER clause to provide a cumulative total of yearly sales for each territory in the `Sales.SalesPerson` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. The data is partitioned by `TerritoryID` and logically ordered by `SalesYTD`. This means that the SUM function is computed for each territory based on the sales year. Notice that for `TerritoryID` 1, there are two rows for sales year 2005 representing the two sales people with sales that year. The cumulative sales total for these two rows is computed and then the third row representing sales for the year 2006 is included in the computation.  
  
```  
SELECT BusinessEntityID, TerritoryID   
   ,DATEPART(yy,ModifiedDate) AS SalesYear  
   ,CONVERT(varchar(20),SalesYTD,1) AS  SalesYTD  
   ,CONVERT(varchar(20),AVG(SalesYTD) OVER (PARTITION BY TerritoryID   
                                            ORDER BY DATEPART(yy,ModifiedDate)   
                                           ),1) AS MovingAvg  
   ,CONVERT(varchar(20),SUM(SalesYTD) OVER (PARTITION BY TerritoryID   
                                            ORDER BY DATEPART(yy,ModifiedDate)   
                                            ),1) AS CumulativeTotal  
FROM Sales.SalesPerson  
WHERE TerritoryID IS NULL OR TerritoryID < 5  
ORDER BY TerritoryID,SalesYear;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
  
BusinessEntityID TerritoryID SalesYear   SalesYTD             MovingAvg            CumulativeTotal  
---------------- ----------- ----------- -------------------- -------------------- --------------------  
274              NULL        2005        559,697.56           559,697.56           559,697.56  
287              NULL        2006        519,905.93           539,801.75           1,079,603.50  
285              NULL        2007        172,524.45           417,375.98           1,252,127.95  
283              1           2005        1,573,012.94         1,462,795.04         2,925,590.07  
280              1           2005        1,352,577.13         1,462,795.04         2,925,590.07  
284              1           2006        1,576,562.20         1,500,717.42         4,502,152.27  
275              2           2005        3,763,178.18         3,763,178.18         3,763,178.18  
277              3           2005        3,189,418.37         3,189,418.37         3,189,418.37  
276              4           2005        4,251,368.55         3,354,952.08         6,709,904.17  
281              4           2005        2,458,535.62         3,354,952.08         6,709,904.17  
  
(10 row(s) affected)  
  
```  
  
 In this example, the OVER clause does not include PARTITION BY. This means that the function will be applied to all rows returned by the query. The ORDER BY clause specified in the OVER clause determines the logical order to which the SUM function is applied. The query returns a cumulative total of sales by year for all sales territories specified in the WHERE clause. The ORDER BY clause specified in the SELECT statement determines the order in which the rows of the query are displayed.  
  
```  
SELECT BusinessEntityID, TerritoryID   
   ,DATEPART(yy,ModifiedDate) AS SalesYear  
   ,CONVERT(varchar(20),SalesYTD,1) AS  SalesYTD  
   ,CONVERT(varchar(20),AVG(SalesYTD) OVER (ORDER BY DATEPART(yy,ModifiedDate)   
                                            ),1) AS MovingAvg  
   ,CONVERT(varchar(20),SUM(SalesYTD) OVER (ORDER BY DATEPART(yy,ModifiedDate)   
                                            ),1) AS CumulativeTotal  
FROM Sales.SalesPerson  
WHERE TerritoryID IS NULL OR TerritoryID < 5  
ORDER BY SalesYear;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
BusinessEntityID TerritoryID SalesYear   SalesYTD             MovingAvg            CumulativeTotal  
---------------- ----------- ----------- -------------------- -------------------- --------------------  
274              NULL        2005        559,697.56           2,449,684.05         17,147,788.35  
275              2           2005        3,763,178.18         2,449,684.05         17,147,788.35  
276              4           2005        4,251,368.55         2,449,684.05         17,147,788.35  
277              3           2005        3,189,418.37         2,449,684.05         17,147,788.35  
280              1           2005        1,352,577.13         2,449,684.05         17,147,788.35  
281              4           2005        2,458,535.62         2,449,684.05         17,147,788.35  
283              1           2005        1,573,012.94         2,449,684.05         17,147,788.35  
284              1           2006        1,576,562.20         2,138,250.72         19,244,256.47  
287              NULL        2006        519,905.93           2,138,250.72         19,244,256.47  
285              NULL        2007        172,524.45           1,941,678.09         19,416,780.93  
(10 row(s) affected)  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. A simple SUM example  
 The following example returns the total number of each product sold in the year 2003.  
  
```  
-- Uses AdventureWorks  
  
SELECT ProductKey, SUM(SalesAmount) AS TotalPerProduct  
FROM dbo.FactInternetSales  
WHERE OrderDateKey >= '20030101'   
      AND OrderDateKey < '20040101'  
GROUP BY ProductKey  
ORDER BY ProductKey;  
  
```  
  
 Here is a partial result set.  
  
 ```
ProductKey  TotalPerProduct
----------  ---------------
214         31421.0200
217         31176.0900
222         29986.4300
225          7956.1500
 ```
  
### D. Calculating group totals with more than one column  
 The following example calculates the sum of the `ListPrice` and `StandardCost` for each color listed in the `Product` table.  
  
```  
-- Uses AdventureWorks  
  
SELECT Color, SUM(ListPrice)AS TotalList,   
       SUM(StandardCost) AS TotalCost  
FROM dbo.DimProduct  
GROUP BY Color  
ORDER BY Color;  
```  
  
 The first part of the result set is shown below:  
  
 ```
Color       TotalList      TotalCost
----------  -------------  --------------
Black       101295.7191    57490.5378
Blue         24082.9484    14772.0524
Grey           125.0000       51.5625
Multi          880.7468      526.4095
NA            3162.3564     1360.6185
 ```  
  
## See Also  
 [Aggregate Functions &#40;Transact-SQL&#41;](../../t-sql/functions/aggregate-functions-transact-sql.md)   
 [OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md)  
  
  

