---
title: "sp_dropsrvrolemember (Transact-SQL)"
description: sp_dropsrvrolemember removes a SQL Server login, Windows user, or Windows group, from a fixed server role.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dropsrvrolemember"
  - "sp_dropsrvrolemember_TSQL"
helpviewer_keywords:
  - "sp_dropsrvrolemember"
dev_langs:
  - "TSQL"
---
# sp_dropsrvrolemember (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login, a Windows user, or Windows group, from a fixed server role.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER SERVER ROLE](../../t-sql/statements/alter-server-role-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropsrvrolemember
    [ @loginame = ] N'loginame'
    [ , [ @rolename = ] N'rolename' ]
[ ; ]
```

## Arguments

#### [ @loginame = ] N'*loginame*'

The name of a login to remove from the fixed server role. *@loginame* is **sysname**, with no default. *@loginame* must exist.

#### [ @rolename = ] N'*rolename*'

The name of a server role. *@rolename* is **sysname**, with a default of `NULL`. *@rolename* must be one of the following values:

- **sysadmin**
- **securityadmin**
- **serveradmin**
- **setupadmin**
- **processadmin**
- **diskadmin**
- **dbcreator**
- **bulkadmin**

## Return code values

`0` (success) or `1` (failure).

## Remarks

Only `sp_dropsrvrolemember` can be used to remove a login from a fixed server role. Use `sp_droprolemember` to remove a member from a database role.

The `sa` login can't be removed from any fixed server role.

`sp_dropsrvrolemember` can't be executed within a user-defined transaction.

## Permissions

Requires membership in the **sysadmin** fixed server role, or both `ALTER ANY LOGIN` permission on the server, and membership in the role from which the member is being dropped.

## Examples

The following example removes the login `JackO` from the **sysadmin** fixed server role.

```sql
EXEC sp_dropsrvrolemember 'JackO', 'sysadmin';
```

## Related content

- [CREATE SERVER ROLE (Transact-SQL)](../../t-sql/statements/create-server-role-transact-sql.md)
- [DROP SERVER ROLE (Transact-SQL)](../../t-sql/statements/drop-server-role-transact-sql.md)
- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [sp_addsrvrolemember (Transact-SQL)](sp-addsrvrolemember-transact-sql.md)
- [sp_droprolemember (Transact-SQL)](sp-droprolemember-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Security Functions (Transact-SQL)](../../t-sql/functions/security-functions-transact-sql.md)
