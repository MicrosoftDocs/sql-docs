---
title: Install PolyBase on Linux | Microsoft Docs
description: This article describes how to install SQL Server Full-Text Search on Linux.
author: aboke 
ms.author: aboke 
manager: craigg
ms.date: 4/12/2019
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
ms.assetid: bb42076f-e823-4cee-9281-cd3f83ae42f5
---
# Install PolyBase on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

The following steps install [PolyBase](../../relational-databases/search/full-text-search.md) (**mssql-server-polybase**) on Linux. PolyBase enables you to run external queries against remote data sources. 

>[!NOTE]
> Before installing Polybase, first [install SQL Server](../../linux/sql-server-linux-setup.md#platforms). This configures the keys and repositories that you use when installing the **mssql-server-polybase** package.

> Scale-out for PolyBase on Linux is currently unavailable.


Install PolyBase for your operating system:

- [Red Hat Enterprise Linux](#RHEL)
- [Ubuntu](#ubuntu)
- [SUSE Linux Enterprise Server](#SLES)



## <a name="RHEL">Install on RHEL</a>

Use the following command to install the **mssql-server-polybase** on Red Hat Enterprise Linux. 

```bash
sudo yum install -y mssql-server-polybase
```

You will be prompted to restart the SQL Server instance. Use the following command to do so.

```bash
sudo systemctl restart mssql-server
```

>[!NOTE]
>After installation, you must [enable the PolyBase feature](#enable).

If you need an offline installation, locate the PolyBase package download in the [Release notes](../../linux/sql-server-linux-release-notes.md). Then use the same offline installation steps described in the article [Install SQL Server](../../linux/sql-server-linux-setup.md#offline).

## <a name="ubuntu">Install on Ubuntu</a>

Use the following command to install the **mssql-server-polybase** on Ubuntu. 

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

## <a name="SLES">Install on SLES</a>

Use the following commands to install the **mssql-server-polybase** on SUSE Linux Enterprise Server. 

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


## <a name="enable">Enable PolyBase</a> 

After installation, PolyBase must be enabled to access its features. Connect to the installed SQL Server instance and use the following Transact-SQL command to enable.

```sql
exec sp_configure @configname = 'polybase enabled', @configvalue = 1;
RECONFIGURE [WITH OVERRIDE];
```

## Update PolyBase

If you already have **mssql-server-polybase** installed, you can update to the latest version with the following commands:

### RHEL

```bash
sudo yum remove -y mssql-server-polybase
sudo yum check-update
sudo yum install -y mssql-server-polybase
```

You will be prompted to restart the SQL Server instance. Use the following command to do so.

```
sudo systemctl restart mssql-server
```

### Ubuntu

```bash
sudo apt-get remove mssql-server-polybase
sudo apt-get update 
sudo apt-get install mssql-server-polybase
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

### Supported external data sources on Linux

PolyBase on Linux can access the following data sources. Follow the provided links for more information on how to create an external table from these sources on PolyBase is enabled. 

- [SQL Server ( & SQL DB, Azure SQL DW)](../../relational-databases/polybase/polybase-configure-sql-server.md)
- [Oracle](../../relational-databases/polybase/polybase-configure-oracle.md)
- [Teradata](../../relational-databases/polybase/polybase-configure-teradata.md)
- [MongoDB (& Cosmos DB)](../../relational-databases/polybase/polybase-configure-mongodb.md)

For more information on how this is used, see Transact-SQL reference article for [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md).