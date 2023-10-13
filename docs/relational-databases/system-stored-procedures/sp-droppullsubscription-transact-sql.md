---
title: "sp_droppullsubscription (Transact-SQL)"
description: sp_droppullsubscription drops a subscription at the current database of the Subscriber.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_droppullsubscription"
  - "sp_droppullsubscription_TSQL"
helpviewer_keywords:
  - "sp_droppullsubscription"
dev_langs:
  - "TSQL"
---
# sp_droppullsubscription (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Drops a subscription at the current database of the Subscriber. This stored procedure is executed at the Subscriber on the pull subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_droppullsubscription
    [ @publisher = ] N'publisher'
    [ , [ @publisher_db = ] N'publisher_db' ]
    , [ @publication = ] N'publication'
    [ , [ @reserved = ] reserved ]
    [ , [ @from_backup = ] from_backup ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The remote server name. *@publisher* is **sysname**, with no default. If `all`, the subscription is dropped at all the Publishers.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the Publisher database. *@publisher_db* is **sysname**, with a default of `NULL`. `all` means all the Publisher databases.

#### [ @publication = ] N'*publication*'

The publication name. *@publication* is **sysname**, with no default. If `all`, the subscription is dropped to all the publications.

#### [ @reserved = ] *reserved*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @from_backup = ] *from_backup*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_droppullsubscription` is used in snapshot replication and transactional replication.

`sp_droppullsubscription` deletes the corresponding row in the [MSreplication_subscriptions (Transact-SQL)](../system-tables/msreplication-subscriptions-transact-sql.md) table and the corresponding Distributor Agent at the Subscriber. If no rows are left in [MSreplication_subscriptions (Transact-SQL)](../system-tables/msreplication-subscriptions-transact-sql.md), it drops the table.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-droppullsubscription-_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role or the user who created the pull subscription can execute `sp_droppullsubscription`. The **db_owner** fixed database role is only able to execute `sp_droppullsubscription` if the user who created the pull subscription belongs to this role.

## Related content

- [Delete a Pull Subscription](../replication/delete-a-pull-subscription.md)
- [sp_addpullsubscription (Transact-SQL)](sp-addpullsubscription-transact-sql.md)
- [sp_change_subscription_properties (Transact-SQL)](sp-change-subscription-properties-transact-sql.md)
- [sp_helppullsubscription (Transact-SQL)](sp-helppullsubscription-transact-sql.md)
- [sp_dropsubscription (Transact-SQL)](sp-dropsubscription-transact-sql.md)
