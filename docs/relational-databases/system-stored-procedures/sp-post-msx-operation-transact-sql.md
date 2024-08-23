---
title: "sp_post_msx_operation (Transact-SQL)"
description: Inserts rows into the sysdownloadlist system table for target servers to download and execute.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_post_msx_operation"
  - "sp_post_msx_operation_TSQL"
helpviewer_keywords:
  - "sp_post_msx_operation"
dev_langs:
  - "TSQL"
---
# sp_post_msx_operation (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Inserts operations (rows) into the `sysdownloadlist` system table for target servers to download and execute.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_post_msx_operation
    [ @operation = ] 'operation'
    [ , [ @object_type = ] 'object_type' ]
    [ , [ @job_id = ] 'job_id' ]
    [ , [ @specific_target_server = ] N'specific_target_server' ]
    [ , [ @value = ] value ]
    [ , [ @schedule_uid = ] 'schedule_uid' ]
[ ; ]
```

## Arguments

#### [ @operation = ] '*operation*'

The type of operation for the posted operation. *@operation* is **varchar(64)**, with no default. Valid operations depend upon *@object_type*.

| Object type | Operation |
| --- | --- |
| `JOB` | `INSERT`<br />`UPDATE`<br />`DELETE`<br />`START`<br />`STOP` |
| `SERVER` | `RE-ENLIST`<br />`DEFECT`<br />`SYNC-TIME`<br />`SET-POLL` |
| `SCHEDULE` | `INSERT`<br />`UPDATE`<br />`DELETE` |

#### [ @object_type = ] '*object_type*'

The type of object for which to post an operation. *@object_type* is **varchar(64)**, with a default of `JOB`. Valid types are `JOB`, `SERVER`, and `SCHEDULE`.

#### [ @job_id = ] '*job_id*'

The job identification number of the job to which the operation applies. *@job_id* is **uniqueidentifier**, with a default of `NULL`. `0x00` indicates all jobs. If *@object_type* is `SERVER`, then *@job_id* isn't required.

#### [ @specific_target_server = ] N'*specific_target_server*'

The name of the target server for which the specified operation applies. *@specific_target_server* is **sysname**, with a default of `NULL`. If *@job_id* is specified, but *@specific_target_server* isn't specified, the operations are posted for all job servers of the job.

#### [ @value = ] *value*

The polling interval, in seconds. *@value* is **int**, with a default of `NULL`. Specify this parameter only if *@operation* is `SET-POLL`.

#### [ @schedule_uid = ] '*schedule_uid*'

The unique identifier for the schedule to which the operation applies. *@schedule_uid* is **uniqueidentifier**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_post_msx_operation` must be run from the `msdb` database.

`sp_post_msx_operation` can always be called safely because it first determines if the current server is a multiserver Microsoft SQL Server Agent and, if so, whether *@object_type* is a multiserver job.

After an operation is posted, it appears in the `sysdownloadlist` table. After a job is created and posted, subsequent changes to that job must also be communicated to the target servers (TSX). This step is also accomplished using the download list.

We highly recommend that you manage the download list in SQL Server Management Studio. For more information, see [View or Modify Jobs](../../ssms/agent/view-or-modify-jobs.md).

## Permissions

To run this stored procedure, users must be granted the **sysadmin** fixed server role.

## Related content

- [sp_add_jobserver (Transact-SQL)](sp-add-jobserver-transact-sql.md)
- [sp_delete_job (Transact-SQL)](sp-delete-job-transact-sql.md)
- [sp_delete_jobserver (Transact-SQL)](sp-delete-jobserver-transact-sql.md)
- [sp_delete_targetserver (Transact-SQL)](sp-delete-targetserver-transact-sql.md)
- [sp_resync_targetserver (Transact-SQL)](sp-resync-targetserver-transact-sql.md)
- [sp_start_job (Transact-SQL)](sp-start-job-transact-sql.md)
- [sp_stop_job (Transact-SQL)](sp-stop-job-transact-sql.md)
- [sp_update_job (Transact-SQL)](sp-update-job-transact-sql.md)
- [sp_update_operator (Transact-SQL)](sp-update-operator-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
