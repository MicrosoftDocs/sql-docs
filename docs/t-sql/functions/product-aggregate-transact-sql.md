---
title: "PRODUCT (Transact-SQL)"
description: "PRODUCT (Transact-SQL)"
author: AaronPitman
ms.author: aaronpitman
ms.reviewer: randolphwest
ms.date: 05/05/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
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
  - "TSQL"
monikerRange: "=fabric || =sql-server-ver17 || =sql-server-linux-ver17"
ms.custom:
  - build-2025
---

# PRODUCT (Transact-SQL)

**Applies to:** [!INCLUDE [sqlserver2025-asdb-asa-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asa-fabricsqldb.md)]

Returns the PRODUCT of all the values, or only the DISTINCT values, in the expression. Use with numeric columns only. Null values are ignored.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Aggregate function syntax.

```syntaxsql
PRODUCT ( [ ALL | DISTINCT ] expression )
```

Analytic function syntax.

```syntaxsql
PRODUCT ( [ ALL ] expression) OVER ( [ PARTITION BY clause ] [ORDER BY clause])
```

## Arguments

#### *ALL*

Applies the aggregate function to all values. ALL is the default.

#### *DISTINCT*

Specifies that PRODUCT returns the PRODUCT of unique values.

#### *expression*

A constant, column, or function, and any combination of arithmetic, bitwise, and string operators. *expression* is an expression of the exact numeric or approximate numeric data type category, except for the **bit** data type. Aggregate functions and subqueries aren't permitted. For more information, see [Expressions](../../t-sql/language-elements/expressions-transact-sql.md).

#### OVER ( [ PARTITION BY clause ] ORDER BY *clause* )

Determines the partitioning and ordering of a rowset before the function is applied.

`PARTITION BY` clause divides the result set produced by the FROM clause into partitions to which the function is applied. If not specified, the function treats all rows of the query result set as a single group.

`ORDER BY` clause determines the logical order in which the operation is performed. For more information, see [OVER clause](../../t-sql/queries/select-over-clause-transact-sql.md).

## Return types

Returns the product of all *expression* values in the most precise *expression* data type.

| Expression result | Return type |
| --- | --- |
| **tinyint** | **int** |
| **smallint** | **int** |
| **int** | **int** |
| **bigint** | **bigint** |
| **decimal** category (p, s) | If (s == 0): **decimal(38, 0)** Else: **decimal(38, 6)** |
| **money** and **smallmoney** category | **money** |
| **float** and **real** category | **float** |

## Remarks

`PRODUCT` is a deterministic function when used without the `OVER` and `ORDER BY` clauses. It's nondeterministic when specified with the `OVER` and `ORDER BY` clauses. For more information, see [Deterministic and nondeterministic functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Multiply rows together

The following examples show using the PRODUCT function

```sql
SELECT PRODUCT(UnitPrice) AS ProductOfPrices
FROM Purchasing.PurchaseOrderDetail
WHERE ModifiedDate <= '2002-05-24'
GROUP BY ProductId;
```

Here's a partial result set.

```output
ProductOfPrices
----------
2526.2435
41.916
3251.9077
21656.2655
40703.3993
4785336.3939
11432159532.8367
5898056095.7678
```

### B. Use the OVER clause

The following example uses the PRODUCT function with the OVER clause to provide a rate of return on hypothetical financial instruments. The data is partitioned by `finInstrument`.

```sql
SELECT finInstrument,
    PRODUCT(1 + rateOfReturn)
        OVER (PARTITION BY finInstrument) AS CompoundedReturn
FROM (
    VALUES (0.1626, 'instrumentA'),
           (0.0483, 'instrumentB'),
           (0.2689, 'instrumentC'),
           (-0.1944, 'instrumentA'),
           (0.2423, 'instrumentA'))
AS MyTable(rateOfReturn, finInstrument);
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

- [Aggregate Functions (Transact-SQL)](../../t-sql/functions/aggregate-functions-transact-sql.md)
- [SELECT - OVER clause (Transact-SQL)](../../t-sql/queries/select-over-clause-transact-sql.md)
