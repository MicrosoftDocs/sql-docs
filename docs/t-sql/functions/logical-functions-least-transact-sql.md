---
description: "Logical Functions - LEAST (Transact-SQL)"
title: "LEAST (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/09/2021"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: reference
f1_keywords: 
  - "LEAST"
  - "LEAST_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "LEAST function"
ms.assetid: 1c382c83-7500-4bae-bbdc-c1dbebd3d83f
author: jmsteen
ms.author: josteen
---
# Logical Functions - LEAST (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database ](../../includes/applies-to-version/sql-asdb.md)]

**_NOTE:_**  LEAST is currently available for Azure SQL Database, Azure SQL Managed Instance and serverless SQL pools in Azure Synapse Analytics.

 This function returns the minimum value from a list of one or more expressions. 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
LEAST ( expression1 [ ,...expressionN ] ) 
```  

## Arguments
 *expression1, expressionN*  
 A list of comma-separated expressions of any comparable data type. The `LEAST` function requires at least one argument and supports no more than 254 arguments.  
 
 Each expression can be a constant, variable, column name or function, as well as any combination of arithmetic, bitwise and string operators. Aggregate functions and scalar subqueries are permitted.  
  
## Return Types  
 Returns the data type with the highest precedence from the set of types passed to the function. For more information, see [Data Type Precedence &#40;Transact-SQL&#41;](../../t-sql/data-types/data-type-precedence-transact-sql.md).  

 If all arguments have the same data type and the type is supported for comparison, `LEAST` will return that type. 

 Otherwise, the function will implicitly convert all arguments to the data type of the highest precedence before comparison and use this type as the return type. 

 For numeric types, the scale of the return type will be the same as the highest precedence argument, or the largest scale if more than one argument is of the highest precedence data type.
  
## Remarks  
 All expressions in the list of arguments must be of a data type that is comparable and that can be implicitly converted to the data type of the argument with the highest precedence. 

 Implicit conversion of all arguments to the highest precedence data type takes place before comparison. 

 If implicit type conversion between the arguments is not supported, the function will fail and return an error. 

 See [Data Type Conversion &#40;Database Engine&#41;](../../t-sql/data-types/data-type-conversion-database-engine.md) for further details on implicit and explicit conversion. 

 If one or more arguments are not null, then null arguments will be ignored during comparison. If all arguments are null, then `LEAST` will return null. 

 Comparison of character arguments follows the rules of [Collation Precedence &#40;Transact-SQL&#41;](../../t-sql/statements/collation-precedence-transact-sql.md). 

 The following types are **not** supported for comparison in `LEAST`: **varchar(max), varbinary(max) or nvarchar(max) exceeding 8,000 bytes, cursor, geometry, geography, image, non-byte-ordered user-defined types, ntext, table, text** and **xml**. 

 The varchar(max), varbinary(max) and nvarchar(max) data types are supported for arguments that are 8,000 bytes or below, and will be implicitly converted to varchar(n), varbinary(n) and nvarchar(n), respectively, prior to comparison. 

 For example, varchar(max) can support up to 8,000 characters if using a single-byte encoding character set, and nvarchar(max) can support up to 4,000 byte-pairs (assuming UTF-16 character encoding). 
  
## Examples  

### A. Simple example

 The following example returns the minimum value from the list of constants that is provided. 

 The scale of the return type is determined by that of the argument with the highest precedence data type. 
 
```sql 
SELECT LEAST ( '6.62', 3.1415, N'7' ) AS Least; 
GO 
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Least 
------- 
 3.1415 

(1 rows affected)  
```  

### B. Simple example with character types

 The following example returns the minimum value from the list of character constants that is provided.  
  
```sql  
SELECT LEAST ('Glacier', N'Joshua Tree', 'Mount Rainier') AS Least;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Least 
------------- 
Glacier 

(1 rows affected)  
```  

### C. Simple example with table
  
 This example returns the minimum value from a list of column arguments and ignores `NULL` values during comparison. 
  
```sql  
USE AdventureWorks2019; 
GO 

SELECT sp.SalesQuota, sp.SalesYTD, sp.SalesLastYear 
      , LEAST(sp.SalesQuota, sp.SalesYTD, sp.SalesLastYear) AS Least 
FROM sales.SalesPerson AS sp 
WHERE sp.SalesYTD < 3000000; 
GO  
  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
SalesQuota            SalesYTD              SalesLastYear         Least 
--------------------- --------------------- --------------------- --------------------- 
                 NULL           559697.5639                 .0000                 .0000 
          250000.0000          1453719.4653          1620276.8966           250000.0000 
          300000.0000          2315185.6110          1849640.9418           300000.0000 
          250000.0000          1352577.1325          1927059.1780           250000.0000 
          250000.0000          2458535.6169          2073505.9999           250000.0000 
          250000.0000          2604540.7172          2038234.6549           250000.0000 
          250000.0000          1573012.9383          1371635.3158           250000.0000 
          300000.0000          1576562.1966                 .0000                 .0000 
                 NULL           172524.4512                 .0000                 .0000 
          250000.0000          1421810.9242          2278548.9776           250000.0000 
                 NULL           519905.9320                 .0000                 .0000 
          250000.0000          1827066.7118          1307949.7917           250000.0000 

(12 rows affected) 
  
```  
### D. Using `LEAST` with local variables

 This example uses `LEAST` to determine the minimum value of a list of local variables within the predicate of a `WHERE` clause. 
  
```sql  
CREATE TABLE studies (    
    Variable varchar(10) NOT NULL,    
    Correlation decimal(4, 3) NULL 
); 

INSERT INTO studies VALUES ('Var1', 0.2), ('Var2', 0.825), ('Var3', 0.61); 
GO 

DECLARE @PredictionA DECIMAL(2,1) = 0.7;  
DECLARE @PredictionB DECIMAL(3,1) = 0.65;  

SELECT Variable, Correlation  
FROM studies 
WHERE Correlation < LEAST(@PredictionA, @PredictionB); 
GO 
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Variable   Correlation 
---------- ----------- 
Var1              .200 
Var3              .610 

(2 rows affected)
```  

### E. Using `LEAST` with columns, constants and variables

 This example uses `LEAST` to determine the minimum value of a list that includes columns, constants and variables. 
  
```sql  
CREATE TABLE products (    
    prod_id int IDENTITY(1,1),    
    listprice smallmoney NULL 
); 

INSERT INTO products VALUES (14.99), (49.99), (24.99); 
GO 

DECLARE @PriceX smallmoney = 19.99;  

SELECT LEAST(listprice, 40, @PriceX) as LeastPrice  
FROM products;
GO 
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
LeastPrice
------------
     14.9900
     19.9900
     19.9900

(3 rows affected) 
```  

  
## See Also  
 [GREATEST &#40;Transact-SQL&#41;](../../t-sql/functions/logical-functions-greatest-transact-sql.md)  
 [MAX &#40;Transact-SQL&#41;](../../t-sql/functions/max-transact-sql.md)  
 [MIN &#40;Transact-SQL&#41;](../../t-sql/functions/min-transact-sql.md)  
 [CASE &#40;Transact-SQL&#41;](../../t-sql/language-elements/case-transact-sql.md)  
 [CHOOSE &#40;Transact-SQL&#41;](../../t-sql/functions/logical-functions-choose-transact-sql.md)  
  
  
