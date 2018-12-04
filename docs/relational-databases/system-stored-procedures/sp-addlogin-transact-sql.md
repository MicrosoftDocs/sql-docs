---
title: "sp_addlogin (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addlogin"
  - "sp_addlogin_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_addlogin"
ms.assetid: 030f19c3-a5e3-4b53-bfc4-de4bfca0fddc
author: VanMSFT
ms.author: vanto
manager: craigg
---
# sp_addlogin (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates a new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login that allows a user to connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE LOGIN](../../t-sql/statements/create-login-transact-sql.md) instead.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addlogin [ @loginame = ] 'login'   
    [ , [ @passwd = ] 'password' ]   
    [ , [ @defdb = ] 'database' ]   
    [ , [ @deflanguage = ] 'language' ]   
    [ , [ @sid = ] sid ]   
    [ , [ @encryptopt = ] 'encryption_option' ]   
[;]  
```  
  
## Arguments  
 [ @loginame= ] '*login*'  
 Is the name of the login. *login* is **sysname**, with no default.  
  
 [ @passwd= ] '*password*'  
 Is the login password. *password* is **sysname**, with a default of NULL.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]  
  
 [ @defdb= ] '*database*'  
 Is the default database of the login (the database to which the login is first connected after logging in). *database* is **sysname**, with a default of **master**.  
  
 [ @deflanguage= ] '*language*'  
 Is the default language of the login. *language* is **sysname**, with a default of NULL. If *language* is not specified, the default *language* of the new login is set to the current default language of the server.  
  
 [ @sid= ] '*sid*'  
 Is the security identification number (SID). *sid* is **varbinary(16)**, with a default of NULL. If *sid* is NULL, the system generates a SID for the new login. Despite the use of a **varbinary** data type, values other than NULL must be exactly 16 bytes in length, and must not already exist. Specifying *sid* is useful, for example, when you are scripting or moving [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins from one server to another and you want the logins to have the same SID on different servers.  
  
 [ @encryptopt= ] '*encryption_option*'  
 Specifies whether the password is passed in as clear text or as the hash of the clear text password. Note that no encryption takes place. The word "encrypt" is used in this discussion for the sake of backward compatibility. If a clear text password is passed in, it is hashed. The hash is stored. *encryption_option* is **varchar(20)**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|NULL|The password is passed in clear. This is the default.|  
|**skip_encryption**|The password is already hashed. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] should store the value without re-hashing it.|  
|**skip_encryption_old**|The supplied password was hashed by an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] should store the value without re-hashing it. This option is provided for upgrade purposes only.|  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins can contain from 1 to 128 characters, including letters, symbols, and numbers. Logins cannot contain a backslash (\\); be a reserved login name, for example sa or public, or already exist; or be NULL or an empty string (`''`).  
  
 If the name of a default database is supplied, you can connect to the specified database without executing the USE statement. However, you cannot use the default database until you are given access to that database by the database owner (by using [sp_adduser](../../relational-databases/system-stored-procedures/sp-adduser-transact-sql.md) or [sp_addrolemember](../../relational-databases/system-stored-procedures/sp-addrolemember-transact-sql.md)) or [sp_addrole](../../relational-databases/system-stored-procedures/sp-addrole-transact-sql.md).  
  
 The SID number is a GUID that will uniquely identify the login in the server.  
  
 Changing the default language of the server does not change the default language of existing logins. To change the default language of the server, use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md).  
  
 Using **skip_encryption** to suppress password hashing is useful if the password is already hashed when the login is added to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the password was hashed by an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], use **skip_encryption_old**.  
  
 sp_addlogin cannot be executed within a user-defined transaction.  
  
 The following table shows several stored procedures that are used with sp_addlogin.  
  
|Stored procedure|Description|  
|----------------------|-----------------|  
|[sp_grantlogin](../../relational-databases/system-stored-procedures/sp-grantlogin-transact-sql.md)|Adds a Windows user or group.|  
|[sp_password](../../relational-databases/system-stored-procedures/sp-password-transact-sql.md)|Changes the password of a user.|  
|[sp_defaultdb](../../relational-databases/system-stored-procedures/sp-defaultdb-transact-sql.md)|Changes the default database of a user.|  
|[sp_defaultlanguage](../../relational-databases/system-stored-procedures/sp-defaultlanguage-transact-sql.md)|Changes the default language of a user.|  
  
## Permissions  
 Requires ALTER ANY LOGIN permission.  
  
## Examples  
  
### A. Creating a SQL Server login  
 The following example creates a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login for the user `Victoria`, with a password of `B1r12-36`, without specifying a default database.  
  
```  
EXEC sp_addlogin 'Victoria', 'B1r12-36';  
GO  
```  
  
### B. Creating a SQL Server login that has a default database  
 The following example creates a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login for the user `Albert`, with a password of `B5432-3M6` and a default database of `corporate`.  
  
```  
EXEC sp_addlogin 'Albert', 'B5432-3M6', 'corporate';  
GO  
```  
  
### C. Creating a SQL Server login that has a different default language  
 The following example creates a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login for the user `TzTodorov`, with a password of `709hLKH7chjfwv`, a default database of `AdventureWorks2012`, and a default language of `Bulgarian`.  
  
```  
EXEC sp_addlogin 'TzTodorov', '709hLKH7chjfwv', 'AdventureWorks2012', N'български'  
```  
  
### D. Creating a SQL Server login that has a specific SID  
 The following example creates a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login for the user `Michael`, with a password of `B548bmM%f6`, a default database of `AdventureWorks2012`, a default language of `us_english`, and a SID of `0x0123456789ABCDEF0123456789ABCDEF`.  
  
```  
EXEC sp_addlogin 'Michael', 'B548bmM%f6', 'AdventureWorks2012', 'us_english', 0x0123456789ABCDEF0123456789ABCDEF  
```  
  
## See Also  
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [sp_droplogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droplogin-transact-sql.md)   
 [sp_helpuser &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpuser-transact-sql.md)   
 [sp_revokelogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-revokelogin-transact-sql.md)   
 [xp_logininfo &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/xp-logininfo-transact-sql.md)  
  
  
