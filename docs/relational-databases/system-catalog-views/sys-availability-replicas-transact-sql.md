---
title: "sys.availability_replicas (Transact-SQL)"
description: sys.availability_replicas (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/17/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "availability_replicas_TSQL"
  - "availability_replicas"
  - "sys.availability_replicas"
  - "sys.availability_replicas_TSQL"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "sys.availability_replicas catalog view"
dev_langs:
  - "TSQL"
---
# sys.availability_replicas (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a row for each of the availability replicas that belong to any Always On availability group in the WSFC failover cluster.  
  
If the local server instance is unable to talk to the WSFC failover cluster, for example because the cluster is down or quorum has been lost, only rows for local availability replicas are returned. These rows will contain only the columns of data that are cached locally in metadata.  
 
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**replica_id**|**uniqueidentifier**|Unique ID of the replica.|  
|**group_id**|**uniqueidentifier**|Unique ID of the availability group to which the replica belongs.|  
|**replica_metadata_id**|**int**|ID for the local metadata object for availability replicas in the Database Engine.|  
|**replica_server_name**|**nvarchar(256)**|Server name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is hosting this replica and, for a non-default instance, its instance name.|  
|**owner_sid**|**varbinary(85)**|Security identifier (SID) registered to this server instance for the external owner of this availability replica.<br /><br />NULL for non-local availability replicas.|  
|**endpoint_url**|**nvarchar(128)**|String representation of the user-specified database mirroring endpoint that is used by connections between primary and secondary replicas for data synchronization. For information about the syntax of endpoint URLs, see [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/specify-endpoint-url-adding-or-modifying-availability-replica.md).<br /><br />NULL = Unable to talk to the WSFC failover cluster.<br /><br />To change this endpoint, use the ENDPOINT_URL option of [ALTER AVAILABILITY GROUP](../../t-sql/statements/alter-availability-group-transact-sql.md)[!INCLUDE[tsql](../../includes/tsql-md.md)] statement.|  
|**availability_mode**|**tinyint**|The availability mode of the replica, one of:<br /><br />0 &#124; Asynchronous commit. The primary replica can commit transactions without waiting for the secondary to write the log to disk.<br /><br />1 &#124; Synchronous commit. The primary replica waits to commit a given transaction until the secondary replica has written the transaction to disk.<br /><br />4 &#124; Configuration only. The primary replica sends availability group configuration metadata to the replica synchronously. User data is not transmitted to the replica. Available in SQL Server 2017 CU1 and later.<br /><br />For more information, see [Availability Modes &#40;Always On Availability Groups&#41;](../../database-engine/availability-groups/windows/availability-modes-always-on-availability-groups.md).|  
|**availability_mode_desc**|**nvarchar(60)**|Description of **availability_mode**, one of:<br /><br />ASYNCHRONOUS_COMMIT<br /><br />SYNCHRONOUS_COMMIT<br /><br />CONFIGURATION_ONLY<br /><br />To change this the availability mode of an availability replica, use the AVAILABILITY_MODE option of [ALTER AVAILABILITY GROUP](../../t-sql/statements/alter-availability-group-transact-sql.md)[!INCLUDE[tsql](../../includes/tsql-md.md)] statement.<br/><br>You cannot change the availability mode of a replica to CONFIGURATION_ONLY. You cannot change a CONFIGURATION_ONLY replica to a secondary or primary replica. |  
|**failover_mode**|**tinyint**|The [failover mode](../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md) of the availability replica, one of:<br /><br />0 &#124; Automatic failover. The replica is a potential target for automatic failovers.  Automatic failover is supported only if the availability mode is set to synchronous commit (**availability_mode** = 1) and the availability replica is currently synchronized.<br /><br />1 &#124; Manual failover. A failover to a secondary replica set to manual failover must be manually initiated by the database administrator. The type of failover that is performed will depend on whether the secondary replica is synchronized, as follows:<br /><br />If the availability replica is not synchronizing or is still synchronizing, only forced failover (with possible data loss) can occur.<br /><br />If the availability mode is set to synchronous commit (**availability_mode** = 1) and the availability replica is currently synchronized, manual failover without data loss can occur.<br /><br />To view a rollup of the database synchronization health of every availability database in an availability replica, use the **synchronization_health** and **synchronization_health_desc** columns of  the [sys.dm_hadr_availability_replica_states](../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-states-transact-sql.md) dynamic management view. The rollup considers the synchronization state of every availability database and the availability mode of its availability replica.<br /><br />**Note:** To view the synchronization health of a given availability database, query the **synchronization_state** and **synchronization_health** columns of the [sys.dm_hadr_database_replica_states](../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql.md) dynamic management view.|  
|**failover_mode_desc**|**nvarchar(60)**|Description of **failover_mode**, one of:<br /><br />MANUAL<br /><br />AUTOMATIC<br /><br />To change the failover mode, use the FAILOVER_MODE option of [ALTER AVAILABILITY GROUP](../../t-sql/statements/alter-availability-group-transact-sql.md)[!INCLUDE[tsql](../../includes/tsql-md.md)] statement.|  
|**session_timeout**|**int**|The time-out period, in seconds. The time-out period is the maximum time that the replica waits to receive a message  from another replica before considering connection between the primary and secondary replica have failed. Session timeout detects whether secondaries are connected the primary replica.<br /><br />On detecting a failed connection with a secondary replica, the primary replica  considers the secondary replica to be NOT_SYNCHRONIZED. On detecting a failed connection with the primary replica, a secondary replica simply attempts to reconnect.<br /><br />**Note:** Session timeouts do not cause automatic failovers.<br /><br />To change this value, use the SESSION_TIMEOUT option of [ALTER AVAILABILITY GROUP](../../t-sql/statements/alter-availability-group-transact-sql.md)[!INCLUDE[tsql](../../includes/tsql-md.md)] statement.|  
|**primary_role_allow_connections**|**tinyint**|Whether the availability allows all connections or only read-write connections, one of:<br /><br />2 = All (default)<br /><br />3 = Read write|  
|**primary_role_allow_connections_desc**|**nvarchar(60)**|Description of **primary_role_allow_connections**, one of:<br /><br />ALL<br /><br />READ_WRITE|  
|**secondary_role_allow_connections**|**tinyint**|Whether an availability replica that is performing the secondary role (that is, a secondary replica) can accept connections from clients, one of:<br /><br />0 = No. No connections are allowed to the databases in the secondary replica, and the databases are not available for read access. This is the default setting.<br /><br />1 = Read only. Only read-only connections are allowed to the databases in the secondary replica. All database(s) in the replica are available for read access.<br /><br />2 = All. All connections are allowed to the databases in the secondary replica for read-only access.<br /><br />For more information, see [Active Secondaries: Readable Secondary Replicas &#40;Always On Availability Groups&#41;](../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md).|  
|**secondary_role_allow_connections_desc**|**nvarchar(60)**|Description of **secondary_role_allow_connections**, one of:<br /><br />NO<br /><br />READ_ONLY<br /><br />ALL|  
|**create_date**|**datetime**|Date that the replica was created.<br /><br />NULL = Replica not on this server instance.|  
|**modify_date**|**datetime**|Date that the replica was last modified.<br /><br />NULL = Replica not on this server instance.|  
|**backup_priority**|**int**|Represents the user-specified priority for performing backups on this replica relative to the other replicas in the same availability group. The value is an integer in the range of 0..100.<br /><br />For more information, see [Active Secondaries: Backup on Secondary Replicas &#40;Always On Availability Groups&#41;](../../database-engine/availability-groups/windows/active-secondaries-backup-on-secondary-replicas-always-on-availability-groups.md).|  
|**read_only_routing_url**|**nvarchar(256)**|Connectivity endpoint (URL) of the read only availability replica. For more information, see [Configure Read-Only Routing for an Availability Group &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md).|  
|**read_write_routing_url**|**nvarchar(256)**|[!INCLUDE[ssSQL15-md](../../includes/sssql19-md.md)] and later. Connectivity endpoint (URL) for when the replica is the primary. For more information, see [Secondary to primary replica read/write connection redirection &#40;Always On Availability Groups&#41;](../../database-engine/availability-groups/windows/secondary-replica-connection-redirection-always-on-availability-groups.md).|
|**seeding_mode**|**tinyint**|One of:<br /><br />0: Automatic<br /><br />1: Manual|
|**seeding_mode_desc**|**nvarchar(60)**|Describes seeding mode.<br /><br />AUTOMATIC<br /><br />MANUAL|

## Security
  
### Permissions

Requires VIEW ANY DEFINITION permission on the server instance.  
  
## See Also

- [sys.availability_groups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md)
- [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
- [Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)
- [Monitor Availability Groups &#40;Transact-SQL&#41;](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)
