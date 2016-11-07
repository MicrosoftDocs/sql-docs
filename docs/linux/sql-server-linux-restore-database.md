---
# required metadata

title: Restore a SQL Server database from Windows to Linux | SQL Server vNext CTP1
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 11-07-2016
ms.topic: article
ms.prod: sql-linux
ms.service: 
ms.technology: 
ms.assetid: 

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---
# Restore a SQL Server database from Windows to Linux

This topic provides a walkthrough for restoring a database backup on Windows to SQL Server vNext CTP1 running on Linux. In this tutorial, you will:

- Download the AdventureWorks backup file on a Windows machine
- Transfer the backup to your Linux machine
- Restore the database using Transact-SQL commands

> [!NOTE] 
> This tutorial assumes that you have installed SQL Server vNext CTP1 and the SQL Server Tools on your target Linux server. If you have not done this, see [Install SQL Server on Linux](sql-server-linux-setup.md).

## Download the AdventureWorks database backup

Although you can use the same steps to restore any database, the AdventureWorks sample database provides a good example. It comes as an existing database backup file.

1. On your Windows machine, go to [https://msftdbprodsamples.codeplex.com/downloads/get/880661](https://msftdbprodsamples.codeplex.com/downloads/get/880661) and download the **Adventure Works 2014 Full Database Backup.zip**.

> [!TIP] Although this tutorial demonstrates backup and restore between Windows and Linux, you could also use a browser on Linux to directly download the AdventureWorks sample to your Linux machine.

2. Open the zip file, and extract the AdventureWorks2014.bak file to a folder on your machine.

> [!NOTE] 
> At this time, you cannot restore SQL Server 2016 database backups to SQL Server on Linux. Database backups must have been created with SQL Server 2014 or prior.

## Transfer the backup file to Linux

To restore the database, you must first transfer the backup file from the Windows machine to the target Linux machine.

1. For Windows, install a Bash shell. There are several options, including the following:

    - Download an open source Bash shell, such as [PuTTY](http://www.putty.org/).
    - Or, on Windows 10, use the new [built-in Bash shell (beta)](https://msdn.microsoft.com/en-us/commandline/wsl/about).
    - Or, if you work with Git, use the [Git Bash shell](https://git-scm.com/downloads).

2. Open a Bash shell (terminal) and navigate to the directory containing **AdventureWorks2014.bak**.

3. Use the **scp** (secure copy) command to transfer the file to the target Linux machine. The following example transfers **AdventureWorks2014.bak** to the home directory of *user1* on the server named *linuxserver1*.

        scp AdventureWorks2014.bak user1@linuxserver1:./

    In the previous example, you could instead provide the IP address in place of the server name.

There are several alternatives to using scp. One is to use [Samba](https://help.ubuntu.com/community/Samba) to setup an SMB network share between Windows and Linux. For a walkthrough on Ubuntu, see [How to Create a Network Share Via Samba](https://help.ubuntu.com/community/How%20to%20Create%20a%20Network%20Share%20Via%20Samba%20Via%20CLI%20%28Command-line%20interface/Linux%20Terminal%29%20-%20Uncomplicated,%20Simple%20and%20Brief%20Way!). Once established, you can access it as a network file share from Windows, such as **\\\\machinenameorip\\share**.

## Move the backup file

At this point, the backup file is on your Linux server. Before restoring the database to SQL Server, you must place the backup in a subdirectory of **/var/opt/mssql**. The **C:\** path is mapped to this location for compatibility with current Transact-SQL commands.

1. Open a Terminal on the taget Linux machine that contains the backup.

2.	Enter super user mode.

        sudo su

3.	Create a new backup directory. The -p parameter does nothing if the directory already exists.

        mkdir -p /var/opt/mssql/backup

4.	Move the backup file to that directory. In the following example, the backup file resides in the home directory of *user1*. Change the command to match the location of **AdventureWorks2014.bak** on your machine.

        mv /home/user1/AdventureWorks2014.bak /var/opt/mssql/backup/

5.	Exit super user mode.

        exit

## Restore the database backup

To restore the backup, you can use the RESTORE DATABASE Transact-SQL (TQL) command.

> [!NOTE] 
> The following steps use the sqlcmd tool. If you haven’t install SQL Server Tools, see [Install SQL Server on Linux](sql-server-linux-setup.md).

1. In the same terminal, launch **sqlcmd**. The following example connects to the local SQL Server instance with the *SA* user. Enter the password when prompted or specify the password with the -P parameter.

        sqlcmd -S localhost -U SA

2. After connecting, enter the following **RESTORE DATABSE** command, pressing ENTER after each line. The example below restores the **AdventureWorks2014.bak** file from the */var/opt/mssql/backup* directory.

        RESTORE DATABASE AdventureWorks 
        FROM DISK = 'C:\backup\AdventureWorks2014.bak' 
        WITH MOVE 'AdventureWorks2014_Data' TO 'C:\data\AdventureWorks2014_Data.mdf', 
        MOVE 'AdventureWorks2014_Log' TO 'C:\data\AdventureWorks2014_Log.ldf'

    You should get a message the database is successfully restored.

3. Verify the restoration by first changing the context to the AdventureWorks database. 

4. Run the following query that lists the top 10 products in the **Production.Products** table.

        SELECT TOP 10 Name, ProductNumber FROM Production.Product ORDER BY Name
        GO

![Output from Production.Products query](./media/sql-server-linux-restore-database/sql-server-linux-adventureworks-query.png)

## Next steps

For more information on SQL Server vNext running on Linux, see [SQL Server on Linux overview](sql-server-linux-overview.md). 
