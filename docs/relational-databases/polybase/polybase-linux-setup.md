---
title: Install PolyBase on Linux
titleSuffix: SQL Server
description: Learn how to install SQL Server PolyBase on Linux. PolyBase enables you to run external queries against remote data sources.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: dakryze
ms.date: 05/12/2021
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
monikerRange: ">= sql-server-linux-ver15 || >= sql-server-ver15"
ms.custom:
  - intro-installation
---
# Install PolyBase on Linux

[!INCLUDE [sqlserver2019-linux](../../includes/applies-to-version/sqlserver2019-linux.md)]

The following steps install [PolyBase](../../relational-databases/polybase/polybase-guide.md) (`mssql-server-polybase` and `mssql-server-polybase-hadoop`) on Linux. PolyBase enables you to run external queries against remote data sources.

>[!NOTE]
> Before installing PolyBase, first [install SQL Server 2019](../../linux/sql-server-linux-setup.md#platforms). This configures the keys and repositories that you use when installing the `mssql-server-polybase` and `mssql-server-polybase-hadoop` package.

>[!NOTE]
>
> - PolyBase is not supported on SQL Server 2017 for Linux.
> - Scale-out for PolyBase on Linux is currently unavailable.

Install PolyBase for your operating system:

- [Red Hat Enterprise Linux](#RHEL)
- [Ubuntu](#ubuntu)
- [SUSE Linux Enterprise Server](#SLES)

## <a name="RHEL"></a>Install on RHEL

1. Download the Microsoft Red Hat repository configuration file.

   For RHEL7:

   ```console
   sudo curl -o /etc/yum.repos.d/msprod.repo https://packages.microsoft.com/config/rhel/7/prod.repo
   ```

   For RHEL8:

   ```console
   sudo curl -o /etc/yum.repos.d/msprod.repo https://packages.microsoft.com/config/rhel/8/prod.repo
   ```

1. Use the following command to install the `mssql-server-polybase` on Red Hat Enterprise Linux. 

   ```console
   sudo yum install -y mssql-server-polybase
   ```

1. You will be prompted to restart the SQL Server instance. Use the following command to do so.

   ```console
   sudo systemctl restart mssql-server
   ```

   >[!NOTE]
   >After installation, you must [enable the PolyBase feature](#enable).

   Use the following command to install the `mssql-server-polybase-hadoop`. 

1. ```console
   sudo yum install -y mssql-server-polybase-hadoop
   ```

   The PolyBase Hadoop package has dependencies on the following packages:
   - `mssql-server`
   - `mssql-server-polybase`
   - `mssql-server-extensibility`
   - `mssql-zulu-jre-11`. 

1. Installation prompts to restart `launchpadd`. Use the following command to do so.

   ```console
   sudo systemctl restart mssql-launchpadd
   ```

>[!NOTE]
>After installation, you must [set the Hadoop connectivity level](../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md#c-set-hadoop-connectivity).

If you need an offline installation, locate the PolyBase package download in the [Release notes](../../linux/sql-server-linux-release-notes-2017.md). Then use the same offline installation steps described in the article [Install SQL Server](../../linux/sql-server-linux-setup.md#offline).

## <a name="ubuntu"></a>Install on Ubuntu

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

2. Use the following command to install the `mssql-server-polybase` on Ubuntu. 

   ```console
   sudo apt-get install mssql-server-polybase
   ```

3. When prompted, restart the SQL Server instance.

   ```console
   sudo systemctl restart mssql-server
   ```

   >[!NOTE]
   >After installation, you must [enable the PolyBase feature](#enable).

   If you need an offline installation, locate the PolyBase package download in the [Release notes](../../linux/sql-server-linux-release-notes-2017.md). Then use the same offline installation steps described in the article [Install SQL Server](../../linux/sql-server-linux-setup.md#offline).

4. Use the following command to install the `mssql-server-polybase-hadoop`. 

   ```console
   sudo apt-get install mssql-server-polybase-hadoop
   ```

   The PolyBase Hadoop package has dependencies on the following packages:

   - `mssql-server`
   - `mssql-server-polybase`
   - `mssql-server-extensibility`
   - `mssql-zulu-jre-11`

5. Installation prompts to restart `launchpadd`. Use the following command to do so.

   ```console
   sudo systemctl restart mssql-launchpadd
   ```

>[!NOTE]
>After installation, you must [set the Hadoop connectivity level](../../relational-databases/polybase/polybase-configure-hadoop.md#configure-hadoop-connectivity).

## <a name="SLES"></a>Install on SLES

1. Add the Microsoft SQL Server repository to Zypper.

   ```console
   sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/12/prod.repo 
   sudo zypper --gpg-auto-import-keys refresh
   ```

1. Use the following commands to install the `mssql-server-polybase` on SUSE Linux Enterprise Server. 

   ```console
   sudo zypper install mssql-server-polybase
   ```

   You will be prompted to restart the SQL Server instance. Use the following command to do so.

1. ```console
   sudo systemctl restart mssql-server
   ```

>[!NOTE]
>After installation, you must [enable the PolyBase feature](#enable).

If you need an offline installation, locate the PolyBase package download in the [Release notes](../../linux/sql-server-linux-release-notes-2017.md). Then use the same offline installation steps described in the article [Install SQL Server](../../linux/sql-server-linux-setup.md#offline).

## <a name="enable"></a> Enable PolyBase

After installation, PolyBase must be enabled to access its features. Connect to the installed SQL Server instance and use the following Transact-SQL command to enable.

```sql
exec sp_configure @configname = 'polybase enabled', @configvalue = 1;
RECONFIGURE WITH OVERRIDE;
```

## Update PolyBase

If you already have `mssql-server-polybase` installed, you can update to the latest version with the following commands:

### RHEL

```console
sudo yum remove -y mssql-server-polybase-hadoop
sudo yum remove -y mssql-server-polybase
sudo yum check-update
sudo yum install -y mssql-server-polybase
sudo yum install -y mssql-server-polybase-hadoop
```

You will be prompted to restart the SQL Server instance. Use the following command to do so.

```console
sudo systemctl restart mssql-server
```

### Ubuntu

```console
sudo apt-get remove mssql-server-polybase-hadoop
sudo apt-get remove mssql-server-polybase
sudo apt-get update 
sudo apt-get install mssql-server-polybase
sudo apt-get install mssql-server-polybase-hadoop
```

You will be prompted to restart the SQL Server instance. Use the following command to do so.

```console
sudo systemctl restart mssql-server
```

### SLES

```console
sudo zypper remove mssql-server-polybase
sudo zypper refresh
sudo zypper install mssql-server-polybase
```

You will be prompted to restart the SQL Server instance. Use the following command to do so.

```console
sudo systemctl restart mssql-server
```

>[!NOTE]
>After installation, you must [enable the PolyBase feature](#enable).

## Next steps

PolyBase on Linux can access the following data sources. Follow the provided links for more information on how to create an external table from these sources on PolyBase is enabled. 

- [SQL Server, SQL Database, Azure Synapse Analytics)](../../relational-databases/polybase/polybase-configure-sql-server.md)
- [Hadoop](../../relational-databases/polybase/polybase-configure-hadoop.md)
- [Azure Blob Storage](../../relational-databases/polybase/polybase-configure-azure-blob-storage.md)
- [Oracle](../../relational-databases/polybase/polybase-configure-oracle.md)
- [Teradata](../../relational-databases/polybase/polybase-configure-teradata.md)
- [MongoDB (& Cosmos DB)](../../relational-databases/polybase/polybase-configure-mongodb.md)

For more information on how this is used, see Transact-SQL reference article for [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md).
