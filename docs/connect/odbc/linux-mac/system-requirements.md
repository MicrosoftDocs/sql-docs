---
title: "System Requirements (ODBC Driver for SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "02/15/2018"
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
author: MightyPen
ms.author: genemi
manager: craigg
---
# System Requirements
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This topic lists the requirements to use the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS.


## Microsoft ODBC Driver 13, 13.1, and 17 for SQL Server

The Linux and macOS drivers are available only for the 64-bit versions of the following operating systems:

|Operating System|Supported Driver Version|
|------------------------------------|--------------------------------|
|Apple OS X 10.11 (El Capitan)|13, 13.1, 17|
|Apple macOS 10.12 (Sierra)|13, 13.1, 17|
|Apple macOS 10.13 (High Sierra)|17| 
|Apple macOS 10.14 (Mojave)|17| 
|Debian Linux 8|13, 13.1, 17|
|Debian Linux 9|17|
|RedHat Enterprise Linux 6|13, 13.1, 17|
|RedHat Enterprise Linux 7|13, 13.1, 17|
|SuSE Linux Enterprise Server 11|13, 13.1, 17 <br /><br /> **NOTE:** ODBC Driver 17 supports SuSE Linux Enterprise Server 11 SP4 only|
|SuSE Linux Enterprise Server 12|13, 13.1, 17|
|SuSE Linux Enterprise Server 15|17|
|Ubuntu Linux 14.04|13, 13.1, 17|
|Ubuntu Linux 15.10|13, 13.1|
|Ubuntu Linux 16.04|13, 13.1, 17|
|Ubuntu Linux 16.10|13, 13.1|
|Ubuntu Linux 17.04|17| 
|Ubuntu Linux 17.10|17|
|Ubuntu Linux 18.04|17| 
|Ubuntu Linux 18.10|17| 

The installation packages for the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 13, 13.1, and 17 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS resolve the driver's dependencies automatically when installed using the package management system of your distribution, as described in [Installing the Driver](../../../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md).

## Microsoft ODBC Driver 11 for SQL Server  
  
-   64-bit UnixODBC 2.3.0 Driver Manager, built for 64-bit SQLLEN/SQLULEN. Later versions of the 64-bit UnixODBC Driver Manager are not supported with the ODBC driver on Linux. See [Installing the Driver Manager](../../../connect/odbc/linux-mac/installing-the-driver-manager.md) for more information.  
  
-   ODBC driver for **Red Hat Enterprise Linux 5 (64-bit)** requires the following packages, and can be downloaded here: [Microsoft ODBC Driver 11 for SQL Server - Red Hat Linux](https://go.microsoft.com/fwlink/?LinkId=267321)  
    -   `glibc`  
    -   `libgcc`  
    -   `libstdc++`  
    -   `e2fsprogs-libs`  
    -   `krb5-libs`  
    -   `openssl`  
  
-   ODBC driver for  **Red Hat Enterprise Linux 6 (64-bit)** requires the following packages, and can be downloaded here: [Microsoft ODBC Driver 11 for SQL Server - Red Hat Linux](https://go.microsoft.com/fwlink/?LinkId=267321)  
    -   `glibc`  
    -   `libgcc`  
    -   `libstdc++`  
    -   `libuuid`  
    -   `krb5-libs`  
    -   `openssl`  
  
-   ODBC driver for **SUSE Linux Enterprise 11 Service Pack 2 (64-bit)** requires the following packages, and can be downloaded here: [Microsoft ODBC Driver 11 Preview for SQL Server - SUSE Linux](https://go.microsoft.com/fwlink/?LinkId=264916)  
    -   `glibc`  
    -   `libstdc++46`  
    -   `libgcc46`  
    -   `libuuid1`  
    -   `krb5`  
    -   `libopenssl0_9_8`  
  
## See Also
[Installing the Driver Manager](../../../connect/odbc/linux-mac/installing-the-driver-manager.md)

[Known Issues in this Version of the Driver](../../../connect/odbc/linux-mac/known-issues-in-this-version-of-the-driver.md)  

[Release Notes](../../../connect/odbc/linux-mac/release-notes-odbc-sql-server-linux-mac.md)  
