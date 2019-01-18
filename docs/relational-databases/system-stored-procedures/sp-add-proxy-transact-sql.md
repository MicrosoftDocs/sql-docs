---
title: "sp_add_proxy (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_add_proxy"
  - "sp_add_proxy_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CREATE PROXY statement"
  - "sp_add_proxy"
ms.assetid: cb59df37-f103-439b-bec1-2871fb669a8b
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_add_proxy (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds the specified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_add_proxy  
    [ @proxy_name = ] 'proxy_name' ,  
    [ @enabled = ] is_enabled ,  
    [ @description = ] 'description' ,  
    [ @credential_name = ] 'credential_name' ,  
    [ @credential_id = ] credential_id ,  
    [ @proxy_id = ] id OUTPUT   
```  
  
## Arguments  
 [ **@proxy_name**= ] **'***proxy_name***'**  
 The name of the proxy to create. The *proxy_name* is **sysname**, with a default of NULL. When the *proxy_name* is NULL or an empty string, the name of the proxy defaults to the *user_name* supplied.  
  
 [ **@enabled** = ] *is_enabled*  
 Specifies whether the proxy is enabled. The *is_enabled* flag is **tinyint**, with a default of 1. When *is_enabled* is **0**, the proxy is not enabled, and cannot be used by a job step.  
  
 [ **@description**= ] **'***description***'**  
 A description of the proxy. The description is **nvarchar(512)**, with a default of NULL. The description allows you to document the proxy, but is not otherwise used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. Therefore, this argument is optional.  
  
 [ **@credential_name** = ] **'***credential_name***'**  
 The name of the credential for the proxy. The *credential_name* is **sysname**, with a default of NULL. Either *credential_name* or *credential_id* must be specified.  
  
 [ **@credential_id** = ] *credential_id*  
 The identification number of the credential for the proxy. The *credential_id* is **int**, with a default of NULL. Either *credential_name* or *credential_id* must be specified.  
  
 [ **@proxy_id**= ] *id* OUTPUT  
 The proxy identification number assigned to the proxy if created successfully.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 This stored procedure must be run in the **msdb** database.  
  
 A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy manages security for job steps that involve subsystems other than the [!INCLUDE[tsql](../../includes/tsql-md.md)] subsystem. Each proxy corresponds to a security credential. A proxy may have access to any number of subsystems.  
  
## Permissions  
 Only members of the **sysadmin** fixed security role can execute this procedure.  
  
 Members of the **sysadmin** fixed security role can create job steps that use any proxy. Use the stored procedure [sp_grant_login_to_proxy &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grant-login-to-proxy-transact-sql.md) to grant other logins access to the proxy.  
  
## Examples  
 This example creates a proxy for the credential `CatalogApplicationCredential`. The code assumes that the credential already exists. For more information about credentials, see [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-credential-transact-sql.md).  
  
```  
USE msdb ;  
GO  
  
EXEC dbo.sp_add_proxy  
    @proxy_name = 'Catalog application proxy',  
    @enabled = 1,  
    @description = 'Maintenance tasks on catalog application.',  
    @credential_name = 'CatalogApplicationCredential' ;  
GO  
```  
  
## See Also  
 [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-credential-transact-sql.md)   
 [sp_grant_login_to_proxy &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grant-login-to-proxy-transact-sql.md)   
 [sp_revoke_login_from_proxy &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-revoke-login-from-proxy-transact-sql.md)  
  
  
