---
# required metadata

title: Install SQL Server on Ubuntu | Microsoft Docs
description: 
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 05/17/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 31c8c92e-12fe-4728-9b95-4bc028250d85

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
# Install SQL Server on Ubuntu

This topic provides a walkthrough of how to install SQL Server 2017 RC1 on Ubuntu 16.04.

> [!NOTE]
> You need at least 3.25GB of memory to run SQL Server on Linux. For other system requirements, see [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

## Install SQL Server

To install the **mssql-server** Package on Ubuntu, follow these steps:

1. Import the public repository GPG keys:

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
   ```

3. Register the Microsoft SQL Server Ubuntu repository:

   ```bash
   curl https://packages.microsoft.com/config/ubuntu/16.04/mssql-server.list | sudo tee /etc/apt/sources.list.d/mssql-server.list
   ```

5. Run the following commands to install SQL Server:

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server
   ```

6. After the package installation finishes, run **mssql-conf setup** and follow the prompts. Make sure to specify a strong password for the SA account (Minimum length 8 characters, including uppercase and lowercase letters, base 10 digits and/or non-alphanumeric symbols).
 
   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

7. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```

8. To allow remote connections, you may need to open the SQL Server TCP port on your firewall. The default SQL Server port is 1433.

## Upgrade SQL Server

To upgrade the **mssql-server** package on Ubuntu, follow these steps:

1. Update the apt-get repository lists:
   ```bash
   sudo apt-get update
   ```

2. Re-run the installation command, this will upgrade the specific mssql-server package:
   ```bash
   sudo apt-get install mssql-server
   ```

These commands will download the newest package and replace the binaries located under `/opt/mssql/`. The user generated databases and system databases will not be affected by this operation. 

## Uninstall SQL Server

To remove the **mssql-server** package on Ubuntu, follow these steps:

1. Run the `remove` command. This will delete the package and remove the files under `/opt/mssql/`. However, this command will not affect user-generated and system databases.
   ```bash
   sudo apt-get remove mssql-server
   ```

2. Removing the package will not delete the generated database files. If you want to delete the database files use the following command:
   ```bash
   sudo rm -rf /var/opt/mssql/
   ```

## <a id="offline"></a> Offline installation

[!INCLUDE[SQL Server Linux offline package installation](../includes/sql-server-linux-offline-package-install-intro.md)]

To manually install the SQL Server database engine package for Ubuntu, use the following steps:

1. **Download the .deb database engine package**. Find package download links in the package details section of the [Release Notes](sql-server-linux-release-notes.md).

1. **Move the downloaded package to your Linux machine**. If you used a different machine to download the packages, one way to move the packages to your Linux machine is with the **scp** commmand.

1. **Install the database engine package**. Use the **dpkg** command. Replace `versionnumber` with your package version number.

    ```bash
    sudo dpkg -i mssql-server_versionnumber_amd64.deb
    ```

1. **Resolve missing dependencies**: Typically, there are missing dependencies at this point. If you have access to approved repositories containing those dependencies, the easiest solution is to use the **apt-get** command:

    ```bash
    sudo apt-get -f install
    ```

    > [!NOTE]
    > This command completes the installation of the SQL Server package as well.

    In some cases, you might have to manually install the dependencies. Inspect the package's dependencies with the following command:

    ```bash
    dpkg -I mssql-server_versionnumber_amd64.deb | grep "Depends:"
    ```

1. **Complete the SQL Server setup**. Use **mssql-conf** to complete the SQL Server setup:

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

## Next steps

- [Install tools](sql-server-linux-setup-tools.md#ubuntu)

- If you already have SQL Server tools, [connect to the SQL Server](sql-server-linux-connect-and-query-sqlcmd.md).
