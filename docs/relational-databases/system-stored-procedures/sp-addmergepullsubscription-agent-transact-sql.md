---
title: "sp_addmergepullsubscription_agent (Transact-SQL)"
description: Adds a new agent job used to schedule synchronization of a pull subscription to a merge publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addmergepullsubscription_agent"
  - "sp_addmergepullsubscription_agent_TSQL"
helpviewer_keywords:
  - "sp_addmergepullsubscription_agent"
dev_langs:
  - "TSQL"
---
# sp_addmergepullsubscription_agent (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Adds a new agent job used to schedule synchronization of a pull subscription to a merge publication. This stored procedure is executed at the Subscriber on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addmergepullsubscription_agent
    [ [ @name = ] N'name' ]
    , [ @publisher = ] N'publisher'
    , [ @publisher_db = ] N'publisher_db'
    , [ @publication = ] N'publication'
    [ , [ @publisher_security_mode = ] publisher_security_mode ]
    [ , [ @publisher_login = ] N'publisher_login' ]
    [ , [ @publisher_password = ] N'publisher_password' ]
    [ , [ @publisher_encrypted_password = ] publisher_encrypted_password ]
    [ , [ @subscriber = ] N'subscriber' ]
    [ , [ @subscriber_db = ] N'subscriber_db' ]
    [ , [ @subscriber_security_mode = ] subscriber_security_mode ]
    [ , [ @subscriber_login = ] N'subscriber_login' ]
    [ , [ @subscriber_password = ] N'subscriber_password' ]
    [ , [ @distributor = ] N'distributor' ]
    [ , [ @distributor_security_mode = ] distributor_security_mode ]
    [ , [ @distributor_login = ] N'distributor_login' ]
    [ , [ @distributor_password = ] N'distributor_password' ]
    [ , [ @encrypted_password = ] encrypted_password ]
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
    [ , [ @optional_command_line = ] N'optional_command_line' ]
    [ , [ @merge_jobid = ] merge_jobid OUTPUT ]
    [ , [ @enabled_for_syncmgr = ] N'enabled_for_syncmgr' ]
    [ , [ @ftp_address = ] N'ftp_address' ]
    [ , [ @ftp_port = ] ftp_port ]
    [ , [ @ftp_login = ] N'ftp_login' ]
    [ , [ @ftp_password = ] N'ftp_password' ]
    [ , [ @alt_snapshot_folder = ] N'alt_snapshot_folder' ]
    [ , [ @working_directory = ] N'working_directory' ]
    [ , [ @use_ftp = ] N'use_ftp' ]
    [ , [ @reserved = ] N'reserved' ]
    [ , [ @use_interactive_resolver = ] N'use_interactive_resolver' ]
    [ , [ @offloadagent = ] N'offloadagent' ]
    [ , [ @offloadserver = ] N'offloadserver' ]
    [ , [ @job_name = ] N'job_name' ]
    [ , [ @dynamic_snapshot_location = ] N'dynamic_snapshot_location' ]
    [ , [ @use_web_sync = ] use_web_sync ]
    [ , [ @internet_url = ] N'internet_url' ]
    [ , [ @internet_login = ] N'internet_login' ]
    [ , [ @internet_password = ] N'internet_password' ]
    [ , [ @internet_security_mode = ] internet_security_mode ]
    [ , [ @internet_timeout = ] internet_timeout ]
    [ , [ @hostname = ] N'hostname' ]
    [ , [ @job_login = ] N'job_login' ]
    [ , [ @job_password = ] N'job_password' ]
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the agent. *@name* is **sysname**, with a default of `NULL`.

#### [ @publisher = ] N'*publisher*'

The name of the Publisher server. *@publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the Publisher database. *@publisher_db* is **sysname**, with no default.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @publisher_security_mode = ] *publisher_security_mode*

The security mode to use when connecting to a Publisher when synchronizing. *@publisher_security_mode* is **int**, with a default of `1`. The following values define the security mode:

- `0` specifies [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication.
- `1` specifies Windows authentication.
- `2` specifies Microsoft Entra password authentication, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.
- `3` specifies Microsoft Entra integrated authentication, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]

#### [ @publisher_login = ] N'*publisher_login*'

The login to use when connecting to a Publisher when synchronizing. *@publisher_login* is **sysname**, with a default of `NULL`.

#### [ @publisher_password = ] N'*publisher_password*'

The password used when connecting to the Publisher. *@publisher_password* is **sysname**, with a default of `NULL`.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)] When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.

#### [ @publisher_encrypted_password = ] *publisher_encrypted_password*

Setting *@publisher_encrypted_password* is no longer supported. Attempting to set this **bit** parameter to `1` results in an error.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with a default of `NULL`.

#### [ @subscriber_db = ] N'*subscriber_db*'

The name of the subscription database. *@subscriber_db* is **sysname**, with a default of `NULL`.

#### [ @subscriber_security_mode = ] *subscriber_security_mode*

The security mode to use when connecting to a Subscriber when synchronizing. *@subscriber_security_mode* is **int**, with a default of `1`. If `0`, specifies [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. If `1`, specifies Windows Authentication.

> [!NOTE]  
> [!INCLUDE [deprecated-parameter-returns-warning](../includes/deprecated-parameter-returns-warning.md)]

#### [ @subscriber_login = ] N'*subscriber_login*'

The Subscriber login to use when connecting to a Subscriber when synchronizing. *@subscriber_login* is **sysname**, with a default of `NULL`. *@subscriber_login* is required if *subscriber_security_mode* is set to `0`.

> [!NOTE]  
> [!INCLUDE [deprecated-parameter-returns-warning](../includes/deprecated-parameter-returns-warning.md)]

#### [ @subscriber_password = ] N'*subscriber_password*'

The Subscriber password for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. *@subscriber_password* is **sysname**, with a default of `NULL`. *@subscriber_password* is required if *@subscriber_security_mode* is set to `0`.

> [!NOTE]  
> [!INCLUDE [deprecated-parameter-returns-warning](../includes/deprecated-parameter-returns-warning.md)]

#### [ @distributor = ] N'*distributor*'

The name of the Distributor. *@distributor* is **sysname**, with a default of *@publisher*; that is, the Publisher is also the Distributor.

#### [ @distributor_security_mode = ] *distributor_security_mode*

The security mode to use when connecting to a Distributor when synchronizing. *@distributor_security_mode* is **int**, with a default of `1`. The following values define the security mode:

- `0` specifies [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication.
- `1` specifies Windows authentication.
- `2` specifies Microsoft Entra password authentication, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.
- `3` specifies Microsoft Entra integrated authentication, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.
- `4` specifies Microsoft Entra token authentication, starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]

#### [ @distributor_login = ] N'*distributor_login*'

The Distributor login to use when connecting to a Distributor when synchronizing. *@distributor_login* is **sysname**, with a default of `NULL`. *@distributor_login* is required if *@distributor_security_mode* is set to `0`.

#### [ @distributor_password = ] N'*distributor_password*'

The Distributor password. *@distributor_password* is **sysname**, with a default of `NULL`. *@distributor_password* is required if *@distributor_security_mode* is set to `0`.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)] When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.

#### [ @encrypted_password = ] *encrypted_password*

Setting *@encrypted_password* is no longer supported. Attempting to set this **bit** parameter to `1` results in an error.

#### [ @frequency_type = ] *frequency_type*

A value indicating when the Merge Agent runs. *@frequency_type* is **int**, and can be one of these values.

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

The day or days that the Merge Agent runs. *@frequency_interval* is **int**, with a default of `NULL`, and can be one of these values.

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

The date of the Merge Agent. This parameter is used when *@frequency_type* is set to `32` (monthly relative). *@frequency_relative_interval* is **int**, and can be one of these values.

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

#### [ @frequency_subday = ] *frequency_subday*

How often to reschedule during the defined period. *@frequency_subday* is **int**, and can be one of these values.

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

The time of day when the Merge Agent is first scheduled, formatted as `HHmmss`. *@active_start_time_of_day* is **int**, with a default of `NULL`.

#### [ @active_end_time_of_day = ] *active_end_time_of_day*

The time of day when the Merge Agent stops being scheduled, formatted as `HHmmss`. *@active_end_time_of_day* is **int**, with a default of `NULL`.

#### [ @active_start_date = ] *active_start_date*

The date when the Merge Agent is first scheduled, formatted as `yyyyMMdd`. *@active_start_date* is **int**, with a default of `NULL`.

#### [ @active_end_date = ] *active_end_date*

The date when the Merge Agent stops being scheduled, formatted as `yyyyMMdd`. *@active_end_date* is **int**, with a default of `NULL`.

#### [ @optional_command_line = ] N'*optional_command_line*'

An optional command prompt that is supplied to the Merge Agent. *@optional_command_line* is **nvarchar(255)**, with a default of an empty string.

Can be used to supply additional parameters to the Merge Agent, such as in the following example that increases the default query time-out to `600` seconds:

```sql
@optional_command_line = N'-QueryTimeOut 600'
```

#### [ @merge_jobid = ] *merge_jobid* OUTPUT

The output parameter for the job ID. *@merge_jobid* is an OUTPUT parameter of type **binary(16)**, with a default of `NULL`.

#### [ @enabled_for_syncmgr = ] N'*enabled_for_syncmgr*'

Specifies if the subscription can be synchronized through Windows Synchronization Manager. *@enabled_for_syncmgr* is **nvarchar(5)**, with a default of `false`. If `false`, the subscription isn't registered with Synchronization Manager. If `true`, the subscription is registered with Synchronization Manager and can be synchronized without starting [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

#### [ @ftp_address = ] N'*ftp_address*'

[!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)]

#### [ @ftp_port = ] *ftp_port*

[!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)]

#### [ @ftp_login = ] N'*ftp_login*'

[!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)]

#### [ @ftp_password = ] N'*ftp_password*'

[!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)]

#### [ @alt_snapshot_folder = ] N'*alt_snapshot_folder*'

Specifies the location from which to pick up the snapshot files. *@alt_snapshot_folder* is **nvarchar(255)**, with a default of `NULL`. If `NULL`, the snapshot files will be picked up from the default location specified by the Publisher.

#### [ @working_directory = ] N'*working_directory*'

The name of the working directory used to temporarily store data and schema files for the publication when FTP is used to transfer snapshot files. *@working_directory* is **nvarchar(255)**, with a default of `NULL`.

#### [ @use_ftp = ] N'*use_ftp*'

Specifies the use of FTP instead of the typical protocol to retrieve snapshots. *@use_ftp* is **nvarchar(5)**, with a default of `false`.

#### [ @reserved = ] N'*reserved*'

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @use_interactive_resolver = ] N'*use_interactive_resolver*'

Uses interactive resolver to resolve conflicts for all articles that allow interactive resolution. *@use_interactive_resolver* is **nvarchar(5)**, with a default of `false`.

#### [ @offloadagent = ] N'*offloadagent*'

> [!NOTE]  
> [!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)] Setting *remote_agent_activation* to a value other than `false` generates an error.

#### [ @offloadserver = ] N'*offloadserver*'

> [!NOTE]  
> [!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)] Setting *remote_agent_server_name* to any non-NULL value will generate an error.

#### [ @job_name = ] N'*job_name*'

The name of an existing agent job. *@job_name* is **sysname**, with a default of `NULL`. This parameter is only specified when the subscription will be synchronized using an existing job instead of a newly created job (the default). If you aren't a member of the **sysadmin** fixed server role, you must specify *@job_login* and *@job_password* when you specify *@job_name*.

#### [ @dynamic_snapshot_location = ] N'*dynamic_snapshot_location*'

The path to the folder where the snapshot files will be read from if a filtered data snapshot is to be used. *@dynamic_snapshot_location* is **nvarchar(260)**, with a default of `NULL`. For more information, see [Parameterized Filters - Parameterized Row Filters](../replication/merge/parameterized-filters-parameterized-row-filters.md).

#### [ @use_web_sync = ] *use_web_sync*

Indicates that Web synchronization is enabled. *@use_web_sync* is **bit**, with a default of `0`. `1` specifies that the pull subscription can be synchronized over the internet using HTTP.

#### [ @internet_url = ] N'*internet_url*'

The location of the replication listener (REPLISAPI.DLL) for Web synchronization. *@internet_url* is **nvarchar(260)**, with a default of `NULL`. *@internet_url* is a fully qualified URL, in the format `http://server.domain.com/directory/replisapi.dll`. If the server is configured to listen on a port other than port 80, the port number must also be supplied in the format `http://server.domain.com:<portnumber>/directory/replisapi.dll`, where `<portnumber>` represents the port.

#### [ @internet_login = ] N'*internet_login*'

The login that the Merge Agent uses when connecting to the Web server that is hosting Web synchronization using HTTP Basic Authentication. *@internet_login* is **sysname**, with a default of `NULL`.

#### [ @internet_password = ] N'*internet_password*'

The password that the Merge Agent uses when connecting to the Web server that is hosting Web synchronization using HTTP Basic Authentication. *@internet_password* is **nvarchar(524)**, with a default of `NULL`.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteStrongPass](../../includes/ssnotestrongpass-md.md)]

#### [ @internet_security_mode = ] *internet_security_mode*

The authentication method used by the Merge Agent when connecting to the Web server during Web synchronization using HTTPS. *@internet_security_mode* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `0` | Basic Authentication is used. |
| `1` (default) | Windows Integrated Authentication is used. |

> [!NOTE]  
> We recommend using Basic Authentication with Web synchronization. To use Web synchronization, you must make a TLS connection to the Web server. For more information, see [Configure Web Synchronization](../replication/configure-web-synchronization.md).

#### [ @internet_timeout = ] *internet_timeout*

The length of time, in seconds, before a Web synchronization request expires. *@internet_timeout* is **int**, with a default of `300` seconds.

#### [ @hostname = ] N'*hostname*'

Overrides the value of `HOST_NAME()` when this function is used in the `WHERE` clause of a parameterized filter. *@hostname* is **sysname**, with a default of `NULL`.

#### [ @job_login = ] N'*job_login*'

The login for the Windows account under which the agent runs. *@job_login* is **nvarchar(257)**, with a default of `NULL`. This Windows account is always used for agent connections to the Subscriber and for connections to the Distributor and Publisher when using Windows Integrated authentication.

#### [ @job_password = ] N'*job_password*'

The password for the Windows account under which the agent runs. *@job_password* is **sysname**, with a default of `NULL`.

> [!CAUTION]  
> Don't store authentication information in script files. For best security, login names and passwords should be supplied at runtime.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addmergepullsubscription_agent` is used in merge replication and uses functionality similar to [sp_addpullsubscription_agent](sp-addpullsubscription-agent-transact-sql.md).

For an example of how to correctly specify security settings when executing `sp_addmergepullsubscription_agent`, see [Create a Pull Subscription](../replication/create-a-pull-subscription.md).

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-addmergepullsubscript_1_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_addmergepullsubscription_agent`.

## Related content

- [Create a Pull Subscription](../replication/create-a-pull-subscription.md)
- [Subscribe to Publications](../replication/subscribe-to-publications.md)
- [sp_addmergepullsubscription (Transact-SQL)](sp-addmergepullsubscription-transact-sql.md)
- [sp_changemergepullsubscription (Transact-SQL)](sp-changemergepullsubscription-transact-sql.md)
- [sp_dropmergepullsubscription (Transact-SQL)](sp-dropmergepullsubscription-transact-sql.md)
- [sp_helpmergepullsubscription (Transact-SQL)](sp-helpmergepullsubscription-transact-sql.md)
- [sp_helpsubscription_properties (Transact-SQL)](sp-helpsubscription-properties-transact-sql.md)
