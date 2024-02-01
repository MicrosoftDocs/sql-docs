---
title: "sp_addsrvrolemember (Transact-SQL)"
description: sp_addsrvrolemember adds a security principal as a member of a fixed server role.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_addsrvrolemember"
  - "sp_addsrvrolemember_TSQL"
helpviewer_keywords:
  - "sp_addsrvrolemember"
dev_langs:
  - "TSQL"
---
# sp_addsrvrolemember (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds a login, or security principal, as a member of a fixed server role.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER SERVER ROLE](../../t-sql/statements/alter-server-role-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addsrvrolemember
    [ @loginame = ] N'loginame'
    [ , [ @rolename = ] N'rolename' ]
[ ; ]
```

## Arguments

#### [ @loginame = ] N'*loginame*'

The name of the security principal being added to the fixed server role. *@loginame* is **sysname**, with no default. *@loginame* can be a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login or a Windows account. If the Windows account isn't already granted access to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], access is automatically granted.

#### [ @rolename = ] N'*rolename*'

The name of the fixed server role to which the security principal is being added. *@rolename* is **sysname**, with a default of `NULL`, and must be one of the following values:

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

When a security principal is added to a fixed server role, it gains the permissions associated with that role.

The role membership of the **sa** user and **public** can't be changed.

Use `sp_addrolemember` to add a member to a fixed database or user-defined role.

`sp_addsrvrolemember` can't be executed within a user-defined transaction.

## Permissions

Requires membership in the role to which the new member is being added.

## Examples

The following example adds the Windows account `Corporate\HelenS` to the **sysadmin** fixed server role.

```sql
EXEC sp_addsrvrolemember 'Corporate\HelenS', 'sysadmin';
GO
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [sp_addrolemember (Transact-SQL)](sp-addrolemember-transact-sql.md)
- [sp_dropsrvrolemember (Transact-SQL)](sp-dropsrvrolemember-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Security Functions (Transact-SQL)](../../t-sql/functions/security-functions-transact-sql.md)
- [CREATE SERVER ROLE (Transact-SQL)](../../t-sql/statements/create-server-role-transact-sql.md)
- [DROP SERVER ROLE (Transact-SQL)](../../t-sql/statements/drop-server-role-transact-sql.md)
