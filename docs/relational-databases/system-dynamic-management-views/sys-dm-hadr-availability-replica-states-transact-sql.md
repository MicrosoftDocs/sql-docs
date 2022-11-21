---
title: "sys.dm_hadr_availability_replica_states (Transact-SQL)"
description: sys.dm_hadr_availability_replica_states (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "10/16/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_hadr_availability_replica_states"
  - "sys.dm_hadr_availability_replica_states_TSQL"
  - "sys.dm_hadr_availability_replica_states"
  - "dm_hadr_availability_replica_states_TSQL"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "sys.dm_hadr_availability_replica_states dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: d2e678bb-51e8-4a61-b223-5c0b8d08b8b1
---
# sys.dm_hadr_availability_replica_states (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a row for each local replica and a row for each remote replica in the same Always On availability group as a local replica. Each row contains information about the state of a given replica.  
  
> [!IMPORTANT]  
>  To obtain information about every replica in a given availability group, query **sys.dm_hadr_availability_replica_states** on the server instance that is hosting the primary replica. When queried on a server instance that is hosting a secondary replica of an availability group, this dynamic management view returns only local information for the availability group.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**replica_id**|**uniqueidentifier**|Unique identifier of the replica.|  
|**group_id**|**uniqueidentifier**|Unique identifier of the availability group.|  
|**is_local**|**bit**|Whether the replica is local, one of:<br /><br /> 0 = Indicates a remote secondary replica in an availability group whose primary replica is hosted by the local server instance. This value occurs only on the primary replica location.<br /><br /> 1 = Indicates a local replica. On secondary replicas, this is the only available value for the availability group to which the replica belongs.|  
|**role**|**tinyint**|Current [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] role of a local replica or a connected remote replica, one of:<br /><br /> 0 = Resolving<br /><br /> 1 = Primary<br /><br /> 2 = Secondary<br /><br /> For information about [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] roles, see [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).|  
|**role_desc**|**nvarchar(60)**|Description of **role**, one of:<br /><br /> RESOLVING<br /><br /> PRIMARY<br /><br /> SECONDARY|  
|**operational_state**|**tinyint**|Current operational state of the replica, one of:<br /><br /> 0 = Pending failover<br /><br /> 1 = Pending<br /><br /> 2 = Online<br /><br /> 3 = Offline<br /><br /> 4 = Failed<br /><br /> 5 = Failed, no quorum<br /><br /> NULL = Replica is not local.<br /><br /> For more information, see [Roles and Operational States](#RolesAndOperationalStates), later in this topic.|  
|**operational\_state\_desc**|**nvarchar(60)**|Description of **operational\_state**, one of:<br /><br /> PENDING_FAILOVER<br /><br /> PENDING<br /><br /> ONLINE<br /><br /> OFFLINE<br /><br /> FAILED<br /><br /> FAILED_NO_QUORUM<br /><br /> NULL|  
|**recovery\_health**|**tinyint**|Rollup of the **database\_state** column of the [sys.dm_hadr_database_replica_states](../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql.md) dynamic management view. The following are the possible values and their descriptions.<br /><br /> 0 : In progress.  At least one joined database has a database state other than ONLINE (**database\_state** is not 0).<br /><br /> 1 : Online. All the joined databases have a database state of ONLINE (**database_state** is 0).<br /><br /> NULL : **is_local** = 0|  
|**recovery_health_desc**|**nvarchar(60)**|Description of **recovery_health**, one of:<br /><br /> ONLINE_IN_PROGRESS<br /><br /> ONLINE<br /><br /> NULL|  
|**synchronization\_health**|**tinyint**|Reflects a rollup of the database synchronization state (**synchronization_state**)of all joined availability databases (also known as *replicas*) and the availability mode of the replica (synchronous-commit or asynchronous-commit mode). The rollup will reflect the least healthy accumulated state the databases on the replica. Below are the possible values and their descriptions.<br /><br /> 0 : Not healthy.   At least one joined database is in the NOT SYNCHRONIZING state.<br /><br /> 1 : Partially healthy. Some replicas are not in the target synchronization state: synchronous-commit replicas should be synchronized, and asynchronous-commit replicas should be synchronizing.<br /><br /> 2 : Healthy. All replicas are in the target synchronization state: synchronous-commit replicas are synchronized, and asynchronous-commit replicas are synchronizing.|  
|**synchronization_health_desc**|**nvarchar(60)**|Description of **synchronization_health**, one of:<br /><br /> NOT_HEALTHY<br /><br /> PARTIALLY_HEALTHY<br /><br /> HEALTHY|  
|**connected_state**|**tinyint**|Whether a secondary replica is currently connected to the primary replica. The possible values are shown below with their descriptions.<br /><br /> 0 : Disconnected. The response of an availability replica to the DISCONNECTED state depends on its role: On the primary replica, if a secondary replica is disconnected, its secondary databases are marked as NOT SYNCHRONIZED on the primary replica, which waits for the secondary to reconnect; On a secondary replica, upon detecting that it is disconnected, the secondary replica attempts to reconnect to the primary replica.<br /><br /> 1 : Connected.<br /><br /> Each primary replica tracks the connection state for every secondary replica in the same availability group. Secondary replicas track the connection state of only the primary replica.|  
|**connected_state_desc**|**nvarchar(60)**|Description of **connection_state**, one of:<br /><br /> DISCONNECTED<br /><br /> CONNECTED|  
|**last_connect_error_number**|**int**|Number of the last connection error.|  
|**last_connect_error_description**|**nvarchar(1024)**|Text of the **last_connect_error_number** message.|  
|**last_connect_error_timestamp**|**datetime**|Date and time timestamp indicating when the **last_connect_error_number** error occurred.|  
  
##  <a name="RolesAndOperationalStates"></a> Roles and Operational States  
 The role, **role**, reflects the state of a given availability replica and the operational state, **operational_state**, describes whether the replica is ready to process client requests for all the database of the availability replica. The following is a summary of the operational states that are possible for each role: RESOLVING, PRIMARY, and SECONDARY.  
  
 **RESOLVING:** When an availability replica is in the RESOLVING role, the possible operational states are as shown in the following table.  
  
|Operational State|Description|  
|-----------------------|-----------------|  
|PENDING_FAILOVER|A failover command is being processed for the availability group.|  
|OFFLINE|All configuration data for the availability replica has been updated on WSFC cluster and, also, in local metadata, but the availability group currently lacks a primary replica.|  
|FAILED|A read failure has occurred during an attempt trying to retrieve information from the WSFC cluster.|  
|FAILED_NO_QUORUM|The local WSFC node does not have quorum. This is an inferred state.|  
  
 **PRIMARY:** When an availability replica is performing the PRIMARY role, it is currently the primary replica. The possible operational states are as shown in the following table.  
  
|Operational State|Description|  
|-----------------------|-----------------|  
|PENDING|This is a transient state, but a primary replica can be stuck in this state if workers are not available to process requests.|  
|ONLINE|The availability group resource is online, and all database worker threads have been picked up.|  
|FAILED|The availability replica is unable to read to and/or write from the WSFC cluster.|  
  
 **SECONDARY:** When an availability replica is performing the SECONDARY role, it is currently a secondary replica. The possible operational states are as shown in the table below.  
  
|Operational State|Description|  
|-----------------------|-----------------|  
|ONLINE|The local secondary replica  is connected to the primary replica.|  
|FAILED|The local secondary replica is unable to read to and/or write from the WSFC cluster.|  
|NULL|On a primary replica, this value is returned when the row relates to a secondary replica.|  
  
## Security  
  
### Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Monitor Availability Groups &#40;Transact-SQL&#41;](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)  
  
  
