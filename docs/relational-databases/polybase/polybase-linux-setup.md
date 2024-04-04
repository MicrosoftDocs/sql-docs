---
title: Install PolyBase on Linux
titleSuffix: SQL Server
description: Learn how to install SQL Server PolyBase on Linux. PolyBase enables you to run external queries against remote data sources.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: dakryze, randolphwest
ms.date: 12/29/2023
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom: intro-installation, linux-related-content
monikerRange: ">=sql-server-linux-ver15 || >=sql-server-ver15"
---
# Install PolyBase on Linux

[!INCLUDE [sqlserver2019-linux](../../includes/applies-to-version/sqlserver2019-linux.md)]

The following steps install [PolyBase](polybase-guide.md) (`mssql-server-polybase` and `mssql-server-polybase-hadoop`) on Linux. PolyBase enables you to run external queries against remote data sources.

## Prerequisites

Before you install PolyBase, first [install SQL Server](../../linux/sql-server-linux-setup.md#platforms). This step configures the keys and repositories that you use when installing the `mssql-server-polybase` and `mssql-server-polybase-hadoop` package.

## Limitations

The length of the hostname where [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is installed needs to be 15 characters or less.

PolyBase isn't supported on [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] for Linux.

Scale-out for PolyBase on Linux is currently unavailable.

Hadoop is no longer supported on [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

## Install PolyBase

Install PolyBase for your operating system:

- Red Hat Enterprise Linux (RHEL)
- Ubuntu
- SUSE Linux Enterprise Server (SLES)

## [Red Hat Enterprise Linux (RHEL)](#tab/rhel)

### Install on RHEL

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions

1. Download the Microsoft Red Hat repository configuration file.

   For RHEL 7:

   ```console
   sudo curl -o /etc/yum.repos.d/msprod.repo https://packages.microsoft.com/config/rhel/7/prod.repo
   ```

   For RHEL 8:

   ```console
   sudo curl -o /etc/yum.repos.d/msprod.repo https://packages.microsoft.com/config/rhel/8/prod.repo
   ```

   For RHEL 9:

   ```console
   sudo curl -o /etc/yum.repos.d/msprod.repo https://packages.microsoft.com/config/rhel/9/prod.repo
   ```

1. Use the following command to install the `mssql-server-polybase` on Red Hat Enterprise Linux.

   ```console
   sudo yum install -y mssql-server-polybase
   ```

1. You're prompted to restart the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance. Use the following command to do so.

   ```console
   sudo systemctl restart mssql-server
   ```

> [!NOTE]  
> After installation, you must [enable the PolyBase feature](#enable).

### Install Hadoop on RHEL

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]

1. Use the following command to install the `mssql-server-polybase-hadoop`.

   ```console
   sudo yum install -y mssql-server-polybase-hadoop
   ```

   The PolyBase Hadoop package has dependencies on the following packages:

   - `mssql-server`
   - `mssql-server-polybase`
   - `mssql-server-extensibility`
   - `mssql-zulu-jre-11`

1. Installation prompts to restart `launchpadd`. Use the following command to do so.

   ```console
   sudo systemctl restart mssql-launchpadd
   ```

> [!NOTE]  
> After installation, you must [set the Hadoop connectivity level](../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md#c-set-hadoop-connectivity).

If you need an offline installation, locate the PolyBase package download in the [Release notes for SQL Server 2019 on Linux](../../linux/sql-server-linux-release-notes-2019.md). Then use the same offline installation steps described in the article [Install SQL Server](../../linux/sql-server-linux-setup.md#offline).

## [Ubuntu](#tab/ubuntu)

### Install on Ubuntu

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions

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

1. Use the following command to install the `mssql-server-polybase` on Ubuntu.

   ```console
   sudo apt-get install mssql-server-polybase
   ```

1. When prompted, restart the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance.

   ```console
   sudo systemctl restart mssql-server
   ```

> [!NOTE]  
> After installation, you must [enable the PolyBase feature](#enable).

If you need an offline installation, locate the PolyBase package download in the [Release notes for SQL Server 2019 on Linux](../../linux/sql-server-linux-release-notes-2019.md). Then use the same offline installation steps described in the article [Install SQL Server](../../linux/sql-server-linux-setup.md#offline).

### Install Hadoop on Ubuntu

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]

1. Use the following command to install the `mssql-server-polybase-hadoop`.

   ```console
   sudo apt-get install mssql-server-polybase-hadoop
   ```

   The PolyBase Hadoop package has dependencies on the following packages:

   - `mssql-server`
   - `mssql-server-polybase`
   - `mssql-server-extensibility`
   - `mssql-zulu-jre-11`

1. Installation prompts to restart `launchpadd`. Use the following command to do so.

   ```console
   sudo systemctl restart mssql-launchpadd
   ```

> [!NOTE]  
> After installation, you must [set the Hadoop connectivity level](../../relational-databases/polybase/polybase-configure-hadoop.md#configure-hadoop-connectivity). This step only applies to [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)].

## [SUSE Linux Enterprise Server (SLES)](#tab/sles)

### Install on SLES

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions

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

1. Use the following commands to install the `mssql-server-polybase` on SUSE Linux Enterprise Server.

   ```console
   sudo zypper install mssql-server-polybase
   ```

1. You're prompted to restart the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance. Use the following command to do so.

   ```console
   sudo systemctl restart mssql-server
   ```

> [!NOTE]  
> After installation, you must [enable the PolyBase feature](#enable).

If you need an offline installation, locate the PolyBase package download in the [Release notes for SQL Server 2019 on Linux](../../linux/sql-server-linux-release-notes-2019.md). Then use the same offline installation steps described in the article [Install SQL Server](../../linux/sql-server-linux-setup.md#offline).

---

## <a id="enable"></a> Enable PolyBase

After installation, PolyBase must be enabled to access its features. Connect to the installed [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance and use the following Transact-SQL command to enable.

```sql
exec sp_configure @configname = 'polybase enabled', @configvalue = 1;
RECONFIGURE WITH OVERRIDE;
```

## Update PolyBase

If you already have `mssql-server-polybase` installed, you can update to the latest version with the following commands:

## [Red Hat Enterprise Linux (RHEL)](#tab/rhel)

### RHEL with Hadoop

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]

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

You're prompted to restart the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance. Use the following command to do so.

```console
sudo systemctl restart mssql-server
```

## [Ubuntu](#tab/ubuntu)

### Ubuntu with Hadoop

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)].

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

You're prompted to restart the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance. Use the following command to do so.

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

You're prompted to restart the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance. Use the following command to do so.

```console
sudo systemctl restart mssql-server
```

---

> [!NOTE]  
> After installation, you must [enable the PolyBase feature](#enable).

## Related links

PolyBase on Linux can access the following data sources. Follow the provided links for more information on how to create an external table from these sources on PolyBase is enabled.

- [SQL Server and Azure SQL](polybase-configure-sql-server.md)
- [Hadoop](polybase-configure-hadoop.md)
- [Azure Blob Storage](polybase-configure-azure-blob-storage.md)
- [Oracle](polybase-configure-oracle.md)
- [Teradata](polybase-configure-teradata.md)
- [MongoDB and Azure Cosmos DB](polybase-configure-mongodb.md)

## Related content

- [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md)
