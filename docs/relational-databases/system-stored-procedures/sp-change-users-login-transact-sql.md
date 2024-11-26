---
title: "sp_change_users_login (Transact-SQL)"
description: sp_change_users_login Maps an existing database user to a SQL Server login.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_change_users_login"
  - "sp_change_users_login_TSQL"
helpviewer_keywords:
  - "sp_change_users_login"
dev_langs:
  - "TSQL"
---
# sp_change_users_login (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Maps an existing database user to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER USER](../../t-sql/statements/alter-user-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_change_users_login
    [ @Action = ] 'Action'
    [ , [ @UserNamePattern = ] N'UserNamePattern' ]
    [ , [ @LoginName = ] N'LoginName' ]
    [ , [ @Password = ] N'Password' ]
[ ; ]
```

## Arguments

#### [ @Action = ] '*Action*'

Describes the action for the stored procedure to perform. *@Action* is **varchar(10)**, with no default, and can have one of the following values.

| Value | Description |
| --- | --- |
| `Auto_Fix` | Links a user entry in the `sys.database_principals` system catalog view in the current database to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login of the same name. If a login with the same name doesn't exist, one is created. Examine the result from the `Auto_Fix` statement, to confirm that the correct link is in fact made. Avoid using `Auto_Fix` in security-sensitive situations.<br /><br />When you use `Auto_Fix`, you must specify *@UserNamePattern* and *@Password* if the login doesn't already exist, otherwise you must specify *@UserNamePattern* but *@Password* is ignored. *@LoginName* must be `NULL`. *@UserNamePattern* must be a valid user in the current database. The login can't have another user mapped to it. |
| `Report` | Lists the users and corresponding security identifiers (SID) in the current database that aren't linked to any login. *@UserNamePattern*, *@LoginName*, and *@Password* must be `NULL` or not specified.<br /><br />To replace the report option with a query using the system tables, compare the entries in `sys.server_principals` with the entries in `sys.database_principals`. |
| `Update_One` | Links the specified *@UserNamePattern* in the current database to an existing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] *@LoginName*. *@UserNamePattern* and *@LoginName* must be specified. *@Password* must be `NULL` or not specified. |

#### [ @UserNamePattern = ] N'*UserNamePattern*'

The name of a user in the current database. *@UserNamePattern* is **sysname**, with a default of `NULL`.

#### [ @LoginName = ] N'*LoginName*'

The name of a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login. *@LoginName* is **sysname**, with a default of `NULL`.

#### [ @Password = ] N'*Password*'

The password assigned to a new [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login that is created by specifying `Auto_Fix`. *@Password* is **sysname**, and can't be `NULL`. If a matching login already exists, the user and login are mapped and *@Password* is ignored. If a matching login doesn't exist, `sp_change_users_login` creates a new [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login and assigns *@Password* as the password for the new login.

> [!IMPORTANT]  
> Always use a [strong password](../security/strong-passwords.md).

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `UserName` | **sysname** | Database user name. |
| `UserSID` | **varbinary(85)** | User's security identifier. |

## Remarks

Use `sp_change_users_login` to link a database user in the current database with a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login. If the login for a user changes, use `sp_change_users_login` to link the user to the new login without losing user permissions. The new *@LoginName* can't be `sa`, and the *@UserNamePattern* can't be `dbo`, `guest`, or an `INFORMATION_SCHEMA` user.

`sp_change_users_login` can't be used to map database users to Windows-level principals, certificates, or asymmetric keys.

`sp_change_users_login` can't be used with a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login created from a Windows principal or with a user created by using `CREATE USER WITHOUT LOGIN`.

`sp_change_users_login` can't be executed within a user-defined transaction.

## Permissions

Requires membership in the **db_owner** fixed database role. Only members of the **sysadmin** fixed server role can specify the `Auto_Fix` option.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Show a report of the current user to login mappings

The following example produces a report of the users in the current database and their security identifiers (SIDs).

```sql
EXEC sp_change_users_login 'Report';
```

### B. Map a database user to a new SQL Server login

In the following example, a database user is associated with a new [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login. Database user `MB-Sales`, which at first is mapped to another login, is remapped to login `MaryB`.

```sql
--Create the new login.
CREATE LOGIN MaryB WITH PASSWORD = '982734snfdHHkjj3';
GO
--Map database user MB-Sales to login MaryB.
USE AdventureWorks2022;
GO
EXEC sp_change_users_login 'Update_One', 'MB-Sales', 'MaryB';
GO
```

### C. Automatically map a user to a login, and create a new login if necessary

The following example shows how to use `Auto_Fix` to map an existing user to a login of the same name, or to create the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login `Mary` that's the password `B3r12-3x$098f6` if the login `Mary` doesn't exist.

```sql
USE AdventureWorks2022;
GO
EXEC sp_change_users_login 'Auto_Fix', 'Mary', NULL, 'B3r12-3x$098f6';
GO
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [CREATE LOGIN (Transact-SQL)](../../t-sql/statements/create-login-transact-sql.md)
- [sp_adduser (Transact-SQL)](sp-adduser-transact-sql.md)
- [sp_helplogins (Transact-SQL)](sp-helplogins-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [sys.database_principals (Transact-SQL)](../system-catalog-views/sys-database-principals-transact-sql.md)
