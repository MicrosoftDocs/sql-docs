---
title: Release notes for SQL Server 2017 on Linux | Microsoft Docs
description: This article contains the release notes and supported features for SQL Server 2017 running on Linux. Release notes are included for the most recent release and several previous releases.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 12/18/2018
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
ms.assetid: 1314744f-fcaf-46db-800e-2918fa7e1b6c
---
# Release notes for SQL Server 2017 on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

The following release notes apply to [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] running on Linux. This article is broken into sections for each release. The GA release has detailed supportability and known issues listed. Each cumulative update (CU) or general distribution release (GDR) has a link to a support article describing the CU changes as well as links to the Linux package downloads.

> [!TIP]
> These release notes are specifically for [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] releases. For more information about the new [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)], see [Release notes for SQL Server 2019 preview on Linux](sql-server-linux-release-notes-2019.md?view=sql-server-ver15).

## Supported platforms

| Platform | File System | Installation Guide |
|-----|-----|-----|
| Red Hat Enterprise Linux 7.3, 7.4, 7.5, or 7.6 Server | XFS or EXT4 | [Installation guide](quickstart-install-connect-red-hat.md) | 
| SUSE Enterprise Linux Server v12 SP2 | XFS or EXT4 | [Installation guide](quickstart-install-connect-suse.md) |
| Ubuntu 16.04LTS | XFS or EXT4 | [Installation guide](quickstart-install-connect-ubuntu.md) | 
| Docker Engine 1.8+ on Windows, Mac, or Linux | N/A | [Installation guide](quickstart-install-connect-docker.md) | 

> [!TIP]
> For more information, review the [system requirements](sql-server-linux-setup.md#system) for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux. For the latest support policy for [!INCLUDE[ssSQL17](../includes/sssql17-md.md)], see the [Technical support policy for Microsoft SQL Server](https://support.microsoft.com/help/4047326/support-policy-for-microsoft-sql-server).

## Tools

Most existing client tools that target [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can seamlessly target [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] running on Linux. Some tools might have a specific version requirement to work well with Linux. For a full list of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tools, see [SQL Tools and Utilities for SQL Server](../tools/overview-sql-tools.md).

## Release history

The following table lists the release history for [!INCLUDE[ssSQL17](../includes/sssql17-md.md)].

| Release               | Version       | Release date |
|-----------------------|---------------|--------------|
| [CU14](#CU14)         | 14.0.3076.1   | 2019-03-25   |
| [CU13](#CU13)         | 14.0.3048.4   | 2018-12-18   |
| [CU12](#CU12)         | 14.0.3045.24  | 2018-10-24   |
| [CU11](#CU11)         | 14.0.3038.14  | 2018-09-20   |
| [CU10](#CU10)         | 14.0.3037.1   | 2018-08-27   |
| [CU9-GDR2](#CU9-GDR2) | 14.0.3035.2   | 2018-08-18   |
| [GDR2](#GDR2)         | 14.0.2002.14  | 2018-08-18   |
| [CU9](#CU9)           | 14.0.3030.27  | 2018-07-18   |
| [CU8](#CU8)           | 14.0.3029.16  | 2018-06-21   |
| [CU7](#CU7)           | 14.0.3026.27  | 2018-05-24   |
| [CU6](#CU6)           | 14.0.3025.34  | 2018-04-19   |
| [CU5](#CU5)           | 14.0.3023.8   | 2018-03-20   |
| [CU4](#CU4)           | 14.0.3022.28  | 2018-02-20   |
| [CU3](#CU3)           | 14.0.3015.40  | 2018-01-03   |
| [GDR1](#GDR1)         | 14.0.2000.63  | 2018-01-03   |
| [CU2](#CU2)           | 14.0.3008.27  | 2017-11-28   |
| [CU1](#CU1)           | 14.0.3006.16  | 2017-10-24   |
| [GA](#GA)             | 14.0.1000.169 | 2017-10-02   |

## <a id="cuinstall"></a> How to install updates

If you have configured the CU repository (**mssql-server-2017**), then you will get the latest CU of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] packages when you perform new installations. The CU repository is the default for all package installation articles for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux. If you have configured the GDR repository (**mssql-server-2017-gdr**), you will only get critical security updates released since GA. If you require Docker container CU or GDR updates, please see official images for [Microsoft SQL Server on Linux for Docker Engine](https://hub.docker.com/r/microsoft/mssql-server). For more information about repository configuration, see [Configure repositories for SQL Server on Linux](sql-server-linux-change-repo.md).

If you are updating existing [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] packages, run the appropriate update command for each package to get the latest CU. For specific update instructions for each package, see the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md#upgrade)
- [Install Full-text Search package](sql-server-linux-setup-full-text-search.md)
- [Install SQL Server Integration Services](sql-server-linux-setup-ssis.md)
- [Enable SQL Server Agent](sql-server-linux-setup-sql-agent.md)

## <a id="CU14"></a> CU14 (Mar 2018)

This is the Cumulative Update 14 (CU14) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3076.1. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4484710](https://support.microsoft.com/help/4484710).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.3076.1-2 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3076.1-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3076.1-2.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3076.1-2.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) | 
| SLES RPM package | 14.0.3076.1-2 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3076.1-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3076.1-2.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3076.1-2.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.3076.1-2 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3076.1-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3076.1-2_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3076.1-2_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU13"></a> CU13 (Dec 2018)

This is the Cumulative Update 13 (CU13) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3048.4. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4466404](https://support.microsoft.com/help/4466404).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.3048.4-1 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3048.4-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3048.4-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3048.4-1.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) | 
| SLES RPM package | 14.0.3048.4-1 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3048.4-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3048.4-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3048.4-1.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.3048.4-1 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3048.4-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3048.4-1_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3048.4-1_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU12"></a> CU12 (Oct 2018)

This is the Cumulative Update 12 (CU12) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3045.24. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4464082](https://support.microsoft.com/help/4464082).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.3045.24-1 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3045.24-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3045.24-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3045.24-1.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) | 
| SLES RPM package | 14.0.3045.24-1 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3045.24-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3045.24-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3045.24-1.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.3045.24-1 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3045.24-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3045.24-1_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3045.24-1_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU11"></a> CU11 (Sept 2018)

This is the Cumulative Update 11 (CU11) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3038.14. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4462262](https://support.microsoft.com/help/4462262).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.3038.14-2 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3038.14-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3038.14-2.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3038.14-2.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) | 
| SLES RPM package | 14.0.3038.14-2 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3038.14-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3038.14-2.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3038.14-2.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.3038.14-2 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3038.14-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3038.14-2_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3038.14-2_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU10"></a> CU10 (Aug 2018)

This is the Cumulative Update 10 (CU10) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3037.1. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4342123](https://support.microsoft.com/help/4342123).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.3037.1-2 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3037.1-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3037.1-2.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3037.1-2.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) | 
| SLES RPM package | 14.0.3037.1-2 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3037.1-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3037.1-2.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3037.1-2.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.3037.1-2 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3037.1-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3037.1-2_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3037.1-2_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU9-GDR2"></a> CU9-GDR2 (Aug 2018)

This is a security update that also includes the previously released CU (CU9) for [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3035.2. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4293805](https://support.microsoft.com/help/4293805).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.3035.2-1 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3035.2-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3035.2-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3035.2-1.x86_64.rpm)| 
| SLES RPM package | 14.0.3035.2-1 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3035.2-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3035.2-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3035.2-1.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.3035.2-1 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3035.2-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3035.2-1_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3035.2-1_amd64.deb)<br/> |

## <a id="GDR2"></a> GDR2 (Aug 2018)

This is a security update that only includes the GDR2 (and GDR1) security fixes for [!INCLUDE[ssSQL17](../includes/sssql17-md.md)].  The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.2002.14. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4293803](https://support.microsoft.com/help/4293803).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.2002.14-1 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-14.0.2002.14-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-ha-14.0.2002.14-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-fts-14.0.2002.14-1.x86_64.rpm) | 
| SLES RPM package | 14.0.2002.14-1 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-14.0.2002.14-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-ha-14.0.2002.14-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-fts-14.0.2002.14-1.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.2002.14-1 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server/mssql-server_14.0.2002.14-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.2002.14-1_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.2002.14-1_amd64.deb) |

## <a id="CU9"></a> CU9 (Jul 2018)

This is the Cumulative Update 9 (CU9) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3030.27. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4341265](https://support.microsoft.com/help/4341265).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.3030.27-1 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3030.27-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3030.27-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3030.27-1.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) | 
| SLES RPM package | 14.0.3030.27-1 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3030.27-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3030.27-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3030.27-1.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.3030.27-1 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3030.27-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3030.27-1_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3030.27-1_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU8"></a> CU8 (Jun 2018)

This is the Cumulative Update 8 (CU8) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3029.16. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4338363](https://support.microsoft.com/help/4338363).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.3029.16-1 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3029.16-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3029.16-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3029.16-1.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) | 
| SLES RPM package | 14.0.3029.16-1 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3029.16-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3029.16-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3029.16-1.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.3029.16-1 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3029.16-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3029.16-1_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3029.16-1_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU7"></a> CU7 (May 2018)

This is the Cumulative Update 7 (CU7) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3026.27. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4229789](https://support.microsoft.com/help/4229789).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.3026.27-2 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3026.27-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3026.27-2.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3026.27-2.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) | 
| SLES RPM package | 14.0.3026.27-2 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3026.27-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3026.27-2.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3026.27-2.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.3026.27-2 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3026.27-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3026.27-2_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3026.27-2_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU6"></a> CU6 (Apr 2018)

This is the Cumulative Update 6 (CU6) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3025.34. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4101464](https://support.microsoft.com/help/4101464).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.3025.34-3 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3025.34-3.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3025.34-3.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3025.34-3.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) | 
| SLES RPM package | 14.0.3025.34-3 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3025.34-3.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3025.34-3.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3025.34-3.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.3025.34-3 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3025.34-3_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3025.34-3_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3025.34-3_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU5"></a> CU5 (Mar 2018)

This is the Cumulative Update 5 (CU5) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3023.8. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4092643](https://support.microsoft.com/help/4092643).

### Known upgrade issue

When you upgrade from a previous release to CU5, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] might fail to start with the following error:

```
Error: 4860, Severity: 16, State: 1.
Cannot bulk load. The file "C:\Install\SqlTraceCollect.dtsx" does not exist or you don't have file access rights.
Error: 912, Severity: 21, State: 2.
Script level upgrade for database 'master' failed because upgrade step 'msdb110_upgrade.sql' encountered error 200, state
```

To resolve this error, enable SQL Server Agent and restart [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with the following commands:

```bash
sudo /opt/mssql/bin/mssql-conf set sqlagent.enabled true
sudo systemctl start mssql-server
```

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.3023.8-5 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3023.8-5.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3023.8-5.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3023.8-5.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) | 
| SLES RPM package | 14.0.3023.8-5 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3023.8-5.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3023.8-5.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3023.8-5.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.3023.8-5 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3023.8-5_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3023.8-5_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3023.8-5_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU4"></a> CU4 (Feb 2018)

This is the Cumulative Update 4 (CU4) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3022.28. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4056498](https://support.microsoft.com/help/4056498).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

> [!NOTE]
> As of CU4, SQL Server Agent is no longer installed as a separate package. It is installed with the Engine package and must be enabled to use.

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.3022.28-2 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3022.28-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3022.28-2.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3022.28-2.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) | 
| SLES RPM package | 14.0.3022.28-2 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3022.28-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3022.28-2.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3022.28-2.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.3022.28-2 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3022.28-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3022.28-2_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3022.28-2_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="GDR1"></a> GDR1 (Jan 2018)

This is a security update that only includes the GDR1 security fixes for [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.2000.63. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4057122](https://support.microsoft.com/help/4057122).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.2000.63-3 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-14.0.2000.63-3.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-ha-14.0.2000.63-3.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-fts-14.0.2000.63-3.x86_64.rpm) | 
| SLES RPM package | 14.0.2000.63-3 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-14.0.2000.63-3.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-ha-14.0.2000.63-3.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-fts-14.0.2000.63-3.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.2000.63-3 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server/mssql-server_14.0.2000.63-3_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.2000.63-3_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.2000.63-3_amd64.deb) |

## <a id="CU3"></a> CU3 (Jan 2018)

This is the Cumulative Update 3 (CU3) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3015.40. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4052987](https://support.microsoft.com/help/4052987).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.3015.40-1 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3015.40-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3015.40-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3015.40-1.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.3015.40-1.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) | 
| SLES RPM package | 14.0.3015.40-1 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3015.40-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3015.40-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3015.40-1.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.3015.40-1.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.3015.40-1 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3015.40-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3015.40-1_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3015.40-1_amd64.deb)</br>[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.3015.40-1_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU2"></a> CU2 (Nov 2017)

This is the Cumulative Update 2 (CU2) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3008.27. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4052574](https://support.microsoft.com/help/4052574).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.3008.27-1 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3008.27-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3008.27-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3008.27-1.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.3008.27-1.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) | 
| SLES RPM package | 14.0.3008.27-1 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3008.27-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3008.27-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3008.27-1.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.3008.27-1.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.3008.27-1 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3008.27-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3008.27-1_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3008.27-1_amd64.deb)</br>[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.3008.27-1_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU1"></a> CU1 (Oct 2017)

This is the Cumulative Update 1 (CU1) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3006.16. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/KB4053439](https://support.microsoft.com/help/4038634).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.3006.16-3 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3006.16-3.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3006.16-3.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3006.16-3.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.3006.16-3.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) | 
| SLES RPM package | 14.0.3006.16-3 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3006.16-3.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3006.16-3.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3006.16-3.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.3006.16-3.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.3006.16-3 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3006.16-3_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3006.16-3_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3006.16-3_amd64.deb)</br>[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.3006.16-3_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="GA"></a> GA (Oct 2017)

This is the General Availability (GA) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.1000.169.

### Package details

Package details and download locations for the RPM and Debian packages are listed in the following table. Note that you do not need to download these packages directly if you use the steps in the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md)
- [Install Full-text Search package](sql-server-linux-setup-full-text-search.md)
- [Install SQL Server Agent package](sql-server-linux-setup-sql-agent.md)
- [Install SQL Server Integration Services](sql-server-linux-setup-ssis.md)

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.1000.169-2 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.1000.169-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.1000.169-2.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.1000.169-2.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.1000.169-2.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) | 
| SLES RPM package | 14.0.1000.169-2 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.1000.169-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.1000.169-2.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.1000.169-2.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.1000.169-2.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.1000.169-2 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.1000.169-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.1000.169-2_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.1000.169-2_amd64.deb)</br>[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.1000.169-2_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a name="Unsupported"></a> Unsupported features & services

The following features and services are not available on Linux at the time of the GA release. The support of these features will be increasingly enabled over time.

| Area | Unsupported feature or service |
|-----|-----|
| **Database engine** | Transactional replication |
| &nbsp; | Merge replication |
| &nbsp; | Change Data Capture (see SQL Server Agent) |
| &nbsp; | Stretch DB |
| &nbsp; | PolyBase |
| &nbsp; | Distributed query with 3rd-party connections |
| &nbsp; | Linked Servers to data sources other than [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]  |
| &nbsp; | System extended stored procedures (XP_CMDSHELL, etc.) |
| &nbsp; | Filetable, FILESTREAM |
| &nbsp; | CLR assemblies with the EXTERNAL_ACCESS or UNSAFE permission set |
| &nbsp; | Buffer Pool Extension |
| **SQL Server Agent** |  Subsystems: CmdExec, PowerShell, Queue Reader, SSIS, SSAS, SSRS |
| &nbsp; | Alerts |
| &nbsp; | Log Reader Agent |
| &nbsp; | Change Data Capture (CDC) |
| &nbsp; | Managed Backup |
| **High Availability** | Database mirroring  |
| **Security** | Extensible Key Management |
| &nbsp; | AD Authentication for Linked Servers | 
| &nbsp; | AD Authentication for Availability Groups (AGs) | 
| &nbsp; | 3rd party AD tools (Centrify, Vintela, Powerbroker) | 
| **Services** | SQL Server Browser |
| &nbsp; | SQL Server R services |
| &nbsp; | StreamInsight |
| &nbsp; | Analysis Services |
| &nbsp; | Reporting Services |
| &nbsp; | Data Quality Services |
| &nbsp; | Master Data Services |
| &nbsp; | Distributed Transaction Coordinator (DTC) |

## Known issues

The following sections describe known issues with the General Availability (GA) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] on Linux.

#### General

- The length of the hostname where [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is installed needs to be 15 characters or less. 

    - **Resolution**: Change the name in /etc/hostname to something 15 characters long or less.

- Manually setting the system time backwards in time will cause [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to stop updating the internal system time within [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].

    - **Resolution**: Restart [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].

- Only single instance installations are supported.

    - **Resolution**: If you want to have more than one instance on a given host, consider using VMs or Docker containers. 

- [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager can't connect to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux.

- The default language of the **sa** login is English.

    - **Resolution**: Change the language of the **sa** login with the **ALTER LOGIN** statement.

#### Databases

- The master database cannot be moved with the mssql-conf utility. Other system databases can be moved with mssql-conf.

- When restoring a database that was backed up on [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Windows, you must use the **WITH MOVE** clause in the Transact-SQL statement.

- Distributed transactions requiring the Microsoft Distributed Transaction Coordinator service are not supported on [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] running on Linux. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] linked servers are supported unless they involve the DTC. For more information, see [Distributed transactions requiring the Microsoft Distributed Transaction Coordinator service are not supported on SQL Server running on Linux](https://blogs.msdn.microsoft.com/bobsql/2017/12/11/sql-server-linux-distributed-transactions-requiring-the-microsoft-distributed-transaction-coordinator-service-are-not-supported-on-sql-server-running-on-linux-sql-server-to-sql-server-distributed-tr/).

- Certain algorithms (cipher suites) for Transport Layer Security (TLS) do not work properly with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux. This results in connection failures when attempting to connect to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], as well as problems establishing connections between replicas in high availability groups.

   - **Resolution**: Modify the **mssql.conf** configuration script for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux to disable problematic cipher suites, by doing the following:

      1. Add the following to /var/opt/mssql/mssql.conf.

      ```
      [network]
      tlsciphers= AES256-GCM-SHA384:AES128-GCM-SHA256:AES256-SHA256:AES128-SHA256:AES256-SHA:AES128-SHA:!ECDHE-RSA-AES128-GCM-SHA256:!ECDHE-RSA-AES256-GCM-SHA384:!ECDHE-ECDSA-AES256-GCM-SHA384:!ECDHE-ECDSA-AES128-GCM-SHA256:!ECDHE-ECDSA-AES256-SHA384:!ECDHE-ECDSA-AES128-SHA256:!ECDHE-ECDSA-AES256-SHA:!ECDHE-ECDSA-AES128-SHA:!ECDHE-RSA-AES256-SHA384:!ECDHE-RSA-AES128-SHA256:!ECDHE-RSA-AES256-SHA:!ECDHE-RSA-AES128-SHA:!DHE-RSA-AES256-GCM-SHA384:!DHE-RSA-AES128-GCM-SHA256:!DHE-RSA-AES256-SHA:!DHE-RSA-AES128-SHA:!DHE-DSS-AES256-SHA256:!DHE-DSS-AES128-SHA256:!DHE-DSS-AES256-SHA:!DHE-DSS-AES128-SHA:!DHE-DSS-DES-CBC3-SHA:!NULL-SHA256:!NULL-SHA
      ```

         >[!NOTE]
         >In the preceding code, `!` negates the expression. This tells OpenSSL to not use the following cipher suite.  

      1. Restart [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with the following command.

      ```bash
      sudo systemctl restart mssql-server
      ```

- [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] databases on Windows that use In-memory OLTP cannot be restored on [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] on Linux. To restore a [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] database that uses in-memory OLTP, first upgrade the databases to [!INCLUDE[ssSQL15](../includes/sssql15-md.md)] or [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] on Windows before moving them to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux via backup/restore or detach/attach.

- User permission **ADMINISTER BULK OPERATIONS** is not supported on Linux at this time.

#### Networking

Features that involve outbound TCP connections from the sqlservr process, such as linked servers or Availability Groups, might not work if both the following conditions are met:

1. The target server is specified as a hostname and not an IP address.

1. The source instance has IPv6 disabled in the kernel. To verify if your system has IPv6 enabled in the kernel, all the following tests must pass:

   - `cat /proc/cmdline` will print the boot cmdline of the current kernel. The output must not contain `ipv6.disable=1`.
   - The /proc/sys/net/ipv6/ directory must exist.
   - A C program that calls `socket(AF_INET6, SOCK_STREAM, IPPROTO_IP)` should succeed - the syscall must return an fd != -1 and not fail with EAFNOSUPPORT.

The exact error depends on the feature. For linked servers, this manifests as a login timeout error. For Availability Groups, the `ALTER AVAILABILITY GROUP JOIN` DDL on the secondary will fail after 5 minutes with a download configuration timeout error.

To work around this issue, do one of the following:

1. Use IPs instead of hostnames to specify the target of the TCP connection.

1. Enable IPv6 in the kernel by removing `ipv6.disable=1` from the boot cmdline. The way to do this depends on the Linux distribution and the bootloader, such as grub. If you do want IPv6 to be disabled, you can still disable it by setting `net.ipv6.conf.all.disable_ipv6 = 1` in the `sysctl` configuration (eg `/etc/sysctl.conf`). This will still prevent the system's network adapter from getting an IPv6 address, but allow the sqlservr features to work.

#### Network File System (NFS)
If you use **Network File System (NFS)** remote shares in production, note the following support requirements:

- Use NFS version **4.2 or higher**. Older versions of NFS do not support required features, such as fallocate and sparse file creation, common to modern file systems.
- Locate only the **/var/opt/mssql** directories on the NFS mount. Other files, such as the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] system binaries, are not supported.
- Ensure that NFS clients use the 'nolock' option when mounting the remote share.

#### Localization

- If your locale is not English (en_us) during setup, you must use UTF-8 encoding in your bash session/terminal. If you use ASCII encoding, you might see an error similar to the following:

   ```
   UnicodeEncodeError: 'ascii' codec can't encode character u'\xf1' in position 8: ordinal not in range(128)
   ```

   If you cannot use UTF-8 encoding, run setup using the MSSQL_LCID environment variable to specify your language choice.

   ```bash
   sudo MSSQL_LCID=<LcidValue> /opt/mssql/bin/mssql-conf setup
   ```

- When running mssql-conf setup, and performing a non-English installation of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], incorrect extended characters are displayed after the localized text, "Configuring SQL Server...". Or, for non-Latin based installations, the sentence might be missing completely. The missing sentence should display the following localized string: "The licensing PID was successfully processed. The new edition is [\<Name\> edition]". This string is output for information purposes only, and the next [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Cumulative Update will address this for all languages. This does not affect the successful installation of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] in any way. 

#### Full-Text Search

- Not all filters are available with this release, including filters for Office documents. For a list of supported filters, see [Install SQL Server Full-Text Search on Linux](sql-server-linux-setup-full-text-search.md#filters).

#### <a id="ssis"></a> SQL Server Integration Services (SSIS)

- The **mssql-server-is** package is not supported on SUSE in this release. It is currently supported on Ubuntu and on Red Hat Enterprise Linux (RHEL).

- With [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] on Linux CTP 2.1 Refresh and later, [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages can use ODBC connections on Linux. This functionality has been tested with the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and the MySQL ODBC drivers, but is also expected to work with any Unicode ODBC driver that observes the ODBC specification. At design time, you can provide either a DSN or a connection string to connect to the ODBC data; you can also use Windows authentication. For more info, see the [blog post announcing ODBC support on Linux](https://blogs.msdn.microsoft.com/ssis/2017/06/16/odbc-is-supported-in-ssis-on-linux-ssis-helsinki-ctp2-1-refresh/).

- The following features are not supported in this release when you run SSIS packages on Linux:
  - [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] Catalog database
  - Scheduled package execution by SQL Agent
  - Windows Authentication
  - Third-party components
  - Change Data Capture (CDC)
  - [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] Scale Out
  - Azure Feature Pack for SSIS
  - Hadoop and HDFS support
  - Microsoft Connector for SAP BW

For a list of built-in SSIS components that are not currently supported, or that are supported with limitations, see [Limitations and known issues for SSIS on Linux](sql-server-linux-ssis-known-issues.md#components).

For more info about SSIS on Linux, see the following articles:
-   [Blog post announcing SSIS support for Linux](https://blogs.msdn.microsoft.com/ssis/2017/05/17/ssis-helsinki-is-available-in-sql-server-vnext-ctp2-1/).
-   [Install SQL Server Integration Services (SSIS) on Linux](sql-server-linux-setup-ssis.md)
-   [Extract, transform, and load data on Linux with SSIS](sql-server-linux-migrate-ssis.md)

#### <a id="ssms"></a> SQL Server Management Studio (SSMS)

The following limitations apply to [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] on Windows connected to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux.

- Maintenance plans are not supported.

- Management Data Warehouse (MDW) and the data collector in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] are not supported. 

- [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] UI components that have Windows Authentication or Windows event log options do not work with Linux. You can still use these features with other options, such as SQL logins. 

- Number of log files to retain cannot be modified.

## Next steps

To get started, see the following quickstarts:

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
- [Install on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Run on Docker](quickstart-install-connect-ubuntu.md)
- [Provision a SQL VM in Azure](/azure/virtual-machines/linux/sql/provision-sql-server-linux-virtual-machine?toc=/sql/linux/toc.json)
- [Run & Connect - Cloud](quickstart-install-connect-clouds.md)

For answers to frequently asked questions, see the [SQL Server on Linux FAQ](sql-server-linux-faq.md).
