---
title: "!&gt; (Not Greater Than) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "Not Greater"
  - "Greater"
  - "!>_TSQL"
  - "!>"
  - "Not Greater Than"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "!> (not greater than)"
  - "not greater than operator (!>)"
ms.assetid: 71a413ed-64f1-4ab9-9c52-c5959a77d00f
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# !&gt; (Not Greater Than) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Compares two expressions (a comparison operator). When you compare nonnull expressions, the result is TRUE if the left operand does not have a greater value than the right operand; otherwise, the result is FALSE. Unlike the = (equality) comparison operator, the result of the !> comparison of two NULL values does not depend on the ANSI_NULLS setting.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
expression !> expression  
```  
  
## Arguments  
 *expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md). Both expressions must have implicitly convertible data types. The conversion depends on the rules of [data type precedence](../../t-sql/data-types/data-type-precedence-transact-sql.md).  
  
## Result Types  
 **Boolean**  
  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)  
  