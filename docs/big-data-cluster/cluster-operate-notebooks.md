---
title: Operating Big Data Clusters (BDC) with Jupyter notebooks and Azure Data Studio
titleSuffix: SQL Server Big Data Clusters
description: Operating Big Data Clusters (BDC) with Jupyter notebooks and Azure Data Studio on SQL Server 2019 big data cluster.
author: cloudmelon
ms.author: melqin
ms.reviewer: mikeray
ms.metadata: seo-lt-2019
ms.date: 09/22/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Operating Big Data Clusters (BDC) the cluster with notebooks

This page is an index of notebooks for SQL Server Big Data Clusters. Those executable notebooks (.ipynb) are designed for SQL Server 2019 to assist in operating Big Data Clusters.

Each notebook is designed to check for its own dependencies. A 'run all cells' will either complete successfully or will raise an exception with a hyperlinked 'HINT' to another notebook to resolve the missing dependency. Follow the 'HINT' hyperlink to the subsequent notebook, press 'run all cells', and upon success return back to the original notebook, and 'run all cells'.

Once all dependencies are installed, but 'run all cells' fails, each notebook will analyze results and where possible, produce a hyperlinked 'HINT' to another notebook to further aid in resolving the issue.


## Backup and restore from Big Data Cluster (BDC)

This section contains a set of notebooks useful for backup and restore operations for SQL Server Big Data Clusters.

<backup-and-restore> notebook table


## Operating Big Data Clusters (BDC)

A set of notebooks to run a notebook of any kernel type, return error, save results and run follow 'expert rules'.

<notebook-runner> notebook table

+

<notebook-o16n> notebook table


## Canary on Big Data Clusters (BDC)

This section contains a set of notebooks that app-deploy a series of 'canary notebooks' and sets up Kubernetes cronjobs to run the 'canary notebooks' on a schedule.

- A 'canary notebook' exercises an end-to-end scenario in the Big Data Cluster in the manner a user of the Big Data Cluster would.
- The goal of the 'canary notebook' is to provide a failure signal should the end-to-end scenario that it performs fail to succeed, this gives the cluster administrator early warning there may be an issue to troubleshoot.
- The notebooks in this chapter ensure the failure (and success) signals are stored in a database called 'runner' in the master and data pool.
- The notebooks in this chapter ensure the output results of each canary notebook execution are stored in the Storage Pool.
- The notebooks in this chapter deploy a Grafana dashboard that visualizes the status of all the canaries and generates an alert should several canary failure signals occur over a window of time.
- To receive notifications of these alerts, configure a 'Notification Channel' in Grafana.

<canary>

## Next steps

You can check out additional samples at [App Deploy Samples](https://aka.ms/sql-app-deploy).

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
