---
title: "sp_add_targetservergroup (Transact-SQL)"
description: "Adds the specified server group."
author: dimitri-furman
ms.author: dfurman
ms.reviewer: vanto
ms.date: 06/19/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_targetservergroup"
  - "sp_add_targetservergroup_TSQL"
helpviewer_keywords:
  - "sp_add_targetservergroup"
dev_langs:
  - "TSQL"
---
# sp_add_targetservergroup (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds the specified server group.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
dbo.sp_add_targetservergroup [ @name = ] N'name'
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the server group to create. *@name* is **sysname**, with no default. *@name* can't contain commas.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

Target server groups provide an easy way to target a job at a collection of target servers. For more information, see [sp_apply_job_to_targets](sp-apply-job-to-targets-transact-sql.md).

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example creates the target server group named `Servers Processing Customer Orders`.

```sql
USE msdb;
GO

EXECUTE dbo.sp_add_targetservergroup 'Servers Processing Customer Orders';
GO
```

## Related content

- [sp_apply_job_to_targets (Transact-SQL)](sp-apply-job-to-targets-transact-sql.md)
- [sp_delete_targetservergroup (Transact-SQL)](sp-delete-targetservergroup-transact-sql.md)
- [sp_help_targetservergroup (Transact-SQL)](sp-help-targetservergroup-transact-sql.md)
- [sp_update_targetservergroup (Transact-SQL)](sp-update-targetservergroup-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
