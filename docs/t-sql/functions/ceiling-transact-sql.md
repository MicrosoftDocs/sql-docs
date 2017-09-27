---
title: "CEILING (Transact-SQL) | Microsoft Docs"
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
  - "CEILING_TSQL"
  - "CEILING"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "smallest integer great than or equal to expression"
  - "integers [SQL Server]"
  - "CEILING function [Transact-SQL]"
ms.assetid: e736b43a-9457-4781-95a4-4bcf9d4fc46a
caps.latest.revision: 34
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CEILING (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Returns the smallest integer greater than, or equal to, the specified numeric expression.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
CEILING ( numeric_expression )  
```  
  
## Arguments  
*numeric_expression*  
Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of the exact numeric or approximate numeric data type category, except for the **bit** data type.
  
## Return types
Returns the same type as *numeric_expression*.
  
## Examples  
The following example shows positive numeric, negative, and zero values with the CEILING function.
  
```sql
SELECT CEILING($123.45), CEILING($-123.45), CEILING($0.0);  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
--------- --------- -------------------------   
124.00    -123.00    0.00                       
  
(1 row(s) affected)  
```  
  
[!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  

The following example shows use of positive numeric, negative, and zero values with the CEILING function.
  
```sql
SELECT CEILING(123.45), CEILING(-123.45), CEILING(0.0);  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
`------- --------- --------`
  
`124.00  -123.00   0.00`
  
## See also
[System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-for-transact-sql.md)
  
  
