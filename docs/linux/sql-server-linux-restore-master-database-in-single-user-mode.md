---
title: "Restore master database on SQL Server in single-user mode on Linux"
description: "Learn how to restore the master database using single-user mode in SQL Server on Linux."
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/09/2022
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "starting SQL Server on Linux, single-user mode"
  - "single-user mode [SQL Server] Linux"
ms.custom: template-how-to
---
# Restore the master database on Linux in single-user mode

[!INCLUDE [sql-linux](../includes/applies-to-version/sql-linux.md)]

Under certain circumstances, you may need to restore the `master` database on an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] in single-user mode on Linux. Scenarios include migrating to a new instance, or recovering from inconsistencies.

> [!NOTE]  
> [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] will automatically shut down after the restore is complete. This behavior is by design.

Restoring the `master` database requires starting [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] in single-user mode, by using the **startup option** `-m` from the command line.

For starting a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance in single-user mode on Windows, see [Start SQL Server in single-user mode](../database-engine/configure-windows/start-sql-server-in-single-user-mode.md).

## Prerequisites

Starting [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] in single-user mode enables any member of the local administrator group to connect to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] as a member of the **sysadmin** fixed server role. For more information, see [Connect to SQL Server when system administrators are locked out](../database-engine/configure-windows/connect-to-sql-server-when-system-administrators-are-locked-out.md).

When you start an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] in single-user mode, note the following:

- Only one user can connect to the server.

- The CHECKPOINT process isn't executed. By default, it is executed automatically at startup.

## Stop the SQL Server service

1. The following command stops the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance if it's currently running:

   ```bash
   systemctl stop mssql-server
   ```

## Change current user to `mssql`

1. [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux runs under the `mssql` user, so you'll need to switch to this user first. You'll be prompted for the `root` password when running this command.

   ```bash
   su mssql
   ```

## Start SQL Server in single-user mode

1. When you use the `-m` option with `SQLCMD`, you can limit the connections to a specified client application (`SQLCMD` must be capitalized as shown):

   ```bash
   /opt/mssql/bin/sqlservr -m"SQLCMD"
   ```

   In the previous example, `-m"SQLCMD"` limits connections to a single connection and that connection must identify itself as the **sqlcmd** client program. Use this option when you're starting [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] in single-user mode to restore a `master` database.

1. When SQL Server starts up, it generates several log entries. You can confirm that it is running in single-user mode by looking for the following lines in the output:

   ```output
   [...]
   2022-05-24 04:26:27.24 Server      Command Line Startup Parameters: 
            -m "SQLCMD" 
   [...]
   2022-05-24 04:26:28.20 spid8s      Warning ******************
   2022-05-24 04:26:28.21 spid8s      SQL Server started in single-user mode. This an informational message only. No user action is required. 
   ```

## Connect to the SQL Server instance

1. Use **sqlcmd** to connect to the SQL Server instance:

   ```bash
   /opt/mssql-tools/bin/sqlcmd -S <ServerName> -U sa -P <StrongPassword> 
   ```

   In the previous example, `<ServerName>` is the name of the host running SQL Server if you're connecting remotely. If you're connecting directly on the host where SQL Server is running, you can skip this parameter, or use `localhost`. `<StringPassword>` is the password for the **SA** account.

## Restore the `master` database

1. Run the following commands inside **sqlcmd**. Remember that **sqlcmd** expects `GO` at the end of the script to execute it.

   ```sql
   use [master];
   RESTORE DATABASE [master] FROM DISK = N'/var/opt/mssql/data/master.bak' WITH FILE=1, 
   MOVE N'master' to N'/var/opt/mssql/data/master.mdf', 
   MOVE N'mastlog' to N'/var/opt/mssql/data/mastlog.ldf', NOUNLOAD, REPLACE, STATS=5;
   GO
   ```

   In the previous example, the path to the `master` database backup file is `/var/opt/mssql/data/master.bak`. You'll need to replace this value with the correct path to your `master` database backup file.

2. You should see output similar to the following example, if the restore is successful.

   ```output
   Processed 456 pages for database 'master', file 'master' on file 1.
   Processed 5 pages for database 'master', file 'mastlog' on file 1.
   The master database has been successfully restored. Shutting down SQL Server.
   SQL Server is terminating this process.
   ```

## Restart the SQL Server service

1. To restart SQL Server, run the following command.

   ```bash
   systemctl start mssql-server
   ```

## Remarks

When you restore a `master` database backup, any existing user databases that were added to the instance after the backup was taken, won't be visible after restoring `master`. The files should still exist on the storage layer, so you'll need to manually reattach those user database files to bring those databases online. See [Attach a Database](../relational-databases/databases/attach-a-database.md) for more information.

## Next steps

- [Troubleshoot SQL Server on Linux](sql-server-linux-troubleshooting-guide.md)
- [sqlcmd Utility](../tools/sqlcmd-utility.md)
- [Start, stop, and restart SQL Server services on Linux](sql-server-linux-start-stop-restart-sql-server-services.md)
