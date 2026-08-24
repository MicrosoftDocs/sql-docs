---
title: "Bitwise Operators (Transact-SQL)"
description: Bitwise operators perform bit manipulations between two expressions of any of the data types of the integer data type category.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/07/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "bitwise"
helpviewer_keywords:
  - "operators [Transact-SQL], bitwise"
  - "bitwise operators"
  - "bit manipulations"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# Bitwise operators (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Bitwise operators perform bit manipulations between two expressions of any of the data types of the integer data type category.

Bitwise operators convert two integer values to binary bits, perform the `AND`, `OR`, or `NOT` operation on each bit, producing a result. Then converts the result to an integer.

For example, the integer `170` converts to binary `1010 1010`.

The integer `75` converts to binary `0100 1011`.

| Operator | Bitwise math |
| --- | --- |
| `AND`<br /><br />If bits at any position are both `1`, the result is `1`. | `1010 1010` = 170<br />`0100 1011` = 75<br />-----------------<br />`0000 1010` = 10 |
| `OR`<br /><br />If either bit at any position is `1`, the result is `1`. | `1010 1010` = 170<br />`0100 1011` = 75<br />-----------------<br />`1110 1011` = 235 |
| `NOT`<br /><br />Reverses the bit value at every bit position. | `1010 1010` = 170<br />-----------------<br />`0101 0101` = 85 |

The following articles provide more information about the bitwise operators available in the [!INCLUDE [ssde-md](../../includes/ssde-md.md)]:

- [&amp; (Bitwise AND)](bitwise-and-transact-sql.md)
- [&amp;= (Bitwise AND assignment)](bitwise-and-equals-transact-sql.md)
- [&#124; (Bitwise OR)](bitwise-or-transact-sql.md)
- [&#124;= (Bitwise OR assignment)](bitwise-or-equals-transact-sql.md)
- [^ (Bitwise exclusive OR)](bitwise-exclusive-or-transact-sql.md)
- [^= (Bitwise exclusive OR assignment)](bitwise-exclusive-or-equals-transact-sql.md)
- [~ (Bitwise NOT)](bitwise-not-transact-sql.md)

The following bitwise operators were introduced in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]:

- [RIGHT_SHIFT](../functions/right-shift-transact-sql.md)
- [LEFT_SHIFT](../functions/left-shift-transact-sql.md)

Operands for bitwise operators can be any one of the data types of the integer or binary string data type categories (except for the **image** data type), except that both operands can't be any one of the data types of the binary string data type category. The following table shows the supported operand data types.

| Left operand | Right operand |
| --- | --- |
| [binary](../data-types/binary-and-varbinary-transact-sql.md) | **int**, **smallint**, or **tinyint** |
| [bit](../data-types/bit-transact-sql.md) | **int**, **smallint**, **tinyint**, or **bit** |
| [bigint](../data-types/int-bigint-smallint-and-tinyint-transact-sql.md) | **bigint**, **int**, **smallint**, **tinyint**, **binary**, or **varbinary** |
| [int](../data-types/int-bigint-smallint-and-tinyint-transact-sql.md) | **int**, **smallint**, **tinyint**, **binary**, or **varbinary** |
| [smallint](../data-types/int-bigint-smallint-and-tinyint-transact-sql.md) | **int**, **smallint**, **tinyint**, **binary**, or **varbinary** |
| [tinyint](../data-types/int-bigint-smallint-and-tinyint-transact-sql.md) | **int**, **smallint**, **tinyint**, **binary**, or **varbinary** |
| [varbinary](../data-types/binary-and-varbinary-transact-sql.md) | **int**, **smallint**, or **tinyint** |

## Related content

- [Operators (Transact-SQL)](operators-transact-sql.md)
- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
- [Compound operators (Transact-SQL)](compound-operators-transact-sql.md)
