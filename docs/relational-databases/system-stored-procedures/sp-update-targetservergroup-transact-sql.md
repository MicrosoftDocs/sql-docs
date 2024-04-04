---
title: "sp_update_targetservergroup (Transact-SQL)"
description: Changes the name of the specified target server group.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_update_targetservergroup_TSQL"
  - "sp_update_targetservergroup"
helpviewer_keywords:
  - "sp_update_targetservergroup"
dev_langs:
  - "TSQL"
---
# sp_update_targetservergroup (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the name of the specified target server group.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_update_targetservergroup
    [ @name = ] N'name'
    , [ @new_name = ] N'new_name'
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the target server group. *@name* is **sysname**, with no default.

#### [ @new_name = ] N'*new_name*'

The new name for the target server group. *@new_name* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Permissions

To run this stored procedure, users must be granted the **sysadmin** fixed server role.

## Remarks

`sp_update_targetservergroup` must be run from the `msdb` database.

## Examples

The following example changes the name of the target server group `Servers Processing Customer Orders` to `Local Servers Processing Customer Orders`.

```sql
USE msdb;
GO

EXEC dbo.sp_update_targetservergroup
    @name = N'Servers Processing Customer Orders',
    @new_name = N'Local Servers Processing Customer Orders';
GO
```

## Related content

- [sp_add_targetservergroup (Transact-SQL)](sp-add-targetservergroup-transact-sql.md)
- [sp_delete_targetservergroup (Transact-SQL)](sp-delete-targetservergroup-transact-sql.md)
- [sp_help_targetservergroup (Transact-SQL)](sp-help-targetservergroup-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
