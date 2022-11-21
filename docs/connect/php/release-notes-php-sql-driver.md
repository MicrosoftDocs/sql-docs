---
title: Release Notes for the Microsoft Drivers for PHP
description: This page discusses what was changed in each version of the Microsoft Drivers for PHP for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-davidengel
ms.date: 06/16/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "what's new in version 1.1"
---
# Release Notes for the Microsoft Drivers for PHP for SQL Server

This page discusses what was added in each version of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  

<!--
Hello, We are standardizing the format of content inside our Release Notes (or What's New) articles.
Instead of bullets (or paragraphs), we have shifted to the 2-column format you see for H2 **What's New in Version 5.6**.
It is not necessary to reformat all the older H2 sections in this Release Notes file, but.....

Going forward, please be sure to use the 2-column format.

Also, all Release Notes .md file names now must begin with 'release-notes-*.md'.  And no filler words.
The 5.6 edition of this file is being renamed.....
FROM:  'release-notes-for-the-php-sql-driver.md'
TO  :  'release-notes-php-sql-driver.md'

For any questions, ask GeneMi or CraigG.
Thanks a lot.  2019-03-28  (DevO= 1467988)
-->

## 5.10

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download Windows Package](https://go.microsoft.com/fwlink/?linkid=2199011)  
[GitHub Release Tag (Linux and macOS packages are available here)](https://github.com/Microsoft/msphpsql/releases/tag/v5.10.1)

- Release number: 5.10.1
- Released: June 14, 2022

Fixed issues in 5.10.1:

- Fixed User-Assigned Managed Identity (ActiveDirectoryMsi) Authentication when specifying UID
- Removed block on ActiveDirectoryIntegrated authentication

### Previous 5.10 releases

- Release number: 5.10.0<sup>1</sup>
- Released: January 31, 2022

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download Windows Package](https://go.microsoft.com/fwlink/?linkid=2185805)  
[GitHub Release Tag (Linux and macOS packages are available here)](https://github.com/Microsoft/msphpsql/releases/tag/v5.10.0)

### What's new in 5.10

| New item | Details |
| :------- | :------ |
| Added support for PHP 8.1. | &nbsp; |
| Dropped support for PHP 7.3. | &nbsp; |
| Dropped support for macOS Mojave, Ubuntu 16.04, Alpine 3.11 and 3.12. | &nbsp; |
| Added support for Windows 11 and Windows Server 2022. | &nbsp; |
| Added support for macOS Monterey, Debian 11, Ubuntu 21.04 and 21.10, Alpine 3.13, 3.14 and 3.15. | &nbsp; |
| Added support for Apple M1 ARM64 hardware. | Requires ODBC Driver 17.8 or above. |
| Added support for Table-valued Parameters. | &nbsp; |
| Allowed setting PDO::ATTR_EMULATE_PREPARES at the connection level. | &nbsp; |
| Adjusted connection keyword and value validation for more flexibility. | &nbsp; |

<sup>1</sup> This release requires ODBC Driver 17.4.2 or above. Otherwise, a warning about failing to set an attribute will occur. This warning may be suppressed when using an older ODBC driver. If using SQLSRV, check [How to: Configure Error and Warning Handling Using the SQLSRV Driver](./how-to-configure-error-and-warning-handling-using-the-sqlsrv-driver.md). If using PDO_SQLSRV, warnings are by default suppressed but can be logged. Check [Logging Activity](./logging-activity.md) for details.

### Known issues

- If string parameters are bound as short string and later reused to bind longer strings, a string truncation error will occur.

## Previous releases

## 5.9

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download Windows Package](https://go.microsoft.com/fwlink/?linkid=2152937)  
[GitHub Release Tag (Linux and macOS packages are available here)](https://github.com/Microsoft/msphpsql/releases/tag/v5.9.0)

### Version information

- Release number: 5.9.0<sup>1</sup>
- Released: January 29, 2021

### What's new in 5.9

| New item | Details |
| :------- | :------ |
| Added support for PHP 8.0. | &nbsp; |
| Dropped support for PHP 7.2. | &nbsp; |
| Added support for Microsoft ODBC Driver 17.7 on all platforms. | &nbsp; |
| Added support for macOS Big Sur, Ubuntu 20.04, Ubuntu 20.10 and Alpine 3.12. | Some require ODBC Driver 17.5 or above. |
| Dropped support for macOS High Sierra, Debian 8, and Ubuntu 19.10. | &nbsp; |
| Support for GB18030 locale. | &nbsp; |
| Extended PDO `errorinfo` to include additional ODBC messages if available. | &nbsp; |
| Support for Data Classification with rank info. | Requires SQL Server 2019 and ODBC Driver 17.4.2 or above. |
| Added Azure Active Directory Service Principal authentication support. | Requires ODBC Driver 17.7 or above. |
| Improved performance when handling decimal numbers as inputs or outputs and removed unnecessary conversions for numeric values. | &nbsp; |
| Improved performance when fetching numbers using client buffers. | &nbsp; |
| Set query timeout without using LOCK TIMEOUT, which saves an extra trip to the server. | &nbsp; |

<sup>1</sup> This release requires ODBC Driver 17.4.2 or above. Otherwise, a warning about failing to set an attribute will occur. This warning may be suppressed when using an older ODBC driver. If using SQLSRV, check [How to: Configure Error and Warning Handling Using the SQLSRV Driver](./how-to-configure-error-and-warning-handling-using-the-sqlsrv-driver.md). If using PDO_SQLSRV, warnings are by default suppressed but can be logged. Check [Logging Activity](./logging-activity.md) for details.

## 5.8.1

This release only applies to Linux and macOS.

[GitHub Release Tag (Linux and macOS packages are available here)](https://github.com/Microsoft/msphpsql/releases/tag/v5.8.1)

### Version information

- Release number: 5.8.1
- Released: April 15, 2020

### What's new in 5.8.1

| New item | Details |
| :------- | :------ |
| Bug fix | Fixed default locale issues in Alpine Linux. |
| Bug fix | Removed unnecessary data structure to support Client-Side Cursors feature in Alpine Linux. |
| Bug fix | Fixed logging issues when both drivers are enabled in Alpine Linux. |

## 5.8

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download Windows Package](https://go.microsoft.com/fwlink/?linkid=2120362)  
[GitHub Release Tag (Linux and macOS packages are available here)](https://github.com/Microsoft/msphpsql/releases/tag/v5.8.0)

### Version information

- Release number: 5.8.0
- Released: January 31, 2020

### What's new in 5.8

| New item | Details |
| :------- | :------ |
| Added support for PHP 7.4. | &nbsp; |
| Dropped support for PHP 7.1. | &nbsp; |
| Added support for Microsoft ODBC Driver 17.5 on all platforms. | &nbsp; |
| Added support for Debian 10 and Red Hat 8. | Both require ODBC Driver 17.4 or above. |
| Added support for macOS Catalina, Alpine Linux 3.11<sup>1</sup> and Ubuntu 19.10. | All require ODBC Driver 17.5 or above. |
| Dropped support for SQL Server 2008 R2, macOS Sierra, Ubuntu 18.10 and Ubuntu 19.04. | &nbsp; |
| Support for Language option when connecting to SQL Server. | &nbsp; |
| Support for PHP extended string types introduced in PHP 7.2. | &nbsp; |
| Support for Data Classification sensitivity metadata retrieval. | Requires SQL Server 2019 and ODBC Driver 17.4.2 or above. |
| Support for Always Encrypted with secure enclaves. | Requires ODBC Driver 17.4 or above. |
| Support configurable options for locale settings in Linux and macOS. |
| Improved performance by caching metadata on fetches and omitting redundant calls. | &nbsp; |

<sup>1</sup> Alpine Linux support is experimental for version 5.8.

## 5.6.1

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download Windows Package](https://go.microsoft.com/fwlink/?linkid=2120446)  
[GitHub Release Tag (Linux and macOS packages are available here)](https://github.com/Microsoft/msphpsql/releases/tag/v5.6.1)

### Version information

- Release number: 5.6.1
- Released: March 19, 2019

### What's new in 5.6.1

| New item | Details |
| :------- | :------ |
| Bug fix | Fixed assumptions made when calculating field or column metadata which may have resulted in application termination. |
| Bug fix | Modified sqlsrv config file such that it can be compiled independently of pdo_sqlsrv. |
| Bug fix | Fixed PDOStatement::getColumnMeta() to return false when something goes wrong. |

## 5.6

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download Windows Package](https://go.microsoft.com/fwlink/?linkid=2120450)  
[GitHub Release Tag (Linux and macOS packages are available here)](https://github.com/Microsoft/msphpsql/releases/tag/v5.6.0)

### Version information

- Release number: 5.6.0
- Released: February 21, 2019

### What's new in 5.6

| New item | Details |
| :------- | :------ |
| Support for PHP 7.3. | &nbsp; |
| Dropped support for PHP 7.0. | &nbsp; |
| Support for Microsoft ODBC Driver 17.3 on all platforms. | &nbsp; |
| Support for macOS Mojave. | Requires ODBC Driver 17.3 or above. |
| Support for Ubuntu 18.10 and Suse Linux 15. | Both require ODBC Driver 17.3 or above. |
| Dropped support for Linux Ubuntu 17.10 and macOS El Capitan. | &nbsp; |
| Support for Azure AD Access Token. | In Linux and macOS, requires ODBC Driver 17.2+ and unixODBC 2.3.6+. |
| Support for Authentication with Azure AD using Managed Identity for Azure Resources. | Requires ODBC Driver 17.3+. |
| New fetch functionalities | &bull; &nbsp; New PDO::SQLSRV_ATTR_FETCHES_DATETIME_TYPE flag for pdo_sqlsrv to return datetime as objects.<br/><br/>&bull; &nbsp; Add ReturnDatesAsStrings option to statement level for sqlsrv.<br/><br/>&bull; &nbsp; New options at connection and statement levels for both drivers for formatting decimal values in the fetched results. |
| Support for static compilation of drivers if users choose to build from source. | &nbsp; |
| Improved performance by caching metadata on fetches and speeding up Unicode string conversions. | &nbsp; |

## 5.3

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download Windows Package](https://go.microsoft.com/fwlink/?linkid=2120447)  
[GitHub Release Tag (Linux and macOS packages are available here)](https://github.com/Microsoft/msphpsql/releases/tag/v5.3.0)

### Version information

- Release number: 5.3.0
- Released: July 20, 2018

### What's new in 5.3

- Support for Microsoft ODBC Driver 17.2 on all platforms
- Support for macOS High Sierra (requires ODBC Driver 17 and above)
- Support for Azure Key Vault for Always Encrypted for basic CRUD functionalities such that Always Encrypted feature is available to all supported Windows, Linux or macOS platforms [Using Always Encrypted with the PHP Drivers for SQL Server](using-always-encrypted-php-drivers.md)
- Support Ubuntu 18.04 LTS (requires ODBC Driver 17.2)
- Support for Connection Resiliency in Linux or macOS as well (requires ODBC Driver 17.2)

## 5.2

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download Windows Package](https://go.microsoft.com/fwlink/?linkid=2120451)  
[GitHub Release Tag (Linux and macOS packages are available here)](https://github.com/Microsoft/msphpsql/releases/tag/v5.2.0)

### Version information

- Release number: 5.2.0
- Released: March 23, 2018

### What's new in 5.2

- Support for PHP 7.2.1 and up on Windows, and 7.2.0 and up on other platforms
- Support for Microsoft ODBC Driver 17
  - Version 17 is now the default on all platforms
- Support for Ubuntu 17.10, Debian 9, and Suse Enterprise Linux 12
- Dropped support for Ubuntu 15.10
- Support for Always Encrypted with CRUD functionalities on Windows. For more information, see [Using Always Encrypted with the PHP Drivers for SQL Server](using-always-encrypted-php-drivers.md)
  - Support for Windows Certificate Store
  - Always Encrypted is only supported with Microsoft ODBC Driver 17 and above
- Support for non-UTF8 locales on Linux and macOS
  - Non-UTF8 locales on Linux and macOS are only supported with Microsoft ODBC Driver 17 and above
- Support for Azure Synapse Analytics
- Support for Azure SQL Managed Instance

## 4.3

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download Windows Package](https://go.microsoft.com/fwlink/?linkid=2120616)  
[GitHub Release Tag (Linux and macOS packages are available here)](https://github.com/Microsoft/msphpsql/releases/tag/v4.3.0)

### Version information

- Release number: 4.3.0
- Released: July 6, 2017

### What's new in 4.3

- Support for PHP 7.1
- Support for macOS Sierra and macOS El Capitan
- Support for Ubuntu 15.10, and Debian 8
- Dropped support for Ubuntu 15.04
- Support for Always On Availability groups via Transparent Network IP Resolution. For more information, see [Connection Options](connection-options.md).
- Added support for sql_variant data type with limitation.
- Idle Connection Resiliency support in Windows. For more information, see [Connection Options](connection-options.md).
- Connection pooling support for Linux and macOS. For more information, see [Connection Pooling](connection-pooling-microsoft-drivers-for-php-for-sql-server.md).
- Support for Azure Active Directory Authentication with ActiveDirectoryPassword and SqlPassword. For more information, see [Connection Options](connection-options.md).

## 4.0

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download Windows Package](https://go.microsoft.com/fwlink/?linkid=2120448)  
[GitHub Release Tag](https://github.com/microsoft/msphpsql/releases/tag/v4.0-RTW)

### Version information

- Release number: 4.0
- Released: July 1, 2016

### What's new in 4.0

- Support for PHP 7.0  
- Full 64-bit support
- Support for Ubuntu 15.04, Ubuntu 16.04, and Red Hat 7

## 3.2

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download Windows Package](https://go.microsoft.com/fwlink/?linkid=2120449)  
[GitHub Release Tag](https://github.com/microsoft/msphpsql/releases/tag/v3.2.0.0)

### Version information

- Release number: 3.2
- Released: March 9, 2015

### What's new in 3.2

- Support for PHP 5.6  
- Includes latest updates for prior PHP versions 5.5 and 5.4  
- Requires Microsoft ODBC Driver 11 for SQL Server  

## 3.1

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download Windows Package](https://go.microsoft.com/fwlink/?linkid=2143027)  
[GitHub Release Tag](https://github.com/microsoft/msphpsql/releases/tag/v3.1.0.0)

### Version information

- Release number: 3.1
- Released: December 12, 2014

### What's new in 3.1

- Support for PHP 5.5  
- Requires Microsoft ODBC Driver 11 for SQL Server. Previous versions required SQL Native Client.  

## 3.0

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download Windows Package](https://go.microsoft.com/fwlink/?linkid=2143026)  

### What's new in 3.0  

- Support for PHP 5.4.  PHP 5.2 is not supported in version 3 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
- AttachDBFileName connection option is added. For more information, see [Connection Options](connection-options.md).  
- Support for LocalDB, which was added in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]. For more information, see [Support for LocalDB](php-driver-for-sql-server-support-for-localdb.md).
- AttachDBFileName connection option is added. For more information, see [Connection Options](connection-options.md).  
- Support for the high-availability, disaster recovery features. For more information, see [Support for High Availability, Disaster Recovery](php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md).
- Support for client-side cursors (caching a result set in-memory). For more information, see [Cursor Types &#40;SQLSRV Driver&#41;](cursor-types-sqlsrv-driver.md) and [Cursor Types &#40;PDO_SQLSRV Driver&#41;](cursor-types-pdo-sqlsrv-driver.md).
- The PDO::ATTR_EMULATE_PREPARES attribute has been added. For more information, see [PDO::prepare](pdo-prepare.md).  

## 2.0

### What's new in 2.0

In version 2.0, support for the PDO_SQLSRV driver was added. For more information, see [PDO_SQLSRV Driver Reference](pdo-sqlsrv-driver-reference.md).  

## See Also

[Overview of the Microsoft Drivers for PHP for SQL Server](overview-of-the-php-sql-driver.md)
