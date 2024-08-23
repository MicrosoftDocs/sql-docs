---
title: "sp_addlogin (Transact-SQL)"
description: Creates a new SQL Server login that allows a user to connect to a SQL Server instance with SQL Server authentication.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_addlogin"
  - "sp_addlogin_TSQL"
helpviewer_keywords:
  - "sp_addlogin"
dev_langs:
  - "TSQL"
---
# sp_addlogin (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Creates a new [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login that allows a user to connect to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE LOGIN](../../t-sql/statements/create-login-transact-sql.md) instead.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addlogin
    [ @loginame = ] N'loginame'
    [ , [ @passwd = ] N'passwd' ]
    [ , [ @defdb = ] N'defdb' ]
    [ , [ @deflanguage = ] N'deflanguage' ]
    [ , [ @sid = ] sid ]
    [ , [ @encryptopt = ] 'encryptopt' ]
[ ; ]
```

## Arguments

#### [ @loginame = ] N'*loginame*'

The name of the login. *@loginame* is **sysname**, with no default.

#### [ @passwd = ] N'*passwd*'

The login password. *@passwd* is **sysname**, with a default of `NULL`.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]

#### [ @defdb = ] N'*defdb*'

The default database of the login (the database to which the login is first connected after logging in). *@defdb* is **sysname**, with a default of `master`.

#### [ @deflanguage = ] N'*deflanguage*'

The default language of the login. *@deflanguage* is **sysname**, with a default of `NULL`. If *@deflanguage* isn't specified, the default *@deflanguage* of the new login is set to the current default language of the server.

#### [ @sid = ] *sid*

The security identification number (SID). *@sid* is **varbinary(16)**, with a default of `NULL`. If *@sid* is `NULL`, the system generates a SID for the new login. Despite the use of a **varbinary** data type, values other than `NULL` must be exactly 16 bytes in length, and can't already exist. Specifying *@sid* is useful, for example, when you're scripting or moving [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins from one server to another and you want the logins to have the same SID on different servers.

#### [ @encryptopt = ] '*encryptopt*'

Specifies whether the password is passed in as clear text or as the hash of the clear text password. No encryption takes place. The word "encrypt" is used in this discussion for the sake of backward compatibility. If a clear text password is passed in, it's hashed. The hash is stored. *@encryptopt* is **varchar(20)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `NULL` (default) | The password is passed in clear. |
| `skip_encryption` | The password is already hashed. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] should store the value without rehashing it. |
| `skip_encryption_old` | The supplied password was hashed by an earlier version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] should store the value without rehashing it. This option is provided for upgrade purposes only. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins can contain from 1 to 128 characters, including letters, symbols, and numbers. Logins can't contain a backslash (`\`); be a reserved login name, for example **sa** or **public**, or already exist; or be `NULL` or an empty string.

If the name of a default database is supplied, you can connect to the specified database without executing the `USE` statement. However, you can't use the default database until you're given access to that database by the database owner (by using [sp_adduser](sp-adduser-transact-sql.md), [sp_addrolemember](sp-addrolemember-transact-sql.md)), or [sp_addrole](sp-addrole-transact-sql.md).

The SID number is a GUID that uniquely identifies the login in the server.

Changing the default language of the server doesn't change the default language of existing logins. To change the default language of the server, use [sp_configure](sp-configure-transact-sql.md).

Using `skip_encryption` to suppress password hashing is useful if the password is already hashed when the login is added to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If the password was hashed by an earlier version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], use `skip_encryption_old`.

`sp_addlogin` can't be executed within a user-defined transaction.

The following table shows several stored procedures that are used with `sp_addlogin`.

| Stored procedure | Description |
| --- | --- |
| [sp_grantlogin](sp-grantlogin-transact-sql.md) | Adds a Windows user or group. |
| [sp_password](sp-password-transact-sql.md) | Changes the password of a user. |
| [sp_defaultdb](sp-defaultdb-transact-sql.md) | Changes the default database of a user. |
| [sp_defaultlanguage](sp-defaultlanguage-transact-sql.md) | Changes the default language of a user. |

## Permissions

Requires ALTER ANY LOGIN permission.

## Examples

### A. Create a SQL Server login

The following example creates a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login for the user `Victoria`, with a password of `B1r12-36`, without specifying a default database.

```sql
EXEC sp_addlogin 'Victoria', 'B1r12-36';
GO
```

### B. Create a SQL Server login that has a default database

The following example creates a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login for the user `Albert`, with a password of `B5432-3M6` and a default database of `corporate`.

```sql
EXEC sp_addlogin 'Albert', 'B5432-3M6', 'corporate';
GO
```

### C. Create a SQL Server login that has a different default language

The following example creates a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login for the user `TzTodorov`, with a password of `709hLKH7chjfwv`, a default database of [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)], and a default language of `Bulgarian`.

```sql
EXEC sp_addlogin 'TzTodorov', '709hLKH7chjfwv', 'AdventureWorks2022', N'български'
```

### D. Create a SQL Server login that has a specific SID

The following example creates a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login for the user `Michael`, with a password of `B548bmM%f6`, a default database of [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)], a default language of `us_english`, and a SID of `0x0123456789ABCDEF0123456789ABCDEF`.

```sql
EXEC sp_addlogin 'Michael', 'B548bmM%f6', 'AdventureWorks2022', 'us_english', 0x0123456789ABCDEF0123456789ABCDEF
```

## Related content

- [CREATE LOGIN (Transact-SQL)](../../t-sql/statements/create-login-transact-sql.md)
- [sp_droplogin (Transact-SQL)](sp-droplogin-transact-sql.md)
- [sp_helpuser (Transact-SQL)](sp-helpuser-transact-sql.md)
- [sp_revokelogin (Transact-SQL)](sp-revokelogin-transact-sql.md)
- [xp_logininfo (Transact-SQL)](xp-logininfo-transact-sql.md)
