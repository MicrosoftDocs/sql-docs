---
title: "sp_setreplfailovermode (Transact-SQL)"
description: sp_setreplfailovermode allows you to set the failover operation mode for subscriptions enabled for immediate updating, with queued updating as failover.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_setreplfailovermode"
  - "sp_setreplfailovermode_TSQL"
helpviewer_keywords:
  - "sp_setreplfailovermode"
dev_langs:
  - "TSQL"
---
# sp_setreplfailovermode (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Allows you to set the failover operation mode for subscriptions enabled for immediate updating, with queued updating as failover. This stored procedure is executed at the Subscriber on the subscription database. For more information about failover modes, see [Updatable Subscriptions - For Transactional Replication](../replication/transactional/updatable-subscriptions-for-transactional-replication.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_setreplfailovermode
    [ @publisher = ] N'publisher'
    , [ @publisher_db = ] N'publisher_db'
    , [ @publication = ] N'publication'
    , [ @failover_mode = ] N'failover_mode'
    [ , [ @override = ] override ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the publication. *@publisher* is **sysname**, with no default. The publication must already exist.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the publication database. *@publisher_db* is **sysname**, with no default.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @failover_mode = ] N'*failover_mode*'

The failover mode for the subscription. *@failover_mode* is **nvarchar(10)**, and can be one of these values.

| Value | Description |
| --- | --- |
| `immediate` or `sync` | Data modifications made at the Subscriber are bulk-copied to the Publisher as they occur. |
| `queued` | Data modifications are stored in a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] queue. |

> [!NOTE]  
> [!INCLUDE [msCoName](../../includes/msconame-md.md)] Message Queuing has been deprecated and is no longer supported.

#### [ @override = ] *override*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_setreplfailovermode` is used in snapshot replication or transactional replication for which subscriptions are enabled, either for queued updating with failover to immediate updating, or for immediate updating with failover to queued updating.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_setreplfailovermode`.

## Related content

- [Switch Between Update Modes for an Updatable Transactional Subscription](../replication/administration/switch-between-update-modes-for-an-updatable-transactional-subscription.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
