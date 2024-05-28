---
title: "bit (Transact-SQL)"
description: The bit data type is an integer data type that can take a value of 1, 0, or `NULL`, representing Boolean values.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 05/21/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "bit_TSQL"
  - "bit"
helpviewer_keywords:
  - "bit data type"
  - "Boolean"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# bit (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

An integer data type that can take a value of `1`, `0`, or `NULL`.

## Remarks

The [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] optimizes storage of **bit** columns. If there are 8 or fewer **bit** columns in a table, the columns are stored as 1 byte. If there are from 9 up to 16 **bit** columns, the columns are stored as 2 bytes, and so on.

The **bit** data type can be used to store Boolean values. The string values `TRUE` and `FALSE` can be converted to **bit** values: `TRUE` is converted to `1`, and `FALSE` is converted to `0`.

Converting to bit promotes any nonzero value to `1`.

## Related content

- [ALTER TABLE (Transact-SQL)](../statements/alter-table-transact-sql.md)
- [CAST and CONVERT (Transact-SQL)](../functions/cast-and-convert-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](../statements/create-table-transact-sql.md)
- [Data type conversion (Database Engine)](data-type-conversion-database-engine.md)
- [Data types (Transact-SQL)](data-types-transact-sql.md)
- [DECLARE @local_variable (Transact-SQL)](../language-elements/declare-local-variable-transact-sql.md)
- [SET @local_variable (Transact-SQL)](../language-elements/set-local-variable-transact-sql.md)
- [sys.types (Transact-SQL)](../../relational-databases/system-catalog-views/sys-types-transact-sql.md)
