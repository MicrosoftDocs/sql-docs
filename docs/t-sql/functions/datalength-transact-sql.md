---
title: "DATALENGTH (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/29/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DATALENGTH_TSQL"
  - "DATALENGTH"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "number of bytes representing expression"
  - "data types [SQL Server], length"
  - "DATALENGTH function"
  - "expressions [SQL Server], length"
  - "lengths [SQL Server], data"
ms.assetid: 00f377f1-cc3e-4eac-be47-b3e3f80267c9
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DATALENGTH (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

This function returns the number of bytes used to represent any expression.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
DATALENGTH ( expression )   
```  
  
## Arguments  
*expression*  
An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any data type.
  
## Return types
**bigint** if *expression* has an **nvarchar(max)**, **varbinary(max)**, or **varchar(max)** data type; otherwise **int**.
  
## Remarks  
`DATALENGTH` becomes really helpful when used with

- **image**
- **ntext**
- **nvarchar**
- **text**
- **varbinary**
- **varchar**

data types, because these data types can store variable-length data.
  
For a NULL value, `DATALENGTH` returns NULL.
  
> [!NOTE]  
>  Compatibility levels can affect return values. See [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) for more information about compatibility levels.  
  
## Examples  
This example finds the length of the `Name` column in the `Product` table:
  
```sql
-- Uses AdventureWorks  
  
SELECT length = DATALENGTH(EnglishProductName), EnglishProductName  
FROM dbo.DimProduct  
ORDER BY EnglishProductName;  
GO  
```  
  
## See also
[LEN &#40;Transact-SQL&#41;](../../t-sql/functions/len-transact-sql.md)  
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
[System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-for-transact-sql.md)
  
  

