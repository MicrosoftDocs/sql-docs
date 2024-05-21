---
title: "sp_helpsrvrole (Transact-SQL)"
description: sp_helpsrvrole Returns a list of the SQL Server fixed server roles.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helpsrvrole_TSQL"
  - "sp_helpsrvrole"
helpviewer_keywords:
  - "sp_helpsrvrole"
dev_langs:
  - "TSQL"
---
# sp_helpsrvrole (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a list of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] fixed server roles.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpsrvrole [ [ @srvrolename = ] N'srvrolename' ]
[ ; ]
```

## Arguments

#### [ @srvrolename = ] N'*srvrolename*'

The name of the fixed server role. *@srvrolename* is **sysname**, with a default of `NULL`, and can be one of the following values.

| Fixed server role | Description |
| --- | --- |
| **sysadmin** | System administrators |
| **securityadmin** | Security administrators |
| **serveradmin** | Server administrators |
| **setupadmin** | Setup administrators |
| **processadmin** | Process administrators |
| **diskadmin** | Disk administrators |
| **dbcreator** | Database creators |
| **bulkadmin** | Can execute `BULK INSERT` statements |

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `ServerRole` | **sysname** | Name of the server role |
| `Description` | **sysname** | Description of `ServerRole` |

## Remarks

Fixed server roles are defined at the server level and have permissions to perform specific server-level administrative activities. Fixed server roles can't be added, removed, or changed.

To add or removed members from server roles, see [ALTER SERVER ROLE](../../t-sql/statements/alter-server-role-transact-sql.md).

All logins are a member of **public**. `sp_helpsrvrole` doesn't recognize the **public** role because, internally, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't implement **public** as a role.

`sp_helpsrvrole` doesn't take a user-defined server role as an argument. To list the user-defined server roles, see the examples in [ALTER SERVER ROLE](../../t-sql/statements/alter-server-role-transact-sql.md).

## Permissions

Requires membership in the **public** role.

## Examples

### A. List the fixed server roles

The following query returns the list of fixed server roles.

```sql
EXEC sp_helpsrvrole;
```

### B. List fixed and user-defined server roles

The following query returns a list of both fixed and user-defined server roles.

```sql
SELECT * FROM sys.server_principals WHERE type = 'R';
```

### C. Return a description of a fixed server role

The following query returns the name and description of the **diskadmin** fixed server roles.

```sql
EXEC sp_helpsrvrole 'diskadmin';
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [Server-level roles](../security/authentication-access/server-level-roles.md)
- [sp_addsrvrolemember (Transact-SQL)](sp-addsrvrolemember-transact-sql.md)
- [sp_dropsrvrolemember (Transact-SQL)](sp-dropsrvrolemember-transact-sql.md)
- [sp_helpsrvrolemember (Transact-SQL)](sp-helpsrvrolemember-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
