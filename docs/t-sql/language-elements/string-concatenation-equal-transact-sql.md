---
title: "+= String concatenation"
description: Concatenates two strings and sets the string to the result of the operation.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/07/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "concatenate strings"
  - "string concatenation"
  - "+= (concatenate operator)"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# += (String concatenation assignment) (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Concatenates two strings and sets the string to the result of the operation. For example, if a variable `@x` equals `'Adventure'`, then `@x += 'Works'` takes the original value of `@x`, adds `'Works'` to the string, and sets `@x` to that new value `'AdventureWorks'`.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
expression += expression
```

## Arguments

#### *expression*

Any valid [expression](expressions-transact-sql.md) of any of the character data types.

## Return types

Returns the data type that is defined for the variable.

## Remarks

`SET @v1 += 'expression'` is equivalent to `SET @v1 = @v1 + ('expression')`. Also, `SET @v1 = @v2 + @v3 + @v4` is equivalent to `SET @v1 = (@v2 + @v3) + @v4`.

The `+=` operator can't be used without a variable. For example, the following code causes an error:

```sql
SELECT 'Adventure' += 'Works'
```

## Examples

### A. Concatenation using the += operator

The following example concatenates using the `+=` operator.

```sql
DECLARE @v1 VARCHAR(40);
SET @v1 = 'This is the original.';
SET @v1 += ' More text.';
PRINT @v1;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
This is the original. More text.
```

### B. Order of evaluation while concatenating using the += operator

The following example concatenates multiple strings to form one long string and then tries to compute the length of the final string. This example demonstrates the evaluation order and truncation rules, while using the concatenation operator.

```sql
DECLARE @x VARCHAR(4000) = REPLICATE('x', 4000);
DECLARE @z VARCHAR(8000) = REPLICATE('z', 8000);
DECLARE @y VARCHAR(MAX);

SET @y = '';
SET @y += @x + @z;
SELECT LEN(@y) AS Y; -- 8000

SET @y = '';
SET @y = @y + @x + @z;
SELECT LEN(@y) AS Y; -- 12000

SET @y = '';
SET @y = @y + (@x + @z);
SELECT LEN(@y) AS Y; -- 8000

SET @y = '';
SET @y = @x + @z + @y;
SELECT LEN(@y) AS Y; -- 8000
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Y
-------
8000

Y
-------
12000

Y
-------
8000

Y
-------
8000
```

## Related content

- [Compound operators (Transact-SQL)](compound-operators-transact-sql.md)
- [Operators (Transact-SQL)](operators-transact-sql.md)
- [+= (Addition assignment) (Transact-SQL)](add-equals-transact-sql.md)
- [+ (String concatenation) (Transact-SQL)](string-concatenation-transact-sql.md)
