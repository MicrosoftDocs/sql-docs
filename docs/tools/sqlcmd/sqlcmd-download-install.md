---
title: Download and Install the sqlcmd Utility
description: Learn how to download, install, or find the sqlcmd utility preinstalled on your system.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dlevy
ms.date: 06/30/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: install-set-up-deploy
ms.collection:
  - data-tools
ms.custom:
  - linux-related-content
  - ignite-2025
helpviewer_keywords:
  - "statements [SQL Server], command prompt"
  - "go-sqlcmd"
  - "QUIT command"
  - "Transact-SQL statements, command prompt"
  - "EXIT command"
  - "sqlcmd commands"
  - "ED command"
  - "sqlcmd utility"
  - "command prompt utilities [SQL Server], sqlcmd"
  - "!! command"
  - "stored procedures [SQL Server], command prompt"
  - "system stored procedures [SQL Server], command prompt"
  - "sqlcmd utility, about sqlcmd utility"
  - "scripts [SQL Server], command prompt"
  - "RESET command"
  - "GO command"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =fabric-sqldb"
---
# Download and install the sqlcmd utility

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

The [sqlcmd utility](sqlcmd-utility.md) lets you enter Transact-SQL statements, system procedures, and script files.

## `sqlcmd` variants

There are two variants of **`sqlcmd`**:

- **`sqlcmd`** (Go): The `go-mssqldb`-based **`sqlcmd`**, sometimes styled as **`go-sqlcmd`**. This version is a standalone tool you can download independently of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. It runs on Windows, macOS, Linux, and in containers.

- **`sqlcmd`** (ODBC): The platform-aligned, ODBC-based **`sqlcmd`**, available with [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] or the Microsoft Command Line Utilities, and part of the `mssql-tools` package on Linux. It also runs on Windows, macOS, Linux, and in containers.

## Download and install sqlcmd (Go)

**`sqlcmd`** (Go) can be installed cross-platform, on Microsoft Windows, macOS, and Linux. Versions newer than 1.6 might not be available in all package managers. There's no estimated date yet for their availability.

### [Windows](#tab/windows)

Choose one of the following options to install **`sqlcmd`** (Go) on Windows.

#### winget (Windows Package Manager CLI)

1. Install the [Windows Package Manager Client](/windows/package-manager/winget) if you don't already have it.
1. Run the following command to install **`sqlcmd`** (Go).

   ```console
   winget install sqlcmd
   ```

#### Chocolatey

1. Install [Chocolatey](https://community.chocolatey.org/) if you don't already have it.
1. Run the following command to install **`sqlcmd`** (Go).

   ```console
   choco install sqlcmd
   ```

#### Direct download

1. Download the corresponding `-windows-amd64.zip` or `-windows-arm.zip` asset from the [latest](https://github.com/microsoft/go-sqlcmd/releases/latest) release of **`sqlcmd`** (Go) from the GitHub code repository.

1. Extract the `sqlcmd.exe` file from the downloaded zip folder.

### [macOS](#tab/mac)

Choose one of the following options to install **`sqlcmd`** (Go) on macOS.

#### Homebrew

1. Install Homebrew if you need to.

   ```bash
   /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
   ```

1. Install **`sqlcmd`** with Homebrew.

   ```bash
   brew install sqlcmd
   ```

#### Direct download

1. Download the `-darwin-amd64.zip` asset from the [latest](https://github.com/microsoft/go-sqlcmd/releases/latest) release of **`sqlcmd`** (Go) from the GitHub code repository.

1. Extract the `sqlcmd` file from the downloaded zip folder.

### [Linux](#tab/linux)

Choose one of the following options to install **`sqlcmd`** (Go) on Linux.

#### apt (Debian/Ubuntu)

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   ```

1. Add the Microsoft repository, where the `ubuntu/20.04` segment might be `debian/11`, `ubuntu/20.04`, or `ubuntu/22.04`.

   ```bash
   add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/20.04/prod.list)"
   ```

1. Install **`sqlcmd`** (Go) with **apt**.

   ```bash
   apt-get update
   apt-get install sqlcmd
   ```

#### yum (Fedora)

1. Import the Microsoft repository key.

   ```bash
   rpm --import https://packages.microsoft.com/keys/microsoft.asc
   ```

1. Download the repository configuration file, where the `fedora/32` segment might be `opensuse/42.3`, `rhel/8`, or `sles/15`. If the version of your OS doesn't directly correspond to one of those options, you might be able to use a repository configuration file from a version.

   ```bash
   curl -o /etc/yum.repos.d/packages-microsoft-com-prod.repo https://packages.microsoft.com/config/fedora/40/prod.repo
   ```

1. Install **`sqlcmd`** (Go) with **yum**.

   ```bash
   yum install sqlcmd
   ```

#### Direct download

1. Download the corresponding `-linux-x64.tar.bz2` or `-linux-arm.tar.bz2` asset from the [latest](https://github.com/microsoft/go-sqlcmd/releases/latest) release of **`sqlcmd`** (Go) from the GitHub code repository.

1. Extract the `sqlcmd` file from the downloaded zip folder.

---

## Download and install sqlcmd (ODBC)

**`sqlcmd`** (ODBC) can be installed cross-platform, on Microsoft Windows, macOS, and Linux.

### [Windows](#tab/windows)

Download the command line utilities for Windows, using the following table.

| Driver | Platform |
| --- | --- |
| Microsoft Command Line Utilities for SQL Server | [x64](https://go.microsoft.com/fwlink/?linkid=2370127)&emsp;[x86](https://go.microsoft.com/fwlink/?linkid=2370024) |

The command line tools are General Availability (GA), however they're also released with the installer package for [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] and later versions.

#### Version information

- Release number: 17.0.4055.5
- Build number: 17.0.4055.5
- Release date: June 30, 2026

> [!NOTE]  
> The **`sqlcmd`** download provided here might not have the same release and build number as **`sqlcmd`** installed with the latest SQL Server cumulative update (CU). This behavior is expected. This version contains all the fixes included in the latest CU.

**`sqlcmd`** (ODBC) supports authentication with [Microsoft Entra ID](/azure/active-directory/fundamentals/new-name), including multifactor authentication (MFA) support for Azure SQL Database, Azure Synapse Analytics, and Always Encrypted features.

#### System requirements

- Windows 10 and later versions
- Windows Server 2016 and later versions

This component requires [the latest Microsoft ODBC Driver for SQL Server](../../connect/odbc/download-odbc-driver-for-sql-server.md).

### [macOS](#tab/mac)

For instructions to install **`sqlcmd`** on macOS, see [Install the sqlcmd and bcp SQL Server command-line tools on Linux](../../linux/install-upgrade/setup-tools.md).

### [Linux](#tab/linux)

For instructions to install **`sqlcmd`** on Linux, see [Install the sqlcmd and bcp SQL Server command-line tools on Linux](../../linux/install-upgrade/setup-tools.md).

---

## Preinstalled

You can also find **`sqlcmd`** preinstalled in certain environments.

### Azure Cloud Shell

You can try the **`sqlcmd`** utility from Azure Cloud Shell, as it's preinstalled by default.

[Launch Cloud Shell](https://shell.azure.com)

### SQL Server Management Studio (SSMS)

To run SQLCMD statements in [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms), navigate to **Query** > **SQLCMD Mode**.

SSMS uses the Microsoft [!INCLUDE [dnprdnshort-md](../../includes/dnprdnshort-md.md)] `SqlClient` for execution in regular and SQLCMD mode in **Query Editor**. When **`sqlcmd`** is run from the command-line, **`sqlcmd`** uses the ODBC driver. Because different default options could apply, you might see different behavior when you execute the same query in SSMS in SQLCMD mode and in the **`sqlcmd`** utility.

## Related content

- [sqlcmd utility](sqlcmd-utility.md)
- [Check installed version of sqlcmd utility](sqlcmd-installed-version.md)
- [Commands in the sqlcmd utility](sqlcmd-commands.md)
- [Start the sqlcmd utility](sqlcmd-start-utility.md)
- [Execute T-SQL from a script file with sqlcmd](sqlcmd-run-transact-sql-script-files.md)
- [Use sqlcmd](sqlcmd-use-utility.md)
