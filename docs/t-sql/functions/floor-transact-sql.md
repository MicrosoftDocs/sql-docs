---
title: "FLOOR (Transact-SQL)"
description: FLOOR returns the largest integer less than or equal to the specified numeric expression.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/21/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "FLOOR_TSQL"
  - "FLOOR"
helpviewer_keywords:
  - "integers [SQL Server]"
  - "largest integers"
  - "FLOOR function [Transact-SQL]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# FLOOR (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Returns the largest integer less than or equal to the specified numeric expression.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
FLOOR ( numeric_expression )
```

## Arguments

#### *numeric_expression*

An [expression](../language-elements/expressions-transact-sql.md) of the exact numeric or approximate numeric data type category.

## Return types

The return type depends on the input type of *numeric_expression*:

| Input type | Return type |
| --- | --- |
| **float**, **real** | **float** |
| **decimal(*p*, *s*)** | **decimal(*p*, 0)** |
| **int**, **smallint**, **tinyint** | **int** |
| **bigint** | **bigint** |
| **money**, **smallmoney** | **money** |
| **bit** | **float** |

If the result doesn't fit in the return type, an arithmetic overflow error occurs.

For more information, see [Precision, scale, and length](../data-types/precision-scale-and-length-transact-sql.md).

## Examples

The following example shows positive numeric, negative numeric, and currency values with the `FLOOR` function.

```sql
SELECT FLOOR(123.45),
       FLOOR(-123.45),
       FLOOR($123.45);
```

The result is the integer part of the calculated value in the same data type as *numeric_expression*.

```output
---- ----- -------
123  -124  123.00
```

## Related content

- [CEILING (Transact-SQL)](ceiling-transact-sql.md)
- [System Functions by category for Transact-SQL](../../relational-databases/system-functions/system-functions-category-transact-sql.md)
- [Mathematical functions (Transact-SQL)](mathematical-functions-transact-sql.md)
