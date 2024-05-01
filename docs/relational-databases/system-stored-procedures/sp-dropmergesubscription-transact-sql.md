---
title: "sp_dropmergesubscription (Transact-SQL)"
description: "Drops a subscription to a merge publication and its associated Merge Agent."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_dropmergesubscription_TSQL"
  - "sp_dropmergesubscription"
helpviewer_keywords:
  - "sp_dropmergesubscription"
dev_langs:
  - "TSQL"
---
# sp_dropmergesubscription (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Drops a subscription to a merge publication and its associated Merge Agent. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropmergesubscription
    [ [ @publication = ] N'publication' ]
    [ , [ @subscriber = ] N'subscriber' ]
    [ , [ @subscriber_db = ] N'subscriber_db' ]
    [ , [ @subscription_type = ] N'subscription_type' ]
    [ , [ @ignore_distributor = ] ignore_distributor ]
    [ , [ @reserved = ] reserved ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The publication name. *@publication* is **sysname**, with a default of `NULL`. The publication must already exist and conform to the rules for [identifiers](../databases/database-identifiers.md).

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with a default of `NULL`.

#### [ @subscriber_db = ] N'*subscriber_db*'

The name of the subscription database. *@subscriber_db* is **sysname**, with a default of `NULL`.

#### [ @subscription_type = ] N'*subscription_type*'

The type of subscription. *@subscription_type* is **nvarchar(15)**, and can be one of these values.

| Value | Description |
| --- | --- |
| `all` | Push, pull, and anonymous subscriptions |
| `anonymous` | Anonymous subscription. |
| `push` | Push subscription. |
| `pull` | Pull subscription. |
| `both` (default) | Both push and pull subscriptions. |

#### [ @ignore_distributor = ] *ignore_distributor*

Indicates whether this stored procedure is executed without connecting to the Distributor. *@ignore_distributor* is **bit**, with a default of `0`. This parameter can be used to drop a subscription without doing cleanup tasks at the Distributor. It's also useful if you have to reinstall the Distributor.

#### [ @reserved = ] *reserved*

Reserved for future use. *@reserved* is **bit**, with a default of `0`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_dropmergesubscription` is used in merge replication.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-dropmergesubscription_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_dropmergesubscription`.

## Related content

- [Delete a Push Subscription](../replication/delete-a-push-subscription.md)
- [Delete a Pull Subscription](../replication/delete-a-pull-subscription.md)
- [sp_addmergesubscription (Transact-SQL)](sp-addmergesubscription-transact-sql.md)
- [sp_changemergesubscription (Transact-SQL)](sp-changemergesubscription-transact-sql.md)
- [sp_helpmergesubscription (Transact-SQL)](sp-helpmergesubscription-transact-sql.md)
