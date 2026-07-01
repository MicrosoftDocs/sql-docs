---
title: "sys.query_store_replicas (Transact-SQL)"
description: The sys.query_store_replicas system view contains information about Query Store replicas.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 05/26/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SYS.QUERY_STORE_REPLICAS_TSQL"
  - "QUERY_STORE_REPLICAS_TSQL"
  - "SYS.QUERY_STORE_REPLICAS"
  - "QUERY_STORE_REPLICAS"
helpviewer_keywords:
  - "query_store_replicas catalog view"
  - "sys.query_store_replicas catalog view"
dev_langs:
  - TSQL
monikerRange: ">=sql-server-ver16 || >=sql-server-linux-ver16 || =azuresqldb-current "
---
# sys.query_store_replicas (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb](../../includes/applies-to-version/sqlserver2025-asdb.md)]

Contains information about Query Store replicas, when [Query Store for readable secondaries](../performance/query-store-for-secondary-replicas.md) is enabled. You can use this information to determine what `replica_group_id` to use when using Query Store to force or unforce a plan on a secondary replica with [sys.sp_query_store_set_query_hints](../system-stored-procedures/sys-sp-query-store-set-hints-transact-sql.md).

Query Store for secondary replicas is supported starting in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, and in Azure SQL Database. For complete platform support, see [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md).

| Column name | Data type | Description |
| --- | --- | --- |
| `replica_group_id` | **bigint** | Identifies the replica set number for this replica. |
| `role_type` | **tinyint** | 1=Primary<br />2=Secondary<br />3=Geo-Primary<br />4=Geo-Secondary<br />5 or greater=Named replica |
| `replica_name` | **nvarchar(max)** | Instance name of the replica. `NULL` for replicas in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]. |

## Remarks

This catalog view returns the same row data on all replicas. The catalog view contains a row per replica for every `role_type` where it was observed. For example, a two-replica availability group initially contains two rows. After a failover, it contains four rows: one row for each replica in both the primary and secondary roles.

## Permissions

Requires the `VIEW DATABASE STATE` permission.

### Permissions for SQL Server 2022 and later

Requires the `VIEW DATABASE PERFORMANCE STATE` permission on the database.

## Related content

- [Query Store for readable secondaries](../performance/query-store-for-secondary-replicas.md)
- [sys.database_query_store_internal_state (Transact-SQL)](sys-database-query-store-internal-state-transact-sql.md)
- [sys.query_store_plan (Transact-SQL)](sys-query-store-plan-transact-sql.md)
- [sys.query_store_query (Transact-SQL)](sys-query-store-query-transact-sql.md)
- [Monitor performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
- [Best practices for monitoring workloads with Query Store](../performance/best-practice-with-the-query-store.md)

