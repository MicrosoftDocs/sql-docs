---
title: "sys.dm_tcp_listener_states (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_tcp_listener_states"
  - "dm_tcp_listener_states"
  - "sys.dm_tcp_listener_states_TSQL"
  - "dm_tcp_listener_states_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "Availability Groups [SQL Server], listeners"
  - "sys.dm_tcp_listener_states dynamic management view"
ms.assetid: 9997ffed-a4c1-428f-8bac-3b9e4b16d7cf
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# sys.dm_tcp_listener_states (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns a row containing dynamic-state information for each TCP listener.  
  
> [!NOTE]
> The availability group listener could listen to the same port as the listener of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. In this case, the listeners are listed separately, the same as for a Service Broker listener.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**listener_id**|**int**|Listener's internal ID. Is not nullable.<br /><br /> Primary key.|  
|**ip_address**|**nvarchar48**|The listener IP address that is online and currently being listening to. Either IPv4 and IPv6 is allowed. If a listener possesses both types of addresses, they are listed separately. An IPv4 wildcard, is displayed as "0.0.0.0". An IPv6 wildcard, is displayed as "::".<br /><br /> Is not nullable.|  
|**is_ipv4**|**bit**|Type of IP address<br /><br /> 1 = IPv4<br /><br /> 0 = IPv6|  
|**port**|**int**|The port number on which the listener is listening. Is not nullable.|  
|**type**|**tinyint**|Listener type, one of:<br /><br /> 0 = [!INCLUDE[tsql](../../includes/tsql-md.md)]<br /><br /> 1 = Service Broker<br /><br /> 2 = Database mirroring<br /><br /> Is not nullable.|  
|**type_desc**|**nvarchar(20)**|Description of the **type**, one of:<br /><br /> TSQL<br /><br /> SERVICE_BROKER<br /><br /> DATABASE_MIRRORING<br /><br /> Is not nullable.|  
|**state**|**tinyint**|State of the availability group listener, one of:<br /><br /> 1 = Online. The listener is listening and processing requests.<br /><br /> 2 = Pending restart. the listener is offline, pending a restart.<br /><br /> If the availability group listener is listening to the same port as the server instance, these two listeners always have the same state.<br /><br /> Is not nullable.<br /><br /> Note: The values in this column come from the TSD_listener object. The column does not support an offline state because when the TDS_listener is offline, it cannot be queried for state.|  
|**state_desc**|**nvarchar(16)**|Description of **state**, one of:<br /><br /> ONLINE<br /><br /> PENDING_RESTART<br /><br /> Is not nullable.|  
|**start_time**|**datetime**|Timestamp indicating when the listener was started. Is not nullable.|  
  
## Security  
  
### Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.md)   
 [Always On Availability Groups Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)   
 [Always On Availability Groups Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/always-on-availability-groups-dynamic-management-views-functions.md)  
  
  
