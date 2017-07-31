---
title: "CHAR (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "char_TSQL"
  - "char"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "converting int ACSII code to character"
  - "control characters"
  - "tab"
  - "ASCII conversions"
  - "CHAR function"
  - "carriage return"
  - "inserting control characters"
  - "characters [SQL Server], control"
  - "line feed"
  - "printing ASCII values"
ms.assetid: 955afe94-539c-465d-af22-16ec45da432a
caps.latest.revision: 39
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CHAR (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Converts an **int** ASCII code to a character.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
CHAR ( integer_expression )  
```  
  
## Arguments  
*integer_expression*  
Is an integer from 0 through 255. `NULL` is returned if the integer expression is not in this range.
  
## Return types
**char(1)**
  
## Remarks  
`CHAR` can be used to insert control characters into character strings. The following table shows some frequently used control characters.
  
|Control character|Value|  
|---|---|
|Tab|**char(9)**|  
|Line feed|**char(10)**|  
|Carriage return|**char(13)**|  
  
## Examples  
  
### A. Using ASCII and CHAR to print ASCII values from a string  
The following example prints the ASCII value and character for each character in the string `New Moon`.
  
```sql
SET TEXTSIZE 0;  
-- Create variables for the character string and for the current   
-- position in the string.  
DECLARE @position int, @string char(8);  
-- Initialize the current position and the string variables.  
SET @position = 1;  
SET @string = 'New Moon';  
WHILE @position <= DATALENGTH(@string)  
   BEGIN  
   SELECT ASCII(SUBSTRING(@string, @position, 1)),   
      CHAR(ASCII(SUBSTRING(@string, @position, 1)))  
   SET @position = @position + 1  
   END;  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
----------- -
78          N  
----------- -  
101         e  
----------- -  
119         w  
----------- -  
32  
----------- -  
77          M  
----------- -  
111         o  
----------- -  
111         o  
----------- - 
110         n  
```
  
### B. Using CHAR to insert a control character  
The following example uses `CHAR(13)` to print the name and e-mail address of an employee on separate lines when the results are returned in text. This example uses the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.
  
```sql
SELECT p.FirstName + ' ' + p.LastName, + CHAR(13)  + pe.EmailAddress   
FROM Person.Person p JOIN Person.EmailAddress pe  
ON p.BusinessEntityID = pe.BusinessEntityID  
AND p.BusinessEntityID = 1;  
GO  
```sql
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
`Ken Sanchez`
  
`ken0@adventure-works.com`
  
`(1 row(s) affected)`
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Using ASCII and CHAR to print ASCII values from a string  
The following example assumes an ASCII character set and returns the character value for 6 ASCII character numbers.
  
```sql
SELECT CHAR(65) AS [65], CHAR(66) AS [66],   
CHAR(97) AS [97], CHAR(98) AS [98],   
CHAR(49) AS [49], CHAR(50) AS [50];  
```
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
65   66   97   98   49   50  
---- ---- ---- ---- ---- ----  
A    B    a    b    1    2  
```
  
### D. Using CHAR to insert a control character  
The following example uses `CHAR(13)` to return information about the databases on separate lines when the results are returned in text.
  
```sql
SELECT name, 'was created on ', create_date, CHAR(13), name, 'is currently ', state_desc   
FROM sys.databases;  
GO  
```
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
name     create_date    name    state_desc  
------------------------------------------------------------  
master                   was created on  2003-04-08 09:13:36.390   
master                   is currently  ONLINE  
tempdb                   was created on  2014-01-10 17:24:24.023   
tempdb                   is currently  ONLINE  
AdventureWorksPDW2012    was created on  2014-05-07 09:05:07.083 
AdventureWorksPDW2012    is currently  ONLINE  
```
  
## See also
[+ &#40;String Concatenation&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/string-concatenation-transact-sql.md)  
[String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)
  
  

