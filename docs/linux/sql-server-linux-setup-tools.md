---
# required metadata

title: Install SQL Server Tools on Linux - SQL Server vNext | Microsoft Docs
description: This topic describes how to install the SQL Server Tools on Linux.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 1/05/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: eff8e226-185f-46d4-a3e3-e18b7a439e63

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
# Install SQL Server tools on Linux

The following steps install the command-line tools, Microsoft ODBC drivers, and their dependencies. The mssql-tools package contains:

- **sqlcmd**: Command-line query utility.
- **bcp**: Bulk import-export utility.

Install the tools for your platform:

- [Red Hat Enterprise Linux](#RHEL)
- [Ubuntu](#ubuntu)
- [SUSE Enterprise Linux](#SLES)
- [macOS](#macos)

## <a name="RHEL">Install tools on RHEL</a>

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

1. Run the following commands to install mssql-tools with the unixODBC developer package.

   ```bash
   sudo yum update
   sudo yum install mssql-tools unixODBC-utf16-devel
   ```

> [!Note] 
> To update to the latest version of mssql-tools run the following commands:
>    ```bash
>   yum check-update
>   yum update mssql-tools
>   ```

Optional Step: Create symlinks to SQLCMD and BCP under /usr/bin/.

   ```bash
   ln -sfn /opt/mssql-tools/bin/sqlcmd{Fill-Version-Here} /usr/bin/sqlcmd
   ln -sfn /opt/mssql-tools/bin/bcp{Fill-Version-Here} /usr/bin/bcp
   ```


## <a name="ubuntu">Install tools on Ubuntu</a>

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
   sudo apt-get install mssql-tools unixodbc-dev-utf16
   ```

> [!Note] 
> To update to the latest version of mssql-tools run the following commands:
>    ```bash
>   apt-get refresh
>   apt-get update mssql-tools
>   ```

Optional Step: Create symlinks to SQLCMD and BCP under /usr/bin/.

   ```bash
   ln -sfn /opt/mssql-tools/bin/sqlcmd{Fill-Version-Here} /usr/bin/sqlcmd
   ln -sfn /opt/mssql-tools/bin/bcp{Fill-Version-Here} /usr/bin/bcp
   ```

## <a name="SLES">Install tools on SLES</a>

1. Enter superuser mode.

   ```bash
   sudo su
   ```

1. Add the Microsoft SQL Server repository to Zypper.

   ```bash
   zypper ar https://packages.microsoft.com/config/sles/12/prod.repo 
   ```

1. Download and import GPG keys.

   ```bash
   wget "http://aka.ms/msodbcrhelpublickey/dpgswdist.v1.asc"
   rpm --import dpgswdist.v1.asc
   wget "https://apt-mo.trafficmanager.net/keys/microsoft.asc"
   rpm --import microsoft.asc
   ```

1. Exit superuser mode.

   ```bash
   exit
   ```

1. Update the sources, and install mssql-tools with the unixODBC developer package.

   ```bash
   sudo zypper update
   sudo zypper install mssql-tools unixODBC-utf16-devel
   ```

> [!Note] 
> To update to the latest version of mssql-tools run the following commands:
>    ```bash
>   zypper refresh
>   zypper update mssql-tools
>   ```

Optional Step: Create symlinks to SQLCMD and BCP under /usr/bin/.

   ```bash
   ln -sfn /opt/mssql-tools/bin/sqlcmd{Fill-Version-Here} /usr/bin/sqlcmd
   ln -sfn /opt/mssql-tools/bin/bcp{Fill-Version-Here} /usr/bin/bcp
   ```


## <a name="macos">Install tools on macOS</a>

Sqlcmd and bcp are not available on macOS. 

Use sql-cli from macOS. For more information, see [sql-cli](https://www.npmjs.com/package/sql-cli).  

## Next steps

After installation, connect to the SQL Server instance to create and manage databases. To get started, see [Connect and query SQL Server on Linux](sql-server-linux-connect-and-query-sqlcmd.md).

