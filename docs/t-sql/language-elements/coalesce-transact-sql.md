---
title: "COALESCE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/30/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "COALESCE"
  - "COALESCE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "expressions [SQL Server], nonnull"
  - "COALESCE function"
  - "first nonnull expressions [SQL Server]"
  - "nonnull expressions"
ms.assetid: fafc0dba-f8a8-4aad-9b7f-908e34b74d88
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# COALESCE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

> [!div class="nextstepaction"]
> [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

Evaluates the arguments in order and returns the current value of the first expression that initially doesn't evaluate to `NULL`. For example, `SELECT COALESCE(NULL, NULL, 'third_value', 'fourth_value');` returns the third value because the third value is the first value that isn't null. 
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
COALESCE ( expression [ ,...n ] )   
```  
  
## Arguments  
_expression_  
Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any type.  
  
## Return Types  
Returns the data type of _expression_ with the highest data type precedence. If all expressions are nonnullable, the result is typed as nonnullable.  
  
## Remarks  
If all arguments are `NULL`, `COALESCE` returns `NULL`. At least one of the null values must be a typed `NULL`.  
  
## Comparing COALESCE and CASE  
The `COALESCE` expression is a syntactic shortcut for the `CASE` expression.  That is, the code `COALESCE`(_expression1_,_...n_) is rewritten by the query optimizer as  the following `CASE` expression:  
  
```sql  
CASE  
WHEN (expression1 IS NOT NULL) THEN expression1  
WHEN (expression2 IS NOT NULL) THEN expression2  
...  
ELSE expressionN  
END  
```  
  
As such, the input values (_expression1_, _expression2_, _expressionN_, and so on) are evaluated multiple times. A value expression that contains a subquery is considered non-deterministic and the subquery is evaluated twice. This result is in compliance with the SQL standard. In either case, different results can be returned between the first evaluation and upcoming evaluations.  
  
For example, when the code `COALESCE((subquery), 1)` is executed, the subquery is evaluated twice. As a result, you can get different results depending on the isolation level of the query. For example, the code can return `NULL` under the `READ COMMITTED` isolation level in a multi-user environment. To ensure stable results are returned, use the `SNAPSHOT ISOLATION` isolation level, or replace `COALESCE` with the `ISNULL` function. As an alternative, you can rewrite the query to push the subquery into a subselect as shown in the following example:  
  
```sql  
SELECT CASE WHEN x IS NOT NULL THEN x ELSE 1 END  
from  
(  
SELECT (SELECT Nullable FROM Demo WHERE SomeCol = 1) AS x  
) AS T;  
  
```  
  
## Comparing COALESCE and ISNULL  
The `ISNULL` function and the `COALESCE` expression have a similar purpose but can behave differently.  
  
1.  Because `ISNULL` is a function, it's evaluated only once.  As described above, the input values for the `COALESCE` expression can be evaluated multiple times.  
  
2.  Data type determination of the resulting expression is different. `ISNULL` uses the data type of the first parameter, `COALESCE` follows the `CASE` expression rules and returns the data type of value with the highest precedence.  
  
3.  The NULLability of the result expression is different for `ISNULL` and `COALESCE`. The `ISNULL` return value is always considered NOT NULLable (assuming the return value is a non-nullable one). By contrast,`COALESCE` with non-null parameters is considered to be `NULL`. So the expressions `ISNULL(NULL, 1)` and `COALESCE(NULL, 1)`, although equal, have different nullability values. These values make a difference if you're using these expressions in computed columns, creating key constraints or making the return value of a scalar UDF deterministic so that it can be indexed as shown in the following example:  
  
    ```sql  
    USE tempdb;  
    GO  
    -- This statement fails because the PRIMARY KEY cannot accept NULL values  
    -- and the nullability of the COALESCE expression for col2   
    -- evaluates to NULL.  
    CREATE TABLE #Demo   
    (   
    col1 integer NULL,   
    col2 AS COALESCE(col1, 0) PRIMARY KEY,   
    col3 AS ISNULL(col1, 0)   
    );   
  
    -- This statement succeeds because the nullability of the   
    -- ISNULL function evaluates AS NOT NULL.  
  
    CREATE TABLE #Demo   
    (   
    col1 integer NULL,   
    col2 AS COALESCE(col1, 0),   
    col3 AS ISNULL(col1, 0) PRIMARY KEY   
    );  
    ```  
  
4.  Validations for `ISNULL` and `COALESCE` are also different. For example, a `NULL` value for `ISNULL` is converted to **int** though for `COALESCE`, you must provide a data type.  
  
5.  `ISNULL` takes only two parameters. By contrast `COALESCE` takes a variable number of parameters.  
  
## Examples  
  
### A. Running a simple example  
The following example shows how `COALESCE` selects the data from the first column that has a nonnull value. This example uses the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```sql  
SELECT Name, Class, Color, ProductNumber,  
COALESCE(Class, Color, ProductNumber) AS FirstNotNull  
FROM Production.Product;  
```  
  
### B. Running a complex example  
In the following example, the `wages` table includes three columns that contain information about the yearly wages of the employees: the hourly wage, salary, and commission. However, an employee receives only one type of pay. To determine the total amount paid to all employees, use `COALESCE` to receive only the nonnull value found in `hourly_wage`, `salary`, and `commission`.  
  
```sql  
SET NOCOUNT ON;  
GO  
USE tempdb;  
IF OBJECT_ID('dbo.wages') IS NOT NULL  
    DROP TABLE wages;  
GO  
CREATE TABLE dbo.wages  
(  
    emp_id        tinyint   identity,  
    hourly_wage   decimal   NULL,  
    salary        decimal   NULL,  
    commission    decimal   NULL,  
    num_sales     tinyint   NULL  
);  
GO  
INSERT dbo.wages (hourly_wage, salary, commission, num_sales)  
VALUES  
    (10.00, NULL, NULL, NULL),  
    (20.00, NULL, NULL, NULL),  
    (30.00, NULL, NULL, NULL),  
    (40.00, NULL, NULL, NULL),  
    (NULL, 10000.00, NULL, NULL),  
    (NULL, 20000.00, NULL, NULL),  
    (NULL, 30000.00, NULL, NULL),  
    (NULL, 40000.00, NULL, NULL),  
    (NULL, NULL, 15000, 3),  
    (NULL, NULL, 25000, 2),  
    (NULL, NULL, 20000, 6),  
    (NULL, NULL, 14000, 4);  
GO  
SET NOCOUNT OFF;  
GO  
SELECT CAST(COALESCE(hourly_wage * 40 * 52,   
   salary,   
   commission * num_sales) AS money) AS 'Total Salary'   
FROM dbo.wages  
ORDER BY 'Total Salary';  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```   
Total Salary  
------------  
10000.00  
20000.00  
20800.00  
30000.00  
40000.00  
41600.00  
45000.00  
50000.00  
56000.00  
62400.00  
83200.00  
120000.00  
  
(12 row(s) affected)
```  
  
### C: Simple Example  
The following example demonstrates how `COALESCE` selects the data from the first column that has a non-null value. Assume for this example that the `Products` table contains this data:  
  
```  
Name         Color      ProductNumber  
------------ ---------- -------------  
Socks, Mens  NULL       PN1278  
Socks, Mens  Blue       PN1965  
NULL         White      PN9876
```  
 
We then run the following COALESCE query:  
  
```sql  
SELECT Name, Color, ProductNumber, COALESCE(Color, ProductNumber) AS FirstNotNull   
FROM Products ;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Name         Color      ProductNumber  FirstNotNull  
------------ ---------- -------------  ------------  
Socks, Mens  NULL       PN1278         PN1278  
Socks, Mens  Blue       PN1965         Blue  
NULL         White      PN9876         White
```  
  
Notice that in the first row, the `FirstNotNull` value is `PN1278`, not `Socks, Mens`. This value is this way because the `Name` column wasn't specified as a parameter for `COALESCE` in the example.  
  
### D: Complex Example  
The following example uses `COALESCE` to compare the values in three columns  and return only the non-null value found in the columns.  
  
```sql  
CREATE TABLE dbo.wages  
(  
    emp_id        tinyint   NULL,  
    hourly_wage   decimal   NULL,  
    salary        decimal   NULL,  
    commission    decimal   NULL,  
    num_sales     tinyint   NULL  
);  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (1, 10.00, NULL, NULL, NULL);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (2, 20.00, NULL, NULL, NULL);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (3, 30.00, NULL, NULL, NULL);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (4, 40.00, NULL, NULL, NULL);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (5, NULL, 10000.00, NULL, NULL);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (6, NULL, 20000.00, NULL, NULL);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (7, NULL, 30000.00, NULL, NULL);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (8, NULL, 40000.00, NULL, NULL);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (9, NULL, NULL, 15000, 3);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (10,NULL, NULL, 25000, 2);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (11, NULL, NULL, 20000, 6);  
  
INSERT INTO dbo.wages (emp_id, hourly_wage, salary, commission, num_sales)  
VALUES (12, NULL, NULL, 14000, 4);  
  
SELECT CAST(COALESCE(hourly_wage * 40 * 52,   
   salary,   
   commission * num_sales) AS decimal(10,2)) AS TotalSalary   
FROM dbo.wages  
ORDER BY TotalSalary;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
Total Salary  
------------  
10000.00  
20000.00  
20800.00  
30000.00  
40000.00  
41600.00  
45000.00  
50000.00  
56000.00  
62400.00  
83200.00  
120000.00
```  
  
## See Also  
[ISNULL &#40;Transact-SQL&#41;](../../t-sql/functions/isnull-transact-sql.md)   
[CASE &#40;Transact-SQL&#41;](../../t-sql/language-elements/case-transact-sql.md)  
  
  
