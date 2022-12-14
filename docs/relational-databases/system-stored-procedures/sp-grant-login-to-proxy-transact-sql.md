---
description: "sp_grant_login_to_proxy (Transact-SQL)"
title: "sp_grant_login_to_proxy (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_grant_login_to_proxy"
  - "sp_grant_login_to_proxy_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_grant_login_to_proxy"
ms.assetid: 90e1a6d5-a692-4462-a163-4b0709d83150
ms.author: vanto
author: VanMSFT
---
# sp_grant_login_to_proxy (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Grants a security principal access to a proxy.  

  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sp_grant_login_to_proxy   
     { [ @login_name = ] 'login_name'   
     | [ @fixed_server_role = ] 'fixed_server_role'   
     | [ @msdb_role = ] 'msdb_role' } ,   
     { [ @proxy_id = ] id | [ @proxy_name = ] 'proxy_name' }  
```  
  
## Arguments  
`[ @login_name = ] 'login_name'`
 The login name to grant access to. The *login_name* is **nvarchar(256)**, with a default of NULL. One of **\@login_name**, **\@fixed_server_role**, or **\@msdb_role** must be specified, or the stored procedure fails.  
  
`[ @fixed_server_role = ] 'fixed_server_role'`
 The fixed server role to grant access to. The *fixed_server_role* is **nvarchar(256)**, with a default of NULL. One of **\@login_name**, **\@fixed_server_role**, or **\@msdb_role** must be specified, or the stored procedure fails.  
  
`[ @msdb_role = ] 'msdb_role'`
 The database role in the **msdb** database to grant access to. The *msdb_role* is **nvarchar(256)**, with a default of NULL. One of **\@login_name**, **\@fixed_server_role**, or **\@msdb_role** must be specified, or the stored procedure fails.  
  
`[ @proxy_id = ] id`
 The identifier for the proxy to grant access for. The *id* is **int**, with a default of NULL. One of **\@proxy_id** or **\@proxy_name** must be specified, or the stored procedure fails.  
  
`[ @proxy_name = ] 'proxy_name'`
 The name of the proxy to grant access for. The *proxy_name* is **nvarchar(256)**, with a default of NULL. One of **\@proxy_id** or **\@proxy_name** must be specified, or the stored procedure fails.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_grant_login_to_proxy** must be run from the **msdb** database.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role may execute **sp_grant_login_to_proxy**.  
  
## Examples  
 The following example allows the login `adventure-works\terrid` to use the proxy `Catalog application proxy`.  
  
```sql
USE msdb ;  
GO  
  
EXEC dbo.sp_grant_login_to_proxy  
    @login_name = N'adventure-works\terrid',  
    @proxy_name = N'Catalog application proxy' ;  
GO  
```  
  
## See Also  
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [sp_add_proxy &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-proxy-transact-sql.md)   
 [sp_revoke_login_from_proxy &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-revoke-login-from-proxy-transact-sql.md)  
  
  
