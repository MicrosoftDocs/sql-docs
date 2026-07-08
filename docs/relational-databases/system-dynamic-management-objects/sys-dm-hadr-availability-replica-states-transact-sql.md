---
title: "sys.dm_hadr_availability_replica_states (Transact-SQL)"
description: Returns a row for each local replica and a row for each remote replica in the same Always On availability group as a local replica.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/16/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.dm_hadr_availability_replica_states_TSQL"
  - "sys.dm_hadr_availability_replica_states"
  - "dm_hadr_availability_replica_states_TSQL"
  - "dm_hadr_availability_replica_states"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "sys.dm_hadr_availability_replica_states dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.dm_hadr_availability_replica_states (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a row for each local replica and a row for each remote replica in the same Always On availability group as a local replica. Each row contains information about the state of a given replica.

> [!IMPORTANT]  
> To get information about every replica in a given availability group, query `sys.dm_hadr_availability_replica_states` on the server instance that hosts the primary replica. When you query this dynamic management view on a server instance that hosts a secondary replica of an availability group, it returns only local information for the availability group.

| Column name | Data type | Nullable | Description |
| --- | --- | --- | --- |
| `replica_id` | **uniqueidentifier** | No | Unique identifier of the replica. |
| `group_id` | **uniqueidentifier** | No | Unique identifier of the availability group. |
| `is_local` | **bit** | No | Whether the replica is local, one of:<br /><br />`0` = Indicates a remote secondary replica in an availability group whose primary replica is hosted by the local server instance. This value occurs only on the primary replica location.<br /><br />`1` = Indicates a local replica. On secondary replicas, this is the only available value for the availability group to which the replica belongs. |
| `role` | **tinyint** | Yes | Current [!INCLUDE [ssHADR](../../includes/sshadr-md.md)] role of a local replica or a connected remote replica, one of:<br /><br />`0` = Resolving<br />`1` = Primary<br />`2` = Secondary<br /><br />For information about [!INCLUDE [ssHADR](../../includes/sshadr-md.md)] roles, see [What is an Always On availability group?](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) |
| `role_desc` | **nvarchar(60)** | Yes | Description of `role`, one of:<br /><br />`RESOLVING`<br />`PRIMARY`<br />`SECONDARY` |
| `operational_state` | **tinyint** | Yes | Current operational state of the replica, one of:<br /><br />`0` = Pending failover<br />`1` = Pending<br />`2` = Online<br />`3` = Offline<br />`4` = Failed<br />`5` = Failed, no quorum<br />`NULL` = Replica isn't local.<br /><br />For more information, see [Roles and Operational States](#RolesAndOperationalStates), later in this article. |
| `operational_state_desc` | **nvarchar(60)** | Yes | Description of `operational_state`, one of:<br /><br />`PENDING_FAILOVER`<br /><br />`PENDING`<br /><br />`ONLINE`<br /><br />`OFFLINE`<br /><br />`FAILED`<br /><br />`FAILED_NO_QUORUM`<br /><br />`NULL` |
| `connected_state` | **tinyint** | Yes | Whether a secondary replica is currently connected to the primary replica. The possible values are shown below with their descriptions.<br /><br />`0` = Disconnected. The response of an availability replica to the `DISCONNECTED` state depends on its role: On the primary replica, if a secondary replica is disconnected, its secondary databases are marked as `NOT SYNCHRONIZED` on the primary replica, which waits for the secondary to reconnect; On a secondary replica, upon detecting that it's disconnected, the secondary replica attempts to reconnect to the primary replica.<br /><br />`1` = Connected.<br /><br />Each primary replica tracks the connection state for every secondary replica in the same availability group. Secondary replicas track the connection state of only the primary replica. |
| `connected_state_desc` | **nvarchar(60)** | Yes | Description of `connection_state`, one of:<br /><br />`DISCONNECTED`<br />`CONNECTED` |
| `recovery_health` | **tinyint** | Yes | Rollup of the `database_state` column of the [sys.dm_hadr_database_replica_states](sys-dm-hadr-database-replica-states-transact-sql.md) dynamic management view. The following are the possible values and their descriptions.<br /><br />`0` = In progress. At least one joined database has a database state other than `ONLINE` (`database_state` isn't `0`).<br /><br />`1` = Online. All the joined databases have a database state of `ONLINE` (`database_state` is `0`).<br /><br />`NULL`: `is_local` = `0` |
| `recovery_health_desc` | **nvarchar(60)** | Yes | Description of `recovery_health`, one of:<br /><br />`ONLINE_IN_PROGRESS`<br />`ONLINE`<br />`NULL` |
| `synchronization_health` | **tinyint** | Yes | Reflects a rollup of the database synchronization state (`synchronization_state`) of all joined availability databases (also known as *replicas*) and the availability mode of the replica (synchronous-commit or asynchronous-commit mode). The rollup will reflect the least healthy accumulated state the databases on the replica. Below are the possible values and their descriptions.<br /><br />`0` = Not healthy. At least one joined database is in the `NOT SYNCHRONIZING` state.<br /><br />`1` = Partially healthy. Some replicas aren't in the target synchronization state: synchronous-commit replicas should be synchronized, and asynchronous-commit replicas should be synchronizing.<br /><br />`2` = Healthy. All replicas are in the target synchronization state: synchronous-commit replicas are synchronized, and asynchronous-commit replicas are synchronizing. |
| `synchronization_health_desc` | **nvarchar(60)** | Yes | Description of `synchronization_health`, one of:<br /><br />`NOT_HEALTHY`<br />`PARTIALLY_HEALTHY`<br />`HEALTHY` |
| `last_connect_error_number` | **int** | Yes | Number of the last connection error. |
| `last_connect_error_description` | **nvarchar(1024)** | Yes | Text of the `last_connect_error_number` message. |
| `last_connect_error_timestamp` | **datetime** | Yes | Date and time timestamp indicating when the `last_connect_error_number` error occurred. |
| `write_lease_remaining_ticks` | **bigint** | Yes | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `current_configuration_commit_start_time_utc` | **datetime** | Yes | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `is_internal` | **bit** | Yes | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `operational_state_desc` | **nvarchar(60)** | Yes | Description of `operational_state`, one of:<br /><br />`PENDING_FAILOVER`<br />`PENDING`<br />`ONLINE`<br />`OFFLINE`<br />`FAILED`<br />`FAILED_NO_QUORUM`<br />`NULL` |
| `recovery_health` | **tinyint** | Yes | Rollup of the `database_state` column of the [sys.dm_hadr_database_replica_states](sys-dm-hadr-database-replica-states-transact-sql.md) dynamic management view. The following are the possible values and their descriptions.<br /><br />`0` = In progress. At least one joined database has a database state other than `ONLINE` (`database_state` isn't `0`).<br /><br />`1` = Online. All the joined databases have a database state of `ONLINE` (`database_state` is `0`).<br /><br />`NULL`: `is_local` isn't `0` |
| `synchronization_health` | **tinyint** | No | Reflects a rollup of the database synchronization state (`synchronization_state`) of all joined availability databases (also known as *replicas*) and the availability mode of the replica (synchronous-commit or asynchronous-commit mode). The rollup reflects the least healthy accumulated state the databases on the replica. The possible values and their descriptions are:<br /><br />`0` = Not healthy. At least one joined database is in the `NOT SYNCHRONIZING` state.<br /><br />`1` = Partially healthy. Some replicas aren't in the target synchronization state: synchronous-commit replicas should be synchronized, and asynchronous-commit replicas should be synchronizing.<br /><br />`2` = Healthy. All replicas are in the target synchronization state: synchronous-commit replicas are synchronized, and asynchronous-commit replicas are synchronizing. |

<a id="RolesAndOperationalStates"></a>

## Roles and operational states

The role, `role`, reflects the state of a given availability replica. The operational state, `operational_state`, describes whether the replica is ready to process client requests for all the databases of the availability replica. The following table summarizes the operational states that are possible for each role: `RESOLVING`, `PRIMARY`, and `SECONDARY`.

`RESOLVING`: When an availability replica is in the `RESOLVING` role, the possible operational states are as shown in the following table.

| Operational state | Description |
| --- | --- |
| `PENDING_FAILOVER` | The system is processing a failover command for the availability group. |
| `OFFLINE` | All configuration data for the availability replica is updated on WSFC cluster and, also, in local metadata, but the availability group currently lacks a primary replica. |
| `FAILED` | A read failure occurred during an attempt to retrieve information from the WSFC cluster. |
| `FAILED_NO_QUORUM` | The local WSFC node doesn't have quorum. This state is inferred. |

`PRIMARY`: When an availability replica performs the `PRIMARY` role, it's currently the primary replica. The possible operational states are as shown in the following table.

| Operational state | Description |
| --- | --- |
| `PENDING` | This state is transient, but a primary replica can be stuck in this state if workers aren't available to process requests. |
| `ONLINE` | The availability group resource is online, and all database worker threads have been picked up. |
| `FAILED` | The availability replica can't read to or write from the WSFC cluster. |

`SECONDARY`: When an availability replica performs the `SECONDARY` role, it's currently a secondary replica. The possible operational states are as shown in the table below.

| Operational state | Description |
| --- | --- |
| `ONLINE` | The local secondary replica is connected to the primary replica. |
| `FAILED` | The local secondary replica can't read to or write from the WSFC cluster. |
| `NULL` | On a primary replica, this value is returned when the row relates to a secondary replica. |

## Permissions

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions require `VIEW SERVER STATE` permission on the server.

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions require `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Related content

- [What is an Always On availability group?](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
- [Monitor Availability Groups (Transact-SQL)](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)
