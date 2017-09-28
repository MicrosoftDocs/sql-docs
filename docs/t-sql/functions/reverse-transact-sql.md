---
title: "REVERSE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "REVERSE_TSQL"
  - "REVERSE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "expressions [SQL Server], reverse"
  - "REVERSE function"
  - "reverse character expressions"
ms.assetid: 555d8877-7cc7-4955-ae2c-6215aca313b7
caps.latest.revision: 46
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# REVERSE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the reverse order of a string value.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
REVERSE ( string_expression )  
```  
  
## Arguments  
 *string_expression*  
 *string_expression* is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of a string or binary data type. *string_expression* can be a constant, variable, or column of either character or binary data.  
  
## Return Types  
 **varchar** or **nvarchar**  
  
## Remarks  
 *string_expression* must be of a data type that is implicitly convertible to **varchar**. Otherwise, use [CAST](../../t-sql/functions/cast-and-convert-transact-sql.md) to explicitly convert *string_expression*.  
  
## Supplementary Characters (Surrogate Pairs)  
 When using SC collations, the REVERSE function will not reverse the order of two halves of a surrogate pair.  
  
## Examples  
 The following example returns all contact first names with the characters reversed. This example uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```  
SELECT FirstName, REVERSE(FirstName) AS Reverse  
FROM Person.Person  
WHERE BusinessEntityID < 5  
ORDER BY FirstName;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `FirstName      Reverse`  
  
 `-------------- --------------`  
  
 `Ken            neK`  
  
 `Rob            boR`  
  
 `Roberto        otreboR`  
  
 `Terri          irreT`  
  
 `(4 row(s) affected)`  
  
 The following example reverses the characters in a variable.  
  
```  
DECLARE @myvar varchar(10);  
SET @myvar = 'sdrawkcaB';  
SELECT REVERSE(@myvar) AS Reversed ;  
GO  
```  
  
 The following example makes an implicit conversion from an **int** data type into **varchar** data type and then reverses the result.  
  
```  
SELECT REVERSE(1234) AS Reversed ;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example returns names of all databases, and the names with the characters reversed.  
  
```  
SELECT name, REVERSE(name) FROM sys.databases;  
GO  
```  
  
 The following example reverses the characters in a variable.  
  
```  
DECLARE @myvar varchar(10);  
SET @myvar = 'sdrawkcaB';  
SELECT REVERSE(@myvar) AS Reversed ;  
GO  
```  
  
 The following example makes an implicit conversion from an **int** data type into **varchar** data type and then reverses the result.  
  
```  
SELECT REVERSE(1234) AS Reversed ;  
GO  
```  
  
## See Also  
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  
  
  

