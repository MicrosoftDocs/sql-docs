---
description: "sp_enum_proxy_for_subsystem (Transact-SQL)"
title: "sp_enum_proxy_for_subsystem (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_enum_proxy_for_subsystem_TSQL"
  - "sp_enum_proxy_for_subsystem"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_enum_proxy_for_subsystems"
ms.assetid: 580cc3be-1068-4a96-8d15-78ca3a5bb719
ms.author: vanto
author: VanMSFT
---
# sp_enum_proxy_for_subsystem (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Lists permissions for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxies to access subsystems.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sp_enum_proxy_for_subsystem  
    [ @proxy_id = ] proxy_id,  
    [ @proxy_name = ] 'proxy_name',  
    [ @subsystem_id = ] subsystem_id,  
    [ @subsystem_name = ] 'subsystem_name'  
```  
  
## Arguments  
`[ @proxy_id = ] proxy_id`
 The identification number of the proxy to list information for. The *proxy_id* is **int**, with a default of NULL. Either the *id* or the *proxy_name* may be specified.  
  
`[ @proxy_name = ] 'proxy_name'`
 The name of the proxy to list information for. The *proxy_name* is **sysname**, with a default of NULL. Either the *id* or the *proxy_name* may be specified.  
  
`[ @subsystem_id = ] subsystem_id`
 The identification number of the subsystem to list information for. The *subsystem_id* is **int**, with a default of NULL. Either the *subsystem_id* or the *subsystem_name* may be specified.  
  
`[ @subsystem_name = ] 'subsystem_name'`
 The name of the subsystem to list information for. The *subsystem_name* is **sysname**, with a default of NULL. Either the *subsystem_id* or the *subsystem_name* may be specified.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**subsystem_id**|**int**|Subsystem identification number.|  
|**subsystem_name**|**sysname**|The name of the subsystem.|  
|**proxy_id**|**int**|Proxy identification number.|  
|**proxy_name**|**sysname**|The name of the proxy.|  
  
## Remarks  
 When no parameters are provided, **sp_enum_proxy_for_subsystem** lists information about all proxies in the instance for every subsystem.  
  
 When a proxy id or proxy name is provided, **sp_enum_proxy_for_subsystem** lists subsystems that the proxy has access to. When a subsystem id or subsystem name is provided, **sp_enum_proxy_for_subsystem** lists proxies that have access to that subsystem.  
  
 When both proxy information and subsystem information is provided, the result set returns a row if the proxy specified has access to the subsystem specified.  
  
 This stored procedure is located in **msdb**.  
  
## Permissions  
 Execution permissions for this procedure default to members of the **sysadmin** fixed server role.  
  
## Examples  
  
### A. Listing all associations  
 The following example lists all permissions established between proxies and subsystems for the current instance.  
  
```sql
USE msdb ;  
GO  
  
EXEC dbo.sp_enum_proxy_for_subsystem ;  
GO  
```  
  
### B. Determining if a proxy has access to a specific subsystem  
 The following example returns a row if the proxy `Catalog application proxy` has access to the `ActiveScripting` subsystem. Otherwise, the example returns an empty result set.  
  
```sql
USE msdb ;  
GO  
  
EXEC dbo.sp_enum_proxy_for_subsystem  
    @subsystem_name = 'ActiveScripting',  
    @proxy_name = 'Catalog application proxy' ;  
GO  
```  
  
## See Also  
 [sp_grant_proxy_to_subsystem &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grant-proxy-to-subsystem-transact-sql.md)  
  
  
