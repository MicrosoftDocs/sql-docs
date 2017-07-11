---
title: "-= (Subtract EQUALS) (Transact-SQL) | Microsoft Docs"
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
  - "-="
  - "-=_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "compound operators, -="
  - "-= (subtract equals)"
ms.assetid: 2a2056b5-1dfa-4ea8-8cfc-6331a2f94da9
caps.latest.revision: 11
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# -= (Subtract EQUALS) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Subtracts two numbers and sets a value to the result of the operation. For example, if a variable @x equals 35, then @x -= 2 takes the original value of @x, subtracts 2 and sets @x to that new value (33).  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
expression -= expression  
```  
  
## Arguments  
 *expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any one of the data types in the numeric category except the **bit** data type.  
  
## Result Types  
 Returns the data type of the argument with the higher precedence. For more information, see [Data Type Precedence &#40;Transact-SQL&#41;](../../t-sql/data-types/data-type-precedence-transact-sql.md).  
  
## Remarks  
 For more information, see [- &#40;Subtract&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/subtract-transact-sql.md).  
  
## See Also  
 [Compound Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/compound-operators-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)  
  
  