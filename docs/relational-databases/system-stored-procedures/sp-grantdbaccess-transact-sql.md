---
title: "sp_grantdbaccess (Transact-SQL)"
description: sp_grantdbaccess adds a database user to the current database.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_grantdbaccess"
  - "sp_grantdbaccess_TSQL"
helpviewer_keywords:
  - "sp_grantdbaccess"
dev_langs:
  - "TSQL"
---
# sp_grantdbaccess (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds a database user to the current database.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE USER](../../t-sql/statements/create-user-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_grantdbaccess
    [ @loginame = ] N'loginame'
    [ , [ @name_in_db = ] N'name_in_db' OUTPUT ]
[ ; ]
```

## Arguments

#### [ @loginame = ] N'*loginame*'

The name of the Windows group, Windows login, or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login, to be mapped to the new database user. *@loginame* is **sysname**, with no default. Names of Windows groups and Windows logins must be qualified with a Windows domain name in the form `<domain>\<login>`; for example, `LONDON\Joeb`. The login can't already be mapped to a user in the database.

#### [ @name_in_db = ] N'*name_in_db*' OUTPUT

The name for the new database user. *@name_in_db* is an OUTPUT parameter of type **sysname**. If not specified, *@loginame* is used. If specified as an OUTPUT variable with a value of `NULL`, *@name_in_db* is set to *@loginame*. *@name_in_db* must not already exist in the current database.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_grantdbaccess` calls `CREATE USER`, which supports extra options. For information about creating database users, see [CREATE USER](../../t-sql/statements/create-user-transact-sql.md). To remove a database user from a database, use [DROP USER](../../t-sql/statements/drop-user-transact-sql.md).

`sp_grantdbaccess` can't be executed within a user-defined transaction.

## Permissions

Requires membership in the **db_owner** fixed database role or the **db_accessadmin** fixed database role.

## Examples

The following example uses `CREATE USER` to add a database user for the Windows account `Edmonds\LolanSo` to the current database, which is the preferred method for creating a database user. The new user is named `Lolan`.

```sql
CREATE USER Lolan FOR LOGIN [Edmonds\LolanSo];
GO
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)
- [DROP USER (Transact-SQL)](../../t-sql/statements/drop-user-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
