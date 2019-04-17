---
title: "+= (Addition Assignment) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "+="
  - "+=_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "+= (add equals)"
  - "compound operators, +="
  - "assignment operators, +="
  - "augmented operators, +="
ms.assetid: 9ea52519-80d1-473f-b988-0572f0e2c92f
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# += (Addition Assignment) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Adds two numbers and sets a value to the result of the operation. For example, if a variable @x equals 35, then @x += 2 takes the original value of @x, add 2 and sets @x to that new value (37).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
expression += expression  
```  
  
## Arguments  
 *expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any data type in the numeric category except the **bit** data type.  
  
## Result Types  
 Returns the data type of the argument with the higher precedence. For more information, see [Data Type Precedence &#40;Transact-SQL&#41;](../../t-sql/data-types/data-type-precedence-transact-sql.md).  
  
## Remarks  
 For more information, see [+ &#40;Addition&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/add-transact-sql.md).  
  
## See Also  
 [Compound Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/compound-operators-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [+= &#40;String Concatenation Assignment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/string-concatenation-equal-transact-sql.md)  
  
  
