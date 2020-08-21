---
title: SQL Server Big Data Clusters release notes
titleSuffix: SQL Server big data clusters
description: This article describes the latest updates and known issues for SQL Server Big Data Clusters. 
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 08/28/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# SQL Server 2019 Big Data Clusters release notes

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

The following release notes apply to [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]. This article is broken into sections for each release. Each release has a link to a support article describing the CU changes as well as links to the Linux package downloads. The article also lists [known issues](#known-issues) for the most recent releases of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] (BDC).

## Supported platforms

This section explains platforms that are supported with BDC.

### Kubernetes platforms

|Platform|Supported versions|
|---------|---------|
|Vanilla (upstream) Kubernetes|Deploy BDC on premises using a Kubernetes cluster version minimum 1.13. See [Kubernetes version and version skew support policy](https://kubernetes.io/docs/setup/release/version-skew-policy/).|
|Red Hat OpenShift|Deploy BDC on premises using an OpenShift cluster version minimum 4.3. See [Red Hat OpenShift Container Platform Life Cycle Policy](https://access.redhat.com/support/policy/updates/openshift).<br><br> Support introduced in SQL Server 2019 CU5.|
|Azure Kubernetes Service (AKS)|Deploy BDC on AKS cluster version minimum 1.13.<br/>See [Supported Kubernetes versions in AKS](/azure/aks/supported-kubernetes-versions) for version support policy.|
|Azure Red Hat OpenShift (ARO)|Deploy BDC on ARO version minimum 4.3. See [Azure Red Hat OpenShift](/azure/openshift/). <br><br> Support introduced in SQL Server 2019 CU5.|

### Host OS for Kubernetes

|Platform|Host OS|Supported versions|
|---------|---------|
|Kubernetes|Ubuntu|16.04|
|Kubernetes|Red Hat Enterprise Linux|7.3, 7.4, 7.5, 7.6|
|OpenShift|Red Hat Enterprise Linux / CoreOS |See [OpenShift release notes](https://docs.openshift.com/container-platform/4.3/release_notes/ocp-4-3-release-notes.html#ocp-4-3-about-this-release)|

### SQL Server Editions

|Edition|Notes|
|---------|---------|
|Enterprise<br/>Standard<br/>Developer| Big Data Cluster edition is determined by the edition of SQL Server master instance. At deployment time Developer edition is deployed by default. You can change the edition after deployment. See [Configure SQL Server master instance](../big-data-cluster/configure-sql-server-master-instance.md). |

## Tools

|Platform|Supported versions|
|---------|---------|
|`azdata`|As a best practice, use the latest version available. Starting with SQL Server 2019 CU5 release, `azdata` has an independent semantic version from the server. <br/><br/>Run `azdata –-version` to validate the version.<br/><br/>See [Release history](#release-history) for latest version.|
|Azure Data Studio|Get the latest build of [Azure Data Studio](https://aka.ms/getazuredatastudio).|

For a complete list, see [Which tools are required?](deploy-big-data-tools.md#which-tools-are-required)

## Release history

The following table lists the release history for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)].

| Release          | BDC Version    | `azdata` Version| Release date |
|------------------|----------------|-----------------|--------------|
| [CU7](#cu7)      | 15.0.4063.15   | 20.0.1          | 2020-08-28   |
| [CU6](#cu6)      | 15.0.4053.23   | 20.0.1          | 2020-08-04   |
| [CU5](#cu5)      | 15.0.4043.16   | 20.0.0          | 2020-06-22   |
| [CU4](#cu4)      | 15.0.4033.1    | 15.0.4033       | 2020-03-31   |
| [CU3](#cu3)      | 15.0.4023.6    | 15.0.4023       | 2020-03-12   |
| [CU2](#cu2)      | 15.0.4013.40   | 15.0.4013       | 2020-02-13   |
| [CU1](#cu1)      | 15.0.4003.23   | 15.0.4003       | 2020-01-07   |
| [GDR1](#rtm)     | 15.0.2070.34   | 15.0.2070       | 2019-11-04   |

## How to install updates

To install updates, see [How to upgrade [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](deployment-upgrade.md).

## <a id="cu6"></a> CU7 (August 2020)

Cumulative Update 7 (CU7) release for SQL Server 2019.

|Package version | Image tag |
|-----|-----|
|15.0.4063.15 |[2019-CU7-ubuntu-16.04]

Before you start to upgrade to CU7, empty Livy jobs in the big data cluster.

## <a id="cu6"></a> CU6 (July 2020)

Cumulative Update 6 (CU6) release for SQL Server 2019.

|Package version | Image tag |
|-----|-----|
|15.0.4053.23 |[2019-CU6-ubuntu-16.04]

This release includes minor fixes and enhancements. The following articles include information related to these updates:

- [Manage big data cluster access in Active Directory mode](manage-user-access.md)
- [Deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in Active Directory mode](deploy-active-directory.md)
- [Deploy SQL Server Big Data Cluster with high availability](deployment-high-availability.md)
- [Configure a SQL Server Big Data Cluster](configure-cluster.md)
- [Configure Apache Spark and Apache Hadoop in Big Data Clusters](configure-spark-hdfs.md)
- [SQL Server master instance configuration properties.](reference-config-master-instance.md)
- [Apache Spark & Apache Hadoop (HDFS) configuration properties](reference-config-spark-hadoop.md)
- [Kubernetes RBAC model & impact on users and service accounts managing BDC](kubernetes-rbac.md)

## <a id="cu5"></a> CU5 (June 2020)

Cumulative Update 5 (CU5) release for SQL Server 2019.

|Package version | Image tag |
|-----|-----|
|15.0.4043.16 |[2019-CU5-ubuntu-16.04]

### Added capabilities

- Support for Big Data Clusters deployment on Red Hat OpenShift. Support includes OpenShift container platform deployed on premises version 4.3 and up and Azure Red Hat OpenShift. See [Deploy SQL Server Big Data Clusters on OpenShift](deploy-openshift.md)
- Updated the BDC deployment security model so privileged containers deployed as part of BDC are no longer *required*. In addition to non-privileged, containers are running as non-root user by default for all new deployments using SQL Server 2019 CU5. 
- Added support for deploying multiple big data clusters against an Active Directory domain.
- `azdata` CLI has its own semantic version, independent from the server. Any dependency between the client and the server version of azdata is removed. We recommend using the latest version for both client and server to ensure you are benefiting from latest enhancements and fixes.
- Introduced two new stored procedures,  sp_data_source_objects and sp_data_source_table_columns, to support introspection of certain External Data Sources. They can be used by customers directly via T-SQL for schema discovery and to see what tables are available to be virtualized. We leverage these changes in the External Table Wizard of the [Data Virtualization Extension](../azure-data-studio/data-virtualization-extension.md) for  Azure Data Studio, which allows you to create external tables from SQL Server, Oracle, MongoDB, and Teradata.
- Added support to persist customizations performed in Grafana. Before CU5 customers would notice that any edits in Grafana configurations would be lost upon `metricsui` pod (that hosts Grafana dashboard) restart. This issue is fixed and all configurations are now persisted. 
- Fixed security issue related to the API used to collect pod and node metrics using Telegraf (hosted in the `metricsdc` pods). As a result of this change, Telegraf now requires a service account, cluster role and cluster bindings to have the necessary permissions to collect the pod and node metrics. See [Custer role required for pods and nodes metrics collection](kubernetes-rbac.md#cluster-role-required-for-pods-and-nodes-metrics-collection) for more details.
- Added two feature switches to control the collection of pod and node metrics. In case you are using different solutions for monitoring your Kubernetes infrastructure, you can turn off the built-in metrics collection for pods and host nodes by setting *allowNodeMetricsCollection* and *allowPodMetricsCollection* to false in control.json deployment configuration file. For OpenShift environments, these settings are set to false by default in the built-in deployment profiles, since collecting pod and node metrics required privileged capabilities.

## <a id="cu4"></a> CU4 (April 2020)

Cumulative Update 4 (CU4) release for SQL Server 2019. The SQL Server Database Engine version for this release is 15.0.4033.1.

|Package version | Image tag |
|-----|-----|
|15.0.4033.1 |[2019-CU4-ubuntu-16.04]

## <a id="cu3"></a> CU3 (March 2020)

Cumulative Update 3 (CU3) release for SQL Server 2019. The SQL Server Database Engine version for this release is 15.0.4023.6.

|Package version | Image tag |
|-----|-----|
|15.0.4023.6 |[2019-CU3-ubuntu-16.04]

### Resolved issues

SQL Server 2019 CU3 resolves the following issues from previous releases.

- [Deployment with private repository](#deployment-with-private-repository)
- [Upgrade may fail due to timeout](#upgrade-may-fail-due-to-timeout)

## <a id="cu2"></a> CU2 (February 2020)

Cumulative Update 2 (CU2) release for SQL Server 2019. The SQL Server Database Engine version for this release is 15.0.4013.40.

|Package version | Image tag |
|-----|-----|
|15.0.4013.40 |[2019-CU2-ubuntu-16.04]

## <a id="cu1"></a> CU1 (January 2020)

Cumulative Update 1 (CU1) release for SQL Server 2019. The SQL Server Database Engine version for this release is 15.0.4003.23.

|Package version | Image tag |
|-----|-----|
|15.0.4003.23|[2019-CU1-ubuntu-16.04]

## <a id="rtm"></a> GDR1 (November 2019)

SQL Server 2019 General Distribution Release 1 (GDR1) - introduces general availability for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-nover.md)]. The SQL Server Database Engine version for this release is 15.0.2070.34.

|Package version | Image tag |
|-----|-----|
|15.0.2070.34|[2019-GDR1-ubuntu-16.04]

[!INCLUDE [sql-server-servicing-updates-version-15](../includes/sql-server-servicing-updates-version-15.md)]

## Known issues

### Big data cluster generated service accounts passwords expiration

- **Affected releases**: All big data cluster deployments with Active Directory integration, irrespective of the release

- **Issue and customer impact**: During big data cluster deployment, the workflow generates a set of [service accounts](active-directory-objects.md).Depending on the password expiration policy set in the Domain Controller, passwords for these accounts can expire (default is 42 days). At this time, there is no mechanism to rotate credentials for all accounts in BDC, so the cluster will become inoperable once the expiration period is met.

- **Workaround**: Update the expiration policy for the BDC service accounts to “Password never expires” in the Domain Controller. For a complete list of these accounts see [Auto generated Active Directory objects](active-directory-objects.md). This action can be done before or after the expiration time. In the latter case, Active Directory will reactivate the expired passwords.

### Credentials for accessing services through gateway endpoint

- **Affected releases**: New clusters deployed starting with CU5.

- **Issue and customer impact**: For new big data clusters deployed using SQL Server 2019 CU5, gateway username is not **root**. If the application used to connect to gateway endpoint is using the wrong credentials, you will see an authentication error. This change is a result of running applications within the big data cluster as non-root user (a new default behavior starting with SQL Server 2019 CU5 release, when you deploy a new big data cluster using CU5, the username for the gateway endpoint is based on the value passed through **AZDATA_USERNAME** environment variable. It is the same username used for the controller and SQL Server endpoints. This is only impacting new deployments, existing big data clusters deployed with any of the previous releases will continue to use **root**. There is no impact to credentials when the cluster is deployed to use Active Directory authentication. 

- **Workaround**: Azure Data Studio will handle the credentials change transparently for the connection made to gateway to enable HDFS browsing experience in the ObjectExplorer. You must install [latest Azure Data Studio release](../azure-data-studio/download-azure-data-studio.md) that includes the necessary changes that address this use case.
For other scenarios where  you must provide credentials for accessing service through the gateway (e.g. logging in with `azdata`, accessing web dashboards for Spark), you must ensure the correct credentials are used. If you are targeting an existing cluster deployed before CU5 you will continue using **root** username to connect to gateway, even after upgrading the cluster to CU5. If you deploy a new cluster using CU5 build, log in by providing the username corresponding to **AZDATA_USERNAME** environment variable.

### Pods and nodes metrics not being collected

- **Affected releases**: New and existing clusters that are using CU5 images

- **Issue and customer impact**: As a result of a security fix related to the API that `telegraf` was using to collect metrics pod and host node metrics, customers may noticed that the metrics are not being collected. This is possible in both new and existing deployments of BDC (after upgrade to CU5). As a result of the fix, Telegraf now requires a service account with cluster wide role permissions. The deployment attempts to create the necessary service account and cluster role, but if the user deploying the cluster or performing the upgrade does not have sufficient permissions, deployment/upgrade proceeds with a warning and succeeds, but the pod & node metrics will not be collected.

- **Workaround**: You can ask an administrator to create the role and service account (either before or after the deployment/upgrade), and BDC will use them. [This article](kubernetes-rbac.md#cluster-role-required-for-pods-and-nodes-metrics-collection) describes how to create the required artifacts.

### `azdata bdc copy-logs` command failure

- **Affected releases**: `azdata` version *20.0.0*

- **Issue and customer impact**: Implementation of *copy-logs* command is assuming `kubectl` client tool is installed on the client machine from which the command is issued. If you are issuing the command against a BDC cluster installed on OpenShift, from a client where only `oc` tool is installed, you will get an error: *An error occurred while collecting the logs: [WinError 2] The system cannot find the file specified*.

- **Workaround**: Install `kubectl` tool on the same client machine and re-issue the `azdata bdc copy-logs` command. See instructions [here](deploy-big-data-tools.md) how to install `kubectl`.

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

       **`totalUpgradeTimeoutInMinutes`**: Designates the combines amount of time for both the controller and controller db to finish upgrading (`controller` + `controllerdb` upgrade). Default is 10. Update to at least  40.

       **`componentUpgradeTimeoutInMinutes`**: Designates the amount of time that each subsequent phase of the upgrade has to complete. Default is 30. Update to 45.

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
