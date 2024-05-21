---
title: "sp_helpuser (Transact-SQL)"
description: sp_helpuser reports information about database-level principals in the current database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helpuser"
  - "sp_helpuser_TSQL"
helpviewer_keywords:
  - "sp_helpuser"
dev_langs:
  - "TSQL"
---
# sp_helpuser (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Reports information about database-level principals in the current database.

> [!IMPORTANT]  
> `sp_helpuser` doesn't return information about securables that were introduced in [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] and later versions. Use [sys.database_principals](../system-catalog-views/sys-database-principals-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpuser [ [ @name_in_db = ] N'name_in_db' ]
[ ; ]
```

## Arguments

#### [ @name_in_db = ] N'*name_in_db*'

The name of database user or database role in the current database. *@name_in_db* is **sysname**, with a default of `NULL`. *@name_in_db* must exist in the current database. If *@name_in_db* isn't specified, `sp_helpuser` returns information about all database principals.

## Return code values

`0` (success) or `1` (failure).

## Result set

The following table shows the result set when no user account, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], or Windows user is specified for *@name_in_db*.

| Column name | Data type | Description |
| --- | --- | --- |
| `UserName` | **sysname** | Users in the current database. |
| `RoleName` | **sysname** | Roles to which `UserName` belongs. |
| `LoginName` | **sysname** | Login of `UserName`. |
| `DefDBName` | **sysname** | Default database of `UserName`. |
| `DefSchemaName` | **sysname** | Default schema of the database user. |
| `UserID` | **smallint** | ID of `UserName` in the current database. |
| `SID` | **smallint** | User security identification number (SID). |

The following table shows the result set when no user account is specified and aliases exist in the current database.

| Column name | Data type | Description |
| --- | --- | --- |
| `LoginName` | **sysname** | Logins aliased to users in the current database. |
| `UserNameAliasedTo` | **sysname** | User name in the current database to which the login is aliased. |

The following table shows the result set when a role is specified for *@name_in_db*.

| Column name | Data type | Description |
| --- | --- | --- |
| `Role_name` | **sysname** | Name of the role in the current database. |
| `Role_id` | **smallint** | Role ID for the role in the current database. |
| `Users_in_role` | **sysname** | Member of the role in the current database. |
| `Userid` | **smallint** | User ID for the member of the role. |

## Remarks

To see information about membership of database roles, use [sys.database_role_members](../system-catalog-views/sys-database-role-members-transact-sql.md). To see information about server role members, use [sys.server_role_members](../system-catalog-views/sys-server-role-members-transact-sql.md), and to see information about server-level principals, use [sys.server_principals](../system-catalog-views/sys-server-principals-transact-sql.md).

## Permissions

Requires membership in the **public** role.

Information returned is subject to restrictions on access to metadata. Entities on which the principal has no permission don't appear. For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

## Examples

### A. List all users

The following example lists all users in the current database.

```sql
EXEC sp_helpuser;
```

### B. List information for a single user

The following example lists information about the user database owner (`dbo`).

```sql
EXEC sp_helpuser 'dbo';
```

### C. List information for a database role

The following example lists information about the **db_securityadmin** fixed database role.

```sql
EXEC sp_helpuser 'db_securityadmin';
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Principals (Database Engine)](../security/authentication-access/principals-database-engine.md)
- [sys.database_principals (Transact-SQL)](../system-catalog-views/sys-database-principals-transact-sql.md)
- [sys.database_role_members (Transact-SQL)](../system-catalog-views/sys-database-role-members-transact-sql.md)
- [sys.server_principals (Transact-SQL)](../system-catalog-views/sys-server-principals-transact-sql.md)
- [sys.server_role_members (Transact-SQL)](../system-catalog-views/sys-server-role-members-transact-sql.md)
