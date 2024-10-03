---
title: "sp_reinitsubscription (Transact-SQL)"
description: sp_reinitsubscription marks the subscription for reinitialization.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_reinitsubscription"
  - "sp_reinitsubscription_TSQL"
helpviewer_keywords:
  - "sp_reinitsubscription"
dev_langs:
  - "TSQL"
---
# sp_reinitsubscription (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Marks the subscription for reinitialization. This stored procedure is executed at the Publisher for push subscriptions.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_reinitsubscription
    [ [ @publication = ] N'publication' ]
    [ , [ @article = ] N'article' ]
    , [ @subscriber = ] N'subscriber'
    [ , [ @destination_db = ] N'destination_db' ]
    [ , [ @for_schema_change = ] for_schema_change ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @ignore_distributor_failure = ] ignore_distributor_failure ]
    [ , [ @invalidate_snapshot = ] invalidate_snapshot ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `all`.

#### [ @article = ] N'*article*'

The name of the article. *@article* is **sysname**, with a default of `all`. For an immediate-updating publication, *@article* must be `all`. Otherwise, the stored procedure skips the publication and reports an error.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with no default.

#### [ @destination_db = ] N'*destination_db*'

The name of the destination database. *@destination_db* is **sysname**, with a default of `all`.

#### [ @for_schema_change = ] *for_schema_change*

Indicates whether reinitialization occurs as a result of a schema change at the publication database. *@for_schema_change* is **bit**, with a default of `0`.

- If `0`, active subscriptions for publications that allow immediate updating are reactivated as long as the whole publication, and not just some of its articles, are reinitialized. This means that the reinitialization is being initiated as a result of schema changes.

- If `1`, active subscriptions aren't reactivated until the Snapshot Agent runs.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be used for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers.

#### [ @ignore_distributor_failure = ] *ignore_distributor_failure*

Allows reinitialization even if the Distributor doesn't exist or is offline. *@ignore_distributor_failure* is **bit**, with a default of `0`. If `0`, reinitialization fails if the Distributor doesn't exist or is offline.

#### [ @invalidate_snapshot = ] *invalidate_snapshot*

Invalidates the existing publication snapshot. *@invalidate_snapshot* is **bit**, with a default of `0`. If `1`, a new snapshot is generated for the publication.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_reinitsubscription` is used in transactional replication.

`sp_reinitsubscription` isn't supported for peer-to-peer transactional replication.

For subscriptions where the initial snapshot is applied automatically and where the publication doesn't allow updatable subscriptions, the Snapshot Agent must be run after this stored procedure is executed so that schema and bulk copy program files are prepared and the Distribution Agents is then able to resynchronize the subscriptions.

For subscriptions where the initial snapshot is applied automatically, and the publication allows updatable subscriptions, the Distribution Agent resynchronizes the subscription using the most recent schema and bulk copy program files previously created by the Snapshot Agent. The Distribution Agent resynchronizes the subscription immediately after the user executes `sp_reinitsubscription`, if the Distribution Agent isn't busy. Otherwise, synchronization might occur after the message interval (specified by Distribution Agent command-prompt parameter `MessageInterval`).

`sp_reinitsubscription` has no effect on subscriptions where the initial snapshot is applied manually.

To resynchronize anonymous subscriptions to a publication, pass in `all` or `NULL` as *@subscriber*.

Transactional replication supports subscription reinitialization at the article level. The snapshot of the article is reapplied at the Subscriber during the next synchronization after the article is marked for reinitialization. However, if there are dependent articles that are also subscribed to by the same Subscriber, reapplying the snapshot on the article might fail unless dependent articles in the publication are also automatically reinitialized under certain circumstances:

- If the precreation command on the article is `drop`, articles for schema-bound views and schema-bound stored procedures on the base object of that article is marked for reinitialization as well.

- If the schema option on the article includes scripting of declared referential integrity on the primary keys, articles that have base tables, with foreign key relationships to base tables of the reinitialized article, are marked for reinitialization as well.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-reinitsubscription-tr_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role, members of the **db_owner** fixed database role, or the creator of the subscription can execute `sp_reinitsubscription`.

## Related content

- [Reinitialize a Subscription](../replication/reinitialize-a-subscription.md)
- [Reinitialize Subscriptions](../replication/reinitialize-subscriptions.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
