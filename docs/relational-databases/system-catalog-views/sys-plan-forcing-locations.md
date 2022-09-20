---
title: "sys.plan_forcing_locations (Transact-SQL)"
description: "The sys.plan_forcing_locations system view contains information about plan forcing on Query Store replicas."
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
  - "plan_forcing_locations catalog view"
  - "sys.plan_forcing_locations catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16"
---
# sys.plan_forcing_locations (Transact-SQL)

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

Contains information about plan forcing on Query Store replicas, when [Query Store for secondary replicas](../performance/monitoring-performance-by-using-the-query-store.md#query-store-for-secondary-replicas) is enabled. You can use this information to determine what `replica_group_id` to use when using Query Store to force or un-force a plan on a secondary replica.

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|**replica_group_id**|**bigint**|Identifies the replica set identifier for which this query was executed with this plan.|
|**query_id**|**bigint**|Query Store unique identifier for a query. Foreign key. Joins to [sys.query_store_query](sys-query-store-query-transact-sql.md).|  
|**plan_id**|**bigint**|Query Store unique identifier for a forced plan.|  

## Permissions

Requires the `VIEW DATABASE STATE` permission.

## Example

Use the following sample query, joined with [sys.query_store_plan](sys-query-store-plan-transact-sql.md), to retrieve all plans forced on all secondary replicas.

```sql
SELECT query_plan 
FROM sys.query_store_plan AS qsp
    INNER JOIN sys.plan_forcing_locations AS pfl 
        ON pfl.query_id = qsp.query_id 
    INNER JOIN sys.query_store_replicas AS qsr
        ON qsr.replica_group_id = qsp.replica_group_id
WHERE qsr.replica_name = 'yourSecondaryReplicaName';
```

## Next steps

- [sys.query_store_replicas (Transact-SQL)](../../relational-databases/system-catalog-views/sys-query-store-replicas.md)
- [sys.database_query_store_internal_state (Transact-SQL)](sys-database-query-store-internal-state-transact-sql.md)
- [sys.query_store_plan (Transact-SQL)](../../relational-databases/system-catalog-views/sys-query-store-plan-transact-sql.md)
- [sys.query_store_query (Transact-SQL)](../../relational-databases/system-catalog-views/sys-query-store-query-transact-sql.md)
- [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [Best Practice with the Query Store](../../relational-databases/performance/best-practice-with-the-query-store.md)
