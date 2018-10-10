---
title: "MAX (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/23/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "MAX"
  - "MAX_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MAX function [Transact-SQL]"
  - "values [SQL Server], maximum"
  - "maximum values [SQL Server]"
ms.assetid: 9b002b69-ab5e-472d-b12e-dc2fbe35ef42
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# MAX (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the maximum value in the expression.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Aggregation Function Syntax  
MAX( [ ALL | DISTINCT ] expression )  
  
-- Analytic Function Syntax  
MAX ([ ALL ] expression) OVER ( [ <partition_by_clause> ] [ <order_by_clause> ] )  
``` 
  
## Arguments  
 **ALL**  
 Applies the aggregate function to all values. ALL is the default.  
  
 DISTINCT  
 Specifies that each unique value is considered. DISTINCT is not meaningful with MAX and is available for ISO compatibility only.  
  
 *expression*  
 Is a constant, column name, or function, and any combination of arithmetic, bitwise, and string operators. MAX can be used with **numeric**, **character**, **uniqueidentifier**, and **datetime** columns, but not with **bit** columns. Aggregate functions and subqueries are not permitted.  
  
 For more information, see [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md).  
  
 OVER **(** [ _partition\_by\_clause_ ] _order\_by\_clause_**)**  
 *partition_by_clause* divides the result set produced by the FROM clause into partitions to which the function is applied. If not specified, the function treats all rows of the query result set as a single group. *order_by_clause* determines the logical order in which the operation is performed. *order_by_clause* is required. For more information, see [OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md).  
  
## Return Types  
 Returns a value same as *expression*.  
  
## Remarks  
 MAX ignores any null values.  
 
 MAX returns NULL when there is no row to select.  
  
 For character columns, MAX finds the highest value in the collating sequence.  
  
 MAX is a deterministic function when used without the OVER and ORDER BY clauses. It is nondeterministic when specified with the OVER and ORDER BY clauses. For more information, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).  
  
## Examples  
  
### A. Simple example  
 The following example returns the highest (maximum) tax rate in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```sql  
SELECT MAX(TaxRate)  
FROM Sales.SalesTaxRate;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```  
 -------------------  
 19.60  
 Warning, null value eliminated from aggregate.  
  
 (1 row(s) affected)  
 ```  
  
### B. Using the OVER clause  
 The following example uses the MIN, MAX, AVG, and COUNT functions with the OVER clause to provide aggregated values for each department in the `HumanResources.Department` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
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
  
```  
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
  
### C. Using MAX with character data   
The following example returns the database name that sorts as the last name alphabetically. The example uses `WHERE database_id < 5`, to consider only the system databases.  
```sql   
SELECT MAX(name) FROM sys.databases WHERE database_id < 5;
```
The last system database is `tempdb`.  
  
## See Also  
 [Aggregate Functions &#40;Transact-SQL&#41;](../../t-sql/functions/aggregate-functions-transact-sql.md)   
 [OVER Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-over-clause-transact-sql.md)  
  
  

