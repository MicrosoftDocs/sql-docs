---
title: Backup and restore SQL Server databases on Linux | Microsoft Docs
description: Learn how to backup and restore SQL Server databases on Linux.
author: MikeRayMSFT 
ms.author: mikeray 
manager: craigg
ms.date: 11/14/2017
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
ms.assetid: d30090fb-889f-466e-b793-5f284fccc4e6
---
# Backup and restore SQL Server databases on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

You can take backups of databases from SQL Server 2017 on Linux with the same tools as other platforms. On a Linux server, you can use **sqlcmd** to connect to the SQL Server and take backups. From Windows, you can connect to SQL Server on Linux and take backups with the user interface. The backup functionality is the same across platforms. For example, you can backup databases locally, to remote drives, or to [Microsoft Azure Blob storage service](../relational-databases/backup-restore/sql-server-backup-to-url.md).

## Backup a database

In the following example **sqlcmd** connects to the local SQL Server instance and takes a full backup of a user database called `demodb`.

```bash
sqlcmd -S localhost -U SA -Q "BACKUP DATABASE [demodb] TO DISK = N'/var/opt/mssql/data/demodb.bak' WITH NOFORMAT, NOINIT, NAME = 'demodb-full', SKIP, NOREWIND, NOUNLOAD, STATS = 10"
```

When you run the command, SQL Server will prompt for a password. After you enter the password, the shell will return the results of the backup progress. For example:

```
Password:
10 percent processed.
21 percent processed.
32 percent processed.
40 percent processed.
51 percent processed.
61 percent processed.
72 percent processed.
80 percent processed.
91 percent processed.
Processed 296 pages for database 'demodb', file 'demodb' on file 1.
100 percent processed.
Processed 2 pages for database 'demodb', file 'demodb_log' on file 1.
BACKUP DATABASE successfully processed 298 pages in 0.064 seconds (36.376 MB/sec).
```

### Backup the transaction log

If your database is in the full recovery model, you can also make transaction log backups for more granular restore options. In the following example, **sqlcmd** connects to the local SQL Server instance and takes a transaction log backup.

```bash
sqlcmd -S localhost -U SA -Q "BACKUP LOG [demodb] TO DISK = N'/var/opt/mssql/data/demodb_LogBackup.bak' WITH NOFORMAT, NOINIT, NAME = N'demodb_LogBackup', NOSKIP, NOREWIND, NOUNLOAD, STATS = 5"
```

## Restore a database

In the following example **sqlcmd** connects to the local instance of SQL Server and restores the demodb database. Note that the `NORECOVERY` option is used to allow for additional restores of log file backups. If you do not plan to restore additional log files, remove the `NORECOVERY` option.

```bash
sqlcmd -S localhost -U SA -Q "RESTORE DATABASE [demodb] FROM DISK = N'/var/opt/mssql/data/demodb.bak' WITH FILE = 1, NOUNLOAD, REPLACE, NORECOVERY, STATS = 5"
```

> [!TIP]
> If you accidentally use NORECOVERY but do not have additional log file backups, run the command `RESTORE DATABASE demodb` with no additional parameters. This finishes the restore and leaves your database operational.

### Restore the transaction log

The following command restores the previous transaction log backup.

```bash
sqlcmd -S localhost -U SA -Q "RESTORE LOG demodb FROM DISK = N'/var/opt/mssql/data/demodb_LogBackup.bak'"
```

## Backup and Restore with SQL Server Management Studio (SSMS)

You can use SSMS from a Windows computer to connect to a Linux database and take a backup through the user-interface.

>[!NOTE] 
> Use the latest version of SSMS to connect to SQL Server. To download and install the latest version, see [Download SSMS](../ssms/download-sql-server-management-studio-ssms.md). For more information on how to use SSMS, see [Use SSMS to Manage SQL Server on Linux](sql-server-linux-manage-ssms.md).

The following steps walk through taking a backup with SSMS. 

1. Start SSMS and connect to your server in SQL Server 2017 on Linux.

1. In Object Explorer, right-click on your database, Click **Tasks**, and then click **Back Up...**.

1. In the **Backup Up Database** dialog, verify the parameters and options, and click **OK**.
 
SQL Server completes the database backup.

### Restore with SQL Server Management Studio (SSMS) 

The following steps walk you through restoring a database with SSMS.

1. In SSMS right-click **Databases** and click **Restore Databases...**. 

1. Under **Source** click **Device:** and then click the ellipses (...).

1. Locate your database backup file and click **OK**. 

1. Under **Restore plan**, verify the backup file and settings. Click **OK**. 

1. SQL Server restores the database. 

## See also

* [Create a Full Database Backup (SQL Server)](../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)
* [Back up a Transaction Log (SQL Server)](../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)
* [BACKUP (Transact-SQL)](../t-sql/statements/backup-transact-sql.md)
* [SQL Server Backup to URL](../relational-databases/backup-restore/sql-server-backup-to-url.md)
