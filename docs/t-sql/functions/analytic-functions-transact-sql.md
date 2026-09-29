---
title: Analytic Functions (Transact-SQL)
description: Analytic functions in Transact-SQL calculate an aggregate value based on a group of rows. Learn about the analytic functions in the SQL Database Engine.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 09/16/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ai-usage: ai-assisted
ms.custom:
  - ignite-2025
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Analytic functions (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-edge-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-edge-fabricse-fabricdw-fabricsqldb.md)]

Analytic functions calculate an aggregate value based on a group of rows. Unlike aggregate functions, however, analytic functions can return multiple rows for each group. Use analytic functions to compute moving averages, running totals, percentages or top-N results within a group.

The [Microsoft SQL Database Engine](../../database-engine/sql-database-engine.md) provides the following analytic functions in some or all platforms. Refer to each syntax article for applicable platforms.

- [ANY_VALUE](any-value-transact-sql.md) - Returns any non-`NULL` value from a group of rows, or `NULL` if all values are `NULL`.
- [APPROX_MEDIAN](approx-median-transact-sql.md) - Returns an approximate median value for a set of values.
- [APPROX_QUANTILE](approx-quantile-transact-sql.md) - Returns an approximate quantile value for a specified quantile position.
- [CUME_DIST](cume-dist-transact-sql.md) - Calculates the cumulative distribution of a value within a group of values.
- [FIRST_VALUE](first-value-transact-sql.md) - Returns the first value in an ordered set of values.
- [LAG](lag-transact-sql.md) - Returns a value from a previous row in the same result set without requiring a self-join.
- [LAST_VALUE](last-value-transact-sql.md) - Returns the last value in an ordered set of values.
- [LEAD](lead-transact-sql.md) - Returns a value from a subsequent row in the same result set without requiring a self-join.
- [MEDIAN](median-transact-sql.md) - Returns the median value of the values in a group.
- [PERCENT_RANK](percent-rank-transact-sql.md) - Calculates the relative rank of a row within a group of rows.
- [PERCENTILE_CONT](percentile-cont-transact-sql.md) - Calculates a percentile based on a continuous distribution, interpolating a result that might not equal a value in the group.
- [PERCENTILE_DISC](percentile-disc-transact-sql.md) - Calculates a percentile based on a discrete distribution, returning a value from the group.
- [QUANTILE](quantile-transact-sql.md) - Returns the value corresponding to a specified quantile within a group.

## Related content

- [SELECT - OVER clause (Transact-SQL)](../queries/select-over-clause-transact-sql.md)
