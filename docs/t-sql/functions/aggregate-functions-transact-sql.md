---
title: Aggregate Functions (Transact-SQL)
description: An aggregate function in Transact-SQL performs a calculation on a set of values, and returns a single value. Learn about the aggregate functions in the SQL Database Engine. 
author: markingmyname
ms.author: maghan
ms.reviewer: wiassaf
ms.date: 09/16/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "functions [SQL Server], aggregate"
  - "aggregate functions [SQL Server], about aggregate functions"
  - "summarizing functions"
  - "aggregate functions [SQL Server]"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Aggregate functions (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricse-fabricdw-fabricsqldb.md)]

An aggregate function in the [Microsoft SQL Database Engine](../../database-engine/sql-database-engine.md) performs a calculation on a set of values, and returns a single value. 

- Except for `COUNT(*)`, aggregate functions ignore `NULL` values. 
- Aggregate functions are often used with the `GROUP BY` clause of the `SELECT` statement.
- Unless otherwise noted, aggregate functions are deterministic. In other words, aggregate functions return the same value each time that they are called, when called with a specific set of input values. For example, `APPROX_MEDIAN` and `APPROX_QUANTILE` are not deterministic. See [Deterministic and nondeterministic functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md) for more information about function determinism. 
- The [OVER clause](../queries/select-over-clause-transact-sql.md) can follow all aggregate functions, except the `STRING_AGG`, `GROUPING`, or `GROUPING_ID` functions.
- Use aggregate functions as expressions only in the select list of a `SELECT` statement (either a subquery or outer query), or in a `HAVING` clause.

The [Microsoft SQL Database Engine](../../database-engine/sql-database-engine.md) provides the following aggregate functions in some or all platforms. Refer to each syntax article for applicable platforms.

- [ANY_VALUE](any-value-transact-sql.md) - Picks an arbitrary value from the rows in a group and returns it.
- [APPROX_COUNT_DISTINCT](approx-count-distinct-transact-sql.md) - Returns an approximate count of distinct non-null values using a memory-efficient algorithm.
- [APPROX_MEDIAN](approx-median-transact-sql.md) - Returns an approximate median value for a set of values.
- [APPROX_PERCENTILE_CONT](approx-percentile-cont-transact-sql.md) - Returns an approximate percentile value using continuous interpolation.
- [APPROX_PERCENTILE_DISC](approx-percentile-disc-transact-sql.md) - Returns an approximate percentile value selected from the actual data values.
- [APPROX_QUANTILE](approx-quantile-transact-sql.md) - Returns an approximate quantile value for a specified quantile position.
- [AVG](avg-transact-sql.md) - Calculates the average of the values in a group.
- [CHECKSUM_AGG](checksum-agg-transact-sql.md) - Returns a checksum value computed over the values in a group.
- [COUNT](count-transact-sql.md) - Returns the number of rows or non-null values in a group.
- [COUNT_BIG](count-big-transact-sql.md) - Returns the number of rows or non-null values in a group as a **bigint** type.
- [GROUPING](grouping-transact-sql.md) - Indicates whether a column value in a result row was aggregated by a grouping operation.
- [GROUPING_ID](grouping-id-transact-sql.md) - Returns a bitmask that identifies which columns were aggregated in a grouping set.
- [PRODUCT](product-aggregate-transact-sql.md) - Returns the product of the non-null values in a group.
- [MAX](max-transact-sql.md) - Returns the maximum value in a group.
- [MEDIAN](median-transact-sql.md) - Returns the median value of the values in a group.
- [MIN](min-transact-sql.md) - Returns the minimum value in a group.
- [PERCENTILE_CONT](percentile-cont-transact-sql.md) - Calculates a percentile based on a continuous distribution of the column value.
- [PERCENTILE_DISC](percentile-disc-transact-sql.md) - Computes a specific percentile for sorted values in an entire rowset or within a rowset's distinct partitions.
- [QUANTILE](quantile-transact-sql.md) - Returns the value corresponding to a specified quantile within a group.
- [STDEV](stdev-transact-sql.md) - Returns the sample standard deviation of the values in a group.
- [STDEVP](stdevp-transact-sql.md) - Returns the population standard deviation of the values in a group.
- [STRING_AGG](string-agg-transact-sql.md) - Concatenates string values from multiple rows into a single string.
- [SUM](sum-transact-sql.md) - Returns the sum of the values in a group.
- [VAR](var-transact-sql.md) - Returns the sample variance of the values in a group.
- [VARP](varp-transact-sql.md) - Returns the population variance of the values in a group.

## Related content

- [What are the SQL database functions?](functions.md)
- [SELECT - OVER clause (Transact-SQL)](../queries/select-over-clause-transact-sql.md)
