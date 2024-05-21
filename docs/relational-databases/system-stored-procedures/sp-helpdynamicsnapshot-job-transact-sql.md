---
title: "sp_helpdynamicsnapshot_job (Transact-SQL)"
description: sp_helpdynamicsnapshot_job returns information on agent jobs that generate filtered data snapshots.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpdynamicsnapshot_TSQL"
  - "sp_helpdynamicsnapshot_job_TSQL"
  - "job_TSQL"
  - "helpdynamicsnapshot"
  - "job"
  - "sp_helpdynamicsnapshot"
  - "sp_helpdynamicsnapshot_job"
  - "helpdynamicsnapshot_TSQL"
helpviewer_keywords:
  - "sp_helpdynamicsnapshot_job"
dev_langs:
  - "TSQL"
---
# sp_helpdynamicsnapshot_job (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns information on agent jobs that generate filtered data snapshots. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpdynamicsnapshot_job
    [ [ @publication = ] N'publication' ]
    [ , [ @dynamic_snapshot_jobname = ] N'dynamic_snapshot_jobname' ]
    [ , [ @dynamic_snapshot_jobid = ] 'dynamic_snapshot_jobid' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `%`, which returns information on all filtered data snapshot jobs that match the specified *@dynamic_snapshot_jobid* and *@dynamic_snapshot_jobname* for all publications.

#### [ @dynamic_snapshot_jobname = ] N'*dynamic_snapshot_jobname*'

The name of a filtered data snapshot job. *@dynamic_snapshot_jobname* is **sysname**, with a default of `%`, which returns all dynamic jobs for a publication with the specified *@dynamic_snapshot_jobname*. If a job name wasn't explicitly specified when the job was created, the job name is in the format `'dyn_' + <name of the standard snapshot job> + <GUID>`.

#### [ @dynamic_snapshot_jobid = ] '*dynamic_snapshot_jobid*'

An identifier for a filtered data snapshot job. *@dynamic_snapshot_jobid* is **uniqueidentifier**, with a default of `NULL`, which returns all snapshot jobs that match the specified *@dynamic_snapshot_jobname*.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `id` | **int** | Identifies the filtered data snapshot job. |
| `job_name` | **sysname** | Name of the filtered data snapshot job. |
| `job_id` | **uniqueidentifier** | Identifies the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent job at the Distributor. |
| `dynamic_filter_login` | **sysname** | Value used for evaluating the [SUSER_SNAME](../../t-sql/functions/suser-sname-transact-sql.md) function in a parameterized row filter defined for the publication. |
| `dynamic_filter_hostname` | **sysname** | Value used for evaluating the [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) function in a parameterized row filter defined for the publication. |
| `dynamic_snapshot_location` | **nvarchar(255)** | Path to the folder where the snapshot files are read from if a parameterized row filter is used. |
| `frequency_type` | **int** | Is the frequency with which the agent is scheduled to run, which can be one of these values.<br /><br />`1` = One time<br />`2` = On demand<br />`4` = Daily<br />`8` = Weekly<br />`16` = Monthly<br />`32` = Monthly relative<br />`64` = Autostart<br />`128` = Recurring |
| `frequency_interval` | **int** | The days that the agent runs, which can be one of these values.<br /><br />`1` = Sunday<br />`2` = Monday<br />`3` = Tuesday<br />`4` = Wednesday<br />`5` = Thursday<br />`6` = Friday<br />`7` = Saturday<br />`8` = Day<br />`9` = Weekdays<br />`10` = Weekend days |
| `frequency_subday_type` | **int** | Is the type that defines how often the agent runs when *frequency_type* is `4` (daily), and can be one of these values.<br /><br />`1` = At the specified time<br />`2` = Seconds<br />`4` = Minutes<br />`8` = Hours |
| `frequency_subday_interval` | **int** | Number of intervals of *frequency_subday_type* that occur between scheduled execution of the agent. |
| `frequency_relative_interval` | **int** | Is the week that the agent runs in a given month when *frequency_type* is `32` (monthly relative), and can be one of these values.<br /><br />`1` = First<br />`2` = Second<br />`4` = Third<br />`8` = Fourth<br />`16` = Last |
| `frequency_recurrence_factor` | **int** | Number of weeks or months between the scheduled execution of the agent. |
| `active_start_date` | **int** | Date when the agent is first scheduled to run, formatted as `yyyyMMdd`. |
| `active_end_date` | **int** | Date when the agent is last scheduled to run, formatted as `yyyyMMdd`. |
| `active_start_time` | **int** | Time when the agent is first scheduled to run, formatted as `HHmmss`. |
| `active_end_time` | **int** | Time when the agent is last scheduled to run, formatted as `HHmmss`. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpdynamicsnapshot_job` is used in merge replication.

If all of the default parameter values are used, information on all partitioned data snapshot jobs for the entire publication database is returned.

## Permissions

Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role, and the publication access list for the publication can execute `sp_helpdynamicsnapshot_job`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
