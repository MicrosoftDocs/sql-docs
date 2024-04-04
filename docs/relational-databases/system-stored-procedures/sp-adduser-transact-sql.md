---
title: "sp_adduser (Transact-SQL)"
description: sp_adduser adds a new user to the current database.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_adduser"
  - "sp_adduser_TSQL"
helpviewer_keywords:
  - "sp_adduser"
dev_langs:
  - "TSQL"
---
# sp_adduser (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds a new user to the current database.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE USER](../../t-sql/statements/create-user-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_adduser
    [ @loginame = ] N'loginame'
    [ , [ @name_in_db = ] N'name_in_db' ]
    [ , [ @grpname = ] N'grpname' ]
[ ; ]
```

## Arguments

#### [ @loginame = ] N'*loginame*'

The name of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login or Windows account. *@loginame* is **sysname**, with no default. *@loginame* must be an existing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login or Windows account.

#### [ @name_in_db = ] N'*name_in_db*'

The name for the new database user. *@name_in_db* is **sysname**, with a default of `NULL`. If *@name_in_db* isn't specified, the name of the new database user defaults to *@loginame*. Specifying *@name_in_db* gives the new user a name in the database different from the server-level login name.

#### [ @grpname = ] N'*grpname*'

The database role of which the new user becomes a member. *@grpname* is **sysname**, with a default of `NULL`. *@grpname* must be a valid database role in the current database.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_adduser` also creates a schema that's the name of the user.

After a user is added, use the `GRANT`, `DENY`, and `REVOKE` statements to define the permissions that control the activities performed by the user.

Use `sys.server_principals` to display a list of valid logins.

Use `sp_helprole` to display a list of the valid role names. When you specify a role, the user automatically gains the permissions that are defined for the role. If a role isn't specified, the user gains the permissions granted to the default **public** role. To add a user to a role, a value for the *@name_in_db* must be supplied. (*@name_in_db* can be the same as *@loginame*.)

User **guest** already exists in every database. Adding user **guest** enables this user, if it was previously disabled. By default, user **guest** is disabled in new databases.

`sp_adduser` can't be executed inside a user-defined transaction.

You can't add a **guest** user because a **guest** user already exists inside every database. To enable the **guest** user, grant **guest** CONNECT permission as shown:

```sql
GRANT CONNECT TO guest;
GO
```

## Permissions

Requires ownership of the database.

## Examples

### A. Add a database user

The following example adds the database user `Vidur` to the existing `Recruiting` role in the current database, using the existing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login `Vidur`.

```sql
EXEC sp_adduser 'Vidur', 'Vidur', 'Recruiting';
```

### B. Add a database user with the same login ID

The following example adds user `Arvind` to the current database for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login `Arvind`. This user belongs to the default **public** role.

```sql
EXEC sp_adduser 'Arvind';
```

### C. Add a database user with a different name than its server-level login

The following example adds [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login `BjornR` to the current database that's a user name of `Bjorn`, and adds database user `Bjorn` to the `Production` database role.

```sql
EXEC sp_adduser 'BjornR', 'Bjorn', 'Production';
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [sys.server_principals (Transact-SQL)](../system-catalog-views/sys-server-principals-transact-sql.md)
- [sp_addrole (Transact-SQL)](sp-addrole-transact-sql.md)
- [CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)
- [sp_dropuser (Transact-SQL)](sp-dropuser-transact-sql.md)
- [sp_grantdbaccess (Transact-SQL)](sp-grantdbaccess-transact-sql.md)
- [sp_grantlogin (Transact-SQL)](sp-grantlogin-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
