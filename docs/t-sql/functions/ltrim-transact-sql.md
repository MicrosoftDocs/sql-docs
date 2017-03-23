---
title: "LTRIM (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/27/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "LTRIM"
  - "LTRIM_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "leading blanks"
  - "deleting blank spaces"
  - "characters [SQL Server], blanks"
  - "removing blank spaces"
  - "LTRIM function"
  - "blank characters [SQL Server]"
ms.assetid: 369ed340-1a09-4597-a9eb-6720156cd39a
caps.latest.revision: 36
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# LTRIM (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns a character expression after it removes leading blanks.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
LTRIM ( character_expression )  
```  
  
## Arguments  
 *character_expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of character or binary data. *character_expression* can be a constant, variable, or column. *character_expression* must be of a data type, except **text**, **ntext**, and **image**, that is implicitly convertible to **varchar**. Otherwise, use [CAST](../../t-sql/functions/cast-and-convert-transact-sql.md) to explicitly convert *character_expression*.  
  
## Return Type  
 **varchar** or **nvarchar**  
  
## Examples  

### A. Simple example   

 The following example uses LTRIM to remove leading spaces from a character expression.  
  
```tsql  
SELECT LTRIM('     Five spaces are at the beginning of this string.') FROM sys.databases;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `---------------------------------------------------------------`  
  `Five spaces are at the beginning of this string.`  

### B: Example using a variable   
  
 The following example uses `LTRIM` to remove leading spaces from a character variable.  
  
```  
DECLARE @string_to_trim varchar(60);  
SET @string_to_trim = '     5 spaces are at the beginning of this string.';  
SELECT 
    @string_to_trim AS 'Original string',
    LTRIM(@string_to_trim) AS 'Without spaces';  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
Original string	Without spaces
--------------------------------------------------- ---------------------------------------------
     5 spaces are at the beginning of this string.	5 spaces are at the beginning of this string.
```  
  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  
  
  


