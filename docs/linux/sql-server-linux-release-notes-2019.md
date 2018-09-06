---
title: Release notes for SQL Server 2019 CTP on Linux | Microsoft Docs
description: This article contains the release notes and supported features for SQL Server 2019 CTP running on Linux. Release notes are included for the most recent release and several previous releases.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 09/24/2018
ms.topic: conceptual
ms.prod: sql
ms.component: ""
ms.suite: "sql"
ms.custom: "sql-linux"
ms.technology: linux
monikerRange: ">= sql-server-linux-ver15  || >= sql-server-ver15 || = sqlallproducts-allversions"
---
# Release notes for SQL Server 2019 CTP on Linux

[!INCLUDE[tsql-appliesto-ssvnext-xxxx-xxxx-xxx-linuxonly.md](../includes/tsql-appliesto-ssvnext-xxxx-xxxx-xxx-linuxonly.md)]

The following release notes apply to SQL Server 2019 CTP running on Linux. This article is broken into sections for each release. Each release has a link to a support article describing the CU changes as well as links to the Linux package downloads.

> [!TIP]
> To learn about new Linux features in SQL Server 2019, see [What's new in SQL Server 2019 CTP 2.0 for Linux](../sql-server/what-s-new-in-sql-server-ver15.md#sqllinux).

## Supported platforms

| Platform | File System | Installation Guide |
|-----|-----|-----|
| Red Hat Enterprise Linux 7.3 or 7.4 Workstation, Server, and Desktop | XFS or EXT4 | [Installation guide](quickstart-install-connect-red-hat.md) | 
| SUSE Enterprise Linux Server v12 SP2 | XFS or EXT4 | [Installation guide](quickstart-install-connect-suse.md) |
| Ubuntu 16.04LTS | XFS or EXT4 | [Installation guide](quickstart-install-connect-ubuntu.md) | 
| Docker Engine 1.8+ on Windows, Mac, or Linux | N/A | [Installation guide](quickstart-install-connect-docker.md) | 

> [!TIP]
> For more information, review the [system requirements](sql-server-linux-setup.md#system) for SQL Server on Linux. For the latest support policy for SQL Server 2017, see the [Technical support policy for Microsoft SQL Server](https://support.microsoft.com/help/4047326/support-policy-for-microsoft-sql-server).

## Tools

Most existing client tools that target SQL Server can seamlessly target SQL Server running on Linux. Some tools might have a specific version requirement to work well with Linux. For a full list of SQL Server tools, see [SQL Tools and Utilities for SQL Server](../tools/overview-sql-tools.md).

## Release history

The following table lists the release history for SQL Server 2019 CTPs.

| Release               | Version       | Release date |
|-----------------------|---------------|--------------|
| [CTP 2.0](#CTP20) | xx.x.xxxx.xx   | 2018-09-24   |

## <a id="cuinstall"></a> How to install updates

If you have configured the preview repository (**mssql-server-preview**), then you will get the latest SQL Server CTP packages when you perform new installations. If you require Docker container images, please see official images for [Microsoft SQL Server on Linux for Docker Engine](http://hub.docker.com/r/microsoft/mssql-server-linux/). For more information about repository configuration, see [Configure repositories for SQL Server on Linux](sql-server-linux-change-repo.md).

If you are updating existing SQL Server packages, run the appropriate update command for each package to get the latest CU. For specific update instructions for each package, see the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md#upgrade)
- [Install Full-text Search package](sql-server-linux-setup-full-text-search.md)
- [Install SQL Server Integration Services](sql-server-linux-setup-ssis.md)
- [Install SQL Server vNext Machine Learning Services R and Python support on Linux](sql-server-linux-setup-machine-learning.md)
- [Enable SQL Server Agent](sql-server-linux-setup-sql-agent.md)

## <a id="CU9-GDR2"></a> CU9-GDR2 (August 2018)

This is a security update that also includes the previously released CU (CU9) for SQL Server 2017. The SQL Server engine version for this release is 14.0.3035.2. For information about the fixes and improvements in this release, see [https://support.microsoft.com/en-us/help/4293805](https://support.microsoft.com/en-us/help/4293805).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | xx.x.xxxx.xx-1 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-xx.x.xxxx.xx-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-xx.x.xxxx.xx-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-xx.x.xxxx.xx-1.x86_64.rpm)| 
| SLES RPM package | xx.x.xxxx.xx-1 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-xx.x.xxxx.xx-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-xx.x.xxxx.xx-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-xx.x.xxxx.xx-1.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | xx.x.xxxx.xx-1 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_xx.x.xxxx.xx-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_xx.x.xxxx.xx-1_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_xx.x.xxxx.xx-1_amd64.deb)<br/> |

## Next steps

To get started, see the following quickstarts:

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
- [Install on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Run on Docker](quickstart-install-connect-ubuntu.md)
- [Provision a SQL VM in Azure](/azure/virtual-machines/linux/sql/provision-sql-server-linux-virtual-machine?toc=%2fsql%2flinux%2ftoc.json)
- [Run & Connect - Cloud](quickstart-install-connect-clouds.md)

For answers to frequently asked questions, see the [SQL Server on Linux FAQ](sql-server-linux-faq.md).