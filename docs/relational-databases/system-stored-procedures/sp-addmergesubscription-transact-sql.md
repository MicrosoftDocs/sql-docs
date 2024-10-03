---
title: "sp_addmergesubscription (Transact-SQL)"
description: Creates a push or pull merge subscription, executed at the Publisher on the publication database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addmergesubscription_TSQL"
  - "sp_addmergesubscription"
helpviewer_keywords:
  - "sp_addmergesubscription"
dev_langs:
  - "TSQL"
---
# sp_addmergesubscription (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Creates a push or pull merge subscription. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addmergesubscription
    [ @publication = ] N'publication'
    [ , [ @subscriber = ] N'subscriber' ]
    [ , [ @subscriber_db = ] N'subscriber_db' ]
    [ , [ @subscription_type = ] N'subscription_type' ]
    [ , [ @subscriber_type = ] N'subscriber_type' ]
    [ , [ @subscription_priority = ] subscription_priority ]
    [ , [ @sync_type = ] N'sync_type' ]
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
    [ , [ @description = ] N'description' ]
    [ , [ @enabled_for_syncmgr = ] N'enabled_for_syncmgr' ]
    [ , [ @offloadagent = ] offloadagent ]
    [ , [ @offloadserver = ] N'offloadserver' ]
    [ , [ @use_interactive_resolver = ] N'use_interactive_resolver' ]
    [ , [ @merge_job_name = ] N'merge_job_name' ]
    [ , [ @hostname = ] N'hostname' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default. The publication must already exist.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with a default of `NULL`.

#### [ @subscriber_db = ] N'*subscriber_db*'

The name of the subscription database. *@subscriber_db* is **sysname**, with a default of `NULL`.

#### [ @subscription_type = ] N'*subscription_type*'

The type of subscription. *@subscription_type* is **nvarchar(15)**, with a default of `push`.

- If `push`, a push subscription is added and the Merge Agent is added at the Distributor.
- If `pull`, a pull subscription is added without adding a Merge Agent at the Distributor.

> [!NOTE]  
> Anonymous subscriptions don't need to use this stored procedure.

#### [ @subscriber_type = ] N'*subscriber_type*'

The type of Subscriber. *@subscriber_type* is **nvarchar(15)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `local` (default) | Subscriber known only to the Publisher. |
| `global` | Subscriber known to all servers. |

In [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] and later versions, local subscriptions are referred to as client subscriptions, and global subscriptions are referred to as server subscriptions.

#### [ @subscription_priority = ] *subscription_priority*

A number indicating the priority for the subscription. *@subscription_priority* is **real**, with a default of `NULL`. For local and anonymous subscriptions, the priority is `0.0`. For global subscriptions, the priority must be less than `100.0`.

#### [ @sync_type = ] N'*sync_type*'

The subscription synchronization type. *@sync_type* is **nvarchar(15)**, with a default of `automatic`.

- If `automatic`, the schema and initial data for published tables are transferred to the Subscriber first.
- If `none`, the Subscriber is assumed to already have the schema and initial data for published tables. System tables and data are always transferred.

> [!NOTE]  
> We recommend not specifying a value of `none`.

#### [ @frequency_type = ] *frequency_type*

A value indicating when the Merge Agent runs. *@frequency_type* is **int**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `1` | Once |
| `4` | Daily |
| `8` | Weekly |
| `10` | Monthly |
| `20` | Monthly, relative to the frequency interval |
| `40` | When [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent starts |
| `NULL` (default) | |

#### [ @frequency_interval = ] *frequency_interval*

The day or days that the Merge Agent runs. *@frequency_interval* is **int**, and can be one of the following values.

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

The scheduled merge occurrence of the frequency interval in each month. *@frequency_relative_interval* is **int**, and can be one of these values.

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

The unit for *@frequency_subday_interval*. *@frequency_subday* is **int**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `1` | Once |
| `2` | Second |
| `4` | Minute |
| `8` | Hour |
| `NULL` (default) | |

#### [ @frequency_subday_interval = ] *frequency_subday_interval*

The frequency for *@frequency_subday* to occur between each merge. *@frequency_subday_interval* is **int**, with a default of `NULL`.

#### [ @active_start_time_of_day = ] *active_start_time_of_day*

The time of day when the Merge Agent is first scheduled, formatted as `HHmmss`. *@active_start_time_of_day* is **int**, with a default of `NULL`.

#### [ @active_end_time_of_day = ] *active_end_time_of_day*

The time of day when the Merge Agent stops being scheduled, formatted as `HHmmss`. *@active_end_time_of_day* is **int**, with a default of `NULL`.

#### [ @active_start_date = ] *active_start_date*

The date when the Merge Agent is first scheduled, formatted as `yyyyMMdd`. *@active_start_date* is **int**, with a default of `NULL`.

#### [ @active_end_date = ] *active_end_date*

The date when the Merge Agent stops being scheduled, formatted as `yyyyMMdd`. *@active_end_date* is **int**, with a default of `NULL`.

#### [ @optional_command_line = ] N'*optional_command_line*'

The optional command prompt to execute. *@optional_command_line* is **nvarchar(4000)**, with a default of `NULL`. This parameter is used to add a command that captures the output and saves it to a file or to specify a configuration file or attribute.

#### [ @description = ] N'*description*'

A brief description of this merge subscription. *@description* is **nvarchar(255)**, with a default of `NULL`. This value is displayed by the Replication Monitor in the `Friendly Name` column, which can be used to sort the subscriptions for a monitored publication.

#### [ @enabled_for_syncmgr = ] N'*enabled_for_syncmgr*'

Specifies if the subscription can be synchronized through [!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows Synchronization Manager. *@enabled_for_syncmgr* is **nvarchar(5)**, with a default of `false`.

- If `false`, the subscription isn't registered with Synchronization Manager.
- If `true`, the subscription is registered with Synchronization Manager and can be synchronized without starting [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

#### [ @offloadagent = ] *offloadagent*

Specifies that the agent can be activated remotely. *@offloadagent* is **bit**, with a default of `0`.

[!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)]

#### [ @offloadserver = ] N'*offloadserver*'

Specifies the network name of server to be used for remote agent activation. *@offloadserver* is **sysname**, with a default of `NULL`.

#### [ @use_interactive_resolver = ] N'*use_interactive_resolver*'

Allows conflicts to be resolved interactively for all articles that allow interactive resolution. *@use_interactive_resolver* is **nvarchar(5)**, with a default of `false`.

#### [ @merge_job_name = ] N'*merge_job_name*'

This parameter is deprecated and can't be set. *@merge_job_name* is **sysname**, with a default of `NULL`.

#### [ @hostname = ] N'*hostname*'

Overrides the value returned by [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) when this function is used in the WHERE clause of a parameterized filter. *@hostname* is **sysname**, with a default of `NULL`.

> [!IMPORTANT]  
> For performance reasons, we recommend that you not apply functions to column names in parameterized row filter clauses, such as `LEFT([MyColumn]) = SUSER_SNAME()`. If you use [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) in a filter clause and override the HOST_NAME value, it might be necessary to convert data types using [CONVERT](../../t-sql/functions/cast-and-convert-transact-sql.md). For more information about best practices for this case, see the section "Overriding the HOST_NAME() Value" in the topic [Parameterized Filters - Parameterized Row Filters](../replication/merge/parameterized-filters-parameterized-row-filters.md).

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addmergesubscription` is used in merge replication.

When `sp_addmergesubscription` is executed by a member of the **sysadmin** fixed server role to create a push subscription, the Merge Agent job is implicitly created and runs under the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account. We recommend that you execute [sp_addmergepushsubscription_agent](sp-addmergepushsubscription-agent-transact-sql.md) and specify the credentials of a different, agent-specific Windows account for *@job_login* and *@job_password*. For more information, see [Replication Agent Security Model](../replication/security/replication-agent-security-model.md).

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-addmergesubscription-_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_addmergesubscription`.

## Related content

- [Create a push subscription](../replication/create-a-push-subscription.md)
- [Create a Pull Subscription](../replication/create-a-pull-subscription.md)
- [Advanced Merge Replication Conflict - Interactive Resolution](../replication/merge/advanced-merge-replication-conflict-interactive-resolution.md)
- [Subscribe to Publications](../replication/subscribe-to-publications.md)
- [sp_changemergesubscription (Transact-SQL)](sp-changemergesubscription-transact-sql.md)
- [sp_dropmergesubscription (Transact-SQL)](sp-dropmergesubscription-transact-sql.md)
- [sp_helpmergesubscription (Transact-SQL)](sp-helpmergesubscription-transact-sql.md)
