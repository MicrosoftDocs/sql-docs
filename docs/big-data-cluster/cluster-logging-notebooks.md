---
title: Gathering and analyzing logs with Jupyter notebooks and Azure Data Studio
titleSuffix: SQL Server Big Data Clusters
description: Logging cluster with Jupyter notebooks and Azure Data Studio on SQL Server 2019 Big Data Clusters.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 07/16/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.metadata: seo-lt-2019
---

# Gathering and analyzing logs in Big Data Clusters by using notebooks

This page is an index of notebooks for SQL Server Big Data Clusters. Those executable notebooks (.ipynb) are designed for SQL Server 2019 to assist in logging big data clusters.

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

Each notebook is designed to check for its own dependencies. The **Run all cells** option either completes successfully or raises an exception with a hyperlinked *hint* to another notebook to resolve the missing dependency. Follow the hint hyperlink to the target notebook, click **Run all cells**. Upon success return back to the original notebook, then click **Run all cells**.

Once all dependencies are installed, but **Run all cells** fails, each notebook will analyze results and where possible, produce a hyperlinked hint to another notebook to further aid in resolving the issue.

* For more information on using notebooks to manage SQL Server Big Data Clusters, see [Manage SQL Server Big Data Clusters with Azure Data Studio notebooks](notebooks-manage-bdc.md).
* For the location of big data cluster administration notebooks, see [Where to find SQL Server Big Data Clusters administration notebooks](view-cluster-status.md#where-to-find--administration-notebooks).

## Gathering logs from Big Data Cluster

This section contains a set of notebooks useful for getting logs from a SQL Server Big Data Cluster.

| Name | Description |
|--|--|
| TSG001 - Run azdata copy-logs | Use the azdata command line interface to copy data in big data clusters. |
| TSG061 - Get tail of all container logs for pods in BDC namespace | Get all container logs for pods from big data cluster in the namespace. |
| TSG062 - Get tail of all previous container logs for pods in BDC namespace | Get all previous container logs for pods from big data cluster in the namespace. |
| TSG083 - Run kubectl cluster-info dump | Use the kubetl command line interface to dump big data cluster-related information. |
| TSG084 - Internal Query Processor Error | Using DMV query to get more information on the internal query processor error. |
| TSG091 - Get the azdata CLI logs | Get the azdata logs from the local machine. |



## Analyze logs from Big Data Clusters

A set of notebooks to gather and analyze logs from a SQL Server big data cluster.  The analysis process will suggest follow-on notebooks to run for known issue found in the logs.

|Name|Description |
|---|---|
|TSG030 - SQL Server error log files|Get SQL Server error log files and analyze log entries and suggest further relevant troubleshooting guides. |
|TSG031 - SQL Server PolyBase logs|Get SQL Server PolyBase logs and analyze log entries and suggest further relevant troubleshooting guides.|
|TSG034 - Livy logs|Get Livy logs and analyze log entries and suggest further relevant troubleshooting guides.|
|TSG035 - Spark History logs|Get Spark History logs and analyze log entries and suggest further relevant troubleshooting guides.|
|TSG036 - Controller logs|Get the last 'n' hours of controller logs and analyze log entries and suggest further relevant troubleshooting guides.|
|TSG046 - Knox gateway logs|Knox gives a 500 error to the client, and removes details (the stack) pointing to the cause of the underlying issue. Therefore use this notebook to get the Knox logs from the cluster. Get Knox gateway logs and analyze log entries and suggest further relevant troubleshooting guides.|
|TSG073 - InfluxDB logs|Get InfluxDB logs and analyze log entries and suggest further relevant troubleshooting guides.|
|TSG076 - Elastic Search logs|Get Elastic Search logs and analyze log entries and suggest further relevant troubleshooting guides.|
|TSG077 - Kibana logs|Get Kibana logs and analyze log entries and suggest further relevant troubleshooting guides.|
|TSG088 - Hadoop datanode logs|Get Hadoop datanode logs and analyze log entries and suggest further relevant troubleshooting guides.|
|TSG090 - Yarn nodemanager logs|Get Yarn nodemanager logs and analyze log entries and suggest further relevant troubleshooting guides.|
|TSG092 - Supervisord log tail for all containers in BDC|Get Supervisord log tail for all containers in BDC and analyze log entries and suggest further relevant troubleshooting guides.|
|TSG093 - Agent log tail for all containers in BDC|Get Agent log tail logs for all containers  in BDC and analyze log entries and suggest further relevant troubleshooting guides.|
|TSG094 - Grafana logs|Get Grafana logs and analyze log entries and suggest further relevant troubleshooting guides.|
|TSG095 - Hadoop namenode logs|Get Hadoop namenode logs and analyze log entries and suggest further relevant troubleshooting guides.|
|TSG096 - Zookeeper logs|Get Zookeeper logs and analyze log entries and suggest further relevant troubleshooting guides.|

## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md).
