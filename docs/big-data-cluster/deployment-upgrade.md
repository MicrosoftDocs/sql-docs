---
title: Upgrade to a new release
titleSuffix: SQL Server Big Data Clusters
description: Learn how to upgrade SQL Server Big Data Clusters to a new release.
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 11/04/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# How to upgrade [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article provides guidance on how to upgrade a SQL Server big data cluster to a new release. The steps in this article specifically apply to how to upgrade from a preview release to SQL Server 2019 service update release.

## Backup and delete the old cluster

Currently, there is no in place upgrade for big data clusters, the only way to upgrade to a new release is to manually remove and recreate the cluster. Each release has a unique version of `azdata` that is not compatible with the previous version. Also, if an older cluster had to download a container image on a new node, the latest image might not be compatible with the older images on the cluster. Note that the newer image is pulled only if you are using the `latest` image tag for in the deployment configuration file for the container settings. By default, each release has a specific image tag corresponding to the SQl Server release version. To upgrade to the latest release, use the following steps:

1. Before deleting the old cluster, back up the data on the SQL Server master instance and on HDFS. For the SQL Server master instance, you can use [SQL Server backup and restore](data-ingestion-restore-database.md). For HDFS, you [can copy out the data with `curl`](data-ingestion-curl.md).

1. Delete the old cluster with the `azdata delete cluster` command.

   ```bash
    azdata bdc delete --name <old-cluster-name>
   ```

   > [!Important]
   > Use the version of `azdata` that matches your cluster. Do not delete an older cluster with the newer version of `azdata`.

   > [!Note]
   > Issuing a `azdata bdc delete` command will result in all objects created within the namespace identified with the big data cluster name to be deleted, but not the namespace itself. Namespace can be reused for subsequent deployments as long as it is empty and no other applications were created within.

1. Uninstall the old version of `azdata`

   ```powershell
   pip3 uninstall -r https://azdatacli.blob.core.windows.net/python/azdata/2019-rc1/requirements.txt
   ```

1. Install the latest version of `azdata`. The following commands install `azdata` from the latest release:

   **Windows:**

   ```powershell
   pip3 install -r https://aka.ms/azdata
   ```

   **Linux:**

   ```bash
   pip3 install -r https://aka.ms/azdata --user
   ```

   > [!IMPORTANT]
   > For each release, the path to the `n-1` version of `azdata` changes. Even if you previously installed `azdata`, you must reinstall from the latest path before creating the new cluster.

## <a id="azdataversion"></a> Verify the azdata version

Before deploying a new big data cluster, verify that you are using the latest version of `azdata` with the `--version` parameter:

```bash
azdata --version
```

## Install the new release

After removing the previous big data cluster and installing the latest `azdata`, deploy the new big data cluster by using the current deployment instructions. For more information, see [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md). Then, restore any required databases or files.

## Next steps

For more information about big data clusters, see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
