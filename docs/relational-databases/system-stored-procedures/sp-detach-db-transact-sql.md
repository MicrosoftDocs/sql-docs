---
title: "sp_detach_db (Transact-SQL)"
description: Detaches a database that is currently not in use from a server instance and, optionally, runs UPDATE STATISTICS on all tables before detaching.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_detach_db"
  - "sp_detach_db_TSQL"
helpviewer_keywords:
  - "sp_detach_db"
  - "detaching databases [SQL Server]"
dev_langs:
  - "TSQL"
---
# sp_detach_db (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Detaches a database that is currently not in use from a server instance and, optionally, runs `UPDATE STATISTICS` on all tables before detaching.

For a replicated database to be detached, it must be unpublished. For more information, see the [Remarks](#remarks) section later in this article.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_detach_db
    [ [ @dbname = ] N'dbname' ]
    [ , [ @skipchecks = ] N'skipchecks' ]
    [ , [ @keepfulltextindexfile = ] N'keepfulltextindexfile' ]
[ ; ]
```

## Arguments

#### [ @dbname = ] N'*dbname*'

The name of the database to be detached. *@dbname* is **sysname**, with a default of `NULL`.

#### [ @skipchecks = ] N'*skipchecks*'

Specifies whether to skip or run `UPDATE STATISTICS`. *@skipchecks* is **nvarchar(10)**, with a default of `NULL`. To skip `UPDATE STATISTICS`, specify `true`. To explicitly run `UPDATE STATISTICS`, specify `false`.

By default, `UPDATE STATISTICS` is performed to update information about the data in the tables and indexes. Performing `UPDATE STATISTICS` is useful for databases that are to be moved to read-only media.

#### [ @keepfulltextindexfile = ] N'*keepfulltextindexfile*'

Specifies that the full-text index file associated with the database that is being detached isn't dropped during the database detach operation. *@keepfulltextindexfile* is **nvarchar(10)**, with a default of `true`.

- If *@keepfulltextindexfile* is `false`, all the full-text index files associated with the database and the metadata of the full-text index are dropped, unless the database is read-only.
- If `NULL` or `true`, full-text related metadata is kept.

> [!IMPORTANT]  
> [!INCLUDE [ssnotedepfutureavoid-md](../../includes/ssnotedepfutureavoid-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

When a database is detached, all its metadata is dropped. If the database was the default database of any login accounts, `master` becomes their default database.

> [!NOTE]  
> For information about how to view the default database of all the login accounts, see [sp_helplogins](sp-helplogins-transact-sql.md). If you've the required permissions, you can use [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md) to assign a new default database to a login.

## Limitations

A database can't be detached if any of the following are true:

- The database is currently in use. For more information, see [Obtain exclusive access](#obtain-exclusive-access).

- If replicated, the database is published.

  Before you can detach the database, you must disable publishing by running [sp_replicationdboption](sp-replicationdboption-transact-sql.md).

  If you can't use `sp_replicationdboption`, you can remove replication by running [sp_removedbreplication](sp-removedbreplication-transact-sql.md).

- A database snapshot exists on the database.

  Before you can detach the database, you must drop all of its snapshots. For more information, see [Drop a Database Snapshot](../databases/drop-a-database-snapshot-transact-sql.md).

  A database snapshot can't be detached or attached.

- The database is being mirrored.

  The database can't be detached until the database mirroring session is terminated. For more information, see [Removing Database Mirroring (SQL Server)](../../database-engine/database-mirroring/removing-database-mirroring-sql-server.md).

- The database is suspect.

  You must put a suspect database into emergency mode before you can detach the database. For more information about how to put a database into emergency mode, see [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md).

- The database is a system database.

## Obtain exclusive access

Detaching a database requires exclusive access to the database. If the database that you want to detach is in use, before you can detach it, set the database to `SINGLE_USER` mode to obtain exclusive access.

Before you set the database to `SINGLE_USER`, verify that the `AUTO_UPDATE_STATISTICS_ASYNC` option is set to `OFF`. When this option is set to `ON`, the background thread that is used to update statistics takes a connection against the database, and you're unable to access the database in single-user mode. For more information, see [Set a database to single-user mode](../databases/set-a-database-to-single-user-mode.md).

For example, the following `ALTER DATABASE` statement obtains exclusive access to the [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database after all current users disconnect from the database.

```sql
USE master;
ALTER DATABASE AdventureWorks2022
SET SINGLE_USER;
GO
```

To force current users out of the database immediately or within a specified number of seconds, you can also use the `ROLLBACK` option.

```sql
ALTER DATABASE <database_name>
SET SINGLE_USER
WITH ROLLBACK <rollback_option>;
```

For more information, see [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md).

## Reattach a database

The detached files remain and can be reattached by using `CREATE DATABASE` (with the `FOR ATTACH` or `FOR ATTACH_REBUILD_LOG` option). The files can be moved to another server and attached there.

## Permissions

Requires membership in the **sysadmin** fixed server role or membership in the **db_owner** role of the database.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

The following example detaches the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database with *@skipchecks* set to `true`.

```sql
EXEC sp_detach_db 'AdventureWorks2022', 'true';
```

The following example detaches the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database and keeps the full-text index files and the metadata of the full-text index. This command runs UPDATE STATISTICS, which is the default behavior.

```sql
EXEC sp_detach_db @dbname = 'AdventureWorks2022',
    @keepfulltextindexfile = 'true';
```

## Related content

- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [Database detach and attach (SQL Server)](../databases/database-detach-and-attach-sql-server.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [Detach a database](../databases/detach-a-database.md)
