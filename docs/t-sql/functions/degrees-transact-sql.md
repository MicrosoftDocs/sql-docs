---
title: "DEGREES (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DEGREES"
  - "DEGREES_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DEGREES function"
  - "number of degrees"
ms.assetid: 5208de3c-90a3-4f59-a7e3-10b01bf285bb
caps.latest.revision: 36
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DEGREES (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the corresponding angle in degrees for an angle specified in radians.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
DEGREES ( numeric_expression )  
```  
  
## Arguments  
 *numeric_expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of the exact numeric or approximate numeric data type category, except for the **bit** data type.  
  
## Return Code Values  
 Returns the same type as *numeric_expression*.  
  
## Examples  
 The following example returns the number of degrees in an angle of PI/2 radians.  
  
```  
SELECT 'The number of degrees in PI/2 radians is: ' +   
CONVERT(varchar, DEGREES((PI()/2)));  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
The number of degrees in PI/2 radians is 90         
  
(1 row(s) affected)  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example returns the number of degrees in an angle of PI/2 radians.  
  
```  
SELECT 'The number of degrees in PI/2 radians is: ' +   
CONVERT(varchar, DEGREES((PI()/2)));  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
The number of degrees in PI/2 radians is 90         
  
(1 row(s) affected)  
```  
  
## See Also  
 [Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)  
  
  

