---
title: "Introducing data virtualization with PolyBase"
description: PolyBase enables your SQL Server instance to process Transact-SQL queries that read data from external data sources, such as Azure Blob Storage.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 01/17/2024
ms.service: sql
ms.subservice: polybase
ms.topic: "overview"
ms.custom:
  - intro-overview
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
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||>=aps-pdw-2016||=azure-sqldw-latest"
---

# Data virtualization with PolyBase in SQL Server

[!INCLUDE [appliesto-ss-xxxx-asdw-pdw-md](../../includes/appliesto-ss-xxxx-asdw-pdw-md.md)]

PolyBase is a data virtualization feature for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. 

## What is PolyBase?

PolyBase enables your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance to query data with T-SQL directly from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], Oracle, Teradata, MongoDB, Hadoop clusters, Cosmos DB, and S3-compatible object storage without separately installing client connection software. You can also use the generic ODBC connector to connect to additional providers using third-party ODBC drivers. PolyBase allows T-SQL queries to join the data from external sources to relational tables in an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].  

A key use case for data virtualization with the PolyBase feature is to allow the data to stay in its original location and format. You can virtualize the external data through the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance, so that it can be queried in place like any other table in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This process minimizes the need for ETL processes for data movement. This data virtualization scenario is possible with the use of PolyBase connectors.

### Supported SQL products and services

PolyBase provides these same functionalities for the following SQL products from Microsoft:

- [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions (Windows)
- [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions (Windows and Linux)
- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [pdw](../../includes/sspdw-md.md)]
- [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] (for dedicated SQL pools)
    - Data virtualization in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] is available in two modes, PolyBase and native. For more information, see [Use external tables with Synapse SQL](/azure/synapse-analytics/sql/develop-tables-external-tables).

> [!NOTE]
> Data virtualization is also available for **Azure SQL Managed Instance**, scoped to querying external data stored in files in Azure Data Lake Storage (ADLS) Gen2 and Azure Blob Storage. Visit [Data virtualization with Azure SQL Managed Instance](/azure/azure-sql/managed-instance/data-virtualization-overview) to learn more.

### SQL Server 2022 PolyBase enhancements

| New to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] | Details |
| :-- | :-- |
| S3-compatible object storage | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] adds new connector, S3-compatible object storage, using the S3 REST API. You can use both [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md) and [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md) to query data files in S3-compatible object storage. |
| Some connectors separate from PolyBase services | The S3-compatible object storage connector, as well as ADSL Gen2, and Azure Blob Storage, are no longer dependent of PolyBase services. PolyBase services must still run to support connectivity with Oracle, Teradata, MongoDB, and Generic ODBC. The PolyBase feature must still be installed on your SQL Server instance. |
| Parquet file format | PolyBase is now capable of querying data from Parquet files stored on S3-compatible object storage. For more information, see to [Virtualize parquet file in a S3-compatible object storage with PolyBase](polybase-virtualize-parquet-file.md). |
| Delta table format | PolyBase is now capable of querying (read-only) data from Delta Table format stored on S3-compatible object storage, Azure Storage Account V2, and Azure Data Lake Storage Gen2. For more information, see to [Virtualize Delta Table format](virtualize-delta.md)|
| Create External Table as Select (CETAS) | PolyBase can now use CETAS to create an external table and then export, in parallel, the result of a [!INCLUDE [tsql](../../includes/tsql-md.md)] SELECT statement to Azure Data Lake Storage Gen2, Azure Storage Account V2, and S3-compatible object storage. For more information, see [CREATE EXTERNAL TABLE AS SELECT (Transact-SQL)](../../t-sql/statements/create-external-table-as-select-transact-sql.md). |

For more new features of [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], see [What's new in SQL Server 2022?](../../sql-server/what-s-new-in-sql-server-2022.md)

> [!TIP]
> For a tutorial of PolyBase features and capabilities in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], see [Get started with PolyBase in SQL Server 2022](polybase-get-started.md).

### PolyBase connectors

 The PolyBase feature provides connectivity to the following external data sources:

| External data sources | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] 2016-2019 with PolyBase | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] with PolyBase | APS PDW | [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] |
|:---------------------------|:-------------|:--|:------------|:---------------|
| Oracle, MongoDB, Teradata                                 | Read               | Read               | **No**     | **No**     |  
| Generic ODBC                                              | Read (Windows Only)| Read (Windows Only)| **No**     | **No**     |  
| Azure Storage                                             | Read/Write         | Read/Write         | Read/Write | Read/Write |
| Hadoop                                                    | Read/Write         | No                 | Read/Write | **No**     |  
| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] | Read               | Read               | **No**     | **No**     |  
| S3-compatible object storage                              | **No**             | Read/Write         | **No**     | **No**     |

- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] does not support Hadoop.
- [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] introduced PolyBase with support for connections to Hadoop and Azure Blob Storage.
- [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] introduced additional connectors, including [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], Oracle, Teradata, and MongoDB.
- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] introduced the S3-compatible storage connector.
- [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] Cumulative update 19 introduced support for Oracle TNS.
- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Cumulative update 2 introduced support for Oracle TNS.

 Examples of external connectors include:

- [[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]](polybase-configure-sql-server.md)
- [Oracle](polybase-configure-oracle.md)
- [Teradata](polybase-configure-teradata.md)
- [MongoDB](polybase-configure-mongodb.md)
- [Hadoop](polybase-configure-hadoop.md)*
- [S3-compatible object storage](polybase-configure-s3-compatible.md)
- [CSV in Azure Blob Storage](virtualize-csv.md)

\* PolyBase supports two Hadoop providers, Hortonworks Data Platform (HDP) and Cloudera Distributed Hadoop (CDH), through SQL Server 2019. [!INCLUDE [polybase-java-connector-banner-retirement](../../includes/polybase-java-connector-banner-retirement.md)]

 To use PolyBase in an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]:

1. [Install PolyBase on Windows](polybase-installation.md) or [Install PolyBase on Linux](polybase-linux-setup.md).
1. Starting with [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], [enable PolyBase in sp_configure](polybase-installation.md#enable), if necessary.
1. Create an [external data source](../../t-sql/statements/create-external-data-source-transact-sql.md).
1. Create an [external table](../../t-sql/statements/create-external-table-transact-sql.md).

### Azure integration

With the underlying help of PolyBase, T-SQL queries can also import and export data from Azure Blob Storage. Further, PolyBase enables [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] to import and export data from Azure Data Lake Store, and from Azure Blob Storage.

## Why use PolyBase?

PolyBase allows you to join data from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance with external data. Prior to PolyBase to join data to external data sources you could either:

- Transfer half your data so that all the data was in one location.
- Query both sources of data, then write custom query logic to join and integrate the data at the client level.

PolyBase allows you to simply use Transact-SQL to join the data.

PolyBase does not require you to install additional software to your Hadoop environment. You query external data by using the same T-SQL syntax used to query a database table. The support actions implemented by PolyBase all happen transparently. The query author does not need any knowledge about the external source.

### PolyBase uses

PolyBase enables the following scenarios in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]:

- **Query data stored in Azure Blob Storage.** Azure Blob Storage is a convenient place to store data for use by Azure services. PolyBase makes it easy to access the data by using T-SQL.

- **Query data stored in Hadoop from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance or PDW.** Users are storing data in cost-effective distributed and scalable systems, such as Hadoop. PolyBase makes it easy to query the data by using T-SQL.

- **Import data from Hadoop, Azure Blob Storage, or Azure Data Lake Store.** Leverage the speed of Microsoft SQL's columnstore technology and analysis capabilities by importing data from Hadoop, Azure Blob Storage, or Azure Data Lake Store into relational tables. There is no need for a separate ETL or import tool.

- **Export data to Hadoop, Azure Blob Storage, or Azure Data Lake Store.** Archive data to Hadoop, Azure Blob Storage, or Azure Data Lake Store to achieve cost-effective storage and keep it online for easy access.

- **Integrate with BI tools.** Use PolyBase with Microsoft's business intelligence and analysis stack, or use any third-party tools that are compatible with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## Performance

There's no hard limit to the number of files or the amount of data that can be queried. Query performance depends on the amount of data, data format, the way data is organized, and complexity of queries and joins.

For more information on performance guidance and recommendations for PolyBase, see [Performance considerations in PolyBase for SQL Server](polybase-performance.md).

## <a id="upgrading-to-sql-server-2022"></a> Upgrade to SQL Server 2022

Starting in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Hortonworks Data Platform (HDP) and Cloudera Distributed Hadoop (CDH) are no longer supported. Due to these changes, it is required to manually drop PolyBase external data sources created on previous versions of SQL Server that use `TYPE = HADOOP` or Azure Storage before migrating to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. Dropping external data sources also requires dropping the associated database objects, such as database scoped credentials and external tables.

Azure Storage connectors must be changed based on the reference table below:

| External Data Source | From | To |
|:--|:--|:--|
| Azure Blob Storage | wasb[s] | abs |
| ADLS Gen 2 | abfs[s] | adls |

## Get started

Before using PolyBase, you must [install PolyBase on Windows](polybase-installation.md) or [install PolyBase on Linux](polybase-linux-setup.md), and [enable PolyBase in sp_configure](polybase-installation.md#enable) if necessary. 

For a tutorial of PolyBase features and capabilities, see [Get started with PolyBase in SQL Server 2022](polybase-get-started.md).

For more tutorials on various external data sources, review:

- [Hadoop](polybase-configure-hadoop.md)
- [Azure Blob Storage](polybase-configure-azure-blob-storage.md)
- [SQL Server](polybase-configure-sql-server.md)
- [Oracle](polybase-configure-oracle.md)
- [Teradata](polybase-configure-teradata.md)
- [MongoDB](polybase-configure-mongodb.md)
- [ODBC Generic Types](polybase-configure-odbc-generic.md)
- [S3-compatible object storage](polybase-configure-s3-compatible.md)
- [CSV](virtualize-csv.md)
- [Delta table](virtualize-delta.md)

### Data virtualization on other platforms

Data virtualization features are also available on other platforms:

- [Use external tables with Synapse SQL](/azure/synapse-analytics/sql/develop-tables-external-tables)
- [Data virtualization with Azure SQL Managed Instance](/azure/azure-sql/managed-instance/data-virtualization-overview)

## Related content

- [Get started with PolyBase in SQL Server 2022](polybase-get-started.md)
- [OPENROWSET (Transact-SQL)](../../t-sql/functions/openrowset-transact-sql.md)
- [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md)
- [Performance considerations in PolyBase for SQL Server](polybase-performance.md)
- [Frequently asked questions in PolyBase](polybase-faq.yml)
- [Monitor and troubleshoot PolyBase](polybase-troubleshooting.md)
- [PolyBase Transact-SQL reference](polybase-t-sql-objects.md)
