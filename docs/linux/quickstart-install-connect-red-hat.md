---
# required metadata
title: Get started with SQL Server 2017 on Red Hat Enterprise Linux | Microsoft Docs
description: Describes how to install SQL Server 2017 CTP 2.1 on Red Hat Enterprise Linux.
author: sabotta 
ms.author: carlasab 
manager: craigg
ms.date: 06/30/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: ""

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
# Install SQL Server and create a database on Red Hat

In this quick start tutorial, you first install SQL Server 2017 CTP 2.1 on Red Hat Enterprise Linux (RHEL). Then connect with **sqlcmd** to create your first database and run queries.

## Prerequisites

- You must have a Linux machine with at least 3.25GB of memory.
- For other system requirements, see [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).


## Install SQL Server

To install the **mssql-server** package on RHEL, follow these steps:

1. Enter superuser mode.

   ```bash
   sudo su
   ```

2. Download the Microsoft SQL Server Red Hat repository configuration file:

   ```bash
   curl https://packages.microsoft.com/config/rhel/7/mssql-server.repo > /etc/yum.repos.d/mssql-server.repo
   ```
   
3. Exit superuser mode.

   ```bash
   exit
   ```

4. Run the following commands to install SQL Server:

   ```bash
   sudo yum install -y mssql-server
   ```
   
5. After the package installation finishes, run **mssql-conf setup** and follow the prompts. Make sure to specify a strong password for the SA account (Minimum length 8 characters, including uppercase and lowercase letters, base 10 digits and/or non-alphanumeric symbols).

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

6. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```
   
7. To allow remote connections, open the SQL Server port on the firewall on RHEL. The default SQL Server port is TCP 1433. If you are using **FirewallD** for your firewall, you can use the following commands:

   ```bash
   sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent
   sudo firewall-cmd --reload
   ```


## <a id="tools"></a>Install the SQL Server command-line tools
To create a database, you need to connect with a tool that can run Transact-SQL statements on SQL Server. The following steps install the **mssql-tools** on Red Hat Enterprise Linux. 


1. Enter superuser mode.

   ```bash
   sudo su
   ```

1. Download the Microsoft Red Hat repository configuration file.

   ```bash
   curl https://packages.microsoft.com/config/rhel/7/prod.repo > /etc/yum.repos.d/msprod.repo
   ```

1. Exit superuser mode.

   ```bash
   exit
   ```

1. If you had a previous version of **mssql-tools** installed, remove any older unixODBC packages.

   ```bash
   sudo yum update
   sudo yum remove unixODBC-utf16 unixODBC-utf16-devel
   ```

1. Run the following commands to install **mssql-tools** with the unixODBC developer package.

   ```bash
   sudo yum update
   sudo yum install mssql-tools unixODBC-devel
   ```

   > [!Note] 
   > To update to the latest version of **mssql-tools** run the following commands:
   >    ```bash
   >   sudo yum check-update
   >   sudo yum update mssql-tools
   >   ```

1. For convenience, add `/opt/mssql-tools/bin/` to your **PATH** environment variable in a bash shell.

   To make **sqlcmd/bcp** accessible from the bash shell for login sessions, modify your **PATH** in the **~/.bash_profile** file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
   ```

   To make **sqlcmd/bcp** accessible from the bash shell for interactive/non-login sessions, modify the **PATH** in the **~/.bashrc** file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
   source ~/.bashrc
   ```

## Connect locally
The following steps use **sqlcmd** to locally connect to your new SQL Server instance.

1. Run **sqlcmd** with parameters for your SQL Server name (-S), the user name (-U), and the password (-P). In this tutorial, you are connecting locally, so the server name is `localhost`. The user name is `SA` and the password is the one you provided for the SA account during setup.

   ```bash
   sqlcmd -S localhost -U SA -P '<YourPassword>'
   ```

   > [!TIP]
   > You can omit the password on the command-line to be prompted to enter it.

   > [!TIP]
   > If you later decide to connect remotely, specify the machine name or IP address for the **-S** parameter, and make sure port 1433 is open on your firewall.

1. If successful, you should get to a **sqlcmd** command prompt: `1>`.

1. If you get a connection failure, first attempt to diagnose the problem from the error message. Then review the [connection troubleshooting recommendations](sql-server-linux-troubleshooting-guide.md#connection).

[!INCLUDE [create-query-data-md](../includes/create-query-data-md.md)]


## Next steps

If you're new to T-SQL, see [Tutorial: Writing Transact-SQL Statements](https://msdn.microsoft.com/library/ms365303.aspx) and the [Transact-SQL Reference (Database Engine)](https://msdn.microsoft.com/library/bb510741.aspx).

To explore other ways to connect and manage SQL Server, see [Visusal Studio Code](sql-server-linux-develop-use-vscode.md) and  [SQL Server Management Studio](sql-server-linux-develop-use-ssms.md).

