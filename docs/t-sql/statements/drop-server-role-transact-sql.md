---
title: "DROP SERVER ROLE (Transact-SQL)"
description: DROP SERVER ROLE (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP SERVER ROLE"
  - "DROP_SERVER_ROLE_TSQL"
helpviewer_keywords:
  - "SERVER ROLE, DROP"
  - "DROP SERVER ROLE statement"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DROP SERVER ROLE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-pdw-md](../../includes/appliesto-ss-xxxx-xxxx-pdw-md.md)]

  Removes a user-defined server role.  
  
 User-defined server roles are new in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
DROP SERVER ROLE role_name  
```  
  
## Arguments  
 *role_name*  
 Specifies the user-defined server role to be dropped from the server.  
  
## Remarks  
 User-defined server roles that own securables cannot be dropped from the server. To drop a user-defined server role that owns securables, you must first transfer ownership of those securables or delete them.  
  
 User-defined server roles that have members cannot be dropped. To drop a user-defined server role that has members, you must first remove members of the role by using [ALTER SERVER ROLE](../../t-sql/statements/alter-server-role-transact-sql.md).  
  
 Fixed server roles cannot be removed.  
  
 You can view information about role membership by querying the [sys.server_role_members](../../relational-databases/system-catalog-views/sys-server-role-members-transact-sql.md) catalog view.  
  
## Permissions  
 Requires CONTROL permission on the server role or ALTER ANY SERVER ROLE permission.  
  
## Examples  
  
### A. To drop a server role  
 The following example drops the server role `purchasing`.  
  
```sql  
DROP SERVER ROLE purchasing;  
GO  
```  
  
### B. To view role membership  
 To view role membership, use the **Server Role (Members**) page in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or execute the following query:  
  
```sql  
SELECT SRM.role_principal_id, SP.name AS Role_Name,   
SRM.member_principal_id, SP2.name  AS Member_Name  
FROM sys.server_role_members AS SRM  
JOIN sys.server_principals AS SP  
    ON SRM.Role_principal_id = SP.principal_id  
JOIN sys.server_principals AS SP2   
    ON SRM.member_principal_id = SP2.principal_id  
ORDER BY  SP.name,  SP2.name  
```  
  
### C. To view role membership  
 To determine whether a server role owns another server role, execute the following query:  
  
```sql  
SELECT SP1.name AS RoleOwner, SP2.name AS Server_Role  
FROM sys.server_principals AS SP1  
JOIN sys.server_principals AS SP2  
    ON SP1.principal_id = SP2.owning_principal_id   
ORDER BY SP1.name ;  
```  
  
## See Also  
 [ALTER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-role-transact-sql.md)   
 [CREATE ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-role-transact-sql.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [DROP ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-role-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sp_addrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addrolemember-transact-sql.md)   
 [sys.database_role_members &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-role-members-transact-sql.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)  
  
  
