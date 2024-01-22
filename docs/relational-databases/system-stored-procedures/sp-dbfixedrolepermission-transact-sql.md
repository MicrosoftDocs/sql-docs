---
title: "sp_dbfixedrolepermission (Transact-SQL)"
description: sp_dbfixedrolepermission displays the permissions of a fixed database role.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dbfixedrolepermission"
  - "sp_dbfixedrolepermission_TSQL"
helpviewer_keywords:
  - "sp_dbfixedrolepermission"
dev_langs:
  - "TSQL"
---
# sp_dbfixedrolepermission (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Displays the permissions of a fixed database role. `sp_dbfixedrolepermission` returns correct information in [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)]. The output doesn't reflect the changes to the permissions hierarchy that were implemented in [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]. For more information, see [Database-Level Roles](../security/authentication-access/database-level-roles.md#fixed-database-roles), which shows a list of fixed database roles and its corresponding permissions.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dbfixedrolepermission [ [ @rolename = ] N'rolename' ]
[ ; ]
```

## Arguments

#### [ @rolename = ] N'*rolename*'

The name of a valid [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] fixed database role. *@rolename* is **sysname**, with a default of `NULL`. If *@rolename* isn't specified, the permissions for all fixed database roles are displayed.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `DbFixedRole` | **sysname** | Name of the fixed database role |
| `Permission` | **nvarchar(70)** | Permissions associated with `DbFixedRole` |

## Remarks

To display a list of the fixed database roles, execute `sp_helpdbfixedrole`. The following table shows the fixed database roles.

| Fixed database role | Description |
| --- | --- |
| **db_owner** | Database owners |
| **db_accessadmin** | Database access administrators |
| **db_securityadmin** | Database security administrators |
| **db_ddladmin** | Database data definition language (DDL) administrators |
| **db_backupoperator** | Database backup operators |
| **db_datareader** | Database data readers |
| **db_datawriter** | Database data writers |
| **db_denydatareader** | Database deny data readers |
| **db_denydatawriter** | Database deny data writers |

Members of the **db_owner** fixed database role have the permissions of all the other fixed database roles. To display the permissions for fixed server roles, execute `sp_srvrolepermission`.

The result set includes the [!INCLUDE [tsql](../../includes/tsql-md.md)] statements that can be executed, and other special activities that can be performed, by members of the database role.

## Permissions

Requires membership in the **public** role.

## Examples

The following query returns the permissions for all fixed database roles because it doesn't specify a fixed database role.

```sql
EXEC sp_dbfixedrolepermission;
GO
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [sp_addrolemember (Transact-SQL)](sp-addrolemember-transact-sql.md)
- [sp_droprolemember (Transact-SQL)](sp-droprolemember-transact-sql.md)
- [sp_helpdbfixedrole (Transact-SQL)](sp-helpdbfixedrole-transact-sql.md)
- [sp_srvrolepermission (Transact-SQL)](sp-srvrolepermission-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
