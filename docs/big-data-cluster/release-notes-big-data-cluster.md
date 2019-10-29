---
title: SQL Server Big Data Clusters release notes
titleSuffix: SQL Server big data clusters
description: This article describes the latest updates and known issues for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] (preview). 
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 11/04/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# SQL Server Big Data Clusters release notes

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

Gain near real-time insights from all your data with [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)], which provide a complete environment for working with large sets of data, including machine learning and AI capabilities.

This article lists the updates and known issues for the most recent releases of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] (BDC).

## <a id="rtm"></a> SQL Server 2019

[!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] introduces SQL Server Big Data Clusters.

Use SQL Server Big Data Clusters to:

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

## SQL Server version

The current version of SQL Server is `15.0.2070.34`.

## Image tags

The image tag for this release is `2019-GDR1-ubuntu-16.04`.

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
|`azdata`|Must be same minor version as the server (same as SQL Server master instance).<br/>Run `azdata –-version` to validate the version. Currently, this version is `15.0.2070`.|
|Azure Data Studio|Get the latest build of [Azure Data Studio](https://aka.ms/getazuredatastudio).|

### SQL Server Editions

|Edition|Notes|
|---------|---------|
|Enterprise<br/>Standard<br/>Developer| Big Data Cluster edition is determined by the edition of SQL Server master instance. At deployment time Developer edition is deployed by default. You can change the edition after deployment. See [Configure SQL Server master instance](../big-data-cluster/configure-sql-server-master-instance.md). |

## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
