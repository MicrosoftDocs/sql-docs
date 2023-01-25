---
title: Backup and restore SQL Server databases on Linux
description: Learn how to backup and restore SQL Server databases on Linux. Also learn how to backup and restore with SQL Server Management Studio (SSMS).
author: VanMSFT
ms.author: vanto
ms.reviewer: vanto
ms.date: 03/31/2022
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
ms.assetid: d30090fb-889f-466e-b793-5f284fccc4e6
---
# Backup and restore SQL Server databases on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

You can take backups of databases from SQL Server on Linux with many different options. On a Linux server, you can use **sqlcmd** to connect to the SQL Server and take backups. From Windows, you can connect to SQL Server on Linux and take backups with the user interface. The backup functionality is the same across platforms. For example, you can backup databases locally, to remote drives, or to [Microsoft Azure Blob Storage](../relational-databases/backup-restore/sql-server-backup-to-url.md).

> [!IMPORTANT]
> SQL Server on Linux only supports backing up to Azure Blob storage using block blobs. Using a storage key for backup and restore will result in a page blog being used, which isn't supported. Use a Shared Access Signature instead. For information on block blogs versus page blogs, see [Backup to block blob vs. page blob](../relational-databases/backup-restore/sql-server-backup-to-url.md#blockbloborpageblob).

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

In the following example **sqlcmd** connects to the local instance of SQL Server and restores the demodb database. The `NORECOVERY` option is used to allow for additional restores of log file backups. If you don't plan to restore additional log files, remove the `NORECOVERY` option.

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

## Backup and restore with SQL Server Management Studio (SSMS)

You can use [SSMS](../ssms/download-sql-server-management-studio-ssms.md) from a Windows computer to connect to a Linux database and take a backup through the user-interface.

>[!NOTE] 
> Use the latest version of SSMS to connect to SQL Server. To download and install the latest version, see [Download SSMS](../ssms/download-sql-server-management-studio-ssms.md). For more information on how to use SSMS, see [Use SSMS to Manage SQL Server on Linux](sql-server-linux-manage-ssms.md).

The following steps walk through taking a backup with SSMS. 

1. Start SSMS and connect to your SQL Server on Linux instance.

1. In **Object Explorer**, right-click on your database, select **Tasks**, and then select **Back Up...**.

1. In the **Backup Up Database** dialog, verify the parameters and options, and select **OK**.
 
SQL Server completes the database backup.

### Restore with SQL Server Management Studio (SSMS) 

The following steps walk you through restoring a database with SSMS.

1. In SSMS right-click **Databases** and select **Restore Databases...**. 

1. Under **Source**, select **Device:** and then select the ellipses (...).

1. Locate your database backup file and select **OK**. 

1. Under **Restore plan**, verify the backup file and settings. Select **OK**. 

1. SQL Server restores the database. 

## See also

* [Create a Full Database Backup (SQL Server)](../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)
* [Back up a Transaction Log (SQL Server)](../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)
* [BACKUP (Transact-SQL)](../t-sql/statements/backup-transact-sql.md)
* [SQL Server Backup to URL](../relational-databases/backup-restore/sql-server-backup-to-url.md)
