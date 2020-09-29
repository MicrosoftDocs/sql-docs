---
title: Gathering and analysing logs with Jupyter notebooks and Azure Data Studio
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

# Gathering and analysing logs in the cluster with notebooks

This page is an index of notebooks for SQL Server Big Data Clusters. Those executable notebooks (.ipynb) are designed for SQL Server 2019 to assist in logging Big Data Clusters.

Each notebook is designed to check for its own dependencies. A 'run all cells' will either complete successfully or will raise an exception with a hyperlinked 'HINT' to another notebook to resolve the missing dependency. Follow the 'HINT' hyperlink to the subsequent notebook, press 'run all cells', and upon success return back to the original notebook, and 'run all cells'.

Once all dependencies are installed, but 'run all cells' fails, each notebook will analyze results and where possible, produce a hyperlinked 'HINT' to another notebook to further aid in resolving the issue.


## Gathering logs from Big Data Cluster (BDC)

This section contains a set of notebooks useful for getting logs from a SQL Server Big Data Cluster (BDC).

<log-files> notebook table

## Analyse logs from Big Data Clusters (BDC)

A set of notebooks to gather and analyze logs from a SQL Server Big Data Cluster.  The analysis process will SUGGEST follow on TSGs to run for known issue found in the logs
<log-analyzers> notebook table



## Next steps

You can check out additional samples at [App Deploy Samples](https://aka.ms/sql-app-deploy).

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
