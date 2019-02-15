---
title: "Data Type Precedence (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "7/23/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "precedence [SQL Server]"
  - "data types [SQL Server], converting"
  - "data types [SQL Server], precedence"
  - "converting data types [SQL Server], precedence"
  - "precedence [SQL Server], data types"
ms.assetid: f4c804ab-ed3f-43b1-a024-c9ac6944b66b
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Data type precedence (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

When an operator combines expressions of different data types, the data type with the lower precedence is first converted to the data type with the higher precedence. If the conversion isn't a supported implicit conversion, an error is returned. For an operator combining operand expressions having the same data type, the result of the operation has that data type.
  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the following precedence order for data types:
  
1.  user-defined data types (highest)  
1.  **sql_variant**  
1.  **xml**  
1.  **datetimeoffset**  
1.  **datetime2**  
1.  **datetime**  
1.  **smalldatetime**  
1.  **date**  
1. **time**  
1. **float**  
1. **real**  
1. **decimal**  
1. **money**  
1. **smallmoney**  
1. **bigint**  
1. **int**  
1. **smallint**  
1. **tinyint**  
1. **bit**  
1. **ntext**  
1. **text**  
1. **image**  
1. **timestamp**  
1. **uniqueidentifier**  
1. **nvarchar** (including **nvarchar(max)** )  
1. **nchar**  
1. **varchar** (including **varchar(max)** )  
1. **char**  
1. **varbinary** (including **varbinary(max)** )  
1. **binary** (lowest)  
  
## See also
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
[Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)  
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)
  