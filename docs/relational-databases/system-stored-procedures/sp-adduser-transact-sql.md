---
title: "sp_adduser (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_adduser"
  - "sp_adduser_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_adduser"
ms.assetid: 61a40eb4-573f-460c-9164-bd1bbfaf8b25
author: VanMSFT
ms.author: vanto
manager: craigg
---
# sp_adduser (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Adds a new user to the current database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE USER](../../t-sql/statements/create-user-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_adduser [ @loginame = ] 'login'   
    [ , [ @name_in_db = ] 'user' ]   
    [ , [ @grpname = ] 'role' ]   
```  
  
## Arguments  
 [ **@loginame =** ] **'***login***'**  
 Is the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or Windows login. *login* is a **sysname**, with no default. *login* must be an existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or Windows login.  
  
 [ **@name_in_db =** ] **'***user***'**  
 Is the name for the new database user. *user* is a **sysname**, with a default of NULL. If *user* is not specified, the name of the new database user defaults to the *login* name. Specifying *user* gives the new user a name in the database different from the server-level login name.  
  
 [ **@grpname =** ] **'***role***'**  
 Is the database role of which the new user becomes a member. *role* is **sysname**, with a default of NULL. *role* must be a valid database role in the current database.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_adduser** will also create a schema that has the name of the user.  
  
 After a user has been added, use the GRANT, DENY, and REVOKE statements to define the permissions that control the activities performed by the user.  
  
 Use **sys.server_principals** to display a list of valid login names.  
  
 Use **sp_helprole** to display a list of the valid role names. When you specify a role, the user automatically gains the permissions that are defined for the role. If a role is not specified, the user gains the permissions granted to the default **public** role. To add a user to a role, a value for the *user name* must be supplied. (*username* can be the same as *login_id*.)  
  
 User **guest** already exists in every database. Adding user **guest** will enable this user, if it was previously disabled. By default, user **guest** is disabled in new databases.  
  
 **sp_adduser** cannot be executed inside a user-defined transaction.  
  
 You cannot add a **guest** user because a **guest** user already exists inside every database. To enable the **guest** user, grant **guest** CONNECT permission as shown:  
  
```  
GRANT CONNECT TO guest;  
GO  
```  
  
## Permissions  
 Requires ownership of the database.  
  
## Examples  
  
### A. Adding a database user  
 The following example adds the database user `Vidur` to the existing `Recruiting` role in the current database, using the existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `Vidur`.  
  
```  
EXEC sp_adduser 'Vidur', 'Vidur', 'Recruiting';  
```  
  
### B. Adding a database user with the same login ID  
 The following example adds user `Arvind` to the current database for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `Arvind`. This user belongs to the default **public** role.  
  
```  
EXEC sp_adduser 'Arvind';  
```  
  
### C. Adding a database user with a different name than its server-level login  
 The following example adds [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `BjornR` to the current database that has a user name of `Bjorn`, and adds database user `Bjorn` to the `Production` database role.  
  
```  
EXEC sp_adduser 'BjornR', 'Bjorn', 'Production';  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [sys.server_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)   
 [sp_addrole &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addrole-transact-sql.md)   
 [CREATE USER &#40;Transact-SQL&#41;](../../t-sql/statements/create-user-transact-sql.md)   
 [sp_dropuser &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropuser-transact-sql.md)   
 [sp_grantdbaccess &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grantdbaccess-transact-sql.md)   
 [sp_grantlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grantlogin-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
