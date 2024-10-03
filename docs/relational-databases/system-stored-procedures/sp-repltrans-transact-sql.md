---
title: "sp_repltrans (Transact-SQL)"
description: sp_repltrans Returns a result set of all the transactions in the publication database transaction log that are marked for replication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_repltrans_TSQL"
  - "sp_repltrans"
helpviewer_keywords:
  - "sp_repltrans"
dev_langs:
  - "TSQL"
---
# sp_repltrans (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns a result set of all the transactions in the publication database transaction log that are marked for replication but aren't marked as distributed. This stored procedure is executed at the Publisher on a publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_repltrans
[ ; ]
```

## Arguments

None.

## Result set

`sp_repltrans` returns information about the publication database from which it's executed, allowing you to view transactions currently not distributed (those transactions remaining in the transaction log that aren't yet sent to the Distributor). The result set displays the log sequence numbers of the first and last records for each transaction. `sp_repltrans` is similar to [sp_replcmds](sp-replcmds-transact-sql.md), but doesn't return the commands for the transactions.

## Remarks

`sp_repltrans` is used in transactional replication.

`sp_repltrans` isn't supported for non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_repltrans`.

## Related content

- [sp_repldone (Transact-SQL)](sp-repldone-transact-sql.md)
- [sp_replflush (Transact-SQL)](sp-replflush-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
