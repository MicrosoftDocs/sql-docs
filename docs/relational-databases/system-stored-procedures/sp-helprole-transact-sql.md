---
title: "sp_helprole (Transact-SQL)"
description: sp_helprole returns information about the roles in the current database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helprole_TSQL"
  - "sp_helprole"
helpviewer_keywords:
  - "sp_helprole"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_helprole (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns information about the roles in the current database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helprole [ [ @rolename = ] N'rolename' ]
[ ; ]
```

## Arguments

#### [ @rolename = ] N'*rolename*'

The name of a role in the current database. *@rolename* is **sysname**, with a default of `NULL`. *@rolename* must exist in the current database. If *@rolename* isn't specified, information about all roles in the current database is returned.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `RoleName` | **sysname** | Name of the role in the current database. |
| `RoleId` | **smallint** | ID of `RoleName`. |
| `IsAppRole` | **int** | `0` = `RoleName` isn't an application role.<br />`1` = `RoleName` is an application role. |

## Remarks

To view the permissions associated with the role, use `sp_helprotect`. To view the members of a database role, use `sp_helprolemember`.

## Permissions

Requires membership in the **public** role.

## Examples

The following query returns all the roles in the current database.

```sql
EXEC sp_helprole;
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [Server-level roles](../security/authentication-access/server-level-roles.md)
- [Database-level roles](../security/authentication-access/database-level-roles.md)
- [sp_addapprole (Transact-SQL)](sp-addapprole-transact-sql.md)
- [sp_addrole (Transact-SQL)](sp-addrole-transact-sql.md)
- [sp_droprole (Transact-SQL)](sp-droprole-transact-sql.md)
- [sp_helprolemember (Transact-SQL)](sp-helprolemember-transact-sql.md)
- [sp_helpsrvrolemember (Transact-SQL)](sp-helpsrvrolemember-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
