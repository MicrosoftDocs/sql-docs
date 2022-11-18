---
description: "sp_addsrvrolemember (Transact-SQL)"
title: "sp_addsrvrolemember (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/20/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_addsrvrolemember"
  - "sp_addsrvrolemember_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_addsrvrolemember"
ms.assetid: 777f0e09-8ee5-4cb2-a3ac-939d02c3cd22
author: markingmyname
ms.author: maghan
---
# sp_addsrvrolemember (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Adds a login as a member of a fixed server role.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER SERVER ROLE](../../t-sql/statements/alter-server-role-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addsrvrolemember [ @loginame= ] 'login'   
    , [ @rolename = ] 'role'  
```  
  
## Arguments  
 [ @loginame **=** ] **'**_login_**'**  
 Is the name of the login being added to the fixed server role. *login* is **sysname**, with no default. *login* can be a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or a Windows login. If the Windows login has not already been granted access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], access is automatically granted.  
  
 [ @rolename **=** ] **'**_role_**'**  
 Is the name of the fixed server role to which the login is being added. *role* is **sysname**, with a default of NULL, and must be one of the following values:  
  
-   sysadmin  
  
-   securityadmin  
  
-   serveradmin  
  
-   setupadmin  
  
-   processadmin  
  
-   diskadmin  
  
-   dbcreator  
  
-   bulkadmin  

## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 When a login is added to a fixed server role, the login gains the permissions associated with that role.  
  
 The role membership of the sa login and public cannot be changed.  
  
 Use sp_addrolemember to add a member to a fixed database or user-defined role.  
  
 sp_addsrvrolemember cannot be executed within a user-defined transaction.  
  
## Permissions  
 Requires membership in the role to which the new member is being added.  
  
## Examples  
 The following example adds the Windows login `Corporate\HelenS` to the `sysadmin` fixed server role.  
  
```  
EXEC sp_addsrvrolemember 'Corporate\HelenS', 'sysadmin';  
GO  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [sp_addrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addrolemember-transact-sql.md)   
 [sp_dropsrvrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropsrvrolemember-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)   
 [CREATE SERVER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-role-transact-sql.md)   
 [DROP SERVER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-role-transact-sql.md)  
  
  
