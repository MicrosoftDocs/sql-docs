---
# required metadata

title: Install SQL Server command-line tools on Linux | Microsoft Docs
description: This topic describes how to install the SQL Server Tools on Linux.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 4/25/2017
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
# Install sqlcmd and bcp the SQL Server command-line tools on Linux

The following steps install the command-line tools, Microsoft ODBC drivers, and their dependencies. The **mssql-tools** package contains:

- **sqlcmd**: Command-line query utility.
- **bcp**: Bulk import-export utility.

Install the tools for your platform:

- [Red Hat Enterprise Linux](#RHEL)
- [Ubuntu](#ubuntu)
- [SUSE Linux Enterprise Server](#SLES)
- [macOS](#macos)
- [Docker](#docker)

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

## <a id="macos"></a> Install tools on macOS

A preview of **sqlcmd** and **bcp** is now available on macOS. For more information, see the [announcement](https://blogs.technet.microsoft.com/dataplatforminsider/2017/04/03/sql-server-command-line-tools-for-mac-preview-now-available/).

To install the tools for Mac El Capitan and Sierra, use the following commands:

```
/usr/bin/ruby -e “$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)”
brew tap microsoft/mssql-preview https://github.com/Microsoft/homebrew-mssql-preview
brew update
brew install mssql-tools
#for silent install ACCEPT_EULA=y brew install mssql-tools
```

## <a id="docker"></a> Docker

Starting with SQL Server 2017 CTP 2.0, the SQL Server command-line tools are included in the Docker image. If you attach to the image with an interactive command-prompt, you can run the tools locally.

## Manually install tools packages

If your Linux machine does not have access to the online repositories used in the previous sections, you can download the tools package directly. These packages are located in the Microsoft repository, [https://packages.microsoft.com](https://packages.microsoft.com).

> [!TIP]
> If you successfully installed the tools with apt-get, yum, or zypper, you do not need to download or manually install any of the packages below.

The following table provides the location for the latest tools packages. To install the RPM packages, use `rpm -ivh packagename.rpm`. To install the Debian packages, use `dpkg -i packagename.deb`. 

| Tools package | Version | Download |
|-----|-----|-----|
| Red Hat RPM tools package | 14.0.5.0-1 | [mssql-tools RPM package](https://packages.microsoft.com/rhel/7.3/prod/mssql-tools-14.0.5.0-1.x86_64.rpm) | 
| SLES RPM tools package | 14.0.5.0-1 | [mssql-tools RPM package](https://packages.microsoft.com/sles/12/prod/mssql-tools-14.0.5.0-1.x86_64.rpm) | 
| Ubuntu 16.04 Debian tools package | 14.0.5.0-1 | [mssql-tools Debian package](https://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/mssql-tools/mssql-tools_14.0.5.0-1_amd64.deb) |
| Ubuntu 16.10 Debian tools package | 14.0.5.0-1 | [mssql-tools Debian package](https://packages.microsoft.com/ubuntu/16.10/prod/pool/main/m/mssql-tools/mssql-tools_14.0.5.0-1_amd64.deb) |

These packages depend on **msodbcsql**, which must be installed first. The **msodbcsql** pacakage also has a dependency on either **unixODBC-devel** (RPM) or **unixodbc-dev** (Debian). The location of the **msodbcsql** packages are listed in the following table.

| msodbcsql package | Version | Download |
|-----|-----|-----|
| Red Hat RPM msodbcsql package | 13.1.6.0-1 | [msodbcsql RPM package](https://packages.microsoft.com/rhel/7.3/prod/msodbcsql-13.1.6.0-1.x86_64.rpm) | 
| SLES RPM msodbcsql package | 13.1.6.0-1 | [msodbcsql RPM package](https://packages.microsoft.com/sles/12/prod/msodbcsql-13.1.6.0-1.x86_64.rpm) | 
| Ubuntu 16.04 Debian msodbcsql package | 13.1.6.0-1 | [msodbcsql Debian package](https://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/msodbcsql/msodbcsql_13.1.6.0-1_amd64.deb) |
| Ubuntu 16.10 Debian msodbcsql package | 13.1.6.0-1 | [msodbcsql Debian package](https://packages.microsoft.com/ubuntu/16.10/prod/pool/main/m/msodbcsql/msodbcsql_13.1.6.0-1_amd64.deb) |

In some cases, you might have to manually install the dependencies for the **mssql-tools** and **msodbcsql** packages. For Debian packages, you can inspect the dependencies with the following commands:

```bash
dpkg -I mssql-tools_14.0.5.0-1_amd64.deb | grep "Depends:"
dpkg -I msodbcsql_13.1.6.0-1_amd64.deb | grep "Depends:"
```

For RPM packages, you can inspect the dependencies with the following commands:

```bash
rpm -qpR mssql-tools-14.0.5.0-1.x86_64.rpm
rpm -qpR msodbcsql-13.1.6.0-1.x86_64.rpm
```

## Next steps

After installation, connect to the SQL Server instance to create and manage databases. To get started, see [Connect and query SQL Server on Linux](sql-server-linux-connect-and-query-sqlcmd.md).
