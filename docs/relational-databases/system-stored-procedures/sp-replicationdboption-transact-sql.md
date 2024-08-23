---
title: "sp_replicationdboption (Transact-SQL)"
description: sp_replicationdboption sets a replication database option for the specified database.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_replicationdboption_TSQL"
  - "sp_replicationdboption"
helpviewer_keywords:
  - "sp_replicationdboption"
dev_langs:
  - "TSQL"
---
# sp_replicationdboption (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Sets a replication database option for the specified database. This stored procedure is executed at the Publisher or Subscriber on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_replicationdboption
    [ @dbname = ] N'dbname'
    , [ @optname = ] N'optname'
    , [ @value = ] { N'true' | N 'false' }
    [ , [ @ignore_distributor = ] ignore_distributor ]
    [ , [ @from_scripting = ] from_scripting ]
[ ; ]
```

## Arguments

#### [ @dbname = ] N'*dbname*'

The database for which the replication database option is being set. *@dbname* is **sysname**, with no default.

#### [ @optname = ] N'*optname*'

The replication database option to enable or disable. *@optname* is **sysname**, and can be one of these values.

| Value | Description |
| --- | --- |
| `merge publish` | Database can be used for merge publications. |
| `publish` | Database can be used for other types of publications. |
| `subscribe` | Database is a subscription database. |
| `sync with backup` | Database is enabled for coordinated backup. For more information, see [Enable Coordinated Backups for Transactional Replication](../replication/administration/enable-coordinated-backups-for-transactional-replication.md). |

#### [ @value = ] { N'true' | N 'false' }

Whether to enable or disable the given replication database option. *@value* is **sysname**, with no default. When this value is `false` and *@optname* is `merge publish`, subscriptions to the merge published database are also dropped.

#### [ @ignore_distributor = ] *ignore_distributor*

Indicates whether this stored procedure is executed without connecting to the Distributor. *@ignore_distributor* is **bit**, with a default of `0`.

- If `0`, the Distributor should be connected to and updated with the new status of the publishing database.

- `1` should be specified only if the Distributor is inaccessible, and `sp_replicationdboption` is being used to disable publishing.

#### [ @from_scripting = ] *from_scripting*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_replicationdboption` is used in snapshot replication, transactional replication, and merge replication.

This procedure creates or drops specific replication system tables, security accounts, and so on, depending on the options given. Sets the corresponding `is_published` (transactional or snapshot replication), `is_merge_published` (merge replication), or `is_distributor` bits in the `master.databases` system table and creates the necessary system tables.

To disable publishing, the publication database must be online. If a database snapshot exists for the publication database, it must be dropped before disabling publishing. A database snapshot is a read-only offline copy of a database, and isn't related to a replication snapshot. For more information, see [Database snapshots (SQL Server)](../databases/database-snapshots-sql-server.md).

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_replicationdboption`.

## Related content

- [Configure Publishing and Distribution](../replication/configure-publishing-and-distribution.md)
- [Create a publication](../replication/publish/create-a-publication.md)
- [Delete a Publication](../replication/publish/delete-a-publication.md)
- [Disable Publishing and Distribution](../replication/disable-publishing-and-distribution.md)
- [sys.databases (Transact-SQL)](../system-catalog-views/sys-databases-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
