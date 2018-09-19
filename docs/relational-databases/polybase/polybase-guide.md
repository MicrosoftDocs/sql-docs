---
title: "PolyBase Guide | Microsoft Docs"
ms.date: "05/31/2017"
ms.prod: sql
ms.reviewer: ""
ms.suite: "sql"
ms.custom: ""
ms.technology: polybase
ms.tgt_pltfrm: ""
ms.topic: quickstart
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
author: rothja
ms.author: jroth
manager: craigg
---
# PolyBase Guide

[!INCLUDE[appliesto-ss-xxxx-asdw-pdw-md](../../includes/appliesto-ss-xxxx-asdw-pdw-md.md)]

PolyBase enables your SQL Server 2016 instance to process Transact-SQL queries that read data from Hadoop. The same query can also access relational tables in your SQL Server. PolyBase enables the same query to also join the data from Hadoop and SQL Server. In SQL Server, an [external table](../../t-sql/statements/create-external-table-transact-sql.md) or [external data source](../../t-sql/statements/create-external-data-source-transact-sql.md) provides the connection to Hadoop.

PolyBase provides these same functionalities for the following SQL products from Microsoft:

- SQL Server 2016, and later verions
- Analytics Platform System (formerly Parallel Data Warehouse)
- Azure SQL Data Warehouse

PolyBase pushes some computations to the Hadoop node to optimize the overall query. However, PolyBase external access is not limited to Hadoop. Other unstructured non-relational tables are also supported, such as delimited text files.

#### Data import and export

With the underlying help of PolyBase, T-SQL queries can also import and export data from Azure Blob Storage. Further, PolyBase enables Azure SQL Data Warehouse to import and export data from Azure Data Lake Store, and from Azure Blob Storage.

To use PolyBase, see [Get started with PolyBase](../../relational-databases/polybase/get-started-with-polybase.md).
  
![PolyBase logical](../../relational-databases/polybase/media/polybase-logical.png "PolyBase logical")

## Why use PolyBase?

In the past it was more difficult to join your SQL Server data with external data. You had the two following unpleasant options:

- Transfer half your data so that all your data was in one format or the other.
- Query both sources of data, then write custom query logic to join and integrate the data at the client level.

PolyBase avoids those unpleasant options by using T-SQL to join the data

To keep things simple, PolyBase does not require you to install additional software to your Hadoop environment. You query external data by using the same T-SQL syntax used to query a database table. The support actions implemented by PolyBase all happen transparently. The query author does not need any knowledge about Hadoop.

PolyBase can:

- **Query data stored in Hadoop from SQL Server or PDW.** Users are storing data in cost-effective distributed and scalable systems, such as Hadoop. PolyBase makes it easy to query the data by using T-SQL.

- **Query data stored in Azure Blob Storage.** Azure blob storage is a convenient place to store data for use by Azure services.  PolyBase makes it easy to access the data by using T-SQL.

- **Import data from Hadoop, Azure Blob Storage, or Azure Data Lake Store.** Leverage the speed of Microsoft SQL's columnstore technology and analysis capabilities by importing data from Hadoop, Azure Blob Storage, or Azure Data Lake Store into relational tables. There is no need for a separate  ETL or import tool.

- **Export data to Hadoop, Azure Blob Storage, or Azure Data Lake Store.** Archive data to Hadoop, Azure Blob Storage, or Azure Data Lake Store to achieve cost-effective storage and keep it online for easy access.

- **Integrate with BI tools.** Use PolyBase with Microsoft's business intelligence and analysis stack, or use any third party tools that are compatible with SQL Server.

## Performance

- **Push computation to Hadoop.** The query optimizer makes a cost-based decision to push computation to Hadoop when doing so will improve query performance.  It uses statistics on external tables to make the cost-based decision. Pushing computation creates MapReduce jobs and leverages Hadoop's distributed computational resources.

- **Scale compute resources.** To improve query performance, you can use SQL Server [PolyBase scale-out groups](../../relational-databases/polybase/polybase-scale-out-groups.md). This enables parallel data transfer between SQL Server instances and Hadoop nodes, and it adds compute resources for operating on the external data.

## PolyBase Guide Topics

This guide includes topics to help you use PolyBase efficiently and effectively.

|||
|-|-|
|**Topic**|**Description**|
|[Get started with PolyBase](../../relational-databases/polybase/get-started-with-polybase.md)|Basic steps to install and configure PolyBase. This shows how to create external objects that point to data in Hadoop or Azure blob storage, and gives query examples.|
|[PolyBase Versioned Feature Summary](../../relational-databases/polybase/polybase-versioned-feature-summary.md)|Describes which  PolyBase features are supported on SQL Server, SQL Database, and SQL Data Warehouse.|
|[PolyBase scale-out groups](../../relational-databases/polybase/polybase-scale-out-groups.md)|Scale out parallelism between SQL Server and Hadoop by using SQL Server scale-out groups.|
|[PolyBase installation](../../relational-databases/polybase/polybase-installation.md)|Reference and steps for installing PolyBase with the installation wizard or with a command-line tool.|
|[PolyBase configuration](../../relational-databases/polybase/polybase-configuration.md)|Configure SQL Server settings for PolyBase.  For example, configure computation pushdown and kerberos security.|
|[PolyBase T-SQL objects](../../relational-databases/polybase/polybase-t-sql-objects.md)|Create the T-SQL objects that PolyBase uses to define and access external data.|
|[PolyBase Queries](../../relational-databases/polybase/polybase-queries.md)|Use T-SQL statements to query, import, or export external data.|
|[PolyBase troubleshooting](../../relational-databases/polybase/polybase-troubleshooting.md)|Techniques to manage PolyBase queries. Use dynamic management views (DMVs) to monitor PolyBase queries, and learn to read a PolyBase query plan to find performance bottlenecks.|
| &nbsp; | &nbsp; |
  
