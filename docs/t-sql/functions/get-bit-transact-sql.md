---
title: "GET_BIT (Transact-SQL)"
description: "Transact-SQL reference for the GET_BIT function."
author: thesqlsith
ms.author: derekw
ms.reviewer: randolphwest
ms.date: 07/26/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "GET_BIT"
  - "GET_BIT_TSQL"
helpviewer_keywords:
  - "bit manipulation [SQL Server], get bit"
  - "GET_BIT function"
  - "bit shifting [SQL Server], get bit"
dev_langs:
  - "TSQL"
monikerRange: ">= sql-server-ver16 || >= sql-server-linux-ver16"
---
# GET_BIT (Transact SQL)

GET_BIT takes two parameters and returns the bit in *expression_value* that is in the offset defined by *bit_offset*.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
GET_BIT ( expression_value, bit_offset )
```

## Arguments

#### *expression_value*

Any integer or binary expression that isn't a large object (LOB).

#### *bit_offset*

Any integer.

## Return type

**bit**

The *bit_offset* parameter in GET_BIT is used to identify the *n*th bit of the data to get or set. In integer types, the `0`th bit is the least significant bit. In binary types, the `0`th bit is the least significant bit in the rightmost byte.

GET_BIT will throw an error if *bit_offset* is negative or greater than the last bit in the data type.

## Remarks

In the initial implementation, Distributed Query functionality for the bit manipulation functions within linked server or ad hoc queries (OPENQUERY) won't be supported.

## Examples

In this example, the second and fourth bits are returned.

```sql
SELECT GET_BIT ( 0xabcdef, 2 ) as Get_2nd_Bit,
GET_BIT ( 0xabcdef, 4 ) as Get_4th_Bit;
```

The results are as follows:

|Get_2nd_Bit|Get_4th_Bit|
|---|---|
| 1 | 0 |

> [!NOTE]  
> `0xabcdef` in binary is 1010 1011 1100 1101 111**0** 1**1**11. The second and fourth bits are highlighted.

## See also

- [SET_BIT (Transact SQL)](set-bit-transact-sql.md)
- [LEFT_SHIFT (Transact SQL)](left-shift-transact-sql.md)
- [RIGHT_SHIFT (Transact SQL)](right-shift-transact-sql.md)
- [BIT_COUNT (Transact SQL)](bit-count-transact-sql.md)
- [Bit manipulation functions](bit-manipulation-functions-overview.md)
