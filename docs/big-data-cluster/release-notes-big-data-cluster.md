---
title: SQL Server Big Data Clusters release notes
titleSuffix: SQL Server big data clusters
description: This article describes the latest updates and known issues for SQL Server Big Data Clusters. 
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 11/04/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# SQL Server 2019 Big Data Clusters release notes

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following release notes apply to [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]. This article is broken into sections for each release. Each release has a link to a support article describing the CU changes as well as links to the Linux package downloads. The article also lists [known issues](known-issues) for the most recent releases of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] (BDC).

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
|`azdata`|Must be same minor version as the server (same as SQL Server master instance).<br/>Run `azdata –-version` to validate the version. Currently, this version is `15.0.2070`.|
|Azure Data Studio|Get the latest build of [Azure Data Studio](https://aka.ms/getazuredatastudio).|

## Release history

The following table lists the release history for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)].

| Release               | Version       | Release date |
|-----------------------|---------------|--------------|
| [CU1](#cu1)           | 15.0.4003.1   | 2019-12-05   |
| [GDR1](#rtm)            | 15.0.2070.34  | 2019-11-04   |

## How to install updates

To install updates, see [How to upgrade [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](deployment-upgrade.md).

## <a id="cu1"></a> CU1 (Dec 2019)

This is the Cumulative Update 1 (CU1) release for SQL Server 2019. The SQL Server Database Engine version for this release is 15.0.4003.1.

|Package version | Image tag |
|-----|-----|
|15.0.4003.1|[2019-CU1-ubuntu-16.04]

## <a id="rtm"></a> GDR1 (Nov 2019)

SQL Server 2019 build number `15.0.2070.34` introduces general availability for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-nover.md)]. This build is servicing update GDR1.

|Package version | Image tag |
|-----|-----|
|15.0.2070.34|[2019-GDR1-ubuntu-16.04]

[!INCLUDE [sql-server-servicing-updates-version-15](../includes/sql-server-servicing-updates-version-15.md)]

## Known issues

### Livy job submission from Azure Data Studio (ADS) or curl fail with 500 error

**Issue and customer impact**: In an HA configuration, Spark shared resources (sparkhead) are configured with multiple replicas. In this case, you might experience failures with Livy job submission from Azure Data Studio (ADS) or `curl`. To verify, `curl` to any sparkhead pod results in refused connection. For example, `curl https://sparkhead-0:8998/` or `curl https://sparkhead-1:8998` returns 500 error.

This happens in the following scenarios:

- Zookeeper pods or process for each zookeeper instance are restarted a few times.
- When networking connectivity is unreliable between Sparkhead pod and Zookeeper pods.

**Workaround**: Restarting both Livy servers.

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

## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
