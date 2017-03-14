---
# required metadata

title: Install SQL Server Tools on Linux | Microsoft Docs
description: This topic describes how to install the SQL Server Tools on Linux.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 3/15/2017
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

The following steps install the command-line tools, Microsoft ODBC drivers, and their dependencies. The **mssql-tools** package contains:

- **sqlcmd**: Command-line query utility.
- **bcp**: Bulk import-export utility.

Install the tools for your platform:

- [Red Hat Enterprise Linux](#RHEL)
- [Ubuntu](#ubuntu)
- [SUSE Linux Enterprise Server](#SLES)
- [Docker](#docker)
- [macOS](#macos)

## <a id="RHEL"><a/>Install tools on RHEL

Use the following steps to install the **mssql-tools** on Red Hat Enterprise Linux. 

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

## <a id="ubuntu"></a>Install tools on Ubuntu

Use the following steps to install the **mssql-tools** on Ubuntu. 

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

## <a id="SLES"></a>Install tools on SLES

Use the following steps to install the **mssql-tools** on SUSE Linux Enterprise Server. 

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
## <a id="docker"></a> Tools on Docker images

Starting with SQL Server vNext CTP 1.4, the SQL Server command-line tools are included in the Docker image. If you attach to the image with an interactive command-prompt, you can run the tools locally. 

## <a id="macos"></a> Install tools on macOS

**Sqlcmd** and **bcp** are not available on macOS. 

Use sql-cli from macOS. For more information, see [sql-cli](https://www.npmjs.com/package/sql-cli).  

## Next steps

After installation, connect to the SQL Server instance to create and manage databases. To get started, see [Connect and query SQL Server on Linux](sql-server-linux-connect-and-query-sqlcmd.md).
