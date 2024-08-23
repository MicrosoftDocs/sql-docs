---
title: "sp_remove_job_from_targets (Transact-SQL)"
description: sp_remove_job_from_targets removes the specified job from the given target servers or target server groups.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_remove_job_from_targets_TSQL"
  - "sp_remove_job_from_targets"
helpviewer_keywords:
  - "sp_remove_job_from_targets"
dev_langs:
  - "TSQL"
---
# sp_remove_job_from_targets (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes the specified job from the given target servers or target server groups.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_remove_job_from_targets
    [ [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    [ , [ @target_server_groups = ] N'target_server_groups' ]
    [ , [ @target_servers = ] N'target_servers' ]
[ ; ]
```

## Arguments

#### [ @job_id = ] '*job_id*'

The job identification number of the job from which to remove the specified target servers or target server groups. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @job_name = ] N'*job_name*'

The name of the job from which to remove the specified target servers or target server groups. *@job_name* is **sysname**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @target_server_groups = ] N'*target_server_groups*'

A comma-separated list of target server groups to be removed from the specified job. *@target_server_groups* is **nvarchar(1024)**, with a default of `NULL`.

#### [ @target_servers = ] N'*target_servers*'

A comma-separated list of target servers to be removed from the specified job. *@target_servers* is **nvarchar(1024)**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Permissions

Permissions to execute this procedure default to members of the **sysadmin** fixed server role.

## Examples

The following example removes the previously created `Weekly Sales Backups` job from the `Servers Processing Customer Orders` target server group, and from the `SEATTLE1` and `SEATTLE2` servers.

```sql
USE msdb;
GO

EXEC dbo.sp_remove_job_from_targets
    @job_name = N'Weekly Sales Backups',
    @target_server_groups = N'Servers Processing Customer Orders',
    @target_servers = N'SEATTLE2,SEATTLE1';
GO
```

## Related content

- [sp_apply_job_to_targets (Transact-SQL)](sp-apply-job-to-targets-transact-sql.md)
- [sp_delete_jobserver (Transact-SQL)](sp-delete-jobserver-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
