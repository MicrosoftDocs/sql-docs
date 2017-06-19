---
title: Get started with SQL Server 2017 on Ubuntu | Microsoft Docs
description: This quick start tutorial shows how to install SQL Server on Ubuntu and then create and query a database with sqlcmd.
author: rothja
ms.author: jroth
manager: jhubbard
ms.date: 06/19/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
---
# Install SQL Server and create a database on Ubuntu

This quick start first installs SQL Server 2017 CTP 2.1 on Ubuntu 16.04. You then connect with **sqlcmd** to create your first database.

## Prerequisites

- You must have a Ubuntu machine with at least 3.25GB of memory.

  > [!TIP] To install Ubuntu, go to [http://www.ubuntu.com/download/server](http://www.ubuntu.com/download/server). You can also create Ubuntu virtual machines in Azure. For instructions, see [Create a Linux virtual machine with the Azure CLI](https://docs.microsoft.com/azure/virtual-machines/linux/quick-create-cli).

- For other system requirements, see [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

## Install SQL Server

To install the **mssql-server** Package on Ubuntu, follow these steps:

1. Import the public repository GPG keys:

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
   ```

1. Register the Microsoft SQL Server Ubuntu repository:

   ```bash
   curl https://packages.microsoft.com/config/ubuntu/16.04/mssql-server.list | sudo tee /etc/apt/sources.list.d/mssql-server.list
   ```

1. Run the following commands to install SQL Server:

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server
   ```

1. After the package installation finishes, run **mssql-conf setup** and follow the prompts. Make sure to specify a strong password for the SA account (Minimum length 8 characters, including uppercase and lowercase letters, base 10 digits and/or non-alphanumeric symbols).

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```
   > [!IMPORTANT] If you plan to connect remotely, you might need to open the SQL Server TCP port on your firewall. The default SQL Server port is 1433.

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```

At this point, SQL Server is running on your Ubuntu machine and is ready to use.

## <a id="tools"></a>Install the SQL Server command-line tools

To create a database, you need to connect with a tool can run Transact-SQL statements on the SQL Server. The following steps install the SQL Server command-line tools, [sqlcmd](../tools/sqlcmd-utility.md) and [bcp](../tools/bcp-utility.md).

> [!TIP]
> **Sqlcmd** is just one tool for connecting to SQL Server to run queries and perform management and development tasks. For other tools such as SQL Server Management Studio and Visual Studio Code, see the [Develop](sql-server-linux-develop-overview.md) and [Manage](sql-server-linux-management-overview.md) areas.

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
   ```

1. Register the Microsoft Ubuntu repository.

   ```bash
   curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
   ```

1. Update the sources list and run the installation command with the unixODBC developer package.

   ```bash
   sudo apt-get update
   sudo apt-get install mssql-tools unixodbc-dev
   ```

1. For convenience, add `/opt/mssql-tools/bin/` to your **PATH** environment variable. This enables you to run the tools without specifying the full path. Run the following commands to modify the **PATH** for both login sessions and interactive/non-login sessions:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
   source ~/.bashrc
   ```

## Connect to SQL Server on Linux

The following steps use **sqlcmd** to locally connect to your new SQL Server instance.

1. On your Linux box, open a command terminal.

1. Run **sqlcmd** with parameters for your SQL Server name (-S), the user name (-U), and the password (-P). In this tutorial, you are connecting locally, so the server name is `localhost`. The user name is `SA` and the password is the one you provided for the SA account during setup.

   ```bash
   sqlcmd -S localhost -U SA -P '<YourPassword>'
   ```

   > [!TIP]
   > You can omit the password on the command-line to be prompted to enter it.

   > [!TIP] If you later decide to connect remotely, specify the machine name or IP address for the **-S** parameter, and make sure port 1433 is open on your firewall.

1. If successful, you should get to a **sqlcmd** command prompt: `1>`.

1. If you get a connection failure, first attempt to diagnose the problem from the error message. Then review the [connection troubleshooting recommendations](sql-server-linux-troubleshooting-guide.md#connection).

## Create a database

From the **sqlcmd** command prompt, paste the following Transact-SQL command to create a test database:

```sql
CREATE DATABASE testdb
```

On the next line, write a query to return the name of all of the databases on your server:

```sql
SELECT Name from sys.Databases
```

The previous two commands were not executed immediately. You must type `GO` on a new line to execute the previous commands:

```sql
GO
```

## Insert data

Switch context to the new `testdb` database:

```sql
USE testdb
```

Create new table named `inventory`:

```sql
CREATE TABLE inventory (id INT, name NVARCHAR(50), quantity INT)
```

Insert data into the new table:

```sql
INSERT INTO inventory VALUES (1, 'banana', 150); INSERT INTO inventory VALUES (2, 'orange', 154);
```

Type `GO` to execute the previous commands:

```sql
GO
```

## Select data

Query rows from the `inventory` table where the quantity is greater than 152:

```sql
SELECT * FROM inventory WHERE quantity > 152;
```

Execute the command:

```sql
GO
```

## Exit the sqlcmd command prompt

To end your **sqlcmd** session, type `QUIT`:

```sql
QUIT
```

In this quick start you connected to SQL Server with **sqlcmd**, and created and populated a database. For more information on how to use sqlcmd.exe, see [sqlcmd Utility](https://msdn.microsoft.com/library/ms162773.aspx).

## Connect from Windows

It is important to note that SQL Server tools on Windows connect to SQL Server instances on Linux in the same way they would connect to any remote SQL Server instance.

If you have a Windows machine that can connect to your Linux machine, try the same steps in this topic from a Windows command-prompt running **sqlcmd**. Just verify that you use the target Linux machine name or IP address rather than localhost, and make sure that TCP port 1433 is open. If you have any problems connecting from Windows, see [connection troubleshooting recommendations](sql-server-linux-troubleshooting-guide.md#connection).

For other tools that run on Windows but connect to SQL Server on Linux, see:
- [SQL Server Management Studio (SSMS)](sql-server-linux-develop-use-ssms.md)
- [Windows PowerShell](sql-server-linux-manage-powershell.md)
- [SQL Server Data Tools (SSDT)](sql-server-linux-develop-use-ssdt.md)

## Next Steps

If you're new to T-SQL, see [Tutorial: Writing Transact-SQL Statements](https://msdn.microsoft.com/library/ms365303.aspx) and the [Transact-SQL Reference (Database Engine)](https://msdn.microsoft.com/library/bb510741.aspx).

For other ways to connect to SQL Server on Linux, see the [Develop](sql-server-linux-develop-overview.md) and [Manage](sql-server-linux-management-overview.md) areas.