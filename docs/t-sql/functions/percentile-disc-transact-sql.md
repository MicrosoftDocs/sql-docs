---
title: "PERCENTILE_DISC (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/20/2015"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "PERCENTILE_DISC"
  - "PERCENTILE_DISC_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "PERCENTILE_DISC function"
  - "analytic functions,PERCENTILE_DISC"
ms.assetid: b545413d-c4f7-4c8e-8617-607599a26680
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# PERCENTILE_DISC (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-all-md](../../includes/tsql-appliesto-ss2012-all-md.md)]

  Computes a specific percentile for sorted values in an entire rowset or within a rowset's distinct partitions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a given percentile value *P*, PERCENTILE_DISC sorts the expression values in the ORDER BY clause and returns the value with the smallest CUME_DIST value (with respect to the same sort specification) that is greater than or equal to *P*. For example, PERCENTILE_DISC (0.5) will compute the 50th percentile (that is, the median) of an expression. PERCENTILE_DISC calculates the percentile based on a discrete distribution of the column values. The result is equal to a specific column value.  
  
 ![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
PERCENTILE_DISC ( numeric_literal ) WITHIN GROUP ( ORDER BY order_by_expression [ ASC | DESC ] )  
    OVER ( [ <partition_by_clause> ] )  
```  
  
## Arguments  
 *literal*  
 The percentile to compute. The value must range between 0.0 and 1.0.  
  
 WITHIN GROUP **(** ORDER BY *order_by_expression* [ **ASC** | DESC)**  
 Specifies a list of values to sort and compute the percentile over. Only one *order_by_expression* is allowed. The default sort order is ascending. The list of values can be of any of the data types that are valid for the sort operation.  
  
 OVER **(** \<partition_by_clause> **)**  
 Divides the FROM clause's result set into partitions. The percentile function is applied to these partitions. For more information, see [OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md). The \<ORDER BY clause> and \<rows or range clause>cannot be specified in a PERCENTILE_DISC function.  
  
## Return Types  
 The return type is determined by the *order_by_expression* type.  
  
## Compatibility Support  
 Under compatibility level 110 and higher, WITHIN GROUP is a reserved keyword. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).  
  
## General Remarks  
 Any nulls in the data set are ignored.  
  
 PERCENTILE_DISC is nondeterministic. For more information, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).  
  
## Examples  
  
### A. Basic syntax example  
 The following example uses PERCENTILE_CONT and PERCENTILE_DISC to find the median employee salary in each department. They may not return the same value - PERCENTILE_CONT interpolates the appropriate value, even if it doesn't exist in the data set, while PERCENTILE_DISC always returns an actual set value.  
  
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
 The following example uses PERCENTILE_CONT and PERCENTILE_DISC to find each department's median employee salary. They may not return the same value - PERCENTILE_CONT interpolates the appropriate value, even if it doesn't exist in the data set, while PERCENTILE_DISC always returns an actual set value.  
  
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
Shipping and Receiving  9.250000     9.0000
```  
  
## See Also  
 [PERCENTILE_CONT &#40;Transact-SQL&#41;](../../t-sql/functions/percentile-cont-transact-sql.md)  
  
  


