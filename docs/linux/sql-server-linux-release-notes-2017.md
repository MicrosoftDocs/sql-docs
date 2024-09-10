---
title: Release notes for SQL Server 2017 on Linux
description: This article contains the release notes and supported features for SQL Server 2017 running on Linux. Release notes include the most recent release and several previous releases.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, vanto
ms.date: 09/10/2024
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---
# Release notes for SQL Server 2017 on Linux

[!INCLUDE [sqlserver2017-linux](../includes/applies-to-version/sqlserver2017-linux.md)]

The following release notes apply to [!INCLUDE [ssSQL17](../includes/sssql17-md.md)] running on Linux. This article is broken into sections for each release. For detailed supportability and known issues, see [SQL Server on Linux: Known issues](sql-server-linux-known-issues.md). Each release links to a support article describing the changes, in addition to the Linux package downloads.

These release notes are specifically for [!INCLUDE [ssSQL17](../includes/sssql17-md.md)] releases. For release notes on other editions, see the following articles:

- [Release notes for SQL Server 2019 on Linux](sql-server-linux-release-notes-2019.md?view=sql-server-ver15&preserve-view=true)
- [Release notes for SQL Server 2022 on Linux](sql-server-linux-release-notes-2022.md?view=sql-server-ver16&preserve-view=true)

[!INCLUDE [support-policy](includes/support-policy.md)]

## Supported platforms

[!INCLUDE [linux-supported-platforms-2017](includes/linux-supported-platforms-2017.md)]

## Tools

Most existing client tools that target [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] can seamlessly target [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] running on Linux. Some tools might have a specific version requirement to work well with Linux. For a full list of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] tools, see [SQL tools overview](../tools/overview-sql-tools.md).

## Latest versions for all packages

This section lists the latest versions of each package per distribution, for [!INCLUDE [ssSQL17](../includes/sssql17-md.md)]. The following table shows the most recent release, which is **CU 31 GDR 3**. For full release history, see [Release history for [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] 2017](/troubleshoot/sql/linux/release-history-2017).

| Version | Release | Date | Build | KB article |
| --- | --- | --- | --- | --- |
| [!INCLUDE [ssSQL17](../includes/sssql17-md.md)] | CU 31 GDR 3 | 2024-09-10 | 14.0.3475.1 | [KB 5042215](https://support.microsoft.com/help/5042215) |

> [!IMPORTANT]  
> This is the final cumulative update for [!INCLUDE [ssSQL17](../includes/sssql17-md.md)].

- As of CU 4, [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Agent is no longer installed as a separate package. It's installed with the Database Engine package and must be enabled to use.
- The **mssql-server-is** package isn't supported on SUSE in this release. For more information, see [SQL Server on Linux: Known issues](sql-server-linux-known-issues.md#sql-server-integration-services-ssis).

| Distribution | Package name | Package version | Download |
| --- | --- | --- | --- |
| **Red Hat Enterprise Linux** | | | |
| RHEL 8 | Database Engine | 14.0.3475.1-4 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/Packages/m/mssql-server-14.0.3475.1-4.x86_64.rpm) |
| RHEL 8 | Full-Text Search | 14.0.3475.1-4 | [Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/Packages/m/mssql-server-fts-14.0.3475.1-4.x86_64.rpm) |
| RHEL 8 | High Availability | 14.0.3475.1-4 | [High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/Packages/m/mssql-server-ha-14.0.3475.1-4.x86_64.rpm) |
| RHEL 7 | SSIS | 14.0.3015.40-1 | [SSIS RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/Packages/m/mssql-server-is-14.0.3015.40-1.x86_64.rpm) |
| **SUSE Linux Enterprise Server** | | | |
| SLES 12 | Database Engine | 14.0.3475.1-4 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/Packages/m/mssql-server-14.0.3475.1-4.x86_64.rpm) |
| SLES 12 | Full-Text Search | 14.0.3475.1-4 | [Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/Packages/m/mssql-server-fts-14.0.3475.1-4.x86_64.rpm) |
| SLES 12 | High Availability | 14.0.3475.1-4 | [High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/Packages/m/mssql-server-ha-14.0.3475.1-4.x86_64.rpm) |
| **Ubuntu** | | | |
| Ubuntu 18.04 | Database Engine | 14.0.3475.1-4 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3475.1-4_amd64.deb) |
| Ubuntu 18.04 | Full-Text Search | 14.0.3475.1-4 | [Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3475.1-4_amd64.deb) |
| Ubuntu 18.04 | High Availability | 14.0.3475.1-4 | [High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3475.1-4_amd64.deb) |
| Ubuntu 16.04 | SSIS | 14.0.3015.40-1 | [SSIS Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.3015.40-1_amd64.deb) |

## <a id="cuinstall"></a> How to install updates

When you configure the CU repository (`mssql-server-2017`), you get the latest CU of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] packages when you perform new installations. If you require Docker container images, see official images for [Microsoft SQL Server on Linux for Docker Engine](https://hub.docker.com/_/microsoft-mssql-server). For more information about repository configuration, see [Configure repositories for installing and upgrading SQL Server on Linux](sql-server-linux-change-repo.md).

If you update existing [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] packages, run the appropriate update command for each package to get the latest CU. For specific update instructions for each package, see the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md#upgrade)
- [Install SQL Server Full-Text Search on Linux](sql-server-linux-setup-full-text-search.md)
- [Install SQL Server Integration Services (SSIS) on Linux](sql-server-linux-setup-ssis.md)
- [Enable SQL Server Agent](sql-server-linux-setup-sql-agent.md)

## Release history

The following table lists the release history for [!INCLUDE [ssSQL17](../includes/sssql17-md.md)].

| Release                                                                  | Version       | Release date |
| ------------------------------------------------------------------------ | ------------- | ------------ |
| [CU 31 GDR 3](/troubleshoot/sql/linux/release-history-2017#CU31-GDR3)    | 14.0.3475.1   | 2024-09-10   |
| [CU 31 GDR 2](/troubleshoot/sql/linux/release-history-2017#CU31-GDR2)    | 14.0.3465.1   | 2023-10-10   |
| [CU 31 GDR](/troubleshoot/sql/linux/release-history-2017#CU31-GDR)       | 14.0.3460.9   | 2023-02-14   |
| [CU 31](/troubleshoot/sql/linux/release-history-2017#CU31)               | 14.0.3456.2   | 2022-09-20   |
| [CU 30](/troubleshoot/sql/linux/release-history-2017#CU30)               | 14.0.3451.2   | 2022-07-13   |
| [CU 29 GDR](/troubleshoot/sql/linux/release-history-2017#CU29-GDR)       | 14.0.3445.2   | 2022-06-14   |
| [CU 29](/troubleshoot/sql/linux/release-history-2017#CU29)               | 14.0.3436.1   | 2022-03-30   |
| [CU 28](/troubleshoot/sql/linux/release-history-2017#CU28)               | 14.0.3430.2   | 2022-01-13   |
| [CU 27](/troubleshoot/sql/linux/release-history-2017#CU27)               | 14.0.3421.10  | 2021-10-27   |
| [CU 26](/troubleshoot/sql/linux/release-history-2017#CU26)               | 14.0.3411.3   | 2021-09-14   |
| [CU 25](/troubleshoot/sql/linux/release-history-2017#CU25)               | 14.0.3401.7   | 2021-07-12   |
| [CU 24](/troubleshoot/sql/linux/release-history-2017#CU24)               | 14.0.3391.2   | 2021-05-10   |
| [CU 23](/troubleshoot/sql/linux/release-history-2017#CU23)               | 14.0.3381.3   | 2021-02-24   |
| [CU 22 GDR](/troubleshoot/sql/linux/release-history-2017#CU22)           | 14.0.3370.1   | 2021-01-12   |
| [GDR 3](/troubleshoot/sql/linux/release-history-2017#GDR3)               | 14.0.2037.2   | 2021-01-12   |
| [CU 22](/troubleshoot/sql/linux/release-history-2017#CU22)               | 14.0.3356.20  | 2020-09-10   |
| [CU 21](/troubleshoot/sql/linux/release-history-2017#CU21)               | 14.0.3335.7   | 2020-07-01   |
| [CU 20](/troubleshoot/sql/linux/release-history-2017#CU20)               | 14.0.3294.2   | 2020-04-10   |
| [CU 19](/troubleshoot/sql/linux/release-history-2017#CU19)               | 14.0.3281.6   | 2020-02-05   |
| [CU 18](/troubleshoot/sql/linux/release-history-2017#CU18)               | 14.0.3257.3   | 2019-12-09   |
| [CU 17](/troubleshoot/sql/linux/release-history-2017#CU17)               | 14.0.3238.1   | 2019-10-08   |
| [CU 16](/troubleshoot/sql/linux/release-history-2017#CU16)               | 14.0.3223.3   | 2019-08-01   |
| [CU 15 GDR](/troubleshoot/sql/linux/release-history-2017#CU15-GDR)       | 14.0.3192.2   | 2019-07-09   |
| [CU 15](/troubleshoot/sql/linux/release-history-2017#CU15)               | 14.0.3162.1   | 2019-05-23   |
| [CU 14](/troubleshoot/sql/linux/release-history-2017#CU14)               | 14.0.3076.1   | 2019-03-25   |
| [CU 13](/troubleshoot/sql/linux/release-history-2017#CU13)               | 14.0.3048.4   | 2018-12-18   |
| [CU 12](/troubleshoot/sql/linux/release-history-2017#CU12)               | 14.0.3045.24  | 2018-10-24   |
| [CU 11](/troubleshoot/sql/linux/release-history-2017#CU11)               | 14.0.3038.14  | 2018-09-20   |
| [CU 10](/troubleshoot/sql/linux/release-history-2017#CU10)               | 14.0.3037.1   | 2018-08-27   |
| [CU 9 GDR](/troubleshoot/sql/linux/release-history-2017#CU9-GDR)         | 14.0.3035.2   | 2018-08-18   |
| [GDR 2](/troubleshoot/sql/linux/release-history-2017#GDR2)               | 14.0.2002.14  | 2018-08-18   |
| [CU 9](/troubleshoot/sql/linux/release-history-2017#CU9)                 | 14.0.3030.27  | 2018-07-18   |
| [CU 8](/troubleshoot/sql/linux/release-history-2017#CU8)                 | 14.0.3029.16  | 2018-06-21   |
| [CU 7](/troubleshoot/sql/linux/release-history-2017#CU7)                 | 14.0.3026.27  | 2018-05-24   |
| [CU 6](/troubleshoot/sql/linux/release-history-2017#CU6)                 | 14.0.3025.34  | 2018-04-19   |
| [CU 5](/troubleshoot/sql/linux/release-history-2017#CU5)                 | 14.0.3023.8   | 2018-03-20   |
| [CU 4](/troubleshoot/sql/linux/release-history-2017#CU4)                 | 14.0.3022.28  | 2018-02-20   |
| [CU 3](/troubleshoot/sql/linux/release-history-2017#CU3)                 | 14.0.3015.40  | 2018-01-03   |
| [GDR 1](/troubleshoot/sql/linux/release-history-2017#GDR1)               | 14.0.2000.63  | 2018-01-03   |
| [CU 2](/troubleshoot/sql/linux/release-history-2017#CU2)                 | 14.0.3008.27  | 2017-11-28   |
| [CU 1](/troubleshoot/sql/linux/release-history-2017#CU1)                 | 14.0.3006.16  | 2017-10-24   |
| [GA](/troubleshoot/sql/linux/release-history-2017#GA)                    | 14.0.1000.169 | 2017-10-02   |

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
