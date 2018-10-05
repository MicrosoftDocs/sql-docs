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

The following sections provide known issues for SQL Server big data clusters in CTP 2.0.

#### Kubernetes

- SQL Server big data clusters have only been tested with Kubernetes version 1.10.*. If you are using Azure Kubernetes Service (AKS), note that disk resizing is not available for version 1.10*.

#### External tables

- It is possible to create a data pool external table for a table that has unsupported column types. If you query the external table, you get a message similar to the following:

   `Msg 7320, Level 16, State 110, Line 44 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 105079; Columns with large object types are not supported for external generic tables.`

- If you query a storage pool external table, you might get an error if the underlying file is being copied into HDFS at the same time.

   `Msg 7320, Level 16, State 110, Line 157 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 110806;A distributed query failed: One or more errors occurred.`

#### Spark

- If a storage node goes down and gets recreated, Spark might crash.

#### Security

- The SA_PASSWORD is part of the environment and discoverable (for example in a cord dump file). You must reset the SA_PASSWORD on the master instance after deployment. This is not a bug but an important security step.

- AKS logs may contain SA password for big data cluster deployments.

## Next steps

For more information about SQL Server big data clusters, see [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md).
