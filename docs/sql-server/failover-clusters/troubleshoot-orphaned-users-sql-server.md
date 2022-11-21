---
title: "Troubleshoot orphaned users"
description: Orphaned users occur when a database user login no longer exist in the master database. This topic discusses how to identify and resolve orphaned users. 
ms.custom: "seo-lt-2019"
ms.date: "07/14/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: high-availability
ms.topic: troubleshooting
helpviewer_keywords: 
  - "orphaned users [SQL Server]"
  - "logins [SQL Server], orphaned users"
  - "troubleshooting [SQL Server], user accounts"
  - "user accounts [SQL Server], orphaned users"
  - "failover [SQL Server], managing metadata"
  - "database mirroring [SQL Server], metadata"
  - "users [SQL Server], orphaned"
ms.assetid: 11eefa97-a31f-4359-ba5b-e92328224133
author: MashaMSFT
ms.author: mathoma
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016"
---
# Troubleshoot orphaned users (SQL Server)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Orphaned users in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] occur when a database user is based on  a login in the **master** database, but the login no longer exists in **master**. This can occur when the login is deleted, or when the database is moved to another server where the login does not exist. This topic describes how to find orphaned users, and remap them to logins.  
  
> [!NOTE]  
>  Reduce the possibility of orphaned users by using contained database users for databases that might be moved. For more information, see [Contained Database Users - Making Your Database Portable](../../relational-databases/security/contained-database-users-making-your-database-portable.md).  
  
## Background  
 To connect to a database on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using a security principal (database user identity) based on a login, the principal must have a valid login in the **master** database. This login is used in the authentication process that verifies the principals identity and determines if the principal is allowed to connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins on a server instance are visible in the **sys.server_principals** catalog view and the **sys.sql_logins** compatibility view.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins access individual databases  as "database user" that is mapped to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. There are three exceptions to this rule:  
  
-   Contained database users  
  
     Contained database users authenticate at the user-database level and are not associated with logins. This is recommended because the databases are more portable and contained database users cannot become orphaned. However they must be recreated for each database. This might be impractical in an environment with many databases.  
  
-   The **guest** account.  
  
     When enabled in the database, this account permits [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins that are not mapped to a database user to enter the database as the **guest** user. The **guest** account is disabled by default.  
  
-   Microsoft Windows group memberships.  
  
     A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login created from a Windows user can enter a database if the Windows user is a member of a Windows group that is also a user in the database.  
  
 Information about the mapping of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to a database user is stored within the database. It includes the name of the database user and the SID of the corresponding [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. The permissions of this database user are applied for authorization in the database.  
  
 A database user (based on a login) for which the corresponding [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login is undefined or is incorrectly defined on a server instance cannot log in to the instance. Such a user is said to be an *orphaned user* of the database on that server instance. Orphaning can happen if the database user is mapped to a login SID that is not present in the `master` instance. A database user can become orphaned after a database is restored or attached to a different instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where the login was never created. A database user can also become orphaned if the corresponding [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login is dropped. Even if the login is recreated, it will have a different SID, so the database user will still be orphaned.  
  
## Detect Orphaned Users  

**For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and PDW**

To detect orphaned users in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] based on missing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication logins, execute the following statement in the user database:  
  
```  
SELECT dp.type_desc, dp.sid, dp.name AS user_name  
FROM sys.database_principals AS dp  
LEFT JOIN sys.server_principals AS sp  
    ON dp.sid = sp.sid  
WHERE sp.sid IS NULL  
    AND dp.authentication_type_desc = 'INSTANCE';  
```  
  
 The output lists the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication  users and corresponding security identifiers (SID) in the current database that are not linked to any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  

**For SQL Database and Azure Synapse Analytics**

The `sys.server_principals` table is not available in SQL Database or Azure Synapse Analytics. Identify orphaned users in those environments with the following steps:

1. Connect to the `master` database and select the SID's for the logins with the following query:
    ```
    SELECT sid 
    FROM sys.sql_logins 
    WHERE type = 'S'; 
    ```

2. Connect to the user database and review the SID's of the users in the `sys.database_principals` table, by using the following query:

    ```
    SELECT name, sid, principal_id
    FROM sys.database_principals 
    WHERE type = 'S' 
      AND name NOT IN ('guest', 'INFORMATION_SCHEMA', 'sys')
      AND authentication_type_desc = 'INSTANCE';
    ```

3. Compare the two lists to determine if there are user SID's in the user database `sys.database_principals` table which are not matched by login SID's in the master database `sql_logins` table. 
  
## Resolve an Orphaned User  
In the master database, use the [CREATE LOGIN](../../t-sql/statements/create-login-transact-sql.md) statement with the SID option to recreate a missing login, providing the `SID` of the database user obtained in the previous section:  
  
```  
CREATE LOGIN <login_name>   
WITH PASSWORD = '<use_a_strong_password_here>',  
SID = <SID>;  
```  
  
 To map an orphaned user to a login which already exists in **master**, execute the [ALTER USER](../../t-sql/statements/alter-user-transact-sql.md) statement in the user database, specifying the login name.  
  
```  
ALTER USER <user_name> WITH Login = <login_name>;  
```  
  
 When you recreate a missing login, the user can access the database using the password provided. Then the user can alter the password of the login account by using the ALTER LOGIN statement.  
  
```  
ALTER LOGIN <login_name> WITH PASSWORD = '<enterStrongPasswordHere>';  
```  
  
> [!IMPORTANT]  
>  Any login can change its own password. Only logins with the `ALTER ANY LOGIN` permission can change the password of another user's login. However, only members of the **sysadmin** role can modify passwords of **sysadmin** role members.  
  
## See Also  
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [ALTER USER &#40;Transact-SQL&#41;](../../t-sql/statements/alter-user-transact-sql.md)   
 [CREATE USER &#40;Transact-SQL&#41;](../../t-sql/statements/create-user-transact-sql.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)   
 [sys.server_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)   
 [sp_change_users_login &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-change-users-login-transact-sql.md)   
 [sp_addlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlogin-transact-sql.md)   
 [sp_grantlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-grantlogin-transact-sql.md)   
 [sp_password &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-password-transact-sql.md)   
 [sys.sysusers &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-sysusers-transact-sql.md)   
 [sys.sql_logins](../../relational-databases/system-catalog-views/sys-sql-logins-transact-sql.md)
 [sys.syslogins &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-syslogins-transact-sql.md)  
  
  
