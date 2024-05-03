---
title: "sp_addpushsubscription_agent (Transact-SQL)"
description: Adds a new scheduled agent job used to synchronize a push subscription to a transactional publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addpushsubscription_agent_TSQL"
  - "sp_addpushsubscription_agent"
helpviewer_keywords:
  - "sp_addpushsubscription_agent"
dev_langs:
  - "TSQL"
---
# sp_addpushsubscription_agent (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Adds a new scheduled agent job used to synchronize a push subscription to a transactional publication. This stored procedure is executed at the Publisher on the publication database.

> [!IMPORTANT]  
> When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *@job_login* and *@job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addpushsubscription_agent
    [ @publication = ] N'publication'
    [ , [ @subscriber = ] N'subscriber' ]
    [ , [ @subscriber_db = ] N'subscriber_db' ]
    [ , [ @subscriber_security_mode = ] subscriber_security_mode ]
    [ , [ @subscriber_login = ] N'subscriber_login' ]
    [ , [ @subscriber_password = ] N'subscriber_password' ]
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
    [ , [ @dts_package_name = ] N'dts_package_name' ]
    [ , [ @dts_package_password = ] N'dts_package_password' ]
    [ , [ @dts_package_location = ] N'dts_package_location' ]
    [ , [ @enabled_for_syncmgr = ] N'enabled_for_syncmgr' ]
    [ , [ @distribution_job_name = ] N'distribution_job_name' ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @subscriber_provider = ] N'subscriber_provider' ]
    [ , [ @subscriber_datasrc = ] N'subscriber_datasrc' ]
    [ , [ @subscriber_location = ] N'subscriber_location' ]
    [ , [ @subscriber_provider_string = ] N'subscriber_provider_string' ]
    [ , [ @subscriber_catalog = ] N'subscriber_catalog' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber instance, or the name of the AG listener if the subscriber database is part of an availability group. *@subscriber* is **sysname**, with a default of `NULL`.

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 "
[!INCLUDE [custom-port](includes/custom-port.md)]
::: moniker-end

#### [ @subscriber_db = ] N'*subscriber_db*'

The name of the subscription database. *@subscriber_db* is **sysname**, with a default of `NULL`.

For a non-SQL Server Subscriber, specify a value of **(default destination)** for *subscriber_db*.

#### [ @subscriber_security_mode = ] *subscriber_security_mode*

The security mode to use when connecting to a Subscriber when synchronizing. *@subscriber_security_mode* is **smallint**, with a default of `1`. The following values define the security mode:

- `0` specifies [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication.
- `1` specifies Windows authentication.
- `2` specifies Microsoft Entra password authentication starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.
- `3` specifies Microsoft Entra integrated authentication starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.
- `4` specifies Microsoft Entra token authentication starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 6.

> [!IMPORTANT]  
> For queued updating subscriptions, use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication for connections to Subscribers, and specify a different account for the connection to each Subscriber. For all other subscriptions, use Windows Authentication.

#### [ @subscriber_login = ] N'*subscriber_login*'

The Subscriber login to use when connecting to a Subscriber when synchronizing. *@subscriber_login* is **sysname**, with a default of `NULL`.

#### [ @subscriber_password = ] N'*subscriber_password*'

The Subscriber password. *subscriber_password* is required if *subscriber_security_mode* is set to `0`. *@subscriber_password* is **sysname**, with a default of `NULL`. If a subscriber password is used, it's automatically encrypted.

> [!IMPORTANT]  
> [!INCLUDE [ssnotestrongpass-md](../../includes/ssnotestrongpass-md.md)] When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.

#### [ @job_login = ] N'*job_login*'

The login for the account under which the agent runs. On Azure SQL Managed Instance, use a SQL Server account. *@job_login* is **nvarchar(257)**, with a default of `NULL`. This Windows account is always used for agent connections to the Distributor and for connections to the Subscriber when using Windows Integrated authentication.

#### [ @job_password = ] N'*job_password*'

The password for the account under which the agent runs. *@job_password* is **sysname**, with no default.

> [!IMPORTANT]  
> When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, you must secure the file to prevent unauthorized access.

#### [ @job_name = ] N'*job_name*'

The name of an existing agent job. *@job_name* is **sysname**, with a default of `NULL`. This parameter is only specified when the subscription is synchronized using an existing job, instead of a newly created job (the default). If you aren't a member of the **sysadmin** fixed server role, you must specify *@job_login* and *@job_password* when you specify *@job_name*.

#### [ @frequency_type = ] *frequency_type*

The frequency with which to schedule the Distribution Agent. *@frequency_type* is **int**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `1` | One time |
| `2` | On demand |
| `4` | Daily |
| `8` | Weekly |
| `16` | Monthly |
| `32` | Monthly relative |
| `64` (default) | Autostart |
| `128` | Recurring |

> [!NOTE]  
> Specifying a value of `64` causes the Distribution Agent to run in continuous mode. This corresponds to setting the `-Continuous` parameter for the agent. For more information, see [Replication Distribution Agent](../replication/agents/replication-distribution-agent.md).

#### [ @frequency_interval = ] *frequency_interval*

The value to apply to the frequency set by *@frequency_type*. *@frequency_interval* is **int**, with a default of `1`.

#### [ @frequency_relative_interval = ] *frequency_relative_interval*

The date of the Distribution Agent. This parameter is used when *frequency_type* is set to `32` (monthly relative).
*@frequency_relative_interval* is **int**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `1` (default) | First |
| `2` | Second |
| `4` | Third |
| `8` | Fourth |
| `16` | Last |

#### [ @frequency_recurrence_factor = ] *frequency_recurrence_factor*

The recurrence factor used by *@frequency_type*. *@frequency_recurrence_factor* is **int**, with a default of `0`.

#### [ @frequency_subday = ] *frequency_subday*

Specifies how often to reschedule during the defined period. *@frequency_subday* is **int**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `1` | Once |
| `2` | Second |
| `4` (default) | Minute |
| `8` | Hour |

#### [ @frequency_subday_interval = ] *frequency_subday_interval*

The interval for *@frequency_subday*. *@frequency_subday_interval* is **int**, with a default of `5`.

#### [ @active_start_time_of_day = ] *active_start_time_of_day*

The time of day when the Distribution Agent is first scheduled, formatted as `HHmmss`. *@active_start_time_of_day* is **int**, with a default of `0`.

#### [ @active_end_time_of_day = ] *active_end_time_of_day*

The time of day when the Distribution Agent stops being scheduled, formatted as `HHmmss`. *@active_end_time_of_day* is **int**, with a default of `235959`.

#### [ @active_start_date = ] *active_start_date*

The date when the Distribution Agent is first scheduled, formatted as `yyyyMMdd`. *@active_start_date* is **int**, with a default of `0`.

#### [ @active_end_date = ] *active_end_date*

The date when the Distribution Agent stops being scheduled, formatted as `yyyyMMdd`. *@active_end_date* is **int**, with a default of `99991231`.

#### [ @dts_package_name = ] N'*dts_package_name*'

Specifies the name of the Data Transformation Services (DTS) package. *@dts_package_name* is **sysname**, with a default of `NULL`. For example, to specify a package name of `DTSPub_Package`, the parameter would be `@dts_package_name = N'DTSPub_Package'`.

#### [ @dts_package_password = ] N'*dts_package_password*'

Specifies the password required to run the package. *@dts_package_password* is **sysname**, with a default of `NULL`, which means the package has no password.

> [!NOTE]  
> You must specify a password if *@dts_package_name* is specified.

#### [ @dts_package_location = ] N'*dts_package_location*'

Specifies the package location. *@dts_package_location* is **nvarchar(12)**, with a default of `distributor`. The location of the package can be `distributor` or `subscriber`.

#### [ @enabled_for_syncmgr = ] N'*enabled_for_syncmgr*'

Specifies whether the subscription can be synchronized through [!INCLUDE [msCoName](../../includes/msconame-md.md)] Synchronization Manager. *@enabled_for_syncmgr* is **nvarchar(5)**, with a default of `false`.

- If `false`, the subscription isn't registered with Synchronization Manager.
- If `true`, the subscription is registered with Synchronization Manager and can be synchronized without starting [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

#### [ @distribution_job_name = ] N'*distribution_job_name*'

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `NULL`.

#### [ @subscriber_provider = ] N'*subscriber_provider*'

The unique programmatic identifier (PROGID) with which the OLE DB provider for the non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data source is registered. *@subscriber_provider* is **sysname**, with a default of `NULL`. *@subscriber_provider* must be unique for the OLE DB provider installed on the Distributor. *@subscriber_provider* is only supported for non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.

#### [ @subscriber_datasrc = ] N'*subscriber_datasrc*'

The name of the data source as understood by the OLE DB provider.*@subscriber_datasrc* is **nvarchar(4000)**, with a default of `NULL`. *@subscriber_datasrc* is passed as the `DBPROP_INIT_DATASOURCE` property to initialize the OLE DB provider. *@subscriber_datasrc* is only supported for non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.

#### [ @subscriber_location = ] N'*subscriber_location*'

The location of the database as understood by the OLE DB provider. *@subscriber_location* is **nvarchar(4000)**, with a default of `NULL`. *@subscriber_location* is passed as the `DBPROP_INIT_LOCATION` property to initialize the OLE DB provider. *@subscriber_location* is only supported for non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.

#### [ @subscriber_provider_string = ] N'*subscriber_provider_string*'

The OLE DB provider-specific connection string that identifies the data source. *@subscriber_provider_string* is **nvarchar(4000)**, with a default of `NULL`. *@subscriber_provider_string* is passed to IDataInitialize or set as the `DBPROP_INIT_PROVIDERSTRING` property to initialize the OLE DB provider. *@subscriber_provider_string* is only supported for non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.

#### [ @subscriber_catalog = ] N'*subscriber_catalog*'

The catalog to be used when making a connection to the OLE DB provider. *@subscriber_catalog* is **sysname**, with a default of `NULL`. *@subscriber_catalog* is passed as the `DBPROP_INIT_CATALOG` property to initialize the OLE DB provider. *@subscriber_catalog* is only supported for non- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addpushsubscription_agent` is used in snapshot replication and transactional replication.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-addpushsubscription-a_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_addpushsubscription_agent`.

## Related content

- [Create a push subscription](../replication/create-a-push-subscription.md)
- [Create a Subscription for a Non-SQL Server Subscriber](../replication/create-a-subscription-for-a-non-sql-server-subscriber.md)
- [Subscribe to Publications](../replication/subscribe-to-publications.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
- [sp_addsubscription (Transact-SQL)](sp-addsubscription-transact-sql.md)
- [sp_changesubscription (Transact-SQL)](sp-changesubscription-transact-sql.md)
- [sp_dropsubscription (Transact-SQL)](sp-dropsubscription-transact-sql.md)
- [sp_helpsubscription (Transact-SQL)](sp-helpsubscription-transact-sql.md)
