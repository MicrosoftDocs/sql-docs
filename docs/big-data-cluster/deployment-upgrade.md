---
title: Upgrade to a new release
titleSuffix: SQL Server big data clusters
description: Learn how to upgrade SQL Server 2019 big data clusters (preview) to a new release.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 04/24/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# How to upgrade SQL Server big data clusters

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article provides guidance on how to upgrade a SQL Server 2019 big data cluster to a new release. The steps in this article specifically apply to how to upgrade between preview releases.

## Backup and delete the old cluster

Currently, the only way to upgrade a big data cluster to a new release is to manually remove and recreate the cluster. Each release has a unique version of **mssqlctl** that is not compatible with the previous version. Also, if an older cluster had to download an image on a new node, the latest image might not be compatible with the older images on the cluster. To upgrade to the latest release, use the following steps:

1. Before deleting the old cluster, back up the data on the SQL Server master instance and on HDFS. For the SQL Server master instance, you can use [SQL Server backup and restore](data-ingestion-restore-database.md). For HDFS, you [can copy out the data with **curl**](data-ingestion-curl.md).

1. Delete the old cluster with the `mssqlctl delete cluster` command.

   ```bash
    mssqlctl cluster delete --name <old-cluster-name>
   ```

   > [!Important]
   > Use the version of **mssqlctl** that matches your cluster. Do not delete an older cluster with the newer version of **mssqlctl**.

1. If you have any previous releases of **mssqlctl** installed, it is important to uninstall **mssqlctl** first before installing the latest version.

   If you are uninstalling **mssqlctl** corresponding to CTP 2.2 or lower run:

   ```powershell
   pip3 uninstall mssqlctl
   ```

   For CTP 2.3 or higher, run the following command. Replace `ctp-2.4` in the command with the version of **mssqlctl** that you are uninstalling:

   ```powershell
   pip3 uninstall -r  https://private-repo.microsoft.com/python/ctp-2.4/mssqlctl/requirements.txt
   ```

1. Install the latest version of **mssqlctl**. The following commands install **mssqlctl** for CTP 2.5:

   **Windows:**

   ```powershell
   pip3 install -r  https://private-repo.microsoft.com/python/ctp-2.5/mssqlctl/requirements.txt
   ```

   **Linux:**

   ```bash
   pip3 install -r  https://private-repo.microsoft.com/python/ctp-2.5/mssqlctl/requirements.txt --user
   ```

   > [!IMPORTANT]
   > For each release, the path to **mssqlctl** changes. Even if you previously installed **mssqlctl**, you must reinstall from the latest path before creating the new cluster.

## <a id="mssqlctlversion"></a> Verify the mssqlctl version

Before deploying a new big data cluster, verify that you are using the latest version of **mssqlctl** with the `--version` parameter:

```bash
mssqlctl --version
```

## Install the new release

After removing the previous big data cluster and installing the latest **mssqlctl**, deploy the new big data cluster by using the current deployment instructions. For more information, see [How to deploy SQL Server big data clusters on Kubernetes](deployment-guidance.md). Then restore any required databases or files.

## Next steps

For more information about big data clusters, see [What are SQL Server big data clusters](big-data-cluster-overview.md).