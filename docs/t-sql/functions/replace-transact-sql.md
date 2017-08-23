---
title: "REPLACE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/23/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "REPLACE_TSQL"
  - "REPLACE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "first string expression [SQL Server]"
  - "replacing string expression"
  - "third string expressions [SQL Server]"
  - "second string expressions [SQL Server]"
  - "REPLACE function"
ms.assetid: 8a7aaaf2-62e3-46c0-8e44-fa22290dd86b
caps.latest.revision: 39
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# REPLACE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Replaces all occurrences of a specified string value with another string value.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
REPLACE ( string_expression , string_pattern , string_replacement )  
```  
  
## Arguments  
 *string_expression*  
 Is the string [expression](../../t-sql/language-elements/expressions-transact-sql.md) to be searched. *string_expression* can be of a character or binary data type.  
  
 *string_*pattern  
 Is the substring to be found. *string_pattern* can be of a character or binary data type. *string_pattern* cannot be an empty string (''), and must not exceed the maximum number of bytes that fits on a page.  
  
 *string_*replacement  
 Is the replacement string. *string_replacement* can be of a character or binary data type.  
  
## Return Types  
 Returns **nvarchar** if one of the input arguments is of the **nvarchar** data type; otherwise, REPLACE returns **varchar**.  
  
 Returns NULL if any one of the arguments is NULL.  
  
 If *string_expression* is not of type **varchar(max)** or **nvarchar(max),REPLACE** truncates the return value at 8,000 bytes. To return values greater than 8,000 bytes, *string_expression* must be explicitly cast to a large-value data type.  
  
## Remarks  
 REPLACE performs comparisons based on the collation of the input. To perform a comparison in a specified collation, you can use [COLLATE](~/t-sql/statements/collations.md) to apply an explicit collation to the input.  
  
 0x0000 (**char(0)**) is an undefined character in Windows collations and cannot be included in REPLACE.  
  
## Examples  
 The following example replaces the string `cde` in `abcdefghi` with `xxx`.  
  
```sql  
SELECT REPLACE('abcdefghicde','cde','xxx');  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
------------  
abxxxfghixxx  
(1 row(s) affected)  
```  
  
 The following example uses the `COLLATE` function.  
  
```sql  
SELECT REPLACE('This is a Test'  COLLATE Latin1_General_BIN,  
'Test', 'desk' );  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
------------  
This is a desk  
(1 row(s) affected)  
```  

  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  
  
