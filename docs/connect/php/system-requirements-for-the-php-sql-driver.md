---
title: "System Requirements for the Microsoft Drivers for PHP for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "02/11/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "requirements"
ms.assetid: 5db4b75f-c605-4785-9560-399a533c0fc9
author: MightyPen
ms.author: genemi
manager: craigg
---

# System Requirements for the Microsoft Drivers for PHP for SQL Server
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This document lists the components that must be installed on your system to access data in a SQL Server or Azure SQL Database using the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].

Versions 3.1 and later of the Microsoft PHP Drivers for SQL Server are officially supported. For full details on support lifecycles and requirements including earlier versions of the PHP drivers, see the [support matrix](../../connect/php/microsoft-php-drivers-for-sql-server-support-matrix.md).

## PHP

For information about how to download and install the latest stable PHP binaries, see [the PHP web site](https://php.net).  The Microsoft Drivers for PHP for SQL Server require the following versions of PHP:

|PHP for SQL Server driver version &#8594;<br />&#8595; PHP version|5.6|5.3|5.2|4.3|4.0|3.2|3.1|
|:---:|---|---|---|---|---|---|---|
|7.3|7.3.0+ | | | | | | |
|7.2|7.2+<sup>1</sup>|7.2+<sup>1</sup>|7.2+<sup>1</sup>| | | | |
|7.1|7.1.0+ |7.1.0+ |7.1.0+ |7.1.0+ |       |        |        |
|7.0|       |7.0.0+ |7.0.0+ |7.0.0+ |7.0.0+ |        |        |
|5.6|       |       |       |       |       |5.6.4+  |        |
|5.5|       |       |       |       |       |5.5.16+ |5.5.16+ |
|5.4|       |       |       |       |       |5.4.32  |5.4.32  |

1. Versions 7.2.1 and later are supported on Windows, while versions 7.2.0 and later are supported on Linux and macOS.

-   A version of the driver file must be in your PHP extension directory. See [Driver Versions](#driver-versions) for information about the different driver files.  To download the drivers, see [Download the Microsoft Drivers for PHP for SQL Server](../../connect/php/download-drivers-php-sql-server.md). For information on configuring the driver for the PHP, see [Loading the Microsoft Drivers for PHP for SQL Server](../../connect/php/loading-the-php-sql-driver.md).

-   A Web server is required. Your Web server must be configured to run PHP. For information about hosting PHP applications with IIS, see the [tutorial on PHP's web site](http://docs.php.net/manual/da/install.windows.iis7.php).

    The [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] has been tested using IIS 10 with FastCGI.  

    > [!NOTE]  
    > Microsoft provides support only for IIS.  

## ODBC Driver

The correct version of the Microsoft ODBC Driver for SQL Server is required on the computer on which PHP is running. You can download all supported versions of the driver for supported platforms on [this page](https://docs.microsoft.com/sql/connect/odbc/download-odbc-driver-for-sql-server?view=sql-server-2017).

If you are downloading the Windows version of the driver on a 64-bit version of Windows, the ODBC 64-bit installer installs both 32-bit and 64-bit ODBC drivers. If you use a 32-bit version of Windows, use the ODBC x86 installer. On non-Windows platforms, only 64-bit versions of the driver are available.

|PHP for SQL Server driver version &#8594;<br />&#8595; ODBC Driver version|5.6|5.3|5.2|4.3|4.0|3.2|3.1|
|---|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
|ODBC Driver 17+ |Y|Y|Y| | | | |
|ODBC Driver 13.1|Y|Y|Y|Y|Y| | |
|ODBC Driver 13  | | | | |Y| | |
|ODBC Driver 11  |Y|Y|Y|Y|Y|Y|Y|

If you are using the SQLSRV driver, [sqlsrv_client_info](../../connect/php/sqlsrv-client-info.md) returns information about which version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Microsoft ODBC Driver for SQL Server is being used by the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]. If you are using the PDO_SQLSRV driver, you can use [PDO::getAttribute](../../connect/php/pdo-getattribute.md) to discover the version.  

## SQL Server

Azure SQL Databases are supported. For information, see [Connecting to Microsoft Azure SQL Database](../../connect/php/connecting-to-microsoft-azure-sql-database.md).

|PHP for SQL Server driver version &#8594;<br />&#8595; SQL Server version|5.6|5.3|5.2|4.3|4.0|3.2|3.1|
|---|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
|Azure SQL Database        |Y|Y|Y|Y| | | |
|Azure SQL Managed Instance|Y|Y|Y|Y| | | |
|Azure SQL Data Warehouse  |Y|Y|Y|Y| | | |
|SQL Server 2017           |Y|Y|Y|Y| | | |
|SQL Server 2016           |Y|Y|Y|Y|Y| | |
|SQL Server 2014           |Y|Y|Y|Y|Y|Y|Y|
|SQL Server 2012           |Y|Y|Y|Y|Y|Y|Y|
|SQL Server 2008 R2        |Y|Y|Y|Y|Y|Y|Y|
|SQL Server 2008           | | | | |Y|Y|Y|

## Operating Systems
Supported operating systems for each version of the driver are as follows:

|PHP for SQL Server driver version &#8594;<br />&#8595; Operating system|5.6|5.3|5.2|4.3|4.0|3.2|3.1|
|---|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
|Windows Server 2016                 |Y  |Y  |Y  |Y  |   |   |   |
|Windows Server 2012 R2              |Y  |Y  |Y  |Y  |Y  |Y  |Y  |
|Windows Server 2012                 |Y  |Y  |Y  |Y  |Y  |Y  |Y  |
|Windows Server 2008 R2 SP1          |   |   |   |   |Y  |Y  |Y  |
|Windows Server 2008 SP2             |   |   |   |   |Y  |Y  |Y  |
|Windows 10                          |Y  |Y  |Y  |Y  |Y  |   |   |
|Windows 8.1                         |Y  |Y  |Y  |Y  |Y  |Y  |Y  |
|Windows 8                           |   |   |   |Y  |Y  |Y  |Y  |
|Windows 7 SP1                       |   |   |   |   |Y  |Y  |Y  |
|Windows Vista SP2                   |   |   |   |   |Y  |Y  |Y  |
|Ubuntu 18.10 (64-bit)               |Y  |   |   |   |   |   |   |
|Ubuntu 18.04 (64-bit)               |Y  |Y  |   |   |   |   |   |
|Ubuntu 17.10 (64-bit)               |   |Y  |Y  |   |   |   |   |
|Ubuntu 16.04 (64-bit)               |Y  |Y  |Y  |Y  |Y  |   |   |
|Ubuntu 15.10 (64-bit)               |   |   |   |Y  |   |   |   |
|Ubuntu 15.04 (64-bit)               |   |   |   |   |Y  |   |   |
|Debian 9 (64-bit)                   |Y  |Y  |Y  |   |   |   |   |
|Debian 8 (64-bit)                   |Y  |Y  |Y  |Y  |   |   |   |
|Red Hat Enterprise Linux 7 (64-bit) |Y  |Y  |Y  |Y  |Y  |   |   |
|Suse Enterprise Linux 15 (64-bit)   |Y  |   |   |   |   |   |   |
|Suse Enterprise Linux 12 (64-bit)   |Y  |Y  |Y  |   |   |   |   |
|macOS Mojave (64-bit)               |Y  |   |   |   |   |   |   |
|macOS High Sierra (64-bit)          |Y  |Y  |   |   |   |   |   |
|macOS Sierra (64-bit)               |Y  |Y  |Y  |Y  |   |   |   |
|macOS El Capitan (64-bit)           |   |Y  |Y  |Y  |   |   |   |

## Driver Versions  
This section lists the driver files that are included with each version of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]. Each installation package contains SQLSRV and PDO_SQLSRV driver files in threaded and non-threaded variants. On Windows, they are also available in 32-bit and 64-bit variants. To configure the driver for use with the PHP runtime, follow the installation instructions in [Loading the Microsoft Drivers for PHP for SQL Server](../../connect/php/loading-the-php-sql-driver.md).

On supported versions of Linux and macOS, the appropriate drivers can be installed using PHP's PECL package system, following the [Linux and macOS installation instructions](../../connect/php/installation-tutorial-linux-mac.md). Alternatively, you can download prebuilt binaries for your platform from the [Microsoft Drivers for PHP for SQL Server](https://github.com/Microsoft/msphpsql/releases) Github project page -- the tables below list the files found in the prebuilt binary packages.

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

**Microsoft Drivers 3.1 for PHP for SQL Server:**  

On Windows, the following versions of the driver are included:

|Driver file|PHP version|Thread safe?|Use with PHP .dll|  
|---------------|:---------------:|:----------------:|---------------------|  
|php_sqlsrv_54_nts.dll<br />php_pdo_sqlsrv_54_nts.dll|5.4|no|php5.dll|  
|php_sqlsrv_54_ts.dll<br />php_pdo_sqlsrv_54_ts.dll|5.4|yes|php5ts.dll|  
|php_sqlsrv_55_nts.dll<br />php_pdo_sqlsrv_55_nts.dll|5.5|no|php5.dll|  
|php_sqlsrv_55_ts.dll<br />php_pdo_sqlsrv_55_ts.dll|5.5|yes|php5ts.dll|  

## See Also  
[Getting Started with the Microsoft Drivers for PHP for SQL Server](../../connect/php/getting-started-with-the-php-sql-driver.md)

[Programming Guide for the Microsoft Drivers for PHP for SQL Server](../../connect/php/programming-guide-for-php-sql-driver.md)

[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)

[PDO_SQLSRV Driver API Reference](../../connect/php/pdo-sqlsrv-driver-reference.md)  
