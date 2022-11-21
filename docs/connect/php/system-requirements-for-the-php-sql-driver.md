---
title: System requirements
description: The Microsoft Drivers for PHP for SQL Server support a wide range of PHP versions, operating systems, and SQL Server versions.
author: David-Engel
ms.author: v-davidengel
ms.date: 02/17/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "requirements"
---

# System requirements for the Microsoft Drivers for PHP for SQL Server

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This article lists the components that must be installed on your system to access data in a SQL Server or Azure SQL Database using the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].

Versions 4.3 and later of the Microsoft PHP Drivers for SQL Server are officially supported. For full details on support lifecycles and requirements of the PHP drivers, see the [support matrix](microsoft-php-drivers-for-sql-server-support-matrix.md).

## PHP

For information about how to download and install the latest stable PHP binaries, see [the PHP web site](https://php.net).  The Microsoft Drivers for PHP for SQL Server require the right versions of PHP as detailed in [PHP Version support](microsoft-php-drivers-for-sql-server-support-matrix.md#php-version-support).

- The correct version of the driver file must be enabled with its corresponding PHP version. See [Driver Versions](#driver-versions) for information about the different driver files. To download the drivers, see [Download the Microsoft Drivers for PHP for SQL Server](download-drivers-php-sql-server.md). For information on configuring the driver for the PHP, see [Loading the Microsoft Drivers for PHP for SQL Server](loading-the-php-sql-driver.md).

- If a Web server is used, your Web server must be configured to run PHP. For information about hosting PHP applications with IIS, see the [tutorial on PHP's web site](http://docs.php.net/manual/da/install.windows.iis7.php).

    The [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] has been tested using IIS 10 with FastCGI.

> [!NOTE]
> Microsoft provides support only for IIS.

## ODBC driver

The correct version of the Microsoft ODBC Driver for SQL Server is required on the computer on which PHP is running. You can download all supported versions of the driver for supported platforms on [this page](../odbc/download-odbc-driver-for-sql-server.md).

If you're downloading the Windows version of the driver on a 64-bit version of Windows, the ODBC 64-bit installer installs both 32-bit and 64-bit ODBC drivers. If you use a 32-bit version of Windows, use the ODBC x86 installer. On non-Windows platforms, only 64-bit versions of the driver are available.

|PHP driver version &#8594;<br />&#8595; ODBC driver version|5.10|5.9|5.8|5.6|5.3|5.2|4.3|4.0|3.2|
|---|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
|ODBC Driver 18  |Yes|   |   |   |   |   |   |   |   |
|ODBC Driver 17  |Yes|Yes|Yes|Yes|Yes|Yes|   |   |   |
|ODBC Driver 13.1|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |
|ODBC Driver 13  |   |   |   |   |   |   |   |Yes|   |
|ODBC Driver 11  |   |   |Yes|Yes|Yes|Yes|Yes|Yes|Yes|

If you're using the SQLSRV driver, [sqlsrv_client_info](sqlsrv-client-info.md) returns information about which version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Microsoft ODBC Driver for SQL Server is being used by the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]. If you're using the PDO_SQLSRV driver, you can use [PDO::getAttribute](pdo-getattribute.md) to discover the version.

## SQL Server

See the [supported database versions](microsoft-php-drivers-for-sql-server-support-matrix.md#sql-server-version-certified-compatibility) for details on which SQL Server versions are supported.

## Operating systems

See the [supported operating systems](microsoft-php-drivers-for-sql-server-support-matrix.md#supported-operating-systems) for details on which operating systems are supported.

## Driver versions

This section lists the driver files that are included with each version of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]. Each installation package contains SQLSRV and PDO_SQLSRV driver files in threaded and non-threaded variants. On Windows, they're also available in 32-bit and 64-bit variants. To configure the driver for use with the PHP runtime, follow the installation instructions in [Loading the Microsoft Drivers for PHP for SQL Server](loading-the-php-sql-driver.md).

On supported versions of Linux and macOS, the appropriate drivers can be installed using PHP's PECL package system, following the [Linux and macOS installation instructions](installation-tutorial-linux-mac.md). Instead, you can download prebuilt binaries for your platform from the [Microsoft Drivers for PHP for SQL Server](https://github.com/Microsoft/msphpsql/releases) GitHub project page. The tables below list the files found in the prebuilt binary packages.

**Microsoft Drivers 5.10 for PHP for SQL Server:**

On Windows, the following driver files are provided:

|Driver file|PHP version|Thread safe?|Use with PHP .dll|
|---------------|:---------------:|:----------------:|---------------------|
|32-bit php_sqlsrv_74_nts.dll<br />32-bit php_pdo_sqlsrv_74_nts.dll|7.4|no |32-bit php7.dll|
|32-bit php_sqlsrv_74_ts.dll <br />32-bit php_pdo_sqlsrv_74_ts.dll |7.4|yes|32-bit php7ts.dll|
|64-bit php_sqlsrv_74_nts.dll<br />64-bit php_pdo_sqlsrv_74_nts.dll|7.4|no |64-bit php7.dll|
|64-bit php_sqlsrv_74_ts.dll <br />64-bit php_pdo_sqlsrv_74_ts.dll |7.4|yes|64-bit php7ts.dll|
|32-bit php_sqlsrv_80_nts.dll<br />32-bit php_pdo_sqlsrv_80_nts.dll|8.0|no |32-bit php8.dll|
|32-bit php_sqlsrv_80_ts.dll <br />32-bit php_pdo_sqlsrv_80_ts.dll |8.0|yes|32-bit php8ts.dll|
|64-bit php_sqlsrv_80_nts.dll<br />64-bit php_pdo_sqlsrv_80_nts.dll|8.0|no |64-bit php8.dll|
|64-bit php_sqlsrv_80_ts.dll <br />64-bit php_pdo_sqlsrv_80_ts.dll |8.0|yes|64-bit php8ts.dll|
|32-bit php_sqlsrv_81_nts.dll<br />32-bit php_pdo_sqlsrv_81_nts.dll|8.1|no |32-bit php8.dll|
|32-bit php_sqlsrv_81_ts.dll <br />32-bit php_pdo_sqlsrv_81_ts.dll |8.1|yes|32-bit php8ts.dll|
|64-bit php_sqlsrv_81_nts.dll<br />64-bit php_pdo_sqlsrv_81_nts.dll|8.1|no |64-bit php8.dll|
|64-bit php_sqlsrv_81_ts.dll <br />64-bit php_pdo_sqlsrv_81_ts.dll |8.1|yes|64-bit php8ts.dll|

On Linux, the following driver files are provided:

|Driver file|PHP version|Thread safe?|
|---------------|:---------------:|:----------------:|
|php_sqlsrv_74_nts.so<br />php_pdo_sqlsrv_74_nts.so|7.4|no |
|php_sqlsrv_74_ts.so <br />php_pdo_sqlsrv_74_ts.so |7.4|yes|
|php_sqlsrv_80_nts.so<br />php_pdo_sqlsrv_80_nts.so|8.0|no |
|php_sqlsrv_80_ts.so <br />php_pdo_sqlsrv_80_ts.so |8.0|yes|
|php_sqlsrv_81_nts.so<br />php_pdo_sqlsrv_81_nts.so|8.1|no |
|php_sqlsrv_81_ts.so <br />php_pdo_sqlsrv_81_ts.so |8.1|yes|

**Microsoft Drivers 5.9 for PHP for SQL Server:**

On Windows, the following driver files are provided:

|Driver file|PHP version|Thread safe?|Use with PHP .dll|
|---------------|:---------------:|:----------------:|---------------------|
|32-bit php_sqlsrv_73_nts.dll<br />32-bit php_pdo_sqlsrv_73_nts.dll|7.3|no |32-bit php7.dll|
|32-bit php_sqlsrv_73_ts.dll <br />32-bit php_pdo_sqlsrv_73_ts.dll |7.3|yes|32-bit php7ts.dll|
|64-bit php_sqlsrv_73_nts.dll<br />64-bit php_pdo_sqlsrv_73_nts.dll|7.3|no |64-bit php7.dll|
|64-bit php_sqlsrv_73_ts.dll <br />64-bit php_pdo_sqlsrv_73_ts.dll |7.3|yes|64-bit php7ts.dll|
|32-bit php_sqlsrv_74_nts.dll<br />32-bit php_pdo_sqlsrv_74_nts.dll|7.4|no |32-bit php7.dll|
|32-bit php_sqlsrv_74_ts.dll <br />32-bit php_pdo_sqlsrv_74_ts.dll |7.4|yes|32-bit php7ts.dll|
|64-bit php_sqlsrv_74_nts.dll<br />64-bit php_pdo_sqlsrv_74_nts.dll|7.4|no |64-bit php7.dll|
|64-bit php_sqlsrv_74_ts.dll <br />64-bit php_pdo_sqlsrv_74_ts.dll |7.4|yes|64-bit php7ts.dll|
|32-bit php_sqlsrv_80_nts.dll<br />32-bit php_pdo_sqlsrv_80_nts.dll|8.0|no |32-bit php8.dll|
|32-bit php_sqlsrv_80_ts.dll <br />32-bit php_pdo_sqlsrv_80_ts.dll |8.0|yes|32-bit php8ts.dll|
|64-bit php_sqlsrv_80_nts.dll<br />64-bit php_pdo_sqlsrv_80_nts.dll|8.0|no |64-bit php8.dll|
|64-bit php_sqlsrv_80_ts.dll <br />64-bit php_pdo_sqlsrv_80_ts.dll |8.0|yes|64-bit php8ts.dll|

On Linux, the following driver files are provided:

|Driver file|PHP version|Thread safe?|
|---------------|:---------------:|:----------------:|
|php_sqlsrv_73_nts.so<br />php_pdo_sqlsrv_73_nts.so|7.3|no |
|php_sqlsrv_73_ts.so <br />php_pdo_sqlsrv_73_ts.so |7.3|yes|
|php_sqlsrv_74_nts.so<br />php_pdo_sqlsrv_74_nts.so|7.4|no |
|php_sqlsrv_74_ts.so <br />php_pdo_sqlsrv_74_ts.so |7.4|yes|
|php_sqlsrv_80_nts.so<br />php_pdo_sqlsrv_80_nts.so|8.0|no |
|php_sqlsrv_80_ts.so <br />php_pdo_sqlsrv_80_ts.so |8.0|yes|

**Microsoft Drivers 5.8 for PHP for SQL Server:**

On Windows, the following versions of the driver are included:

|Driver file|PHP version|Thread safe?|Use with PHP .dll|
|---------------|:---------------:|:----------------:|---------------------|
|32-bit php_sqlsrv_72_nts.dll<br />32-bit php_pdo_sqlsrv_72_nts.dll|7.2|no |32-bit php7.dll|
|32-bit php_sqlsrv_72_ts.dll <br />32-bit php_pdo_sqlsrv_72_ts.dll |7.2|yes|32-bit php7ts.dll|
|64-bit php_sqlsrv_72_nts.dll<br />64-bit php_pdo_sqlsrv_72_nts.dll|7.2|no |64-bit php7.dll|
|64-bit php_sqlsrv_72_ts.dll <br />64-bit php_pdo_sqlsrv_72_ts.dll |7.2|yes|64-bit php7ts.dll|
|32-bit php_sqlsrv_73_nts.dll<br />32-bit php_pdo_sqlsrv_73_nts.dll|7.3|no |32-bit php7.dll|
|32-bit php_sqlsrv_73_ts.dll <br />32-bit php_pdo_sqlsrv_73_ts.dll |7.3|yes|32-bit php7ts.dll|
|64-bit php_sqlsrv_73_nts.dll<br />64-bit php_pdo_sqlsrv_73_nts.dll|7.3|no |64-bit php7.dll|
|64-bit php_sqlsrv_73_ts.dll <br />64-bit php_pdo_sqlsrv_73_ts.dll |7.3|yes|64-bit php7ts.dll|
|32-bit php_sqlsrv_74_nts.dll<br />32-bit php_pdo_sqlsrv_74_nts.dll|7.4|no |32-bit php7.dll|
|32-bit php_sqlsrv_74_ts.dll <br />32-bit php_pdo_sqlsrv_74_ts.dll |7.4|yes|32-bit php7ts.dll|
|64-bit php_sqlsrv_74_nts.dll<br />64-bit php_pdo_sqlsrv_74_nts.dll|7.4|no |64-bit php7.dll|
|64-bit php_sqlsrv_74_ts.dll <br />64-bit php_pdo_sqlsrv_74_ts.dll |7.4|yes|64-bit php7ts.dll|

On Linux, the following versions of the driver are included:

|Driver file|PHP version|Thread safe?|
|---------------|:---------------:|:----------------:|
|php_sqlsrv_72_nts.so<br />php_pdo_sqlsrv_72_nts.so|7.2|no |
|php_sqlsrv_72_ts.so <br />php_pdo_sqlsrv_72_ts.so |7.2|yes|
|php_sqlsrv_73_nts.so<br />php_pdo_sqlsrv_73_nts.so|7.3|no |
|php_sqlsrv_73_ts.so <br />php_pdo_sqlsrv_73_ts.so |7.3|yes|
|php_sqlsrv_74_nts.so<br />php_pdo_sqlsrv_74_nts.so|7.4|no |
|php_sqlsrv_74_ts.so <br />php_pdo_sqlsrv_74_ts.so |7.4|yes|

**Microsoft Drivers 5.6 for PHP for SQL Server:**

On Windows, the following versions of the driver are included:

|Driver file|PHP version|Thread safe?|Use with PHP .dll|
|---------------|:---------------:|:----------------:|---------------------|
|32-bit php_sqlsrv_71_nts.dll<br />32-bit php_pdo_sqlsrv_71_nts.dll|7.1|no |32-bit php7.dll|
|32-bit php_sqlsrv_71_ts.dll <br />32-bit php_pdo_sqlsrv_71_ts.dll |7.1|yes|32-bit php7ts.dll|
|64-bit php_sqlsrv_71_nts.dll<br />64-bit php_pdo_sqlsrv_71_nts.dll|7.1|no |64-bit php7.dll|
|64-bit php_sqlsrv_71_ts.dll <br />64-bit php_pdo_sqlsrv_71_ts.dll |7.1|yes|64-bit php7ts.dll|
|32-bit php_sqlsrv_72_nts.dll<br />32-bit php_pdo_sqlsrv_72_nts.dll|7.2|no |32-bit php7.dll|
|32-bit php_sqlsrv_72_ts.dll <br />32-bit php_pdo_sqlsrv_72_ts.dll |7.2|yes|32-bit php7ts.dll|
|64-bit php_sqlsrv_72_nts.dll<br />64-bit php_pdo_sqlsrv_72_nts.dll|7.2|no |64-bit php7.dll|
|64-bit php_sqlsrv_72_ts.dll <br />64-bit php_pdo_sqlsrv_72_ts.dll |7.2|yes|64-bit php7ts.dll|
|32-bit php_sqlsrv_73_nts.dll<br />32-bit php_pdo_sqlsrv_73_nts.dll|7.3|no |32-bit php7.dll|
|32-bit php_sqlsrv_73_ts.dll <br />32-bit php_pdo_sqlsrv_73_ts.dll |7.3|yes|32-bit php7ts.dll|
|64-bit php_sqlsrv_73_nts.dll<br />64-bit php_pdo_sqlsrv_73_nts.dll|7.3|no |64-bit php7.dll|
|64-bit php_sqlsrv_73_ts.dll <br />64-bit php_pdo_sqlsrv_73_ts.dll |7.3|yes|64-bit php7ts.dll|

On Linux, the following versions of the driver are included:

|Driver file|PHP version|Thread safe?|
|---------------|:---------------:|:----------------:|
|php_sqlsrv_71_nts.so<br />php_pdo_sqlsrv_71_nts.so|7.1|no |
|php_sqlsrv_71_ts.so <br />php_pdo_sqlsrv_71_ts.so |7.1|yes|
|php_sqlsrv_72_nts.so<br />php_pdo_sqlsrv_72_nts.so|7.2|no |
|php_sqlsrv_72_ts.so <br />php_pdo_sqlsrv_72_ts.so |7.2|yes|
|php_sqlsrv_73_nts.so<br />php_pdo_sqlsrv_73_nts.so|7.3|no |
|php_sqlsrv_73_ts.so <br />php_pdo_sqlsrv_73_ts.so |7.3|yes|

**Microsoft Drivers 5.3 for PHP for SQL Server:**

On Windows, the following versions of the driver are included:

|Driver file|PHP version|Thread safe?|Use with PHP .dll|
|---------------|:---------------:|:----------------:|---------------------|
|32-bit php_sqlsrv_7_nts.dll <br />32-bit php_pdo_sqlsrv_7_nts.dll |7.0|no |32-bit php7.dll|
|32-bit php_sqlsrv_7_ts.dll  <br />32-bit php_pdo_sqlsrv_7_ts.dll  |7.0|yes|32-bit php7ts.dll|
|64-bit php_sqlsrv_7_nts.dll <br />64-bit php_pdo_sqlsrv_7_nts.dll |7.0|no |64-bit php7.dll|
|64-bit php_sqlsrv_7_ts.dll  <br />64-bit php_pdo_sqlsrv_7_ts.dll  |7.0|yes|64-bit php7ts.dll|
|32-bit php_sqlsrv_71_nts.dll<br />32-bit php_pdo_sqlsrv_71_nts.dll|7.1|no |32-bit php7.dll|
|32-bit php_sqlsrv_71_ts.dll <br />32-bit php_pdo_sqlsrv_71_ts.dll |7.1|yes|32-bit php7ts.dll|
|64-bit php_sqlsrv_71_nts.dll<br />64-bit php_pdo_sqlsrv_71_nts.dll|7.1|no |64-bit php7.dll|
|64-bit php_sqlsrv_71_ts.dll <br />64-bit php_pdo_sqlsrv_71_ts.dll |7.1|yes|64-bit php7ts.dll|
|32-bit php_sqlsrv_72_nts.dll<br />32-bit php_pdo_sqlsrv_72_nts.dll|7.2|no |32-bit php7.dll|
|32-bit php_sqlsrv_72_ts.dll <br />32-bit php_pdo_sqlsrv_72_ts.dll |7.2|yes|32-bit php7ts.dll|
|64-bit php_sqlsrv_72_nts.dll<br />64-bit php_pdo_sqlsrv_72_nts.dll|7.2|no |64-bit php7.dll|
|64-bit php_sqlsrv_72_ts.dll <br />64-bit php_pdo_sqlsrv_72_ts.dll |7.2|yes|64-bit php7ts.dll|

On Linux, the following versions of the driver are included:

|Driver file|PHP version|Thread safe?|
|---------------|:---------------:|:----------------:|
|php_sqlsrv_7_nts.so <br />php_pdo_sqlsrv_7_nts.so |7.0|no |
|php_sqlsrv_7_ts.so  <br />php_pdo_sqlsrv_7_ts.so  |7.0|yes|
|php_sqlsrv_71_nts.so<br />php_pdo_sqlsrv_71_nts.so|7.1|no |
|php_sqlsrv_71_ts.so <br />php_pdo_sqlsrv_71_ts.so |7.1|yes|
|php_sqlsrv_72_nts.so<br />php_pdo_sqlsrv_72_nts.so|7.2|no |
|php_sqlsrv_72_ts.so <br />php_pdo_sqlsrv_72_ts.so |7.2|yes|

**Microsoft Drivers 5.2 for PHP for SQL Server:**

On Windows, the following versions of the driver are included:

|Driver file|PHP version|Thread safe?|Use with PHP .dll|
|---------------|:---------------:|:----------------:|---------------------|
|32-bit php_sqlsrv_7_nts.dll <br />32-bit php_pdo_sqlsrv_7_nts.dll |7.0|no |32-bit php7.dll|
|32-bit php_sqlsrv_7_ts.dll  <br />32-bit php_pdo_sqlsrv_7_ts.dll  |7.0|yes|32-bit php7ts.dll|
|64-bit php_sqlsrv_7_nts.dll <br />64-bit php_pdo_sqlsrv_7_nts.dll |7.0|no |64-bit php7.dll|
|64-bit php_sqlsrv_7_ts.dll  <br />64-bit php_pdo_sqlsrv_7_ts.dll  |7.0|yes|64-bit php7ts.dll|
|32-bit php_sqlsrv_71_nts.dll<br />32-bit php_pdo_sqlsrv_71_nts.dll|7.1|no |32-bit php7.dll|
|32-bit php_sqlsrv_71_ts.dll <br />32-bit php_pdo_sqlsrv_71_ts.dll |7.1|yes|32-bit php7ts.dll|
|64-bit php_sqlsrv_71_nts.dll<br />64-bit php_pdo_sqlsrv_71_nts.dll|7.1|no |64-bit php7.dll|
|64-bit php_sqlsrv_71_ts.dll <br />64-bit php_pdo_sqlsrv_71_ts.dll |7.1|yes|64-bit php7ts.dll|
|32-bit php_sqlsrv_72_nts.dll<br />32-bit php_pdo_sqlsrv_72_nts.dll|7.2|no |32-bit php7.dll|
|32-bit php_sqlsrv_72_ts.dll <br />32-bit php_pdo_sqlsrv_72_ts.dll |7.2|yes|32-bit php7ts.dll|
|64-bit php_sqlsrv_72_nts.dll<br />64-bit php_pdo_sqlsrv_72_nts.dll|7.2|no |64-bit php7.dll|
|64-bit php_sqlsrv_72_ts.dll <br />64-bit php_pdo_sqlsrv_72_ts.dll |7.2|yes|64-bit php7ts.dll|

On Linux, the following versions of the driver are included:

|Driver file|PHP version|Thread safe?|
|---------------|:---------------:|:----------------:|
|php_sqlsrv_7_nts.so <br />php_pdo_sqlsrv_7_nts.so |7.0|no |
|php_sqlsrv_7_ts.so  <br />php_pdo_sqlsrv_7_ts.so  |7.0|yes|
|php_sqlsrv_71_nts.so<br />php_pdo_sqlsrv_71_nts.so|7.1|no |
|php_sqlsrv_71_ts.so <br />php_pdo_sqlsrv_71_ts.so |7.1|yes|
|php_sqlsrv_72_nts.so<br />php_pdo_sqlsrv_72_nts.so|7.2|no |
|php_sqlsrv_72_ts.so <br />php_pdo_sqlsrv_72_ts.so |7.2|yes|

**Microsoft Drivers 4.3 for PHP for SQL Server:**

On Windows, the following versions of the driver are included:

|Driver file|PHP version|Thread safe?|Use with PHP .dll|
|---------------|:---------------:|:----------------:|---------------------|
|32-bit php_sqlsrv_7_nts.dll <br />32-bit php_pdo_sqlsrv_7_nts.dll |7.0|no |32-bit php7.dll|
|32-bit php_sqlsrv_7_ts.dll  <br />32-bit php_pdo_sqlsrv_7_ts.dll  |7.0|yes|32-bit php7ts.dll|
|64-bit php_sqlsrv_7_nts.dll <br />64-bit php_pdo_sqlsrv_7_nts.dll |7.0|no |64-bit php7.dll|
|64-bit php_sqlsrv_7_ts.dll  <br />64-bit php_pdo_sqlsrv_7_ts.dll  |7.0|yes|64-bit php7ts.dll|
|32-bit php_sqlsrv_71_nts.dll<br />32-bit php_pdo_sqlsrv_71_nts.dll|7.1|no |32-bit php7.dll|
|32-bit php_sqlsrv_71_ts.dll <br />32-bit php_pdo_sqlsrv_71_ts.dll |7.1|yes|32-bit php7ts.dll|
|64-bit php_sqlsrv_71_nts.dll<br />64-bit php_pdo_sqlsrv_71_nts.dll|7.1|no |64-bit php7.dll|
|64-bit php_sqlsrv_71_ts.dll <br />64-bit php_pdo_sqlsrv_71_ts.dll |7.1|yes|64-bit php7ts.dll|

On Linux, the following versions of the driver are included:

|Driver file|PHP version|Thread safe?|
|---------------|:---------------:|:----------------:|
|php_sqlsrv_7_nts.so <br />php_pdo_sqlsrv_7_nts.so |7.0|no |
|php_sqlsrv_7_ts.so  <br />php_pdo_sqlsrv_7_ts.so  |7.0|yes|
|php_sqlsrv_71_nts.so<br />php_pdo_sqlsrv_71_nts.so|7.1|no |
|php_sqlsrv_71_ts.so <br />php_pdo_sqlsrv_71_ts.so |7.1|yes|

**Microsoft Drivers 4.0 for PHP for SQL Server:**

On Windows, the following versions of the driver are included:

|Driver file|PHP version|Thread safe?|Use with PHP .dll|
|---------------|:---------------:|:----------------:|---------------------|
|php_sqlsrv_7_nts_x86.dll<br />php_pdo_sqlsrv_7_nts_x86.dll|7.0|no|32-bit php7.dll|
|php_sqlsrv_7_ts_x86.dll<br />php_pdo_sqlsrv_7_ts_x86.dll|7.0|yes|32-bit php7ts.dll|
|php_sqlsrv_7_nts_x64.dll<br />php_pdo_sqlsrv_7_nts_x64.dll|7.0|no|64-bit php7.dll|
|php_sqlsrv_7_ts_x64.dll<br />php_pdo_sqlsrv_7_ts_x64.dll|7.0|yes|64-bit php7ts.dll|

On Linux, the following versions of the driver are included:

|Driver file|PHP version|Thread safe?|
|---------------|:---------------:|:----------------:|
|php_sqlsrv_7_nts.so <br />php_pdo_sqlsrv_7_nts.so |7.0|no |
|php_sqlsrv_7_ts.so  <br />php_pdo_sqlsrv_7_ts.so  |7.0|yes|

**Microsoft Drivers 3.2 for PHP for SQL Server:**

On Windows, the following versions of the driver are included:

|Driver file|PHP version|Thread safe?|Use with PHP .dll|
|---------------|:---------------:|:----------------:|---------------------|
|php_sqlsrv_54_nts.dll<br />php_pdo_sqlsrv_54_nts.dll|5.4|no|php5.dll|
|php_sqlsrv_54_ts.dll<br />php_pdo_sqlsrv_54_ts.dll|5.4|yes|php5ts.dll|
|php_sqlsrv_55_nts.dll<br />php_pdo_sqlsrv_55_nts.dll|5.5|no|php5.dll|
|php_sqlsrv_55_ts.dll<br />php_pdo_sqlsrv_55_ts.dll|5.5|yes|php5ts.dll|
|php_sqlsrv_56_nts.dll<br />php_pdo_sqlsrv_56_nts.dll|5.6|no|php5.dll|
|php_sqlsrv_56_ts.dll<br />php_pdo_sqlsrv_56_ts.dll|5.6|yes|php5ts.dll|

## See also

- [Getting Started with the Microsoft Drivers for PHP for SQL Server](getting-started-with-the-php-sql-driver.md)
- [Programming Guide for the Microsoft Drivers for PHP for SQL Server](programming-guide-for-php-sql-driver.md)
- [SQLSRV Driver API Reference](sqlsrv-driver-api-reference.md)
- [PDO_SQLSRV Driver API Reference](pdo-sqlsrv-driver-reference.md)
