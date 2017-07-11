---
# required metadata
title: Get started with SQL Server 2017 on Red Hat Enterprise Linux | Microsoft Docs
description: Describes how to install SQL Server 2017 CTP 2.1 on Red Hat Enterprise Linux.
author: sabotta 
ms.author: carlasab 
manager: craigg
ms.date: 07/19/2017
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

In this quick start tutorial, you first install SQL Server 2017 CTP 2.1 on Red Hat Enterprise Linux (RHEL) 7.3. Then connect with **sqlcmd** to create your first database and run queries.

## Prerequisites

- You must have a RHEL 7.3 machine with at least 3.25 GB of memory.

  > [!TIP]
  > To install RHEL, go to [http://access.redhat.com/products/red-hat-enterprise-linux/evaluation](http://access.redhat.com/products/red-hat-enterprise-linux/evaluation). You can also create RHEL virtual machines in Azure. For the basic process, see [Create a Linux virtual machine with the Azure CLI](https://docs.microsoft.com/azure/virtual-machines/linux/quick-create-cli).

- For other system requirements, see [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

## <a id="install"></a>Install SQL Server

To configure SQL Server on RHEL, run the following commands in a terminal to install the **mssql-server** package:

1. Enter superuser mode.

   ```bash
   sudo su
   ```

1. Download the Microsoft SQL Server Red Hat repository configuration file:

   ```bash
   curl https://packages.microsoft.com/config/rhel/7/mssql-server.repo > /etc/yum.repos.d/mssql-server.repo
   ```

1. Exit superuser mode.

   ```bash
   exit
   ```

1. Run the following commands to install SQL Server:

   ```bash
   sudo yum install -y mssql-server
   ```
   
1. After the package installation finishes, run **mssql-conf setup** and follow the prompts. Make sure to specify a strong password for the SA account (Minimum length 8 characters, including uppercase and lowercase letters, base 10 digits and/or non-alphanumeric symbols).

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```
   
1. To allow remote connections, open the SQL Server port on the firewall on RHEL. The default SQL Server port is TCP 1433. If you are using **FirewallD** for your firewall, you can use the following commands:

   ```bash
   sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent
   sudo firewall-cmd --reload
   ```

At this point, SQL Server is running on your RHEL machine and is ready to use!

## <a id="tools"></a>Install the SQL Server command-line tools

To create a database, you need to connect with a tool that can run Transact-SQL statements on the SQL Server. The following steps install the SQL Server command-line tools, [sqlcmd](../tools/sqlcmd-utility.md) and [bcp](../tools/bcp-utility.md).

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

1. For convenience, add `/opt/mssql-tools/bin/` to your **PATH** environment variable. This enables you to run the tools without specifying the full path. Run the following commands to modify the **PATH** for both login sessions and interactive/non-login sessions:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
   source ~/.bashrc
   ```

> [!TIP]
> **Sqlcmd** is just one tool for connecting to SQL Server to run queries and perform management and development tasks. Other tools include [SQL Server Management Studio](sql-server-linux-develop-use-ssms.md) and [Visual Studio Code](sql-server-linux-develop-use-vscode.md).

[!INCLUDE [Connect, create, and query data](../includes/sql-linux-quickstart-connect-query.md)]