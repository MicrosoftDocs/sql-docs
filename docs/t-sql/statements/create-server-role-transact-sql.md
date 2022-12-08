---
title: "CREATE SERVER ROLE (Transact-SQL)"
description: CREATE SERVER ROLE (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/02/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SERVER_ROLE_TSQL"
  - "CREATE SERVER ROLE"
  - "SERVER ROLE"
  - "CREATE_SERVER_ROLE_TSQL"
helpviewer_keywords:
  - "SERVER ROLE"
  - "SERVER ROLE, CREATE"
  - "CREATE SERVER ROLE statement"
  - "ROLE"
  - "user-defined server roles [SQL Server]"
  - "roles, server"
dev_langs:
  - "TSQL"
---
# CREATE SERVER ROLE (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Creates a new user-defined server role.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
CREATE SERVER ROLE role_name [ AUTHORIZATION server_principal ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *role_name*  
 Is the name of the server role to be created.  
  
 AUTHORIZATION *server_principal*  
 Is the login that will own the new server role. If no login is specified, the server role will be owned by the login that executes CREATE SERVER ROLE.  
  
## Remarks  
 Server roles are server-level securables. After you create a server role, configure the server-level permissions of the role by using GRANT, DENY, and REVOKE. To add logins to or remove logins from a server role, use [ALTER SERVER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-role-transact-sql.md). To drop a server role, use [DROP SERVER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-role-transact-sql.md). For more information, see [sys.server_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md).  
  
 You can view the server roles by querying the [sys.server_role_members](../../relational-databases/system-catalog-views/sys-server-role-members-transact-sql.md) and [sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md) catalog views.  
  
 Server roles cannot be granted permission on database-level securables. To create database roles, see [CREATE ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-role-transact-sql.md).  
  
 For information about designing a permissions system, see [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md).  
  
## Permissions  
 Requires CREATE SERVER ROLE permission or membership in the sysadmin fixed server role.  
  
 Also requires IMPERSONATE on the *server_principal* for logins, ALTER permission for server roles used as the *server_principal*, or membership in a Windows group that is used as the server_principal.  
  
 This will fire the Audit Server Principal Management event withthe object type set to server role and event type to add.  
  
 When you use the AUTHORIZATION option to assign server role ownership, the following permissions are also required:  
  
-   To assign ownership of a server role to another login, requires IMPERSONATE permission on that login.  
  
-   To assign ownership of a server role to another server role, requires membership in the recipient server role or ALTER permission on that server role.  
  
## Examples  
  
### A. Creating a server role that is owned by a login  
 The following example creates the server role `buyers` that is owned by login `BenMiller`.  
  
```sql  
USE master;  
CREATE SERVER ROLE buyers AUTHORIZATION BenMiller;  
GO  
```  
  
### B. Creating a server role that is owned by a fixed server role  
 The following example creates the server role `auditors` that is owned the `securityadmin` fixed server role.  
  
```sql  
USE master;  
CREATE SERVER ROLE auditors AUTHORIZATION securityadmin;  
GO  
```  
  
## See Also  
 [DROP SERVER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-role-transact-sql.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sp_addrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addrolemember-transact-sql.md)   
 [sys.database_role_members &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-role-members-transact-sql.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)   
 [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md)  
  
  
