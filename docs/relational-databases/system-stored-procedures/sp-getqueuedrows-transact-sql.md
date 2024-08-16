---
title: "sp_getqueuedrows (Transact-SQL)"
description: sp_getqueuedrows retrieves rows at the Subscriber that have updates pending in the queue.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_getqueuedrows_TSQL"
  - "sp_getqueuedrows"
helpviewer_keywords:
  - "sp_getqueuedrows"
dev_langs:
  - "TSQL"
---
# sp_getqueuedrows (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Retrieves rows at the Subscriber that have updates pending in the queue. This stored procedure is executed at the Subscriber on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_getqueuedrows
    [ @tablename = ] N'tablename'
    [ , [ @owner = ] N'owner' ]
    [ , [ @tranid = ] N'tranid' ]
[ ; ]
```

## Arguments

#### [ @tablename = ] N'*tablename*'

The name of the table. *@tablename* is **sysname**, with no default. The table must be a part of a queued subscription.

#### [ @owner = ] N'*owner*'

The subscription owner. *@owner* is **sysname**, with a default of `NULL`.

#### [ @tranid = ] N'*tranid*'

Allows the output to be filtered by the transaction ID. *@tranid* is **nvarchar(70)**, with a default of `NULL`. If specified, the transaction ID associated with the queued command is displayed. If `NULL`, all the commands in the queue are displayed.

## Return code values

`0` (success) or `1` (failure).

## Result set

Shows all rows that currently have at least one queued transaction for the subscribed table.

| Column name | Data type | Description |
| --- | --- | --- |
| `action` | **nvarchar(10)** | Type of action to be taken when synchronization occurs.<br /><br />`INS` = insert<br />`DEL` = delete<br />`UPD` = update |
| `tranid` | **nvarchar(70)** | Transaction ID that the command was executed under. |
| `table column1 ...n` | | The value for each column of the table specified in *@tablename*. |
| `msrepl_tran_version` | **uniqueidentifier** | This column is used to track changes to replicated data, and to perform conflict detection at the Publisher. This column is added to the table automatically. |

## Remarks

`sp_getqueuedrows` is used at Subscribers participating in queued updating.

`sp_getqueuedrows` finds rows of a given table on a subscription database that participated in a queued update, yet currently aren't resolved by the queue reader agent.

## Permissions

`sp_getqueuedrows` requires `SELECT` permissions on the table specified in *@tablename*.

## Related content

- [Updatable Subscriptions - For Transactional Replication](../replication/transactional/updatable-subscriptions-for-transactional-replication.md)
- [Updatable Subscriptions - Queued Updating Conflict Resolution](../replication/transactional/updatable-subscriptions-queued-updating-conflict-resolution.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
