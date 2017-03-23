---
title: "ACOS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "ACOS"
  - "ACOS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "cosine"
  - "ACOS function"
  - "arccosine"
ms.assetid: 4ec6b46e-9438-4f0f-8b96-461edd84280a
caps.latest.revision: 43
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# ACOS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  A mathematical function that returns the angle, in radians, whose cosine is the specified **float** expression; also called arccosine.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
ACOS ( float_expression )  
```  
  
## Arguments  
 *float_expression*  
 Is an expression of the type **float** or of a type that can be implicitly converted to **float**, with a value from -1 through 1. Values outside this range return NULL and report a domain error.  
  
## Return Types  
 **float**  
  
## Examples  
 The following example returns the ACOS of the specified number.  
  
```  
SET NOCOUNT OFF;  
DECLARE @cos float;  
SET @cos = -1.0;  
SELECT 'The ACOS of the number is: ' + CONVERT(varchar, ACOS(@cos));  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
---------------------------------   
The ACOS of the number is: 3.14159   
  
(1 row(s) affected)  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example returns the ACOS of the specified number.  
  
```  
DECLARE @cos float;  
SET @cos = -1.0;  
SELECT 'The ACOS of the number is: ' + CONVERT(varchar, ACOS(@cos));  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
---------------------------------   
The ACOS of the number is: 3.14159   
  
(1 row(s) affected)  
```  
  
## See Also  
 [Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)   
 [Functions](../../t-sql/functions/functions.md)  
  
  

