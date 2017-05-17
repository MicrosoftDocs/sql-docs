---
# required metadata
title: Install SQL Server on Red Hat Enterprise Linux | Microsoft Docs
description: Describes how to install SQL Server 2017 CTP 2.1 on Red Hat Enterprise Linux.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 05/17/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 92503f59-96dc-4f6a-b1b0-d135c43e935e

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
# Install SQL Server on Red Hat Enterprise Linux

This topic provides a walkthrough of how to install SQL Server 2017 CTP 2.1 on Red Hat Enterprise Linux (RHEL).

> [!NOTE] 
> You need at least 3.25GB of memory to run SQL Server on Linux.
> SQL Server Engine has been tested up to 1 TB of memory at this time.


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

## Upgrade SQL Server

To upgrade the **mssql-server** package on RHEL, execute the following command:

   ```bash
   sudo yum update mssql-server
   ```

These commands will download the newest package and replace the binaries located under `/opt/mssql/`. The user generated databases and system databases will not be affected by this operation. 

## Uninstall SQL Server

To remove the **mssql-server** package on RHEL, follow these steps:

1. Run the `remove` command. This will delete the package and remove the files under `/opt/mssql/`. However, this command will not affect user-generated and system database files, which are located under `/var/opt/mssql`.
   ```bash
   sudo yum remove mssql-server
   ```

2. Removing the package will not delete the generated database files. If you want to delete the database files use the following command:
   ```bash
   sudo rm -rf /var/opt/mssql/
   ```

## <a id="offline"></a> Offline installation

[!INCLUDE[SQL Server Linux offline package installation](../includes/sql-server-linux-offline-package-install-intro.md)]

To manually install the SQL Server database engine package for Red Hat Enterprise Linux, use the following steps:

1. **Download the .rpm database engine package**. Find package download links in the package details section of the [Release Notes](sql-server-linux-release-notes.md).

1. **Move the downloaded package to your Linux machine**. If you used a different machine to download the packages, one way to move the packages to your Linux machine is with the **scp** commmand.

1. **Install the database engine package**. Use the **yum** command with the **localinstall** option. Replace `versionnumber` with your package version number.

    ```bash
    sudo yum localinstall mssql-server_versionnumber.x86_64.rpm
    ```

    > [!NOTE]
    > You can also install the RPM package with the `rpm -ivh` command, but the `yum localinstall` command also installs dependencies if available from approved repositories.

1. **Resolve missing dependencies**: You might have missing dependencies at this point. If not, you can skip this step. In some cases, you must manually locate and install these dependencies. Use the **rpm** command to inspect the dependencies of the SQL Server packages:

    ```bash
    rpm -qpR mssql-server_versionnumber.x86_64.rpm
    ```

1. **Complete the SQL Server setup**. Use **mssql-conf** to complete the SQL Server setup:

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

## Next steps

- [Install tools](sql-server-linux-setup-tools.md#RHEL)

- If you already have SQL Server tools, [connect to the SQL Server](sql-server-linux-connect-and-query-sqlcmd.md).

