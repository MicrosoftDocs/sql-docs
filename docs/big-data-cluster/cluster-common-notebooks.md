---
title: Common scenario working with Big Data Clusters with Jupyter notebooks and Azure Data Studio
titleSuffix: SQL Server Big Data Clusters
description: Common scenario working with BDC withJupyter notebooks and Azure Data Studio on SQL Server 2019 big data cluster.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 07/16/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.metadata: seo-lt-2019
---

# Common notebooks for SQL Server Big Data Clusters

This article lists notebooks for SQL Server Big Data Clusters. The executable notebooks (.ipynb) are designed for SQL Server 2019 to assist with common scenarios in big data clusters.

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

Each notebook is designed to check for its own dependencies. The **Run all cells** option either completes successfully or raises an exception with a hyperlinked *hint* to another notebook to resolve the missing dependency. Follow the hint hyperlink to the target notebook, click **Run all cells**. Upon success return back to the original notebook, then click **Run all cells**.

Once all dependencies are installed, but **Run all cells** fails, each notebook will analyze results and where possible, produce a hyperlinked hint to another notebook to further aid in resolving the issue.

* For more information on using notebooks to manage SQL Server Big Data Clusters, see [Manage SQL Server Big Data Clusters with Azure Data Studio notebooks](notebooks-manage-bdc.md).
* For the location of big data cluster administration notebooks, see [Where to find SQL Server Big Data Clusters administration notebooks](view-cluster-status.md#where-to-find--administration-notebooks).

## Gathering logs from Big Data Cluster

The notebooks in this section are used as prerequisites for other notebooks, such as login and logout of a cluster.

|Name |Description |
|---|---|
|SOP005 - az login|Use the az command line interface to login to Azure. |
|SOP006 - az logout|Use the az command line interface to logout of Azure.|
|SOP007 - Version information (azdata, bdc, kubernetes)|Define helper functions used in the notebook on versioning information.|
|SOP011 - Set kubernetes configuration context|Set the kubernetes configuration to use. |
|SOP028 - azdata login|Use the azdata command line interface to login to a Big Data Cluster. |
|SOP033 - azdata logout|Use the azdata command line interface to logout of a Big Data Cluster. |
|SOP034 - Wait for BDC to be Healthy|Blocks until the Big Data Cluster is healthy, or the specified timeout expires. The min_pod_count parameter indicates that the health check will not pass until at least this number of pods exists in the cluster. If any existing pods beyond this limit are unhealthy, the cluster is not healthy.|
|OPR001 - Create app-deploy|Use this notebook to create an app in Big Data Cluster. |
|OPR002 - Run app-deploy|Use this notebook to run an app in Big Data Cluster. |
|OPR003 - Create cronjob|Use this notebook to create a cronjob in Big Data Cluster. |
|OPR004 - Suspend cronjob|Use this notebook to suspend a cronjob in Big Data Cluster. |
|OPR005 - Resume cronjob|Use this notebook to resume a cronjob in Big Data Cluster. |
|OPR006 - Delete cronjob|Use this notebook to delete a cronjob in Big Data Cluster. |
|OPR007 - Delete app-deploy|Use this notebook to delete an app in Big Data Cluster. |
|OPR100 - Deploy and Schedule notebook(s)|Use this notebook to deploy a list of notebooks to an App-Deploy pod, run the App-Deploy on a schedule using a Kubernetes CronJob and install a Grafana dashboard to view results.|
|OPR600 - Monitor infrastructure (Kubernetes)|Use this notebook to monitor infrastructure.|
|OPR700 - Create Grafana Dashboard for App-Deploy Applications|Use this notebook to create a dashboard in Grafana to monitor App-Deploy results and generate alerts if a Canary or Application starts failing.|
|OPR900 - Troubleshoot run app-deploy|Use this notebook to run the app-deploy script directly in the container (using kubectl exec). This can give more debugging information for troubleshooting issues, especially related to issues in the script start up stage.|
|OPR901 - Troubleshoot cronjob|Use this notebook to run the cronjob script directly in the container (using kubectl exec). This can give more debugging information for troubleshooting issues.|


## Analyze logs from Big Data Clusters

A set of example notebooks demonstrating SQL Server Big Data Cluster scenarios.

|Name |Description |
|---|---|
|SAM001a - Query Storage Pool from SQL Server Master Pool (1 of 3) - Load sample data|In this 3 part tutorial, load data into the Storage Pool (HDFS) using azdata, convert it into Parquet (using Spark) and in the 3rd part, query the data using the Master Pool (SQL Server). |
|SAM001b - Query Storage Pool from SQL Server Master Pool (2 of 3) - Convert data to parquet|In this second part of a 3 part tutorial, use Spark to convert a .csv file into a parquet file.|
|SAM001c - Query Storage Pool from SQL Server Master Pool (3 of 3) - Query HDFS from SQL Server|In this 3rd part of the Storage Pool tutorial, you'll learn how to create an external table pointing to HDFS data in a big data cluster and join this data with high-value data in the master instance.|
|SAM002 - Storage Pool (2 of 2) - Query HDFS|In this second part of the Storage Pool tutorial, you'll learn how to create an external table pointing to HDFS data in a big data cluster and join this data with high-value data in the master instance|
|SAM003 - Data Pool Example|In this tutorial, you learn how to create a data pool source and an external table in the data pool, then insert data in data pool tables and loading data from one data pool table to another. Join data in the data pool table with other data pool tables, also truncating tables and cleanup. |
|SAM004 - Virtualize data from MongoDB|To query the data from a MongoDB external data source, you must create external tables to reference that external data. This section provides sample code to create these external tables.|
|SAM005 - Virtualize data from Oracle|To query the data from an Oracle external data source, you must create external tables to reference that external data. This section provides sample code to create these external tables.|
|SAM006 - Virtualize data from SQL Server|To query data virtually from another SQL Server data source, you must create external tables to reference that external data. This section provides sample code to create these external tables.|
|SAM007 - Virtualize data from Teradata|To query the data from a Teradata external data source, you must create external tables to reference that external data. This section provides sample code to create these external tables.|
|SAM008 - Spark using azdata|Azdata and kubectl commands to work with Spark sessions.|
|SAM009 - HDFS using azdata|Azdata and kubectl commands to work with HDFS.|
|SAM010 - App using azdata|Azdata and kubectl commands to work with App Deploy. |

## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md).
