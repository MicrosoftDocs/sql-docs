---
title: "CHARINDEX (Transact-SQL)"
description: "Transact-SQL reference for the CHARINDEX function."
author: markingmyname
ms.author: maghan
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CHARINDEX"
  - "CHARINDEX_TSQL"
helpviewer_keywords:
  - "expressions [SQL Server], pattern searching"
  - "CHARINDEX function"
  - "pattern searching [SQL Server]"
  - "starting point of expression in character string"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# CHARINDEX (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function searches for one character expression inside a second character expression, returning the starting position of the first expression if found.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
CHARINDEX ( expressionToFind , expressionToSearch [ , start_location ] )   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*expressionToFind*  
A character [expression](../../t-sql/language-elements/expressions-transact-sql.md) containing the sequence to find. *expressionToFind* has an 8000 character limit.
  
*expressionToSearch*  
A character expression to search.
  
*start_location*  
An **integer** or **bigint** expression at which the search starts. If *start_location* is not specified, has a negative value, or has a zero (0) value, the search starts at the beginning of *expressionToSearch*.
  
## Return types
**bigint** if *expressionToSearch* has an **nvarchar(max)**, **varbinary(max)**, or **varchar(max)** data type; **int** otherwise.
  
## Remarks  
If either the *expressionToFind* or *expressionToSearch* expression has a Unicode data type (**nchar** or **nvarchar**), and the other expression does not, the CHARINDEX function converts that other expression to a Unicode data type. CHARINDEX cannot be used with **image**, **ntext**, or **text** data types.
  
If either the *expressionToFind* or *expressionToSearch* expression has a NULL value, CHARINDEX returns NULL.
  
If CHARINDEX does not find *expressionToFind* within *expressionToSearch*, CHARINDEX returns 0.
  
CHARINDEX performs comparisons based on the input collation. To perform a comparison in a specified collation, use COLLATE to apply an explicit collation to the input.
  
The starting position returned is 1-based, not 0-based.
  
0x0000 (**char(0)**) is an undefined character in Windows collations and cannot be included in CHARINDEX.
  
## Supplementary Characters (Surrogate Pairs)  
When using SC collations, both *start_location* and the return value count surrogate pairs as one character, not two. For more information, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).
  
## Examples  
  
### A. Returning the starting position of an expression  
This example searches for `bicycle` in the searched string value variable `@document`.
  
```sql
DECLARE @document VARCHAR(64);  
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
This example uses the optional *start_location* parameter to start the search for `vital` at the fifth character of the searched string value variable `@document`.
  
```sql
DECLARE @document VARCHAR(64);  
  
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
This example shows the result set when CHARINDEX does not find *expressionToFind* within *expressionToSearch*.
  
```sql
DECLARE @document VARCHAR(64);  
  
SELECT @document = 'Reflectors are vital safety' +  
                   ' components of your bicycle.';  
SELECT CHARINDEX('bike', @document);  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
-----------
0
  
(1 row(s) affected)
```
  
### D. Performing a case-sensitive search  
This example shows a case-sensitive search for the string `'TEST'` in searched string `'This is a Test``'`.
  
```sql
USE tempdb;  
GO  
--perform a case sensitive search  
SELECT CHARINDEX ( 'TEST',  
       'This is a Test'  
       COLLATE Latin1_General_CS_AS);  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
-----------
0
```  
  
This example shows a case-sensitive search for the string `'Test'` in `'This is a Test'`.
  
```sql
  
USE tempdb;  
GO  
SELECT CHARINDEX ( 'Test',  
       'This is a Test'  
       COLLATE Latin1_General_CS_AS);  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
-----------
11
```  
  
### E. Performing a case-insensitive search  
This example shows a case-insensitive search for the string `'TEST'` in `'This is a Test'`.
  
```sql
USE tempdb;  
GO  
SELECT CHARINDEX ( 'TEST',  
       'This is a Test'  
       COLLATE Latin1_General_CI_AS);  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
-----------
11
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### F. Searching from the start of a string expression  
This example returns the first location of the string `is` in string `This is a string`, starting from position 1 (the first character) of `This is a string`.
  
```sql
SELECT CHARINDEX('is', 'This is a string');  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
---------
3
```  
  
### G. Searching from a position other than the first position  
This example returns the first location of the string `is` in string `This is a string`, starting the search from position 4 (the fourth character).
  
```sql
SELECT CHARINDEX('is', 'This is a string', 4);  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
---------
 6
 ```  
  
### H. Results when the string is not found  
This example shows the return value when CHARINDEX does not find string *string_pattern* in the searched string.
  
```sql
SELECT TOP(1) CHARINDEX('at', 'This is a string') FROM dbo.DimCustomer;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
---------
0
```  
  
## See also
 [LEN &#40;Transact-SQL&#41;](../../t-sql/functions/len-transact-sql.md)  
 [PATINDEX &#40;Transact-SQL&#41;](../../t-sql/functions/patindex-transact-sql.md)  
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  
 [+ &#40;String Concatenation&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/string-concatenation-transact-sql.md)  
 [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md)  
  
  


