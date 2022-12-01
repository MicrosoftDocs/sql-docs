---
title: "sys.dm_hadr_availability_group_states (Transact-SQL)"
description: sys.dm_hadr_availability_group_states (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
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
ms.assetid: d18019dd-f8dc-4492-b035-b1a639369b65
---
# sys.dm_hadr_availability_group_states (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a row for each Always On availability group that possesses an availability replica on the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Each row displays the states that define the health of a given availability group.  
  
> [!NOTE]  
>  To obtain the complete list of, query the [sys.availability_groups](../../relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md) catalog view.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**group_id**|**uniqueidentifier**|Unique identifier of the availability group.|  
|**primary_replica**|**varchar(128)**|Name of the server instance that is hosting the current primary replica.<br /><br /> NULL = Not the primary replica and unable to communicate with the WSFC failover cluster.|  
|**primary_recovery_health**|**tinyint**|Indicates the recovery health of the primary replica, one of:<br /><br /> 0 = In progress<br /><br /> 1 = Online<br /><br /> NULL<br /><br /> On secondary replicas the **primary_recovery_health** column is NULL.|  
|**primary_recovery_health_desc**|**nvarchar(60)**|Description of **primary_replica_health**, one of:<br /><br /> ONLINE_IN_PROGRESS<br /><br /> ONLINE<br /><br /> NULL|  
|**secondary_recovery_health**|**tinyint**|Indicates the recovery health of a secondary replica replica,one of:<br /><br /> 0 = In progress<br /><br /> 1 = Online<br /><br /> NULL<br /><br /> On the primary replica, the **secondary_recovery_health** column is NULL.|  
|**secondary_recovery_health_desc**|**nvarchar(60)**|Description of **secondary_recovery_health**, one of:<br /><br /> ONLINE_IN_PROGRESS<br /><br /> ONLINE<br /><br /> NULL|  
|**synchronization_health**|**tinyint**|Reflects a rollup of the **synchronization_health** of all availability replicas in the availability group. Below are the possible values and their descriptions.<br /><br /> 0: Not healthy. None of the availability replicas have a healthy **synchronization_health** (2 = HEALTHY).<br /><br /> 1: Partially healthy. The synchronization health of some, but not all, availability replicas is healthy.<br /><br /> 2: Healthy. The synchronization health of every availability replica is healthy.<br /><br /> For information about replica synchronization health, see the **synchronization_health** column in [sys.dm_hadr_availability_replica_states &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-states-transact-sql.md).|  
|**synchronization_health_desc**|**nvarchar(60)**|Description of **synchronization_health**, one of:<br /><br /> NOT_HEALTHY<br /><br /> PARTIALLY_HEALTHY<br /><br /> HEALTHY|  
  
## Security  
  
### Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Monitor Availability Groups &#40;Transact-SQL&#41;](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)   
 [Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)   
 [Always On Availability Groups Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/always-on-availability-groups-dynamic-management-views-functions.md)  
  
  
