---
title: "ALTER LOGIN (Transact-SQL)"
description: ALTER LOGIN changes the properties of a SQL Server login account.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 01/21/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER_LOGIN_TSQL"
  - "ALTER LOGIN"
helpviewer_keywords:
  - "ALTER LOGIN statement"
  - "change password"
  - "mapping logins [SQL Server]"
  - "logins [SQL Server], modifying"
  - "passwords [SQL Server], modifying"
  - "names [SQL Server], logins"
  - "modifying login accounts"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
ms.custom:
  - build-2025
---
# ALTER LOGIN (Transact-SQL)

Changes the properties of a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login account.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

[!INCLUDE [entra-id](../../includes/entra-id.md)]

[!INCLUDE [select-product](../includes/select-product.md)]

::: moniker range=">=sql-server-2017 || >=sql-server-linux-2017"

:::row:::
    :::column:::
        ***\* SQL Server \**** &nbsp;
    :::column-end:::
    :::column:::
        [SQL Database](alter-login-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed Instance](alter-login-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-login-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](alter-login-transact-sql.md?view=aps-pdw-2016&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## SQL Server

### Syntax for SQL Server

```syntaxsql
ALTER LOGIN login_name
    {
    <status_option>
    | WITH <set_option> [ , ... ]
    | <cryptographic_credential_option>
    }
[;]

<status_option> ::=
        ENABLE | DISABLE

<set_option> ::=
    PASSWORD = 'password' | hashed_password HASHED
    [
      OLD_PASSWORD = 'oldpassword'
      | <password_option> [ <password_option> ]
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

#### *login_name*

Specifies the name of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login that is being changed. Domain logins must be enclosed in brackets in the format `[<domain>\<user>]`.

#### ENABLE | DISABLE

Enables or disables this login. Disabling a login doesn't affect the behavior of logins that are already connected. (Use the `KILL` statement to terminate an existing connection.) Disabled logins retain their permissions and can still be impersonated.

#### PASSWORD = '*password*'

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies the password for the login that is being changed. Passwords are case-sensitive.

#### PASSWORD = *hashed_password*

Applies to the HASHED keyword only. Specifies the hashed value of the password for the login that is being created.

> [!IMPORTANT]  
> When a login (or a contained database user) connects and is authenticated, the connection caches identity information about the login. For a Windows Authentication login, this includes information about membership in Windows groups. The identity of the login remains authenticated as long as the connection is maintained. To force changes in the identity, such as a password reset or change in Windows group membership, the login must sign out from the authentication authority (Windows or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]), and sign in again. A member of the **sysadmin** fixed server role or any login with the `ALTER ANY CONNECTION` permission can use the `KILL` command to end a connection and force a login to reconnect. [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] can reuse connection information when opening multiple connections to Object Explorer and Query Editor windows. Close all connections to force reconnection.

#### HASHED

Applies to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins only. Specifies that the password entered after the PASSWORD argument is already hashed. If this option isn't selected, the password is hashed before being stored in the database. This option should only be used for login synchronization between two servers. Don't use the HASHED option to routinely change passwords.

#### OLD_PASSWORD = '*old_password*'

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. The current password of the login to which a new password will be assigned. Passwords are case-sensitive.

#### MUST_CHANGE

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. If this option is included, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] prompts for an updated password the first time the altered login is used.

#### DEFAULT_DATABASE = *database*

Specifies a default database to be assigned to the login.

#### DEFAULT_LANGUAGE = *language*

Specifies a default language to be assigned to the login. The default language for all SQL Database logins is English and can't be changed. The default language of the `sa` login on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Linux is English, but it can be changed.

#### NAME = *login_name*

The new name of the login that is being renamed. If this is a Windows login, the SID of the Windows principal corresponding to the new name must match the SID associated with the login in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The new name of a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login can't contain a backslash character (`\`).

#### CHECK_EXPIRATION = { ON | OFF }

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies whether password expiration policy should be enforced on this login. The default value is OFF.

#### CHECK_POLICY = { ON | OFF }

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies that the Windows password policies of the computer on which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running should be enforced on this login. The default value is ON.

#### CREDENTIAL = *credential_name*

The name of a credential to be mapped to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login. The credential must already exist in the server. For more information, see [Credentials](../../relational-databases/security/authentication-access/credentials-database-engine.md). A credential can't be mapped to the sa login.

#### NO CREDENTIAL

Removes any existing mapping of the login to a server credential. For more information, see [Credentials](../../relational-databases/security/authentication-access/credentials-database-engine.md).

#### UNLOCK

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies that a login that is locked out should be unlocked.

#### ADD CREDENTIAL

Adds an Extensible Key Management (EKM) provider credential to the login. For more information, see [Extensible Key Management (EKM)](../../relational-databases/security/encryption/extensible-key-management-ekm.md).

#### DROP CREDENTIAL

Removes an Extensible Key Management (EKM) provider credential from the login. For more information, see [Extensible Key Management (EKM)](../../relational-databases/security/encryption/extensible-key-management-ekm.md).

## Remarks

When CHECK_POLICY is set to ON, the HASHED argument can't be used.

When CHECK_POLICY is changed to ON, the following behavior occurs:

- The password history is initialized with the value of the current password hash.

When CHECK_POLICY is changed to OFF, the following behavior occurs:

- CHECK_EXPIRATION is also set to OFF.
- The password history is cleared.
- The value of *lockout_time* is reset.

If MUST_CHANGE is specified, CHECK_EXPIRATION and CHECK_POLICY must be set to ON. Otherwise, the statement fails.

If CHECK_POLICY is set to OFF, CHECK_EXPIRATION can't be set to ON. An ALTER LOGIN statement that has this combination of options fail.

You can't use ALTER LOGIN with the DISABLE argument to deny access to a Windows group. For example, `ALTER LOGIN [<domain>\<group>] DISABLE` returns the following error message:

```output
"Msg 15151, Level 16, State 1, Line 1
Cannot alter the login '*Domain\Group*', because it doesn't exist or you don't have permission.
```

This is by design.

[!INCLUDE [encryption-algorithm-history-md](../../includes/encryption-algorithm-history.md)]

In [!INCLUDE [ssSDS](../../includes/sssds-md.md)], login data required to authenticate a connection and server-level firewall rules are temporarily cached in each database. This cache is periodically refreshed. To force a refresh of the authentication cache and make sure that a database has the latest version of the logins table, execute [DBCC FLUSHAUTHCACHE](../../t-sql/database-console-commands/dbcc-flushauthcache-transact-sql.md).

## Permissions

Requires ALTER ANY LOGIN permission.

If the CREDENTIAL option is used, also requires ALTER ANY CREDENTIAL permission.

If the login that is being changed is a member of the **sysadmin** fixed server role or a grantee of CONTROL SERVER permission, also requires CONTROL SERVER permission when making the following changes:

- Resetting the password without supplying the old password.
- Enabling MUST_CHANGE, CHECK_POLICY, or CHECK_EXPIRATION.
- Changing the login name.
- Enabling or disabling the login.
- Mapping the login to a different credential.

A principal can change the password, default language, and default database for its own login.

## Examples

### A. Enable a disabled login

The following example enables the login `Mary5`.

```sql
ALTER LOGIN Mary5 ENABLE;
```

### B. Change the password of a login

The following example changes the password of login `Mary5` to a strong password.

```sql
ALTER LOGIN Mary5 WITH PASSWORD = '<enterStrongPasswordHere>';
```

### C. Change the password of a login when logged in as the login

If you're attempting to change the password of the login that you're currently logged in with, and you don't have the `ALTER ANY LOGIN` permission you must specify the `OLD_PASSWORD` option.

```sql
ALTER LOGIN Mary5 WITH PASSWORD = '<enterStrongPasswordHere>' OLD_PASSWORD = '<oldWeakPasswordHere>';
```

### D. Change the name of a login

The following example changes the name of login `Mary5` to `John2`.

```sql
ALTER LOGIN Mary5 WITH NAME = John2;
```

### E. Map a login to a credential

The following example maps the login `John2` to the credential `Custodian04`.

```sql
ALTER LOGIN John2 WITH CREDENTIAL = Custodian04;
```

### F. Map a login to an Extensible Key Management credential

The following example maps the login `Mary5` to the EKM credential `EKMProvider1`.

```sql
ALTER LOGIN Mary5 ADD CREDENTIAL EKMProvider1;
GO
```

### F. Unlock a login

To unlock a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login, execute the following statement, replacing `****` with the desired account password.

```sql
ALTER LOGIN [Mary5] WITH PASSWORD = '****' UNLOCK;
GO
```

To unlock a login without changing the password, turn off the check policy and then on again.

```sql
ALTER LOGIN [Mary5] WITH CHECK_POLICY = OFF;
ALTER LOGIN [Mary5] WITH CHECK_POLICY = ON;
GO
```

### G. Change the password of a login using HASHED

The following example changes the password of the `TestUser` login to an already hashed value.

```sql
ALTER LOGIN TestUser WITH PASSWORD = 0x01000CF35567C60BFB41EBDE4CF700A985A13D773D6B45B90900 HASHED;
GO
```

## Related content

- [Credentials (Database Engine)](../../relational-databases/security/authentication-access/credentials-database-engine.md)
- [CREATE LOGIN (Transact-SQL)](create-login-transact-sql.md)
- [DROP LOGIN (Transact-SQL)](drop-login-transact-sql.md)
- [CREATE CREDENTIAL (Transact-SQL)](create-credential-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
- [Extensible Key Management (EKM)](../../relational-databases/security/encryption/extensible-key-management-ekm.md)

::: moniker-end

::: moniker range="=azuresqldb-current"

:::row:::
    :::column:::
        [SQL Server](alter-login-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        ***\* SQL Database \****
    :::column-end:::
    :::column:::
        [SQL Managed Instance](alter-login-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-login-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](alter-login-transact-sql.md?view=aps-pdw-2016&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## SQL Database

### Syntax for Azure SQL Database

```syntaxsql
ALTER LOGIN login_name
  {
      <status_option>
    | WITH <set_option> [ , .. .n ]
  }
[;]

<status_option> ::=
    ENABLE | DISABLE

<set_option> ::=
    PASSWORD = 'password'
    [
      OLD_PASSWORD = 'oldpassword'
    ]
    | NAME = login_name
```

## Arguments

#### *login_name*

Specifies the name of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login that is being changed. Domain logins must be enclosed in brackets in the format [domain\user].

#### ENABLE | DISABLE

Enables or disables this login. Disabling a login doesn't affect the behavior of logins that are already connected. (Use the `KILL` statement to terminate an existing connection.) Disabled logins retain their permissions and can still be impersonated.

#### PASSWORD = '*password*'

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies the password for the login that is being changed. Passwords are case-sensitive.

Continuously active connections to SQL Database require reauthorization (performed by the Database Engine) at least every 10 hours. The Database Engine attempts reauthorization using the originally submitted password and no user input is required. For performance reasons, when a password is reset in SQL Database, the connection isn't reauthenticated, even if the connection is reset due to connection pooling. This is different from the behavior of on-premises SQL Server. If the password has changed since the connection was initially authorized, the connection must be terminated and a new connection made using the new password. A user with the KILL DATABASE CONNECTION permission can explicitly terminate a connection to SQL Database by using the KILL command. For more information, see [KILL](../../t-sql/language-elements/kill-transact-sql.md).

> [!IMPORTANT]  
> When a login (or a contained database user) connects and is authenticated, the connection caches identity information about the login. For a Windows Authentication login, this includes information about membership in Windows groups. The identity of the login remains authenticated as long as the connection is maintained. To force changes in the identity, such as a password reset or change in Windows group membership, the login must sign out from the authentication authority (Windows or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]), and sign in again. A member of the **sysadmin** fixed server role or any login with the **ALTER ANY CONNECTION** permission can use the **KILL** command to end a connection and force a login to reconnect. [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] can reuse connection information when opening multiple connections to Object Explorer and Query Editor windows. Close all connections to force reconnection.

#### OLD_PASSWORD = '*old_password*'

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. The current password of the login to which a new password will be assigned. Passwords are case-sensitive.

#### NAME = *login_name*

The new name of the login that is being renamed. If this is a Windows login, the SID of the Windows principal corresponding to the new name must match the SID associated with the login in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The new name of a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login can't contain a backslash character (\\).

## Remarks

In [!INCLUDE [ssSDS](../../includes/sssds-md.md)], login data required to authenticate a connection and server-level firewall rules are temporarily cached in each database. This cache is periodically refreshed. To force a refresh of the authentication cache and make sure that a database has the latest version of the logins table, execute [DBCC FLUSHAUTHCACHE](../../t-sql/database-console-commands/dbcc-flushauthcache-transact-sql.md).

## Permissions

Requires ALTER ANY LOGIN permission.

If the login that is being changed is a member of the **sysadmin** fixed server role or a grantee of CONTROL SERVER permission, also requires CONTROL SERVER permission when making the following changes:

- Resetting the password without supplying the old password.
- Changing the login name.
- Enabling or disabling the login.
- Mapping the login to a different credential.

A principal can change the password for its own login.

## Examples

These examples also include examples for using other SQL products. See which previous arguments are supported.

### A. Enable a disabled login

The following example enables the login `Mary5`.

```sql
ALTER LOGIN Mary5 ENABLE;
```

### B. Change the password of a login

The following example changes the password of login `Mary5` to a strong password.

```sql
ALTER LOGIN Mary5 WITH PASSWORD = '<enterStrongPasswordHere>';
```

### C. Change the name of a login

The following example changes the name of login `Mary5` to `John2`.

```sql
ALTER LOGIN Mary5 WITH NAME = John2;
```

### D. Map a login to a credential

The following example maps the login `John2` to the credential `Custodian04`.

```sql
ALTER LOGIN John2 WITH CREDENTIAL = Custodian04;
```

### E. Map a login to an Extensible Key Management credential

The following example maps the login `Mary5` to the EKM credential `EKMProvider1`.

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later.

```sql
ALTER LOGIN Mary5 ADD CREDENTIAL EKMProvider1;
GO
```

### F. Change the password of a login using HASHED

The following example changes the password of the `TestUser` login to an already hashed value.

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later.

```sql
ALTER LOGIN TestUser WITH PASSWORD = 0x01000CF35567C60BFB41EBDE4CF700A985A13D773D6B45B90900 HASHED;
GO
```

## Related content

- [Credentials (Database Engine)](../../relational-databases/security/authentication-access/credentials-database-engine.md)
- [CREATE LOGIN (Transact-SQL)](create-login-transact-sql.md)
- [DROP LOGIN (Transact-SQL)](drop-login-transact-sql.md)
- [CREATE CREDENTIAL (Transact-SQL)](create-credential-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
- [Extensible Key Management (EKM)](../../relational-databases/security/encryption/extensible-key-management-ekm.md)

::: moniker-end

::: moniker range="=azuresqldb-mi-current"

:::row:::
    :::column:::
        [SQL Server](alter-login-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Database](alter-login-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        ***\* SQL Managed Instance \****
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-login-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](alter-login-transact-sql.md?view=aps-pdw-2016&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## Azure SQL Managed Instance

### Syntax for SQL Server and Azure SQL Managed Instance

```syntaxsql
ALTER LOGIN login_name
    {
    <status_option>
    | WITH <set_option> [ , ... ]
    | <cryptographic_credential_option>
    }
[;]

<status_option> ::=
        ENABLE | DISABLE

<set_option> ::=
    PASSWORD = 'password' | hashed_password HASHED
    [
      OLD_PASSWORD = 'oldpassword'
      | <password_option> [ <password_option> ]
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

```syntaxsql
-- Syntax for Azure SQL Managed Instance using Microsoft Entra logins

ALTER LOGIN login_name
  {
      <status_option>
    | WITH <set_option> [ , .. .n ]
  }
[;]

<status_option> ::=
    ENABLE | DISABLE

<set_option> ::=
     DEFAULT_DATABASE = database
   | DEFAULT_LANGUAGE = language
```

## Arguments

### Arguments applicable to SQL and Microsoft Entra logins

#### *login_name*

Specifies the name of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login that is being changed. Microsoft Entra logins must be specified as user@domain. For example, <john.smith@contoso.com>, or as the Microsoft Entra group or application name. For Microsoft Entra logins, the *login_name* must correspond to an existing Microsoft Entra login created in the `master` database.

#### ENABLE | DISABLE

Enables or disables this login. Disabling a login doesn't affect the behavior of logins that are already connected. (Use the `KILL` statement to terminate an existing connection.) Disabled logins retain their permissions and can still be impersonated.

#### DEFAULT_DATABASE = *database*

Specifies a default database to be assigned to the login.

#### DEFAULT_LANGUAGE = *language*

Specifies a default language to be assigned to the login. The default language for all SQL Database logins is English and can't be changed. The default language of the `sa` login on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Linux is English, but it can be changed.

### Arguments applicable only to SQL logins

#### PASSWORD = '*password*'

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies the password for the login that is being changed. Passwords are case-sensitive. Passwords also don't apply when used with external logins, like Microsoft Entra logins.

Continuously active connections to SQL Database require reauthorization (performed by the Database Engine) at least every 10 hours. The Database Engine attempts reauthorization using the originally submitted password and no user input is required. For performance reasons, when a password is reset in SQL Database, the connection isn't reauthenticated, even if the connection is reset due to connection pooling. This is different from the behavior of on-premises SQL Server. If the password has changed since the connection was initially authorized, the connection must be terminated and a new connection made using the new password. A user with the KILL DATABASE CONNECTION permission can explicitly terminate a connection to SQL Database by using the KILL command. For more information, see [KILL](../../t-sql/language-elements/kill-transact-sql.md).

#### PASSWORD = *hashed_password*

Applies to the HASHED keyword only. Specifies the hashed value of the password for the login that is being created.

#### HASHED

Applies to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins only. Specifies that the password entered after the PASSWORD argument is already hashed. If this option isn't selected, the password is hashed before being stored in the database. This option should only be used for login synchronization between two servers. Don't use the HASHED option to routinely change passwords.

#### OLD_PASSWORD = '*old_password*'

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. The current password of the login to which a new password will be assigned. Passwords are case-sensitive.

#### MUST_CHANGE

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. If this option is included, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] prompts for an updated password the first time the altered login is used.

#### NAME = *login_name*

The new name of the login that is being renamed. If the login is a Windows login, the SID of the Windows principal corresponding to the new name must match the SID associated with the login in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The new name of a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login can't contain a backslash character (\\).

#### CHECK_EXPIRATION = { ON | OFF }

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies whether password expiration policy should be enforced on this login. The default value is OFF.

#### CHECK_POLICY = { ON | OFF }

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies that the Windows password policies of the computer on which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running should be enforced on this login. The default value is ON.

#### CREDENTIAL = *credential_name*

The name of a credential to be mapped to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login. The credential must already exist in the server. For more information, see [Credentials](../../relational-databases/security/authentication-access/credentials-database-engine.md). A credential can't be mapped to the sa login.

#### NO CREDENTIAL

Removes any existing mapping of the login to a server credential. For more information, see [Credentials](../../relational-databases/security/authentication-access/credentials-database-engine.md).

#### UNLOCK

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies that a login that is locked out should be unlocked.

#### ADD CREDENTIAL

Adds an Extensible Key Management (EKM) provider credential to the login. For more information, see [Extensible Key Management (EKM)](../../relational-databases/security/encryption/extensible-key-management-ekm.md).

#### DROP CREDENTIAL

Removes an Extensible Key Management (EKM) provider credential from the login. For more information, see [Extensible Key Management (EKM)](../../relational-databases/security/encryption/extensible-key-management-ekm.md).

## Remarks

When CHECK_POLICY is set to ON, the HASHED argument can't be used.

When CHECK_POLICY is changed to ON, the following behavior occurs:

- The password history is initialized with the value of the current password hash.

When CHECK_POLICY is changed to OFF, the following behavior occurs:

- CHECK_EXPIRATION is also set to OFF.
- The password history is cleared.
- The value of *lockout_time* is reset.

If MUST_CHANGE is specified, CHECK_EXPIRATION and CHECK_POLICY must be set to ON. Otherwise, the statement fails.

If CHECK_POLICY is set to OFF, CHECK_EXPIRATION can't be set to ON. An ALTER LOGIN statement that has this combination of options fail.

You can't use ALTER_LOGIN with the DISABLE argument to deny access to a Windows group. This is by design. For example, ALTER_LOGIN [*domain\group*] DISABLE returns the following error message:

`"Msg 15151, Level 16, State 1, Line 1
"Cannot alter the login '*Domain\Group*', because it doesn't exist or you don't have permission."`

In [!INCLUDE [ssSDS](../../includes/sssds-md.md)], login data required to authenticate a connection and server-level firewall rules are temporarily cached in each database. This cache is periodically refreshed. To force a refresh of the authentication cache and make sure that a database has the latest version of the logins table, execute [DBCC FLUSHAUTHCACHE](../../t-sql/database-console-commands/dbcc-flushauthcache-transact-sql.md).

## Permissions

Requires ALTER ANY LOGIN permission.

If the CREDENTIAL option is used, also requires ALTER ANY CREDENTIAL permission.

If the login that is being changed is a member of the **sysadmin** fixed server role or a grantee of CONTROL SERVER permission, also requires CONTROL SERVER permission when making the following changes:

- Resetting the password without supplying the old password.
- Enabling MUST_CHANGE, CHECK_POLICY, or CHECK_EXPIRATION.
- Changing the login name.
- Enabling or disabling the login.
- Mapping the login to a different credential.

A principal can change the password, default language, and default database for its own login.

Only a SQL principal with `sysadmin` privileges can execute an ALTER LOGIN command against a Microsoft Entra login.

## Examples

These examples also include examples for using other SQL products. See which previous arguments are supported.

### A. Enable a disabled login

The following example enables the login `Mary5`.

```sql
ALTER LOGIN Mary5 ENABLE;
```

### B. Change the password of a login

The following example changes the password of login `Mary5` to a strong password.

```sql
ALTER LOGIN Mary5 WITH PASSWORD = '<enterStrongPasswordHere>';
```

### C. Change the name of a login

The following example changes the name of login `Mary5` to `John2`.

```sql
ALTER LOGIN Mary5 WITH NAME = John2;
```

### D. Map a login to a credential

The following example maps the login `John2` to the credential `Custodian04`.

```sql
ALTER LOGIN John2 WITH CREDENTIAL = Custodian04;
```

### E. Map a login to an Extensible Key Management credential

The following example maps the login `Mary5` to the EKM credential `EKMProvider1`.

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later, and Azure SQL Managed Instance.

```sql
ALTER LOGIN Mary5 ADD CREDENTIAL EKMProvider1;
GO
```

### F. Unlock a login

To unlock a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login, execute the following statement, replacing `****` with the desired account password.

```sql
ALTER LOGIN [Mary5] WITH PASSWORD = '****' UNLOCK;
GO
```

To unlock a login without changing the password, turn off the check policy and then on again.

```sql
ALTER LOGIN [Mary5] WITH CHECK_POLICY = OFF;

ALTER LOGIN [Mary5] WITH CHECK_POLICY = ON;
GO
```

### G. Change the password of a login using HASHED

The following example changes the password of the `TestUser` login to an already hashed value.

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later, and Azure SQL Managed Instance.

```sql
ALTER LOGIN TestUser WITH PASSWORD = 0x01000CF35567C60BFB41EBDE4CF700A985A13D773D6B45B90900 HASHED;
GO
```

### H. Disable the login of a Microsoft Entra user

The following example disables the login of a Microsoft Entra user, <joe@contoso.com>.

```sql
ALTER LOGIN [joe@contoso.com] DISABLE;
```

## Related content

- [Credentials (Database Engine)](../../relational-databases/security/authentication-access/credentials-database-engine.md)
- [CREATE LOGIN (Transact-SQL)](create-login-transact-sql.md)
- [DROP LOGIN (Transact-SQL)](drop-login-transact-sql.md)
- [CREATE CREDENTIAL (Transact-SQL)](create-credential-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
- [Extensible Key Management (EKM)](../../relational-databases/security/encryption/extensible-key-management-ekm.md)

::: moniker-end

::: moniker range="=azure-sqldw-latest"

:::row:::
    :::column:::
        [SQL Server](alter-login-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Database](alter-login-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed Instance](alter-login-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        ***\* Azure Synapse<br />Analytics \****
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](alter-login-transact-sql.md?view=aps-pdw-2016&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## Azure Synapse Analytics

[!INCLUDE [synapse-fabric-migration](../../includes/synapse-fabric-migration.md)]

### Syntax for Azure Synapse

```syntaxsql
ALTER LOGIN login_name
  {
      <status_option>
    | WITH <set_option> [ , .. .n ]
  }
[;]

<status_option> ::=
    ENABLE | DISABLE

<set_option> ::=
    PASSWORD = 'password'
    [
      OLD_PASSWORD = 'oldpassword'
    ]
    | NAME = login_name
```

## Arguments

#### *login_name*

Specifies the name of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login that is being changed. Domain logins must be enclosed in brackets in the format [domain\user].

#### ENABLE | DISABLE

Enables or disables this login. Disabling a login doesn't affect the behavior of logins that are already connected. (Use the `KILL` statement to terminate an existing connection.) Disabled logins retain their permissions and can still be impersonated.

#### PASSWORD = '*password*'

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies the password for the login that is being changed. Passwords are case-sensitive.

Continuously active connections to SQL Database require reauthorization (performed by the Database Engine) at least every 10 hours. The Database Engine attempts reauthorization using the originally submitted password and no user input is required. For performance reasons, when a password is reset in SQL Database, the connection isn't reauthenticated, even if the connection is reset due to connection pooling. This is different from the behavior of on-premises SQL Server. If the password has changed since the connection was initially authorized, the connection must be terminated and a new connection made using the new password. A user with the KILL DATABASE CONNECTION permission can explicitly terminate a connection to SQL Database by using the KILL command. For more information, see [KILL](../../t-sql/language-elements/kill-transact-sql.md).

> [!IMPORTANT]  
> When a login (or a contained database user) connects and is authenticated, the connection caches identity information about the login. For a Windows Authentication login, this includes information about membership in Windows groups. The identity of the login remains authenticated as long as the connection is maintained. To force changes in the identity, such as a password reset or change in Windows group membership, the login must sign out from the authentication authority (Windows or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]), and sign in again. A member of the **sysadmin** fixed server role or any login with the **ALTER ANY CONNECTION** permission can use the **KILL** command to end a connection and force a login to reconnect. [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] can reuse connection information when opening multiple connections to Object Explorer and Query Editor windows. Close all connections to force reconnection.

#### OLD_PASSWORD = '*old_password*'

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. The current password of the login to which a new password will be assigned. Passwords are case-sensitive.

#### NAME = *login_name*

The new name of the login that is being renamed. If this is a Windows login, the SID of the Windows principal corresponding to the new name must match the SID associated with the login in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The new name of a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login can't contain a backslash character (\\).

## Remarks

In [!INCLUDE [ssSDS](../../includes/sssds-md.md)], login data required to authenticate a connection and server-level firewall rules are temporarily cached in each database. This cache is periodically refreshed. To force a refresh of the authentication cache and make sure that a database has the latest version of the logins table, execute [DBCC FLUSHAUTHCACHE](../../t-sql/database-console-commands/dbcc-flushauthcache-transact-sql.md).

## Permissions

Requires ALTER ANY LOGIN permission.

If the login that is being changed is a member of the **sysadmin** fixed server role or a grantee of CONTROL SERVER permission, also requires CONTROL SERVER permission when making the following changes:

- Resetting the password without supplying the old password.
- Changing the login name.
- Enabling or disabling the login.
- Mapping the login to a different credential.

A principal can change the password for its own login.

## Examples

These examples also include examples for using other SQL products. See which previous arguments are supported.

### A. Enable a disabled login

The following example enables the login `Mary5`.

```sql
ALTER LOGIN Mary5 ENABLE;
```

### B. Change the password of a login

The following example changes the password of login `Mary5` to a strong password.

```sql
ALTER LOGIN Mary5 WITH PASSWORD = '<enterStrongPasswordHere>';
```

### C. Change the name of a login

The following example changes the name of login `Mary5` to `John2`.

```sql
ALTER LOGIN Mary5 WITH NAME = John2;
```

### D. Map a login to a credential

The following example maps the login `John2` to the credential `Custodian04`.

```sql
ALTER LOGIN John2 WITH CREDENTIAL = Custodian04;
```

### E. Map a login to an Extensible Key Management credential

The following example maps the login `Mary5` to the EKM credential `EKMProvider1`.

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later.

```sql
ALTER LOGIN Mary5 ADD CREDENTIAL EKMProvider1;
GO
```

### F. Unlock a login

To unlock a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login, execute the following statement, replacing `****` with the desired account password.

```sql
ALTER LOGIN [Mary5] WITH PASSWORD = '****' UNLOCK;
GO
```

### G. Change the password of a login using HASHED

The following example changes the password of the `TestUser` login to an already hashed value.

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later.

```sql
ALTER LOGIN TestUser WITH PASSWORD = 0x01000CF35567C60BFB41EBDE4CF700A985A13D773D6B45B90900 HASHED;
GO
```

## Related content

- [Credentials (Database Engine)](../../relational-databases/security/authentication-access/credentials-database-engine.md)
- [CREATE LOGIN (Transact-SQL)](create-login-transact-sql.md)
- [DROP LOGIN (Transact-SQL)](drop-login-transact-sql.md)
- [CREATE CREDENTIAL (Transact-SQL)](create-credential-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
- [Extensible Key Management (EKM)](../../relational-databases/security/encryption/extensible-key-management-ekm.md)

::: moniker-end

::: moniker range=">=aps-pdw-2016"

:::row:::
    :::column:::
        [SQL Server](alter-login-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Database](alter-login-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed Instance](alter-login-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-login-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        ***\* Analytics<br />Platform System (PDW) \****
    :::column-end:::
:::row-end:::

&nbsp;

## Analytics Platform System

### Syntax for Analytics Platform System

```syntaxsql
ALTER LOGIN login_name
    {
    <status_option>
    | WITH <set_option> [ , ... ]
    }

<status_option> ::= ENABLE | DISABLE

<set_option> ::=
    PASSWORD = 'password'
    [
      OLD_PASSWORD = 'oldpassword'
      | <password_option> [ <password_option> ]
    ]
    | NAME = login_name
    | CHECK_POLICY = { ON | OFF }
    | CHECK_EXPIRATION = { ON | OFF }

<password_option> ::=
    MUST_CHANGE | UNLOCK
```

## Arguments

#### *login_name*

Specifies the name of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login that is being changed. Domain logins must be enclosed in brackets in the format [domain\user].

#### ENABLE | DISABLE

Enables or disables this login. Disabling a login doesn't affect the behavior of logins that are already connected. (Use the `KILL` statement to terminate an existing connection.) Disabled logins retain their permissions and can still be impersonated.

#### PASSWORD = '*password*'

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies the password for the login that is being changed. Passwords are case-sensitive.

> [!IMPORTANT]  
> When a login (or a contained database user) connects and is authenticated, the connection caches identity information about the login. For a Windows Authentication login, this includes information about membership in Windows groups. The identity of the login remains authenticated as long as the connection is maintained. To force changes in the identity, such as a password reset or change in Windows group membership, the login must sign out from the authentication authority (Windows or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]), and sign in again. A member of the **sysadmin** fixed server role or any login with the **ALTER ANY CONNECTION** permission can use the **KILL** command to end a connection and force a login to reconnect. [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] can reuse connection information when opening multiple connections to Object Explorer and Query Editor windows. Close all connections to force reconnection.

#### OLD_PASSWORD = '*old_password*'

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. The current password of the login to which a new password will be assigned. Passwords are case-sensitive.

#### MUST_CHANGE

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. If this option is included, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] prompts for an updated password the first time the altered login is used.

#### NAME = *login_name*

The new name of the login that is being renamed. If the login is a Windows login, the SID of the Windows principal corresponding to the new name must match the SID associated with the login in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The new name of a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login can't contain a backslash character (\\).

#### CHECK_EXPIRATION = { ON | OFF }

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies whether password expiration policy should be enforced on this login. The default value is OFF.

#### CHECK_POLICY = { ON | OFF }

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies that the Windows password policies of the computer on which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running should be enforced on this login. The default value is ON.

#### UNLOCK

Applies only to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins. Specifies that a login that is locked out should be unlocked.

## Remarks

When CHECK_POLICY is set to ON, the HASHED argument can't be used.

When CHECK_POLICY is changed to ON, the following behavior occurs:

- The password history is initialized with the value of the current password hash.

When CHECK_POLICY is changed to OFF, the following behavior occurs:

- CHECK_EXPIRATION is also set to OFF.
- The password history is cleared.
- The value of *lockout_time* is reset.

If MUST_CHANGE is specified, CHECK_EXPIRATION and CHECK_POLICY must be set to ON. Otherwise, the statement fails.

If CHECK_POLICY is set to OFF, CHECK_EXPIRATION can't be set to ON. An ALTER LOGIN statement that has this combination of options fail.

You can't use ALTER_LOGIN with the DISABLE argument to deny access to a Windows group. This is by design. For example, ALTER_LOGIN [*domain\group*] DISABLE returns the following error message:

`"Msg 15151, Level 16, State 1, Line 1
"Cannot alter the login '*Domain\Group*', because it doesn't exist or you don't have permission."`

In [!INCLUDE [ssSDS](../../includes/sssds-md.md)], login data required to authenticate a connection and server-level firewall rules are temporarily cached in each database. This cache is periodically refreshed. To force a refresh of the authentication cache and make sure that a database has the latest version of the logins table, execute [DBCC FLUSHAUTHCACHE](../../t-sql/database-console-commands/dbcc-flushauthcache-transact-sql.md).

## Permissions

Requires ALTER ANY LOGIN permission.

If the CREDENTIAL option is used, also requires ALTER ANY CREDENTIAL permission.

If the login that is being changed is a member of the **sysadmin** fixed server role or a grantee of CONTROL SERVER permission, also requires CONTROL SERVER permission when making the following changes:

- Resetting the password without supplying the old password.
- Enabling MUST_CHANGE, CHECK_POLICY, or CHECK_EXPIRATION.
- Changing the login name.
- Enabling or disabling the login.
- Mapping the login to a different credential.

A principal can change the password, default language, and default database for its own login.

## Examples

These examples also include examples for using other SQL products. See which previous arguments are supported.

### A. Enable a disabled login

The following example enables the login `Mary5`.

```sql
ALTER LOGIN Mary5 ENABLE;
```

### B. Change the password of a login

The following example changes the password of login `Mary5` to a strong password.

```sql
ALTER LOGIN Mary5 WITH PASSWORD = '<enterStrongPasswordHere>';
```

### C. Change the name of a login

The following example changes the name of login `Mary5` to `John2`.

```sql
ALTER LOGIN Mary5 WITH NAME = John2;
```

### D. Map a login to a credential

The following example maps the login `John2` to the credential `Custodian04`.

```sql
ALTER LOGIN John2 WITH CREDENTIAL = Custodian04;
```

### E. Map a login to an Extensible Key Management credential

The following example maps the login `Mary5` to the EKM credential `EKMProvider1`.

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later.

```sql
ALTER LOGIN Mary5 ADD CREDENTIAL EKMProvider1;
GO
```

### F. Unlock a login

To unlock a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login, execute the following statement, replacing `****` with the desired account password.

```sql
ALTER LOGIN [Mary5] WITH PASSWORD = '****' UNLOCK;
GO
```

To unlock a login without changing the password, turn off the check policy and then on again.

```sql
ALTER LOGIN [Mary5] WITH CHECK_POLICY = OFF;

ALTER LOGIN [Mary5] WITH CHECK_POLICY = ON;
GO
```

### G. Change the password of a login using HASHED

The following example changes the password of the `TestUser` login to an already hashed value.

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later.

```sql
ALTER LOGIN TestUser WITH PASSWORD = 0x01000CF35567C60BFB41EBDE4CF700A985A13D773D6B45B90900 HASHED;
GO
```

## Related content

- [Credentials (Database Engine)](../../relational-databases/security/authentication-access/credentials-database-engine.md)
- [CREATE LOGIN (Transact-SQL)](create-login-transact-sql.md)
- [DROP LOGIN (Transact-SQL)](drop-login-transact-sql.md)
- [CREATE CREDENTIAL (Transact-SQL)](create-credential-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
- [Extensible Key Management (EKM)](../../relational-databases/security/encryption/extensible-key-management-ekm.md)

::: moniker-end
