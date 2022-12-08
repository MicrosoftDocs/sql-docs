---
title: "SET_BIT (Transact-SQL)"
description: "Transact-SQL reference for the SET_BIT function."
author: thesqlsith
ms.author: derekw
ms.reviewer: randolphwest
ms.date: 07/26/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SET_BIT"
  - "SET_BIT_TSQL"
helpviewer_keywords:
  - "bit manipulation [SQL Server], set bit"
  - "SET_BIT function"
  - "bit shifting [SQL Server], set bit"
dev_langs:
  - "TSQL"
monikerRange: ">= sql-server-ver16 || >= sql-server-linux-ver16"
---
# SET_BIT (Transact SQL)

SET_BIT returns *expression_value* offset by the bit defined by *bit_offset*. The bit value defaults to 1, or is set by *bit_value*.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
SET_BIT ( expression_value, bit_offset ) 
SET_BIT ( expression_value, bit_offset, bit_value )
```

## Arguments

#### *expression_value*

Any integer or binary expression that isn't a large object (LOB).

#### *bit_offset*

Any integer.

## Return types

The same type as *expression_value*.

The *bit_offset* parameter is used to identify the *n*th bit of the data to set. In integer types, the `0`th bit is the least significant bit. In binary types, the `0`th bit is the least significant bit in the rightmost byte.

*bit_value* can be an integer or a bit. However, the only valid values for *bit_value* are 1 and 0, regardless of the data type. SET_BIT will throw an error if *bit_value* isn't 1 or 0 or null.

SET_BIT will throw an error if *bit_offset* is negative or greater than the last bit in the data type.

## Remarks

In the initial implementation, Distributed Query functionality for the bit manipulation functions within linked server or ad hoc queries (OPENQUERY) won't be supported.

## Examples

#### A. Use SET_BIT to modify a value

In this example, the third bit (at offset 2, zero-based index) is being set to `1`.

```sql
SELECT SET_BIT ( 0x00, 2 ) as VARBIN1;
```

The result is `0x04`. This is because the *expression_value* of `0x00` is converted to `0000`. SET_BIT changes its third bit (offset 2) to 1, making it `0100`. This binary value is then returned as `4` in hexadecimal representation.

#### B. Use SET_BIT to modify a value with a custom bit_value

In this example, the *bit_value* is being set to 0 instead of the default of 1.

```sql
SELECT SET_BIT ( 0xabcdef, 0, 0 ) as VARBIN2;
```

The result is `0xABCDEE`. The *expression_value* is converted to binary, which is `1010 1011 1100 1101 1110 1111`. SET_BIT changes the first bit to a 0, and the result is returned in hexadecimal format.

## See also

- [LEFT_SHIFT (Transact SQL)](left-shift-transact-sql.md)
- [RIGHT_SHIFT (Transact SQL)](right-shift-transact-sql.md)
- [BIT_COUNT (Transact SQL)](bit-count-transact-sql.md)
- [GET_BIT (Transact SQL)](get-bit-transact-sql.md)
- [Bit manipulation functions](bit-manipulation-functions-overview.md)
