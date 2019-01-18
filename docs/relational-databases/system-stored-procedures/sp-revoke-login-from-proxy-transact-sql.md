---
title: "sp_revoke_login_from_proxy (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_revoke_login_from_proxy_TSQL"
  - "sp_revoke_login_from_proxy"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_revoke_login_from_proxy"
ms.assetid: e4546c13-9fba-4bab-8b42-d6f18b33ec25
author: VanMSFT
ms.author: vanto
manager: craigg
---
# sp_revoke_login_from_proxy (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes access to a proxy for a security principal.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_revoke_login_from_proxy   
    [ @name = ] 'name' ,  
    [ @proxy_id = ] id ,  
    [ @proxy_name = ] 'proxy_name'  
```  
  
## Arguments  
 [ **@name=** ] **'***name***'**  
 The name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, server role, or **msdb** database role to remove access for. *name* is **nvarchar(256)** with no default.  
  
 [ **@proxy_id=** ] *id*  
 The id of the proxy to remove access for. Either *id* or *proxy_name* must be specified, but both cannot be specified. The *id* is **int**, with a default of NULL.  
  
 [ **@proxy_name=** ] **'***proxy_name***'**  
 The name of the proxy to remove access for. Either *id* or *proxy_name* must be specified, but both cannot be specified. The *proxy_name* is **sysname**, with a default of NULL.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 Jobs that are owned by the login that references this proxy will fail to run.  
  
## Permissions  
 To execute this stored procedure, a user must be a member of the **sysadmin** fixed server role.  
  
## Examples  
 The following example revokes access for the login `terrid` to access the proxy `Catalog application proxy`.  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_revoke_login_from_proxy  
    @name = N'terrid',  
    @proxy_name = N'Catalog application proxy' ;  
GO  
```  
  
## See Also  
 [SQL Server Agent Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sql-server-agent-stored-procedures-transact-sql.md)   
 [sp_grant_login_to_proxy &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grant-login-to-proxy-transact-sql.md)   
 [sp_help_proxy &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-proxy-transact-sql.md)  
  
  
