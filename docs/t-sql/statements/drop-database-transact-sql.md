---
title: "DROP DATABASE (Transact-SQL)"
description: DROP DATABASE (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP DATABASE"
  - "DROP_DATABASE_TSQL"
helpviewer_keywords:
  - "snapshots [SQL Server database snapshots], deleting"
  - "removing databases"
  - "database snapshots [SQL Server], removing"
  - "deleting databases"
  - "dropping databases"
  - "databases [SQL Server], dropping"
  - "DROP DATABASE statement"
  - "database removal [SQL Server], DROP DATABASE statement"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DROP DATABASE (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Removes one or more user databases or database snapshots from an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
-- SQL Server Syntax
DROP DATABASE [ IF EXISTS ] { database_name | database_snapshot_name } [ ,...n ] [;]
```

```syntaxsql
-- Azure SQL Database, Azure Synapse Analytics and Analytics Platform System Syntax
DROP DATABASE database_name [;]
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

 *IF EXISTS*  
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).

Conditionally drops the database only if it already exists.

*database_name*
Specifies the name of the database to be removed. To display a list of databases, use the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.

*database_snapshot_name*
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later.

Specifies the name of a database snapshot to be removed.

## General Remarks

A database can be dropped regardless of its state: offline, read-only, suspect, and so on. To display the current state of a database, use the **sys.databases** catalog view.

A dropped database can be re-created only by restoring a backup. Database snapshots cannot be backed up and, therefore, cannot be restored.

When a database is dropped, the [master database](../../relational-databases/databases/master-database.md) should be backed up.

Dropping a database deletes the database from an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and deletes the physical disk files used by the database. If the database or any one of its files is offline when it is dropped, the disk files are not deleted. These files can be deleted manually by using Windows Explorer. To remove a database from the current server without deleting the files from the file system, use [sp_detach_db](../../relational-databases/system-stored-procedures/sp-detach-db-transact-sql.md).

> [!WARNING]
> Dropping a database that has FILE_SNAPSHOT backups associated with it will succeed, but the database files that have associated snapshots will not be deleted to avoid invalidating the backups referring to these database files. The file will be truncated, but will not be physically deleted in order to keep the FILE_SNAPSHOT backups intact. For more information, see [SQL Server Backup and Restore with Microsoft Azure Blob Storage](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md). **Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level).

### [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

Dropping a database snapshot deletes the database snapshot from an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and deletes the physical NTFS File System sparse files used by the snapshot. For information about using sparse files by database snapshots, see [Database Snapshots](../../relational-databases/databases/database-snapshots-sql-server.md). Dropping a database snapshot clears the plan cache for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Clearing the plan cache causes a recompilation of all subsequent execution plans and can cause a sudden, temporary decrease in query performance. For each cleared cachestore in the plan cache, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log contains the following informational message: " [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has encountered %d occurrence(s) of cachestore flush for the '%s' cachestore (part of plan cache) due to some database maintenance or reconfigure operations". This message is logged every five minutes as long as the cache is flushed within that time interval.

## Interoperability

### SQL Server

To drop a database published for transactional replication, or published or subscribed to merge replication, you must first remove replication from the database. If a database is damaged or replication cannot first be removed or both, in most cases you still can drop the database by using ALTER DATABASE to set the database offline and then dropping it.

If the database is involved in log shipping, remove log shipping before dropping the database. For more information, see [About Log Shipping](../../database-engine/log-shipping/about-log-shipping-sql-server.md).

## Limitations and Restrictions

[System databases](../../relational-databases/databases/system-databases.md) cannot be dropped.

The DROP DATABASE statement must run in autocommit mode and is not allowed in an explicit or implicit transaction. Autocommit mode is the default transaction management mode.

> [!WARNING]
> You cannot drop a database currently being used. This means locks being held for reading or writing by any user. One way to remove users from the database is to use ALTER DATABASE to set the database to SINGLE_USER. In this strategy, you should execute the ALTER DATABASE and DROP DATABASE in the same batch, to avoid another connection claiming single user session allowed. See [Example D below](#d-dropping-a-database-after-checking-if-it-exists).

### [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

Any database snapshots on a database must be dropped before the database can be dropped.

Dropping a database enable for Stretch Database does not remove the remote data. If you want to delete the remote data, you have to remove it manually.

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

### [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

You must be connected to the master database to drop a database.

 The DROP DATABASE statement must be the only statement in a SQL batch and you can drop only one database at a time.

### [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)]

You must be connected to the master database to drop a database.

The DROP DATABASE statement must be the only statement in a SQL batch and you can drop only one database at a time.

## Permissions

### [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

Requires the **CONTROL** permission on the database, or **ALTER ANY DATABASE** permission, or membership in the **db_owner** fixed database role.

### [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

Only the server-level principal login (created by the provisioning process) or members of the **dbmanager** database role can drop a database.

### [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

Requires the **CONTROL** permission on the database, or **ALTER ANY DATABASE** permission, or membership in the **db_owner** fixed database role.

## Examples

### A. Dropping a single database

The following example removes the `Sales` database.

```sql
DROP DATABASE Sales;
```

### B. Dropping multiple databases

**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later.

The following example removes each of the listed databases.

```sql
DROP DATABASE Sales, NewSales;
```

### C. Dropping a database snapshot

**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later.

The following example removes a database snapshot, named `sales_snapshot0600`, without affecting the source database.

```sql
DROP DATABASE sales_snapshot0600;
```

### D. Dropping a database after checking if it exists


The following example first checks to see if a database named `Sales` exists. If so, the example changes the database named `Sales` to single-user mode to force disconnect of all other sessions, then drops the database. For more information on SINGLE_USER, see [ALTER DATABASE SET options](alter-database-transact-sql-set-options.md).


```sql
USE tempdb;
GO
DECLARE @SQL nvarchar(1000);
IF EXISTS (SELECT 1 FROM sys.databases WHERE [name] = N'Sales')
BEGIN
    SET @SQL = N'USE [Sales];

                 ALTER DATABASE Sales SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                 USE [tempdb];

                 DROP DATABASE Sales;';
    EXEC (@SQL);
END;
```
## See Also

- [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [EVENTDATA](../../t-sql/functions/eventdata-transact-sql.md)
- [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
