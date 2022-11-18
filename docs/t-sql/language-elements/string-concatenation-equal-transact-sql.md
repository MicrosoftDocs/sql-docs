---
title: "+= String concatenation"
description: Concatenate two strings and set the string to the result of the operation.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "12/07/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "concatenate strings"
  - "string concatenation"
  - "+= (concatenate operator)"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---

# += (String Concatenation Assignment) (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Concatenates two strings and sets the string to the result of the operation. For example, if a variable @x equals 'Adventure', then @x += 'Works' takes the original value of @x, adds 'Works' to the string, and sets @x to that new value 'AdventureWorks'.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
 
## Syntax  
  
```syntaxsql
expression += expression  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any of the character data types.  
  
## Result Types  
 Returns the data type that is defined for the variable.  
  
## Remarks  
 SET @v1 += 'expression' is equivalent to SET @v1 = @v1 + ('expression'). Also, SET @v1 = @v2 + @v3 + @v4 is equivalent to SET @v1 = (@v2 + @v3) + @v4.  
  
 The += operator cannot be used without a variable. For example, the following code will cause an error:  
  
```sql  
SELECT 'Adventure' += 'Works'  
```  
  
## Examples  
### A. Concatenation using += operator
 The following example concatenates using the `+=` operator.  
  
```sql  
DECLARE @v1 VARCHAR(40);  
SET @v1 = 'This is the original.';  
SET @v1 += ' More text.';  
PRINT @v1;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `This is the original. More text.`  
  
### B. Order of evaluation while concatenating using += operator
The following example concatenates multiple strings to form one long string and then tries to compute the length of the final string. This example demonstrates the evaluation order and truncation rules, while using the concatenation operator. 

```sql
DECLARE @x VARCHAR(4000) = REPLICATE('x', 4000)
DECLARE @z VARCHAR(8000) = REPLICATE('z',8000)
DECLARE @y VARCHAR(max);
 
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
  
  
