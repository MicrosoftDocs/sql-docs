---
title: "sys.availability_databases_cluster (Transact-SQL)"
description: sys.availability_databases_cluster returns a row for each availability database in the WSFC cluster.
author: rwestMSFT
ms.author: randolphwest
ms.date: 02/05/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.availability_databases_cluster_TSQL"
  - "sys.availability_databases_cluster"
  - "availability_databases_cluster_TSQL"
  - "availability_databases_cluster"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "Availability Groups [SQL Server], WSFC clusters"
  - "sys.availability_databases_cluster catalog view"
  - "Availability Groups [SQL Server], databases"
dev_langs:
  - "TSQL"
---
# sys.availability_databases_cluster (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns one row for each availability database on the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that hosts an availability replica for any Always On availability group in the Windows Server Failover Clustering (WSFC) cluster, regardless of whether the local copy database has been joined to the availability group yet.

> [!NOTE]
> When a database is added to an availability group, the primary database is automatically joined to the group. Secondary databases must be prepared on each secondary replica before they can be joined to the availability group.

| Column name | Data type | Description |
| --- | --- | --- |
| `group_id` | **uniqueidentifier** | Unique identifier of the availability group in which the database is participating.<br /><br />NULL = database isn't part of an availability replica in an availability group. |
| `group_database_id` | **uniqueidentifier** | Unique identifier of the database within the availability group, if any, in which the database is participating. `group_database_id` is the same for this database on the primary replica and on every secondary replica on which the database has been joined to the availability group.<br /><br />NULL = database isn't part of an availability replica in any availability group. |
| `database_name` | **sysname** | Name of the database that was added to the availability group. |  
  
## Permissions

If the caller of **sys.availability_databases_cluster** isn't the owner of the database, the minimum permissions required to see the corresponding row are ALTER ANY DATABASE or VIEW ANY DATABASE server-level permission, or CREATE DATABASE permission in the **master** database.

### Permissions for SQL Server 2022 and later

Requires VIEW SERVER PERFORMANCE STATE permission on the server.

## Related content

- [sys.availability_groups (Transact-SQL)](sys-availability-groups-transact-sql.md)
- [sys.databases (Transact-SQL)](sys-databases-transact-sql.md)
- [sys.dm_hadr_database_replica_states (Transact-SQL)](../system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql.md)
- [sys.dm_hadr_database_replica_cluster_states (Transact-SQL)](../system-dynamic-management-views/sys-dm-hadr-database-replica-cluster-states-transact-sql.md)
- [What is an Always On availability group?](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
