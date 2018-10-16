---
title: "ASCII (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ASCII_TSQL"
  - "ASCII"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ASCII function"
  - "characters [SQL Server], ASCII"
  - "code [SQL Server], ASCII"
  - "leftmost character of expression"
ms.assetid: 45c2044a-0593-4805-8bae-0fad4bde2e6b
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ASCII (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Returns the ASCII code value of the leftmost character of a character expression.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
ASCII ( character_expression )  
```  
  
## Arguments  
*character_expression*  
An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of type **char** or **varchar**.
  
## Return types
 **int**  
  
## Remarks
ASCII stands for **A**merican **S**tandard **C**ode for **I**nformation **I**nterchange. It serves as a character encoding standard for modern computers. See the **Printable characters** section of [ASCII](https://www.wikipedia.org/wiki/ASCII) for a list of ASCII characters.

## Examples  
This example assumes an ASCII character set, and returns the `ASCII` value for 6 characters.
  
```sql
SELECT ASCII('A') AS A, ASCII('B') AS B,   
ASCII('a') AS a, ASCII('b') AS b,  
ASCII(1) AS [1], ASCII(2) AS [2];  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
A           B           a           b           1           2  
----------- ----------- ----------- ----------- ----------- -----------  
65          66          97          98          49          50  
```  
  
## See also
 [CHAR &#40;Transact-SQL&#41;](../../t-sql/functions/char-transact-sql.md)  
 [NCHAR &#40;Transact-SQL&#41;](../../t-sql/functions/nchar-transact-sql.md)  
 [UNICODE &#40;Transact-SQL&#41;](../../t-sql/functions/unicode-transact-sql.md)  
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)
  
  


