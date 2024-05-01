---
title: "sp_droprole (Transact-SQL)"
description: sp_droprole removes a database role from the current database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_droprole"
  - "sp_droprole_TSQL"
helpviewer_keywords:
  - "sp_droprole"
dev_langs:
  - "TSQL"
---
# sp_droprole (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes a database role from the current database.

> [!IMPORTANT]  
> In [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)], `sp_droprole` was replaced by the DROP ROLE statement. `sp_droprole` is included only for compatibility with earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and might not be supported in a future release.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_droprole [ @rolename = ] N'rolename'
[ ; ]
```

## Arguments

#### [ @rolename = ] N'*rolename*'

The name of the database role to remove from the current database. *@rolename* is **sysname**, with no default. *@rolename* must already exist in the current database.

## Return code values

`0` (success) or `1` (failure).

## Remarks

Only database roles can be removed by using `sp_droprole`.

A database role with existing members can't be removed. All members of a database role must be removed before the database role can be removed. To remove users from a role, use `sp_droprolemember`. If any users are still members of the role, `sp_droprole` displays those members.

Fixed roles and the **public** role can't be removed.

A role can't be removed if it owns any securables. Before dropping an application role that owns securables, you must first transfer ownership of the securables, or drop them. Use `ALTER AUTHORIZATION` to change the owner of objects that must not be removed.

`sp_droprole` can't be executed within a user-defined transaction.

## Permissions

Requires `CONTROL` permission on the role.

## Examples

The following example removes the application role `Sales`.

```sql
EXEC sp_droprole 'Sales';
GO
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [sp_addrole (Transact-SQL)](sp-addrole-transact-sql.md)
- [DROP ROLE (Transact-SQL)](../../t-sql/statements/drop-role-transact-sql.md)
- [ALTER AUTHORIZATION (Transact-SQL)](../../t-sql/statements/alter-authorization-transact-sql.md)
- [sp_dropapprole (Transact-SQL)](sp-dropapprole-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
