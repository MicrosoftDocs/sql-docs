---
title: "RIGHT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "RIGHT_TSQL"
  - "RIGHT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "rightmost character of expression"
  - "RIGHT function"
  - "character strings [SQL Server], RIGHT"
ms.assetid: 43f1fe1f-aa18-47e3-ba20-e03e32254a6d
caps.latest.revision: 37
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# RIGHT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the right part of a character string with the specified number of characters.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
RIGHT ( character_expression , integer_expression )  
```  
  
## Arguments  
 *character_expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of character or binary data. *character_expression* can be a constant, variable, or column. *character_expression* can be of any data type, except **text** or **ntext**, that can be implicitly converted to **varchar** or **nvarchar**. Otherwise, use the [CAST](../../t-sql/functions/cast-and-convert-transact-sql.md) function to explicitly convert *character_expression*.  
  
 *integer_expression*  
 Is a positive integer that specifies how many characters of *character_expression* will be returned. If *integer_expression* is negative, an error is returned. If *integer_expression* is type **bigint** and contains a large value, *character_expression* must be of a large data type such as **varchar(max)**.  
  
## Return Types  
 Returns **varchar** when *character_expression* is a non-Unicode character data type.  
  
 Returns **nvarchar** when *character_expression* is a Unicode character data type.  
  
## Supplementary Characters (Surrogate Pairs)  
 When using SC collations, the RIGHT function counts a UTF-16 surrogate pair as a single character. For more information, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).  
  
## Examples  
  
### A: Using RIGHT with a column  
 The following example returns the five rightmost characters of the first name for each person in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```  
SELECT RIGHT(FirstName, 5) AS 'First Name'  
FROM Person.Person  
WHERE BusinessEntityID < 5  
ORDER BY FirstName;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
First Name  
----------  
Ken  
Terri  
berto  
Rob  
  
(4 row(s) affected)  
  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### B. Using RIGHT with a column  
 The following example returns the five rightmost characters of each last name in the `DimEmployee` table.  
  
```  
-- Uses AdventureWorks  
  
SELECT RIGHT(LastName, 5) AS Name  
FROM dbo.DimEmployee  
ORDER BY EmployeeKey;  
```  
  
 Here is a partial result set.  
  
 `Name`  
  
 `-----`  
  
 `lbert`  
  
 `Brown`  
  
 `rello`  
  
 `lters`  
  
### C. Using RIGHT with a character string  
 The following example uses `RIGHT` to return the two rightmost characters of the character string `abcdefg`.  
  
```  
-- Uses AdventureWorks  
  
SELECT TOP(1) RIGHT('abcdefg',2) FROM dbo.DimProduct;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `-------`  
  
 `fg`  
  
## See Also  
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  
  
  

