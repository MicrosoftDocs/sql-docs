---
title: "ALTER SERVER ROLE (Transact-SQL)"
description: ALTER SERVER ROLE (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: 07/13/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER_SERVER_ROLE_TSQL"
  - "ALTER SERVER ROLE"
helpviewer_keywords:
  - "SERVER ROLE, ALTER"
  - "ALTER SERVER ROLE statement"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=azuresqldb-current"
---
# ALTER SERVER ROLE (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-pdw.md)]

Changes the membership of a server role or changes name of a user-defined server role. Fixed server roles cannot be renamed.  
  
:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax
  
```syntaxsql
-- Syntax for SQL Server, Azure SQL Database, Azure SQL Managed Instance
  
ALTER SERVER ROLE server_role_name   
{  
    [ ADD MEMBER server_principal ]  
  | [ DROP MEMBER server_principal ]  
  | [ WITH NAME = new_server_role_name ]  
} [ ; ]  
```  
  
```syntaxsql
-- Syntax for Parallel Data Warehouse  
  
ALTER SERVER ROLE  server_role_name  ADD MEMBER login;  
  
ALTER SERVER ROLE  server_role_name  DROP MEMBER login;  
```  
  
## Arguments

#### *server_role_name*
Is the name of the server role to be changed.  
  
#### ADD MEMBER *server_principal*
Adds the specified server principal to the server role. *server_principal* can be a login or a user-defined server role. *server_principal* cannot be a fixed server role, a database role, or sa.  
  
#### DROP MEMBER *server_principal*
Removes the specified server principal from the server role. *server_principal* can be a login or a user-defined server role. *server_principal* cannot be a fixed server role, a database role, or sa.  
  
#### WITH NAME =_new_server_role_name_
Specifies the new name of the user-defined server role. This name cannot already exist in the server.  
  
## Remarks
Changing the name of a user-defined server role does not change ID number, owner, or permissions of the role.  
  
For changing role membership, `ALTER SERVER ROLE` replaces sp_addsrvrolemember and sp_dropsrvrolemember. These stored procedures are deprecated.  
  
You can view server roles by querying the `sys.server_role_members` and `sys.server_principals` catalog views.  
  
To change the owner of a user-defined server role, use [ALTER AUTHORIZATION (Transact-SQL)](../../t-sql/statements/alter-authorization-transact-sql.md).  

In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], `ALTER SERVER ROLE` must be run in the `master` database.
  
## Permissions
Requires `ALTER ANY SERVER ROLE` permission on the server to change the name of a user-defined server role.  
  
**Fixed server roles**  
  
To add a member to a fixed server role, you must be a member of that fixed server role, or be a member of the `sysadmin` fixed server role.  
  
> [!NOTE]  
>  The `CONTROL SERVER` and `ALTER ANY SERVER ROLE` permissions are not sufficient to execute `ALTER SERVER ROLE` for a fixed server role, and `ALTER` permission cannot be granted on a fixed server role.  
  
**User-defined server roles**  
  
To add a member to a user-defined server role, you must be a member of the `sysadmin` fixed server role or have `CONTROL SERVER` or `ALTER ANY SERVER ROLE` permission. Or you must have `ALTER` permission on that role.  
  
> [!NOTE]  
>  Unlike fixed server roles, members of a user-defined server role do not inherently have permission to add members to that same role.  
  
## Examples
  
### A. Change the name of a server role
The following example creates a server role named `Product`, and then changes the name of server role to `Production`.  
  
```sql
CREATE SERVER ROLE Product ;  
ALTER SERVER ROLE Product WITH NAME = Production ;  
GO  
```  
  
### B. Add a domain account to a server role
The following example adds a domain account named `adventure-works\roberto0` to the user-defined server role named `Production`.  
  
```sql
ALTER SERVER ROLE Production ADD MEMBER [adventure-works\roberto0] ;  
```  
  
### C. Add a SQL Server login to a server role
The following example adds a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login named `Ted` to the `diskadmin` fixed server role.  
  
```sql
ALTER SERVER ROLE diskadmin ADD MEMBER Ted ;  
GO  
```  
  
### D. Remove a domain account from a server role
The following example removes a domain account named `adventure-works\roberto0` from the user-defined server role named `Production`.  
  
```sql
ALTER SERVER ROLE Production DROP MEMBER [adventure-works\roberto0] ;  
```  
  
### E. Remove a SQL Server login from a server role
The following example removes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `Ted` from the `diskadmin` fixed server role.  
  
```sql
ALTER SERVER ROLE Production DROP MEMBER Ted ;  
GO  
```  
  
### F. Grant a login the permission to add logins to a user-defined server role
The following example allows `Ted` to add other logins to the user-defined server role named `Production`.  
  
```sql
GRANT ALTER ON SERVER ROLE::Production TO Ted ;  
GO  
```  
  
### G. View role membership
To view role membership, use the **Server Role (Members)** page in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or execute the following query:  
  
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
  
## Examples: [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]
  
### H. Add a member to a server role
The following example adds the login `Anna` to the `LargeRC` server role.  
  
```sql
ALTER SERVER ROLE LargeRC ADD MEMBER Anna;  
```  
  
### I. Remove a login from a resource class
The following example drops Anna's membership in the `LargeRC` server role.  
  
```sql
ALTER SERVER ROLE LargeRC DROP MEMBER Anna;  
```  
  
## Next steps

- [CREATE SERVER ROLE (Transact-SQL)](../../t-sql/statements/create-server-role-transact-sql.md)
- [DROP SERVER ROLE (Transact-SQL)](../../t-sql/statements/drop-server-role-transact-sql.md)
- [CREATE ROLE (Transact-SQL)](../../t-sql/statements/create-role-transact-sql.md)
- [ALTER ROLE (Transact-SQL)](../../t-sql/statements/alter-role-transact-sql.md)
- [DROP ROLE (Transact-SQL)](../../t-sql/statements/drop-role-transact-sql.md)
- [Security Stored Procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)
- [Security Functions (Transact-SQL)](../../t-sql/functions/security-functions-transact-sql.md)
- [Principals (Database Engine)](../../relational-databases/security/authentication-access/principals-database-engine.md)
- [sys.server_role_members (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-role-members-transact-sql.md)
- [sys.server_principals (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)