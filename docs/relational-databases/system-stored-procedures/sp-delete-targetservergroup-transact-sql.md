---
title: "sp_delete_targetservergroup (Transact-SQL)"
description: Deletes the specified target server group.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_targetservergroup_TSQL"
  - "sp_delete_targetservergroup"
helpviewer_keywords:
  - "sp_delete_targetservergroup"
dev_langs:
  - "TSQL"
---
# sp_delete_targetservergroup (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Deletes the specified target server group.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_targetservergroup [ @name = ] N'name'
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the target server group to remove. *@name* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example removes the target server group `Servers Processing Customer Orders`.

```sql
USE msdb;
GO

EXEC sp_delete_targetservergroup
    @name = N'Servers Processing Customer Orders';
GO
```

## Related content

- [sp_add_targetservergroup (Transact-SQL)](sp-add-targetservergroup-transact-sql.md)
- [sp_help_targetservergroup (Transact-SQL)](sp-help-targetservergroup-transact-sql.md)
- [sp_update_targetservergroup (Transact-SQL)](sp-update-targetservergroup-transact-sql.md)
