---
title: "sp_revokelogin (Transact-SQL)"
description: sp_revokelogin Removes the login entries from SQL Server for a Windows user or group.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_revokelogin_TSQL"
  - "sp_revokelogin"
helpviewer_keywords:
  - "sp_revokelogin"
dev_langs:
  - "TSQL"
---
# sp_revokelogin (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes the login entries from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] for a Windows user or group created by using `CREATE LOGIN`, `sp_grantlogin`, or `sp_denylogin`.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [DROP LOGIN](../../t-sql/statements/drop-login-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_revokelogin [ @loginame = ] N'loginame'
[ ; ]
```

## Arguments

#### [ @loginame = ] N'*loginame*'

The name of the Windows user or group. *@loginame* is **sysname**, with no default. *@loginame* can be any existing Windows user name or group in the form `<ComputerName>\<User>` or `<Domain>\<User>`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_revokelogin` disables connections using the account specified by *@loginame*. Windows users that are granted access to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] through membership in a Windows group, can still connect as the group after their individual access has been revoked. Similarly, if *@loginame* specifies the name of a Windows group, members of that group that have been separately granted access to the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can still connect.

For example, if Windows user `ADVWORKS\john` is a member of the Windows group `ADVWORKS\Admins`, and `sp_revokelogin` revokes the access of `ADVWORKS\john`:

```sql
EXEC sp_revokelogin [ADVWORKS\john]
```

User `ADVWORKS\john` can still connect if `ADVWORKS\Admins` is granted access to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Similarly, if Windows group `ADVWORKS\Admins` has its access revoked but `ADVWORKS\john` is granted access, `ADVWORKS\john` can still connect.

Use `sp_denylogin` to explicitly prevent users from connecting to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], regardless of their Windows group memberships.

`sp_revokelogin` can't be executed within a user-defined transaction.

## Permissions

Requires `ALTER ANY LOGIN` permission on the server.

## Examples

The following example removes the login entries for the Windows user `Corporate\MollyA`.

```sql
EXEC sp_revokelogin 'Corporate\MollyA';
```

Or

```sql
EXEC sp_revokelogin [Corporate\MollyA];
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [DROP LOGIN (Transact-SQL)](../../t-sql/statements/drop-login-transact-sql.md)
- [sp_denylogin (Transact-SQL)](sp-denylogin-transact-sql.md)
- [sp_droplogin (Transact-SQL)](sp-droplogin-transact-sql.md)
- [sp_grantlogin (Transact-SQL)](sp-grantlogin-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
