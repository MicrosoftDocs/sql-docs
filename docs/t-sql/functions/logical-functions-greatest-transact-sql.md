---
description: "Logical Functions - GREATEST (Transact-SQL)"
title: "GREATEST (Transact-SQL)"
ms.custom: ""
ms.date: "04/09/2021"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.technology: t-sql
ms.topic: reference
f1_keywords: 
  - "GREATEST"
  - "GREATEST_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "GREATEST function"
author: jmsteen
ms.author: josteen
ms.reviewer: wiassaf
---
# Logical Functions - GREATEST (Transact-SQL)
[!INCLUDE [asdb-asdbmi](../../includes/applies-to-version/asdb-asdbmi.md)]

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
SELECT GREATEST ( '6.62', 3.1415, N'7' ) AS Greatest; 
GO 
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Greatest 
-------- 
  7.0000 

(1 rows affected)  
```  

### B. Simple example with character types

 The following example returns the maximum value from the list of character constants that is provided.  
  
```sql  
SELECT GREATEST ('Glacier', N'Joshua Tree', 'Mount Rainier') AS Greatest;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Greatest 
------------- 
Mount Rainier 

(1 rows affected)  
```  

### C. Simple example with table
  
 This example returns the maximum value from a list of column arguments and ignores `NULL` values during comparison. 
  
```sql  
USE AdventureWorks2019; 
GO 

SELECT sp.SalesQuota, sp.SalesYTD, sp.SalesLastYear 
      , GREATEST(sp.SalesQuota, sp.SalesYTD, sp.SalesLastYear) AS Greatest 
FROM sales.SalesPerson AS sp 
WHERE sp.SalesYTD < 3000000; 
GO  
  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
SalesQuota            SalesYTD              SalesLastYear         Greatest 

--------------------- --------------------- --------------------- --------------------- 
                 NULL           559697.5639                 .0000           559697.5639 
          250000.0000          1453719.4653          1620276.8966          1620276.8966 
          300000.0000          2315185.6110          1849640.9418          2315185.6110 
          250000.0000          1352577.1325          1927059.1780          1927059.1780 
          250000.0000          2458535.6169          2073505.9999          2458535.6169 
          250000.0000          2604540.7172          2038234.6549          2604540.7172 
          250000.0000          1573012.9383          1371635.3158          1573012.9383 
          300000.0000          1576562.1966                 .0000          1576562.1966 
                 NULL           172524.4512                 .0000           172524.4512 
          250000.0000          1421810.9242          2278548.9776          2278548.9776 
                 NULL           519905.9320                 .0000           519905.9320 
          250000.0000          1827066.7118          1307949.7917          1827066.7118 

(12 rows affected)
  
```  
### D. Using `GREATEST` with local variables

 This example uses `GREATEST` to determine the maximum value of a list of local variables within the predicate of a `WHERE` clause. 
  
```sql  
CREATE TABLE dbo.studies (    
    Variable varchar(10) NOT NULL,    
    Correlation decimal(4, 3) NULL 
); 

INSERT INTO dbo.studies VALUES ('Var1', 0.2), ('Var2', 0.825), ('Var3', 0.61); 
GO 

DECLARE @PredictionA DECIMAL(2,1) = 0.7;  
DECLARE @PredictionB DECIMAL(3,1) = 0.65;  

SELECT Variable, Correlation  
FROM dbo.studies 
WHERE Correlation > GREATEST(@PredictionA, @PredictionB); 
GO 
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Variable   Correlation 
---------- ----------- 
Var2              .825 

(1 rows affected)  
```  

### E. Using `GREATEST` with columns, constants, and variables

 This example uses `GREATEST` to determine the maximum value of a list that includes columns, constants, and variables. 
  
```sql  
CREATE TABLE dbo.products (    
    prod_id int IDENTITY(1,1),    
    listprice smallmoney NULL 
); 

INSERT INTO dbo.products VALUES (14.99), (49.99), (24.99); 
GO 

DECLARE @PriceX smallmoney = 19.99;  

SELECT GREATEST(listprice, 0, @PriceX) as Price  
FROM dbo.products;
GO 
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
GreatestPrice
-------------
      19.9900
      49.9900
      24.9900

(3 rows affected)  
```  

  
## See also  
 [LEAST &#40;Transact-SQL&#41;](../../t-sql/functions/logical-functions-least-transact-sql.md)  
 [MAX &#40;Transact-SQL&#41;](../../t-sql/functions/max-transact-sql.md)  
 [MIN &#40;Transact-SQL&#41;](../../t-sql/functions/min-transact-sql.md)  
 [CASE &#40;Transact-SQL&#41;](../../t-sql/language-elements/case-transact-sql.md)  
 [CHOOSE &#40;Transact-SQL&#41;](../../t-sql/functions/logical-functions-choose-transact-sql.md)  
  
  
