---
title: "sys.availability_group_listeners (Transact-SQL)"
description: sys.availability_group_listeners (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "availability_group_listeners_TSQL"
  - "sys.availability_group_listeners"
  - "sys.availability_group_listeners_TSQL"
  - "availability_group_listeners"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "sys.availability_group_listeners catalog view"
  - "Availability Groups [SQL Server], listeners"
dev_langs:
  - "TSQL"
ms.assetid: b5e7d1fb-3ffb-4767-8135-604c575016b1
---
# sys.availability_group_listeners (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  For each Always On availability group, returns either zero rows indicating that no network name is associated with the availability group, or returns a row for each availability-group listener configuration in the Windows Server Failover Clustering (WSFC) cluster. This view displays the real-time configuration gathered from cluster.  
  
> [!NOTE]  
>  This catalog view does not describe details of an IP configuration, that was defined in the WSFC cluster.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**group_id**|**uniqueidentifier**|Availability group ID (**group_id**) from [sys.availability_groups](../../relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md).|  
|**listener_id**|**nvarchar(36)**|GUID from the cluster resource ID.|  
|**dns_name**|**nvarchar(63)**|Configured network name (hostname) of the availability group listener.|  
|**port**|**int**|The TCP port number configured for the availability group listener.<br /><br /> NULL = Listener was configured outside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and its port number has not been added to the availability group. To add the port, pleaseuse the MODIFY LISTENER  option of the [ALTER AVAILABILITY GROUP](../../t-sql/statements/alter-availability-group-transact-sql.md)[!INCLUDE[tsql](../../includes/tsql-md.md)] statement.|  
|**is_conformant**|**bit**|Whether this IP configuration is conformant, one of:<br /><br /> 1 = Listener is conformant. Only "OR" relations exist among its Internet Protocol (IP) addresses. *Conformant* encompasses every an IP configuration that was created by the [CREATE AVAILABILITY GROUP](../../t-sql/statements/create-availability-group-transact-sql.md)[!INCLUDE[tsql](../../includes/tsql-md.md)] statement. In addition, if an IP configuration that was created outside of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], for example by using the WSFC Failover Cluster Manager, but can be modified by the ALTER AVAILABILITY GROUP tsql statement, the IP configuration qualifies as conformant.<br /><br /> 0 = Listener is nonconformant. Typically, this indicates  an IP address that could not be configured by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] commands and, instead, was defined directly in the WSFC cluster.|  
|**ip_configuration_string_from_cluster**|**nvarchar(max)**|Cluster IP configuration strings, if any, for this listener. NULL = Listener has no virtual IP addresses. For example:<br /><br /> IPv4 address:  `65.55.39.10`.<br /><br /> IPv6 address:  `2001::4898:23:1002:20f:1fff:feff:b3a3`|  
|**is_distributed_network_name**|**bit**|**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] CU8 and later, [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU25 and later, [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP3 and later<br /><br /> This column indicates the listener is a distributed network name (DNN) listener if value set to 1. For more information see [Configure a DNN listener for an availability group](/azure/azure-sql/virtual-machines/windows/availability-group-distributed-network-name-dnn-listener-configure) |

## Security  
  
### Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Always On Availability Groups Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/always-on-availability-groups-dynamic-management-views-functions.md)   
 [Always On Availability Groups Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)   
 [Monitor Availability Groups &#40;Transact-SQL&#41;](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)   
 [Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)  
  
  
