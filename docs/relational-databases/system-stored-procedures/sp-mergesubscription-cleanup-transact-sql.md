---
title: "sp_mergesubscription_cleanup (Transact-SQL)"
description: "Removes metadata, such as triggers and entries, in sysmergesubscriptions and sysmergearticles."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_mergesubscription_cleanup"
  - "sp_mergesubscription_cleanup_TSQL"
helpviewer_keywords:
  - "sp_mergesubscription_cleanup"
dev_langs:
  - "TSQL"
---
# sp_mergesubscription_cleanup (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes metadata, such as triggers and entries, in `sysmergesubscriptions` and `sysmergearticles` after the specified merge push subscription is removed at the Publisher. This stored procedure is run at the Subscriber on the subscription database.

> [!NOTE]  
> For a pull subscription, metadata is removed when [sp_dropmergepullsubscription (Transact-SQL)](sp-dropmergepullsubscription-transact-sql.md) is executed.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_mergesubscription_cleanup
    [ @publisher = ] N'publisher'
    , [ @publisher_db = ] N'publisher_db'
    , [ @publication = ] N'publication'
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the Publisher database. *@publisher_db* is **sysname**, with no default.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_mergesubscription_cleanup` is used in merge replication.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_mergesubscription_cleanup`.

## Related content

- [Delete a Push Subscription](../replication/delete-a-push-subscription.md)
- [sp_expired_subscription_cleanup (Transact-SQL)](sp-expired-subscription-cleanup-transact-sql.md)
- [sp_subscription_cleanup (Transact-SQL)](sp-subscription-cleanup-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
