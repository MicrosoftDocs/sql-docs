---
title: System Requirements (ODBC Driver for SQL Server)
description: This lists the system requirements for the ODBC Driver for SQL Server on Linux and macOS operating systems.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/08/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "prerequisites"
  - "system requirements"
  - "requirements"
---
# System Requirements (Linux and macOS)

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This topic lists the requirements to use the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS.

## SQL version compatibility

The Linux and macOS drivers SQL version compatibility is the same as the [Windows drivers SQL version compatibility](../windows/system-requirements-installation-and-driver-files.md#sql-version-compatibility).

## Operating system support

Versions 18, 17, 13.1, and 13 of the Linux and macOS drivers are supported on the x64 architecture of the following operating systems. The ARM64 architecture on macOS is supported starting with version 17.8. The ARM64 architecture on Red Hat 8, 9, Debian 11, and Ubuntu 20.04, 22.04 are supported starting with version 18.1.

|Driver version&nbsp;&#8594;<br />&#8595; Operating System     |18.1|18.0|17.10|17.9|17.8|17.7|17.6|17.5|17.4|17.3|17.2|17.1|17.0|13.1|13|
|-------------------------------|----|----|----|----|----|----|----|----|----|----|----|----|----|---|
|Apple OS X 10.11 (El Capitan)  |    |    |    |    |    |    |    |    |Yes |Yes |Yes |Yes |Yes |Yes |Yes|
|Apple macOS 10.12 (Sierra)     |    |    |    |    |    |    |    |    |Yes |Yes |Yes |Yes |Yes |Yes |Yes|
|Apple macOS 10.13 (High Sierra)|    |    |    |    |    |    |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes|
|Apple macOS 10.14 (Mojave)     |    |    |    |    |Yes |Yes |Yes |Yes |Yes |Yes |    |    |    |    |   |
|Apple macOS 10.15 (Catalina)   |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |    |    |    |    |    |    |   |
|Apple macOS 11.0 (Big Sur)     |Yes |Yes |Yes |Yes |Yes |Yes |    |    |    |    |    |    |    |    |   |
|Apple macOS 12.0 (Monterey)    |Yes |Yes |Yes |Yes |    |    |    |    |    |    |    |    |    |    |   |
|Alpine Linux 3.11              |    |    |    |    |Yes |Yes |Yes |Yes |    |    |    |    |    |    |   |
|Alpine Linux 3.12              |    |Yes |Yes |Yes |Yes |Yes |    |    |    |    |    |    |    |    |   |
|Alpine Linux 3.13              |Yes |Yes |Yes |Yes |Yes |Yes |    |    |    |    |    |    |    |    |   |
|Alpine Linux 3.14              |Yes |Yes |Yes |Yes |Yes |Yes |    |    |    |    |    |    |    |    |   |
|Alpine Linux 3.15              |Yes |Yes |Yes |Yes |Yes |Yes |    |    |    |    |    |    |    |    |   |
|Debian Linux 8                 |    |    |    |    |    |    |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes|
|Debian Linux 9                 |    |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes|
|Debian Linux 10                |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |    |    |    |    |    |   |
|Debian Linux 11                |Yes |Yes |Yes |Yes |    |    |    |    |    |    |    |    |    |    |   |
|Oracle Linux 7                 |Yes |Yes |Yes |Yes |Yes |Yes |    |    |    |    |    |    |    |    |   |
|Oracle Linux 8                 |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |    |    |    |    |    |    |   |
|Red Hat Enterprise Linux 6     |    |    |    |    |    |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes|
|Red Hat Enterprise Linux 7     |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes|
|Red Hat Enterprise Linux 8     |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |    |    |    |    |    |   |
|Red Hat Enterprise Linux 9     |Yes |    |Yes |    |    |    |    |   |    |    |    |    |    |    |   |
|SUSE Linux Enterprise Server 11<sup>1</sup>|    |    |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes|
|SUSE Linux Enterprise Server 12|Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes|
|SUSE Linux Enterprise Server 15|Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |    |    |    |    |   |
|Ubuntu Linux 14.04             |    |    |    |    |    |    |    |    |Yes |Yes |Yes |Yes |Yes |Yes |Yes|
|Ubuntu Linux 16.04             |    |    |    |    |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes|
|Ubuntu Linux 18.04             |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |Yes |    |    |    |   |
|Ubuntu Linux 20.04             |Yes |Yes |Yes |Yes |Yes |Yes |Yes |    |    |    |    |    |    |    |   |
|Ubuntu Linux 22.04             |Yes |    |Yes |    |    |    |    |    |    |    |    |    |    |    |   |

<sup>1</sup> ODBC Driver 17 supports SUSE Linux Enterprise Server 11 SP4 only

The installation packages for the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] ODBC Driver 13, 13.1, and 17 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS resolve the driver's dependencies automatically when installed using the package management system of your distribution, as described in [Install the ODBC Driver (Linux)](installing-the-microsoft-odbc-driver-for-sql-server.md) and [Install the ODBC Driver (macOS)](install-microsoft-odbc-driver-sql-server-macos.md).

## Microsoft ODBC Driver 11 for SQL Server  
  
* 64-bit UnixODBC 2.3.0 Driver Manager, built for 64-bit SQLLEN/SQLULEN. Later versions of the 64-bit UnixODBC Driver Manager are not supported with the ODBC driver on Linux. See [Installing the Driver Manager](../../../connect/odbc/linux-mac/installing-the-driver-manager.md) for more information.  
  
* ODBC driver for **Red Hat Enterprise Linux 5 (64-bit)** requires the following packages, and can be downloaded here: [Microsoft ODBC Driver 11 for SQL Server - Red Hat Linux](https://go.microsoft.com/fwlink/?LinkId=267321)  
  * `glibc`  
  * `libgcc`  
  * `libstdc++`  
  * `e2fsprogs-libs`  
  * `krb5-libs`  
  * `openssl`  
  
* ODBC driver for  **Red Hat Enterprise Linux 6 (64-bit)** requires the following packages, and can be downloaded here: [Microsoft ODBC Driver 11 for SQL Server - Red Hat Linux](https://go.microsoft.com/fwlink/?LinkId=267321)  
  * `glibc`  
  * `libgcc`  
  * `libstdc++`  
  * `libuuid`  
  * `krb5-libs`  
  * `openssl`  
  
* ODBC driver for **SUSE Linux Enterprise 11 Service Pack 2 (64-bit)** requires the following packages, and can be downloaded here: [Microsoft ODBC Driver 11 Preview for SQL Server - SUSE Linux](https://go.microsoft.com/fwlink/?LinkId=264916)  
  * `glibc`  
  * `libstdc++46`  
  * `libgcc46`  
  * `libuuid1`  
  * `krb5`  
  * `libopenssl0_9_8`  
  
## See Also

[Installing the Driver Manager](../../../connect/odbc/linux-mac/installing-the-driver-manager.md)  
[Known Issues in this Version of the Driver](../../../connect/odbc/linux-mac/known-issues-in-this-version-of-the-driver.md)  
[Release Notes](../../../connect/odbc/linux-mac/release-notes-odbc-sql-server-linux-mac.md)  
