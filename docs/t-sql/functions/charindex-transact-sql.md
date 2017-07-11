---
title: "CHARINDEX (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/08/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "CHARINDEX"
  - "CHARINDEX_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "expressions [SQL Server], pattern searching"
  - "CHARINDEX function"
  - "pattern searching [SQL Server]"
  - "starting point of expression in character string"
ms.assetid: 78c10341-8373-4b30-b404-3db20e1a3ac4
caps.latest.revision: 52
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CHARINDEX (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Searches an expression for another expression and returns its starting position if found.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
CHARINDEX ( expressionToFind , expressionToSearch [ , start_location ] )   
```  
  
## Arguments  
 *expressionToFind*  
 Is a character [expression](../../t-sql/language-elements/expressions-transact-sql.md) that contains the sequence to be found. *expressionToFind* is limited to 8000 characters.  
  
 *expressionToSearch*  
 Is a character expression to be searched.  
  
 *start_location*  
 Is an **integer** or **bigint** expression at which the search starts. If *start_location* is not specified, is a negative number, or is 0, the search starts at the beginning of *expressionToSearch*.  
  
## Return Types  
 **bigint** if *expressionToSearch* is of the **varchar(max)**, **nvarchar(max)**, or **varbinary(max)** data types; otherwise, **int**.  
  
## Remarks  
 If either *expressionToFind* or *expressionToSearch* is of a Unicode data type (**nvarchar** or **nchar**) and the other is not, the other is converted to a Unicode data type. CHARINDEX cannot be used with **text**, **ntext**, and **image** data types.  
  
 If either *expressionToFind* or *expressionToSearch* is NULL, CHARINDEX returns NULL.  
  
 If *expressionToFind* is not found within *expressionToSearch*, CHARINDEX returns 0.  
  
 CHARINDEX performs comparisons based on the collation of the input. To perform a comparison in a specified collation, you can use COLLATE to apply an explicit collation to the input.  
  
 The starting position returned is 1-based, not 0-based.  
  
 0x0000 (**char(0)**) is an undefined character in Windows collations and cannot be included in CHARINDEX.  
  
## Supplementary Characters (Surrogate Pairs)  
 When using SC collations, both *start_location* and the return value count surrogate pairs as one character, not two. For more information, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).  
  
## Examples  
  
### A. Returning the starting position of an expression  
 The following example returns the position at which the sequence of characters `bicycle` starts in the `DocumentSummary` column of the `Document` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```  
DECLARE @document varchar(64);  
SELECT @document = 'Reflectors are vital safety' +  
                   ' components of your bicycle.';  
SELECT CHARINDEX('bicycle', @document);  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
-----------   
48            
```  
  
### B. Searching from a specific position  
 The following example uses the optional *start_location* parameter to start looking for `vital` at the fifth character of the `DocumentSummary` column in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```  
DECLARE @document varchar(64);  
  
SELECT @document = 'Reflectors are vital safety' +  
                   ' components of your bicycle.';  
SELECT CHARINDEX('vital', @document, 5);  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
-----------   
16            
  
(1 row(s) affected)  
```  
  
### C. Searching for a nonexistent expression  
 The following example shows the result set when *expressionToFind* is not found within *expressionToSearch*.  
  
```  
DECLARE @document varchar(64);  
  
SELECT @document = 'Reflectors are vital safety' +  
                   ' components of your bicycle.';  
SELECT CHARINDEX('bike', @document);  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `-----------`  
  
 `0`  
  
 `(1 row(s) affected)`  
  
### D. Performing a case-sensitive search  
 The following example performs a case-sensitive search for the string `'TEST'` in `'This is a Test``'`.  
  
```  
USE tempdb;  
GO  
--perform a case sensitive search  
SELECT CHARINDEX ( 'TEST',  
       'This is a Test'  
       COLLATE Latin1_General_CS_AS);  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `-----------`  
  
 `0`  
  
 The following example performs a case-sensitive search for the string `'Test'` in `'This is a Test'`.  
  
```  
  
USE tempdb;  
GO  
SELECT CHARINDEX ( 'Test',  
       'This is a Test'  
       COLLATE Latin1_General_CS_AS);  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `-----------`  
  
 `13`  
  
### E. Performing a case-insensitive search  
 The following example performs a case-insensitive search for the string `'TEST'` in `'This is a Test'`.  
  
```  
  
USE tempdb;  
GO  
SELECT CHARINDEX ( 'TEST',  
       'This is a Test'  
       COLLATE Latin1_General_CI_AS);  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `-----------`  
  
 `13`  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### F. Searching from the start of a string expression  
 The following example returns the first location of the `is` string in `This is a string`, starting from position 1 (the first character) in the string.  
  
```  
SELECT CHARINDEX('is', 'This is a string');  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `---------`  
  
 `3`  
  
### G. Searching from a position other than the first position  
 The following example returns the first location of the `is` string in `This is a string`, starting with the fourth position.  
  
```  
SELECT CHARINDEX('is', 'This is a string', 4);  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `---------`  
  
 `6`  
  
### H. Results when the string is not found  
 The following example shows the return value when the *string_pattern* is not found in the searched string.  
  
```  
SELECT TOP(1) CHARINDEX('at', 'This is a string') FROM dbo.DimCustomer;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `---------`  
  
 `0`  
  
## See Also  
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)   
 [+ &#40;String Concatenation&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/string-concatenation-transact-sql.md)   
 [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)  
  
  


