---
title: "&amp;= (Bitwise AND assignment) (Transact-SQL)"
description: Performs a bitwise logical AND operation between two integer values, and sets a value to the result of the operation.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/25/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "&="
  - "&=_TSQL"
helpviewer_keywords:
  - "compound operators, &="
  - "assignment operators, &="
  - "augmented operators, &="
  - "&= (bitwise AND equals)"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---

# &amp;= (Bitwise AND assignment) (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Performs a bitwise logical AND operation between two integer values, and sets a value to the result of the operation.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
expression &= expression
```

## Arguments

#### *expression*

Any valid [expression](expressions-transact-sql.md) of any one of the data types in the numeric category, except the **bit** data type.

## Return types

Returns the data type of the argument with the higher precedence. For more information, see [Data type precedence (Transact-SQL)](../data-types/data-type-precedence-transact-sql.md).

## Remarks

The `&=` operator is shorthand for using the `=` and `&` operators. The following two queries are equivalent.

```sql
-- &= operator
DECLARE @bitwise INT = 1;
SET @bitwise &= 1;
SELECT @bitwise;
GO

-- = and & operators
DECLARE @bitwise INT = 1;
SET @bitwise = @bitwise & 1;
SELECT @bitwise;
GO
```

Both examples return a result of `1`.

For more information, see [&amp; (Bitwise AND) (Transact-SQL)](bitwise-and-transact-sql.md).

## Related content

- [Compound Operators (Transact-SQL)](compound-operators-transact-sql.md)
- [Expressions (Transact-SQL)](expressions-transact-sql.md)
- [Operators (Transact-SQL)](operators-transact-sql.md)
- [Bitwise operators (Transact-SQL)](bitwise-operators-transact-sql.md)
