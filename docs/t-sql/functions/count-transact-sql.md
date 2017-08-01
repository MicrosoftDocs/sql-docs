---
title: "COUNT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "COUNT_TSQL"
  - "COUNT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "totals [SQL Server], COUNT function"
  - "totals [SQL Server]"
  - "counting items in group"
  - "groups [SQL Server], number of items in"
  - "number of group items"
  - "COUNT function [Transact-SQL]"
ms.assetid: 28d39da6-bc2e-46c7-858c-b1721c938830
caps.latest.revision: 45
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# COUNT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Returns the number of items in a group. COUNT works like the [COUNT_BIG](../../t-sql/functions/count-big-transact-sql.md) function. The only difference between the two functions is their return values. COUNT always returns an **int** data type value. COUNT_BIG always returns a **bigint** data type value.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
-- Syntax for SQL Server and Azure SQL Database  
  
COUNT ( { [ [ ALL | DISTINCT ] expression ] | * } )   
    [ OVER (   
        [ partition_by_clause ]   
        [ order_by_clause ]   
        [ ROW_or_RANGE_clause ]  
    ) ]  
```  
  
```sql
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
-- Aggregation Function Syntax  
COUNT ( { [ [ ALL | DISTINCT ] expression ] | * } )  

-- Analytic Function Syntax  
COUNT ( { expression | * } ) OVER ( [ <partition_by_clause> ] )  
```  
  
## Arguments  
**ALL**  
Applies the aggregate function to all values. ALL is the default.
  
DISTINCT  
Specifies that COUNT returns the number of unique nonnull values.
  
*expression*  
Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any type except **text**, **image**, or **ntext**. Aggregate functions and subqueries are not permitted.
  
\*  
Specifies that all rows should be counted to return the total number of rows in a table. COUNT(\*) takes no parameters and cannot be used with DISTINCT. COUNT(\*) does not require an *expression* parameter because, by definition, it does not use information about any particular column. COUNT(*) returns the number of rows in a specified table without getting rid of duplicates. It counts each row separately. This includes rows that contain null values.
  
OVER **(** [ *partition_by_clause* ] [ *order_by_clause* ] [ *ROW_or_RANGE_clause* ] **)**  
*partition_by_clause* divides the result set produced by the FROM clause into partitions to which the function is applied. If not specified, the function treats all rows of the query result set as a single group. *order_by_clause* determines the logical order in which the operation is performed. For more information, see [OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md).
  
## Return types
 **int**  
  
## Remarks  
COUNT(*) returns the number of items in a group. This includes NULL values and duplicates.
  
COUNT(ALL *expression*) evaluates *expression* for each row in a group and returns the number of nonnull values.
  
COUNT(DISTINCT *expression*) evaluates *expression* for each row in a group and returns the number of unique, nonnull values.
  
For return values greater than 2^31-1, COUNT produces an error. Use COUNT_BIG instead.
  
COUNT is a deterministic function when used without the OVER and ORDER BY clauses. It is nondeterministic when specified with the OVER and ORDER BY clauses. For more information, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).
  
## Examples  
  
### A. Using COUNT and DISTINCT  
The following example lists the number of different titles that an employee who works at [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)] can hold.
  
```sql
SELECT COUNT(DISTINCT Title)  
FROM HumanResources.Employee;  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
`-----------`
  
 `67`  
  
`(1 row(s) affected)`
  
### B. Using COUNT(*)  
The following example finds the total number of employees who work at [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)].
  
```sql
SELECT COUNT(*)  
FROM HumanResources.Employee;  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
`-----------`
  
 `290`  
  
`(1 row(s) affected)`
  
### C. Using COUNT(*) with other aggregates  
The following example shows that `COUNT(*)` can be combined with other aggregate functions in the select list. The example uses the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.
  
```sql
SELECT COUNT(*), AVG(Bonus)  
FROM Sales.SalesPerson  
WHERE SalesQuota > 25000;  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
`----------- ---------------------`
  
`14            3472.1428`
  
`(1 row(s) affected)`
  
### C. Using the OVER clause  
The following example uses the MIN, MAX, AVG and COUNT functions with the OVER clause to provide aggregated values for each department in the `HumanResources.Department` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.
  
```sql
SELECT DISTINCT Name  
       , MIN(Rate) OVER (PARTITION BY edh.DepartmentID) AS MinSalary  
       , MAX(Rate) OVER (PARTITION BY edh.DepartmentID) AS MaxSalary  
       , AVG(Rate) OVER (PARTITION BY edh.DepartmentID) AS AvgSalary  
       ,COUNT(edh.BusinessEntityID) OVER (PARTITION BY edh.DepartmentID) AS EmployeesPerDept  
FROM HumanResources.EmployeePayHistory AS eph  
JOIN HumanResources.EmployeeDepartmentHistory AS edh  
     ON eph.BusinessEntityID = edh.BusinessEntityID  
JOIN HumanResources.Department AS d  
ON d.DepartmentID = edh.DepartmentID
WHERE edh.EndDate IS NULL  
ORDER BY Name;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
Name                          MinSalary             MaxSalary             AvgSalary             EmployeesPerDept  
----------------------------- --------------------- --------------------- --------------------- ----------------  
Document Control              10.25                 17.7885               14.3884               5  
Engineering                   32.6923               63.4615               40.1442               6  
Executive                     39.06                 125.50                68.3034               4  
Facilities and Maintenance    9.25                  24.0385               13.0316               7  
Finance                       13.4615               43.2692               23.935                10  
Human Resources               13.9423               27.1394               18.0248               6  
Information Services          27.4038               50.4808               34.1586               10  
Marketing                     13.4615               37.50                 18.4318               11  
Production                    6.50                  84.1346               13.5537               195  
Production Control            8.62                  24.5192               16.7746               8  
Purchasing                    9.86                  30.00                 18.0202               14  
Quality Assurance             10.5769               28.8462               15.4647               6  
Research and Development      40.8654               50.4808               43.6731               4  
Sales                         23.0769               72.1154               29.9719               18  
Shipping and Receiving        9.00                  19.2308               10.8718               6  
Tool Design                   8.62                  29.8462               23.5054               6  
  
(16 row(s) affected)
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### D. Using COUNT and DISTINCT  
The following example lists the number of different titles that an employee who works at a specific company can hold.
  
```sql
USE ssawPDW;  
  
SELECT COUNT(DISTINCT Title)  
FROM dbo.DimEmployee;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
`-----------`
  
 `67`  
  
### E. Using COUNT(*)  
The following example returns the total number of rows in the `dbo.DimEmployee` table.
  
```sql
USE ssawPDW;  
  
SELECT COUNT(*)  
FROM dbo.DimEmployee;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
`-------------`
  
 `296`  
  
### F. Using COUNT(*) with other aggregates  
The following example combines `COUNT(*)` with other aggregate functions in the SELECT list. The query returns the number of sales representatives with a annual sales quota greater than $500,000 and the average sales quota.
  
```sql
USE ssawPDW;  
  
SELECT COUNT(EmployeeKey) AS TotalCount, AVG(SalesAmountQuota) AS [Average Sales Quota]  
FROM dbo.FactSalesQuota  
WHERE SalesAmountQuota > 500000 AND CalendarYear = 2001;  
  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
`TotalCount  Average Sales Quota`
  
`----------  -------------------`
  
`10          683800.0000`
  
### G. Using COUNT with HAVING  
The following example uses COUNT with the HAVING clause to return the departments in a company that have more than 15 employees.
  
```sql
USE ssawPDW;  
  
SELECT DepartmentName,   
       COUNT(EmployeeKey)AS EmployeesInDept  
FROM dbo.DimEmployee  
GROUP BY DepartmentName  
HAVING COUNT(EmployeeKey) > 15;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
`DepartmentName  EmployeesInDept`
  
`--------------  ---------------`
  
`Sales           18`
  
`Production      179`
  
### H. Using COUNT with OVER  
The following example uses COUNT with the OVER clause to return the number of products that are contained in each of the specified sales orders.
  
```sql
USE ssawPDW;  
  
SELECT DISTINCT COUNT(ProductKey) OVER(PARTITION BY SalesOrderNumber) AS ProductCount  
    ,SalesOrderNumber  
FROM dbo.FactInternetSales  
WHERE SalesOrderNumber IN (N'SO53115',N'SO55981');  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
`ProductCount   SalesOrderID`
  
`------------   -----------------`
  
`3              SO53115`
  
`1              SO55981`
  
## See also
[Aggregate Functions &#40;Transact-SQL&#41;](../../t-sql/functions/aggregate-functions-transact-sql.md)  
[COUNT_BIG &#40;Transact-SQL&#41;](../../t-sql/functions/count-big-transact-sql.md)  
[OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md)
  
  


