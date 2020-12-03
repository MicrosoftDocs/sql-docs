---
title: Install PolyBase on Linux
titlesuffix: SQL Server
description: Learn how to install SQL Server PolyBase on Linux. PolyBase enables you to run external queries against remote data sources.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: dakryze
ms.date: 8/18/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: linux
monikerRange: ">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"
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

Use the following command to install the `mssql-server-polybase` on Red Hat Enterprise Linux. 

```bash
sudo yum install -y mssql-server-polybase
```

You will be prompted to restart the SQL Server instance. Use the following command to do so.

```bash
sudo systemctl restart mssql-server
```

>[!NOTE]
>After installation, you must [enable the PolyBase feature](#enable).

Use the following command to install the `mssql-server-polybase-hadoop`. 

```bash
sudo yum install -y mssql-server-polybase-hadoop
```

The PolyBase Hadoop package has dependencies on the following packages:
- `mssql-server`
- `mssql-server-polybase`
- `mssql-server-extensibility`
- `mssql-zulu-jre-11`. 

Installation prompts to restart `launchpadd`. Use the following command to do so.

```bash
sudo systemctl restart mssql-launchpadd
```

>[!NOTE]
>After installation, you must [set the Hadoop connectivity level](../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md#c-set-hadoop-connectivity).

If you need an offline installation, locate the PolyBase package download in the [Release notes](../../linux/sql-server-linux-release-notes.md). Then use the same offline installation steps described in the article [Install SQL Server](../../linux/sql-server-linux-setup.md#offline).

## <a name="ubuntu"></a>Install on Ubuntu

Use the following command to install the `mssql-server-polybase` on Ubuntu. 

```bash
sudo apt-get install mssql-server-polybase
```

You will be prompted to restart the SQL Server instance. Use the following command to do so.

```bash
sudo systemctl restart mssql-server
```

>[!NOTE]
>After installation, you must [enable the PolyBase feature](#enable).

If you need an offline installation, locate the PolyBase package download in the [Release notes](../../linux/sql-server-linux-release-notes.md). Then use the same offline installation steps described in the article [Install SQL Server](../../linux/sql-server-linux-setup.md#offline).

Use the following command to install the `mssql-server-polybase-hadoop`. 

```bash
sudo apt-get install mssql-server-polybase-hadoop
```

The PolyBase Hadoop package has dependencies on the following packages:
- `mssql-server`
- `mssql-server-polybase`
- `mssql-server-extensibility`
- `mssql-zulu-jre-11`. 

Installation prompts to restart `launchpadd`. Use the following command to do so.

```bash
sudo systemctl restart mssql-launchpadd
```

>[!NOTE]
>After installation, you must [set the Hadoop connectivity level](../../relational-databases/polybase/polybase-configure-hadoop.md#configure-hadoop-connectivity).

## <a name="SLES"></a>Install on SLES

Use the following commands to install the `mssql-server-polybase` on SUSE Linux Enterprise Server. 

```bash
sudo zypper install mssql-server-polybase
```

You will be prompted to restart the SQL Server instance. Use the following command to do so.

```bash
sudo systemctl restart mssql-server
```

>[!NOTE]
>After installation, you must [enable the PolyBase feature](#enable).

If you need an offline installation, locate the PolyBase package download in the [Release notes](../../linux/sql-server-linux-release-notes.md). Then use the same offline installation steps described in the article [Install SQL Server](../../linux/sql-server-linux-setup.md#offline).


## <a name="enable"></a> Enable PolyBase

After installation, PolyBase must be enabled to access its features. Connect to the installed SQL Server instance and use the following Transact-SQL command to enable.

```sql
exec sp_configure @configname = 'polybase enabled', @configvalue = 1;
RECONFIGURE WITH OVERRIDE;
```

## Update PolyBase

If you already have `mssql-server-polybase` installed, you can update to the latest version with the following commands:

### RHEL

```bash
sudo yum remove -y mssql-server-polybase-hadoop
sudo yum remove -y mssql-server-polybase
sudo yum check-update
sudo yum install -y mssql-server-polybase
sudo yum install -y mssql-server-polybase-hadoop
```

You will be prompted to restart the SQL Server instance. Use the following command to do so.

```
sudo systemctl restart mssql-server
```

### Ubuntu

```bash
sudo apt-get remove mssql-server-polybase-hadoop
sudo apt-get remove mssql-server-polybase
sudo apt-get update 
sudo apt-get install mssql-server-polybase
sudo apt-get remove mssql-server-polybase-hadoop
```

You will be prompted to restart the SQL Server instance. Use the following command to do so.

```
sudo systemctl restart mssql-server
```

### SLES

```bash
sudo zypper remove mssql-server-polybase
sudo zypper refresh
sudo zypper install mssql-server-polybase
```

You will be prompted to restart the SQL Server instance. Use the following command to do so.

```
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
