---
title: "Precision, Scale, and Length (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "data types [SQL Server], length"
  - "data types [SQL Server], scale"
  - "precision [SQL Server], data types"
  - "lengths [SQL Server], data types"
  - "number of digits"
  - "scale [SQL Server]"
  - "scale [SQL Server], data types"
  - "data types [SQL Server], precision"
ms.assetid: fbc9ad2c-0d3b-4e98-8fdd-4d912328e40a
caps.latest.revision: 31
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Precision, Scale, and Length (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Precision is the number of digits in a number. Scale is the number of digits to the right of the decimal point in a number. For example, the number 123.45 has a precision of 5 and a scale of 2.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the default maximum precision of **numeric** and **decimal** data types is 38. In earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the default maximum is 28.  
  
 Length for a numeric data type is the number of bytes that are used to store the number. Length for a character string or Unicode data type is the number of characters. The length for **binary**, **varbinary**, and **image** data types is the number of bytes. For example, an **int** data type can hold 10 digits, is stored in 4 bytes, and does not accept decimal points. The **int** data type has a precision of 10, a length of 4, and a scale of 0.  
  
 When two **char**, **varchar**, **binary**, or **varbinary** expressions are concatenated, the length of the resulting expression is the sum of the lengths of the two source expressions or 8,000 characters, whichever is less.  
  
 When two **nchar** or **nvarchar** expressions are concatenated, the length of the resulting expression is the sum of the lengths of the two source expressions or 4,000 characters, whichever is less.  
  
 When two expressions of the same data type but different lengths are compared by using UNION, EXCEPT, or INTERSECT, the resulting length is the maximum length of the two expressions.  
  
 The precision and scale of the numeric data types besides **decimal** are fixed. If an arithmetic operator has two expressions of the same type, the result has the same data type with the precision and scale defined for that type. If an operator has two expressions with different numeric data types, the rules of data type precedence define the data type of the result. The result has the precision and scale defined for its data type.  
  
 The following table defines how the precision and scale of the result are calculated when the result of an operation is of type **decimal**. The result is **decimal** when either of the following is true:  
  
-   Both expressions are **decimal**.  
  
-   One expression is **decimal** and the other is a data type with a lower precedence than **decimal**.  
  
 The operand expressions are denoted as expression e1, with precision p1 and scale s1, and expression e2, with precision p2 and scale s2. The precision and scale for any expression that is not **decimal** is the precision and scale defined for the data type of the expression.  
  
|Operation|Result precision|Result scale *|  
|---------------|----------------------|---------------------|  
|e1 + e2|max(s1, s2) + max(p1-s1, p2-s2) + 1|max(s1, s2)|  
|e1 - e2|max(s1, s2) + max(p1-s1, p2-s2) + 1|max(s1, s2)|  
|e1 * e2|p1 + p2 + 1|s1 + s2|  
|e1 / e2|p1 - s1 + s2 + max(6, s1 + p2 + 1)|max(6, s1 + p2 + 1)|  
|e1 { UNION &#124; EXCEPT &#124; INTERSECT } e2|max(s1, s2) + max(p1-s1, p2-s2)|max(s1, s2)|  
|e1 % e2|min(p1-s1, p2 -s2) + max( s1,s2 )|max(s1, s2)|  
  
 \* The result precision and scale have an absolute maximum of 38. When a result precision is greater than 38, the corresponding scale is reduced to prevent the integral part of a result from being truncated.  
  
## See Also  
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
  
  