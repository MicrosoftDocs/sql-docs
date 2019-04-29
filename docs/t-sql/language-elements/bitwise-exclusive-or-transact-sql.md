---
title: "^ (Bitwise Exclusive OR) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/10/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "^_TSQL"
  - "Bitwise Exclusive OR"
  - "Exclusive"
  - "^"
  - "bitwise"
  - "OR"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "^ (bitwise exclusive OR operator)"
  - "OR operator"
  - "exclusive OR mathematical operations"
  - "bitwise exclusive OR (^)"
ms.assetid: f38f0ad4-46d0-40ea-9851-0f928fda5293
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ^ (Bitwise Exclusive OR) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Performs a bitwise exclusive OR operation between two integer values.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
expression ^ expression  
```  
  
## Arguments  
 *expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any one of the data types of the integer data type category, or the **bit**, or the **binary** or **varbinary** data types. *expression* is treated as a binary number for the bitwise operation.  
  
> [!NOTE]  
>  Only one *expression* can be of either **binary** or **varbinary** data type in a bitwise operation.  
  
## Result Types  
 **int** if the input values are **int**.  
  
 **smallint** if the input values are **smallint**.  
  
 **tinyint** if the input values are **tinyint**.  
  
## Remarks  
 The **^** bitwise operator performs a bitwise logical exclusive OR between the two expressions, taking each corresponding bit for both expressions. The bits in the result are set to 1 if either (but not both) bits (for the current bit being resolved) in the input expressions have a value of 1. If both bits are 0 or both bits are 1, the bit in the result is cleared to a value of 0.  
  
 If the left and right expressions have different integer data types (for example, the left *expression* is **smallint** and the right *expression* is **int**), the argument of the smaller data type is converted to the larger data type. In this case, the **smallint**_expression_ is converted to an **int**.  
  
## Examples  
 The following example creates a table using the **int** data type to store the original values and inserts two values into one row.  
  
```  
CREATE TABLE bitwise  
(   
a_int_value int NOT NULL,  
b_int_value int NOT NULL  
);  
GO  
INSERT bitwise VALUES (170, 75);  
GO  
```  
  
 The following query performs the bitwise exclusive OR on the `a_int_value` and `b_int_value` columns.  
  
```  
SELECT a_int_value ^ b_int_value  
FROM bitwise;  
GO  
```  
  
 Here is the result set:  
  
```  
-----------   
225           
  
(1 row(s) affected)  
```  
  
 The binary representation of 170 (`a_int_value` or `A`) is `0000 0000 1010 1010`. The binary representation of 75 (`b_int_value` or `B`) is `0000 0000 0100 1011`. Performing the bitwise exclusive OR operation on these two values produces the binary result `0000 0000 1110 0001`, which is decimal 225.  
  
```  
(A ^ B)     
         0000 0000 1010 1010  
         0000 0000 0100 1011  
         -------------------  
         0000 0000 1110 0001  
```  
  

  
## See Also  
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [Bitwise Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/bitwise-operators-transact-sql.md)   
 [^= &#40;Bitwise Exclusive OR Assignment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/bitwise-exclusive-or-equals-transact-sql.md)   
 [Compound Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/compound-operators-transact-sql.md)  
  
  


