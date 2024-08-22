---
title: "sp_marksubscriptionvalidation (Transact-SQL)"
description: sp_marksubscriptionvalidation marks the current open transaction to be a subscription-level validation transaction for the specified subscriber.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_marksubscriptionvalidation"
  - "sp_marksubscriptionvalidation_TSQL"
helpviewer_keywords:
  - "sp_marksubscriptionvalidation"
dev_langs:
  - "TSQL"
---
# sp_marksubscriptionvalidation (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Marks the current open transaction to be a subscription-level validation transaction for the specified subscriber. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_marksubscriptionvalidation
    [ @publication = ] N'publication'
    , [ @subscriber = ] N'subscriber'
    , [ @destination_db = ] N'destination_db'
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with no default.

#### [ @destination_db = ] N'*destination_db*'

The name of the destination database. *@destination_db* is **sysname**, with no default.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be used for a publication that belongs to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_marksubscriptionvalidation` is used in transactional replication.

`sp_marksubscriptionvalidation` doesn't support non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.

For non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers, you can't execute `sp_marksubscriptionvalidation` from within an explicit transaction. This is because explicit transactions aren't supported over the linked server connection used to access the Publisher.

`sp_marksubscriptionvalidation` must be used together with [sp_article_validation](sp-article-validation-transact-sql.md), specifying a value of `1` for *@subscription_level*, and can be used with other calls to `sp_marksubscriptionvalidation` to mark the current open transaction for other subscribers.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_marksubscriptionvalidation`.

## Examples

The following query can be applied to the publishing database to post subscription-level validation commands. These commands are picked up by the Distribution Agents of specified Subscribers. The first transaction validates article `art1`, while the second transaction validates `art2`. The calls to `sp_marksubscriptionvalidation` and [sp_article_validation](sp-article-validation-transact-sql.md) are encapsulated in a transaction. We recommend only one call to [sp_article_validation](sp-article-validation-transact-sql.md) per transaction. This is because [sp_article_validation](sp-article-validation-transact-sql.md) holds a shared table lock on the source table during the transaction. You should keep the transaction short to maximize concurrency.

```sql
BEGIN TRANSACTION;

EXEC sp_marksubscriptionvalidation @publication = 'pub1',
    @subscriber = 'Sub',
    @destination_db = 'SubDB';

EXEC sp_marksubscriptionvalidation @publication = 'pub1',
    @subscriber = 'Sub2',
    @destination_db = 'SubDB';

EXEC sp_article_validation @publication = 'pub1',
    @article = 'art1',
    @rowcount_only = 0,
    @full_or_fast = 0,
    @shutdown_agent = 0,
    @subscription_level = 1;

COMMIT TRANSACTION;

BEGIN TRANSACTION;

EXEC sp_marksubscriptionvalidation @publication = 'pub1',
    @subscriber = 'Sub',
    @destination_db = 'SubDB';

EXEC sp_marksubscriptionvalidation @publication = 'pub1',
    @subscriber = 'Sub2',
    @destination_db = 'SubDB';

EXEC sp_article_validation @publication = 'pub1',
    @article = 'art2',
    @rowcount_only = 0,
    @full_or_fast = 0,
    @shutdown_agent = 0,
    @subscription_level = 1;

COMMIT TRANSACTION;
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Validate Replicated Data](../replication/validate-data-at-the-subscriber.md)
