---
title: SQL Server 2019 Big Data Clusters platform release notes
titleSuffix: SQL Server 2019 Big Data Clusters
description: This article describes the latest updates and known issues for SQL Server Big Data Clusters.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei, randolphwest
ms.date: 10/22/2023
ms.service: sql
ms.subservice: big-data-cluster
ms.custom: linux-related-content
ms.topic: release-notes
---
# SQL Server 2019 Big Data Clusters platform release notes

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

The following release notes apply to [!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)]. This article is broken into sections for each release describing the CU changes. The article also lists [known issues](#known-issues) for the most recent releases of [!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)].

## Tested configurations

[!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] is a fully containerized solution orchestrated by Kubernetes. Starting with [!INCLUDE[sssql19-md](../includes/sssql19-md.md)] CU12, each release of SQL Server Big Data Clusters is tested against a fixed configuration of components. The configuration is evaluated with each release and adjustments are made to stay in-line with the ecosystem as Kubernetes continues to evolve.

> [!IMPORTANT]  
> Kubernetes is a fast paced ecosystem. It is key to keep your platform updated in order to be secure, and to be on a tested configuration for SQL Server Big Data Clusters.

> [!WARNING]  
> On [!INCLUDE[sssql19-md](../includes/sssql19-md.md)] Cumulative Update 15, **the upgrade order is critical**. Upgrade your big data cluster to CU15 *before* upgrading the Kubernetes cluster to version 1.21. If the Kubernetes cluster is upgraded to version 1.21 before BDC is upgraded to CU14 or CU15 then the cluster will end up in error state and the BDC upgrade will not succeed. In this case, reverting back to Kubernetes version 1.20 will fix the problem.

The following table contains the tested configuration matrix for each release of SQL Server 2019 Big Data Clusters:

| Release | Container OS | Kubernetes API | Runtime | Data Storage | Log Storage |
| --- | --- | --- | --- | --- | --- |
| CU23 | Ubuntu 20.04 LTS | 1.25.4 | containerd 1.4.6<br />CRI-O 1.20.4 | Block only | Block only |
| CU22 | Ubuntu 20.04 LTS | 1.25.4 | containerd 1.4.6<br />CRI-O 1.20.4 | Block only | Block only |
| CU21 | Ubuntu 20.04 LTS | 1.23.1 | containerd 1.4.6<br />CRI-O 1.20.4 | Block only | Block only |
| CU20 | Ubuntu 20.04 LTS | 1.23.1 | containerd 1.4.6<br />CRI-O 1.20.4 | Block only | Block only |
| CU19 | Ubuntu 20.04 LTS | 1.23.1 | containerd 1.4.6<br />CRI-O 1.20.4 | Block only | Block only |
| CU18 GDR | Ubuntu 20.04 LTS | 1.23.1 | containerd 1.4.6<br />CRI-O 1.20.4 | Block only | Block only |
| CU18 | Ubuntu 20.04 LTS | 1.23.1 | containerd 1.4.6<br />CRI-O 1.20.4 | Block only | Block only |
| CU17 | Ubuntu 20.04 LTS | 1.23.1 | containerd 1.4.6<br />CRI-O 1.20.4 | Block only | Block only |
| CU16 GDR | Ubuntu 20.04 LTS | 1.21 | containerd 1.4.6<br />CRI-O 1.20.4 | Block only | Block only |
| CU16 | Ubuntu 20.04 LTS | 1.21 | containerd 1.4.6<br />CRI-O 1.20.4 | Block only | Block only |
| CU15 | Ubuntu 20.04 LTS | 1.21 | containerd 1.4.6<br />CRI-O 1.20.4 | Block only | Block only |
| CU14 | Ubuntu 20.04 LTS | 1.21 | containerd 1.4.6<br />CRI-O 1.20.4 | Block only | Block only |
| CU13 | Ubuntu 20.04 LTS | 1.20 | containerd 1.4.6<br />CRI-O 1.20.0 | Block only | Block only |
| CU12 | Ubuntu 20.04 LTS | 1.20 | containerd 1.4.3<br />docker 20.10.2<br />CRI-O 1.20.0 | Block only | Block only |

Restrictions:

- SQL Server 2019 Big Data Clusters is supported as a *workload*. Microsoft provides support for the software components on the containers installed and configured by SQL Server 2019 Big Data Clusters only. The support team doesn't support Kubernetes itself, and other containers that might influence SQL Server 2019 Big Data Clusters behavior. For Kubernetes support, contact your certified Kubernetes distribution provider.
- SQL Server 2019 Big Data Clusters requires block storage for all persisted volumes. Management operation on top of the persisted volumes created and used by a big data cluster is a capability that depends on the storage provider including, for example, operations to expand persistent volumes (PVs). Reference your specific CSI storage provider documentation or the [partner reference architecture and white papers](partner-big-data-cluster.md).
- The open-source components included by SQL Server 2019 Big Data Clusters are fixed for that particular release and must not be updated or modified.
- Container images are provided "as-is". Composability features of Kubernetes aren't supported. Changing the set of container images in a SQL Server 2019 Big Data Cluster release, or to customize the containers, isn't supported.

Reference architecture and white papers for [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] can be found on the following pages:

- [SQL Server 2019](https://www.microsoft.com/sql-server/sql-server-2019)
- [SQL Server 2019 Big Data Clusters partners](partner-big-data-cluster.md)

## Release history

The following table lists the release history for [!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)]. For more information, see [SQL Server 2019 Big Data Clusters cumulative updates history](release-notes-cumulative-updates-history.md).

| Release <sup>1</sup> | [!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] version | [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] version <sup>2</sup> | Release date | Details |
| --- | --- | --- | --- | --- |
| [CU23](release-notes-cumulative-update-23.md) | 15.0.4335.1 | 20.3.12 | Jun 15 2023 | Libraries are same as CU19. |
| [CU22](release-notes-cumulative-update-22.md) | 15.0.4322.2 | 20.3.12 | Jun 15 2023 | Libraries are same as CU19. |
| [CU21](release-notes-cumulative-update-21.md) | 15.0.4316.3 | 20.3.12 | Jun 15 2023 | Tested configurations, libraries are same as CU19. |
| [CU20](release-notes-cumulative-update-20.md) | 15.0.4312.2 | 20.3.12 | Apr 14 2023 | Tested configurations, libraries are same as CU19. |
| [CU19](release-notes-cumulative-update-19.md) | 15.0.4298.1 | 20.3.12 | Feb 16 2023 | |
| CU18 GDR [KB 5021124](https://support.microsoft.com/help/5021124) | 15.0.4280.7 | 20.3.12 | Feb 14 2023 | |
| [CU18](release-notes-cumulative-update-18.md) | 15.0.4261.1 | 20.3.12 | Sep 28 2022 | |
| [CU17](release-notes-cumulative-update-17.md) | 15.0.4249.2 | 20.3.12 | Aug 11 2022 | |
| CU16 GDR [KB 5014356](https://support.microsoft.com/help/5014356) | 15.0.4236.7 | 20.3.12 | Jun 14 2022 | |
| [CU16](release-notes-cumulative-update-16.md) | 15.0.4223.1 | 20.3.11 | May 2 2022 | |
| [CU15](release-notes-cumulative-update-15.md) | 15.0.4198.2 | 20.3.10 | Jan 27 2022 | |
| [CU14](release-notes-cumulative-update-14.md) | 15.0.4188.2 | 20.3.9 | Nov 22 2021 | |
| [CU13](release-notes-cumulative-update-13.md) | 15.0.4178.15 | 20.3.8 | Sep 9 2021 | |
| [CU12](release-notes-cumulative-update-12.md) | 15.0.4153.13 | 20.3.7 | Aug 4 2021 | |
| [CU11](release-notes-cumulative-updates-history.md#cu11) | 15.0.4138.2 | 20.3.5 | Jun 10 2021 | |
| [CU10](release-notes-cumulative-updates-history.md#cu10) | 15.0.4123.1 | 20.3.2 | Apr 6 2021 | |
| [CU9](release-notes-cumulative-updates-history.md#cu9) | 15.0.4102.2 | 20.3.0 | Feb 11 2021 | |
| [CU8-GDR](release-notes-cumulative-updates-history.md#cu8-gdr) | 15.0.4083.2 | 20.2.6 | Jan 12 2021 | |
| [CU8](release-notes-cumulative-updates-history.md#cu8) | 15.0.4073.23 | 20.2.2 | Oct 19 2020 | |
| [CU6](release-notes-cumulative-updates-history.md#cu6) | 15.0.4053.23 | 20.0.1 | Aug 4 2020 | |
| [CU5](release-notes-cumulative-updates-history.md#cu5) | 15.0.4043.16 | 20.0.0 | June 22 2020 | |
| [CU4](release-notes-cumulative-updates-history.md#cu4) | 15.0.4033.1 | 15.0.4033 | March 31 2020 | |
| [CU3](release-notes-cumulative-updates-history.md#cu3) | 15.0.4023.6 | 15.0.4023 | March 12 2020 | |
| [CU2](release-notes-cumulative-updates-history.md#cu2) | 15.0.4013.40 | 15.0.4013 | Feb 13 2020 | |
| [CU1](release-notes-cumulative-updates-history.md#cu1) | 15.0.4003.23 | 15.0.4003 | Jan 7 2020 | |
| [GDR1](release-notes-cumulative-updates-history.md#rtm) | 15.0.2070.34 | 15.0.2070 | Nov 4 2019 | |

<sup>1</sup> CU7 isn't available for [!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)].

<sup>2</sup> [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] version reflects the version of the tool at the time of the CU release. `azdata` can also release independently of the server release, therefore you might get newer versions when you install the latest packages. Newer versions are compatible with previously released CUs.

## How to install updates

To install updates, see [How to upgrade [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)]](deployment-upgrade.md).

## Known issues

For known issues and resolves issues, see [SQL Server 2019 Big Data Clusters platform known issues](known-issues.md).

## Related content

For more information about [!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)], see [Introducing [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
