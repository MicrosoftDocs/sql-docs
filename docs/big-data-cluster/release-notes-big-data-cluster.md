---
title: SQL Server Big Data Clusters release notes
titleSuffix: SQL Server big data clusters
description: This article describes the latest updates and known issues for SQL Server Big Data Clusters. 
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 03/12/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# SQL Server 2019 Big Data Clusters release notes

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following release notes apply to [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]. This article is broken into sections for each release. Each release has a link to a support article describing the CU changes as well as links to the Linux package downloads. The article also lists [known issues](#known-issues) for the most recent releases of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] (BDC).

## Supported platforms

This section explains platforms that are supported with [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] (BDC).

### Kubernetes platforms

|Platform|Supported versions|
|---------|---------|
|Kubernetes|BDC requires Kubernetes version minimum 1.13. See [Kubernetes version and version skew support policy](https://kubernetes.io/docs/setup/release/version-skew-policy/) for Kubernetes version support policy.|
|Azure Kubernetes Service (AKS)|BDC requires AKS version minimum 1.13.<br/>See [Supported Kubernetes versions in AKS](/azure/aks/supported-kubernetes-versions) for version support policy.|

### Host OS for Kubernetes

|Platform|Supported versions|
|---------|---------|
|Red Hat Enterprise Linux|7.3, 7.4, 7.5, 7.6|
|Ubuntu|16.04|

### SQL Server Editions

|Edition|Notes|
|---------|---------|
|Enterprise<br/>Standard<br/>Developer| Big Data Cluster edition is determined by the edition of SQL Server master instance. At deployment time Developer edition is deployed by default. You can change the edition after deployment. See [Configure SQL Server master instance](../big-data-cluster/configure-sql-server-master-instance.md). |

## Tools

|Platform|Supported versions|
|---------|---------|
|`azdata`|Must be same minor version as the server (same as SQL Server master instance).<br/><br/>Run `azdata –-version` to validate the version.<br/><br/>As of SQL Server 2019 CU3, this version is `15.0.4023`.|
|Azure Data Studio|Get the latest build of [Azure Data Studio](https://aka.ms/getazuredatastudio).|

## Release history

The following table lists the release history for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)].

| Release               | Version       | Release date |
|-----------------------|---------------|--------------|
| [CU3](#cu3)           | 15.0.4023.6    | 2020-03-12   |
| [CU2](#cu2)           | 15.0.4013.40    | 2020-02-13   |
| [CU1](#cu1)           | 15.0.4003.23   | 2020-01-07   |
| [GDR1](#rtm)            | 15.0.2070.34  | 2019-11-04   |

## How to install updates

To install updates, see [How to upgrade [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](deployment-upgrade.md).

## <a id="cu3"></a> CU3 (Mar 2020)

Cumulative Update 3 (CU3) release for SQL Server 2019. The SQL Server Database Engine version for this release is 15.0.4023.6.

|Package version | Image tag |
|-----|-----|
|15.0.4023.6 |[2019-CU3-ubuntu-16.04]

### Resolved issues

SQL Server 2019 CU3 resolves the following issues from previous releases.

- [Deployment with private repository](#deployment-with-private-repository)
- [Upgrade may fail due to timeout](#upgrade-may-fail-due-to-timeout)

## <a id="cu2"></a> CU2 (Feb 2020)

Cumulative Update 2 (CU2) release for SQL Server 2019. The SQL Server Database Engine version for this release is 15.0.4013.40.

|Package version | Image tag |
|-----|-----|
|15.0.4013.40 |[2019-CU2-ubuntu-16.04]

## <a id="cu1"></a> CU1 (Jan 2020)

Cumulative Update 1 (CU1) release for SQL Server 2019. The SQL Server Database Engine version for this release is 15.0.4003.23.

|Package version | Image tag |
|-----|-----|
|15.0.4003.23|[2019-CU1-ubuntu-16.04]

## <a id="rtm"></a> GDR1 (Nov 2019)

SQL Server 2019 General Distribution Release 1 (GDR1) - introduces general availability for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-nover.md)]. The SQL Server Database Engine version for this release is 15.0.2070.34.

|Package version | Image tag |
|-----|-----|
|15.0.2070.34|[2019-GDR1-ubuntu-16.04]

[!INCLUDE [sql-server-servicing-updates-version-15](../includes/sql-server-servicing-updates-version-15.md)]

## Known issues

### Deployment with private repository

- **Affected releases**: GDR1, CU1, CU2. Resolved for CU3.

- **Issue and customer impact**: Upgrade from private repository has specific requirements

- **Workaround**: If you use a private repository to pre-pull the images for deploying or upgrading BDC, ensure that the current build images as well as the target build images are in the private repository. This enables successful rollback, if necessary. Also, if you changed the credentials of the  private repository since the original deployment, update the corresponding secret in Kubernetes before you upgrade. `azdata` does not support updating the credentials through `AZDATA_PASSWORD` and `AZDATA_USERNAME` environment variables. Update the secret using [`kubectl edit secrets`](https://kubernetes.io/docs/concepts/configuration/secret/#editing-a-secret). 

Upgrading using different repositories for current and target builds is not supported.

### Upgrade may fail due to timeout

- **Affected releases**: GDR1, CU1, CU2. Resolved for CU 3.

- **Issue and customer impact**: An upgrade may fail due to timeout.

   The following code shows what the failure might look like:

   ```
   >azdata.EXE bdc upgrade --name <mssql-cluster>
   Upgrading cluster to version 15.0.4003

   NOTE: Cluster upgrade can take a significant amount of time depending on
   configuration, network speed, and the number of nodes in the cluster.

   Upgrading Control Plane.
   Control plane upgrade failed. Failed to upgrade controller.
   ```

   This error is more likely to occur when you upgrade BDC in Azure Kubernetes Service (AKS).

- **Workaround**: Increase the timeout for the upgrade. 

   To increase the timeouts for an upgrade, edit the upgrade config map. To edit the upgrade config map:

   1. Run the following command:

      ```bash
      kubectl edit configmap controller-upgrade-configmap
      ```

   2. Edit the following fields:

       **`controllerUpgradeTimeoutInMinutes`** Designates the number of minutes to wait for the controller or controller db to finish upgrading. Default is 5. Update to at least 20.

       **`totalUpgradeTimeoutInMinutes`**: Designates the combines amount of time for both the controller and controller db to finish upgrading (controller + controllerdb upgrade).Default is 10. Update to at least  40.

       **`componentUpgradeTimeoutInMinutes`**: Designates the amount of time that each subsequent phase of the upgrade has to complete.  Default is 30. Update to 45.

   3. Save and exit

   The python script below is another way to set the timeout:

   ```python
   from kubernetes import client, config
   import json

   def set_upgrade_timeouts(namespace, controller_timeout=20, controller_total_timeout=40, component_timeout=45):
       """ Set the timeouts for upgrades

       The timeout settings are as follows

       controllerUpgradeTimeoutInMinutes: sets the max amount of time for the controller
           or controllerdb to finish upgrading

       totalUpgradeTimeoutInMinutes: sets the max amount of time to wait for both the
           controller and controllerdb to complete their upgrade

       componentUpgradeTimeoutInMinutes: sets the max amount of time allowed for
           subsequent phases of the upgrade to complete
       """
       config.load_kube_config()

       upgrade_config_map = client.CoreV1Api().read_namespaced_config_map("controller-upgrade-configmap", namespace)

       upgrade_config = json.loads(upgrade_config_map.data["controller-upgrade"])

       upgrade_config["controllerUpgradeTimeoutInMinutes"] = controller_timeout

       upgrade_config["totalUpgradeTimeoutInMinutes"] = controller_total_timeout

       upgrade_config["componentUpgradeTimeoutInMinutes"] = component_timeout

       upgrade_config_map.data["controller-upgrade"] = json.dumps(upgrade_config)

       client.CoreV1Api().patch_namespaced_config_map("controller-upgrade-configmap", namespace, upgrade_config_map)
   ```

### Livy job submission from Azure Data Studio (ADS) or curl fail with 500 error

- **Issue and customer impact**: In an HA configuration, Spark shared resources `sparkhead` are configured with multiple replicas. In this case, you might experience failures with Livy job submission from Azure Data Studio (ADS) or `curl`. To verify, `curl` to any `sparkhead` pod results in refused connection. For example, `curl https://sparkhead-0:8998/` or `curl https://sparkhead-1:8998` returns 500 error.

   This happens in the following scenarios:

   - Zookeeper pods, or processes for each zookeeper instance, are restarted a few times.
   - When networking connectivity is unreliable between `sparkhead` pod and Zookeeper pods.

- **Workaround**: Restarting both Livy servers.

   ```bash
   kubectl -n <clustername> exec sparkhead-0 -c hadoop-livy-sparkhistory supervisorctl restart livy
   ```

   ```bash
   kubectl -n <clustername> exec sparkhead-1 -c hadoop-livy-sparkhistory supervisorctl restart livy
   ```

### Create memory optimized table when master instance in an availability group

- **Issue and customer impact**: You cannot use the primary endpoint exposed for connecting to availability group databases (listener) to create memory optimized tables.

- **Workaround**: To create memory optimized tables when SQL Server master instance is an availability group configuration, [connect to the SQL Server instance](deployment-high-availability.md#instance-connect), expose an endpoint, connect to the SQL Server database, and create the memory optimized tables in the session created with the new connection.

### Insert to external tables Active Directory authentication mode

- **Issue and customer impact**: When SQL Server master instance is in Active Directory authentication mode, a query that selects only from external tables, where at least one of the external tables is in a storage pool, and inserts into another external table, the query returns:

   ```
   Msg 7320, Level 16, State 102, Line 1
   Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "SQLNCLI11". Only domain logins can be used to query Kerberized storage pool.
   ```

- **Workaround**: Modify the query in one of the following ways. Either join the storage pool table to a local table, or insert into the local table first, then read from the local table to insert into the data pool.

### Transparent Data Encryption capabilities can not be used with databases that are part of the availability group in the SQL Server master instance

- **Issue and customer impact**: In an HA configuration, databases that have encryption enabled can't be used after a failover since the master key used for encryption is different on each replica. 

- **Workaround**: There is no workaround for this issue. We recommend to not enable encryption in this configuration until a fix is in place.

## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
