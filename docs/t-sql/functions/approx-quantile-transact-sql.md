---
title: APPROX_QUANTILE (Transact-SQL)
description: The APPROX_QUANTILE function estimates a continuous quantile of non-NULL numeric values. Review its syntax, performance behavior, and examples.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop
ms.date: 09/16/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "APPROX_QUANTILE_TSQL"
  - "APPROX_QUANTILE"
helpviewer_keywords:
  - "APPROX_QUANTILE function"
  - "analytic functions, APPROX_QUANTILE"
dev_langs:
  - TSQL
monikerRange: "=fabric"
---

# APPROX_QUANTILE (Transact-SQL)

[!INCLUDE [fabric-se-dw](../../includes/applies-to-version/fabric-se-dw.md)]

The `APPROX_QUANTILE` function returns an approximate continuous quantile of non-`NULL` numeric values. You can use it as both an aggregate function and a window (analytic) function:

- Aggregate usage: Returns the requested approximate quantile for an entire group.
- Window usage: Returns the requested approximate quantile for each partition while preserving row-level output.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Aggregation function syntax:

```syntaxsql
APPROX_QUANTILE ( numeric_literal , numeric_expression )
```

Analytic function syntax:

```syntaxsql
APPROX_QUANTILE ( numeric_literal , numeric_expression ) OVER ( [ <partition_by_clause> ] )
```

## Arguments

#### *numeric_literal*

The quantile to estimate. The value must be in the inclusive range from `0.0` through `1.0`. For example, specify `0.5` to estimate the median or `0.95` to estimate the 95th percentile.

#### *numeric_expression*

The numeric expression whose approximate quantile is calculated. Supported exact numeric types are `int`, `bigint`, `smallint`, `tinyint`, `numeric`, `decimal`, `smallmoney`, and `money`. Supported approximate numeric types are `float` and `real`.

#### OVER clause

The *partition_by_clause* divides the result set produced by the `FROM` clause into partitions, and the function is applied to each partition.

If you don't specify *partition_by_clause*, the function treats all rows of the query result set as a single partition.

The `OVER` clause doesn't support `ORDER BY`, `ROWS`, or `RANGE` for `APPROX_QUANTILE`.

For more information, see [SELECT - OVER clause (Transact-SQL)](../queries/select-over-clause-transact-sql.md).

## Return types

Returns `float(53)`.

## Remarks

`APPROX_QUANTILE` estimates a continuous quantile over the ordered non-`NULL` input values. Use [QUANTILE](quantile-transact-sql.md) when you require an exact result.

Approximate results can differ slightly from exact `QUANTILE` results and can vary across executions because of execution plans and parallel merge paths. `APPROX_QUANTILE` is nondeterministic. For more information, see [Deterministic and nondeterministic functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).

`NULL` values are ignored. If all input values are `NULL`, or if no rows qualify, `APPROX_QUANTILE` returns `NULL`. When `ANSI_WARNINGS` is `ON`, eliminating `NULL` values produces the standard aggregate warning.

`APPROX_QUANTILE` is intended to reduce latency and memory usage compared with exact `QUANTILE` calculations on large datasets.

`DISTINCT` isn't supported. Character, date, time, and datetime expressions aren't supported.

The `APPROX_QUANTILE` function is available in Fabric Data Warehouse and the SQL analytics endpoint of Fabric items. The `APPROX_QUANTILE` function isn't supported in SQL Server, Azure SQL Database, Azure SQL Managed Instance, or SQL database in Fabric.

## Use case

Use `APPROX_QUANTILE` for large-scale dashboards, exploratory analysis, and grouped reporting where a close estimate is sufficient. For example, estimate p90 claim amounts, p95 service latency, or p99 telemetry values without the cost of an exact quantile calculation.

## Examples

### A. Calculate aggregate approximate quantiles

This example estimates the first quartile, median, third quartile, and 95th percentile.

```sql
WITH Samples AS (
    SELECT *
    FROM (VALUES
        (1), (NULL), (2), (3), (4), (5), (6), (7), (8), (13)
    ) AS v(value)
)
SELECT
    APPROX_QUANTILE(0.25, value) AS ApproxQ1,
    APPROX_QUANTILE(0.50, value) AS ApproxMedian,
    APPROX_QUANTILE(0.75, value) AS ApproxQ3,
    APPROX_QUANTILE(0.95, value) AS ApproxP95
FROM Samples;
```

### B. Calculate an approximate quantile for each group

This example estimates the 90th percentile claim amount for each insurance plan.

```sql
WITH Claims AS (
    SELECT *
    FROM (VALUES
        ('Plan-A', 420.00),
        ('Plan-A', 500.00),
        ('Plan-A', 610.00),
        ('Plan-B', 250.00),
        ('Plan-B', 275.00),
        ('Plan-B', 310.00)
    ) AS v(plan_name, claim_amount)
)
SELECT
    plan_name,
    APPROX_QUANTILE(0.90, claim_amount) AS ApproxP90ClaimAmount
FROM Claims
GROUP BY plan_name;
```

### C. Return NULL for an all-NULL group

This example returns `NULL` because all qualifying values are `NULL`.

```sql
WITH CampaignDiscounts AS (
    SELECT *
    FROM (VALUES
        (9001, NULL),
        (9001, NULL),
        (9001, NULL),
        (9002, 10.00)
    ) AS v(campaign_id, discount_percent)
)
SELECT APPROX_QUANTILE(0.90, discount_percent) AS ApproxP90DiscountPercent
FROM CampaignDiscounts
WHERE campaign_id = 9001;
```

### D. Calculate a partitioned window approximate quantile

This example adds the approximate 95th-percentile latency for each service to every request row.

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
    APPROX_QUANTILE(0.95, latency_ms) OVER (PARTITION BY service) AS ServiceApproxP95Latency
FROM ServiceLatency;
```

## Related content

- [Aggregate functions (Transact-SQL)](aggregate-functions-transact-sql.md)
- [Analytic functions (Transact-SQL)](analytic-functions-transact-sql.md)
- [APPROX_PERCENTILE_CONT (Transact-SQL)](approx-percentile-cont-transact-sql.md)
- [QUANTILE (Transact-SQL)](quantile-transact-sql.md)
- [SELECT - OVER clause (Transact-SQL)](../queries/select-over-clause-transact-sql.md)
