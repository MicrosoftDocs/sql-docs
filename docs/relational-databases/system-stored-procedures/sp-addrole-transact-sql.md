---
title: "sp_addrole (Transact-SQL)"
description: sp_addrole creates a new database role in the current database.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_addrole"
  - "sp_addrole_TSQL"
helpviewer_keywords:
  - "sp_addrole"
dev_langs:
  - "TSQL"
---
# sp_addrole (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Creates a new database role in the current database.

> [!IMPORTANT]  
> `sp_addrole` is included for compatibility with earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and might not be supported in a future release. Use [CREATE ROLE](../../t-sql/statements/create-role-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addrole
    [ @rolename = ] N'rolename'
    [ , [ @ownername = ] N'ownername' ]
[ ; ]
```

## Arguments

#### [ @rolename = ] N'*rolename*'

The name of the new database role. *@rolename* is **sysname**, with no default. *@rolename* must be a valid identifier and must not already exist in the current database.

#### [ @ownername = ] N'*ownername*'

The owner of the new database role. *@ownername* is **sysname**, with a default of the current executing user. *@ownername* must be a database user or database role in the current database.

## Return code values

`0` (success) or `1` (failure).

## Remarks

The names of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database roles can contain from 1 through 128 characters, including letters, symbols, and numbers. The names of database roles can't contain a backslash character (`\`), be `NULL`, or an empty string (`''`).

After you add a database role, use [sp_addrolemember](sp-addrolemember-transact-sql.md) to add principals to the role. When `GRANT`, `DENY`, or `REVOKE` statements are used to apply permissions to the database role, members of the database role inherit those permissions as if the permissions were applied directly to their accounts.

> [!NOTE]  
> New server roles can't be created. Roles can only be created at the database level.

`sp_addrole` can't be used inside a user-defined transaction.

## Permissions

Requires `CREATE ROLE` permission on the database. If creating a schema, requires `CREATE SCHEMA` on the database. If *@ownername* is specified as a user or group, requires `IMPERSONATE` on that user or group. If *@ownername* is specified as a role, requires `ALTER` permission on that role or on a member of that role. If owner is specified as an application role, requires `ALTER` permission on that application role.

## Examples

The following example adds a new role called `Managers` to the current database.

```sql
EXEC sp_addrole 'Managers';
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [CREATE ROLE (Transact-SQL)](../../t-sql/statements/create-role-transact-sql.md)
