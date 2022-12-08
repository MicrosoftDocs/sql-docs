---
title: Release notes for SQL Server 2019 on Linux
description: This article contains the release notes and supported features for SQL Server 2019 running on Linux. Release notes are included for the most recent release and several previous releases.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, vanto
ms.date: 09/28/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
---
# Release notes for SQL Server 2019 on Linux

[!INCLUDE [sqlserver2019-linux](../includes/applies-to-version/sqlserver2019-linux.md)]

The following release notes apply to [!INCLUDE[ssSQL19](../includes/sssql19-md.md)] running on Linux. This article is broken into sections for each release. Detailed supportability and known issues are listed at the end of the article. Each release has a link to a support article describing the changes as well as links to the Linux package downloads.

These release notes are specifically for [!INCLUDE[ssSQL19](../includes/sssql19-md.md)] releases. For release notes on other editions, see the following articles:

- [Release notes for SQL Server 2017 on Linux](sql-server-linux-release-notes-2017.md?view=sql-server-ver14&preserve-view=true)
- [Release notes for SQL Server 2022 on Linux](sql-server-linux-release-notes-2022.md?view=sql-server-ver16&preserve-view=true)

## Supported platforms

[!INCLUDE[linux-supported-platforms-2019](includes/linux-supported-platforms-2019.md)]

## Tools

Most existing client tools that target [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can seamlessly target [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] running on Linux. Some tools might have a specific version requirement to work well with Linux. For a full list of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tools, see [SQL Tools and Utilities for SQL Server](../tools/overview-sql-tools.md).

## Latest versions for all packages

This section lists the latest versions of each package per distribution, for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 2019. The following table shows the most recent release, which is **CU 18**. For full release history, see [Release history for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 2019](sql-server-linux-release-history-2019.md).

| Version | Release | Date | Build | KB article |
| --- | --- | --- | --- | --- |
| [!INCLUDE[ssSQL19](../includes/sssql19-md.md)] | CU 18 | 2022-09-28 | 15.0.4261.1 | [KB 5017593](https://support.microsoft.com/help/5017593) |


- The **mssql-server-is** package isn't supported on SUSE in this release. See [SQL Server Integration Services (SSIS)](#ssis) for more information.

| Distribution | Package name | Package version | Download |
| --- | --- | --- | --- |
| **Red Hat Enterprise Linux** | | | |
| RHEL 8 | Database Engine | 15.0.4261.1-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4261.1-2.x86_64.rpm) |
| RHEL 8 | Extensibility | 15.0.4261.1-2 | [Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4261.1-2.x86_64.rpm) |
| RHEL 8 | Full-Text Search | 15.0.4261.1-2 | [Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4261.1-2.x86_64.rpm) |
| RHEL 8 | High Availability | 15.0.4261.1-2 | [High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4261.1-2.x86_64.rpm) |
| RHEL 8 | Java Extensibility | 15.0.4261.1-2 | [Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4261.1-2.x86_64.rpm) |
| RHEL 8 | PolyBase | 15.0.4261.1-2 | [PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4261.1-2.x86_64.rpm) |
| RHEL 8 | SSIS | 15.0.4063.15-88 | [SSIS RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-is-15.0.4063.15-88.x86_64.rpm) |
| **SUSE Enterprise Linux Server** | | | |
| SLES 15 | Database Engine | 15.0.4261.1-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-15.0.4261.1-2.x86_64.rpm) |
| SLES 15 | Extensibility | 15.0.4261.1-2 | [Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-extensibility-15.0.4261.1-2.x86_64.rpm) |
| SLES 15 | Full-Text Search | 15.0.4261.1-2 | [Full-Text Search RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-fts-15.0.4261.1-2.x86_64.rpm) |
| SLES 15 | High Availability | 15.0.4261.1-2 | [High Availability RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-ha-15.0.4261.1-2.x86_64.rpm) |
| SLES 15 | Java Extensibility | 15.0.4261.1-2 | [Java Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-extensibility-java-15.0.4261.1-2.x86_64.rpm) |
| SLES 15 | PolyBase | 15.0.4261.1-2 | [PolyBase RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-polybase-15.0.4261.1-2.x86_64.rpm) |
| **Ubuntu** | | | |
| Ubuntu 20.04 | Database Engine | 15.0.4261.1-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4261.1-2_amd64.deb) |
| Ubuntu 20.04 | Extensibility | 15.0.4261.1-2 | [Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4261.1-2_amd64.deb) |
| Ubuntu 20.04 | Full-Text Search | 15.0.4261.1-2 | [Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4261.1-2_amd64.deb) |
| Ubuntu 20.04 | High Availability | 15.0.4261.1-2 | [High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4261.1-2_amd64.deb) |
| Ubuntu 20.04 | Java Extensibility | 15.0.4261.1-2 | [Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4261.1-2_amd64.deb) |
| Ubuntu 20.04 | PolyBase | 15.0.4261.1-2 | [PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4261.1-2_amd64.deb) |
| Ubuntu 18.04 | SSIS | 15.0.4153.1-89 | [SSIS Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-is/mssql-server-is_15.0.4153.1-89_amd64.deb) |

## <a id="cuinstall"></a> How to install updates

If you have configured the CU repository (`mssql-server-2019`), then you will get the latest CU of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] packages when you perform new installations. If you require Docker container images, see official images for [Microsoft SQL Server on Linux for Docker Engine](https://hub.docker.com/r/microsoft/mssql-server/). For more information about repository configuration, see [Configure repositories for SQL Server on Linux](sql-server-linux-change-repo.md).

If you are updating existing [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] packages, run the appropriate update command for each package to get the latest CU. For specific update instructions for each package, see the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md#upgrade)
- [Install Full-Text Search package](sql-server-linux-setup-full-text-search.md)
- [Install SQL Server Integration Services](sql-server-linux-setup-ssis.md)
- [Install SQL Server 2019 Machine Learning Services R and Python support on Linux](sql-server-linux-setup-machine-learning.md)
- [Install PolyBase package](../relational-databases/polybase/polybase-linux-setup.md)
- [Enable SQL Server Agent](sql-server-linux-setup-sql-agent.md)

## Release history

The following table lists the release history for [!INCLUDE[ssSQL19](../includes/sssql19-md.md)].

| Release               | Version       | Release date |
| --------------------- | ------------- | ------------ |
| [CU 18](sql-server-linux-release-history-2019.md#CU18) | 15.0.4261.1   | 2022-09-28   |
| [CU 17](sql-server-linux-release-history-2019.md#CU17) | 15.0.4249.2   | 2022-08-11   |
| [CU 16 GDR](sql-server-linux-release-history-2019.md#CU16-GDR) | 15.0.4236.7   | 2022-06-14   |
| [CU 16](sql-server-linux-release-history-2019.md#CU16) | 15.0.4223.1   | 2022-04-18   |
| [CU 15](sql-server-linux-release-history-2019.md#CU15) | 15.0.4198.2   | 2022-01-27   |
| [CU 14](sql-server-linux-release-history-2019.md#CU14) | 15.0.4188.2   | 2021-11-22   |
| [CU 13](sql-server-linux-release-history-2019.md#CU13) | 15.0.4178.1   | 2021-10-05   |
| [CU 12](sql-server-linux-release-history-2019.md#CU12) | 15.0.4153.1   | 2021-08-04   |
| [CU 11](sql-server-linux-release-history-2019.md#CU11) | 15.0.4138.2   | 2021-06-10   |
| [CU 10](sql-server-linux-release-history-2019.md#CU10) | 15.0.4123.1   | 2021-04-06   |
| [CU 9](sql-server-linux-release-history-2019.md#CU9) | 15.0.4102.2   | 2021-02-10   |
| [CU 8 GDR](sql-server-linux-release-history-2019.md#CU8-GDR) | 15.0.4083.2   | 2021-01-12   |
| [GDR 1](sql-server-linux-release-history-2019.md#GDR1) | 15.0.2080.9   | 2021-01-12   |
| [CU 8](sql-server-linux-release-history-2019.md#CU8) | 15.0.4073.23  | 2020-10-07   |
| [CU 7 (Removed)](https://support.microsoft.com/help/4570012) | 15.0.4063.15  | 2020-09-02   |
| [CU 6](sql-server-linux-release-history-2019.md#CU6) | 15.0.4053.23  | 2020-08-04   |
| [CU 5](sql-server-linux-release-history-2019.md#CU5) | 15.0.4043.16  | 2020-06-22   |
| [CU 4](sql-server-linux-release-history-2019.md#CU4) | 15.0.4033.1   | 2020-03-31   |
| [CU 3](sql-server-linux-release-history-2019.md#CU3) | 15.0.4023.6   | 2020-03-12   |
| [CU 2](sql-server-linux-release-history-2019.md#CU2) | 15.0.4013.40  | 2020-02-13   |
| [CU 1](sql-server-linux-release-history-2019.md#CU1) | 15.0.4003.23  | 2020-01-07   |
| [GA](sql-server-linux-release-history-2019.md#GA) | 15.0.2000.5   | 2019-11-04   |

## Known issues

[!INCLUDE [linux-known-issues-2019](includes/linux-known-issues-2019.md)]

## See also

- [SQL Server on Linux FAQ](sql-server-linux-faq.yml)

## Next steps

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
- [Install on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Run on Docker](quickstart-install-connect-docker.md)
- [Provision a SQL VM in Azure](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart)
- [Run & Connect - Cloud](quickstart-install-connect-clouds.md)

