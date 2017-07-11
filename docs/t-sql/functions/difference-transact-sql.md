---
title: "DIFFERENCE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DIFFERENCE"
  - "DIFFERENCE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DIFFERENCE function"
  - "comparing SOUNDEX values"
  - "SOUNDEX values"
ms.assetid: c58ca25d-d6ea-48fa-93bb-c9374b0b2a2b
caps.latest.revision: 27
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DIFFERENCE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns an integer value that indicates the difference between the SOUNDEX values of two character expressions.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
DIFFERENCE ( character_expression , character_expression )  
```  
  
## Arguments  
 *character_expression*  
 Is an alphanumeric [expression](../../t-sql/language-elements/expressions-transact-sql.md) of character data. *character_expression* can be a constant, variable, or column.  
  
## Return Types  
 **int**  
  
## Remarks  
 The integer returned is the number of characters in the SOUNDEX values that are the same. The return value ranges from 0 through 4: 0 indicates weak or no similarity, and 4 indicates strong similarity or the same values.  
  
 DIFFERENCE and SOUNDEX are collation sensitive.  
  
## Examples  
 In the first part of the following example, the `SOUNDEX` values of two very similar strings are compared. For a Latin1_General collation `DIFFERENCE` returns a value of `4`. In the second part of the following example, the `SOUNDEX` values for two very different strings are compared, and for a Latin1_General collation `DIFFERENCE` returns a value of `0`.  
  
```  
-- Returns a DIFFERENCE value of 4, the least possible difference.  
SELECT SOUNDEX('Green'), SOUNDEX('Greene'), DIFFERENCE('Green','Greene');  
GO  
-- Returns a DIFFERENCE value of 0, the highest possible difference.  
SELECT SOUNDEX('Blotchet-Halls'), SOUNDEX('Greene'), DIFFERENCE('Blotchet-Halls', 'Greene');  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
----- ----- -----------   
G650  G650  4             
  
(1 row(s) affected)  
  
----- ----- -----------   
B432  G650  0             
  
(1 row(s) affected)  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 In the first part of the following example, the `SOUNDEX` values of two very similar strings are compared. For a Latin1_General collation `DIFFERENCE` returns a value of `4`. In the second part of the following example, the `SOUNDEX` values for two very different strings are compared, and for a Latin1_General collation `DIFFERENCE` returns a value of `0`.  
  
```  
-- Returns a DIFFERENCE value of 4, the least possible difference.  
SELECT SOUNDEX('Green'), SOUNDEX('Greene'), DIFFERENCE('Green','Greene');  
GO  
-- Returns a DIFFERENCE value of 0, the highest possible difference.  
SELECT SOUNDEX('Blotchet-Halls'), SOUNDEX('Greene'), DIFFERENCE('Blotchet-Halls', 'Greene');  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
----- ----- -----------   
G650  G650  4             
  
(1 row(s) affected)  
  
----- ----- -----------   
B432  G650  0             
  
(1 row(s) affected)  
```  
  
## See Also  
 [SOUNDEX &#40;Transact-SQL&#41;](../../t-sql/functions/soundex-transact-sql.md)   
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  
  
  

