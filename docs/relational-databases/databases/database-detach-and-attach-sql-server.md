---
title: "Database detach and attach (SQL Server)"
description: Detach and reattach data and transaction log files of a SQL Server database when changing the database to a different instance, or to move the database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 03/30/2024
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords:
  - "upgrading databases"
  - "databases [SQL Server], detaching"
  - "detach database [SQL Server]"
  - "databases [SQL Server], attaching"
  - "removing databases"
  - "transaction logs [SQL Server], detaching"
  - "databases [SQL Server], removing"
  - "restoring [SQL Server], attached databases"
  - "transaction logs [SQL Server], attaching"
  - "differential backups, after detaching"
  - "moving databases"
  - "attach database [SQL Server]"
  - "detaching databases [SQL Server]"
  - "differential base [SQL Server]"
  - "attaching databases [SQL Server]"
  - "databases [SQL Server], moving"
---
# Database detach and attach (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The data and transaction log files of a database can be detached and then reattached to the same or another instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Detaching and attaching a database is useful if you want to change the database to a different instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on the same computer or to move the database.

## Permissions

File access permissions are set during several database operations, including detaching or attaching a database.

> [!IMPORTANT]  
> We recommend that you don't attach or restore databases from unknown or untrusted sources. Such databases could contain malicious code that might execute unintended [!INCLUDE [tsql](../../includes/tsql-md.md)] code or cause errors by modifying the schema or the physical database structure.
> Before you use a database from an unknown or untrusted source, run [DBCC CHECKDB (Transact-SQL)](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) on the database on a nonproduction server and also examine the code, such as stored procedures or other user-defined code, in the database.

## <a id="DetachDb"></a> Detach a database

Detaching a database removes it from the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] but leaves the database intact within its data files and transaction log files. These files can then be used to attach the database to any instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], including the server from which the database was detached.

You can't detach a database if any of the following are true:

- The database is replicated and published. If replicated, the database must be unpublished. Before you can detach it, you must disable publishing by running [sp_replicationdboption](../system-stored-procedures/sp-replicationdboption-transact-sql.md).

  > [!NOTE]  
  > If you can't use `sp_replicationdboption`, you can remove replication by running [sp_removedbreplication](../system-stored-procedures/sp-removedbreplication-transact-sql.md).

- A database snapshot exists on the database.

  Before you can detach the database, you must drop all of its snapshots. For more information, see [Drop a Database Snapshot (Transact-SQL)](drop-a-database-snapshot-transact-sql.md).

  > [!NOTE]  
  > A database snapshot can't be detached or attached.

- The database is part of an Always On availability group.

  The database can't be detached until it's removed from the availability group. For more information, see [Remove a primary database from an Always On availability group](../../database-engine/availability-groups/windows/remove-a-primary-database-from-an-availability-group-sql-server.md).

- The database is being mirrored in a database mirroring session.

  The database can't be detached unless the session is terminated. For more information, see [Removing Database Mirroring (SQL Server)](../../database-engine/database-mirroring/removing-database-mirroring-sql-server.md).

- The database is suspect. A suspect database can't be detached; before you can detach it, you must put it into emergency mode. For more information about how to put a database into emergency mode, see [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md).

- The database is a system database.

### Backup, restore, and detach

Detaching a read-only database loses information about the differential bases of differential backups. For more information, see [Differential backups (SQL Server)](../backup-restore/differential-backups-sql-server.md).

### Respond to detach errors

Errors produced while detaching a database can prevent the database from closing cleanly and the transaction log from being rebuilt. If you receive an error message, perform the following corrective actions:

1. Reattach all files associated with the database, not just the primary file.

1. Resolve the problem that caused the error message.

1. Detach the database again.

## <a id="AttachDb"></a> Attach a database

You can attach a copied or detached [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database. When you attach a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] database that contains full-text catalog files onto a [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] server instance, the catalog files are attached from their previous location along with the other database files, the same as in [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)]. For more information, see [Upgrade Full-Text Search](../search/upgrade-full-text-search.md).

When you attach a database, all data files (`.mdf` and `.ndf` files) must be available. If any data file has a different path from when the database was first created or last attached, you must specify the current path of the file.

> [!NOTE]  
> If the primary data file being attached is read-only, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] assumes that the database is read-only.

When an encrypted database is first attached to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the database owner must open the database master key (DMK) by executing the following statement: `OPEN MASTER KEY DECRYPTION BY PASSWORD = 'password'`. We recommend that you enable automatic decryption of the DMK by executing the following statement: `ALTER MASTER KEY ADD ENCRYPTION BY SERVICE MASTER KEY`. For more information, see [CREATE MASTER KEY (Transact-SQL)](../../t-sql/statements/create-master-key-transact-sql.md) and [ALTER MASTER KEY (Transact-SQL)](../../t-sql/statements/alter-master-key-transact-sql.md).

The requirement for attaching log files depends partly on whether the database is read-write or read-only, as follows:

- For a read-write database, you can usually attach a log file in a new location. However, in some cases, reattaching a database requires its existing log files. Therefore, it's important to always keep all the detached log files until the database is successfully attached without them.

  If a read-write database has a single log file and you don't specify a new location for the log file, the attach operation looks in the old location for the file. If the old log file is found, it is used regardless of whether the database was shut down cleanly. However, if the old log file isn't found, and if the database was shut down cleanly and has no active log chain, the attach operation attempts to build a new log file for the database.

- If the primary data file being attached is read-only, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] assumes that the database is read-only. For a read-only database, the log file or files must be available at the location specified in the primary file of the database. A new log file can't be built because [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can't update the log location stored in the primary file.

### <a id="Metadata"></a> Metadata changes on attaching a database

When a read-only database is detached and then reattached, the backup information about the current differential base is lost. The *differential base* is the most recent full backup of all the data in the database or in a subset of the files or filegroups of the database. Without the base-backup information, the `master` database becomes unsynchronized with the read-only database, so differential backups taken thereafter might provide unexpected results. Therefore, if you're using differential backups with a read-only database, you should establish a new differential base by taking a full backup after you reattach the database. For information about differential backups, see [Differential backups (SQL Server)](../backup-restore/differential-backups-sql-server.md).

On attach, database startup occurs. Generally, attaching a database places it in the same state that it was in when it was detached or copied. However, attach-and-detach operations both disable cross-database ownership chaining for the database. For information about how to enable chaining, see [cross db ownership chaining Server Configuration Option](../../database-engine/configure-windows/cross-db-ownership-chaining-server-configuration-option.md).

> [!IMPORTANT]  
> By default and for security, the options for *is_broker_enabled*, *is_honor_broker_priority_on* and *is_trustworthy_on* are set to OFF whenever the database is attached. For information about how to set these options on, see [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md). For more information about metadata, see [Manage Metadata When Making a Database Available on Another Server](manage-metadata-when-making-a-database-available-on-another-server.md).

### Backup, restore, and attach

Like any database that is fully or partially offline, a database with restoring files can't be attached. If you stop the restore sequence, you can attach the database. Then, you can restart the restore sequence.

### <a id="OtherServerInstance"></a> Attach a database to another server instance

> [!IMPORTANT]  
> A database created by a more recent version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can't be attached in earlier versions. This precludes the database from being physically used with an older version of the [!INCLUDE [ssDE-md](../../includes/ssde-md.md)]. However, this relates to metadata state and doesn't affect the [database compatibility level](view-or-change-the-compatibility-level-of-a-database.md). For more information, see [ALTER DATABASE (Transact-SQL) compatibility level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

To provide a consistent experience to users and applications, when you attach a database onto another server instance, you might have to recreate some or all of the metadata for the database on the other server instance. This metadata includes such as logins and jobs. For more information, see [Manage Metadata When Making a Database Available on Another Server](manage-metadata-when-making-a-database-available-on-another-server.md).

## Related tasks

| Task | Article |
| --- | --- |
| Detach a database | - [sp_detach_db (Transact-SQL)](../system-stored-procedures/sp-detach-db-transact-sql.md)<br />- [Detach a database](detach-a-database.md) |
| Attach a database | - [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)<br />- [Attach a Database](attach-a-database.md)<br />- [sp_attach_db (Transact-SQL)](../system-stored-procedures/sp-attach-db-transact-sql.md)<br />- [sp_attach_single_file_db (Transact-SQL)](../system-stored-procedures/sp-attach-single-file-db-transact-sql.md) |
| Upgrade a database using detach and attach operations | - [Upgrade a database using detach and attach (Transact-SQL)](upgrade-a-database-using-detach-and-attach-transact-sql.md) |
| Move a database using detach and attach operations | - [Move a database using detach and attach (Transact-SQL)](move-a-database-using-detach-and-attach-transact-sql.md) |
| Delete a database snapshot | - [Drop a Database Snapshot (Transact-SQL)](drop-a-database-snapshot-transact-sql.md) |

## Related content

- [Database Files and Filegroups](database-files-and-filegroups.md)
