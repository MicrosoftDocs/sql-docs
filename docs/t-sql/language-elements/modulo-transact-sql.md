---
title: "% (Modulus) (Transact-SQL)"
description: "The % (modulus) operator returns the remainder of one number divided by another."
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/19/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "modulo"
  - "modulus"
  - "% (Modulo)"
  - "% (Modulus)"
  - "MOD_TSQL"
helpviewer_keywords:
  - "% (modulo operator)"
  - "% (modulus operator)"
  - "remainder of division operation"
  - "modulo operator (%)"
  - "modulus operator (%)"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# % (Modulus) (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns the remainder of one number divided by another.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
dividend % divisor
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *dividend*

The numeric expression to divide. *dividend* must be a valid [expression](expressions-transact-sql.md) of any one of the data types in the integer and monetary data type categories, or the **numeric** data type.

#### *divisor*

The numeric expression by which to divide the dividend. *divisor* must be any valid expression of any one of the data types in the integer and monetary data type categories, or the **numeric** data type.

## Result types

Determined by data types of the two arguments.

## Remarks

You can use the modulo arithmetic operator in the select list of the `SELECT` statement with any combination of column names, numeric constants, or any valid expression of the integer and monetary data type categories, or the **numeric** data type.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Basic example

The following example divides the number `38` by `5`. The result is `7` as the integer portion of the result, and demonstrates how modulo returns the remainder of `3`.

```sql
SELECT
    38 / 5 AS [Integer],
    38 % 5 AS [Remainder];
```

### B. Example using columns in a table

The following example returns the product ID number, the unit price of the product, and the modulo (remainder) of dividing the price of each product, converted to an integer value, into the number of products ordered.

```sql
SELECT TOP (100) ProductID,
    UnitPrice,
    OrderQty,
    CAST((UnitPrice) AS INT) % OrderQty AS Modulo
FROM Sales.SalesOrderDetail;
GO
```

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

### C: Basic example

The following example shows results for the `%` operator when dividing `3` by `2`.

```sql
SELECT TOP(1) 3 % 2
FROM DimEmployee;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
1
```

## Related content

- [What are the SQL database functions?](../functions/functions.md)
- [LIKE (Transact-SQL)](like-transact-sql.md)
- [Operators (Transact-SQL)](operators-transact-sql.md)
- [SELECT (Transact-SQL)](../queries/select-transact-sql.md)
- [%= (Modulus assignment) (Transact-SQL)](modulo-equals-transact-sql.md)
- [Compound Operators (Transact-SQL)](compound-operators-transact-sql.md)
