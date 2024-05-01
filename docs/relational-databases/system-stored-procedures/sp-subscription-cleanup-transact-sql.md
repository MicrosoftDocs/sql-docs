---
title: "sp_subscription_cleanup (Transact-SQL)"
description: sp_subscription_cleanup removes metadata when a subscription is dropped at a Subscriber.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_subscription_cleanup_TSQL"
  - "sp_subscription_cleanup"
helpviewer_keywords:
  - "sp_subscription_cleanup"
dev_langs:
  - "TSQL"
---
# sp_subscription_cleanup (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes metadata when a subscription is dropped at a Subscriber. For a synchronizing transaction subscription, it also includes immediate-updating triggers. This stored procedure is executed at the Subscriber on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_subscription_cleanup
    [ @publisher = ] N'publisher'
    [ , [ @publisher_db = ] N'publisher_db' ]
    [ , [ @publication = ] N'publication' ]
    [ , [ @reserved = ] N'reserved' ]
    [ , [ @from_backup = ] from_backup ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the Publisher database. *@publisher_db* is **sysname**, with a default of `NULL`.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `NULL`. If `NULL`, subscriptions using a shared agent publication in the publishing database are deleted.

#### [ @reserved = ] N'*reserved*'

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @from_backup = ] *from_backup*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_subscription_cleanup` is used in transactional and snapshot replication.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_subscription_cleanup`.

## Related content

- [sp_expired_subscription_cleanup (Transact-SQL)](sp-expired-subscription-cleanup-transact-sql.md)
- [sp_mergesubscription_cleanup (Transact-SQL)](sp-mergesubscription-cleanup-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
