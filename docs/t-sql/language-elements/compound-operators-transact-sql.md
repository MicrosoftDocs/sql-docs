---
title: "Compound Operators (Transact-SQL)"
description: Compound operators execute some operation and set an original value to the result of the operation.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/07/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "compound operators"
  - "compound operators, described"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# Compound operators (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Compound operators execute some operation and set an original value to the result of the operation. For example, if a variable `@x` equals `35`, then `@x += 2` takes the original value of `@x`, adds `2`, and sets `@x` to that new value (`37`).

[!INCLUDE [tsql](../../includes/tsql-md.md)] provides the following compound operators:

| Operator | Link to more information | Action |
| --- | --- | --- |
| += | [Addition assignment](add-equals-transact-sql.md) | Adds some amount to the original value and sets the original value to the result. |
| -= | [Subtraction assignment](subtract-equals-transact-sql.md) | Subtracts some amount from the original value and sets the original value to the result. |
| *= | [Multiplication assignment](multiply-equals-transact-sql.md) | Multiplies by an amount and sets the original value to the result. |
| /= | [Division assignment](divide-equals-transact-sql.md) | Divides by an amount and sets the original value to the result. |
| %= | [Modulus assignment](modulo-equals-transact-sql.md) | Divides by an amount and sets the original value to the modulo. |
| &= | [Bitwise AND assignment](bitwise-and-equals-transact-sql.md) | Performs a bitwise `AND` and sets the original value to the result. |
| ^= | [Bitwise exclusive OR assignment](bitwise-exclusive-or-equals-transact-sql.md) | Performs a bitwise exclusive `OR` and sets the original value to the result. |
| &#124;= | [Bitwise OR assignment](bitwise-or-equals-transact-sql.md) | Performs a bitwise `OR` and sets the original value to the result. |

## Syntax

```syntaxsql
expression <operator> expression
```

## Arguments

#### *expression*

Any valid [expression](expressions-transact-sql.md) of any one of the data types in the numeric category.

## Return types

Returns the data type of the argument with the higher precedence. For more information, see [Data type precedence](../data-types/data-type-precedence-transact-sql.md).

## Remarks

For more information, see the topics related to each operator.

## Examples

The following examples demonstrate compound operations.

```sql
DECLARE @x1 AS INT = 27;
SET @x1 += 2;

SELECT @x1 AS Added_2;

DECLARE @x2 AS INT = 27;
SET @x2 -= 2;

SELECT @x2 AS Subtracted_2;

DECLARE @x3 AS INT = 27;
SET @x3 *= 2;

SELECT @x3 AS Multiplied_by_2;

DECLARE @x4 AS INT = 27;
SET @x4 /= 2;

SELECT @x4 AS Divided_by_2;

DECLARE @x5 AS INT = 27;
SET @x5 %= 2;

SELECT @x5 AS Modulo_of_27_divided_by_2;

DECLARE @x6 AS INT = 9;
SET @x6 &= 13;

SELECT @x6 AS Bitwise_AND;

DECLARE @x7 AS INT = 27;
SET @x7 ^= 2;

SELECT @x7 AS Bitwise_Exclusive_OR;

DECLARE @x8 AS INT = 27;
SET @x8 |= 2;

SELECT @x8 AS Bitwise_OR;
```

## Related content

- [Operators (Transact-SQL)](operators-transact-sql.md)
- [Bitwise operators (Transact-SQL)](bitwise-operators-transact-sql.md)
