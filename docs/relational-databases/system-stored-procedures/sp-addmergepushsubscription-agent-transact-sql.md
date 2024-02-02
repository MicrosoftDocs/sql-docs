---
title: "sp_addmergepushsubscription_agent (Transact-SQL)"
description: Adds a new agent job used to schedule synchronization of a push subscription to a merge publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addmergepushsubscription_agent_TSQL"
  - "sp_addmergepushsubscription_agent"
helpviewer_keywords:
  - "sp_addmergepushsubscription_agent"
dev_langs:
  - "TSQL"
---
# sp_addmergepushsubscription_agent (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Adds a new agent job used to schedule synchronization of a push subscription to a merge publication. This stored procedure is executed at the Publisher on the publication database.

> [!IMPORTANT]  
> When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *job_login* and *job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

[!INCLUDE [entra-id](../../includes/entra-id.md)]

## Syntax

```syntaxsql
sp_addmergepushsubscription_agent
    [ @publication = ] N'publication'
    [ , [ @subscriber = ] N'subscriber' ]
    [ , [ @subscriber_db = ] N'subscriber_db' ]
    [ , [ @subscriber_security_mode = ] subscriber_security_mode ]
    [ , [ @subscriber_login = ] N'subscriber_login' ]
    [ , [ @subscriber_password = ] N'subscriber_password' ]
    [ , [ @publisher_security_mode = ] publisher_security_mode ]
    [ , [ @publisher_login = ] N'publisher_login' ]
    [ , [ @publisher_password = ] N'publisher_password' ]
    [ , [ @job_login = ] N'job_login' ]
    [ , [ @job_password = ] N'job_password' ]
    [ , [ @job_name = ] N'job_name' ]
    [ , [ @frequency_type = ] frequency_type ]
    [ , [ @frequency_interval = ] frequency_interval ]
    [ , [ @frequency_relative_interval = ] frequency_relative_interval ]
    [ , [ @frequency_recurrence_factor = ] frequency_recurrence_factor ]
    [ , [ @frequency_subday = ] frequency_subday ]
    [ , [ @frequency_subday_interval = ] frequency_subday_interval ]
    [ , [ @active_start_time_of_day = ] active_start_time_of_day ]
    [ , [ @active_end_time_of_day = ] active_end_time_of_day ]
    [ , [ @active_start_date = ] active_start_date ]
    [ , [ @active_end_date = ] active_end_date ]
    [ , [ @enabled_for_syncmgr = ] N'enabled_for_syncmgr' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with a default of `NULL`.

#### [ @subscriber_db = ] N'*subscriber_db*'

The name of the subscription database. *@subscriber_db* is **sysname**, with a default of `NULL`.

#### [ @subscriber_security_mode = ] *subscriber_security_mode*

The security mode to use when connecting to a Subscriber when synchronizing. *@subscriber_security_mode* is **smallint**, with a default of `1`. The following values define the security mode:

- `0` specifies [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication.
- `1` specifies Windows authentication.
- `2` specifies Microsoft Entra password authentication, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.
- `3` specifies Microsoft Entra integrated authentication, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.
- `4` specifies Microsoft Entra token authentication, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]

#### [ @subscriber_login = ] N'*subscriber_login*'

The Subscriber login to use when connecting to a Subscriber when synchronizing. *@subscriber_login* is **sysname**, with a default of `NULL`. *@subscriber_login* is required if *@subscriber_security_mode* is set to `0`.

#### [ @subscriber_password = ] N'*subscriber_password*'

The Subscriber password for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. *@subscriber_password* is **sysname**, with a default of `NULL`. *@subscriber_password* is required if *@subscriber_security_mode* is set to `0`. If a subscriber password is used, it's automatically encrypted.

> [!IMPORTANT]  
> When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.

#### [ @publisher_security_mode = ] *publisher_security_mode*

The security mode to use when connecting to a Publisher when synchronizing. *@publisher_security_mode* is **smallint**, with a default of `1`. The following values define the security mode:

- `0` specifies [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication.
- `1` specifies Windows authentication.
- `2` specifies Microsoft Entra password authentication, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.
- `3` specifies Microsoft Entra integrated authentication, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.
- `4` specifies Microsoft Entra token authentication, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]

#### [ @publisher_login = ] N'*publisher_login*'

The login to use when connecting to a Publisher when synchronizing. *@publisher_login* is **sysname**, with a default of `NULL`.

#### [ @publisher_password = ] N'*publisher_password*'

The password used when connecting to the Publisher. *@publisher_password* is **sysname**, with a default of `NULL`.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)] When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.

#### [ @job_login = ] N'*job_login*'

The login for the Windows account under which the agent runs. *@job_login* is **nvarchar(257)**, with a default of `NULL`. This Windows account is always used for agent connections to the Distributor and for connections to the Subscriber and Publisher when using Windows Integrated authentication.

#### [ @job_password = ] N'*job_password*'

The password for the Windows account under which the agent runs. *@job_password* is **sysname**, with no default.

> [!IMPORTANT]  
> When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.

#### [ @job_name = ] N'*job_name*'

The name of an existing agent job. *@job_name* is **sysname**, with a default of `NULL`. This parameter is only specified when the subscription is synchronized using an existing job instead of a newly created job (the default). If you aren't a member of the **sysadmin** fixed server role, you must specify *job_login* and *job_password* when you specify *@job_name*.

#### [ @frequency_type = ] *frequency_type*

A value indicating when the Merge Agent runs. *@frequency_type* is **int**, and can be one of the following values.

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

> [!NOTE]  
> Specifying a value of `64` causes the Merge Agent to run in continuous mode. This corresponds to setting the `-Continuous` parameter for the agent. For more information, see [Replication Merge Agent](../replication/agents/replication-merge-agent.md).

#### [ @frequency_interval = ] *frequency_interval*

The days that the Merge Agent runs. *@frequency_interval* is **int**, and can be one of the following values.

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

#### [ @frequency_relative_interval = ] *frequency_relative_interval*

The date of the Merge Agent. This parameter is used when *@frequency_type* is set to `32` (monthly relative). *@frequency_relative_interval* is **int**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `1` | First |
| `2` | Second |
| `4` | Third |
| `8` | Fourth |
| `16` | Last |
| `NULL` (default) | |

#### [ @frequency_recurrence_factor = ] *frequency_recurrence_factor*

The recurrence factor used by *@frequency_type*. *@frequency_recurrence_factor* is **int**, with a default of `0`.

#### [ @frequency_subday = ] *frequency_subday*

How often to reschedule during the defined period. *@frequency_subday* is **int**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `1` | Once |
| `2` | Second |
| `4` | Minute |
| `8` | Hour |
| `NULL` (default) | |

#### [ @frequency_subday_interval = ] *frequency_subday_interval*

The interval for *@frequency_subday*. *@frequency_subday_interval* is **int**, with a default of `NULL`.

#### [ @active_start_time_of_day = ] *active_start_time_of_day*

The time of day when the Merge Agent is first scheduled, formatted as `HHmmss`. *@active_start_time_of_day* is **int**, with a default of `0`.

#### [ @active_end_time_of_day = ] *active_end_time_of_day*

The time of day when the Merge Agent stops being scheduled, formatted as `HHmmss`. *@active_end_time_of_day* is **int**, with a default of `235959`.

#### [ @active_start_date = ] *active_start_date*

The date when the Merge Agent is first scheduled, formatted as `yyyyMMdd`. *@active_start_date* is **int**, with a default of `0`.

#### [ @active_end_date = ] *active_end_date*

The date when the Merge Agent stops being scheduled, formatted as `yyyyMMdd`. *@active_end_date* is **int**, with a default of `99991231`.

#### [ @enabled_for_syncmgr = ] N'*enabled_for_syncmgr*'

Specifies if the subscription can be synchronized through Windows Synchronization Manager. *@enabled_for_syncmgr* is **nvarchar(5)**, with a default of `false`.

- If `false`, the subscription isn't registered with Synchronization Manager.
- If `true`, the subscription is registered with Synchronization Manager and can be synchronized without starting [!INCLUDE [ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)].

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addmergepushsubscription_agent` is used in merge replication and uses functionality similar to [sp_addpushsubscription_agent](sp-addpullsubscription-agent-transact-sql.md).

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-addmergepushsubscript_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_addmergepushsubscription_agent`.

## Related content

- [Create a push subscription](../replication/create-a-push-subscription.md)
- [Subscribe to Publications](../replication/subscribe-to-publications.md)
- [sp_addmergesubscription (Transact-SQL)](sp-addmergesubscription-transact-sql.md)
- [sp_changemergesubscription (Transact-SQL)](sp-changemergesubscription-transact-sql.md)
- [sp_dropmergesubscription (Transact-SQL)](sp-dropmergesubscription-transact-sql.md)
- [sp_helpmergesubscription (Transact-SQL)](sp-helpmergesubscription-transact-sql.md)
