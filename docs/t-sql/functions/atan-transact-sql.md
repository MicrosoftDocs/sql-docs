---
title: "ATAN (Transact-SQL) | Microsoft Docs"
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
  - "ATAN_TSQL"
  - "ATAN"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "arctangent"
  - "ATAN function"
  - "tangent"
ms.assetid: 6d3dd28e-4fa6-40ba-94cf-b33c0ff614ec
caps.latest.revision: 25
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# ATAN (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the angle in radians whose tangent is a specified **float** expression. This is also called arctangent.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
ATAN ( float_expression )  
```  
  
## Arguments  
 *float_expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of the type **float** or of a type that can be implicitly converted to **float**.  
  
## Return Types  
 **float**  
  
## Examples  
 The following example takes a **float** expression and returns the ATAN of the specified angle.  
  
```  
SELECT 'The ATAN of -45.01 is: ' + CONVERT(varchar, ATAN(-45.01))  
SELECT 'The ATAN of -181.01 is: ' + CONVERT(varchar, ATAN(-181.01))  
SELECT 'The ATAN of 0 is: ' + CONVERT(varchar, ATAN(0))  
SELECT 'The ATAN of 0.1472738 is: ' + CONVERT(varchar, ATAN(0.1472738))  
SELECT 'The ATAN of 197.1099392 is: ' + CONVERT(varchar, ATAN(197.1099392))  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
  
-------------------------------   
The ATAN of -45.01 is: -1.54858                         
  
(1 row(s) affected)  
  
--------------------------------   
The ATAN of -181.01 is: -1.56527                         
  
(1 row(s) affected)  
  
--------------------------------   
The ATAN of 0 is: 0                                
  
(1 row(s) affected)  
  
----------------------------------   
The ATAN of 0.1472738 is: 0.146223                         
  
(1 row(s) affected)  
  
-----------------------------------   
The ATAN of 197.1099392 is: 1.56572                          
  
(1 row(s) affected)  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example takes a **float** expression and returns the arctangent of the specified angle.  
  
```  
SELECT ATAN(45.87) AS atanCalc1,  
    ATAN(-181.01) AS atanCalc2,  
    ATAN(0) AS atanCalc3,  
    ATAN(0.1472738) AS atanCalc4,  
    ATAN(197.1099392) AS atanCalc5;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `atanCalc1  atanCalc2  atanCalc3  atanCalc4  atanCalc5`  
  
 `---------  ---------  ---------  ---------  ---------`  
  
 `1.55       -1.57       0.00       0.15       1.57`  
  
## See Also  
 [CEILING &#40;Transact-SQL&#41;](../../t-sql/functions/ceiling-transact-sql.md)   
 [Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)  
  
  

