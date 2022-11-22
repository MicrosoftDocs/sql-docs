---
title: "!&gt; (Not Greater Than) (Transact-SQL)"
description: "!&gt; (Not Greater Than) (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "03/13/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "!>_TSQL"
  - "!>"
helpviewer_keywords:
  - "!> (not greater than)"
  - "not greater than operator (!>)"
dev_langs:
  - "TSQL"
---

# !&gt; (Not Greater Than) (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Compares two expressions (a comparison operator). When you compare non-null expressions, the result is TRUE if the left operand doesn't have a greater value than the right operand. Otherwise, the result is FALSE. Unlike the = (equality) comparison operator, the result of the !> comparison of two NULL values doesn't depend on the ANSI_NULLS setting.  
  
![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Article link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql 
expression !> expression  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md). Both expressions must have implicitly convertible data types. The conversion depends on the rules of [data type precedence](../../t-sql/data-types/data-type-precedence-transact-sql.md).  
  
## Result Types  
 **Boolean**  
  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)  
  
