---
title: "sys.database_query_store_internal_state (Transact-SQL)"
description: "sys.database_query_store_internal_state contains information about queue length and memory usage for the Query Store when Query Store for secondary replicas is enabled."
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/19/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom: event-tier1-build-2022
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
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16||=azuresqldb-mi-current"
---
# sys.database_query_store_internal_state (Transact-SQL)

[!INCLUDE [sqlserver2022-asmi](../../includes/applies-to-version/sqlserver2022-asmi.md)]

Contains information about queue length and memory usage for the Query Store when [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md) is enabled.

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|**pending_message_count**|**bigint**|The number of messages waiting in the queue on the primary for the replica where the system view is being viewed from. Not nullable. |
|**messaging_memory_used_mb**|**bigint**|The amount of memory in total taken up by the messages in the queue. Not nullable.|

## Permissions

 Requires the **VIEW DATABASE STATE** permission.

## Remarks

 For information on configured replicas for Query Store, see [sys.query_store_replicas (Transact-SQL)](sys-query-store-replicas.md).

## Next steps

Learn more about Query Store and related concepts in the following articles:

- [Monitor performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
- [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md)
- [sp_query_store_clear_message_queues (Transact-SQL)](../system-stored-procedures/sp-query-store-clear-message-queues-transact-sql.md)
- [sys.query_store_wait_stats (Transact-SQL)](sys-query-store-wait-stats-transact-sql.md)
- [sys.query_store_runtime_stats (Transact-SQL)](sys-query-store-runtime-stats-transact-sql.md)
- [sys.query_store_replicas (Transact-SQL)](sys-query-store-replicas.md)
- [sys.query_store_plan_forcing_locations (Transact-SQL)](sys-query-store-plan-forcing-locations-transact-sql.md)
