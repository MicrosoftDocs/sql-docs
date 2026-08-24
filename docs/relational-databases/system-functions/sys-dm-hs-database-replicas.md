---
title: "sys.dm_hs_database_replicas (Transact-SQL)"
description: The sys.dm_hs_database_replicas dynamic management function returns information about Azure SQL Database Hyperscale secondary replicas.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: adbadram, dfurman
ms.date: 08/11/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_hs_database_replicas"
  - "sys.dm_hs_database_replicas_TSQL"
  - "dm_hs_database_replicas"
  - "dm_hs_database_replicas_TSQL"
helpviewer_keywords:
  - "sys.dm_hs_database_replicas dynamic management function"
  - "sys.dm_hs_database_replicas function"
  - "sys.dm_hs_database_replicas DMF"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# sys.dm_hs_database_replicas (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

The `sys.dm_hs_database_replicas` dynamic management function (DMF) returns information about Azure SQL Database [Hyperscale secondary replicas](/azure/azure-sql/database/service-tier-hyperscale-replicas?view=azuresql-db&preserve-view=true) of a given Hyperscale database.

## Syntax

```syntaxsql
sys.dm_hs_database_replicas ( 
{ database_id | NULL }                                     
)                             
```

## Arguments

#### database_id

*database_id* is an **integer** representing the database ID, with no default value. Valid inputs are either a database ID or `NULL`.

When a `database_id` is provided, `sys.dm_hs_database_replicas` returns a row for that specific database.

When not provided or when `NULL` is provided, for a single database, returns a row for the current database. For a database in an elastic pool, returns rows for all databases in the pool where the caller has sufficient [permissions](#permissions).

The built-in function [DB_ID](../../t-sql/functions/db-id-transact-sql.md) can be specified.
 
## Table returned

| Column name | Data type | Description |
|:--|:--|:--|
| **replica_id**         | **uniqueidentifier** | Identifier of the replica. Is not nullable.       |
| **replica_server_name**| **sysname**          | Name of the logical server where the replica resides. Is not nullable.      |
| **replica_database_name** | **sysname**       | Name of the replica database. Is not nullable.    |
| **is_local**           | **bit**              | Whether the replica database is local: <br> `1` = The replica database is the one you are connected to when querying `sys.dm_hs_database_replicas`. <br> `0` = The replica database isn't the one you are connected to.  |
| **replica_role**       | **tinyint**          | Replica role: <br> `0` = Primary <br> `1` = High availability secondary <br> `2` = Geo-replication forwarder <br> `3` = Named replica. Is not nullable.     |
| **replica_role_desc**  | **nvarchar(256)**   | Primary <br> High availability secondary <br> Geo-replication forwarder <br> Named replica. </ br></ br> Is not nullable.|
| **local_database_id**  | **int**              | Identifier of the database. <br> In Azure SQL Database, values are unique within a single database or elastic pool, but not within the logical server. |
| **parent_replica_id**  | **uniqueidentifier** | Applicable to high availability replicas only. <br> Identifier of the parent replica (for example, the Primary, geo-forwarder, or named replica that has an HA replica). Nullable. |

## Remarks

The `sys.dm_hs_database_replicas` dynamic management function currently applies to Azure SQL Database Hyperscale tier only.

While you can add secondary replicas to a geo-secondary replica of the primary database, `sys.dm_hs_database_replicas` does not return rows for the secondary replicas added under the geo-secondary replica when queried on the primary replica. You should query `sys.dm_hs_database_replicas` when connected to a geo-secondary replica to list the secondary replicas added under it.


## Permissions

This dynamic management function requires the VIEW DATABASE PERFORMANCE STATE permission. For more information, see [System dynamic management views](../system-dynamic-management-views/system-dynamic-management-views.md). To query a pooled database, the VIEW SERVER STATE permission is needed. If the caller has VIEW DATABASE PERFORMANCE STATE permission, rows for the databases where this permission is held are returned.

## Examples

### A. Return primary replica and all the secondary replicas of a database created in the Hyperscale tier

The following example returns a row for each secondary replica added in the Hyperscale database. 

```sql
SELECT replica_role_desc, replica_server_name, replica_id
FROM sys.dm_hs_database_replicas(DB_ID(N'Contosodb'));
```

## Related content

- [sys.dm_database_replica_states (Azure SQL Database)](../system-dynamic-management-views/sys-dm-database-replica-states-azure-sql-database.md?view=azuresqldb-current&preserve-view=true)
- [sys.dm_exec_requests (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md?view=azuresqldb-current&preserve-view=true)
- [sys.dm_os_wait_stats (Transact-SQL)](../system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md?view=azuresqldb-current&preserve-view=true)
- [DATABASEPROPERTYEX (Transact-SQL)](../../t-sql/functions/databasepropertyex-transact-sql.md?view=azuresqldb-current&preserve-view=true)
