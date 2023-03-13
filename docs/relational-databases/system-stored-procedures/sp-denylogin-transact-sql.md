---
title: "sp_denylogin (Transact-SQL)"
description: "sp_denylogin (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_denylogin_TSQL"
  - "sp_denylogin"
helpviewer_keywords:
  - "sp_denylogin"
dev_langs:
  - "TSQL"
---
# sp_denylogin (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Prevents a Windows user or Windows group from connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md) instead.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_denylogin [ @loginame = ] 'login'   
```  
  
## Arguments  
`[ @loginame = ] 'login_ '`
 Is the name of a Windows user or group. *login* is **sysname**, with no default.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_denylogin** denies CONNECT SQL permission to the server-level principal mapped to the specified Windows user or Windows group. If the server principal does not exist, it will be created. The new principal will be visible in the [sys.server_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md) catalog view.  
  
 **sp_denylogin** cannot be executed within a user-defined transaction.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## Examples  
 The following example shows how to use **sp_denylogin** to prevent Windows user `CORPORATE\GeorgeV` from connecting to the server.  
  
```  
EXEC sp_denylogin 'CORPORATE\GeorgeV';  
```  
  
## See Also  
 [sp_grantlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grantlogin-transact-sql.md)   
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [ALTER LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/alter-login-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
