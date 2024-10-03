---
title: "sp_replshowcmds (Transact-SQL)"
description: sp_replshowcmds returns the commands for transactions marked for replication in readable format.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_replshowcmds"
  - "sp_replshowcmds_TSQL"
helpviewer_keywords:
  - "sp_replshowcmds"
dev_langs:
  - "TSQL"
---
# sp_replshowcmds (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns the commands for transactions marked for replication in readable format. `sp_replshowcmds` can be run only when client connections (including the current connection) aren't reading replicated transactions from the log. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_replshowcmds [ [ @maxtrans = ] maxtrans ]
[ ; ]
```

## Arguments

#### [ @maxtrans = ] *maxtrans*

The number of transactions about which to return information. *@maxtrans* is **int**, with a default of `1`, which specifies the maximum number of transactions pending replication for which `sp_replshowcmds` returns information.

## Result set

`sp_replshowcmds` is a diagnostic procedure that returns information about the publication database from which it's executed.

| Column name | Data type | Description |
| --- | --- | --- |
| `xact_seqno` | **binary(10)** | Sequence number of the command. |
| `originator_id` | **int** | ID of the command originator, always `0`. |
| `publisher_database_id` | **int** | ID of the Publisher database, always `0`. |
| `article_id` | **int** | ID of the article. |
| `type` | **int** | Type of command. |
| `command` | **nvarchar(1024)** | [!INCLUDE [tsql](../../includes/tsql-md.md)] command. |

## Remarks

`sp_replshowcmds` is used in transactional replication.

Using `sp_replshowcmds`, you can view transactions that currently aren't distributed (those transactions remaining in the transaction log that aren't yet sent to the Distributor).

Clients that run `sp_replshowcmds` and `sp_replcmds` within the same database receive error 18752.

To avoid this error, the first client must disconnect or the role of the client as log reader must be released by executing `sp_replflush`. After all clients disconnect from the log reader, `sp_replshowcmds` can be run successfully.

> [!NOTE]  
> `sp_replshowcmds` should be run only to troubleshoot problems with replication.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_replshowcmds`.

## Related content

- [Error Messages](../native-client-odbc-error-messages/error-messages.md)
- [sp_replcmds (Transact-SQL)](sp-replcmds-transact-sql.md)
- [sp_repldone (Transact-SQL)](sp-repldone-transact-sql.md)
- [sp_replflush (Transact-SQL)](sp-replflush-transact-sql.md)
- [sp_repltrans (Transact-SQL)](sp-repltrans-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
