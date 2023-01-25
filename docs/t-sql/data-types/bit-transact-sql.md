---
title: "bit (Transact-SQL)"
description: "bit (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "07/23/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "bit_TSQL"
  - "bit"
helpviewer_keywords:
  - "bit data type"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# bit (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  An integer data type that can take a value of 1, 0, or NULL.  
  
## Remarks  
The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] optimizes storage of **bit** columns. If there are 8 or fewer **bit** columns in a table, the columns are stored as 1 byte. If there are from 9 up to 16 **bit** columns, the columns are stored as 2 bytes, and so on.
  
The string values TRUE and FALSE can be converted to **bit** values: TRUE is converted to 1 and FALSE is converted to 0.
  
Converting to bit promotes any nonzero value to 1.
  
## See also
[ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)  
[CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)  
[Data Type Conversion &#40;Database Engine&#41;](../../t-sql/data-types/data-type-conversion-database-engine.md)  
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
[DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)  
[SET @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/set-local-variable-transact-sql.md)  
[sys.types &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-types-transact-sql.md)
  
  
