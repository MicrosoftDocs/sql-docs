---
title: "contained database authentication Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "contained_database_authentication_TSQL"
  - "contained database authentication"
helpviewer_keywords: 
  - "contained database, enabling"
  - "contained database authentication option"
ms.assetid: b80768d2-ac20-4035-a335-d9adb74b3f6e
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# contained database authentication Server Configuration Option
  Use the **contained database authentication** option to enable contained databases on the instance of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
 This server option allows you to control **contained database authentication**.  
  
-   When **contained database authentication** is off (0) for the instance, contained databases cannot be created, or attached to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   When **contained database authentication** is on (1) for the instance, contained databases can be created, or attached to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
 A contained database includes all database settings and metadata required to define the database and has no configuration dependencies on the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] where the database is installed. Users can connect to the database without authenticating a login at the [!INCLUDE[ssDE](../../includes/ssde-md.md)] level. Isolating the database from the Database Engine makes it possible to easily move the database to another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Including all the database settings in the database enables database owners to manage all the configuration settings for the database. For more information about contained databases, see [Contained Databases](../../relational-databases/databases/contained-databases.md).  
  
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
 [sp_configure &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql)   
 [RECONFIGURE &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/reconfigure-transact-sql)   
 [Server Configuration Options &#40;SQL Server&#41;](server-configuration-options-sql-server.md)  
  
  
