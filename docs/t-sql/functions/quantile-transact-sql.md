---
title: QUANTILE (Transact-SQL)
description: The QUANTILE function returns an exact continuous quantile of non-NULL numeric values. Review its syntax, interpolation behavior, and examples.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop
ms.date: 09/16/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "QUANTILE_TSQL"
  - "QUANTILE"
helpviewer_keywords:
  - "QUANTILE function"
  - "analytic functions, QUANTILE"
dev_langs:
  - TSQL
monikerRange: "=fabric"
---

# QUANTILE (Transact-SQL)

[!INCLUDE [fabric-se-dw](../../includes/applies-to-version/fabric-se-dw.md)]

The `QUANTILE` function returns an exact continuous quantile of non-`NULL` numeric values. You can use it as both an aggregate function and a window (analytic) function:

- Aggregate usage: Returns the requested quantile for an entire group.
- Window usage: Returns the requested quantile for each partition while preserving row-level output.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Aggregation function syntax:

```syntaxsql
QUANTILE ( numeric_literal , numeric_expression )
```

Analytic function syntax:

```syntaxsql
QUANTILE ( numeric_literal , numeric_expression ) OVER ( [ <partition_by_clause> ] )
```

## Arguments

#### *numeric_literal*

The quantile to calculate. The value must be in the inclusive range from `0.0` through `1.0`. For example, specify `0.5` to calculate the median or `0.95` to calculate the 95th percentile.

#### *numeric_expression*

The numeric expression whose quantile is calculated. Supported exact numeric types are `int`, `bigint`, `smallint`, `tinyint`, `numeric`, `decimal`, `smallmoney`, and `money`. Supported approximate numeric types are `float` and `real`.

#### OVER clause

The *partition_by_clause* divides the result set produced by the `FROM` clause into partitions, and the function is applied to each partition.

If you don't specify *partition_by_clause*, the function treats all rows of the query result set as a single partition.

The `OVER` clause doesn't support `ORDER BY`, `ROWS`, or `RANGE` for `QUANTILE`.

For more information, see [SELECT - OVER clause (Transact-SQL)](../queries/select-over-clause-transact-sql.md).

## Return types

Returns `float(53)`.

## Remarks

`QUANTILE` computes a continuous quantile over the ordered non-`NULL` input values. When the requested quantile falls between two values, the function interpolates between them. The result might not be a value that exists in the input rows.

The aggregate form of `QUANTILE(p, numeric_expression)` is equivalent to `PERCENTILE_CONT(p) WITHIN GROUP (ORDER BY numeric_expression)`. The analytic form has equivalent percentile-continuous semantics within each partition.

`NULL` values are ignored. If all input values are `NULL`, or if no rows qualify, `QUANTILE` returns `NULL`. When `ANSI_WARNINGS` is `ON`, eliminating `NULL` values produces the standard aggregate warning.

For the same non-`NULL` input values and *numeric_literal*, `QUANTILE` returns the same result.

`DISTINCT` isn't supported. Character, date, time, and datetime expressions aren't supported.

The `QUANTILE` function isn't supported in SQL Server, Azure SQL Database, Azure SQL Managed Instance, or SQL database in Fabric.

## Use case

Use `QUANTILE` to calculate quartiles, percentiles, and other exact distribution boundaries with concise syntax. For example, calculate p90 claim amounts by plan, p95 service latency by region, or Q1, median, and Q3 values for a business metric.

## Examples

### A. Calculate aggregate quantiles

This example calculates the first quartile, median, third quartile, and 95th percentile.

```sql
WITH Samples AS (
    SELECT *
    FROM (VALUES
        (1), (NULL), (2), (3), (4), (5), (6), (7), (8), (13)
    ) AS v(value)
)
SELECT
    QUANTILE(0.25, value) AS Q1_25thPercentile,
    QUANTILE(0.50, value) AS Median_50thPercentile,
    QUANTILE(0.75, value) AS Q3_75thPercentile,
    QUANTILE(0.95, value) AS P95
FROM Samples;
```

### B. Calculate a quantile for each group

This example calculates the 90th percentile claim amount for each insurance plan.

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
    QUANTILE(0.90, claim_amount) AS P90ClaimAmount
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
SELECT QUANTILE(0.90, discount_percent) AS P90DiscountPercent
FROM CampaignDiscounts
WHERE campaign_id = 9001;
```

### D. Calculate a partitioned window quantile

This example adds the 95th-percentile latency for each service to every request row.

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
    QUANTILE(0.95, latency_ms) OVER (PARTITION BY service) AS ServiceP95Latency
FROM ServiceLatency;
```

## Related content

- [Aggregate functions (Transact-SQL)](aggregate-functions-transact-sql.md)
- [Analytic functions (Transact-SQL)](analytic-functions-transact-sql.md)
- [PERCENTILE_CONT (Transact-SQL)](percentile-cont-transact-sql.md)
- [SELECT - OVER clause (Transact-SQL)](../queries/select-over-clause-transact-sql.md)
