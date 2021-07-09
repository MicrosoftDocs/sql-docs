---
title: Gathering and analyzing logs with Jupyter notebooks and Azure Data Studio
titleSuffix: SQL Server Big Data Clusters
description: Logging cluster with Jupyter notebooks and Azure Data Studio on SQL Server 2019 big data cluster.
author: cloudmelon
ms.author: melqin
ms.reviewer: mikeray
ms.metadata: seo-lt-2019
ms.date: 09/22/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Gathering and analyzing logs in the cluster with notebooks

This page is an index of notebooks for SQL Server Big Data Clusters. Those executable notebooks (.ipynb) are designed for SQL Server 2019 to assist in logging Big Data Clusters.

Each notebook is designed to check for its own dependencies. A **Run all cells** will either complete successfully or will raise an exception with a hyperlinked hint to another notebook to resolve the missing dependency. Follow the hint hyperlink to the subsequent notebook, press **Run all cells**, and upon success return back to the original notebook, and **Run all cells**.

Once all dependencies are installed, but **Run all cells** fails, each notebook will analyze results and where possible, produce a hyperlinked hint to another notebook to further aid in resolving the issue.

## Gathering logs from Big Data Cluster (BDC)

This section contains a set of notebooks useful for getting logs from a SQL Server Big Data Cluster (BDC).

| Name | Description |
|--|--|
| TSG001 - Run azdata copy-logs | Use the azdata command line interface to copy data in BDC clusters. |
| TSG061 - Get tail of all container logs for pods in BDC namespace | Get all container logs for pods from BDC cluster in the namespace. |
| TSG062 - Get tail of all previous container logs for pods in BDC namespace | Get all previous container logs for pods  from BDC cluster in the namespace. |
| TSG083 - Run kubectl cluster-info dump | Use the kubetl command line interface to dump BDC cluster-related information. |
| TSG084 - Internal Query Processor Error | Using DMV query to get more information on the internal query processor error. |
| TSG091 - Get the azdata CLI logs | Get the azdata logs from the local machine. |



## Analyse logs from Big Data Clusters (BDC)

A set of notebooks to gather and analyze logs from a SQL Server Big Data Cluster.  The analysis process will suggest follow-on notebooks to run for known issue found in the logs.

|Name|Description |
|---|---|
|TSG030 - SQL Server errorlog files|Get SQL Server errorlog files and analyze log entries and suggest further relevant troubleshooting guides. |
|TSG031 - SQL Server PolyBase logs|Get SQL Server PolyBase logs and analyze log entries and suggest further relevant troubleshooting guides.|
|TSG034 - Livy logs|Get Livy logs and analyze log entries and suggest further relevant troubleshooting guides.|
|TSG035 - Spark History logs|Get Spark History logs and analyze log entries and suggest further relevant troubleshooting guides.|
|TSG036 - Controller logs|Get the last ‘n’ hours of controller logs and analyze log entries and suggest further relevant troubleshooting guides.|
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

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
