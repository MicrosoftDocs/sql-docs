---
title: Overview of the SQL Big Data Clusters master instance | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 09/24/2018
ms.topic: conceptual
ms.prod: sql
---

# Overview of the SQL Server Big Data Cluster master instance

This article describes the role of the *SQL Server master instance* in a SQL Server 2019 preview Big Data cluster. The master instance is a SQL Server instance running in the SQL Server Big Data Clusters [control plane](big-data-cluster-overview.md#controlplane).

The SQL Server master instance serves the following functions:

## Connectivity

The SQL Server master instance provides an externally-accessible TDS endpoint for the cluster.

## Scale-out query management

The SQL Server master instance contains the scale-out query engine that is used to distribute queries across SQL Server instances running nodes in the cluster compute plane and data plane. The scale-out query engine also provides access through Transact-SQL to all Hive tables in the cluster without any additional configuration.

## Metadata and user databases

In addition to the standard SQL Server system databases, the SQL master instance also contains a metadata database that holds HDFS table metadata, a data plane shard map, and details of external tables that provide access to the cluster data plane, and external sources using PolyBase.

You can also choose to add your own high-value databases to the SQL master instance.

## Next steps

To learn more about the overall architecture of Big Data Clusters, see [What is SQL Server 2019 Big Data Clusters?](big-data-cluster-overview.md). 

To deploy a Big Data cluster on Kubernetes, see [Quickstart: Deploy SQL Server Big Data Cluster on Kubernetes](quickstart-big-data-cluster-deploy.md). 
