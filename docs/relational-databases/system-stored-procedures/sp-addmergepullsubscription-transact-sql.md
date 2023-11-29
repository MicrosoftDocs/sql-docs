---
title: "sp_addmergepullsubscription (Transact-SQL)"
description: Adds a pull subscription to a merge publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addmergepullsubscription_TSQL"
  - "sp_addmergepullsubscription"
helpviewer_keywords:
  - "sp_addmergepullsubscription"
dev_langs:
  - "TSQL"
---
# sp_addmergepullsubscription (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Adds a pull subscription to a merge publication. This stored procedure is executed at the Subscriber on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addmergepullsubscription
    [ @publication = ] N'publication'
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @publisher_db = ] N'publisher_db' ]
    [ , [ @subscriber_type = ] N'subscriber_type' ]
    [ , [ @subscription_priority = ] subscription_priority ]
    [ , [ @sync_type = ] N'sync_type' ]
    [ , [ @description = ] N'description' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of the local server name. The Publisher must be a valid server.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the Publisher database. *@publisher_db* is **sysname**, with a default of `NULL`.

#### [ @subscriber_type = ] N'*subscriber_type*'

The type of Subscriber. *@subscriber_type* is **nvarchar(15)**, with a default of `local`, and can be one of `global`, `local`, or `anonymous`. In [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] and later versions, *local* subscriptions are referred to as *client* subscriptions, and *global* subscriptions are referred to as *server* subscriptions.

#### [ @subscription_priority = ] *subscription_priority*

The subscription priority. *@subscription_priority* is **real**, with a default of `NULL`. For local and anonymous subscriptions, the priority is `0.0`. The priority is used by the default resolver to pick a winner when conflicts are detected. For global subscribers, the subscription priority must be less than `100`, which is the priority of the publisher.

#### [ @sync_type = ] N'*sync_type*'

The subscription synchronization type. *@sync_type* is **nvarchar(15)**, with a default of `automatic`. Can be `automatic` or `none`. If `automatic`, the schema and initial data for published tables are transferred to the Subscriber first. If `none`, the Subscriber is assumed to already have the schema and initial data for published tables. System tables and data are always transferred.

We recommend specifying a value of `automatic`.

#### [ @description = ] N'*description*'

A brief description of this pull subscription. *@description* is **nvarchar(255)**, with a default of `NULL`. This value is displayed by the Replication Monitor in the `Friendly Name` column, which can be used to sort the subscriptions for a monitored publication.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addmergepullsubscription` is used for merge replication.

If using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent to synchronize the subscription, the [sp_addmergepullsubscription_agent](sp-addmergepullsubscription-agent-transact-sql.md) stored procedure must be run at the Subscriber to create an agent and job to synchronize with the Publication.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-addmergepullsubscript_0_1.sql":::

:::code language="sql" source="../replication/codesnippet/tsql/sp-addmergepullsubscript_0_2.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_addmergepullsubscription`.

## Related content

- [Create a Pull Subscription](../replication/create-a-pull-subscription.md)
- [Subscribe to Publications](../replication/subscribe-to-publications.md)
- [sp_addmergepullsubscription_agent (Transact-SQL)](sp-addmergepullsubscription-agent-transact-sql.md)
- [sp_changemergepullsubscription (Transact-SQL)](sp-changemergepullsubscription-transact-sql.md)
- [sp_dropmergepullsubscription (Transact-SQL)](sp-dropmergepullsubscription-transact-sql.md)
- [sp_helpmergepullsubscription (Transact-SQL)](sp-helpmergepullsubscription-transact-sql.md)
- [sp_helpsubscription_properties (Transact-SQL)](sp-helpsubscription-properties-transact-sql.md)
