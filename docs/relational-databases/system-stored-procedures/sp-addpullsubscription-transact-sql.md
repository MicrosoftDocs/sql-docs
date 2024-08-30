---
title: "sp_addpullsubscription (Transact-SQL)"
description: Adds a pull subscription to a snapshot or transactional publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addpullsubscription"
  - "sp_addpullsubscription_TSQL"
helpviewer_keywords:
  - "sp_addpullsubscription"
dev_langs:
  - "TSQL"
---
# sp_addpullsubscription (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Adds a pull subscription to a snapshot or transactional publication. This stored procedure is executed at the Subscriber on the database where the pull subscription is to be created.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addpullsubscription
    [ @publisher = ] N'publisher'
    [ , [ @publisher_db = ] N'publisher_db' ]
    , [ @publication = ] N'publication'
    [ , [ @independent_agent = ] N'independent_agent' ]
    [ , [ @subscription_type = ] N'subscription_type' ]
    [ , [ @description = ] N'description' ]
    [ , [ @update_mode = ] N'update_mode' ]
    [ , [ @immediate_sync = ] immediate_sync ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with no default.

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 "
[!INCLUDE [custom-port](includes/custom-port.md)]
::: moniker-end

#### [ @publisher_db = ] N'*publisher_db*'

The name of the Publisher database. *@publisher_db* is **sysname**, with a default of `NULL`. *@publisher_db* is ignored by Oracle Publishers.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @independent_agent = ] N'*independent_agent*'

Specifies if there's a stand-alone Distribution Agent for this publication. *@independent_agent* is **nvarchar(5)**, with a default of `true`.

- If `true`, there's a stand-alone Distribution Agent for this publication.
- If `false`, there's one Distribution Agent for each Publisher database/Subscriber database pair.

*@independent_agent* is a property of the publication and must have the same value as the Publisher.

#### [ @subscription_type = ] N'*subscription_type*'

The type of subscription. *@subscription_type* is **nvarchar(9)**, with a default of `anonymous`. You must specify a value of `pull` for *@subscription_type*, unless you want to create a subscription without registering the subscription at the Publisher. In this case, you must specify a value of `anonymous`. This is necessary for cases in which you can't establish a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] connection to the Publisher during subscription configuration.

#### [ @description = ] N'*description*'

The description of the publication. *@description* is **nvarchar(100)**, with a default of `NULL`.

#### [ @update_mode = ] N'*update_mode*'

The type of update. *@update_mode* is **nvarchar(30)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `read only` (default) | The subscription is read-only. Any changes at the Subscriber aren't sent back to the Publisher. Should be used when updates aren't made at the Subscriber. |
| `synctran` | Enables support for immediate updating subscriptions. |
| `queued tran` | Enables the subscription for queued updating. Data modifications can be made at the Subscriber, stored in a queue, and then propagated to the Publisher. |
| `failover` | Enables the subscription for immediate updating with queued updating as a failover. Data modifications can be made at the Subscriber and propagated to the Publisher immediately. If the Publisher and Subscriber aren't connected, data modifications made at the Subscriber can be stored in a queue until the Subscriber and Publisher are reconnected. |
| `queued failover` | **Note:** Not supported for Oracle Publishers.<br /><br />Enables the subscription as a queued updating subscription with the ability to change to immediate updating mode. Data modifications can be made at the Subscriber and stored in a queue until a connection is established between the Subscriber and Publisher. When a continuous connection is established the updating mode can be changed to immediate updating. |

#### [ @immediate_sync = ] *immediate_sync*

Specifies whether the synchronization files are created or re-created each time the Snapshot Agent runs. *@immediate_sync* is **bit**, with a default of `1`, and must be set to the same value as *@immediate_sync* in [sp_addpublication](sp-addpublication-transact-sql.md). *@immediate_sync* is a property of the publication and must have the same value as the Publisher.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addpullsubscription` is used in snapshot replication and transactional replication.

> [!IMPORTANT]  
> For queued updating subscriptions, use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication for connections to Subscribers, and specify a different account for the connection to each Subscriber. When creating a pull subscription that supports queued updating, replication always sets the connection to use Windows Authentication (for pull subscriptions, replication can't access metadata at the Subscriber required to use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication). In this case, you should execute [sp_changesubscription](sp-changesubscription-transact-sql.md) to change the connection to use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication after the subscription is configured.

If the [MSreplication_subscriptions](../system-tables/msreplication-subscriptions-transact-sql.md) table doesn't exist at the Subscriber, `sp_addpullsubscription` creates it. It also adds a row to the [MSreplication_subscriptions](../system-tables/msreplication-subscriptions-transact-sql.md) table. For pull subscriptions, [sp_addsubscription](sp-addsubscription-transact-sql.md) should be called at the Publisher first.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-addpullsubscription-t_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_addpullsubscription`.

## Related content

- [Create a Pull Subscription](../replication/create-a-pull-subscription.md)
- [Create an Updatable Subscription to a Transactional Publication](../replication/publish/create-an-updatable-subscription-to-a-transactional-publication.md)
- [Subscribe to Publications](../replication/subscribe-to-publications.md)
- [sp_addpullsubscription_agent (Transact-SQL)](sp-addpullsubscription-agent-transact-sql.md)
- [sp_change_subscription_properties (Transact-SQL)](sp-change-subscription-properties-transact-sql.md)
- [sp_droppullsubscription (Transact-SQL)](sp-droppullsubscription-transact-sql.md)
- [sp_helppullsubscription (Transact-SQL)](sp-helppullsubscription-transact-sql.md)
- [sp_helpsubscription_properties (Transact-SQL)](sp-helpsubscription-properties-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
