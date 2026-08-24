---
title: "sys.query_store_plan_forcing_locations (Transact-SQL)"
description: "The sys.query_store_plan_forcing_locations system view contains information about where Query Store plans have been forced on secondary replicas."
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/26/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
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
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16||=azuresqldb-current"
---
# sys.query_store_plan_forcing_locations (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb](../../includes/applies-to-version/sqlserver2025-asdb.md)]

Contains information about Query Store plans that have been forced on secondary replicas using [sp_query_store_force_plan](../system-stored-procedures/sp-query-store-force-plan-transact-sql.md), when Query Store for secondary replicas is enabled. You can use this information to determine what queries have plans forced on different replica sets.

Query Store for secondary replicas is supported starting in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, and in Azure SQL Database. For complete platform support, see [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md).

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|`plan_forcing_location_id` |**bigint** |System-assigned ID for this plan forcing location. |
|`query_id` |**bigint**|References `query_id` in [sys.query_store_query](../../relational-databases/system-catalog-views/sys-query-store-query-transact-sql.md) | 
|`plan_id` |**bigint**|References `plan_id` in [sys.query_store_plan](../../relational-databases/system-catalog-views/sys-query-store-plan-transact-sql.md) |
|`replica_group_id` |**bigint** | From the parameter `force_plan_scope` in [sp_query_store_force_plan (Transact-SQL)](../system-stored-procedures/sp-query-store-force-plan-transact-sql.md). References `replica_group_id` in [sys.query_store_replicas](sys-query-store-replicas.md) |

## Permissions

Requires the `VIEW DATABASE STATE` permission.

### Permissions for SQL Server 2022 and later

Requires the `VIEW DATABASE PERFORMANCE STATE` permission on the database.

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

## Related content

- [sys.query_store_replicas (Transact-SQL)](sys-query-store-replicas.md)
- [sp_query_store_force_plan (Transact-SQL)](../system-stored-procedures/sp-query-store-force-plan-transact-sql.md)
- [sys.database_query_store_internal_state (Transact-SQL)](sys-database-query-store-internal-state-transact-sql.md)
- [sys.query_store_plan (Transact-SQL)](sys-query-store-plan-transact-sql.md)
- [sys.query_store_query (Transact-SQL)](sys-query-store-query-transact-sql.md)
- [Monitor performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
- [Best practices for monitoring workloads with Query Store](../performance/best-practice-with-the-query-store.md)
