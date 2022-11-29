---
title: Release history for SQL Server 2017 on Linux
description: This article contains the release history for SQL Server 2017 running on Linux. Information is included for all Cumulative Updates and GDRs.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, vanto
ms.date: 09/28/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
---
# <a id="release-history"></a> Release history for SQL Server 2017 on Linux

[!INCLUDE [sqlserver2017-linux](../includes/applies-to-version/sqlserver2017-linux.md)]

The following table lists the release history for [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. For release history on other editions, see the following articles:

- [Release history for SQL Server 2019 on Linux](sql-server-linux-release-history-2019.md?view=sql-server-ver15&preserve-view=true).
- [Release history for SQL Server 2022 on Linux](sql-server-linux-release-history-2022.md?view=sql-server-ver16&preserve-view=true).

| Release               | Version       | Release date |
| --------------------- | ------------- | ------------ |
| [CU 31](#CU31)        | 14.0.3456.2   | 2022-09-20   |
| [CU 30](#CU30)        | 14.0.3451.2   | 2022-07-13   |
| [CU 29 GDR](#CU29-GDR) | 14.0.3445.2   | 2022-06-14   |
| [CU 29](#CU29)        | 14.0.3436.1   | 2022-03-30   |
| [CU 28](#CU28)        | 14.0.3430.2   | 2022-01-13   |
| [CU 27](#CU27)        | 14.0.3421.10  | 2021-10-27   |
| [CU 26](#CU26)        | 14.0.3411.3   | 2021-09-14   |
| [CU 25](#CU25)        | 14.0.3401.7   | 2021-07-12   |
| [CU 24](#CU24)        | 14.0.3391.2   | 2021-05-10   |
| [CU 23](#CU23)        | 14.0.3381.3   | 2021-02-24   |
| [CU 22 GDR](#CU22)    | 14.0.3370.1   | 2021-01-12   |
| [GDR 3](#GDR3)        | 14.0.2037.2   | 2021-01-12   |
| [CU 22](#CU22)        | 14.0.3356.20  | 2020-09-10   |
| [CU 21](#CU21)        | 14.0.3335.7   | 2020-07-01   |
| [CU 20](#CU20)        | 14.0.3294.2   | 2020-04-10   |
| [CU 19](#CU19)        | 14.0.3281.6   | 2020-02-05   |
| [CU 18](#CU18)        | 14.0.3257.3   | 2019-12-09   |
| [CU 17](#CU17)        | 14.0.3238.1   | 2019-10-08   |
| [CU 16](#CU16)        | 14.0.3223.3   | 2019-08-01   |
| [CU 15 GDR](#CU15-GDR) | 14.0.3192.2   | 2019-07-09   |
| [CU 15](#CU15)        | 14.0.3162.1   | 2019-05-23   |
| [CU 14](#CU14)        | 14.0.3076.1   | 2019-03-25   |
| [CU 13](#CU13)        | 14.0.3048.4   | 2018-12-18   |
| [CU 12](#CU12)        | 14.0.3045.24  | 2018-10-24   |
| [CU 11](#CU11)        | 14.0.3038.14  | 2018-09-20   |
| [CU 10](#CU10)        | 14.0.3037.1   | 2018-08-27   |
| [CU 9 GDR](#CU9-GDR)  | 14.0.3035.2   | 2018-08-18   |
| [GDR 2](#GDR2)        | 14.0.2002.14  | 2018-08-18   |
| [CU 9](#CU9)          | 14.0.3030.27  | 2018-07-18   |
| [CU 8](#CU8)          | 14.0.3029.16  | 2018-06-21   |
| [CU 7](#CU7)          | 14.0.3026.27  | 2018-05-24   |
| [CU 6](#CU6)          | 14.0.3025.34  | 2018-04-19   |
| [CU 5](#CU5)          | 14.0.3023.8   | 2018-03-20   |
| [CU 4](#CU4)          | 14.0.3022.28  | 2018-02-20   |
| [CU 3](#CU3)          | 14.0.3015.40  | 2018-01-03   |
| [GDR 1](#GDR1)        | 14.0.2000.63  | 2018-01-03   |
| [CU 2](#CU2)          | 14.0.3008.27  | 2017-11-28   |
| [CU 1](#CU1)          | 14.0.3006.16  | 2017-10-24   |
| [GA](#GA)             | 14.0.1000.169 | 2017-10-02   |

## <a id="CU31"></a> CU 31 (September 2022)

This is the Cumulative Update 31 (CU 31) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3456.2. For information about the fixes and improvements in this release, see [KB 5016884](https://support.microsoft.com/help/5016884).

> [!IMPORTANT]  
> This is the final cumulative update for [!INCLUDE[ssSQL17](../includes/sssql17-md.md)].

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3456.2-3 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3456.2-3.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3456.2-3.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3456.2-3.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3456.2-3 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3456.2-3.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3456.2-3.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3456.2-3.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/)) | 14.0.3456.2-3 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3456.2-3_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3456.2-3_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3456.2-3_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU30"></a> CU 30 (July 2022)

This is the Cumulative Update 30 (CU 30) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3451.2. For information about the fixes and improvements in this release, see [KB 5013756](https://support.microsoft.com/help/5013756).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3451.2-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3451.2-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3451.2-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3451.2-1.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3451.2-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3451.2-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3451.2-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3451.2-1.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/)) | 14.0.3451.2-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3451.2-1_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3451.2-1_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3451.2-1_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU29-GDR"></a> CU 29 GDR (June 2022)

This is the Cumulative Update 29-GDR (CU 29 GDR) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. This is a security update that also includes the previously released CU (CU 29). The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3445.2. For information about the fixes and improvements in this release, see [KB 5014553](https://support.microsoft.com/help/5014553).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3445.2-4 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3445.2-4.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3445.2-4.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3445.2-4.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3445.2-4 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3445.2-4.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3445.2-4.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3445.2-4.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/)) | 14.0.3445.2-4 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3445.2-4_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3445.2-4_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3445.2-4_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU29"></a> CU 29 (March 2022)

This is the Cumulative Update 29 (CU 29) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3436.1. For information about the fixes and improvements in this release, see [KB 5010786](https://support.microsoft.com/help/5010786).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3436.1-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3436.1-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3436.1-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3436.1-1.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3436.1-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3436.1-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3436.1-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3436.1-1.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/)) | 14.0.3436.1-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3436.1-1_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3436.1-1_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3436.1-1_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU28"></a> CU 28 (January 2022)

This is the Cumulative Update 28 (CU 28) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3430.2. For information about the fixes and improvements in this release, see [KB 5008084](https://support.microsoft.com/help/5008084).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3430.2-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3430.2-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3430.2-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3430.2-1.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3430.2-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3430.2-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3430.2-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3430.2-1.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/)) | 14.0.3430.2-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3430.2-1_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3430.2-1_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3430.2-1_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU27"></a> CU 27 (October 2021)

This is the Cumulative Update 27 (CU 27) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3421.10. For information about the fixes and improvements in this release, see [KB 5006944](https://support.microsoft.com/help/5006944).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3421.10-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3421.10-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3421.10-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3421.10-2.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3421.10-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3421.10-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3421.10-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3421.10-2.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/)) | 14.0.3421.10-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3421.10-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3421.10-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3421.10-2_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU26"></a> CU 26 (September 2021)

This is the Cumulative Update 26 (CU 26) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3411.3. For information about the fixes and improvements in this release, see [KB 5005226](https://support.microsoft.com/help/5005226).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3411.3-16 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3411.3-16.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3411.3-16.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3411.3-16.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3411.3-16 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3411.3-16.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3411.3-16.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3411.3-16.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/)) | 14.0.3411.3-16 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3411.3-16_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3411.3-16_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3411.3-16_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU25"></a> CU 25 (July 2021)

This is the Cumulative Update 25 (CU 25) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3401.7. For information about the fixes and improvements in this release, see [KB 5003830](https://support.microsoft.com/help/5003830).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3401.7-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3401.7-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3401.7-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3401.7-2.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3401.7-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3401.7-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3401.7-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3401.7-2.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/)) | 14.0.3401.7-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3401.7-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3401.7-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3401.7-2_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU24"></a> CU 24 (May 2021)

This is the Cumulative Update 24 (CU 24) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3391.2. For information about the fixes and improvements in this release, see [KB 5001228](https://support.microsoft.com/help/5001228).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3391.2-12 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3391.2-12.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3391.2-12.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3391.2-12.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3391.2-12 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3391.2-12.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3391.2-12.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3391.2-12.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/)) | 14.0.3391.2-12 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3391.2-12_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3391.2-12_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3391.2-12_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU23"></a> CU 23 (February 2021)

This is the Cumulative Update 23 (CU 23) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3381.3. For information about the fixes and improvements in this release, see [KB 5000685](https://support.microsoft.com/help/5000685).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3381.3-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3381.3-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3381.3-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3381.3-2.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3381.3-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3381.3-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3381.3-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3381.3-2.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/)) | 14.0.3381.3-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3381.3-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3381.3-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3381.3-2_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU22"></a> CU 22 GDR (January 2021)

This is the Cumulative Update 22-GDR (CU 22 GDR) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. This is a security update that also includes the previously released CU (CU 22). The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3370.1. For information about the fixes and improvements in this release, see [KB 4577467](https://support.microsoft.com/help/4577467).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3370.1-18 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3370.1-18.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3370.1-18.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3370.1-18.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3370.1-18 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3370.1-18.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3370.1-18.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3370.1-18.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/)) | 14.0.3370.1-18 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3370.1-18_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3370.1-18_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3370.1-18_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="GDR3"></a> GDR 3 (January 2021)

This is the General Distribution Release GDR3 (GDR 3) of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. This is a security update that only includes fixes for GDR releases. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.2037.2. For information about the fixes and improvements in this release, see [KB 4583456](https://support.microsoft.com/help/4583456).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.2037.2-23 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-14.0.2037.2-23.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-fts-14.0.2037.2-23.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-ha-14.0.2037.2-23.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.2037.2-23 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-14.0.2037.2-23.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-fts-14.0.2037.2-23.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-ha-14.0.2037.2-23.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.2037.2-23 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server/mssql-server_14.0.2037.2-23_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.2037.2-23_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.2037.2-23_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU22"></a> CU 22 (September 2020)

This is the Cumulative Update 22 (CU 22) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3356.20. For information about the fixes and improvements in this release, see [KB 4577467](https://support.microsoft.com/help/4577467).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3356.20-23 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3356.20-23.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3356.20-23.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3356.20-23.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3356.20-23 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3356.20-23.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3356.20-23.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3356.20-23.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/)) | 14.0.3356.20-23 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3356.20-23_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3356.20-23_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3356.20-23_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU21"></a> CU 21 (July 2020)

This is the Cumulative Update 21 (CU 21) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3335.7. For information about the fixes and improvements in this release, see [KB 4557397](https://support.microsoft.com/help/4557397).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3335.7-17 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3335.7-17.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3335.7-17.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3335.7-17.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3335.7-17 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3335.7-17.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3335.7-17.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3335.7-17.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/)) | 14.0.3335.7-17 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3335.7-17_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3335.7-17_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3335.7-17_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU20"></a> CU 20 (April 2020)

This is the Cumulative Update 20 (CU 20) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3294.2. For information about the fixes and improvements in this release, see [KB 4541283](https://support.microsoft.com/help/4541283).

> [!NOTE]  
> **Ubuntu 18.04** and **RHEL 8** are now supported on [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] starting with CU 20.
>
> The offline package installation links for Ubuntu are pointing to Ubuntu 18.04 packages, except for the SSIS package (which isn't available for Ubuntu 18.04). If you are looking for Ubuntu 16.04 packages, refer to the download path <https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/>.
>
> The offline package installation links for Red Hat are pointing to RHEL 8 packages, except for the SSIS package (which isn't available for RHEL 8). If you are looking for RHEL 7 packages, refer to the download path <https://packages.microsoft.com/rhel/7/mssql-server-2017/>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2017/)) | 14.0.3294.2-27 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-14.0.3294.2-27.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-fts-14.0.3294.2-27.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2017/mssql-server-ha-14.0.3294.2-27.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3294.2-27 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3294.2-27.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3294.2-27.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3294.2-27.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/)) | 14.0.3294.2-27 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3294.2-27_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3294.2-27_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3294.2-27_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU19"></a> CU 19 (February 2020)

This is the Cumulative Update 19 (CU 19) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3281.6. For information about the fixes and improvements in this release, see [KB 4535007](https://support.microsoft.com/help/4535007).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3281.6-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3281.6-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3281.6-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3281.6-2.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3281.6-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3281.6-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3281.6-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3281.6-2.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3281.6-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3281.6-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3281.6-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3281.6-2_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU18"></a> CU 18 (December 2019)

This is the Cumulative Update 18 (CU 18) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3257.3. For information about the fixes and improvements in this release, see [KB 4527377](https://support.microsoft.com/help/4527377).

### Added support

- Change Data Capture (CDC) is supported with [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] on Linux starting with CU 18.
- Transactional Replication is supported with [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] on Linux starting with CU 18.

### Remarks

[!INCLUDE[ssSQL17](../includes/sssql17-md.md)] containers now have a new tagging pattern as described below with examples.

- `mcr.microsoft.com/mssql/server:<SQL Server Version>-<update>-<Linux Distribution>-<Linux Distribution Version>`

  This will pull the container image with the combination described in the tag.

- `mcr.microsoft.com/mssql/server:<SQL Server Version>-latest`

  This will pull the latest [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] version on the latest supported Ubuntu version.

#### Examples

- `mcr.microsoft.com/mssql/server:2017-CU18-ubuntu-16.04`

  This will pull [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] CU 18 based on the Ubuntu 16.04 container.

- `mcr.microsoft.com/mssql/server:2017-latest`

  This will pull the latest [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] version (CU 18 as the time of this writing) based on the %%LATESTUBUNTU%% container.

> [!NOTE]
>  
> We will no longer be publishing containers with other tagging patterns for [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] containers in the future.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3257.3-13 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3257.3-13.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3257.3-13.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3257.3-13.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3257.3-13 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3257.3-13.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3257.3-13.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3257.3-13.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3257.3-13 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3257.3-13_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3257.3-13_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3257.3-13_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU17"></a> CU 17 (October 2019)

This is the Cumulative Update 17 (CU 17) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3238.1. For information about the fixes and improvements in this release, see [KB 4515579](https://support.microsoft.com/help/4515579).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3238.1-19 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3238.1-19.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3238.1-19.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3238.1-19.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3238.1-19 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3238.1-19.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3238.1-19.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3238.1-19.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3238.1-19 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3238.1-19_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3238.1-19_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3238.1-19_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU16"></a> CU 16 (August 2019)

This is the Cumulative Update 16 (CU 16) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3223.3. For information about the fixes and improvements in this release, see [KB 4508218](https://support.microsoft.com/help/4508218).

### What's new

| New feature or update | Details |
| --- | --- |
| MSDTC support | Support for the Microsoft Distributed Transaction Coordinator (MSDTC) for [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. For more information, see [How to configure the Microsoft Distributed Transaction Coordinator (MSDTC) on Linux](sql-server-linux-configure-msdtc.md). |

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3223.3-15 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3223.3-15.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3223.3-15.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3223.3-15.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3223.3-15 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3223.3-15.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3223.3-15.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3223.3-15.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3223.3-15 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3223.3-15_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3223.3-15_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3223.3-15_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU15-GDR"></a> CU 15 GDR (July 2019)

This is the Cumulative Update 15-GDR (CU 15 GDR) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. This is a security update that also includes the previously released CU (CU 15). The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3192.2. For information about the fixes and improvements in this release, see [KB 4505225](https://support.microsoft.com/help/4505225).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3192.2-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3192.2-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3192.2-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3192.2-2.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3192.2-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3192.2-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3192.2-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3192.2-2.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3192.2-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3192.2-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3192.2-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3192.2-2_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU15"></a> CU 15 (May 2019)

This is the Cumulative Update 15 (CU 15) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3162.1. For information about the fixes and improvements in this release, see [KB 4498951](https://support.microsoft.com/help/4498951).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3162.1-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3162.1-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3162.1-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3162.1-1.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3162.1-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3162.1-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3162.1-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3162.1-1.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3162.1-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3162.1-1_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3162.1-1_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3162.1-1_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU14"></a> CU 14 (March 2019)

This is the Cumulative Update 14 (CU 14) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3076.1. For information about the fixes and improvements in this release, see [KB 4484710](https://support.microsoft.com/help/4484710).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3076.1-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3076.1-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3076.1-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3076.1-2.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3076.1-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3076.1-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3076.1-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3076.1-2.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3076.1-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3076.1-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3076.1-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3076.1-2_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU13"></a> CU 13 (December 2018)

This is the Cumulative Update 13 (CU 13) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3048.4. For information about the fixes and improvements in this release, see [KB 4466404](https://support.microsoft.com/help/4466404).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3048.4-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3048.4-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3048.4-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3048.4-1.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3048.4-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3048.4-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3048.4-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3048.4-1.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3048.4-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3048.4-1_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3048.4-1_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3048.4-1_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU12"></a> CU 12 (October 2018)

This is the Cumulative Update 12 (CU 12) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3045.24. For information about the fixes and improvements in this release, see [KB 4464082](https://support.microsoft.com/help/4464082).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3045.24-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3045.24-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3045.24-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3045.24-1.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3045.24-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3045.24-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3045.24-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3045.24-1.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3045.24-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3045.24-1_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3045.24-1_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3045.24-1_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU11"></a> CU 11 (September 2018)

This is the Cumulative Update 11 (CU 11) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3038.14. For information about the fixes and improvements in this release, see [KB 4462262](https://support.microsoft.com/help/4462262).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3038.14-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3038.14-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3038.14-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3038.14-2.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3038.14-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3038.14-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3038.14-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3038.14-2.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3038.14-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3038.14-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3038.14-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3038.14-2_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU10"></a> CU 10 (August 2018)

This is the Cumulative Update 10 (CU 10) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3037.1. For information about the fixes and improvements in this release, see [KB 4342123](https://support.microsoft.com/help/4342123).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3037.1-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3037.1-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3037.1-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3037.1-2.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3037.1-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3037.1-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3037.1-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3037.1-2.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3037.1-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3037.1-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3037.1-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3037.1-2_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU9-GDR"></a> CU 9 GDR (August 2018)

This is the Cumulative Update 9-GDR (CU 9 GDR) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. This is a security update that also includes the previously released CU (CU 9). The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3035.2. For information about the fixes and improvements in this release, see [KB 4293805](https://support.microsoft.com/help/4293805).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3035.2-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3035.2-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3035.2-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3035.2-1.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3035.2-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3035.2-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3035.2-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3035.2-1.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3035.2-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3035.2-1_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3035.2-1_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3035.2-1_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="GDR2"></a> GDR 2 (August 2018)

This is the General Distribution Release GDR2 (GDR 2) of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. This is a security update that only includes fixes for GDR releases. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.2002.14. For information about the fixes and improvements in this release, see [KB 4293803](https://support.microsoft.com/help/4293803).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.2002.14-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-14.0.2002.14-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-fts-14.0.2002.14-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-ha-14.0.2002.14-1.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.2002.14-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-14.0.2002.14-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-fts-14.0.2002.14-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-ha-14.0.2002.14-1.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.2002.14-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server/mssql-server_14.0.2002.14-1_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.2002.14-1_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.2002.14-1_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU9"></a> CU 9 (July 2018)

This is the Cumulative Update 9 (CU 9) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3030.27. For information about the fixes and improvements in this release, see [KB 4341265](https://support.microsoft.com/help/4341265).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3030.27-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3030.27-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3030.27-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3030.27-1.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3030.27-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3030.27-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3030.27-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3030.27-1.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3030.27-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3030.27-1_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3030.27-1_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3030.27-1_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU8"></a> CU 8 (June 2018)

This is the Cumulative Update 8 (CU 8) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3029.16. For information about the fixes and improvements in this release, see [KB 4338363](https://support.microsoft.com/help/4338363).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3029.16-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3029.16-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3029.16-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3029.16-1.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3029.16-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3029.16-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3029.16-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3029.16-1.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3029.16-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3029.16-1_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3029.16-1_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3029.16-1_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU7"></a> CU 7 (May 2018)

This is the Cumulative Update 7 (CU 7) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3026.27. For information about the fixes and improvements in this release, see [KB 4229789](https://support.microsoft.com/help/4229789).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3026.27-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3026.27-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3026.27-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3026.27-2.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3026.27-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3026.27-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3026.27-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3026.27-2.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3026.27-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3026.27-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3026.27-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3026.27-2_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU6"></a> CU 6 (April 2018)

This is the Cumulative Update 6 (CU 6) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3025.34. For information about the fixes and improvements in this release, see [KB 4101464](https://support.microsoft.com/help/4101464).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3025.34-3 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3025.34-3.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3025.34-3.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3025.34-3.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3025.34-3 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3025.34-3.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3025.34-3.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3025.34-3.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3025.34-3 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3025.34-3_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3025.34-3_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3025.34-3_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU5"></a> CU 5 (March 2018)

This is the Cumulative Update 5 (CU 5) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3023.8. For information about the fixes and improvements in this release, see [KB 4092643](https://support.microsoft.com/help/4092643).

### Known upgrade issue

When you upgrade from a previous release to CU 5, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] might fail to start with the following error:

```output
Error: 4860, Severity: 16, State: 1.
Cannot bulk load. The file "C:\Install\SqlTraceCollect.dtsx" does not exist or you don't have file access rights.
Error: 912, Severity: 21, State: 2.
Script level upgrade for database 'master' failed because upgrade step 'msdb110_upgrade.sql' encountered error 200, state
```

To resolve this error, enable [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent and restart [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with the following commands:

```bash
sudo /opt/mssql/bin/mssql-conf set sqlagent.enabled true
sudo systemctl start mssql-server
```

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3023.8-5 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3023.8-5.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3023.8-5.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3023.8-5.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3023.8-5 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3023.8-5.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3023.8-5.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3023.8-5.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3023.8-5 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3023.8-5_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3023.8-5_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3023.8-5_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU4"></a> CU 4 (February 2018)

This is the Cumulative Update 4 (CU 4) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3022.28. For information about the fixes and improvements in this release, see [KB 4056498](https://support.microsoft.com/help/4056498).

> [!NOTE]  
> As of CU 4, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent is no longer installed as a separate package. It is installed with the Database Engine package and must be enabled to use.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3022.28-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3022.28-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3022.28-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3022.28-2.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3022.28-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3022.28-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3022.28-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3022.28-2.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3022.28-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3022.28-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3022.28-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3022.28-2_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU3"></a> CU 3 (January 2018)

This is the Cumulative Update 3 (CU 3) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3015.40. For information about the fixes and improvements in this release, see [KB 4052987](https://support.microsoft.com/help/4052987).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3015.40-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3015.40-1.x86_64.rpm)<br />[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.3015.40-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3015.40-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3015.40-1.x86_64.rpm)<br />[SSIS RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.3015.40-1.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3015.40-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3015.40-1.x86_64.rpm)<br />[SQL Server Agent RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-agent-14.0.3015.40-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3015.40-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3015.40-1.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3015.40-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3015.40-1_amd64.deb)<br />[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.3015.40-1_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3015.40-1_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3015.40-1_amd64.deb)<br />[SSIS Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.3015.40-1_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="GDR1"></a> GDR 1 (January 2018)

This is the General Distribution Release GDR1 (GDR 1) of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. This is a security update that only includes fixes for GDR releases. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.2000.63. For information about the fixes and improvements in this release, see [KB 4057122](https://support.microsoft.com/help/4057122).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.2000.63-3 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-14.0.2000.63-3.x86_64.rpm)<br />[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-agent-14.0.2000.63-3.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-fts-14.0.2000.63-3.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017-gdr/mssql-server-ha-14.0.2000.63-3.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.2000.63-3 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-14.0.2000.63-3.x86_64.rpm)<br />[SQL Server Agent RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-agent-14.0.2000.63-3.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-fts-14.0.2000.63-3.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017-gdr/mssql-server-ha-14.0.2000.63-3.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.2000.63-3 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server/mssql-server_14.0.2000.63-3_amd64.deb)<br />[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.2000.63-3_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.2000.63-3_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.2000.63-3_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU2"></a> CU 2 (November 2017)

This is the Cumulative Update 2 (CU 2) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3008.27. For information about the fixes and improvements in this release, see [KB 4052574](https://support.microsoft.com/help/4052574).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3008.27-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3008.27-1.x86_64.rpm)<br />[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.3008.27-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3008.27-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3008.27-1.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3008.27-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3008.27-1.x86_64.rpm)<br />[SQL Server Agent RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-agent-14.0.3008.27-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3008.27-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3008.27-1.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3008.27-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3008.27-1_amd64.deb)<br />[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.3008.27-1_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3008.27-1_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3008.27-1_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU1"></a> CU 1 (October 2017)

This is the Cumulative Update 1 (CU 1) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.3006.16. For information about the fixes and improvements in this release, see [KB 4038634](https://support.microsoft.com/help/4038634).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.3006.16-3 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.3006.16-3.x86_64.rpm)<br />[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.3006.16-3.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.3006.16-3.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.3006.16-3.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.3006.16-3 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.3006.16-3.x86_64.rpm)<br />[SQL Server Agent RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-agent-14.0.3006.16-3.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.3006.16-3.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.3006.16-3.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.3006.16-3 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.3006.16-3_amd64.deb)<br />[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.3006.16-3_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.3006.16-3_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.3006.16-3_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="GA"></a> GA (October 2017)

This is the General Availability (GA) release of [!INCLUDE[ssSQL17](../includes/sssql17-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 14.0.1000.169.

### Package details

Package details and download locations for the RPM and Debian packages are listed in the following table. You don't need to download these packages directly if you use the steps in the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md)
- [Install Full-Text Search package](sql-server-linux-setup-full-text-search.md)
- [Install SQL Server Agent package](sql-server-linux-setup-sql-agent.md)
- [Install SQL Server Integration Services](sql-server-linux-setup-ssis.md)

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 14.0.1000.169-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-14.0.1000.169-2.x86_64.rpm)<br />[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-agent-14.0.1000.169-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-fts-14.0.1000.169-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-ha-14.0.1000.169-2.x86_64.rpm)<br />[SSIS RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2017/mssql-server-is-14.0.1000.169-1.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 14.0.1000.169-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-14.0.1000.169-2.x86_64.rpm)<br />[SQL Server Agent RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-agent-14.0.1000.169-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-fts-14.0.1000.169-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2017/mssql-server-ha-14.0.1000.169-2.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 14.0.1000.169-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server/mssql-server_14.0.1000.169-2_amd64.deb)<br />[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.1000.169-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.1000.169-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.1000.169-2_amd64.deb)<br />[SSIS Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017/pool/main/m/mssql-server-is/mssql-server-is_14.0.1000.169-1_amd64.deb)<br />|

Go back to the [release history](#release-history).

## See also

- [SQL Server on Linux FAQ](sql-server-linux-faq.yml)

## Next steps

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
- [Install on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Run on Docker](quickstart-install-connect-docker.md)
- [Provision a SQL VM in Azure](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart)
- [Run & Connect - Cloud](quickstart-install-connect-clouds.md)

