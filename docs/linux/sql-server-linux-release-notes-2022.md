---
title: Release notes for SQL Server 2022 Preview on Linux
description: This article contains the release notes and supported features for SQL Server 2022 Preview running on Linux. Release notes are included for the most recent release and several previous releases.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, vanto
ms.date: 08/23/2022
ms.prod: sql
ms.technology: linux
ms.topic: conceptual
---
# Release notes for [!INCLUDE[sssql22](../includes/sssql22-md.md)] on Linux

[!INCLUDE[tsql-appliesto-ssver16-xxxx-xxxx-xxx-linuxonly.md](../includes/tsql-appliesto-ssver16-xxxx-xxxx-xxx-linuxonly.md)]

The following release notes apply to the release candidate (RC) 0 of [!INCLUDE[sssql22](../includes/sssql22-md.md)] running on Linux.

> [!TIP]  
> To learn about new Linux features in [!INCLUDE[sssql22](../includes/sssql22-md.md)], see [What's new in SQL Server 2022 Preview](../sql-server/what-s-new-in-sql-server-2022.md).

## Supported platforms

[!INCLUDE [linux-supported-platforms-2022](../includes/linux/linux-supported-platforms-2022.md)]

## Tools

Most existing client tools that target SQL Server can seamlessly target SQL Server running on Linux. Some tools might have a specific version requirement to work well with Linux. For a full list of SQL Server tools, see [SQL Tools and Utilities for SQL Server](../tools/overview-sql-tools.md).

## How to install updates

If you've configured the preview repository (`mssql-server-preview`), then you'll get the latest public preview of SQL Server packages when you perform new installations. If you require Docker container images, see official images for [Microsoft SQL Server on Linux for Docker Engine](https://hub.docker.com/r/microsoft/mssql-server/).

If you're updating existing SQL Server packages, run the appropriate update command for each package to get the latest public preview. For specific update instructions for each package, see the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md#upgrade)
- [Install Full-Text Search package](sql-server-linux-setup-full-text-search.md)
- [Install SQL Server Integration Services](sql-server-linux-setup-ssis.md)
- [Install SQL Server Machine Learning Services R and Python support on Linux](sql-server-linux-setup-machine-learning.md)
- [Install PolyBase package](../relational-databases/polybase/polybase-linux-setup.md)
- [Enable SQL Server Agent](sql-server-linux-setup-sql-agent.md)

## <a id="rc0"></a> RC 0 (August 2022)

The following section provides package locations and known issues for the latest public release candidate (RC) 0. To learn more about new features [!INCLUDE[sssql22](../includes/sssql22-md.md)] running on Linux, see the [What's new in SQL Server 2022 Preview](../sql-server/what-s-new-in-sql-server-2022.md).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages for the latest supported distributions, with the information in the following table.

> [!IMPORTANT]  
> SUSE Linux Enterprise Server (SLES) **is not supported** for [!INCLUDE[sssql22](../includes/sssql22-md.md)] on Linux RC 0.

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 8.x RPM packages** | 16.0.900.6-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-preview/mssql-server-16.0.900.6-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-preview/mssql-server-ha-16.0.900.6-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-preview/mssql-server-fts-16.0.900.6-1.x86_64.rpm)</br>[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-preview/mssql-server-extensibility-16.0.900.6-1.x86_64.rpm)</br>[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-preview/mssql-server-extensibility-java-16.0.900.6-1.x86_64.rpm)</br>[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-preview/mssql-server-polybase-16.0.900.6-1.x86_64.rpm)|
| **Ubuntu 20.04 Debian packages** | 16.0.900.6-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-preview/pool/main/m/mssql-server/mssql-server_16.0.900.6-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-preview/pool/main/m/mssql-server-ha/mssql-server-ha_16.0.900.6-1_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-preview/pool/main/m/mssql-server-fts/mssql-server-fts_16.0.900.6-1_amd64.deb)</br>[Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-preview/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_16.0.900.6-1_amd64.deb)</br>[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-preview/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_16.0.900.6-1_amd64.deb)</br>[PolyBase Debian Package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-preview/pool/main/m/mssql-server-polybase/mssql-server-polybase_16.0.900.6-1_amd64.deb)|

## Known issues

[!INCLUDE [linux-known-issues-2022](../includes/linux/linux-known-issues-2022.md)]

## See also

- [SQL Server on Linux FAQ](sql-server-linux-faq.yml)

## Next steps

To get started, see the following quickstarts:

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Run on Docker](quickstart-install-connect-ubuntu.md)
- [Provision a SQL VM in Azure](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart?toc=/sql/toc/toc.json)
- [Run SQL Server in the cloud](quickstart-install-connect-clouds.md)
