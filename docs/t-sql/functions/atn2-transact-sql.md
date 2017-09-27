---
title: "ATN2 (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "ATN2"
  - "ATN2_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "arctangent"
  - "tangent"
  - "ATN2 function"
ms.assetid: 014b291e-7cd7-4c39-b20d-5db3a9f0505d
caps.latest.revision: 36
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# ATN2 (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Returns the angle, in radians, between the positive x-axis and the ray from the origin to the point (y, x), where x and y are the values of the two specified float expressions.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
ATN2 ( float_expression , float_expression )  
```  
  
## Arguments  
*float_expression*
Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of the **float** data type.
  
## Return types
**float**
  
## Examples  
The following example calculates the `ATN2` for the specified `x` and `y` components.
  
```sql
DECLARE @x float = 35.175643, @y float = 129.44;  
SELECT 'The ATN2 of the angle is: ' + CONVERT(varchar,ATN2(@x,@y ));  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
The ATN2 of the angle is: 0.265345                         
(1 row(s) affected)  
```  
  
## Examples:
 [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] 

The following example calculates the `ATN2` for the specified `x` and `y` components.
  
```sql
DECLARE @x float = 35.175643, @y float = 129.44;  
SELECT 'The ATN2 of the angle is: ' + CONVERT(varchar,ATN2(@x,@y ));  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
The ATN2 of the angle is: 0.265345                         
(1 row(s) affected)  
```  
  
## See also
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
[float and real &#40;Transact-SQL&#41;](../../t-sql/data-types/float-and-real-transact-sql.md)  
[Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)
  
  

