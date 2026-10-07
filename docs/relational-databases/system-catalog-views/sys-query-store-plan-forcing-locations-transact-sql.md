---
title: "sys.query_store_plan_forcing_locations (Transact-SQL)"
description: "The sys.query_store_plan_forcing_locations system view contains information about where Query Store plans have been forced on secondary replicas."
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/06/2026
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

Contains information about Query Store plans that are forced by using [sp_query_store_force_plan](../system-stored-procedures/sp-query-store-force-plan-transact-sql.md). Use this information to determine which queries have plans forced on a database's read/write replica (primary) and one or more read-only replicas.

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|`plan_forcing_location_id` |**bigint** |System-assigned ID for this plan forcing location. |
|`query_id` |**bigint**|References `query_id` in [sys.query_store_query](../../relational-databases/system-catalog-views/sys-query-store-query-transact-sql.md) | 
|`plan_id` |**bigint**|References `plan_id` in [sys.query_store_plan](../../relational-databases/system-catalog-views/sys-query-store-plan-transact-sql.md) |
|`replica_group_id` |**bigint** | From the parameter `force_plan_scope` in [sp_query_store_force_plan (Transact-SQL)](../system-stored-procedures/sp-query-store-force-plan-transact-sql.md). References `replica_group_id` in [sys.query_store_replicas](sys-query-store-replicas.md) |
|`timestamp` |**datetime** | UTC date and time when the plan forcing operation was applied. |
| `plan_forcing_type` | **int** | Type of plan forcing.<br /><br />`0` = `NONE`<br />`1` = `MANUAL`<br />`2` = `AUTO` |
| `plan_forcing_type_desc` | **nvarchar(60)** | Text description of `plan_forcing_type`.<br /><br />`NONE`: No plan forcing<br />`MANUAL`: Plan forced by a user<br />`AUTO`: Plan forced by automatic tuning |

## Permissions

Requires the `VIEW DATABASE STATE` permission.

### Permissions for SQL Server 2022 and later

Requires the `VIEW DATABASE PERFORMANCE STATE` permission on the database.

## Example

Use `sys.query_store_plan_forcing_locations`, joined with [sys.query_store_replicas](sys-query-store-replicas.md), to retrieve the top 20 Query Store plans that have been forced.

```sql
SELECT TOP (20)
    pfl.query_id,
    pfl.plan_id,
    CASE qsr.replica_group_id
        WHEN 1 THEN 'PRIMARY'
        WHEN 2 THEN 'SECONDARY'
        WHEN 3 THEN 'GEO SECONDARY'
        WHEN 4 THEN 'GEO HA SECONDARY'
        ELSE CONCAT('REPLICA_', qsr.replica_group_id)
    END AS replica_type,
    pfl.[timestamp],
    pfl.plan_forcing_type_desc
FROM sys.query_store_plan_forcing_locations AS pfl
INNER JOIN sys.query_store_replicas AS qsr
    ON qsr.replica_group_id = pfl.replica_group_id
ORDER BY pfl.[timestamp] DESC;
```

## Related content

- [sys.query_store_replicas (Transact-SQL)](sys-query-store-replicas.md)
- [sp_query_store_force_plan (Transact-SQL)](../system-stored-procedures/sp-query-store-force-plan-transact-sql.md)
- [Query Store for readable secondary replicas](../performance/query-store-for-secondary-replicas.md).
- [sys.database_query_store_internal_state (Transact-SQL)](sys-database-query-store-internal-state-transact-sql.md)
- [sys.query_store_plan (Transact-SQL)](sys-query-store-plan-transact-sql.md)
- [sys.query_store_query (Transact-SQL)](sys-query-store-query-transact-sql.md)
- [Monitor performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
