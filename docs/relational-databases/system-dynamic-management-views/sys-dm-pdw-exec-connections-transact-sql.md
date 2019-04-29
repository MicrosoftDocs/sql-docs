---
title: "sys.dm_pdw_exec_connections (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.technology: data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 2625466b-d0ef-4c71-bedc-6d13491a8351
author: ronortloff
ms.author: rortloff
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.dm_pdw_exec_connections (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  Returns information about the connections established to this instance of [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] and the details of each connection.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|session_id|**int**|Identifies the session associated with this connection. Use `SESSION_ID()` to return the `session_id` of the current connection.|  
|connect_time|**datetime**|Timestamp when connection was established. Is not nullable.|  
|encrypt_option|**nvarchar(40)**|Indicates TRUE (connection is encrypted) or FALSE (connection is not enctypred).|  
|auth_scheme|**nvarchar(40)**|Specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/Windows Authentication scheme used with this connection. Is not nullable.|  
|client_id|**varchar(48)**|IP address of the client connecting to this server. Is nullable.|  
|sql_spid|**int**|The server process ID of the connection. Use `@@SPID` to return the `sql_spid` of the current connection.For most purposed, use the `session_id` instead.|  
  
## Permissions  
 Requires **VIEW SERVER STATE** permission on the server.  
  
## Relationship Cardinalities  
  
||||  
|-|-|-|  
|dm_pdw_exec_sessions.session_id|dm_pdw_exec_connections.session_id|One-to-one|  
|dm_pdw_exec_requests.connection_id|dm_pdw_exec_connections.connection_id|Many to one|  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
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
 [SQL Data Warehouse and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  
  
  

