---
title: "contained database authentication Server Configuration Option"
description: "Learn about the contained database authentication option. See how to turn it on so that you can attach contained databases to the SQL Server Database Engine."
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/02/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "contained database, enabling"
  - "contained database authentication option"
---
# contained database authentication Server Configuration Option
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Use the **contained database authentication** option to enable contained databases on the instance of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
 This server option allows you to control **contained database authentication**.  
  
-   When **contained database authentication** is off (0) for the instance, contained databases cannot be created, or attached to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   When **contained database authentication** is on (1) for the instance, contained databases can be created, or attached to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
 A contained database includes all database settings and metadata required to define the database and has no configuration dependencies on the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] where the database is installed. Users can connect to the database without authenticating a login at the [!INCLUDE[ssDE](../../includes/ssde-md.md)] level. Isolating the database from the Database Engine makes it possible to easily move the database to another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Including all the database settings in the database enables database owners to manage all the configuration settings for the database. For more information about contained databases, see [Contained Databases](../../relational-databases/databases/contained-databases.md).  

> [!NOTE]
> Contained databases are always enabled for [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] and [!INCLUDE[ssSDW_md](../../includes/sssdw-md.md)] and cannot be disabled.
  
 If an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has any contained databases the **contained database authentication** setting can be set to 0 by using the **RECONFIGURE WITH OVERRIDE** statement. Setting **contained database authentication** to 0 will disable contained database authentication for the contained databases.  
  
> [!IMPORTANT]  
>  When contained databases are enabled, database users with the ALTER ANY USER permission, such as members of the db_owner and db_accessadmin database roles, can grant access to databases and by doing so, grant access to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This means that control over access to the server is no longer limited to members of the sysadmin and securityadmin fixed server role, and logins with the server level CONTROL SERVER and ALTER ANY LOGIN permission. Before allowing contained databases, you should understand the risks associated with contained databases. For more information, see [Security Best Practices with Contained Databases](../../relational-databases/databases/security-best-practices-with-contained-databases.md).  
  
## Examples  
 The following example enables contained databases on the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
```sql  
sp_configure 'contained database authentication', 1;  
GO  
RECONFIGURE;  
GO  
```  
  
## See Also  
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [RECONFIGURE &#40;Transact-SQL&#41;](../../t-sql/language-elements/reconfigure-transact-sql.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)  
  
  
