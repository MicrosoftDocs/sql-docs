---
title: Common scenario working with Big Data Clusters (BDC) with Jupyter notebooks and Azure Data Studio
titleSuffix: SQL Server Big Data Clusters
description: Common scenario working with BDC withJupyter notebooks and Azure Data Studio on SQL Server 2019 big data cluster.
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

This page is an index of notebooks for SQL Server Big Data Clusters. Those executable notebooks (.ipynb) are designed for SQL Server 2019 to assist in showing common scenario on Big Data Clusters.

Each notebook is designed to check for its own dependencies. A 'run all cells' will either complete successfully or will raise an exception with a hyperlinked 'HINT' to another notebook to resolve the missing dependency. Follow the 'HINT' hyperlink to the subsequent notebook, press 'run all cells', and upon success return back to the original notebook, and 'run all cells'.

Once all dependencies are installed, but 'run all cells' fails, each notebook will analyze results and where possible, produce a hyperlinked 'HINT' to another notebook to further aid in resolving the issue.


## Gathering logs from Big Data Cluster (BDC)

The notebooks in this section are used as prerequisites for other notebooks, such as login and logout of a cluster.

|Name<br /><sub>(notebooks)</sub> |Description |
|---|---|---|---|
|SOP005 - az login|Use the az command line interface to login to Azure. |
|SOP006 - az logout|Use the az command line interface to logout of Azure.|Use the az command line interface to login to Azure.|
|SOP007 - Version information (azdata, bdc, kubernetes)|Define helper functions used in the notebook on versioning information.|
|SOP011 - Set kubernetes configuration context|Set the kubernetes configuration to use. |
|SOP028 - azdata login|Use the azdata command line interface to login to a Big Data Cluster. |
|SOP033 - azdata logout|Use the azdata command line interface to logout of a Big Data Cluster. |
|SOP034 - Wait for BDC to be Healthy|Blocks until the Big Data Cluster is healthy, or the specified timeout expires.The min_pod_count parameter indicates that the health check will not pass until at least this number of pods exists in the cluster. If any existing pods beyond this limit are unhealthy, the cluster is not healthy.|

## Analyse logs from Big Data Clusters (BDC)

A set of example notebooks demonstrating SQL Server Big Data Cluster scenarios.

|Name<br /><sub>(notebooks)</sub> |Description |
|---|---|---|---|
|SAM001a - Query Storage Pool from SQL Server Master Pool (1 of 3) - Load sample data|In this 3 part tutorial, load data into the Storage Pool (HDFS) using azdata, convert it into Parquet (using Spark) and the in the 3rd part, query the data using the Master Pool (SQL Server). |
|SAM001b - Query Storage Pool from SQL Server Master Pool (2 of 3) - Convert data to parquet|Use the az command line interface to logout of Azure.|n this 2nd part of a 3 part tutorial, use Spark to convert a .csv file into a parquet file.|
|SAM001c - Query Storage Pool from SQL Server Master Pool (3 of 3) - Query HDFS from SQL Server|In this 3rd part of the Storage Pool tutorial, you’ll learn how to create an external table pointing to HDFS data in a big data cluster
and join this data with high-value data in the master instance.|
|SAM002 - Storage Pool (2 of 2) - Query HDFS|In this 2nd part of the Storage Pool tutorial, you’ll learn how to create an external table pointing to HDFS data in a big data cluster and join this data with high-value data in the master instance|
|SAM003 - Data Pool Example|In this tutorial, you learn how to create a data pool source and an external table in the data pool, then insert data in data pool tables and loading data from one data pool table to another. Join data in the data pool table with other data pool tables, also truncating tables and cleanup. |
|SAM004 - Virtualize data from MongoDB|To query the data from a MongoDB external data source, you must create external tables to reference that external data. This section provides sample code to create these external tables.|
|SAM005 - Virtualize data from Oracle|To query the data from an Oracle external data source, you must create external tables to reference that external data. This section provides sample code to create these external tables.|
|SAM006 - Virtualize data from SQL Server|To query data virtually from another SQL Server data source, you must create external tables to reference that external data. This section provides sample code to create these external tables.|
|SAM007 - Virtualize data from Teradata|To query the data from a Teradata external data source, you must create external tables to reference that external data. This section provides sample code to create these external tables.|
|SAM008 - Spark using azdata|Azdata and kubectl commands to work with Spark sessions.|
|SAM009 - HDFS using azdata|Azdata and kubectl commands to work with HDFS.|
|SAM010 - App using azdata|Azdata and kubectl commands to work with App Deploy. |



## Next steps

You can check out additional samples at [App Deploy Samples](https://aka.ms/sql-app-deploy).

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
