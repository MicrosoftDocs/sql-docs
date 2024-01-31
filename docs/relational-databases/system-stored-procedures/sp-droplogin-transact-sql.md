---
title: "sp_droplogin (Transact-SQL)"
description: sp_droplogin removes a SQL Server login.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_droplogin"
  - "sp_droplogin_TSQL"
helpviewer_keywords:
  - "sp_droplogin"
dev_langs:
  - "TSQL"
---
# sp_droplogin (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login, which prevents access to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] under that login name.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [DROP LOGIN](../../t-sql/statements/drop-login-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_droplogin [ @loginame = ] N'loginame'
[ ; ]
```

## Arguments

#### [ @loginame = ] N'*loginame*'

The login to be removed. *@loginame* is **sysname**, with no default. *@loginame* must already exist in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_droplogin` calls `DROP LOGIN`.

`sp_droplogin` can't be executed within a user-defined transaction.

## Permissions

Requires `ALTER ANY LOGIN` permission on the server.

## Examples

The following example uses `DROP LOGIN` to remove the login `Victoria` from an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This method is preferred.

```sql
DROP LOGIN Victoria;
GO
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [DROP LOGIN (Transact-SQL)](../../t-sql/statements/drop-login-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
