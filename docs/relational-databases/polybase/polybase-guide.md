--
title: "PolyBase Guide | Microsoft Docs"
ms.date: "5/30/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine-polybase"
ms.tgt_pltfrm: ""
ms.topic: "get-started-article"
f1_keywords: 
  - "PolyBase"
  - "PolyBase, guide"
helpviewer_keywords: 
  - "PolyBase"
  - "PolyBase, overview"
  - "Hadoop import ×"
  - "Hadoop export"
  - "Hadoop export, PolyBase overview"
  - "Hadoop import, PolyBase overview"
ms.assetid: b42b7d48-328a-4046-abe9-f73fd83212dc
caps.latest.revision: 26
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# PolyBase Guide
[!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-pdw_md](../../includes/tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md)]

  PolyBase is a technology that accesses data outside of the database via the t-sql language.  In SQL Server 2016, it allows you to run queries on external data in Hadoop or to import/export data from Azure Blob Storage. Queries are optimized to push computation to Hadoop. In Azure SQL Data Warehouse, you can import/export data from Azure Blob Storage and Azure Data Lake Store.
  
  
 To use PolyBase, see [Get started with PolyBase](../../relational-databases/polybase/get-started-with-polybase.md).  
  
 ![PolyBase logical](../../relational-databases/polybase/media/polybase-logical.png "PolyBase logical")  
  
## Why use PolyBase?  
To make good decisions, you want to analyze both relational data and other data that is not structured into tables —notably Hadoop. This is difficult to do unless you have a way to transfer data among the different types of data stores. PolyBase bridges this gap by operating on data that is external to SQL Server.  
  
To keep it simple, PolyBase does not require you to install additional software to your Hadoop environment. Querying external data uses the same syntax as querying a database table. This all happens transparently. PolyBase handles all the details behind-the-scenes, and no knowledge about Hadoop is required by the end user to query external tables. 
  
 PolyBase can:  
  
-   **Query data stored in Hadoop from SQL Server or PDW.** Users are storing data in cost-effective distributed and scalable systems, such as Hadoop. PolyBase makes it easy to query the data by using T-SQL.  
  
-   **Query data stored in Azure Blob Storage.** Azure blob storage is a convenient place to store data for use by Azure services.  PolyBase makes it easy to access the data by using T-SQL.  
  
-   **Import data from Hadoop, Azure Blob Storage, or Azure Data Lake Store** Leverage the speed of Microsoft SQL's columnstore technology and analysis capabilities by importing data from Hadoop, Azure Blob Storage, or Azure Data Lake Store into relational tables. There is no need for a separate  ETL or import tool.  

-   **Export data to Hadoop, Azure Blob Storage, or Azure Data Lake Store.** Archive data to Hadoop, Azure Blob Storage, or Azure Data Lake Store to achieve cost-effective storage and keep it online for easy access.  
  
-   **Integrate with BI tools.** Use PolyBase with Microsoft’s business intelligence and analysis stack, or use any third party tools that is compatible with SQL Server.  
  
## Performance  
  
-   **Push computation to Hadoop.**The query optimizer makes a cost-based decision to push computation to Hadoop when doing so will improve query performance.  It uses statistics on external tables to make the cost-based decision.   Pushing computation creates MapReduce jobs and leverages Hadoop's distributed computational resources.  
  
-   **Scale compute resources.** To improve query performance, you can use SQL Server [PolyBase scale-out groups](../../relational-databases/polybase/polybase-scale-out-groups.md). This enables parallel data transfer between SQL Server instances and Hadoop nodes, and it adds compute resources for operating on the external data.  
  
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
  
  
