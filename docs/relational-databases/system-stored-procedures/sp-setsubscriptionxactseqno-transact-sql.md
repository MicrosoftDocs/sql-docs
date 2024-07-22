---
title: "sp_setsubscriptionxactseqno (Transact-SQL)"
description: Troubleshooting method to specify the last delivered transaction using the log sequence number (LSN), allowing the Distribution Agent to begin delivering at the next transaction.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_setsubscriptionxactseqno"
  - "sp_setsubscriptionxactseqno_TSQL"
helpviewer_keywords:
  - "sp_setsubscriptionxactseqno"
dev_langs:
  - "TSQL"
---
# sp_setsubscriptionxactseqno (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Used during troubleshooting to specify the last delivered transaction using the log sequence number (LSN), allowing the Distribution Agent to begin delivering at the next transaction. After it restarts, the Distribution Agent returns transactions greater than this watermark (LSN) from the Distribution database cache (msrepl_commands). This stored procedure is executed at the Subscriber on the subscription database. Not supported for non-SQL Server Subscribers.

> [!CAUTION]  
> Using this stored procedure incorrectly or specifying an incorrect LSN value can cause the Distribution Agent to revert changes that were already applied at the Subscriber or skip over all remaining changes.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_setsubscriptionxactseqno
    [ @publisher = ] N'publisher'
    , [ @publisher_db = ] N'publisher_db'
    , [ @publication = ] N'publication'
    , [ @xact_seqno = ] xact_seqno
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the publication database. *@publisher_db* is **sysname**, with no default. For a non-SQL Server Publisher, *@publisher_db* is the name of the distribution database.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default. When more than one publication shares the Distribution Agent, you must specify a value of `ALL` for *@publication*.

#### [ @xact_seqno = ] *xact_seqno*

The LSN of the next transaction at the Distributor to be applied at the Subscriber. *@xact_seqno* is **varbinary(16)**, with no default.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `ORIGINAL XACT_SEQNO` | **varbinary(16)** | The original LSN of the next transaction to be applied at the Subscriber. |
| `UPDATED XACT_SEQNO` | **varbinary(16)** | The updated LSN of the next transaction to be applied at the Subscriber. |
| `SUBSCRIPTION STREAM COUNT` | **int** | The number of subscription streams used during the last synchronization. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_setsubscriptionxactseqno` is used in transactional replication.

`sp_setsubscriptionxactseqno` can't be used in a peer-to-peer transactional replication topology.

`sp_setsubscriptionxactseqno` can be used to skip a specific transaction that is causing an error when applies at the Subscriber. When there's a failure and after the Distribution Agent stops, call [sp_helpsubscriptionerrors](sp-helpsubscriptionerrors-transact-sql.md) at the Distributor to retrieve the `xact_seqno` value of the failed transaction, and then call `sp_setsubscriptionxactseqno`, passing this value for *@xact_seqno*. This ensures that only the commands after this LSN are processed.

Specify a value of `0` for *@xact_seqno* to deliver all pending commands in the distribution database to the Subscriber.

`sp_setsubscriptionxactseqno` might fail if the Distribution Agent uses multi-subscription streams.

When this error occurs, you must run the Distribution Agent with a single subscription stream. For more information, see [Replication Distribution Agent](../replication/agents/replication-distribution-agent.md).

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_setsubscriptionxactseqno`.
