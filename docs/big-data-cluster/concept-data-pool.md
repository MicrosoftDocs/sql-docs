---
title: What is a SQL Big Data Clusters data pool? | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 10/01/2018
ms.topic: conceptual
ms.prod: sql
---

# What is a SQL Big Data Clusters data pool?

This article describes the role of *SQL Server data pools* in a SQL Server 2019 preview Big Data cluster. The following sections describe the architecture and functionality of a SQL data pool.

## Data pool architecture

A data pool consists of one or more SQL Server data pool instances. SQL data pool instances provide persistent SQL Server storage for the cluster. A data pool is used to ingest data from SQL queries or Spark jobs. To provide better performance across large data sets, data in a data pool is distributed into shards across the member SQL data pool instances.

## Scale-out data marts

Data pools enable the creation of scale-out data marts, where external data from multiple sources is ingested into the data pool. Because data is distributed across data pool instances, parallel queries against the curated data are more efficient.

![Scale-out data mart](media/concept-data-pool/data-virtualization-improvements.png)

## Next steps

To learn more about the SQL Server Big Data Clusters, see the following overview:

- [What is SQL Server 2019 Big Data Clusters?](big-data-cluster-overview.md)
