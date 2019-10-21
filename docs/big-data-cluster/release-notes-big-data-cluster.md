---
title: SQL Server Big Data Clusters release notes
titleSuffix: SQL Server big data clusters
description: This article describes the latest updates and known issues for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] (preview). 
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 11/01/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md) release notes

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

Gain near real-time insights from all your data with SQL Server 2019 [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)], which provide a complete environment for working with large sets of data, including machine learning and AI capabilities.

This article lists the updates and known issues for the most recent releases of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] (BDC).

## <a id="rtm"></a> SQL Server 2019

[!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] introduces [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md).

Use [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md) to:

- [Deploy scalable clusters](../big-data-cluster/deploy-get-started.md) of SQL Server, Spark, and HDFS containers running on Kubernetes. 
- Read, write, and process big data from Transact-SQL or Spark.
- Easily combine and analyze high-value relational data with high-volume big data.
- Query external data sources.
- Store big data in HDFS managed by SQL Server.
- Query data from multiple external data sources through the cluster.
- Use the data for AI, machine learning, and other analysis tasks.
- [Deploy and run applications](../big-data-cluster/concept-application-deployment.md) in [!INCLUDE[big-data-clusters](../includes/ssbigdataclusters-nover.md)].
- Virtualize data with [PolyBase](../relational-databases/polybase/polybase-guide.md). Query data from external SQL Server, Oracle, Teradata, MongoDB, and ODBC data sources with external tables.
- Provide high availability for the SQL Server master instance and all databases by using Always On availability group technology.

<!---
|New feature or update | Details |
|:---|:---|
|SQL Server Always On Availability Group |When you deploy a SQL Server Big Data Cluster, you can configure the deployment to create an availability group to provide:<br/><br/>- High availability <br/><br/>- Read-scale out <br/><br/>- Scale-out data insertion into data pool<br/><br>See [Deploy with high availability](../big-data-cluster/deployment-high-availability.md). |
|`azdata` |Simplified installation for the tool with [installation manager](./deploy-install-azdata-linux-package.md)<br/><br/>[`azdata notebook` command](./reference-azdata-notebook.md)<br/><br/>[`azdata bdc status` command](./reference-azdata-bdc-status.md) |
|Azure Data Studio|[Download the Release Candidate build of Azure Data Studio](deploy-big-data-tools.md#download-and-install-azure-data-studio-sql-server-2019-release-candidate-rc).<br/><br/>Added troubleshooting notebooks through SQL Server 2019 guide Jupyter Book.<br/><br/>Added controller login experience.<br/><br/>Added controller dashboard to view service endpoints, view cluster health status, and access troubleshooting notebooks.<br/><br/>Improved notebook cell output/editing performance.|
| &nbsp; | &nbsp; |
-->

## Build number

The RTM build number for SQL Server 2019 is `15.0.2000.5`.

## Image tags

The image tag for this release is `2019-GDR1-ubuntu-1604`.

[!INCLUDE [sql-server-servicing-updates-version-15](../includes/sql-server-servicing-updates-version-15.md)]

## Supportability

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

### Tools

|Platform|Supported versions|
|---------|---------|
|`azdata`|Must be same minor version as the server (same as SQL Server master instance).<br/>Run `azdata –-version` to validate the version.<br/>For SQL Server 2019 Service Update you must use version 15.0.2100|
|Azure Data Studio|Get the latest build of [Azure Data Studio](https://aka.ms/getazuredatastudio).|

### SQL Server Editions

|Edition|Notes|
|---------|---------|
|Enterprise<br/>Standard<br/>Developer| Big Data Cluster edition is determined by the edition of SQL Server master instance. At deployment time Developer edition is deployed by default. You can change the edition after deployment. See [Configure SQL Server master instance](../big-data-cluster/configure-sql-server-master-instance.md). |

## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
