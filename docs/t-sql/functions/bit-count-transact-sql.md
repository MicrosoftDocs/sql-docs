---
title: "BIT_COUNT (Transact-SQL)"
description: "Transact-SQL reference for the BIT_COUNT function."
author: thesqlsith
ms.author: derekw
ms.reviewer: randolphwest
ms.date: 07/26/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "BIT_COUNT"
  - "BIT_COUNT_TSQL"
helpviewer_keywords:
  - "bit manipulation [SQL Server], bit count"
  - "BIT_COUNT function"
  - "bit shifting [SQL Server], bit count"
dev_langs:
  - "TSQL"
monikerRange: ">= sql-server-ver16 || >= sql-server-linux-ver16"
---
# BIT_COUNT (Transact SQL)

BIT_COUNT takes one parameter and returns the number of bits set to 1 in that parameter as a **bigint** type.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
BIT_COUNT ( expression_value )
```

## Arguments

#### *expression_value*

Any integer or binary expression that isn't a large object (LOB).

## Return type

**bigint**

BIT_COUNT doesn't cast prior to counting the number of bits. This is because the same number can have a different number of ones in its binary representation depending on the data type.

For example, `SELECT BIT_COUNT (CAST (-1 as smallint))` and `SELECT BIT_COUNT (CAST (-1 as int))` will return 16 and 32 respectively. This is intended, as the binary representation of `-1` can have a different number of bits set to 1 depending on the data type.

## Remarks

In the initial implementation, Distributed Query functionality for the bit manipulation functions within linked server or ad hoc queries (OPENQUERY) won't be supported.

## Examples

#### A. Calculate the BIT_COUNT in a binary value

In the following example, the number of bits set to `1` in a binary value are calculated.

```sql
SELECT BIT_COUNT ( 0xabcdef ) as Count;
```

The result is `17`. This is because `0xabcdef` in binary is `1010 1011 1100 1101 1110 1111`, and there are 17 bits with a value set to `1`.

#### B. Calculate the BIT_COUNT in an integer

In the following example, the number of bits set to `1` in an integer are calculated.

```sql
SELECT BIT_COUNT ( 17 ) as Count;
```

The result is `2`. This is because `17` in binary is `0001 0001`, and there are only 2 bits with a value set to `1`.

## See also

- [LEFT_SHIFT (Transact SQL)](left-shift-transact-sql.md)
- [RIGHT_SHIFT (Transact SQL)](right-shift-transact-sql.md)
- [SET_BIT (Transact SQL)](set-bit-transact-sql.md)
- [GET_BIT (Transact SQL)](get-bit-transact-sql.md)
- [Bit manipulation functions](bit-manipulation-functions-overview.md)
