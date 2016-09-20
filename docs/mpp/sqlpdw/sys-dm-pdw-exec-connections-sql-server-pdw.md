---
title: "sys.dm_pdw_exec_connections (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 8daeb3d5-821d-46bb-aa67-8d5983356633
caps.latest.revision: 6
author: BarbKess
---
# sys.dm_pdw_exec_connections (SQL Server PDW)
Returns information about the connections established to this instance of SQL Server PDW and the details of each connection.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|session_id|**int**|Identifies the session associated with this connection. Use `SESSION_ID()` to return the `session_id` of the current connection.|  
|connect_time|**datetime**|Timestamp when connection was established. Is not nullable.|  
|encrypt_option|**nvarchar(40)**|Indicates TRUE (connection is encrypted) or FALSE (connection is not enctypred).|  
|auth_scheme|**nvarchar(40)**|Specifies SQL Server/Windows Authentication scheme used with this connection. Is not nullable.|  
|client_id|**varchar(48)**|IP address of the client connecting to this server. Is nullable.|  
|sql_spid|**int**|The server process ID of the connection. Use `@@SPID` to return the `sql_spid` of the current connection.For most purposed, use the `session_id` instead.|  
  
## Permissions  
Requires **VIEW SERVER STATE** permission on the server.  
  
## Relationship Cardinalities  
  
||||  
|-|-|-|  
|dm_pdw_exec_sessions.session_id|dm_pdw_exec_connections.session_id|One-to-one|  
|dm_pdw_exec_requests.connection_id|dm_pdw_exec_connections.connection_id|Many to one|  
  
## Examples  
Typical query to gather information about a queries own connection.  
  
```  
SELECT  
    c.session_id, c.encrypt_option,  
    c.auth_scheme, s.client_id, s.login_name,   
    s.status, s.query_count  
FROM sys.dm_pdw_exec_connections AS c  
JOIN sys.dm_pdw_exec_sessions AS s  
    ON c.session_id = s.session_id  
WHERE c.session_id = SESSION_ID();  
```  
  
## See Also  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
