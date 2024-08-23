---
title: "sp_replflush (Transact-SQL)"
description: sp_replflush flushes the article cache.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_replflush"
  - "sp_replflush_TSQL"
helpviewer_keywords:
  - "sp_replflush"
dev_langs:
  - "TSQL"
---
# sp_replflush (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Flushes the article cache. This stored procedure is executed at the Publisher on the publication database.

> [!IMPORTANT]  
> You shouldn't have to execute this procedure manually. You should only use `sp_replflush` for troubleshooting replication as directed by an experienced replication support professional.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_replflush
[ ; ]
```

## Arguments

None.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_replflush` is used in transactional replication.

Article definitions are stored in the cache for efficiency. `sp_replflush` is used by other replication stored procedures whenever an article definition is modified or dropped.

Only one client connection can have log reader access to a given database. If a client has log reader access to a database, executing `sp_replflush` causes the client to release its access. Other clients can then scan the transaction log using `sp_replcmds` or `sp_replshowcmds`.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_replflush`.

## Related content

- [sp_replcmds (Transact-SQL)](sp-replcmds-transact-sql.md)
- [sp_repldone (Transact-SQL)](sp-repldone-transact-sql.md)
- [sp_repltrans (Transact-SQL)](sp-repltrans-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
