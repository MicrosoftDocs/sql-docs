---
title: Release notes for SQL Server 2019 big data clusters | Microsoft Docs
description: This article describes the latest updates and known issues for SQL Server 2019 (preview) big data clusters. 
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 10/04/2018
ms.topic: conceptual
ms.prod: sql
---

# Release notes for SQL Server 2019 big data clusters

This article provides the latest updates and known issues for the latest release of SQL Server big data clusters.

[!INCLUDE [Limited public preview note](../includes/big-data-cluster-preview-note.md)]

## CTP 2.0 (October 2018)

The following sections describe the new features and known issues for big data clusters in SQL Server 2019 CTP 2.0.

### What's New?

- Architecture changes to separate out compute pools, data pools, and storage pools.
- Update to use the latest Spark version (2.3).
- Ability to create external tables over HDFS to natively parquet and CSV files using the SQL Server instance collocated on the HDFS data node in the storage pool.
- Data virtualization (PolyBase) connectors for Oracle, SQL Server, Teradata, MongoDB, and Generic ODBC.
- External table wizard in [Azure Data Studio](../azure-data-studio/what-is.md) to create connectors to Oracle and SQL Server.
- New native [notebook experience](notebooks-guidance.md) in Azure Data Studio.
- New deployment engine that uses the control plane technology from Azure's data services.
- Ability to create data caches in the data pools.
- Ability to load data into data pools using the new .jar from Spark/HDFS.
- Predicate pushdown to data pools and storage pools.
- Cluster Administration portal:
  - Deployment status view
  - Grafana for viewing monitoring data about SQL Server and Kubernetes
  - Kibana for log analytics of all the logs that are collected across the cluster
  - Status views of each of the pools
  - Service end points view
- mssqlctl .whl package to support installing with `pip install mssqlctl`.

### Known issues

TBD 

## Next steps

For more information about SQL Server big data clusters, see [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md).
