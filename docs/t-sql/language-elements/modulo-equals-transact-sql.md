---
title: "%= (Modulus assignment) (Transact-SQL)"
description: "The %= (modulus assignment) operator divides one number by another and sets a value to the result of the operation."
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/19/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "%=_TSQL"
  - "%="
helpviewer_keywords:
  - "%= (modulo equals)"
  - "%= (modulus assignment)"
  - "compound operators, %="
  - "assignment operators, %="
  - "augmented operators, %="
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# %= (Modulus assignment) (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Divides one number by another and sets a value to the result of the operation. For example, if a variable `@x` equals `38`, then `@x %= 5` takes the original value of `@x`, divides by `5`, and sets `@x` to the remainder of that division (`3`).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
expression %= expression
```

## Arguments

#### *expression*

Any valid [expression](expressions-transact-sql.md) of any one of the data types in the numeric category, except the **bit** data type.

## Result types

Returns the data type of the argument with the higher precedence. For more information, see [Data type precedence (Transact-SQL)](../data-types/data-type-precedence-transact-sql.md).

## Remarks

For more information, see [% (Modulus) (Transact-SQL)](modulo-transact-sql.md).

## Related content

- [Compound Operators (Transact-SQL)](compound-operators-transact-sql.md)
- [Expressions (Transact-SQL)](expressions-transact-sql.md)
- [Operators (Transact-SQL)](operators-transact-sql.md)
