---
title: "-= (Subtraction Assignment) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "-="
  - "-=_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "compound operators, -="
  - "assignment operators, -="
  - "augmented operators, -="
  - "-= (subtract equals)"
  - "-= (subtraction assignment)"
ms.assetid: 2a2056b5-1dfa-4ea8-8cfc-6331a2f94da9
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# -= (Subtraction Assignment) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Subtracts two numbers and sets a value to the result of the operation. For example, if a variable @x equals 35, then @x -= 2 takes the original value of @x, subtracts 2 and sets @x to that new value (33).  
  
## Syntax  
  
```  
expression -= expression  
```  
  
## Arguments  
 *expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any one of the data types in the numeric category except the **bit** data type.  
  
## Result Types  
 Returns the data type of the argument with the higher precedence. For more information, see [Data Type Precedence &#40;Transact-SQL&#41;](../../t-sql/data-types/data-type-precedence-transact-sql.md).  
  
## Remarks  
 For more information, see [- &#40;Subtraction&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/subtract-transact-sql.md).  
  
## See Also  
 [Compound Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/compound-operators-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)  
  
  
