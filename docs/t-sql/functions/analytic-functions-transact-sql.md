---
title: Analytic Functions (Transact-SQL)
description: Analytic functions in Transact-SQL calculate an aggregate value based on a group of rows. Learn about the analytic functions in the SQL Database Engine.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/17/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
dev_langs:
  - TSQL
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Analytic functions (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-edge-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-edge-fabricse-fabricdw-fabricsqldb.md)]

Analytic functions calculate an aggregate value based on a group of rows. Unlike aggregate functions, however, analytic functions can return multiple rows for each group. Use analytic functions to compute moving averages, running totals, percentages or top-N results within a group.

The [Microsoft SQL Database Engine](../../database-engine/sql-database-engine.md) supports these analytic functions:

- [ANY_VALUE](any-value-transact-sql.md)
- [CUME_DIST](cume-dist-transact-sql.md)
- [FIRST_VALUE](first-value-transact-sql.md)
- [LAG](lag-transact-sql.md)
- [LAST_VALUE](last-value-transact-sql.md)
- [LEAD](lead-transact-sql.md)
- [PERCENT_RANK](percent-rank-transact-sql.md)
- [PERCENTILE_CONT](percentile-cont-transact-sql.md)  
- [PERCENTILE_DISC](percentile-disc-transact-sql.md)

## Related content

- [SELECT - OVER clause (Transact-SQL)](../queries/select-over-clause-transact-sql.md)
