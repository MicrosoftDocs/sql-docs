---
title: Release history for SQL Server 2022 (Preview) on Linux
description: This article contains the release history for SQL Server 2022 (Preview) running on Linux. Information is included for all Cumulative Updates and GDRs.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, vanto
ms.date: 09/26/2022
ms.prod: sql
ms.technology: linux
ms.topic: conceptual
---
# <a id="release-history"></a> Release history for 2022 (Preview) on Linux

[!INCLUDE [sqlserver2022-linux](../includes/applies-to-version/sqlserver2022-linux.md)]

The following table lists the release history for [!INCLUDE[ssSQL22](../includes/sssql22-md.md)]. For release history on other editions, see the following articles:

- [Release history for SQL Server 2017 on Linux](sql-server-linux-release-history-2017.md?view=sql-server-ver14&preserve-view=true).
- [Release history for SQL Server 2019 on Linux](sql-server-linux-release-history-2019.md?view=sql-server-ver15&preserve-view=true).

| Release               | Version       | Release date |
| --------------------- | ------------- | ------------ |
| [RC0](#RC0)           | 16.0.900.3    | 2022-09-01   |
| [CTP2.1](#CTP2.1)     | 16.0.600.9    | 2022-05-31   |

## <a id="RC0"></a> RC0 (September 2022)

This is the RC0 release of [!INCLUDE[ssSQL22](../includes/sssql22-md.md)]. This is a pre-release version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 16.0.900.3.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages** | 16.0.900.3-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-preview/mssql-server-16.0.900.3-2.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-preview/mssql-server-extensibility-16.0.900.3-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-preview/mssql-server-fts-16.0.900.3-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-preview/mssql-server-ha-16.0.900.3-2.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-preview/mssql-server-polybase-16.0.900.3-2.x86_64.rpm)<br />|
| **Ubuntu 20.04 Debian packages** | 16.0.900.3-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-preview/pool/main/m/mssql-server/mssql-server_16.0.900.3-2_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-preview/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_16.0.900.3-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-preview/pool/main/m/mssql-server-fts/mssql-server-fts_16.0.900.3-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-preview/pool/main/m/mssql-server-ha/mssql-server-ha_16.0.900.3-2_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-preview/pool/main/m/mssql-server-polybase/mssql-server-polybase_16.0.900.3-2_amd64.deb)<br />|

Go back to the [release history](#release-history).

## <a id="CTP2.1"></a> CTP2.1 (May 2022)

This is the CTP2.1 release of [!INCLUDE[ssSQL22](../includes/sssql22-md.md)]. This is a pre-release version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] version for this release is 16.0.600.9.

### Package details

For manual or offline package installations, you can download the RPM and Debian packages with the information in the following table:

| Distribution | Package version | Downloads |
| --- | --- | --- |
| **RHEL 8.x RPM packages** | 16.0.600.9-2 | [Database Engine RPM package](https://packages.microsoft.com/rhel/8/mssql-server-preview/mssql-server-16.0.600.9-2.x86_64.rpm)<br />[Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-preview/mssql-server-extensibility-16.0.600.9-2.x86_64.rpm)<br />[Java Extensibility RPM package](https://packages.microsoft.com/rhel/8/mssql-server-preview/mssql-server-extensibility-java-16.0.600.9-2.x86_64.rpm)<br />[Full-Text Search RPM package](https://packages.microsoft.com/rhel/8/mssql-server-preview/mssql-server-fts-16.0.600.9-2.x86_64.rpm)<br />[High Availability RPM package](https://packages.microsoft.com/rhel/8/mssql-server-preview/mssql-server-ha-16.0.600.9-2.x86_64.rpm)<br />[PolyBase RPM package](https://packages.microsoft.com/rhel/8/mssql-server-preview/mssql-server-polybase-16.0.600.9-2.x86_64.rpm)<br />|
| **Ubuntu 20.04 Debian packages** | 16.0.600.9-2 | [Database Engine Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-preview/pool/main/m/mssql-server/mssql-server_16.0.600.9-2_amd64.deb)<br />[Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-preview/pool/main/m/mssql-server-extensibility/mssql-server-extensibility_16.0.600.9-2_amd64.deb)<br />[Java Extensibility Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-preview/pool/main/m/mssql-server-extensibility-java/mssql-server-extensibility-java_16.0.600.9-2_amd64.deb)<br />[Full-Text Search Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-preview/pool/main/m/mssql-server-fts/mssql-server-fts_16.0.600.9-2_amd64.deb)<br />[High Availability Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-preview/pool/main/m/mssql-server-ha/mssql-server-ha_16.0.600.9-2_amd64.deb)<br />[PolyBase Debian package](https://packages.microsoft.com/ubuntu/20.04/mssql-server-preview/pool/main/m/mssql-server-polybase/mssql-server-polybase_16.0.600.9-2_amd64.deb)<br />|

Go back to the [release history](#release-history).

## See also

- [SQL Server on Linux FAQ](sql-server-linux-faq.yml)

## Next steps

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
- [Install on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Run on Docker](quickstart-install-connect-docker.md)
- [Provision a SQL VM in Azure](/azure/virtual-machines/linux/sql/provision-sql-server-linux-virtual-machine?toc=/sql/toc/toc.json)
- [Run & Connect - Cloud](quickstart-install-connect-clouds.md)
