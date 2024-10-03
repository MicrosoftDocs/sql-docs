---
title: "sp_adddynamicsnapshot_job (Transact-SQL)"
description: Creates an agent job that generates a filtered data snapshot for a publication with parameterized row filters.
author: mashamsft
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_adddynamicsnapshot_job"
  - "sp_adddynamicsnapshot_job_TSQL"
helpviewer_keywords:
  - "sp_adddynamicsnapshot_job"
dev_langs:
  - "TSQL"
---
# sp_adddynamicsnapshot_job (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Creates an agent job that generates a filtered data snapshot for a publication with parameterized row filters. This stored procedure is executed at the Publisher on the publication database. This stored procedure is used by an administrator to manually create filtered data snapshot jobs for Subscribers.

> [!NOTE]  
> In order for a filtered data snapshot job to be created, a standard snapshot job for the publication must already exist.

For more information, see [Create a Snapshot for a Merge Publication with Parameterized Filters](../replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_adddynamicsnapshot_job
    [ @publication = ] N'publication'
    [ , [ @suser_sname = ] N'suser_sname' ]
    [ , [ @host_name = ] N'host_name' ]
    [ , [ @dynamic_snapshot_jobname = ] N'dynamic_snapshot_jobname' OUTPUT ]
    [ , [ @dynamic_snapshot_jobid = ] 'dynamic_snapshot_jobid' OUTPUT ]
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
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication to which the filtered data snapshot job is being added. *@publication* is **sysname**, with no default.

#### [ @suser_sname = ] N'*suser_sname*'

The value used when creating a filtered data snapshot for a subscription that is filtered by the value of the [SUSER_SNAME](../../t-sql/functions/suser-sname-transact-sql.md) function at the Subscriber. *@suser_sname* is **sysname**, with a default of `NULL`. *@suser_sname* should be `NULL` if this function isn't used to dynamically filter the publication.

#### [ @host_name = ] N'*host_name*'

The value used when creating a filtered data snapshot for a subscription that is filtered by the value of the [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) function at the Subscriber. *@host_name* is **sysname**, with a default of `NULL`. *host_name* should be `NULL` if this function isn't used to dynamically filter the publication.

#### [ @dynamic_snapshot_jobname = ] N'*dynamic_snapshot_jobname*' OUTPUT

The name of the filtered data snapshot job created. *@dynamic_snapshot_jobname* is an OUTPUT parameter of type **sysname**. If specified, *@dynamic_snapshot_jobname* must resolve to a unique job at the Distributor. If unspecified, a job name is automatically generated in the result set, where the name is created as follows:

```text
'dyn_' + <name of the standard snapshot job> + <GUID>
```

> [!NOTE]  
> When generating the name of the dynamic snapshot job, you might truncate the name of the standard snapshot job.

#### [ @dynamic_snapshot_jobid = ] '*dynamic_snapshot_jobid*' OUTPUT

An identifier for the filtered data snapshot job created. *@dynamic_snapshot_jobid* is an OUTPUT parameter of type **uniqueidentifier**, with a default of `NULL`.

#### [ @frequency_type = ] *frequency_type*

Specifies the frequency by which the filtered data snapshot job is scheduled. *@frequency_type* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` | One time |
| `2` (default) | On demand |
| `4` | Daily |
| `8` | Weekly |
| `16` | Monthly |
| `32` | Monthly relative |
| `64` | Autostart |
| `128` | Recurring |

#### [ @frequency_interval = ] *frequency_interval*

The period, measured in days, when the filtered data snapshot job is executed. *@frequency_interval* is **int**, and depends on the value of *@frequency_type*.

| Value of *@frequency_type* | Effect on *@frequency_interval* |
| --- | --- |
| `1` (default) | *@frequency_interval* is unused. |
| `4` | Every *@frequency_interval* days. |
| `8` | *@frequency_interval* is one or more of the following (combined with a [&#124; (Bitwise OR)](../../t-sql/language-elements/bitwise-or-transact-sql.md) logical operator):<br /><br />`1` = Sunday<br />`2` = Monday<br />`4` = Tuesday<br />`8` = Wednesday<br />`16` = Thursday<br />`32` = Friday<br />`64` = Saturday |
| `16` | On the *@frequency_interval* day of the month. |
| `32` | *@frequency_interval* is one of the following options:<br /><br />`1` = Sunday<br />`2` = Monday<br />`3` = Tuesday<br />`4` = Wednesday<br />`5` = Thursday<br />`6` = Friday<br />`7` = Saturday<br />`8` = Day<br />`9` = Weekday<br />`10` = Weekend day |
| `64` | *@frequency_interval* is unused. |
| `128` | *@frequency_interval* is unused. |

#### [ @frequency_subday = ] *frequency_subday*

Specifies the units for *@frequency_subday_interval*. *@frequency_subday* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` (default) | Once |
| `2` | Second |
| `4` | Minute |
| `8` | Hour |

#### [ @frequency_subday_interval = ] *frequency_subday_interval*

The number of *frequency_subday* periods that occur between each execution of the job. *@frequency_subday_interval* is **int**, with a default of `1`.

#### [ @frequency_relative_interval = ] *frequency_relative_interval*

The occurrence of the filtered data snapshot job in each month. This parameter is used when *@frequency_type* is set to `32` (monthly relative). *@frequency_relative_interval* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` (default) | First |
| `2` | Second |
| `4` | Third |
| `8` | Fourth |
| `16` | Last |

#### [ @frequency_recurrence_factor = ] *frequency_recurrence_factor*

The recurrence factor used by *frequency_type*. *@frequency_recurrence_factor* is **int**, with a default of `1`.

#### [ @active_start_date = ] *active_start_date*

The date when the filtered data snapshot job is first scheduled, formatted as `yyyyMMdd`. *@active_start_date* is **int**, with a default of `0`.

#### [ @active_end_date = ] *active_end_date*

The date when the filtered data snapshot job stops being scheduled, formatted as `yyyyMMdd`. *@active_end_date* is **int**, with a default of `0`.

#### [ @active_start_time_of_day = ] *active_start_time_of_day*

The time of day when the filtered data snapshot job is first scheduled, formatted as `HHmmss`. *@active_start_time_of_day* is **int**, with a default of `0`.

#### [ @active_end_time_of_day = ] *active_end_time_of_day*

The time of day when the filtered data snapshot job stops being scheduled, formatted as `HHmmss`. *@active_end_time_of_day* is **int**, with a default of `0`.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `id` | **int** | Identifies the filtered data snapshot job in the [MSdynamicsnapshotjobs](../system-tables/msdynamicsnapshotjobs-transact-sql.md) system table. |
| `dynamic_snapshot_jobname` | **sysname** | Name of the filtered data snapshot job. |
| `dynamic_snapshot_jobid` | **uniqueidentifier** | Uniquely identifies the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent job at the Distributor. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_adddynamicsnapshot_job` is used in merge replication for publications that use a parameterized filter.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-adddynamicsnapshot-jo_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_adddynamicsnapshot_job`.

## Related content

- [Create a Snapshot for a Merge Publication with Parameterized Filters](../replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md)
- [Parameterized Filters - Parameterized Row Filters](../replication/merge/parameterized-filters-parameterized-row-filters.md)
- [sp_dropdynamicsnapshot_job (Transact-SQL)](sp-dropdynamicsnapshot-job-transact-sql.md)
- [sp_helpdynamicsnapshot_job (Transact-SQL)](sp-helpdynamicsnapshot-job-transact-sql.md)
