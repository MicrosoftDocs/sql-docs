---
title: "sp_grantlogin (Transact-SQL)"
description: sp_grantlogin creates a SQL Server login.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_grantlogin_TSQL"
  - "sp_grantlogin"
helpviewer_keywords:
  - "sp_grantlogin"
dev_langs:
  - "TSQL"
---
# sp_grantlogin (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Creates a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE LOGIN](../../t-sql/statements/create-login-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_grantlogin [ @loginame = ] N'loginame'
[ ; ]
```

## Arguments

#### [ @loginame = ] N'*loginame*'

The name of a Windows user or group. *@loginame* is **sysname**, with no default. The Windows user or group must be qualified with a Windows domain name in the form `<domain>\<user>`; for example, `London\Joeb`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_grantlogin` calls `CREATE LOGIN`, which supports extra options. For information on creating SQL Server logins, see [CREATE LOGIN](../../t-sql/statements/create-login-transact-sql.md)

`sp_grantlogin` can't be executed within a user-defined transaction.

## Permissions

Requires membership in the **securityadmin** fixed server role.

## Examples

The following example uses `CREATE LOGIN` to create a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login for the Windows user `Corporate\BobJ`, which is the preferred method.

```sql
CREATE LOGIN [Corporate\BobJ] FROM WINDOWS;
GO
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [CREATE LOGIN (Transact-SQL)](../../t-sql/statements/create-login-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
