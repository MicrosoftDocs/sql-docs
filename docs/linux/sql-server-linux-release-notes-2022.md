---
title: Release Notes for SQL Server 2022 on Linux
description: This article contains the release notes and supported features for SQL Server 2022 running on Linux. Release notes include the most recent release and several previous releases.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, vanto
ms.date: 07/08/2025
ms.service: sql
ms.subservice: linux
ms.topic: release-notes
ms.custom:
  - linux-related-content
  - build-2025
---
# Release notes for SQL Server 2022 on Linux

[!INCLUDE [sqlserver2022-linux](../includes/applies-to-version/sqlserver2022-linux.md)]

The following release notes apply to [!INCLUDE [ssSQL22](../includes/sssql22-md.md)] running on Linux. This article is broken into sections for each release. For detailed supportability and known issues, see [SQL Server on Linux: Known issues](sql-server-linux-known-issues.md). Each release links to a support article describing the changes, in addition to the Linux package downloads.

These release notes are specifically for [!INCLUDE [ssSQL22](../includes/sssql22-md.md)] releases. For release notes on other editions, see the following articles:

- [Release notes for SQL Server 2017 on Linux](sql-server-linux-release-notes-2017.md?view=sql-server-ver14&preserve-view=true)
- [Release notes for SQL Server 2019 on Linux](sql-server-linux-release-notes-2019.md?view=sql-server-ver15&preserve-view=true)
- [Release notes for SQL Server 2025 Preview on Linux](sql-server-linux-release-notes-2025.md?view=sql-server-ver17&preserve-view=true)

[!INCLUDE [support-policy](includes/support-policy.md)]

## Supported platforms

[!INCLUDE [linux-supported-platforms-2022](includes/linux-supported-platforms-2022.md)]

## Tools

Most existing client tools that target [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] can seamlessly target [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] running on Linux. Some tools might have a specific version requirement to work well with Linux. For a full list of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] tools, see [SQL tools overview](../tools/overview-sql-tools.md).

## Latest versions for all packages

This section lists the latest versions of each package per distribution, for [!INCLUDE [ssSQL22](../includes/sssql22-md.md)]. The following table shows the most recent release, which is **CU 19 GDR**. For full release history, see [Release history for SQL Server 2022 on Linux](/troubleshoot/sql/releases/linux/release-history-2022).

| Version | Release | Date | Build | KB article |
| --- | --- | --- | --- | --- |
| [!INCLUDE [ssSQL22](../includes/sssql22-md.md)] | CU 19 GDR | 2025-07-08 | 16.0.4200.1 | [KB 5058721](https://support.microsoft.com/help/5058721) |

- The **mssql-server-is** package isn't supported on SUSE in this release. For more information, see [SQL Server on Linux: Known issues](sql-server-linux-known-issues.md#sql-server-integration-services-ssis).

| Distribution | Package name | Package version | Download |
| --- | --- | --- | --- |
| **Red Hat Enterprise Linux** | | | |
| RHEL 9 | Database Engine | 16.0.4200.1-4 | [Database Engine RPM package](https://packages.microsoft.com/rhel/9/mssql-server-2022/Packages/m/mssql-server-16.0.4200.1-4.x86_64.rpm) |
| RHEL 9 | Extensibility | 16.0.4200.1-4 | [Extensibility RPM package](https://packages.microsoft.com/rhel/9/mssql-server-2022/Packages/m/mssql-server-extensibility-16.0.4200.1-4.x86_64.rpm) |
| RHEL 9 | Full-Text Search | 16.0.4200.1-4 | [Full-Text Search RPM package](https://packages.microsoft.com/rhel/9/mssql-server-2022/Packages/m/mssql-server-fts-16.0.4200.1-4.x86_64.rpm) |
| RHEL 9 | High Availability | 16.0.4200.1-4 | [High Availability RPM package](https://packages.microsoft.com/rhel/9/mssql-server-2022/Packages/m/mssql-server-ha-16.0.4200.1-4.x86_64.rpm) |
| RHEL 9 | PolyBase | 16.0.4200.1-4 | [PolyBase RPM package](https://packages.microsoft.com/rhel/9/mssql-server-2022/Packages/m/mssql-server-polybase-16.0.4200.1-4.x86_64.rpm) |
| RHEL 8 | SSIS | 16.0.4003.1-1 | [SSIS RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/Packages/m/mssql-server-is-16.0.4003.1-1.x86_64.rpm) |
| **SUSE Linux Enterprise Server** | | | |
| SLES 15 | Database Engine | 16.0.4200.1-4 | [Database Engine RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/Packages/m/mssql-server-16.0.4200.1-4.x86_64.rpm) |
| SLES 15 | Extensibility | 16.0.4200.1-4 | [Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/Packages/m/mssql-server-extensibility-16.0.4200.1-4.x86_64.rpm) |
| SLES 15 | Full-Text Search | 16.0.4200.1-4 | [Full-Text Search RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/Packages/m/mssql-server-fts-16.0.4200.1-4.x86_64.rpm) |
| SLES 15 | High Availability | 16.0.4200.1-4 | [High Availability RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/Packages/m/mssql-server-ha-16.0.4200.1-4.x86_64.rpm) |
| SLES 15 | PolyBase | 16.0.4200.1-4 | [PolyBase RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/Packages/m/mssql-server-polybase-16.0.4200.1-4.x86_64.rpm) |
| **Ubuntu** | | | |
| Ubuntu 22.04 | Database Engine | 16.0.4200.1-4 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/22.04/mssql-server-2022/pool/main/m/mssql-server/mssql-server_16.0.4200.1-4_amd64.deb) |
| Ubuntu 22.04 | Extensibility | 16.0.4200.1-4 | [Extensibility Debian package](https://packages.microsoft.com/ubuntu/22.04/mssql-server-2022/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_16.0.4200.1-4_amd64.deb) |
| Ubuntu 22.04 | Full-Text Search | 16.0.4200.1-4 | [Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/22.04/mssql-server-2022/pool/main/m/mssql-server-fts/mssql-server-fts_16.0.4200.1-4_amd64.deb) |
| Ubuntu 22.04 | High Availability | 16.0.4200.1-4 | [High Availability Debian package](https://packages.microsoft.com/ubuntu/22.04/mssql-server-2022/pool/main/m/mssql-server-ha/mssql-server-ha_16.0.4200.1-4_amd64.deb) |
| Ubuntu 22.04 | PolyBase | 16.0.4200.1-4 | [PolyBase Debian package](https://packages.microsoft.com/ubuntu/22.04/mssql-server-2022/pool/main/m/mssql-server-polybase/mssql-server-polybase_16.0.4200.1-4_amd64.deb) |
| Ubuntu 20.04 | SSIS | 16.0.4003.1-1 | [SSIS Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server-is/mssql-server-is_16.0.4003.1-1_amd64.deb) |

<a id="cuinstall"></a>

## How to install updates

When you configure the CU repository (`mssql-server-2022`), you get the latest CU of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] packages when you perform new installations. If you require Docker container images, see official images for [Microsoft SQL Server on Linux for Docker Engine](https://hub.docker.com/_/microsoft-mssql-server). For more information about repository configuration, see [Configure repositories for installing and upgrading SQL Server on Linux](sql-server-linux-change-repo.md).

If you update existing [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] packages, run the appropriate update command for each package to get the latest CU. For specific update instructions for each package, see the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md#upgrade)
- [Install SQL Server Full-Text Search on Linux](sql-server-linux-setup-full-text-search.md)
- [Install SQL Server Integration Services (SSIS) on Linux](sql-server-linux-setup-ssis.md)
- [Install SQL Server 2019 Machine Learning Services R and Python support on Linux](sql-server-linux-setup-machine-learning.md)
- [Install PolyBase package](../relational-databases/polybase/polybase-linux-setup.md)
- [Enable SQL Server Agent](sql-server-linux-setup-sql-agent.md)

## Release history

The following table lists the release history for [!INCLUDE [ssSQL22](../includes/sssql22-md.md)].

> [!NOTE]  
> Any missing GDRs apply to the Windows version only.

| Release | Version | Release date |
| --- | --- | --- |
| [CU 19 GDR](/troubleshoot/sql/releases/linux/release-history-2022#CU19-GDR) | 16.0.4200.1 | 2025-07-08 |
| [CU 19](/troubleshoot/sql/releases/linux/release-history-2022#CU19) | 16.0.4195.2 | 2025-05-15 |
| [CU 18](/troubleshoot/sql/releases/linux/release-history-2022#CU18) | 16.0.4185.3 | 2025-03-13 |
| [CU 17](/troubleshoot/sql/releases/linux/release-history-2022#CU17) | 16.0.4175.1 | 2025-01-16 |
| [CU 16](/troubleshoot/sql/releases/linux/release-history-2022#CU16) | 16.0.4165.4 | 2024-11-14 |
| [CU 15 GDR 2](/troubleshoot/sql/releases/linux/release-history-2022#CU15-GDR2) | 16.0.4155.4 | 2024-11-12 |
| [CU 15 GDR 1](/troubleshoot/sql/releases/linux/release-history-2022#CU15-GDR1) | 16.0.4150.1 | 2024-10-08 |
| [CU 15](/troubleshoot/sql/releases/linux/release-history-2022#CU15) | 16.0.4145.4 | 2024-09-25 |
| [CU 14 GDR](/troubleshoot/sql/releases/linux/release-history-2022#CU14-GDR) | 16.0.4140.3 | 2024-09-10 |
| [CU 14](/troubleshoot/sql/releases/linux/release-history-2022#CU14) | 16.0.4135.4 | 2024-07-23 |
| [CU 13](/troubleshoot/sql/releases/linux/release-history-2022#CU13) | 16.0.4125.3 | 2024-05-16 |
| [CU 12 GDR](/troubleshoot/sql/releases/linux/release-history-2022#CU12-GDR) | 16.0.4120.1 | 2024-04-09 |
| [CU 12](/troubleshoot/sql/releases/linux/release-history-2022#CU12) | 16.0.4115.5 | 2024-03-14 |
| [CU 11](/troubleshoot/sql/releases/linux/release-history-2022#CU11) | 16.0.4105.2 | 2024-01-11 |
| [CU 10 GDR](/troubleshoot/sql/releases/linux/release-history-2022#CU10-GDR) | 16.0.4100.1 | 2024-01-09 |
| [CU 10](/troubleshoot/sql/releases/linux/release-history-2022#CU10) | 16.0.4095.4 | 2023-11-16 |
| [CU 9](/troubleshoot/sql/releases/linux/release-history-2022#CU9) | 16.0.4085.2 | 2023-10-12 |
| [CU 8 GDR](/troubleshoot/sql/releases/linux/release-history-2022#CU8-GDR) | 16.0.4080.1 | 2023-10-10 |
| [CU 8](/troubleshoot/sql/releases/linux/release-history-2022#CU8) | 16.0.4075.1 | 2023-09-15 |
| [CU 7](/troubleshoot/sql/releases/linux/release-history-2022#CU7) | 16.0.4065.3 | 2023-08-10 |
| [CU 6](/troubleshoot/sql/releases/linux/release-history-2022#CU6) | 16.0.4055.4 | 2023-07-13 |
| [CU 5](/troubleshoot/sql/releases/linux/release-history-2022#CU5) | 16.0.4045.3 | 2023-06-15 |
| [CU 4](/troubleshoot/sql/releases/linux/release-history-2022#CU4) | 16.0.4035.4 | 2023-05-11 |
| [CU 3](/troubleshoot/sql/releases/linux/release-history-2022#CU3) | 16.0.4025.1 | 2023-04-13 |
| [CU 2](/troubleshoot/sql/releases/linux/release-history-2022#CU2) | 16.0.4015.1 | 2023-03-15 |
| [CU 1](/troubleshoot/sql/releases/linux/release-history-2022#CU1) | 16.0.4003.1 | 2023-02-16 |
| [GDR 1](/troubleshoot/sql/releases/linux/release-history-2022#GDR1) | 16.0.1050.5 | 2023-02-14 |
| [GA](/troubleshoot/sql/releases/linux/release-history-2022#GA) | 16.0.1000.6 | 2022-11-16 |

## Known issues

For more information, see [SQL Server on Linux: Known issues](sql-server-linux-known-issues.md).

## Related content

- [SQL Server on Linux FAQ](sql-server-linux-faq.yml)
- [Quickstart: Install SQL Server and create a database on Red Hat](quickstart-install-connect-red-hat.md)
- [Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Quickstart: Install SQL Server and create a database on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Quickstart: Run SQL Server Linux container images with Docker](quickstart-install-connect-docker.md)
- [Provision a Linux virtual machine running SQL Server in the Azure portal](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart)
- [Quickstart: Run SQL Server in the cloud](quickstart-install-connect-clouds.md)
