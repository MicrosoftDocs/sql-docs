---
title: "sp_dropsubscription (Transact-SQL)"
description: Drops subscriptions to an article, publication, or subscriptions on the Publisher. This stored procedure runs at the Publisher on the publication database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_dropsubscription"
  - "sp_dropsubscription_TSQL"
helpviewer_keywords:
  - "sp_dropsubscription"
dev_langs:
  - "TSQL"
---
# sp_dropsubscription (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Drops subscriptions to a particular article, publication, or set of subscriptions on the Publisher. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropsubscription
    [ [ @publication = ] N'publication' ]
    [ , [ @article = ] N'article' ]
    , [ @subscriber = ] N'subscriber'
    [ , [ @destination_db = ] N'destination_db' ]
    [ , [ @ignore_distributor = ] ignore_distributor ]
    [ , [ @reserved = ] N'reserved' ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the associated publication. *@publication* is **sysname**, with a default of `NULL`. If `all`, all subscriptions for all publications for the specified Subscriber are canceled. *publication* is a required parameter.

#### [ @article = ] N'*article*'

The name of the article. *@article* is **sysname**, with a default of `NULL`. If `all`, subscriptions to all articles for each specified publication and Subscriber are dropped. Use `all` for publications that allow immediate updating.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber that will have its subscriptions dropped. *@subscriber* is **sysname**, with no default. If `all`, all subscriptions for all Subscribers are dropped.

#### [ @destination_db = ] N'*destination_db*'

The name of the destination database. *@destination_db* is **sysname**, with a default of `NULL`. If `NULL`, all the subscriptions from that Subscriber are dropped.

#### [ @ignore_distributor = ] *ignore_distributor*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @reserved = ] N'*reserved*'

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @publisher = ] N'*publisher*'

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_dropsubscription` is used in snapshot and transactional replication.

If you drop the subscription on an article in an immediate-sync publication, you can't add it back unless you drop the subscriptions on all the articles in the publication and add them all back at once.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-dropsubscription-tran_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the user that created the subscription can execute `sp_dropsubscription`.

## Related content

- [Delete a Push Subscription](../replication/delete-a-push-subscription.md)
- [sp_addsubscription (Transact-SQL)](sp-addsubscription-transact-sql.md)
- [sp_changesubstatus (Transact-SQL)](sp-changesubstatus-transact-sql.md)
- [sp_helpsubscription (Transact-SQL)](sp-helpsubscription-transact-sql.md)
