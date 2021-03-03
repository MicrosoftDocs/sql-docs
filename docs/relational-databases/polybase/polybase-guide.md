---
title: "Introducing Data Virtualization with PolyBase"
description: PolyBase enables your SQL Server instance to process Transact-SQL queries that read data from external data sources such as Hadoop and Azure Blob Storage.
ms.date: 03/03/2021
ms.prod: sql
ms.technology: polybase
ms.topic: "overview"
f1_keywords: 
  - "PolyBase"
  - "PolyBase, guide"
helpviewer_keywords: 
  - "PolyBase"
  - "PolyBase, overview"
  - "Hadoop import"
  - "Hadoop export"
  - "Hadoop export, PolyBase overview"
  - "Hadoop import, PolyBase overview"
ms.custom: contperf-fy21q2
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
monikerRange: ">=sql-server-2016||>=sql-server-linux-2019||>=aps-pdw-2016||=azure-sqldw-latest"
---

# Introducing Data Virtualization with PolyBase

[!INCLUDE[appliesto-ss-xxxx-asdw-pdw-md](../../includes/appliesto-ss-xxxx-asdw-pdw-md.md)]

PolyBase is a data virtualization feature for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. 

## What is PolyBase?

PolyBase enables your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to query data directly from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Oracle, SAP HANA, MongoDB, Hadoop clusters, Cosmos DB using T-SQL, without separately installing client connection software. PolyBase allows one T-SQL query to join the data from external sources and other SQL Server instances to relational tables in an instance of SQL Server. 

A key use case for data virtualization with the PolyBase feature is to allow the data to stay in its original location and format. You can virtualize the external data through the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, so that it can be queried in place like any other table in SQL Server. This process minimizes the need for ETL processes for data movement. This data virtualization scenario is possible with the use of PolyBase connectors.

### PolyBase connectors

 The PolyBase feature provides the connection to the external data source.

* [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] introduced PolyBase with support for connections to Hadoop and Azure Blob Storage.
* [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] introduced additional connectors, including SQL Server, Oracle, Teradata, and MongoDB.
* Other unstructured non-relational tables are also supported with PolyBase, such as delimited text files. 

![PolyBase logical](../../relational-databases/polybase/media/polybase-logical.png "PolyBase logical")

 Examples of external connectors include:

- [SQL Server](polybase-configure-sql-server.md)
- [Oracle](polybase-configure-oracle.md)
- [Teradata](polybase-configure-teradata.md)
- [MongoDB](polybase-configure-mongodb.md)

 To use PolyBase in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:

1. [Install PolyBase on Windows](polybase-installation.md) or [Install PolyBase on Linux](polybase-linux-setup.md)
1. [Enable PolyBase in sp_configure](polybase-installation#enable), if necessary. 
1. Create an [external data source](../../t-sql/statements/create-external-data-source-transact-sql.md)
1. Create an [external table](../../t-sql/statements/create-external-table-transact-sql.md)

### Supported SQL products and services

PolyBase provides these same functionalities for the following SQL products from Microsoft:

- [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later versions (Windows only)
- [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later versions (Linux)
- Analytics Platform System (formerly Parallel Data Warehouse)
- Azure Synapse Analytics

### Azure integration

With the underlying help of PolyBase, T-SQL queries can also import and export data from Azure Blob Storage. Further, PolyBase enables Azure Synapse Analytics to import and export data from Azure Data Lake Store, and from Azure Blob Storage.

## Why use PolyBase?

PolyBase allows you to join data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance with external data. Prior to PolyBase to join data to external data sources you could either:

- Transfer half your data so that all the data was in one location.
- Query both sources of data, then write custom query logic to join and integrate the data at the client level.

PolyBase allows you to simply use Transact-SQL to join the data.

PolyBase does not require you to install additional software to your Hadoop environment. You query external data by using the same T-SQL syntax used to query a database table. The support actions implemented by PolyBase all happen transparently. The query author does not need any knowledge about the external source.

### PolyBase uses

PolyBase enables the following scenarios in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:

- **Query data stored in Hadoop from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance or PDW.** Users are storing data in cost-effective distributed and scalable systems, such as Hadoop. PolyBase makes it easy to query the data by using T-SQL.

- **Query data stored in Azure Blob Storage.** Azure blob storage is a convenient place to store data for use by Azure services. PolyBase makes it easy to access the data by using T-SQL.

- **Import data from Hadoop, Azure Blob Storage, or Azure Data Lake Store.** Leverage the speed of Microsoft SQL's columnstore technology and analysis capabilities by importing data from Hadoop, Azure Blob Storage, or Azure Data Lake Store into relational tables. There is no need for a separate ETL or import tool.

- **Export data to Hadoop, Azure Blob Storage, or Azure Data Lake Store.** Archive data to Hadoop, Azure Blob Storage, or Azure Data Lake Store to achieve cost-effective storage and keep it online for easy access.

- **Integrate with BI tools.** Use PolyBase with Microsoft's business intelligence and analysis stack, or use any third-party tools that are compatible with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## Performance

- **Push computation to Hadoop.** PolyBase pushes some computations to the external source to optimize the overall query. The query optimizer makes a cost-based decision to push computation to Hadoop, if that will improve query performance.  The query optimizer uses statistics on external tables to make the cost-based decision. Pushing computation creates MapReduce jobs and leverages Hadoop's distributed computational resources. For more information, see [Pushdown computations in PolyBase](polybase-pushdown-computation.md). 

- **Scale compute resources.** To improve query performance, you can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [PolyBase scale-out groups](../../relational-databases/polybase/polybase-scale-out-groups.md). This enables parallel data transfer between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances and Hadoop nodes, and it adds compute resources for operating on the external data.

## Next steps

Before using PolyBase, you must [install PolyBase on Windows](polybase-installation.md) or [install PolyBase on Linux](polybase-linux-setup.md), and [enable PolyBase in sp_configure](polybase-installation#enable) if necessary. Then see the following configuration guides depending on your data source:

- [Hadoop](polybase-configure-hadoop.md)
- [Azure Blob Storage](polybase-configure-azure-blob-storage.md)
- [SQL Server](polybase-configure-sql-server.md)
- [Oracle](polybase-configure-oracle.md)
- [Teradata](polybase-configure-teradata.md)
- [MongoDB](polybase-configure-mongodb.md)
- [ODBC Generic Types](polybase-configure-odbc-generic.md)
