---
title: "sp_apply_job_to_targets (Transact-SQL)"
description: Applies a job to one or more target servers or to the target servers belonging to one or more target server groups.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_apply_job_to_targets"
  - "sp_apply_job_to_targets_TSQL"
helpviewer_keywords:
  - "sp_apply_job_to_targets"
dev_langs:
  - "TSQL"
---
# sp_apply_job_to_targets (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Applies a job to one or more target servers or to the target servers belonging to one or more target server groups.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_apply_job_to_targets
    [ [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    [ , [ @target_server_groups = ] N'target_server_groups' ]
    [ , [ @target_servers = ] N'target_servers' ]
    [ , [ @operation = ] 'operation' ]
[ ; ]
```

## Arguments

#### [ @job_id = ] '*job_id*'

The job identification number of the job to apply to the specified target servers or target server groups. *@job_id* is **uniqueidentifier**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @job_name = ] N'*job_name*'

The name of the job to apply to the specified the associated target servers or target server groups. *@job_name* is **sysname**, with a default of `NULL`.

Either *@job_id* or *@job_name* must be specified, but both can't be specified.

#### [ @target_server_groups = ] N'*target_server_groups*'

A comma-separated list of target server groups to which the specified job is to be applied. *@target_server_groups* is **nvarchar(2048)**, with a default of `NULL`.

#### [ @target_servers = ] N'*target_servers*'

A comma-separated list of target servers to which the specified job is to be applied. *@target_servers* is **nvarchar(2048)**, with a default of `NULL`.

#### [ @operation = ] '*operation*'

Specifies whether the specified job should be applied to or removed from the specified target servers or target server groups. *@operation* is **varchar(7)**, with a default of `APPLY`. Valid operations are `APPLY` and `REMOVE`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_apply_job_to_targets` provides an easy way to apply (or remove) a job from multiple target servers, and is an alternative to calling `sp_add_jobserver` (or `sp_delete_jobserver`) once for each target server required.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example applies the previously created `Backup Customer Information` job to all the target servers in the `Servers Maintaining Customer Information` group.

```sql
USE msdb;
GO

EXEC dbo.sp_apply_job_to_targets
    @job_name = N'Backup Customer Information',
    @target_server_groups = N'Servers Maintaining Customer Information',
    @operation = N'APPLY' ;
GO
```

## Related content

- [sp_add_jobserver (Transact-SQL)](sp-add-jobserver-transact-sql.md)
- [sp_delete_jobserver (Transact-SQL)](sp-delete-jobserver-transact-sql.md)
- [sp_remove_job_from_targets (Transact-SQL)](sp-remove-job-from-targets-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
