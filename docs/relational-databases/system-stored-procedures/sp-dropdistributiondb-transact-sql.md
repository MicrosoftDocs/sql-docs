---
title: "sp_dropdistributiondb (Transact-SQL)"
description: Drops a distribution database and files used by it if they aren't used by another database. This stored procedure runs at the Distributor on any database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_dropdistributiondb_TSQL"
  - "sp_dropdistributiondb"
helpviewer_keywords:
  - "sp_dropdistributiondb"
dev_langs:
  - "TSQL"
---
# sp_dropdistributiondb (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Drops a distribution database. Drops the physical files used by the database if they aren't used by another database. This stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropdistributiondb
    [ @database = ] N'database'
    [ , [ @former_ag_secondary = ] former_ag_secondary ]
[ ; ]
```

## Arguments

#### [ @database = ] N'*database*'

The database to drop. *@database* is **sysname**, with no default.

#### [ @former_ag_secondary = ] *former_ag_secondary*

Specifies whether this node was previously part of an availability group for the distribution database. *@former_ag_secondary* is **int**, with a default of `0`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_dropdistributiondb` is used in all types of replication.

This stored procedure must be executed before dropping the Distributor by executing `sp_dropdistributor`.

`sp_dropdistributiondb` also removes a Queue Reader Agent job for the distribution database, if one exists.

To disable distribution, the distribution database must be online. If a database snapshot exists for the distribution database, it must be dropped before disabling distribution. A database snapshot is a read-only offline copy of a database, and isn't related to a replication snapshot. For more information, see [Database snapshots (SQL Server)](../databases/database-snapshots-sql-server.md).

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-dropdistributiondb-tr_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_dropdistributiondb`.

## Related content

- [Disable Publishing and Distribution](../replication/disable-publishing-and-distribution.md)
- [sp_adddistributiondb (Transact-SQL)](sp-adddistributiondb-transact-sql.md)
- [sp_changedistributiondb (Transact-SQL)](sp-changedistributiondb-transact-sql.md)
- [sp_helpdistributiondb (Transact-SQL)](sp-helpdistributiondb-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
