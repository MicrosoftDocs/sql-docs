---
title: "sp_helprolemember (Transact-SQL)"
description: sp_helprolemember returns information about the direct members of a role in the current database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helprolemember_TSQL"
  - "sp_helprolemember"
helpviewer_keywords:
  - "sp_helprolemember"
dev_langs:
  - "TSQL"
---
# sp_helprolemember (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information about the direct members of a role in the current database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helprolemember [ [ @rolename = ] N'rolename' ]
[ ; ]
```

## Arguments

#### [ @rolename = ] N'*rolename*'

The name of a role in the current database. *@rolename* is **sysname**, with a default of `NULL`. *@rolename* must exist in the current database. If *@rolename* isn't specified, then all roles that contain at least one member from the current database are returned.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `DbRole` | **sysname** | Name of the role in the current database. |
| `MemberName` | **sysname** | Name of a member of `DbRole`. |
| `MemberSID` | **varbinary(85)** | Security identifier of `MemberName`. |

## Remarks

If the database contains nested roles, `MemberName` might be the name of a role. `sp_helprolemember` doesn't show membership obtained through nested roles. For example if `User1` is a member of `Role1`, and `Role1` is a member of `Role2`, `EXEC sp_helprolemember 'Role2';` returns `Role1`, but not the members of `Role1` (`User1` in this example). To return nested memberships, you must execute `sp_helprolemember` repeatedly for each nested role.

Use `sp_helpsrvrolemember` to display the members of a fixed server role.

Use [IS_ROLEMEMBER](../../t-sql/functions/is-rolemember-transact-sql.md) to check role membership for a specified user.

## Permissions

Requires membership in the **public** role.

## Examples

The following example displays the members of the `Sales` role in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
EXEC sp_helprolemember 'Sales';
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [sp_addrolemember (Transact-SQL)](sp-addrolemember-transact-sql.md)
- [sp_droprolemember (Transact-SQL)](sp-droprolemember-transact-sql.md)
- [sp_helprole (Transact-SQL)](sp-helprole-transact-sql.md)
- [sp_helpsrvrolemember (Transact-SQL)](sp-helpsrvrolemember-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
