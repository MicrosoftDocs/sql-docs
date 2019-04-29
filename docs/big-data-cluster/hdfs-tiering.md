---
title: Configure HDFS tiering
titleSuffix: SQL Server big data clusters
description: This article explains how to configure HDFS tiering to mount an external Azure Data Lake Storage file system into HDFS on a SQL Server 2019 big data cluster (preview).
author: nelgson
ms.author: negust
ms.reviewer: jroth
manager: craigg
ms.date: 04/23/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Configure HDFS tiering on SQL Server big data clusters

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

HDFS Tiering provides the ability to mount external, HDFS-compatible file system in HDFS. This article explains how to configure HDFS tiering for SQL Server 2019 big data clusters (preview). At this time, we support connecting to Azure Data Lake Storage Gen2, and Amazon S3. 

## HDFS tiering overview

With tiering, applications can seamlessly access data in a variety of external stores as though the data resides in the local HDFS. Mounting is a metadata operation, where the metadata that describes the namespace on the external file system is copied over to your local HDFS. This metadata includes information about the external directories and files along with their permissions and ACLs. The corresponding data is only copied on-demand, when the data itself is accessed through for example a query. The external file-system data can now be accessed from the SQL Server big data cluster. You can run Spark jobs and SQL queries on this data in the same way that you would run them on any local data stored in HDFS on the cluster.

### Caching
Today, by default, 1% of the total HDFS storage will be reserved for caching of mounted data. Caching is a global setting across mounts.

> [!NOTE]
> HDFS Tiering is a feature developed by Microsoft, and an earlier version of it has been released as part of Apache Hadoop 3.1 distribution. For more information, see [https://issues.apache.org/jira/browse/HDFS-9806](https://issues.apache.org/jira/browse/HDFS-9806) for details.

The following sections provide an example of how to configure HDFS tiering with an Azure Data Lake Storage Gen2 data source.

## Prerequisites

- [Deployed big data cluster](deployment-guidance.md)
- [Big data tools](deploy-big-data-tools.md)
  - **mssqlctl**
  - **kubectl**

## Mounting instructions

We support connecting to Azure Data Lake Storage Gen2 and Amazon S3. Instructions on how to mount against these storage types can be found in the following articles:

- [How to mount ADLS Gen2 for HDFS tiering in a big data cluster](hdfs-tiering-mount-adlsgen2.md)
- [How to mount S3 for HDFS tiering in a big data cluster](hdfs-tiering-mount-s3.md)

## <a id="issues"></a> Known issues and limitations

The following list provides known issues and current limitations when using HDFS tiering in SQL Server big data clusters:

- If the mount is stuck in a `CREATING` state for a long time, it has most likely failed. In this situation, cancel the command and delete the mount if necessary. Verify that your parameters and credentials are correct before retrying.

- Mounts cannot be created on existing directories.

- Mounts cannot be created within existing mounts.

- If any of the ancestors of the mount-point do not exist, they will be created with the permissions defaulted to r-xr-xr-x (555).

- Mount creation can take some time depending on the number and size of files being mounted. During this process, the files under the mount aren't visible to users. While the mount is created, all files will be added to a temporary path, which defaults to `/_temporary/_mounts/<mount-location>`.

- The mount creation command is asynchronous. After the command is run, the mount status can be checked to understand the state of the mount.

- When creating the mount, the argument used for **--mount-path** is essentially a unique identifier of the mount. The same string (including the "/" in the end if present) must be used in subsequent commands.

- The mounts are read-only. You cannot create any directories or files under a mount.

- We do not recommend mounting directories and files that can change. After the mount is created, any changes or updates to the remote location will not be reflected in the mount in HDFS. If changes do occur in the remote location, you can choose to delete and recreate the mount to reflect the updated state.

## Next steps

For more information about SQL Server 2019 big data clusters, see [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md).
