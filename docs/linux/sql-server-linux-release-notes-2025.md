---
title: Release Notes for SQL Server 2025 on Linux
description: This article contains the release notes and supported features for SQL Server 2025 running on Linux. Release notes include the most recent release and several previous releases.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 07/16/2026
ms.service: sql
ms.subservice: linux
ms.topic: release-notes
ms.custom:
  - linux-related-content
  - ignite-2025
---
# Release notes for SQL Server 2025 on Linux

[!INCLUDE [sqlserver2025-linux](../includes/applies-to-version/sqlserver2025-linux.md)]

The following release notes apply to [!INCLUDE [ssSQL25](../includes/sssql25-md.md)] running on Linux. This article is broken into sections for each release. For detailed supportability and known issues, see [SQL Server on Linux: Known issues](sql-server-linux-known-issues.md). Each release links to a support article describing the changes, in addition to the Linux package downloads.

These release notes are specifically for [!INCLUDE [ssSQL25](../includes/sssql25-md.md)] releases. For release notes on other editions, see the following articles:

- [Release notes for SQL Server 2017 on Linux](sql-server-linux-release-notes-2017.md?view=sql-server-ver14&preserve-view=true)
- [Release notes for SQL Server 2019 on Linux](sql-server-linux-release-notes-2019.md?view=sql-server-ver15&preserve-view=true)
- [Release notes for SQL Server 2022 on Linux](sql-server-linux-release-notes-2022.md?view=sql-server-ver16&preserve-view=true)

[!INCLUDE [support-policy](includes/support-policy.md)]

## Supported platforms

[!INCLUDE [linux-supported-platforms-2025](includes/linux-supported-platforms-2025.md)]

## Tools

Most existing client tools that target [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] can seamlessly target [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] running on Linux. Some tools might have a specific version requirement to work well with Linux. For a full list of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] tools, see [SQL tools overview](../tools/overview-sql-tools.md).

## Release and container tag guidance

- Starting with [!INCLUDE [ssSQL25](../includes/sssql25-md.md)], **SUSE Linux Enterprise Server** (SLES) isn't supported.

  Customers using earlier versions of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on SLES aren't affected, and there are no changes to your support for existing deployments. For more information about version lifecycle policies, see [SQL Server 2022](/lifecycle/products/sql-server-2022), [SQL Server 2019](/lifecycle/products/sql-server-2019), and [SQL Server 2017](/lifecycle/products/sql-server-2017). To upgrade to [!INCLUDE [ssSQL25](../includes/sssql25-md.md)], [back up your databases and restore them](business-continuity/backup-restore/database-backup-restore.md) to a [supported distribution](#supported-platforms).

- Some GDR releases apply only to Windows. These Windows-only GDRs aren't published for Linux, and don't appear in this article.

- Container tags can vary by release. For a list of available tags, see [RHEL](https://mcr.microsoft.com/product/mssql/rhel/server/tags) and [Ubuntu](https://mcr.microsoft.com/product/mssql/server/tags) in the Microsoft Artifact Registry.

## Latest versions for all packages

This section lists the latest versions of each package per distribution, for [!INCLUDE [ssSQL25](../includes/sssql25-md.md)]. The following table shows the most recent release, which is **CU 7**. For full release history, see [Release history for SQL Server 2025 on Linux](/troubleshoot/sql/releases/linux/release-history-2025).

| Version | Release | Date | Build | KB article |
| --- | --- | --- | --- | --- |
| [!INCLUDE [ssSQL25](../includes/sssql25-md.md)] | CU 7 | 2026-07-16 | 17.0.4065.4 | [KB 5096981](https://support.microsoft.com/help/5096981) |

| Distribution | Package name | Package version | Download |
| --- | --- | --- | --- |
| **Red Hat Enterprise Linux** | | | |
| RHEL 10 | Database Engine | 17.0.4065.4-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/10/mssql-server-2025/Packages/m/mssql-server-17.0.4065.4-1.x86_64.rpm) |
| RHEL 10 | Extensibility | 17.0.4065.4-1 | [Extensibility RPM package](https://packages.microsoft.com/rhel/10/mssql-server-2025/Packages/m/mssql-server-extensibility-17.0.4065.4-1.x86_64.rpm) |
| RHEL 10 | Full-Text Search | 17.0.4065.4-1 | [Full-Text Search RPM package](https://packages.microsoft.com/rhel/10/mssql-server-2025/Packages/m/mssql-server-fts-17.0.4065.4-1.x86_64.rpm) |
| RHEL 10 | High Availability | 17.0.4065.4-1 | [High Availability RPM package](https://packages.microsoft.com/rhel/10/mssql-server-2025/Packages/m/mssql-server-ha-17.0.4065.4-1.x86_64.rpm) |
| RHEL 10 | PolyBase | 17.0.4065.4-1 | [PolyBase RPM package](https://packages.microsoft.com/rhel/10/mssql-server-2025/Packages/m/mssql-server-polybase-17.0.4065.4-1.x86_64.rpm) |
| **Ubuntu** | | | |
| Ubuntu 24.04 | Database Engine | 17.0.4065.4-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/24.04/mssql-server-2025/pool/main/m/mssql-server/mssql-server_17.0.4065.4-1_amd64.deb) |
| Ubuntu 24.04 | Extensibility | 17.0.4065.4-1 | [Extensibility Debian package](https://packages.microsoft.com/ubuntu/24.04/mssql-server-2025/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_17.0.4065.4-1_amd64.deb) |
| Ubuntu 24.04 | Full-Text Search | 17.0.4065.4-1 | [Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/24.04/mssql-server-2025/pool/main/m/mssql-server-fts/mssql-server-fts_17.0.4065.4-1_amd64.deb) |
| Ubuntu 24.04 | High Availability | 17.0.4065.4-1 | [High Availability Debian package](https://packages.microsoft.com/ubuntu/24.04/mssql-server-2025/pool/main/m/mssql-server-ha/mssql-server-ha_17.0.4065.4-1_amd64.deb) |
| Ubuntu 24.04 | PolyBase | 17.0.4065.4-1 | [PolyBase Debian package](https://packages.microsoft.com/ubuntu/24.04/mssql-server-2025/pool/main/m/mssql-server-polybase/mssql-server-polybase_17.0.4065.4-1_amd64.deb) |

<a id="cuinstall"></a>

## How to install updates

When you configure the CU repository (`mssql-server-2025`), you get the latest CU of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] packages when you perform new installations. If you require Docker container images, see official images for [Microsoft SQL Server on Linux for Docker Engine](https://hub.docker.com/r/microsoft/mssql-server). For more information about repository configuration, see [Configure repositories for installing and upgrading SQL Server 2025 on Linux](install-upgrade/change-repo-2025.md).

If you update existing [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] packages, run the appropriate update command for each package to get the latest CU. For specific update instructions for each package, see the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md#upgrade)
- [Install SQL Server Full-Text Search on Linux](install-upgrade/setup-full-text-search.md)
- [Install SQL Server Integration Services (SSIS) on Linux](install-upgrade/setup-ssis.md)
- [Install SQL Server 2019 Machine Learning Services (Python and R) on Linux](install-upgrade/setup-machine-learning.md)
- [Install PolyBase on Linux](../relational-databases/polybase/polybase-linux-setup.md)
- [Install SQL Server Agent on Linux](install-upgrade/setup-sql-agent.md)

## Release history

The following table lists the release history for [!INCLUDE [ssSQL25](../includes/sssql25-md.md)].

| Release | Version | Release date |
| --- | --- | --- |
| [CU 7](/troubleshoot/sql/releases/linux/release-history-2025#17-0-4065) | 17.0.4065.4 | 2026-07-16 |
| [CU 6 GDR (Jul 2026)](/troubleshoot/sql/releases/linux/release-history-2025#17-0-4060) | 17.0.4060.2 | 2026-07-14 |
| [CU 6](/troubleshoot/sql/releases/linux/release-history-2025#17-0-4055) | 17.0.4055.5 | 2026-06-17 |
| [CU 5](/troubleshoot/sql/releases/linux/release-history-2025#17-0-4045) | 17.0.4045.5 | 2026-05-20 |
| [CU 4 GDR (May 2026)](/troubleshoot/sql/releases/linux/release-history-2025#17-0-4040) | 17.0.4040.1 | 2026-05-12 |
| [CU 4](/troubleshoot/sql/releases/linux/release-history-2025#17-0-4035) | 17.0.4035.5 | 2026-04-16 |
| [CU 3 GDR (Apr 2026)](/troubleshoot/sql/releases/linux/release-history-2025#17-0-4030) | 17.0.4030.1 | 2026-04-14 |
| [CU 3](/troubleshoot/sql/releases/linux/release-history-2025#17-0-4025) | 17.0.4025.3 | 2026-03-12 |
| [CU 2 GDR (Mar 2026)](/troubleshoot/sql/releases/linux/release-history-2025#17-0-4020) | 17.0.4020.2 | 2026-03-10 |
| [CU 2](/troubleshoot/sql/releases/linux/release-history-2025#17-0-4015) | 17.0.4015.4 | 2026-02-12 |
| [CU 1](/troubleshoot/sql/releases/linux/release-history-2025#17-0-4006) | 17.0.4006.2 | 2026-01-29 |
| [GA](/troubleshoot/sql/releases/linux/release-history-2025#17-0-1000) | 17.0.1000.7 | 2025-11-18 |

## Known issues

For more information, see [SQL Server on Linux: Known issues](sql-server-linux-known-issues.md).

## Related content

- [SQL Server on Linux FAQ](sql-server-linux-faq.yml)
- [Quickstart: Install SQL Server and create a database on Red Hat Enterprise Linux](install-upgrade/quickstart-install-red-hat.md)
- [Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server](install-upgrade/quickstart-install-suse.md)
- [Quickstart: Install SQL Server and create a database on Ubuntu](install-upgrade/quickstart-install-ubuntu.md)
- [Quickstart: Run SQL Server Linux container images with Docker](install-upgrade/quickstart-install-docker.md)
- [Provision a Linux virtual machine running SQL Server in the Azure portal](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart)
- [Quickstart: Run SQL Server in the cloud](install-upgrade/quickstart-install-clouds.md)
