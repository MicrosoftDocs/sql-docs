---
title: "+= (String Concatenation and Assignment) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/07/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "concatenate strings"
  - "string concatenation"
  - "+= (concatenate operator)"
ms.assetid: 4aaeaab7-9b2b-48e0-8487-04ed672ebcb1
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# += (String Concatenation Assignment) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Concatenates two strings and sets the string to the result of the operation. For example, if a variable @x equals 'Adventure', then @x += 'Works' takes the original value of @x, adds 'Works' to the string, and sets @x to that new value 'AdventureWorks'.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
expression += expression  
```  
  
## Arguments  
 *expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any of the character data types.  
  
## Result Types  
 Returns the data type that is defined for the variable.  
  
## Remarks  
 SET @v1 += 'expression' is equivalent to SET @v1 = @v1 + ('expression'). Also, SET @v1 = @v2 + @v3 + @v4 is equivalent to SET @v1 = (@v2 + @v3) + @v4.  
  
 The += operator cannot be used without a variable. For example, the following code will cause an error:  
  
```  
SELECT 'Adventure' += 'Works'  
```  
  
## Examples  
### A. Concatenation using += operator
 The following example concatenates using the `+=` operator.  
  
```  
DECLARE @v1 varchar(40);  
SET @v1 = 'This is the original.';  
SET @v1 += ' More text.';  
PRINT @v1;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `This is the original. More text.`  
  
### B. Order of evaluation while concatenating using += operator
The following example concatenates multiple strings to form one long string and then tries to compute the length of the final string. This example demonstrates the evaluation order and truncation rules, while using the concatenation operator. 

```
DECLARE @x varchar(4000) = replicate('x', 4000)
DECLARE @z varchar(8000) = replicate('z',8000)
DECLARE @y varchar(max);
 
SET @y = '';
SET @y += @x + @z;
SELECT LEN(@y) AS Y; -- 8000
 
SET @y = '';
SET @y = @y + @x + @z;
SELECT LEN(@y) AS Y; -- 12000
 
SET @y = '';
SET @y = @y +(@x + @z);
SELECT LEN(@y) AS Y; -- 8000
-- or
SET @y = '';
SET @y = @x + @z + @y;
SELECT LEN(@y) AS Y; -- 8000
GO
```
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 Y       
 ------- 
 8000 
  
 (1 row(s) affected) 
  
    
 Y       
 ------- 
 12000 
  
 (1 row(s) affected) 

 Y       
 ------- 
 8000 
  
 (1 row(s) affected) 
  
 Y       
 ------- 
 8000 
  
 (1 row(s) affected)
  ```   
   
## See Also  
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [+= &#40;Add Assignment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/add-equals-transact-sql.md)   
 [+ &#40;String Concatenation&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/string-concatenation-transact-sql.md)  
  
  
