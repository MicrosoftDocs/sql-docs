---
title: "Release Notes ODBC Driver for SQL Server on Linux and macOS"
description: "Learn what's new and changed in released versions of the Microsoft ODBC Driver for SQL Server."
ms.custom: ""
ms.date: "05/06/2020"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: "v-jizho2"
ms.technology: connectivity
ms.topic: conceptual
author: v-chojas
ms.author: v-jizho2
manager: kenvh
---
# Release Notes for the Microsoft ODBC Driver for SQL Server on Linux and macOS

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This article lists and describes what's new in the versioned releases of the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS.

<!--
Going forward, please use the new 2-column markdown table for each new H2 version section.
And we are keeping the H2 titles much shorter, partly by removing redundant or obvious info.
We want to track the Month YYYY each new version H2 section is added, at the end of the H2 title.
This is the new standard format for Release Notes article content.

OLD     FILE NAME:    linux-mac/release-notes.md
NOW NEW FILE NAME:    linux-mac/release-notes-odbc-sql-server-linux-mac.md

Thank you.
GeneMi.  2019/04/03.
-->


## 17.6, July 2020

| New item | Details |
| :------- | :------ |
| New distributions supported. | Ubuntu 20.04 |
| Support for Federated Authentication | See [Using Azure Active Directory](../using-azure-active-directory.md). |
| Metadata caching for prepared statements | See [Using Always Encrypted](../using-always-encrypted-with-the-odbc-driver.md). |
| SQL_COPT_SS_AUTOBEGINTXN connection attribute to control whether automatic BEGIN TRANSACTION happens after ROLLBACK or COMMIT | See [DSN and Connection String Attributes and Keywords](../dsn-connection-string-attribute.md). |
| Bug fixes. | [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

## 17.5.2.2, April 2020 (Alpine Linux only)

| Feature added | Details |
| :------------ | :------ |
| Bug fixed. | See [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

## 17.5.2, March 2020

| Feature added | Details |
| :------------ | :------ |
| Support authentication with Managed Identity for Azure Key Vault | See [Using Always Encrypted with the ODBC Driver](../using-always-encrypted-with-the-odbc-driver.md). |
| Support for additional Azure Key Vault endpoints | See [Using Always Encrypted with the ODBC Driver](../using-always-encrypted-with-the-odbc-driver.md). |
| Bug fixes. | See [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

## 17.5, January 2020

| Feature added | Details |
| :------------ | :------ |
| SQL_COPT_SS_SPID connection attribute to retrieve SPID without round trip to server | See [DSN and Connection String Attributes and Keywords](../dsn-connection-string-attribute.md). |
| Support for indicating EULA acceptance via `debconf` on Debian and Ubuntu | See [Installing the Driver](./installing-the-microsoft-odbc-driver-for-sql-server.md). |
| New distributions supported. | &bull; &nbsp; &nbsp; Alpine Linux (3.10, 3.11)<br/>&bull; &nbsp; &nbsp; Oracle Linux 8<br/>&bull; &nbsp; &nbsp; Ubuntu 19.10<br/>&bull; &nbsp; &nbsp; macOS 10.15 |
| Bug fixes. | See [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

## 17.4.2, October 2019

| Feature added | Details |
| :------------ | :------ |
| Support for additional Azure Key Vault endpoints | See [Using Always Encrypted with the ODBC Driver](../using-always-encrypted-with-the-odbc-driver.md). |
| Support for setting data classification version | See [Data Classification](../data-classification.md#bkmk-version). |
| Bug fixes. | See [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

**Known Issue:**

When using Always Encrypted with secure enclaves and Azure Key Vault, odd key path lengths may result in CMK signature verification errors. If you encounter this issue, try changing the length of the key path by one character by renaming the AKV key.

## 17.4, August 2019

| Feature added | Details |
| :------------ | :------ |
| Always Encrypted with Secure Enclaves. | See [Using Always Encrypted with the ODBC Driver](../using-always-encrypted-with-the-odbc-driver.md). |
| Dynamic loading of OpenSSL | See [Programming Guidelines](programming-guidelines.md#bkmk-openssl). |
| Configurable TCP Keep Alive settings. | See [Connecting to SQL Server](connection-string-keywords-and-data-source-names-dsns.md). |
| Bug fixes. | See [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

## 17.3, February 2019

| New item | Details |
| :------- | :------ |
| New distributions supported. | &bull; &nbsp; &nbsp; SUSE 15<br/>&bull; &nbsp; &nbsp; Ubuntu 18.10<br/>&bull; &nbsp; &nbsp; macOS 10.14 |
| Azure Active Directory Managed Service Identity (system and user-assigned) authentication mode. | See [Using Azure Active Directory with the ODBC Driver](../using-azure-active-directory.md). |
| Ability to stream input parameters against Always Encrypted columns. | For more information, see [Limitations of the ODBC driver when using Always Encrypted](../using-always-encrypted-with-the-odbc-driver.md#limitations-of-the-odbc-driver-when-using-always-encrypted). |
| XA distributed transactions. | See [Using XA Transactions](../use-xa-with-dtc.md).<br/><br/>XA is an initialism for _eXtended Architecture_, which is a standard for the execution of a global transaction that accesses more than one server-side data storage system. |
| &nbsp; | &nbsp; |

## 17.2, July 2018

| New item | Details |
| :------- | :------ |
| New distributions supported. | &bull; &nbsp; &nbsp; Ubuntu 18.04 |
| Data Classification for Azure SQL Database and SQL Server. | See [Data Classification](../data-classification.md). |
| Support UTF-8 server encoding. | &nbsp; |
| `SQLBrowseConnect` | &nbsp; |
| Dynamic dependency on `libcurl`. | Starting with this version, the `libcurl` package is not an explicit dependency.<br/>The `libcurl` package for OpenSSL or NSS is required when using Azure Key Vault or Azure Active Directory authentication.<br/>If you encounter an error regarding `libcurl`, ensure it is installed. |
| Idle Connection Resiliency with ConnectRetryCount and ConnectRetryInterval keywords in connection string. | &bull; &nbsp; &nbsp; Use `SQL_COPT_SS_CONNECT_RETRY_COUNT`(read only) to retrieve the number of connection retry attempts.<br/><br/>&bull; &nbsp; &nbsp; Use `SQL_COPT_SS_CONNECT_RETRY_INTERVAL`(read only) to retrieve the length of the connection retry interval.<br/><br/>See [Connection Resiliency in the Windows ODBC Driver](../windows/connection-resiliency-in-the-windows-odbc-driver.md). |
| Bug fixes. | [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

## 17.1, March 2018

| New item | Details |
| :------- | :------ |
| Support for `SQL_COPT_SS_CEKCACHETTL` and `SQL_COPT_SS_TRUSTEDCMKPATHS` connection attributes. | &bull; &nbsp; &nbsp; `SQL_COPT_SS_CEKCACHETTL` allows controlling the time that the local cache of Column Encryption Keys exists, as well as flushing it.<br/><br/>&bull; &nbsp; &nbsp; `SQL_COPT_SS_TRUSTEDCMKPATHS` allows the application to restrict Always Encrypted operations to use only the specified list of Column Master Keys.<br/><br/>See [Using Always Encrypted with the ODBC Driver for SQL Server](../using-always-encrypted-with-the-odbc-driver.md)). |
| Support for loading the `.rll` from default location. | See ['Resource File Loading' section in the Installation document](installing-the-microsoft-odbc-driver-for-sql-server.md#resource-file-loading). |
| Bug fixes. | [Bug fixes](../bug-fixes.md). |
| &nbsp; | &nbsp; |

## 17

**New distributions supported**:
macOS High Sierra and Ubuntu 17.10 

**Performance Improvements**:
Greater than 10x performance improvement when driver converts to/from UTF-8/16.

**Features Added**:

Always Encrypted support for BCP API

New connection string attribute UseFMTOnly causes driver to use legacy metadata in special cases requiring temp tables.

Support for Azure SQL Managed Instance. 
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

## 13.1, for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS, May 2017

ODBC Driver 13.1 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] adds support for Always Encrypted and Azure Active Directory when used in conjunction with Microsoft SQL Server 2016.

**New distributions supported**:
OS X 10.11 and macOS 10.12 are supported in the first release of the ODBC Driver on macOS. Ubuntu 16.10 is now also supported, along with Red Hat 6, 7, and SUSE 12. Each platform has a platform-relevant package (RPM or DEB) to ease installation and configuration. For more information, see the ODBC driver installation instructions for [Linux](../../../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md)
and [macOS](../../../connect/odbc/linux-mac/install-microsoft-odbc-driver-sql-server-macos.md).

**unixODBC Driver Manager 2.3.1 Support Changes**: The ODBC driver no longer depends on custom packaging for the unixODBC driver manager (except on RedHat 6), and instead relies on the distribution package manager to resolve the UnixODBC dependency from the distribution's repositories.

**BCP API Support**: The Linux and macOS ODBC driver now supports the use of the [BCP API functions (**bcp_init**, etc.)](../../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md)

## 13.0, for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux

With Microsoft ODBC Driver 13.0 for SQL Server, SQL Server 2014 and SQL Server 2016 are now also supported.  

**New distributions supported**:

Ubuntu is now supported, along with Red Hat and SUSE. Each platform has a platform-relevant package (RPM or DEB) to ease installation and configuration.  See [Installing the Driver](../../../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md) for installation instructions.

**unixODBC Driver Manager 2.3.1 Support**: In addition to a newer driver manager, there is also a package for installing this dependency that eases installation and configuration.  

**Transparent Network IP Resolution**: Transparent Network IP Resolution is a revision of the existing Multi-Subnet Failover feature that affects the connection sequence of the driver in the case where the first resolved IP of the hostname does not respond and there are multiple IPs associated with the hostname.

**TLS 1.2 Support**: The Microsoft ODBC Driver 13.0 for SQL Server on Linux now supports TLS 1.2 when secure communications with SQL Server are used.

## 11, for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux

The ODBC driver on SUSE Linux (Preview) supports 64-bit SUSE Linux Enterprise 11 Service Pack 2. For more information, see [System Requirements](../../../connect/odbc/linux-mac/system-requirements.md).  

The ODBC driver on Linux supports [!INCLUDE[ssHADR](../../../includes/sshadr_md.md)]. For more information, see [ODBC Driver on Linux Support for High Availability, Disaster Recovery](../../../connect/odbc/linux-mac/odbc-driver-on-linux-support-for-high-availability-disaster-recovery.md).  

The ODBC driver on Linux supports connections to Azure SQL Database.

The `-l` option (login timeout) has been added to `bcp`. For more information, see [Connecting with **bcp**](../../../connect/odbc/linux-mac/connecting-with-bcp.md).
