---
title: "sp_password (Transact-SQL)"
description: sp_password adds or changes a password for a SQL Server login.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_password"
  - "sp_password_TSQL"
helpviewer_keywords:
  - "sp_password"
dev_langs:
  - "TSQL"
---
# sp_password (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds or changes a password for a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_password
    [ [ @old = ] N'old' ]
    , [ @new = ] N'new'
    [ , [ @loginame = ] N'loginame' ]
[ ; ]
```

## Arguments

#### [ @old = ] N'*old*'

The old password. *@old* is **sysname**, with a default of `NULL`.

#### [ @new = ] N'*new*'

The new password. *@new* is **sysname**, with no default. *@old* must be specified if named parameters aren't used.

> [!IMPORTANT]  
> Don't use a `NULL` password. Use a strong password. For more information, see [Strong Passwords](../security/strong-passwords.md).

#### [ @loginame = ] N'*loginame*'

The name of the login affected by the password change. *@loginame* is **sysname**, with a default of `NULL`. *@loginame* must already exist and can be specified only by members of the **sysadmin** or **securityadmin** fixed server roles.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_password` calls `ALTER LOGIN`. This statement supports more options. For information on changing passwords, see [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md).

`sp_password` can't be executed within a user-defined transaction.

## Permissions

Requires `ALTER ANY LOGIN` permission. Also requires `CONTROL SERVER` permission to reset a password without supplying the old password, or if the login that is being changed has `CONTROL SERVER` permission.

A principal can change its own password.

## Examples

### A. Change the password of a login without knowing the old password

The following example shows how to use `ALTER LOGIN` to change the password for the login `Victoria` to `B3r1000d#2-36`. This method is preferred. The user that is executing this command must have `CONTROL SERVER` permission.

```sql
ALTER LOGIN Victoria
    WITH PASSWORD = 'B3r1000d#2-36';
GO
```

### B. Change a password

The following example shows how to use `ALTER LOGIN` to change the password for the login `Victoria` from `B3r1000d#2-36` to `V1cteAmanti55imE`. This method is preferred. User `Victoria` can execute this command without extra permissions. Other users require `ALTER ANY LOGIN` permission.

```sql
ALTER LOGIN Victoria
    WITH PASSWORD = 'V1cteAmanti55imE'
    OLD_PASSWORD = 'B3r1000d#2-36';
GO
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [ALTER LOGIN (Transact-SQL)](../../t-sql/statements/alter-login-transact-sql.md)
- [CREATE LOGIN (Transact-SQL)](../../t-sql/statements/create-login-transact-sql.md)
- [sp_addlogin (Transact-SQL)](sp-addlogin-transact-sql.md)
- [sp_adduser (Transact-SQL)](sp-adduser-transact-sql.md)
- [sp_grantlogin (Transact-SQL)](sp-grantlogin-transact-sql.md)
- [sp_revokelogin (Transact-SQL)](sp-revokelogin-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
