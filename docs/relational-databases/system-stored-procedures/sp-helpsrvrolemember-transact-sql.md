---
title: "sp_helpsrvrolemember (Transact-SQL)"
description: sp_helpsrvrolemember returns information about the members of a SQL Server fixed server role.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helpsrvrolemember"
  - "sp_helpsrvrolemember_TSQL"
helpviewer_keywords:
  - "sp_helpsrvrolemember"
dev_langs:
  - "TSQL"
---
# sp_helpsrvrolemember (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information about the members of a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] fixed server role.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpsrvrolemember [ [ @srvrolename = ] N'srvrolename' ]
[ ; ]
```

## Arguments

#### [ @srvrolename = ] N'*srvrolename*'

The name of a fixed server role. *@srvrolename* is **sysname**, with a default of `NULL`, and can be any of the following values.

| Fixed server role | Description |
| --- | --- |
| `sysadmin` | System administrators |
| `securityadmin` | Security administrators |
| `serveradmin` | Server administrators |
| `setupadmin` | Setup administrators |
| `processadmin` | Process administrators |
| `diskadmin` | Disk administrators |
| `dbcreator` | Database creators |
| `bulkadmin` | Can execute `BULK INSERT` statements |

If *@srvrolename* isn't specified, the result set includes information about all fixed server roles.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `ServerRole` | **sysname** | Name of the server role |
| `MemberName` | **sysname** | Name of a member of `ServerRole` |
| `MemberSID` | **varbinary(85)** | Security identifier of `MemberName` |

## Remarks

Use `sp_helprolemember` to display the members of a database role.

All logins are a member of public. `sp_helpsrvrolemember` doesn't recognize the **public** role because, internally, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't implement **public** as a role.

To add or removed members from server roles, see [ALTER SERVER ROLE](../../t-sql/statements/alter-server-role-transact-sql.md).

`sp_helpsrvrolemember` doesn't take a user-defined server role as an argument. To determine the members of a user-defined server role, see the examples in [ALTER SERVER ROLE](../../t-sql/statements/alter-server-role-transact-sql.md).

## Permissions

Requires membership in the **public** role.

## Examples

The following example lists the members of the **sysadmin** fixed server role.

```sql
EXEC sp_helpsrvrolemember 'sysadmin';
```

## Related content

- [sp_helprole (Transact-SQL)](sp-helprole-transact-sql.md)
- [sp_helprolemember (Transact-SQL)](sp-helprolemember-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [Security Functions (Transact-SQL)](../../t-sql/functions/security-functions-transact-sql.md)
