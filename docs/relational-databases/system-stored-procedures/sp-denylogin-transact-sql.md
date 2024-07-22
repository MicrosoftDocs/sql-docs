---
title: "sp_denylogin (Transact-SQL)"
description: sp_denylogin Prevents a Windows user or Windows group from connecting to an instance of SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_denylogin_TSQL"
  - "sp_denylogin"
helpviewer_keywords:
  - "sp_denylogin"
dev_langs:
  - "TSQL"
---
# sp_denylogin (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Prevents a Windows user or Windows group from connecting to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_denylogin [ @loginame = ] N'loginame'
[ ; ]
```

## Arguments

#### [ @loginame = ] N'*loginame*'

The name of a Windows user or group. *@loginame* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_denylogin` denies `CONNECT` SQL permission to the server-level principal mapped to the specified Windows user or Windows group. If the server principal doesn't exist, it's created. The new principal is visible in the [sys.server_principals](../system-catalog-views/sys-server-principals-transact-sql.md) catalog view.

`sp_denylogin` can't be executed within a user-defined transaction.

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Examples

The following example shows how to use `sp_denylogin` to prevent Windows user `CORPORATE\GeorgeV` from connecting to the server.

```sql
EXEC sp_denylogin 'CORPORATE\GeorgeV';
```

## Related content

- [sp_grantlogin (Transact-SQL)](sp-grantlogin-transact-sql.md)
- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [ALTER LOGIN (Transact-SQL)](../../t-sql/statements/alter-login-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
