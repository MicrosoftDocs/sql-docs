---
title: "sys.dm_hs_database_log_rate (Transact-SQL)"
description: The sys.dm_hs_database_log_rate dynamic management function returns information about log generation rate in Azure SQL Database Hyperscale.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman, atsingh
ms.date: 08/11/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_hs_database_log_rate"
  - "sys.dm_hs_database_log_rate_TSQL"
  - "dm_hs_database_log_rate"
  - "dm_hs_database_log_rate_TSQL"
helpviewer_keywords:
  - "sys.dm_hs_database_log_rate catalog view"
  - "sys.dm_hs_database_log_rate function"
  - "sys.dm_hs_database_log_rate DMF"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# sys.dm_hs_database_log_rate (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

The `sys.dm_hs_database_log_rate` dynamic management function (DMF) returns information about log generation rate in Azure SQL Database Hyperscale.

Each row in the result set represents a component that controls (reduces) log generation rate in a Hyperscale database. There are multiple [components in Hyperscale tier architecture](/azure/azure-sql/database/hyperscale-architecture) that could reduce log generation rate to keep database performance stable and balanced.

Certain types of components, such as secondary compute replicas or page servers, can temporarily reduce log generation rate on the primary compute replica to ensure the overall database health and stability.

If log generation rate is not reduced by any component, a row is returned for the primary compute replica component, showing the maximum allowed log generation rate for the database.

This dynamic management function returns rows only when the session is connected to the primary replica.

## Syntax

```syntaxsql
sys.dm_hs_database_log_rate ( 
{ database_id | NULL }                                     
)                             
```

## Arguments

#### database_id

*database_id* is an **integer** representing the database ID, with no default value. Valid inputs are either a database ID or `NULL`.

When a `database_id` is provided, `sys.dm_hs_database_log_rate` returns a row for that specific database.

When not provided or when `NULL` is provided, for a single database, returns a row for the current database. For a database in an elastic pool, returns rows for all databases in the pool where the caller has sufficient [permissions](#permissions).

The built-in function [DB_ID](../../t-sql/functions/db-id-transact-sql.md) can be specified.

## Table Returned


| Column name | Data type | Description |
|:--|:--|:--|
| `database_id` |  **int** NOT NULL | Identifier of the database. The values are unique within a single database or an elastic pool, but not within a logical server. |
| `replica_id` |   **uniqueidentifier** NOT NULL  |  Identifier of a compute replica which corresponds to the `replica_id` column in `sys.dm_database_replica_states`. `NULL` when `component_id` corresponds to a Hyperscale page server.</br></br>This value is returned by the `DATABASEPROPERTYEX(DB_NAME(), 'ReplicaID')` function call when connected to the replica. |
| `file_id` |  **int**  NULL | ID of database file within the database that corresponds to the page server limiting the log generation rate. Will be populated only if the role is page server, otherwise returns `NULL`. This value corresponds to the `file_id` column in `sys.database_files`. |
| `current_max_log_rate` | **bigint** NOT NULL  | The current max log rate limit for log generation rate on the primary compute replica, in bytes/sec. If no component is reducing log generation rate, reports the log generation rate limit for a Hyperscale database. |
| `component_id` | **uniqueidentifier** NOT NULL | A unique identifier of a Hyperscale component such as a page server or a compute replica. |
| `role` |**smallint** NOT NULL | All existing component roles that can reduce log generation.<br /><br />`Unknown` = 0<br />`Storage` = 1<br />`Primary` = 2<br />`Replica` = 4<br />`LocalDestage` = 5<br />`Destage` = 6 <br />`GeoReplica` = 10<br />`StorageCheckpoint` = 12<br />`MigrationTarget` = 14<br />When log generation rate is limited, the following wait types corresponding to each role might be observed on the primary compute replica:<br />1 - RBIO_RG_STORAGE <br />4 - RBIO_RG_REPLICA <br />5 - RBIO_RG_LOCALDESTAGE  <br />6 - RBIO_RG_DESTAGE <br />10 - RBIO_RG_GEOREPLICA <br />12 - RBIO_RG_STORAGE_CHECKPOINT <br />14 - RBIO_RG_MIGRATION_TARGET <br />For more information, see [Log rate throttling waits](/azure/azure-sql/database/hyperscale-performance-diagnostics#log-rate-throttling-waits)|
| `role_desc` | **nvarchar(60)** NOT NULL | `Unknown` = The component role is not known<br />`Storage` = Page server(s)<br />`Primary` = Primary compute replica<br />`Replica` = Secondary compute replica such as a high availability (HA) replica or a named replica.<br />`LocalDestage` = Log service<br />`Destage` = Long term log storage<br />`GeoReplica` = Geo-secondary replica<br />`StorageCheckpoint` = A checkpoint on a page server<br />`MigrationTarget` = The target database during reverse migration from Hyperscale to a non-Hyperscale database.|
| `catchup_rate` | **bigint** NOT NULL | Catchup Rate in bytes/sec. Returns zero when log rate is not reduced.| 
| `catchup_bytes` | **bigint** NOT NULL | The amount of transaction log, in bytes, that a component must apply to catch up with the primary compute replica. Returns `0` when the component is caught up. |
| `last_reported_time` | **datetime** | The last time the Hyperscale log service reported values. |

## Remarks

The `sys.dm_hs_database_log_rate` dynamic management function applies to Azure SQL Database Hyperscale tier only.

In the Hyperscale service tier of Azure SQL Database, the log service ensures that the distributed components don't get far behind in applying transaction log. This is required to maintain the overall system health and stability. When components are behind and their catch-up rate is less than current log generation rate, the log service reduces the log generation rate on the primary. The `sys.dm_hs_database_log_rate()` DMF can be used to understand which component is causing the reduction in log rate and to what extent, and for how long the reduction of log rate might last.

For more context about log rate reduction, see [Performance diagnostics in Hyperscale](/azure/azure-sql/database/hyperscale-performance-diagnostics?view=azuresql-db&preserve-view=true).

## Permissions

This dynamic management function requires **VIEW DATABASE PERFORMANCE STATE** permission.

## Examples

### A. Return the component causing log rate reduction in a specific database

The following example returns a row for the component causing log rate reduction. If log generation rate is not reduced by any component, a row will be returned for the primary compute, showing the maximum allowed log generation rate for the database.

```sql
SELECT current_max_log_rate_bps, role_desc, catchup_rate_bps, catchup_distance_bytes
FROM sys.dm_hs_database_log_rate(DB_ID(N'Contosodb'));                   
```

### B. Return the components causing log rate reduction

When connected to a database in an elastic pool, the following example returns a row for the component causing log rate reduction, for every database in the pool where you have sufficient permissions. When connected to a single database, returns the row for the database.

```sql
SELECT current_max_log_rate_bps, role_desc, catchup_rate_bps, catchup_distance_bytes 
FROM sys.dm_hs_database_log_rate(NULL);        
```

## Related content

- [sys.dm_database_replica_states (Azure SQL Database) (Transact-SQL)](../system-dynamic-management-views/sys-dm-database-replica-states-azure-sql-database.md?view=azuresqldb-current&preserve-view=true)
- [sys.dm_hs_database_replicas (Transact-SQL)](sys-dm-hs-database-replicas.md?view=azuresqldb-current&preserve-view=true)
- [sys.dm_exec_requests (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md?view=azuresqldb-current&preserve-view=true)
- [sys.dm_os_wait_stats (Transact-SQL)](../system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md?view=azuresqldb-current&preserve-view=true)
- [DATABASEPROPERTYEX (Transact-SQL)](../../t-sql/functions/databasepropertyex-transact-sql.md?view=azuresqldb-current&preserve-view=true)
