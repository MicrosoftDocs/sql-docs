---
title: Release history for SQL Server 2022 on Linux
description: This article contains the release history for SQL Server 2022 running on Linux. Information includes all Cumulative Updates and GDRs.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, vanto
ms.date: 02/16/2023
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
---
# <a id="release-history"></a> Release history for SQL Server 2022 on Linux

[!INCLUDE [sqlserver2022-linux](../includes/applies-to-version/sqlserver2022-linux.md)]

The following table lists the release history for [!INCLUDE[ssSQL22](../includes/sssql22-md.md)]. For release history on other editions, see the following articles:

- [Release history for SQL Server 2017 on Linux](sql-server-linux-release-history-2017.md?view=sql-server-ver14&preserve-view=true).
- [Release history for SQL Server 2019 on Linux](sql-server-linux-release-history-2019.md?view=sql-server-ver15&preserve-view=true).

| Release               | Version       | Release date |
| --------------------- | ------------- | ------------ |
| [CU 1](#CU1)          | 16.0.4003.1   | 2023-02-16   |
| [GDR 1](#GDR1)        | 16.0.1050.5   | 2023-02-14   |
| [GA](#GA)             | 16.0.1000.6   | 2022-11-16   |

## <a id="CU1"></a> CU 1 (February 2023)

This is the Cumulative Update 1 (CU 1) release of [!INCLUDE[ssSQL22](../includes/sssql22-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 16.0.4003.1. For information about the fixes and improvements in this release, see the [Support article](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate1).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages** | 16.0.4003.1-1 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-16.0.4003.1-1.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-extensibility-16.0.4003.1-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-fts-16.0.4003.1-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-ha-16.0.4003.1-1.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-polybase-16.0.4003.1-1.x86_64.rpm)<br />|
| **SLES 15 RPM packages** | 16.0.4003.1-1 | [Database Engine RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-16.0.4003.1-1.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-extensibility-16.0.4003.1-1.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-fts-16.0.4003.1-1.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-ha-16.0.4003.1-1.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-polybase-16.0.4003.1-1.x86_64.rpm)<br />|
| **Ubuntu 20.04 Debian packages** | 16.0.4003.1-1 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server/mssql-server_16.0.4003.1-1_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_16.0.4003.1-1_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server-fts/mssql-server-fts_16.0.4003.1-1_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server-ha/mssql-server-ha_16.0.4003.1-1_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server-polybase/mssql-server-polybase_16.0.4003.1-1_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="GDR1"></a> GDR 1 (February 2023)

This is the General Distribution Release GDR1 (GDR 1) of [!INCLUDE[ssSQL22](../includes/sssql22-md.md)]. This is a security update that only includes fixes for GDR releases. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 16.0.1050.5. For information about the fixes and improvements in this release, see [KB 5021522](https://support.microsoft.com/help/5021522).

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages** | 16.0.1050.5-4 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-16.0.1050.5-4.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-extensibility-16.0.1050.5-4.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-fts-16.0.1050.5-4.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-ha-16.0.1050.5-4.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-polybase-16.0.1050.5-4.x86_64.rpm)<br />|
| **SLES 15 RPM packages** | 16.0.1050.5-4 | [Database Engine RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-16.0.1050.5-4.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-extensibility-16.0.1050.5-4.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-fts-16.0.1050.5-4.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-ha-16.0.1050.5-4.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-polybase-16.0.1050.5-4.x86_64.rpm)<br />|
| **Ubuntu 20.04 Debian packages** | 16.0.1050.5-4 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server/mssql-server_16.0.1050.5-4_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_16.0.1050.5-4_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server-fts/mssql-server-fts_16.0.1050.5-4_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server-ha/mssql-server-ha_16.0.1050.5-4_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server-polybase/mssql-server-polybase_16.0.1050.5-4_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="GA"></a> GA (November 2022)

This is the General Availability (GA) release of [!INCLUDE[ssSQL22](../includes/sssql22-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 16.0.1000.6.

### Package details

Package details and download locations for the RPM and Debian packages are listed in the following table. You don't need to download these packages directly if you use the steps in the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md)
- [Install Full-Text Search package](sql-server-linux-setup-full-text-search.md)
- [Install SQL Server Agent package](sql-server-linux-setup-sql-agent.md)

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages** | 16.0.1000.6-26 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-16.0.1000.6-26.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-extensibility-16.0.1000.6-26.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-fts-16.0.1000.6-26.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-ha-16.0.1000.6-26.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-2022/mssql-server-polybase-16.0.1000.6-26.x86_64.rpm)<br />|
| **SLES 15 RPM packages** | 16.0.1000.6-26 | [Database Engine RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-16.0.1000.6-26.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-extensibility-16.0.1000.6-26.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-fts-16.0.1000.6-26.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-ha-16.0.1000.6-26.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/sles/15/mssql-server-2022/mssql-server-polybase-16.0.1000.6-26.x86_64.rpm)<br />|
| **Ubuntu 20.04 Debian packages** | 16.0.1000.6-26 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server/mssql-server_16.0.1000.6-26_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_16.0.1000.6-26_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server-fts/mssql-server-fts_16.0.1000.6-26_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server-ha/mssql-server-ha_16.0.1000.6-26_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2022/pool/main/m/mssql-server-polybase/mssql-server-polybase_16.0.1000.6-26_amd64.deb)<br />|

Go back to the [release history](#release-history).

## See also

- [SQL Server on Linux FAQ](sql-server-linux-faq.yml)

## Next steps

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
- [Install on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Run on Docker](quickstart-install-connect-docker.md)
- [Create a SQL VM in Azure](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart)
- [Run & Connect - Cloud](quickstart-install-connect-clouds.md)

