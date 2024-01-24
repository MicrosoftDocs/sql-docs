---
title: "sp_addqueued_artinfo (Transact-SQL)"
description: Creates the MSsubscription_articles table at the Subscriber that is used to track article subscription information.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addqueued_artinfo"
  - "sp_addqueued_artinfo_TSQL"
helpviewer_keywords:
  - "sp_addqueued_artinfo"
dev_langs:
  - "TSQL"
---
# sp_addqueued_artinfo (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> The [sp_script_synctran_commands](sp-script-synctran-commands-transact-sql.md) procedure should be used instead of `sp_addqueued_artinfo`. [sp_script_synctran_commands](sp-script-synctran-commands-transact-sql.md) generates a script that contains the `sp_addqueued_artinfo` and `sp_addsynctrigger` calls.

Creates the [MSsubscription_articles](../system-tables/mssubscription-articles-transact-sql.md) table at the Subscriber that is used to track article subscription information (queued, updating, and immediate updating with queued updating as failover). This stored procedure is executed at the Subscriber on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addqueued_artinfo
    [ @artid = ] artid
    , [ @article = ] N'article'
    , [ @publisher = ] N'publisher'
    , [ @publisher_db = ] N'publisher_db'
    , [ @publication = ] N'publication'
    , [ @dest_table = ] N'dest_table'
    , [ @owner = ] N'owner'
    , [ @cft_table = ] N'cft_table'
    [ , [ @columns = ] columns ]
[ ; ]
```

## Arguments

#### [ @artid = ] *artid*

The name of the article ID. *@artid* is **int**, with no default.

#### [ @article = ] N'*article*'

The name of the article to be scripted. *@article* is **sysname**, with no default.

#### [ @publisher = ] N'*publisher*'

The name of the Publisher server. *@publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the Publisher database. *@publisher_db* is **sysname**, with no default.

#### [ @publication = ] N'*publication*'

The name of the publication to be scripted. *@publication* is **sysname**, with no default.

#### [ @dest_table = ] N'*dest_table*'

The name of the destination table. *@dest_table* is **sysname**, with no default.

#### [ @owner = ] N'*owner*'

The owner of the subscription. *@owner* is **sysname**, with no default.

#### [ @cft_table = ] N'*cft_table*'

Name of the queued updating conflict table for this article. *@cft_table* is **sysname**, with no default.

#### [ @columns = ] *columns*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addqueued_artinfo` is used by the Distribution Agent as part of subscription initialization. This stored procedure isn't commonly run by users, but might be useful if you need to manually set up a subscription.

Use [sp_script_synctran_commands](sp-script-synctran-commands-transact-sql.md) instead of `sp_addqueued_artinfo`.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_addqueued_artinfo`.

## Related content

- [Updatable Subscriptions - For Transactional Replication](../replication/transactional/updatable-subscriptions-for-transactional-replication.md)
- [sp_script_synctran_commands (Transact-SQL)](sp-script-synctran-commands-transact-sql.md)
- [MSsubscription_articles (Transact-SQL)](../system-tables/mssubscription-articles-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
