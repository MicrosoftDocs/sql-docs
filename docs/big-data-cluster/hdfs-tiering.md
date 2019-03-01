---
title: Configure HDFS tiering
titleSuffix: SQL Server 2019 big data clusters
description: This article explains how to configure HDFS tiering to mount an external Azure Data Lake Storage file system into HDFS on a SQL Server 2019 big data cluster (preview).
author: nelgson
ms.author: negust
ms.reviewer: jroth
manager: craigg
ms.date: 02/29/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Configure HDFS tiering on SQL Server 2019 big data clusters

HDFS Tiering provides the ability to mount external, HDFS-compatible file system in HDFS. This article explains how to configure HDFS tiering for SQL Server 2019 big data clusters (preview). At this time, CTP 2.3 only supports connecting to Azure Data Lake Storage Gen2, which is the focus on this article.

## HDFS tiering overview

With tiering, applications can seamlessly access data in a variety of external stores as though the data resides in HDFS. Mounting is a metadata operation, where the metadata that describes the namespace on the external file system is copied over to HDFS. This metadata includes information about the external directories and files along with their permissions and ACLs. The corresponding data is only copied on-demand, when the data itself is accessed. The external file-system data can now be accessed from the SQL Server big data cluster. You can run Spark jobs and SQL queries on this data in the same way that you would run them on any local data stored in HDFS on the cluster.

> [!NOTE]
> HDFS Tiering is a feature developed by Microsoft, and an earlier version of it has been released as part of Apache Hadoop 3.1 distribution. For more information, see [https://issues.apache.org/jira/browse/HDFS-9806](https://issues.apache.org/jira/browse/HDFS-9806) for details.

The remaining sections of this article provides an example of how to configure HDFS tiering with an Azure Data Lake Storage Gen2 data soure.

## Prerequisites

- [Deployed big data cluster](deployment-guidance.md)
- [Big data tools](deploy-big-data-tools.md)
  - **mssqlctl**
  - **kubectl**

## Load data into Azure Data Lake Storage

The following section describes how to setup Azure Data Lake Storage Gen2 for testing HDFS tiering. If you already have data stored in Azure Data Lake Storage, you can skip this section to use your own data.

1. [Create a storage account with Data Lake Storage Gen2 capabilities](https://docs.microsoft.com/azure/storage/blobs/data-lake-storage-quickstart-create-account).

1. [Create a blob container](https://docs.microsoft.com/azure/storage/blobs/storage-quickstart-blobs-portal) in this storage account for your external data.

1. Upload a CSV or Parquet file into the container. This is the external HDFS data that will be mounted to HDFS in the big data cluster.

## 

## Next steps
