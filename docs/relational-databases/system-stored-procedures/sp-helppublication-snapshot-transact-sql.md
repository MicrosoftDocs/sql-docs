---
title: "sys.sp_helppublication_snapshot (Transact-SQL)"
description: sp_helppublication_snapshot returns information on the Snapshot agent for a given publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/19/2026
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helppublication_snapshot"
  - "sp_helppublication_snapshot_TSQL"
helpviewer_keywords:
  - "sp_helppublication_snapshot"
dev_langs:
  - "TSQL"
---
# sys.sp_helppublication_snapshot (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns information on the Snapshot agent for a given publication. Execute this stored procedure at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_helppublication_snapshot
    [ @publication = ] N'publication'
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *@publisher* is **sysname**, with a default of `NULL`.

Don't use *@publisher* when you add an article to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `id` | **int** | ID of the Snapshot Agent. |
| `name` | **nvarchar(100)** | Name of the Snapshot Agent. |
| `publisher_security_mode` | **smallint** | Security mode the agent uses when connecting to the Publisher. It can be one of the following values:<br /><br />`0` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br />`1` = Windows Authentication. |
| `publisher_login` | **sysname** | Login used when connecting to the Publisher. |
| `publisher_password` | **nvarchar(524)** | For security reasons, this column always returns a value of `**********`. |
| `job_id` | **uniqueidentifier** | Unique ID of the agent job. |
| `job_login` | **nvarchar(512)** | The Windows account under which the Snapshot agent runs. The column returns this value in the format `<domain>\<username>`. |
| `job_password` | **sysname** | For security reasons, this column always returns a value of `**********`. |
| `schedule_name` | **sysname** | The name of the schedule used for this agent job. |
| `frequency_type` | **int** | The frequency with which the agent is scheduled to run. It can be one of these values:<br /><br />`1` = One time<br />`2` = On demand<br />`4` = Daily<br />`8` = Weekly<br />`16` = Monthly<br />`32` = Monthly relative<br />`64` = Autostart<br />`128` = Recurring |
| `frequency_interval` | **int** | The days that the agent runs, which can be one of these values:<br /><br />`1` = Sunday<br />`2` = Monday<br />`3` = Tuesday<br />`4` = Wednesday<br />`5` = Thursday<br />`6` = Friday<br />`7` = Saturday<br />`8` = Day<br />`9` = Weekdays<br />`10` = Weekend days |
| `frequency_subday_type` | **int** | The type that defines how often the agent runs when `frequency_type` is `4` (daily), and can be one of these values:<br /><br />`1` = At the specified time<br />`2` = Seconds<br />`4` = Minutes<br />`8` = Hours |
| `frequency_subday_interval` | **int** | The number of intervals of `frequency_subday_type` that occur between scheduled execution of the agent. |
| `frequency_relative_interval` | **int** | The week that the agent runs in a given month when `frequency_type` is `32` (monthly relative), and can be one of these values:<br /><br />`1` = First<br />`2` = Second<br />`4` = Third<br />`8` = Fourth<br />`16` = Last |
| `frequency_recurrence_factor` | **int** | The number of weeks or months between the scheduled execution of the agent. |
| `active_start_date` | **int** | The date when the agent is first scheduled to run, formatted as `yyyyMMdd`. |
| `active_end_date` | **int** | The date when the agent is last scheduled to run, formatted as `yyyyMMdd`. |
| `active_start_time` | **int** | The time when the agent is first scheduled to run, formatted as `HHmmss`. |
| `active_end_time` | **int** | The time when the agent is last scheduled to run, formatted as `HHmmss`. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_help_publication_snapshot` is used in all types of replication.

## Permissions

Only members of the **sysadmin** fixed server role at the Publisher or members of the **db_owner** fixed database role on the publication database can run `sp_help_publication_snapshot`.

## Related content

- [View and Modify Publication Properties](../replication/publish/view-and-modify-publication-properties.md)
- [sys.sp_addpublication_snapshot (Transact-SQL)](sp-addpublication-snapshot-transact-sql.md)
- [sys.sp_changepublication_snapshot (Transact-SQL)](sp-changepublication-snapshot-transact-sql.md)
- [sys.sp_dropmergepublication (Transact-SQL)](sp-dropmergepublication-transact-sql.md)
- [sys.sp_droppublication (Transact-SQL)](sp-droppublication-transact-sql.md)
