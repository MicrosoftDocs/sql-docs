---
title: "||= (Compound assignment) (Transact-SQL)"
description: "Use ||= to concatenate an expression with the value of a character or binary string variable, and assign the resulting expression to the variable."
author: abhimantiwari
ms.author: abhtiwar
ms.reviewer: randolphwest, wiassaf, umajay
ms.date: 06/04/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "concatenation"
  - "string"
helpviewer_keywords:
  - "string concatenation with compound assignment operation"
  - "||= (string concatenation with compound assignment)"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---

# ||= (Compound assignment) (Transact-SQL)

[!INCLUDE [asdb](../../includes/applies-to-version/asdb.md)]

The `||=` concatenation with compound assignment operator can be used to [concatenate an expression](string-concatenation-pipes-transact-sql.md) with the value of a character or binary string variable, and then assign the resulting expression to the variable.

The `||=` operator supports the same behavior as the [+= operator](string-concatenation-equal-transact-sql.md) for character and binary strings.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
variable ||= expression
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *variable*

A T-SQL variable of character type: **char**, **varchar**, **nchar**, **nvarchar**, **varchar(max)**, or **nvarchar(max)**, or of binary type: **binary** or **varbinary** or **varbinary(max)**.

#### *expression*

A character or binary [expression](expressions-transact-sql.md). If the expression isn't of the character type, then the type of the expression must be able to be implicitly converted to a character string.

## Return types

Assigns the result of the concatenation operator for character strings to the variable.

- If the variable or expression is a SQL `NULL` value, then the result of the concatenated expression is `NULL`.
- If the variable is of a large object (LOB) data type (**varchar(max)** or **nvarchar(max)**), then the resulting expression is of **varchar(max)** or **nvarchar(max)**.
- If the variable is of a LOB type **varbinary(max)**, then the resulting expression is of **varbinary(max)**.
- If the variable isn't a LOB type, then the result is truncated to the maximum length of declared type of the variable.

## Remarks

If the result of the concatenation of strings exceeds the limit of 8,000 bytes, the result is truncated. However, if at least one of the strings concatenated is a large value type, truncation doesn't occur.

An explicit conversion to character data must be used when concatenating binary strings and any characters between the binary strings.

### Zero-length strings and characters

The `||=` (string concatenation) operator behaves differently when it works with an empty, zero-length string than when it works with `NULL`, or unknown values. A zero-length character string can be specified as two single quotation marks without any characters inside the quotation marks. A zero-length binary string can be specified as `0x` without any byte values specified in the hexadecimal constant. Concatenating a zero-length string always concatenates the two specified strings.

### Concatenation of NULL values

As with arithmetic operations that are performed on `NULL` values, when a `NULL` value is added to a known value, the result is typically a `NULL` value. A string concatenation operation performed with a `NULL` value should also produce a `NULL` result.

The `||=` operator doesn't honor the `SET CONCAT_NULL_YIELDS_NULL` option, and always behaves as if the ANSI SQL behavior is enabled, yielding `NULL` if any of the inputs is `NULL`. This is the primary difference in behavior between the `+=` and `||=` concatenation operators. For more information, see [SET CONCAT_NULL_YIELDS_NULL](../statements/set-concat-null-yields-null-transact-sql.md).

## Examples

### A. Use concatenation with compound assignment for strings

```sql
DECLARE @v1 varchar(10) = 'a'
SET @v1 ||= 'b';
SELECT @v1
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
ab
```

### B. Use concatenation with compound assignment for binary data

```sql
DECLARE @v2 varbinary(10) = 0x1a;
SET @v2 ||= 0x2b;
select @v2;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
0x1A2B
```

## Related content

- [&#124;&#124; (String concatenation) (Transact-SQL)](string-concatenation-pipes-transact-sql.md)
- [+ (String concatenation) (Transact-SQL)](string-concatenation-transact-sql.md)
- [+= (String Concatenation Assignment) (Transact-SQL)](string-concatenation-equal-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../statements/alter-database-transact-sql.md)
- [CAST and CONVERT (Transact-SQL)](../functions/cast-and-convert-transact-sql.md)
- [Data type conversion (Database Engine)](../data-types/data-type-conversion-database-engine.md)
- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
- [Expressions (Transact-SQL)](expressions-transact-sql.md)
- [Built-in Functions (Transact-SQL)](../functions/functions.md)
- [Operators (Transact-SQL)](operators-transact-sql.md)
- [SELECT (Transact-SQL)](../queries/select-transact-sql.md)
- [SET Statements (Transact-SQL)](../statements/set-statements-transact-sql.md)
