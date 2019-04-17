---
title: "sp_password (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_password"
  - "sp_password_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_password"
ms.assetid: 0ecbec81-e637-44a9-a61e-11bf060ef084
author: VanMSFT
ms.author: vanto
manager: craigg
---
# sp_password (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds or changes a password for a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_password [ [ @old = ] 'old_password' , ]  
     { [ @new =] 'new_password' }  
     [ , [ @loginame = ] 'login' ]  
```  
  
## Arguments  
`[ @old = ] 'old_password'`
 Is the old password. *old_password* is **sysname**, with a default of NULL.  
  
`[ @new = ] 'new_password'`
 Is the new password. *new_password* is **sysname**, with no default. *old_password* must be specified if named parameters are not used.  
  
> [!IMPORTANT]  
>  Do not use a NULL password. Use a strong password. For more information, see [Strong Passwords](../../relational-databases/security/strong-passwords.md).  
  
`[ @loginame = ] 'login'`
 Is the name of the login affected by the password change. *login* is **sysname**, with a default of NULL. *login* must already exist and can be specified only by members of the **sysadmin** or **securityadmin** fixed server roles.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_password** calls ALTER LOGIN. This statement supports additional options. For information on changing passwords, see [ALTER LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/alter-login-transact-sql.md).  
  
 **sp_password** cannot be executed within a user-defined transaction.  
  
## Permissions  
 Requires ALTER ANY LOGIN permission. Also requires CONTROL SERVER permission to reset a password without supplying the old password, or if the login that is being changed has CONTROL SERVER permission.  
  
 A principal can change its own password.  
  
## Examples  
  
### A. Changing the password of a login without knowing the old password  
 The following example shows how to use `ALTER LOGIN` to change the password for the login `Victoria` to `B3r1000d#2-36`. This is the preferred method. The user that is executing this command must have CONTROL SERVER permission.  
  
```  
ALTER LOGIN Victoria WITH PASSWORD = 'B3r1000d#2-36';  
GO  
```  
  
### B. Changing a password  
 The following example shows how to use `ALTER LOGIN` to change the password for the login `Victoria` from `B3r1000d#2-36` to `V1cteAmanti55imE`. This is the preferred method. User `Victoria` can execute this command without additional permissions. Other users require ALTER ANY LOGIN permission.  
  
```  
ALTER LOGIN Victoria WITH   
     PASSWORD = 'V1cteAmanti55imE'   
     OLD_PASSWORD = 'B3r1000d#2-36';  
GO  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [ALTER LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/alter-login-transact-sql.md)   
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [sp_addlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlogin-transact-sql.md)   
 [sp_adduser &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adduser-transact-sql.md)   
 [sp_grantlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grantlogin-transact-sql.md)   
 [sp_revokelogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-revokelogin-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
