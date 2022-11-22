---
description: "sp_addrole (Transact-SQL)"
title: "sp_addrole (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_addrole"
  - "sp_addrole_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_addrole"
ms.assetid: e8a21642-8440-419a-8585-93d3d9d44f00
author: VanMSFT
ms.author: vanto
---
# sp_addrole (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Creates a new database role in the current database.  
  
> [!IMPORTANT]
>  **sp_addrole** is included for compatibility with earlier versions of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and may not be supported in a future release. Use [CREATE ROLE](../../t-sql/statements/create-role-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addrole [ @rolename = ] 'role' [ , [ @ownername = ] 'owner' ]   
```  
  
## Arguments  
`[ @rolename = ] 'role'`
 Is the name of the new database role. *role* is a **sysname**, with no default. *role* must be a valid identifier (ID) and must not already exist in the current database.  
  
`[ @ownername = ] 'owner'`
 Is the owner of the new database role. *owner* is a **sysname**, with a default of the current executing user. *owner* must be a database user or database role in the current database.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 The names of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database roles can contain from 1 through 128 characters, including letters, symbols, and numbers. The names of database roles cannot :contain a backslash character (\\), be NULL, or an empty string (**''**).  
  
 After you add a database role, use [sp_addrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addrolemember-transact-sql.md) to add principals to the role. When GRANT, DENY, or REVOKE statements are used to apply permissions to the database role, members of the database role inherit those permissions as if the permissions were applied directly to their accounts.  
  
> [!NOTE]  
>  New server roles cannot be created. Roles can only be created at the database level.  
  
 **sp_addrole** cannot be used inside a user-defined transaction.  
  
## Permissions  
 Requires CREATE ROLE permission on the database. If creating a schema, requires CREATE SCHEMA on the database. If *owner* is specified as a user or group, requires IMPERSONATE on that user or group. If *owner* is specified as a role, requires ALTER permission on that role or on a member of that role. If owner is specified as an application role, requires ALTER permission on that application role.  
  
## Examples  
 The following example adds a new role called `Managers` to the current database.  
  
```  
EXEC sp_addrole 'Managers';  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [CREATE ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-role-transact-sql.md)  
  
  
