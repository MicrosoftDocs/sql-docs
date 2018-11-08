---
title: "FLOOR (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, pdw, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "FLOOR_TSQL"
  - "FLOOR"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "integers [SQL Server]"
  - "largest integers"
  - "FLOOR function [Transact-SQL]"
ms.assetid: 4f26c784-9240-491f-b854-754be3fccae4
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# FLOOR (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md)]

  Returns the largest integer less than or equal to the specified numeric expression.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
FLOOR ( numeric_expression )  
```  
  
## Arguments  
 *numeric_expression*  
 Is an expression of the exact numeric or approximate numeric data type category, except for the **bit** data type.  
  
## Return Types  
 Returns the same type as *numeric_expression*.  
  
## Examples  
 The following example shows positive numeric, negative numeric, and currency values with the `FLOOR` function.  
  
```  
SELECT FLOOR(123.45), FLOOR(-123.45), FLOOR($123.45);  
```  
  
 The result is the integer part of the calculated value in the same data type as *numeric_expression*.  
  
```  
---------      ---------     -----------  
123            -124          123.0000     
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example shows positive numeric, negative numeric, and values with the `FLOOR` function.  
  
```  
SELECT FLOOR(123.45), FLOOR(-123.45), FLOOR($123.45);  
```  
  
 The result is the integer part of the calculated value in the same data type as *numeric_expression*.  
  
 ```
 -----   ---------    -----------  
  
 123     -124         123
 ```  
  
## See Also  
 [Mathematical Functions &#40;Transact-SQL&#41;](../../t-sql/functions/mathematical-functions-transact-sql.md)  
  
  

