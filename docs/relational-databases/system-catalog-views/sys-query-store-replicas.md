---
title: "sys.query_store_replicas (Transact-SQL)"
description: "The sys.query_store_replicas system view contains information about Query Store replicas."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 09/19/2022
ms.prod: sql
ms.technology: system-objects
ms.topic: "reference"
ms.custom:
f1_keywords:
  - "SYS.QUERY_STORE_RUNTIME_STATS_TSQL"
  - "QUERY_STORE_RUNTIME_STATS_TSQL"
  - "SYS.QUERY_STORE_RUNTIME_STATS"
  - "QUERY_STORE_RUNTIME_STATS"
helpviewer_keywords:
  - "query_store_replicas catalog view"
  - "sys.query_store_replicas catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16||=azuresqldb-mi-current"
---
# sys.query_store_replicas (Transact-SQL)

[!INCLUDE [sqlserver2022-asmi](../../includes/applies-to-version/sqlserver2022-asmi.md)]

Contains information about Query Store replicas, when [Query Store for secondary replicas](../performance/monitoring-performance-by-using-the-query-store.md#query-store-for-secondary-replicas) is enabled. You can use this information to determine what `replica_group_id` to use when using Query Store to force or un-force a plan on a secondary replica.

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|**replica_group_id**|**bigint**|Identifies the replica set number for this replica.|
|**role_id**|**tinyint**|1 = Primary<BR />2= Secondary<BR />3=Geo-Primary<BR />4=Geo-Secondary |
|**replica_name**|**nvarchar(max)**|Provided at the time of configuration of the availability group. `NULL` for replicas in [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)] or [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. |

## Permissions

Requires the `VIEW DATABASE STATE` permission.

## Next steps 

- [sys.database_query_store_internal_state (Transact-SQL)](sys-database-query-store-internal-state-transact-sql.md)
- [sys.query_store_plan (Transact-SQL)](../../relational-databases/system-catalog-views/sys-query-store-plan-transact-sql.md)
- [sys.query_store_query (Transact-SQL)](../../relational-databases/system-catalog-views/sys-query-store-query-transact-sql.md)
- [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [Best Practice with the Query Store](../../relational-databases/performance/best-practice-with-the-query-store.md)
