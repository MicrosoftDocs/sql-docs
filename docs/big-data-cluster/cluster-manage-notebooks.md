---
title: Managing Big Data Clusters (BDC) with Jupyter notebooks and Azure Data Studio
titleSuffix: SQL Server Big Data Clusters
description: Managing Big Data Clusters (BDC) with Jupyter notebooks and Azure Data Studio on SQL Server 2019 big data cluster.
author: cloudmelon
ms.author: melqin
ms.reviewer: mikeray
ms.metadata: seo-lt-2019
ms.date: 09/22/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Managing Big Data Clusters (BDC) the cluster with notebooks

This page is an index of notebooks for SQL Server Big Data Clusters. Those executable notebooks (.ipynb) are designed for SQL Server 2019 to assist in managing Big Data Clusters.

Each notebook is designed to check for its own dependencies. A **Run all cells** will either complete successfully or will raise an exception with a hyperlinked hint to another notebook to resolve the missing dependency. Follow the hint hyperlink to the subsequent notebook, press **Run all cells**, and upon success return back to the original notebook, and **Run all cells**.

Once all dependencies are installed, but **Run all cells** fails, each notebook will analyze results and where possible, produce a hyperlinked hint to another notebook to further aid in resolving the issue.


## Installing and uninstalling utilities Big Data Cluster (BDC)

This section contains a set of notebooks useful for installing and uninstalling command line tools and packages needed to manage SQL Server Big Data Clusters.


<install> notebook table + <install-kubeadm-with-lpvs>




## Managing Certificates on Big Data Clusters (BDC)

A set of notebooks to run a notebook for managing Certificates on Big Data Clusters. 

<cert-management> notebook table




## Next steps

You can check out additional samples at [App Deploy Samples](https://aka.ms/sql-app-deploy).

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
