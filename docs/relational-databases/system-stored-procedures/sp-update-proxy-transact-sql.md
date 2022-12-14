---
description: "sp_update_proxy (Transact-SQL)"
title: "sp_update_proxy (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_update_proxy"
  - "sp_update_proxy_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ALTER PROXY statement"
  - "sp_update_proxy"
ms.assetid: 864fd0e6-9d61-4f07-92ef-145318d2f881
author: VanMSFT
ms.author: vanto
---
# sp_update_proxy (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Changes the properties of an existing proxy.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_update_proxy   
    [ @proxy_id = ] id,  
    [ @proxy_name = ] 'proxy_name',  
    [ @credential_name = ] 'credential_name' ,  
    [ @credential_id = ] credential_id ,  
    [ @new_name = ] 'new_name' ,  
    [ @enabled = ] is_enabled ,  
    [ @description = ] 'description'  
```  
  
## Arguments  
`[ @proxy_id = ] id`
 The proxy identification number of the proxy to change. The *proxy_id* is **int**, with a default of NULL.  
  
`[ @proxy_name = ] 'proxy_name'`
 The name of the proxy to change. The *proxy_name* is **sysname**, with a default of NULL.  
  
`[ @credential_name = ] 'credential_name'`
 The name of the new credential for the proxy. The *credential_name* is **sysname**, with a default of NULL. Either *credential_name* or *credential_id* may be specified.  
  
`[ @credential_id = ] credential_id`
 The identification number of the new credential for the proxy. The *credential_id* is **int**, with a default of NULL. Either *credential_name* or *credential_id* may be specified.  
  
`[ @new_name = ] 'new_name'`
 The new name of the proxy. The *new_name* is **sysname**, with a default of NULL. When provided, the procedure changes the name of the proxy to *new_name*. When this argument is NULL, the name of the proxy remains unchanged.  
  
`[ @enabled = ] is_enabled`
 Is whether the proxy is enabled. The *is_enabled* flag is **tinyint**, with a default of NULL. When *is_enabled* is **0**, the proxy is not enabled, and cannot be used by a job step. When this argument is NULL, the status of the proxy remains unchanged.  
  
`[ @description = ] 'description'`
 The new description of the proxy. The *description* is **nvarchar(512)**, with a default of NULL. When this argument is NULL, the description of the proxy remains unchanged.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 Either **\@proxy_name** or **\@proxy_id** must be specified. If both arguments are specified, the arguments must both refer to the same proxy or the stored procedure fails.  
  
 Either **\@credential_name** or **\@credential_id** must be specified to change the credential for the proxy. If both arguments are specified, the arguments must refer to the same credential or the stored procedure fails.  
  
 This procedure changes the proxy, but does not change access to the proxy. To change access to a proxy, use **sp_grant_login_to_proxy** and **sp_revoke_login_from_proxy**.  
  
## Permissions  
 Only members of the **sysadmin** fixed security role can execute this procedure.  
  
## Examples  
 The following example sets the enabled value for the proxy `Catalog application proxy` to `0`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_update_proxy  
    @proxy_name = 'Catalog application proxy',  
    @enabled = 0;  
GO  
```  
  
## See Also  
 [SQL Server Agent Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sql-server-agent-stored-procedures-transact-sql.md)   
 [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md)   
 [sp_add_proxy &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-proxy-transact-sql.md)   
 [sp_delete_proxy &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-proxy-transact-sql.md)   
 [sp_grant_login_to_proxy &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grant-login-to-proxy-transact-sql.md)   
 [sp_revoke_login_from_proxy &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-revoke-login-from-proxy-transact-sql.md)  
  
  
