---
title: APPROX_MEDIAN (Transact-SQL)
description: The APPROX_MEDIAN function estimates the median of non-NULL numeric values for faster analysis. Review its syntax, behavior, and examples.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop
ms.date: 09/16/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "APPROX_MEDIAN_TSQL"
  - "APPROX_MEDIAN"
helpviewer_keywords:
  - "APPROX_MEDIAN function"
  - "analytic functions, APPROX_MEDIAN"
dev_langs:
  - TSQL
monikerRange: "=fabric"
---

# APPROX_MEDIAN (Transact-SQL)

[!INCLUDE [fabric-se-dw](../../includes/applies-to-version/fabric-se-dw.md)]

The `APPROX_MEDIAN` function returns an approximate median, or 50th percentile, of non-`NULL` numeric values. You can use it as both an aggregate function and a window (analytic) function:

- Aggregate usage: Returns an approximate median for an entire group.
- Window usage: Returns an approximate median for each partition while preserving row-level output.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Aggregation function syntax:

```syntaxsql
APPROX_MEDIAN ( numeric_expression )
```

Analytic function syntax:

```syntaxsql
APPROX_MEDIAN ( numeric_expression ) OVER ( [ <partition_by_clause> ] )
```

## Arguments

#### *numeric_expression*

The numeric expression whose approximate median is calculated. Supported exact numeric types are `int`, `bigint`, `smallint`, `tinyint`, `numeric`, `decimal`, `smallmoney`, and `money`. Supported approximate numeric types are `float` and `real`.

#### OVER clause

The *partition_by_clause* divides the result set produced by the `FROM` clause into partitions, and the function is applied to each partition.

If you don't specify *partition_by_clause*, the function treats all rows of the query result set as a single partition.

The `OVER` clause doesn't support `ORDER BY`, `ROWS`, or `RANGE` for `APPROX_MEDIAN`.

For more information, see [SELECT - OVER clause (Transact-SQL)](../queries/select-over-clause-transact-sql.md).

## Return types

Returns `float(53)`.

## Remarks

`APPROX_MEDIAN` estimates the continuous 50th percentile of the ordered non-`NULL` input values. Use [MEDIAN](median-transact-sql.md) when you require an exact result.

Approximate results can differ slightly from exact `MEDIAN` results and can vary across executions because of execution plans and parallel merge paths. `APPROX_MEDIAN` is nondeterministic. For more information, see [Deterministic and nondeterministic functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).

`NULL` values are ignored. If all input values are `NULL`, or if no rows qualify, `APPROX_MEDIAN` returns `NULL`. When `ANSI_WARNINGS` is `ON`, eliminating `NULL` values produces the standard aggregate warning.

`APPROX_MEDIAN` is intended to reduce latency and memory usage compared with exact `MEDIAN` calculations on large datasets.

`DISTINCT` isn't supported. Character, date, time, and datetime expressions aren't supported.

The `APPROX_MEDIAN` function is available in Fabric Data Warehouse and the SQL analytics endpoint of Fabric items. The `APPROX_MEDIAN` function isn't supported in SQL Server, Azure SQL Database, Azure SQL Managed Instance, or SQL database in Fabric.

## Use case

Use `APPROX_MEDIAN` for large-scale dashboards, exploratory analysis, and grouped reporting where a close estimate is sufficient and query performance matters more than an exact median. Common examples include approximate median order value, service latency, and telemetry measurements.

## Examples

### A. Calculate an aggregate approximate median

This example estimates the median of eight values.

```sql
SELECT APPROX_MEDIAN(value) AS ApproxMedianValue
FROM (VALUES (1), (2), (3), (4), (5), (6), (7), (8)) AS t(value);
```

The result is close to the exact median of `4.5`, but it can vary slightly.

### B. Calculate an approximate median for each group

This example estimates the median order amount for each sales region.

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
    APPROX_MEDIAN(order_amount) AS ApproxMedianOrderAmount
FROM SalesOrders
GROUP BY region;
```

### C. Ignore NULL values

This example ignores `NULL` values when estimating the median.

```sql
SELECT APPROX_MEDIAN(value) AS ApproxMedianValue
FROM (VALUES (1), (2), (NULL), (3), (4)) AS t(value);
```

If every input value is `NULL`, the function returns `NULL`.

### D. Calculate a partitioned window approximate median

This example adds the approximate median latency for each service to every request row.

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
    APPROX_MEDIAN(latency_ms) OVER (PARTITION BY service) AS ServiceApproxMedianLatency
FROM ServiceLatency;
```

## Related content

- [Aggregate functions (Transact-SQL)](aggregate-functions-transact-sql.md)
- [Analytic functions (Transact-SQL)](analytic-functions-transact-sql.md)
- [APPROX_PERCENTILE_CONT (Transact-SQL)](approx-percentile-cont-transact-sql.md)
- [MEDIAN (Transact-SQL)](median-transact-sql.md)
- [SELECT - OVER clause (Transact-SQL)](../queries/select-over-clause-transact-sql.md)
