---
title: "Server-level roles | Microsoft Docs"
description: SQL Server provides server-level roles. These security principals group other principals to manage the server-wide permissions.
ms.custom:
- event-tier1-build-2022
ms.date: "07/25/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
f1_keywords: 
  - "sql13.Security.NT_AUTHORITY.SYSTEM"
  - "sql13.Security.BUILTIN.administrators"
helpviewer_keywords: 
  - "roles [SQL Server], server-level"
  - "principals [SQL Server], server-level"
  - "CONTROL SERVER permission"
  - "fixed server roles [SQL Server]"
  - "credentials [SQL Server], roles"
  - "sysadmin fixed server role"
  - "server-level roles [SQL Server]"
  - "authentication [SQL Server], roles"
ms.assetid: 7adf2ad7-015d-4cbe-9e29-abaefd779008
author: VanMSFT
ms.author: vanto
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Server-level roles
[!INCLUDE[SQL Server Azure SQL Managed Instance Parallel Data Warehouse](../../../includes/applies-to-version/sql-asdbmi-pdw.md)]

  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] provides server-level roles to help you manage the permissions on a server. These roles are security principals that group other principals. Server-level roles are server-wide in their permissions scope. (*Roles* are like *groups* in the Windows operating system.)
  
SQL Server 2019 and previous versions provided nine fixed server roles. The permissions that are granted to the fixed server roles (except **public**) can't be changed. Beginning with [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], you can create user-defined server roles and add server-level permissions to the user-defined server roles.
[!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] comes with 10 additional server roles that have been designed specifically with the [*Principle of Least Privilege*](https://techcommunity.microsoft.com/t5/azure-sql-blog/security-the-principle-of-least-privilege-polp/ba-p/2067390) in mind, which have the prefix `##MS_` and the suffix `##` to distinguish them from other regular user-created principals and custom server roles. Those new roles contain privileges that apply on server scope but also can inherit down to individual databases (with the exception of the **##MS_LoginManager##** server role.)
  
Like SQL Server on-premises, server permissions are organized hierarchically. The permissions that are held by these server-level roles can propagate to database permissions. For the permissions to be effectively useful at the database level, a login needs to either be a member of the server-level role **##MS_DatabaseConnector##** (starting with [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)]), which grants the **CONNECT** permission to all databases, or have a user account in individual databases. This also applies to the `master` database.
Consider the following example: The server-level role **##MS_ServerStateReader##** holds the permission **VIEW SERVER STATE**. A login who is member of this role has a user account in the databases, `master` and `WideWorldImporters`. This user will then also have the permission, **VIEW DATABASE STATE** in those two databases by inheritance.

 You can add server-level principals ([!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] logins, Windows accounts, and Windows groups) into server-level roles. Each member of a fixed server role can add other logins to that same role. Members of user-defined server roles can't add other server principals to the role.
  
## Fixed server-level roles  

> [!NOTE]
> These server-level roles introduced prior to [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] are not available in Azure SQL Database or Azure Synapse Analytics. There are special [Azure SQL Database server roles for permission management](/azure/azure-sql/database/security-server-roles) that are equivalent to the server-level roles introduced in [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)]. For more information about SQL Database, see [Controlling and granting database access.](/azure/sql-database/sql-database-manage-logins).

The following table shows the fixed server-level roles and their capabilities.  

|Fixed server-level role |Description |
|------------------------------|-----------------|  
|**sysadmin**|Members of the **sysadmin** fixed server role can perform any activity in the server.|
|**serveradmin**|Members of the **serveradmin** fixed server role can change server-wide configuration options and shut down the server.|
|**securityadmin**|Members of the **securityadmin** fixed server role manage logins and their properties. They can `GRANT`, `DENY`, and `REVOKE` server-level permissions. They can also `GRANT`, `DENY`, and `REVOKE` database-level permissions if they have access to a database. Additionally, they can reset passwords for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] logins.<br /><br /> **IMPORTANT:** The ability to grant access to the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] and to configure user permissions allows the security admin to assign most server permissions. The **securityadmin** role should be treated as equivalent to the **sysadmin** role. As an alternative, starting with [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)], consider using the new fixed server role **##MS_LoginManager##**.|
|**processadmin**|Members of the **processadmin** fixed server role can end processes that are running in an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].|
|**setupadmin**|Members of the **setupadmin** fixed server role can add and remove linked servers by using [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements. (**sysadmin** membership is needed when using [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)].)|
|**bulkadmin**|Members of the **bulkadmin** fixed server role can run the `BULK INSERT` statement.<br /><br /> The **bulkadmin** role or ADMINISTER BULK OPERATIONS permissions isn't supported for SQL Server on Linux. Only the **sysadmin** can perform bulk inserts for SQL Server on Linux. |
|**diskadmin**|The **diskadmin** fixed server role is used for managing disk files.|
|**dbcreator**|Members of the **dbcreator** fixed server role can create, alter, drop, and restore any database.|
|**public**|Every [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login belongs to the **public** server role. When a server principal hasn't been granted or denied specific permissions on a securable object, the user inherits the permissions granted to **public** on that object. Only assign public permissions on any object when you want the object to be available to all users. You can't change membership in public.<br /><br /> **Note:** **public** is implemented differently than other roles, and permissions can be granted, denied, or revoked from the public fixed server roles.|  
  
> [!IMPORTANT] 
> Most of the permissions provided by the following server roles are not applicable to Azure Synapse Analytics - **processadmin**, **serveradmin**, **setupadmin**, and **diskadmin**.
  
## Fixed server-level roles introduced in SQL Server 2022

The following table shows additional fixed server-level roles that are introduced with [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] and their capabilities.

> [!NOTE]
> These server-level permissions are not available for Azure SQL Managed Instance or Azure Synapse Analytics. **##MS_PerformanceDefinitionReader##**, **##MS_ServerPerformanceStateReader##**, and **##MS_ServerSecurityStateReader##** is introduced in [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)], and are not available in Azure SQL Database.

|Fixed server-level role |Description |
|------------------------------|-----------------|  
|**##MS_DatabaseConnector##**|Members of the **##MS_DatabaseConnector##** fixed server role can connect to any database without requiring a User-account in the database to connect to. <br /><br />To deny the **CONNECT** permission to a specific database, users can create a matching user account for this login in the database and then **DENY** the **CONNECT** permission to the database-user. This **DENY** permission will overrule the **GRANT CONNECT** permission coming from this role.|
|**##MS_LoginManager##**|Members of the **##MS_LoginManager##** fixed server role can create, delete and modify logins. Contrary to the old fixed server role **securityadmin**, this role does not allow members to `GRANT` privileges. It is a more limited role that helps to comply with the *Principle of least Privilege*.|
|**##MS_DatabaseManager##**|Members of the **##MS_DatabaseManager##** fixed server role can create and delete databases. A member of the **##MS_DatabaseManager##** role that creates a database, becomes the owner of that database, which allows that user to connect to that database as the `dbo` user. The `dbo` user has all database permissions in the database. Members of the **##MS_DatabaseManager##** role don't necessarily have permission to access databases that they don't own.|
|**##MS_ServerStateManager##**|Members of the **##MS_ServerStateManager##** fixed server role have the same permissions as the **##MS_ServerStateReader##** role. Also, it holds the **ALTER SERVER STATE** permission, which allows access to several management operations, such as: `DBCC FREEPROCCACHE`, `DBCC FREESYSTEMCACHE ('ALL')`, `DBCC SQLPERF()`|
|**##MS_ServerStateReader##**|Members of the **##MS_ServerStateReader##** fixed server role can read all dynamic management views (DMVs) and functions that are covered by **VIEW SERVER STATE**, and respectively has **VIEW DATABASE STATE** permission on any database on which the member of this role has a user account.|
|**##MS_ServerPerformanceStateReader##**|Members of the **##MS_ServerPerformanceStateReader##** fixed server role can read all dynamic management views (DMVs) and functions that are covered by **VIEW SERVER PERFORMANCE STATE**, and respectively has **VIEW DATABASE PERFORMANCE STATE** permission on any database on which the member of this role has a user account. This is a subset of what the **##MS_ServerStateReader##** server role has access to which helps to comply with the *Principle of least Privilege*.|
|**##MS_ServerSecurityStateReader##**|Members of the **##MS_ServerSecurityStateReader##** fixed server role can read all dynamic management views (DMVs) and functions that are covered by **VIEW SERVER SECURITY STATE**, and respectively has **VIEW DATABASE SECURITY STATE** permission on any database on which the member of this role has a user account. This is a small subset of what the **##MS_ServerStateReader##** server role has access to, which helps to comply with the *Principle of least Privilege*.|
|**##MS_DefinitionReader##**|Members of the **##MS_DefinitionReader##** fixed server role can read all catalog views that are covered by **VIEW ANY DEFINITION**, and respectively has **VIEW DEFINITION** permission on any database on which the member of this role has a user account.|
|**##MS_PerformanceDefinitionReader##**|Members of the **##MS_PerformanceDefinitionReader##** fixed server role can read all catalog views that are covered by **VIEW ANY PERFORMANCE DEFINITION**, and respectively has **VIEW PERFORMANCE DEFINITION** permission on any database on which the member of this role has a user account. This is a subset of what the **##MS_DefinitionReader##** server role has access to.|
|**##MS_SecurityDefinitionReader##**|Members of the **##MS_SecurityDefinitionReader##** fixed server role can read all catalog views that are covered by **VIEW ANY SECURITY DEFINITION**, and respectively has **VIEW SECURITY DEFINITION** permission on any database on which the member of this role has a user account. This is a small subset of what the **##MS_DefinitionReader##** server role has access to which helps to comply with the *Principle of least Privilege*.|

## Permissions of fixed server roles  
 Each fixed server role has certain permissions assigned to it.

### Permissions of new fixed server roles in SQL Server 2022

Each fixed server-level role has certain permissions assigned to it. The following table shows the permissions assigned to the server-level roles. It also shows the  database-level permissions which are inherited as long as the user can connect to individual databases.

|Fixed server-level role  | Server-level permissions | Database-level permissions |
|---|---|---|
|**##MS_DatabaseConnector##**|CONNECT ANY DATABASE |CONNECT |
|**##MS_LoginManager##**|CREATE LOGIN<br />ALTER ANY LOGIN | N/A |
|**##MS_DatabaseManager##**|CREATE ANY DATABASE<br />ALTER ANY DATABASE | ALTER|
|**##MS_ServerStateManager##**| ALTER SERVER STATE<br />VIEW SERVER STATE<br />VIEW SERVER PERFORMANCE STATE<br />VIEW SERVER SECURITY STATE | VIEW DATABASE STATE<br />VIEW DATABASE PERFORMANCE STATE<br />VIEW DATABASE SECURITY STATE |
|**##MS_ServerStateReader##**|VIEW SERVER STATE<br />VIEW SERVER PERFORMANCE STATE<br />VIEW SERVER SECURITY STATE | VIEW DATABASE STATE<br />VIEW DATABASE PERFORMANCE STATE<br />VIEW DATABASE SECURITY STATE |
|**##MS_ServerPerformanceStateReader##**|VIEW SERVER PERFORMANCE STATE|VIEW DATABASE PERFORMANCE STATE |
|**##MS_ServerSecurityStateReader##**|VIEW SERVER SECURITY STATE|VIEW DATABASE SECURITY STATE|
|**##MS_DefinitionReader##**|VIEW ANY DATABASE<br />VIEW ANY DEFINITION<br />VIEW ANY PERFORMANCE DEFINITION<br />VIEW ANY SECURITY DEFINITION| VIEW DEFINITION<br />VIEW PERFORMANCE DEFINITION<br />VIEW SECURITY DEFINITION|
|**##MS_PerformanceDefinitionReader##**|VIEW ANY PERFORMANCE DEFINITION|VIEW PERFORMANCE DEFINITION |
|**##MS_SecurityDefinitionReader##**| VIEW ANY SECURITY DEFINITION | VIEW SECURITY DEFINITION |


### Permissions of server roles for SQL Server 2019 and earlier

The following graphic shows the permissions assigned to the legacy server roles (SQL Server 2019 and earlier versions).   
![fixed_server_role_permissions](../../../relational-databases/security/authentication-access/media/permissions-of-server-roles.png)   
  
> [!IMPORTANT]  
>  The **CONTROL SERVER** permission is similar but not identical to the **sysadmin** fixed server role. Permissions do not imply role memberships and role memberships do not grant permissions. (E.g. **CONTROL SERVER** does not imply membership in the **sysadmin** fixed server role.) However, it is sometimes possible to impersonate between roles and equivalent permissions. Most **DBCC** commands and many system procedures require membership in the **sysadmin** fixed server role. For a list of 171 system stored procedures that require **sysadmin** membership, see the following blog post by Andreas Wolter [CONTROL SERVER vs. sysadmin/sa: permissions, system procedures, DBCC, automatic schema creation and privilege escalation - caveats](http://andreas-wolter.com/en/control-server-vs-sysadmin-sa/). 

## Server-level permissions  
 Only server-level permissions can be added to user-defined server roles. To list the server-level permissions, execute the following statement. The server-level permissions are:  
  
```  
SELECT * FROM sys.fn_builtin_permissions('SERVER') ORDER BY permission_name;  
```  
  
 For more information about permissions, see [Permissions &#40;Database Engine&#41;](../../../relational-databases/security/permissions-database-engine.md) and [sys.fn_builtin_permissions &#40;Transact-SQL&#41;](../../../relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql.md).  
  
## Working with server-level roles  
 The following table explains the commands, views, and functions that you can use to work with server-level roles.  
  
|Feature|Type|Description|  
|-------------|----------|-----------------|  
|[sp_helpsrvrole &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-helpsrvrole-transact-sql.md)|Metadata|Returns a list of server-level roles.|  
|[sp_helpsrvrolemember &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-helpsrvrolemember-transact-sql.md)|Metadata|Returns information about the members of a server-level role.|  
|[sp_srvrolepermission &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-srvrolepermission-transact-sql.md)|Metadata|Displays the permissions of a server-level role.|  
|[IS_SRVROLEMEMBER &#40;Transact-SQL&#41;](../../../t-sql/functions/is-srvrolemember-transact-sql.md)|Metadata|Indicates whether a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login is a member of the specified server-level role.|  
|[sys.server_role_members &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-server-role-members-transact-sql.md)|Metadata|Returns one row for each member of each server-level role.|  
|[CREATE SERVER ROLE &#40;Transact-SQL&#41;](../../../t-sql/statements/create-server-role-transact-sql.md)|Command|Creates a user-defined server role.|  
|[ALTER SERVER ROLE &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-server-role-transact-sql.md)|Command|Changes the membership of a server role or changes name of a user-defined server role.|  
|[DROP SERVER ROLE &#40;Transact-SQL&#41;](../../../t-sql/statements/drop-server-role-transact-sql.md)|Command|Removes a user-defined server role.|  
|[sp_addsrvrolemember &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addsrvrolemember-transact-sql.md)|Command|Adds a login as a member of a server-level role. Deprecated. Use [ALTER SERVER ROLE](../../../t-sql/statements/alter-server-role-transact-sql.md) instead.|  
|[sp_dropsrvrolemember &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-dropsrvrolemember-transact-sql.md)|Command|Removes a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login or a Windows user or group from a server-level role. Deprecated. Use [ALTER SERVER ROLE](../../../t-sql/statements/alter-server-role-transact-sql.md) instead.|  
  
## See also  
 [Database-Level Roles](../../../relational-databases/security/authentication-access/database-level-roles.md)   
 [Security Catalog Views &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Security Functions &#40;Transact-SQL&#41;](../../../t-sql/functions/security-functions-transact-sql.md)   
 [Securing SQL Server](../../../relational-databases/security/securing-sql-server.md)   
 [GRANT Server Principal Permissions &#40;Transact-SQL&#41;](../../../t-sql/statements/grant-server-principal-permissions-transact-sql.md)   
 [REVOKE Server Principal Permissions &#40;Transact-SQL&#41;](../../../t-sql/statements/revoke-server-principal-permissions-transact-sql.md)   
 [DENY Server Principal Permissions &#40;Transact-SQL&#41;](../../../t-sql/statements/deny-server-principal-permissions-transact-sql.md)   
 [Create a Server Role](../../../relational-databases/security/authentication-access/create-a-server-role.md)  
 [Azure SQL Database server roles for permission management](/azure/azure-sql/database/security-server-roles) 
