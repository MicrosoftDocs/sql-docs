---
title: "PATINDEX (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/19/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "PATINDEX"
  - "PATINDEX_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "first occurrence of pattern [SQL Server]"
  - "searches [SQL Server], pattern starting position"
  - "starting position of patten search"
  - "pattern searching [SQL Server]"
  - "PATINDEX function"
ms.assetid: c0dfb17f-2230-4e36-98da-a9b630bab656
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# PATINDEX (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the starting position of the first occurrence of a pattern in a specified expression, or zeros if the pattern is not found, on all valid text and character data types.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
PATINDEX ( '%pattern%' , expression )  
```  
  
## Arguments  
 *pattern*  
 Is a character expression that contains the sequence to be found. Wildcard characters can be used; however, the % character must come before and follow *pattern* (except when you search for first or last characters). *pattern* is an expression of the character string data type category. *pattern* is limited to 8000 characters.  
  
 *expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md), typically a column that is searched for the specified pattern. *expression* is of the character string data type category.  
  
## Return Types  
 **bigint** if *expression* is of the **varchar(max)** or **nvarchar(max)** data types; otherwise **int**.  
  
## Remarks  
 If either *pattern* or *expression* is NULL, PATINDEX returns NULL.  
  
 PATINDEX performs comparisons based on the collation of the input. To perform a comparison in a specified collation, you can use COLLATE to apply an explicit collation to the input.  
  
## Supplementary Characters (Surrogate Pairs)  
 When using SC collations, the return value will count any UTF-16 surrogate pairs in the *expression* parameter as a single character. For more information, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).  
  
 0x0000 (**char(0)**) is an undefined character in Windows collations and cannot be included in PATINDEX.  
  
## Examples  
  
### A. Simple PATINDEX example  
 The following example checks a short character string (`interesting data`) for the starting location of the characters `ter`.  
  
```  
SELECT PATINDEX('%ter%', 'interesting data');  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `3`  
  
### B. Using a pattern with PATINDEX  
 The following example finds the position at which the pattern `ensure` starts in a specific row of the `DocumentSummary` column in the `Document` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```  
SELECT PATINDEX('%ensure%',DocumentSummary)  
FROM Production.Document  
WHERE DocumentNode = 0x7B40;  
GO   
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
-----------  
64  
(1 row(s) affected)
```  
  
 If you do not restrict the rows to be searched by using a `WHERE` clause, the query returns all rows in the table and reports nonzero values for those rows in which the pattern was found, and zero for all rows in which the pattern was not found.  
  
### C. Using wildcard characters with PATINDEX  
 The following example uses % and _ wildcards to find the position at which the pattern `'en'`, followed by any one character and `'ure'` starts in the specified string (index starts at 1):  
  
```  
SELECT PATINDEX('%en_ure%', 'please ensure the door is locked');  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
-----------  
8  
```  
  
 `PATINDEX` works just like `LIKE`, so you can use any of the wildcards. You do not have to enclose the pattern between percents. `PATINDEX('a%', 'abc')` returns 1 and `PATINDEX('%a', 'cba')` returns 3.  
  
 Unlike `LIKE`, `PATINDEX` returns a position, similar to what `CHARINDEX` does.  
  
### D. Using COLLATE with PATINDEX  
 The following example uses the `COLLATE` function to explicitly specify the collation of the expression that is searched.  
  
```  
USE tempdb;  
GO  
SELECT PATINDEX ( '%ein%', 'Das ist ein Test'  COLLATE Latin1_General_BIN) ;  
GO  
```  
  
### E. Using a variable to specify the pattern  
 The following example uses a variable to pass a value to the *pattern* parameter. This example uses the  [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```  
DECLARE @MyValue varchar(10) = 'safety';   
SELECT PATINDEX('%' + @MyValue + '%', DocumentSummary)   
FROM Production.Document  
WHERE DocumentNode = 0x7B40;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 ------------  
 22
 ```  
  

  
## See Also  
 [LIKE &#40;Transact-SQL&#41;](../../t-sql/language-elements/like-transact-sql.md)   
 [CHARINDEX &#40;Transact-SQL&#41;](../../t-sql/functions/charindex-transact-sql.md)  
 [LEN &#40;Transact-SQL&#41;](../../t-sql/functions/len-transact-sql.md)  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)   
 [&#40;Wildcard - Character&#40;s&#41; to Match&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/wildcard-character-s-to-match-transact-sql.md)   
 [&#40;Wildcard - Character&#40;s&#41; Not to Match&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/wildcard-character-s-not-to-match-transact-sql.md)   
 [_ &#40;Wildcard - Match One Character&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/wildcard-match-one-character-transact-sql.md)   
 [Percent character &#40;Wildcard - Character&#40;s&#41; to Match&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/percent-character-wildcard-character-s-to-match-transact-sql.md)  
  
  


