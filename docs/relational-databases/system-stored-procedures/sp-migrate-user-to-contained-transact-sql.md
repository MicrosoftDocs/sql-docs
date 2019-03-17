---
title: "sp_migrate_user_to_contained (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_migrate_user_to_contained"
  - "sp_migrate_user_to_contained_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_migrate_user_to_contained"
ms.assetid: b3a49ff6-46ad-4ee7-b6fe-7e54213dc33e
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_migrate_user_to_contained (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Converts a database user that is mapped to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, to a contained database user with password. In a contained database, use this procedure to remove dependencies on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where the database is installed. **sp_migrate_user_to_contained** separates the user from the original [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, so that settings such as password and default language can be administered separately for the contained database. **sp_migrate_user_to_contained** can be used before moving the contained database to a different instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] to eliminate dependencies on the current [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance logins.  
  
 **Note** This procedure is only used in a contained database. For more information, see [Contained Databases](../../relational-databases/databases/contained-databases.md).  
  
## Syntax  
  
```  
  
sp_migrate_user_to_contained [ @username = ] N'user' ,   
    [ @rename = ] { N'copy_login_name' | N'keep_name' } ,   
    [ @disablelogin = ] { N'disable_login' | N'do_not_disable_login' }   
```  
  
## Arguments  
 [**@username =** ] **N'***user***'**  
 Name of a user in the current contained database that is mapped to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authenticated login. The value is **sysname**, with a default of **NULL**.  
  
 [**@rename =** ] **N'***copy_login_name***'** | **N'***keep_name***'**  
 When a database user based on a login has a different user name than the login name, use *keep_name* to retain the database user name during the migration. Use *copy_login_name* to create the new contained database user with the name of the login, instead of the user. When a database user based on a login has the same user name as the login name, both options create the contained database user without changing the name.  
  
 [**@disablelogin =** ] **N'***disable_login***'** | **N'***do_not_disable_login***'**  
 *disable_login* disables the login in the master database. To connect when the login is disabled, the connection must provide the contained database name as the **initial catalog** as part of the connection string.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 **sp_migrate_user_to_contained** creates the contained database user with password, regardless of the properties or permissions of the login. For example, the procedure can succeed if the login is disabled or if the user is denied the **CONNECT** permission to the database.  
  
 **sp_migrate_user_to_contained** has the following restrictions.  
  
-   The user name cannot already exist in the database.  
  
-   Built-in users, for example dbo and guest, cannot be converted.  
  
-   The user cannot be specified in the **EXECUTE AS** clause of a signed stored procedure.  
  
-   The user cannot own a stored procedure that includes the **EXECUTE AS OWNER** clause.  
  
-   **sp_migrate_user_to_contained** cannot be used in a system database.  
  
## Security  
 When migrating users, be careful not to disable or delete all the administrator logins from the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If all logins are deleted, see [Connect to SQL Server When System Administrators Are Locked Out](../../database-engine/configure-windows/connect-to-sql-server-when-system-administrators-are-locked-out.md).  
  
 If the **BUILTIN\Administrators** login is present, administrators can connect by starting their application using the **Run as Administrator** option.  
  
### Permissions  
 Requires the **CONTROL SERVER** permission.  
  
## Examples  
  
### A. Migrating a single user  
 The following example migrates a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login named `Barry`, to a contained database user with password. The example retains the does not change the user name, and retains the login as enabled.  
  
```sql  
sp_migrate_user_to_contained   
@username = N'Barry',  
@rename = N'keep_name',  
@disablelogin = N'do_not_disable_login' ;  
  
```  
  
### B. Migrating all database users with logins to contained database users without logins  
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
 [Migrate to a Partially Contained Database](../../relational-databases/databases/migrate-to-a-partially-contained-database.md)   
 [Contained Databases](../../relational-databases/databases/contained-databases.md)  
  
  
