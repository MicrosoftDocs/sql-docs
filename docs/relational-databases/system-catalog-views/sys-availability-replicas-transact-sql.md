---
title: "sys.availability_replicas (Transact-SQL)"
description: Returns a row for each of the availability replicas that belong to any Always On availability group in the WSFC failover cluster.
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/12/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.availability_replicas_TSQL"
  - "sys.availability_replicas"
  - "availability_replicas_TSQL"
  - "availability_replicas"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "sys.availability_replicas catalog view"
dev_langs:
  - "TSQL"
---
# sys.availability_replicas (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a row for each of the availability replicas that belong to any Always On availability groups in the Windows Server Failover Cluster (WSFC).

If the local server instance can't connect to the WSFC failover cluster, for example because the cluster is down or quorum is lost, `sys.availability_replicas` returns only rows for local availability replicas. These rows contain only the columns of data that are cached locally in metadata.

| Column name | Data type | Description |
| --- | --- | --- |
| `replica_id` | **uniqueidentifier** | Unique ID of the replica. |
| `group_id` | **uniqueidentifier** | Unique ID of the availability group to which the replica belongs. |
| `replica_metadata_id` | **int** | ID for the local metadata object for availability replicas in the Database Engine. |
| `replica_server_name` | **nvarchar(256)** | Server name of the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that's hosting this replica and, for a non-default instance, its instance name. |
| `owner_sid` | **varbinary(85)** | Security identifier (SID) registered to this server instance for the external owner of this availability replica.<br /><br />`NULL` for non-local availability replicas. |
| `endpoint_url` | **nvarchar(256)** | String representation of the user-specified database mirroring endpoint that is used by connections between primary and secondary replicas for data synchronization. For information about the syntax of endpoint URLs, see [Specify Endpoint URL - Adding or Modifying Availability Replica](../../database-engine/availability-groups/windows/specify-endpoint-url-adding-or-modifying-availability-replica.md).<br /><br />`NULL` = Unable to talk to the WSFC failover cluster.<br /><br />To change this endpoint, use the `ENDPOINT_URL` option of [ALTER AVAILABILITY GROUP](../../t-sql/statements/alter-availability-group-transact-sql.md) [!INCLUDE [tsql](../../includes/tsql-md.md)] statement. |
| `availability_mode` | **tinyint** | The availability mode of the replica, one of:<br /><br />`0` = Asynchronous commit. The primary replica can commit transactions without waiting for the secondary to write the log to disk.<br /><br />`1` = Synchronous commit. The primary replica waits to commit a given transaction until the secondary replica has written the transaction to disk.<br /><br />`4` = Configuration only. The primary replica sends availability group configuration metadata to the replica synchronously. User data isn't transmitted to the replica.<br /><br />For more information, see [Differences between availability modes for an Always On availability group](../../database-engine/availability-groups/windows/availability-modes-always-on-availability-groups.md).<br /><br />Applies to [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU 1 and later versions. |
| `availability_mode_desc` | **nvarchar(60)** | Description of `availability_mode`, one of:<br /><br />`ASYNCHRONOUS_COMMIT`<br />`SYNCHRONOUS_COMMIT`<br />`CONFIGURATION_ONLY`<br /><br />To change the availability mode of an availability replica, use the `AVAILABILITY_MODE` option of the [ALTER AVAILABILITY GROUP](../../t-sql/statements/alter-availability-group-transact-sql.md) [!INCLUDE [tsql](../../includes/tsql-md.md)] statement.<br /><br />You can't change the availability mode of a replica to `CONFIGURATION_ONLY`. You can't change a `CONFIGURATION_ONLY` replica to a secondary or primary replica. |
| `failover_mode` | **tinyint** | The [failover mode](../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md) of the availability replica, one of:<br /><br />`0` = Automatic failover. The replica is a potential target for automatic failovers. Automatic failover is supported only if the availability mode is set to synchronous commit (`availability_mode` is `1`) and the availability replica is currently synchronized.<br /><br />`1` = Manual failover. When a secondary replica is configured for manual failover, the database administrator must manually initiate the failover. The type of failover that is performed depends on whether the secondary replica is synchronized, as follows:<br /><br />If the availability replica isn't synchronizing or is still synchronizing, only forced failover (with possible data loss) can occur.<br /><br />If the availability mode is set to synchronous commit (`availability_mode` is `1`) and the availability replica is currently synchronized, manual failover without data loss can occur.<br /><br />To view a rollup of the database synchronization health of every availability database in an availability replica, use the `synchronization_health` and `synchronization_health_desc` columns of the [sys.dm_hadr_availability_replica_states](../system-dynamic-management-objects/sys-dm-hadr-availability-replica-states-transact-sql.md) dynamic management view. The rollup considers the synchronization state of every availability database and the availability mode of its availability replica.<br /><br />**Note:** To view the synchronization health of a given availability database, query the `synchronization_state` and `synchronization_health` columns of the [sys.dm_hadr_database_replica_states](../system-dynamic-management-objects/sys-dm-hadr-database-replica-states-transact-sql.md) dynamic management view. |
| `failover_mode_desc` | **nvarchar(60)** | Description of `failover_mode`, one of:<br /><br />`MANUAL`<br />`AUTOMATIC`<br /><br />To change the failover mode, use the `FAILOVER_MODE` option of [ALTER AVAILABILITY GROUP](../../t-sql/statements/alter-availability-group-transact-sql.md) [!INCLUDE [tsql](../../includes/tsql-md.md)] statement. |
| `session_timeout` | **int** | The timeout period, in seconds. The timeout period is the maximum time that the replica waits to receive a message from another replica, before considering that the connection between the primary and secondary replica has failed. Session timeout detects whether secondaries are connected to the primary replica.<br /><br />On detecting a failed connection with a secondary replica, the primary replica considers the secondary replica to be `NOT_SYNCHRONIZED`. On detecting a failed connection with the primary replica, a secondary replica simply attempts to reconnect.<br /><br />**Note:** Session timeouts don't cause automatic failovers.<br /><br />To change this value, use the `SESSION_TIMEOUT` option of [ALTER AVAILABILITY GROUP](../../t-sql/statements/alter-availability-group-transact-sql.md) [!INCLUDE [tsql](../../includes/tsql-md.md)] statement. |
| `primary_role_allow_connections` | **tinyint** | Whether the availability allows all connections or only read-write connections, one of:<br /><br />`2` = All (default)<br />`3` = Read write |
| `primary_role_allow_connections_desc` | **nvarchar(60)** | Description of `primary_role_allow_connections`, one of:<br /><br />`ALL`<br />`READ_WRITE` |
| `secondary_role_allow_connections` | **tinyint** | Whether an availability replica that's performing the secondary role (that is, a secondary replica) can accept connections from clients, one of:<br /><br />`0` = No. No connections are allowed to the databases in the secondary replica, and the databases aren't available for read access. This is the default setting.<br /><br />`1` = Read only. Only read-only connections are allowed to the databases in the secondary replica. All databases in the replica are available for read access.<br /><br />`2` = All. All connections are allowed to the databases in the secondary replica for read-only access.<br /><br />For more information, see [Offload read-only workload to secondary replica of an Always On availability group](../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md). |
| `secondary_role_allow_connections_desc` | **nvarchar(60)** | Description of `secondary_role_allow_connections`, one of:<br /><br />`NO`<br />`READ_ONLY`<br />`ALL` |
| `create_date` | **datetime** | Date that the replica was created.<br /><br />`NULL` = Replica not on this server instance. |
| `modify_date` | **datetime** | Date that the replica was last modified.<br /><br />`NULL` = Replica not on this server instance. |
| `backup_priority` | **int** | Represents the user-specified priority for performing backups on this replica relative to the other replicas in the same availability group. The value is an integer in the range of 0..100.<br /><br />For more information, see [Offload supported backups to secondary replicas of an availability group](../../database-engine/availability-groups/windows/active-secondaries-backup-on-secondary-replicas-always-on-availability-groups.md). |
| `read_only_routing_url` | **nvarchar(256)** | Connectivity endpoint (URL) of the read only availability replica. For more information, see [Configure read-only routing for an Always On availability group](../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md). |
| `seeding_mode` | **tinyint** | One of:<br /><br />`0` = Automatic<br />`1` = Manual |
| `seeding_mode_desc` | **nvarchar(60)** | Describes seeding mode.<br /><br />`AUTOMATIC`<br />`MANUAL` |
| `read_write_routing_url` | **nvarchar(256)** | Connectivity endpoint (URL) for when the replica is the primary. For more information, see [Secondary to primary replica read/write connection redirection (Always On Availability Groups)](../../database-engine/availability-groups/windows/secondary-replica-connection-redirection-always-on-availability-groups.md).<br /><br />**Applies to**: [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions. |

## Permissions

Requires `VIEW ANY DEFINITION` permission on the server instance.

## Related content

- [sys.availability_groups (Transact-SQL)](sys-availability-groups-transact-sql.md)
- [What is an Always On availability group?](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
- [Monitor Availability Groups (Transact-SQL)](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)
