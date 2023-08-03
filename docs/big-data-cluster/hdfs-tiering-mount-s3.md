---
title: Mount S3 for HDFS tiering
titleSuffix: SQL Server Big Data Clusters
description: This article explains how to configure HDFS tiering to mount an external S3 file system into HDFS on a SQL Server 2019 big data cluster.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 08/21/2019
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# How to mount S3 for HDFS tiering in a big data cluster

The following sections provide an example of how to configure HDFS tiering with an S3 Storage data source.

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

## Prerequisites

- [Deployed big data cluster](deployment-guidance.md)
- [Big data tools](deploy-big-data-tools.md)
  - **azdata**
  - **kubectl**
- Create and upload data to an S3 bucket 
  - Upload CSV or Parquet files to your S3 bucket. This is the external HDFS data that will be mounted to HDFS in the big data cluster.

## Access keys

### Set environment variable for access key credentials

Open a command-prompt on a client machine that can access your big data cluster. Set an environment variable using the following format. Note that the credentials need to be in a comma separated list. The 'set' command is used on Windows. If you are using Linux, then use 'export' instead.

   ```text
    set MOUNT_CREDENTIALS=fs.s3a.access.key=<Access Key ID of the key>,
    fs.s3a.secret.key=<Secret Access Key of the key>
   ```

   > [!TIP]
   > For more information on how to create S3 access keys, see [S3 access keys](https://docs.aws.amazon.com/general/latest/gr/aws-sec-cred-types.html#access-keys-and-secret-access-keys).

## <a id="mount"></a> Mount the remote HDFS storage

Now that you have prepared a credential file with access keys, you can start mounting. The following steps mount the remote HDFS storage in S3 to the local HDFS storage of your big data cluster.

1. Use **kubectl** to find the IP Address for the endpoint **controller-svc-external** service in your big data cluster. Look for the **External-IP**.

   ```bash
   kubectl get svc controller-svc-external -n <your-big-data-cluster-name>
   ```

1. Log in with **azdata** using the external IP address of the controller endpoint with your cluster username and password:

   ```bash
   azdata login -e https://<IP-of-controller-svc-external>:30080/
   ```
   
1. Set environment variable MOUNT_CREDENTIALS following the instructions above

1. Mount the remote HDFS storage in Azure using **azdata bdc hdfs mount create**. Replace the placeholder values before running the following command:

   ```bash
   azdata bdc hdfs mount create --remote-uri s3a://<S3 bucket name> --mount-path /mounts/<mount-name>
   ```

   > [!NOTE]
   > The mount create command is asynchronous. At this time, there is no message indicating whether the mount succeeded. See the [status](#status) section to check the status of your mounts.

If mounted successfully, you should be able to query the HDFS data and run Spark jobs against it. It will appear in the HDFS for your big data cluster in the location specified by `--mount-path`.

## <a id="status"></a> Get the status of mounts

To list the status of all mounts in your big data cluster, use the following command:

```bash
azdata bdc hdfs mount status
```

To list the status of a mount at a specific path in HDFS, use the following command:

```bash
azdata bdc hdfs mount status --mount-path <mount-path-in-hdfs>
```

## Refresh a mount

The following example refreshes the mount.

```bash
azdata bdc hdfs mount refresh --mount-path <mount-path-in-hdfs>
```

## <a id="delete"></a> Delete the mount

To delete the mount, use the **azdata bdc hdfs mount delete** command, and specify the mount path in HDFS:

```bash
azdata bdc hdfs mount delete --mount-path <mount-path-in-hdfs>
```

## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)], see [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md).
