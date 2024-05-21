---
title: "sp_helpreplfailovermode (Transact-SQL)"
description: sp_helpreplfailovermode displays the current failover mode of a subscription.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpreplfailovermode"
  - "sp_helpreplfailovermode_TSQL"
helpviewer_keywords:
  - "sp_helpreplfailovermode"
dev_langs:
  - "TSQL"
---
# sp_helpreplfailovermode (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Displays the current failover mode of a subscription. This stored procedure is executed at the Subscriber on any database. For more information about failover modes, see [Updatable Subscriptions - For Transactional Replication](../replication/transactional/updatable-subscriptions-for-transactional-replication.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpreplfailovermode
    [ @publisher = ] N'publisher'
    , [ @publisher_db = ] N'publisher_db'
    , [ @publication = ] N'publication'
    [ , [ @failover_mode_id = ] failover_mode_id OUTPUT ]
    [ , [ @failover_mode = ] N'failover_mode' OUTPUT ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher that is participating in the update of this Subscriber. *@publisher* is **sysname**, with no default. The Publisher must already be configured for publishing.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the publication database. *@publisher_db* is **sysname**, with no default.

#### [ @publication = ] N'*publication*'

The name of the publication that is participating in the update of this Subscriber. *@publication* is **sysname**, with no default.

#### [ @failover_mode_id = ] *failover_mode_id* OUTPUT

Returns the integer value of the failover mode. *@failover_mode_id* is an OUTPUT parameter of type **tinyint**. It returns `0` for immediate updating and `1` for queued updating.

#### [ @failover_mode = ] N'*failover_mode*' OUTPUT

Returns the mode in which data modifications are made at the Subscriber. *@failover_mode* is an OUTPUT parameter of type **nvarchar(10)**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `immediate` | Immediate updating: updates made at the Subscriber are immediately propagated to the Publisher using two-phase commit protocol (2PC). |
| `queued` | Queued updating: updates made at the Subscriber are stored in a queue. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpreplfailovermode` is used in snapshot replication or transactional replication for which subscriptions are enabled for immediate updating with queued updating as failover, if there's a failure.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_helpreplfailovermode`.

## Related content

- [sp_setreplfailovermode (Transact-SQL)](sp-setreplfailovermode-transact-sql.md)
