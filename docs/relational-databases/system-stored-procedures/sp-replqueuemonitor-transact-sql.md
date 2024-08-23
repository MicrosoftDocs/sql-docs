---
title: "sp_replqueuemonitor (Transact-SQL)"
description: sp_replqueuemonitor lists the queue messages from a queue in SQL Server or Microsoft Message Queue.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_replqueuemonitor"
  - "sp_replqueuemonitor_TSQL"
helpviewer_keywords:
  - "sp_replqueuemonitor"
dev_langs:
  - "TSQL"
---
# sp_replqueuemonitor (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Lists the queue messages from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] queue or [!INCLUDE [msCoName](../../includes/msconame-md.md)] Message Queuing for queued updating subscriptions to a specified publication. If [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] queues are used, this stored procedure is executed at the Subscriber on the subscription database. If Message Queuing is used, this stored procedure is executed at the Distributor on the distribution database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_replqueuemonitor
    [ [ @publisher = ] N'publisher' ]
    [ , [ @publisherdb = ] N'publisherdb' ]
    [ , [ @publication = ] N'publication' ]
    [ , [ @tranid = ] N'tranid' ]
    [ , [ @queuetype = ] queuetype ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `NULL`. The server must be configured for publishing. `NULL` is used to get all Publishers.

#### [ @publisherdb = ] N'*publisherdb*'

The name of the publication database. *@publisherdb* is **sysname**, with a default of `NULL`. `NULL` is used to get all publication databases.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `NULL`. `NULL` is used to get all publications.

#### [ @tranid = ] N'*tranid*'

The transaction ID. *@tranid* is **sysname**, with a default of `NULL`. `NULL` is used to get all transactions.

#### [ @queuetype = ] *queuetype*

The type of queue that stores transactions. *@queuetype* is **tinyint**, and can be one of these values.

| Value | Description |
| --- | --- |
| `0` (default) | All types of queues |
| `1` | Message Queuing |
| `2` | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] queue |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_replqueuemonitor` is used in snapshot replication or transactional replication with queued updating subscriptions. The queue messages that don't contain SQL commands or are part of a spanning SQL command aren't displayed.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_replqueuemonitor`.

## Related content

- [Updatable Subscriptions - For Transactional Replication](../replication/transactional/updatable-subscriptions-for-transactional-replication.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
