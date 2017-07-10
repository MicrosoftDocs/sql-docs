---
# required metadata
title: Get started with SQL Server 2017 on SUSE Linux Enterprise Server | Microsoft Docs
description: Describes how to install SQL Server 2017 CTP 2.1 on SUSE Linux Enterprise Server.
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
# Install SQL Server and create a database on SUSE Linux Enterprise Server

In this quick start tutorial, you first install SQL Server 2017 CTP 2.1 on SUSE Linux Enterprise Server (SLES) v12 SP2. Then connect with **sqlcmd** to create your first database and run queries.

## Prerequisites
- You need at least 3.25GB of memory to run SQL Server on Linux.
- The file system must be **XFS** or **EXT4**. Other file systems, such as **BTRFS**, are unsupported.
- For other system requirements, see [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).


## Install SQL Server
To install the **mssql-server** package on SLES, follow these steps:

1. Download the Microsoft SQL Server SLES repository configuration file:

   ```bash
   sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/12/mssql-server.repo

   sudo zypper --gpg-auto-import-keys refresh
   ```
   
2. Run the following commands to install SQL Server:

   ```bash
   sudo zypper install mssql-server
   ```
   
3. After the package installation finishes, run **mssql-conf setup** and follow the prompts. Make sure to specify a strong password for the SA account (Minimum length 8 characters, including uppercase and lowercase letters, base 10 digits and/or non-alphanumeric symbols).

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

4. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```
 
5. To allow remote connections, you may need to open the SQL Server TCP port on your firewall. The default SQL Server port is 1433.

## <a id="tools"></a>Install the SQL Server command-line tools
To create a database, you need to connect with a tool that can run Transact-SQL statements on the SQL Server. The following steps install the **mssql-tools** on SUSE Linux Enterprise Server. 

1. Add the Microsoft SQL Server repository to Zypper.

   ```bash
   sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/12/prod.repo 
   sudo zypper --gpg-auto-import-keys refresh
   ```

1. Install **mssql-tools** with the unixODBC developer package.

   ```bash
   sudo zypper install mssql-tools unixODBC-devel
   ```

   > [!Note] 
   > To update to the latest version of **mssql-tools** run the following commands:
   >    ```bash
   >   sudo zypper refresh
   >   sudo zypper update mssql-tools
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

[!INCLUDE [create-query-data-md](../includes/sql-linux-quickstart-connect-query.md)]
