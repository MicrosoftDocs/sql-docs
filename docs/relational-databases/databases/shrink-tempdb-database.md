---
title: Shrink the tempdb database
description: Learn how to shrink the tempdb database by using Transact-SQL.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/25/2024
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.shrinkdatabase.f1"
helpviewer_keywords:
  - "shrinking tempdb database"
  - "tempdb database [SQL Server], shrinking"
  - "decreasing tempdb database size"
  - "tempdb database shrinking [SQL Server]"
  - "reducing tempdb database size"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Shrink the tempdb database

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This article discusses various methods that you can use to shrink the `tempdb` database in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

You can use any of the following methods to alter the size of `tempdb`. The first three options are described in this article. If you want to use [!INCLUDE [ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)] (SSMS), follow the instructions in [Shrink a database](shrink-a-database.md).

| Method | Requires restart? | More information |
| --- | --- | --- |
| `ALTER DATABASE` | Yes | Gives complete control on the size of the default `tempdb` files (`tempdev` and `templog`). |
| `DBCC SHRINKDATABASE` | No | Operates at database level. |
| `DBCC SHRINKFILE` | No | Lets you shrink individual files. |
| SQL Server Management Studio | No | Shrink database files through a graphical user interface. |

## Remarks

By default, the `tempdb` database is configured to autogrow as needed. Therefore, this database might unexpectedly grow in time to a size larger than the desired size. Larger `tempdb` database sizes don't adversely affect the performance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

When [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] starts, `tempdb` is re-created by using a copy of the `model` database, and `tempdb` is reset to its last configured size. The configured size is the last explicit size that was set by using a file size changing operation such as `ALTER DATABASE` that uses the `MODIFY FILE` option or the `DBCC SHRINKFILE` or `DBCC SHRINKDATABASE` statements. Therefore, unless you have to use different values or obtain immediate resolution to a large `tempdb` database, you can wait for the next restart of the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service for the size to decrease.

You can shrink `tempdb` while `tempdb` activity is ongoing. However, you might encounter other errors such as blocking, deadlocks, and so on, that can prevent shrink from completing. Therefore, in order to make sure that a shrink of `tempdb` succeeds, we recommend that you do this while the server is in single-user mode or when you stop all `tempdb` activity.

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] records only enough information in the `tempdb` transaction log to roll back a transaction, but not to redo transactions during database recovery. This feature increases the performance of `INSERT` statements in `tempdb`. Additionally, you don't have to log information to redo any transactions because `tempdb` is re-created every time that you restart [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. Therefore, it has no transactions to roll forward or to roll back.

For more information about managing and monitoring `tempdb`, see [Capacity planning](tempdb-database.md#capacity-planning-for-tempdb-in-sql-server) and [Monitor tempdb use](tempdb-database.md#monitoring-tempdb-use).

## Use the ALTER DATABASE command

> [!NOTE]  
> This command operates only on the default `tempdb` logical files `tempdev` and `templog`. If more files are added to `tempdb`, you can shrink them after you restart [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] as a service. All `tempdb` files are re-created during startup. However, they are empty and can be removed. To remove additional files in `tempdb`, use the `ALTER DATABASE` command with the `REMOVE FILE` option.

This method requires you to restart [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

1. Stop [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

1. At a command prompt, start the instance in minimum configuration mode. To do this, follow these steps:

   1. At a command prompt, change to the folder where [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is installed (replace `<VersionNumber>` and `<InstanceName>` in the following example):

      ```console
      cd C:\Program Files\Microsoft SQL Server\MSSQL<VersionNumber>.<InstanceName>\MSSQL\Binn
      ```

   1. If the instance is a named instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], run the following command (replace `<InstanceName>` in the following example):

      ```console
      sqlservr.exe -s <InstanceName> -c -f -mSQLCMD
      ```

   1. If the instance is the default instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], run the following command:

      ```console
      sqlservr -c -f -mSQLCMD
      ```

      > [!NOTE]  
      > The `-c` and `-f` parameters cause [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to start in a minimum configuration mode that has a `tempdb` size of 1 MB for the data file, and 0.5 MB for the log file. The `-mSQLCMD` parameter prevents any other application than **sqlcmd** from taking over the single-user connection.

1. Connect to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] with **sqlcmd**, and then run the following Transact-SQL commands. Replace `<target_size_in_MB>` with the desired size:

   ```sql
   ALTER DATABASE tempdb MODIFY FILE
   (NAME = 'tempdev', SIZE = <target_size_in_MB>);

   ALTER DATABASE tempdb MODIFY FILE
   (NAME = 'templog', SIZE = <target_size_in_MB>);
   ```

1. Stop [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. To do this, press `Ctrl+C` at the Command Prompt window, restart [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] as a service, and then verify the size of the `tempdb.mdf` and `templog.ldf` files.

## Use the DBCC SHRINKDATABASE command

`DBCC SHRINKDATABASE` receives the parameter `target_percent`. This is the desired percentage of free space left in the database file after the database is shrunk. If you use `DBCC SHRINKDATABASE`, you might have to restart [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

1. Determine the space that is currently used in `tempdb` by using the `sp_spaceused` stored procedure. Then, calculate the percentage of free space that is left for use as a parameter to `DBCC SHRINKDATABASE`. This calculation is based on the desired database size.

   > [!NOTE]  
   > In some cases, you might have to execute `sp_spaceused @updateusage = true` to recalculate the space that is used and to obtain an updated report. For more information, see [sp_spaceused](../system-stored-procedures/sp-spaceused-transact-sql.md).

   Consider the following example:

   Assume that `tempdb` has two files: the primary data file (`tempdb`.mdf) that is 1,024 MB and the log file (`tempdb.ldf`) that is 360 MB. Assume that `sp_spaceused` reports that the primary data file contains 600 MB of data. Also, assume that you want to shrink the primary data file to 800 MB. Calculate the desired percentage of free space left after the shrink: 800 MB - 600 MB = 200 MB. Now, divide 200 MB by 800 MB = 25 percent, and that is your `target_percent`. The transaction log file is shrunk accordingly, leaving 25 percent or 200 MB of space free after the database is shrunk.

1. Connect to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] with SSMS, Azure Data Studio, or **sqlcmd**, and then run the following Transact-SQL command. Replace `<target_percent>` with the desired percentage:

   ```sql
   DBCC SHRINKDATABASE (tempdb, '<target_percent>');
   ```

There are limitations with the `DBCC SHRINKDATABASE` command on `tempdb`. The target size for data and log files can't be smaller than the size that is specified when the database was created, or smaller than the last size that was explicitly set by using a file-size-changing operation such as `ALTER DATABASE` that uses the `MODIFY FILE` option. Another limitation of `DBCC SHRINKDATABASE` is the calculation of the `target_percentage` parameter and its dependency on the current space that is used.

## Use the DBCC SHRINKFILE command

Use the `DBCC SHRINKFILE` command to shrink the individual `tempdb` files. `DBCC SHRINKFILE` provides more flexibility than `DBCC SHRINKDATABASE` because you can use it on a single database file without affecting other files that belong to the same database. `DBCC SHRINKFILE` receives the `target_size` parameter. This is the desired final size for the database file.

1. Determine the desired size for the primary data file (`tempdb.mdf`), the log file (`templog.ldf`), and extra files that are added to `tempdb`. Make sure that the space that is used in the files is less than or equal to the desired target size.

1. Connect to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] with SSMS, Azure Data Studio, or **sqlcmd**, and then run the following Transact-SQL commands for the specific database files that you want to shrink. Replace `<target_size_in_MB>` with the desired size:

   ```sql
   USE tempdb;
   GO

   -- This command shrinks the primary data file
   DBCC SHRINKFILE (tempdev, '<target_size_in_MB>');
   GO

   -- This command shrinks the log file, examine the last paragraph.
   DBCC SHRINKFILE (templog, '<target_size_in_MB>');
   GO
   ```

An advantage of `DBCC SHRINKFILE` is that it can reduce the size of a file to a size that is smaller than its original size. You can issue `DBCC SHRINKFILE` on any of the data or log files. You can't make the database smaller than the size of the `model` database.

## Error 8909 when you run shrink operations

If `tempdb` is being used, and if you try to shrink it by using the `DBCC SHRINKDATABASE` or `DBCC SHRINKFILE` commands, you might receive messages that resemble the following, depending on the version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] that you're using:

```output
Server: Msg 8909, Level 16, State 1, Line 1 Table error: Object ID 0, index ID -1, partition ID 0, alloc unit ID 0 (type Unknown), page ID (6:8040) contains an incorrect page ID in its page header. The PageId in the page header = (0:0).
```

This error doesn't indicate any real corruption in `tempdb`. However, there might be other reasons for physical data corruption errors like error 8909, and that those reasons include I/O subsystem problems. Therefore, if the error happens outside shrink operations, you should do more investigation.

Although an 8909 message is returned to the application or to the user who is executing the shrink operation, the shrink operations don't fail.

## Related content

- [Considerations for the autogrow and autoshrink settings in SQL Server](/troubleshoot/sql/admin/considerations-autogrow-autoshrink)
- [Database Files and Filegroups](database-files-and-filegroups.md)
- [sys.databases (Transact-SQL)](../system-catalog-views/sys-databases-transact-sql.md)
- [sys.database_files (Transact-SQL)](../system-catalog-views/sys-database-files-transact-sql.md)
- [Shrink a database](shrink-a-database.md)
- [DBCC SHRINKDATABASE (Transact-SQL)](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md)
- [DBCC SHRINKFILE (Transact-SQL)](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md)
- [Delete Data or Log Files from a Database](delete-data-or-log-files-from-a-database.md)
- [Shrink a file](shrink-a-file.md)
