---
title: "sp_defaultdb (Transact-SQL)"
description: sp_defaultdb changes the default database for a SQL Server login.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_defaultdb_TSQL"
  - "sp_defaultdb"
helpviewer_keywords:
  - "sp_defaultdb"
dev_langs:
  - "TSQL"
---
# sp_defaultdb (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the default database for a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_defaultdb
    [ @loginame = ] N'loginame'
    , [ @defdb = ] N'defdb'
[ ; ]
```

## Arguments

#### [ @loginame = ] N'*loginame*'

The login name. *@loginame* is **sysname**, with no default. *@loginame* can be an existing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login or a Windows user or group. If a login for the Windows user or group doesn't exist in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], it's automatically added.

#### [ @defdb = ] N'*defdb*'

The name of the new default database. *@defdb* is **sysname**, with no default. *@defdb* must already exist.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_defaultdb` calls `ALTER LOGIN`, which supports extra options. For information about changing default database, see [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md).

`sp_defaultdb` can't be executed within a user-defined transaction.

## Permissions

Requires `ALTER ANY LOGIN` permission.

## Examples

The following example sets [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] as the default database for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login `Victoria`.

```sql
EXEC sp_defaultdb 'Victoria', 'AdventureWorks2022';
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [ALTER LOGIN (Transact-SQL)](../../t-sql/statements/alter-login-transact-sql.md)
- [sp_addlogin (Transact-SQL)](sp-addlogin-transact-sql.md)
- [sp_droplogin (Transact-SQL)](sp-droplogin-transact-sql.md)
- [sp_grantdbaccess (Transact-SQL)](sp-grantdbaccess-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
