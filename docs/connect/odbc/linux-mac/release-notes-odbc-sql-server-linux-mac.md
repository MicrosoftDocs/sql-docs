---
title: Release Notes ODBC Driver for SQL Server on Linux and macOS
description: Learn what's new and changed in released versions of the Microsoft ODBC Driver for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest, davidengel, sunilbs, mcimfl
ms.date: 08/17/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: release-notes
ms.custom:
  - linux-related-content
---
# Release notes for the Microsoft ODBC Driver for SQL Server on Linux and macOS

[!INCLUDE [Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This article lists and describes what's new in the versioned releases of the [!INCLUDE [msCoName](../../../includes/msconame-md.md)] ODBC driver for [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS.

## 17.1.1, April 2026

| Feature | Description |
| --- | --- |
| New distributions supported. | macOS 14, 15, 26, Debian 13, Red Hat 10, Oracle Linux 9, 10, SUSE 16, Ubuntu 24.04, 25.10, Alpine 3.21, 3.22, 3.23 |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 18.6.2, March 2026

| Feature | Description |
| --- | --- |
| Vector parameters. | Improve handling of output and input/output vector parameters when using prepared statements. |
| Server redirections. | Support Microsoft Fabric redirection scenarios allowing up to 10 server redirections per connection attempt. |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 18.6.1, December 2025

| Feature | Description |
| --- | --- |
| New data type | Added support for new Vector data type (float32). See [Vector data type (ODBC)](../vector-data-type.md) for usage and examples. |
| ConcatNullYieldsNull property | Added support for ConcatNullYieldsNull as a connection string property. |
| New distributions supported. | Azure Linux 3.0 ARM, Debian Linux 13, RedHat Linux 10, Ubuntu Linux 25.10 |
| Azure Linux License | Changed License Acceptance Process for Azure Linux. |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 18.5.1, March 2025

| Feature | Description |
| --- | --- |
| New distributions supported. | macOS 15, Alpine Linux 3.20, Azure Linux 3.0, Oracle Linux 9, Ubuntu 24.10 |
| Packet Size option. | Expose the Packet Size as a connection string option. |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 18.4.1, July 2024

| Feature | Description |
| --- | --- |
| New distributions supported. | macOS 14, Ubuntu 24.04, Alpine 3.19 |
| Accept EULA by file presence. | Added the ability to accept the EULA for DEB and RPM packages by the presence of a file. |
| Microsoft Entra ID | Renamed all occurrences of "Azure Active Directory" to "Microsoft Entra ID". For more information, see [New name for Azure Active Directory](/entra/fundamentals/new-name). |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 18.3.3, April 2024

| Feature | Description |
| --- | --- |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 17.10.6, April 2024

| Feature | Description |
| --- | --- |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 18.3.2, October 2023

| Feature | Description |
| --- | --- |
| New distribution supported. | Debian 12 |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 17.10.5, October 2023

| Feature | Description |
| --- | --- |
| New distribution supported. | Debian 12 |
| Improved Performance | More efficient packet buffer memory allocation |
| New Managed Identity (MSI) Authentication Support | Support for MSI authentication for Azure Arc and Azure Cloud Shell and updated to a newer Azure App Service API version |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 18.3.1, July 2023

| Feature | Description |
| --- | --- |
| Improved Performance | More efficient packet buffer memory allocation |
| New Managed Identity (MSI) Authentication Support | Support for MSI authentication for Azure Arc and Azure Cloud Shell and updated to a newer Azure App Service API version |
| New distributions supported | Ubuntu 23.04, Alpine 3.17, Alpine 3.18. Arm64 is now supported on Alpine. |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 18.2.2, June 2023

| Feature | Description |
| --- | --- |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 17.10.4, June 2022

| Feature | Description |
| --- | --- |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 18.2, January 2023

| Feature | Description |
| --- | --- |
| New distributions supported. | Ubuntu 22.10, macOS 13, Alpine 3.16 |
| Server name details added to connection errors | Added original and redirected server names to connect errors |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 17.10.2, November 2022

| Feature | Description |
| --- | --- |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 18.1.2, November 2022

| Feature | Description |
| --- | --- |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 18.1, August 2022

| Feature | Description |
| --- | --- |
| New distributions supported. | Ubuntu 22.04, Red Hat 9. Arm64 Linux platforms: Debian 11, Red Hat 8 and 9, Ubuntu 20.04, 22.04 |
| `IpAddressPreference` option | See [DSN and connection string keywords and attributes](../dsn-connection-string-attribute.md). |
| `RetryExec` option | See [DSN and connection string keywords and attributes](../dsn-connection-string-attribute.md). |
| `VBS-NONE` enclave attestation protocol | New enclave attestation option to not attest the enclave. See [Using Always Encrypted with the ODBC Driver for SQL Server](../using-always-encrypted-with-the-odbc-driver.md) |
| Wildcard matching of `HostnameInCertificate` | Now able to use wildcard for specifying hostname, if different from default value found in Addr/Address/Server. |
| `ServerCertificate` option | See [DSN and connection string keywords and attributes](../dsn-connection-string-attribute.md). |
| `TrustedConnection_UseAAD` option | Now only Kerberos integrated authentication is enabled when specifying Trusted_Connection=yes. Use the TrustedConnection_UseAAD option to enable the previous behavior of using either Kerberos or Azure Active Directory integrated. For more information, see [Using Microsoft Entra ID with the ODBC Driver](../using-azure-active-directory.md). |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 17.10, June 2022

| Feature | Description |
| --- | --- |
| New distributions supported. | Ubuntu 22.04, Red Hat 9 |
| TrustedConnection_UseAAD option | Now only Kerberos integrated authentication is enabled when specifying Trusted_Connection=yes. Use the TrustedConnection_UseAAD option to enable the previous behavior of using either Kerberos or Azure Active Directory integrated. |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 18.0, February 2022

| Feature | Description |
| --- | --- |
| New distributions supported. | Debian 11, Ubuntu 21.10, macOS 12 |
| Added compatibility with OpenSSL 3.0 | See [Connection String Keywords and Data Source Names](connection-string-keywords-and-data-source-names-dsns.md#using-tlsssl). |
| Ability to send long types as max types | See [DSN and connection string keywords and attributes](../dsn-connection-string-attribute.md). |
| Support for TDS 8.0 | See [Features of the Microsoft ODBC Driver for SQL Server on Windows](../windows/features-of-the-microsoft-odbc-driver-for-sql-server-on-windows.md). |
| Compatibility extensions for SQLGetData | See [Features of the Microsoft ODBC Driver for SQL Server on Windows](../windows/features-of-the-microsoft-odbc-driver-for-sql-server-on-windows.md). |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 17.9, February 2022

| Feature | Description |
| --- | --- |
| New distributions supported. | Debian 11, Ubuntu 21.10, macOS 12 |
| Added compatibility with OpenSSL 3.0 | See [Connection String Keywords and Data Source Names](connection-string-keywords-and-data-source-names-dsns.md#using-tlsssl). |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 17.8.1.2, October 2021

| Feature | Description |
| --- | --- |
| Package update | Updated RPM packages for Red Hat 7, Red Hat 8, SUSE 12, and SUSE 15 to use SHA256 RPM signing. |

## 17.8, July 2021

| Feature | Description |
| --- | --- |
| New distributions supported. | Ubuntu 21.04, Alpine 3.13 |
| Support for Apple M1 Arm64 hardware | See [Install the Microsoft ODBC driver for SQL Server (macOS)](install-microsoft-odbc-driver-sql-server-macos.md). |
| Replication option added to the connection string | See [DSN and connection string keywords and attributes](../dsn-connection-string-attribute.md). |
| KeepAlive and KeepAliveInterval options added to the connection string | See [DSN and connection string keywords and attributes](../dsn-connection-string-attribute.md). |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 17.7.2, March 2021

| Feature | Description |
| --- | --- |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 17.7, January 2021

| Feature | Description |
| --- | --- |
| New distributions supported. | Ubuntu 20.10, macOS Big Sur (11.0), Oracle Linux 7 |
| Service Principal Authentication | See [DSN and connection string keywords and attributes](../dsn-connection-string-attribute.md). |
| Ability to insert into encrypted **money** and **smallmoney** columns | See [Using Always Encrypted with the ODBC Driver for SQL Server](../using-always-encrypted-with-the-odbc-driver.md). |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 17.6, July 2020

| Feature | Description |
| --- | --- |
| New distributions supported. | Ubuntu 20.04 |
| Support for Federated Authentication | See [Using Microsoft Entra ID with the ODBC Driver](../using-azure-active-directory.md). |
| Metadata caching for prepared statements | See [Using Always Encrypted with the ODBC Driver for SQL Server](../using-always-encrypted-with-the-odbc-driver.md). |
| `SQL_COPT_SS_AUTOBEGINTXN` connection attribute to control whether automatic `BEGIN TRANSACTION` happens after `ROLLBACK` or `COMMIT` | See [DSN and connection string keywords and attributes](../dsn-connection-string-attribute.md). |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 17.5.2.2, April 2020 (Alpine Linux only)

| Feature | Description |
| --- | --- |
| Bug fixes. | See [List of bugs fixed](../bug-fixes.md). |

## 17.5.2, March 2020

| Feature | Description |
| --- | --- |
| Support authentication with Managed Identity for Azure Key Vault | See [Using Always Encrypted with the ODBC Driver for SQL Server](../using-always-encrypted-with-the-odbc-driver.md). |
| Support for more Azure Key Vault endpoints | See [Using Always Encrypted with the ODBC Driver for SQL Server](../using-always-encrypted-with-the-odbc-driver.md). |
| Bug fixes | See [List of bugs fixed](../bug-fixes.md). |

## 17.5, January 2020

| Feature | Description |
| --- | --- |
| SQL_COPT_SS_SPID connection attribute to retrieve SPID without round trip to server | See [DSN and connection string keywords and attributes](../dsn-connection-string-attribute.md). |
| Support for indicating EULA acceptance via `debconf` on Debian and Ubuntu | See [Install the Microsoft ODBC driver for SQL Server (Linux)](installing-the-microsoft-odbc-driver-for-sql-server.md). |
| New distributions supported. | &bull; &nbsp; &nbsp; Alpine Linux (3.10, 3.11).<br />&bull; &nbsp; &nbsp; Oracle Linux 8.<br />&bull; &nbsp; &nbsp; Ubuntu 19.10.<br />&bull; &nbsp; &nbsp; macOS 10.15. |
| Bug fixes | See [List of bugs fixed](../bug-fixes.md). |

## 17.4.2, October 2019

| Feature | Description |
| --- | --- |
| Support for more Azure Key Vault endpoints | See [Using Always Encrypted with the ODBC Driver for SQL Server](../using-always-encrypted-with-the-odbc-driver.md). |
| Support for setting data classification version | See [Data Classification](../data-classification.md#bkmk-version). |
| Bug fixes | See [List of bugs fixed](../bug-fixes.md). |

**Known Issue:**

When using Always Encrypted with secure enclaves and Azure Key Vault, odd key path lengths might result in CMK signature verification errors. If you encounter this issue, try changing the length of the key path by one character by renaming the AKV key.

## 17.4, August 2019

| Feature | Description |
| --- | --- |
| Always Encrypted with secure enclaves. | See [Using Always Encrypted with the ODBC Driver for SQL Server](../using-always-encrypted-with-the-odbc-driver.md). |
| Dynamic loading of OpenSSL | See [Programming Guidelines](programming-guidelines.md#bkmk-openssl). |
| Configurable TCP Keep Alive settings. | See [Connecting from Linux or macOS](connection-string-keywords-and-data-source-names-dsns.md). |
| Bug fixes | See [List of bugs fixed](../bug-fixes.md). |

## 17.3, February 2019

| Feature | Description |
| --- | --- |
| New distributions supported. | &bull; &nbsp; &nbsp; SUSE 15.<br />&bull; &nbsp; &nbsp; Ubuntu 18.10.<br />&bull; &nbsp; &nbsp; macOS 10.14. |
| Azure Active Directory Managed Identity (system and user-assigned) authentication mode. | See [Using Microsoft Entra ID with the ODBC Driver](../using-azure-active-directory.md). |
| Ability to stream input parameters against Always Encrypted columns. | For more information, see [Limitations of the ODBC driver when using Always Encrypted](../using-always-encrypted-with-the-odbc-driver.md#limitations-of-the-odbc-driver-when-using-always-encrypted). |
| XA distributed transactions. | See [Using XA Transactions](../use-xa-with-dtc.md).<br /><br />XA is an initialism for _eXtended Architecture_, which is a standard for the execution of a global transaction that accesses more than one server-side data storage system. |

## 17.2, July 2018

| Feature | Description |
| --- | --- |
| New distributions supported. | &bull; &nbsp; &nbsp; Ubuntu 18.04 |
| Data Classification for Azure SQL Database and SQL Server. | See [Data Classification](../data-classification.md). |
| Support UTF-8 server encoding. | &nbsp; |
| `SQLBrowseConnect` | &nbsp; |
| Dynamic dependency on `libcurl`. | Starting with this version, the `libcurl` package isn't an explicit dependency.<br />The `libcurl` package for OpenSSL or NSS is required when using Azure Key Vault or Azure Active Directory authentication.<br />If you encounter an error regarding `libcurl`, ensure it's installed. |
| Idle Connection Resiliency with ConnectRetryCount and ConnectRetryInterval keywords in connection string. | &bull; &nbsp; &nbsp; Use `SQL_COPT_SS_CONNECT_RETRY_COUNT`(read only) to retrieve the number of connection retry attempts.<br /><br />&bull; &nbsp; &nbsp; Use `SQL_COPT_SS_CONNECT_RETRY_INTERVAL` (read only) to retrieve the length of the connection retry interval.<br /><br />See [Connection resiliency in the ODBC driver](../connection-resiliency.md). |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 17.1, March 2018

| Feature | Description |
| --- | --- |
| Support for `SQL_COPT_SS_CEKCACHETTL` and `SQL_COPT_SS_TRUSTEDCMKPATHS` connection attributes. | &bull; &nbsp; &nbsp; `SQL_COPT_SS_CEKCACHETTL` controls how long the local cache of Column Encryption Keys exists and flushes it.<br /><br />&bull; &nbsp; &nbsp; `SQL_COPT_SS_TRUSTEDCMKPATHS` restricts Always Encrypted operations to use only the specified list of Column Master Keys.<br /><br />See [Using Always Encrypted with the ODBC Driver for SQL Server](../using-always-encrypted-with-the-odbc-driver.md)). |
| Support for loading the `.rll` from default location. | See ['Resource File Loading' section in the Installation document](installing-the-microsoft-odbc-driver-for-sql-server.md#resource-file-loading). |
| Bug fixes | [List of bugs fixed](../bug-fixes.md). |

## 17

**New distributions supported**:
macOS High Sierra and Ubuntu 17.10

**Performance Improvements**:
Greater than 10 times performance improvement when driver converts to/from UTF-8/16.

**Features Added**:

Always Encrypted support for BCP API

New connection string attribute UseFMTOnly causes driver to use legacy metadata in special cases requiring temp tables.

Support for Azure SQL Managed Instance.

> [!NOTE]  
> There are many differences when using Managed Instance:
>
> - FILESTREAM isn't supported
> - Local filesystem access isn't supported, but required for things like tracefiles
> - Create UDT from local path isn't supported
> - Windows Integrated Authentication isn't supported
> - DTC isn't supported
> - 'sa' account isn't present (default account is called 'cloudSA')
> - TDS token ERROR (0xAA) returns incorrect server name
> - Special characters in database name aren't supported
> - `ALTER DATABASE [dbname1] MODIFY NAME = [dbname2]` isn't supported
> - The error messages are always shown in English, regardless of language settings (same as Azure)

## 13.1, for SQL Server on Linux and macOS, May 2017

ODBC Driver 13.1 for [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] adds support for Always Encrypted and Azure Active Directory when used with Microsoft SQL Server 2016.

**New distributions supported**:
OS X 10.11 and macOS 10.12 are supported in the first release of the ODBC Driver on macOS. Ubuntu 16.10 is now also supported, along with Red Hat 6, 7, and SUSE 12. Each platform has a platform-relevant package (RPM or DEB) to ease installation and configuration. For more information, see the ODBC driver installation instructions for [Linux](installing-the-microsoft-odbc-driver-for-sql-server.md)
and [macOS](install-microsoft-odbc-driver-sql-server-macos.md).

**unixODBC Driver Manager 2.3.1 Support Changes**: The ODBC driver no longer depends on custom packaging for the unixODBC driver manager (except on Red Hat 6), and instead relies on the distribution package manager to resolve the UnixODBC dependency from the distribution's repositories.

**BCP API Support**: The Linux and macOS ODBC driver now supports the use of the [BCP API functions](../../../relational-databases/native-client-odbc-extensions-bulk-copy-functions/sql-server-driver-extensions-bulk-copy-functions.md), including `bcp_init`.

## 13.0, for SQL Server on Linux

With Microsoft ODBC Driver 13.0 for SQL Server, SQL Server 2014 and SQL Server 2016 are now also supported.

**New distributions supported**:

Ubuntu is now supported, along with Red Hat and SUSE. Each platform has a platform-relevant package (RPM or DEB) to ease installation and configuration. See [Install the Microsoft ODBC driver for SQL Server (Linux)](installing-the-microsoft-odbc-driver-for-sql-server.md) for installation instructions.

**unixODBC Driver Manager 2.3.1 Support**: In addition to a newer driver manager, there's also a package for installing this dependency that eases installation and configuration.

**Transparent Network IP Resolution**: Transparent Network IP Resolution is a revision of the existing Multi-Subnet Failover feature that affects the connection sequence of the driver in the case where the first resolved IP of the hostname doesn't respond and there are multiple IPs associated with the hostname.

**TLS 1.2 Support**: The Microsoft ODBC Driver 13.0 for SQL Server on Linux now supports TLS 1.2 when secure communications with SQL Server are used.

## 11, for SQL Server on Linux

The ODBC driver on SUSE Linux (Preview) supports 64-bit SUSE Linux Enterprise 11 Service Pack 2. For more information, see [System Requirements (Linux and macOS)](system-requirements.md).

The ODBC driver on Linux supports [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)]. For more information, see [High availability and disaster recovery](../odbc-driver-support-for-high-availability-disaster-recovery.md).

The ODBC driver on Linux supports connections to Azure SQL Database.

The `-l` option (login timeout) was added to `bcp`. For more information, see [bcp utility](../../../tools/bcp/bcp-utility.md).
