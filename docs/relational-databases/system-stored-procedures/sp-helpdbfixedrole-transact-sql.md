---
title: "sp_helpdbfixedrole (Transact-SQL)"
description: sp_helpdbfixedrole returns a list of the fixed database roles.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helpdbfixedrole"
  - "sp_helpdbfixedrole_TSQL"
helpviewer_keywords:
  - "sp_helpdbfixedrole"
dev_langs:
  - "TSQL"
---
# sp_helpdbfixedrole (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a list of the fixed database roles.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpdbfixedrole [ [ @rolename = ] N'rolename' ]
[ ; ]
```

## Arguments

#### [ @rolename = ] N'*rolename*'

The name of a fixed database role. *@rolename* is **sysname**, with a default of `NULL`. If *@rolename* is specified, only information about that role is returned; otherwise, a list and description of all fixed database roles is returned.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `DbFixedRole` | **sysname** | Name of the fixed database role. |
| `Description` | **nvarchar(70)** | Description of `DbFixedRole`. |

## Remarks

Fixed database roles, as shown in the following table, are defined at the database level and have permissions to perform specific database-level administrative activities. Fixed database roles can't be added or removed. The permissions granted to a fixed database role can't be changed.

| Fixed database role | Description |
| --- | --- |
| `db_owner` | Database owners |
| `db_accessadmin` | Database access administrators |
| `db_securityadmin` | Database security administrators |
| `db_ddladmin` | Database DDL administrators |
| `db_backupoperator` | Database backup operators |
| `db_datareader` | Database data readers |
| `db_datawriter` | Database data writers |
| `db_denydatareader` | Database deny data readers |
| `db_denydatawriter` | Database deny data writers |

The following table shows stored procedures that are used for modifying database roles.

| Stored procedure | Action |
| --- | --- |
| `sp_addrolemember` | Adds a database user to a fixed database role. |
| `sp_helprole` | Displays a list of the members of a fixed database role. |
| `sp_droprolemember` | Removes a member from a fixed database role. |

## Permissions

Requires membership in the **public** role.

Information returned is subject to restrictions on access to metadata. Entities on which the principal has no permission don't appear. For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

## Examples

The following example shows a list of all fixed database roles.

```sql
EXEC sp_helpdbfixedrole;
GO
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [sp_addrolemember (Transact-SQL)](sp-addrolemember-transact-sql.md)
- [sp_dbfixedrolepermission (Transact-SQL)](sp-dbfixedrolepermission-transact-sql.md)
- [sp_droprolemember (Transact-SQL)](sp-droprolemember-transact-sql.md)
- [sp_helprole (Transact-SQL)](sp-helprole-transact-sql.md)
- [sp_helprolemember (Transact-SQL)](sp-helprolemember-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
