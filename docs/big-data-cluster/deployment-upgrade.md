---
title: Upgrade to a new release
titleSuffix: SQL Server Big Data Clusters
description: Learn how to upgrade SQL Server Big Data Clusters to a new release.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 09/21/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.custom: event-tier1-build-2022
---

# How to upgrade [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

The upgrade path depends on the current version of SQL Server Big Data Cluster. To upgrade from a supported release, including general distribution release (GDR), cumulative update (CU), or quick fix engineering (QFE) update, you can upgrade in place. In-place upgrade from a community technology preview (CTP) or release candidate version of BDC is not supported. You need to remove and recreate the cluster. The following sections describe the steps for each scenario:

- [Upgrade from supported release](#upgrade-from-supported-release)
- [Update a BDC deployment from CTP or release candidate](#update-a-bdc-deployment-from-ctp-or-release-candidate)

>[!NOTE]
>The oldest currently supported release of Big Data Clusters is SQL Server 2019 CU8.

## Upgrade release notes

Before you proceed, check the [upgrade release notes for known issues](release-notes-big-data-cluster.md#known-issues).

> [!WARNING]
> The parameter ```imagePullPolicy``` was required to be set as ```"Always"``` in the deployment profile control.json file when the cluster was initially deployed. This parameter can't be changed after deployment.
> In the case that it is set with a different value, unexpected results may happen during the upgrade process and a cluster redeployment will be required.

## Upgrade from supported release

This section explains how to upgrade a SQL Server BDC from a supported release (starting with SQL Server 2019 GDR1) to a newer supported release.

1. Verify no active Livy sessions.

   Make sure no active Livy sessions or batch jobs are running in Azure Data Studio. An easy way to confirm this is either through `curl` command or a browser to request these URLs:

    - `<your-gateway-endpoint>/gateway/default/livy/v1/sessions`
    - `<your-gateway-endpoint>/gateway/default/livy/v1/batches`

1. Back up SQL Server master instance.

1. Back up HDFS.

   ```
   azdata bdc hdfs cp --from-path <path> --to-path <path>
   ```
   
   For example: 

   ```
   azdata bdc hdfs cp --from-path hdfs://user/hive/warehouse/%%D --to-path ./%%D
   ```

1. Update [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)].

   Follow the instructions for installing [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)]. 
   - [Windows installer](../azdata/install/deploy-install-azdata-installer.md)
   - [Linux with apt](../azdata/install/deploy-install-azdata-linux-package.md)
   - [Linux with yum](../azdata/install/deploy-install-azdata-yum.md)
   - [Linux with zypper](../azdata/install/deploy-install-azdata-zypper.md)

   >[!NOTE]
   >If [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] was installed with `pip` you need to manually remove it before installing with the Windows installer or the Linux package manager.

1. Update the Big Data Cluster.

   ```
   azdata bdc upgrade -n <clusterName> -t <imageTag> -r <containerRegistry>/<containerRepository>
   ```

   For example, the following script uses `2019-CU6-ubuntu-16.04` image tag:

   ```
   azdata bdc upgrade -n bdc -t 2019-CU6-ubuntu-16.04 -r mcr.microsoft.com/mssql/bdc
   ```

>[!NOTE]
>The latest image tags are available at [SQL Server 2019 Big Data Clusters release notes](release-notes-big-data-cluster.md).

>[!IMPORTANT]
>If you use a private repository to pre-pull the images for deploying or upgrading BDC, ensure that the current build images as well as >the target build images are in the private repository. This enables successful rollback, if necessary. Also, if you changed the >credentials of the private repository since the original deployment, update the corresponding environment variables DOCKER_PASSWORD and >DOCKER_USERNAME. Upgrading using different private repositories for current and target builds is not supported.

### Increase the timeout for the upgrade

A timeout can occur if certain components are not upgraded in the allocated time. The following code shows what the failure might look like:

   ```
   >azdata.EXE bdc upgrade --name <mssql-cluster>
   Upgrading cluster to version 15.0.4003

   NOTE: Cluster upgrade can take a significant amount of time depending on
   configuration, network speed, and the number of nodes in the cluster.

   Upgrading Control Plane.
   Control plane upgrade failed. Failed to upgrade controller.
   ```

To increase the timeouts for an upgrade, use **--controller-timeout** and **--component-timeout** parameters to specify higher values when you issue the upgrade. This option is only available starting with SQL Server 2019 CU2 release. For example:

   ```bash
   azdata bdc upgrade -t 2019-CU6-ubuntu-16.04 --controller-timeout=40 --component-timeout=40 --stability-threshold=3
   ```
**--controller-timeout** designates the number of minutes to wait for the controller or controller db to finish upgrading.
**--component-timeout** designates the amount of time that each subsequent phase of the upgrade has to complete.

To increase the timeouts for an upgrade before the SQL Server 2019 CU2 release, edit the upgrade config map. To edit the upgrade config map:

Run the following command:

   ```bash
   kubectl edit configmap controller-upgrade-configmap
   ```

Edit the following fields:

   **controllerUpgradeTimeoutInMinutes** Designates the number of minutes to wait for the controller or controller db to finish upgrading. Default is 5. Update to at least 20.
   **totalUpgradeTimeoutInMinutes**: Designates the combined amount of time for both the controller and controller db to finish upgrading (controller + controller db upgrade).Default is 10. Update to at least 40.
   **componentUpgradeTimeoutInMinutes**: Designates the amount of time that each subsequent phase of the upgrade has to complete. Default is 30. Update to 45.

Save and exit.

## Update a BDC deployment from CTP or release candidate

In-place upgrade from a CTP or release candidate build of SQL Server Big Data Clusters is not supported. The following section explains how to manually remove and recreate the cluster.

### Backup and delete the old cluster

There is no in place upgrade for big data clusters deployed before SQL Server 2019 GDR1 release. The only way to upgrade to a new release is to manually remove and recreate the cluster. Each release has a unique version of [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] that is not compatible with the previous version. Also, if a newer container image is downloaded on cluster deployed with different older version, the latest image might not be compatible with the older images on the cluster. The newer image is pulled if you are using the `latest` image tag for in the deployment configuration file for the container settings. By default, each release has a specific image tag corresponding to the SQL Server release version. To upgrade to the latest release, use the following steps:

1. Before deleting the old cluster, back up the data on the SQL Server master instance and on HDFS. For the SQL Server master instance, you can use [SQL Server backup and restore](data-ingestion-restore-database.md). For HDFS, you [can copy out the data with `curl`](data-ingestion-curl.md).

1. Delete the old cluster with the `azdata delete cluster` command.

   ```bash
    azdata bdc delete --name <old-cluster-name>
   ```

   > [!Important]
   > Use the version of [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] that matches your cluster. Do not delete an older cluster with the newer version of [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)].

   > [!Note]
   > Issuing a `azdata bdc delete` command will result in all objects created within the namespace identified with the big data cluster name to be deleted, but not the namespace itself. Namespace can be reused for subsequent deployments as long as it is empty and no other applications were created within.

1. Uninstall the old version of [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)].

   ```powershell
   pip3 uninstall -r https://azdatacli.blob.core.windows.net/python/azdata/2019-rc1/requirements.txt
   ```

1. Install the latest version of [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)]. The following commands install [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] from the latest release:

   **Windows:**

   ```powershell
   pip3 install -r https://aka.ms/azdata
   ```

   **Linux:**

   ```bash
   pip3 install -r https://aka.ms/azdata --user
   ```

   > [!IMPORTANT]
   > For each release, the path to the `n-1` version of [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] changes. Even if you previously installed [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)], you must reinstall from the latest path before creating the new cluster.

### <a id="azdataversion"></a> Verify the azdata version

Before deploying a new big data cluster, verify that you are using the latest version of [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] with the `--version` parameter:

```bash
azdata --version
```

### Install the new release

After removing the previous big data cluster and installing the latest [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)], deploy the new big data cluster by using the current deployment instructions. For more information, see [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md). Then, restore any required databases or files.

## Next steps

For more information about big data clusters, see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
