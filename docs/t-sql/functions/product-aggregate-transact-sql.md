---
title: "PRODUCT (Transact-SQL)"
description: PRODUCT (Transact-SQL)
author: AaronPitman
ms.author: aaronpitman
ms.reviewer: randolphwest
ms.date: 11/18/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "PRODUCT"
  - "PRODUCT_TSQL"
helpviewer_keywords:
  - "values [SQL Server], PRODUCT of all"
  - "PRODUCT function [Transact-SQL]"
  - "values [SQL Server]"
  - "PRODUCT [SQL Server]"
  - "expressions [SQL Server], PRODUCT of all values"
  - "DISTINCT keyword"
  - "totals [SQL Server], PRODUCT"
  - "multiply values [SQL Server]"
dev_langs:
  - TSQL
monikerRange: "=fabric || =sql-server-ver17 || =sql-server-linux-ver17"
---

# PRODUCT (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-asa-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-asa-fabricdw-fabricsqldb.md)]

The `PRODUCT` function returns the product of all the values, or only the `DISTINCT` values, in an expression. Use with numeric columns only. Null values are ignored.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Aggregate function syntax:

```syntaxsql
PRODUCT ( [ ALL | DISTINCT ] expression )
```

Analytic function syntax:

```syntaxsql
PRODUCT ( [ ALL ] expression) OVER ( [ partition_by_clause ] [ order_by_clause ] )
```

## Arguments

#### ALL

Applies the aggregate function to all values. `ALL` is the default.

#### DISTINCT

Specifies that `PRODUCT` returns the product of unique values.

#### *expression*

A constant, column, or function, and any combination of arithmetic, bitwise, and string operators. *expression* is an expression of the exact numeric or approximate numeric data type category, except for the **bit** data type. Aggregate functions and subqueries aren't permitted. For more information, see [Expressions](../language-elements/expressions-transact-sql.md).

#### OVER ( [ *partition_by_clause* ] [ *order_by_clause* ] )

Determines the partitioning and ordering of a rowset before the function is applied.

*partition_by_clause* divides the result set produced by the `FROM` clause into partitions to which the function is applied. If not specified, the function treats all rows of the query result set as a single group.

*order_by_clause* determines the logical order in which the operation is performed. For more information, see [SELECT - OVER clause](../queries/select-over-clause-transact-sql.md).

## Return types

Returns the product of all *expression* values in the most precise *expression* data type.

| Expression result | Return type |
| --- | --- |
| **tinyint** | **int** |
| **smallint** | **int** |
| **int** | **int** |
| **bigint** | **bigint** |
| **decimal** category (*p*, *s*) | If *s* is `0`, **decimal(38, 0)**, otherwise **decimal(38, 6)** |
| **money** and **smallmoney** category | **money** |
| **float** and **real** category | **float** |

## Remarks

Support for `PRODUCT` in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] is limited to [!INCLUDE [ssazure-sqlmi-autd](../../includes/applies-to-version/ssazure-sqlmi-autd.md)].

`PRODUCT` is a deterministic function when used without the `OVER` and `ORDER BY` clauses. It's nondeterministic when specified with the `OVER` and `ORDER BY` clauses. For more information, see [Deterministic and nondeterministic functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Multiply rows together

The following example uses the `PRODUCT` function:

```sql
SELECT PRODUCT(UnitPrice) AS ProductOfPrices
FROM Purchasing.PurchaseOrderDetail
WHERE ModifiedDate <= '2023-05-24'
GROUP BY ProductId;
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
ProductOfPrices
----------
2526.2435
41.916
3251.9077
640559.8491
1469352.0378
222137708.073
11432159376.271
5898056028.2633
14030141.2883
2526.4194
```

### B. Use the OVER clause

The following example uses the `PRODUCT` function with the `OVER` clause to provide a rate of return on hypothetical financial instruments. The data is partitioned by `finInstrument`.

```sql
SELECT finInstrument,
       PRODUCT(1 + rateOfReturn) OVER (PARTITION BY finInstrument) AS CompoundedReturn
FROM (VALUES (0.1626, 'instrumentA'),
             (0.0483, 'instrumentB'),
             (0.2689, 'instrumentC'),
             (-0.1944, 'instrumentA'),
             (0.2423, 'instrumentA')
) AS MyTable(rateOfReturn, finInstrument);
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```sql
finInstrument CompoundedReturn
------------- ---------------------------------------
instrumentA   1.163527
instrumentA   1.163527
instrumentA   1.163527
instrumentB   1.048300
instrumentC   1.268900
```

## Related content

- [Aggregate functions (Transact-SQL)](aggregate-functions-transact-sql.md)
- [SELECT - OVER clause (Transact-SQL)](../queries/select-over-clause-transact-sql.md)
