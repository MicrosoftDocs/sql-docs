---
title: "Restore the master Database (Transact-SQL)"
description: This article shows you how to restore the master database in SQL Server from a full database backup by using Transact-SQL.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 04/17/2025
ms.service: sql
ms.subservice: backup-restore
ms.topic: how-to
helpviewer_keywords:
  - "master database [SQL Server], restoring"
---
# Restore the master database (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article explains how to restore the `master` database from a full database backup.

> [!WARNING]  
> In the event of disaster recovery, the instance where the `master` database is being restored to should be as close to an exact match to the original as possible. At a minimum, this recovery instance should be the same version, edition, and patch level, and it should have the same selection of features and the same external configuration (hostname, cluster membership, and so on) as the original instance. Doing otherwise can result in undefined SQL Server instance behavior, with inconsistent feature support, and isn't guaranteed to be viable.

## Restore the `master` database

1. Start the server instance in single-user mode.

   You can start SQL Server by either using the `-m` or `-f` startup parameters. For more information about startup parameters, see [Database Engine Service startup options](../../database-engine/configure-windows/database-engine-service-startup-options.md).

   From a command prompt, run the following commands, and make sure you replace `MSSQLXX.instance` with the appropriate folder name:

   ```console
   cd C:\Program Files\Microsoft SQL Server\MSSQLXX.instance\MSSQL\Binn
   sqlservr -c -f -s <instance> -mSQLCMD
   ```

   - The `-mSQLCMD` parameter ensures that only **sqlcmd** can connect to SQL Server.
   - For a default instance name, use `-s MSSQLSERVER`
   - `-c` starts SQL Server as an application to bypass Service Control Manager to shorten startup time

   If the SQL Server instance can't start due to a damaged `master` database, you must rebuild the system databases first. For more information, see [Rebuild system databases](../databases/rebuild-system-databases.md).

1. Connect to SQL Server using **sqlcmd** from another command prompt window:

   ```console
   sqlcmd -S <instance> -E -d master
   ```

1. To restore a full database backup of `master`, use the following [RESTORE Statements](../../t-sql/statements/restore-statements-transact-sql.md)[!INCLUDE [tsql](../../includes/tsql-md.md)] statement:

   ```sql
   RESTORE DATABASE master FROM <backup_device> WITH REPLACE;
   ```

   The `REPLACE` option instructs [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to restore the specified database even when a database of the same name already exists. The existing database, if any, is deleted. In single-user mode, we recommend that you enter the `RESTORE DATABASE` statement in the [sqlcmd utility](../../tools/sqlcmd/sqlcmd-utility.md). For more information, see [Use sqlcmd](../../tools/sqlcmd/sqlcmd-use-utility.md).

   > [!IMPORTANT]  
   > After `master` is restored, the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] shuts down and terminates the `sqlcmd` process. Before you restart the server instance, remove the single-user startup parameter. For more information, see [SQL Server Configuration Manager: Configure server startup options](../../database-engine/configure-windows/scm-services-configure-server-startup-options.md).

1. Restart the server instance normally as a service, without using any startup parameters.

1. Continue other recovery steps such as restoring other databases, attaching databases, and correcting user mismatches.

## Examples

The following example restores the `master` database on the default server instance. The example assumes that the server instance is already running in single-user mode. The example starts **sqlcmd** and executes a `RESTORE DATABASE` statement that restores a full database backup of `master` from a disk device: `Z:\SQLServerBackups\master.bak`.

For a named instance, the **sqlcmd** command must specify the `-S<computer-name>\<instance-name>` option.

```console
C:\> sqlcmd
1> RESTORE DATABASE master FROM DISK = 'Z:\SQLServerBackups\master.bak' WITH REPLACE;
2> GO
```

## Related content

- [Complete Database Restores (Simple Recovery Model)](complete-database-restores-simple-recovery-model.md)
- [Complete Database Restores (Full Recovery Model)](complete-database-restores-full-recovery-model.md)
- [Troubleshoot orphaned users (SQL Server)](../../sql-server/failover-clusters/troubleshoot-orphaned-users-sql-server.md)
- [Database detach and attach (SQL Server)](../databases/database-detach-and-attach-sql-server.md)
- [Rebuild system databases](../databases/rebuild-system-databases.md)
- [Database Engine Service startup options](../../database-engine/configure-windows/database-engine-service-startup-options.md)
- [SQL Server Configuration Manager](../../tools/configuration-manager/sql-server-configuration-manager.md)
- [Back up and restore: System databases (SQL Server)](back-up-and-restore-of-system-databases-sql-server.md)
- [RESTORE Statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
- [Single-user mode for SQL Server](../../database-engine/configure-windows/start-sql-server-in-single-user-mode.md)
