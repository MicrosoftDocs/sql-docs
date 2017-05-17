---
title: "Release Notes - Microsoft ODBC Driver for SQL Server on Linux and macOS | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Release Notes for the Microsoft ODBC Driver for SQL Server on Linux and macOS
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

## What's New in the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 13.1 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on Linux and macOS  

ODBC Driver 13.1 for [!INCLUDEssNoVersion] adds support for Always Encrypted and Azure Active Directory when used in conjunction with Microsoft SQL Server 2016. 

**New distributions supported**: 
OS X 10.11 and macOS 10.12 are supported in the first release of the ODBC Driver on macOS. Ubuntu 16.10 is now also supported, along with Red Hat 6, 7, and SUSE 12. Each platform has a platform-relevant package (RPM or DEB) to ease installation and configuration.  See [Installing the Driver](../../../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md) for installation instructions.

**unixODBC Driver Manager 2.3.1 Support Changes**: The ODBC driver no longer depends on custom packaging for the unixODBC driver manager (except on RedHat 6), and instead relies on the distribution package manager to resolve the UnixODBC dependency from the distribution's repositories.

**BCP API Support**: The Linux and macOS ODBC driver now supports the use of the [BCP API functions (`bcp_init`, etc.)](../../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md)

## What's New in the Microsoft ODBC Driver 13.0 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on Linux  
With Microsoft ODBC Driver 13.0 for SQL Server, SQL Server 2014 and SQL Server 2016 are now also supported.  
  
**New distributions supported**:

Ubuntu is now supported, along with Red Hat and SUSE. Each platform has a platform-relevant package (RPM or DEB) to ease installation and configuration.  See [Installing the Driver](../../../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md) for installation instructions.
  
**unixODBC Driver Manager 2.3.1 Support**: In addition to a newer driver manager, there is also a package for installing this dependency that eases installation and configuration.  

**Transparent Network IP Resolution**: Transparent Network IP Resolution is a revision of the existing Multi-Subnet Failover feature that affects the connection sequence of the driver in the case where the first resolved IP of the hostname does not respond and there are multiple IPs associated with the hostname.

**TLS 1.2 Support**: The Microsoft ODBC Driver 13.0 for SQL Server on Linux now supports TLS 1.2 when secure communications with SQL Server are used.

## What's New in the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 11 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on Linux  
The ODBC driver on SUSE Linux (Preview) supports 64-bit SUSE Linux Enterprise 11 Service Pack 2. For more information, see [System Requirements](../../../connect/odbc/linux-mac/system-requirements.md).  
  
The ODBC driver on Linux supports [!INCLUDE[ssHADR](../../../includes/sshadr_md.md)]. For more information, see [ODBC Driver on Linux Support for High Availability, Disaster Recovery](../../../connect/odbc/linux-mac/odbc-driver-on-linux-support-for-high-availability-disaster-recovery.md).  
  
The ODBC driver on Linux supports connections to Microsoft Azure SQL Database. For more information, see [How to: Connect to Windows Azure SQL Database Using ODBC](http://msdn.microsoft.com/library/hh974312.aspx).  
  
The `-l` option (login timeout) has been added to `bcp`. For more information, see [Connecting with `bcp`](../../../connect/odbc/linux-mac/connecting-with-bcp.md).
  
