---
title: "CEILING (Transact-SQL)"
description: CEILING returns the smallest integer greater than, or equal to, the specified numeric expression.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/18/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CEILING_TSQL"
  - "CEILING"
helpviewer_keywords:
  - "smallest integer great than or equal to expression"
  - "integers [SQL Server]"
  - "CEILING function [Transact-SQL]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# CEILING (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

This function returns the smallest integer greater than, or equal to, the specified numeric expression.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
CEILING ( numeric_expression )
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *numeric_expression*

An [expression](../language-elements/expressions-transact-sql.md) of the exact numeric or approximate numeric data type category.

## Return types

The return type depends on the input type of *numeric_expression*:

| Input type | Return type |
| --- | --- |
| **float**, **real** | **float** |
| **decimal(*p*, *s*)** | **decimal(38, *s*)** |
| **int**, **smallint**, **tinyint** | **int** |
| **bigint** | **bigint** |
| **money**, **smallmoney** | **money** |
| **bit** | **float** |

If the result doesn't fit in the return type, an arithmetic overflow error occurs.

## Examples

This example shows positive numeric, negative numeric, and zero value inputs for the `CEILING` function.

```sql
SELECT CEILING($123.45), CEILING($-123.45), CEILING($0.0);
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
--------- --------- -------------------------
124.00    -123.00    0.00
```

## Related content

- [System Functions (Transact-SQL)](../../relational-databases/system-functions/system-functions-category-transact-sql.md)
