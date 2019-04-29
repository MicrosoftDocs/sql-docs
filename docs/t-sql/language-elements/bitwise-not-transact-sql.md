---
title: "~ (Bitwise NOT) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/10/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "~_TSQL"
  - "bitwise"
  - "NOT"
  - "~"
  - "Bitwise NOT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "NOT keyword"
  - "bitwise NOT (~)"
  - "~ (bitwise NOT)"
ms.assetid: 02da8016-f6c0-41ae-8d59-33eaa02bfc95
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ~ (Bitwise NOT) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Performs a bitwise logical NOT operation on an integer value.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
~ expression  
```  
  
## Arguments  
 *expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any one of the data types of the integer data type category, the **bit**, or the **binary** or **varbinary** data types. *expression* is treated as a binary number for the bitwise operation.  
  
> [!NOTE]  
>  Only one *expression* can be of either **binary** or **varbinary** data type in a bitwise operation.  
  
## Result Types  
 **int** if the input values are **int**.  
  
 **smallint** if the input values are **smallint**.  
  
 **tinyint** if the input values are **tinyint**.  
  
 **bit** if the input values are **bit**.  
  
## Remarks  
 The **~** bitwise operator performs a bitwise logical NOT for the *expression*, taking each bit in turn. If *expression* has a value of 0, the bits in the result set are set to 1; otherwise, the bit in the result is cleared to a value of 0. In other words, ones are changed to zeros and zeros are changed to ones.  
  
> [!IMPORTANT]  
>  When you perform any kind of bitwise operation, the storage length of the expression used in the bitwise operation is important. We recommend that you use this same number of bytes when storing values. For example, storing the decimal value of 5 as a **tinyint**, **smallint**, or **int** produces a value stored with different numbers of bytes: **tinyint** stores data using 1 byte; **smallint** stores data using 2 bytes, and **int** stores data using 4 bytes. Therefore, performing a bitwise operation on an **int** decimal value can produce different results from those using a direct binary or hexadecimal translation, especially when the **~** (bitwise NOT) operator is used. The bitwise NOT operation may occur on a variable of a shorter length. In this case, when the shorter length is converted to a longer data type variable, the bits in the upper 8 bits may not be set to the expected value. We recommend that you convert the smaller data type variable to the larger data type, and then perform the NOT operation on the result.  
  
## Examples  
 The following example creates a table using the **int** data type to store the values and inserts the two values into one row.  
  
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
  
 The following query performs the bitwise NOT on the `a_int_value` and `b_int_value` columns.  
  
```  
SELECT ~ a_int_value, ~ b_int_value  
FROM bitwise;  
```  
  
 Here is the result set:  
  
```  
--- ---   
-171  -76   
  
(1 row(s) affected)  
```  
  
 The binary representation of 170 (`a_int_value` or `A`) is `0000 0000 1010 1010`. Performing the bitwise NOT operation on this value produces the binary result `1111 1111 0101 0101`, which is decimal -171. The binary representation for 75 is `0000 0000 0100 1011`. Performing the bitwise NOT operation produces `1111 1111 1011 0100`, which is decimal -76.  
  
```  
 (~A)     
         0000 0000 1010 1010  
         -------------------  
         1111 1111 0101 0101  
(~B)     
         0000 0000 0100 1011  
         -------------------  
         1111 1111 1011 0100  
```  
  
 
## See Also  
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [Bitwise Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/bitwise-operators-transact-sql.md)  
  
  


