---
title: "sys.availability_group_listener_ip_addresses (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "availability_group_listener_ip_addresses"
  - "sys.availability_group_listener_ip_addresses"
  - "availability_group_listener_ip_addresses_TSQL"
  - "sys.availability_group_listener_ip_addresses_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "Availability Groups [SQL Server], listeners"
  - "sys.availability_group_listener_ip_addresses catalog view"
ms.assetid: e515fa6b-1354-4110-9b70-ab2e6164c992
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# sys.availability_group_listener_ip_addresses (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns a row for every IP address that is associated with any Always On availability group listener in the Windows Server Failover Clustering (WSFC) cluster.  
  
 Primary key:  **listener_id** + **ip_address** + **ip_sub_mask**  
  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**listener_id**|**nvarchar(36)**|Resource GUID from Windows Server Failover Clustering (WSFC) cluster.|  
|**ip_address**|**nvarchar(48)**|Configured virtual IP address of the availability group listener. Returns a single IPv4 or IPv6 address.|  
|**ip_subnet_mask**|**nvarchar(15)**|Configured IP subnet mask for the IPv4 address, if any, that is configured for the availability group listener.<br /><br /> NULL = IPv6 subnet|  
|**is_dhcp**|**bit**|Whether the IP address is configured by DHCP, one of:<br /><br /> 0 = IP address is not configured by DHCP.<br /><br /> 1 = IP address is configured by DHCP|  
|**network_subnet_ip**|**nvarchar(48)**|Network subnet IP address that specifies the subnet to which the IP address belongs.|  
|**network_subnet_prefix_length**|**int**|Network subnet prefix length of the subnet to which the IP address belongs.|  
|**network_subnet_ipv4_mask**|**nvarchar(45)**|Network subnet mask of the subnet to which the IP address belongs. **network_subnet_ipv4_mask** to specify the DHCP <network_subnet_option> options in a WITH DHCP clause of the [CREATE AVAILABILITY GROUP](../../t-sql/statements/create-availability-group-transact-sql.md) or [ALTER AVAILABILITY GROUP](../../t-sql/statements/alter-availability-group-transact-sql.md)[!INCLUDE[tsql](../../includes/tsql-md.md)] statement.<br /><br /> NULL = IPv6 subnet|  
|**state**|**tinyint**|IP resource ONLINE/OFFLINE state from the WSFC cluster, one of:<br /><br /> 1 = Online. IP resource is online.<br /><br /> 0 = Offline. IP resource is offline.<br /><br /> 2 = Online Pending. IP resource is offline but is being brought online.<br /><br /> 3 = Failed. IP resource was being brought online but failed.|  
|**state_desc**|**nvarchar(60)**|Description of **state**, one of:<br /><br /> ONLINE<br /><br /> OFFLINE<br /><br /> ONLINE_PENDING<br /><br /> FAILED|  
  
## Security  
  
### Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
