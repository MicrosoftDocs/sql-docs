---
title: "CEILING (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CEILING_TSQL"
  - "CEILING"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "smallest integer great than or equal to expression"
  - "integers [SQL Server]"
  - "CEILING function [Transact-SQL]"
ms.assetid: e736b43a-9457-4781-95a4-4bcf9d4fc46a
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CEILING (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

This function returns the smallest integer greater than, or equal to, the specified numeric expression.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
CEILING ( numeric_expression )  
```  
  
## Arguments  
*numeric_expression*  
An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of the exact numeric or approximate numeric data type category. For this function, the **bit** data type is invalid.
  
## Return types
Return values have the same type as *numeric_expression*.
  
## Examples  
This example shows positive numeric, negative numeric, and zero value inputs for the CEILING function.
  
```sql
SELECT CEILING($123.45), CEILING($-123.45), CEILING($0.0);  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]
  
```sql
--------- --------- -------------------------   
124.00    -123.00    0.00                       
  
(1 row(s) affected)  
```  
  
## See also
[System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-for-transact-sql.md)
  
  
