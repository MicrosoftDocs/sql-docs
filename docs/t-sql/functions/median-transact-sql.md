---
title: MEDIAN (Transact-SQL)
description: The MEDIAN function returns the exact median of non-NULL numeric values in a group or partition. Review its syntax, behavior, and examples.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop
ms.date: 09/16/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "MEDIAN_TSQL"
  - "MEDIAN"
helpviewer_keywords:
  - "MEDIAN function"
  - "analytic functions, MEDIAN"
dev_langs:
  - TSQL
monikerRange: "=fabric"
---

# MEDIAN (Transact-SQL)

[!INCLUDE [fabric-se-dw](../../includes/applies-to-version/fabric-se-dw.md)]

The `MEDIAN` function returns the exact median, or 50th percentile, of non-`NULL` numeric values. You can use it as both an aggregate function and a window (analytic) function:

- Aggregate usage: Returns the median for an entire group.
- Window usage: Returns the median for each partition while preserving row-level output.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Aggregation function syntax:

```syntaxsql
MEDIAN ( numeric_expression )
```

Analytic function syntax:

```syntaxsql
MEDIAN ( numeric_expression ) OVER ( [ <partition_by_clause> ] )
```

## Arguments

#### *numeric_expression*

The numeric expression whose median is calculated. Supported exact numeric types are `int`, `bigint`, `smallint`, `tinyint`, `numeric`, `decimal`, `bit`, `smallmoney`, and `money`. Supported approximate numeric types are `float` and `real`.

#### OVER clause

The *partition_by_clause* divides the result set produced by the `FROM` clause into partitions, and the function is applied to each partition.

If you don't specify *partition_by_clause*, the function treats all rows of the query result set as a single partition.

The `OVER` clause doesn't support `ORDER BY`, `ROWS`, or `RANGE` for `MEDIAN`.

For more information, see [SELECT - OVER clause (Transact-SQL)](../queries/select-over-clause-transact-sql.md).

## Return types

Returns `float(53)`.

## Remarks

`MEDIAN` computes the continuous 50th percentile of the ordered non-`NULL` input values. For an even number of input values, the function interpolates between the two middle values. The result might not be a value that exists in the input rows.

`MEDIAN` is equivalent to `PERCENTILE_CONT(0.5) WITHIN GROUP (ORDER BY numeric_expression)` for aggregate usage. The analytic form has equivalent percentile-continuous semantics within each partition.

`NULL` values are ignored. If all input values are `NULL`, or if no rows qualify, `MEDIAN` returns `NULL`. When `ANSI_WARNINGS` is `ON`, eliminating `NULL` values produces the standard aggregate warning.

`MEDIAN` is nondeterministic because floating-point calculations in parallel execution paths can produce slight variations. For more information, see [Deterministic and nondeterministic functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).

`DISTINCT` isn't supported. Character, date, time, and datetime expressions aren't supported.

The `MEDIAN` function is available in Fabric Data Warehouse and the SQL analytics endpoint of Fabric items. The `MEDIAN` function isn't supported in SQL Server, Azure SQL Database, Azure SQL Managed Instance, or SQL database in Fabric.

## Use case

Use `MEDIAN` when an average could be distorted by unusually high or low values. For example, median order value, response time, or claim amount often represents a typical observation more clearly than the arithmetic mean. The aggregate form summarizes groups, and the analytic form adds a partition benchmark to each detail row.

## Examples

### A. Calculate an aggregate median

This example returns the median of eight values. Because the input has an even number of rows, `MEDIAN` interpolates between 4 and 5.

```sql
SELECT MEDIAN(value) AS MedianValue
FROM (VALUES (1), (2), (3), (4), (5), (6), (7), (8)) AS t(value);
```

The result is `4.5`.

### B. Calculate a median for each group

This example calculates the median order amount for each sales region.

```sql
WITH SalesOrders AS (
    SELECT *
    FROM (VALUES
        ('North', 120.00),
        ('North', 220.00),
        ('North', 320.00),
        ('South', 150.00),
        ('South', 250.00),
        ('South', 450.00)
    ) AS v(region, order_amount)
)
SELECT
    region,
    MEDIAN(order_amount) AS MedianOrderAmount
FROM SalesOrders
GROUP BY region;
```

### C. Ignore NULL values

This example ignores the `NULL` input and calculates the median from the remaining values.

```sql
SELECT MEDIAN(value) AS MedianValue
FROM (VALUES (1), (2), (NULL), (3), (4)) AS t(value);
```

If every input value is `NULL`, the function returns `NULL`.

### D. Calculate a partitioned window median

This example adds the median latency for each service to every request row.

```sql
WITH ServiceLatency AS (
    SELECT *
    FROM (VALUES
        ('Checkout', 'req-001', 180),
        ('Checkout', 'req-002', 220),
        ('Checkout', 'req-003', 260),
        ('Search', 'req-010', 90),
        ('Search', 'req-011', 110),
        ('Search', 'req-012', 130)
    ) AS v(service, request_id, latency_ms)
)
SELECT
    service,
    request_id,
    latency_ms,
    MEDIAN(latency_ms) OVER (PARTITION BY service) AS ServiceMedianLatency
FROM ServiceLatency;
```

## Related content

- [Aggregate functions (Transact-SQL)](aggregate-functions-transact-sql.md)
- [Analytic functions (Transact-SQL)](analytic-functions-transact-sql.md)
- [PERCENTILE_CONT (Transact-SQL)](percentile-cont-transact-sql.md)
- [SELECT - OVER clause (Transact-SQL)](../queries/select-over-clause-transact-sql.md)
