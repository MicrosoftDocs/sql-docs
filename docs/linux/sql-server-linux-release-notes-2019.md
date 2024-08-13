---
title: Release notes for SQL Server 2019 on Linux
description: This article contains the release notes and supported features for SQL Server 2019 running on Linux. Release notes include the most recent release and several previous releases.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, vanto
ms.date: 08/01/2024
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---
# Release notes for SQL Server 2019 on Linux

[!INCLUDE [sqlserver2019-linux](../includes/applies-to-version/sqlserver2019-linux.md)]

The following release notes apply to [!INCLUDE [ssSQL19](../includes/sssql19-md.md)] running on Linux. This article is broken into sections for each release. For detailed supportability and known issues, see [SQL Server on Linux: Known issues](sql-server-linux-known-issues.md). Each release links to a support article describing the changes, in addition to the Linux package downloads.

These release notes are specifically for [!INCLUDE [ssSQL19](../includes/sssql19-md.md)] releases. For release notes on other editions, see the following articles:

- [Release notes for SQL Server 2017 on Linux](sql-server-linux-release-notes-2017.md?view=sql-server-ver14&preserve-view=true)
- [Release notes for SQL Server 2022 on Linux](sql-server-linux-release-notes-2022.md?view=sql-server-ver16&preserve-view=true)

## Supported platforms

[!INCLUDE [linux-supported-platforms-2019](includes/linux-supported-platforms-2019.md)]

## Tools

Most existing client tools that target [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] can seamlessly target [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] running on Linux. Some tools might have a specific version requirement to work well with Linux. For a full list of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] tools, see [SQL tools overview](../tools/overview-sql-tools.md).

## Latest versions for all packages

This section lists the latest versions of each package per distribution, for [!INCLUDE [ssSQL19](../includes/sssql19-md.md)]. The following table shows the most recent release, which is **CU 28**. For full release history, see [Release history for [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] 2019](/troubleshoot/sql/linux/release-history-2019).

| Version | Release | Date | Build | KB article |
| --- | --- | --- | --- | --- |
| [!INCLUDE [ssSQL19](../includes/sssql19-md.md)] | CU 28 | 2024-08-01 | 15.0.4385.2 | [Support article](/troubleshoot/sql/releases/sqlserver-2019/cumulativeupdate28) |

- The **mssql-server-is** package isn't supported on SUSE in this release. For more information, see [SQL Server on Linux: Known issues](sql-server-linux-known-issues.md#sql-server-integration-services-ssis).

| Distribution | Package name | Package version | Download |
| --- | --- | --- | --- |
| **Red Hat Enterprise Linux** | | | |
| RHEL 8 | Database Engine | 15.0.4385.2-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/Packages/m/mssql-server-15.0.4385.2-1.x86_64.rpm) |
| RHEL 8 | Extensibility | 15.0.4385.2-1 | [Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/Packages/m/mssql-server-extensibility-15.0.4385.2-1.x86_64.rpm) |
| RHEL 8 | Full-Text Search | 15.0.4385.2-1 | [Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/Packages/m/mssql-server-fts-15.0.4385.2-1.x86_64.rpm) |
| RHEL 8 | High Availability | 15.0.4385.2-1 | [High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/Packages/m/mssql-server-ha-15.0.4385.2-1.x86_64.rpm) |
| RHEL 8 | Java Extensibility | 15.0.4385.2-1 | [Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/Packages/m/mssql-server-extensibility-java-15.0.4385.2-1.x86_64.rpm) |
| RHEL 8 | PolyBase | 15.0.4385.2-1 | [PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/Packages/m/mssql-server-polybase-15.0.4385.2-1.x86_64.rpm) |
| RHEL 8 | SSIS | 15.0.4063.15-88 | [SSIS RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/Packages/m/mssql-server-is-15.0.4063.15-88.x86_64.rpm) |
| **SUSE Linux Enterprise Server** | | | |
| SLES 15 | Database Engine | 15.0.4385.2-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/Packages/m/mssql-server-15.0.4385.2-1.x86_64.rpm) |
| SLES 15 | Extensibility | 15.0.4385.2-1 | [Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/Packages/m/mssql-server-extensibility-15.0.4385.2-1.x86_64.rpm) |
| SLES 15 | Full-Text Search | 15.0.4385.2-1 | [Full-Text Search RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/Packages/m/mssql-server-fts-15.0.4385.2-1.x86_64.rpm) |
| SLES 15 | High Availability | 15.0.4385.2-1 | [High Availability RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/Packages/m/mssql-server-ha-15.0.4385.2-1.x86_64.rpm) |
| SLES 15 | Java Extensibility | 15.0.4385.2-1 | [Java Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/Packages/m/mssql-server-extensibility-java-15.0.4385.2-1.x86_64.rpm) |
| SLES 15 | PolyBase | 15.0.4385.2-1 | [PolyBase RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/Packages/m/mssql-server-polybase-15.0.4385.2-1.x86_64.rpm) |
| **Ubuntu** | | | |
| Ubuntu 20.04 | Database Engine | 15.0.4385.2-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4385.2-1_amd64.deb) |
| Ubuntu 20.04 | Extensibility | 15.0.4385.2-1 | [Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4385.2-1_amd64.deb) |
| Ubuntu 20.04 | Full-Text Search | 15.0.4385.2-1 | [Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4385.2-1_amd64.deb) |
| Ubuntu 20.04 | High Availability | 15.0.4385.2-1 | [High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4385.2-1_amd64.deb) |
| Ubuntu 20.04 | Java Extensibility | 15.0.4385.2-1 | [Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4385.2-1_amd64.deb) |
| Ubuntu 20.04 | PolyBase | 15.0.4385.2-1 | [PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4385.2-1_amd64.deb) |
| Ubuntu 18.04 | SSIS | 15.0.4153.1-89 | [SSIS Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-is/mssql-server-is_15.0.4153.1-89_amd64.deb) |

## <a id="cuinstall"></a> How to install updates

When you configure the CU repository (`mssql-server-2019`), you get the latest CU of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] packages when you perform new installations. If you require Docker container images, see official images for [Microsoft SQL Server on Linux for Docker Engine](https://hub.docker.com/_/microsoft-mssql-server). For more information about repository configuration, see [Configure repositories for installing and upgrading SQL Server on Linux](sql-server-linux-change-repo.md).

If you update existing [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] packages, run the appropriate update command for each package to get the latest CU. For specific update instructions for each package, see the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md#upgrade)
- [Install SQL Server Full-Text Search on Linux](sql-server-linux-setup-full-text-search.md)
- [Install SQL Server Integration Services (SSIS) on Linux](sql-server-linux-setup-ssis.md)
- [Install SQL Server 2019 Machine Learning Services R and Python support on Linux](sql-server-linux-setup-machine-learning.md)
- [Install PolyBase package](../relational-databases/polybase/polybase-linux-setup.md)
- [Enable SQL Server Agent](sql-server-linux-setup-sql-agent.md)

## Release history

The following table lists the release history for [!INCLUDE [ssSQL19](../includes/sssql19-md.md)].

| Release                                                                  | Version       | Release date |
| ------------------------------------------------------------------------ | ------------- | ------------ |
| [CU 28](/troubleshoot/sql/linux/release-history-2019#CU28)               | 15.0.4385.2   | 2024-08-01   |
| [CU 27](/troubleshoot/sql/linux/release-history-2019#CU27)               | 15.0.4375.4   | 2024-06-13   |
| [CU 26](/troubleshoot/sql/linux/release-history-2019#CU26)               | 15.0.4365.2   | 2024-04-11   |
| [CU 25 GDR](/troubleshoot/sql/linux/release-history-2019#CU25-GDR)       | 15.0.4360.2   | 2024-04-09   |
| [CU 25](/troubleshoot/sql/linux/release-history-2019#CU25)               | 15.0.4355.3   | 2024-02-15   |
| [CU 24](/troubleshoot/sql/linux/release-history-2019#CU24)               | 15.0.4345.5   | 2023-12-14   |
| [CU 23](/troubleshoot/sql/linux/release-history-2019#CU23)               | 15.0.4335.1   | 2023-10-12   |
| [CU 22 GDR](/troubleshoot/sql/linux/release-history-2019#CU22-GDR)       | 15.0.4326.1   | 2023-10-10   |
| [CU 22](/troubleshoot/sql/linux/release-history-2019#CU22)               | 15.0.4322.2   | 2023-08-14   |
| [CU 21](/troubleshoot/sql/linux/release-history-2019#CU21)               | 15.0.4316.3   | 2023-06-15   |
| [CU 20](/troubleshoot/sql/linux/release-history-2019#CU20)               | 15.0.4312.2   | 2023-04-13   |
| [CU 19](/troubleshoot/sql/linux/release-history-2019#CU19)               | 15.0.4298.1   | 2023-02-16   |
| [CU 18 GDR](/troubleshoot/sql/linux/release-history-2019#CU18-GDR)       | 15.0.4280.7   | 2023-02-14   |
| [CU 18](/troubleshoot/sql/linux/release-history-2019#CU18)               | 15.0.4261.1   | 2022-09-28   |
| [CU 17](/troubleshoot/sql/linux/release-history-2019#CU17)               | 15.0.4249.2   | 2022-08-11   |
| [CU 16 GDR](/troubleshoot/sql/linux/release-history-2019#CU16-GDR)       | 15.0.4236.7   | 2022-06-14   |
| [CU 16](/troubleshoot/sql/linux/release-history-2019#CU16)               | 15.0.4223.1   | 2022-04-18   |
| [CU 15](/troubleshoot/sql/linux/release-history-2019#CU15)               | 15.0.4198.2   | 2022-01-27   |
| [CU 14](/troubleshoot/sql/linux/release-history-2019#CU14)               | 15.0.4188.2   | 2021-11-22   |
| [CU 13](/troubleshoot/sql/linux/release-history-2019#CU13)               | 15.0.4178.1   | 2021-10-05   |
| [CU 12](/troubleshoot/sql/linux/release-history-2019#CU12)               | 15.0.4153.1   | 2021-08-04   |
| [CU 11](/troubleshoot/sql/linux/release-history-2019#CU11)               | 15.0.4138.2   | 2021-06-10   |
| [CU 10](/troubleshoot/sql/linux/release-history-2019#CU10)               | 15.0.4123.1   | 2021-04-06   |
| [CU 9](/troubleshoot/sql/linux/release-history-2019#CU9)                 | 15.0.4102.2   | 2021-02-10   |
| [CU 8 GDR](/troubleshoot/sql/linux/release-history-2019#CU8-GDR)         | 15.0.4083.2   | 2021-01-12   |
| [GDR 1](/troubleshoot/sql/linux/release-history-2019#GDR1)               | 15.0.2080.9   | 2021-01-12   |
| [CU 8](/troubleshoot/sql/linux/release-history-2019#CU8)                 | 15.0.4073.23  | 2020-10-07   |
| [CU 7 (Removed)](/troubleshoot/sql/releases/sqlserver-2019/cumulativeupdate7) | 15.0.4063.15  | 2020-09-02   |
| [CU 6](/troubleshoot/sql/linux/release-history-2019#CU6)                 | 15.0.4053.23  | 2020-08-04   |
| [CU 5](/troubleshoot/sql/linux/release-history-2019#CU5)                 | 15.0.4043.16  | 2020-06-22   |
| [CU 4](/troubleshoot/sql/linux/release-history-2019#CU4)                 | 15.0.4033.1   | 2020-03-31   |
| [CU 3](/troubleshoot/sql/linux/release-history-2019#CU3)                 | 15.0.4023.6   | 2020-03-12   |
| [CU 2](/troubleshoot/sql/linux/release-history-2019#CU2)                 | 15.0.4013.40  | 2020-02-13   |
| [CU 1](/troubleshoot/sql/linux/release-history-2019#CU1)                 | 15.0.4003.23  | 2020-01-07   |
| [GA](/troubleshoot/sql/linux/release-history-2019#GA)                    | 15.0.2000.5   | 2019-11-04   |

## Known issues

For more information, see [SQL Server on Linux: Known issues](sql-server-linux-known-issues.md).

## Related content

- [SQL Server on Linux FAQ](sql-server-linux-faq.yml)
- [Quickstart: Install SQL Server and create a database on Red Hat](quickstart-install-connect-red-hat.md)
- [Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Quickstart: Install SQL Server and create a database on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Quickstart: Run SQL Server Linux container images with Docker](quickstart-install-connect-docker.md)
- [Create a SQL VM in Azure](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart)
- [Quickstart: Run SQL Server in the cloud](quickstart-install-connect-clouds.md)
