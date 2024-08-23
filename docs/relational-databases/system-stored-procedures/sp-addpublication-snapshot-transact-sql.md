---
title: "sp_addpublication_snapshot (Transact-SQL)"
description: sp_addpublication_snapshot creates the Snapshot Agent for the specified publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addpublication_snapshot_TSQL"
  - "sp_addpublication_snapshot"
helpviewer_keywords:
  - "sp_addpublication_snapshot"
dev_langs:
  - "TSQL"
---
# sp_addpublication_snapshot (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Creates the Snapshot Agent for the specified publication. This stored procedure is executed at the Publisher on the publication database.

> [!IMPORTANT]  
> When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *@job_login* and *@job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addpublication_snapshot
    [ @publication = ] N'publication'
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
    [ , [ @snapshot_job_name = ] N'snapshot_job_name' ]
    [ , [ @publisher_security_mode = ] publisher_security_mode ]
    [ , [ @publisher_login = ] N'publisher_login' ]
    [ , [ @publisher_password = ] N'publisher_password' ]
    [ , [ @job_login = ] N'job_login' ]
    [ , [ @job_password = ] N'job_password' ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @distributor_security_mode = ] distributor_security_mode ]
    [ , [ @distributor_login = ] N'distributor_login' ]
    [ , [ @distributor_password = ] N'distributor_password' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @frequency_type = ] *frequency_type*

The frequency with which the Snapshot Agent is executed. *@frequency_type* is **int**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `1` | Once |
| `4` (default) | Daily |
| `8` | Weekly |
| `16` | Monthly |
| `32` | Monthly, relative to the frequency interval |
| `64` | When [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent starts |
| `128` | Run when the computer is idle |

#### [ @frequency_interval = ] *frequency_interval*

The value to apply to the frequency set by *@frequency_type*. *@frequency_interval* is **int**, and can be one of the following values.

| Value of frequency_type | Effect on frequency_interval |
| --- | --- |
| `1` | *@frequency_interval* is unused. |
| `4` (default) | Every *@frequency_interval* days, with a default of daily. |
| `8` | *@frequency_interval* is one or more of the following (combined with a [&#124; (Bitwise OR)](../../t-sql/language-elements/bitwise-or-transact-sql.md) logical operator):<br /><br />`1` = Sunday &#124;<br /><br />`2` = Monday &#124;<br /><br />`4` = Tuesday &#124;<br /><br />`8` = Wednesday &#124;<br /><br />`16` = Thursday &#124;<br /><br />`32` = Friday &#124;<br /><br />`64` = Saturday |
| `16` | On the *@frequency_interval* day of the month. |
| `32` | *@frequency_interval* is one of the following values:<br /><br />`1` = Sunday &#124;<br /><br />`2` = Monday &#124;<br /><br />`3` = Tuesday &#124;<br /><br />`4` = Wednesday &#124;<br /><br />`5` = Thursday &#124;<br /><br />`6` = Friday &#124;<br /><br />`7` = Saturday &#124;<br /><br />`8` = Day &#124;<br /><br />`9` = Weekday &#124;<br /><br />`10` = Weekend day |
| `64` | *@frequency_interval* is unused. |
| `128` | *@frequency_interval* is unused. |

#### [ @frequency_subday = ] *frequency_subday*

The unit for *freq_subday_interval*. *@frequency_subday* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` | Once |
| `2` | Second |
| `4` (default) | Minute |
| `8` | Hour |

#### [ @frequency_subday_interval = ] *frequency_subday_interval*

The interval for *frequency_subday*, in minutes. *@frequency_subday_interval* is **int**, with a default of `5`.

#### [ @frequency_relative_interval = ] *frequency_relative_interval*

The date the Snapshot Agent runs. *@frequency_relative_interval* is **int**, with a default of `1`.

#### [ @frequency_recurrence_factor = ] *frequency_recurrence_factor*

The recurrence factor used by *frequency_type*. *@frequency_recurrence_factor* is **int**, with a default of `0`.

#### [ @active_start_date = ] *active_start_date*

The date when the Snapshot Agent is first scheduled, formatted as `yyyyMMdd`. *@active_start_date* is **int**, with a default of `0`.

#### [ @active_end_date = ] *active_end_date*

The date when the Snapshot Agent stops being scheduled, formatted as `yyyyMMdd`. *@active_end_date* is **int**, with a default of `99991231`,which means December 31, 9999.

#### [ @active_start_time_of_day = ] *active_start_time_of_day*

The time of day when the Snapshot Agent is first scheduled, formatted as `HHmmss`. *@active_start_time_of_day* is **int**, with a default of `0`.

#### [ @active_end_time_of_day = ] *active_end_time_of_day*

The time of day when the Snapshot Agent stops being scheduled, formatted as `HHmmss`. *@active_end_time_of_day* is **int**, with a default of `235959`, which means 11:59:59 P.M. as measured on a 24-hour clock.

#### [ @snapshot_job_name = ] N'*snapshot_job_name*'

The name of an existing Snapshot Agent job name if an existing job is being used. *@snapshot_job_name* is **nvarchar(100)**, with a default of `NULL`. This parameter is for internal use and shouldn't be specified when creating a new publication. If *@snapshot_agent_name* is specified, then *@job_login* and *@job_password* must be `NULL`.

#### [ @publisher_security_mode = ] *publisher_security_mode*

[!INCLUDE [entra-id](../../includes/entra-id.md)]

The the security mode used by the agent when connecting to the Publisher. *@publisher_security_mode* is **int**, with a default of `1`. A value of `0` must be specified for non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers. [!INCLUDE [ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]. The following values define the security mode:

- `0` specifies [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication.
- `1` specifies Windows authentication.
- `2` specifies Microsoft Entra password authentication, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.
- `3` specifies Microsoft Entra integrated authentication, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.
- `4` specifies Microsoft Entra token authentication, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.

#### [ @publisher_login = ] N'*publisher_login*'

The login used when connecting to the Publisher. *@publisher_login* is **sysname**, with a default of `NULL`. *@publisher_login* must be specified when *@publisher_security_mode* is `0`. If *@publisher_login* is `NULL` and *@publisher_security_mode* is `1`, then the account specified in *@job_login* is used when connecting to the Publisher.

#### [ @publisher_password = ] N'*publisher_password*'

The password used when connecting to the Publisher. *@publisher_password* is **sysname**, with a default of `NULL`.

> [!IMPORTANT]  
> Don't store authentication information in script files. To help improve security, we recommend that you provide login names and passwords at run time.

#### [ @job_login = ] N'*job_login*'

The login for the account under which the agent runs. On Azure SQL Managed Instance, use a SQL Server account. *@job_login* is **nvarchar(257)**, with a default of `NULL`. This account is always used for agent connections to the Distributor. You must supply this parameter when creating a new Snapshot Agent job.

For non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers, this must be the same login specified in [sp_adddistpublisher](sp-adddistpublisher-transact-sql.md).

#### [ @job_password = ] N'*job_password*'

The password for the Windows account under which the agent runs. *@job_password* is **sysname**, with no default. You must supply this parameter when creating a new Snapshot Agent job.

> [!IMPORTANT]  
> Don't store authentication information in script files. To help improve security, we recommend that you provide login names and passwords at run time.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be used when creating a Snapshot Agent at a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

#### [ @distributor_security_mode = ] *distributor_security_mode*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @distributor_login = ] N'*distributor_login*'

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @distributor_password = ] N'*distributor_password*'

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addpublication_snapshot` is used in snapshot replication, transactional replication, and merge replication.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-addpublication-snapsh_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_addpublication_snapshot`.

## Related content

- [Create a publication](../replication/publish/create-a-publication.md)
- [Create and Apply the Initial Snapshot](../replication/create-and-apply-the-initial-snapshot.md)
- [sp_addpublication (Transact-SQL)](sp-addpublication-transact-sql.md)
- [sp_changepublication_snapshot (Transact-SQL)](sp-changepublication-snapshot-transact-sql.md)
- [sp_startpublication_snapshot (Transact-SQL)](sp-startpublication-snapshot-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
