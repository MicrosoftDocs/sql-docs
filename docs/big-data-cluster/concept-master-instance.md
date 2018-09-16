---
title: What is the SQL Big Data Clusters master instance? | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 09/24/2018
ms.topic: conceptual
ms.prod: sql
---

# What is the SQL Big Data Clusters master instance?

This article describes the role of the *SQL Server master instance* in a SQL Server 2019 preview Big Data cluster. The master instance is a SQL Server instance running in the SQL Server Big Data Clusters [control plane](big-data-cluster-overview.md#controlplane).

The SQL Server master instance provides the following functionality:

## Connectivity

The SQL Server master instance provides an externally accessible TDS endpoint for the cluster.

## Scale-out query management

The SQL Server master instance contains the scale-out query engine that is used to distribute queries across SQL Server instances on nodes in the [compute pool](concept-compute-pool.md). The scale-out query engine also provides access through Transact-SQL to all Hive tables in the cluster without any additional configuration.

## Metadata and user databases

In addition to the standard SQL Server system databases, the SQL master instance also contains the following:

- A metadata database that holds HDFS-table metadata
- A data plane shard map
- Details of external tables that provide access to the cluster data plane.
- External sources using PolyBase.

You can also choose to add your own high-value databases to the SQL master instance.

## Next steps

To learn more about the SQL Server Big Data Clusters, see the following articles:

[What is SQL Server 2019 Big Data Clusters?](big-data-cluster-overview.md).
[Quickstart: Deploy SQL Server Big Data Cluster on Kubernetes](quickstart-big-data-cluster-deploy.md).
