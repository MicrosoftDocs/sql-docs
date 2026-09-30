---
title: Migrate SQL Server Database from Windows to Linux
description: This tutorial shows how to take a SQL Server database backup on Windows and restore it to a Linux machine running SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/07/2026
ms.service: sql
ms.subservice: linux
ms.topic: upgrade-and-migration-article
ms.custom:
  - intro-migration
  - linux-related-content
  - sfi-image-nochange
---
# Migrate a SQL Server database from Windows to Linux using backup and restore

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

SQL Server's backup and restore feature is the recommended way to migrate a database from [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Windows to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux. In this tutorial, you walk through the steps required to move a database to Linux with backup and restore techniques.

> [!div class="checklist"]
> - Create a backup file on Windows with [SQL Server Management Studio (SSMS)](/ssms/install/install)
> - Install a Bash shell on Windows
> - Move the backup file to Linux from the Bash shell
> - Restore the backup file on Linux with Transact-SQL (T-SQL)
> - Run a query to verify the migration

You can also create a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Always On Availability Group to migrate a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] database from Windows to Linux. See [Configure SQL Server Always On availability group on Windows and Linux (cross-platform)](../business-continuity/availability-groups/cross-platform.md).

## Prerequisites

The following prerequisites are required to complete this tutorial:

- On a Windows machine:
  - [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) installed.
  - [SQL Server Management Studio](/ssms/install/install) installed.
  - Target database to migrate.

- On a Linux machine:
  - [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] ([Red Hat Enterprise Linux](../install-upgrade/quickstart-install-red-hat.md), [SUSE Linux Enterprise Server](../install-upgrade/quickstart-install-suse.md), or [Ubuntu](../install-upgrade/quickstart-install-ubuntu.md)) with command-line tools.

  > [!NOTE]  
  > Starting in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], SUSE Linux Enterprise Server (SLES) isn't supported.

## Create a backup on Windows

There are several ways to create a backup file of a database on Windows. The following steps use SQL Server Management Studio (SSMS).

1. Start **SQL Server Management Studio** on your Windows machine.

1. In the connection dialog, enter **localhost**.

1. In Object Explorer, expand **Databases**.

1. Right-click your target database, select **Tasks**, and then select **Back Up...**.

   :::image type="content" source="media/restore-database/ssms-create-backup.png" alt-text="Screenshot of using SSMS to create a backup file.":::

1. In the **Back Up Database** dialog, verify that the **Backup type** option is **Full**, and the **Back up to** option is **Disk**. Note the name and location of the file. For example, a database named `YourDB` on [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] has a default backup path of `C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Backup\YourDB.bak`.

1. Select **OK** to back up your database.

Another option is to run a T-SQL query to create the backup file. The following T-SQL command performs the same actions as the previous steps for a database called `YourDB`:

```sql
BACKUP DATABASE [YourDB]
    TO DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Backup\YourDB.bak'
    WITH NOFORMAT, NOINIT, NAME = N'YourDB-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10;
GO
```

## Install a Bash shell on Windows

To restore the database, you must first transfer the backup file from the Windows machine to the target Linux machine. In this tutorial, we move the file to Linux from a Bash shell (terminal window) running on Windows.

1. Install a Bash shell on your Windows machine that supports the **scp** (secure copy) and **ssh** (remote sign in) commands. Two examples include:

   - The [Windows Subsystem for Linux](/windows/wsl/about)
   - The [Git Bash shell](https://git-scm.com/install/windows)

1. Open a Bash session on Windows.

<a id="scp"></a>

## Copy the backup file to Linux

1. In your Bash session, navigate to the directory containing your backup file. For example:

   ```bash
   cd 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Backup\'
   ```

1. Use the **scp** command to transfer the file to the target Linux machine. The following example transfers `YourDB.bak` to the home directory of `user1` on the Linux server with an IP address of *192.168.2.9*:

   ```bash
   scp YourDB.bak user1@192.168.2.9:./
   ```

   Here's the expected output:

   ```output
   The authenticity of host '192.168.2.9 (192.168.2.9)' can't be established.
   ECDSA key fingerprint is SHA256: aB1cD2eF-3gH4iJ5kL6-mN7oP8qR=
   Are you sure you want to continue connecting (yes/no)? yes
   Warning: Permanently added '192.168.2.9' (ECDSA) to the list of known hosts.
   Password:
   YourDB.bak                                      100% 8960KB 7.4MB/s 00:01
   ```

> [!TIP]  
> There are alternatives to using **scp** for file transfer. One is to use [Samba](https://help.ubuntu.com/community/Samba) to configure an SMB network share between Windows and Linux. For a walkthrough on Ubuntu, see [Samba as a file server](https://ubuntu.com/server/docs/samba-as-a-file-server). Once established, you can access it as a network file share from Windows, such as `\\machinenameorip\share`.

## Move the backup file before restoring

At this point, the backup file is on your Linux server in your user's home directory. Before restoring the database to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], you must place the backup in a subdirectory of `/var/opt/mssql`, as this directory is owned by the user `mssql` and group `mssql`. If you're looking to change the default backup location, see the [Configure with mssql-conf](../configure/mssql-conf.md#backupdir) article.

1. In the same Windows Bash session, connect remotely to your target Linux machine with **ssh**. The following example connects to the Linux machine `192.168.2.9` as user `user1`.

   ```bash
   ssh user1@192.168.2.9
   ```

   You're now running commands on the remote Linux server.

1. Enter superuser mode.

   ```bash
   sudo su
   ```

1. Create a new backup directory. The `-p` parameter does nothing if the directory already exists.

   ```bash
   mkdir -p /var/opt/mssql/backup
   ```

1. Move the backup file to that directory. In the following example, the backup file resides in the home directory of `user1`. Change the command to match the location and file name of your backup file.

   ```bash
   mv /home/user1/YourDB.bak /var/opt/mssql/backup/
   ```

1. Exit superuser mode.

   ```bash
   exit
   ```

## Restore your database on Linux

To restore the database backup, you can use the `RESTORE DATABASE` T-SQL command.

The following steps use the **`sqlcmd`** tool. If you haven't installed [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] tools, see [Install the sqlcmd and bcp SQL Server command-line tools on Linux](../install-upgrade/setup-tools.md).

1. In the same terminal, launch **`sqlcmd`**. The following example connects to the local [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance with the `sa` account. Enter the password when prompted, or specify the password by adding the `-P` parameter.

   ```bash
   sqlcmd -S localhost -U sa
   ```

1. At the `>1` prompt, enter the following `RESTORE DATABASE` command, pressing ENTER after each line (you can't copy and paste the entire multi-line command at once). Replace all occurrences of `YourDB` with the name of your database.

   ```sql
   RESTORE DATABASE YourDB FROM DISK = '/var/opt/mssql/backup/YourDB.bak'
       WITH MOVE 'YourDB' TO '/var/opt/mssql/data/YourDB.mdf',
       MOVE 'YourDB_Log' TO '/var/opt/mssql/data/YourDB_Log.ldf';
   GO
   ```

   You should get a message that the database is successfully restored.

   `RESTORE DATABASE` might return an error like the following example:

   ```output
   File 'YourDB_Product' cannot be restored to 'Z:\Microsoft SQL Server\MSSQL15.GLOBAL\MSSQL\Data\YourDB\YourDB_Product.ndf'. Use WITH MOVE to identify a valid location for the file.
   Msg 5133, Level 16, State 1, Server servername, Line 1
   Directory lookup for the file "Z:\Microsoft SQL Server\MSSQL15.GLOBAL\MSSQL\Data\YourDB\YourDB_Product.ndf" failed with the operating system error 2(The system cannot find the file specified.).
   ```

   In this case, the database contains secondary files. If these files aren't specified in the `MOVE` clause of `RESTORE DATABASE`, the restore procedure tries to create them in the same path as the original server.

   You can list all files included in the backup:

   ```sql
   RESTORE FILELISTONLY FROM DISK = '/var/opt/mssql/backup/YourDB.bak';
   GO
   ```

   You should get a list like the following example (listing only the first two columns):

   ```output
   LogicalName         PhysicalName                                                                 ..............
   ------------------- ---------------------------------------------------------------------------- ---------------
   YourDB              Z:\Microsoft SQL Server\MSSQL15.GLOBAL\MSSQL\Data\YourDB\YourDB.mdf          ..............
   YourDB_Product      Z:\Microsoft SQL Server\MSSQL15.GLOBAL\MSSQL\Data\YourDB\YourDB_Product.ndf  ..............
   YourDB_Customer     Z:\Microsoft SQL Server\MSSQL15.GLOBAL\MSSQL\Data\YourDB\YourDB_Customer.ndf ..............
   YourDB_Log          Z:\Microsoft SQL Server\MSSQL15.GLOBAL\MSSQL\Data\YourDB\YourDB_Log.ldf      ..............
   ```

   You can use this list to create `MOVE` clauses for the extra files. In this example, the `RESTORE DATABASE` is:

   ```sql
   RESTORE DATABASE YourDB FROM DISK = '/var/opt/mssql/backup/YourDB.bak'
       WITH MOVE 'YourDB' TO '/var/opt/mssql/data/YourDB.mdf',
       MOVE 'YourDB_Product' TO '/var/opt/mssql/data/YourDB_Product.ndf',
       MOVE 'YourDB_Customer' TO '/var/opt/mssql/data/YourDB_Customer.ndf',
       MOVE 'YourDB_Log' TO '/var/opt/mssql/data/YourDB_Log.ldf';
   GO
   ```

1. Verify the restoration by listing all of the databases on the server. The restored database should be listed.

   ```sql
   SELECT name
   FROM sys.databases;
   GO
   ```

1. Run other queries on your migrated database. The following command switches context to the `YourDB` database and selects rows from one of its tables.

   ```sql
   USE YourDB;

   SELECT *
   FROM YourTable;
   GO
   ```

1. When you're done using **`sqlcmd`**, type `exit`.

1. When you're done working in the remote **ssh** session, type `exit` again.

## Next step

In this tutorial, you learned how to back up a database on Windows and move it to a Linux server running SQL Server. You learned how to:

> [!div class="checklist"]
> - Use SSMS and T-SQL to create a backup file on Windows
> - Install a Bash shell on Windows
> - Use **scp** to move backup files from Windows to Linux
> - Use **ssh** to remotely connect to your Linux machine
> - Relocate the backup file to prepare for restore
> - Use **`sqlcmd`** to run T-SQL commands
> - Restore the database backup with the `RESTORE DATABASE` command
> - Run a query to verify the migration

Next, explore other migration scenarios for SQL Server on Linux.

> [!div class="nextstepaction"]
> [Migrate databases and structured data to SQL Server on Linux](overview.md)
