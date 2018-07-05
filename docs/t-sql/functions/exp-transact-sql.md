---
title: "EXP (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: t-sql
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "EXP_TSQL"
  - "EXP"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "exponential functions"
  - "EXP function"
ms.assetid: 5a9b8c52-6fb6-4e33-8b02-a878785b2f51
caps.latest.revision: 35
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || = sqlallproducts-allversions"
---
# EXP (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

A mathematical function that returns the exponential value of the specified **float** expression.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
EXP ( float_expression )  
```  
  
## Arguments  
 *float_expression*  
An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of type **float**, or of a type that can implicitly convert to **float**.  
  
## Return Types  
**float**  
  
## Remarks  
The constant **e** (2.718281…) serves as the base of natural logarithms.  
  
The exponent of a number is the constant **e** raised to the power of the number. For example,

EXP(1.0) = e^1.0 = 2.71828182845905  

and  

EXP(10) = e^10 = 22026.4657948067  
  
The exponential of the natural logarithm of a number is the number itself:  
  
EXP (LOG (*n*)) = *n*  
  
and the natural logarithm of the exponential of a number is the number itself:  

LOG (EXP (*n*)) = *n*  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### A. The exponent of a number  
This example declares a variable, and then returns the exponential value of the specified variable (`10`) with a text description.  
  
```  
DECLARE @var float  
SET @var = 10  
SELECT 'The EXP of the variable = ' + CONVERT(varchar,EXP(@var))  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
----------------------------------------------------------  
The EXP of the variable = 22026.5  
(1 row(s) affected)  
```  
  
### B. Finding exponentials and natural logarithms  
This example returns the exponential value of the natural logarithm of `20`, and the natural logarithm of the exponential of `20`. Because these functions are inverse functions of one another, both statements return a value of `20`.  
  
```  
SELECT EXP( LOG(20)), LOG( EXP(20))  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
---------------------- ----------------------  
20                     20  
  
(1 row(s) affected)  
```  
  
### C. Finding the exponent of a number  
This example returns the exponential value of the specified value (`10`).  
  
```  
SELECT EXP(10);  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
----------  
22026.4657948067  
```  
  
## See Also  
 [Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)   
 [LOG &#40;Transact-SQL&#41;](../../t-sql/functions/log-transact-sql.md)   
 [LOG10 &#40;Transact-SQL&#41;](../../t-sql/functions/log10-transact-sql.md)  
  
  

