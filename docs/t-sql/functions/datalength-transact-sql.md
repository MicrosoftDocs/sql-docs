---
title: "DATALENGTH (Transact-SQL)"
description: "DATALENGTH (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "08/20/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DATALENGTH_TSQL"
  - "DATALENGTH"
helpviewer_keywords:
  - "number of bytes representing expression"
  - "data types [SQL Server], length"
  - "DATALENGTH function"
  - "expressions [SQL Server], length"
  - "lengths [SQL Server], data"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# DATALENGTH (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function returns the number of bytes used to represent any expression.

> [!NOTE]
> To return the number of characters in a string expression, use the [LEN](../../t-sql/functions/len-transact-sql.md) function.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DATALENGTH ( expression )   
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*expression*  
An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of any data type.
  
## Return types
**bigint** if *expression* has an **nvarchar(max)**, **varbinary(max)**, or **varchar(max)** data type; otherwise **int**.
  
## Remarks  
`DATALENGTH` becomes really helpful when used with data types that can store variable-length data, such as:
- **image**
- **ntext**
- **nvarchar**
- **text**
- **varbinary**
- **varchar**
  
For a NULL value, `DATALENGTH` returns NULL.
  
> [!NOTE]  
> Compatibility levels can affect return values. See [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md) for more information about compatibility levels.  

> [!NOTE]
> Use the [LEN](../../t-sql/functions/len-transact-sql.md) to return the number of characters encoded into a given string expression, and [DATALENGTH](../../t-sql/functions/datalength-transact-sql.md) to return the size in bytes for a given string expression. These outputs may differ depending on the data type and type of encoding used in the column. For more information on storage differences between different encoding types, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).

## Examples  
This example finds the length of the `Name` column in the `Product` table:
  
```sql
USE AdventureWorks2016  
GO
SELECT length = DATALENGTH(EnglishProductName), EnglishProductName  
FROM dbo.DimProduct  
ORDER BY EnglishProductName;  
GO  
```  
  
## See also
[LEN &#40;Transact-SQL&#41;](../../t-sql/functions/len-transact-sql.md)  
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
[System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-category-transact-sql.md)
