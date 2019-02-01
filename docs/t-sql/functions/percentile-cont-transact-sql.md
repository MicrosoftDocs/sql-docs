---
title: "PERCENTILE_CONT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/20/2015"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "PERCENTILE_CONT_TSQL"
  - "PERCENTILE_CONT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "analytic functions, PERCENTILE_CONT"
  - "PERCENTILE_CONT function"
ms.assetid: d019419e-5297-4994-97d5-e9c8fc61bbf4
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# PERCENTILE_CONT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-all-md](../../includes/tsql-appliesto-ss2012-all-md.md)]

  Calculates a percentile based on a continuous distribution of the column value in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The result is interpolated and might not be equal to any of the specific values in the column.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
PERCENTILE_CONT ( numeric_literal )   
    WITHIN GROUP ( ORDER BY order_by_expression [ ASC | DESC ] )  
    OVER ( [ <partition_by_clause> ] )  
```  
  
## Arguments  
 *numeric_literal*  
 The percentile to compute. The value must range between 0.0 and 1.0.  
  
 WITHIN GROUP **(** ORDER BY *order_by_expression* [ **ASC** | DESC ]**)**  
 Specifies a list of numeric values to sort and compute the percentile over. Only one *order_by_expression* is allowed. The expression must evaluate to an exact numeric type (**int**, **bigint**, **smallint**, **tinyint**, **numeric**, **bit**, **decimal**, **smallmoney**, **money**) or an approximate numeric type (**float**, **real**). Other data types are not allowed. The default sort order is ascending.  
  
 OVER **(** \<partition_by_clause> **)**  
 Divides the result set produced by the FROM clause into partitions to which the percentile function is applied. For more information, see [OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md). The \<ORDER BY clause> and \<rows or range clause> of the OVER syntax cannot be specified in a PERCENTILE_CONT function.  
  
## Return Types  
 **float(53)**  
  
## Compatibility Support  
 Under compatibility level 110 and higher, WITHIN GROUP is a reserved keyword. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).  
  
## General Remarks  
 Any nulls in the data set are ignored.  
  
 PERCENTILE_CONT is nondeterministic. For more information, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).  
  
## Examples  
  
### A. Basic syntax example  
 The following example uses PERCENTILE_CONT and PERCENTILE_DISC to find the median employee salary in each department. Note that these functions may not return the same value. This is because PERCENTILE_CONT interpolates the appropriate value, whether or not it exists in the data set, while PERCENTILE_DISC always returns an actual value from the set.  
  
```  
USE AdventureWorks2012;  
  
SELECT DISTINCT Name AS DepartmentName  
      ,PERCENTILE_CONT(0.5) WITHIN GROUP (ORDER BY ph.Rate)   
                            OVER (PARTITION BY Name) AS MedianCont  
      ,PERCENTILE_DISC(0.5) WITHIN GROUP (ORDER BY ph.Rate)   
                            OVER (PARTITION BY Name) AS MedianDisc  
FROM HumanResources.Department AS d  
INNER JOIN HumanResources.EmployeeDepartmentHistory AS dh   
    ON dh.DepartmentID = d.DepartmentID  
INNER JOIN HumanResources.EmployeePayHistory AS ph  
    ON ph.BusinessEntityID = dh.BusinessEntityID  
WHERE dh.EndDate IS NULL;  
```  
  
 Here is a partial result set.  
  
 ```
DepartmentName        MedianCont    MedianDisc
--------------------   ----------   ----------
Document Control       16.8269      16.8269
Engineering            34.375       32.6923
Executive              54.32695     48.5577
Human Resources        17.427850    16.5865
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### B. Basic syntax example  
 The following example uses PERCENTILE_CONT and PERCENTILE_DISC to find the median employee salary in each department. Note that these functions may not return the same value. This is because PERCENTILE_CONT interpolates the appropriate value, whether or not it exists in the data set, while PERCENTILE_DISC always returns an actual value from the set.  
  
```  
-- Uses AdventureWorks  
  
SELECT DISTINCT DepartmentName  
,PERCENTILE_CONT(0.5) WITHIN GROUP (ORDER BY BaseRate)  
    OVER (PARTITION BY DepartmentName) AS MedianCont  
,PERCENTILE_DISC(0.5) WITHIN GROUP (ORDER BY BaseRate)  
    OVER (PARTITION BY DepartmentName) AS MedianDisc  
FROM dbo.DimEmployee;  
  
```  
  
 Here is a partial result set.  
  
 ```
DepartmentName        MedianCont    MedianDisc
--------------------   ----------   ----------
Document Control       16.826900    16.8269
Engineering            34.375000    32.6923
Human Resources        17.427850    16.5865
Shipping and Receiving 9.250000      9.0000
```  
  
## See Also  
 [PERCENTILE_DISC &#40;Transact-SQL&#41;](../../t-sql/functions/percentile-disc-transact-sql.md)  
  
 