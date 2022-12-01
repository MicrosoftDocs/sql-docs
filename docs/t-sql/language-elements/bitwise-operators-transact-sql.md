---
title: "Bitwise operators (Transact-SQL)"
description: Bitwise operators perform bit manipulations between two expressions of any of the data types of the integer data type category.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/26/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "bitwise"
helpviewer_keywords:
  - "operators [Transact-SQL], bitwise"
  - "bitwise operators"
  - "bit manipulations"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# Bitwise operators (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Bitwise operators perform bit manipulations between two expressions of any of the data types of the integer data type category.  

Bitwise operators convert two integer values to binary bits, perform the `AND`, `OR`, or `NOT` operation on each bit, producing a result. Then converts the result to an integer.

For example, the integer `170` converts to binary `1010 1010`.

The integer `75` converts to binary `0100 1011`.

|Operator|Bitwise math|
| --- | --- |
|**AND**<br /><br />If bits at any position are both `1`, the result is `1`. |`1010 1010` = 170<br />`0100 1011` = 75<br />-----------------<br />`0000 1010` = 10 |
|**OR**<br /><br />If either bit at any position is `1`, the result is `1`. |`1010 1010` = 170<br />`0100 1011` = 75<br />-----------------<br />`1110 1011` = 235|
|**NOT**<br /><br />Reverses the bit value at every bit position. |`1010 1010` = 170<br />-----------------<br />`0101 0101` = 85 |

The following articles provide more information about the bitwise operators available in the [!INCLUDE [ssde-md](../../includes/ssde-md.md)]:

- [& (Bitwise AND)](../../t-sql/language-elements/bitwise-and-transact-sql.md)
- [&= (Bitwise AND Assignment)](../../t-sql/language-elements/bitwise-and-equals-transact-sql.md)
- [&#124; (Bitwise OR)](../../t-sql/language-elements/bitwise-or-transact-sql.md)
- [&#124;= (Bitwise OR Assignment)](../../t-sql/language-elements/bitwise-or-equals-transact-sql.md)
- [^ (Bitwise Exclusive OR)](../../t-sql/language-elements/bitwise-exclusive-or-transact-sql.md)
- [^= (Bitwise Exclusive OR Assignment)](../../t-sql/language-elements/bitwise-exclusive-or-equals-transact-sql.md)
- [~ (Bitwise NOT)](../../t-sql/language-elements/bitwise-not-transact-sql.md)

The following bitwise operators were introduced in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]:

- [>> (Shift right)](../../t-sql/functions/right-shift-transact-sql.md)
- [<< (Shift left)](../../t-sql/functions/left-shift-transact-sql.md)

Operands for bitwise operators can be any one of the data types of the integer or binary string data type categories (except for the **image** data type), except that both operands can't be any one of the data types of the binary string data type category. The following table shows the supported operand data types.

|Left operand|Right operand|
|------------------|-------------------|
|[binary](../../t-sql/data-types/binary-and-varbinary-transact-sql.md)|**int**, **smallint**, or **tinyint**|
|[bit](../../t-sql/data-types/bit-transact-sql.md)|**int**, **smallint**, **tinyint**, or **bit**|
|[bigint](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md)|**bigint**, **int**, **smallint**, **tinyint**, **binary**, or **varbinary**|
|[int](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md)|**int**, **smallint**, **tinyint**, **binary**, or **varbinary**|
|[smallint](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md)|**int**, **smallint**, **tinyint**, **binary**, or **varbinary**|
|[tinyint](../../t-sql/data-types/int-bigint-smallint-and-tinyint-transact-sql.md)|**int**, **smallint**, **tinyint**, **binary**, or **varbinary**|
|[varbinary](../../t-sql/data-types/binary-and-varbinary-transact-sql.md)|**int**, **smallint**, or **tinyint**|

## See also

- [Operators (Transact-SQL)](../../t-sql/language-elements/operators-transact-sql.md)
- [Data types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md)
- [Compound operators (Transact-SQL)](../../t-sql/language-elements/compound-operators-transact-sql.md)
