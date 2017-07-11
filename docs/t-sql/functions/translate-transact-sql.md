---
title: "TRANSLATE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/16/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "TRANSLATE"
  - "TRANSLATE_TSQL"
helpviewer_keywords: 
  - "TRANSLATE function"
ms.assetid: 0426fa90-ef6d-4d19-8207-02ee59f74aec
caps.latest.revision: 5
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# TRANSLATE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ssvNxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]

Returns the string provided as a first argument after some characters specified in the second argument are translated into a destination set of characters.

## Syntax   
```
TRANSLATE ( inputString, characters, translations) 
```

## Arguments   

inputString   
Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any character type (nvarchar, varchar, nchar, char).

characters   
Is a [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any character type containing characters that should be replaced.

translations   
Is a character [expression](../../t-sql/language-elements/expressions-transact-sql.md) that matches second argument by type and length.

## Return Types   
Returns a character expression of the same type as `inputString` where characters from the second argument are replaced with the matching characters from third argument.

## Remarks   

`TRANSLATE` function will return an error if characters and translations have different lengths. `TRANSLATE` function should return unchanged input if null vales are provided as characters or replacement arguments. The behavior of the `TRANSLATE` function should be identical to the [REPLACE](../../t-sql/functions/replace-transact-sql.md) function.   

The behavior of the `TRANSLATE` function is equivalent to using multiple `REPLACE` functions.

`TRANSLATE` is always SC collation aware.

## Examples   

### A. Replace square and curly braces with regular braces    
The following query replaces square and curly braces in the input string with parentheses:
```
SELECT TRANSLATE('2*[3+4]/{7-2}', '[]{}', '()()');
```
[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]
```
2*(3+4)/(7-2)
```

>  [!NOTE]
>  The `TRANSLATE` function in this example is equivalent to but much simplier than the following statement using `REPLACE`:
> `SELECT REPLACE(REPLACE(REPLACE(REPLACE('2*[3+4]/{7-2}','[','('), ']', ')'), '{', '('), '}', ')');` 


###  B. Convert GeoJSON points into WKT    
GeoJSON is a format for encoding a variety of geographic data structures. With the `TRANSLATE` function, developers can easily convert GeoJSON points to WKT format and vice versa. The following query replaces square and curly braces in input  with regular braces:   
```tsql
SELECT TRANSLATE('[137.4, 72.3]' , '[,]', '( )') AS Point,
    TRANSLATE('(137.4 72.3)' , '( )', '[,]') AS Coordinates;
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]   


|Point  |Coordinates |  
---------|--------- |
(137.4  72.3) |[137.4,72.3] |


## See Also

[String Functions (Transact-SQL)](../../t-sql/functions/string-functions-transact-sql.md)   
[REPLACE (Transact-SQL)](../../t-sql/functions/replace-transact-sql.md)   

