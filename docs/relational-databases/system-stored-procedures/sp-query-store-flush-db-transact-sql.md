---
title: "sp_query_store_flush_db (Transact-SQL)"
description: "sp_query_store_flush_db (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: 09/30/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom: event-tier1-build-2022
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
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_query_store_flush_db (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Flushes the in-memory portion of the Query Store data to disk.

 :::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_query_store_flush_db [;]
```

## Return Code Values

 0 (success) or 1 (failure)

## Remarks

If [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md) is enabled, when `sys.sp_query_store_flush_db` is executed on a secondary replica, that secondary replica's cache is forced to be flushed to the cache on the primary replica. This can accelerate the Query Store cache data being synced to the primary replica if the secondary replica cache flush is otherwise delayed under heavy workload.

## Permissions

 Requires the **ALTER** permission on the database.

## Examples

 The following example flushes the in-memory portion of the Query Store data to disk.

```sql
EXEC sp_query_store_flush_db;
```

## Next steps

Learn more about Query Store in the following articles:

- [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md)
- [sp_query_store_force_plan (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-force-plan-transact-sql.md)
- [sp_query_store_remove_query (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-remove-query-transact-sql.md)
- [sp_query_store_unforce_plan (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-unforce-plan-transact-sql.md)
- [sp_query_store_reset_exec_stats (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-reset-exec-stats-transact-sql.md)
- [sp_query_store_remove_plan (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-remove-plan-transct-sql.md)
- [sp_query_store_reset_exec_stats (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-query-store-reset-exec-stats-transact-sql.md)
- [Query Store Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/query-store-catalog-views-transact-sql.md)
