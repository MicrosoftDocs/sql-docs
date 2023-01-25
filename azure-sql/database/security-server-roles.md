---
title: Server roles
titleSuffix: Azure SQL Database
description: This article provides an overview of server roles for the logical server of Azure SQL Database
author: AndreasWolter
ms.author: anwolter
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 07/12/2022
ms.service: sql-database
ms.subservice: security
ms.topic: conceptual
---

# Azure SQL Database server roles for permission management

> [!NOTE]
> The fixed server-level roles in this article are in public preview for Azure SQL Database. These server-level roles are also part of the release for [SQL Server 2022](/sql/relational-databases/security/authentication-access/server-level-roles#fixed-server-level-roles-introduced-in-sql-server-2022).

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

In Azure SQL Database, the server is a logical concept and permissions can't be granted on a server level. To simplify permission management, Azure SQL Database provides a set of fixed server-level roles to help you manage the permissions on a [logical server](logical-servers.md). Roles are security principals that group logins.

> [!NOTE]
> The *roles* concept in this article are like *groups* in the Windows operating system.

These special fixed server-level roles use the prefix **##MS_** and the suffix **##** to distinguish from other regular user-created principals.

Like SQL Server on-premises, server permissions are organized hierarchically. The permissions that are held by these server-level roles can propagate to database permissions. For the permissions to be effectively useful at the database level, a login needs to either be a member of the server-level role **##MS_DatabaseConnector##**, which grants **CONNECT** to all databases, or have a user account in individual databases. This also applies to the virtual `master` database.

For example, the server-level role **##MS_ServerStateReader##** holds the permission **VIEW SERVER STATE**. If a login who is member of this role has a user account in the databases `master` and `WideWorldImporters`, this user will have the permission, **VIEW DATABASE STATE** in those two databases.

> [!NOTE]
> Any permission can be denied within user databases, in effect, overriding the server-wide grant via role membership. However, in the system database *master*, permissions cannot be granted or denied.

Azure SQL Database currently provides 7 fixed server roles. The permissions that are granted to the fixed server roles can't be changed and these roles can't have other fixed roles as members. You can add server-level logins as members to server-level roles.

> [!IMPORTANT]
> Each member of a fixed server role can add other logins to that same role.

For more information on Azure SQL Database logins and users, see [Authorize database access to SQL Database, SQL Managed Instance, and Azure Synapse Analytics](logins-create-manage.md).
  
## Fixed server-level roles

The following table shows the fixed server-level roles and their capabilities.  
  
|Fixed server-level role|Description|  
|------------------------------|-----------------|  
|**##MS_DatabaseConnector##**|Members of the **##MS_DatabaseConnector##** fixed server role can connect to any database without requiring a User-account in the database to connect to. <br /><br />To deny the **CONNECT** permission to a specific database, users can create a matching user account for this login in the database and then **DENY** the **CONNECT** permission to the database-user. This **DENY** permission will overrule the **GRANT CONNECT** permission coming from this role.|
|**##MS_DatabaseManager##**|Members of the **##MS_DatabaseManager##** fixed server role can create and delete databases. A member of the **##MS_DatabaseManager##** role that creates a database, becomes the owner of that database, which allows that user to connect to that database as the `dbo` user. The `dbo` user has all database permissions in the database. Members of the **##MS_DatabaseManager##** role don't necessarily have permission to access databases that they don't own. It's recommended to use this server role over the **dbmanager** database level role that exists in `master`.|
|**##MS_DefinitionReader##**|Members of the **##MS_DefinitionReader##** fixed server role can read all catalog views that are covered by **VIEW ANY DEFINITION**, respectively **VIEW DEFINITION** on any database on which the member of this role has a user account.|  
|**##MS_LoginManager##**|Members of the **##MS_LoginManager##** fixed server role can create and delete logins. It's recommended to use this server role over the **loginmanager** database level role that exists in `master`.|
|**##MS_SecurityDefinitionReader##**|Members of the **##MS_SecurityDefinitionReader##** fixed server role can read all catalog views that are covered by **VIEW ANY SECURITY DEFINITION**, and respectively has **VIEW SECURITY DEFINITION** permission on any database on which the member of this role has a user account. This is a small subset of what the **##MS_DefinitionReader##** server role has access to.|
|**##MS_ServerStateReader##**|Members of the **##MS_ServerStateReader##** fixed server role can read all dynamic management views (DMVs) and functions that are covered by **VIEW SERVER STATE**, respectively **VIEW DATABASE STATE** on any database on which the member of this role has a user account.|
|**##MS_ServerStateManager##**|Members of the **##MS_ServerStateManager##** fixed server role have the same permissions as the **##MS_ServerStateReader##** role. Also, it holds the **ALTER SERVER STATE** permission, which allows access to several management operations, such as: `DBCC FREEPROCCACHE`, `DBCC FREESYSTEMCACHE ('ALL')`, `DBCC SQLPERF()`; |  

## Permissions of fixed server roles

Each fixed server-level role has certain permissions assigned to it. The following table shows the permissions assigned to the server-level roles. It also shows the database-level permissions which are inherited as long as the user can connect to individual databases.
  
|Fixed server-level role|Server-level permissions|Database-level permissions (if a database user matching the login exists)  
|-------------|----------|-----------------|  
|**##MS_DatabaseConnector##**|CONNECT ANY DATABASE |CONNECT |
|**##MS_DatabaseManager##**|CREATE ANY DATABASE<br />ALTER ANY DATABASE | ALTER|
|**##MS_DefinitionReader##**|VIEW ANY DATABASE, VIEW ANY DEFINITION, VIEW ANY SECURITY DEFINITION|VIEW DEFINITION, VIEW SECURITY DEFINITION| 
|**##MS_LoginManager##**|CREATE LOGIN<br />ALTER ANY LOGIN | N/A |
|**##MS_SecurityDefinitionReader##**| VIEW ANY SECURITY DEFINITION | VIEW SECURITY DEFINITION | 
|**##MS_ServerStateReader##**|VIEW SERVER STATE, VIEW SERVER PERFORMANCE STATE, VIEW SERVER SECURITY STATE|VIEW DATABASE STATE, VIEW DATABASE PERFORMANCE STATE, VIEW DATABASE SECURITY STATE|  
|**##MS_ServerStateManager##**|ALTER SERVER STATE, VIEW SERVER STATE, VIEW SERVER PERFORMANCE STATE, VIEW SERVER SECURITY STATE|VIEW DATABASE STATE, VIEW DATABASE PERFORMANCE STATE, VIEW DATABASE SECURITY STATE|   
  
  
## Working with server-level roles

The following table explains the system views, and functions that you can use to work with server-level roles in Azure SQL Database.  
  
|Feature|Type|Description|  
|-------------|----------|-----------------|  
|[IS_SRVROLEMEMBER &#40;Transact-SQL&#41;](/sql/t-sql/functions/is-srvrolemember-transact-sql)|Metadata|Indicates whether a SQL login is a member of the specified server-level role.|  
|[sys.server_role_members &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-server-role-members-transact-sql)|Metadata|Returns one row for each member of each server-level role.|
|[sys.sql_logins &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-sql-logins-transact-sql)|Metadata|Returns one row for each SQL login.|
|[ALTER SERVER ROLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-server-role-transact-sql)|Command|Changes the membership of a server role.| 

## <a name="_examples"></a> Examples

The examples in this section show how to work with server-level roles in Azure SQL Database.  

### A. Adding a SQL login to a server-level role

The following example adds the SQL login 'Jiao' to the server-level role ##MS_ServerStateReader##. This statement has to be run in the virtual `master` database.
  
```sql  
ALTER SERVER ROLE ##MS_ServerStateReader##
	ADD MEMBER Jiao;  
GO
```  

### B. Listing all principals (SQL authentication) which are members of a server-level role

The following statement returns all members of any fixed server-level role using the `sys.server_role_members` and `sys.sql_logins` catalog views. This statement has to be run in the virtual `master` database.
  
```sql  
SELECT
		sql_logins.principal_id			AS MemberPrincipalID
	,	sql_logins.name					AS MemberPrincipalName
	,	roles.principal_id				AS RolePrincipalID
	,	roles.name						AS RolePrincipalName
FROM sys.server_role_members AS server_role_members
INNER JOIN sys.server_principals AS roles
    ON server_role_members.role_principal_id = roles.principal_id
INNER JOIN sys.sql_logins AS sql_logins 
    ON server_role_members.member_principal_id = sql_logins.principal_id
;  
GO  
```

### C. Complete example: Adding a login to a server-level role, retrieving metadata for role membership and permissions, and running a test query

#### Part 1: Preparing role membership and user account

Run this command from the virtual `master` database.

```sql  
ALTER SERVER ROLE ##MS_ServerStateReader##
	ADD MEMBER Jiao

-- check membership in metadata:
select IS_SRVROLEMEMBER('##MS_ServerStateReader##', 'Jiao')
--> 1 = Yes

SELECT
		sql_logins.principal_id			AS MemberPrincipalID
	,	sql_logins.name					AS MemberPrincipalName
	,	roles.principal_id				AS RolePrincipalID
	,	roles.name						AS RolePrincipalName
FROM sys.server_role_members AS server_role_members
INNER JOIN sys.server_principals AS roles
    ON server_role_members.role_principal_id = roles.principal_id
INNER JOIN sys.sql_logins AS sql_logins 
    ON server_role_members.member_principal_id = sql_logins.principal_id
;   
GO  
``` 

Here's the result set.
  
```
MemberPrincipalID MemberPrincipalName RolePrincipalID RolePrincipalName        
------------- ------------- ------------------ -----------   
6         Jiao      11            ##MS_ServerStateReader##   
```  

Run this command from a user database.

```sql  
-- Creating a database-User for 'Jiao'
CREATE USER Jiao
	FROM LOGIN Jiao
;   
GO  
``` 

#### Part 2: Testing role membership

Log in as login `Jiao` and connect to the user database used in the example.

```sql  
-- retrieve server-level permissions of currently logged on User
SELECT * FROM sys.fn_my_permissions(NULL, 'Server')
;  

-- check server-role membership for `##MS_ServerStateReader##` of currently logged on User
SELECT USER_NAME(), IS_SRVROLEMEMBER('##MS_ServerStateReader##')
--> 1 = Yes

-- Does the currently logged in User have the `VIEW DATABASE STATE`-permission?
SELECT HAS_PERMS_BY_NAME(NULL, 'DATABASE', 'VIEW DATABASE STATE'); 
--> 1 = Yes

-- retrieve database-level permissions of currently logged on User
SELECT * FROM sys.fn_my_permissions(NULL, 'DATABASE')
GO 

-- example query:
SELECT * FROM sys.dm_exec_query_stats
--> will return data since this user has the necessary permission

``` 

### D. Check server-level roles for Azure AD logins

Run this command in the virtual `master` database to see all Azure AD logins that are part of server-level roles in SQL Database. For more information on Azure AD server logins, see [Azure Active Directory server principals](authentication-azure-ad-logins.md).

```sql
SELECT
		member.principal_id			AS MemberPrincipalID
	,	member.name					AS MemberPrincipalName
	,	roles.principal_id			AS RolePrincipalID
	,	roles.name					AS RolePrincipalName
FROM sys.server_role_members AS server_role_members
INNER JOIN sys.server_principals	AS roles
    ON server_role_members.role_principal_id = roles.principal_id
INNER JOIN sys.server_principals	AS member 
    ON server_role_members.member_principal_id = member.principal_id
LEFT OUTER JOIN sys.sql_logins		AS sql_logins 
    ON server_role_members.member_principal_id = sql_logins.principal_id
WHERE member.principal_id NOT IN (-- prevent SQL Logins from interfering with resultset
	SELECT principal_id FROM sys.sql_logins AS sql_logins
		WHERE member.principal_id = sql_logins.principal_id)
; 
```

### E. Check the virtual master database roles for specific logins

Run this command in the virtual `master` database to check with roles `bob` has, or change the value to match your principal.

```sql
SELECT DR1.name AS DbRoleName, isnull (DR2.name, 'No members')  AS DbUserName
   FROM sys.database_role_members AS DbRMem RIGHT OUTER JOIN sys.database_principals AS DR1
    ON DbRMem.role_principal_id = DR1.principal_id LEFT OUTER JOIN sys.database_principals AS DR2
     ON DbRMem.member_principal_id = DR2.principal_id
      WHERE DR1.type = 'R' and DR2.name like 'bob%'
```

## Limitations of server-level roles

- Role assignments may take up to 5 minutes to become effective. Also for existing sessions, changes to server role assignments don't take effect until the connection is closed and reopened. This is due to the distributed architecture between the `master` database and other databases on the same logical server.
  - Partial workaround: to reduce the waiting period and ensure that server role assignments are current in a database, a server administrator, or an Azure AD administrator can run `DBCC FLUSHAUTHCACHE` in the user database(s) on which the login has access. Current logged on users still have to reconnect after running `DBCC FLUSHAUTHCACHE` for the membership changes to take effect on them.

- `IS_SRVROLEMEMBER()` isn't supported in the `master` database.


## See also

- [Database-Level Roles](/sql/relational-databases/security/authentication-access/database-level-roles)   
- [Security Catalog Views &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/security-catalog-views-transact-sql)   
- [Security Functions &#40;Transact-SQL&#41;](/sql/t-sql/functions/security-functions-transact-sql)   
- [Permissions &#40;Database Engine&#41;](/sql/relational-databases/security/permissions-database-engine)
- [DBCC FLUSHAUTHCACHE (Transact-SQL)](/sql/t-sql/database-console-commands/dbcc-flushauthcache-transact-sql)
