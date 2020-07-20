---
title: "Ubuntu: Install SQL Server on Linux"
description: This quickstart shows how to install SQL Server 2017 or SQL Server 2019 on Ubuntu and then create and query a database with sqlcmd.
author: VanMSFT 
ms.author: vanto
ms.date: 07/15/2020
ms.topic: conceptual
ms.prod: sql
ms.custom: seo-lt-2019
ms.technology: linux
ms.assetid: 31c8c92e-12fe-4728-9b95-4bc028250d85
---
# Quickstart: Install SQL Server and create a database on Ubuntu
[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]


<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

In this quickstart, you install SQL Server 2017 on Ubuntu 18.04. You then connect with **sqlcmd** to create your first database and run queries.

> [!TIP]
> This tutorial requires user input and an internet connection. If you are interested in the unattended or offline installation procedures, see [Installation guidance for SQL Server on Linux](sql-server-linux-setup.md). For a list of supported platforms, see our [Release notes](sql-server-linux-release-notes.md).

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

In this quickstart, you install SQL Server 2019 on Ubuntu 18.04. You then connect with **sqlcmd** to create your first database and run queries.

> [!TIP]
> This tutorial requires user input and an internet connection. If you are interested in the unattended or offline installation procedures, see [Installation guidance for SQL Server on Linux](sql-server-linux-setup.md). For a list of supported platforms, see our [Release notes](sql-server-linux-release-notes-2019.md).

::: moniker-end

## Prerequisites

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

You must have an Ubuntu 16.04 or 18.04 machine with **at least 2 GB** of memory.

To install Ubuntu 18.04 on your own machine, go to <http://releases.ubuntu.com/bionic/>. You can also create Ubuntu virtual machines in Azure. See [Create and Manage Linux VMs with the Azure CLI](https://docs.microsoft.com/azure/virtual-machines/linux/tutorial-manage-vm).

> [!NOTE]
> At this time, the [Windows Subsystem for Linux](https://msdn.microsoft.com/commandline/wsl/about) for Windows 10 is not supported as an installation target.

For other system requirements, see [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

> [!NOTE]
> Ubuntu 18.04 is supported starting with SQL Server 2017 CU20. If you want to use the instructions on this article with Ubuntu 18.04, make sure you use the correct [repository path](sql-server-linux-change-repo.md), `18.04` instead of `16.04`.
>
> If you are running SQL Server on a lower version, the configuration is possible with [modifications](https://blogs.msdn.microsoft.com/sql_server_team/installing-sql-server-2017-for-linux-on-ubuntu-18-04-lts/).

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

You must have an Ubuntu 16.04 or 18.04 machine with **at least 2 GB** of memory.

To install Ubuntu 18.04 on your own machine, go to <http://releases.ubuntu.com/bionic/>. You can also create Ubuntu virtual machines in Azure. See [Create and Manage Linux VMs with the Azure CLI](https://docs.microsoft.com/azure/virtual-machines/linux/tutorial-manage-vm).

> [!NOTE]
> At this time, the [Windows Subsystem for Linux](https://msdn.microsoft.com/commandline/wsl/about) for Windows 10 is not supported as an installation target.

For other system requirements, see [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

::: moniker-end

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

## <a id="install"></a>Install SQL Server

> [!NOTE]
> The following commands for SQL Server 2017 points to the Ubuntu 18.04 repository. If you are using Ubuntu 16.04, change the path below to `/ubuntu/16.04/` instead of `/ubuntu/18.04/`.

To configure SQL Server on Ubuntu, run the following commands in a terminal to install the **mssql-server** package.

1. Import the public repository GPG keys:

   ```bash
   wget -qO- https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
   ```

2. Register the Microsoft SQL Server Ubuntu repository:

   ```bash
   sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/18.04/mssql-server-2017.list)"
   ```

   > [!TIP]
   > If you want to install SQL Server 2019 , you must instead register the SQL Server 2019 repository. Use the following command for SQL Server 2019 installations:
   >
   > ```bash
   > sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/18.04/mssql-server-2019.list)"
   > ```

3. Run the following commands to install SQL Server:

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server
   ```

4. After the package installation finishes, run **mssql-conf setup** and follow the prompts to set the SA password and choose your edition.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   > [!TIP]
   > The following SQL Server 2017 editions are freely licensed: Evaluation, Developer, and Express.

   > [!NOTE]
   > Make sure to specify a strong password for the SA account (Minimum length 8 characters, including uppercase and lowercase letters, base 10 digits and/or non-alphanumeric symbols).

5. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server --no-pager
   ```

6. If you plan to connect remotely, you might also need to open the SQL Server TCP port (default 1433) on your firewall.

At this point, SQL Server is running on your Ubuntu machine and is ready to use!

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

## <a id="install"></a>Install SQL Server

> [!NOTE]
> The following commands for SQL Server 2019 points to the Ubuntu 18.04 repository. If you are using Ubuntu 16.04, change the path below to `/ubuntu/16.04/` instead of `/ubuntu/18.04/`.

To configure SQL Server on Ubuntu, run the following commands in a terminal to install the **mssql-server** package.

1. Import the public repository GPG keys:

   ```bash
   wget -qO- https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
   ```

2. Register the Microsoft SQL Server Ubuntu repository for SQL Server 2019:

   ```bash
   sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/18.04/mssql-server-2019.list)"
   ```

3. Run the following commands to install SQL Server:

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server
   ```

4. After the package installation finishes, run **mssql-conf setup** and follow the prompts to set the SA password and choose your edition.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   > [!NOTE]
   > Make sure to specify a strong password for the SA account (Minimum length 8 characters, including uppercase and lowercase letters, base 10 digits and/or non-alphanumeric symbols).

5. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server --no-pager
   ```

6. If you plan to connect remotely, you might also need to open the SQL Server TCP port (default 1433) on your firewall.

At this point, SQL Server 2019 is running on your Ubuntu machine and is ready to use!

::: moniker-end

## <a id="tools"></a>Install the SQL Server command-line tools

To create a database, you need to connect with a tool that can run Transact-SQL statements on the SQL Server. The following steps install the SQL Server command-line tools: [sqlcmd](../tools/sqlcmd-utility.md) and [bcp](../tools/bcp-utility.md).

Use the following steps to install the **mssql-tools** on Ubuntu. 

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
   ```

1. Register the Microsoft Ubuntu repository.

   ```bash
   curl https://packages.microsoft.com/config/ubuntu/18.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
   ```

1. Update the sources list and run the installation command with the unixODBC developer package. For more information, see [Install the Microsoft ODBC driver for SQL Server (Linux)](../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md).

   ```bash
   sudo apt-get update 
   sudo apt-get install mssql-tools unixodbc-dev
   ```

   > [!Note] 
   > To update to the latest version of **mssql-tools** run the following commands:
   >    ```bash
   >   sudo apt-get update 
   >   sudo apt-get install mssql-tools 
   >   ```

1. **Optional**: Add `/opt/mssql-tools/bin/` to your **PATH** environment variable in a bash shell.

   To make **sqlcmd/bcp** accessible from the bash shell for login sessions, modify your **PATH** in the **~/.bash_profile** file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
   ```

   To make **sqlcmd/bcp** accessible from the bash shell for interactive/non-login sessions, modify the **PATH** in the **~/.bashrc** file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
   source ~/.bashrc
   ```

[!INCLUDE [Connect, create, and query data](../includes/sql-linux-quickstart-connect-query.md)]
