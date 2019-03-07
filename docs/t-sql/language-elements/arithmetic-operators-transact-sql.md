---
title: "Arithmetic Operators (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "operators [Transact-SQL], arithmetic"
  - "arithmetic operators"
  - "math operations [Transact-SQL]"
ms.assetid: a41b92a5-1061-4e4d-bb3b-a180b73c88fa
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Arithmetic Operators (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

Arithmetic operators run mathematical operations on two expressions of one or more data types. They're run from the numeric data type category. For more information about data type categories, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).  
  
|Operator|Meaning|  
|--------------|-------------|  
|[+ (Add)](../../t-sql/language-elements/add-transact-sql.md)|Addition|  
|[- (Subtract)](../../t-sql/language-elements/subtract-transact-sql.md)|Subtraction|  
|[* (Multiply)](../../t-sql/language-elements/multiply-transact-sql.md)|Multiplication|  
|[/ (Divide)](../../t-sql/language-elements/divide-transact-sql.md)|Division|  
|[% (Modulo)](../../t-sql/language-elements/modulo-transact-sql.md)|Returns the integer remainder of a division. For example, 12 % 5 = 2 because the remainder of 12 divided by 5 is 2.|  
  
The plus (+) and minus (-) operators can also be used to run arithmetic operations on **datetime** and **smalldatetime** values.  
  
For more information about the precision and scale of an arithmetic operation result, see [Precision, Scale, and Length &#40;Transact-SQL&#41;](../../t-sql/data-types/precision-scale-and-length-transact-sql.md).  
  
## See Also  
[Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)   
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
[Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)  
  
  
