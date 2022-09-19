---
title: Release notes for SQL Server 2017 on Linux
description: This article contains the release notes and supported features for SQL Server 2017 running on Linux. Release notes are included for the most recent release and several previous releases.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, vanto
ms.date: 07/13/2022
ms.prod: sql
ms.technology: linux
ms.topic: conceptual
---
# Release notes for SQL Server 2017 on Linux

[!INCLUDE [sqlserver2017-linux](../includes/applies-to-version/sqlserver2017-linux.md)]

The following release notes apply to [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] running on Linux. This article is broken into sections for each release. The GA release has detailed supportability and known issues listed. Each cumulative update (CU) or general distribution release (GDR) has a link to a support article describing the CU changes as well as links to the Linux package downloads.

> [!TIP]  
> These release notes are specifically for [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] releases. For more information about the new [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)], see [Release notes for SQL Server 2019 on Linux](sql-server-linux-release-notes-2019.md?view=sql-server-ver15&preserve-view=true).

## Supported platforms

[!INCLUDE [linux-supported-platforms-2017](includes/linux-supported-platforms-2017.md)]

## Tools

Most existing client tools that target [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can seamlessly target [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] running on Linux. Some tools might have a specific version requirement to work well with Linux. For a full list of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tools, see [SQL Tools and Utilities for SQL Server](../tools/overview-sql-tools.md).

## Release history

The following table lists the release history for [!INCLUDE[ssSQL17](../includes/sssql17-md.md)].

| Release               | Version       | Release date |
|-----------------------|---------------|--------------|
| [CU30](#CU30)         | 14.0.3451.2   | 2022-07-13   |
| [CU29-GDR](#CU29-GDR) | 14.0.3445.2   | 2022-06-14   |
| [CU29](#CU29)         | 14.0.3436.1   | 2022-03-30   |
| [CU28](#CU28)         | 14.0.3430.2   | 2022-01-13   |
| [CU27](#CU27)         | 14.0.3421.10  | 2021-10-27   |
| [CU26](#CU26)         | 14.0.3411.3   | 2021-09-14   |
| [CU25](#CU25)         | 14.0.3401.7   | 2021-07-12   |
| [CU24](#CU24)         | 14.0.3391.2   | 2021-05-10   |
| [CU23](#CU23)         | 14.0.3381.3   | 2021-02-24   |
| [CU22-GDR](#CU22)     | 14.0.3370.1   | 2021-01-12   |
| [CU22](#CU22)         | 14.0.3356.20  | 2020-09-10   |
| [CU21](#CU21)         | 14.0.3335.7   | 2020-07-01   |
| [CU20](#CU20)         | 14.0.3294.2   | 2020-04-10   |
| [CU19](#CU19)         | 14.0.3281.6   | 2020-02-05   |
| [CU18](#CU18)         | 14.0.3257.3   | 2019-12-09   |
| [CU17](#CU17)         | 14.0.3238.1   | 2019-10-08   |
| [CU16](#CU16)         | 14.0.3223.3   | 2019-08-01   |
| [CU15](#CU15)         | 14.0.3162.1   | 2019-05-23   |
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

## <a id="CU30"></a> CU30 (July 2022)

This is the Cumulative Update 30 (CU30) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3451.2. For information about the fixes and improvements in this release, see <https://support.microsoft.com/help/5013756>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3451.2-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3451.2-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3451.2-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3451.2-1.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3451.2-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3451.2-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3451.2-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3451.2-1.x86_64.rpm) |
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/)) | 14.0.3451.2-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3451.2-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3451.2-1_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3451.2-1_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU29-GDR"></a> CU29-GDR (June 2022)

This is the Cumulative Update 29-GDR (CU29-GDR) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3445.2. For information about the fixes and improvements in this release, see <https://support.microsoft.com/help/5014553>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3445.2-4 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3445.2-4.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3445.2-4.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3445.2-4.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3445.2-4 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3445.2-4.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3445.2-4.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3445.2-4.x86_64.rpm) |
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/)) | 14.0.3445.2-4 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3445.2-4_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3445.2-4_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3445.2-4_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU29"></a> CU29 (March 2022)

This is the Cumulative Update 29 (CU29) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3436.1. For information about the fixes and improvements in this release, see <https://support.microsoft.com/help/5010786>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3436.1-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3436.1-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3436.1-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3436.1-1.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3436.1-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3436.1-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3436.1-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3436.1-1.x86_64.rpm) |
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/)) | 14.0.3436.1-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3436.1-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3436.1-1_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3436.1-1_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU28"></a> CU28 (January 2022)

This is the Cumulative Update 28 (CU28) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3430.2. For information about the fixes and improvements in this release, see <https://support.microsoft.com/help/5008084>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3430.2-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3430.2-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3430.2-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3430.2-1.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3430.2-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3430.2-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3430.2-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3430.2-1.x86_64.rpm) |
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/)) | 14.0.3430.2-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3430.2-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3430.2-1_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3430.2-1_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU27"></a> CU27 (October 2021)

This is the Cumulative Update 27 (CU27) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3421.10. For information about the fixes and improvements in this release, see <https://support.microsoft.com/help/5006944>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3421.10-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3421.10-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3421.10-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3421.10-2.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3421.10-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3421.10-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3421.10-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3421.10-2.x86_64.rpm) |
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/)) | 14.0.3421.10-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3421.10-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3421.10-2_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3421.10-2_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU26"></a> CU26 (September 2021)

This is the Cumulative Update 26 (CU26) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3411.3. For information about the fixes and improvements in this release, see <https://support.microsoft.com/help/5005226>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3411.3-16 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3411.3-16.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3411.3-16.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3411.3-16.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3411.3-16 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3411.3-16.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3411.3-16.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3411.3-16.x86_64.rpm) |
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/)) | 14.0.3411.3-16 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3411.3-16_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3411.3-16_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3411.3-16_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU25"></a> CU25 (July 2021)

This is the Cumulative Update 25 (CU25) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3401.7. For information about the fixes and improvements in this release, see <https://support.microsoft.com/help/5003830>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3401.7-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3401.7-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3401.7-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3401.7-2.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3401.7-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3401.7-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3401.7-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3401.7-2.x86_64.rpm) |
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/)) | 14.0.3401.7-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3401.7-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3401.7-2_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3401.7-2_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU24"></a> CU24 (May 2021)

This is the Cumulative Update 24 (CU24) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3391.2. For information about the fixes and improvements in this release, see <https://support.microsoft.com/help/5001228>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3391.2-12 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3391.2-12.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3391.2-12.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3391.2-12.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3391.2-12 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3391.2-12.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3391.2-12.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3391.2-12.x86_64.rpm) |
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/)) | 14.0.3391.2-12 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3391.2-12_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3391.2-12_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3391.2-12_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU23"></a> CU23 (February 2021)

This is the Cumulative Update 23 (CU23) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3381.3. For information about the fixes and improvements in this release, see <https://support.microsoft.com/help/5000685>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3381.3-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3381.3-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3381.3-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3381.3-2.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3381.3-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3381.3-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3381.3-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3381.3-2.x86_64.rpm) |
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/)) | 14.0.3381.3-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3381.3-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3381.3-2_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3381.3-2_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU22-GDR"></a> CU22-GDR (January 2021)

This is the Cumulative Update 22-GDR (CU22-GDR) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3370.1. For information about the fixes and improvements in this release, see <https://support.microsoft.com/help/4577467>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3370.1-23-18 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3370.1-18.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3370.1-18.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3370.1-18.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3370.1-18 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3370.1-18.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3370.1-18.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3370.1-18.x86_64.rpm) |
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/)) | 14.0.3370.1-18 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3370.1-18_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3370.1-18_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3370.1-18_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU22"></a> CU22 (September 2020)

This is the Cumulative Update 22 (CU22) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3356.20. For information about the fixes and improvements in this release, see <https://support.microsoft.com/help/4577467>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3356.20-23 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3356.20-23.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3356.20-23.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3356.20-23.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3356.20-23 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3356.20-23.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3356.20-23.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3356.20-23.x86_64.rpm) |
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/)) | 14.0.3356.20-23 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3356.20-23_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3356.20-23_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3356.20-23_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU21"></a> CU21 (July 2020)

This is the Cumulative Update 21 (CU21) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3335.7. For information about the fixes and improvements in this release, see <https://support.microsoft.com/help/4557397>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3335.7-17 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3335.7-17.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3335.7-17.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3335.7-17.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3335.7-17 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3335.7-17.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3335.7-17.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3335.7-17.x86_64.rpm) |
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/)) | 14.0.3335.7-17 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3335.7-17_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3335.7-17_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3335.7-17_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU20"></a> CU20 (April 2020)

This is the Cumulative Update 20 (CU20) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3294.2. For information about the fixes and improvements in this release, see <https://support.microsoft.com/help/4541283>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

> [!NOTE]  
> **Ubuntu 18.04** and **RHEL 8** are now supported on SQL Server 2017 starting with CU20.
>
> The offline package installation links for Ubuntu are pointing to Ubuntu 18.04 packages, except for the SSIS package (which isn't available for Ubuntu 18.04). If you are looking for Ubuntu 16.04 packages, refer to the download path <https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/>.
>
> The offline package installation links for Red Hat are pointing to RHEL 8 packages, except for the SSIS package (which isn't available for RHEL 8). If you are looking for RHEL 7 packages, refer to the download path <https://packages.microsoft.com/rhel/7/mssql-server-2017/>.

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3294.2-27 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3294.2-27.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3294.2-27.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3294.2-27.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3294.2-27 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3294.2-27.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3294.2-27.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3294.2-27.x86_64.rpm) |
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/)) | 14.0.3294.2-27 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3294.2-27_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3294.2-27_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3294.2-27_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU19"></a> CU19 (February 2020)

This is the Cumulative Update 19 (CU19) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3281.6. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4535007](https://support.microsoft.com/help/4535007).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3281.6-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3281.6-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3281.6-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3281.6-2.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3281.6-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3281.6-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3281.6-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3281.6-2.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3281.6-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3281.6-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3281.6-2_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3281.6-2_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU18"></a> CU18 (December 2019)

This is the Cumulative Update 18 (CU18) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3257.3. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4527377](https://support.microsoft.com/help/4527377).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3257.3-13 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3257.3-13.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3257.3-13.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3257.3-13.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3257.3-13 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3257.3-13.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3257.3-13.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3257.3-13.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3257.3-13 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3257.3-13_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3257.3-13_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3257.3-13_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

### Added support

- Change Data Capture (CDC) is supported with SQL Server 2017 on Linux starting with CU18.
- Transactional Replication is supported with SQL Server 2017 on Linux starting with CU18.

### Remarks

SQL Server 2017 containers now have a new tagging pattern as described below with examples.

- `mcr.microsoft.com/mssql/server:<SQL Server Version>-<update>-<Linux Distribution>-<Linux Distribution Version>`

  This will pull the container image with the combination described in the tag.

- `mcr.microsoft.com/mssql/server:<SQL Server Version>-latest`

    This will pull the latest SQL Server version on the latest supported Ubuntu version.

**Examples:**

`mcr.microsoft.com/mssql/server:2017-CU18-ubuntu-16.04`

This will pull SQL Server 2017 CU18 based on the Ubuntu 16.04 container.

`mcr.microsoft.com/mssql/server:2017-latest`

This will pull the latest SQL Server 2017 version (CU18 as the time of this writing) based on the Ubuntu 16.04 container.

> [!NOTE]  
> We will no longer be publishing containers with other tagging patterns for SQL Server 2017 containers in the future.


## <a id="CU17"></a> CU17 (October 2019)

This is the Cumulative Update 17 (CU17) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3238.1. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4515579](https://support.microsoft.com/help/4515579).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3238.1-19 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3238.1-19.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3238.1-19.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3238.1-19.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3238.1-19 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3238.1-19.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3238.1-19.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3238.1-19.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3238.1-19 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3238.1-19_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3238.1-19_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3238.1-19_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU16"></a> CU16 (August 2019)

This is the Cumulative Update 16 (CU16) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3223.3. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4508218](https://support.microsoft.com/help/4508218).

### What's New

|New feature or update | Details |
|:---|:---|
| MSDTC support | Support for the Microsoft Distributed Transaction Coordinator (MSDTC) for SQL Server 2017. For more information, see [How to configure the Microsoft Distributed Transaction Coordinator (MSDTC) on Linux](sql-server-linux-configure-msdtc.md). |

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3223.3-15 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3223.3-15.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3223.3-15.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3223.3-15.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3223.3-15 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3223.3-15.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3223.3-15.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3223.3-15.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3223.3-15 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3223.3-15_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3223.3-15_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3223.3-15_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU15"></a> CU15 (May 2019)

This is the Cumulative Update 15 (CU15) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3162.1. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4498951](https://support.microsoft.com/help/4498951).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3162.1-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3162.1-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3162.1-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3162.1-1.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3162.1-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3162.1-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3162.1-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3162.1-1.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3162.1-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3162.1-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3162.1-1_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3162.1-1_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU14"></a> CU14 (March 2019)

This is the Cumulative Update 14 (CU14) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3076.1. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4484710](https://support.microsoft.com/help/4484710).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3076.1-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3076.1-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3076.1-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3076.1-2.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3076.1-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3076.1-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3076.1-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3076.1-2.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3076.1-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3076.1-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3076.1-2_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3076.1-2_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU13"></a> CU13 (December 2018)

This is the Cumulative Update 13 (CU13) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3048.4. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4466404](https://support.microsoft.com/help/4466404).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3048.4-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3048.4-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3048.4-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3048.4-1.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3048.4-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3048.4-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3048.4-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3048.4-1.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3048.4-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3048.4-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3048.4-1_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3048.4-1_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU12"></a> CU12 (October 2018)

This is the Cumulative Update 12 (CU12) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3045.24. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4464082](https://support.microsoft.com/help/4464082).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3045.24-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3045.24-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3045.24-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3045.24-1.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3045.24-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3045.24-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3045.24-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3045.24-1.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3045.24-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3045.24-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3045.24-1_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3045.24-1_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU11"></a> CU11 (September 2018)

This is the Cumulative Update 11 (CU11) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3038.14. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4462262](https://support.microsoft.com/help/4462262).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3038.14-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3038.14-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3038.14-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3038.14-2.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3038.14-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3038.14-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3038.14-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3038.14-2.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3038.14-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3038.14-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3038.14-2_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3038.14-2_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU10"></a> CU10 (August 2018)

This is the Cumulative Update 10 (CU10) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3037.1. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4342123](https://support.microsoft.com/help/4342123).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3037.1-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3037.1-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3037.1-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3037.1-2.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3037.1-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3037.1-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3037.1-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3037.1-2.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3037.1-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3037.1-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3037.1-2_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3037.1-2_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU9-GDR2"></a> CU9-GDR2 (August 2018)

This is a security update that also includes the previously released CU (CU9) for [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3035.2. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4293805](https://support.microsoft.com/help/4293805).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3035.2-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3035.2-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3035.2-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3035.2-1.x86_64.rpm)|
| **SLES v12 RPM packages** | 14.0.3035.2-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3035.2-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3035.2-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3035.2-1.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3035.2-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3035.2-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3035.2-1_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3035.2-1_amd64.deb)<br/> |

## <a id="GDR2"></a> GDR2 (August 2018)

This is a security update that only includes the GDR2 (and GDR1) security fixes for [!INCLUDE[ssSQL17](../includes/sssql17-md.md)].  The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.2002.14. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4293803](https://support.microsoft.com/help/4293803).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.2002.14-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-14.0.2002.14-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-ha-14.0.2002.14-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-fts-14.0.2002.14-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.2002.14-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-14.0.2002.14-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-ha-14.0.2002.14-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-fts-14.0.2002.14-1.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.2002.14-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server/mssql-server_14.0.2002.14-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.2002.14-1_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.2002.14-1_amd64.deb) |

## <a id="CU9"></a> CU9 (July 2018)

This is the Cumulative Update 9 (CU9) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3030.27. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4341265](https://support.microsoft.com/help/4341265).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3030.27-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3030.27-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3030.27-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3030.27-1.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3030.27-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3030.27-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3030.27-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3030.27-1.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3030.27-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3030.27-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3030.27-1_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3030.27-1_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU8"></a> CU8 (June 2018)

This is the Cumulative Update 8 (CU8) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3029.16. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4338363](https://support.microsoft.com/help/4338363).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3029.16-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3029.16-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3029.16-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3029.16-1.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3029.16-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3029.16-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3029.16-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3029.16-1.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3029.16-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3029.16-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3029.16-1_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3029.16-1_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU7"></a> CU7 (May 2018)

This is the Cumulative Update 7 (CU7) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3026.27. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4229789](https://support.microsoft.com/help/4229789).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3026.27-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3026.27-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3026.27-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3026.27-2.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3026.27-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3026.27-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3026.27-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3026.27-2.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3026.27-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3026.27-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3026.27-2_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3026.27-2_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU6"></a> CU6 (April 2018)

This is the Cumulative Update 6 (CU6) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3025.34. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4101464](https://support.microsoft.com/help/4101464).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3025.34-3 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3025.34-3.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3025.34-3.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3025.34-3.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3025.34-3 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3025.34-3.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3025.34-3.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3025.34-3.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3025.34-3 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3025.34-3_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3025.34-3_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3025.34-3_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU5"></a> CU5 (March 2018)

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
| **RHEL 7.x RPM packages** | 14.0.3023.8-5 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3023.8-5.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3023.8-5.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3023.8-5.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3023.8-5 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3023.8-5.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3023.8-5.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3023.8-5.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3023.8-5 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3023.8-5_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3023.8-5_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3023.8-5_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU4"></a> CU4 (February 2018)

This is the Cumulative Update 4 (CU4) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3022.28. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4056498](https://support.microsoft.com/help/4056498).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

> [!NOTE]  
> As of CU4, SQL Server Agent is no longer installed as a separate package. It is installed with the Engine package and must be enabled to use.

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3022.28-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3022.28-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3022.28-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3022.28-2.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3022.28-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3022.28-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3022.28-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3022.28-2.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3022.28-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3022.28-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3022.28-2_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3022.28-2_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="GDR1"></a> GDR1 (January 2018)

This is a security update that only includes the GDR1 security fixes for [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.2000.63. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4057122](https://support.microsoft.com/help/4057122).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.2000.63-3 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-14.0.2000.63-3.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-ha-14.0.2000.63-3.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-fts-14.0.2000.63-3.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.2000.63-3 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-14.0.2000.63-3.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-ha-14.0.2000.63-3.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-fts-14.0.2000.63-3.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.2000.63-3 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server/mssql-server_14.0.2000.63-3_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.2000.63-3_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.2000.63-3_amd64.deb) |

## <a id="CU3"></a> CU3 (January 2018)

This is the Cumulative Update 3 (CU3) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3015.40. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4052987](https://support.microsoft.com/help/4052987).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3015.40-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3015.40-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3015.40-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3015.40-1.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.3015.40-1.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3015.40-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3015.40-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3015.40-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3015.40-1.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.3015.40-1.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3015.40-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3015.40-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3015.40-1_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3015.40-1_amd64.deb)</br>[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.3015.40-1_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU2"></a> CU2 (November 2017)

This is the Cumulative Update 2 (CU2) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3008.27. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/4052574](https://support.microsoft.com/help/4052574).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3008.27-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3008.27-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3008.27-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3008.27-1.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.3008.27-1.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3008.27-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3008.27-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3008.27-1.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3008.27-1.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.3008.27-1.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3008.27-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3008.27-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3008.27-1_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3008.27-1_amd64.deb)</br>[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.3008.27-1_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="CU1"></a> CU1 (October 2017)

This is the Cumulative Update 1 (CU1) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3006.16. For information about the fixes and improvements in this release, see [https://support.microsoft.com/help/KB4053439](https://support.microsoft.com/help/4038634).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.3006.16-3 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3006.16-3.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3006.16-3.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3006.16-3.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.3006.16-3.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.3006.16-3 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3006.16-3.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3006.16-3.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3006.16-3.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.3006.16-3.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.3006.16-3 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3006.16-3_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3006.16-3_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3006.16-3_amd64.deb)</br>[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.3006.16-3_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## <a id="GA"></a> GA (October 2017)

This is the General Availability (GA) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.1000.169.

### Package details

Package details and download locations for the RPM and Debian packages are listed in the following table. You don't need to download these packages directly if you use the steps in the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md)
- [Install Full-text Search package](sql-server-linux-setup-full-text-search.md)
- [Install SQL Server Agent package](sql-server-linux-setup-sql-agent.md)
- [Install SQL Server Integration Services](sql-server-linux-setup-ssis.md)

| Package | Package version | Downloads |
|-----|-----|-----|
| **RHEL 7.x RPM packages** | 14.0.1000.169-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.1000.169-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.1000.169-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.1000.169-2.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.1000.169-2.x86_64.rpm)</br>[SSIS package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm) |
| **SLES v12 RPM packages** | 14.0.1000.169-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.1000.169-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.1000.169-2.x86_64.rpm)</br>[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.1000.169-2.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.1000.169-2.x86_64.rpm) |
| **Ubuntu 16.04 Debian packages** | 14.0.1000.169-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.1000.169-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.1000.169-2_amd64.deb)</br>[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.1000.169-2_amd64.deb)</br>[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.1000.169-2_amd64.deb)<br/>[SSIS package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb) |

## Known issues

[!INCLUDE [linux-known-issues-2017](includes/linux-known-issues-2017.md)]

## See also

- [SQL Server on Linux FAQ](sql-server-linux-faq.yml)

## Next steps

To get started, see the following quickstarts:

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
- [Install on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Run on Docker](quickstart-install-connect-docker.md)
- [Provision a SQL VM in Azure](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart?toc=/sql/toc/toc.json)
- [Run & Connect - Cloud](quickstart-install-connect-clouds.md)
