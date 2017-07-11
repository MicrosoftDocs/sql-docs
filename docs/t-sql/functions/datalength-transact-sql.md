---
title: "DATALENGTH (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 31
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DATALENGTH (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns the number of bytes used to represent any expression.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
DATALENGTH ( expression )   
```  
  
## Arguments  
 *expression*  
 Is an [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any data type.  
  
## Return Types  
 **bigint** if *expression* is of the **varchar(max)**, **nvarchar(max)** or **varbinary(max)** data types; otherwise **int**.  
  
## Remarks  
 DATALENGTH is especially useful with **varchar**, **varbinary**, **text**, **image**, **nvarchar**, and **ntext** data types because these data types can store variable-length data.  
  
 The DATALENGTH of NULL is NULL.  
  
> [!NOTE]  
>  Compatibility levels can affect return values. For more information about compatibility levels, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).  
  
## Examples  
 The following example finds the length of the `Name` column in the `Product` table.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT length = DATALENGTH(Name), Name  
FROM Production.Product  
ORDER BY Name;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example finds the length of the `Name` column in the `Product` table.  
  
```  
-- Uses AdventureWorks  
  
SELECT length = DATALENGTH(EnglishProductName), EnglishProductName  
FROM dbo.DimProduct  
ORDER BY EnglishProductName;  
GO  
```  
  
## See Also  
 [LEN &#40;Transact-SQL&#41;](../../t-sql/functions/len-transact-sql.md)   
 [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-for-transact-sql.md)  
  
  

