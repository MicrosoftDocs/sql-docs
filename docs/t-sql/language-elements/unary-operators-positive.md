---
title: "+ (Unary positive) (Transact-SQL)"
description: Returns the value of a numeric expression (a unary operator).
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/23/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "+ (positive)"
  - "positive"
helpviewer_keywords:
  - "+ (positive operator)"
  - "positive operator (+)"
  - "positive values [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---

# Unary operators - Positive (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns the value of a numeric expression (a unary operator). Unary operators perform an operation on only one expression of any one of the data types of the numeric data type category.

| Operator | Meaning |
| --- | --- |
| [+ (Unary positive)](unary-operators-positive.md) | Numeric value is positive. |
| [- (Unary negative)](unary-operators-negative.md) | Numeric value is negative. |
| [~ (Bitwise NOT)](bitwise-not-transact-sql.md) | Returns the ones' complement of the number. |

The `+` (positive) and `-` (negative) operators can be used on any expression of any one of the data types of the numeric data type category. The `~` (bitwise `NOT`) operator can be used only on expressions of any one of the data types of the integer data type category.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
+ numeric_expression
```

## Arguments

#### *numeric_expression*

Any valid [expression](expressions-transact-sql.md) of any one of the data types in the numeric data type category, except the **datetime** and **smalldatetime** data types.

## Return types

Returns the data type of *numeric_expression*.

## Remarks

Although a unary plus can appear before any numeric expression, it performs no operation on the value returned from the expression. Specifically, it doesn't return the positive value of a negative expression. To return positive value of a negative expression, use the [ABS](../functions/abs-transact-sql.md) function.

## Examples

### A. Set a variable to a positive value

The following example sets a variable to a positive value.

```sql
USE tempdb;
GO

DECLARE @MyNumber DECIMAL(10, 2);
SET @MyNumber = + 123.45;

SELECT @MyNumber AS PositiveValue;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
PositiveValue
--------------
123.45
```

### B. Use the unary plus operator with a negative value

The following example shows using the unary plus with a negative expression and the [ABS](../functions/abs-transact-sql.md) function on the same negative expression. The unary plus doesn't affect the expression, but the `ABS()` function returns the positive value of the expression.

```sql
USE tempdb;
GO

DECLARE @Num1 INT;
SET @Num1 = -5;

SELECT + @Num1 AS NegativeValue,
    ABS(@Num1) AS PositiveValue;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
NegativeValue  PositiveValue
-------------- --------------
-5             5
```

## Related content

- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
- [Expressions (Transact-SQL)](expressions-transact-sql.md)
- [Operators (Transact-SQL)](operators-transact-sql.md)
- [ABS (Transact-SQL)](../functions/abs-transact-sql.md)
