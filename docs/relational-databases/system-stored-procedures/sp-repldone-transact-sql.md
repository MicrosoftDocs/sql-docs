---
title: "sp_repldone (Transact-SQL)"
description: sp_repldone updates the record that identifies the last distributed transaction of the server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_repldone"
  - "sp_repldone_TSQL"
helpviewer_keywords:
  - "sp_repldone"
dev_langs:
  - "TSQL"
---
# sp_repldone (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Updates the record that identifies the last distributed transaction of the server. This stored procedure is executed at the Publisher on the publication database.

> [!CAUTION]  
> If you execute `sp_repldone` manually, you can invalidate the order and consistency of delivered transactions. You should only use `sp_repldone` for troubleshooting replication as directed by an experienced replication support professional.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_repldone [ @xactid = ] xactid
    , [ @xact_seqno = ] xact_seqno
    [ , [ @numtrans = ] numtrans ]
    [ , [ @time = ] time ]
    [ , [ @reset = ] reset ]
[ ; ]
```

## Arguments

#### [ @xactid = ] *xactid*

The log sequence number (LSN) of the first record for the last distributed transaction of the server. *@xactid* is **binary(10)**, with no default.

#### [ @xact_seqno = ] *xact_seqno*

The LSN of the last record for the last distributed transaction of the server. *@xact_seqno* is **binary(10)**, with no default.

#### [ @numtrans = ] *numtrans*

The number of transactions distributed. *@numtrans* is **int**, with no default.

#### [ @time = ] *time*

The number of milliseconds, if provided, needed to distribute the last batch of transactions. *@time* is **int**, with no default.

#### [ @reset = ] *reset*

The reset status. *@reset* is **int**, with no default.

- If `1`, all replicated transactions in the log are marked as distributed.
- If `0`, the transaction log is reset to the first replicated transaction and no replicated transactions are marked as distributed.

*@reset* is valid only when both *@xactid* and *@xact_seqno* are `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_repldone` is used in transactional replication.

`sp_repldone` is used by the log reader process to track which transactions have been distributed.

With `sp_repldone`, you can manually tell the server that a transaction has been replicated (sent to the Distributor). It also allows you to change the transaction marked as the next one awaiting replication. You can move forward or backward in the list of replicated transactions. (All transactions less than or equal to that transaction are marked as distributed.)

The required parameters *@xactid* and *@xact_seqno* can be obtained by using `sp_repltrans` or `sp_replcmds`.

This procedure can be used in emergency situations to allow truncation of the transaction log when transactions pending replication are present. For more information, see the [Examples](#examples) section.

## Permissions

Members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_repldone`.

## Examples

When *@xactid* is `NULL`, *@xact_seqno* is `NULL`, and *@reset* is `1`, all replicated transactions in the log are marked as distributed. This is useful when there are replicated transactions in the transaction log that are no longer valid and you want to truncate the log, for example:

```sql
EXEC sp_repldone
    @xactid = NULL,
    @xact_seqno = NULL,
    @numtrans = 0,
    @time = 0,
    @reset = 1;
```

## Related content

- [sp_replcmds (Transact-SQL)](sp-replcmds-transact-sql.md)
- [sp_replflush (Transact-SQL)](sp-replflush-transact-sql.md)
- [sp_repltrans (Transact-SQL)](sp-repltrans-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
