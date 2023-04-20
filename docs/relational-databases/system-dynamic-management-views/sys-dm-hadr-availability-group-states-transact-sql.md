---
title: "sys.dm_hadr_availability_group_states (Transact-SQL)"
description: sys.dm_hadr_availability_group_states returns a row for each AG that possesses an availability replica on the local instance of SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/17/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_hadr_availability_group_states"
  - "sys.dm_hadr_availability_group_states_TSQL"
  - "dm_hadr_availability_group_states_TSQL"
  - "dm_hadr_availability_group_states"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "sys.dm_hadr_availability_group_states dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.dm_hadr_availability_group_states (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a row for each Always On availability group that possesses an availability replica on the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Each row displays the states that define the health of a given availability group.

> [!NOTE]  
> To obtain the complete list of availability groups, query the [sys.availability_groups](../system-catalog-views/sys-availability-groups-transact-sql.md) catalog view.

| Column name | Data type | Description |
| --- | --- | --- |
| **group_id** | **uniqueidentifier** | Unique identifier of the availability group. |
| **primary_replica** | **varchar(128)** | Name of the server instance that is hosting the current primary replica.<br /><br />NULL = Not the primary replica and unable to communicate with the WSFC failover cluster. |
| **primary_recovery_health** | **tinyint** | Indicates the recovery health of the primary replica, one of:<br /><br />0 = In progress<br /><br />1 = Online<br /><br />NULL<br /><br />On secondary replicas, the **primary_recovery_health** column is NULL. |
| **primary_recovery_health_desc** | **nvarchar(60)** | Description of **primary_replica_health**, one of:<br /><br />ONLINE_IN_PROGRESS<br /><br />ONLINE<br /><br />NULL |
| **secondary_recovery_health** | **tinyint** | Indicates the recovery health of a secondary replica, one of:<br /><br />0 = In progress<br /><br />1 = Online<br /><br />NULL<br /><br />On the primary replica, the **secondary_recovery_health** column is NULL. |
| **secondary_recovery_health_desc** | **nvarchar(60)** | Description of **secondary_recovery_health**, one of:<br /><br />ONLINE_IN_PROGRESS<br /><br />ONLINE<br /><br />NULL |
| **synchronization_health** | **tinyint** | Reflects a rollup of the **synchronization_health** of all availability replicas in the availability group. The possible values and their descriptions are as follows:<br /><br />0: Not healthy. None of the availability replicas have a healthy **synchronization_health** (2 = HEALTHY).<br /><br />1: Partially healthy. The synchronization health of some, but not all, availability replicas is healthy.<br /><br />2: Healthy. The synchronization health of every availability replica is healthy.<br /><br />For information about replica synchronization health, see the **synchronization_health** column in [sys.dm_hadr_availability_replica_states](../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-states-transact-sql.md). |
| **synchronization_health_desc** | **nvarchar(60)** | Description of **synchronization_health**, one of:<br /><br />NOT_HEALTHY<br /><br />PARTIALLY_HEALTHY<br /><br />HEALTHY |

## Permissions

For [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions, requires VIEW SERVER STATE permission on the server.

For [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, requires VIEW SERVER PERFORMANCE STATE permission on the server.

## See also

- [Monitor Availability Groups (Transact-SQL)](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)
- [Always On Availability Groups (SQL Server)](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)
- [Always On Availability Groups Dynamic Management Views and Functions (Transact-SQL)](../../relational-databases/system-dynamic-management-views/always-on-availability-groups-dynamic-management-views-functions.md)
