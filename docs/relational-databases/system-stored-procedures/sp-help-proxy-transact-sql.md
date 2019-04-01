---
title: "sp_help_proxy (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_help_proxy"
  - "sp_help_proxy_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_proxy"
ms.assetid: a2fce164-2b64-40c2-8f35-6eeb7844abf1
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_help_proxy (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Lists information for one or more proxies.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_proxy   
    [ @proxy_id = ] id,  
    [ @proxy_name = ] 'proxy_name' ,  
    [ @subsystem_name = ] 'subsystem_name' ,  
    [ @name = ] 'name'  
```  
  
## Arguments  
`[ @proxy_id = ] id`
 The proxy identification number of the proxy to list information for. The *proxy_id* is **int**, with a default of NULL. Either the *id* or the *proxy_name* may be specified.  
  
`[ @proxy_name = ] 'proxy_name'`
 The name of the proxy to list information for. The *proxy_name* is **sysname**, with a default of NULL. Either the *id* or the *proxy_name* may be specified.  
  
`[ @subsystem_name = ] 'subsystem_name'`
 The name of the subsystem to list proxies for. The *subsystem_name* is **sysname**, with a default of NULL. When *subsystem_name* is specified, *name* must also be specified.  
  
 The following table lists the values for each subsystem.  
  
|Value|Description|  
|-----------|-----------------|  
|ActiveScripting|ActiveX Script|  
|CmdExec|Operating System (CmdExec)|  
|Snapshot|Replication Snapshot Agent|  
|LogReader|Replication Log Reader Agent|  
|Distribution|Replication  Distribution Agent|  
|Merge|Replication Merge Agent|  
|QueueReader|Replication Queue Reader Agent|  
|ANALYSISQUERY|Analysis Services Command|  
|ANALYSISCOMMAND|Analysis Services Query|  
|Dts|SSIS package execution|  
|PowerShell|PowerShell Script|  
  
`[ @name = ] 'name'`
 The name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to list proxies for. The name is **nvarchar(256)**, with a default of NULL. When *name* is specified, *subsystem_name* must also be specified.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**proxy_id**|**int**|Proxy identification number.|  
|**name**|**sysname**|The name of the proxy.|  
|**credential_identity**|**sysname**|The Microsoft Windows domain name and user name for the credential associated with the proxy.|  
|**enabled**|**tinyint**|Whether this proxy is enabled. { **0** = not enabled, **1** = enabled }|  
|**description**|**nvarchar(1024)**|The description for this proxy.|  
|**user_sid**|**varbinary(85)**|The Windows security id of the Windows user for this proxy.|  
|**credential_id**|**int**|The identifier for the credential associated with this proxy.|  
|**credential_identity_exists**|**int**|Whether the credential_identity exists. { 0 = does not exist, 1 = exists }|  
  
## Remarks  
 When no parameters are provided, **sp_help_proxy** lists information for all proxies in the instance.  
  
 To determine which proxies a login can use for a given subsystem, specify *name* and *subsystem_name*. When these arguments are provided, **sp_help_proxy** lists proxies that the login specified may access and that may be used for the specified subsystem.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted the **SQLAgentOperatorRole** fixed database role in the **msdb** database.  
  
 For details about **SQLAgentOperatorRole**, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
> [!NOTE]  
>  The **credential_identity** and **user_sid** columns are only returned in the result set when members of **sysadmin** execute this stored procedure.  
  
## Examples  
  
### A. Listing information for all proxies  
 The following example lists information for all proxies in the instance.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_proxy ;  
GO  
```  
  
### B. Listing information for a specific proxy  
 The following example lists information for the proxy named `Catalog application proxy`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_help_proxy  
    @proxy_name = N'Catalog application proxy' ;  
GO  
```  
  
## See Also  
 [SQL Server Agent Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sql-server-agent-stored-procedures-transact-sql.md)   
 [sp_add_proxy &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-proxy-transact-sql.md)   
 [sp_delete_proxy &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-proxy-transact-sql.md)  
  
  
