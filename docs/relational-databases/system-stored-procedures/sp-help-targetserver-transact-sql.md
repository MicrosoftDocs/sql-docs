---
title: "sp_help_targetserver (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_help_targetserver_TSQL"
  - "sp_help_targetserver"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_targetserver"
ms.assetid: f841d3bd-901a-4980-ad0b-1c6eeba3f717
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_help_targetserver (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Lists all target servers.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_targetserver   
     [ [ @server_name = ] 'server_name' ]  
```  
  
## Arguments  
`[ @server_name = ] 'server_name'`
 The name of the server for which to return information. *server_name* is **nvarchar(30)**, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 If *server_name* is not specified, **sp_help_targetserver** returns this result set.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**server_id**|**int**|Server identification number.|  
|**server_name**|**nvarchar(30)**|Server name.|  
|**location**|**nvarchar(200)**|Location of the specified server.|  
|**time_zone_adjustment**|**int**|Time zone adjustment, in hours, from Greenwich mean time (GMT).|  
|**enlist_date**|**datetime**|Date of the specified server's enlistment.|  
|**last_poll_date**|**datetime**|Date the server was last polled for jobs.|  
|**status**|**int**|Status of the specified server.|  
|**unread_instructions**|**int**|Whether the server has unread instructions. If all rows have been downloaded, this column is **0**.|  
|**local_time**|**datetime**|Local date and time on the target server, which is based on the local time on the target server as of the last poll of the master server.|  
|**enlisted_by_nt_user**|**nvarchar(100)**|Microsoft Windows user that enlisted the target server.|  
|**poll_interval**|**int**|Frequency in seconds with which the target server polls the Master SQLServerAgent service in order to download jobs and upload job status.|  
  
## Permissions  
 To execute this stored procedure, a user must be a member of the **sysadmin** fixed server role.  
  
## Examples  
  
### A. Listing information for all registered target servers  
 The following example lists information for all registered target servers.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_targetserver ;  
GO  
```  
  
### B. Listing information for a specific target server  
 The following example lists information for the target server `SEATTLE2`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_targetserver N'SEATTLE2' ;  
GO  
```  
  
## See Also  
 [sp_add_targetservergroup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-targetservergroup-transact-sql.md)   
 [sp_delete_targetserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-targetserver-transact-sql.md)   
 [sp_delete_targetservergroup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-targetservergroup-transact-sql.md)   
 [sp_update_targetservergroup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-update-targetservergroup-transact-sql.md)   
 [dbo.sysdownloadlist &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysdownloadlist-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
