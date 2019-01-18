---
title: "Migrate to a Partially Contained Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "contained database, migrating to"
ms.assetid: 90faac38-f79e-496d-b589-e8b2fe01c562
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Migrate to a Partially Contained Database
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic discusses how to prepare to change to the partially contained database model and then provides the migration steps.  
  
 **In this topic:**  
  
-   [Preparing to Migrate a Database](#prepare)  
  
-   [Enable Contained Databases](#enable)  
  
-   [Converting a Database to Partially Contained](#convert)  
  
-   [Migrating Users to Contained Database Users](#users)  
  
##  <a name="prepare"></a> Preparing to Migrate a Database  
 Review the following items when considering migrating a database to the partially contained database model.  
  
-   You should understand the partially contained database model. For more information, see [Contained Databases](../../relational-databases/databases/contained-databases.md).  
  
-   You should understand risks that are unique to partially contained databases. For more information, see [Security Best Practices with Contained Databases](../../relational-databases/databases/security-best-practices-with-contained-databases.md).  
  
-   Contained databases do not support replication, change data capture, or change tracking. Confirm the database does not use these features.  
  
-   Review the list of database features that are modified for partially contained databases. For more information, see [Modified Features &#40;Contained Database&#41;](../../relational-databases/databases/modified-features-contained-database.md).  
  
-   Query [sys.dm_db_uncontained_entities &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-uncontained-entities-transact-sql.md) to find uncontained objects or features in the database. For more information, see.  
  
-   Monitor the **database_uncontained_usage** XEvent to see when uncontained features are used.  
  
##  <a name="enable"></a> Enable Contained Databases  
 Contained databases must be enabled on the instance of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], before contained databases can be created.  
  
### Enabling Contained Databases Using Transact-SQL  
 The following example enables contained databases on the instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
```sql  
sp_configure 'contained database authentication', 1;  
GO  
RECONFIGURE ;  
GO  
```  
  
#### Enabling Contained Databases Using Management Studio  
 The following example enables contained databases on the instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
1.  In Object Explorer, right-click the server name, and then click **Properties**.  
  
2.  On the **Advanced** page, in the **Containment** section, set the **Enable Contained Databases** option to **True**.  
  
3.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
##  <a name="convert"></a> Converting a Database to Partially Contained  
 A database is converted to a contained database by changing the **CONTAINMENT** option.  
  
### Converting a Database to Partially Contained Using Transact-SQL  
 The following example converts a database named `Accounting` to a partially contained database.  
  
```sql  
USE [master]  
GO  
ALTER DATABASE [Accounting] SET CONTAINMENT = PARTIAL  
GO  
```  
  
### Converting a Database to Partially contained Using Management Studio  
 The following example converts a database to a partially contained database.  
  
1.  In Object Explorer, expand **Databases**, right-click the database to be converted, and then click **Properties**.  
  
2.  On the **Options** page, change the **Containment type** option to **Partial**.  
  
3.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
##  <a name="users"></a> Migrating Users to Contained Database Users  
 The following example migrates all users that are based on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins to contained database users with passwords. The example excludes logins that are not enabled. The example must be executed in the contained database.  
  
```sql  
DECLARE @username sysname ;  
DECLARE user_cursor CURSOR  
    FOR   
        SELECT dp.name   
        FROM sys.database_principals AS dp  
        JOIN sys.server_principals AS sp   
        ON dp.sid = sp.sid  
        WHERE dp.authentication_type = 1 AND sp.is_disabled = 0;  
OPEN user_cursor  
FETCH NEXT FROM user_cursor INTO @username  
    WHILE @@FETCH_STATUS = 0  
    BEGIN  
        EXECUTE sp_migrate_user_to_contained   
        @username = @username,  
        @rename = N'keep_name',  
        @disablelogin = N'disable_login';  
    FETCH NEXT FROM user_cursor INTO @username  
    END  
CLOSE user_cursor ;  
DEALLOCATE user_cursor ;  
```  
  
## See Also  
 [Contained Databases](../../relational-databases/databases/contained-databases.md)   
 [sp_migrate_user_to_contained &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-migrate-user-to-contained-transact-sql.md)   
 [sys.dm_db_uncontained_entities &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-uncontained-entities-transact-sql.md)  
  
  
