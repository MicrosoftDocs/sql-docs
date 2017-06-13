---
title: "CREATE LOGIN (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "CREATE_LOGIN_TSQL"
  - "CREATE LOGIN"
  - "LOGIN_TSQL"
  - "LOGIN"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "passwords [SQL Server], logins"
  - "mapping logins [SQL Server]"
  - "logins [SQL Server], creating"
  - "CREATE LOGIN statement"
  - "permissions [SQL Server], logins"
  - "Windows domain accounts [SQL Server]"
  - "re-hashing passwords"
  - "certificates [SQL Server], logins"
ms.assetid: eb737149-7c92-4552-946b-91085d8b1b01
caps.latest.revision: 101
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CREATE LOGIN (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Creates a [!INCLUDE[ssDE](../../includes/ssde-md.md)] login for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server  
  
CREATE LOGIN login_name { WITH <option_list1> | FROM <sources> }  
  
<option_list1> ::=   
    PASSWORD = { 'password' | hashed_password HASHED } [ MUST_CHANGE ]  
    [ , <option_list2> [ ,... ] ]  
  
<option_list2> ::=    
    SID = sid  
    | DEFAULT_DATABASE = database      
    | DEFAULT_LANGUAGE = language  
    | CHECK_EXPIRATION = { ON | OFF}  
    | CHECK_POLICY = { ON | OFF}  
    | CREDENTIAL = credential_name   
  
<sources> ::=  
    WINDOWS [ WITH <windows_options>[ ,... ] ]  
    | CERTIFICATE certname  
    | ASYMMETRIC KEY asym_key_name  
  
<windows_options> ::=        
    DEFAULT_DATABASE = database  
    | DEFAULT_LANGUAGE = language  
```  
  
```  
-- Syntax for Azure SQL Database and Azure SQL Data Warehouse  
  
CREATE LOGIN login_name  
 { WITH <option_list3> }  
  
<option_list3> ::=   
    PASSWORD = { 'password' }  
    [ SID = sid ]  
```  
  
```  
-- Syntax for Parallel Data Warehouse  
  
CREATE LOGIN loginName { WITH <option_list1> | FROM WINDOWS }  
  
<option_list1> ::=   
    PASSWORD = { 'password' } [ MUST_CHANGE ]  
    [ , <option_list2> [ ,... ] ]  
  
<option_list2> ::=    
      CHECK_EXPIRATION = { ON | OFF}  
    | CHECK_POLICY = { ON | OFF}  
```  
  
## Arguments  
 *login_name*  
 Specifies the name of the login that is created. There are four types of logins: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins, Windows logins, certificate-mapped logins, and asymmetric key-mapped logins. When you are creating logins that are mapped from a Windows domain account, you must use the pre-Windows 2000 user logon name in the format [\<domainName>\\<login_name>]. You cannot use a UPN in the format login_name@DomainName. For an example, see example D later in this topic. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication logins are type **sysname** and must conform to the rules for [Identifiers](http://msdn.microsoft.com/library/ms175874.aspx) and cannot contain a '**\\**'. Windows logins can contain a '**\\**'. Logins based on users and groups in Active Directory, are limited to names of less than 21 characters.  
  
 PASSWORD **='***password***'**  
 Applies to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins only. Specifies the password for the login that is being created. You should use a strong password. For more information see [Strong Passwords](../../relational-databases/security/strong-passwords.md) and [Password Policy](../../relational-databases/security/password-policy.md). Beginning with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], stored password information is calculated using SHA-512 of the salted password.  
  
 Passwords are case-sensitive. Passwords should always be at least 8 characters long, and cannot exceed 128 characters.  Passwords can include a-z, A-Z, 0-9, and most non-alphanumeric characters. Passwords cannot contain single quotes, or the *login_name*.  
  
 PASSWORD **=***hashed_password*  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Applies to the HASHED keyword only. Specifies the hashed value of the password for the login that is being created.  
  
 HASHED  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Applies to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins only. Specifies that the password entered after the PASSWORD argument is already hashed. If this option is not selected, the string entered as password is hashed before it is stored in the database. This option should only be used for migrating databases from one server to another. Do not use the HASHED option to create new logins. The HASHED option cannot be used with hashes created by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 7 or earlier,  
  
 MUST_CHANGE  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Applies to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins only. If this option is included, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] prompts the user for a new password the first time the new login is used.  
  
 CREDENTIAL **=***credential_name*  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 The name of a credential to be mapped to the new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. The credential must already exist in the server. Currently this option only links the credential to a login. A credential cannot be mapped to the System Administrator (sa) login.  
  
 SID = *sid*  
 Used to recreate a login. Applies to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication logins only, not Windows authentication logins. Specifies the SID of the new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication login. If this option is not used, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically assigns a SID. The SID structure depends on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login SID: a 16 byte (**binary(16)**) literal value based on a GUID. For example `SID = 0x14585E90117152449347750164BA00A7`.  
  
-   [!INCLUDE[ssSDS](../../includes/sssds-md.md)] login SID: a SID structure valid for [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. Typically this is a 32 byte (**binary(32)**) literal consisting of `0x01060000000000640000000000000000` plus 16 bytes representing a GUID. For example `SID = 0x0106000000000064000000000000000014585E90117152449347750164BA00A7`.  
  
DEFAULT_DATABASE **=***database*  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies the default database to be assigned to the login. If this option is not included, the default database is set to master.  
  
DEFAULT_LANGUAGE **=***language*  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies the default language to be assigned to the login. If this option is not included, the default language is set to the current default language of the server. If the default language of the server is later changed, the default language of the login remains unchanged.  
  
CHECK_EXPIRATION **=** { ON | **OFF** }  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Applies to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins only. Specifies whether password expiration policy should be enforced on this login. The default value is OFF.  
  
CHECK_POLICY **=** { **ON** | OFF }  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Applies to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins only. Specifies that the Windows password policies of the computer on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running should be enforced on this login. The default value is ON.  
  
 If the Windows policy requires strong passwords, passwords must contain at least three of the following four characteristics:  
  
-   An uppercase character (A-Z).  
-   A lowercase character (a-z).  
-   A digit (0-9).  
-   One of the non-alphanumeric characters, such as a space, _, @, *, ^, %, !, $, #, or &.  
  
WINDOWS  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies that the login be mapped to a Windows login.  
  
CERTIFICATE *certname*  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies the name of a certificate to be associated with this login. This certificate must already occur in the master database.  
  
ASYMMETRIC KEY *asym_key_name*  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies the name of an asymmetric key to be associated with this login. This key must already occur in the master database.  
  
## Remarks  
 Passwords are case-sensitive.  
  
 Prehashing of passwords is supported only when you are creating [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins.  
  
 If MUST_CHANGE is specified, CHECK_EXPIRATION and CHECK_POLICY must be set to ON. Otherwise, the statement will fail.  
  
 A combination of CHECK_POLICY = OFF and CHECK_EXPIRATION = ON is not supported.  
  
 When CHECK_POLICY is set to OFF, *lockout_time* is reset and CHECK_EXPIRATION is set to OFF.  
  
> [!IMPORTANT]  
>  CHECK_EXPIRATION and CHECK_POLICY are only enforced on [!INCLUDE[winxpsvr](../../includes/winxpsvr-md.md)] and later. For more information, see [Password Policy](../../relational-databases/security/password-policy.md).  
  
 Logins created from certificates or asymmetric keys are used only for code signing. They cannot be used to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can create a login from a certificate or asymmetric key only when the certificate or asymmetric key already exists in master.  
  
 For a script to transfer logins, see [How to transfer the logins and the passwords between instances of SQL Server 2005 and SQL Server 2008](http://support.microsoft.com/kb/918992).  
  
 Creating a login automatically enables the new login and grants the login the server level **CONNECT SQL** permission.  
  
 For information about designing a permissions system, see [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md).  
  
## [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] Logins  
 In [!INCLUDE[ssSDS](../../includes/sssds-md.md)], the **CREATE LOGIN** statement must be the only statement in a batch.  
  
 In some methods of connecting to [!INCLUDE[ssSDS](../../includes/sssds-md.md)], such as **sqlcmd**, you must append the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server name to the login name in the connection string by using the *\<login>*@*\<server>* notation. For example, if your login is `login1` and the fully qualified name of the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server is `servername.database.windows.net`, the *username* parameter of the connection string should be `login1@servername`. Because the total length of the *username* parameter is 128 characters, *login_name* is limited to 127 characters minus the length of the server name. In the example, `login_name` can only be 117 characters long because `servername` is 10 characters.  
  
 In [!INCLUDE[ssSDS](../../includes/sssds-md.md)] and [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] you must be connected to the master database to create a login.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rules allow you create a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication login in the format \<loginname>@\<servername>. If your [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server is **myazureserver** and your login is **myemail@live.com**, then you must supply your login as **myemail@live.com@myazureserver**.  
  
 In [!INCLUDE[ssSDS](../../includes/sssds-md.md)], login data required to authenticate a connection and server-level firewall rules are temporarily cached in each database. This cache is periodically refreshed. To force a refresh of the authentication cache and make sure that a database has the latest version of the logins table, execute [DBCC FLUSHAUTHCACHE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-flushauthcache-transact-sql.md).  
  
 For more information about [!INCLUDE[ssSDS](../../includes/sssds-md.md)] logins, see [Managing Databases and Logins in Windows Azure SQL Database](http://msdn.microsoft.com/library/ee336235.aspx).  
  
## Permissions  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], requires **ALTER ANY LOGIN** permission on the server or membership in the **securityadmin** fixed server role.  
  
 In [!INCLUDE[ssSDS](../../includes/sssds-md.md)], only the server-level principal login (created by the provisioning process) or members of the `loginmanager` database role in the master database can create new logins.  
  
 If the **CREDENTIAL** option is used, also requires **ALTER ANY CREDENTIAL** permission on the server.  
  
## Next Steps  
 After creating a login, the login can connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] or [!INCLUDE[ssSDS](../../includes/sssds-md.md)] but only has the permissions granted to the **public** role. Consider performing the some of the following activities.  
  
-   To connect to a database, create a database user for the login. For more information, see [CREATE USER &#40;Transact-SQL&#41;](../../t-sql/statements/create-user-transact-sql.md).  
  
-   Create a user-defined server role by using [CREATE SERVER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-role-transact-sql.md). Use **ALTER SERVER ROLE** … **ADD MEMBER** to add the new login to the user-defined server role. For more information, see [CREATE SERVER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-role-transact-sql.md) and [ALTER SERVER ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-role-transact-sql.md).  
  
-   Use **sp_addsrvrolemember** to add the login to a fixed server role. For more information, see [Server-Level Roles](../../relational-databases/security/authentication-access/server-level-roles.md) and [sp_addsrvrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addsrvrolemember-transact-sql.md).  
  
-   Use the **GRANT** statement, to grant server-level permissions to the new login or to a role containing the login. For more information, see [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md).  
  
## Examples  
  
### A. Creating a login with a password  
 The following example creates a login for a particular user and assigns a password.  
  
```  
CREATE LOGIN <login_name> WITH PASSWORD = '<enterStrongPasswordHere>';  
GO  
```  
  
### B. Creating a login with a password  
 The following example creates a login for a particular user and assigns a password. The `MUST_CHANGE` option requires users to change this password the first time they connect to the server.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```  
CREATE LOGIN <login_name> WITH PASSWORD = '<enterStrongPasswordHere>' MUST_CHANGE;  
GO  
```  
  
### C. Creating a login mapped to a credential  
 The following example creates the login for a particular user, using the user. This login is mapped to the credential.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```  
CREATE LOGIN <login_name> WITH PASSWORD = '<enterStrongPasswordHere>',   
    CREDENTIAL = <credentialName>;  
GO  
```  
  
### D. Creating a login from a certificate  
 The following example creates login for a particular user from a certificate in master.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```  
USE MASTER;  
CREATE CERTIFICATE <certificateName>  
    WITH SUBJECT = '<login_name> certificate in master database',  
    EXPIRY_DATE = '12/05/2025';  
GO  
CREATE LOGIN <login_name> FROM CERTIFICATE <certificateName>;  
GO  
```  
  
### E. Creating a login from a Windows domain account  
 The following example creates a login from a Windows domain account.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```  
CREATE LOGIN [<domainName>\<login_name>] FROM WINDOWS;  
GO  
```  
  
### F. Creating a login from a SID  
 The following example first creates a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication login and determines the SID of the login.  
  
```  
CREATE LOGIN TestLogin WITH PASSWORD = 'SuperSecret52&&';  
  
SELECT name, sid FROM sys.sql_logins WHERE name = 'TestLogin';  
GO  
```  
  
 My query returns 0x241C11948AEEB749B0D22646DB1A19F2 as the SID. Your query will return a different value. The following statements delete the login, and then recreate the login. Use the SID from your previous query.  
  
```  
DROP LOGIN TestLogin;  
GO  
  
CREATE LOGIN TestLogin   
WITH PASSWORD = 'SuperSecret52&&', SID = 0x241C11948AEEB749B0D22646DB1A19F2;  
  
SELECT * FROM sys.sql_logins WHERE name = 'TestLogin';  
GO  
```  
  
## Examples: [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### G. Creating a SQL Server authentication login with a password  
 The following example creates the login `Mary7` with password `A2c3456`.  
  
```tsql  
CREATE LOGIN Mary7 WITH PASSWORD = 'A2c3456$#' ;  
```  
  
### H. Using Options  
 The following example creates the login `Mary8` with password some of the optional arguments.  
  
```  
CREATE LOGIN Mary8 WITH PASSWORD = 'A2c3456$#' MUST_CHANGE,  
CHECK_EXPIRATION = ON,  
CHECK_POLICY = ON;  
```  
  
### I. Creating a login from a Windows domain account  
 The following example creates a login from a Windows domain account named `Mary` in the `Contoso` domain.  
  
```  
CREATE LOGIN [Contoso\Mary] FROM WINDOWS;  
GO  
```  
  
## See Also  
 [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [Password Policy](../../relational-databases/security/password-policy.md)   
 [ALTER LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/alter-login-transact-sql.md)   
 [DROP LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/drop-login-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [Create a Login](../../relational-databases/security/authentication-access/create-a-login.md)  
  
  
