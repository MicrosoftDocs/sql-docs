---
title: "sp_revokelogin (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_revokelogin_TSQL"
  - "sp_revokelogin"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_revokelogin"
ms.assetid: cb1ab102-1ae0-4811-9144-9a8121ef2d7e
author: VanMSFT
ms.author: vanto
manager: craigg
---
# sp_revokelogin (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes the login entries from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for a Windows user or group created by using CREATE LOGIN, **sp_grantlogin**, or **sp_denylogin**.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [DROP LOGIN](../../t-sql/statements/drop-login-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_revokelogin [ @loginame= ] 'login'  
```  
  
## Arguments  
 [ **@loginame=**] **'***login***'**  
 Is the name of the Windows user or group. *login* is **sysname**, with no default. *login* can be any existing Windows user name or group in the form *Computer name*\\*User or Domain*\\*User*.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_revokelogin** disables connections using the account specified by the *login* parameter. But Windows users that have been granted access to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] through membership in a Windows group can still connect as the group after their individual access has been revoked. Similarly, if the *login* parameter specifies the name of a Windows group, members of that group that have been separately granted access to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will still be able to connect.  
  
 For example, if Windows user **ADVWORKS\john** is a member of the Windows group **ADVWORKS\Admins**, and **sp_revokelogin** revokes the access of `ADVWORKS\john`:  
  
```  
sp_revokelogin [ADVWORKS\john]  
```  
  
 User **ADVWORKS\john** can still connect if **ADVWORKS\Admins** has been granted access to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Similarly, if Windows group **ADVWORKS\Admins** has its access revoked but **ADVWORKS\john** is granted access, **ADVWORKS\john** can still connect.  
  
 Use **sp_denylogin** to explicitly prevent users from connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], regardless of their Windows group memberships.  
  
 **sp_revokelogin** cannot be executed within a user-defined transaction.  
  
## Permissions  
 Requires ALTER ANY LOGIN permission on the server.  
  
## Examples  
 The following example removes the login entries for the Windows user `Corporate\MollyA`.  
  
```  
EXEC sp_revokelogin 'Corporate\MollyA';  
```  
  
 Or  
  
```  
EXEC sp_revokelogin [Corporate\MollyA];  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [DROP LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/drop-login-transact-sql.md)   
 [sp_denylogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-denylogin-transact-sql.md)   
 [sp_droplogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droplogin-transact-sql.md)   
 [sp_grantlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grantlogin-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
