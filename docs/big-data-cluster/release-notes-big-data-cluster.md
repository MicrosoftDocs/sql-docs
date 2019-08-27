---
title: Release notes
titleSuffix: SQL Server big data clusters
description: This article describes the latest updates and known issues for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] (preview). 
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 08/28/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Release notes for SQL Server Big Data Clusters

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article lists the updates and know issues for the most recent releases of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)].

## <a id="rc"></a> Release candidate (August)

The following sections describe the new features and known issues for big data clusters in SQL Server 2019 release candidate.

### What's New

|New feature or update | Details |
|:---|:---|
|SQL Server Always On Availability Group |The release candidate creates an availability group to provide:<br/><br/> High availability <br/><br/>Read-scale out <br/><br/>Scale-out data insertion into data pool |
|`azdata` |Simplified installation for the tool with [installation manager](./deploy-install-azdata-installer.md)<br/><br/>Notebook commands<br/><br/>New `azdata bdc` status command experience |
|Azure Data Studio|New SQL Server Big Data Cluster status dashboard. |
| &nbsp; | &nbsp; |

### Known issues

SQL Server 2019 Big Data Clusters release candidate build number is greater than the SQL Server 2019 release candidate build number.

#### Build version

SQL Server 2019 Big Data Clusters has different build

## <a id="ctp32"></a> CTP 3.2 (July)

The following sections describe the new features and known issues for big data clusters in SQL Server 2019 CTP 3.2.

### What's New

|New feature or update | Details |
|:---|:---|
|Public preview |Prior to CTP 3.2, SQL Server big data cluster was available to registered early adopters. This release allows anyone to experience the features of SQL Server Big data clusters. <br/><br/> See [Get started with [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](deploy-get-started.md).|
|`azdata` |CTP 3.2 introduces `azdata` - a command-line utility written in Python that enables cluster administrators to bootstrap and manage the big data cluster via REST APIs. `azdata` replaces `mssqlctl`. See [Install `azdata`](deploy-install-azdata.md). |
|PolyBase |External table column names are now used for querying SQL Server, Oracle, Teradata, MongoDB, and ODBC data sources. In previous CTP releases, columns in the external data source was bound based on the ordinal position only and the names specified in the EXTERNAL TABLE definition was not used. |
|HDFS tiering refresh |Introducing refresh functionality for HDFS tiering so that an existing mount can be refreshed for the latest snapshot of the remote data. See [HDFS tiering](hdfs-tiering.md) |
|Notebook-based troubleshooting |CTP 3.2 introduces Jupyter notebooks to assist with [deployment](deploy-notebooks.md) and [discovery, diagnosis, and troubleshooting](manage-notebooks.md) for components in a SQL Server big data cluster. |
| &nbsp; | &nbsp; |

### Known issues

The following sections describe the known issues and limitations with this release.

#### PolyBase

- Push-down of TOP clause when the count is > 1000 is not supported in this release. All rows will be read from the remote data source in such cases. (Fixed in release candidate)

- Push-down of colocated joins to external data sources is not supported in this release. For example, push-down of two data pool tables of ROUND_ROBIN distribution type will get the data to SQL Master instance or Compute Pool instance to perform the join operation.

#### Compute Pool

- Big data cluster deployment only supports compute pool with one instance. (Fixed in release candidate)

#### Storage Pool

- Querying parquet files in storage pool does not support certain data types.

- MAP and LIST parent columns, as well as parent columns without an attached type, cannot be queried. It will return an error.

- REPEATED nodes cannot be queried and it might return wrong results.

## <a id="ctp31"></a> CTP 3.1 (June)

The following sections describe the new features and known issues for big data clusters in SQL Server 2019 CTP 3.1.

### What's New

| New feature or update | Details |
|:---|:---|
| `mssqlctl` command changes | `mssqlctl cluster` commands have been renamed to `mssqlctl bdc`. For more information, see the [`mssqlctl` reference](reference-azdata.md). |
| New `mssqlctl` status commands and removal of the Cluster Administration Portal. | The Cluster Administration Portal is removed in this release. New status commands have been added to `mssqlctl` that complement existing monitoring commands. |
| Spark compute pools | Create additional nodes to increase Spark compute power without having to scale up storage. In addition, you can start storage pool nodes that aren't used for Spark. Spark and storage are decoupled. For more information, see [Configure storage without spark](deployment-custom-configuration.md#sparkstorage). |
| MSSQL Spark connector | Support for read/write to data pool external tables. Previous releases supported read/write to MASTER instance tables only. For more information, see [How to read and write to SQL Server from Spark using the MSSQL Spark Connector](spark-mssql-connector.md). |
| Machine Learning using MLeap | [Train an MLeap machine learning model in Spark and score it in SQL Server using the Java language extension](spark-create-machine-learning-model.md). |

### Known issues

The following sections describe the known issues and limitations with this release.

#### HDFS

- If you right-click on a file in HDFS to preview it, you might see the following error:

   `Error previewing file: File exceeds max size of 30MB`

   Currently there is no way to preview files larger than 30 MB in Azure Data Studio.

- Configuration changes to HDFS that involve changes to hdfs-site.xml are not supported.

#### Deployment

- Upgrading a big data data cluster from a previous release is not supported.

   > [!IMPORTANT]
   > You must backup your data and then delete your existing big data cluster (using the previous version of **azdata**) before deploying the latest release. For more information, see [Upgrade to a new release](deployment-upgrade.md).

- After deploying on AKS, you might see the following two warning events from the deployment. Both of these events are known issues, but they do not prevent you from successfully deploying the big data cluster on AKS.

   `Warning  FailedMount: Unable to mount volumes for pod "mssql-storage-pool-default-1_sqlarisaksclus(c83eae70-c81b-11e8-930f-f6b6baeb7348)": timeout expired waiting for volumes to attach or mount for pod "sqlarisaksclus"/"mssql-storage-pool-default-1". list of unmounted volumes=[storage-pool-storage hdfs storage-pool-mlservices-storage hadoop-logs]. list of unattached volumes=[storage-pool-storage hdfs storage-pool-mlservices-storage hadoop-logs storage-pool-java-storage secrets default-token-q9mlx]`

   `Warning  Unhealthy: Readiness probe failed: cat: /tmp/provisioner.done: No such file or directory`

- If a big data cluster deployment fails, the associated namespace is not removed. This could result in an orphaned namespace on the cluster. A workaround is to delete the namespace manually before deploying a cluster with the same name.

#### External tables

- Big data cluster deployment no longer creates the **SqlDataPool** and **SqlStoragePool** external data sources. You can create these data sources manually to support data virtualization to the data pool and storage pool.

   > [!NOTE]
   > The URI for creating these external data sources is different between CTPs. Please see the Transact-SQL commands below to see how to create them 

   ```sql
   -- Create default data sources for SQL Big Data Cluster
   IF NOT EXISTS(SELECT * FROM sys.external_data_sources WHERE name = 'SqlDataPool')
       CREATE EXTERNAL DATA SOURCE SqlDataPool
       WITH (LOCATION = 'sqldatapool://controller-svc/default');
 
   IF NOT EXISTS(SELECT * FROM sys.external_data_sources WHERE name = 'SqlStoragePool')
       CREATE EXTERNAL DATA SOURCE SqlStoragePool
       WITH (LOCATION = 'sqlhdfs://controller-svc/default');
   ```

- It is possible to create a data pool external table for a table that has unsupported column types. If you query the external table, you get a message similar to the following:

   `Msg 7320, Level 16, State 110, Line 44 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 105079; Columns with large object types are not supported for external generic tables.`

- If you query a storage pool external table, you might get an error if the underlying file is being copied into HDFS at the same time.

   `Msg 7320, Level 16, State 110, Line 157 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 110806;A distributed query failed: One or more errors occurred.`

- If you are creating an external table to Oracle that use character data types, the Azure Data Studio virtualization wizard interprets these columns as VARCHAR in the external table definition. This will cause a failure in the external table DDL. Either modify the Oracle schema to use the NVARCHAR2 type, or create EXTERNAL TABLE statements manually and specify NVARCHAR instead of using the wizard.

#### Application deployment

- When calling an R, Python, or MLeap application from the RESTful API, the call times-out in 5 minutes.

#### Spark and notebooks

- POD IP addresses may change in the Kubernetes environment as PODs restarts. In the scenario where the master-pod restarts, the Spark session may fail with `NoRoteToHostException`. This is caused by JVM caches that don't get refreshed with new IP addresses.

- If you have Jupyter already installed and a separate Python on Windows, Spark notebooks might fail. To work around this issue, upgrade Jupyter to the latest version.

- In a notebook, if you click the **Add Text** command, the text cell is added in preview mode rather than edit mode. You can click on the preview icon to toggle to edit mode and edit the cell.

#### Security

- The SA_PASSWORD is part of the environment and discoverable (for example in a cord dump file). You must reset the SA_PASSWORD on the master instance after deployment. This is not a bug but a security step. For more information on how to change the SA_PASSWORD in a Linux container, see [Change the SA password](../linux/quickstart-install-connect-docker.md#sapassword).

- AKS logs may contain SA password for big data cluster deployments.

#### Kibana logs dashboards

- Between Aris CTP 3.0 and 3.1, the Kibana version was upgraded from 6.3.1 to 7.0.1.  This has made the Edge browser incompatible with Kibana. Users will see a blank page when loading the current version of the Kibana dashboards in Edge. See [here]( https://www.elastic.co/support/matrix#matrix_browse) for supported browsers for Kibana.rs 


## <a id="ctp30"></a> CTP 3.0 (May)

The following sections describe the new features and known issues for big data clusters in SQL Server 2019 CTP 3.0.

### What's New

| New feature or update | Details |
|:---|:---|
| **mssqlctl** updates | Several **mssqlctl** [command and parameter updates](reference-azdata.md). This includes an update to the **mssqlctl login** command, which now targets the controller username and endpoint. |
| Storage enhancements | Support for different storage configurations for logs and data. Also, the number of persistent volume claims for a big data cluster has been reduced. |
| Multiple compute pool instances | Support for multiple compute pool instances. |
| New pool behavior and capabilities | The compute pool is now used by default for storage pool and data pool operations in a **ROUND_ROBIN** distribution only. The data pool can now use a new **REPLICATED** distribution type, which means that the same data is present on all the data pool instances. |
| External table improvements | External tables of HADOOP data source type now supports reading rows up to 1 MB in size. External tables (ODBC, storage pool, data pool) now support rows as wide as a SQL Server table. |

### Known issues

The following sections describe the known issues and limitations with this release.

#### HDFS

- Azure Data Studio returns an error when you attempt to create a new folder in HDFS. To enable this functionality, install the insiders build of Azure Data Studio:
  
   - [Windows User Installer - **Insiders build**](https://azuredatastudio-update.azurewebsites.net/latest/win32-x64-user/insider)
   - [Windows System Installer - **Insiders build**](https://azuredatastudio-update.azurewebsites.net/latest/win32-x64/insider)
   - [Windows ZIP - **Insiders build**](https://azuredatastudio-update.azurewebsites.net/latest/win32-x64-archive/insider)
   - [macOS ZIP - **Insiders build**](https://azuredatastudio-update.azurewebsites.net/latest/darwin/insider)
   - [Linux TAR.GZ - **Insiders build**](https://azuredatastudio-update.azurewebsites.net/latest/linux-x64/insider)

- If you right-click on a file in HDFS to preview it, you might see the following error:

   `Error previewing file: File exceeds max size of 30MB`

   Currently there is no way to preview files larger than 30 MB in Azure Data Studio.

- Configuration changes to HDFS that involve changes to hdfs-site.xml are not supported.

#### Deployment

- The previous deployment procedures for GPU-enabled big data clusters are not supported in CTP 3.0. An alternate deployment procedure is being investigated. For now, the article "Deploy a big data cluster with GPU support and run TensorFlow" has been temporarily unpublished to avoid confusion.

- Upgrading a big data data cluster from a previous release is not supported.

   > [!IMPORTANT]
   > You must backup your data and then delete your existing big data cluster (using the previous version of **azdata**) before deploying the latest release. For more information, see [Upgrade to a new release](deployment-upgrade.md).

- After deploying on AKS, you might see the following two warning events from the deployment. Both of these events are known issues, but they do not prevent you from successfully deploying the big data cluster on AKS.

   `Warning  FailedMount: Unable to mount volumes for pod "mssql-storage-pool-default-1_sqlarisaksclus(c83eae70-c81b-11e8-930f-f6b6baeb7348)": timeout expired waiting for volumes to attach or mount for pod "sqlarisaksclus"/"mssql-storage-pool-default-1". list of unmounted volumes=[storage-pool-storage hdfs storage-pool-mlservices-storage hadoop-logs]. list of unattached volumes=[storage-pool-storage hdfs storage-pool-mlservices-storage hadoop-logs storage-pool-java-storage secrets default-token-q9mlx]`

   `Warning  Unhealthy: Readiness probe failed: cat: /tmp/provisioner.done: No such file or directory`

- If a big data cluster deployment fails, the associated namespace is not removed. This could result in an orphaned namespace on the cluster. A workaround is to delete the namespace manually before deploying a cluster with the same name.

#### External tables

- Big data cluster deployment no longer creates the **SqlDataPool** and **SqlStoragePool** external data sources. You can create these data sources manually to support data virtualization to the data pool and storage pool.

   > [!NOTE]
   > The URI for creating these external data sources is different between CTPs. Please see the Transact-SQL commands below to see how to create them 

   ```sql
   -- Create default data sources for SQL Big Data Cluster
   IF NOT EXISTS(SELECT * FROM sys.external_data_sources WHERE name = 'SqlDataPool')
       CREATE EXTERNAL DATA SOURCE SqlDataPool
       WITH (LOCATION = 'sqldatapool://controller-svc/default');
 
   IF NOT EXISTS(SELECT * FROM sys.external_data_sources WHERE name = 'SqlStoragePool')
       CREATE EXTERNAL DATA SOURCE SqlStoragePool
       WITH (LOCATION = 'sqlhdfs://controller-svc/default');
   ```

- It is possible to create a data pool external table for a table that has unsupported column types. If you query the external table, you get a message similar to the following:

   `Msg 7320, Level 16, State 110, Line 44 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 105079; Columns with large object types are not supported for external generic tables.`

- If you query a storage pool external table, you might get an error if the underlying file is being copied into HDFS at the same time.

   `Msg 7320, Level 16, State 110, Line 157 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 110806;A distributed query failed: One or more errors occurred.`

- If you are creating an external table to Oracle that use character data types, the Azure Data Studio virtualization wizard interprets these columns as VARCHAR in the external table definition. This will cause a failure in the external table DDL. Either modify the Oracle schema to use the NVARCHAR2 type, or create EXTERNAL TABLE statements manually and specify NVARCHAR instead of using the wizard.

#### Application deployment

- When calling an R, Python, or MLeap application from the RESTful API, the call times-out in 5 minutes.

#### Spark and notebooks

- POD IP addresses may change in the Kubernetes environment as PODs restarts. In the scenario where the master-pod restarts, the Spark session may fail with `NoRoteToHostException`. This is caused by JVM caches that don't get refreshed with new IP addresses.

- If you have Jupyter already installed and a separate Python on Windows, Spark notebooks might fail. To work around this issue, upgrade Jupyter to the latest version.

- In a notebook, if you click the **Add Text** command, the text cell is added in preview mode rather than edit mode. You can click on the preview icon to toggle to edit mode and edit the cell.

#### Security

- The SA_PASSWORD is part of the environment and discoverable (for example in a cord dump file). You must reset the SA_PASSWORD on the master instance after deployment. This is not a bug but a security step. For more information on how to change the SA_PASSWORD in a Linux container, see [Change the SA password](../linux/quickstart-install-connect-docker.md#sapassword).

- AKS logs may contain SA password for big data cluster deployments.

## <a id="ctp25"></a> CTP 2.5 (April)

The following sections describe the new features and known issues for big data clusters in SQL Server 2019 CTP 2.5.

### What's New

| New feature or update | Details |
|:---|:---|
| Deployment profiles | Use default and customized [deployment configuration JSON files](deployment-guidance.md#configfile) for big data cluster deployments instead of environment variables. |
| Prompted deployments | `azdata cluster create` now prompts for any necessary settings for default deployments. |
| Service endpoint and pod name changes | The following external endpoints have changed names:<br/>&nbsp;&nbsp;&nbsp;- **endpoint-master-pool** => **master-svc-external**<br/>&nbsp;&nbsp;&nbsp;- **endpoint-controller** => **controller-svc-external**<br/>&nbsp;&nbsp;&nbsp;- **endpoint-service-proxy** => **mgmtproxy-svc-external**<br/>&nbsp;&nbsp;&nbsp;- **endpoint-security** => **gateway-svc-external**<br/>&nbsp;&nbsp;&nbsp;- **endpoint-app-service-proxy** => **appproxy-svc-external**|
| **azdata** improvements | Use **azdata** to [list external endpoints](deployment-guidance.md#endpoints) and check the version of **azdata** with the `--version` parameter. |
| Offline install | Guidance for offline big data cluster deployments. |
| HDFS tiering improvements | S3 tiering, mount caching, and OAuth support for ADLS Gen2. |
| New `mssql` Spark-SQL Server connector | |

### Known issues

The following sections describe the known issues and limitations with this release.

#### Deployment

- Upgrading a big data data cluster from a previous release is not supported.

   > [!IMPORTANT]
   > You must backup your data and then delete your existing big data cluster (using the previous version of **azdata**) before deploying the latest release. For more information, see [Upgrade to a new release](deployment-upgrade.md).

- After deploying on AKS, you might see the following two warning events from the deployment. Both of these events are known issues, but they do not prevent you from successfully deploying the big data cluster on AKS.

   `Warning  FailedMount: Unable to mount volumes for pod "mssql-storage-pool-default-1_sqlarisaksclus(c83eae70-c81b-11e8-930f-f6b6baeb7348)": timeout expired waiting for volumes to attach or mount for pod "sqlarisaksclus"/"mssql-storage-pool-default-1". list of unmounted volumes=[storage-pool-storage hdfs storage-pool-mlservices-storage hadoop-logs]. list of unattached volumes=[storage-pool-storage hdfs storage-pool-mlservices-storage hadoop-logs storage-pool-java-storage secrets default-token-q9mlx]`

   `Warning  Unhealthy: Readiness probe failed: cat: /tmp/provisioner.done: No such file or directory`

- If a big data cluster deployment fails, the associated namespace is not removed. This could result in an orphaned namespace on the cluster. A workaround is to delete the namespace manually before deploying a cluster with the same name.

#### External tables

- Big data cluster deployment no longer creates the **SqlDataPool** and **SqlStoragePool** external data sources. You can create these data sources manually to support data virtualization to the data pool and storage pool.

   ```sql
   -- Create default data sources for SQL Big Data Cluster
   IF NOT EXISTS(SELECT * FROM sys.external_data_sources WHERE name = 'SqlDataPool')
       CREATE EXTERNAL DATA SOURCE SqlDataPool
       WITH (LOCATION = 'sqldatapool://service-mssql-controller:8080/datapools/default');
 
   IF NOT EXISTS(SELECT * FROM sys.external_data_sources WHERE name = 'SqlStoragePool')
       CREATE EXTERNAL DATA SOURCE SqlStoragePool
       WITH (LOCATION = 'sqlhdfs://nmnode-0-svc:50070');
   ```

- It is possible to create a data pool external table for a table that has unsupported column types. If you query the external table, you get a message similar to the following:

   `Msg 7320, Level 16, State 110, Line 44 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 105079; Columns with large object types are not supported for external generic tables.`

- If you query a storage pool external table, you might get an error if the underlying file is being copied into HDFS at the same time.

   `Msg 7320, Level 16, State 110, Line 157 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 110806;A distributed query failed: One or more errors occurred.`

- If you are creating an external table to Oracle that use character data types, the Azure Data Studio virtualization wizard interprets these columns as VARCHAR in the external table definition. This will cause a failure in the external table DDL. Either modify the Oracle schema to use the NVARCHAR2 type, or create EXTERNAL TABLE statements manually and specify NVARCHAR instead of using the wizard.

#### Application deployment

- When calling an R, Python, or MLeap application from the RESTful API, the call times-out in 5 minutes.

#### Spark and notebooks

- POD IP addresses may change in the Kubernetes environment as PODs restarts. In the scenario where the master-pod restarts, the Spark session may fail with `NoRoteToHostException`. This is caused by JVM caches that don't get refreshed with new IP addresses.

- If you have Jupyter already installed and a separate Python on Windows, Spark notebooks might fail. To work around this issue, upgrade Jupyter to the latest version.

- In a notebook, if you click the **Add Text** command, the text cell is added in preview mode rather than edit mode. You can click on the preview icon to toggle to edit mode and edit the cell.

#### HDFS

- If you right-click on a file in HDFS to preview it, you might see the following error:

   `Error previewing file: File exceeds max size of 30MB`

   Currently there is no way to preview files larger than 30 MB in Azure Data Studio.

- Configuration changes to HDFS that involve changes to hdfs-site.xml are not supported.

#### Security

- The SA_PASSWORD is part of the environment and discoverable (for example in a cord dump file). You must reset the SA_PASSWORD on the master instance after deployment. This is not a bug but a security step. For more information on how to change the SA_PASSWORD in a Linux container, see [Change the SA password](../linux/quickstart-install-connect-docker.md#sapassword).

- AKS logs may contain SA password for big data cluster deployments.

## <a id="ctp24"></a> CTP 2.4 (March)

The following sections describe the new features and known issues for big data clusters in SQL Server 2019 CTP 2.4.

### What's New

| New feature or update | Details |
|:---|:---|
| Guidance on GPU support for running deep learning with TensorFlow in Spark. | [Deploy a big data cluster with GPU support and run TensorFlow](spark-gpu-tensorflow.md). |
| **SqlDataPool** and **SqlStoragePool** data sources are no longer created by default. | Create these manually as needed. See the [known issues](#externaltablesctp24). |
| `INSERT INTO SELECT` support for the data pool. | For an example, see [Tutorial: Ingest data into a SQL Server data pool with Transact-SQL](tutorial-data-pool-ingest-sql.md). |
| `FORCE SCALEOUTEXECUTION` and `DISABLE SCALEOUTEXECUTION` option. | Forces or disables the use of the compute pool for queries on external tables. For example, `SELECT TOP(100) * FROM web_clickstreams_hdfs_book_clicks OPTION(FORCE SCALEOUTEXECUTION)`. |
| Updated AKS deployment recommendations. | When evaluating big data clusters on AKS, we now recommend using a single node of size **Standard_L8s**. |
| Spark runtime upgrade to Spark 2.4. | |

### Known issues

The following sections describe the known issues and limitations with this release.

#### Deployment

- Upgrading a big data data cluster from a previous release is not supported.

   > [!IMPORTANT]
   > You must backup your data and then delete your existing big data cluster (using the previous version of **azdata**) before deploying the latest release. For more information, see [Upgrade to a new release](deployment-upgrade.md).

- After deploying on AKS, you might see the following two warning events from the deployment. Both of these events are known issues, but they do not prevent you from successfully deploying the big data cluster on AKS.

   `Warning  FailedMount: Unable to mount volumes for pod "mssql-storage-pool-default-1_sqlarisaksclus(c83eae70-c81b-11e8-930f-f6b6baeb7348)": timeout expired waiting for volumes to attach or mount for pod "sqlarisaksclus"/"mssql-storage-pool-default-1". list of unmounted volumes=[storage-pool-storage hdfs storage-pool-mlservices-storage hadoop-logs]. list of unattached volumes=[storage-pool-storage hdfs storage-pool-mlservices-storage hadoop-logs storage-pool-java-storage secrets default-token-q9mlx]`

   `Warning  Unhealthy: Readiness probe failed: cat: /tmp/provisioner.done: No such file or directory`

- If a big data cluster deployment fails, the associated namespace is not removed. This could result in an orphaned namespace on the cluster. A workaround is to delete the namespace manually before deploying a cluster with the same name.

#### kubeadm deployments

If you use kubeadm to deploy Kubernetes on multiple machines, the cluster administration portal does not correctly display the endpoints needed to connect to the big data cluster. If you are experiencing this problem, use the following work around to discover the service endpoint IP addresses:

- If you are connecting from within the cluster, query Kubernetes for the service IP for the endpoint that you want to connect to. For example, the following **kubectl** command displays the IP address of the SQL Server master instance:

   ```bash
   kubectl get service endpoint-master-pool -n <clusterName> -o=custom-columns="IP:.spec.clusterIP,PORT:.spec.ports[*].nodePort"
   ```

- If you are connecting from outside the cluster, use the following steps to connect:

   1. Get the IP address of the node running the SQL Server master instance: `kubectl get pod mssql-master-pool-0 -o jsonpath="Name: {.metadata.name} Status: {.status.hostIP}" -n <clusterName>`.

   1. Connect to SQL Server master instance using this IP address.

   1. Query the **cluster_endpoint_table** in master database for other external endpoints.

      If this fails with a connection timeout, it is possible the respective node is firewalled. In this case, you must contact your Kubernetes cluster administrator and ask for the node IP that is exposed externally. This could be any node. You can then use that IP and the corresponding port to connect to various services running in the cluster. For example, the administrator can find this IP by running:

      ```
      [root@m12hn01 config]# kubectl cluster-info
      Kubernetes master is running at https://172.50.253.99:6443
      KubeDNS is running at https://172.30.243.91:6443/api/v1/namespaces/kube-system/services/kube-dns:dns/proxy
      ```

#### Delete cluster fails

When you attempt to delete a cluster with **azdata**, it fails with the following error:

```
2019-03-26 20:38:11.0614 UTC | INFO | Deleting cluster ...
Error processing command: "TypeError"
delete_namespaced_service() takes 3 positional arguments but 4 were given
Makefile:61: recipe for target 'delete-cluster' failed
make[2]: *** [delete-cluster] Error 1
Makefile:223: recipe for target 'deploy-clean' failed
make[1]: *** [deploy-clean] Error 2
Makefile:203: recipe for target 'deploy-clean' failed
make: *** [deploy-clean] Error 2
```

A new Python Kubernetes client (version 9.0.0) changed the delete namespaces API, which currently breaks **azdata**. This only happens if you have a newer Kubernetes python client installed. You can work around this problem by directly deleting the cluster using **kubectl** (`kubectl delete ns <ClusterName>`), or you can install the older version using `sudo pip install kubernetes==8.0.1`.

#### <a id="externaltablesctp24"></a> External tables

- Big data cluster deployment no longer creates the **SqlDataPool** and **SqlStoragePool** external data sources. You can create these data sources manually to support data virtualization to the data pool and storage pool.

   ```sql
   -- Create default data sources for SQL Big Data Cluster
   IF NOT EXISTS(SELECT * FROM sys.external_data_sources WHERE name = 'SqlDataPool')
       CREATE EXTERNAL DATA SOURCE SqlDataPool
       WITH (LOCATION = 'sqldatapool://service-mssql-controller:8080/datapools/default');
 
   IF NOT EXISTS(SELECT * FROM sys.external_data_sources WHERE name = 'SqlStoragePool')
       CREATE EXTERNAL DATA SOURCE SqlStoragePool
       WITH (LOCATION = 'sqlhdfs://service-master-pool:50070');
   ```

- It is possible to create a data pool external table for a table that has unsupported column types. If you query the external table, you get a message similar to the following:

   `Msg 7320, Level 16, State 110, Line 44 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 105079; Columns with large object types are not supported for external generic tables.`

- If you query a storage pool external table, you might get an error if the underlying file is being copied into HDFS at the same time.

   `Msg 7320, Level 16, State 110, Line 157 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 110806;A distributed query failed: One or more errors occurred.`

- If you are creating an external table to Oracle that use character data types, the Azure Data Studio virtualization wizard interprets these columns as VARCHAR in the external table definition. This will cause a failure in the external table DDL. Either modify the Oracle schema to use the NVARCHAR2 type, or create EXTERNAL TABLE statements manually and specify NVARCHAR instead of using the wizard.

#### Application deployment

- When calling an R, Python, or MLeap application from the RESTful API, the call times-out in 5 minutes.

#### Spark and notebooks

- POD IP addresses may change in the Kubernetes environment as PODs restarts. In the scenario where the master-pod restarts, the Spark session may fail with `NoRoteToHostException`. This is caused by JVM caches that don't get refreshed with new IP addresses.

- If you have Jupyter already installed and a separate Python on Windows, Spark notebooks might fail. To work around this issue, upgrade Jupyter to the latest version.

- In a notebook, if you click the **Add Text** command, the text cell is added in preview mode rather than edit mode. You can click on the preview icon to toggle to edit mode and edit the cell.

#### HDFS

- If you right-click on a file in HDFS to preview it, you might see the following error:

   `Error previewing file: File exceeds max size of 30MB`

   Currently there is no way to preview files larger than 30 MB in Azure Data Studio.

- Configuration changes to HDFS that involve changes to hdfs-site.xml are not supported.

#### Security

- The SA_PASSWORD is part of the environment and discoverable (for example in a cord dump file). You must reset the SA_PASSWORD on the master instance after deployment. This is not a bug but a security step. For more information on how to change the SA_PASSWORD in a Linux container, see [Change the SA password](../linux/quickstart-install-connect-docker.md#sapassword).

- AKS logs may contain SA password for big data cluster deployments.

## <a id="ctp23"></a> CTP 2.3 (February)

The following sections describe the new features and known issues for big data clusters in SQL Server 2019 CTP 2.3.

### What's New

| New feature or update | Details |
| :---------- | :------ |
| Submit Spark jobs on big data clusters in IntelliJ. | [Submit Spark jobs on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in IntelliJ](spark-submit-job-intellij-tool-plugin.md) |
| Common CLI for application deployment and cluster management. | [How to deploy an app on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-create-apps.md) |
| VS Code extension to deploy applications to a big data cluster. | [How to use VS Code to deploy applications to [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](app-deployment-extension.md) |
| Changes to the **azdata** tool command usage. | For more details see the [known issues for azdata](#azdatactp23). |
| Use Sparklyr in big data cluster | [Use Sparklyr in SQL Server 2019 big data cluster](sparklyr-from-RStudio.md) |
| Mount external HDFS-compatible storage into big data cluster with **HDFS tiering**. | See [HDFS tiering](hdfs-tiering.md). |
| New unified connection experience for the SQL Server master instance and the HDFS/Spark Gateway. | See [SQL Server master instance and the HDFS/Spark Gateway](connect-to-big-data-cluster.md). |
| Deleting a cluster with **azdata cluster delete** now deletes only the objects in the namespace that were part of the big data cluster. | The namespace is not deleted. However, in earlier releases this command did delete the entire namespace. |
| _Security_ endpoint names have been changed and consolidated. | **service-security-lb** and **service-security-nodeport** have been consolidated into the **endpoint-security** endpoint. |
| _Proxy_ endpoint names have been changed and consolidated. | **service-proxy-lb** and **service-proxy-nodeport** have been consolidated into the **endpoint-service-proxy** endpoint. |
| _Controller_ endpoint names have been changed and consolidated. | **service-mssql-controller-lb** and **service-mssql-controller-nodeport** have been consolidated into the **endpoint-controller** enpoint. |
| &nbsp; | &nbsp; |

### Known issues

The following sections describe the known issues and limitations with this release.

#### Deployment

- Upgrading a big data data cluster from a previous release is not supported.

   > [!IMPORTANT]
   > You must backup your data and then delete your existing big data cluster (using the previous version of **azdata**) before deploying the latest release. For more information, see [Upgrade to a new release](deployment-upgrade.md).

- The **ACCEPT_EULA** environment variable must be "yes" or "Yes" to accept the EULA. Previous releases permitted "y" and "Y" but these are no longer accepted and will cause the deployment to fail.

- The **CLUSTER_PLATFORM** environment variables does not have a default as it did in previous releases.

- After deploying on AKS, you might see the following two warning events from the deployment. Both of these events are known issues, but they do not prevent you from successfully deploying the big data cluster on AKS.

   `Warning  FailedMount: Unable to mount volumes for pod "mssql-storage-pool-default-1_sqlarisaksclus(c83eae70-c81b-11e8-930f-f6b6baeb7348)": timeout expired waiting for volumes to attach or mount for pod "sqlarisaksclus"/"mssql-storage-pool-default-1". list of unmounted volumes=[storage-pool-storage hdfs storage-pool-mlservices-storage hadoop-logs]. list of unattached volumes=[storage-pool-storage hdfs storage-pool-mlservices-storage hadoop-logs storage-pool-java-storage secrets default-token-q9mlx]`

   `Warning  Unhealthy: Readiness probe failed: cat: /tmp/provisioner.done: No such file or directory`

- If a big data cluster deployment fails, the associated namespace is not removed. This could result in an orphaned namespace on the cluster. A workaround is to delete the namespace manually before deploying a cluster with the same name.

#### kubeadm deployments

If you use kubeadm to deploy Kubernetes on multiple machines, the cluster administration portal does not correctly display the endpoints needed to connect to the big data cluster. If you are experiencing this problem, use the following work around to discover the service endpoint IP addresses:

- If you are connecting from within the cluster, query Kubernetes for the service IP for the endpoint that you want to connect to. For example, the following **kubectl** command displays the IP address of the SQL Server master instance:

   ```bash
   kubectl get service endpoint-master-pool -n <clusterName> -o=custom-columns="IP:.spec.clusterIP,PORT:.spec.ports[*].nodePort"
   ```

- If you are connecting from outside the cluster, use the following steps to connect:

   1. Get the IP address of the node running the SQL Server master instance: `kubectl get pod mssql-master-pool-0 -o jsonpath="Name: {.metadata.name} Status: {.status.hostIP}" -n <clusterName>`.

   1. Connect to SQL Server master instance using this IP address.

   1. Query the **cluster_endpoint_table** in master database for other external endpoints.

      If this fails with a connection timeout, it is possible the respective node is firewalled. In this case, you must contact your Kubernetes cluster administrator and ask for the node IP that is exposed externally. This could be any node. You can then use that IP and the corresponding port to connect to various services running in the cluster. For example, the administrator can find this IP by running:

      ```
      [root@m12hn01 config]# kubectl cluster-info
      Kubernetes master is running at https://172.50.253.99:6443
      KubeDNS is running at https://172.30.243.91:6443/api/v1/namespaces/kube-system/services/kube-dns:dns/proxy
      ```

#### <a id="azdatactp23"></a> azdata

- The **azdata** tool changed from a verb-noun command ordering to a noun-verb order. For example, `azdata create cluster` is now `azdata cluster create`.

- The `--name` parameter is now required when creating a cluster with `azdata cluster create`.

   ```bash
   azdata cluster create --name <cluster_name>
   ```

- For important information about upgrading to the latest version of big data clusters and **azdata**, see [Upgrade to a new release](deployment-upgrade.md).

#### External tables

- It is possible to create a data pool external table for a table that has unsupported column types. If you query the external table, you get a message similar to the following:

   `Msg 7320, Level 16, State 110, Line 44 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 105079; Columns with large object types are not supported for external generic tables.`

- If you query a storage pool external table, you might get an error if the underlying file is being copied into HDFS at the same time.

   `Msg 7320, Level 16, State 110, Line 157 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 110806;A distributed query failed: One or more errors occurred.`

- If you are creating an external table to Oracle that use character data types, the Azure Data Studio virtualization wizard interprets these columns as VARCHAR in the external table definition. This will cause a failure in the external table DDL. Either modify the Oracle schema to use the NVARCHAR2 type, or create EXTERNAL TABLE statements manually and specify NVARCHAR instead of using the wizard.

#### Application deployment

- When calling an R, Python, or MLeap application from the RESTful API, the call times-out in 5 minutes.

#### Spark and notebooks

- POD IP addresses may change in the Kubernetes environment as PODs restarts. In the scenario where the master-pod restarts, the Spark session may fail with `NoRoteToHostException`. This is caused by JVM caches that don't get refreshed with new IP addresses.

- If you have Jupyter already installed and a separate Python on Windows, Spark notebooks might fail. To work around this issue, upgrade Jupyter to the latest version.

- In a notebook, if you click the **Add Text** command, the text cell is added in preview mode rather than edit mode. You can click on the preview icon to toggle to edit mode and edit the cell.

#### HDFS

- If you right-click on a file in HDFS to preview it, you might see the following error:

   `Error previewing file: File exceeds max size of 30MB`

   Currently there is no way to preview files larger than 30 MB in Azure Data Studio.

- Configuration changes to HDFS that involve changes to hdfs-site.xml are not supported.

#### Security

- The SA_PASSWORD is part of the environment and discoverable (for example in a cord dump file). You must reset the SA_PASSWORD on the master instance after deployment. This is not a bug but a security step. For more information on how to change the SA_PASSWORD in a Linux container, see [Change the SA password](../linux/quickstart-install-connect-docker.md#sapassword).

- AKS logs may contain SA password for big data cluster deployments.

## <a id="ctp22"></a> CTP 2.2 (December 2018)

The following sections describe the new features and known issues for big data clusters in SQL Server 2019 CTP 2.2.

### New features

- Cluster Admin Portal accessed with `/portal` (**https://\<ip-address\>:30777/portal**).
- Master pool service name changed from `service-master-pool-lb` and `service-master-pool-nodeport` to `endpoint-master-pool`.
- New version of **azdata** and updated images.
- Miscellaneous bug fixes and improvements.

### Known issues

The following sections describe the known issues and limitations with this release.

#### Deployment

- Upgrading a big data data cluster from a previous release is not supported. You must backup and delete any existing big data cluster before deploying the latest release. For more information, see [Upgrade to a new release](deployment-upgrade.md).

- After deploying on AKS, you might see the following two warning events from the deployment. Both of these events are known issues, but they do not prevent you from successfully deploying the big data cluster on AKS.

   `Warning  FailedMount: Unable to mount volumes for pod "mssql-storage-pool-default-1_sqlarisaksclus(c83eae70-c81b-11e8-930f-f6b6baeb7348)": timeout expired waiting for volumes to attach or mount for pod "sqlarisaksclus"/"mssql-storage-pool-default-1". list of unmounted volumes=[storage-pool-storage hdfs storage-pool-mlservices-storage hadoop-logs]. list of unattached volumes=[storage-pool-storage hdfs storage-pool-mlservices-storage hadoop-logs storage-pool-java-storage secrets default-token-q9mlx]`

   `Warning  Unhealthy: Readiness probe failed: cat: /tmp/provisioner.done: No such file or directory`

- If a big data cluster deployment fails, the associated namespace is not removed. This could result in an orphaned namespace on the cluster. A workaround is to delete the namespace manually before deploying a cluster with the same name.

#### Cluster administration portal

The cluster administration portal does not display the endpoint for the SQL Server master instance. To find the IP address and port for the master instance, use the following **kubectl** command:

```
kubectl get svc endpoint-master-pool -n <your-big-data-cluster-name>
```

#### External tables

- It is possible to create a data pool external table for a table that has unsupported column types. If you query the external table, you get a message similar to the following:

   `Msg 7320, Level 16, State 110, Line 44 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 105079; Columns with large object types are not supported for external generic tables.`

- If you query a storage pool external table, you might get an error if the underlying file is being copied into HDFS at the same time.

   `Msg 7320, Level 16, State 110, Line 157 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 110806;A distributed query failed: One or more errors occurred.`

#### Spark and notebooks

- POD IP addresses may change in the Kubernetes environment as PODs restarts. In the scenario where the master-pod restarts, the Spark session may fail with `NoRoteToHostException`. This is caused by JVM caches that don't get refreshed with new IP addresses.

- If you have Jupyter already installed and a separate Python on Windows, Spark notebooks might fail. To work around this issue, upgrade Jupyter to the latest version.

- In a notebook, if you click the **Add Text** command, the text cell is added in preview mode rather than edit mode. You can click on the preview icon to toggle to edit mode and edit the cell.

#### HDFS

- If you right-click on a file in HDFS to preview it, you might see the following error:

   `Error previewing file: File exceeds max size of 30MB`

   Currently there is no way to preview files larger than 30 MB in Azure Data Studio.

- Configuration changes to HDFS that involve changes to hdfs-site.xml are not supported.

#### Security

- The SA_PASSWORD is part of the environment and discoverable (for example in a cord dump file). You must reset the SA_PASSWORD on the master instance after deployment. This is not a bug but a security step. For more information on how to change the SA_PASSWORD in a Linux container, see [Change the SA password](../linux/quickstart-install-connect-docker.md#sapassword).

- AKS logs may contain SA password for big data cluster deployments.

## <a id="ctp21"></a> CTP 2.1 (November 2018)

The following sections describe the new features and known issues for big data clusters in SQL Server 2019 CTP 2.1.

### New features

- [Deploy Python and R apps](big-data-cluster-create-apps.md) in a big data cluster.
- New version of **azdata** and updated images. 
- Miscellaneous bug fixes and improvements.

### Known issues

The following sections provide known issues for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in CTP 2.1.

#### Deployment

- Upgrading a big data data cluster from a previous release is not supported. You must backup and delete any existing big data cluster before deploying the latest release. For more information, see [Upgrade to a new release](deployment-upgrade.md).

- After deploying on AKS, you might see the following two warning events from the deployment. Both of these events are known issues, but they do not prevent you from successfully deploying the big data cluster on AKS.

   `Warning  FailedMount: Unable to mount volumes for pod "mssql-storage-pool-default-1_sqlarisaksclus(c83eae70-c81b-11e8-930f-f6b6baeb7348)": timeout expired waiting for volumes to attach or mount for pod "sqlarisaksclus"/"mssql-storage-pool-default-1". list of unmounted volumes=[storage-pool-storage hdfs storage-pool-mlservices-storage hadoop-logs]. list of unattached volumes=[storage-pool-storage hdfs storage-pool-mlservices-storage hadoop-logs storage-pool-java-storage secrets default-token-q9mlx]`

   `Warning  Unhealthy: Readiness probe failed: cat: /tmp/provisioner.done: No such file or directory`

- If a big data cluster deployment fails, the associated namespace is not removed. This could result in an orphaned namespace on the cluster. A workaround is to delete the namespace manually before deploying a cluster with the same name.

#### Admin portal

- When you [create an app using msqlctl-ctp command](big-data-cluster-create-apps.md) and deploy it on a SQL Server big data cluster, the Cluster Admin Portal shows the pods where the application was deployed as "Unknown" in the Controller section of the Admin Portion.

#### External tables

- It is possible to create a data pool external table for a table that has unsupported column types. If you query the external table, you get a message similar to the following:

   `Msg 7320, Level 16, State 110, Line 44 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 105079; Columns with large object types are not supported for external generic tables.`

- If you query a storage pool external table, you might get an error if the underlying file is being copied into HDFS at the same time.

   `Msg 7320, Level 16, State 110, Line 157 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 110806;A distributed query failed: One or more errors occurred.`

#### Spark and notebooks

- POD IP addresses may change in the Kubernetes environment as PODs restarts. In the scenario where the master-pod restarts, the Spark session may fail with `NoRoteToHostException`. This is caused by JVM caches that don't get refreshed with new IP addresses.

- If you have Jupyter already installed and a separate Python on Windows, Spark notebooks might fail. To work around this issue, upgrade Jupyter to the latest version.

- In a notebook, if you click the **Add Text** command, the text cell is added in preview mode rather than edit mode. You can click on the preview icon to toggle to edit mode and edit the cell.

#### HDFS

- If you right-click on a file in HDFS to preview it, you might see the following error:

   `Error previewing file: File exceeds max size of 30MB`

   Currently there is no way to preview files larger than 30 MB in Azure Data Studio.

- Configuration changes to HDFS that involve changes to hdfs-site.xml are not supported.

#### Security

- The SA_PASSWORD is part of the environment and discoverable (for example in a cord dump file). You must reset the SA_PASSWORD on the master instance after deployment. This is not a bug but a security step. For more information on how to change the SA_PASSWORD in a Linux container, see [Change the SA password](../linux/quickstart-install-connect-docker.md#sapassword).

- AKS logs may contain SA password for big data cluster deployments.

## <a id="ctp20"></a> CTP 2.0 (October 2018)

The following sections describe the new features and known issues for big data clusters in SQL Server 2019 CTP 2.0.

### New features

- Simple deployment experience using azdata management tool
- Native notebook experience in Azure Data Studio
- Query HDFS files via Storage Instance of SQL Server
- Data virtualization via master to SQL Server, Oracle, MongoDB, and HDFS
- Data virtualization wizard for SQL Server and Oracle in Azure Data Studio
- ML Services on master
- Cluster administration portal that you can use for monitoring and troubleshooting
- Spark job submit in Azure Data Studio 
- Spark UI in the cluster administration portal
- Volume mounting to storage classes
- Queries over data pools from master
- Show plan for distributed queries in SSMS
- Pip package for azdata management tool
- Built-in deployment engine through controller service

### Known issues

The following sections provide known issues for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in CTP 2.0.

#### Deployment

- If you are using Azure Kubernetes Service (AKS), the recommended version of Kubernetes is 1.10.*, which does not support disk resizing. You should make sure you are sizing the storage accordingly at deployment time. For more information on how to adjust storage sizes, see the [Data persistence](concept-data-persistence.md) article. For Kubernetes deployed on VMs, the recommended version is 1.11.

- After deploying on AKS, you might see the following two warning events from the deployment. Both of these events are known issues, but they do not prevent you from successfully deploying the big data cluster on AKS.

   `Warning  FailedMount: Unable to mount volumes for pod "mssql-storage-pool-default-1_sqlarisaksclus(c83eae70-c81b-11e8-930f-f6b6baeb7348)": timeout expired waiting for volumes to attach or mount for pod "sqlarisaksclus"/"mssql-storage-pool-default-1". list of unmounted volumes=[storage-pool-storage hdfs storage-pool-mlservices-storage hadoop-logs]. list of unattached volumes=[storage-pool-storage hdfs storage-pool-mlservices-storage hadoop-logs storage-pool-java-storage secrets default-token-q9mlx]`

   `Warning  Unhealthy: Readiness probe failed: cat: /tmp/provisioner.done: No such file or directory`

- If a big data cluster deployment fails, the associated namespace is not removed. This could result in an orphaned namespace on the cluster. A workaround is to delete the namespace manually before deploying a cluster with the same name.

#### External tables

- It is possible to create a data pool external table for a table that has unsupported column types. If you query the external table, you get a message similar to the following:

   `Msg 7320, Level 16, State 110, Line 44 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 105079; Columns with large object types are not supported for external generic tables.`

- If you query a storage pool external table, you might get an error if the underlying file is being copied into HDFS at the same time.

   `Msg 7320, Level 16, State 110, Line 157 Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "(null)". 110806;A distributed query failed: One or more errors occurred.`

#### Spark and notebooks

- POD IP addresses may change in the Kubernetes environment as PODs restarts. In the scenario where the master-pod restarts, the Spark session may fail with `NoRoteToHostException`. This is caused by JVM caches that don't get refreshed with new IP addresses.

- If you have Jupyter already installed and a separate Python on Windows, Spark notebooks might fail. To work around this issue, upgrade Jupyter to the latest version.

- In a notebook, if you click the **Add Text** command, the text cell is added in preview mode rather than edit mode. You can click on the preview icon to toggle to edit mode and edit the cell.

#### HDFS

- If you right-click on a file in HDFS to preview it, you might see the following error:

   `Error previewing file: File exceeds max size of 30MB`

   Currently there is no way to preview files larger than 30 MB in Azure Data Studio.

- Configuration changes to HDFS that involve changes to hdfs-site.xml are not supported.

#### Security

- The SA_PASSWORD is part of the environment and discoverable (for example in a cord dump file). You must reset the SA_PASSWORD on the master instance after deployment. This is not a bug but a security step. For more information on how to change the SA_PASSWORD in a Linux container, see [Change the SA password](../linux/quickstart-install-connect-docker.md#sapassword).

- AKS logs may contain SA password for big data cluster deployments.

## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
