---
title: Install SQL Server command-line tools on Linux
titleSuffix: SQL Server
description: Learn how to install the SQL Server command-line tools, Microsoft ODBC drivers, and their dependencies on Linux.
author: VanMSFT
ms.author: vanto
ms.date: 02/01/2022
ms.topic: conceptual
ms.service: sql
ms.custom:
  - sqlfreshmay19
  - intro-installation
ms.subservice: linux
ms.assetid: eff8e226-185f-46d4-a3e3-e18b7a439e63
---
# Install the SQL Server command-line tools sqlcmd and bcp on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

The following steps install the command-line tools, Microsoft ODBC drivers, and their dependencies. The **mssql-tools** package contains:

- **sqlcmd**: Command-line query utility.
- **bcp**: Bulk import-export utility.

Install the tools for your platform:

- [Red Hat Enterprise Linux](#RHEL)
- [Ubuntu](#ubuntu)
- [SUSE Linux Enterprise Server](#SLES)
- [macOS](#macos)
- [Docker](#docker)

This article describes how to install the command-line tools. If you are looking for examples of how to use **sqlcmd** or **bcp**, see the [links](#next-steps) at the end of this topic.

> [!Warning]
> sqlcmd and bcp are available in mssql-tools for x64 architecture. An alternative for both arm64 and x64 environments is in preview across Linux, macOS, and Windows, [go-sqlcmd on GitHub](https://github.com/microsoft/go-sqlcmd).

## <a id="RHEL"><a/>Install tools on RHEL 8

Use the following steps to install the **mssql-tools** on Red Hat Enterprise Linux. 

1. Enter superuser mode.

   ```bash
   sudo su
   ```

1. Download the Microsoft Red Hat repository configuration file.

   ```bash
   curl https://packages.microsoft.com/config/rhel/8/prod.repo > /etc/yum.repos.d/msprod.repo
   ```

1. Exit superuser mode.

   ```bash
   exit
   ```

1. If you had a previous version of **mssql-tools** installed, remove any older unixODBC packages.

   ```bash
   sudo yum remove mssql-tools unixODBC-utf16-devel
   ```

1. Run the following commands to install **mssql-tools** with the unixODBC developer package.

   ```bash
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

## <a id="ubuntu"></a>Install tools on Ubuntu 20.04

Use the following steps to install the **mssql-tools** on Ubuntu.

> [!NOTE]
>
> - Ubuntu 18.04 is supported starting with SQL Server 2019 CU3.
> - Ubuntu 20.04 is supported starting with SQL Server 2019 CU10.
> - If you are using Ubuntu 16.04 or Ubuntu 18.04, change the repository path in step 2 below from `/ubuntu/20.04` to `/ubuntu/16.04` or `/ubuntu/18.04`.

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
   ```

1. Register the Microsoft Ubuntu repository.

   ```bash
   curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
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

## <a id="SLES"></a>Install tools on SLES 12

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

A preview of **sqlcmd** and **bcp** is now available on macOS. For more information, see the [announcement](https://blogs.technet.microsoft.com/dataplatforminsider/2017/05/16/sql-server-command-line-tools-for-macos-released/).

*Install [Homebrew](https://brew.sh) if you don't have it already:*

- `/usr/bin/ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"`

To install the tools for Mac El Capitan and Sierra, use the following commands:

```bash
# brew untap microsoft/mssql-preview if you installed the preview version 
brew tap microsoft/mssql-release https://github.com/Microsoft/homebrew-mssql-release
brew update
brew install mssql-tools
#for silent install: 
#HOMEBREW_NO_ENV_FILTERING=1 ACCEPT_EULA=y brew install mssql-tools
```

## <a id="docker"></a> Docker

If you [run SQL Server in a Docker container](quickstart-install-connect-docker.md), the SQL Server command-line tools are already included in the SQL Server Linux container image. If you attach to a running container with an interactive bash shell, you can run the tools locally.

If you are creating a container with the SQL Server command-line tools, it is recommended to add `ACCEPT_EULA=Y` to the installation command to silently accept the EULA and not interupt image creation.  An example final command as part of installation on an Ubuntu-based image is:
```bash
sudo ACCEPT_EULA=Y apt-get install mssql-tools unixodbc-dev
```

## Offline installation

[!INCLUDE[SQL Server Linux offline package installation](../includes/sql-server-linux-offline-package-install-intro.md)]

1. First, locate and copy the **mssql-tools** package for your Linux distribution:

   | Linux distribution | **mssql-tools** package location |
   |---|---|
   | Red Hat | [https://packages.microsoft.com/rhel/7.3/prod](https://packages.microsoft.com/rhel/7.3/prod) |
   | SLES | [https://packages.microsoft.com/sles/12/prod](https://packages.microsoft.com/sles/12/prod)|
   | Ubuntu 20.04 | [https://packages.microsoft.com/ubuntu/20.04/prod/pool/main/m/mssql-tools](https://packages.microsoft.com/ubuntu/20.04/prod/pool/main/m/mssql-tools) |

1. Also locate and copy the **msodbcsql** package, which is a dependency. The **msodbcsql** package also has a dependency on either **unixODBC-devel** (Red Hat and SLES) or **unixodbc-dev** (Ubuntu). The location of the **msodbcsql** packages are listed in the following table:

   | Linux distribution | ODBC packages location |
   |---|---|
   | Red Hat | [https://packages.microsoft.com/rhel/8/prod](https://packages.microsoft.com/rhel/8/prod) |
   | SLES | [https://packages.microsoft.com/sles/12/prod](https://packages.microsoft.com/sles/12/prod)|
   | Ubuntu 20.04 | [**msodbcsql**](https://packages.microsoft.com/ubuntu/20.04/prod/pool/main/m/msodbcsql17/)<br/>[**unixodbc-dev**](https://packages.microsoft.com/ubuntu/20.04/prod/pool/main/u/unixodbc/) |

1. **Move the downloaded packages to your Linux machine**. If you used a different machine to download the packages, one way to move the packages to your Linux machine is with the **scp** command.

1. **Install the and packages**: Install the **mssql-tools** and **msodbc** packages. If you get any dependency errors, ignore them until the next step.

    | Platform | Package install commands |
    |-----|-----|
    | Red Hat | `sudo yum localinstall msodbcsql-<version>.rpm`<br/>`sudo yum localinstall mssql-tools-<version>.rpm` |
    | SLES | `sudo zypper install msodbcsql-<version>.rpm`<br/>`sudo zypper install mssql-tools-<version>.rpm` |
    | Ubuntu | `sudo dpkg -i msodbcsql_<version>.deb`<br/>`sudo dpkg -i mssql-tools_<version>.deb` |

1. **Resolve missing dependencies**: You might have missing dependencies at this point. If not, you can skip this step. In some cases, you must manually locate and install these dependencies.

    For RPM packages, you can inspect the required dependencies with the following commands:

    ```bash
    rpm -qpR msodbcsql-<version>.rpm
    rpm -qpR mssql-tools-<version>.rpm
    ```

    For Debian packages, if you have access to approved repositories containing those dependencies, the easiest solution is to use the **apt-get** command:

    ```bash
    sudo apt-get -f install
    ```

    > [!NOTE]
    > This command completes the installation of the SQL Server packages as well.

    If this does not work for your Debian package, you can inspect the required dependencies with the following commands:

    ```bash
    dpkg -I msodbcsql_<version>_amd64.deb | grep "Depends:"
    dpkg -I mssql-tools_<version>_amd64.deb | grep "Depends:"
    ```

## Next steps

For an example of how to use **sqlcmd** to connect to SQL Server and create a database, see one of the following quickstarts:

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
- [Install on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Run on Docker](quickstart-install-connect-docker.md)

For an example of how to use **bcp** to bulk import and export data, see [Bulk copy data to SQL Server on Linux](sql-server-linux-migrate-bcp.md).
