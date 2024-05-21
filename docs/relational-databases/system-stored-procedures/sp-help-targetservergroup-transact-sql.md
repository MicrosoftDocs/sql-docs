---
title: "sp_help_targetservergroup (Transact-SQL)"
description: sp_help_targetservergroup lists all target servers in the specified group.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_targetservergroup_TSQL"
  - "sp_help_targetservergroup"
helpviewer_keywords:
  - "sp_help_targetservergroup"
dev_langs:
  - "TSQL"
---
# sp_help_targetservergroup (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Lists all target servers in the specified group. If no group is specified, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] returns information about all target server groups.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_targetservergroup [ [ @name = ] N'name' ]
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the target server group for which to return information. *@name* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `servergroup_id` | **int** | Identification number of the server group |
| `name` | **sysname** | Name of the server group |

## Permissions

Permissions to execute this procedure default to the **sysadmin** fixed server role.

## Examples

### A. List information for all target server groups

This example lists information for all target server groups.

```sql
USE msdb;
GO

EXEC dbo.sp_help_targetservergroup;
GO
```

### B. List information for a specific target server group

This example lists information for the `Servers Maintaining Customer Information` target server group.

```sql
USE msdb;
GO

EXEC dbo.sp_help_targetservergroup
    N'Servers Maintaining Customer Information';
GO
```

## Related content

- [sp_add_targetservergroup (Transact-SQL)](sp-add-targetservergroup-transact-sql.md)
- [sp_delete_targetservergroup (Transact-SQL)](sp-delete-targetservergroup-transact-sql.md)
- [sp_update_targetservergroup (Transact-SQL)](sp-update-targetservergroup-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
