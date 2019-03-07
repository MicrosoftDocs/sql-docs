---
title: "LOWER (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "LOWER"
  - "LOWER_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "characters [SQL Server], lowercase"
  - "LOWER function"
  - "uppercase characters [SQL Server]"
  - "characters [SQL Server], uppercase"
  - "lowercase characters"
  - "converting uppercase to lowercase characters"
ms.assetid: 1783352b-6852-4658-9d94-51963c59b9bf
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# LOWER (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns a character expression after converting uppercase character data to lowercase.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
LOWER ( character_expression )  
```  
  
## Arguments  
 *character_expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of character or binary data. *character_expression* can be a constant, variable, or column. *character_expression* must be of a data type that is implicitly convertible to **varchar**. Otherwise, use [CAST](../../t-sql/functions/cast-and-convert-transact-sql.md) to explicitly convert *character_expression*.  
  
## Return Types  
 **varchar** or **nvarchar**  
  
## Examples  
 The following example uses the `LOWER` function, the `UPPER` function, and nests the `UPPER` function inside the `LOWER` function in selecting product names that have prices between $11 and $20.  
  
```  
-- Uses AdventureWorks  
  
SELECT LOWER(SUBSTRING(EnglishProductName, 1, 20)) AS Lower,   
       UPPER(SUBSTRING(EnglishProductName, 1, 20)) AS Upper,   
       LOWER(UPPER(SUBSTRING(EnglishProductName, 1, 20))) As LowerUpper  
FROM dbo.DimProduct  
WHERE ListPrice between 11.00 and 20.00;  
  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
Lower                 Upper                  LowerUpper  
--------------------  ---------------------  --------------------  
minipump              MINIPUMP               minipump  
taillights - battery  TAILLIGHTS - BATTERY   taillights - battery
```  
  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)  
 [UPPER &#40;Transact-SQL&#41;](../../t-sql/functions/upper-transact-sql.md)  
  
  

