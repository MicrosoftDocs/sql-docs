---
title: "sp_helpserver (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpserver"
  - "sp_helpserver_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_helpserver"
ms.assetid: e8f42de7-c738-41c3-8bf5-dbd559dc7184
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpserver (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Reports information about a particular remote or replication server, or about all servers of both types. Provides the server name, the network name of the server, the replication status of the server, the identification number of the server, and the collation name. Also provides time-out values for connecting to, or queries against, linked servers.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpserver [ [ @server = ] 'server' ]   
  [ , [ @optname = ] 'option' ]   
  [ , [ @show_topology = ] 'show_topology' ]  
```  
  
## Arguments  
`[ @server = ] 'server'`
 Is the server about which information is reported. When *server* is not specified, reports about all servers in **master.sys.servers**. *server* is **sysname**, with a default of NULL.  
  
`[ @optname = ] 'option'`
 Is the option describing the server. *option* is **varchar(**35**)**, with a default of NULL, and must be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**collation compatible**|Affects the Distributed Query execution against linked servers. If this option is set to true,|  
|**data access**|Enables and disables a linked server for distributed query access.|  
|**dist**|Distributor.|  
|**dpub**|Remote Publisher to this Distributor.|  
|**lazy schema validation**|Skips schema checking of remote tables at the beginning of the query.|  
|**pub**|Publisher.|  
|**rpc**|Enables RPC from the specified server.|  
|**rpc out**|Enables RPC to the specified server.|  
|**sub**|Subscriber.|  
|**system**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**use remote collation**|Uses the collation of a remote column instead of that of the local server.|  
  
`[ @show_topology = ] 'show_topology'`
 Is the relationship of the specified server to other servers. *show_topology* is **varchar(**1**)**, with a default of NULL. If *show_topology* is not equal to **t** or is NULL, **sp_helpserver** returns columns listed in the Result Sets section. If *show_topology* is equal to **t**, in addition to the columns listed in the Result Sets, **sp_helpserver** also returns **topx** and **topy** information.  
  
## Return Code Values  
 0 (success) or 1 (failure).  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Server name.|  
|**network_name**|**sysname**|Network name of the server.|  
|**status**|**varchar(**70**)**|Server status.|  
|**id**|**char(**4**)**|Identification number of the server.|  
|**collation_name**|**sysname**|Collation of the server.|  
|**connect_timeout**|**int**|Time-out value for connecting to linked server.|  
|**query_timeout**|**int**|Time-out value for queries against linked server.|  
  
## Remarks  
 A server can have more than one status.  
  
## Permissions  
 No permissions are checked.  
  
## Examples  
  
### A. Displaying information about all servers  
 The following example displays information about all servers by using `sp_helpserver` with no parameters.  
  
```  
USE master;  
GO  
EXEC sp_helpserver;  
```  
  
### B. Displaying information about a specific server  
 The following example displays all information about the `SEATTLE2` server.  
  
```  
USE master;  
GO  
EXEC sp_helpserver 'SEATTLE2';  
```  
  
## See Also  
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [sp_adddistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql.md)   
 [sp_addserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md)   
 [sp_addsubscriber &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addsubscriber-transact-sql.md)   
 [sp_changesubscriber &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changesubscriber-transact-sql.md)   
 [sp_dropserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropserver-transact-sql.md)   
 [sp_dropsubscriber &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropsubscriber-transact-sql.md)   
 [sp_helpdistributor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdistributor-transact-sql.md)   
 [sp_helpremotelogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpremotelogin-transact-sql.md)   
 [sp_helpsubscriberinfo &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpsubscriberinfo-transact-sql.md)   
 [sp_serveroption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-serveroption-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
