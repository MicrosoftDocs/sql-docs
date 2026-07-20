---
title: "Arithmetic Operators (Transact-SQL)"
description: Arithmetic Transact-SQL operators run mathematical operations on two expressions of one or more data types, in the SQL Server Database Engine.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/16/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "operators [Transact-SQL], arithmetic"
  - "arithmetic operators"
  - "math operations [Transact-SQL]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Arithmetic operators (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Arithmetic operators run mathematical operations on two expressions of one or more data types. They're run from the numeric data type category. For more information about data type categories, see [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

| Operator | Meaning |
| --- | --- |
| [+ (Add)](add-transact-sql.md) | Addition |
| [- (Subtract)](subtract-transact-sql.md) | Subtraction |
| [* (Multiply)](multiply-transact-sql.md) | Multiplication |
| [/ (Divide)](divide-transact-sql.md) | Division |
| [% (Modulo)](modulo-transact-sql.md) | Returns the integer remainder of a division. For example, `12 % 5 = 2` because the remainder of `12` divided by `5` is `2`. |

The plus (`+`) and minus (`-`) operators can also be used to run arithmetic operations on **datetime** and **smalldatetime** values.

For more information about the precision and scale of an arithmetic operation result, see [Precision, scale, and length](../data-types/precision-scale-and-length-transact-sql.md).

## Related content

- [Mathematical functions (Transact-SQL)](../functions/mathematical-functions-transact-sql.md)
- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
- [Expressions (Transact-SQL)](expressions-transact-sql.md)
