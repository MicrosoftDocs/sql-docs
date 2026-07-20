---
title: "&amp; (Bitwise AND) (Transact-SQL)"
description: Performs a bitwise logical AND operation between two integer values.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/07/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "&"
  - "&_TSQL"
helpviewer_keywords:
  - "AND, bitwise AND"
  - "& (bitwise AND)"
  - "bitwise AND (&)"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# &amp; (Bitwise AND) (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Performs a bitwise logical `AND` operation between two integer values.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
expression & expression
```

## Arguments

#### *expression*

Any valid [expression](expressions-transact-sql.md) of any of the data types of the integer data type category, or the **bit**, or the **binary** or **varbinary** data types. *expression* is treated as a binary number for the bitwise operation.

> [!NOTE]  
> In a bitwise operation, only one *expression* can be of either **binary** or **varbinary** data type.

## Return types

- **int** if the input values are **int**.
- **smallint** if the input values are **smallint**.
- **tinyint** if the input values are **tinyint** or **bit**.

## Remarks

The `&` bitwise operator performs a bitwise logical `AND` between the two expressions, taking each corresponding bit for both expressions. The bits in the result are set to `1` if and only if both bits (for the current bit being resolved) in the input expressions have a value of `1`. Otherwise, the bit in the result is set to `0`.

If the left and right expressions have different integer data types (for example, the left *expression* is **smallint** and the right *expression* is **int**), the argument of the smaller data type is converted to the larger data type. In this case, the **smallint** *expression* is converted to an **int**.

## Examples

The following example creates a table using the **int** data type to store the values and inserts two values into one row.

```sql
CREATE TABLE bitwise
(
    a_int_value INT NOT NULL,
    b_int_value INT NOT NULL
);
GO

INSERT bitwise
VALUES (170, 75);
GO
```

This query performs the bitwise `AND` between the `a_int_value` and `b_int_value` columns.

```sql
SELECT a_int_value & b_int_value
FROM bitwise;
GO
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
-----------
10
```

The binary representation of 170 (`a_int_value` or `A`) is `0000 0000 1010 1010`. The binary representation of 75 (`b_int_value` or `B`) is `0000 0000 0100 1011`. Performing the bitwise `AND` operation on these two values produces the binary result `0000 0000 0000 1010`, which is decimal 10.

```output
(A & B)
0000 0000 1010 1010
0000 0000 0100 1011
-------------------
0000 0000 0000 1010
```

## Related content

- [Expressions (Transact-SQL)](expressions-transact-sql.md)
- [Operators (Transact-SQL)](operators-transact-sql.md)
- [Bitwise operators (Transact-SQL)](bitwise-operators-transact-sql.md)
- [&amp;= (Bitwise AND assignment) (Transact-SQL)](bitwise-and-equals-transact-sql.md)
- [Compound operators (Transact-SQL)](compound-operators-transact-sql.md)
