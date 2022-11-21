---
title: "sys.query_store_replicas (Transact-SQL)"
description: "The sys.query_store_replicas system view contains information about Query Store replicas."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 10/11/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
f1_keywords:
  - "SYS.QUERY_STORE_REPLICAS_TSQL"
  - "QUERY_STORE_REPLICAS_TSQL"
  - "SYS.QUERY_STORE_REPLICAS"
  - "QUERY_STORE_REPLICAS"
helpviewer_keywords:
  - "query_store_replicas catalog view"
  - "sys.query_store_replicas catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16||=azuresqldb-mi-current"
---
# sys.query_store_replicas (Transact-SQL)

[!INCLUDE [sqlserver2022-asmi](../../includes/applies-to-version/sqlserver2022-asmi.md)]

Contains information about Query Store replicas, when [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md) is enabled. You can use this information to determine what `replica_group_id` to use when using Query Store to force or un-force a plan on a secondary replica with [sys.sp_query_store_set_query_hints](../system-stored-procedures/sys-sp-query-store-set-hints-transact-sql.md).

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|**replica_group_id**|**bigint**|Identifies the replica set number for this replica.|
|**role_id**|**tinyint**|1 = Primary<BR />2= Secondary<BR />3=Geo-Primary<BR />4=Geo-Secondary |
|**replica_name**|**nvarchar(max)**|Instance name of the replica in the availability group. `NULL` for replicas in [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)] or [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. |

## Remarks

This catalog view will return the same row data on all replicas. The catalog view will contain a row per replica for every `role_id` where it has been observed. For example, a two-replica availability group will initially contain two rows. After a failover, it will contain four rows: one row for each replica in both the primary and secondary roles.

## Permissions

Requires the `VIEW DATABASE STATE` permission.

## Next steps 

- [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md)
- [sys.database_query_store_internal_state (Transact-SQL)](sys-database-query-store-internal-state-transact-sql.md)
- [sys.query_store_plan (Transact-SQL)](../../relational-databases/system-catalog-views/sys-query-store-plan-transact-sql.md)
- [sys.query_store_query (Transact-SQL)](../../relational-databases/system-catalog-views/sys-query-store-query-transact-sql.md)
- [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [Best Practice with the Query Store](../../relational-databases/performance/best-practice-with-the-query-store.md)
