---
title: Monitor Big Data Clusters with Jupyter notebooks and Azure Data Studio
titleSuffix: SQL Server Big Data Clusters
description: Monitoring cluster with Jupyter notebooks and Azure Data Studio on SQL Server 2019 Big Data Clusters.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 07/16/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.metadata: seo-lt-2019
---

# Monitor Big Data Clusters by using Jupyter Notebooks and Azure Data Studio

This page is an index of notebooks for SQL Server Big Data Clusters. Those executable notebooks (.ipynb) are designed for SQL Server 2019 to assist in monitoring Big Data Clusters.

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

Each notebook is designed to check for its own dependencies. The **Run all cells** option either completes successfully or raises an exception with a hyperlinked *hint* to another notebook to resolve the missing dependency. Follow the hint hyperlink to the target notebook, click **Run all cells**. Upon success return back to the original notebook, then click **Run all cells**.

Once all dependencies are installed, but **Run all cells** fails, each notebook will analyze results and where possible, produce a hyperlinked hint to another notebook to further aid in resolving the issue.

* For more information on using notebooks to manage SQL Server Big Data Clusters, see [Manage SQL Server Big Data Clusters with Azure Data Studio notebooks](notebooks-manage-bdc.md).
* For the location of big data cluster administration notebooks, see [Where to find SQL Server Big Data Clusters administration notebooks](view-cluster-status.md#where-to-find--administration-notebooks).

## Monitoring Kubernetes

This section contains a set of notebooks useful for getting information and status about a SQL Server big data cluster using the `azdata` command line interface (CLI).

|Name |Description |
|---|---|---|---|
|TSG006 - Get system pod status|View the status of all system pods. |
|TSG007 - Get BDC pod status|View the big data cluster pods status.|
|TSG008 - Get version information (Kubernetes)|Get the Kubernetes cluster-info.|
|TSG009 - Get nodes (Kubernetes)|Get the kubernetes contexts. |
|TSG010 - Get configuration contexts|Using DMV query to get more information on the internal query processor error|
|TSG015 - View BDC services (Kubernetes)|Get the services status of big data cluster deployed in Kubernetes cluster. |
|TSG016 - Describe BDC pods|Get the pods status of BDC deployed in Kubernetes cluster. |
|TSG020 - Describe nodes (Kubernetes)|Get the node information for big data cluster  use the kubectl command line interface. |
|TSG021 - Get cluster info (Kubernetes)|Get the Kubernetes cluster-info. |
|TSG022 - Get external IP address for kubeadm host|Get the external IP address of the host of kubeadm. |
|TSG023 - Get all BDC objects (Kubernetes)|Get a summary of all Kubernetes resources for the system namespace and the big data cluster namespace. |
|TSG042 - Get node name and external mounts for Data and Logs PVCs|Get node name hosting pod along with the Data and Logs external mounts. |
|TSG063 - Get storage classes (Kubernetes)|Use this notebook to get Kubernetes storage classes. |
|TSG064 - Get BDC Persistent Volume Claims|Show the persistent volume claims (PVCs) for the big data cluster. |
|TSG065 - Get BDC secrets (Kubernetes)|View the big data cluster secrets. |
|TSG066 - Get BDC event (Kubernetes)|View the big data cluster events.|
|TSG072 - Get Persistent Volumes (Kubernetes)|Show the persistent volume (PVs) for the Kubernetes cluster. Persistent Volumes are non-namespaces objects. |
|TSG081 - Get namespaces (Kubernetes)|Get the kubernetes namespaces. |
|TSG089 - Describe BDC non-running pods|Show non-running BDC pods for the Kubernetes cluster. |
|TSG097 - Get BDC stateful sets (Kubernetes)|Show BDC statefulsets for the Kubernetes cluster. |
|TSG098 - Get BDC replicasets (Kubernetes)|Show BDC replicasets for the Kubernetes cluster. |
|TSG099 - Get BDC daemonsets (Kubernetes)|Show BDC daemonsets for the Kubernetes cluster. |


## Monitor Big Data Clusters 

This section contains a set of notebooks useful for getting information and status about the Kubernetes cluster hosting a SQL Server big data cluster.

|Name |Description |
|---|---|---|---|
|TSG003 - Show BDC Spark sessions|View BDC Spark sessions. |
|TSG004 - Show BDC Apps|View the apps up and running in big data cluster.|
|TSG012 - Show BDC Status|Get the current status of different components of big data cluster.|
|TSG013 - Show file list in Storage Pool (HDFS)|Get file list in Storage Pool (HDFS). |
|TSG014 - Show BDC endpoints|Get all available endpoints for BDC Cluster.|
|TSG017 - Show BDC Configuration|Get BDC Configuration. |
|TSG033 - Show BDC SQL status|Get the SQL Server status of BDC deployed in Kubernetes cluster. |
|TSG049 - Show BDC Controller status|et the controller status of BDC deployed in Kubernetes cluster. |
|TSG068 - Show BDC HDFS status|Get the HDFS status of BDC deployed in Kubernetes cluster. |
|TSG069 - Show big data cluster Gateway status|Get the BDC Gateway status of BDC deployed in Kubernetes cluster. |
|TSG070 - Query SQL master pool| Execute SQL query on master instance. |

## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md).
