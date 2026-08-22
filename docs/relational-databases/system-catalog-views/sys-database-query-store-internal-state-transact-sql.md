---
title: "sys.database_query_store_internal_state (Transact-SQL)"
description: "sys.database_query_store_internal_state contains information about queue length and memory usage for the Query Store when Query Store for secondary replicas is enabled."
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/17/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "QUERY_STORE_INTERNAL_STATE"
  - "SYS.QUERY_STORE_INTERNAL_STATE_TSQL"
  - "SYS.QUERY_STORE_INTERNAL_STATE"
  - "QUERY_STORE_INTERNAL_STATE_TSQL"
helpviewer_keywords:
  - "database_query_store_internal_state catalog view"
  - "sys.database_query_store_internal_state catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16||=azuresqldb-current"
---
# sys.database_query_store_internal_state (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb](../../includes/applies-to-version/sqlserver2025-asdb.md)]

Contains information about queue length and memory usage for the Query Store when the Query Store for secondary replicas is enabled. 

Query Store for secondary replicas is supported starting in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, and in Azure SQL Database. For complete platform support, see [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md).

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|`pending_message_count`|**bigint**|The number of messages waiting in the queue on the primary for the replica where the system view is being viewed from. Not nullable. |
|`messaging_memory_used_mb`|**bigint**|The amount of memory in total taken up by the messages in the queue. Not nullable.|

## Permissions

 Requires the **VIEW DATABASE STATE** permission.

## Remarks

 For information on configured replicas for Query Store, see [sys.query_store_replicas (Transact-SQL)](sys-query-store-replicas.md).

## Related content

- [Monitor performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
- [Query Store for readable secondary replicas (preview)](../performance/query-store-for-secondary-replicas.md)
- [sp_query_store_clear_message_queues (Transact-SQL)](../system-stored-procedures/sp-query-store-clear-message-queues-transact-sql.md)
- [sys.query_store_wait_stats (Transact-SQL)](sys-query-store-wait-stats-transact-sql.md)
- [sys.query_store_runtime_stats (Transact-SQL)](sys-query-store-runtime-stats-transact-sql.md)
- [sys.query_store_replicas (Transact-SQL)](sys-query-store-replicas.md)
- [sys.query_store_plan_forcing_locations (Transact-SQL)](sys-query-store-plan-forcing-locations-transact-sql.md)
