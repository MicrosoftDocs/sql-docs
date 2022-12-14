---
title: "GREATEST (Transact-SQL)"
description: "The GREATEST logical functions returns the maximum value from a list of one or more expressions."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/11/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: event-tier1-build-2022
f1_keywords:
  - "GREATEST"
  - "GREATEST_TSQL"
helpviewer_keywords:
  - "GREATEST function"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = azuresqldb-mi-current || = azure-sqldw-latest"
---
# Logical Functions - GREATEST (Transact-SQL)

[!INCLUDE [sqlserver-2022-asdb-asdbmi-asa-svrless-poolonly](../../includes/applies-to-version/sqlserver2022-asdb-asdbmi-asa-svrless-poolonly.md)]


 This function returns the maximum value from a list of one or more expressions. 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
GREATEST ( expression1 [ ,...expressionN ] ) 
```  

## Arguments
 *expression1, expressionN*  
 A list of comma-separated expressions of any comparable data type. The `GREATEST` function requires at least one argument and supports no more than 254 arguments.  
 
 Each expression can be a constant, variable, column name or function, and any combination of arithmetic, bitwise, and string operators. Aggregate functions and scalar subqueries are permitted.  
  
## Return types  
 Returns the data type with the highest precedence from the set of types passed to the function. For more information, see [Data Type Precedence &#40;Transact-SQL&#41;](../../t-sql/data-types/data-type-precedence-transact-sql.md).  

 If all arguments have the same data type and the type is supported for comparison, `GREATEST` will return that type. 

 Otherwise, the function will implicitly convert all arguments to the data type of the highest precedence before comparison and use this type as the return type. 

 For numeric types, the scale of the return type will be the same as the highest precedence argument, or the largest scale if more than one argument is of the highest precedence data type.
  
## Remarks  
 All expressions in the list of arguments must be of a data type that is comparable and that can be implicitly converted to the data type of the argument with the highest precedence. 

 Implicit conversion of all arguments to the highest precedence data type takes place before comparison. 

 If implicit type conversion between the arguments is not supported, the function will fail and return an error. 

 For more information on implicit and explicit conversion, see [Data Type Conversion &#40;Database Engine&#41;](../../t-sql/data-types/data-type-conversion-database-engine.md). 

 If one or more arguments are not `NULL`, then `NULL` arguments will be ignored during comparison. If all arguments are `NULL`, then `GREATEST` will return `NULL`. 

 Comparison of character arguments follows the rules of [Collation Precedence &#40;Transact-SQL&#41;](../../t-sql/statements/collation-precedence-transact-sql.md). 

 The following types are **not** supported for comparison in `GREATEST`: **varchar(max), varbinary(max) or nvarchar(max) exceeding 8,000 bytes, cursor, geometry, geography, image, non-byte-ordered user-defined types, ntext, table, text**, and **xml**. 

 The varchar(max), varbinary(max), and nvarchar(max) data types are supported for arguments that are 8,000 bytes or below, and will be implicitly converted to varchar(n), varbinary(n), and nvarchar(n), respectively, prior to comparison. 

 For example, varchar(max) can support up to 8,000 characters if using a single-byte encoding character set, and nvarchar(max) can support up to 4,000 byte-pairs (assuming UTF-16 character encoding). 
  
## Examples  

### A. Simple example

 The following example returns the maximum value from the list of constants that is provided. 

 The scale of the return type is determined by the scale of the argument with the highest precedence data type. 
 
```sql 
SELECT GREATEST ( '6.62', 3.1415, N'7' ) AS GreatestVal; 
GO 
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```output  
GreatestVal 
-------- 
  7.0000 

(1 rows affected)  
```  

### B. Simple example with character types

 The following example returns the maximum value from the list of character constants that is provided.  
  
```sql  
SELECT GREATEST ('Glacier', N'Joshua Tree', 'Mount Rainier') AS GreatestString;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```output  
GreatestString 
------------- 
Mount Rainier 

(1 rows affected)  
```  

### C. Simple example with table

This example returns the minimum value from a list of column arguments and ignores `NULL` values during comparison. This sample uses the `AdventureWorksLT` database, which can be quickly installed as the sample database for a new Azure SQL Database. For more information, see [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md#deploy-to-azure-sql-database).
  
```sql  
SELECT P.Name, P.SellStartDate, P.DiscontinuedDate, PM.ModifiedDate AS ModelModifiedDate
    , GREATEST(P.SellStartDate, P.DiscontinuedDate, PM.ModifiedDate) AS LatestDate
FROM SalesLT.Product AS P
INNER JOIN SalesLT.ProductModel AS PM on P.ProductModelID = PM.ProductModelID
WHERE GREATEST(P.SellStartDate, P.DiscontinuedDate, PM.ModifiedDate) >='2007-01-01'
AND P.SellStartDate >='2007-01-01'
AND P.Name LIKE 'Touring %'
ORDER BY P.Name;
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  `LatestDate` chooses the greatest date value of the three values, ignoring `NULL`.
  
```output  
Name                 SellStartDate           DiscontinuedDate    ModelModifiedDate       LatestDate
-------------------- ----------------------- ------------------- ----------------------- -----------------------
Touring Pedal        2007-07-01 00:00:00.000 NULL                2009-05-16 16:34:29.027 2009-05-16 16:34:29.027
Touring Tire         2007-07-01 00:00:00.000 NULL                2007-06-01 00:00:00.000 2007-07-01 00:00:00.000
Touring Tire Tube    2007-07-01 00:00:00.000 NULL                2007-06-01 00:00:00.000 2007-07-01 00:00:00.000

(3 rows affected)  
```  

### D. Using `GREATEST` with local variables

 This example uses `GREATEST` to determine the maximum value of a list of local variables within the predicate of a `WHERE` clause. 
  
```sql  
CREATE TABLE dbo.studies (    
    VarX varchar(10) NOT NULL,    
    Correlation decimal(4, 3) NULL 
); 

INSERT INTO dbo.studies VALUES ('Var1', 0.2), ('Var2', 0.825), ('Var3', 0.61); 
GO 

DECLARE @PredictionA DECIMAL(2,1) = 0.7;  
DECLARE @PredictionB DECIMAL(3,1) = 0.65;  

SELECT VarX, Correlation  
FROM dbo.studies 
WHERE Correlation > GREATEST(@PredictionA, @PredictionB); 
GO 
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  Only values greater than 0.7 are displayed.
  
```output  
VarX       Correlation 
---------- ----------- 
Var2              .825 

(1 rows affected)  
```  

### E. Using `GREATEST` with columns, constants, and variables

 This example uses `GREATEST` to determine the maximum value of a mixed list that includes columns, constants, and variables. 
  
```sql  
CREATE TABLE dbo.studies (    
    VarX varchar(10) NOT NULL,    
    Correlation decimal(4, 3) NULL 
); 

INSERT INTO dbo.studies VALUES ('Var1', 0.2), ('Var2', 0.825), ('Var3', 0.61); 
GO 

DECLARE @VarX decimal(4, 3) = 0.59;  

SELECT VarX, Correlation, GREATEST(Correlation, 0, @VarX) AS GreatestVar  
FROM dbo.studies;
GO 
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```output  
VarX       Correlation           GreatestVar 
---------- --------------------- ---------------------
Var1       0.200                 0.590
Var2       0.825                 0.825
Var3       0.610                 0.610

(3 rows affected) 
```  

## Next steps

- [LEAST &#40;Transact-SQL&#41;](../../t-sql/functions/logical-functions-least-transact-sql.md)  
- [MAX &#40;Transact-SQL&#41;](../../t-sql/functions/max-transact-sql.md)  
- [MIN &#40;Transact-SQL&#41;](../../t-sql/functions/min-transact-sql.md)  
- [CASE &#40;Transact-SQL&#41;](../../t-sql/language-elements/case-transact-sql.md)  
- [CHOOSE &#40;Transact-SQL&#41;](../../t-sql/functions/logical-functions-choose-transact-sql.md)
