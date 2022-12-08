---
title: "- (Negative) (Transact-SQL)"
description: "- (Negative) (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "03/13/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "negative"
helpviewer_keywords:
  - "- (negative)"
  - "negative operator (-)"
  - "negative values"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# Unary Operators - Negative
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the negative of the value of a numeric expression (a unary operator). Unary operators perform an operation on only one expression of any one of the data types of the numeric data type category.   
  
|Operator|Meaning|  
|--------------|-------------|  
|[+ (Positive)](../../t-sql/language-elements/unary-operators-positive.md)|Numeric value is positive.|  
|[- (Negative)](../../t-sql/language-elements/unary-operators-negative.md)|Numeric value is negative.|  
|[~ (Bitwise NOT)](../../t-sql/language-elements/bitwise-not-transact-sql.md)|Returns the ones complement of the number.|  
  
 The + (Positive) and - (Negative) operators can be used on any expression of any one of the data types of the numeric data type category. The ~ (Bitwise NOT) operator can be used only on expressions of any one of the data types of the integer data type category. 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
- numeric_expression  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *numeric_expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any one of the data types of the numeric data type category, except the date and time category.  
  
## Result Types  
 Returns the data type of *numeric_expression*, except that an unsigned **tinyint** expression is promoted to a signed **smallint** result.  
  
## Examples  
  
### A. Setting a variable to a negative value  
 The following example sets a variable to a negative value.  
  
```sql 
USE tempdb;  
GO  
DECLARE @MyNumber DECIMAL(10,2);  
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
  
```sql  
USE tempdb;  
GO  
DECLARE @Num1 INT;  
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
  
```sql  
USE ssawPDW;  
  
SELECT TOP (1) - 17 FROM DimEmployee;  
```  
  
 Returns  
  
```  
-17  
```  
  
### D. Returning the positive of a negative constant  
 The following example returns the positive of a negative constant.  
  
```sql  
USE ssawPDW;  
  
SELECT TOP (1) - ( - 17) FROM DimEmployee;  
```  
  
 Returns  
  
```  
17  
```  
  
### E. Returning the negative of a column  
 The following example returns the negative of the `BaseRate` value for each employee in the `dimEmployee` table.  
  
```sql  
USE ssawPDW;  
  
SELECT - BaseRate FROM DimEmployee;  
```  
  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)  
  
  

