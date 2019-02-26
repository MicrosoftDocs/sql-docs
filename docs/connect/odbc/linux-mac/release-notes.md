---
title: "Release Notes - Microsoft ODBC Driver for SQL Server on Linux and macOS | Microsoft Docs"
ms.custom: ""
ms.date: "06/29/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
author: MightyPen
ms.author: v-jizho2
manager: kenvh
---
# Release Notes for the Microsoft ODBC Driver for SQL Server on Linux and macOS
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

## What's New in the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 17.3 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS

**New distributions supported**: SuSE 15, Ubuntu 18.10, macOS 10.14

**Features Added**:

- Azure Active Directory Managed Service Identity (system and user-assigned) authentication mode, for more information see [Using Azure Active Directory with the ODBC Driver](../using-azure-active-directory.md)
- AE input parameter streaming, for more information see [Limitations of the ODBC driver when using Always Encrypted](../using-always-encrypted-with-the-odbc-driver.md#Limitations-of-the-ODBC-driver-when-using-Always-Encrypted)
- XA DTC, for more information see [Using XA Transactions](../use-xa-with-dtc.md)

## What's New in the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 17.2 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS

**New distributions supported**:
Ubuntu 18.04

**Features Added**:

Data Classification for Azure SQL Database and SQL Server, for more information see [Data Classification](../data-classification.md)

Support UTF-8 server encoding

SQLBrowseConnect

Dynamic dependency on `libcurl`:
- Starting with this version, the `libcurl` package is not an explict dependency. The `libcurl` package for OpenSSL or NSS is required when using Azure Key Vault or Azure Active Directory authentication. If you encounter an error regarding `libcurl`, ensure it is installed.

Idle Connection Resiliency with ConnectRetryCount and ConnectRetryInterval keywords in connection string (For more information, see [Connection Resiliency in the Windows ODBC Driver](../windows/connection-resiliency-in-the-windows-odbc-driver.md)):
- Use `SQL_COPT_SS_CONNECT_RETRY_COUNT`(read only) to retrieve the number of connection retry attempts.
- Use `SQL_COPT_SS_CONNECT_RETRY_INTERVAL`(read only) to retrieve the length of the connection retry interval.
- Connection will be retried once by default.


[Bug fixes](../bug-fixes.md)



## What's New in the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 17.1 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS

**Features Added**:

Support for `SQL_COPT_SS_CEKCACHETTL` and `SQL_COPT_SS_TRUSTEDCMKPATHS` connection attributes (For more information, see [Using Always Encrypted with the ODBC Driver for SQL Server](../using-always-encrypted-with-the-odbc-driver.md))
- `SQL_COPT_SS_CEKCACHETTL` Allows controlling the time that the local cache of Column Encryption Keys exists, as well as flushing it
- `SQL_COPT_SS_TRUSTEDCMKPATHS` Allows the application to restrict AE operations to only use the specified list of Column Master Keys



Support for loading the `.rll` from default location (For more information, see ['Resource File Loading' section in the Installation document](installing-the-microsoft-odbc-driver-for-sql-server.md#resource-file-loading))

[Bug fixes](../bug-fixes.md)



## What's New in the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 17 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS

**New distributions supported**:
macOS High Sierra and Ubuntu 17.10 

**Performance Improvements**:
Greater than 10x performance improvement when driver converts to/from UTF-8/16.

**Features Added**:

Always Encrypted support for BCP API

New connection string attribute UseFMTOnly causes driver to use legacy metadata in special cases requiring temp tables.

Support for Azure SQL Managed Instance (Extended Private Preview). 
> [!NOTE]
> There are a number of differences when using Managed Instance:
> -   FILESTREAM is not supported 
> -   Local filesystem access is not supported, but required for things like tracefiles 
> -   Create UDT from local path is not supported 
> -   Windows Integrated Authentication is not supported 
> -   DTC is not supported 
> -   'sa' account is not present (default account is called 'cloudSA')
> -   TDS token ERROR (0xAA) returns incorrect server name
> -   Special characters in database name are not supported 
> -   ALTER DATABASE [dbname1] MODIFY NAME = [dbname2] is not supported
> -   The error messages are always shown in English, regardless of language settings (same as Azure) 

## What's New in the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 13.1 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS  

ODBC Driver 13.1 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] adds support for Always Encrypted and Azure Active Directory when used in conjunction with Microsoft SQL Server 2016.

**New distributions supported**:
OS X 10.11 and macOS 10.12 are supported in the first release of the ODBC Driver on macOS. Ubuntu 16.10 is now also supported, along with Red Hat 6, 7, and SUSE 12. Each platform has a platform-relevant package (RPM or DEB) to ease installation and configuration.  See [Installing the Driver](../../../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md) for installation instructions.

**unixODBC Driver Manager 2.3.1 Support Changes**: The ODBC driver no longer depends on custom packaging for the unixODBC driver manager (except on RedHat 6), and instead relies on the distribution package manager to resolve the UnixODBC dependency from the distribution's repositories.

**BCP API Support**: The Linux and macOS ODBC driver now supports the use of the [BCP API functions (**bcp_init**, etc.)](../../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md)

## What's New in the Microsoft ODBC Driver 13.0 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux  
With Microsoft ODBC Driver 13.0 for SQL Server, SQL Server 2014 and SQL Server 2016 are now also supported.  

**New distributions supported**:

Ubuntu is now supported, along with Red Hat and SUSE. Each platform has a platform-relevant package (RPM or DEB) to ease installation and configuration.  See [Installing the Driver](../../../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md) for installation instructions.

**unixODBC Driver Manager 2.3.1 Support**: In addition to a newer driver manager, there is also a package for installing this dependency that eases installation and configuration.  

**Transparent Network IP Resolution**: Transparent Network IP Resolution is a revision of the existing Multi-Subnet Failover feature that affects the connection sequence of the driver in the case where the first resolved IP of the hostname does not respond and there are multiple IPs associated with the hostname.

**TLS 1.2 Support**: The Microsoft ODBC Driver 13.0 for SQL Server on Linux now supports TLS 1.2 when secure communications with SQL Server are used.

## What's New in the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 11 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux  
The ODBC driver on SUSE Linux (Preview) supports 64-bit SUSE Linux Enterprise 11 Service Pack 2. For more information, see [System Requirements](../../../connect/odbc/linux-mac/system-requirements.md).  

The ODBC driver on Linux supports [!INCLUDE[ssHADR](../../../includes/sshadr_md.md)]. For more information, see [ODBC Driver on Linux Support for High Availability, Disaster Recovery](../../../connect/odbc/linux-mac/odbc-driver-on-linux-support-for-high-availability-disaster-recovery.md).  

The ODBC driver on Linux supports connections to Microsoft Azure SQL Database. For more information, see [How to: Connect to Windows Azure SQL Database Using ODBC](https://msdn.microsoft.com/library/hh974312.aspx).  

The `-l` option (login timeout) has been added to `bcp`. For more information, see [Connecting with **bcp**](../../../connect/odbc/linux-mac/connecting-with-bcp.md).
