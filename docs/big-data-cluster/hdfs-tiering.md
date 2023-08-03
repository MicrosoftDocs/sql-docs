---
title: Configure HDFS tiering
titleSuffix: SQL Server Big Data Clusters
description: This article explains how to configure HDFS tiering to mount an external Azure Data Lake Storage file system into HDFS on a SQL Server 2019 big data cluster.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 08/21/2019
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# Configure HDFS tiering on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

HDFS Tiering provides the ability to mount external, HDFS-compatible file system in HDFS. This article explains how to configure HDFS tiering for SQL Server Big Data Clusters. At this time, we support connecting to Azure Data Lake Storage Gen2, and Amazon S3. 

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

## HDFS tiering overview

With tiering, applications can seamlessly access data in a variety of external stores as though the data resides in the local HDFS. Mounting is a metadata operation, where the metadata that describes the namespace on the external file system is copied over to your local HDFS. This metadata includes information about the external directories and files along with their permissions and ACLs. The corresponding data is only copied on-demand, when the data itself is accessed through for example a query. The external file-system data can now be accessed from the SQL Server big data cluster. You can run Spark jobs and SQL queries on this data in the same way that you would run them on any local data stored in HDFS on the cluster.

This 7-minute video provides an overview of HDFS tiering:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Unify-your-data-lakes-with-HDFS-tiering/player?WT.mc_id=dataexposed-c9-niner]


### Caching
Today, by default, 1% of the total HDFS storage will be reserved for caching of mounted data. Caching is a global setting across mounts.

> [!NOTE]
> HDFS Tiering is a feature developed by Microsoft, and an earlier version of it has been released as part of Apache Hadoop 3.1 distribution. For more information, see [https://issues.apache.org/jira/browse/HDFS-9806](https://issues.apache.org/jira/browse/HDFS-9806) for details.

The following sections provide an example of how to configure HDFS tiering with an Azure Data Lake Storage Gen2 data source.

## Refresh

HDFS tiering supports refresh. Refresh an existing mount for the latest snapshot of the remote data.

## Prerequisites

- [Deployed big data cluster](deployment-guidance.md)
- [Big data tools](deploy-big-data-tools.md)
  - **azdata**
  - **kubectl**

## Mounting instructions

We support connecting to Azure Data Lake Storage Gen2 and Amazon S3. Instructions on how to mount against these storage types can be found in the following articles:

- [How to mount ADLS Gen2 for HDFS tiering in a big data cluster](hdfs-tiering-mount-adlsgen2.md)
- [How to mount S3 for HDFS tiering in a big data cluster](hdfs-tiering-mount-s3.md)

## <a id="issues"></a> Known issues and limitations

The following list provides known issues and current limitations when using HDFS tiering in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]:

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

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)], see [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md).
