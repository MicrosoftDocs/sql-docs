---
title: "NULL and UNKNOWN (Transact-SQL)"
description: Learn about NULL and UNKNOWN, and how they work in Transact-SQL.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 09/12/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =azure-sqldw-latest || =fabric"
---
# NULL and UNKNOWN (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

`NULL` indicates that the value is *unknown*. A null value is different from an empty or zero value. No two null values are equal. Comparisons between two null values, or between a null value and any other value, return unknown because the value of each `NULL` is unknown.

Null values generally indicate data that is unknown, not applicable, or to be added later. For example, a customer's middle initial might not be known at the time the customer places an order.

Consider:

- To test for null values in a query, use `IS NULL` or `IS NOT NULL` in the `WHERE` clause.

- You can insert null values into a column by explicitly stating `NULL` in an `INSERT` or `UPDATE` statement, or by leaving a column out of an `INSERT` statement.

- Null values can't be used as information that is required to distinguish one row in a table from another row in a table. Examples include primary keys, or for information used to distribute rows, such as distribution keys.

## Remarks

When null values are present in data, logical and comparison operators can potentially return a third result of `UNKNOWN` instead of just `TRUE` or `FALSE`. This need for three-valued logic is a source of many application errors. Logical operators in a boolean expression that includes `UNKNOWN` return `UNKNOWN`, unless the result of the operator doesn't depend on the `UNKNOWN` expression. These tables provide examples of this behavior.

The following table shows the results of applying an `AND` operator to two Boolean expressions where one expression returns `UNKNOWN`.

| Expression 1 | Expression 2 | Result |
| --- | --- | --- |
| `TRUE` | `UNKNOWN` | `UNKNOWN` |
| `UNKNOWN` | `UNKNOWN` | `UNKNOWN` |
| `FALSE` | `UNKNOWN` | `FALSE` |

The following table shows the results of applying an `OR` operator to two Boolean expressions where one expression returns `UNKNOWN`.

| Expression 1 | Expression 2 | Result |
| --- | --- | --- |
| `TRUE` | `UNKNOWN` | `TRUE` |
| `UNKNOWN` | `UNKNOWN` | `UNKNOWN` |
| `FALSE` | `UNKNOWN` | `UNKNOWN` |

## Related content

- [AND (Transact-SQL)](and-transact-sql.md)
- [OR (Transact-SQL)](or-transact-sql.md)
- [NOT (Transact-SQL)](not-transact-sql.md)
- [IS NULL (Transact-SQL)](../queries/is-null-transact-sql.md)
- [IS [NOT] DISTINCT FROM (Transact-SQL)](../queries/is-distinct-from-transact-sql.md)
