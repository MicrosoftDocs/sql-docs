---
title: "- (Negative) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "negative"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "- (negative)"
  - "negative operator (-)"
  - "negative values"
ms.assetid: d6c14d14-d379-403b-82db-c197ad58c896
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Unary Operators - Negative
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the negative of the value of a numeric expression (a unary operator). Unary operators perform an operation on only one expression of any one of the data types of the numeric data type category.   
  
|Operator|Meaning|  
|--------------|-------------|  
|[+ (Positive)](../../t-sql/language-elements/unary-operators-positive.md)|Numeric value is positive.|  
|[- (Negative)](../../t-sql/language-elements/unary-operators-negative.md)|Numeric value is negative.|  
|[~ (Bitwise NOT)](../../t-sql/language-elements/bitwise-not-transact-sql.md)|Returns the ones complement of the number.|  
  
 The + (Positive) and - (Negative) operators can be used on any expression of any one of the data types of the numeric data type category. The ~ (Bitwise NOT) operator can be used only on expressions of any one of the data types of the integer data type category. 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
- numeric_expression  
```  
  
## Arguments  
 *numeric_expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any one of the data types of the numeric data type category, except the date and time category.  
  
## Result Types  
 Returns the data type of *numeric_expression*, except that an unsigned **tinyint** expression is promoted to a signed **smallint** result.  
  
## Examples  
  
### A. Setting a variable to a negative value  
 The following example sets a variable to a negative value.  
  
```  
USE tempdb;  
GO  
DECLARE @MyNumber decimal(10,2);  
SET @MyNumber = -123.45;  
SELECT @MyNumber AS NegativeValue;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
NegativeValue  
---------------------------------------  
-123.45  
  
(1 row(s) affected)  
  
```  
  
### B. Changing a variable to a negative value  
 The following example changes a variable to a negative value.  
  
```  
USE tempdb;  
GO  
DECLARE @Num1 int;  
SET @Num1 = 5;  
SELECT @Num1 AS VariableValue, -@Num1 AS NegativeValue;  
GO  
  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
VariableValue NegativeValue  
------------- -------------  
5             -5  
  
(1 row(s) affected)  
  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Returning the negative of a positive constant  
 The following example returns the negative of a positive constant.  
  
```  
USE ssawPDW;  
  
SELECT TOP (1) - 17 FROM DimEmployee;  
```  
  
 Returns  
  
```  
-17  
```  
  
### D. Returning the positive of a negative constant  
 The following example returns the positive of a negative constant.  
  
```  
USE ssawPDW;  
  
SELECT TOP (1) - ( - 17) FROM DimEmployee;  
```  
  
 Returns  
  
```  
17  
```  
  
### E. Returning the negative of a column  
 The following example returns the negative of the `BaseRate` value for each employee in the `dimEmployee` table.  
  
```  
USE ssawPDW;  
  
SELECT - BaseRate FROM DimEmployee;  
```  
  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)  
  
  

