---
title: "Bit manipulation functions (Transact-SQL)"
description: "Bit manipulation functions allow moving, retrieving (getting), setting, or counting single bits within an integer or binary value."
author: thesqlsith
ms.author: derekw
ms.reviewer: randolphwest
ms.date: 07/26/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "bit manipulation [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">= sql-server-ver16 || >= sql-server-linux-ver16"
---
# Bit manipulation functions

[!INCLUDE [_ss2022](../../includes/applies-to-version/_ss2022.md)]

Bit manipulation functions such as moving, retrieving (getting), setting, or counting single bits within an integer or binary value, allow you to process and store data more efficiently than with individual bits.

A *bit* has two values (`1` or `0`, which represent `on` or `off`, or `true` or `false`). A *byte* is made up of a sequence of 8 bits. Bit manipulation functions in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] treat the "leftmost" bit in a byte as the biggest (the most significant). To the bit manipulation functions, bits are numbered from right to left, with bit `0` being the rightmost and the smallest and bit `7` being the leftmost and largest.

For example, a binary sequence of `00000111` is the decimal equivalent of the number `7`. You can calculate this out using powers of 2 as follows:

00000111 = (2^2 + 2^1 + 2^0 = 4 + 2 + 1 = 7)

What this means in practice is that while [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] stores this value as `11100000` (byte-reversed), the bit manipulation functions will treat it as though it's `00000111`.

When looking at multiple bytes, the first byte (reading left to right) is the biggest.

You can use the following images to visualize how [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]'s bit manipulation functions interpret bit and byte expression values and bit offsets.

**int**

:::image type="content" source="media/bit-manipulation-functions-overview/int.png" alt-text="Diagram showing an int value where 4 bytes represent the reversed binary of each byte from left to right.":::

**smallint**

:::image type="content" source="media/bit-manipulation-functions-overview/smallint.png" alt-text="Diagram showing a small int value where the first byte represents bits 15 to 8, and the second byte represents bits 7 to 0.":::

## Functions

There are five functions available for manipulating bits in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]:

- [LEFT_SHIFT()](left-shift-transact-sql.md)
- [RIGHT_SHIFT()](right-shift-transact-sql.md)
- [BIT_COUNT()](bit-count-transact-sql.md)
- [GET_BIT()](get-bit-transact-sql.md)
- [SET_BIT()](set-bit-transact-sql.md)

All five functions are intended to operate on **tinyint**, **smallint**, **int**, **bigint**, **binary(*n*)**, and **varbinary(*n*)** data types.

The following types aren't supported: **varchar**, **nvarchar**, **image**, **ntext**, **text**, **xml**, and **table**.

## Remarks

In the initial implementation, Distributed Query functionality for the bit manipulation functions within linked server or ad hoc queries (OPENQUERY) won't be supported.
