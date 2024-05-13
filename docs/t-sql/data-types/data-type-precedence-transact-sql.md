---
title: "Data type precedence (Transact-SQL)"
description: "Explains data type precedence for Transact-SQL"
author: MikeRayMSFT
ms.author: mikeray
ms.date: 05/02/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
helpviewer_keywords:
  - "precedence [SQL Server]"
  - "data types [SQL Server], converting"
  - "data types [SQL Server], precedence"
  - "converting data types [SQL Server], precedence"
  - "precedence [SQL Server], data types"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqledge-current||=azure-sqldw-latest||=fabric"
---
# Data type precedence (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

When an operator combines expressions of different data types, the data type with the lower precedence is first converted to the data type with the higher precedence. If the conversion isn't a supported implicit conversion, an error is returned. For an operator combining operand expressions having the same data type, the result of the operation has that data type.
  
[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses the following precedence order for data types:
  
1. user-defined data types (highest)  
1. **json**
1. **sql_variant**  
1. **xml**  
1. **datetimeoffset**  
1. **datetime2**  
1. **datetime**  
1. **smalldatetime**  
1. **date**  
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
  
## Related content

- [Data types (Transact-SQL)](data-types-transact-sql.md)
- [Expressions (Transact-SQL)](../language-elements/expressions-transact-sql.md)
- [CAST and CONVERT (Transact-SQL)](../functions/cast-and-convert-transact-sql.md)
