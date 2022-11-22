---
title: "sys.availability_read_only_routing_lists (Transact-SQL)"
description: sys.availability_read_only_routing_lists (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "availability_read_only_routing_lists_TSQL"
  - "availability_read_only_routing_lists"
  - "sys.availability_read_only_routing_lists"
  - "sys.availability_read_only_routing_lists_TSQL"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "read-only routing"
  - "Availability Groups [SQL Server], readable secondary replicas"
  - "Availability Groups [SQL Server], read-only routing"
  - "readable secondary replicas"
  - "sys.availability_read_only_routing_lists dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 0686bc5a-c206-41ef-b40a-79a8259d51d2
---
# sys.availability_read_only_routing_lists (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a row for the read only routing list of each availability replica in an Always On availability group in the WSFC failover cluster.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**replica_id**|**uniqueidentifier**|Unique ID of the availability replica that owns the routing list.|  
|**routing_priority**|**int**|Priority order for routing (1 is first, 2 is second, and so forth).|  
|**read_only_replica_id**|**uniqueidentifier**|Unique ID of the availability replica to which a read-only workload will be routed.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Always On Availability Groups Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/always-on-availability-groups-dynamic-management-views-functions.md)   
 [Always On Availability Groups Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)   
 [Monitor Availability Groups &#40;Transact-SQL&#41;](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)   
 [Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)  
  
  
