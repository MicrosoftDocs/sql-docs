---
title: "ALTER LOGIN (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/06/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_LOGIN_TSQL"
  - "ALTER LOGIN"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ALTER LOGIN statement"
  - "change password"
  - "mapping logins [SQL Server]"
  - "logins [SQL Server], modifying"
  - "passwords [SQL Server], modifying"
  - "names [SQL Server], logins"
  - "modifying login accounts"
ms.assetid: e247b84e-c99e-4af8-8b50-57586e1cb1c5
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ALTER LOGIN (Transact-SQL)

Changes the properties of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login account.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
 
## Click a product!

In the following row, click whichever product name you are interested in. The click displays different content here on this webpage, appropriate for whichever product you click.

::: moniker range=">=sql-server-2016||>=sql-server-linux-2017||=sqlallproducts-allversions"

> [!div class="mx-tdCol2BreakAll"]
> ||||||
> |-|-|-|-|-|
> |**_\* SQL Server \*_**|[SQL Database<br />logical server](alter-login-transact-sql.md?view=azuresqldb-current)|[SQL Database<br />Managed Instance](alter-login-transact-sql.md?view=azuresqldb-mi-current)|[SQL Data<br />Warehouse](alter-login-transact-sql.md?view=azure-sqldw-latest)|[Parallel<br />Data Warehouse](alter-login-transact-sql.md?view=aps-pdw-2016)

&nbsp;

## SQL Server
 
## Syntax  
  
```  
-- Syntax for SQL Server  
  
ALTER LOGIN login_name   
    {   
    <status_option>   
    | WITH <set_option> [ ,... ]  
    | <cryptographic_credential_option>  
    }   
[;]  
  
<status_option> ::=  
        ENABLE | DISABLE  
  
<set_option> ::=              
    PASSWORD = 'password' | hashed_password HASHED  
    [   
      OLD_PASSWORD = 'oldpassword'  
      | <password_option> [<password_option> ]   
    ]  
    | DEFAULT_DATABASE = database  
    | DEFAULT_LANGUAGE = language  
    | NAME = login_name  
    | CHECK_POLICY = { ON | OFF }  
    | CHECK_EXPIRATION = { ON | OFF }  
    | CREDENTIAL = credential_name  
    | NO CREDENTIAL  
  
<password_option> ::=   
    MUST_CHANGE | UNLOCK  
  
<cryptographic_credentials_option> ::=   
    ADD CREDENTIAL credential_name  
  | DROP CREDENTIAL credential_name  
```  
  
  
## Arguments  
 *login_name*  
 Specifies the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login that is being changed. Domain logins must be enclosed in brackets in the format [domain\user].  
  
 ENABLE | DISABLE  
 Enables or disables this login. Disabling a login does not affect the behavior of logins that are already connected. (Use the `KILL` statement to terminate an existing connections.) Disabled logins retain their permissions and can still be impersonated.  
  
 PASSWORD **='**_password_**'**  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies the password for the login that is being changed. Passwords are case-sensitive.  
  
 PASSWORD **=**_hashed\_password_  
 Applies to the HASHED keyword only. Specifies the hashed value of the password for the login that is being created.  
  
> [!IMPORTANT]  
>  When a login (or a contained database user) connects and is authenticated, the connection caches identity information about the login. For a Windows Authentication login, this includes information about membership in Windows groups. The identity of the login remains authenticated as long as the connection is maintained. To force changes in the identity, such as a password reset or change in Windows group membership, the login must logoff from the authentication authority (Windows or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]), and log in again. A member of the **sysadmin** fixed server role or any login with the **ALTER ANY CONNECTION** permission can use the **KILL** command to end a connection and force a login to reconnect. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] can reuse connection information when opening multiple connections to Object Explorer and Query Editor windows. Close all connections to force reconnection.  
  
 HASHED    
 Applies to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins only. Specifies that the password entered after the PASSWORD argument is already hashed. If this option is not selected, the password is hashed before being stored in the database. This option should only be used for login synchronization between two servers. Do not use the HASHED option to routinely change passwords.  
  
 OLD_PASSWORD **='**_oldpassword_**'**  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. The current password of the login to which a new password will be assigned. Passwords are case-sensitive.  
  
 MUST_CHANGE  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. If this option is included, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will prompt for an updated password the first time the altered login is used.  
  
 DEFAULT_DATABASE **=**_database_  
 Specifies a default database to be assigned to the login.  
  
 DEFAULT_LANGUAGE **=**_language_  
 Specifies a default language to be assigned to the login. The default language for all SQL Database logins is English and cannot be changed. The default language of the `sa` login on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Linux, is English but it can be changed.  
  
 NAME = *login_name*  
 The new name of the login that is being renamed. If this is a Windows login, the SID of the Windows principal corresponding to the new name must match the SID associated with the login in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The new name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login cannot contain a backslash character (\\).  
  
 CHECK_EXPIRATION = { ON | **OFF** }  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies whether password expiration policy should be enforced on this login. The default value is OFF.  
  
 CHECK_POLICY **=** { **ON** | OFF }  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies that the Windows password policies of the computer on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running should be enforced on this login. The default value is ON.  
  
 CREDENTIAL = *credential_name*  
 The name of a credential to be mapped to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. The credential must already exist in the server. For more information, see [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md). A credential cannot be mapped to the sa login.  
  
 NO CREDENTIAL  
 Removes any existing mapping of the login to a server credential. For more information, see [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md).  
  
 UNLOCK  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies that a login that is locked out should be unlocked.  
  
 ADD CREDENTIAL  
 Adds an Extensible Key Management (EKM) provider credential to the login. For more information, see [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md).  
  
 DROP CREDENTIAL  
 Removes an Extensible Key Management (EKM) provider credential from the  login. For more information, see [Extensible Key Management &#40;EKM&#41;] (../.. /relational-databases/security/encryption/extensible-key-management-ekm.md).  
  
## Remarks  
 When CHECK_POLICY is set to ON, the HASHED argument cannot be used.  
  
 When CHECK_POLICY is changed to ON, the following behavior occurs:  
  
-   The password history is initialized with the value of the current password hash.  
  
 When CHECK_POLICY is changed to OFF, the following behavior occurs:  
  
-   CHECK_EXPIRATION is also set to OFF.  
  
-   The password history is cleared.  
  
-   The value of *lockout_time* is reset.  
  
If MUST_CHANGE is specified, CHECK_EXPIRATION and CHECK_POLICY must be set to ON. Otherwise, the statement will fail.  
  
If CHECK_POLICY is set to OFF, CHECK_EXPIRATION cannot be set to ON. An ALTER LOGIN statement that has this combination of options will fail.  
  
You cannot use ALTER_LOGIN with the DISABLE argument to deny access to a Windows group. For example, ALTER_LOGIN [*domain\group*] DISABLE will return the following error message:  
  
 `"Msg 15151, Level 16, State 1, Line 1
 "Cannot alter the login '*Domain\Group*', because it does not exist or you do not have permission."`
  
 This is by design.  
  
In [!INCLUDE[ssSDS](../../includes/sssds-md.md)], login data required to authenticate a connection and server-level firewall rules are temporarily cached in each database. This cache is periodically refreshed. To force a refresh of the authentication cache and make sure that a database has the latest version of the logins table, execute [DBCC FLUSHAUTHCACHE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-flushauthcache-transact-sql.md).  
  
## Permissions  
 Requires ALTER ANY LOGIN permission.  
  
 If the CREDENTIAL option is used, also requires ALTER ANY CREDENTIAL permission.  
  
 If the login that is being changed is a member of the **sysadmin** fixed server role or a grantee of CONTROL SERVER permission, also requires CONTROL SERVER permission when making the following changes:  
  
-   Resetting the password without supplying the old password.  
  
-   Enabling MUST_CHANGE, CHECK_POLICY, or CHECK_EXPIRATION.  
  
-   Changing the login name.  
  
-   Enabling or disabling the login.  
  
-   Mapping the login to a different credential.  
  
 A principal can change the password, default language, and default database for its own login.  
  
## Examples  
  
### A. Enabling a disabled login  
 The following example enables the login `Mary5`.  
  
```sql  
ALTER LOGIN Mary5 ENABLE;  
```  
  
### B. Changing the password of a login  
 The following example changes the password of login `Mary5` to a strong password.  
  
```sql  
ALTER LOGIN Mary5 WITH PASSWORD = '<enterStrongPasswordHere>';  
```  

### C. Changing the password of a login when logged in as the login 
 If you are attempting to change the password of the login that you're currently logged in with and you do not have the `ALTER ANY LOGIN` permission you must specify the `OLD_PASSWORD` option.    
  
```sql  
ALTER LOGIN Mary5 WITH PASSWORD = '<enterStrongPasswordHere>' OLD_PASSWORD = '<oldWeakPasswordHere>';  
```  

### D. Changing the name of a login  
 The following example changes the name of login `Mary5` to `John2`.  
  
```sql  
ALTER LOGIN Mary5 WITH NAME = John2;  
```  
  
### E. Mapping a login to a credential  
 The following example maps the login `John2` to the credential `Custodian04`.  
  
```sql  
ALTER LOGIN John2 WITH CREDENTIAL = Custodian04;  
```  
  
### F. Mapping a login to an Extensible Key Management credential  
 The following example maps the login `Mary5` to the EKM credential `EKMProvider1`.  
  
  
```sql  
ALTER LOGIN Mary5  
ADD CREDENTIAL EKMProvider1;  
GO  
```  
  
### F. Unlocking a login  
 To unlock a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, execute the following statement, replacing \*\*\*\* with the desired account password.  
  
  
```sql  
ALTER LOGIN [Mary5] WITH PASSWORD = '****' UNLOCK ;  

GO  
```  
  
 To unlock a login without changing the password, turn the check policy off and then on again.  
  
```sql  
ALTER LOGIN [Mary5] WITH CHECK_POLICY = OFF;  
ALTER LOGIN [Mary5] WITH CHECK_POLICY = ON;  
GO  
```  
  
### G. Changing the password of a login using HASHED  
 The following example changes the password of the `TestUser` login to an already hashed value.  
  
```sql  
ALTER LOGIN TestUser WITH   
PASSWORD = 0x01000CF35567C60BFB41EBDE4CF700A985A13D773D6B45B90900 HASHED ;  
GO  
```  
  
 
  
## See Also  
 [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md)   
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [DROP LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/drop-login-transact-sql.md)   
 [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-credential-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md)  
  
::: moniker-end

::: moniker range="=azuresqldb-current||=sqlallproducts-allversions"

> [!div class="mx-tdCol2BreakAll"]
> ||||||
> |-|-|-|-|-|
> |[SQL Server](alter-login-transact-sql.md?view=sql-server-2016)|**_\* SQL Database<br />logical server \*_**|[SQL Database<br />Managed Instance](alter-login-transact-sql.md?view=azuresqldb-mi-current)|[SQL Data<br />Warehouse](alter-login-transact-sql.md?view=azure-sqldw-latest)|[Parallel<br />Data Warehouse](alter-login-transact-sql.md?view=aps-pdw-2016)

&nbsp;

## Azure SQL Database logical server

## SQL Server
 
## Syntax  

```  
-- Syntax for Azure SQL Database and Azure SQL Data Warehouse 
  
ALTER LOGIN login_name   
  {   
      <status_option>   
    | WITH <set_option> [ ,.. .n ]   
  }   
[;]  
  
<status_option> ::=  
    ENABLE | DISABLE  
  
<set_option> ::=   
    PASSWORD ='password'   
    [  
      OLD_PASSWORD ='oldpassword'  
    ]   
    | NAME = login_name  
```  


## Arguments  
 *login_name*  
 Specifies the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login that is being changed. Domain logins must be enclosed in brackets in the format [domain\user].  
  
 ENABLE | DISABLE  
 Enables or disables this login. Disabling a login does not affect the behavior of logins that are already connected. (Use the `KILL` statement to terminate an existing connections.) Disabled logins retain their permissions and can still be impersonated.  
  
 PASSWORD **='**_password_**'**  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies the password for the login that is being changed. Passwords are case-sensitive.  
  
 Continuously active connections to SQL Database require reauthorization (performed by the Database Engine) at least every 10 hours. The Database Engine attempts reauthorization using the originally submitted password and no user input is required. For performance reasons, when a password is reset in SQL Database, the connection will not be re-authenticated, even if the connection is reset due to connection pooling. This is different from the behavior of on-premises SQL Server. If the password has been changed since the connection was initially authorized, the connection must be terminated and a new connection made using the new password. A user with the KILL DATABASE CONNECTION permission can explicitly terminate a connection to SQL Database by using the KILL command. For more information, see [KILL &#40;Transact-SQL&#41;](../../t-sql/language-elements/kill-transact-sql.md).  
  
> [!IMPORTANT]  
>  When a login (or a contained database user) connects and is authenticated, the connection caches identity information about the login. For a Windows Authentication login, this includes information about membership in Windows groups. The identity of the login remains authenticated as long as the connection is maintained. To force changes in the identity, such as a password reset or change in Windows group membership, the login must logoff from the authentication authority (Windows or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]), and log in again. A member of the **sysadmin** fixed server role or any login with the **ALTER ANY CONNECTION** permission can use the **KILL** command to end a connection and force a login to reconnect. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] can reuse connection information when opening multiple connections to Object Explorer and Query Editor windows. Close all connections to force reconnection.  
  
 OLD_PASSWORD **='**_oldpassword_**'**  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. The current password of the login to which a new password will be assigned. Passwords are case-sensitive.  
  
 NAME = *login_name*  
 The new name of the login that is being renamed. If this is a Windows login, the SID of the Windows principal corresponding to the new name must match the SID associated with the login in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The new name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login cannot contain a backslash character (\\).  
  
## Remarks  
  
 In [!INCLUDE[ssSDS](../../includes/sssds-md.md)], login data required to  authenticate a connection and server-level firewall rules are temporarily  cached in each database. This cache is periodically refreshed. To force a  refresh of the authentication cache and make sure that a database has the  latest version of the logins table, execute [DBCC FLUSHAUTHCACHE &#40; Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-flushauthcache-transact-sql.md).  
  
## Permissions  
 Requires ALTER ANY LOGIN permission.  
  
 If the login that is being changed is a member of the **sysadmin** fixed server role or a grantee of CONTROL SERVER permission, also requires CONTROL SERVER permission when making the following changes:  
  
-   Resetting the password without supplying the old password.  
  
-   Changing the login name.  
  
-   Enabling or disabling the login.  
  
-   Mapping the login to a different credential.  
  
A principal can change the password for its own login.  
  
## Examples  

These examples also include examples for using other SQL products. Please see which arguments are supported above.
  
### A. Enabling a disabled login  
 The following example enables the login `Mary5`.  
  
```sql  
ALTER LOGIN Mary5 ENABLE;  
```  
  
### B. Changing the password of a login  
 The following example changes the password of login `Mary5` to a strong password.  
  
```sql  
ALTER LOGIN Mary5 WITH PASSWORD = '<enterStrongPasswordHere>';  
```  
  
### C. Changing the name of a login  
 The following example changes the name of login `Mary5` to `John2`.  
  
```sql  
ALTER LOGIN Mary5 WITH NAME = John2;  
```  
  
### D. Mapping a login to a credential  
 The following example maps the login `John2` to the credential `Custodian04`.  
  
```sql  
ALTER LOGIN John2 WITH CREDENTIAL = Custodian04;  
```  
  
### E. Mapping a login to an Extensible Key Management credential  
 The following example maps the login `Mary5` to the EKM credential `EKMProvider1`.  
  
 
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```sql  
ALTER LOGIN Mary5  
ADD CREDENTIAL EKMProvider1;  
GO  
```  
  
### F. Unlocking a login  
 To unlock a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, execute the following statement, replacing \*\*\*\* with the desired account password.  
  
  
```sql  
ALTER LOGIN [Mary5] WITH PASSWORD = '****' UNLOCK ;  

GO  
```  
  
 To unlock a login without changing the password, turn the check policy off and then on again.  
  
```sql  
ALTER LOGIN [Mary5] WITH CHECK_POLICY = OFF;  
ALTER LOGIN [Mary5] WITH CHECK_POLICY = ON;  
GO  
```  
  
### G. Changing the password of a login using HASHED  
 The following example changes the password of the `TestUser` login to an already hashed value.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```sql  
ALTER LOGIN TestUser WITH   
PASSWORD = 0x01000CF35567C60BFB41EBDE4CF700A985A13D773D6B45B90900 HASHED ;  
GO  
```  
  
 
  
## See Also  
 [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md)   
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [DROP LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/drop-login-transact-sql.md)   
 [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-credential-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md)

::: moniker-end
::: moniker range="=azuresqldb-mi-current||=sqlallproducts-allversions"

> [!div class="mx-tdCol2BreakAll"]
> ||||||
> |-|-|-|-|-|
> |[SQL Server](alter-login-transact-sql.md?view=sql-server-2016)|[SQL Database<br />logical server](alter-login-transact-sql.md?view=azuresqldb-current)|**_\* SQL Database<br />Managed Instance \*_**|[SQL Data<br />Warehouse](alter-login-transact-sql.md?view=azure-sqldw-latest)|[Parallel<br />Data Warehouse](alter-login-transact-sql.md?view=aps-pdw-2016)

&nbsp;

## Azure SQL Database Managed Instance

## Syntax  
  
```  
-- Syntax for SQL Server and Azure SQL Database Managed Instance
  
ALTER LOGIN login_name   
    {   
    <status_option>   
    | WITH <set_option> [ ,... ]  
    | <cryptographic_credential_option>  
    }   
[;]  
  
<status_option> ::=  
        ENABLE | DISABLE  
  
<set_option> ::=              
    PASSWORD = 'password' | hashed_password HASHED  
    [   
      OLD_PASSWORD = 'oldpassword'  
      | <password_option> [<password_option> ]   
    ]  
    | DEFAULT_DATABASE = database  
    | DEFAULT_LANGUAGE = language  
    | NAME = login_name  
    | CHECK_POLICY = { ON | OFF }  
    | CHECK_EXPIRATION = { ON | OFF }  
    | CREDENTIAL = credential_name  
    | NO CREDENTIAL  
  
<password_option> ::=   
    MUST_CHANGE | UNLOCK  
  
<cryptographic_credentials_option> ::=   
    ADD CREDENTIAL credential_name  
  | DROP CREDENTIAL credential_name  
```  

> [!IMPORTANT]
> Azure AD logins for SQL Database Managed Instance is in **public preview**.
  
```  
-- Syntax for Azure SQL Database Managed Instance using Azure AD logins
  
ALTER LOGIN login_name   
  {   
      <status_option>   
    | WITH <set_option> [ ,.. .n ]   
  }   
[;]  
  
<status_option> ::=  
    ENABLE | DISABLE  
  
<set_option> ::=
     DEFAULT_DATABASE = database
   | DEFAULT_LANGUAGE = language
```


## Arguments  

### Arguments applicable to SQL and Azure AD logins
 *login_name*  
 Specifies the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login that is being changed. Azure AD logins must be specified as user@domain. For example, john.smith@contoso.com, or as the Azure AD group or application name. For Azure AD logins, the *login_name* must correspond to an existing Azure AD login created in the master database.  
  
 ENABLE | DISABLE  
 Enables or disables this login. Disabling a login does not affect the behavior of logins that are already connected. (Use the `KILL` statement to terminate an existing connection.) Disabled logins retain their permissions and can still be impersonated.  

 DEFAULT_DATABASE **=**_database_  
 Specifies a default database to be assigned to the login.  
  
 DEFAULT_LANGUAGE **=**_language_  
 Specifies a default language to be assigned to the login. The default language for all SQL Database logins is English and cannot be changed. The default language of the `sa` login on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Linux, is English but it can be changed.

### Arguments applicable only to SQL logins
  
 PASSWORD **='**_password_**'**  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies the password for the login that is being changed. Passwords are case-sensitive. Passwords also do not apply when used with external logins, like Azure AD logins.
  
 Continuously active connections to SQL Database require reauthorization (performed by the Database Engine) at least every 10 hours. The Database Engine attempts reauthorization using the originally submitted password and no user input is required. For performance reasons, when a password is reset in SQL Database, the connection will not be re-authenticated, even if the connection is reset due to connection pooling. This is different from the behavior of on-premises SQL Server. If the password has been changed since the connection was initially authorized, the connection must be terminated and a new connection made using the new password. A user with the KILL DATABASE CONNECTION permission can explicitly terminate a connection to SQL Database by using the KILL command. For more information, see [KILL &#40;Transact-SQL&#41;](../../t-sql/language-elements/kill-transact-sql.md).  
  
 PASSWORD **=**_hashed\_password_  
 Applies to the HASHED keyword only. Specifies the hashed value of the password for the login that is being created.  
  
 HASHED  
 Applies to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins only. Specifies that the password entered after the PASSWORD argument is already hashed. If this option is not selected, the password is hashed before being stored in the database. This option should only be used for login synchronization between two servers. Do not use the HASHED option to routinely change passwords.  
  
 OLD_PASSWORD **='**_oldpassword_**'**  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. The current password of the login to which a new password will be assigned. Passwords are case-sensitive.  
  
 MUST_CHANGE<br>
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. If this option is included, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will prompt for an updated password the first time the altered login is used.   
  
 NAME = *login_name*  
 The new name of the login that is being renamed. If the login is a Windows login, the SID of the Windows principal corresponding to the new name must match the SID associated with the login in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The new name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login cannot contain a backslash character (\\).  
  
 CHECK_EXPIRATION = { ON | **OFF** }  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies whether password expiration policy should be enforced on this login. The default value is OFF.  
  
 CHECK_POLICY **=** { **ON** | OFF }  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies that the Windows password policies of the computer on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running should be enforced on this login. The default value is ON.  
  
 CREDENTIAL = *credential_name*  
 The name of a credential to be mapped to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. The credential must already exist in the server. For more information, see [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md). A credential cannot be mapped to the sa login.  
  
 NO CREDENTIAL  
 Removes any existing mapping of the login to a server credential. For more information, see [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md).  
  
 UNLOCK  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies that a login that is locked out should be unlocked.  
  
 ADD CREDENTIAL  
 Adds an Extensible Key Management (EKM) provider credential to the login. For more information, see [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md).  
  
 DROP CREDENTIAL  
 Removes an Extensible Key Management (EKM) provider credential from the login. For more information, see [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md).  
  
## Remarks  
 When CHECK_POLICY is set to ON, the HASHED argument cannot be used.  
  
 When CHECK_POLICY is changed to ON, the following behavior occurs:  
  
-   The password history is initialized with the value of the current password hash.  
  
 When CHECK_POLICY is changed to OFF, the following behavior occurs:  
  
-   CHECK_EXPIRATION is also set to OFF.  
  
-   The password history is cleared.  
  
-   The value of *lockout_time* is reset.  
  
If MUST_CHANGE is specified, CHECK_EXPIRATION and CHECK_POLICY must be set to ON. Otherwise, the statement will fail.  
  
If CHECK_POLICY is set to OFF, CHECK_EXPIRATION cannot be set to ON. An ALTER LOGIN statement that has this combination of options will fail.  
  
You cannot use ALTER_LOGIN with the DISABLE argument to deny access to a Windows group. This is by design. For example, ALTER_LOGIN [*domain\group*] DISABLE will return the following error message:  
  
 `"Msg 15151, Level 16, State 1, Line 1 
  "Cannot alter the login '*Domain\Group*', because it does not exist or you do not have permission."`
  
In [!INCLUDE[ssSDS](../../includes/sssds-md.md)], login data required to authenticate a connection and server-level firewall rules are temporarily cached in each database. This cache is periodically refreshed. To force a refresh of the authentication cache and make sure that a database has the latest version of the logins table, execute [DBCC FLUSHAUTHCACHE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-flushauthcache-transact-sql.md).  
  
## Permissions  
 Requires ALTER ANY LOGIN permission.  
  
 If the CREDENTIAL option is used, also requires ALTER ANY CREDENTIAL permission.  
  
 If the login that is being changed is a member of the **sysadmin** fixed server role or a grantee of CONTROL SERVER permission, also requires CONTROL SERVER permission when making the following changes:  
  
-   Resetting the password without supplying the old password.  
  
-   Enabling MUST_CHANGE, CHECK_POLICY, or CHECK_EXPIRATION.  
  
-   Changing the login name.  
  
-   Enabling or disabling the login.  
  
-   Mapping the login to a different credential.  
  
 A principal can change the password, default language, and default database for its own login.

Only a SQL principal with `sysadmin` privileges can execute an ALTER LOGIN command against an Azure AD login.
  
## Examples

These examples also include examples for using other SQL products. Please see which arguments are supported above.
  
### A. Enabling a disabled login  
 The following example enables the login `Mary5`.  
  
```sql  
ALTER LOGIN Mary5 ENABLE;  
```  
  
### B. Changing the password of a login  
 The following example changes the password of login `Mary5` to a strong password.  
  
```sql  
ALTER LOGIN Mary5 WITH PASSWORD = '<enterStrongPasswordHere>';  
```  
  
### C. Changing the name of a login  
 The following example changes the name of login `Mary5` to `John2`.  
  
```sql  
ALTER LOGIN Mary5 WITH NAME = John2;  
```  
  
### D. Mapping a login to a credential  
 The following example maps the login `John2` to the credential `Custodian04`.  
  
```sql  
ALTER LOGIN John2 WITH CREDENTIAL = Custodian04;  
```  
  
### E. Mapping a login to an Extensible Key Management credential  
 The following example maps the login `Mary5` to the EKM credential `EKMProvider1`.  
  
 
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], and Azure SQL Database Managed Instance.
  
```sql  
ALTER LOGIN Mary5  
ADD CREDENTIAL EKMProvider1;  
GO  
```  
  
### F. Unlocking a login  
 To unlock a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, execute the following statement, replacing \*\*\*\* with the desired account password.  
  
  
```sql  
ALTER LOGIN [Mary5] WITH PASSWORD = '****' UNLOCK ;  

GO  
```  
  
 To unlock a login without changing the password, turn the check policy off and then on again.  
  
```sql  
ALTER LOGIN [Mary5] WITH CHECK_POLICY = OFF;  
ALTER LOGIN [Mary5] WITH CHECK_POLICY = ON;  
GO  
```  
  
### G. Changing the password of a login using HASHED  
 The following example changes the password of the `TestUser` login to an already hashed value.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], and Azure SQL Database Managed Instance.
  
```sql  
ALTER LOGIN TestUser WITH   
PASSWORD = 0x01000CF35567C60BFB41EBDE4CF700A985A13D773D6B45B90900 HASHED ;  
GO  
```

### H. Disabling the login of an Azure AD user
 The following example disables the login of an Azure AD user, joe@contoso.com.

```sql
ALTER LOGIN [joe@contoso.com] DISABLE
```

## See Also  
 [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md)   
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [DROP LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/drop-login-transact-sql.md)   
 [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-credential-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md) 

::: moniker-end
::: moniker range="=azure-sqldw-latest||=sqlallproducts-allversions"

> [!div class="mx-tdCol2BreakAll"]
> ||||||
> |-|-|-|-|-|
> |[SQL Server](alter-login-transact-sql.md?view=sql-server-2016)|[SQL Database<br />logical server](alter-login-transact-sql.md?view=azuresqldb-current)|[SQL Database<br />Managed Instance](alter-login-transact-sql.md?view=azuresqldb-mi-current)|**_\* SQL Data<br />Warehouse \*_**|[Parallel<br />Data Warehouse](alter-login-transact-sql.md?view=aps-pdw-2016)

&nbsp;

## Azure SQL Data Warehouse


 
## Syntax  
  
```  
-- Syntax for Azure SQL Database and Azure SQL Data Warehouse 
  
ALTER LOGIN login_name   
  {   
      <status_option>   
    | WITH <set_option> [ ,.. .n ]   
  }   
[;]  
  
<status_option> ::=  
    ENABLE | DISABLE  
  
<set_option> ::=   
    PASSWORD ='password'   
    [  
      OLD_PASSWORD ='oldpassword'  
    ]   
    | NAME = login_name  
```  

  
## Arguments  
 *login_name*  
 Specifies the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login that is being changed. Domain logins must be enclosed in brackets in the format [domain\user].  
  
 ENABLE | DISABLE  
 Enables or disables this login. Disabling a login does not affect the behavior of logins that are already connected. (Use the `KILL` statement to terminate an existing connections.) Disabled logins retain their permissions and can still be impersonated.  
  
 PASSWORD **='**_password_**'**  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies the password for the login that is being changed. Passwords are case-sensitive.  
  
 Continuously active connections to SQL Database require reauthorization (performed by the Database Engine) at least every 10 hours. The Database Engine attempts reauthorization using the originally submitted password and no user input is required. For performance reasons, when a password is reset in SQL Database, the connection will not be re-authenticated, even if the connection is reset due to connection pooling. This is different from the behavior of on-premises SQL Server. If the password has been changed since the connection was initially authorized, the connection must be terminated and a new connection made using the new password. A user with the KILL DATABASE CONNECTION permission can explicitly terminate a connection to SQL Database by using the KILL command. For more information, see [KILL &#40;Transact-SQL&#41;](../../t-sql/language-elements/kill-transact-sql.md).  
  
> [!IMPORTANT]  
>  When a login (or a contained database user) connects and is authenticated, the connection caches identity information about the login. For a Windows Authentication login, this includes information about membership in Windows groups. The identity of the login remains authenticated as long as the connection is maintained. To force changes in the identity, such as a password reset or change in Windows group membership, the login must logoff from the authentication authority (Windows or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]), and log in again. A member of the **sysadmin** fixed server role or any login with the **ALTER ANY CONNECTION** permission can use the **KILL** command to end a connection and force a login to reconnect. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] can reuse connection information when opening multiple connections to Object Explorer and Query Editor windows. Close all connections to force reconnection.  
  
 OLD_PASSWORD **='**_oldpassword_**'**  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. The current password of the login to which a new password will be assigned. Passwords are case-sensitive.  
  
 NAME = *login_name*  
 The new name of the login that is being renamed. If this is a Windows login, the SID of the Windows principal corresponding to the new name must match the SID associated with the login in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The new name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login cannot contain a backslash character (\\).  
  
## Remarks  
  
 In [!INCLUDE[ssSDS](../../includes/sssds-md.md)], login data required to  authenticate a connection and server-level firewall rules are temporarily  cached in each database. This cache is periodically refreshed. To force a  refresh of the authentication cache and make sure that a database has the  latest version of the logins table, execute [DBCC FLUSHAUTHCACHE &#40; Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-flushauthcache-transact-sql.md).  
  
## Permissions  
 Requires ALTER ANY LOGIN permission.  
  
 If the login that is being changed is a member of the **sysadmin** fixed server role or a grantee of CONTROL SERVER permission, also requires CONTROL SERVER permission when making the following changes:  
  
-   Resetting the password without supplying the old password.  
  
-   Changing the login name.  
  
-   Enabling or disabling the login.  
  
-   Mapping the login to a different credential.  
  
A principal can change the password for its own login.  
  
## Examples  

These examples also include examples for using other SQL products. Please see which arguments are supported above.
  
### A. Enabling a disabled login  
 The following example enables the login `Mary5`.  
  
```sql  
ALTER LOGIN Mary5 ENABLE;  
```  
  
### B. Changing the password of a login  
 The following example changes the password of login `Mary5` to a strong password.  
  
```sql  
ALTER LOGIN Mary5 WITH PASSWORD = '<enterStrongPasswordHere>';  
```  
  
### C. Changing the name of a login  
 The following example changes the name of login `Mary5` to `John2`.  
  
```sql  
ALTER LOGIN Mary5 WITH NAME = John2;  
```  
  
### D. Mapping a login to a credential  
 The following example maps the login `John2` to the credential `Custodian04`.  
  
```sql  
ALTER LOGIN John2 WITH CREDENTIAL = Custodian04;  
```  
  
### E. Mapping a login to an Extensible Key Management credential  
 The following example maps the login `Mary5` to the EKM credential `EKMProvider1`.  
  
 
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```sql  
ALTER LOGIN Mary5  
ADD CREDENTIAL EKMProvider1;  
GO  
```  
  
### F. Unlocking a login  
 To unlock a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, execute the following statement, replacing \*\*\*\* with the desired account password.  
  
  
```sql  
ALTER LOGIN [Mary5] WITH PASSWORD = '****' UNLOCK ;  

GO  
```  
  
 To unlock a login without changing the password, turn the check policy off and then on again.  
  
```sql  
ALTER LOGIN [Mary5] WITH CHECK_POLICY = OFF;  
ALTER LOGIN [Mary5] WITH CHECK_POLICY = ON;  
GO  
```  
  
### G. Changing the password of a login using HASHED  
 The following example changes the password of the `TestUser` login to an already hashed value.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```sql  
ALTER LOGIN TestUser WITH   
PASSWORD = 0x01000CF35567C60BFB41EBDE4CF700A985A13D773D6B45B90900 HASHED ;  
GO  
```  
  
 
  
## See Also  
 [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md)   
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [DROP LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/drop-login-transact-sql.md)   
 [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-credential-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md) 

::: moniker-end
::: moniker range=">=aps-pdw-2016||=sqlallproducts-allversions"

> [!div class="mx-tdCol2BreakAll"]
> ||||||
> |-|-|-|-|-|
> |[SQL Server](alter-login-transact-sql.md?view=sql-server-2016)|[SQL Database<br />logical server](alter-login-transact-sql.md?view=azuresqldb-current)|[SQL Database<br />Managed Instance](alter-login-transact-sql.md?view=azuresqldb-mi-current)|[SQL Data<br />Warehouse](alter-login-transact-sql.md?view=azure-sqldw-latest)|**_\* Parallel<br />Data Warehouse \*_**

&nbsp;

## Parallel Data Warehouse


 
## Syntax  
  
```  
-- Syntax for Parallel Data Warehouse  
  
ALTER LOGIN login_name   
    {   
    <status_option>   
    | WITH <set_option> [ ,... ]  
    }   
  
<status_option> ::=ENABLE | DISABLE  
  
<set_option> ::=              
    PASSWORD ='password'   
    [   
      OLD_PASSWORD ='oldpassword'  
      | <password_option> [<password_option> ]   
    ]  
    | NAME = login_name  
    | CHECK_POLICY = { ON | OFF }  
    | CHECK_EXPIRATION = { ON | OFF }   
      
<password_option> ::=   
    MUST_CHANGE | UNLOCK  
```  
  
## Arguments  
 *login_name*  
 Specifies the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login that is being changed. Domain logins must be enclosed in brackets in the format [domain\user].  
  
 ENABLE | DISABLE  
 Enables or disables this login. Disabling a login does not affect the behavior of logins that are already connected. (Use the `KILL` statement to terminate an existing connection.) Disabled logins retain their permissions and can still be impersonated.  
  
 PASSWORD **='**_password_**'**  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies the password for the login that is being changed. Passwords are case-sensitive.  
  
> [!IMPORTANT]  
>  When a login (or a contained database user) connects and is authenticated, the connection caches identity information about the login. For a Windows Authentication login, this includes information about membership in Windows groups. The identity of the login remains authenticated as long as the connection is maintained. To force changes in the identity, such as a password reset or change in Windows group membership, the login must logoff from the authentication authority (Windows or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]), and log in again. A member of the **sysadmin** fixed server role or any login with the **ALTER ANY CONNECTION** permission can use the **KILL** command to end a connection and force a login to reconnect. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] can reuse connection information when opening multiple connections to Object Explorer and Query Editor windows. Close all connections to force reconnection.  
  
 OLD_PASSWORD **='**_oldpassword_**'**  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. The current password of the login to which a new password will be assigned. Passwords are case-sensitive.  
  
 MUST_CHANGE  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. If this option is included, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will prompt for an updated password the first time the altered login is used.  
  
 NAME = *login_name*  
 The new name of the login that is being renamed. If the login is a Windows login, the SID of the Windows principal corresponding to the new name must match the SID associated with the login in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The new name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login cannot contain a backslash character (\\).  
  
 CHECK_EXPIRATION = { ON | **OFF** }    
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies whether password expiration policy should be enforced on this login. The default value is OFF.  
  
 CHECK_POLICY **=** { **ON** | OFF }  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies that the Windows password policies of the computer on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running should be enforced on this login. The default value is ON.  
  
 UNLOCK  
 Applies only to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies that a login that is locked out should be unlocked.  
  
## Remarks  
 When CHECK_POLICY is set to ON, the HASHED argument cannot be used.  
  
 When CHECK_POLICY is changed to ON, the following behavior occurs:  
  
-   The password history is initialized with the value of the current password hash.  
  
 When CHECK_POLICY is changed to OFF, the following behavior occurs:  
  
-   CHECK_EXPIRATION is also set to OFF.  
  
-   The password history is cleared.  
  
-   The value of *lockout_time* is reset.  
  
If MUST_CHANGE is specified, CHECK_EXPIRATION and CHECK_POLICY must be set to ON. Otherwise, the statement will fail.  
  
If CHECK_POLICY is set to OFF, CHECK_EXPIRATION cannot be set to ON. An ALTER LOGIN statement that has this combination of options will fail.  
  
You cannot use ALTER_LOGIN with the DISABLE argument to deny access to a Windows group. This is by design. For example, ALTER_LOGIN [*domain\group*] DISABLE will return the following error message:  
  
 `"Msg 15151, Level 16, State 1, Line 1 
  "Cannot alter the login '*Domain\Group*', because it does not exist or you do not have permission."` 
  
In [!INCLUDE[ssSDS](../../includes/sssds-md.md)], login data required to authenticate a connection and server-level firewall rules are temporarily cached in each database. This cache is periodically refreshed. To force a refresh of the authentication cache and make sure that a database has the latest version of the logins table, execute [DBCC FLUSHAUTHCACHE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-flushauthcache-transact-sql.md).  
  
## Permissions  
 Requires ALTER ANY LOGIN permission.  
  
 If the CREDENTIAL option is used, also requires ALTER ANY CREDENTIAL permission.  
  
 If the login that is being changed is a member of the **sysadmin** fixed server role or a grantee of CONTROL SERVER permission, also requires CONTROL SERVER permission when making the following changes:  
  
-   Resetting the password without supplying the old password.  
  
-   Enabling MUST_CHANGE, CHECK_POLICY, or CHECK_EXPIRATION.  
  
-   Changing the login name.  
  
-   Enabling or disabling the login.  
  
-   Mapping the login to a different credential.  
  
A principal can change the password, default language, and default database for its own login.  
  
## Examples  

These examples also include examples for using other SQL products. Please see which arguments are supported above.
  
### A. Enabling a disabled login  
 The following example enables the login `Mary5`.  
  
```sql  
ALTER LOGIN Mary5 ENABLE;  
```  
  
### B. Changing the password of a login  
 The following example changes the password of login `Mary5` to a strong password.  
  
```sql  
ALTER LOGIN Mary5 WITH PASSWORD = '<enterStrongPasswordHere>';  
```  
  
### C. Changing the name of a login  
 The following example changes the name of login `Mary5` to `John2`.  
  
```sql  
ALTER LOGIN Mary5 WITH NAME = John2;  
```  
  
### D. Mapping a login to a credential  
 The following example maps the login `John2` to the credential `Custodian04`.  
  
```sql  
ALTER LOGIN John2 WITH CREDENTIAL = Custodian04;  
```  
  
### E. Mapping a login to an Extensible Key Management credential  
 The following example maps the login `Mary5` to the EKM credential `EKMProvider1`.  
  
 
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```sql  
ALTER LOGIN Mary5  
ADD CREDENTIAL EKMProvider1;  
GO  
```  
  
### F. Unlocking a login  
 To unlock a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, execute the following statement, replacing \*\*\*\* with the desired account password.  
  
  
```sql  
ALTER LOGIN [Mary5] WITH PASSWORD = '****' UNLOCK ;  

GO  
```  
  
 To unlock a login without changing the password, turn the check policy off and then on again.  
  
```sql  
ALTER LOGIN [Mary5] WITH CHECK_POLICY = OFF;  
ALTER LOGIN [Mary5] WITH CHECK_POLICY = ON;  
GO  
```
  
### G. Changing the password of a login using HASHED  
 The following example changes the password of the `TestUser` login to an already hashed value.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```sql
ALTER LOGIN TestUser WITH   
PASSWORD = 0x01000CF35567C60BFB41EBDE4CF700A985A13D773D6B45B90900 HASHED ;  
GO  
```
  

## See Also  
 [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md)   
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [DROP LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/drop-login-transact-sql.md)   
 [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-credential-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md)

::: moniker-end
