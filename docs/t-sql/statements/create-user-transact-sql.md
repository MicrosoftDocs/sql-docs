---
title: "CREATE USER (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/03/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "WITHOUT_LOGIN_TSQL"
  - "CREATE_USER_TSQL"
  - "SQL13.SWB.DATABASEUSER.OWNEDSCHEMAS.F1"
  - "WITHOUT LOGIN"
  - "CREATE USER"
  - "ALLOW_ENCRYPTED_VALUE_MODIFICATIONS"
  - "ALLOW_ENCRYPTED_VALUE_MODIFICATIONS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "adding users"
  - "WITHOUT LOGIN [SQL Server]"
  - "CREATE USER statement"
  - "database user additions [SQL Server]"
  - "USER WITHOUT LOGIN [SQL Server]"
  - "users [SQL Server], adding"
  - "users [SQL Server]"
ms.assetid: 01de7476-4b25-4d58-85b7-1118fe64aa80
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CREATE USER (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Adds a user to the current database. The 12 types of users are listed below with a sample of the most basic syntax:  
  
**Users based on logins in master** - This is the most common type of user.  
  
-   User based on a login based on a Windows Active Directory account. `CREATE USER [Contoso\Fritz];`     
-   User based on a login based on a Windows group. `CREATE USER [Contoso\Sales];`   
-   User based on a login using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication. `CREATE USER Mary;`  
  
**Users that authenticate at the database** - Recommended to help make your database more portable.  
 Always allowed in [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)]. Only allowed in a contained database in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)].  
  
-   User based on a Windows user that has no login. `CREATE USER [Contoso\Fritz];`    
-   User based on a Windows group that has no login. `CREATE USER [Contoso\Sales];`  
-   User in [!INCLUDE[ssSDS](../../includes/sssds-md.md)] or [!INCLUDE[ssSDW_md](../../includes/sssdw-md.md)] based on an Azure Active Directory user. `CREATE USER [Contoso\Fritz] FROM EXTERNAL PROVIDER;`     

-   Contained database user with password. (Not available in [!INCLUDE[ssSDW_md](../../includes/sssdw-md.md)].) `CREATE USER Mary WITH PASSWORD = '********';`   
  
**Users based on Windows principals that connect through Windows group logins**  
  
-   User based on a Windows user that has no login, but can connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] through membership in a Windows group. `CREATE USER [Contoso\Fritz];`  
  
-   User based on a Windows group that has no login, but can connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] through membership in a different Windows group. `CREATE USER [Contoso\Fritz];`  
  
**Users that cannot authenticate** - These users cannot login to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
-   User without a login. Cannot login but can be granted permissions. `CREATE USER CustomApp WITHOUT LOGIN;`    
-   User based on a certificate. Cannot login but can be granted permissions and can sign modules. `CREATE USER TestProcess FOR CERTIFICATE CarnationProduction50;`  
-   User based on an asymmetric key. Cannot login but can be granted permissions and can sign modules. `CREATE User TestProcess FROM ASYMMETRIC KEY PacificSales09;`   
 
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Database, and Azure SQL Database managed instance
  
-- Syntax Users based on logins in master  
CREATE USER user_name   
    [   
        { FOR | FROM } LOGIN login_name   
    ]  
    [ WITH <limited_options_list> [ ,... ] ]   
[ ; ]  
  
-- Users that authenticate at the database  
CREATE USER   
    {  
      windows_principal [ WITH <options_list> [ ,... ] ]  
  
    | user_name WITH PASSWORD = 'password' [ , <options_list> [ ,... ]   
    | Azure_Active_Directory_principal FROM EXTERNAL PROVIDER   
    }  
  
 [ ; ]  
  
-- Users based on Windows principals that connect through Windows group logins  
CREATE USER   
    {   
          windows_principal [ { FOR | FROM } LOGIN windows_principal ]  
        | user_name { FOR | FROM } LOGIN windows_principal  
}  
    [ WITH <limited_options_list> [ ,... ] ]   
[ ; ]  
  
-- Users that cannot authenticate   
CREATE USER user_name   
    {  
         WITHOUT LOGIN [ WITH <limited_options_list> [ ,... ] ]  
       | { FOR | FROM } CERTIFICATE cert_name   
       | { FOR | FROM } ASYMMETRIC KEY asym_key_name   
    }  
 [ ; ]  
  
<options_list> ::=  
      DEFAULT_SCHEMA = schema_name  
    | DEFAULT_LANGUAGE = { NONE | lcid | language name | language alias }  
    | SID = sid   
    | ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = [ ON | OFF ] ]  
  
<limited_options_list> ::=  
      DEFAULT_SCHEMA = schema_name ]   
    | ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = [ ON | OFF ] ]  
  
-- SQL Database syntax when connected to a federation member  
CREATE USER user_name  
[;]

-- Syntax for users based on Azure AD logins for Azure SQL Database managed instance
CREATE USER user_name   
    [   { FOR | FROM } LOGIN login_name  ]  
    | FROM EXTERNAL PROVIDER
    [ WITH <limited_options_list> [ ,... ] ]   
[ ; ]  

<limited_options_list> ::=  
      DEFAULT_SCHEMA = schema_name 
    | DEFAULT_LANGUAGE = { NONE | lcid | language name | language alias }   
    | ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = [ ON | OFF ] ] 
```

> [!IMPORTANT]
> Azure AD logins for SQL Database managed instance is in **public preview**.

```  
-- Syntax for Azure SQL Data Warehouse  
  
CREATE USER user_name   
    [ { { FOR | FROM } { LOGIN login_name }   
      | WITHOUT LOGIN  
    ]   
    [ WITH DEFAULT_SCHEMA = schema_name ]  
[;]

CREATE USER Azure_Active_Directory_principal FROM EXTERNAL PROVIDER  
    [ WITH DEFAULT_SCHEMA = schema_name ]  
[;]
``` 
  
```  
-- Syntax for Parallel Data Warehouse  
  
CREATE USER user_name   
    [ { { FOR | FROM }  
      {   
        LOGIN login_name   
      }   
      | WITHOUT LOGIN  
    ]   
    [ WITH DEFAULT_SCHEMA = schema_name ]  
[;]  
```  
  
## Arguments  
 *user_name*  
 Specifies the name by which the user is identified inside this database. *user_name* is a **sysname**. It can be up to 128 characters long. When creating a user based on a Windows principal, the Windows principal name becomes the user name unless another user name is specified.  
  
 LOGIN *login_name*  
 Specifies the login for which the database user is being created. *login_name* must be a valid login in the server. Can be a login based on a Windows principal (user or group), or a login using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication. When this SQL Server login enters the database, it acquires the name and ID of the database user that is being created. When creating a login mapped from a Windows principal, use the format **[**_\<domainName\>_**\\**_\<loginName\>_**]**. For examples, see [Syntax Summary](#SyntaxSummary).  
  
 If the CREATE USER statement is the only statement in a SQL batch, Windows Azure SQL Database supports the WITH LOGIN clause. If the CREATE USER statement is not the only statement in a SQL batch or is executed in dynamic SQL, the WITH LOGIN clause is not supported.  
  
 WITH DEFAULT_SCHEMA = *schema_name*  
 Specifies the first schema that will be searched by the server when it resolves the names of objects for this database user.  
  
 '*windows_principal*'  
 Specifies the Windows principal for which the database user is being created. The *windows_principal* can be a Windows user, or a Windows group. The user will be created even if the *windows_principal* does not have a login. When connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], if the *windows_principal* does not have a login, the Windows principal must authenticate at the [!INCLUDE[ssDE](../../includes/ssde-md.md)] through membership in a Windows group that has a login, or the connection string must specify the contained database as the initial catalog. When creating a user from a Windows principal, use the format **[**_\<domainName\>_**\\**_\<loginName\>_**]**. For examples, see [Syntax Summary](#SyntaxSummary). Users based on Active Directory users, are limited to names of fewer than 21 characters.
  
 '*Azure_Active_Directory_principal*'  
 **Applies to**: [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)], [!INCLUDE[ssSDW_md](../../includes/sssdw-md.md)].  
  
 Specifies the Azure Active Directory principal for which the database user is being created. The *Azure_Active_Directory_principal* can be an Azure Active Directory user, an Azure Active Directory group, or an Azure Active Directory application. (Azure Active Directory users cannot have Windows Authentication logins in [!INCLUDE[ssSDS](../../includes/sssds-md.md)]; only database users.) The connection string must specify the contained database as the initial catalog.

 For users, you use the full alias of their domain principal.   
 
-   `CREATE USER [bob@contoso.com] FROM EXTERNAL PROVIDER;`  
  
-   `CREATE USER [alice@fabrikam.onmicrosoft.com] FROM EXTERNAL PROVIDER;`

 For security groups, you use the *Display Name* of the security group. For the *Nurses* security group, you would use:  
  
-   `CREATE USER [Nurses] FROM EXTERNAL PROVIDER;`  
  
 For more information, see [Connecting to SQL Database By Using Azure Active Directory Authentication](https://azure.microsoft.com/documentation/articles/sql-database-aad-authentication).  
  
WITH PASSWORD = '*password*'  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Can only be used in a contained database. Specifies the password for the user that is being created. Beginning with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], stored password information is calculated using SHA-512 of the salted password.  
  
WITHOUT LOGIN  
 Specifies that the user should not be mapped to an existing login.  
  
CERTIFICATE *cert_name*  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Specifies the certificate for which the database user is being created.  
  
ASYMMETRIC KEY *asym_key_name*  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Specifies the asymmetric key for which the database user is being created.  
  
DEFAULT_LANGUAGE = *{ NONE | \<lcid> | \<language name> | \<language alias> }*  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)],   [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Specifies the default language for the new user. If a default language is specified for the user and the default language of the database is later changed, the users default language remains as specified. If no default language is specified, the default language for the user will be the default language of the database. If the default language for the user is not specified and the default language of the database is later changed, the default language of the user will change to the new default language for the database.  
  
> [!IMPORTANT]  
>  *DEFAULT_LANGUAGE* is used only for a contained database user.  
  
SID = *sid*  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Applies only to users with passwords ( [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication) in a contained database. Specifies the SID of the new database user. If this option is not selected, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically assigns a SID. Use the SID parameter to create users in multiple databases that have the same identity (SID). This is useful when creating users in multiple databases to prepare for Always On failover. To determine the SID of a user, query sys.database_principals.  
  
ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = [ ON | **OFF** ]  
 **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
 Suppresses cryptographic metadata checks on the server in bulk copy operations. This  enables the user to bulk copy encrypted data between tables or databases, without decrypting the data. The default is OFF.  
  
> [!WARNING]  
>  Improper use of this option can lead to data corruption. For more information, see [Migrate Sensitive Data Protected by Always Encrypted](../../relational-databases/security/encryption/migrate-sensitive-data-protected-by-always-encrypted.md).  
  
## Remarks  
 If FOR LOGIN is omitted, the new database user will be mapped to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login with the same name.  
  
 The default schema will be the first schema that will be searched by the server when it resolves the names of objects for this database user. Unless otherwise specified, the default schema will be the owner of objects created by this database user.  
  
 If the user has a default schema, that default schema will used. If the user does not have a default schema, but the user is a member of a group that has a default schema, the default schema of the group will be used. If the user does not have a default schema, and is a member of more than one group, the default schema for the user will be that of the Windows group with the lowest principal_id and an explicitly set default schema. (It is not possible to explicitly select one of the available default schemas as the preferred schema.) If no default schema can be determined for a user, the **dbo** schema will be used.  
  
 DEFAULT_SCHEMA can be set before the schema that it points to is created.  
  
 DEFAULT_SCHEMA cannot be specified when you are creating a user mapped to a certificate, or an asymmetric key.  
  
 The value of DEFAULT_SCHEMA is ignored if the user is a member of the sysadmin fixed server role. All members of the sysadmin fixed server role have a default schema of `dbo`.  
  
 The WITHOUT LOGIN clause creates a user that is not mapped to a SQL Server login. It can connect to other databases as guest. Permissions can be assigned to this user without login and when the security context is changed to a user without login, the original users receives the permissions of the user without login. See example [D. Creating and using a user without a login](#withoutLogin).  
  
 Only users that are mapped to Windows principals can contain the backslash character (**\\**).
  
 CREATE USER cannot be used to create a guest user because the guest user already exists inside every database. You can enable the guest user by granting it CONNECT permission, as shown:  
  
```  
GRANT CONNECT TO guest;  
GO  
```  
  
 Information about database users is visible in the [sys.database_principals](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md) catalog view.

A new syntax extension, **FROM EXTERNAL PROVIDER** is available for creating server-level Azure AD logins in SQL Database managed instance. Azure AD logins allow database-level Azure AD principals to be mapped to server-level Azure AD logins. To create an Azure AD user from an Azure AD login use the following syntax:

`CREATE USER [AAD_principal] FROM LOGIN [Azure AD login]`

When creating the user in the SQL Database managed instance database, the login_name must correspond to an existing Azure AD login, or else using the **FROM EXTERNAL PROVIDER** clause will only create an Azure AD user without a login in the master database. For example, this command will create a contained user:

`CREATE USER [bob@contoso.com] FROM EXTERNAL PROVIDER`
  
##  <a name="SyntaxSummary"></a> Syntax Summary  
 **Users based on logins in master**  
  
 The following list shows possible syntax for users based on logins. The default schema options are not listed.  
  
-   `CREATE USER [Domain1\WindowsUserBarry]`  
-   `CREATE USER [Domain1\WindowsUserBarry] FOR LOGIN Domain1\WindowsUserBarry`  
-   `CREATE USER [Domain1\WindowsUserBarry] FROM LOGIN Domain1\WindowsUserBarry`  
-   `CREATE USER [Domain1\WindowsGroupManagers]`  
-   `CREATE USER [Domain1\WindowsGroupManagers] FOR LOGIN [Domain1\WindowsGroupManagers]`  
-   `CREATE USER [Domain1\WindowsGroupManagers] FROM LOGIN [Domain1\WindowsGroupManagers]`  
-   `CREATE USER SQLAUTHLOGIN`  
-   `CREATE USER SQLAUTHLOGIN FOR LOGIN SQLAUTHLOGIN`  
-   `CREATE USER SQLAUTHLOGIN FROM LOGIN SQLAUTHLOGIN`  
  
**Users that authenticate at the database**  
  
 The following list shows possible syntax for users that can only be used in a contained database. The users created will not be related to any logins in the **master** database. The default schema and language options are not listed.  
  
> [!IMPORTANT]  
>  This syntax grants users access to the database and also grants new access to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   `CREATE USER [Domain1\WindowsUserBarry]`  
-   `CREATE USER [Domain1\WindowsGroupManagers]`  
-   `CREATE USER Barry WITH PASSWORD = 'sdjklalie8rew8337!$d'`  
  
**Users based on Windows principals without logins in master**  
  
 The following list shows possible syntax for users that have access to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] through a Windows group but do not have a login in **master**. This syntax can be used in all types of databases. The default schema and language options are not listed.  
  
 This syntax is similar to users based on logins in master, but this category of user does not have a login in master. The user must have access to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] through a Windows group login.  
  
 This syntax is similar to contained database users based on Windows principals, but this category of user does not get new access to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   `CREATE USER [Domain1\WindowsUserBarry]`  
-   `CREATE USER [Domain1\WindowsUserBarry] FOR LOGIN Domain1\WindowsUserBarry`  
-   `CREATE USER [Domain1\WindowsUserBarry] FROM LOGIN Domain1\WindowsUserBarry`  
-   `CREATE USER [Domain1\WindowsGroupManagers]`  
-   `CREATE USER [Domain1\WindowsGroupManagers] FOR LOGIN [Domain1\WindowsGroupManagers]`  
-   `CREATE USER [Domain1\WindowsGroupManagers] FROM LOGIN [Domain1\WindowsGroupManagers]`  
  
**Users that cannot authenticate**  
  
 The following list shows possible syntax for users that cannot login to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   `CREATE USER RIGHTSHOLDER WITHOUT LOGIN`  
-   `CREATE USER CERTUSER FOR CERTIFICATE SpecialCert`  
-   `CREATE USER CERTUSER FROM CERTIFICATE SpecialCert`  
-   `CREATE USER KEYUSER FOR ASYMMETRIC KEY SecureKey`  
-   `CREATE USER KEYUSER FROM ASYMMETRIC KEY SecureKey`  
  
## Security  
 Creating a user grants access to a database but does not automatically grant any access to the objects in a database. After creating a user, common actions are to add users to database roles that have permission to access database objects, or grant object permissions to the user. For information about designing a permissions system, see [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md).  
  
### Special Considerations for Contained Databases  
 When connecting to a contained database, if the user does not have a login in the **master** database, the connection string must include the contained database name as the initial catalog. The initial catalog parameter is always required for a contained database user with password.  
  
 In a contained database, creating users helps separate the database from the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] so that the database can easily be moved to another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Contained Databases](../../relational-databases/databases/contained-databases.md) and [Contained Database Users - Making Your Database Portable](../../relational-databases/security/contained-database-users-making-your-database-portable.md). To change a database user from a user based on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication login to a contained database user with password, see [sp_migrate_user_to_contained &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-migrate-user-to-contained-transact-sql.md).  
  
 In a contained database, users do not have to have logins in the **master** database. [!INCLUDE[ssDE](../../includes/ssde-md.md)] administrators should understand that access to a contained database can be granted at the database level, instead of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] level. For more information, see [Security Best Practices with Contained Databases](../../relational-databases/databases/security-best-practices-with-contained-databases.md).  
  
 When using contained database users on [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], configure access using a database-level firewall rule, instead of a server-level firewall rule. For more information, see [sp_set_database_firewall_rule &#40;Azure SQL Database&#41;](../../relational-databases/system-stored-procedures/sp-set-database-firewall-rule-azure-sql-database.md).
 
For [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] and [!INCLUDE[ssSDW_md](../../includes/sssdw-md.md)] contained database users, SSMS can support Multi-Factor Authentication. For more information, see [SSMS support for Azure AD MFA with SQL Database and SQL Data Warehouse](https://azure.microsoft.com/documentation/articles/sql-database-ssms-mfa-authentication/).  
  
### Permissions  
 Requires ALTER ANY USER permission on the database.  
  
## Examples  
  
### A. Creating a database user based on a SQL Server login  
 The following example first creates a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login named `AbolrousHazem`, and then creates a corresponding database user `AbolrousHazem` in `AdventureWorks2012`.  
  
```  
CREATE LOGIN AbolrousHazem   
    WITH PASSWORD = '340$Uuxwp7Mcxo7Khy';  
```   
Change to a user database. For example, in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] use the `USE AdventureWorks2012` statement. In [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], you must make a new connection to the user database.

```   
CREATE USER AbolrousHazem FOR LOGIN AbolrousHazem;  
GO   
```  
  
### B. Creating a database user with a default schema  
 The following example first creates a server login named `WanidaBenshoof` with a password, and then creates a corresponding database user `Wanida`, with the default schema `Marketing`.  
  
```  
CREATE LOGIN WanidaBenshoof   
    WITH PASSWORD = '8fdKJl3$nlNv3049jsKK';  
USE AdventureWorks2012;  
CREATE USER Wanida FOR LOGIN WanidaBenshoof   
    WITH DEFAULT_SCHEMA = Marketing;  
GO  
```  
  
### C. Creating a database user from a certificate  
 The following example creates a database user `JinghaoLiu` from certificate `CarnationProduction50`.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```  
USE AdventureWorks2012;  
CREATE CERTIFICATE CarnationProduction50  
    WITH SUBJECT = 'Carnation Production Facility Supervisors',  
    EXPIRY_DATE = '11/11/2011';  
GO  
CREATE USER JinghaoLiu FOR CERTIFICATE CarnationProduction50;  
GO   
```  
  
###  <a name="withoutLogin"></a> D. Creating and using a user without a login  
 The following example creates a database user `CustomApp` that does not map to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. The example then grants a user `adventure-works\tengiz0` permission to impersonate the `CustomApp` user.  
  
```  
USE AdventureWorks2012 ;  
CREATE USER CustomApp WITHOUT LOGIN ;  
GRANT IMPERSONATE ON USER::CustomApp TO [adventure-works\tengiz0] ;  
GO   
```  
  
 To use the `CustomApp` credentials, the user `adventure-works\tengiz0` executes the following statement.  
  
```  
EXECUTE AS USER = 'CustomApp' ;  
GO  
```  
  
 To revert back to the `adventure-works\tengiz0` credentials, the user executes the following statement.  
  
```  
REVERT ;  
GO  
```  
  
### E. Creating a contained database user with password  
 The following example creates a contained database user with password. This example can only be executed in a contained database.  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. This example works in [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] if DEFAULT_LANGUAGE is removed.  
  
```  
USE AdventureWorks2012 ;  
GO  
CREATE USER Carlo  
WITH PASSWORD='RN92piTCh%$!~3K9844 Bl*'  
    , DEFAULT_LANGUAGE=[Brazilian]  
    , DEFAULT_SCHEMA=[dbo]  
GO   
```  
  
### F. Creating a contained database user for a domain login  
 The following example creates a contained database user for a login named Fritz in a domain named Contoso. This example can only be executed in a contained database.  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```  
USE AdventureWorks2012 ;  
GO  
CREATE USER [Contoso\Fritz] ;  
GO   
```  
  
### G. Creating a contained database user with a specific SID  
 The following example creates a SQL Server authenticated contained database user named CarmenW. This example can only be executed in a contained database.  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```  
USE AdventureWorks2012 ;  
GO  
CREATE USER CarmenW WITH PASSWORD = 'a8ea v*(Rd##+'  
, SID = 0x01050000000000090300000063FF0451A9E7664BA705B10E37DDC4B7;  
  
```  
  
### H. Creating a user to copy encrypted data  
 The following example creates a user that can copy data that is protected by the Always Encrypted feature from  one set of tables, containing encrypted columns, to another set of tables with encrypted columns (in the same or a different database).  For more information, see [Migrate Sensitive Data Protected by Always Encrypted](../../relational-databases/security/encryption/migrate-sensitive-data-protected-by-always-encrypted.md).  
  
**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
```  
CREATE USER [Chin]   
WITH   
      DEFAULT_SCHEMA = dbo  
    , ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = ON ;  
```

### I. Create an Azure AD user from an Azure AD login in SQL Database managed instance

 To create an Azure AD user from an Azure AD login, use the following syntax.

 Sign into your managed instance with an Azure AD login granted with the `sysadmin` role. The following creates an Azure AD user bob@contoso.com, from the login bob@contoso.com. This login was created in the [CREATE LOGIN](create-login-transact-sql.md#d-creating-a-login-for-a-federated-azure-ad-account) example.

```sql
CREATE USER [bob@contoso.com] FROM LOGIN [bob@contoso.com];
GO
```

> [!IMPORTANT]
> When creating a **USER** from an Azure AD login, specify the *user_name* as the same *login_name* from **LOGIN**.

Creating an Azure AD user as a group from an Azure AD login that is a group is supported.

```sql
CREATE USER [AAD group] FROM LOGIN [AAD group];
GO
```

You can also create an Azure AD user from an Azure AD login that is a group.

```sql
CREATE USER [bob@contoso.com] FROM LOGIN [AAD group];
GO
```

### J. Create an Azure AD user without an AAD login for the database

The following syntax is used to create an Azure AD user bob@contoso.com, in the SQL Database managed instance database (contained user):

```sql
CREATE USER [bob@contoso.com] FROM EXTERNAL PROVIDER;
GO
```

## Next steps  
Once the user is created, consider adding the user to a database role using the [ALTER ROLE](../../t-sql/statements/alter-role-transact-sql.md) statement.  
You might also want to [GRANT Object Permissions](../../t-sql/statements/grant-object-permissions-transact-sql.md) to the role so they can access tables. For general information about the SQL Server security model, see [Permissions](../../relational-databases/security/permissions-database-engine.md).   
  
## See Also  
 [Create a Database User](../../relational-databases/security/authentication-access/create-a-database-user.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)   
 [ALTER USER &#40;Transact-SQL&#41;](../../t-sql/statements/alter-user-transact-sql.md)   
 [DROP USER &#40;Transact-SQL&#41;](../../t-sql/statements/drop-user-transact-sql.md)   
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [Contained Databases](../../relational-databases/databases/contained-databases.md)   
 [Connecting to SQL Database By Using Azure Active Directory Authentication](https://azure.microsoft.com/documentation/articles/sql-database-aad-authentication)   
 [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md)  
  
  


