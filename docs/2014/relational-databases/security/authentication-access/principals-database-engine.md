---
title: "Principals (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.roleproperties.selectroll.f1"
  - "sql12.swb.databaseuser.permissions.user.f1--May use common.permissions"
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
---
# Principals (Database Engine)
  *Principals* are entities that can request [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resources. Like other components of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] authorization model, principals can be arranged in a hierarchy. The scope of influence of a principal depends on the scope of the definition of the principal: Windows, server, database; and whether the principal is indivisible or a collection. A Windows Login is an example of an indivisible principal, and a Windows Group is an example of a principal that is a collection. Every principal has a security identifier (SID).  
  
 **Windows-level principals**  
  
-   Windows Domain Login  
  
-   Windows Local Login  
  
 **SQL Server**-**level** **principals**  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Login  
  
-   Server Role  
  
 **Database-level principals**  
  
-   Database User  
  
-   Database Role  
  
-   Application Role  
  
## The SQL Server sa Login  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] sa log in is a server-level principal. By default, it is created when an instance is installed. Beginning in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], the default database of sa is master. This is a change of behavior from earlier versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## public Database Role  
 Every database user belongs to the public database role. When a user has not been granted or denied specific permissions on a securable, the user inherits the permissions granted to public on that securable.  
  
## INFORMATION_SCHEMA and sys  
 Every database includes two entities that appear as users in catalog views: INFORMATION_SCHEMA and sys. These entities are required by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. They are not principals, and they cannot be modified or dropped.  
  
## Certificate-based SQL Server Logins  
 Server principals with names enclosed by double hash marks (##) are for internal system use only. The following principals are created from certificates when [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is installed, and should not be deleted.  
  
-   \##MS_SQLResourceSigningCertificate##  
  
-   \##MS_SQLReplicationSigningCertificate##  
  
-   \##MS_SQLAuthenticatorCertificate##  
  
-   \##MS_AgentSigningCertificate##  
  
-   \##MS_PolicyEventProcessingLogin##  
  
-   \##MS_PolicySigningCertificate##  
  
-   \##MS_PolicyTsqlExecutionLogin##  
  
## The guest User  
 Each database includes a **guest**. Permissions granted to the **guest** user are inherited by users who have access to the database, but who do not have a user account in the database. The **guest** user cannot be dropped, but it can be disabled by revoking it's `CONNECT` permission. The `CONNECT` permission can be revoked by executing `REVOKE CONNECT FROM GUEST` within any database other than master or tempdb.  
  
## Client and Database Server  
 By definition, a client and a database server are security principals and can be secured. These entities can be mutually authenticated before a secure network connection is established. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports the [Kerberos](https://go.microsoft.com/fwlink/?LinkId=100758) authentication protocol, which defines how clients interact with a network authentication service.  
  
## Related Tasks  
 The following topics are included in this section of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online:  
  
-   [Managing Logins, Users, and Schemas How-to Topics](managing-logins-users-and-schemas-how-to-topics.md)  
  
-   [Server-Level Roles](server-level-roles.md)  
  
-   [Database-Level Roles](database-level-roles.md)  
  
-   [Application Roles](application-roles.md)  
  
## See Also  
 [Securing SQL Server](../securing-sql-server.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-database-principals-transact-sql)   
 [sys.server_principals &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-server-principals-transact-sql)   
 [sys.sql_logins &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-sql-logins-transact-sql)   
 [sys.database_role_members &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-database-role-members-transact-sql)   
 [Server-Level Roles](server-level-roles.md)   
 [Database-Level Roles](database-level-roles.md)  
  
  
