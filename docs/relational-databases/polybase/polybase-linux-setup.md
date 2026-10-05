---
title: Install PolyBase on Linux
titleSuffix: SQL Server
description: Learn how to install SQL Server PolyBase on Linux. PolyBase enables you to run external queries against remote data sources.
author: markingmyname
ms.author: maghan
ms.reviewer: dakryze, hudequei, randolphwest
ms.date: 04/27/2026
ms.service: sql
ms.subservice: linux
ms.topic: install-set-up-deploy
ms.custom:
  - intro-installation
  - linux-related-content
monikerRange: ">=sql-server-linux-ver15 || >=sql-server-ver15"
---
# Install PolyBase on Linux

[!INCLUDE [sqlserver2019-linux](../../includes/applies-to-version/sqlserver2019-linux.md)]

The following steps install [PolyBase](overview.md) (`mssql-server-polybase` and `mssql-server-polybase-hadoop`) on Linux. With PolyBase, you can run external queries against remote data sources.

## Prerequisites

Before you install PolyBase, first [install SQL Server](../../linux/sql-server-linux-setup.md#platforms). This step configures the keys and repositories that you use when installing the `mssql-server-polybase` and `mssql-server-polybase-hadoop` packages.

## Support for ODBC data sources

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]

In [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions, PolyBase supports ODBC data sources on Linux. ODBC data source support for Linux requires the .NET runtime, which is automatically downloaded and installed during PolyBase setup. Internet access is required during the installation.

## Limitations

The hostname where [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is installed has a maximum length of 15 characters.

PolyBase isn't supported on [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] for Linux.

Scale-out for PolyBase on Linux isn't currently available.

Hadoop isn't supported on [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions.

## Install PolyBase

Install PolyBase for your operating system:

- Red Hat Enterprise Linux (RHEL)
- Ubuntu
- SUSE Linux Enterprise Server (SLES)

## [Red Hat Enterprise Linux (RHEL)](#tab/rhel)

### Install on RHEL

**Applies to**: [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions.

1. Download the Microsoft Red Hat repository configuration file.

   For RHEL 10:

   ```console
   sudo curl -o /etc/yum.repos.d/msprod.repo https://packages.microsoft.com/config/rhel/10/prod.repo
   ```

   For RHEL 9:

   ```console
   sudo curl -o /etc/yum.repos.d/msprod.repo https://packages.microsoft.com/config/rhel/9/prod.repo
   ```

   For RHEL 8:

   ```console
   sudo curl -o /etc/yum.repos.d/msprod.repo https://packages.microsoft.com/config/rhel/8/prod.repo
   ```

   For RHEL 7:

   ```console
   sudo curl -o /etc/yum.repos.d/msprod.repo https://packages.microsoft.com/config/rhel/7/prod.repo
   ```

1. Use the following command to install the `mssql-server-polybase` on Red Hat Enterprise Linux:

   ```console
   sudo yum install -y mssql-server-polybase
   ```

1. Restart the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance when prompted:

   ```console
   sudo systemctl restart mssql-server
   ```

> [!NOTE]  
> After installation, [enable the PolyBase feature](#enable).

::: moniker range="<=sql-server-linux-ver15 || <=sql-server-ver15"

### Install Hadoop on RHEL

**Applies to**: [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] only.

1. Use the following command to install the `mssql-server-polybase-hadoop` package:

   ```console
   sudo yum install -y mssql-server-polybase-hadoop
   ```

   The PolyBase Hadoop package depends on the following packages:

   - `mssql-server`
   - `mssql-server-polybase`
   - `mssql-server-extensibility`
   - `mssql-zulu-jre-11`

1. Restart `launchpadd` when prompted:

   ```console
   sudo systemctl restart mssql-launchpadd
   ```

> [!NOTE]  
> After installation, you must [set the Hadoop connectivity level](../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md#c-set-hadoop-connectivity).

If you need an offline installation, find the PolyBase package download in the [Latest updates and version history for SQL Server on Linux](https://support.microsoft.com/help/5122767). Then use the same offline installation steps described in the article [Install SQL Server](../../linux/sql-server-linux-setup.md#offline).

::: moniker-end

## [Ubuntu](#tab/ubuntu)

### Install on Ubuntu

**Applies to**: [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions.

1. Register the Microsoft Ubuntu repository.

   For Ubuntu 16.04:

   ```console
   curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
   ```

   For Ubuntu 18.04:

   ```console
   curl https://packages.microsoft.com/config/ubuntu/18.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
   ```

   For Ubuntu 20.04:

   ```console
   curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
   ```

   For Ubuntu 22.04:

   ```console
   curl https://packages.microsoft.com/config/ubuntu/22.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
   ```

   For Ubuntu 24.04:

   ```console
   curl https://packages.microsoft.com/config/ubuntu/24.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
   ```

1. Use the following command to install the `mssql-server-polybase` on Ubuntu:

   ```console
   sudo apt-get install mssql-server-polybase
   ```

1. Restart the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance when prompted:

   ```console
   sudo systemctl restart mssql-server
   ```

> [!NOTE]  
> After installation, [enable the PolyBase feature](#enable).

If you need an offline installation, find the PolyBase package download in the [Latest updates and version history for SQL Server on Linux](https://support.microsoft.com/help/5122767). Then use the same offline installation steps described in the article [Install SQL Server](../../linux/sql-server-linux-setup.md#offline).

::: moniker range="<=sql-server-linux-ver15 || <=sql-server-ver15"

### Install Hadoop on Ubuntu

**Applies to**: [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] only.

1. Use the following command to install the `mssql-server-polybase-hadoop` package:

   ```console
   sudo apt-get install mssql-server-polybase-hadoop
   ```

   The PolyBase Hadoop package depends on the following packages:

   - `mssql-server`
   - `mssql-server-polybase`
   - `mssql-server-extensibility`
   - `mssql-zulu-jre-11`

1. Restart `launchpadd` when prompted:

   ```console
   sudo systemctl restart mssql-launchpadd
   ```

> [!NOTE]  
> After installation, you must [set the Hadoop connectivity level](polybase-configure-hadoop.md#configure-hadoop-connectivity). This step only applies to [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)].

::: moniker-end

## [SUSE Linux Enterprise Server (SLES)](#tab/sles)

### Install on SLES

**Applies to**: [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions.

1. Add the Microsoft [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] repository to Zypper.

   For SLES 12:

   ```console
   sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/12/prod.repo
   sudo zypper --gpg-auto-import-keys refresh
   ```

   For SLES 15:

   ```console
   sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/15/prod.repo
   sudo zypper --gpg-auto-import-keys refresh
   ```

1. Use the following commands to install the `mssql-server-polybase` package on SUSE Linux Enterprise Server:

   ```console
   sudo zypper install mssql-server-polybase
   ```

1. Restart the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance when prompted:

   ```console
   sudo systemctl restart mssql-server
   ```

> [!NOTE]  
> After installation, [enable the PolyBase feature](#enable).

If you need an offline installation, find the PolyBase package download in the [Latest updates and version history for SQL Server on Linux](https://support.microsoft.com/help/5122767). Then use the same offline installation steps described in the article [Install SQL Server](../../linux/sql-server-linux-setup.md#offline).

---

<a id="enable"></a>

## Enable PolyBase

After installation, enable PolyBase to access its features. Connect to the installed [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance and run the following Transact-SQL command:

```sql
EXECUTE sp_configure
    @configname = 'polybase enabled',
    @configvalue = 1;

RECONFIGURE WITH OVERRIDE;
```

### Trace flag

**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]

To use PolyBase capabilities on Linux, you must enable [trace flag 13702](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf13702) during SQL Server start up. For more information, see [Configure SQL Server on Linux with the mssql-conf tool](../../linux/sql-server-linux-configure-mssql-conf.md).

## Update PolyBase

If you already have `mssql-server-polybase` installed, you can update to the latest version with the following commands:

## [Red Hat Enterprise Linux (RHEL)](#tab/rhel)

### RHEL with Hadoop

**Applies to**: [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] only.

```console
sudo yum remove -y mssql-server-polybase-hadoop
sudo yum remove -y mssql-server-polybase
sudo yum check-update
sudo yum install -y mssql-server-polybase
sudo yum install -y mssql-server-polybase-hadoop
```

### RHEL without Hadoop

```console
sudo yum remove -y mssql-server-polybase
sudo yum check-update
sudo yum install -y mssql-server-polybase
```

Restart the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance when prompted:

```console
sudo systemctl restart mssql-server
```

## [Ubuntu](#tab/ubuntu)

### Ubuntu with Hadoop

**Applies to**: [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] only.

```console
sudo apt-get remove mssql-server-polybase-hadoop
sudo apt-get remove mssql-server-polybase
sudo apt-get update
sudo apt-get install mssql-server-polybase
sudo apt-get install mssql-server-polybase-hadoop
```

### Ubuntu without Hadoop

```console
sudo apt-get remove mssql-server-polybase
sudo apt-get update
sudo apt-get install mssql-server-polybase
```

Restart the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance when prompted:

```console
sudo systemctl restart mssql-server
```

## [SUSE Linux Enterprise Server (SLES)](#tab/sles)

### SLES

```console
sudo zypper remove mssql-server-polybase
sudo zypper refresh
sudo zypper install mssql-server-polybase
```

Restart the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance when prompted:

```console
sudo systemctl restart mssql-server
```

---

> [!NOTE]  
> After installation, [enable the PolyBase feature](#enable).

## PolyBase offline installation

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.

In [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], PolyBase on Linux supports ODBC data sources and requires .NET components that the package manager typically installs.

Starting with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] Cumulative Update (CU) 4, you can install the required .NET components offline. This method is useful for large-scale deployments and environments without internet access.

You need a machine with internet access to download the .NET runtime and a target [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] machine where you install PolyBase.

1. On a machine with internet access, download the supported .NET runtime that PolyBase requires (**.NET 8.0.418**). Extract the package, and copy the extracted files to the target [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] machine.

   On the target machine, create the following directory if it doesn't exist: `/opt/mssql-ees-dotnet/`.

   Copy the extracted .NET components to `/opt/mssql-ees-dotnet/`.

1. Install PolyBase.

   If setup can't find the components in the default location (`/opt/mssql-ees-dotnet/`), provide the path when prompted.

1. If you don't provide a path, setup prompts you to download the components.

## Related links

PolyBase on Linux can access the following data sources. Use these links for information on how to create an external table when PolyBase is enabled:

- [SQL Server and Azure SQL](polybase-configure-sql-server.md)
- [Hadoop](polybase-configure-hadoop.md)
- [Azure Blob Storage](polybase-configure-azure-blob-storage.md)
- [Oracle](polybase-configure-oracle.md)
- [Teradata](polybase-configure-teradata.md)
- [MongoDB and Azure Cosmos DB](polybase-configure-mongodb.md)

## Related content

- [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md)
