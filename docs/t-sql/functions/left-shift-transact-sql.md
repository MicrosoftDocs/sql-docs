---
title: "LEFT_SHIFT (Transact-SQL)"
description: "Transact-SQL reference for the LEFT_SHIFT function."
author: thesqlsith
ms.author: derekw
ms.reviewer: randolphwest
ms.date: 07/26/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "LEFT_SHIFT"
  - "LEFT_SHIFT_TSQL"
helpviewer_keywords:
  - "bit manipulation [SQL Server], left shift"
  - "LEFT_SHIFT function"
  - "bit shifting [SQL Server], left shift"
dev_langs:
  - "TSQL"
monikerRange: ">= sql-server-ver16 || >= sql-server-linux-ver16"
---
# LEFT_SHIFT (Transact SQL)

LEFT_SHIFT takes two parameters, and returns the first parameter bit-shifted left by the number of bits specified in the second parameter.

The LEFT_SHIFT function is also accessible through the `<<` operator.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
LEFT_SHIFT ( expression_value, shift_amount )
expression_value << shift_amount
```

## Arguments

#### *expression_value*

Any integer or binary expression that isn't a large object (LOB).

#### *shift_amount*

The number of bits by which *expression_value* should be shifted. *shift_amount* must be an integer type.

## Return types

Returns the same type as *expression_value*.

The *shift_amount* parameter is cast to a **bigint**. The parameter can be positive or negative, and can also be greater than the number of bits in the datatype of *expression_value*. When *shift_amount* is negative, the shift operates in the opposite direction. For example, `LEFT_SHIFT (expr, -1)` is the same as `RIGHT_SHIFT (expr, 1)`. When *shift_amount* is greater than the number of bits in *expression_value*, the result returned will be `0`.

LEFT_SHIFT performs a logical shift. After bits are shifted, any vacant positions will be filled by `0`, regardless of whether the original value was positive or negative.

## Remarks

In the initial implementation, Distributed Query functionality for the bit manipulation functions within linked server or ad hoc queries (OPENQUERY) won't be supported.

## Examples

In the following example, the integer value 12345 is left-shifted by 5 bits.

```sql
SELECT LEFT_SHIFT(12345, 5);
```

The result is 395040. If you convert 12345 to binary, you have `0011 0000 0011 1001`. Shifting this to the left by 5 becomes `0110 0000 0111 0010 0000`, which is `395040` in decimal.

The following table demonstrates what happens during each shift.

|Integer value|Binary value|Description|
|---:|---:|---|
|12345|`0011 0000 0011 1001`|Starting value|
|24690|`0110 0000 0111 0010`|Shift left by 1|
|49380|`1100 0000 1110 0100`|Shift left by 2|
|98760|`0001 1000 0001 1100 1000`|Shift left by 3,<br/>and open into a new byte|
|197520|`0011 0000 0011 1001 0000`|Shift left by 4|
|395040|`0110 0000 0111 0010 0000`|Shift left by 5|

## See also

- [RIGHT_SHIFT (Transact SQL)](right-shift-transact-sql.md)
- [SET_BIT (Transact SQL)](set-bit-transact-sql.md)
- [GET_BIT (Transact SQL)](get-bit-transact-sql.md)
- [BIT_COUNT (Transact SQL)](bit-count-transact-sql.md)
- [Bit manipulation functions](bit-manipulation-functions-overview.md)
