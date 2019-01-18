---
title: "sp_change_users_login (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/13/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_change_users_login"
  - "sp_change_users_login_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_change_users_login"
ms.assetid: 1554b39f-274b-4ef8-898e-9e246b474333
author: VanMSFT
ms.author: vanto
manager: craigg
---
# sp_change_users_login (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Maps an existing database user to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER USER](../../t-sql/statements/alter-user-transact-sql.md) instead.  
  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_change_users_login [ @Action = ] 'action'   
    [ , [ @UserNamePattern = ] 'user' ]   
    [ , [ @LoginName = ] 'login' ]   
    [ , [ @Password = ] 'password' ]  
[;]  
```  
  
## Arguments  
 [ @Action= ] '*action*'  
 Describes the action to be performed by the procedure. *action* is **varchar(10)**. *action* can have one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**Auto_Fix**|Links a user entry in the sys.database_principals system catalog view in the current database to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login of the same name. If a login with the same name does not exist, one will be created. Examine the result from the **Auto_Fix** statement to confirm that the correct link is in fact made. Avoid using **Auto_Fix** in security-sensitive situations.<br /><br /> When you use **Auto_Fix**, you must specify *user* and *password* if the login does not already exist, otherwise you must specify *user* but *password* will be ignored. *login* must be NULL. *user* must be a valid user in the current database. The login cannot have another user mapped to it.|  
|**Report**|Lists the users and corresponding security identifiers (SID) in the current database that are not linked to any login. *user*, *login*, and *password* must be NULL or not specified.<br /><br /> To replace the report option with a query using the system tables, compare the entries in **sys.server_prinicpals** with the entries in **sys.database_principals**.|  
|**Update_One**|Links the specified *user* in the current database to an existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] *login*. *user* and *login* must be specified. *password* must be NULL or not specified.|  
  
 [ @UserNamePattern= ] '*user*'  
 Is the name of a user in the current database. *user* is **sysname**, with a default of NULL.  
  
 [ @LoginName= ] '*login*'  
 Is the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. *login* is **sysname**, with a default of NULL.  
  
 [ @Password= ] '*password*'  
 Is the password assigned to a new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login that is created by specifying **Auto_Fix**. If a matching login already exists, the user and login are mapped and *password* is ignored. If a matching login does not exist, sp_change_users_login creates a new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login and assigns *password* as the password for the new login. *password* is **sysname**, and must not be NULL.  
  
> **IMPORTANT!!** Always use a [strong Password!](../../relational-databases/security/strong-passwords.md)
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|UserName|**sysname**|Database user name.|  
|UserSID|**varbinary(85)**|User's security identifier.|  
  
## Remarks  
 Use sp_change_users_login to link a database user in the current database with a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. If the login for a user has changed, use sp_change_users_login to link the user to the new login without losing user permissions. The new *login* cannot be sa, and the *user*cannot be dbo, guest, or an INFORMATION_SCHEMA user.  
  
 sp_change_users_login cannot be used to map database users to Windows-level principals, certificates, or asymmetric keys.  
  
 sp_change_users_login cannot be used with a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login created from a Windows principal or with a user created by using CREATE USER WITHOUT LOGIN.  
  
 sp_change_users_login cannot be executed within a user-defined transaction.  
  
## Permissions  
 Requires membership in the db_owner fixed database role. Only members of the sysadmin fixed server role can specify the **Auto_Fix** option.  
  
## Examples  
  
### A. Showing a report of the current user to login mappings  
 The following example produces a report of the users in the current database and their security identifiers (SIDs).  
  
```  
EXEC sp_change_users_login 'Report';  
```  
  
### B. Mapping a database user to a new SQL Server login  
 In the following example, a database user is associated with a new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. Database user `MB-Sales`, which at first is mapped to another login, is remapped to login `MaryB`.  
  
```  
--Create the new login.  
CREATE LOGIN MaryB WITH PASSWORD = '982734snfdHHkjj3';  
GO  
--Map database user MB-Sales to login MaryB.  
USE AdventureWorks2012;  
GO  
EXEC sp_change_users_login 'Update_One', 'MB-Sales', 'MaryB';  
GO  
```  
  
### C. Automatically mapping a user to a login, creating a new login if it is required  
 The following example shows how to use `Auto_Fix` to map an existing user to a login of the same name, or to create the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `Mary` that has the password `B3r12-3x$098f6` if the login `Mary` does not exist.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_change_users_login 'Auto_Fix', 'Mary', NULL, 'B3r12-3x$098f6';  
GO  
```  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [sp_adduser &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adduser-transact-sql.md)   
 [sp_helplogins &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helplogins-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)  
  
  
