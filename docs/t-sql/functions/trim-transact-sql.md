---
title: "TRIM (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/20/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "TRIM"
  - "TRIM_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "TRIM function"
ms.assetid: a00245aa-32c7-4ad4-a0d1-64f3d6841153
caps.latest.revision: 4
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# TRIM (Transact-SQL)
[!INCLUDE[tsql-appliesto-ssvNxt-asdb-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-asdb-xxxx-xxx.md)]

Removes the space character `char(32)` or other specified characters from the start or end of a string.  
 
## Syntax   
```
TRIM ( [ characters FROM ] string ) 
```
[//]: # "[ BOTH | LEADING | TRAILING ] not yet available."

## Arguments   

characters   
Is a literal, variable, or function call of any non-LOB character type (`nvarchar`, `varchar`, `nchar`, or `char`) containing characters that should be removed. `nvarchar(max)` and `varchar(max)` types are not allowed.

string   
Is an expression of any character type (`nvarchar`, `varchar`, `nchar`, or `char`) where characters should be removed.

## Return Types   
Returns a character expression with a type of string argument where the space character `char(32)` or other specified characters are removed from both sides. Returns `NULL` if input string is `NULL`.

## Remarks   
By default `TRIM` function removes the space character `char(32)` from both sides. This is equivalent to `LTRIM(RTRIM(@string))`. Behavior of `TRIM ` function with specified characters is identical to behavior of `REPLACE` function where characters from start or end are replaced with empty strings.


## Examples
### A.  Removes the space character from both sides of string   
The following example removes spaces from before and after the word `test`.   
```tsql
SELECT TRIM( '     test    ') AS Result;
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]   

`test`


### B.  Removes specified characters from both sides of string   
The following example removes a trailing period and trailing spaces.
```tsql
SELECT TRIM( '.,! ' FROM  '#     test    .') AS Result;
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]   
`#     test`


## See Also
[String Functions (Transact-SQL)](../../t-sql/functions/string-functions-transact-sql.md)   
[LTRIM (Transact-SQL)](../../t-sql/functions/ltrim-transact-sql.md)   
[RTRIM (Transact-SQL)](../../t-sql/functions/rtrim-transact-sql.md)   
[REPLACE (Transact-SQL)](../../t-sql/functions/replace-transact-sql.md)   
