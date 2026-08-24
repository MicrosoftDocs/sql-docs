---
title: "ROUND (Transact-SQL)"
description: ROUND returns a numeric value, rounded to the specified length or precision.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 03/31/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "ROUND_TSQL"
  - "ROUND"
helpviewer_keywords:
  - "rounding expressions"
  - "ROUND function [Transact-SQL]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# ROUND (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Returns a numeric value, rounded to the specified length or precision.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
ROUND ( numeric_expression , length [ , function ] )
```

## Arguments

#### *numeric_expression*

An [expression](../../t-sql/language-elements/expressions-transact-sql.md) of the exact numeric or approximate numeric data type category.

#### *length*

The precision to which *numeric_expression* is to be rounded. *length* must be an expression of type **tinyint**, **smallint**, or **int**. When *length* is a positive number, *numeric_expression* is rounded to the number of decimal positions specified by *length*. When *length* is a negative number, *numeric_expression* is rounded on the left side of the decimal point, as specified by *length*.

#### *function*

The type of operation to perform. *function* must be **tinyint**, **smallint**, or **int**. When *function* is omitted or has a value of `0` (default), *numeric_expression* is rounded. When a value other than `0` is specified, *numeric_expression* is truncated.

## Return types

Returns the following data types.

| Expression result | Return type |
| --- | --- |
| **tinyint** | **int** |
| **smallint** | **int** |
| **int** | **int** |
| **bigint** | **bigint** |
| **decimal** and **numeric** category (p, s) | **decimal(p, s)** |
| **money** and **smallmoney** category | **money** |
| **float** and **real** category | **float** |

## Remarks

- `ROUND` always returns a value. If *length* is negative and larger than the number of digits before the decimal point, `ROUND` returns `0`.

  | Example | Result |
  | --- | --- |
  | `ROUND(748.58, -4)` | 0 |

- `ROUND` returns a rounded *numeric_expression*, regardless of data type, when *length* is a negative number.

  | Examples | Result |
  | --- | --- |
  | `ROUND(748.58, -1)` | 750.00 |
  | `ROUND(748.58, -2)` | 700.00 |
  | `ROUND(748.58, -3)` | Results in an arithmetic overflow, because 748.58 defaults to **decimal(5, 2)**, which can't return `1000.00`. |

- To round up to four digits, change the data type of the input. For example:

  ```sql
  SELECT ROUND(CAST (748.58 AS DECIMAL (6, 2)), -3);
  ```

  [!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

  ```output
  1000.00
  ```

- `ROUND` breaks ties by rounding half away from zero (also known as commercial rounding).

  | Examples | Result |
  | --- | --- |
  | `ROUND(1.15, 1)` | 1.2 |
  | `ROUND(-1.15, 1)` | -1.2 |

## Examples

### A. Use ROUND and estimates

The following example shows two expressions that demonstrate by using `ROUND`, the last digit is always an estimate.

```sql
SELECT ROUND(123.9994, 3),
       ROUND(123.9995, 3);
GO
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
----------- -----------
123.9990    124.0000
```

### B. Use ROUND and rounding approximations

The following example shows rounding and approximations.

```sql
SELECT ROUND(123.4545, 2),
       ROUND(123.45, -2);
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
----------  ----------
123.4500    100.00
```

### C. Use ROUND to truncate

The following example uses two `SELECT` statements to demonstrate the difference between rounding and truncation. The first statement rounds the result. The second statement truncates the result.

```sql
SELECT ROUND(150.75, 0);
GO
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
151.00
```

```sql
SELECT ROUND(150.75, 0, 1);
GO
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
150.00
```

## Related content

- [CEILING (Transact-SQL)](ceiling-transact-sql.md)
- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
- [Expressions (Transact-SQL)](../language-elements/expressions-transact-sql.md)
- [FLOOR (Transact-SQL)](floor-transact-sql.md)
- [Mathematical functions (Transact-SQL)](mathematical-functions-transact-sql.md)
