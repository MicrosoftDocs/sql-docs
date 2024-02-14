---
title: "sp_addsubscription (Transact-SQL)"
description: Adds a subscription to a publication and sets the Subscriber status. This stored procedure runs at the Publisher on the publication database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/02/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addsubscription"
  - "sp_addsubscription_TSQL"
helpviewer_keywords:
  - "sp_addsubscription"
dev_langs:
  - "TSQL"
---
# sp_addsubscription (Transact-SQL)

[!INCLUDE [sql-asdb](../../includes/applies-to-version/sql-asdb.md)]

Adds a subscription to a publication and sets the Subscriber status. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addsubscription
    [ @publication = ] N'publication'
    [ , [ @article = ] N'article' ]
    [ , [ @subscriber = ] N'subscriber' ]
    [ , [ @destination_db = ] N'destination_db' ]
    [ , [ @sync_type = ] N'sync_type' ]
    [ , [ @status = ] N'status' ]
    [ , [ @subscription_type = ] N'subscription_type' ]
    [ , [ @update_mode = ] N'update_mode' ]
    [ , [ @loopback_detection = ] N'loopback_detection' ]
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
    [ , [ @reserved = ] N'reserved' ]
    [ , [ @enabled_for_syncmgr = ] N'enabled_for_syncmgr' ]
    [ , [ @offloadagent = ] offloadagent ]
    [ , [ @offloadserver = ] N'offloadserver' ]
    [ , [ @dts_package_name = ] N'dts_package_name' ]
    [ , [ @dts_package_password = ] N'dts_package_password' ]
    [ , [ @dts_package_location = ] N'dts_package_location' ]
    [ , [ @distribution_job_name = ] N'distribution_job_name' ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @backupdevicetype = ] N'backupdevicetype' ]
    [ , [ @backupdevicename = ] N'backupdevicename' ]
    [ , [ @mediapassword = ] N'mediapassword' ]
    [ , [ @password = ] N'password' ]
    [ , [ @fileidhint = ] fileidhint ]
    [ , [ @unload = ] unload ]
    [ , [ @subscriptionlsn = ] subscriptionlsn ]
    [ , [ @subscriptionstreams = ] subscriptionstreams ]
    [ , [ @subscriber_type = ] subscriber_type ]
    [ , [ @memory_optimized = ] memory_optimized ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The article to which the publication is subscribed. *@article* is **sysname**, with a default of `all`. If `all`, a subscription is added to all articles in that publication. Only values of `all` or `NULL` are supported for Oracle Publishers.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with a default of `NULL`.

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15"
[!INCLUDE [custom-port](includes/custom-port.md)]
::: moniker-end

#### [ @destination_db = ] N'*destination_db*'

The name of the destination database in which to place replicated data. *@destination_db* is **sysname**, with a default of `NULL`. When `NULL`, *@destination_db* is set to the name of the publication database. For Oracle Publishers, *@destination_db* must be specified. For a non-SQL Server Subscriber, specify a value of (default destination) for *@destination_db*.

#### [ @sync_type = ] N'*sync_type*'

The subscription synchronization type. *@sync_type* is **nvarchar(255)**, and can be one of the following values:

| Value | Description |
| --- | --- |
| `none` <sup>1</sup> | Subscriber already has the schema and initial data for published tables. |
| `automatic` (default) | Schema and initial data for published tables are transferred to the Subscriber first. |
| `replication support only` <sup>2</sup> | Provides automatic generation at the Subscriber of article custom stored procedures and triggers that support updating subscriptions, if appropriate. Assumes that the Subscriber already has the schema and initial data for published tables. When configuring a peer-to-peer transactional replication topology, ensure that the data at all nodes in the topology is identical. For more information, see [Peer-to-Peer - Transactional Replication](../replication/transactional/peer-to-peer-transactional-replication.md). |
| `initialize with backup` <sup>2</sup> | Schema and initial data for published tables are obtained from a backup of the publication database. Assumes that the Subscriber has access to a backup of the publication database. The location of the backup and media type for the backup are specified by *@backupdevicename* and *@backupdevicetype*. When using this option, a peer-to-peer transactional replication topology need not be quiesced during configuration. |
| `initialize from lsn` | Used when you're adding a node to a peer-to-peer transactional replication topology. Used with @subscriptionlsn to make sure that all relevant transactions are replicated to the new node. Assumes that the Subscriber already has the schema and initial data for published tables. For more information, see [Peer-to-Peer - Transactional Replication](../replication/transactional/peer-to-peer-transactional-replication.md). |

<sup>1</sup> This option has been deprecated. Use replication support only instead.

<sup>2</sup> Not supported for subscriptions to non-[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] publications.

> [!NOTE]  
> System tables and data are always transferred.

#### [ @status = ] N'*status*'

The subscription status. *@status* is **sysname**, with a default of `NULL`. When this parameter isn't explicitly set, replication automatically sets it to one of these values.

| Value | Description |
| --- | --- |
| `active` | Subscription is initialized and ready to accept changes. This option is set when the value of *@sync_type* is none, initialize with backup, or replication support only. |
| `subscribed` | Subscription needs to be initialized. This option is set when the value of *@sync_type* is automatic. |

#### [ @subscription_type = ] N'*subscription_type*'

The type of subscription. *@subscription_type* is **nvarchar(4)**, with a default of `push`. Can be `push` or `pull`. The Distribution Agents of push subscriptions reside at the Distributor, and the Distribution Agents of pull subscriptions reside at the Subscriber. *@subscription_type* can be `pull` to create a named pull subscription that is known to the Publisher. For more information, see [Subscribe to Publications](../replication/subscribe-to-publications.md).

> [!NOTE]  
> Anonymous subscriptions don't need to use this stored procedure.

#### [ @update_mode = ] N'*update_mode*'

The type of update. *@update_mode* is **nvarchar(30)**, and can be one of these values.

| Value | Description |
| --- | --- |
| `read only` (default) | The subscription is read-only. The changes at the Subscriber aren't sent to the Publisher. |
| `sync tran` | Enables support for immediate updating subscriptions. Not supported for Oracle Publishers. |
| `queued tran` | Enables the subscription for queued updating. Data modifications can be made at the Subscriber, stored in a queue, and then propagated to the Publisher. Not supported for Oracle Publishers. |
| `failover` | Enables the subscription for immediate updating with queued updating as a failover. Data modifications can be made at the Subscriber and propagated to the Publisher immediately. If the Publisher and Subscriber aren't connected, the updating mode can be changed so that data modifications made at the Subscriber are stored in a queue until the Subscriber and Publisher are reconnected. Not supported for Oracle Publishers. |
| `queued failover` | Enables the subscription as a queued updating subscription with the ability to change to immediate updating mode. Data modifications can be made at the Subscriber and stored in a queue until a connection is established between the Subscriber and Publisher. When a continuous connection is established the updating mode can be changed to immediate updating. Not supported for Oracle Publishers. |

The values `sync tran` and `queued tran` aren't allowed if the publication being subscribed to allows DTS.

#### [ @loopback_detection = ] N'*loopback_detection*'

Specifies if the Distribution Agent sends transactions that originated at the Subscriber back to the Subscriber. *@loopback_detection* is **nvarchar(5)**, and can be one of these values.

| Value | Description |
| --- | --- |
| `true` | Distribution Agent doesn't send transactions originated at the Subscriber back to the Subscriber. Used with bidirectional transactional replication. For more information, see [Bidirectional Transactional Replication](../replication/transactional/bidirectional-transactional-replication.md). |
| `false` | Distribution Agent sends transactions that originated at the Subscriber back to the Subscriber. |
| `NULL` (default) | Automatically set to true for a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscriber and false for a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscriber. |

#### [ @frequency_type = ] *frequency_type*

The frequency with which to schedule the distribution task. *@frequency_type* is **int**, and can be one of these values.

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

#### [ @frequency_interval = ] *frequency_interval*

The value to apply to the frequency set by *@frequency_type*. *@frequency_interval* is **int**, with a default of `NULL`.

#### [ @frequency_relative_interval = ] *frequency_relative_interval*

The date of the Distribution Agent. This parameter is used when *@frequency_type* is set to `32` (monthly relative). *@frequency_relative_interval* is **int**, and can be one of these values.

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

How often, in minutes, to reschedule during the defined period. *@frequency_subday* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` | Once |
| `2` | Second |
| `4` | Minute |
| `8` | Hour |
| `NULL` | |

#### [ @frequency_subday_interval = ] *frequency_subday_interval*

The interval for *@frequency_subday*. *@frequency_subday_interval* is **int**, with a default of `NULL`.

#### [ @active_start_time_of_day = ] *active_start_time_of_day*

The time of day when the Distribution Agent is first scheduled, formatted as `HHmmss`. *@active_start_time_of_day* is **int**, with a default of `NULL`.

#### [ @active_end_time_of_day = ] *active_end_time_of_day*

The time of day when the Distribution Agent stops being scheduled, formatted as `HHmmss`. *@active_end_time_of_day* is **int**, with a default of `NULL`.

#### [ @active_start_date = ] *active_start_date*

The date when the Distribution Agent is first scheduled, formatted as `yyyyMMdd`. *@active_start_date* is **int**, with a default of `NULL`.

#### [ @active_end_date = ] *active_end_date*

The date when the Distribution Agent stops being scheduled, formatted as `yyyyMMdd`. *@active_end_date* is **int**, with a default of `NULL`.

#### [ @optional_command_line = ] N'*optional_command_line*'

The optional command prompt to execute. *@optional_command_line* is **nvarchar(4000)**, with a default of `NULL`.

#### [ @reserved = ] N'*reserved*'

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @enabled_for_syncmgr = ] N'*enabled_for_syncmgr*'

Whether the subscription can be synchronized through Windows Synchronization Manager. *@enabled_for_syncmgr* is **nvarchar(5)**, with a default of `NULL`, which is the same as `false`. If `false`, the subscription isn't registered with Windows Synchronization Manager. If `true`, the subscription is registered with Windows Synchronization Manager and can be synchronized without starting [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Not supported for Oracle Publishers.

#### [ @offloadagent = ] *offloadagent*

Specifies that the agent can be activated remotely. *@offloadagent* is **bit**, with a default of `0`.

> [!NOTE]  
> This parameter has been deprecated and is only maintained for backward compatibility of scripts.

#### [ @offloadserver = ] N'*offloadserver*'

Specifies the network name of server to be used for remote activation. *@offloadserver* is **sysname**, with a default of `NULL`.

#### [ @dts_package_name = ] N'*dts_package_name*'

Specifies the name of the Data Transformation Services (DTS) package. *@dts_package_name* is **sysname**, with a default of `NULL`. For example, to specify a package of `DTSPub_Package`, the parameter would be `@dts_package_name = N'DTSPub_Package'`. This parameter is available for push subscriptions. To add DTS package information to a pull subscription, use `sp_addpullsubscription_agent`.

#### [ @dts_package_password = ] N'*dts_package_password*'

Specifies the password on the package, if there's one. *@dts_package_password* is **sysname**, with a default of `NULL`.

> [!NOTE]  
> You must specify a password if *@dts_package_name* is specified.

#### [ @dts_package_location = ] N'*dts_package_location*'

Specifies the package location. *@dts_package_location* is **nvarchar(12)**, with a default of `NULL`, which is the same as `distributor`. The location of the package can be `distributor` or `subscriber`.

#### [ @distribution_job_name = ] N'*distribution_job_name*'

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *@publisher* is **sysname**, with a default of `NULL`.

> [!NOTE]  
> *@publisher* shouldn't be specified for a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

#### [ @backupdevicetype = ] N'*backupdevicetype*'

Specifies the type of backup device used when initializing a Subscriber from a backup. *@backupdevicetype* is **nvarchar(20)**, and can be one of these values:

| Value | Description |
| --- | --- |
| `logical` (default) | The backup device is a logical device |
| `disk` | The backup device is disk drive |
| `tape` | The backup device is a tape drive |

*@backupdevicetype* is only used when *@sync_method* is set to initialize_with_backup.

#### [ @backupdevicename = ] N'*backupdevicename*'

Specifies the name of the device used when initializing a Subscriber from a backup. *@backupdevicename* is **nvarchar(1000)**, with a default of `NULL`.

#### [ @mediapassword = ] N'*mediapassword*'

Specifies a password for the media set if a password was set when the media was formatted. *@mediapassword* is **sysname**, with a default of `NULL`.

> [!NOTE]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

#### [ @password = ] N'*password*'

Specifies a password for the backup if a password was set when the backup was created. *@password* is **sysname**, with a default of `NULL`.

#### [ @fileidhint = ] *fileidhint*

Identifies an ordinal value of the backup set to be restored. *@fileidhint* is **int**, with a default of `NULL`.

#### [ @unload = ] *unload*

Specifies if a tape backup device should be unloaded after the initialization from back is complete. *@unload* is **bit**, with a default of `1`, which specifies that the tape should be unloaded. *@unload* is only used when *@backupdevicetype* is `tape`.

#### [ @subscriptionlsn = ] *subscriptionlsn*

Specifies the log sequence number (LSN) at which a subscription should start delivering changes to a node in a peer-to-peer transactional replication topology. *@subscriptionlsn* is **binary(10)**, with a default of `NULL`. Used with a *@sync_type* value of `initialize from lsn` to make sure that all relevant transactions are replicated to a new node. For more information, see [Peer-to-Peer - Transactional Replication](../replication/transactional/peer-to-peer-transactional-replication.md).

#### [ @subscriptionstreams = ] *subscriptionstreams*

The number of connections allowed per Distribution Agent to apply batches of changes in parallel to a Subscriber, while maintaining many of the transactional characteristics present when using a single thread. *@subscriptionstreams* is **tinyint**, with a default of `NULL`. A range of values from `1` to `64` is supported. This parameter isn't supported for non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers, Oracle Publishers or peer-to-peer subscriptions. Whenever *@subscriptionstreams* is used, additional rows are added in the `msreplication_subscriptions` table (one row per stream) with an `agent_id` set to `NULL`.

> [!NOTE]  
> Subscription streams don't work for articles configured to deliver [!INCLUDE [tsql](../../includes/tsql-md.md)]. To use subscription streams, configure articles to deliver stored procedure calls instead.

#### [ @subscriber_type = ] *subscriber_type*

The type of Subscriber. *@subscriber_type* is **tinyint**, and can be one of these values.

| Value | Description |
| --- | --- |
| `0` (default) | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscriber |
| `1` | ODBC data source server |
| `2` | [!INCLUDE [msCoName](../../includes/msconame-md.md)] Jet database |
| `3` | OLE DB provider |

#### [ @memory_optimized = ] *memory_optimized*

Indicates that the subscription supports memory optimized tables. *@memory_optimized* is **bit**, with a default of `0` (false). `1` (true) means that the subscription supports memory optimized tables.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addsubscription` is used in snapshot replication and transactional replication.

When `sp_addsubscription` is executed by a member of the **sysadmin** fixed server role to create a push subscription, the Distribution Agent job is implicitly created and runs under the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service account. We recommend that you execute [sp_addpushsubscription_agent](sp-addpushsubscription-agent-transact-sql.md) and specify the credentials of a different, agent-specific Windows account for @job_login and @job_password. For more information, see [Replication Agent Security Model](../replication/security/replication-agent-security-model.md).

`sp_addsubscription` prevents ODBC and OLE DB Subscribers access to publications that:

- Were created with the native *@sync_method* in the call to [sp_addpublication](sp-addpublication-transact-sql.md).

- Contain articles that were added to the publication with the [sp_addarticle](sp-addarticle-transact-sql.md) stored procedure that had a *@pre_creation_cmd* parameter value of 3 (truncate).

- Attempt to set *@update_mode* to `sync tran`.

- Have an article configured to use parameterized statements.

In addition, if a publication has the *@allow_queued_tran* option set to true (which enables queuing of changes at the Subscriber until they can be applied at the Publisher), the timestamp column in an article is scripted out as **timestamp**, and changes on that column are sent to the Subscriber. The Subscriber generates and updates the timestamp column value. For an ODBC or OLE DB Subscriber, `sp_addsubscription` fails if an attempt is made to subscribe to a publication that has *@allow_queued_tran* set to true and articles with timestamp columns in it.

If a subscription doesn't use a DTS package, it can't subscribe to a publication that is set to *@allow_transformable_subscriptions*. If the table from the publication needs to be replicated to both a DTS subscription and non-DTS subscription, two separate publications have to be created: one for each type of subscription.

When selecting the **sync_type** options `replication support only`, `initialize with backup`, or `initialize from lsn`, the log reader agent must run after executing `sp_addsubscription`, so that the set-up scripts are written to the distribution database. The log reader agent must be running under an account that is a member of the **sysadmin** fixed server role. When the *@sync_type* option is set to `Automatic`, no special log reader agent actions are required.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_addsubscription`. For pull subscriptions, users with logins in the publication access list can execute `sp_addsubscription`.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-addsubscription-trans_1.sql":::

## Related content

- [Create a push subscription](../replication/create-a-push-subscription.md)
- [Create a Subscription for a Non-SQL Server Subscriber](../replication/create-a-subscription-for-a-non-sql-server-subscriber.md)
- [Subscribe to Publications](../replication/subscribe-to-publications.md)
- [sp_addpushsubscription_agent (Transact-SQL)](sp-addpushsubscription-agent-transact-sql.md)
- [sp_changesubstatus (Transact-SQL)](sp-changesubstatus-transact-sql.md)
- [sp_dropsubscription (Transact-SQL)](sp-dropsubscription-transact-sql.md)
- [sp_helpsubscription (Transact-SQL)](sp-helpsubscription-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
