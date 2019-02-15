---
title: "sys.availability_group_listeners (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "availability_group_listeners_TSQL"
  - "sys.availability_group_listeners"
  - "sys.availability_group_listeners_TSQL"
  - "availability_group_listeners"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "sys.availability_group_listeners catalog view"
  - "Availability Groups [SQL Server], listeners"
ms.assetid: b5e7d1fb-3ffb-4767-8135-604c575016b1
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# sys.availability_group_listeners (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

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
  
## Security  
  
### Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Always On Availability Groups Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/always-on-availability-groups-dynamic-management-views-functions.md)   
 [Always On Availability Groups Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)   
 [Monitor Availability Groups &#40;Transact-SQL&#41;](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)   
 [Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)  
  
  
