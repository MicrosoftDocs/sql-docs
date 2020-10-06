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

Each notebook is designed to check for its own dependencies. A **Run all cells** will either complete successfully or will raise an exception with a hyperlinked hint to another notebook to resolve the missing dependency. Follow the hint hyperlink to the subsequent notebook, press **Run all cells**, and upon success return back to the original notebook, and **Run all cells**.

Once all dependencies are installed, but **Run all cells** fails, each notebook will analyze results and where possible, produce a hyperlinked hint to another notebook to further aid in resolving the issue.


## Backup and restore from Big Data Cluster (BDC)

This section contains a set of notebooks useful for backup and restore operations for SQL Server Big Data Clusters.

| Name | Description |
|--|--|
| SOP008 - Backup HDFS files to Azure Data Lake Store Gen2 with distcp | This Standard Operating Procedure (SOP) backs up data from the source SQL Server 2019 BDC cluster’s HDFS filesystem to the Azure Data Lake Store Gen2 account you specify. Please ensure the Azure Data Lake Store Gen2 account is configured with “hierarchical namespace” enabled. |


## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
