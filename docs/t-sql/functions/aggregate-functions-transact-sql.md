---
title: Aggregate Functions (Transact-SQL)
description: An aggregate function in Transact-SQL performs a calculation on a set of values, and returns a single value. Learn about the aggregate functions in the SQL Database Engine. 
author: markingmyname
ms.author: maghan
ms.date: 03/17/2026
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
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Aggregate functions (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

An aggregate function in the [Microsoft SQL Database Engine](../../database-engine/sql-database-engine.md) performs a calculation on a set of values, and returns a single value. Except for `COUNT(*)`, aggregate functions ignore `NULL` values. Aggregate functions are often used with the `GROUP BY` clause of the SELECT statement.

All aggregate functions are deterministic. In other words, aggregate functions return the same value each time that they are called, when called with a specific set of input values. See [Deterministic and nondeterministic functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md) for more information about function determinism. The [OVER clause](../queries/select-over-clause-transact-sql.md) might follow all aggregate functions, except the `STRING_AGG`, `GROUPING`, or `GROUPING_ID` functions.

Use aggregate functions as expressions only in the following situations:

- The select list of a `SELECT` statement (either a subquery or an outer query).
- A `HAVING` clause.

[!INCLUDE[tsql](../../includes/tsql-md.md)] provides the following aggregate functions:

- [ANY_VALUE](any-value-transact-sql.md)
- [APPROX_COUNT_DISTINCT](approx-count-distinct-transact-sql.md)
- [AVG](avg-transact-sql.md)
- [CHECKSUM_AGG](checksum-agg-transact-sql.md)
- [COUNT](count-transact-sql.md)
- [COUNT_BIG](count-big-transact-sql.md)
- [GROUPING](grouping-transact-sql.md)
- [GROUPING_ID](grouping-id-transact-sql.md)
- [MAX](max-transact-sql.md)
- [MIN](min-transact-sql.md)
- [PRODUCT](product-aggregate-sql.md) (applies to SQL Server 2025 (17.x), Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, Warehouse in Microsoft Fabric, SQL database in Microsoft Fabric)
- [STDEV](stdev-transact-sql.md)
- [STDEVP](stdevp-transact-sql.md)
- [STRING_AGG](string-agg-transact-sql.md)
- [SUM](sum-transact-sql.md)
- [VAR](var-transact-sql.md)
- [VARP](varp-transact-sql.md)

## Related content

- [What are the SQL database functions?](functions.md)
- [SELECT - OVER clause (Transact-SQL)](../queries/select-over-clause-transact-sql.md)
