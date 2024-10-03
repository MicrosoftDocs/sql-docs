---
title: "sp_query_store_flush_db (Transact-SQL)"
description: "Flushes the in-memory portion of the Query Store data to disk."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_query_store_flush_db_TSQL"
  - "sys.sp_query_store_flush_db_TSQL"
  - "sp_query_store_flush_db"
  - "sys.sp_query_store_flush_db"
helpviewer_keywords:
  - "sys.sp_query_store_flush_db"
  - "sp_query_store_flush_db"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_query_store_flush_db (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Flushes the in-memory portion of the Query Store data to disk.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_query_store_flush_db
[ ; ]
```

## Return code values

`0` (success) or `1` (failure).

## Remarks

If [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md) is enabled, when `sys.sp_query_store_flush_db` is executed on a secondary replica, that secondary replica's cache is forced to flush to the cache on the primary replica. This can accelerate the Query Store cache data being synced to the primary replica, if the secondary replica cache flush is otherwise delayed under heavy workload.

## Permissions

Requires the ALTER permission on the database.

## Examples

The following example flushes the in-memory portion of the Query Store data to disk.

```sql
EXEC sp_query_store_flush_db;
```

## Related content

- [Monitor performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
- [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md)
- [sp_query_store_force_plan (Transact-SQL)](sp-query-store-force-plan-transact-sql.md)
- [sp_query_store_remove_query (Transact-SQL)](sp-query-store-remove-query-transact-sql.md)
- [sp_query_store_unforce_plan (Transact-SQL)](sp-query-store-unforce-plan-transact-sql.md)
- [sp_query_store_reset_exec_stats (Transact-SQL)](sp-query-store-reset-exec-stats-transact-sql.md)
- [sp_query_store_remove_plan (Transact-SQL)](sp-query-store-remove-plan-transact-sql.md)
- [Query Store catalog views (Transact-SQL)](../system-catalog-views/query-store-catalog-views-transact-sql.md)
