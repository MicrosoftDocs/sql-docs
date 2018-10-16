---
title: "Principals (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "01/09/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.roleproperties.selectroll.f1"
  - "sql13.swb.databaseuser.permissions.user.f1--May use common.permissions"
helpviewer_keywords: 
  - "certificates [SQL Server], principals"
  - "roles [SQL Server], principals"
  - "permissions [SQL Server], principals"
  - "##MS_SQLAuthenticatorCertificate##"
  - "principals [SQL Server]"
  - "##MS_SQLResourceSigningCertificate##"
  - "groups [SQL Server], principals"
  - "##MS_AgentSigningCertificate##"
  - "authentication [SQL Server], principals"
  - "schemas [SQL Server], principals"
  - "principals [SQL Server], about principals"
  - "security [SQL Server], principals"
  - "users [SQL Server], principals"
  - "##MS_SQLReplicationSigningCertificate##"
ms.assetid: 3f7adbf7-6e40-4396-a8ca-71cbb843b5c2
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Principals (Database Engine)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  *Principals* are entities that can request [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resources. Like other components of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] authorization model, principals can be arranged in a hierarchy. The scope of influence of a principal depends on the scope of the definition of the principal: Windows, server, database; and whether the principal is indivisible or a collection. A Windows Login is an example of an indivisible principal, and a Windows Group is an example of a principal that is a collection. Every principal has a security identifier (SID). This topic applies to all version of SQL Server, but there are some restictions on server-level principals in SQL Database or SQL Data Warehouse. 
  
## SQL Server-level principals  
  
- [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] authentication Login   
- Windows authentication login for a Windows user  
- Windows authentication login for a Windows group   
- Azure Active Directory authentication login for a AD user
- Azure Active Directory authentication login for a AD group
- Server Role  
  
## Database-level principals
  
- Database User (There are 11 types of users. For more information, see [CREATE USER](../../../t-sql/statements/create-user-transact-sql.md).)
- Database Role
- Application Role
  
## sa Login  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] `sa` log in is a server-level principal. By default, it is created when an instance is installed. Beginning in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], the default database of sa is master. This is a change of behavior from earlier versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The `sa` login is a member of the `sysadmin` fixed database role. The `sa` login has all permissions on the server and cannot be limited. The `sa` login cannot be dropped, but it can be disabled so that no one can use it.

## dbo User and dbo Schema

The `dbo` user is a special user principal in each database. All SQL Server administrators, members of the `sysadmin` fixed server role, `sa` login, and owners of the database, enter databases as the `dbo` user. The `dbo` user has all permissions in the database and cannot be limited or dropped. `dbo` stands for database owner, but the `dbo`user account is not the same as the `db_owner` fixed database role, and the `db_owner` fixed database role is not the same as the user account that is recorded as the owner of the database.     
The `dbo` user owns the `dbo` schema. The `dbo` schema is the default schema for all users, unless some other schema is specified.  The `dbo` schema cannot be dropped.
  
## public Server Role and Database Role  
Every login belongs to the `public` fixed server role, and every database user belongs to the `public` database role. When a login or user has not been granted or denied specific permissions on a securable, the login or user inherits the permissions granted to public on that securable. The `public` fixed server role and the `public` fixed database role cannot be dropped. However you can revoke permissions from the `public` roles. There are many permissions that are assigned to the `public` roles by default. Most of these permissions are needed for routine operations in the database; the type of things that everyone should be able to do. Be careful when revoking permissions from the public login or user, as it will affect all logins/users. Generally you should not deny permissions to public, because the deny statement overrides any grant statements you might make to individuals. 
  
## INFORMATION_SCHEMA and sys Users and Schemas 
 Every database includes two entities that appear as users in catalog views: `INFORMATION_SCHEMA` and `sys`. These entities are required for internal use by the Database Engine. They cannot be modified or dropped.  
  
## Certificate-based SQL Server Logins  
 Server principals with names enclosed by double hash marks (##) are for internal system use only. The following principals are created from certificates when [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is installed, and should not be deleted.  
  
-   \##MS_SQLResourceSigningCertificate##    
-   \##MS_SQLReplicationSigningCertificate##    
-   \##MS_SQLAuthenticatorCertificate##    
-   \##MS_AgentSigningCertificate##   
-   \##MS_PolicyEventProcessingLogin##   
-   \##MS_PolicySigningCertificate##   
-   \##MS_PolicyTsqlExecutionLogin##   
 
 These principal accounts do not have passwords that can be changed by administrators as they are based on certificates issued to Microsoft.
  
## The guest User  
 Each database includes a `guest`. Permissions granted to the `guest` user are inherited by users who have access to the database, but who do not have a user account in the database. The `guest` user cannot be dropped, but it can be disabled by revoking it's CONNECT permission. The CONNECT permission can be revoked by executing `REVOKE CONNECT FROM GUEST;` within any database other than `master` or `tempdb`.  
  
  
## Related Tasks  
 For information about designing a permissions system, see [Getting Started with Database Engine Permissions](../../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md).  
  
 The following topics are included in this section of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online:  
  
-   [Managing Logins, Users, and Schemas How-to Topics](../../../relational-databases/security/authentication-access/managing-logins-users-and-schemas-how-to-topics.md)  
  
-   [Server-Level Roles](../../../relational-databases/security/authentication-access/server-level-roles.md)  
  
-   [Database-Level Roles](../../../relational-databases/security/authentication-access/database-level-roles.md)  
  
-   [Application Roles](../../../relational-databases/security/authentication-access/application-roles.md)  
  
## See Also  
 [Securing SQL Server](../../../relational-databases/security/securing-sql-server.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)   
 [sys.server_principals &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)   
 [sys.sql_logins &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-sql-logins-transact-sql.md)   
 [sys.database_role_members &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-database-role-members-transact-sql.md)   
 [Server-Level Roles](../../../relational-databases/security/authentication-access/server-level-roles.md)   
 [Database-Level Roles](../../../relational-databases/security/authentication-access/database-level-roles.md)  
  
  
