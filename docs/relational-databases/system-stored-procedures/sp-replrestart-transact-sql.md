---
title: "sp_replrestart (Transact-SQL)"
description: sp_replrestart is used by transactional replication during backup and restore.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_replrestart_TSQL"
  - "sp_replrestart"
helpviewer_keywords:
  - "sp_replrestart"
dev_langs:
  - "TSQL"
---
# sp_replrestart (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Used by transactional replication during backup and restore so that the replicated data at the Distributor is synchronized with data at the Publisher. This stored procedure is executed at the Publisher on the publication database.

> [!IMPORTANT]  
> `sp_replrestart` is an internal replication stored procedure and should only be used when restoring a database published in a transactional replication topology as directed in [Strategies for Backing Up and Restoring Snapshot and Transactional Replication](../replication/administration/strategies-for-backing-up-and-restoring-snapshot-and-transactional-replication.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
sp_replrestart
[ ; ]
```

## Arguments

None.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_replrestart` is used when the highest log sequence number (LSN) value at the Distributor doesn't match the highest LSN value at the Publisher.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_replrestart`.

## Related content

- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
