---
title: "UPPER (Transact-SQL) | Microsoft Docs"
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
  - "UPPER_TSQL"
  - "UPPER"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "UPPER function"
  - "characters [SQL Server], lowercase"
  - "converting lowercase to uppercase"
  - "uppercase characters [SQL Server]"
  - "characters [SQL Server], uppercase"
  - "lowercase characters"
ms.assetid: 5ced55f7-ac89-4cf2-9465-f63f4dc480db
caps.latest.revision: 26
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# UPPER (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns a character expression with lowercase character data converted to uppercase.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
UPPER ( character_expression )  
```  
  
## Arguments  
 *character_expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of character data. *character_expression* can be a constant, variable, or column of either character or binary data.  
  
 *character_expression* must be of a data type that is implicitly convertible to **varchar**. Otherwise, use [CAST](../../t-sql/functions/cast-and-convert-transact-sql.md) to explicitly convert *character_expression*.  
  
## Return Types  
 **varchar** or **nvarchar**  
  
## Examples  
 The following example uses the `UPPER` and `RTRIM` functions to return the last name of people in the `Person` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database so that it is uppercase, trimmed, and concatenated with the first name.  
  
```  
SELECT UPPER(RTRIM(LastName)) + ', ' + FirstName AS Name  
FROM Person.Person  
ORDER BY LastName;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example uses the `UPPER` and `RTRIM` functions to return the last name of people in the `dbo.DimEmployee` table so that it is in uppercase, trimmed, and concatenated with the first name.  
  
```  
-- Uses AdventureWorks  
  
SELECT UPPER(RTRIM(LastName)) + ', ' + FirstName AS Name  
FROM dbo.DimEmployee  
ORDER BY LastName;  
```  
  
 Here is a partial result set.  
  
 `Name`  
  
 `------------------------------`  
  
 `ABBAS, Syed`  
  
 `ABERCROMBIE, Kim`  
  
 `ABOLROUS, Hazem`  
  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  
  
  

