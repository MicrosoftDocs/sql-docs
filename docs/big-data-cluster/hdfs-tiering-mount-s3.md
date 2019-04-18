---
title: Mount S3 for HDFS tiering
titleSuffix: SQL Server big data clusters
description: This article explains how to configure HDFS tiering to mount an external S3 file system into HDFS on a SQL Server 2019 big data cluster (preview).
author: nelgson
ms.author: negust
ms.reviewer: jroth
manager: craigg
ms.date: 04/15/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# How to mount S3 for HDFS tiering in a big data cluster

The following sections provide an example of how to configure HDFS tiering with an S3 Storage data source.

## Prerequisites

- [Deployed big data cluster](deployment-guidance.md)
- [Big data tools](deploy-big-data-tools.md)
  - **mssqlctl**
  - **kubectl**
- Create and upload data to an S3 bucket 
  - Upload CSV or Parquet files to your S3 bucket. This is the external HDFS data that will be mounted to HDFS in the big data cluster.

## Access keys

1. Open a command-prompt on a client machine that can access your big data cluster.

1. Create a local file named **filename.creds** that contains your S3 account credentials using the following format:

   ```text
    fs.s3a.access.key=<Access Key ID of the key>
    fs.s3a.secret.key=<Secret Access Key of the key>
   ```

   > [!TIP]
   > For more information on how to create S3 access keys (`<s3-access-key>`), see [S3 access keys](https://docs.aws.amazon.com/general/latest/gr/aws-sec-cred-types.html#access-keys-and-secret-access-keys).

## <a id="mount"></a> Mount the remote HDFS storage

Now that you have prepared a credential file with access keys, you can start mounting. The following steps mount the remote HDFS storage in S3 to the local HDFS storage of your big data cluster.

1. Use **kubectl** to find the IP Address for the **mgmtproxy-svc-external** service in your big data cluster. Look for the **External-IP**.

   ```bash
   kubectl get svc mgmtproxy-svc-external -n <your-cluster-name>
   ```

1. Log in with **mssqlctl** using the external IP address of the management proxy endpoint with your cluster username and password:

   ```bash
   mssqlctl login -e https://<IP-of-mgmtproxy-svc-external>:30777/ -u <username> -p <password>
   ```

1. Mount the remote HDFS storage in Azure using **mssqlctl storage mount create**. Replace the placeholder values before running the following command:

   ```bash
   mssqlctl storage mount create --remote-uri s3a://<S3 bucket name> --mount-path /mounts/<mount-name> --credential-file <path-to-s3-credentials>/file.creds
   ```

   > [!NOTE]
   > The mount create command is asynchronous. At this time, there is no message indicating whether the mount succeeded. See the [status](#status) section to check the status of your mounts.

If mounted successfully, you should be able to query the HDFS data and run Spark jobs against it. It will appear in the HDFS for your big data cluster in the location specified by `--mount-path`.

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

## Next steps

For more information about SQL Server 2019 big data clusters, see [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md).
