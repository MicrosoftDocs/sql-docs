---
title: "PATINDEX (Transact-SQL)"
description: "PATINDEX (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "07/19/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "PATINDEX"
  - "PATINDEX_TSQL"
helpviewer_keywords:
  - "first occurrence of pattern [SQL Server]"
  - "searches [SQL Server], pattern starting position"
  - "starting position of patten search"
  - "pattern searching [SQL Server]"
  - "PATINDEX function"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# PATINDEX (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns the starting position of the first occurrence of a pattern in a specified expression, or zero if the pattern is not found, on all valid text and character data types.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
PATINDEX ( '%pattern%' , expression )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *pattern*  
 Is a character expression that contains the sequence to be found. Wildcard characters can be used; however, the % character must come before and follow *pattern* (except when you search for first or last characters). *pattern* is an expression of the character string data type category. *pattern* is limited to 8000 characters.

 > [!NOTE]
 > While traditional regular expressions are not natively supported in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], similar complex pattern matching can be achieved by using various wildcard expressions. See the [String Operators](../../t-sql/language-elements/string-operators-transact-sql.md) documentation for more detail on wildcard syntax.
  
 *expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md), typically a column that is searched for the specified pattern. *expression* is of the character string data type category.  
  
## Return Types  
**bigint** if *expression* is of the **varchar(max)** or **nvarchar(max)** data types; otherwise **int**.  
  
## Remarks  
If either *pattern* or *expression* is NULL, PATINDEX returns NULL.  
 
The starting position for PATINDEX is 1.
 
PATINDEX performs comparisons based on the collation of the input. To perform a comparison in a specified collation, you can use COLLATE to apply an explicit collation to the input.  
  
## Supplementary Characters (Surrogate Pairs)  
When using SC collations, the return value will count any UTF-16 surrogate pairs in the *expression* parameter as a single character. For more information, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).  
  
0x0000 (**char(0)**) is an undefined character in Windows collations and cannot be included in PATINDEX.  
  
## Examples  
  
### A. Simple PATINDEX example  
 The following example checks a short character string (`interesting data`) for the starting location of the characters `ter`.  
  
```sql  
SELECT position = PATINDEX('%ter%', 'interesting data');  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  

```
position
--------
3
```
  
### B. Using a pattern with PATINDEX  
The following example finds the position at which the pattern `ensure` starts in a specific row of the `DocumentSummary` column in the `Document` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```sql  
SELECT position = PATINDEX('%ensure%',DocumentSummary)  
FROM Production.Document  
WHERE DocumentNode = 0x7B40;  
GO   
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
position
--------  
64  
```  
  
If you do not restrict the rows to be searched by using a `WHERE` clause, the query returns all rows in the table and reports nonzero values for those rows in which the pattern was found, and zero for all rows in which the pattern was not found.  
  
### C. Using wildcard characters with PATINDEX  
 The following example uses % and _ wildcards to find the position at which the pattern `'en'`, followed by any one character and `'ure'` starts in the specified string (index starts at 1):  
  
```sql  
SELECT position = PATINDEX('%en_ure%', 'Please ensure the door is locked!');  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
position
--------  
8  
```  
  
`PATINDEX` works just like `LIKE`, so you can use any of the wildcards. You do not have to enclose the pattern between percents. `PATINDEX('a%', 'abc')` returns 1 and `PATINDEX('%a', 'cba')` returns 3.  
  
 Unlike `LIKE`, `PATINDEX` returns a position, similar to what `CHARINDEX` does.  

### D. Using complex wildcard expressions with PATINDEX 
The following example uses the `[^]` [string operator](../../t-sql/language-elements/wildcard-character-s-not-to-match-transact-sql.md) to find the position of a character that is not a number, letter, or space.

```sql
SELECT position = PATINDEX('%[^ 0-9A-Za-z]%', 'Please ensure the door is locked!'); 
```
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  

```
position
--------
33
```

### E. Using COLLATE with PATINDEX  
 The following example uses the `COLLATE` function to explicitly specify the collation of the expression that is searched.  
  
```sql  
USE tempdb;  
GO  
SELECT PATINDEX ( '%ein%', 'Das ist ein Test'  COLLATE Latin1_General_BIN) ;  
GO  
```  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  

```
position
--------
9
```

### F. Using a variable to specify the pattern  
The following example uses a variable to pass a value to the *pattern* parameter. This example uses the  [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```sql  
DECLARE @MyValue VARCHAR(10) = 'safety';   
SELECT position = PATINDEX('%' + @MyValue + '%', DocumentSummary)   
FROM Production.Document  
WHERE DocumentNode = 0x7B40;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
position
--------  
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
  
  


