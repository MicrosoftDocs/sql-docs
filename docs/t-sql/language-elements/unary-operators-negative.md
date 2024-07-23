---
title: "- (Unary negative) (Transact-SQL)"
description: Returns the negative of the value of a numeric expression (a unary operator).
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/23/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "negative"
helpviewer_keywords:
  - "- (negative)"
  - "negative operator (-)"
  - "negative values"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# Unary operators - Negative (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns the negative of the value of a numeric expression (a unary operator). Unary operators perform an operation on only one expression of any one of the data types of the numeric data type category.

| Operator | Meaning |
| --- | --- |
| [+ (Unary positive)](unary-operators-positive.md) | Numeric value is positive. |
| [- (Unary negative)](unary-operators-negative.md) | Numeric value is negative. |
| [~ (Bitwise NOT)](bitwise-not-transact-sql.md) | Returns the ones' complement of the number. |

The `+` (positive) and `-` (negative) operators can be used on any expression of any one of the data types of the numeric data type category. The `~` (bitwise `NOT`) operator can be used only on expressions of any one of the data types of the integer data type category.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
- numeric_expression
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *numeric_expression*

Any valid [expression](expressions-transact-sql.md) of any one of the data types of the numeric data type category, except the date and time category.

## Return types

Returns the data type of *numeric_expression*, except that an unsigned **tinyint** expression is promoted to a signed **smallint** result.

## Examples

### A. Set a variable to a negative value

The following example sets a variable to a negative value.

```sql
USE tempdb;
GO

DECLARE @MyNumber DECIMAL(10, 2);
SET @MyNumber = -123.45;

SELECT @MyNumber AS NegativeValue;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
NegativeValue
--------------
-123.45
```

### B. Change a variable to a negative value

The following example changes a variable to a negative value.

```sql
USE tempdb;
GO

DECLARE @Num1 INT;
SET @Num1 = 5;

SELECT @Num1 AS VariableValue,
    -@Num1 AS NegativeValue;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
VariableValue NegativeValue
------------- -------------
5             -5
```

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### C. Return the negative of a positive constant

The following example returns the negative of a positive constant.

```sql
USE ssawPDW;
GO

SELECT TOP (1) - 17 FROM DimEmployee;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
-17
```

  Notice the same result returned as if the unary negative is applied to a value with unary [Unary operators - Positive](unary-operators-positive.md) applied.

```sql
USE ssawPDW;
GO

SELECT TOP (1) - (+ 17)
FROM DimEmployee;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
-17
```

### D. Return the positive of a negative constant

The following example returns the positive of a negative constant.

```sql
USE ssawPDW;
GO

SELECT TOP (1) - (- 17)
FROM DimEmployee;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
-17
```

### E. Return the negative of a column

The unary negative reverses the numeric operator of a column's values. As result, the negative values are returned from positive values, and positive values are returned from negative values.

The following example returns the negative of the `BaseRate` value for each employee in the `DimEmployee` table.

```sql
USE ssawPDW;
GO

SELECT - BaseRate
FROM DimEmployee;
```

## Related content

- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
- [Expressions (Transact-SQL)](expressions-transact-sql.md)
- [Operators (Transact-SQL)](operators-transact-sql.md)
