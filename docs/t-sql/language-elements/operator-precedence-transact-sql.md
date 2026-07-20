---
title: "Operator Precedence (Transact-SQL)"
description: Operator precedence determines the sequence of operations when a complex expression has multiple operators.
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/29/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "operators [Transact-SQL], precedence"
  - "operator precedence [Transact-SQL]"
  - "order of operator execution [Transact-SQL]"
  - "precedence [SQL Server], operators"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Operator precedence (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

When a complex expression contains multiple operators, operator precedence determines the sequence of operations. The order of execution can significantly affect the resulting value.

Operators have the precedence levels shown in the following table. An operator on a higher level is evaluated before an operator on a lower level. In the following table, 1 is the highest level and 8 is the lowest level.

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16"

| Level | Operators |
| --- | --- |
| 1 | `~` (bitwise `NOT`) |
| 2 | `*` (multiplication), `/` (division), `%` (modulus) |
| 3 | `+` (positive), `-` (negative), `+` (addition), `+` (concatenation), `-` (subtraction), `&` (bitwise `AND`), `^` (bitwise exclusive `OR`), <code>&#124;</code> (bitwise `OR`), `<<` (bitwise left shift), `>>` (bitwise right shift) |
| 4 | `=`, `>`, `<`, `>=`, `<=`, `<>`, `!=`, `!>`, `!<` (comparison operators) |
| 5 | `NOT` |
| 6 | `AND` |
| 7 | `ALL`, `ANY`, `BETWEEN`, `IN`, `LIKE`, `OR`, `SOME` |
| 8 | `=` (assignment) |

::: moniker-end

::: moniker range="<=sql-server-ver15 || <=sql-server-linux-ver15"

| Level | Operators |
| --- | --- |
| 1 | `~` (bitwise `NOT`) |
| 2 | `*` (multiplication), `/` (division), `%` (modulus) |
| 3 | `+` (positive), `-` (negative), `+` (addition), `+` (concatenation), `-` (subtraction), `&` (bitwise `AND`), `^` (bitwise exclusive `OR`), <code>&#124;</code> (bitwise `OR`) |
| 4 | `=`, `>`, `<`, `>=`, `<=`, `<>`, `!=`, `!>`, `!<` (comparison operators) |
| 5 | `NOT` |
| 6 | `AND` |
| 7 | `ALL`, `ANY`, `BETWEEN`, `IN`, `LIKE`, `OR`, `SOME` |
| 8 | `=` (assignment) |

::: moniker-end

When two operators in an expression have the same precedence level, the evaluation happens from left to right based on their position in the expression. For example, in the expression used in the following `SET` statement, the subtraction operator is evaluated before the addition operator.

```sql
DECLARE @MyNumber AS INT;
SET @MyNumber = 4 - 2 + 27;

-- Evaluates to 2 + 27 which yields an expression result of 29.
SELECT @MyNumber;
```

Use parentheses to override the defined precedence of the operators in an expression. Everything within parentheses is evaluated to yield a single value. Any operator outside those parentheses can use that value.

For example, in the expression used in the following `SET` statement, the multiplication operator has a higher precedence than the addition operator. The multiplication operation is evaluated first. The expression result is `13`.

```sql
DECLARE @MyNumber AS INT;
SET @MyNumber = 2 * 4 + 5;

-- Evaluates to 8 + 5 which yields an expression result of 13.
SELECT @MyNumber;
```

In the expression used in the following `SET` statement, the parentheses cause the addition to be evaluated first. The expression result is `18`.

```sql
DECLARE @MyNumber AS INT;
SET @MyNumber = 2 * (4 + 5);

-- Evaluates to 2 * 9 which yields an expression result of 18.
SELECT @MyNumber;
```

If an expression has nested parentheses, the most deeply nested expression is evaluated first. The following example contains nested parentheses, with the expression `5 - 3` in the most deeply nested set of parentheses. This expression yields a value of `2`. Then, the addition operator (`+`) adds this result to `4`, which yields a value of `6`. Finally, the `6` is multiplied by `2` to yield an expression result of `12`.

```sql
DECLARE @MyNumber AS INT;
SET @MyNumber = 2 * (4 + (5 - 3));

-- Evaluates to 2 * (4 + 2) which then evaluates to 2 * 6, and
-- yields an expression result of 12.
SELECT @MyNumber;
```

## Related content

- [Logical operators (Transact-SQL)](logical-operators-transact-sql.md)
- [Operators (Transact-SQL)](operators-transact-sql.md)
- [What are the SQL database functions?](../functions/functions.md)
