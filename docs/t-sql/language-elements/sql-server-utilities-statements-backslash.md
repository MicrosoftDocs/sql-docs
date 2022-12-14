---
title: "Backslash (Line Continuation) (Transact-SQL)"
description: "Backslash (Line Continuation) (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "07/25/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "\\_TSQL"
  - "\\"
helpviewer_keywords:
  - "backwhack"
  - "backslash"
  - "escape character"
  - "hack character"
  - "\\ (backslash)"
  - "backslant"
  - "bash"
  - "reverse slant"
  - "slosh"
  - "reversed virgule"
  - "line continuation character"
  - "reverse solidus"
dev_langs:
  - "TSQL"
---
# Backslash (Line Continuation) (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

`\`  breaks a long string constant, character or binary, into two or more lines for readability.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
<first section of string> \  
<continued section of string>  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 \<first section of string>  
 Is the start of a string.  
  
 \<continued section of string>  
 Is the continuation of a string.  
  
## Remarks  
This command returns the first and continued sections of the string as one string, without the backslash. The new line after the backslash must either be a line feed character (U+000A) or a combination of carriage return (U+000D) and line feed (U+000A) in that order. 

## Examples  

### A. Splitting a character string  

The following example uses a backslash and a carriage return to split a character string into two lines.  
  
```sql  
SELECT 'abc\  
def' AS [ColumnResult];  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```  
 ColumnResult  
 ------------  
 abcdef
 ```    

### B. Splitting a binary string  

The following example uses a backslash and a carriage return to split a binary string into two lines.  

```sql  
SELECT 0xabc\
def AS [ColumnResult];  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```  
 ColumnResult  
 ------------  
 0xABCDEF
 ```    

## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [&#40;Division&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/divide-transact-sql.md)   
 [&#40;Division Assignment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/divide-equals-transact-sql.md)   
 [Compound Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/compound-operators-transact-sql.md)  
  
  
