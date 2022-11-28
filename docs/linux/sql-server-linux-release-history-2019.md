---
title: Release history for SQL Server 2019 on Linux
description: This article contains the release history for SQL Server 2019 running on Linux. Information is included for all Cumulative Updates and GDRs.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, vanto
ms.date: 09/28/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
---
# <a id="release-history"></a> Release history for SQL Server 2019 on Linux

[!INCLUDE [sqlserver2019-linux](../includes/applies-to-version/sqlserver2019-linux.md)]

The following table lists the release history for [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. For release history on other editions, see the following articles:

- [Release history for SQL Server 2017 on Linux](sql-server-linux-release-history-2017.md?view=sql-server-ver14&preserve-view=true).
- [Release history for SQL Server 2022 on Linux](sql-server-linux-release-history-2022.md?view=sql-server-ver16&preserve-view=true).

| Release               | Version       | Release date |
| --------------------- | ------------- | ------------ |
| [CU 18](#CU18)        | 15.0.4261.1   | 2022-09-28   |
| [CU 17](#CU17)        | 15.0.4249.2   | 2022-08-11   |
| [CU 16 GDR](#CU16-GDR) | 15.0.4236.7   | 2022-06-14   |
| [CU 16](#CU16)        | 15.0.4223.1   | 2022-04-18   |
| [CU 15](#CU15)        | 15.0.4198.2   | 2022-01-27   |
| [CU 14](#CU14)        | 15.0.4188.2   | 2021-11-22   |
| [CU 13](#CU13)        | 15.0.4178.1   | 2021-10-05   |
| [CU 12](#CU12)        | 15.0.4153.1   | 2021-08-04   |
| [CU 11](#CU11)        | 15.0.4138.2   | 2021-06-10   |
| [CU 10](#CU10)        | 15.0.4123.1   | 2021-04-06   |
| [CU 9](#CU9)          | 15.0.4102.2   | 2021-02-10   |
| [CU 8 GDR](#CU8-GDR)  | 15.0.4083.2   | 2021-01-12   |
| [GDR 1](#GDR1)        | 15.0.2080.9   | 2021-01-12   |
| [CU 8](#CU8)          | 15.0.4073.23  | 2020-10-07   |
| [CU 7 (Removed)](https://support.microsoft.com/help/4570012) | 15.0.4063.15  | 2020-09-02   |
| [CU 6](#CU6)          | 15.0.4053.23  | 2020-08-04   |
| [CU 5](#CU5)          | 15.0.4043.16  | 2020-06-22   |
| [CU 4](#CU4)          | 15.0.4033.1   | 2020-03-31   |
| [CU 3](#CU3)          | 15.0.4023.6   | 2020-03-12   |
| [CU 2](#CU2)          | 15.0.4013.40  | 2020-02-13   |
| [CU 1](#CU1)          | 15.0.4003.23  | 2020-01-07   |
| [GA](#GA)             | 15.0.2000.5   | 2019-11-04   |

## <a id="CU18"></a> CU 18 (September 2022)

This is the Cumulative Update 18 (CU 18) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4261.1. For information about the fixes and improvements in this release, see [KB 5017593](https://support.microsoft.com/help/5017593).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4261.1-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4261.1-2.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4261.1-2.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4261.1-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4261.1-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4261.1-2.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4261.1-2.x86_64.rpm)<br />|
| **SLES 15 RPM packages**<br /><br />(Get [SLES 12 RPM packages](https://packages.microsoft.com/sles/12/mssql-server-2019/)) | 15.0.4261.1-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-15.0.4261.1-2.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-extensibility-15.0.4261.1-2.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-extensibility-java-15.0.4261.1-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-fts-15.0.4261.1-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-ha-15.0.4261.1-2.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-polybase-15.0.4261.1-2.x86_64.rpm)<br />|
| **Ubuntu 20.04 Debian packages**<br /><br />(Get [Ubuntu 18.04 Debian packages](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.4261.1-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4261.1-2_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4261.1-2_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4261.1-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4261.1-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4261.1-2_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4261.1-2_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU17"></a> CU 17 (August 2022)

This is the Cumulative Update 17 (CU 17) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4249.2. For information about the fixes and improvements in this release, see [KB 5016394](https://support.microsoft.com/help/5016394).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4249.2-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4249.2-1.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4249.2-1.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4249.2-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4249.2-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4249.2-1.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4249.2-1.x86_64.rpm)<br />|
| **SLES 15 RPM packages**<br /><br />(Get [SLES 12 RPM packages](https://packages.microsoft.com/sles/12/mssql-server-2019/)) | 15.0.4249.2-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-15.0.4249.2-1.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-extensibility-15.0.4249.2-1.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-extensibility-java-15.0.4249.2-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-fts-15.0.4249.2-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-ha-15.0.4249.2-1.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-polybase-15.0.4249.2-1.x86_64.rpm)<br />|
| **Ubuntu 20.04 Debian packages**<br /><br />(Get [Ubuntu 18.04 Debian packages](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.4249.2-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4249.2-1_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4249.2-1_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4249.2-1_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4249.2-1_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4249.2-1_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4249.2-1_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU16-GDR"></a> CU 16 GDR (June 2022)

This is the Cumulative Update 16-GDR (CU 16 GDR) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. This is a security update that also includes the previously released CU (CU 16). The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4236.7. For information about the fixes and improvements in this release, see [KB 5014553](https://support.microsoft.com/help/5014553).

> [!IMPORTANT]  
> TDE-compressed backups that are made using [!INCLUDE[ssSQL19](../includes/sssql19-md.md)] CU 16 and later cannot be restored to previous CU versions of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. For more information, see [FIX: Error 3241 occurs during executing RESTORE LOG or RESTORE DATABASE](https://support.microsoft.com/help/5014298).
>
> Transparent Data Encryption (TDE)-compressed backups that are made using previous CU versions of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)] can still be restored by using [!INCLUDE[ssSQL19](../includes/sssql19-md.md)] CU 16 and later versions.

> [!WARNING]  
> The **adutil** tool has been removed from [!INCLUDE[ssSQL19](../includes/sssql19-md.md)] on Linux for CU 16, and will be reintroduced in a future Cumulative Update. However, you can still [install **adutil** manually](sql-server-linux-ad-auth-adutil-introduction.md#install-adutil).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4236.7-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4236.7-1.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4236.7-1.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4236.7-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4236.7-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4236.7-1.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4236.7-1.x86_64.rpm)<br />|
| **SLES 15 RPM packages**<br /><br />(Get [SLES 12 RPM packages](https://packages.microsoft.com/sles/12/mssql-server-2019/)) | 15.0.4236.7-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-15.0.4236.7-1.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-extensibility-15.0.4236.7-1.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-extensibility-java-15.0.4236.7-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-fts-15.0.4236.7-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-ha-15.0.4236.7-1.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-polybase-15.0.4236.7-1.x86_64.rpm)<br />|
| **Ubuntu 20.04 Debian packages**<br /><br />(Get [Ubuntu 18.04 Debian packages](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.4236.7-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4236.7-1_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4236.7-1_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4236.7-1_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4236.7-1_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4236.7-1_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4236.7-1_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU16"></a> CU 16 (April 2022)

This is the Cumulative Update 16 (CU 16) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4223.1. For information about the fixes and improvements in this release, see [KB 5011644](https://support.microsoft.com/help/5011644).

> [!IMPORTANT]  
> TDE-compressed backups that are made using [!INCLUDE[ssSQL19](../includes/sssql19-md.md)] CU 16 and later cannot be restored to previous CU versions of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. For more information, see [FIX: Error 3241 occurs during executing RESTORE LOG or RESTORE DATABASE](https://support.microsoft.com/help/5014298).
>
> Transparent Data Encryption (TDE)-compressed backups that are made using previous CU versions of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)] can still be restored by using [!INCLUDE[ssSQL19](../includes/sssql19-md.md)] CU 16 and later versions.

> [!WARNING]  
> The **adutil** tool has been removed from [!INCLUDE[ssSQL19](../includes/sssql19-md.md)] on Linux for CU 16, and will be reintroduced in a future Cumulative Update. However, you can still [install **adutil** manually](sql-server-linux-ad-auth-adutil-introduction.md#install-adutil).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4223.1-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4223.1-2.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4223.1-2.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4223.1-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4223.1-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4223.1-2.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4223.1-2.x86_64.rpm)<br />|
| **SLES 15 RPM packages**<br /><br />(Get [SLES 12 RPM packages](https://packages.microsoft.com/sles/12/mssql-server-2019/)) | 15.0.4223.1-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-15.0.4223.1-2.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-extensibility-15.0.4223.1-2.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-extensibility-java-15.0.4223.1-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-fts-15.0.4223.1-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-ha-15.0.4223.1-2.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-polybase-15.0.4223.1-2.x86_64.rpm)<br />|
| **Ubuntu 20.04 Debian packages**<br /><br />(Get [Ubuntu 18.04 Debian packages](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.4223.1-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4223.1-2_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4223.1-2_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4223.1-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4223.1-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4223.1-2_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4223.1-2_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU15"></a> CU 15 (January 2022)

This is the Cumulative Update 15 (CU 15) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4198.2. For information about the fixes and improvements in this release, see [KB 5008996](https://support.microsoft.com/help/5008996).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4198.2-10 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4198.2-10.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4198.2-10.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4198.2-10.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4198.2-10.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4198.2-10.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4198.2-10.x86_64.rpm)<br />|
| **SLES 15 RPM packages**<br /><br />(Get [SLES 12 RPM packages](https://packages.microsoft.com/sles/12/mssql-server-2019/)) | 15.0.4198.2-10 | [Database Engine RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-15.0.4198.2-10.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-extensibility-15.0.4198.2-10.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-extensibility-java-15.0.4198.2-10.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-fts-15.0.4198.2-10.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-ha-15.0.4198.2-10.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-polybase-15.0.4198.2-10.x86_64.rpm)<br />|
| **Ubuntu 20.04 Debian packages**<br /><br />(Get [Ubuntu 18.04 Debian packages](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.4198.2-10 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4198.2-10_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4198.2-10_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4198.2-10_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4198.2-10_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4198.2-10_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4198.2-10_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU14"></a> CU 14 (November 2021)

This is the Cumulative Update 14 (CU 14) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4188.2. For information about the fixes and improvements in this release, see [KB 5007182](https://support.microsoft.com/help/5007182).

> [!NOTE]  
> **SLES v15** is now supported on [!INCLUDE[ssSQL19](../includes/sssql19-md.md)] starting with CU 14. The offline package installation links for SLES are pointing to SLES v15 packages. If you are looking for SLES v12 packages, refer to the download path <https://packages.microsoft.com/sles/12/mssql-server-2019/>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4188.2-3 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4188.2-3.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4188.2-3.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4188.2-3.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4188.2-3.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4188.2-3.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4188.2-3.x86_64.rpm)<br />|
| **SLES 15 RPM packages**<br /><br />(Get [SLES 12 RPM packages](https://packages.microsoft.com/sles/12/mssql-server-2019/)) | 15.0.4188.2-3 | [Database Engine RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-15.0.4188.2-3.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-extensibility-15.0.4188.2-3.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-extensibility-java-15.0.4188.2-3.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-fts-15.0.4188.2-3.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-ha-15.0.4188.2-3.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/15/mssql-server-2019/mssql-server-polybase-15.0.4188.2-3.x86_64.rpm)<br />|
| **Ubuntu 20.04 Debian packages**<br /><br />(Get [Ubuntu 18.04 Debian packages](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.4188.2-3 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4188.2-3_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4188.2-3_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4188.2-3_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4188.2-3_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4188.2-3_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4188.2-3_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU13"></a> CU 13 (October 2021)

This is the Cumulative Update 13 (CU 13) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4178.1. For information about the fixes and improvements in this release, see [KB 5005679](https://support.microsoft.com/help/5005679).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4178.1-3 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4178.1-3.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4178.1-3.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4178.1-3.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4178.1-3.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4178.1-3.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4178.1-3.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 15.0.4178.1-3 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-15.0.4178.1-3.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-15.0.4178.1-3.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-java-15.0.4178.1-3.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-fts-15.0.4178.1-3.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-ha-15.0.4178.1-3.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-polybase-15.0.4178.1-3.x86_64.rpm)<br />|
| **Ubuntu 20.04 Debian packages**<br /><br />(Get [Ubuntu 18.04 Debian packages](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.4178.1-3 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4178.1-3_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4178.1-3_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4178.1-3_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4178.1-3_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4178.1-3_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4178.1-3_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU12"></a> CU 12 (August 2021)

This is the Cumulative Update 12 (CU 12) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4153.1. For information about the fixes and improvements in this release, see [KB 5004524](https://support.microsoft.com/help/5004524).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4153.1-6 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4153.1-6.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4153.1-6.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4153.1-6.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4153.1-6.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4153.1-6.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4153.1-6.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 15.0.4153.1-6 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-15.0.4153.1-6.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-15.0.4153.1-6.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-java-15.0.4153.1-6.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-fts-15.0.4153.1-6.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-ha-15.0.4153.1-6.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-polybase-15.0.4153.1-6.x86_64.rpm)<br />|
| **Ubuntu 20.04 Debian packages**<br /><br />(Get [Ubuntu 18.04 Debian packages](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.4153.1-6 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4153.1-6_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4153.1-6_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4153.1-6_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4153.1-6_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4153.1-6_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4153.1-6_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU11"></a> CU 11 (June 2021)

This is the Cumulative Update 11 (CU 11) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4138.2. For information about the fixes and improvements in this release, see [KB 5003249](https://support.microsoft.com/help/5003249).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4138.2-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4138.2-1.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4138.2-1.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4138.2-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4138.2-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4138.2-1.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4138.2-1.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 15.0.4138.2-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-15.0.4138.2-1.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-15.0.4138.2-1.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-java-15.0.4138.2-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-fts-15.0.4138.2-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-ha-15.0.4138.2-1.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-polybase-15.0.4138.2-1.x86_64.rpm)<br />|
| **Ubuntu 20.04 Debian packages**<br /><br />(Get [Ubuntu 18.04 Debian packages](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.4138.2-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4138.2-1_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4138.2-1_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4138.2-1_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4138.2-1_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4138.2-1_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4138.2-1_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU10"></a> CU 10 (April 2021)

This is the Cumulative Update 10 (CU 10) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4123.1. For information about the fixes and improvements in this release, see [KB 5001090](https://support.microsoft.com/help/5001090).

> [!NOTE]  
> **Ubuntu 20.04** is now supported on [!INCLUDE[ssSQL19](../includes/sssql19-md.md)] starting with CU 10. The offline package installation links for Ubuntu are pointing to Ubuntu 20.04 packages. If you are looking for Ubuntu 18.04 packages, refer to the download path <https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4123.1-5 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4123.1-5.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4123.1-5.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4123.1-5.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4123.1-5.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4123.1-5.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4123.1-5.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 15.0.4123.1-5 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-15.0.4123.1-5.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-15.0.4123.1-5.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-java-15.0.4123.1-5.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-fts-15.0.4123.1-5.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-ha-15.0.4123.1-5.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-polybase-15.0.4123.1-5.x86_64.rpm)<br />|
| **Ubuntu 20.04 Debian packages**<br /><br />(Get [Ubuntu 18.04 Debian packages](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.4123.1-5 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4123.1-5_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4123.1-5_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4123.1-5_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4123.1-5_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4123.1-5_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4123.1-5_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU9"></a> CU 9 (February 2021)

This is the Cumulative Update 9 (CU 9) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4102.2. For information about the fixes and improvements in this release, see [KB 5000642](https://support.microsoft.com/help/5000642).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4102.2-4 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4102.2-4.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4102.2-4.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4102.2-4.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4102.2-4.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4102.2-4.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4102.2-4.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 15.0.4102.2-4 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-15.0.4102.2-4.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-15.0.4102.2-4.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-java-15.0.4102.2-4.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-fts-15.0.4102.2-4.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-ha-15.0.4102.2-4.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-polybase-15.0.4102.2-4.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.4102.2-4 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4102.2-4_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4102.2-4_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4102.2-4_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4102.2-4_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4102.2-4_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4102.2-4_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU8-GDR"></a> CU 8 GDR (January 2021)

This is the Cumulative Update 8-GDR (CU 8 GDR) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. This is a security update that also includes the previously released CU (CU 8). The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4083.2. For information about the fixes and improvements in this release, see [KB 4577194](https://support.microsoft.com/help/4577194).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4083.2-15 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4083.2-15.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4083.2-15.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4083.2-15.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4083.2-15.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4083.2-15.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4083.2-15.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 15.0.4083.2-15 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-15.0.4083.2-15.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-15.0.4083.2-15.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-java-15.0.4083.2-15.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-fts-15.0.4083.2-15.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-ha-15.0.4083.2-15.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-polybase-15.0.4083.2-15.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.4083.2-15 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4083.2-15_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4083.2-15_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4083.2-15_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4083.2-15_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4083.2-15_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4083.2-15_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="GDR1"></a> GDR 1 (January 2021)

This is the General Distribution Release GDR1 (GDR 1) of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. This is a security update that only includes fixes for GDR releases. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.2080.9. For information about the fixes and improvements in this release, see [KB 4583458](https://support.microsoft.com/help/4583458).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 15.0.2080.9-4 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2019-gdr/mssql-server-15.0.2080.9-4.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2019-gdr/mssql-server-extensibility-15.0.2080.9-4.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2019-gdr/mssql-server-extensibility-java-15.0.2080.9-4.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2019-gdr/mssql-server-fts-15.0.2080.9-4.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2019-gdr/mssql-server-ha-15.0.2080.9-4.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2019-gdr/mssql-server-polybase-15.0.2080.9-4.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 15.0.2080.9-4 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019-gdr/mssql-server-15.0.2080.9-4.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019-gdr/mssql-server-extensibility-15.0.2080.9-4.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019-gdr/mssql-server-extensibility-java-15.0.2080.9-4.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019-gdr/mssql-server-fts-15.0.2080.9-4.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019-gdr/mssql-server-ha-15.0.2080.9-4.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019-gdr/mssql-server-polybase-15.0.2080.9-4.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 15.0.2080.9-4 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019-gdr/pool/main/m/mssql-server/mssql-server_15.0.2080.9-4_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019-gdr/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.2080.9-4_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019-gdr/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.2080.9-4_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019-gdr/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.2080.9-4_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019-gdr/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.2080.9-4_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019-gdr/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.2080.9-4_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU8"></a> CU 8 (October 2020)

This is the Cumulative Update 8 (CU 8) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4073.23. For information about the fixes and improvements in this release, see [KB 4577194](https://support.microsoft.com/help/4577194).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4073.23-4 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4073.23-4.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4073.23-4.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4073.23-4.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4073.23-4.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4073.23-4.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4073.23-4.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 15.0.4073.23-4 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-15.0.4073.23-4.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-15.0.4073.23-4.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-java-15.0.4073.23-4.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-fts-15.0.4073.23-4.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-ha-15.0.4073.23-4.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-polybase-15.0.4073.23-4.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.4073.23-4 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4073.23-4_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4073.23-4_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4073.23-4_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4073.23-4_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4073.23-4_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4073.23-4_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU6"></a> CU 6 (August 2020)

This is the Cumulative Update 6 (CU 6) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4053.23. For information about the fixes and improvements in this release, see [KB 4563110](https://support.microsoft.com/help/4563110).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4053.23-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4053.23-2.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4053.23-2.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4053.23-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4053.23-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4053.23-2.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4053.23-2.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 15.0.4053.23-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-15.0.4053.23-2.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-15.0.4053.23-2.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-java-15.0.4053.23-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-fts-15.0.4053.23-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-ha-15.0.4053.23-2.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-polybase-15.0.4053.23-2.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.4053.23-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4053.23-2_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4053.23-2_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4053.23-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4053.23-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4053.23-2_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4053.23-2_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU5"></a> CU 5 (June 2020)

This is the Cumulative Update 5 (CU 5) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4043.16. For information about the fixes and improvements in this release, see [KB 4552255](https://support.microsoft.com/help/4552255).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4043.16-4 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4043.16-4.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4043.16-4.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4043.16-4.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4043.16-4.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4043.16-4.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4043.16-4.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 15.0.4043.16-4 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-15.0.4043.16-4.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-15.0.4043.16-4.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-java-15.0.4043.16-4.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-fts-15.0.4043.16-4.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-ha-15.0.4043.16-4.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-polybase-15.0.4043.16-4.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.4043.16-4 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4043.16-4_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4043.16-4_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4043.16-4_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4043.16-4_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4043.16-4_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4043.16-4_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU4"></a> CU 4 (March 2020)

This is the Cumulative Update 4 (CU 4) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4033.1. For information about the fixes and improvements in this release, see [KB 4548597](https://support.microsoft.com/help/4548597).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4033.1-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4033.1-2.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4033.1-2.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4033.1-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4033.1-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4033.1-2.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4033.1-2.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 15.0.4033.1-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-15.0.4033.1-2.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-15.0.4033.1-2.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-java-15.0.4033.1-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-fts-15.0.4033.1-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-ha-15.0.4033.1-2.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-polybase-15.0.4033.1-2.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.4033.1-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4033.1-2_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4033.1-2_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4033.1-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4033.1-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4033.1-2_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4033.1-2_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU3"></a> CU 3 (March 2020)

This is the Cumulative Update 3 (CU 3) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4023.6. For information about the fixes and improvements in this release, see [KB 4538853](https://support.microsoft.com/help/4538853).

> [!NOTE]  
> **Ubuntu 18.04** is now supported on [!INCLUDE[ssSQL19](../includes/sssql19-md.md)] starting with CU 3. The offline package installation links for Ubuntu are pointing to Ubuntu 18.04 packages. If you are looking for Ubuntu 16.04 packages, refer to the download path <https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4023.6-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4023.6-2.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4023.6-2.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4023.6-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4023.6-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4023.6-2.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4023.6-2.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 15.0.4023.6-2 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-15.0.4023.6-2.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-15.0.4023.6-2.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-java-15.0.4023.6-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-fts-15.0.4023.6-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-ha-15.0.4023.6-2.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-polybase-15.0.4023.6-2.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.4023.6-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4023.6-2_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4023.6-2_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4023.6-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4023.6-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4023.6-2_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4023.6-2_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU2"></a> CU 2 (February 2020)

This is the Cumulative Update 2 (CU 2) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4013.40. For information about the fixes and improvements in this release, see [KB 4536075](https://support.microsoft.com/help/4536075).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4013.40-8 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4013.40-8.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4013.40-8.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4013.40-8.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4013.40-8.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4013.40-8.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4013.40-8.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 15.0.4013.40-8 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-15.0.4013.40-8.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-15.0.4013.40-8.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-java-15.0.4013.40-8.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-fts-15.0.4013.40-8.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-ha-15.0.4013.40-8.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-polybase-15.0.4013.40-8.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 15.0.4013.40-8 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4013.40-8_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4013.40-8_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4013.40-8_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4013.40-8_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4013.40-8_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4013.40-8_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CU1"></a> CU 1 (January 2020)

This is the Cumulative Update 1 (CU 1) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.4003.23. For information about the fixes and improvements in this release, see [KB 4527376](https://support.microsoft.com/help/4527376).

> [!NOTE]  
> **RHEL 8** is now supported on [!INCLUDE[ssSQL19](../includes/sssql19-md.md)] starting with CU 1. The offline package installation links for RHEL are pointing to RHEL 8 packages. If you are looking for RHEL 7 packages, refer to the download path <https://packages.microsoft.com/rhel/7/mssql-server-2019/>.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages**<br /><br />(Get [RHEL 7.x RPM packages](https://packages.microsoft.com/rhel/7/mssql-server-2019/)) | 15.0.4003.23-3 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-15.0.4003.23-3.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-15.0.4003.23-3.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-extensibility-java-15.0.4003.23-3.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-fts-15.0.4003.23-3.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-ha-15.0.4003.23-3.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2019/mssql-server-polybase-15.0.4003.23-3.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 15.0.4003.23-3 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-15.0.4003.23-3.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-15.0.4003.23-3.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-java-15.0.4003.23-3.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-fts-15.0.4003.23-3.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-ha-15.0.4003.23-3.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-polybase-15.0.4003.23-3.x86_64.rpm)<br />|
| **Ubuntu 16.04 Debian packages** | 15.0.4003.23-3 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server/mssql-server_15.0.4003.23-3_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_15.0.4003.23-3_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_15.0.4003.23-3_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server-fts/mssql-server-fts_15.0.4003.23-3_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server-ha/mssql-server-ha_15.0.4003.23-3_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server-polybase/mssql-server-polybase_15.0.4003.23-3_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="GA"></a> GA (November 2019)

This is the General Availability (GA) release of [!INCLUDE[ssSQL19](../includes/sssql19-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 15.0.2000.5.

### Package details

Package details and download locations for the RPM and Debian packages are listed in the following table. You don't need to download these packages directly if you use the steps in the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md)
- [Install Full-Text Search package](sql-server-linux-setup-full-text-search.md)
- [Install SQL Server Agent package](sql-server-linux-setup-sql-agent.md)
- [Install SQL Server Integration Services](sql-server-linux-setup-ssis.md)

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 7.x RPM packages** | 15.0.2000.5-5 | [Database Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2019/mssql-server-15.0.2000.5-5.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2019/mssql-server-extensibility-15.0.2000.5-5.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2019/mssql-server-extensibility-java-15.0.2000.5-5.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2019/mssql-server-fts-15.0.2000.5-5.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2019/mssql-server-ha-15.0.2000.5-5.x86_64.rpm)<br />[SSIS RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2019/mssql-server-is-15.0.2000.5-4.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/7/mssql-server-2019/mssql-server-polybase-15.0.2000.5-5.x86_64.rpm)<br />|
| **SLES 12 RPM packages** | 15.0.2000.5-5 | [Database Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-15.0.2000.5-5.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-15.0.2000.5-5.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-extensibility-java-15.0.2000.5-5.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-fts-15.0.2000.5-5.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-ha-15.0.2000.5-5.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/12/mssql-server-2019/mssql-server-polybase-15.0.2000.5-5.x86_64.rpm)<br />|
| **Ubuntu 18.04 Debian packages**<br /><br />(Get [Ubuntu 16.04 Debian packages](https://packages.microsoft.com/ubuntu/16.04/mssql-server-2019/pool/main/m/mssql-server/)) | 15.0.2000.5-4 | [SSIS Debian package](https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019/pool/main/m/mssql-server-is/mssql-server-is_15.0.2000.5-4_amd64.deb)<br />|

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

