---
title: Release notes for SQL Server 2022 on Linux
description: This article contains the release notes and supported features for SQL Server 2022 running on Linux. Release notes are included for the most recent release and several previous releases.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, vanto
ms.date: 09/29/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
---
# Release notes for SQL Server 2022 on Linux

[!INCLUDE [sqlserver2022-linux](../includes/applies-to-version/sqlserver2022-linux.md)]

The following release notes apply to [!INCLUDE[ssSQL22](../includes/sssql22-md.md)] running on Linux. This article is broken into sections for each release. Detailed supportability and known issues are listed at the end of the article. Each release has a link to a support article describing the changes as well as links to the Linux package downloads.

These release notes are specifically for [!INCLUDE[ssSQL22](../includes/sssql22-md.md)] releases. For release notes on other editions, see the following articles:

- [Release notes for SQL Server 2017 on Linux](sql-server-linux-release-notes-2017.md?view=sql-server-ver14&preserve-view=true)
- [Release notes for SQL Server 2019 on Linux](sql-server-linux-release-notes-2019.md?view=sql-server-ver15&preserve-view=true)

## Supported platforms

[!INCLUDE[linux-supported-platforms-2022](includes/linux-supported-platforms-2022.md)]

## Tools

Most existing client tools that target [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can seamlessly target [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] running on Linux. Some tools might have a specific version requirement to work well with Linux. For a full list of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tools, see [SQL Tools and Utilities for SQL Server](../tools/overview-sql-tools.md).

## Latest versions for all packages

This section lists the latest versions of each package per distribution, for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 2022. The following table shows the most recent release, which is **GA**. For full release history, see [Release history for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 2022](sql-server-linux-release-history-2022.md).

| Version | Release | Date | Build | KB article |
| --- | --- | --- | --- | --- |
| [!INCLUDE[ssSQL22](../includes/sssql22-md.md)] | GA | 2022-11-16 | 16.0.1000.6 | |


- The **mssql-server-is** package isn't supported on SUSE in this release. See [SQL Server Integration Services (SSIS)](#ssis) for more information.

| Distribution | Package name | Package version | Download |
| --- | --- | --- | --- |
| **Red Hat Enterprise Linux** | | | |
| RHEL 8 | Database Engine | 16.0.1000.6-26 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-16.0.1000.6-26.x86_64.rpm) |
| RHEL 8 | Extensibility | 16.0.1000.6-26 | [Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-extensibility-16.0.1000.6-26.x86_64.rpm) |
| RHEL 8 | Full-Text Search | 16.0.1000.6-26 | [Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-fts-16.0.1000.6-26.x86_64.rpm) |
| RHEL 8 | High Availability | 16.0.1000.6-26 | [High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-ha-16.0.1000.6-26.x86_64.rpm) |
| RHEL 8 | PolyBase | 16.0.1000.6-26 | [PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-polybase-16.0.1000.6-26.x86_64.rpm) |
| **SUSE Enterprise Linux Server** | | | |
| SLES 15 | Database Engine | 16.0.1000.6-26 | [Database Engine RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-16.0.1000.6-26.x86_64.rpm) |
| SLES 15 | Extensibility | 16.0.1000.6-26 | [Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-extensibility-16.0.1000.6-26.x86_64.rpm) |
| SLES 15 | Full-Text Search | 16.0.1000.6-26 | [Full-Text Search RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-fts-16.0.1000.6-26.x86_64.rpm) |
| SLES 15 | High Availability | 16.0.1000.6-26 | [High Availability RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-ha-16.0.1000.6-26.x86_64.rpm) |
| SLES 15 | PolyBase | 16.0.1000.6-26 | [PolyBase RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-polybase-16.0.1000.6-26.x86_64.rpm) |
| **Ubuntu** | | | |
| Ubuntu 20.04 | Database Engine | 16.0.1000.6-26 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server/mssql-server_16.0.1000.6-26_amd64.deb) |
| Ubuntu 20.04 | Extensibility | 16.0.1000.6-26 | [Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_16.0.1000.6-26_amd64.deb) |
| Ubuntu 20.04 | Full-Text Search | 16.0.1000.6-26 | [Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server-fts/mssql-server-fts_16.0.1000.6-26_amd64.deb) |
| Ubuntu 20.04 | High Availability | 16.0.1000.6-26 | [High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server-ha/mssql-server-ha_16.0.1000.6-26_amd64.deb) |
| Ubuntu 20.04 | PolyBase | 16.0.1000.6-26 | [PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server-polybase/mssql-server-polybase_16.0.1000.6-26_amd64.deb) |

## <a id="cuinstall"></a> How to install updates

If you have configured the CU repository (`mssql-server-2022`), then you will get the latest CU of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] packages when you perform new installations. If you require Docker container images, see official images for [Microsoft SQL Server on Linux for Docker Engine](https://hub.docker.com/r/microsoft/mssql-server/). For more information about repository configuration, see [Configure repositories for SQL Server on Linux](sql-server-linux-change-repo.md).

If you are updating existing [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] packages, run the appropriate update command for each package to get the latest CU. For specific update instructions for each package, see the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md#upgrade)
- [Install Full-Text Search package](sql-server-linux-setup-full-text-search.md)
- [Install SQL Server Integration Services](sql-server-linux-setup-ssis.md)
- [Install SQL Server 2019 Machine Learning Services R and Python support on Linux](sql-server-linux-setup-machine-learning.md)
- [Install PolyBase package](../relational-databases/polybase/polybase-linux-setup.md)
- [Enable SQL Server Agent](sql-server-linux-setup-sql-agent.md)

## Release history

The following table lists the release history for [!INCLUDE[ssSQL22](../includes/sssql22-md.md)].

| Release               | Version       | Release date |
| --------------------- | ------------- | ------------ |
| [GA](sql-server-linux-release-history-2022.md#GA)| 16.0.1000.6 | 2022-11-16 |

## Known issues

[!INCLUDE [linux-known-issues-2022](includes/linux-known-issues-2022.md)]

## See also

- [SQL Server on Linux FAQ](sql-server-linux-faq.yml)

## Next steps

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
- [Install on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Run on Docker](quickstart-install-connect-docker.md)
- [Provision a SQL VM in Azure](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart)
- [Run & Connect - Cloud](quickstart-install-connect-clouds.md)

