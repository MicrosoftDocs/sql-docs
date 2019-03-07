---
title: "Backslash (Line Continuation) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/09/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "\\_TSQL"
  - "\\"
dev_langs: 
  - "TSQL"
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
ms.assetid: c97fbb20-3d12-4d0b-9b52-62a229bc83c0
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Backslash (Line Continuation) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

`\`  breaks a long string constant, character or binary, into two or more lines for readability.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
<first section of string> \  
<continued section of string>  
```  
  
## Arguments  
 \<first section of string>  
 Is the start of a string.  
  
 \<continued section of string>  
 Is the continuation of a string.  
  
## Remarks  
 This command returns the first and continued sections of the string as one string, without the backslash.  

## Examples  

### A. Splitting a character string  

The following example uses a backslash and a carriage return to split a character string into two lines.  
  
```  
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

```  
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
  
  
