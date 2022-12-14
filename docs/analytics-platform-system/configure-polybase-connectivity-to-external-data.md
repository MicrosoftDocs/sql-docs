---
title: Configure PolyBase connectivity
description: Explains how to configure PolyBase in Parallel Data Warehouse to connect to external Hadoop or Microsoft Azure storage blob data sources. Use PolyBase to run queries that integrate data from multiple sources, including Hadoop, Azure Blob Storage, and Parallel Data Warehouse.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 04/17/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom: seo-dt-2019
---

# Configure PolyBase connectivity
PolyBase enables your Analytics Platform System (APS) to process Transact-SQL queries that can read data from and write data to external data sources. The same queries that access external data can also include relation tables in your APS. This allows you to combine data from external sources with high-value relational data in your APS databases.

![PolyBase logical](media/polybase/polybase-logical.png)

PolyBase on APS supports reading and writing to Hadoop (HDFS) file system and Azure Blob Storage. PolyBase also has the ability to push some computation to Hadoop nodes as mapreduce jobs to optimize the overall query performance. PolyBase on APS can operate on delimited text, ORC and Parquet files. See [What is PolyBase](../relational-databases/polybase/polybase-guide.md) for a full description and its capabilities.

> [!NOTE]
> APS currently only supports standard general purpose v1 locally redundant (LRS) Azure Blob Storage.

## Features and limitations
See [features and limitation](../relational-databases/polybase/polybase-versioned-feature-summary.md) for a summary of PolyBase features available and known limitations on APS and other SQL Server products.

> [!NOTE] 
> The rest of the PolyBase related articles descibe how to configure PolyBase on APS 2016 (AU6) and later.

## See Also
- [Hadoop](polybase-configure-hadoop.md)
- [Azure Blob Storage](polybase-configure-azure-blob-storage.md)
<!-- MISSING LINKS [PolyBase &#40;SQL Server PDW&#41;](../sqlpdw/polybase-sql-server-pdw.md)  -->  
