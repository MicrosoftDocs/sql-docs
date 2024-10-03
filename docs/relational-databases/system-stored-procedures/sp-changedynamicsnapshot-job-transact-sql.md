---
title: "sp_changedynamicsnapshot_job (Transact-SQL)"
description: Modifies the agent job that generates the snapshot for a subscription to a publication with a parameterized row filter.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_changedynamicsnapshot_job"
  - "sp_changedynamicsnapshot_job_TSQL"
helpviewer_keywords:
  - "sp_changedynamicsnapshot_job"
dev_langs:
  - "TSQL"
---
# sp_changedynamicsnapshot_job (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Modifies the agent job that generates the snapshot for a subscription to a publication with a parameterized row filter. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_changedynamicsnapshot_job
    [ @publication = ] N'publication'
    [ , [ @dynamic_snapshot_jobname = ] N'dynamic_snapshot_jobname' ]
    [ , [ @dynamic_snapshot_jobid = ] 'dynamic_snapshot_jobid' ]
    [ , [ @frequency_type = ] frequency_type ]
    [ , [ @frequency_interval = ] frequency_interval ]
    [ , [ @frequency_subday = ] frequency_subday ]
    [ , [ @frequency_subday_interval = ] frequency_subday_interval ]
    [ , [ @frequency_relative_interval = ] frequency_relative_interval ]
    [ , [ @frequency_recurrence_factor = ] frequency_recurrence_factor ]
    [ , [ @active_start_date = ] active_start_date ]
    [ , [ @active_end_date = ] active_end_date ]
    [ , [ @active_start_time_of_day = ] active_start_time_of_day ]
    [ , [ @active_end_time_of_day = ] active_end_time_of_day ]
    [ , [ @job_login = ] N'job_login' ]
    [ , [ @job_password = ] N'job_password' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @dynamic_snapshot_jobname = ] N'*dynamic_snapshot_jobname*'

The name of the snapshot job being changed. *@dynamic_snapshot_jobname* is **sysname**, with a default of `%`.

If *@dynamic_snapshot_jobid* is specified, you must use the default value for *@dynamic_snapshot_jobname*.

#### [ @dynamic_snapshot_jobid = ] '*dynamic_snapshot_jobid*'

The ID of the snapshot job being changed. *@dynamic_snapshot_jobid* is **uniqueidentifier**, with a default of `NULL`.

If *@dynamic_snapshot_jobname* is specified, you must use the default value for *@dynamic_snapshot_jobid*.

#### [ @frequency_type = ] *frequency_type*

Specifies the frequency with which to schedule the agent. *@frequency_type* is **int**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `1` | One time |
| `2` | On demand |
| `4` | Daily |
| `8` | Weekly |
| `16` | Monthly |
| `32` | Monthly relative |
| `64` | Autostart |
| `128` | Recurring |
| `NULL` (default) | |

#### [ @frequency_interval = ] *frequency_interval*

The days that the agent runs. *@frequency_interval* is **int**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `1` | Sunday |
| `2` | Monday |
| `3` | Tuesday |
| `4` | Wednesday |
| `5` | Thursday |
| `6` | Friday |
| `7` | Saturday |
| `8` | Day |
| `9` | Weekdays |
| `10` | Weekend days |
| `NULL` (default) | |

#### [ @frequency_subday = ] *frequency_subday*

Specifies how often to reschedule during the defined period. *@frequency_subday* is **int**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `1` | Once |
| `2` | Second |
| `4` | Minute |
| `8` | Hour |
| `NULL` (default) | |

#### [ @frequency_subday_interval = ] *frequency_subday_interval*

The interval for *@frequency_subday*. *@frequency_subday_interval* is **int**, with a default of `NULL`.

#### [ @frequency_relative_interval = ] *frequency_relative_interval*

The date that the Merge Agent runs. This parameter is used when *@frequency_type* is set to `32` (monthly relative). *@frequency_relative_interval* is **int**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `1` | First |
| `2` | Second |
| `4` | Third |
| `8` | Fourth |
| `16` | Last |
| `NULL` (default) | |

#### [ @frequency_recurrence_factor = ] *frequency_recurrence_factor*

The recurrence factor used by *@frequency_type*. *@frequency_recurrence_factor* is **int**, with a default of `NULL`.

#### [ @active_start_date = ] *active_start_date*

The date when the Merge Agent is first scheduled, formatted as `yyyyMMdd`. *@active_start_date* is **int**, with a default of `NULL`.

#### [ @active_end_date = ] *active_end_date*

The date when the Merge Agent stops being scheduled, formatted as `yyyyMMdd`. *@active_end_date* is **int**, with a default of `NULL`.

#### [ @active_start_time_of_day = ] *active_start_time_of_day*

The time of day when the Merge Agent is first scheduled, formatted as `HHmmss`. *@active_start_time_of_day* is **int**, with a default of `NULL`.

#### [ @active_end_time_of_day = ] *active_end_time_of_day*

The time of day when the Merge Agent stops being scheduled, formatted as `HHmmss`. *@active_end_time_of_day* is **int**, with a default of `NULL`.

#### [ @job_login = ] N'*job_login*'

The Windows account under which the Snapshot Agent runs when generating the snapshot for a subscription using a parameterized row filter. *@job_login* is **nvarchar(257)**, with a default of `NULL`.

#### [ @job_password = ] N'*job_password*'

The password for the Windows Account under which the Snapshot Agent runs when generating the snapshot for a subscription using a parameterized row filter. *@job_password* is **sysname**, with a default of `NULL`.

> [!IMPORTANT]  
> When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_changedynamicsnapshot_job` is used in merge replication for publications with parameterized row filters.

After changing an agent login or password, you must stop and restart the agent before the change takes effect.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_changedynamicsnapshot_job`.

## Related content

- [View and modify replication security settings](../replication/security/view-and-modify-replication-security-settings.md)
- [Create a Snapshot for a Merge Publication with Parameterized Filters](../replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md)
