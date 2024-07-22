---
title: "CREATE LOGIN (Transact-SQL)"
description: CREATE LOGIN (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: 04/15/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CREATE_LOGIN_TSQL"
  - "CREATE LOGIN"
  - "LOGIN_TSQL"
  - "LOGIN"
helpviewer_keywords:
  - "passwords [SQL Server], logins"
  - "mapping logins [SQL Server]"
  - "logins [SQL Server], creating"
  - "CREATE LOGIN statement"
  - "permissions [SQL Server], logins"
  - "Windows domain accounts [SQL Server]"
  - "re-hashing passwords"
  - "certificates [SQL Server], logins"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CREATE LOGIN (Transact-SQL)

Creates a login for SQL Server, Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, or Analytics Platform System databases. Choose one of the following tabs for the syntax, arguments, remarks, permissions, and examples for a particular version.

CREATE LOGIN participates in transactions. If CREATE LOGIN is executed within a transaction and the transaction is rolled back, then login creation is rolled back. If executed within a transaction, the created login cannot be used until the transaction is committed.

[!INCLUDE [entra-id](../../includes/entra-id.md)]

For more information about the syntax conventions, see [Transact-SQL syntax conventions](../language-elements/transact-sql-syntax-conventions-transact-sql.md).

[!INCLUDE [select-product](../includes/select-product.md)]

::: moniker range=">=sql-server-2016||>=sql-server-linux-2017"

:::row:::
    :::column:::
        **_\* SQL Server \*_** &nbsp;
    :::column-end:::
    :::column:::
        [Azure SQL Database](create-login-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure SQL<br />Managed Instance](create-login-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](create-login-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](create-login-transact-sql.md?view=aps-pdw-2016&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## SQL Server

## Syntax

```syntaxsql
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
    | EXTERNAL PROVIDER
    | CERTIFICATE certname
    | ASYMMETRIC KEY asym_key_name
  
<windows_options> ::=
    DEFAULT_DATABASE = database
    | DEFAULT_LANGUAGE = language
```

## Arguments

#### *login_name*
Specifies the name of the login that is created. There are five types of logins: SQL Server logins, Windows logins, Microsoft Entra logins, certificate-mapped logins, and asymmetric key-mapped logins. 

When you're creating logins that are mapped from a Windows domain account, you must use the logon name in the format [\<domainName>\\<login_name>]. You cannot use a UPN in the format login_name@DomainName. For an example, see [example E](create-login-transact-sql.md?#e-creating-a-login-from-a-windows-domain-account) later in this article. Authentication logins are type **sysname** and must conform to the rules for [Identifiers](../../relational-databases/databases/database-identifiers.md) and cannot contain a backslash (\). Windows logins can contain a '**\\**'. Logins based on Active Directory users are limited to names of fewer than 21 characters.


When using the **FROM EXTERNAL PROVIDER** clause, the login name must match the display name of an existing Microsoft Entra principal in the same tenant that the SQL instance is Arc-enabled to. Microsoft Entra users, groups, and applications can be used to create logins.

#### PASSWORD ='*password*'
Applies to SQL Server logins only. Specifies the password for the login that is being created. Use a strong password. For more information, see [Strong Passwords](../../relational-databases/security/strong-passwords.md) and [Password Policy](../../relational-databases/security/password-policy.md). Beginning with SQL Server 2012 (11.x), stored password information is calculated using SHA-512 of the salted password.

Passwords are case-sensitive. Passwords should always be at least eight characters long, and can't exceed 128 characters. Passwords can include a-z, A-Z, 0-9, and most nonalphanumeric characters. Passwords can't contain single quotes, or the *login_name*.

#### PASSWORD = *hashed\_password*
Applies to the HASHED keyword only. Specifies the hashed value of the password for the login that is being created.

#### HASHED
Applies to SQL Server logins only. Specifies that the password entered after the PASSWORD argument is already hashed. If this option isn't selected, the string entered as password is hashed before it's stored in the database. This option should only be used for migrating databases from one server to another. Don't use the HASHED option to create new logins. The HASHED option can't be used with hashes created by SQL 7 or earlier.

#### MUST_CHANGE
Applies to SQL Server logins only. If this option is included, SQL Server prompts the user for a new password the first time the new login is used.

#### CREDENTIAL =_credential\_name_
The name of a credential to be mapped to the new SQL Server login. The credential must already exist in the server. Currently this option only links the credential to a login. A credential can't be mapped to the System Administrator (sa) login.

#### SID = *sid*
Used to recreate a login. Applies to SQL Server authentication logins only, not Windows authentication logins. Specifies the SID of the new SQL Server authentication login. If this option is not used, SQL Server automatically assigns a SID. The SID structure depends on the SQL Server version. SQL Server login SID: a 16 byte (**binary(16)**) literal value based on a GUID. For example, `SID = 0x14585E90117152449347750164BA00A7`.

#### DEFAULT_DATABASE =_database_
Specifies the default database to be assigned to the login. If this option isn't included, the default database is set to `master`.

#### DEFAULT_LANGUAGE =_language_
Specifies the default language to be assigned to the login. If this option isn't included, the default language is set to the current default language of the server. If the default language of the server is later changed, the default language of the login remains unchanged.

#### CHECK_EXPIRATION = { ON | OFF }
Applies to SQL Server logins only. Specifies whether password expiration policy should be enforced on this login. The default value is OFF.

#### CHECK_POLICY = { ON | OFF }
Applies to SQL Server logins only. Specifies that the Windows password policies of the computer on which SQL Server is running should be enforced on this login. The default value is ON.

If the Windows policy requires strong passwords, passwords must contain at least three of the following four characteristics:

- An uppercase character (A-Z).
- A lowercase character (a-z).
- A digit (0-9).
- One of the nonalphanumeric characters, such as a space, _, @, *, ^, %, !, $, #, or &.

#### WINDOWS
Specifies that the login be mapped to a Windows login.

#### FROM EXTERNAL PROVIDER
Specifies that the login is mapped to a Microsoft Entra principal. This option is available for Arc-enabled SQL Server 2022 and later versions. For more information, see [Microsoft Entra authentication for SQL Server](/sql/relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview?view=sql-server-ver16&preserve-view=true)

#### CERTIFICATE *certname*
Specifies the name of a certificate to be associated with this login. This certificate must already occur in the `master` database.

#### ASYMMETRIC KEY *asym_key_name*
Specifies the name of an asymmetric key to be associated with this login. This key must already occur in the `master` database.

## Remarks

- Passwords are case-sensitive.
- Prehashing of passwords is supported only when you're creating SQL Server logins.
- If `MUST_CHANGE` is specified, CHECK_EXPIRATION and CHECK_POLICY must be set to `ON`. Otherwise, the statement will fail.
- A combination of `CHECK_POLICY = OFF` and `CHECK_EXPIRATION = ON` isn't supported.
- When CHECK_POLICY is set to `OFF`, *lockout_time* is reset and `CHECK_EXPIRATION` is set to `OFF`.

> [!IMPORTANT]
> CHECK_EXPIRATION and CHECK_POLICY are only enforced on Windows Server 2003 and later versions. For more information, see [Password Policy](../../relational-databases/security/password-policy.md).

- Logins created from certificates or asymmetric keys are used only for code signing. They can't be used to connect to SQL Server. You can create a login from a certificate or asymmetric key only when the certificate or asymmetric key already exists in `master`.
- For a script to transfer logins, see [How to transfer the logins and the passwords between instances of SQL Server 2005 and SQL Server 2008](https://support.microsoft.com/kb/918992).
- Creating a login automatically enables the new login and grants the login the server level **CONNECT SQL** permission.
- The server's [authentication mode](../../relational-databases/security/choose-an-authentication-mode.md) must match the login type to permit access.
- For information about designing a permissions system, see [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md).

## Permissions

- Only users with **ALTER ANY LOGIN** permission on the server or membership in the **securityadmin** fixed server role can create logins. For more information, see [Server-Level Roles](/azure/sql-database/sql-database-manage-logins#groups-and-roles) and [ALTER SERVER ROLE](alter-server-role-transact-sql.md).
- If the **CREDENTIAL** option is used, also requires **ALTER ANY CREDENTIAL** permission on the server.

### Permissions for SQL Server 2022 and later
Requires CREATE LOGIN permission on the server or membership in the **##MS_LoginManager##** fixed server role.

## After creating a login

After creating a login, the login can connect to SQL Server, but only has the permissions granted to the **public** role. Consider performing some of the following activities.

- To connect to a database, create a database user for the login. For more information, see [CREATE USER](create-user-transact-sql.md).
- Create a user-defined server role by using [CREATE SERVER ROLE](create-server-role-transact-sql.md). Use `ALTER SERVER ROLE ... ADD MEMBER` to add the new login to the user-defined server role. For more information, see [CREATE SERVER ROLE](create-server-role-transact-sql.md) and [ALTER SERVER ROLE](alter-server-role-transact-sql.md).
- Use `sp_addsrvrolemember` to add the login to a fixed server role. For more information, see [Server-Level Roles](../../relational-databases/security/authentication-access/server-level-roles.md) and [sp_addsrvrolemember](../../relational-databases/system-stored-procedures/sp-addsrvrolemember-transact-sql.md).
- Use the **GRANT** statement, to grant server-level permissions to the new login or to a role containing the login. For more information, see [GRANT](grant-transact-sql.md).

## Examples

### <a id="a-creating-a-login-with-a-password"></a> A. Create a login with a password

The following example creates a login for a particular user and assigns a password.

```sql
CREATE LOGIN <login_name> WITH PASSWORD = '<enterStrongPasswordHere>';
GO
```

### <a id="b-creating-a-login-with-a-password-that-must-be-changed"></a> B. Create a login with a password that must be changed

The following example creates a login for a particular user and assigns a password. The `MUST_CHANGE` option requires users to change this password the first time they connect to the server.

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions.

```sql
CREATE LOGIN <login_name> WITH PASSWORD = '<enterStrongPasswordHere>'
    MUST_CHANGE, CHECK_EXPIRATION = ON;
GO
```

> [!NOTE]
> The MUST_CHANGE option cannot be used when CHECK_EXPIRATION is OFF.

### <a id="c-creating-a-login-mapped-to-a-credential"></a> C. Create a login mapped to a credential

The following example creates the login for a particular user, using the user. This login is mapped to the credential.

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions.

```sql
CREATE LOGIN <login_name> WITH PASSWORD = '<enterStrongPasswordHere>',
    CREDENTIAL = <credentialName>;
GO
```

### <a id="d-creating-a-login-from-a-certificate"></a> D. Create a login from a certificate

The following example creates login for a particular user from a certificate in `master`.

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions.

```sql
USE MASTER;
CREATE CERTIFICATE <certificateName>
    WITH SUBJECT = '<login_name> certificate in master database',
    EXPIRY_DATE = '12/05/2025';
GO
CREATE LOGIN <login_name> FROM CERTIFICATE <certificateName>;
GO
```

### <a id="e-creating-a-login-from-a-windows-domain-account"></a> E. Create a login from a Windows domain account

The following example creates a login from a Windows domain account.

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later versions.

```sql
CREATE LOGIN [<domainName>\<login_name>] FROM WINDOWS;
GO
```

### <a id="f-creating-a-login-from-a-sid"></a> F. Create a login from a SID

The following example first creates a SQL Server authentication login and determines the SID of the login.

```sql
CREATE LOGIN TestLogin WITH PASSWORD = 'SuperSecret52&&';
SELECT name, sid FROM sys.sql_logins WHERE name = 'TestLogin';
GO
```

My query returns 0x241C11948AEEB749B0D22646DB1A19F2 as the SID. Your query will return a different value. The following statements delete the login, and then recreate the login. Use the SID from your previous query.

```sql
DROP LOGIN TestLogin;
GO

CREATE LOGIN TestLogin
WITH PASSWORD = 'SuperSecret52&&', SID = 0x241C11948AEEB749B0D22646DB1A19F2;

SELECT * FROM sys.sql_logins WHERE name = 'TestLogin';
GO
```

### <a id="g-creating-a-login-with-multiple-arguments"></a> G. Create a login with multiple arguments

The following example shows how to string multiple arguments together using commas between each argument.

```sql
CREATE LOGIN [MyUser]
WITH PASSWORD = 'MyPassword',
DEFAULT_DATABASE = MyDatabase,
CHECK_POLICY = OFF,
CHECK_EXPIRATION = OFF ;
```


### <a id="h-creating-a-sql-login-with-hashed-password"></a> H. Create a SQL login with hashed password

The following example shows how to create SQL Logins with the same password as existing Logins as done in a migration scenario. The first step is to retrieve the password hash from existing Logins on the source database server. Then the same hash will be used to create the Login on a new database server. By doing this the new Login will have the same password as on the old server.

```sql
-- run this to retrieve the password hash for an individual Login:
SELECT LOGINPROPERTY('Andreas','PASSWORDHASH') AS password_hash;
-- as an alternative, the catalog view sys.sql_logins can be used to retrieve the password hashes for multiple accounts at once. (This could be used to create a dynamic sql statemnt from the result set)
SELECT name, password_hash
FROM sys.sql_logins
  WHERE
    principal_id > 1    -- excluding sa
    AND
    name NOT LIKE '##MS_%##' -- excluding special MS system accounts
-- create the new SQL Login on the new database server using the hash of the source server
CREATE LOGIN Andreas
  WITH PASSWORD = 0x02000A1A89CD6C6E4C8B30A282354C8EA0860719D5D3AD05E0CAE1952A1C6107A4ED26BEBA2A13B12FAB5093B3CC2A1055910CC0F4B9686A358604E99BB9933C75B4EA48FDEA HASHED;
```

### <a id="i-creating-a-login-for-a-microsoft-entra-user"></a> I. Create a login for a Microsoft Entra user

 The following example creates a login for the Microsoft Entra account joe@contoso.onmicrosoft.com that exists in the Microsoft Entra tenant named *contoso*.

```sql
CREATE LOGIN [joe@contoso.onmicrosoft.com] FROM EXTERNAL PROVIDER

GO
```

### <a id="j-creating-a-login-for-a-federated-microsoft-entra-account"></a> J. Create a login for a federated Microsoft Entra account

 The following example creates a login for a federated Microsoft Entra account bob@contoso.com that exists in a tenant called *contoso*. User bob can also be a guest user.

```sql
CREATE LOGIN [bob@contoso.com] FROM EXTERNAL PROVIDER
GO
```

### <a id="k-creating-a-login-for-a-microsoft-entra-group"></a> K. Create a login for a Microsoft Entra group

 The following example creates a login for the Microsoft Entra group *mygroup* that exists in the tenant *contoso*.

```sql
CREATE LOGIN [mygroup] FROM EXTERNAL PROVIDER
GO
```

### <a id="l-creating-a-login-for-a-microsoft-entra-application"></a> L. Create a login for a Microsoft Entra application

The following example creates a login for the Microsoft Entra application *myapp* that exists in the tenant *contoso*.

```sql
CREATE LOGIN [myapp] FROM EXTERNAL PROVIDER
```

## Related content

- [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md)
- [Principals](../../relational-databases/security/authentication-access/principals-database-engine.md)
- [Password Policy](../../relational-databases/security/password-policy.md)
- [ALTER LOGIN](alter-login-transact-sql.md)
- [DROP LOGIN](drop-login-transact-sql.md)
- [EVENTDATA](../functions/eventdata-transact-sql.md)
- [Create a Login](../../relational-databases/security/authentication-access/create-a-login.md)

::: moniker-end
::: moniker range="=azuresqldb-current"

:::row:::
    :::column:::
        [SQL Server](create-login-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* Azure SQL Database \*_** &nbsp;
    :::column-end:::
    :::column:::
        [Azure SQL<br />Managed Instance](create-login-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](create-login-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](create-login-transact-sql.md?view=aps-pdw-2016&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## SQL Database

For more information on connectivity and authorization to Azure SQL Database, see:

- [Connectivity settings for Azure SQL Database](/azure/azure-sql/database/connectivity-settings?view=azuresql-db&preserve-view=true)
- [Authorize database access to SQL Database](/azure/azure-sql/database/logins-create-manage?view=azuresql-db&preserve-view=true)

## Syntax

```syntaxsql
-- Syntax for Azure SQL Database
CREATE LOGIN login_name
  { 
    FROM EXTERNAL PROVIDER [WITH OBJECT_ID = 'objectid'] 
    | WITH <option_list> [,..] 
  }

<option_list> ::=
    PASSWORD = { 'password' }
    [ , SID = sid ]
```

## Arguments

#### *login_name*

> [!NOTE]
> [Microsoft Entra server principals (logins)](/azure/azure-sql/database/authentication-azure-ad-logins) are currently in public preview for Azure SQL Database.

When used with the **FROM EXTERNAL PROVIDER** clause, the login specifies the Microsoft Entra principal, which is a Microsoft Entra user, group, or application. Otherwise, the login represents the name of the SQL login that was created.

Microsoft Entra users and service principals that are members of more than 2048 Microsoft Entra security groups aren't supported to sign in to the database in SQL Database, SQL Managed Instance, or Azure Synapse.

#### FROM EXTERNAL PROVIDER
Specifies that the login is for Microsoft Entra authentication.

#### WITH OBJECT_ID = *'objectid'*
Specifies the Microsoft Entra Object ID. If the `OBJECT_ID` is specified, the login_name isn't required to match the Microsoft Entra display name. The login_name must be a unique name in the `sys.server_principals` view and adhere to all other `sysname` limitations. For more information on using the `WITH OBJECT_ID` option, see [Microsoft Entra logins and users with nonunique display names](/azure/azure-sql/database/authentication-microsoft-entra-create-users-with-nonunique-names).

> [!NOTE]
> If the service principal display name is not a duplicate, the default `CREATE LOGIN` or `CREATE USER` statement should be used. The `WITH OBJECT_ID` extension is in **public preview**, and is a troubleshooting repair item implemented for use with nonunique service principals. Using it with a unique service principal is not recommended. Using the `WITH OBJECT_ID` extension for a service principal without adding a suffix will run successfully, but it will not be obvious which service principal the login or user was created for. It's recommended to create an alias using a suffix to uniquely identify the service principal. The `WITH OBJECT_ID` extension is not supported for SQL Server.

#### PASSWORD ='password'
Specifies the password for the SQL login that is being created. Use a strong password. For more information, see [Strong Passwords](../../relational-databases/security/strong-passwords.md) and [Password Policy](../../relational-databases/security/password-policy.md). Beginning with [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], stored password information is calculated using SHA-512 of the salted password.

Passwords are case-sensitive. Passwords should always be at least eight characters long, and can't exceed 128 characters. Passwords can include a-z, A-Z, 0-9, and most nonalphanumeric characters. Passwords can't contain single quotes, or the *login_name*.

#### SID = *sid*
Used to recreate a login. Applies to SQL Server authentication logins only, not Windows authentication logins. Specifies the SID of the new SQL Server authentication login. If this option isn't used, SQL Server automatically assigns a SID. The SID structure depends on the SQL Server version. For SQL Database, this is a 32 byte (**binary(32)**) literal consisting of `0x01060000000000640000000000000000` plus 16 bytes representing a GUID. For example, `SID = 0x0106000000000064000000000000000014585E90117152449347750164BA00A7`.

## Remarks

- Passwords are case-sensitive.
- Creating a login automatically enables the new login and grants the login the server level **CONNECT SQL** permission.

> [!IMPORTANT]
> See [Manage Logins in Azure SQL Database](/azure/sql-database/sql-database-manage-logins?view=azuresql-db&preserve-view=true) for information about working with logins and users in Azure SQL Database.

## Login

### SQL Database logins

The **CREATE LOGIN** statement must be the only statement in a batch.

In some methods of connecting to SQL Database, such as **sqlcmd**, you must append the SQL Database server name to the login name in the connection string by using the *\<login>*@*\<server>* notation. For example, if your login is `login1` and the fully qualified name of the SQL Database server is `servername.database.windows.net`, the *username* parameter of the connection string should be `login1@servername`. Because the total length of the *username* parameter is 128 characters, *login_name* is limited to 127 characters minus the length of the server name. In the example, `login_name` can only be 117 characters long because `servername` is 10 characters.

In SQL Database, you must be connected to the `master` database with the appropriate permissions to create a login. For more information, see [Create additional logins and users having administrative permissions](/azure/sql-database/sql-database-manage-logins?view=azuresql-db&preserve-view=true#create-additional-logins-and-users-having-administrative-permissions).

SQL Server rules allow you to create a SQL Server authentication login in the format \<loginname>@\<servername>. If your [!INCLUDE [ssSDS](../../includes/sssds-md.md)] server is **myazureserver** and your login is **myemail@contoso.com**, then you must supply your login as **myemail@contoso.com@myazureserver**.

In SQL Database, login data required to authenticate a connection and server-level firewall rules is temporarily cached in each database. This cache is periodically refreshed. To force a refresh of the authentication cache and make sure that a database has the latest version of the logins table, execute [DBCC FLUSHAUTHCACHE](../database-console-commands/dbcc-flushauthcache-transact-sql.md).

## Permissions

Only the server-level principal login (created by the provisioning process) or members of the `loginmanager` database role in the `master` database can create new logins. For more information, see [Create additional logins and users having administrative permissions](/azure/sql-database/sql-database-manage-logins?view=azuresql-db&preserve-view=true#create-additional-logins-and-users-having-administrative-permissions).

## Examples

### <a id="a-creating-a-login-with-a-password"></a> A. Create a login with a password

The following example creates a login for a particular user and assigns a password. Connect to the `master` databases, then use the following code sample.

```sql
CREATE LOGIN <login_name> WITH PASSWORD = '<enterStrongPasswordHere>';
GO
```

### <a id="b-creating-a-login-from-a-sid"></a> B. Create a login from a SID

The following example first creates a SQL Server authentication login and determines the SID of the login. Connect to the `master` databases, then use the following code sample.

```sql
CREATE LOGIN TestLogin WITH PASSWORD = 'SuperSecret52&&';

SELECT name, sid FROM sys.sql_logins WHERE name = 'TestLogin';
GO
```

My query returns 0x241C11948AEEB749B0D22646DB1A19F2 as the SID. Your query will return a different value. The following statements delete the login, and then recreate the login. Use the SID from your previous query.

```sql
DROP LOGIN TestLogin;
GO

CREATE LOGIN TestLogin
WITH PASSWORD = 'SuperSecret52&&', SID = 0x241C11948AEEB749B0D22646DB1A19F2;

SELECT * FROM sys.sql_logins WHERE name = 'TestLogin';
GO
```

<a name='c-create-a-login-using-an-azure-ad-account'></a>

### C. Create a login using a Microsoft Entra account

This example creates a login in the Azure SQL logical server using the credentials of a user `bob@contoso.com` that exists in the Microsoft Entra domain called `contoso`. Connect to the `master` databases, then use the following code sample.

```sql
CREATE LOGIN [bob@contoso.com] FROM EXTERNAL PROVIDER;
GO
```

### D. Create a login with an alias using Object ID

You can create an alias for your *login_name* by specifying the Object ID of the Microsoft Entra service principal or group. Connect to the `master` databases, then use the following code sample.

```sql
CREATE LOGIN [myapp4466e] FROM EXTERNAL PROVIDER 
  WITH OBJECT_ID='4466e2f8-0fea-4c61-a470-xxxxxxxxxxxx';
```

For more information on obtaining the Object ID of a service principal, see [service principal objects in Microsoft Entra ID](/entra/identity-platform/app-objects-and-service-principals#service-principal-object).

## Related content

- [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md?view=azuresql-db&preserve-view=true)
- [Principals](../../relational-databases/security/authentication-access/principals-database-engine.md?view=azuresql-db&preserve-view=true)
- [Password Policy](../../relational-databases/security/password-policy.md)
- [ALTER LOGIN](alter-login-transact-sql.md?view=azuresql-db&preserve-view=true)
- [DROP LOGIN](drop-login-transact-sql.md?view=azuresql-db&preserve-view=true)
- [EVENTDATA](../functions/eventdata-transact-sql.md?view=azuresql-db&preserve-view=true)
- [Create a Login](../../relational-databases/security/authentication-access/create-a-login.md?view=azuresql-db&preserve-view=true)

::: moniker-end
::: moniker range="=azuresqldb-mi-current"

:::row:::
    :::column:::
        [SQL Server](create-login-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure SQL Database](create-login-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* Azure SQL<br />Managed Instance \*_**
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](create-login-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](create-login-transact-sql.md?view=aps-pdw-2016&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## Azure SQL Managed Instance

## Syntax

```syntaxsql
-- Syntax for Azure SQL Managed Instance
CREATE LOGIN login_name [FROM EXTERNAL PROVIDER [WITH OBJECT_ID = 'objectid'] ] { WITH <option_list> [,..]}

<option_list> ::=
    PASSWORD = {'password'}
    | SID = sid
    | DEFAULT_DATABASE = database
    | DEFAULT_LANGUAGE = language
```

## Arguments

#### *login_name*
When used with the **FROM EXTERNAL PROVIDER** clause, the login specifies the Microsoft Entra principal, which is a Microsoft Entra user, group, or application. Otherwise, the login represents the name of the SQL login that was created.

Microsoft Entra users and service principals that are members of more than 2048 Microsoft Entra security groups aren't supported to log into databases in Azure SQL Database, Azure SQL Managed Instance, or Azure Synapse.

#### FROM EXTERNAL PROVIDER </br>
Specifies that the login is for Microsoft Entra authentication.

#### WITH OBJECT_ID = *'objectid'*
Specifies the Microsoft Entra Object ID. If the `OBJECT_ID` is specified, the login_name can be a user defined alias formed from the original principal display name with a suffix appended. The login_name must be a unique name in the `sys.server_principals` view and adhere to all other `sysname` limitations. For more information on using the `WITH OBJECT_ID` option, see [Microsoft Entra logins and users with nonunique display names](/azure/azure-sql/database/authentication-microsoft-entra-create-users-with-nonunique-names).

> [!NOTE]
> If the service principal display name is not a duplicate, the default `CREATE LOGIN` or `CREATE USER` statement should be used. The `WITH OBJECT_ID` extension is in **public preview**, and is a troubleshooting repair item implemented for use with nonunique service principals. Using it with a unique service principal is not necessary. Using the `WITH OBJECT_ID` extension for a service principal without adding a suffix will run successfully, but it will not be obvious which service principal the login or user was created for. It's recommended to create an alias using a suffix to uniquely identify the service principal. The `WITH OBJECT_ID` extension is not supported for SQL Server.

#### PASSWORD = '*password*'
Specifies the password for the SQL login that is being created. Use a strong password. For more information, see [Strong Passwords](../../relational-databases/security/strong-passwords.md) and [Password Policy](../../relational-databases/security/password-policy.md). Beginning with [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], stored password information is calculated using SHA-512 of the salted password.

Passwords are case-sensitive. Passwords should always be at least 10 characters long, and can't exceed 128 characters. Passwords can include a-z, A-Z, 0-9, and most nonalphanumeric characters. Passwords can't contain single quotes, or the *login_name*.

#### SID = *sid*
Used to recreate a login. Applies to SQL Server authentication logins only. Specifies the SID of the new SQL Server authentication login. If this option isn't used, SQL Server automatically assigns a SID. The SID structure depends on the SQL Server version. For SQL Database, this is a 32 byte (**binary(32)**) literal consisting of `0x01060000000000640000000000000000` plus 16 bytes representing a GUID. For example, `SID = 0x0106000000000064000000000000000014585E90117152449347750164BA00A7`.

## Remarks

- The `FROM EXTERNAL PROVIDER` syntax creates server-level principals mapped to Microsoft Entra accounts
- When `FROM EXTERNAL PROVIDER` is specified:


  - The login_name must represent an existing Microsoft Entra account (user, group, or application) that is accessible in Microsoft Entra ID by the current Azure SQL Managed Instance. For Microsoft Entra principals, the CREATE LOGIN syntax requires:
    - UserPrincipalName of the Microsoft Entra object for Microsoft Entra users.

    - DisplayName of the Microsoft Entra object for Microsoft Entra groups and applications.

  - The **PASSWORD** option can't be used.
- By default, when the `FROM EXTERNAL PROVIDER` clause is omitted, a regular SQL login is created.

- Microsoft Entra logins are visible in `sys.server_principals`, with type column value set to `E` and `type_desc` set to **EXTERNAL_LOGIN** for logins mapped to Microsoft Entra users, or type column value set to `X` and `type_desc` value set to **EXTERNAL_GROUP** for logins mapped to Microsoft Entra groups.
- For a script to transfer logins, see [How to transfer the logins and the passwords between instances of SQL Server 2005 and SQL Server 2008](https://support.microsoft.com/kb/918992).
- Creating a login automatically enables the new login and grants the login the server level **CONNECT SQL** permission.

> [!IMPORTANT]
> See [Manage Logins in Azure SQL Database](/azure/sql-database/sql-database-manage-logins) for information about working with logins and users in Azure SQL Database.

## Logins and Permissions

Only the server-level principal login (created by the provisioning process) or members of the `securityadmin` or `sysadmin` database role in the `master` database can create new logins. For more information, see [Server-Level Roles](/azure/sql-database/sql-database-manage-logins#groups-and-roles) and [ALTER SERVER ROLE](alter-server-role-transact-sql.md).

By default, the standard permission granted to a newly created Microsoft Entra login in `master` is:
**CONNECT SQL** and **VIEW ANY DATABASE**.

### SQL Managed Instance logins

- Must have **ALTER ANY LOGIN** permission on the server or membership in the one of the fixed server roles `securityadmin` or `sysadmin`. Only a Microsoft Entra account with **ALTER ANY LOGIN** permission on the server or membership in one of those roles can execute the create command.
- If the login is a SQL principal, only logins that are part of the `sysadmin` role can use the create command to create logins for a Microsoft Entra account.

- Must be a member of the same Microsoft Entra tenant as the Azure SQL Managed Instance.

## After creating a login

After creating a login, the login can connect to a managed instance, but only has the permissions granted to the **public** role. Consider performing some of the following activities.

- To create a user from a Microsoft Entra login, see [CREATE USER](create-user-transact-sql.md).

- To grant permissions to a user in a database, use the `ALTER SERVER ROLE ... ADD MEMBER` statement to add the user to one of the built-in database roles or a custom role, or grant permissions to the user directly using the [GRANT](grant-transact-sql.md) statement. For more information, see [Non-administrator Roles](/azure/sql-database/sql-database-manage-logins#non-administrator-users), [Additional server-level administrative roles](/azure/sql-database/sql-database-manage-logins#additional-server-level-administrative-roles), [ALTER SERVER ROLE](alter-server-role-transact-sql.md), and [GRANT](grant-transact-sql.md) statement.
- To grant server-wide permissions, create a database user in the `master` database and use the `ALTER SERVER ROLE ... ADD MEMBER` statement to add the user to one of the administrative server roles. For more information, see [Server-Level Roles](/azure/sql-database/sql-database-manage-logins#groups-and-roles) and [ALTER SERVER ROLE](alter-server-role-transact-sql.md), and [Server roles](/azure/sql-database/sql-database-manage-logins#additional-server-level-administrative-roles).
  - Use the following command to add the `sysadmin` role to a Microsoft Entra login:
  `ALTER SERVER ROLE sysadmin ADD MEMBER [MS_Entra_login]`
- Use the **GRANT** statement, to grant server-level permissions to the new login or to a role containing the login. For more information, see [GRANT](grant-transact-sql.md).

## Limitations

- Setting a Microsoft Entra login mapped to a Microsoft Entra group as the database owner is not supported.
- Impersonation of Microsoft Entra logins using other Microsoft Entra principals is supported, such as the [EXECUTE AS](execute-as-transact-sql.md) clause.

- Only server principals (logins) that are part of the `sysadmin` role can execute the following operations targeting Microsoft Entra principals:
  - EXECUTE AS USER
  - EXECUTE AS LOGIN
- External (guest) users imported from another Microsoft Entra directory can't be directly configured as a Microsoft Entra admin for SQL Managed Instance using the Azure portal. Instead, join external user to a [role-assignable group](/entra/identity/role-based-access-control/groups-create-eligible) and configure the group as the instance administrator. You can use PowerShell or Azure CLI to set individual guest users as the instance administrator.
- Logins aren't replicated to the secondary instance in a failover group. Logins are saved in the `master` database, which is a system database, and as such isn't geo-replicated. To solve this, logins must be created with the same SID on the secondary instance.

```SQL
-- Code to create login on the secondary instance
CREATE LOGIN foo WITH PASSWORD = '<enterStrongPasswordHere>', SID = <login_sid>;
```

## Examples

### <a id="a-creating-a-login-with-a-password"></a> A. Create a login with a password

The following example creates a login for a particular user and assigns a password.

 ```sql
 CREATE LOGIN <login_name> WITH PASSWORD = '<enterStrongPasswordHere>';
 GO
 ```

### <a id="b-creating-a-login-from-a-sid"></a> B. Create a login from a SID

 The following example first creates a SQL Server authentication login and determines the SID of the login.

 ```sql
 CREATE LOGIN TestLogin WITH PASSWORD = 'SuperSecret52&&';

 SELECT name, sid FROM sys.sql_logins WHERE name = 'TestLogin';
 GO
 ```

My query returns 0x241C11948AEEB749B0D22646DB1A19F2 as the SID. Your query will return a different value. The following statements delete the login, and then recreate the login. Use the SID from your previous query.

 ```sql
 DROP LOGIN TestLogin;
 GO

 CREATE LOGIN TestLogin
 WITH PASSWORD = 'SuperSecret52&&', SID = 0x241C11948AEEB749B0D22646DB1A19F2;

 SELECT * FROM sys.sql_logins WHERE name = 'TestLogin';
 GO
 ```

<a name='c-creating-a-login-for-a-local-azure-ad-account'></a>

### <a id="c-creating-a-login-for-a-microsoft-entra-user"></a> C. Create a login for a Microsoft Entra user

 The following example creates a login for the Microsoft Entra account joe@contoso.onmicrosoft.com that exists in the tenant named *contoso*.


```sql
CREATE LOGIN [joe@contoso.onmicrosoft.com] FROM EXTERNAL PROVIDER

GO
```

<a name='d-creating-a-login-for-a-federated-azure-ad-account'></a>

### <a id="d-creating-a-login-for-a-federated-microsoft-entra-account"></a> D. Create a login for a federated Microsoft Entra account

 The following example creates a login for a federated Microsoft Entra account bob@contoso.com that exists in a tenant called *contoso*. User bob can also be a guest user.


```sql
CREATE LOGIN [bob@contoso.com] FROM EXTERNAL PROVIDER
GO
```

<a name='e-creating-a-login-for-an-azure-ad-group'></a>

### <a id="e-creating-a-login-for-a-microsoft-entra-group"></a> E. Create a login for a Microsoft Entra group

 The following example creates a login for the Microsoft Entra group *mygroup* that exists in the tenant *contoso*.

```sql
CREATE LOGIN [mygroup] FROM EXTERNAL PROVIDER
GO
```

<a name='f-creating-a-login-for-an-azure-ad-application'></a>

### <a id="f-creating-a-login-for-a-microsoft-entra-application"></a> F. Create a login for a Microsoft Entra application

The following example creates a login for the Microsoft Entra application *myapp* that exists in the tenant *contoso*.

```sql
CREATE LOGIN [myapp] FROM EXTERNAL PROVIDER
```

### G. Check newly added logins

To check the newly added login, execute the following T-SQL command:

```sql
SELECT *
FROM sys.server_principals;
GO
```

## Related content

- [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md)
- [Principals](../../relational-databases/security/authentication-access/principals-database-engine.md)
- [Password Policy](../../relational-databases/security/password-policy.md)
- [ALTER LOGIN](alter-login-transact-sql.md)
- [DROP LOGIN](drop-login-transact-sql.md)
- [EVENTDATA](../functions/eventdata-transact-sql.md)
- [Create a Login](../../relational-databases/security/authentication-access/create-a-login.md)

::: moniker-end
::: moniker range="=azure-sqldw-latest"

:::row:::
    :::column:::
        [SQL Server](create-login-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure SQL Database](create-login-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure SQL<br />Managed Instance](create-login-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* Azure Synapse<br />Analytics \*_**
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](create-login-transact-sql.md?view=aps-pdw-2016&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## Azure Synapse Analytics

## Syntax

```syntaxsql
-- Syntax for Azure Synapse Analytics
CREATE LOGIN login_name
  { 
    FROM EXTERNAL PROVIDER
    | WITH <option_list> [,..] 
  }

<option_list> ::=
    PASSWORD = { 'password' }
    [ , SID = sid ]
```

## Arguments

> [!NOTE]
> [Microsoft Entra server principals (logins)](/azure/azure-sql/database/authentication-azure-ad-logins) are currently in preview.

When used with the **FROM EXTERNAL PROVIDER** clause, the login specifies the Microsoft Entra principal, which is a Microsoft Entra user, group, or application. Otherwise, the login represents the name of the SQL login that was created.

Microsoft users and service principals (Microsoft Entra applications) that are members of more than 2048 Microsoft Entra security groups aren't supported to sign into the database in SQL Database, SQL Managed Instance, or Azure Synapse.

#### FROM EXTERNAL PROVIDER
Specifies that the login is for Microsoft Entra authentication.

#### *login_name*
Specifies the name of the login that is created. SQL Analytics in Azure Synapse supports only SQL logins. To create accounts for Microsoft Entra users, use the [CREATE USER](create-user-transact-sql.md) statement.

#### PASSWORD ='password'
Specifies the password for the SQL login that is being created. Use a strong password. For more information, see [Strong Passwords](../../relational-databases/security/strong-passwords.md) and [Password Policy](../../relational-databases/security/password-policy.md). Beginning with [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], stored password information is calculated using SHA-512 of the salted password.

Passwords are case-sensitive. Passwords should always be at least eight characters long, and can't exceed 128 characters. Passwords can include a-z, A-Z, 0-9, and most nonalphanumeric characters. Passwords can't contain single quotes, or the *login_name*.

#### SID = *sid*
Used to recreate a login. Applies to SQL Server authentication logins only, not Windows authentication logins. Specifies the SID of the new SQL Server authentication login. If this option isn't used, SQL Server automatically assigns a SID. The SID structure depends on the SQL Server version. For SQL Analytics, this is a 32 byte (**binary(32)**) literal consisting of `0x01060000000000640000000000000000` plus 16 bytes representing a GUID. For example, `SID = 0x0106000000000064000000000000000014585E90117152449347750164BA00A7`.

## Remarks

- Passwords are case-sensitive.
- For a script to transfer logins, see [How to transfer the logins and the passwords between instances of SQL Server 2005 and SQL Server 2008](https://support.microsoft.com/kb/918992).
- Creating a login automatically enables the new login and grants the login the server level **CONNECT SQL** permission.
- The server's [authentication mode](../../relational-databases/security/choose-an-authentication-mode.md) must match the login type to permit access.
- For information about designing a permissions system, see [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md).

## Logins

The **CREATE LOGIN** statement must be the only statement in a batch.

When connecting to Azure Synapse using tools such as **sqlcmd**, you must append the SQL Analytics server name to the login name in the connection string by using the *\<login>*@*\<server>* notation. For example, if your login is `login1` and the fully qualified name of the SQL Analytics server is `servername.database.windows.net`, the *username* parameter of the connection string should be `login1@servername`. Because the total length of the *username* parameter is 128 characters, *login_name* is limited to 127 characters minus the length of the server name. In the example, `login_name` can only be 117 characters long because `servername` is 10 characters.

To create a login, you must be connected to the `master` database.

SQL Server rules allow you to create a SQL Server authentication login in the format \<loginname>@\<servername>. If your [!INCLUDE [ssSDS](../../includes/sssds-md.md)] server is **myazureserver** and your login is **myemail@contoso.com**, then you must supply your login as **myemail@contoso.com@myazureserver**.

Login data required to authenticate a connection and server-level firewall rules is temporarily cached in each database. This cache is periodically refreshed. To force a refresh of the authentication cache and make sure that a database has the latest version of the logins table, execute [DBCC FLUSHAUTHCACHE](../database-console-commands/dbcc-flushauthcache-transact-sql.md).

For more information about logins, see [Managing Databases and Logins](/azure/sql-database/sql-database-manage-logins).

## Permissions

Only the server-level principal login (created by the provisioning process) or members of the `loginmanager` database role in the `master` database can create new logins. For more information, see [Server-Level Roles](/azure/sql-database/sql-database-manage-logins#groups-and-roles) and [ALTER SERVER ROLE](alter-server-role-transact-sql.md).

## After creating a login

After creating a login, the login can connect to Azure Synapse but only has the permissions granted to the **public** role. Consider performing some of the following activities.

- To connect to a database, create a database user for the login. For more information, see [CREATE USER](create-user-transact-sql.md).
- To grant permissions to a user in a database, use the `ALTER SERVER ROLE ... ADD MEMBER` statement to add the user to one of the built-in database roles or a custom role, or grant permissions to the user directly using the [GRANT](grant-transact-sql.md) statement. For more information, see [Non-administrator Roles](/azure/sql-database/sql-database-manage-logins#non-administrator-users), [Additional server-level administrative roles](/azure/sql-database/sql-database-manage-logins#additional-server-level-administrative-roles), [ALTER SERVER ROLE](alter-server-role-transact-sql.md), and [GRANT](grant-transact-sql.md) statement.
- To grant server-wide permissions, create a database user in the `master` database and use the `ALTER SERVER ROLE ... ADD MEMBER` statement to add the user to one of the administrative server roles. For more information, see [Server-Level Roles](/azure/sql-database/sql-database-manage-logins#groups-and-roles) and [ALTER SERVER ROLE](alter-server-role-transact-sql.md), and [Server roles](/azure/sql-database/sql-database-manage-logins#additional-server-level-administrative-roles).

- Use the **GRANT** statement, to grant server-level permissions to the new login or to a role containing the login. For more information, see [GRANT](grant-transact-sql.md).

## Examples

### <a id="a-creating-a-login-with-a-password"></a> A. Create a login with a password

The following example creates a login for a particular user and assigns a password.

```sql
CREATE LOGIN <login_name> WITH PASSWORD = '<enterStrongPasswordHere>';
GO
```

### <a id="b-creating-a-login-from-a-sid"></a> B. Create a login from a SID

 The following example first creates a SQL Server authentication login and determines the SID of the login.

```sql
CREATE LOGIN TestLogin WITH PASSWORD = 'SuperSecret52&&';

SELECT name, sid FROM sys.sql_logins WHERE name = 'TestLogin';
GO
```

My query returns 0x241C11948AEEB749B0D22646DB1A19F2 as the SID. Your query will return a different value. The following statements delete the login, and then recreate the login. Use the SID from your previous query.

```sql
DROP LOGIN TestLogin;
GO

CREATE LOGIN TestLogin
WITH PASSWORD = 'SuperSecret52&&', SID = 0x241C11948AEEB749B0D22646DB1A19F2;

SELECT * FROM sys.sql_logins WHERE name = 'TestLogin';
GO
```

## Related content

- [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md)
- [Principals](../../relational-databases/security/authentication-access/principals-database-engine.md)
- [Password Policy](../../relational-databases/security/password-policy.md)
- [ALTER LOGIN](alter-login-transact-sql.md)
- [DROP LOGIN](drop-login-transact-sql.md)
- [EVENTDATA](../functions/eventdata-transact-sql.md)
- [Create a Login](../../relational-databases/security/authentication-access/create-a-login.md)

::: moniker-end
::: moniker range=">=aps-pdw-2016"

:::row:::
    :::column:::
        [SQL Server](create-login-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure SQL Database](create-login-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure SQL<br />Managed Instance](create-login-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](create-login-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* Analytics<br />Platform System (PDW) \*_**
    :::column-end:::
:::row-end:::

&nbsp;

## Analytics Platform System

## Syntax

```syntaxsql
-- Syntax for Analytics Platform System
CREATE LOGIN loginName { WITH <option_list1> | FROM WINDOWS }

<option_list1> ::=
    PASSWORD = { 'password' } [ MUST_CHANGE ]
    [ , <option_list> [ ,... ] ]
  
<option_list> ::=
      CHECK_EXPIRATION = { ON | OFF}
    | CHECK_POLICY = { ON | OFF}
```

## Arguments

#### *login_name*
Specifies the name of the login that is created. There are four types of logins: SQL Server logins, Windows logins, certificate-mapped logins, and asymmetric key-mapped logins. When you're creating logins that are mapped from a Windows domain account, you must use the pre-Windows 2000 user logon name in the format [\<domainName>\\<login_name>]. You can't use a UPN in the format login_name@DomainName. For an example, see example D later in this article. Authentication logins are type **sysname** and must conform to the rules for [Identifiers](../../relational-databases/databases/database-identifiers.md) and can't contain a '**\\**'. Windows logins can contain a '**\\**'. Logins based on Active Directory users, are limited to names of fewer than 21 characters.

#### PASSWORD ='_password_'
Applies to SQL Server logins only. Specifies the password for the login that is being created. Use a strong password. For more information, see [Strong Passwords](../../relational-databases/security/strong-passwords.md) and [Password Policy](../../relational-databases/security/password-policy.md). Beginning with SQL Server 2012 (11.x), stored password information is calculated using SHA-512 of the salted password.

Passwords are case-sensitive. Passwords should always be at least eight characters long, and can't exceed 128 characters. Passwords can include a-z, A-Z, 0-9, and most nonalphanumeric characters. Passwords can't contain single quotes, or the *login_name*.

#### MUST_CHANGE
Applies to SQL Server logins only. If this option is included, SQL Server prompts the user for a new password the first time the new login is used.

#### CHECK_EXPIRATION = { ON | OFF }
Applies to SQL Server logins only. Specifies whether password expiration policy should be enforced on this login. The default value is OFF.

#### CHECK_POLICY = { ON | OFF }
Applies to SQL Server logins only. Specifies that the Windows password policies of the computer on which SQL Server is running should be enforced on this login. The default value is ON.

If the Windows policy requires strong passwords, passwords must contain at least three of the following four characteristics:

- An uppercase character (A-Z).
- A lowercase character (a-z).
- A digit (0-9).
- One of the nonalphanumeric characters, such as a space, _, @, *, ^, %, !, $, #, or &.

#### WINDOWS
Specifies that the login be mapped to a Windows login.

## Remarks

- Passwords are case-sensitive.
- If MUST_CHANGE is specified, CHECK_EXPIRATION and CHECK_POLICY must be set to ON. Otherwise, the statement will fail.
- A combination of CHECK_POLICY = OFF and CHECK_EXPIRATION = ON isn't supported.
- When CHECK_POLICY is set to OFF, *lockout_time* is reset and CHECK_EXPIRATION is set to OFF.

> [!IMPORTANT]
> CHECK_EXPIRATION and CHECK_POLICY are only enforced on Windows Server 2003 and later versions. For more information, see [Password Policy](../../relational-databases/security/password-policy.md).

- For a script to transfer logins, see [How to transfer the logins and the passwords between instances of SQL Server 2005 and SQL Server 2008](https://support.microsoft.com/kb/918992).
- Creating a login automatically enables the new login and grants the login the server level **CONNECT SQL** permission.
- For information about designing a permissions system, see [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md).

## Permissions

Only users with **ALTER ANY LOGIN** permission on the server or membership in the **securityadmin** fixed server role can create logins. For more information, see [Server-Level Roles](/azure/sql-database/sql-database-manage-logins#groups-and-roles) and [ALTER SERVER ROLE](alter-server-role-transact-sql.md).

## After creating a login

After creating a login, the login can connect to Azure Synapse Analytics, but only has the permissions granted to the **public** role. Consider performing some of the following activities.

- To connect to a database, create a database user for the login. For more information, see [CREATE USER](create-user-transact-sql.md).
- Create a user-defined server role by using [CREATE SERVER ROLE](create-server-role-transact-sql.md). Use `ALTER SERVER ROLE ... ADD MEMBER` to add the new login to the user-defined server role. For more information, see [CREATE SERVER ROLE](create-server-role-transact-sql.md) and [ALTER SERVER ROLE](alter-server-role-transact-sql.md).
- Use `sp_addsrvrolemember` to add the login to a fixed server role. For more information, see [Server-Level Roles](../../relational-databases/security/authentication-access/server-level-roles.md) and [sp_addsrvrolemember](../../relational-databases/system-stored-procedures/sp-addsrvrolemember-transact-sql.md).
- Use the **GRANT** statement, to grant server-level permissions to the new login or to a role containing the login. For more information, see [GRANT](grant-transact-sql.md).

## Examples

### <a id="g-creating-a-sql-server-authentication-login-with-a-password"></a> G. Create a SQL Server authentication login with a password

The following example creates the login `Mary7` with password `A2c3456`.

```sql
CREATE LOGIN Mary7 WITH PASSWORD = 'A2c3456$#' ;
```

### <a id="h-using-options"></a> H. Use Options

The following example creates the login `Mary8` with password and some of the optional arguments.

```sql
CREATE LOGIN Mary8 WITH PASSWORD = 'A2c3456$#' MUST_CHANGE,
CHECK_EXPIRATION = ON,
CHECK_POLICY = ON;
```

### <a id="i-creating-a-login-from-a-windows-domain-account"></a> I. Create a login from a Windows domain account

The following example creates a login from a Windows domain account named `Mary` in the `Contoso` domain.

```sql
CREATE LOGIN [Contoso\Mary] FROM WINDOWS;
GO
```

## Related content

- [Getting Started with Database Engine Permissions](../../relational-databases/security/authentication-access/getting-started-with-database-engine-permissions.md)
- [Principals](../../relational-databases/security/authentication-access/principals-database-engine.md)
- [Password Policy](../../relational-databases/security/password-policy.md)
- [ALTER LOGIN](alter-login-transact-sql.md)
- [DROP LOGIN](drop-login-transact-sql.md)
- [EVENTDATA](../functions/eventdata-transact-sql.md)
- [Create a Login](../../relational-databases/security/authentication-access/create-a-login.md)

---

::: moniker-end
