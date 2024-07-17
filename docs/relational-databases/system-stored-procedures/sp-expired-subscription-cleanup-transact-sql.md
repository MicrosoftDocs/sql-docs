---
title: "sp_expired_subscription_cleanup (Transact-SQL)"
description: sp_expired_subscription_cleanup checks the status of all the subscriptions of every publication and drops expired subscriptions.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_expired_subscription_cleanup"
  - "SP_EXPIRED_SUBSCRIPTION_CLEANUP_TSQL"
helpviewer_keywords:
  - "sp_expired_subscription_cleanup"
dev_langs:
  - "TSQL"
---
# sp_expired_subscription_cleanup (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Checks the status of all the subscriptions of every publication and drops subscriptions that are expired. This stored procedure is executed at the Publisher on any database, or at the Distributor on the distribution database for a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_expired_subscription_cleanup [ [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] publisher. *@publisher* is **sysname**, with a default of `NULL`. You shouldn't specify this parameter for a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_expired_subscription_cleanup` is used in all types of replication.

The Expired Subscription Clean Up job runs `sp_expired_subscription_cleanup` to detect and remove expired subscriptions from publication databases every 24 hours. If any of the subscriptions are out-of-date, that is, aren't synchronized with the Publisher within the retention period, the publication is declared expired, and the traces of the subscription are cleaned up at the Publisher. For more information, see [Subscription Expiration and Deactivation](../replication/subscription-expiration-and-deactivation.md).

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_expired_subscription_cleanup`.

## Related content

- [sp_mergesubscription_cleanup (Transact-SQL)](sp-mergesubscription-cleanup-transact-sql.md)
- [sp_subscription_cleanup (Transact-SQL)](sp-subscription-cleanup-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
