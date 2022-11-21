---
title: "sys.tcp_endpoints (Transact-SQL)"
description: sys.tcp_endpoints (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.tcp_endpoints"
  - "sys.tcp_endpoints_TSQL"
  - "tcp_endpoints"
  - "tcp_endpoints_TSQL"
helpviewer_keywords:
  - "sys.tcp_endpoints catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 43cc3afa-cced-4463-8e97-fbfdaf2e4fa8
---
# sys.tcp_endpoints (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains one row for each TCP endpoint that is in the system. The endpoints that are described by **sys.tcp_endpoints** provide an object to grant and revoke the connection privilege. The information that is displayed regarding ports and IP addresses is not used to configure the protocols and may not match the actual protocol configuration. To view and configure protocols, use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.  
  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**< inherited columns>**||Inherits columns from [sys.endpoints](../../relational-databases/system-catalog-views/sys-endpoints-transact-sql.md).|  
|**port**|int|The port number that the endpoint is listening on. Is not nullable.|  
|**is_dynamic_port**|bit|1 = Port number was dynamically assigned.<br /><br /> Is not nullable.|  
|**ip_address**|**nvarchar(45)**|Listener IP address as specified by the LISTENER_IP clause. Is nullable.|  
  
## Remarks  
 Execute the following query to gather information about the endpoints and connections. Endpoints without current connections or without TCP connections will appear with NULL values. Add the **WHERE** clause `WHERE des.session_id = @@SPID` to return information about the current connection.  
  
```  
SELECT des.login_name, des.host_name, program_name,  dec.net_transport, des.login_time,   
e.name AS endpoint_name, e.protocol_desc, e.state_desc, e.is_admin_endpoint,   
t.port, t.is_dynamic_port, dec.local_net_address, dec.local_tcp_port   
FROM sys.endpoints AS e  
LEFT JOIN sys.tcp_endpoints AS t  
   ON e.endpoint_id = t.endpoint_id  
LEFT JOIN sys.dm_exec_sessions AS des  
   ON e.endpoint_id = des.endpoint_id  
LEFT JOIN sys.dm_exec_connections AS dec  
   ON des.session_id = dec.session_id;  
```  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Endpoints Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/endpoints-catalog-views-transact-sql.md)  
  
  
