---
title: System Requirements (ODBC Driver for SQL Server)
description: This lists the system requirements for the ODBC Driver for SQL Server on Linux and macOS operating systems.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest, davidengel, sunilbs, mcimfl
ms.date: 04/30/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ms.custom:
  - linux-related-content
helpviewer_keywords:
  - "prerequisites"
  - "system requirements"
  - "requirements"
---
# System Requirements (Linux and macOS)

[!INCLUDE [Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This article lists the requirements to use the [!INCLUDE [msCoName](../../../includes/msconame-md.md)] ODBC Driver for [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS.

## SQL version compatibility

The Linux and macOS drivers SQL version compatibility is the same as the [Windows drivers SQL version compatibility](../windows/system-requirements-installation-and-driver-files.md#sql-version-compatibility).

## ODBC Driver 18 operating system support

The following table shows the operating systems supported by ODBC Driver 18 for SQL Server on Linux and macOS.  
The driver supports the x64 architecture on all listed operating systems. The Arm64 architecture on macOS is supported starting with version 17.8 and continues in version 18. The Arm64 architecture on Red Hat 8, 9, Debian 11, and Ubuntu 20.04, 22.04 is supported starting with version 18.1. Subsequent versions of 18 continue Arm64 support on newer versions of Red Hat, Debian, and Ubuntu. Arm64 on Alpine is supported starting with version 18.3.

| Driver version&nbsp;&#8594;<br />&#8595; Operating System | 18.6 | 18.5 | 18.4 | 18.3 | 18.2 | 18.1 | 18.0 |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Apple macOS 10.15 (Catalina) | No | No | No | No | No | Yes | Yes |
| Apple macOS 11.0 (Big Sur) | No | No | Yes | Yes | Yes | Yes | Yes |
| Apple macOS 12.0 (Monterey) | No | No | Yes | Yes | Yes | Yes | Yes |
| Apple macOS 13.0 (Ventura) | No | Yes | Yes | Yes | Yes | No | No |
| Apple macOS 14.0 (Sonoma) | Yes | Yes | Yes | No | No | No | No |
| Apple macOS 15.0 (Sequoia) | Yes | Yes | No | No | No | No | No |
| Apple macOS 26 (Tahoe) | Yes | No | No | No | No | No | No |
| Alpine Linux 3.12 | No | No | No | No | No | No | Yes |
| Alpine Linux 3.13 | No | No | No | No | No | Yes | Yes |
| Alpine Linux 3.14 | No | No | No | No | Yes | Yes | Yes |
| Alpine Linux 3.15 | No | No | Yes | Yes | Yes | Yes | Yes |
| Alpine Linux 3.16 | No | No | Yes | Yes | Yes | No | No |
| Alpine Linux 3.17 | No | No | Yes | Yes | No | No | No |
| Alpine Linux 3.18 | No | Yes | Yes | Yes | No | No | No |
| Alpine Linux 3.19 | No | Yes | Yes | Yes | No | No | No |
| Alpine Linux 3.20 | Yes | Yes | No | No | No | No | No |
| Alpine Linux 3.21 | Yes | No | No | No | No | No | No |
| Alpine Linux 3.22 | Yes | No | No | No | No | No | No |
| Azure Linux 3.0 | Yes | Yes | No | No | No | No | No |
| Debian Linux 9 | No | No | No | No | No | No | Yes |
| Debian Linux 10 | No | No | Yes | Yes | Yes | Yes | Yes |
| Debian Linux 11 | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Debian Linux 12 | Yes | Yes | Yes | Yes | No | No | No |
| Debian Linux 13 | Yes | No | No | No | No | No | No |
| Oracle Linux 7 | No | No | Yes | Yes | Yes | Yes | Yes |
| Oracle Linux 8 | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Oracle Linux 9 | Yes | Yes | No | No | No | No | No |
| Oracle Linux 10 | Yes | No | No | No | No | No | No |
| Red Hat Enterprise Linux 7 | No | No | Yes | Yes | Yes | Yes | Yes |
| Red Hat Enterprise Linux 8 | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Red Hat Enterprise Linux 9 | Yes | Yes | Yes | Yes | Yes | Yes | No |
| Red Hat Enterprise Linux 10 | Yes | No | No | No | No | No | No |
| SUSE Linux Enterprise Server 12 | No | Yes | Yes | Yes | Yes | Yes | Yes |
| SUSE Linux Enterprise Server 15 | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Ubuntu Linux 18.04 | No | No | No | No | Yes | Yes | Yes |
| Ubuntu Linux 20.04 | No | Yes | Yes | Yes | Yes | Yes | Yes |
| Ubuntu Linux 22.04 | Yes | Yes | Yes | Yes | Yes | Yes | No |
| Ubuntu Linux 24.04 | Yes | Yes | Yes | Yes | No | No | No |
| Ubuntu Linux 25.10 | Yes | No | No | No | No | No | No |

## Previous ODBC Driver versions (17 and 13)

The following table shows the operating systems supported by previous versions of the ODBC Driver for SQL Server on Linux and macOS. These versions are in maintenance mode and receive only critical security updates. For new projects, we recommend using ODBC Driver 18.  
All versions support the x64 architecture. The Arm64 architecture on macOS is supported starting with version 17.8.

| Driver version&nbsp;&#8594;<br />&#8595; Operating System | 17.11 | 17.10 | 17.9 | 17.8 | 17.7 | 17.6 | 17.5 | 17.4 | 17.3 | 17.2 | 17.1 | 17.0 | 13.1 | 13 |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Apple OS X 10.11 (El Capitan) | No | No | No | No | No | No | No | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Apple macOS 10.12 (Sierra) | No | No | No | No | No | No | No | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Apple macOS 10.13 (High Sierra) | No | No | No | No | No | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Apple macOS 10.14 (Mojave) | No | No | No | Yes | Yes | Yes | Yes | Yes | Yes | No | No | No | No | No |
| Apple macOS 10.15 (Catalina) | No | Yes | Yes | Yes | Yes | Yes | Yes | No | No | No | No | No | No | No |
| Apple macOS 11.0 (Big Sur) | No | Yes | Yes | Yes | Yes | No | No | No | No | No | No | No | No | No |
| Apple macOS 12.0 (Monterey) | No | Yes | Yes | No | No | No | No | No | No | No | No | No | No | No |
| Apple macOS 14.0 (Sonoma) | Yes | No | No | No | No | No | No | No | No | No | No | No | No | No |
| Apple macOS 15.0 (Sequoia) | Yes | No | No | No | No | No | No | No | No | No | No | No | No | No |
| Apple macOS 26 (Tahoe) | Yes | No | No | No | No | No | No | No | No | No | No | No | No | No |
| Alpine Linux 3.11 | No | No | No | Yes | Yes | Yes | Yes | No | No | No | No | No | No | No |
| Alpine Linux 3.12 | No | Yes | Yes | Yes | Yes | No | No | No | No | No | No | No | No | No |
| Alpine Linux 3.13 | No | Yes | Yes | Yes | Yes | No | No | No | No | No | No | No | No | No |
| Alpine Linux 3.14 | No | Yes | Yes | Yes | Yes | No | No | No | No | No | No | No | No | No |
| Alpine Linux 3.15 | No | Yes | Yes | Yes | Yes | No | No | No | No | No | No | No | No | No |
| Alpine Linux 3.21 | Yes | No | No | No | No | No | No | No | No | No | No | No | No | No |
| Alpine Linux 3.22 | Yes | No | No | No | No | No | No | No | No | No | No | No | No | No |
| Alpine Linux 3.23 | Yes | No | No | No | No | No | No | No | No | No | No | No | No | No |
| Debian Linux 8 | No | No | No | No | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Debian Linux 9 | No | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | No | No |
| Debian Linux 10 | No | Yes | Yes | Yes | Yes | Yes | Yes | Yes | No | No | No | No | No | No |
| Debian Linux 11 | Yes | Yes | Yes | No | No | No | No | No | No | No | No | No | No | No |
| Debian Linux 12 | Yes | Yes | No | No | No | No | No | No | No | No | No | No | No | No |
| Debian Linux 13 | Yes | No | No | No | No | No | No | No | No | No | No | No | No | No |
| Oracle Linux 7 | No | Yes | Yes | Yes | Yes | No | No | No | No | No | No | No | No | No |
| Oracle Linux 8 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | No | No | No | No | No | No | No |
| Oracle Linux 9 | Yes | No | No | No | No | No | No | No | No | No | No | No | No | No |
| Oracle Linux 10 | Yes | No | No | No | No | No | No | No | No | No | No | No | No | No |
| Red Hat Enterprise Linux 6 | No | No | No | No | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Red Hat Enterprise Linux 7 | No | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Red Hat Enterprise Linux 8 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | No | No | No | No | No | No |
| Red Hat Enterprise Linux 9 | Yes | Yes | No | No | No | No | No | No | No | No | No | No | No | No |
| Red Hat Enterprise Linux 10 | Yes | No | No | No | No | No | No | No | No | No | No | No | No | No |
| SUSE Linux Enterprise Server 11 <sup>1</sup> | No | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| SUSE Linux Enterprise Server 12 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| SUSE Linux Enterprise Server 15 | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | No | No | No | No | No |
| SUSE Linux Enterprise Server 16 | Yes | No | No | No | No | No | No | No | No | No | No | No | No | No |
| Ubuntu Linux 14.04 | No | No | No | No | No | No | No | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Ubuntu Linux 16.04 | No | No | No | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Ubuntu Linux 18.04 | No | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | Yes | No | No | No | No |
| Ubuntu Linux 20.04 | No | Yes | Yes | Yes | Yes | Yes | No | No | No | No | No | No | No | No |
| Ubuntu Linux 22.04 | Yes | Yes | No | No | No | No | No | No | No | No | No | No | No | No |
| Ubuntu Linux 24.04 | Yes | No | No | No | No | No | No | No | No | No | No | No | No | No |
| Ubuntu Linux 25.10 | Yes | No | No | No | No | No | No | No | No | No | No | No | No | No |

<sup>1</sup> ODBC Driver 17 supports SUSE Linux Enterprise Server 11 SP4 only

The installation packages for the [!INCLUDE [msCoName](../../../includes/msconame-md.md)] ODBC Driver 13, 13.1, and 17 for [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS resolve the driver's dependencies automatically when installed using the package management system of your distribution.

For more information, see:

- [Install the Microsoft ODBC driver for SQL Server (Linux)](installing-the-microsoft-odbc-driver-for-sql-server.md)
- [Install the Microsoft ODBC driver for SQL Server (macOS)](install-microsoft-odbc-driver-sql-server-macos.md)

## Microsoft ODBC Driver 11 for SQL Server

- 64-bit UnixODBC 2.3.0 Driver Manager, built for 64-bit SQLLEN/SQLULEN. Later versions of the 64-bit UnixODBC Driver Manager aren't supported with the ODBC driver on Linux. For more information, see [Installing the Driver Manager](installing-the-driver-manager.md).

- ODBC driver for **Red Hat Enterprise Linux 5 (64-bit)** requires the following packages, and you can download it from [Microsoft ODBC Driver 11 for SQL Server - Red Hat Linux](https://go.microsoft.com/fwlink/?LinkId=267321).
  - `glibc`
  - `libgcc`
  - `libstdc++`
  - `e2fsprogs-libs`
  - `krb5-libs`
  - `openssl`

- ODBC driver for **Red Hat Enterprise Linux 6 (64-bit)** requires the following packages, and you can download it from [Microsoft ODBC Driver 11 for SQL Server - Red Hat Linux](https://go.microsoft.com/fwlink/?LinkId=267321).
  - `glibc`
  - `libgcc`
  - `libstdc++`
  - `libuuid`
  - `krb5-libs`
  - `openssl`

- ODBC driver for **SUSE Linux Enterprise 11 Service Pack 2 (64-bit)** requires the following packages, and you can download it from [Microsoft ODBC Driver 11 Preview for SQL Server - SUSE Linux](https://go.microsoft.com/fwlink/?LinkId=264916).
  - `glibc`
  - `libstdc++46`
  - `libgcc46`
  - `libuuid1`
  - `krb5`
  - `libopenssl0_9_8`

## Related content

- [Installing the Driver Manager](installing-the-driver-manager.md)
- [Known issues for the ODBC driver on Linux and macOS](known-issues-in-this-version-of-the-driver.md)
- [Release notes for the Microsoft ODBC Driver for SQL Server on Linux and macOS](release-notes-odbc-sql-server-linux-mac.md)
