---
title: "Bitwise Operators (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/07/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "operators [Transact-SQL], bitwise"
  - "bitwise operators"
  - "bit manipulations"
ms.assetid: 2b994cf5-2daa-438a-b8c7-4bd8d451ac8d
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Bitwise Operators (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Bitwise operators perform bit manipulations between two expressions of any of the data types of the integer data type category.  
  Bitwise operators convert two integer values to binary bits, perform the AND, OR, or NOT operation on each bit, producing a result. Then converts the result to an integer.  
  
  For example, the integer 170 converts to binary 1010 1010.
  The integer 75 converts to binary 0100 1011.

|operator|bitwise math|
|---- |---- |
|AND <br> If bits at any location are both 1, the result is 1. |1010 1010 = 170 <br>0100 1011 =  75 <br>-----------------  <br> 0000 1010 =  10 |
|OR <br> If either bit at any location is 1, the result is 1. |1010 1010 = 170 <br>0100 1011 =  75 <br>-----------------  <br> 1110 1011 = 235|
|NOT  <br> Reverses the bit value at every bit location. |1010 1010 = 170 <br>----------------- <br>  0101 0101 =   85 |
  
See the following topics:   
* [& &#40;Bitwise AND&#41;](../../t-sql/language-elements/bitwise-and-transact-sql.md)  
* [&= &#40;Bitwise AND Assignment&#41;](../../t-sql/language-elements/bitwise-and-equals-transact-sql.md)   
* [&#124; &#40;Bitwise OR&#41;](../../t-sql/language-elements/bitwise-or-transact-sql.md)  
* [&#124;= &#40;Bitwise OR Assignment&#41;](../../t-sql/language-elements/bitwise-or-equals-transact-sql.md)   
* [^ &#40;Bitwise Exclusive OR&#41;](../../t-sql/language-elements/bitwise-exclusive-or-transact-sql.md)  
* [^= &#40;Bitwise Exclusive OR Assignment&#41;](../../t-sql/language-elements/bitwise-exclusive-or-equals-transact-sql.md)  
* [~ &#40;Bitwise NOT&#41;](../../t-sql/language-elements/bitwise-not-transact-sql.md)  
  
 The operands for bitwise operators can be any one of the data types of the integer or binary string data type categories (except for the **image** data type), except that both operands cannot be any one of the data types of the binary string data type category. The following table shows the supported operand data types.  
  
|Left operand|Right operand|  
|------------------|-------------------|  
|[binary](../../t-sql/data-types/binary-and-varbinary-transact-sql.md)|**int**, **smallint**, or **tinyint**|  
|[bit](../../t-sql/data-types/bit-transact-sql.md)|**int**, **smallint**, **tinyint**, or **bit**|  
|[int](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md)|**int**, **smallint**, **tinyint**, **binary**, or **varbinary**|  
|[smallint](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md)|**int**, **smallint**, **tinyint**, **binary**, or **varbinary**|  
|[tinyint](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md)|**int**, **smallint**, **tinyint**, **binary**, or **varbinary**|  
|[varbinary](../../t-sql/data-types/binary-and-varbinary-transact-sql.md)|**int**, **smallint**, or **tinyint**|  
  
## See Also  
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Compound Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/compound-operators-transact-sql.md)   
  
