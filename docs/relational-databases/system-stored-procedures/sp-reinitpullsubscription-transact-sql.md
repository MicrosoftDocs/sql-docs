---
title: "sp_reinitpullsubscription (Transact-SQL)"
description: sp_reinitpullsubscription marks a transactional pull or anonymous subscription for reinitialization the next time the Distribution Agent runs.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_reinitpullsubscription_TSQL"
  - "sp_reinitpullsubscription"
helpviewer_keywords:
  - "sp_reinitpullsubscription"
dev_langs:
  - "TSQL"
---
# sp_reinitpullsubscription (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Marks a transactional pull or anonymous subscription for reinitialization the next time the Distribution Agent runs. This stored procedure is executed at the Subscriber on the pull subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_reinitpullsubscription
    [ @publisher = ] N'publisher'
    [ , [ @publisher_db = ] N'publisher_db' ]
    [ , [ @publication = ] N'publication' ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the Publisher database. *@publisher_db* is **sysname**, with a default of `NULL`.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `all`, which marks all subscriptions for reinitialization.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_reinitpullsubscription` is used in transactional replication.

`sp_reinitpullsubscription` isn't supported for peer-to-peer transactional replication.

`sp_reinitpullsubscription` can be called from the Subscriber to reinitialize the subscription, during the next run of the Distribution Agent.

Subscriptions to publications created with a value of `false` for *@immediate_sync* can't be reinitialized from the Subscriber.

You can reinitialize a pull subscription by either executing `sp_reinitpullsubscription` at the Subscriber or `sp_reinitsubscription` at the Publisher.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-reinitpullsubscriptio_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_reinitpullsubscription`.

## Related content

- [Reinitialize a Subscription](../replication/reinitialize-a-subscription.md)
- [Reinitialize Subscriptions](../replication/reinitialize-subscriptions.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
