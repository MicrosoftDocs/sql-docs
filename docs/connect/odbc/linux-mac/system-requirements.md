---
title: "System Requirements (ODBC Driver for SQL Server)"
description: "This lists the system requirements for the ODBC Driver for SQL Server on Linux and macOS operating systems."
ms.custom: ""
ms.date: "03/18/2020"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "prerequisites"
  - "system requirements"
  - "requirements"
ms.assetid: f03b7fdd-0e9d-4e74-958d-e8c87e027348
author: David-Engel
ms.author: v-daenge
---
# System Requirements (Linux and macOS)

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This topic lists the requirements to use the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS.

## SQL version compatibility

The Linux and macOS drivers SQL version compatibility is the same as the [Windows drivers SQL version compatibility](../windows/system-requirements-installation-and-driver-files.md#sql-version-compatibility).

## Operating system support

Versions 17, 13.1, and 13 of the Linux and macOS drivers are supported on the x64 architecture of the following operating systems:

|Supported Operating System     |17.5|17.4|17.3|17.2|17.1|17.0|13.1|13|
|-------------------------------|----|----|----|----|----|----|----|--|
|Apple OS X 10.11 (El Capitan)  | |Y|Y|Y|Y|Y|Y|Y|
|Apple macOS 10.12 (Sierra)     | |Y|Y|Y|Y|Y|Y|Y|
|Apple macOS 10.13 (High Sierra)|Y|Y|Y|Y|Y|Y|Y|Y|
|Apple macOS 10.14 (Mojave)     |Y|Y|Y| | | | | |
|Apple macOS 10.15 (Catalina)   |Y| | | | | | | |
|Alpine Linux 3.11              |Y| | | | | | | |
|Debian Linux 8                 | |Y|Y|Y|Y|Y|Y|Y|
|Debian Linux 9                 |Y|Y|Y|Y|Y|Y|Y|Y|
|Debian Linux 10                |Y|Y| | | | | | |
|Oracle Linux 8                 |Y| | | | | | | |
|RedHat Enterprise Linux 6      |Y|Y|Y|Y|Y|Y|Y|Y|
|RedHat Enterprise Linux 7      |Y|Y|Y|Y|Y|Y|Y|Y|
|RedHat Enterprise Linux 8      |Y|Y| | | | | | |
|SUSE Linux Enterprise Server 11<sup>1</sup>|Y|Y|Y|Y|Y|Y|Y|Y|
|SUSE Linux Enterprise Server 12|Y|Y|Y|Y|Y|Y|Y|Y|
|SUSE Linux Enterprise Server 15|Y|Y|Y| | | | | |
|Ubuntu Linux 14.04             | |Y|Y|Y|Y|Y|Y|Y|
|Ubuntu Linux 16.04             |Y|Y|Y|Y|Y|Y|Y|Y|
|Ubuntu Linux 18.04             |Y|Y|Y|Y| | | | |
|Ubuntu Linux 19.10             |Y| | | | | | | |

<sup>1</sup> ODBC Driver 17 supports SUSE Linux Enterprise Server 11 SP4 only

The installation packages for the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 13, 13.1, and 17 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS resolve the driver's dependencies automatically when installed using the package management system of your distribution, as described in [Install the ODBC Driver (Linux)](installing-the-microsoft-odbc-driver-for-sql-server.md) and [Install the ODBC Driver (macOS)](install-microsoft-odbc-driver-sql-server-macos.md).

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
