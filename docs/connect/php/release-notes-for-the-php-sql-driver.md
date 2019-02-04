---
title: "Release Notes for the Microsoft Drivers for PHP for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "02/11/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "what's new in version 1.1"
ms.assetid: 91cca3d2-ba99-4a6d-b0de-beb9699cb3f8
author: MightyPen
ms.author: genemi
manager: craigg
---
# Release Notes for the Microsoft Drivers for PHP for SQL Server
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This page discusses what was added in each version of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  

## What's New in Version 5.6

- Support for PHP 7.3
- Support for Microsoft ODBC Driver 17.3 on all platforms
- Support for macOS Mojave (requires ODBC Driver 17.3 or above)
- Support for Ubuntu 18.10 and Suse Linux 15 (both require ODBC Driver 17.3 or above)
- Dropped support for PHP 7.0
- Dropped support for Ubuntu 17.10
- Support for Azure AD Access Token (in Linux and macOS, requires ODBC Driver 17.2+ and unixODBC 2.3.6+)
- Support for Authentication with Azure AD using Managed Identity for Azure Resources (requires ODBC Driver 17.3+)
- New fetch functionalities:
  - New PDO::SQLSRV_ATTR_FETCHES_DATETIME_TYPE flag for pdo_sqlsrv to return datetime as objects
  - Add ReturnDatesAsStrings option to statement level for sqlsrv
  - New options at connection and statement levels for both drivers for formatting decimal values in the fetched results

## What's New in Version 5.3

- Support for Microsoft ODBC Driver 17.2 on all platforms
- Support for macOS High Sierra (requires ODBC Driver 17 and above)
- Support for Azure Key Vault for Always Encrypted for basic CRUD functionalities such that Always Encrypted feature is available to all supported Windows, Linux or macOS platforms [Using Always Encrypted with the PHP Drivers for SQL Server](../../connect/php/using-always-encrypted-php-drivers.md)
- Support Ubuntu 18.04 LTS (requires ODBC Driver 17.2)
- Support for Connection Resiliency in Linux or macOS as well (requires ODBC Driver 17.2)

## What's New in Version 5.2

- Support for PHP 7.2.1 and up on Windows, and 7.2.0 and up on other platforms
- Support for Microsoft ODBC Driver 17
  - Version 17 is now the default on all platforms
- Support for Ubuntu 17.10, Debian 9, and Suse Enterprise Linux 12
- Dropped support for Ubuntu 15.10
- Support for Always Encrypted with CRUD functionalities on Windows. For more information, see [Using Always Encrypted with the PHP Drivers for SQL Server](../../connect/php/using-always-encrypted-php-drivers.md)
  - Support for Windows Certificate Store
  - Always Encrypted is only supported with Microsoft ODBC Driver 17 and above
- Support for non-UTF8 locales on Linux and macOS
  - Non-UTF8 locales on Linux and macOS are only supported with Microsoft ODBC Driver 17 and above
- Support for Azure SQL Data Warehouse
- Support for Azure SQL Managed Instance (Extended Private Preview)


## What's New in Version 4.3

- Support for PHP 7.1
- Support for macOS Sierra and macOS El Capitan
- Support for Ubuntu 15.10, and Debian 8
- Dropped support for Ubuntu 15.04
- Support for Always On Availability groups via Transparent Network IP Resolution. For more information, see [Connection Options](../../connect/php/connection-options.md).
- Added support for sql_variant data type with limitation.
- Idle Connection Resiliency support in Windows. For more information, see [Connection Options](../../connect/php/connection-options.md).
- Connection pooling support for Linux and macOS. For more information, see [Connection Pooling](../../connect/php/connection-pooling-microsoft-drivers-for-php-for-sql-server.md).
- Support for Azure Active Directory Authentication with ActiveDirectoryPassword and SqlPassword. For more information, see [Connection Options](../../connect/php/connection-options.md).

## What's New in Version 4.0

- Support for PHP 7.0  
- Full 64-bit support
- Support for Ubuntu 15.04, Ubuntu 16.04, and RedHat 7

## What's New in Version 3.2

- Support for PHP 5.6   
- Includes latest updates for prior PHP versions 5.5 and 5.4   
- Requires Microsoft ODBC Driver 11 for SQL Server  

## What's New in Version 3.1

- Support for PHP 5.5  
- Requires Microsoft ODBC Driver 11 for SQL Server. Previous versions required SQL Native Client.  

## What's New in Version 3.0  

- Support for PHP 5.4.  PHP 5.2 is not supported in version 3 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
- AttachDBFileName connection option is added. For more information, see [Connection Options](../../connect/php/connection-options.md).  
- Support for LocalDB, which was added in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]. For more information, see [Support for LocalDB](../../connect/php/php-driver-for-sql-server-support-for-localdb.md).
- AttachDBFileName connection option is added. For more information, see [Connection Options](../../connect/php/connection-options.md).  
- Support for the high-availability, disaster recovery features. For more information, see [Support for High Availability, Disaster Recovery](../../connect/php/php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md).
- Support for client-side cursors (caching a result set in-memory). For more information, see [Cursor Types &#40;SQLSRV Driver&#41;](../../connect/php/cursor-types-sqlsrv-driver.md) and [Cursor Types &#40;PDO_SQLSRV Driver&#41;](../../connect/php/cursor-types-pdo-sqlsrv-driver.md).
- The PDO::ATTR_EMULATE_PREPARES attribute has been added. For more information, see [PDO::prepare](../../connect/php/pdo-prepare.md).  

## What's New in Version 2.0  
In version 2.0, support for the PDO_SQLSRV driver was added. For more information, see [PDO_SQLSRV Driver Reference](../../connect/php/pdo-sqlsrv-driver-reference.md).  

## See Also  
[Overview of the Microsoft Drivers for PHP for SQL Server](../../connect/php/overview-of-the-php-sql-driver.md)
