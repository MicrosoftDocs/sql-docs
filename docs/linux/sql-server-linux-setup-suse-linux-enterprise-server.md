---
# required metadata
title: Install SQL Server on SUSE Linux Enterprise Server | Microsoft Docs
description: Describes how to install SQL Server 2017 CTP 2.1 on SUSE Linux Enterprise Server.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 05/17/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 31ddfb80-f75c-4f51-8540-de6213cb68b8

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
# Install SQL Server on SUSE Linux Enterprise Server

This topic provides a walkthrough of how to install SQL Server 2017 CTP 2.1 on SUSE Linux Enterprise Server (SLES) v12 SP2.

> [!NOTE]
> You need at least 3.25GB of memory to run SQL Server on Linux. Also, the file system must be **XFS** or **EXT4**. Other file systems, such as **BTRFS**, are unsupported. For other system requirements, see [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

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

## Upgrade SQL Server

To upgrade the **mssql-server** package on SLES, execute the following command:

   ```bash
   sudo zypper update mssql-server
   ```

These commands will download the newest package and replace the binaries located under `/opt/mssql/`. The user generated databases and system databases will not be affected by this operation. 

## Uninstall SQL Server

To remove the **mssql-server** package on SLES, follow these steps:

1. Run the `remove` command. This will delete the package and remove the files under `/opt/mssql/`. However, this command will not affect user-generated and system database files, which are located under `/var/opt/mssql`.
   ```bash
   sudo zypper remove mssql-server
   ```

2. Removing the package will not delete the generated database files. If you want to delete the database files use the following command:
   ```bash
   sudo rm -rf /var/opt/mssql/
   ```

## <a id="offline"></a> Offline installation

[!INCLUDE[SQL Server Linux offline package installation](../includes/sql-server-linux-offline-package-install-intro.md)]

To manually install the SQL Server database engine package for SUSE Linux Enterprise Server, use the following steps:

1. **Download the .rpm database engine package**. Find package download links in the package details section of the [Release Notes](sql-server-linux-release-notes.md).

1. **Move the downloaded package to your Linux machine**. If you used a different machine to download the packages, one way to move the packages to your Linux machine is with the **scp** commmand.

1. **Install the database engine package**. Use the **zypper install** command. Replace `versionnumber` with your package version number.

    ```bash
    sudo zypper install mssql-server_versionnumber.x86_64.rpm
    ```

    > [!NOTE]
    > You can also install the RPM package with the `rpm -ivh` command, but the `zypper install` command also installs dependencies if available from approved repositories.

1. **Resolve missing dependencies**: You might have missing dependencies at this point. If not, you can skip this step. In some cases, you must manually locate and install these dependencies. Use the **rpm** command to inspect the dependencies of the SQL Server packages:

    ```bash
    rpm -qpR mssql-server_versionnumber.x86_64.rpm
    ```

1. **Complete the SQL Server setup**. Use **mssql-conf** to complete the SQL Server setup:

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

## Next steps

- [Install tools](sql-server-linux-setup-tools.md#SLES)

- If you already have SQL Server tools, [connect to the SQL Server](sql-server-linux-connect-and-query-sqlcmd.md).
