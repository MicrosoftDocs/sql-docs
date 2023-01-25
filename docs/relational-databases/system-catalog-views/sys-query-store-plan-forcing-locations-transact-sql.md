---
title: "sys.query_store_plan_forcing_locations (Transact-SQL)"
description: "The sys.query_store_plan_forcing_locations system view contains information about where Query Store plans have been forced on secondary replicas."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 10/14/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
f1_keywords:
  - "SYS.query_store_plan_forcing_locations_TSQL"
  - "query_store_plan_forcing_locations_TSQL"
  - "SYS.query_store_plan_forcing_locations"
  - "query_store_plan_forcing_locations"
helpviewer_keywords:
  - "query_store_plan_forcing_locations catalog view"
  - "sys.query_store_plan_forcing_locations catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16||=azuresqldb-mi-current"
---
# sys.query_store_plan_forcing_locations (Transact-SQL)

[!INCLUDE [sqlserver2022-asmi](../../includes/applies-to-version/sqlserver2022-asmi.md)]

Contains information about Query Store plans that have been forced on secondary replicas using [sp_query_store_force_plan](../system-stored-procedures/sp-query-store-force-plan-transact-sql.md), when [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md) is enabled. You can use this information to determine what queries have plans forced on different replica sets.

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|**plan_forcing_location_id**|**bigint** |System-assigned ID for this plan forcing location. |
|**query_id**|**bigint**|References `query_id` in [sys.query_store_query](../../relational-databases/system-catalog-views/sys-query-store-query-transact-sql.md) | 
|**plan_id** |**bigint**|References `plan_id` in [sys.query_store_plan](../../relational-databases/system-catalog-views/sys-query-store-plan-transact-sql.md) |
|**replica_group_id**  |**bigint** | From the parameter `force_plan_scope` in [sp_query_store_force_plan (Transact-SQL)](../system-stored-procedures/sp-query-store-force-plan-transact-sql.md). References `replica_group_id` in [sys.query_store_replicas](sys-query-store-replicas.md) |

## Permissions

Requires the `VIEW DATABASE STATE` permission.

## Example

Use `sys.query_store_plan_forcing_locations`, joined with [sys.query_store_replicas](sys-query-store-replicas.md), to retrieve [Query Store plans forced on all secondary replicas](../performance/query-store-for-secondary-replicas.md).

```sql
SELECT query_plan 
FROM sys.query_store_plan AS qsp
    INNER JOIN sys.query_store_plan_forcing_locations AS pfl 
        ON pfl.query_id = qsp.query_id 
    INNER JOIN sys.query_store_replicas AS qsr
        ON qsr.replica_group_id = qsp.replica_group_id
WHERE qsr.replica_name = 'yourSecondaryReplicaName';
```

## Next steps 

- [sys.query_store_replicas (Transact-SQL)](sys-query-store-replicas.md)
- [sys.sp_query_store_force_plan (Transact-SQL)](../system-stored-procedures/sp-query-store-force-plan-transact-sql.md)
- [sys.database_query_store_internal_state (Transact-SQL)](sys-database-query-store-internal-state-transact-sql.md)
- [sys.query_store_plan (Transact-SQL)](../../relational-databases/system-catalog-views/sys-query-store-plan-transact-sql.md)
- [sys.query_store_query (Transact-SQL)](../../relational-databases/system-catalog-views/sys-query-store-query-transact-sql.md)
- [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [Best Practice with the Query Store](../../relational-databases/performance/best-practice-with-the-query-store.md)
