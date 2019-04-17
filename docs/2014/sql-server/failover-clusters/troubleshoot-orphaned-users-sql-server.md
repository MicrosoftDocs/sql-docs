---
title: "Troubleshoot Orphaned Users (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "orphaned users [SQL Server]"
  - "logins [SQL Server], orphaned users"
  - "troubleshooting [SQL Server], user accounts"
  - "user accounts [SQL Server], orphaned users"
  - "failover [SQL Server], managing metadata"
  - "database mirroring [SQL Server], metadata"
  - "users [SQL Server], orphaned"
ms.assetid: 11eefa97-a31f-4359-ba5b-e92328224133
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Troubleshoot Orphaned Users (SQL Server)
  To log into an instance of Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a principal must have a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. This login is used in the authentication process that verifies whether the principal is allowed to connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins on a server instance are visible in the **sys.server_principals** catalog view and the **sys.syslogins** compatibility view.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins access individual databases using a database user that is mapped to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. There are two exceptions to this rule:  
  
-   The guest account.  
  
     This is an account that, when enabled in the database, enables [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins that are not mapped to a database user to enter the database as the guest user.  
  
-   Microsoft Windows group memberships.  
  
     A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login created from a Windows user can enter a database if the Windows user is a member of a Windows group that is also a user in the database.  
  
 Information about the mapping of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to a database user is stored within the database. It includes the name of the database user and the SID of the corresponding [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. The permissions of this database user are used for authorization in the database.  
  
 A database user for which the corresponding [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login is undefined or is incorrectly defined on a server instance cannot log in to the instance. Such a user is said to be an *orphaned user* of the database on that server instance. A database user can become orphaned if the corresponding [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login is dropped. Also, a database user can become orphaned after a database is restored or attached to a different instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Orphaning can happen if the database user is mapped to a SID that is not present in the new server instance.  
  
> [!NOTE]  
>  A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login cannot access a database in which it lacks a corresponding database user unless **guest** is enabled in that database. For information about creating a database user account, see [CREATE USER &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-user-transact-sql).  
  
## To Detect Orphaned Users  
 To detect orphaned users, execute the following Transact-SQL statements:  
  
```  
USE <database_name>;  
GO;   
sp_change_users_login @Action='Report';  
GO;  
```  
  
 The output lists the users and corresponding security identifiers (SID) in the current database that are not linked to any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. For more information, see [sp_change_users_login &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-change-users-login-transact-sql).  
  
> [!NOTE]  
>  **sp_change_users_login** cannot be used with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins that are created from Windows.  
  
## To Resolve an Orphaned User  
 To resolve an orphaned user, use the following procedure:  
  
1.  The following command relinks the server login account specified by *<login_name>* with the database user specified by *<database_user>*.  
  
    ```  
    USE <database_name>;  
    GO  
    sp_change_users_login @Action='update_one', @UserNamePattern='<database_user>', @LoginName='<login_name>';  
    GO  
  
    ```  
  
     For more information, see [sp_change_users_login &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-change-users-login-transact-sql).  
  
2.  After you run the code in the preceding step, the user can access the database. The user then can alter the password of the *<login_name>* login account by using the **sp_password** stored procedure, as follows:  
  
    ```  
    USE master   
    GO  
    sp_password @old=NULL, @new='password', @loginame='<login_name>';  
    GO  
    ```  
  
    > [!IMPORTANT]  
    >  Only logins with the ALTER ANY LOGIN permission can change the password of another user's login. However, only members of the **sysadmin** role can modify passwords of **sysadmin** role members.  
  
    > [!NOTE]  
    >  **sp_password** cannot be used for [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows accounts. Users connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] through their Windows network account are authenticated by Windows; therefore, their passwords can only be changed in Windows.  
  
     For more information, see [sp_password &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-password-transact-sql).  
  
## See Also  
 [CREATE USER &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-user-transact-sql)   
 [CREATE LOGIN &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-login-transact-sql)   
 [sp_change_users_login &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-change-users-login-transact-sql)   
 [sp_addlogin &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addlogin-transact-sql)   
 [sp_grantlogin &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-grantlogin-transact-sql)   
 [sp_password &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-password-transact-sql)   
 [sys.sysusers &#40;Transact-SQL&#41;](/sql/relational-databases/system-compatibility-views/sys-sysusers-transact-sql)   
 [sys.syslogins &#40;Transact-SQL&#41;](/sql/relational-databases/system-compatibility-views/sys-syslogins-transact-sql)  
  
  
