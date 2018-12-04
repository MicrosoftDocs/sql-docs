---
title: "| (Bitwise OR) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/10/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "|"
  - "|_TSQL"
  - "Bitwise OR"
  - "bitwise"
  - "OR"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "OR operator"
  - "bitwise OR (|)"
  - "| (bitwise OR operator)"
ms.assetid: 86a3b87f-9688-4eaf-a552-29f1b01d880a
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# | (Bitwise OR) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Performs a bitwise logical OR operation between two specified integer values as translated to binary expressions within [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```   
expression | expression  
```  
  
## Arguments  
 *expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of the integer data type category, or the **bit**, **binary**, or **varbinary** data types. *expression* is treated as a binary number for the bitwise operation.  
  
> [!NOTE]  
>  Only one *expression* can be of either **binary** or **varbinary** data type in a bitwise operation.  
  
## Result Types  
 Returns an **int** if the input values are **int**, a **smallint** if the input values are **smallint**, or a **tinyint** if the input values are **tinyint**.  
  
## Remarks  
 The bitwise | operator performs a bitwise logical OR between the two expressions, taking each corresponding bit for both expressions. The bits in the result are set to 1 if either or both bits (for the current bit being resolved) in the input expressions have a value of 1; if neither bit in the input expressions is 1, the bit in the result is set to 0.  
  
 If the left and right expressions have different integer data types (for example, the left *expression* is **smallint** and the right *expression* is **int**), the argument of the smaller data type is converted to the larger data type. In this example, the **smallint**_expression_ is converted to an **int**.  
  
## Examples  
 The following example creates a table with **int** data types to show the original values and puts the table into one row.  
  
```sql  
CREATE TABLE bitwise  
(   
 a_int_value int NOT NULL,  
b_int_value int NOT NULL  
);  
GO  
INSERT bitwise VALUES (170, 75);  
GO  
```  
  
 The following query performs the bitwise OR on the **a_int_value** and **b_int_value** columns.  
  
```  
SELECT a_int_value | b_int_value  
FROM bitwise;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
-----------   
235           
  
(1 row(s) affected)  
```  
  
 The binary representation of 170 (**a_int_value** or `A`, below) is `0000 0000 1010 1010`. The binary representation of 75 (**b_int_value** or `B`, below) is `0000 0000 0100 1011`. Performing the bitwise OR operation on these two values produces the binary result `0000 0000 1110 1011`, which is decimal 235.  
  
```  
(A | B)  
0000 0000 1010 1010  
0000 0000 0100 1011  
-------------------  
0000 0000 1110 1011  
```  
  
## See Also  
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [Bitwise Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/bitwise-operators-transact-sql.md)   
 [&#124;= &#40;Bitwise OR Assignment&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/bitwise-or-equals-transact-sql.md)   
 [Compound Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/compound-operators-transact-sql.md)  
  
  


