---
title: "What is PolyBase? | Microsoft Docs"
description: PolyBase enables your SQL Server instance to process Transact-SQL queries that read data from external data sources such as Hadoop and Azure Blob Storage.
ms.date: 12/14/2019
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
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||>=aps-pdw-2016||=azure-sqldw-latest"
---

# What is PolyBase?

[!INCLUDE[appliesto-ss-xxxx-asdw-pdw-md](../../includes/appliesto-ss-xxxx-asdw-pdw-md.md)]

PolyBase enables your SQL Server instance to process Transact-SQL queries that read data from external data sources. The same query can also access relational tables in your instance of SQL Server. PolyBase enables the same query to also join the data from external sources and SQL Server.

To use PolyBase, in an instance of SQL Server:

1. [Install PolyBase on Windows](polybase-installation.md)
1. Create an [external data source](../../t-sql/statements/create-external-data-source-transact-sql.md)
1. Create an [external table](../../t-sql/statements/create-external-table-transact-sql.md)

Together, these provide the connection to the external data source.

SQL Server 2016 introduces PolyBase with support for connections to Hadoop and Azure Blob Storage.

SQL Server 2019 introduces additional connectors, including SQL Server, Oracle, Teradata, and MongoDB.

![PolyBase logical](../../relational-databases/polybase/media/polybase-logical.png "PolyBase logical")

PolyBase pushes some computations to the external source to optimize the overall query. PolyBase external access is not limited to Hadoop. Other unstructured non-relational tables are also supported, such as delimited text files.

Examples of external connectors include:

- [SQL Server](polybase-configure-sql-server.md)
- [Oracle](polybase-configure-oracle.md)
- [Teradata](polybase-configure-teradata.md)
- [MongoDB](polybase-configure-mongodb.md)

### Supported SQL products and services

PolyBase provides these same functionalities for the following SQL products from Microsoft:

- SQL Server 2016 and later versions (Windows only)
- Analytics Platform System (formerly Parallel Data Warehouse)
- Azure Synapse Analytics

### Azure integration

With the underlying help of PolyBase, T-SQL queries can also import and export data from Azure Blob Storage. Further, PolyBase enables Azure Synapse Analytics to import and export data from Azure Data Lake Store, and from Azure Blob Storage.

## Why use PolyBase?

PolyBase allows you to join data from a SQL Server instance with external data. Prior to PolyBase to join data to external data sources you could either:

- Transfer half your data so that all the data was in one location.
- Query both sources of data, then write custom query logic to join and integrate the data at the client level.

PolyBase allows you to simply use Transact-SQL to join the data.

PolyBase does not require you to install additional software to your Hadoop environment. You query external data by using the same T-SQL syntax used to query a database table. The support actions implemented by PolyBase all happen transparently. The query author does not need any knowledge about the external source.

### PolyBase uses

PolyBase enables the following scenarios in SQL Server:

- **Query data stored in Hadoop from a SQL Server instance or PDW.** Users are storing data in cost-effective distributed and scalable systems, such as Hadoop. PolyBase makes it easy to query the data by using T-SQL.

- **Query data stored in Azure Blob Storage.** Azure blob storage is a convenient place to store data for use by Azure services.  PolyBase makes it easy to access the data by using T-SQL.

- **Import data from Hadoop, Azure Blob Storage, or Azure Data Lake Store.** Leverage the speed of Microsoft SQL's columnstore technology and analysis capabilities by importing data from Hadoop, Azure Blob Storage, or Azure Data Lake Store into relational tables. There is no need for a separate  ETL or import tool.

- **Export data to Hadoop, Azure Blob Storage, or Azure Data Lake Store.** Archive data to Hadoop, Azure Blob Storage, or Azure Data Lake Store to achieve cost-effective storage and keep it online for easy access.

- **Integrate with BI tools.** Use PolyBase with Microsoft's business intelligence and analysis stack, or use any third party tools that are compatible with SQL Server.

## Performance

- **Push computation to Hadoop.** The query optimizer makes a cost-based decision to push computation to Hadoop, if that will improve query performance.  The query optimizer uses statistics on external tables to make the cost-based decision. Pushing computation creates MapReduce jobs and leverages Hadoop's distributed computational resources.

- **Scale compute resources.** To improve query performance, you can use SQL Server [PolyBase scale-out groups](../../relational-databases/polybase/polybase-scale-out-groups.md). This enables parallel data transfer between SQL Server instances and Hadoop nodes, and it adds compute resources for operating on the external data.

## Next steps

Before using PolyBase, you must [install the PolyBase feature](polybase-installation.md). Then see the following configuration guides depending on your data source:

- [Hadoop](polybase-configure-hadoop.md)
- [Azure Blob Storage](polybase-configure-azure-blob-storage.md)
- [SQL Server](polybase-configure-sql-server.md)
- [Oracle](polybase-configure-oracle.md)
- [Teradata](polybase-configure-teradata.md)
- [MongoDB](polybase-configure-mongodb.md)
- [ODBC Generic Types](polybase-configure-odbc-generic.md)