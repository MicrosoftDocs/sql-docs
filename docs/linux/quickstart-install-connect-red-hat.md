---
title: "RHEL: Install SQL Server on Linux" 
titleSuffix: SQL Server
description:  This quickstart shows how to install SQL Server 2017 or SQL Server 2019 on Red Hat Enterprise Linux (RHEL) and then create and query a database with sqlcmd.
author: VanMSFT 
ms.custom: seo-lt-2019
ms.author: vanto
ms.date: 06/22/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: linux
ms.assetid: 92503f59-96dc-4f6a-b1b0-d135c43e935e
---
# Quickstart: Install SQL Server and create a database on Red Hat

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

In this quickstart, you install SQL Server 2017 or SQL Server 2019 on Red Hat Enterprise Linux (RHEL). You then connect with **sqlcmd** to create your first database and run queries.

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

In this quickstart, you install SQL Server 2019 on Red Hat Enterprise Linux (RHEL) 8. You then connect with **sqlcmd** to create your first database and run queries.

::: moniker-end

> [!TIP]
> This tutorial requires user input and an internet connection. If you are interested in the [unattended](sql-server-linux-setup.md#unattended) or [offline](sql-server-linux-setup.md#offline) installation procedures, see [Installation guidance for SQL Server on Linux](sql-server-linux-setup.md).

## Prerequisites

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

You must have a RHEL 7.3 - 7.8, or 8.0 - 8.2 machine with **at least 2 GB** of memory.

::: moniker-end

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

You must have a RHEL 7.3, 7.4, 7.5, 7.6, or 8.0 machine with **at least 2 GB** of memory.

::: moniker-end

To install Red Hat Enterprise Linux on your own machine, go to [https://access.redhat.com/products/red-hat-enterprise-linux/evaluation](https://access.redhat.com/products/red-hat-enterprise-linux/evaluation). You can also create RHEL virtual machines in Azure. See [Create and Manage Linux VMs with the Azure CLI](https://docs.microsoft.com/azure/virtual-machines/linux/tutorial-manage-vm), and use `--image RHEL` in the call to `az vm create`.

If you have previously installed a CTP or RC release of SQL Server, you must first remove the old repository before following these steps. For more information, see [Configure Linux repositories for SQL Server 2017 and 2019](sql-server-linux-change-repo.md).

For other system requirements, see [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

## <a id="install"></a>Install SQL Server

> [!NOTE]
> RHEL 8 is supported for SQL Server 2017 starting with CU20. The following commands for SQL Server 2017 points to the RHEL 8 repository. RHEL 8 does not come preinstalled with python2, which is required by SQL Server. Before you begin the SQL Server install steps, execute the command and verify that python2 is selected as the interpreter:
>
> ```
> sudo alternatives --config python
> # If not configured, install python2 and openssl10 using the following commands: 
> sudo yum install python2
> sudo yum install compat-openssl10
> # Configure python2 as the default interpreter using this command: 
> sudo alternatives --config python
> ```
> For more information, see the following blog on installing python2 and configuring it as the default interpreter: https://www.redhat.com/en/blog/installing-microsoft-sql-server-red-hat-enterprise-linux-8-beta.
>
> If you are using RHEL 7, change the path below to `/rhel/7` instead of `/rhel/8`.

To configure SQL Server on RHEL, run the following commands in a terminal to install the **mssql-server** package:

1. Download the Microsoft SQL Server 2017 Red Hat repository configuration file:

   ```bash
   sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/mssql-server-2017.repo
   ```

   > [!TIP]
   > If you want to install SQL Server 2019 , you must instead register the SQL Server 2019 repository. Use the following command for SQL Server 2019 installations:
   >
   > ```bash
   > sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/mssql-server-2019.repo
   > ```

2. Run the following commands to install SQL Server:

   ```bash
   sudo yum install -y mssql-server
   ```

3. After the package installation finishes, run **mssql-conf setup** and follow the prompts to set the SA password and choose your edition.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   > [!TIP]
   > The following SQL Server 2017 editions are freely licensed: Evaluation, Developer, and Express.

   > [!NOTE]
   > Make sure to specify a strong password for the SA account (Minimum length 8 characters, including uppercase and lowercase letters, base 10 digits and/or non-alphanumeric symbols).

4. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```

5. To allow remote connections, open the SQL Server port on the firewall on RHEL. The default SQL Server port is TCP 1433. If you are using **FirewallD** for your firewall, you can use the following commands:

   ```bash
   sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent
   sudo firewall-cmd --reload
   ```

At this point, SQL Server is running on your RHEL machine and is ready to use!

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

## <a id="install"></a>Install SQL Server

> [!NOTE]
> The following commands for SQL Server 2019 points to the RHEL 8 repository. RHEL 8 does not come preinstalled with python2, which is required by SQL Server. Before you begin the SQL Server install steps, execute the command and verify that python2 is selected as the interpreter: 
>
> ```
> sudo alternatives --config python
> # If not configured, install python2 and openssl10 using the following commands: 
> sudo yum install python2
> sudo yum install compat-openssl10
> # Configure python2 as the default interpreter using this command: 
> sudo alternatives --config python
> ``` 
> For more information about these steps, see the following blog on installing python2 and configuring it as the default interpreter: https://www.redhat.com/en/blog/installing-microsoft-sql-server-red-hat-enterprise-linux-8-beta.
> 
> If you are using RHEL 7, change the path below to `/rhel/7` instead of `/rhel/8`.

To configure SQL Server on RHEL, run the following commands in a terminal to install the **mssql-server** package:

1. Download the Microsoft SQL Server 2019 Red Hat repository configuration file:

   ```bash
   sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/mssql-server-2019.repo
   ```

2. Run the following commands to install SQL Server:

   ```bash
   sudo yum install -y mssql-server
   ```

3. After the package installation finishes, run **mssql-conf setup** and follow the prompts to set the SA password and choose your edition.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   > [!NOTE]
   > Make sure to specify a strong password for the SA account (Minimum length 8 characters, including uppercase and lowercase letters, base 10 digits and/or non-alphanumeric symbols).

4. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```

5. To allow remote connections, open the SQL Server port on the firewall on RHEL. The default SQL Server port is TCP 1433. If you are using **FirewallD** for your firewall, you can use the following commands:

   ```bash
   sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent
   sudo firewall-cmd --reload
   ```

At this point, SQL Server 2019 is running on your RHEL machine and is ready to use!

::: moniker-end

## <a id="tools"></a>Install the SQL Server command-line tools

To create a database, you need to connect with a tool that can run Transact-SQL statements on the SQL Server. The following steps install the SQL Server command-line tools: [sqlcmd](../tools/sqlcmd-utility.md) and [bcp](../tools/bcp-utility.md).

1. Download the Microsoft Red Hat repository configuration file.

   ```bash
   sudo curl -o /etc/yum.repos.d/msprod.repo https://packages.microsoft.com/config/rhel/8/prod.repo
   ```

1. If you had a previous version of **mssql-tools** installed, remove any older unixODBC packages.

   ```bash
   sudo yum remove unixODBC-utf16 unixODBC-utf16-devel
   ```

1. Run the following commands to install **mssql-tools** with the unixODBC developer package. For more information, see [Install the Microsoft ODBC driver for SQL Server (Linux)](../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md).

   ```bash
   sudo yum install -y mssql-tools unixODBC-devel
   ```

1. For convenience, add `/opt/mssql-tools/bin/` to your **PATH** environment variable. This enables you to run the tools without specifying the full path. Run the following commands to modify the **PATH** for both login sessions and interactive/non-login sessions:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
   source ~/.bashrc
   ```

[!INCLUDE [Connect, create, and query data](../includes/sql-linux-quickstart-connect-query.md)]
