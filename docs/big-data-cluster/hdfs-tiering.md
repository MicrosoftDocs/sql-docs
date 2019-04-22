---
title: Configure HDFS tiering
titleSuffix: SQL Server big data clusters
description: This article explains how to configure HDFS tiering to mount an external Azure Data Lake Storage file system into HDFS on a SQL Server 2019 big data cluster (preview).
author: nelgson
ms.author: negust
ms.reviewer: jroth
manager: craigg
ms.date: 03/27/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Configure HDFS tiering on SQL Server big data clusters

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

HDFS Tiering provides the ability to mount external, HDFS-compatible file system in HDFS. This article explains how to configure HDFS tiering for SQL Server 2019 big data clusters (preview). At this time, CTP 2.4 only supports connecting to Azure Data Lake Storage Gen2, which is the focus of this article.

## HDFS tiering overview

With tiering, applications can seamlessly access data in a variety of external stores as though the data resides in HDFS. Mounting is a metadata operation, where the metadata that describes the namespace on the external file system is copied over to HDFS. This metadata includes information about the external directories and files along with their permissions and ACLs. The corresponding data is only copied on-demand, when the data itself is accessed. The external file-system data can now be accessed from the SQL Server big data cluster. You can run Spark jobs and SQL queries on this data in the same way that you would run them on any local data stored in HDFS on the cluster.

> [!NOTE]
> HDFS Tiering is a feature developed by Microsoft, and an earlier version of it has been released as part of Apache Hadoop 3.1 distribution. For more information, see [https://issues.apache.org/jira/browse/HDFS-9806](https://issues.apache.org/jira/browse/HDFS-9806) for details.

The following sections provide an example of how to configure HDFS tiering with an Azure Data Lake Storage Gen2 data source.

## Prerequisites

- [Deployed big data cluster](deployment-guidance.md)
- [Big data tools](deploy-big-data-tools.md)
  - **mssqlctl**
  - **kubectl**

## <a id="load"></a> Load data into Azure Data Lake Storage

The following section describes how to set up Azure Data Lake Storage Gen2 for testing HDFS tiering. If you already have data stored in Azure Data Lake Storage, you can skip this section to use your own data.

1. [Create a storage account with Data Lake Storage Gen2 capabilities](https://docs.microsoft.com/azure/storage/blobs/data-lake-storage-quickstart-create-account).

1. [Create a blob container](https://docs.microsoft.com/azure/storage/blobs/storage-quickstart-blobs-portal) in this storage account for your external data.

1. Upload a CSV or Parquet file into the container. This is the external HDFS data that will be mounted to HDFS in the big data cluster.

## <a id="mount"></a> Mount the remote HDFS storage

The following steps mount the remote HDFS storage in Azure Data Lake into the local HDFS storage of your big data cluster.

1. Open a command-prompt on a client machine that can access your big data cluster.

1. Create a local file named **files.creds** that contains your Azure Data Lake Storage Gen2 account credentials using the following format:

   ```text
   fs.azure.abfs.account.name=<your-storage-account-name>.dfs.core.windows.net
   fs.azure.account.key.<your-storage-account-name>.dfs.core.windows.net=<storage-account-access-key>
   ```

   > [!TIP]
   > For more information on how to find the access key (`<storage-account-access-key>`) for your storage account, see [View and copy access keys](https://docs.microsoft.com/azure/storage/common/storage-account-manage?#view-and-copy-access-keys).

1. Use **kubectl** to find the IP Address for the **endpoint-service-proxy** service in your big data cluster. Look for the **External-IP**.

   ```bash
   kubectl get svc endpoint-service-proxy -n <your-cluster-name>
   ```

1. Log in with **mssqlctl** using the service proxy endpoint with your cluster username and password:

   ```bash
   mssqlctl login -e https://<IP-of-endpoint-service-proxy>:30777/ -u <username> -p <password>
   ```

1. Mount the remote HDFS storage in Azure using **mssqlctl storage mount create**. Replace the placeholder values before running the following command:

   ```bash
   mssqlctl storage mount create --remote-uri abfs://<blob-container-name>@<storage-account-name>.dfs.core.windows.net/ --mount-path /mounts/<mount-name> --credential-file <path-to-adls-credentials>/file.creds
   ```

   > [!NOTE]
   > The mount create command is asynchronous. At this time, there is no message indicating whether the mount succeeded. See the [status](#status) section to check the status of your mounts.

If mounted successfully, you should be able to query the HDFS data and run Spark jobs against it. It will appear in the HDFS for your big data cluster in the location specified by `--local-path`.

## <a id="status"></a> Get the status of mounts

To list the status of all mounts in your big data cluster, use the following command:

```bash
mssqlctl storage mount status
```

To list the status of a mount at a specific path in HDFS, use the following command:

```bash
mssqlctl storage mount status --mount-path <mount-path-in-hdfs>
```

## <a id="delete"></a> Delete the mount

To delete the mount, use the **mssqlctl storage mount delete** command, and specify the mount path in HDFS:

```bash
mssqlctl storage mount delete --mount-path <mount-path-in-hdfs>
```

## <a id="issues"></a> Known issues and limitations

The following list provides known issues and current limitations when using HDFS tiering in SQL Server big data clusters:

- If the size of the external directory being mounted is larger than the capacity of the cluster, mounting fails.

- If the mount is stuck in a `CREATING` state for a long time, it has most likely failed. In this situation, cancel the command and delete the mount if necessary. Verify that your parameters and credentials are correct before retrying.

- Mounts cannot be created on existing directories.

- Mounts cannot be created within existing mounts.

- If any of the ancestors of the mount-point do not exist, they will be created with the permissions defaulted to r-xr-xr-x (555).

- Mount creation can take some time depending on the number and size of files being mounted. During this process, the files under the mount aren't visible to users. While the mount is created, all files will be added to a temporary path, which defaults to `/_temporary/_mounts/<mount-location>`.

- The mount creation command is asynchronous. After the command is run, the mount status can be checked to understand the state of the mount.

- When creating the mount, the argument used for **--local-path** is essentially a unique identifier of the mount. The same string (including the "/" in the end if present) must be used in subsequent commands.

- The mounts are read-only. You cannot create any directories or files under a mount.

- We do not recommend mounting directories and files that can change. After the mount is created, any changes or updates to the remote location will not be reflected in the mount in HDFS. If changes do occur in the remote location, you can choose to delete and recreate the mount to reflect the updated state.

## Next steps

For more information about SQL Server 2019 big data clusters, see [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md).
