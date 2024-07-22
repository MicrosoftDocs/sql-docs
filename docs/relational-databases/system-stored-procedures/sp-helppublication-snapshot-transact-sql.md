---
title: "sp_helppublication_snapshot (Transact-SQL)"
description: sp_helppublication_snapshot returns information on the Snapshot agent for a given publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
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
# sp_helppublication_snapshot (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns information on the Snapshot agent for a given publication. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helppublication_snapshot
    [ @publication = ] N'publication'
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *@publisher* is **sysname**, with a default of `NULL`.

*publisher* shouldn't be used when adding an article to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `id` | **int** | ID of the Snapshot Agent. |
| `name` | **nvarchar(100)** | Name of the Snapshot Agent. |
| `publisher_security_mode` | **smallint** | Security mode used by the agent when connecting to the Publisher, which can be one of the following:<br /><br />`0` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br />`1` = Windows Authentication. |
| `publisher_login` | **sysname** | Login used when connecting to the Publisher. |
| `publisher_password` | **nvarchar(524)** | For security reasons, a value of `**********` is always returned. |
| `job_id` | **uniqueidentifier** | Unique ID of the agent job. |
| `job_login` | **nvarchar(512)** | Is the Windows account under which the Snapshot agent runs, which is returned in the format `<domain>\<username>`. |
| `job_password` | **sysname** | For security reasons, a value of `**********` is always returned. |
| `schedule_name` | **sysname** | Name of the schedule used for this agent job. |
| `frequency_type` | **int** | Is the frequency with which the agent is scheduled to run, which can be one of these values.<br /><br />`1` = One time<br />`2` = On demand<br />`4` = Daily<br />`8` = Weekly<br />`16` = Monthly<br />`32` = Monthly relative<br />`64` = Autostart<br />`128` = Recurring |
| `frequency_interval` | **int** | The days that the agent runs, which can be one of these values.<br /><br />`1` = Sunday<br />`2` = Monday<br />`3` = Tuesday<br />`4` = Wednesday<br />`5` = Thursday<br />`6` = Friday<br />`7` = Saturday<br />`8` = Day<br />`9` = Weekdays<br />`10` = Weekend days |
| `frequency_subday_type` | **int** | Is the type that defines how often the agent runs when `frequency_type` is `4` (daily), and can be one of these values.<br /><br />`1` = At the specified time<br />`2` = Seconds<br />`4` = Minutes<br />`8` = Hours |
| `frequency_subday_interval` | **int** | Number of intervals of *frequency_subday_type* that occur between scheduled execution of the agent. |
| `frequency_relative_interval` | **int** | Is the week that the agent runs in a given month when `frequency_type` is `32` (monthly relative), and can be one of these values.<br /><br />`1` = First<br />`2` = Second<br />`4` = Third<br />`8` = Fourth<br />`16` = Last |
| `frequency_recurrence_factor` | **int** | Number of weeks or months between the scheduled execution of the agent. |
| `active_start_date` | **int** | Date when the agent is first scheduled to run, formatted as `yyyyMMdd`. |
| `active_end_date` | **int** | Date when the agent is last scheduled to run, formatted as `yyyyMMdd`. |
| `active_start_time` | **int** | Time when the agent is first scheduled to run, formatted as `HHmmss`. |
| `active_end_time` | **int** | Time when the agent is last scheduled to run, formatted as `HHmmss`. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_help_publication_snapshot` is used in all types of replication.

## Permissions

Only members of the **sysadmin** fixed server role at the Publisher or members of the **db_owner** fixed database role on the publication database can execute `sp_help_publication_snapshot`.

## Related content

- [View and Modify Publication Properties](../replication/publish/view-and-modify-publication-properties.md)
- [sp_addpublication_snapshot (Transact-SQL)](sp-addpublication-snapshot-transact-sql.md)
- [sp_changepublication_snapshot (Transact-SQL)](sp-changepublication-snapshot-transact-sql.md)
- [sp_dropmergepublication (Transact-SQL)](sp-dropmergepublication-transact-sql.md)
- [sp_droppublication (Transact-SQL)](sp-droppublication-transact-sql.md)
