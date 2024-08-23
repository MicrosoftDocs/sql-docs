---
title: "sp_MSchange_snapshot_agent_properties (T-SQL)"
description: Describes the sp_MSchange_snapshot_agent_properties stored procedure used to change the properties for the Snapshot Agent used for SQL Server Replication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_MSchange_snapshot_agent_properties_TSQL"
  - "sp_MSchange_snapshot_agent_properties"
helpviewer_keywords:
  - "sp_MSchange_snapshot_agent_properties"
dev_langs:
  - "TSQL"
---
# sp_MSchange_snapshot_agent_properties (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the properties of a Snapshot Agent job that runs at a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] or later version Distributor. This stored procedure is used to change properties when the Publisher runs on an instance of [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)]. This stored procedure is executed at the Distributor on the distribution database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_MSchange_snapshot_agent_properties
    [ @publisher = ] N'publisher'
    , [ @publisher_db = ] N'publisher_db'
    , [ @publication = ] N'publication'
    , [ @frequency_type = ] frequency_type
    , [ @frequency_interval = ] frequency_interval
    , [ @frequency_subday = ] frequency_subday
    , [ @frequency_subday_interval = ] frequency_subday_interval
    , [ @frequency_relative_interval = ] frequency_relative_interval
    , [ @frequency_recurrence_factor = ] frequency_recurrence_factor
    , [ @active_start_date = ] active_start_date
    , [ @active_end_date = ] active_end_date
    , [ @active_start_time_of_day = ] active_start_time_of_day
    , [ @active_end_time_of_day = ] active_end_time_of_day
    , [ @snapshot_job_name = ] N'snapshot_job_name'
    , [ @publisher_security_mode = ] publisher_security_mode
    , [ @publisher_login = ] N'publisher_login'
    , [ @publisher_password = ] N'publisher_password'
    , [ @job_login = ] N'job_login'
    , [ @job_password = ] N'job_password'
    , [ @publisher_type = ] N'publisher_type'
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the publication database. *@publisher_db* is **sysname**, with no default.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @frequency_type = ] *frequency_type*

Specifies the frequency with which the Snapshot Agent is executed. *@frequency_type* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` | Once |
| `2` | On demand |
| `4` | Daily |
| `8` | Weekly |
| `10` | Monthly |
| `20` | Monthly, relative to the frequency interval |
| `40` | When [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent starts |

#### [ @frequency_interval = ] *frequency_interval*

The value to apply to the frequency set by *@frequency_type*. *@frequency_interval* is **int**, with no default.

#### [ @frequency_subday = ] *frequency_subday*

The units for *@frequency_subday_interval*. *@frequency_subday* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` | Once |
| `2` | Second |
| `4` | Minute |
| `8` | Hour |

#### [ @frequency_subday_interval = ] *frequency_subday_interval*

The interval for *@frequency_subday*. *@frequency_subday_interval* is **int**, with no default.

#### [ @frequency_relative_interval = ] *frequency_relative_interval*

The date the Snapshot Agent runs. *@frequency_relative_interval* is **int**, with no default.

#### [ @frequency_recurrence_factor = ] *frequency_recurrence_factor*

The recurrence factor used by *@frequency_type*. *@frequency_recurrence_factor* is **int**, with no default.

#### [ @active_start_date = ] *active_start_date*

The date when the Snapshot Agent is first scheduled, formatted as `yyyyMMdd`. *@active_start_date* is **int**, with no default.

#### [ @active_end_date = ] *active_end_date*

The date when the Snapshot Agent stops being scheduled, formatted as `yyyyMMdd`. *@active_end_date* is **int**, with no default.

#### [ @active_start_time_of_day = ] *active_start_time_of_day*

The time of day when the Snapshot Agent is first scheduled, formatted as `HHmmss`. *@active_start_time_of_day* is **int**, with no default.

#### [ @active_end_time_of_day = ] *active_end_time_of_day*

The time of day when the Snapshot Agent stops being scheduled, formatted as `HHmmss`. *@active_end_time_of_day* is **int**, with no default.

#### [ @snapshot_job_name = ] N'*snapshot_job_name*'

The name of an existing Snapshot Agent job name if an existing job is being used. *@snapshot_job_name* is **nvarchar(100)**, with no default.

#### [ @publisher_security_mode = ] *publisher_security_mode*

The security mode used by the agent when connecting to the Publisher. *@publisher_security_mode* is **int**, with no default. A value of `0` must be specified for non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers.

- `0` specifies [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication
- `1` specifies Windows authentication

[!INCLUDE [ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]

#### [ @publisher_login = ] N'*publisher_login*'

The login used when connecting to the Publisher. *@publisher_login* is **sysname**, with no default. *@publisher_login* must be specified when *@publisher_security_mode* is `0`. If *@publisher_login* is `NULL` and *@publisher_security_mode* is `1`, then the Windows account specified in *@job_login* is used when connecting to the Publisher.

#### [ @publisher_password = ] N'*publisher_password*'

The password used when connecting to the Publisher. *@publisher_password* is **nvarchar(524)**, with no default.

> [!IMPORTANT]  
> Don't store authentication information in script files. To help improve security, we recommend that you provide login names and passwords at run time.

#### [ @job_login = ] N'*job_login*'

The login for the Windows account under which the agent runs. *@job_login* is **nvarchar(257)**, with no default. This Windows account is always used for agent connections to the Distributor. You must supply this parameter when creating a new Snapshot Agent job. *This property can't be changed for a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.*

#### [ @job_password = ] N'*job_password*'

The password for the Windows account under which the agent runs. *@job_password* is **sysname**, with no default. You must supply this parameter when creating a new Snapshot Agent job.

> [!IMPORTANT]  
> Don't store authentication information in script files. To help improve security, we recommend that you provide login names and passwords at run time.

#### [ @publisher_type = ] N'*publisher_type*'

Specifies the Publisher type when the Publisher isn't running in an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. *@publisher_type* is **sysname**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `MSSQLSERVER` | Specifies a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. |
| `ORACLE` | Specifies a standard Oracle Publisher. |
| `ORACLE GATEWAY` | Specifies an Oracle Gateway Publisher. |

For more information about the differences between an Oracle Publisher and an Oracle Gateway Publisher, see [Oracle Publishing Overview](../replication/non-sql/oracle-publishing-overview.md).

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_MSchange_snapshot_agent_properties` is used in snapshot replication, transactional replication, and merge replication.

You must specify all parameters when executing `sp_MSchange_snapshot_agent_properties`. Execute [sp_helppublication_snapshot](sp-helppublication-snapshot-transact-sql.md) to return the current properties of the Snapshot Agent job.

You can use [sp_changepublication_snapshot](sp-changepublication-snapshot-transact-sql.md) on the Publisher to change properties of a Snapshot Agent job.

## Permissions

Only members of the **sysadmin** fixed server role at the Distributor can execute `sp_MSchange_snapshot_agent_properties`.

## Related content

- [sp_addpublication_snapshot (Transact-SQL)](sp-addpublication-snapshot-transact-sql.md)
