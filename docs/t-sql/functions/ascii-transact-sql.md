---
title: "ASCII (Transact-SQL)"
description: "ASCII (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "11/14/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ASCII_TSQL"
  - "ASCII"
helpviewer_keywords:
  - "ASCII function"
  - "characters [SQL Server], ASCII"
  - "code [SQL Server], ASCII"
  - "leftmost character of expression"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# ASCII (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Returns the ASCII code value of the leftmost character of a character expression.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
ASCII ( character_expression )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*character_expression*  
An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of type **char** or **varchar**.
  
## Return types
 **int**  
  
## Remarks
ASCII stands for **A**merican **S**tandard **C**ode for **I**nformation **I**nterchange. It serves as a character encoding standard for modern computers. See the **Printable characters** section of [ASCII](https://www.wikipedia.org/wiki/ASCII) for a list of ASCII characters.

ASCII is a 7-bit character set. Extended ASCII or High ASCII is an 8-bit character set that is not handled by the `ASCII` function. 

## Examples 

### A. This example assumes an ASCII character set, and returns the `ASCII` value for 6 characters.
  
```sql
SELECT ASCII('A') AS A, ASCII('B') AS B,   
ASCII('a') AS a, ASCII('b') AS b,  
ASCII(1) AS [1], ASCII(2) AS [2];  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```
A           B           a           b           1           2  
----------- ----------- ----------- ----------- ----------- -----------  
65          66          97          98          49          50  
```  
  
### B. This examples shows how a 7-bit ASCII value is returned correctly, but an 8-bit Extended ASCII value is not handled.

```sql
SELECT ASCII('P') AS [ASCII], ASCII('æ') AS [Extended_ASCII];
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```
ASCII       Extended_ASCII
----------- --------------
80          195
```

To verify if the results above map to the correct character code point, use the output values with the `CHAR` or `NCHAR` function:

```sql
SELECT NCHAR(80) AS [CHARACTER], NCHAR(195) AS [CHARACTER];
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```
CHARACTER CHARACTER
--------- ---------
P         Ã
```

From the previous result, notice that the character for code point 195 is **Ã** and not **æ**. This is because the `ASCII` function is capable of reading the first 7-bit stream, but not the extra bit. The correct code point for character `æ` can be found using the `UNICODE` function, which is capable or returning the correct character code point:

```sql
SELECT UNICODE('æ') AS [Extended_ASCII], NCHAR(230) AS [CHARACTER];
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```
Extended_ASCII CHARACTER
-------------- ---------
230            æ
```

## See also
 [CHAR &#40;Transact-SQL&#41;](../../t-sql/functions/char-transact-sql.md)  
 [NCHAR &#40;Transact-SQL&#41;](../../t-sql/functions/nchar-transact-sql.md)  
 [UNICODE &#40;Transact-SQL&#41;](../../t-sql/functions/unicode-transact-sql.md)  
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)
  
