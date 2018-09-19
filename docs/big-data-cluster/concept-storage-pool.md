---
title: What is the SQL Server Big Data Clusters storage pool? | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 09/24/2018
ms.topic: conceptual
ms.prod: sql
---

# What is the SQL Server Big Data Clusters storage pool?

This article describes the role of the *SQL Server storage pool* in a SQL Server 2019 preview Big Data cluster. The following sections describe the architecture and functionality of a SQL storage pool.

## Storage pool architecture

The storage pool consists of storage nodes comprised of SQL Server on Linux, Spark, and HDFS. All the storage nodes in a SQL Big Data cluster are members of an HDFS cluster.

![Storage pool architecture](media/concept-storage-pool/scale-big-data-on-demand.png)

## Responsibilities

Storage nodes are responsible for:

- Data ingestion through Spark.
- Data storage in HDFS (Parquet format). HDFS also provides data persistency, as HDFS data is spread across all the storage nodes in the SQL Big Data cluster.
- Data access through HDFS and SQL Server endpoints.

## Next steps

To learn more about the SQL Server Big Data Clusters, see the following overview:

- [What is SQL Server 2019 Big Data Clusters?](big-data-cluster-overview.md)