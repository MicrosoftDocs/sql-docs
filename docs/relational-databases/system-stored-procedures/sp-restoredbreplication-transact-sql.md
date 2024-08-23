---
title: "sp_restoredbreplication (Transact-SQL)"
description: sp_restoredbreplication removes replication settings when restoring a database to a non-originating server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_restoredbreplication"
  - "sp_restoredbreplication_TSQL"
helpviewer_keywords:
  - "sp_restoredbreplication"
dev_langs:
  - "TSQL"
---
# sp_restoredbreplication (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Removes replication settings if restoring a database to the non-originating server, database, or system that is otherwise not capable of running replication processes. When restoring a replicated database to a server or database other than the one where the backup was taken, replication settings can't be preserved. On the restore, the server calls `sp_restoredbreplication` directly to automatically remove replication metadata from the restored database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_restoredbreplication
    [ @srv_orig = ] N'srv_orig'
    , [ @db_orig = ] N'db_orig'
    [ , [ @keep_replication = ] keep_replication ]
    [ , [ @perform_upgrade = ] perform_upgrade ]
    [ , [ @recoveryforklsn = ] recoveryforklsn ]
[ ; ]
```

## Arguments

#### [ @srv_orig = ] N'*srv_orig*'

The name of the server where the backup was created. *@srv_orig* is **sysname**, with no default.

#### [ @db_orig = ] N'*db_orig*'

The name of the database that was backed up. *@db_orig* is **sysname**, with no default.

#### [ @keep_replication = ] *keep_replication*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @perform_upgrade = ] *perform_upgrade*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### [ @recoveryforklsn = ] *recoveryforklsn*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_restoredbreplication` is used in all types of replication.

## Permissions

Only members of the **sysadmin** or **dbcreator** fixed server role, or the `dbo` database schema, can execute `sp_restoredbreplication`.

## Related content

- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
