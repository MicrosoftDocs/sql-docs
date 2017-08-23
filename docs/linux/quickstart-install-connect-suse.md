---
title: Get started with SQL Server 2017 on SUSE Linux Enterprise Server | Microsoft Docs
description:  This quick start tutorial shows how to install SQL Server 2017 on SUSE Linux Enterprise Server and then create and query a database with sqlcmd.
author: sabotta 
ms.author: carlasab 
manager: craigg
ms.date: 07/24/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 31ddfb80-f75c-4f51-8540-de6213cb68b8
---
# Install SQL Server and create a database on SUSE Linux Enterprise Server

[!INCLUDE[tsql-appliesto-sslinux-only](../../docs/includes/tsql-appliesto-sslinux-only.md)]

In this quick start tutorial, you first install SQL Server 2017 RC2 on SUSE Linux Enterprise Server (SLES) v12 SP2. Then connect with **sqlcmd** to create your first database and run queries.

> [!TIP]
> This tutorial requires user input and an internet connection. If you are interested in the [unattended](sql-server-linux-setup.md#unattended) or [offline](sql-server-linux-setup.md#offline) installation procedures, see [Installation guidance for SQL Server on Linux](sql-server-linux-setup.md).

## Prerequisites

You must have a SLES v12 SP2 machine with **at least 3.25 GB** of memory. The file system must be **XFS** or **EXT4**. Other file systems, such as **BTRFS**, are unsupported.

To install SUSE Linux Enterprise Server on your own machine, go to [https://www.suse.com/products/server](https://www.suse.com/products/server). You can also create SLES virtual machines in Azure. See [Create and Manage Linux VMs with the Azure CLI](https://docs.microsoft.com/azure/virtual-machines/linux/tutorial-manage-vm), and use `--image SLES` in the call to `az vm create`.

For other system requirements, see [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

## <a id="install"></a>Install SQL Server

To configure SQL Server on SLES, run the following commands in a terminal to install the **mssql-server** package:

1. Download the Microsoft SQL Server SLES repository configuration file:

   ```bash
   sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/12/mssql-server.repo
   sudo zypper --gpg-auto-import-keys refresh
   ```

1. Run the following commands to install SQL Server:

   ```bash
   sudo zypper install -y mssql-server
   ```

1. After the package installation finishes, run **mssql-conf setup** and follow the prompts to set the SA password and chose your edition.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   > [!TIP]
   > Make sure to specify a strong password for the SA account (Minimum length 8 characters, including uppercase and lowercase letters, base 10 digits and/or non-alphanumeric symbols).

   > [!TIP]
   > When installing RC2, no purchased licenses are required to try any of the editions. Because it is a release candidate, the following message appears regardless of the edition you select:
   >
   > `This is an evaluation version.  There are [175] days left in the evaluation period.`
   >
   > This message does not reflect the edition you selected. It relates to the preview period for RC2.

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```

1. If you plan to connect remotely, you might also need to open the SQL Server TCP port (default 1433) on your firewall.

At this point, SQL Server is running on your SLES machine and is ready to use!

## <a id="tools"></a>Install the SQL Server command-line tools

To create a database, you need to connect with a tool that can run Transact-SQL statements on the SQL Server. The following steps install the SQL Server command-line tools: [sqlcmd](../tools/sqlcmd-utility.md) and [bcp](../tools/bcp-utility.md).

1. Add the Microsoft SQL Server repository to Zypper.

   ```bash
   sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/12/prod.repo 
   sudo zypper --gpg-auto-import-keys refresh
   ```

1. Install **mssql-tools** with the unixODBC developer package.

   ```bash
   sudo zypper install -y mssql-tools unixODBC-devel
   ```

1. For convenience, add `/opt/mssql-tools/bin/` to your **PATH** environment variable. This enables you to run the tools without specifying the full path. Run the following commands to modify the **PATH** for both login sessions and interactive/non-login sessions:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
   source ~/.bashrc
   ```

> [!TIP]
> **Sqlcmd** is just one tool for connecting to SQL Server to run queries and perform management and development tasks. Other tools include [SQL Server Management Studio](sql-server-linux-develop-use-ssms.md) and [Visual Studio Code](sql-server-linux-develop-use-vscode.md).

[!INCLUDE [Connect, create, and query data](../includes/sql-linux-quickstart-connect-query.md)]