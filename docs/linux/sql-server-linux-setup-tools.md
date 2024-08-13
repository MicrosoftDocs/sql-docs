---
title: Install SQL Server command-line tools on Linux
titleSuffix: SQL Server
description: Learn how to install the SQL Server command-line tools, Microsoft ODBC drivers, and their dependencies on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/22/2024
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - intro-installation
  - linux-related-content
---
# Install the SQL Server command-line tools sqlcmd and bcp on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

The following steps install the command-line tools, Microsoft ODBC drivers, and their dependencies. The **mssql-tools** package contains:

- **sqlcmd**: Command-line query utility.
- **bcp**: Bulk import-export utility.

Install the tools for your platform:

- [Red Hat Enterprise Linux](?tabs=redhat-install#RHEL)
- [SUSE Linux Enterprise Server](?tabs=sles-install#SLES)
- [Ubuntu](?tabs=ubuntu-install#ubuntu)
- [macOS](#macos)
- [Docker](#docker)

This article describes how to install the command-line tools. If you're looking for examples of how to use **sqlcmd** or **bcp**, see the [Related content](#related-content) at the end of this article.

> [!IMPORTANT]  
> **sqlcmd** and **bcp** are available in **mssql-tools18** for `x64` and `arm64` architectures. For a modern alternative across Linux, macOS, and Windows, see [go-sqlcmd utility](../tools/sqlcmd/go-sqlcmd-utility.md).

## Install tools on Linux

These instructions are for installing the [!INCLUDE [msconame-md](../includes/msconame-md.md)] ODBC 18 packages. For previous versions, see [Install the Microsoft ODBC driver for SQL Server (Linux)](../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md).

### [Red Hat Enterprise Linux](#tab/redhat-install)

[!INCLUDE [odbc-redhat](includes/odbc-redhat.md)]

### [SUSE Linux Enterprise Server](#tab/sles-install)

[!INCLUDE [odbc-sles](includes/odbc-sles.md)]

### [Ubuntu](#tab/ubuntu-install)

[!INCLUDE [odbc-ubuntu](includes/odbc-ubuntu.md)]

---

## <a id="macos"></a> Install tools on macOS

Install [Homebrew](https://brew.sh) if you don't have it already:

```bash
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
```

To install the tools for macOS El Capitan and later versions, use the following commands:

```bash
# brew untap microsoft/mssql-preview if you installed the preview version
brew tap microsoft/mssql-release https://github.com/Microsoft/homebrew-mssql-release
brew update
brew install mssql-tools18
```

## <a id="docker"></a> Install tools on Docker

If you [run SQL Server in a Docker container](quickstart-install-connect-docker.md), the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] command-line tools are already included in the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Linux container image. If you attach to a running container with an interactive bash shell, you can run the tools locally.

If you're creating a container with the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] command-line tools, you should add `ACCEPT_EULA=Y` to the installation command to silently accept the EULA, and not interrupt image creation. An example final command as part of installation on an Ubuntu-based image is:

```bash
sudo ACCEPT_EULA=Y apt-get install mssql-tools18 unixodbc-dev
```

## Offline installation

[!INCLUDE [offline-package-install-intro](includes/offline-package-install-intro.md)]

### [Red Hat Enterprise Linux](#tab/redhat-install)

1. First, locate and copy the **mssql-tools18** package for your Linux distribution. For Red Hat 8.0, this package is located at <https://packages.microsoft.com/rhel/8/prod>.

1. Also locate and copy the **msodbcsql18** package, which is a dependency. The **msodbcsql18** package also has a dependency on **unixODBC-devel**. For Red Hat, the **msodbcsql18** package is located at <https://packages.microsoft.com/rhel/8/prod>.

1. **Move the downloaded packages to your Linux machine**. If you used a different machine to download the packages, one way to move the packages to your Linux machine is with the **scp** command.

1. **Install the and packages**: Install the **mssql-tools18** and **msodbc18** packages. If you get any dependency errors, ignore them until the next step. Replace `<version>` with the correct version:

   ```bash
   sudo yum localinstall msodbcsql18-<version>.rpm
   sudo yum localinstall mssql-tools18-<version>.rpm
   ```

1. **Resolve missing dependencies**: You might have missing dependencies at this point. If not, you can skip this step. In some cases, you must manually locate and install these dependencies.

   You can inspect the required dependencies with the following commands. Replace `<version>` with the correct version:

   ```bash
   rpm -qpR msodbcsql18-<version>.rpm
   rpm -qpR mssql-tools18-<version>.rpm
    ```

### [SUSE Linux Enterprise Server](#tab/sles-install)

1. First, locate and copy the **mssql-tools18** package for your Linux distribution. For SLES 15, this package is located at <https://packages.microsoft.com/sles/15/prod>.

1. Also locate and copy the **msodbcsql18** package, which is a dependency. The **msodbcsql18** package also has a dependency on **unixODBC-devel**. For SLES, the **msodbcsql18** package is located at <https://packages.microsoft.com/sles/15/prod>.

1. **Move the downloaded packages to your Linux machine**. If you used a different machine to download the packages, one way to move the packages to your Linux machine is with the **scp** command.

1. **Install the and packages**: Install the **mssql-tools18** and **msodbc18** packages. If you get any dependency errors, ignore them until the next step. Replace `<version>` with the correct version:

   ```bash
   sudo zypper install msodbcsql18-<version>.rpm
   sudo zypper install mssql-tools18-<version>.rpm
   ```

1. **Resolve missing dependencies**: You might have missing dependencies at this point. If not, you can skip this step. In some cases, you must manually locate and install these dependencies.

   You can inspect the required dependencies with the following commands. Replace `<version>` with the correct version:

   ```bash
   rpm -qpR msodbcsql18-<version>.rpm
   rpm -qpR mssql-tools18-<version>.rpm
    ```

### [Ubuntu](#tab/ubuntu-install)

1. First, locate and copy the **mssql-tools18** package for your Linux distribution. For Ubuntu 20.04, this package is located at <https://packages.microsoft.com/ubuntu/20.04/prod/pool/main/m/mssql-tools>.

1. Also locate and copy the **msodbcsql18** package, which is a dependency. The **msodbcsql18** package also has a dependency on **unixodbc-dev**. For Ubuntu, the **msodbcsql18** packages are located at [**msodbcsql18**](https://packages.microsoft.com/ubuntu/20.04/prod/pool/main/m/msodbcsql17/), and [**unixodbc-dev**](https://packages.microsoft.com/ubuntu/20.04/prod/pool/main/u/unixodbc/).

1. **Move the downloaded packages to your Linux machine**. If you used a different machine to download the packages, one way to move the packages to your Linux machine is with the **scp** command.

1. **Install the and packages**: Install the **mssql-tools18** and **msodbc18** packages. If you get any dependency errors, ignore them until the next step. Replace `<version>` with the correct version:

   ```bash
   sudo dpkg -i msodbcsql18_<version>.deb
   sudo dpkg -i mssql-tools18_<version>.deb
   ```

1. **Resolve missing dependencies**: You might have missing dependencies at this point. If not, you can skip this step. In some cases, you must manually locate and install these dependencies.

   If you have access to approved repositories containing those dependencies, the easiest solution is to use the **apt-get** command:

   ```bash
   sudo apt-get -f install
   ```

   This command completes the installation of the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] packages as well.

   If this step doesn't work for your Debian package, you can inspect the required dependencies with the following commands:

   ```bash
   dpkg -I msodbcsql18_<version>_amd64.deb | grep "Depends:"
   dpkg -I mssql-tools18_<version>_amd64.deb | grep "Depends:"
   ```

---

## Related content

- [Quickstart: Install SQL Server and create a database on Red Hat](quickstart-install-connect-red-hat.md)
- [Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Quickstart: Install SQL Server and create a database on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Quickstart: Run SQL Server Linux container images with Docker](quickstart-install-connect-docker.md)
- [Bulk copy data with bcp to SQL Server on Linux](sql-server-linux-migrate-bcp.md)

[!INCLUDE [contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]
