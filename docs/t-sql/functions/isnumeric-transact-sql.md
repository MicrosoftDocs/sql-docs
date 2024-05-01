---
title: "ISNUMERIC (Transact-SQL)"
description: ISNUMERIC determines whether an expression is a valid numeric type.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 02/05/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ISNUMERIC"
  - "ISNUMERIC_TSQL"
helpviewer_keywords:
  - "expressions [SQL Server], valid numeric type"
  - "numeric data"
  - "ISNUMERIC function"
  - "verifying valid numeric type"
  - "valid numeric type [SQL Server]"
  - "checking valid numeric type"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# ISNUMERIC (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Determines whether an expression is a valid numeric type.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
ISNUMERIC ( expression )
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *expression*

The [expression](../language-elements/expressions-transact-sql.md) to be evaluated.

## Return types

**int**

## Remarks

`ISNUMERIC` returns `1` when the input expression evaluates to a valid numeric data type; otherwise it returns `0`. Valid [numeric data types](../data-types/numeric-types.md) include the following items:

| Area | Numeric data types |
| --- | --- |
| [Exact numerics](../data-types/int-bigint-smallint-and-tinyint-transact-sql.md) | **bigint**, **int**, **smallint**, **tinyint**, **bit** |
| [Fixed precision](../data-types/decimal-and-numeric-transact-sql.md) | **decimal**, **numeric** |
| [Approximate](../data-types/float-and-real-transact-sql.md) | **float**, **real** |
| [Monetary values](../data-types/money-and-smallmoney-transact-sql.md) | **money**, **smallmoney** |

`ISNUMERIC` returns `1` for some characters that aren't numbers, such as plus (`+`), minus (`-`), and valid currency symbols such as the dollar sign (`$`). For a complete list of currency symbols, see [money and smallmoney (Transact-SQL)](../data-types/money-and-smallmoney-transact-sql.md).

## Examples

The following example uses `ISNUMERIC` to return all the postal codes that aren't numeric values.

```sql
USE AdventureWorks2022;
GO

SELECT City,
    PostalCode
FROM Person.Address
WHERE ISNUMERIC(PostalCode) <> 1;
GO
```

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

The following example uses `ISNUMERIC` to return whether the database name and ID are numeric values.

```sql
USE master;
GO

SELECT name,
    ISNUMERIC(name) AS IsNameANumber,
    database_id,
    ISNUMERIC(database_id) AS IsIdANumber
FROM sys.databases;
GO
```

## Related content

- [Expressions (Transact-SQL)](../language-elements/expressions-transact-sql.md)
- [System Functions by category for Transact-SQL](../../relational-databases/system-functions/system-functions-category-transact-sql.md)
- [Data types (Transact-SQL)](../data-types/data-types-transact-sql.md)
