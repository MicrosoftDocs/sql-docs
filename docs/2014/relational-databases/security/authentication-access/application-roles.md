---
title: "Application Roles | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "application roles [SQL Server], about application roles"
  - "principals [SQL Server], application roles"
  - "credentials [SQL Server], roles"
  - "application roles [SQL Server]"
  - "roles [SQL Server], application"
  - "permissions [SQL Server], roles"
  - "users [SQL Server], application roles"
  - "authentication [SQL Server], roles"
  - "groups [SQL Server], roles"
ms.assetid: dca18b8a-ca03-4b7f-9a46-8474d5b66f76
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Application Roles
  An application role is a database principal that enables an application to run with its own, user-like permissions. You can use application roles to enable access to specific data to only those users who connect through a particular application. Unlike database roles, application roles contain no members and are inactive by default. Application roles work with both authentication modes. Application roles are enabled by using **sp_setapprole**, which requires a password. Because application roles are a database-level principal, they can access other databases only through permissions granted in those databases to **guest**. Therefore, any database in which **guest** has been disabled will be inaccessible to application roles in other databases.  
  
 In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], application roles cannot access server-level metadata because they are not associated with a server-level principal. To disable this restriction and thereby allow application roles to access server-level metadata, set the global flag 4616. For more information, see [Trace Flags &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql) and [DBCC TRACEON &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-traceon-transact-sql).  
  
## Connecting with an Application Role  
 The following steps make up the process by which an application role switches security contexts:  
  
1.  A user executes a client application.  
  
2.  The client application connects to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] as the user.  
  
3.  The application then executes the **sp_setapprole** stored procedure with a password known only to the application.  
  
4.  If the application role name and password are valid, the application role is enabled.  
  
5.  At this point the connection loses the permissions of the user and assumes the permissions of the application role.  
  
 The permissions acquired through the application role remain in effect for the duration of the connection.  
  
 In earlier versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the only way for a user to reacquire its original security context after starting an application role is to disconnect and reconnect to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Beginning with [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], **sp_setapprole** has an option that creates a cookie. The cookie contains context information before the application role is enabled. The cookie can be used by **sp_unsetapprole** to revert the session to its original context. For information about this new option and an example, see [sp_setapprole &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-setapprole-transact-sql).  
  
> [!IMPORTANT]  
>  The ODBC **encrypt** option is not supported by **SqlClient**. When you are transmitting confidential information over a network, use Secure Sockets Layer (SSL) or IPsec to encrypt the channel. If you must persist credentials in the client application, encrypt the credentials by using the crypto API functions. In [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and later versions, the parameter *password* is stored as a one-way hash.  
  
## Related Tasks  
  
|||  
|-|-|  
|Create an application role.|[Create an Application Role](create-an-application-role.md) and [CREATE APPLICATION ROLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-application-role-transact-sql)|  
|Alter an application role.|[ALTER APPLICATION ROLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-application-role-transact-sql)|  
|Delete an application role.|[DROP APPLICATION ROLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-application-role-transact-sql)|  
|Using an application role.|[sp_setapprole &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-setapprole-transact-sql)|  
  
## See Also  
 [Securing SQL Server](../securing-sql-server.md)  
  
  
