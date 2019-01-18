---
title: "Microsoft Drivers for PHP for SQL Server Support Matrix | Microsoft Docs"
ms.custom: ""
ms.date: "07/20/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
author: "David-Engel"
ms.author: "v-daveng"
manager: ""
---

# Microsoft PHP Drivers for SQL Server Support Matrix
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

  This page contains the support matrix and support lifecycle policy for the Microsoft PHP Drivers for SQL Server.

## Microsoft PHP Drivers Support Lifecycle Matrix and Policy
 The Microsoft Support Lifecycle (MSL) policy provides transparent, predictable information regarding the support lifecycle of Microsoft products. PHP Drivers versions 3.x, 4.x, and 5.x have five years of Mainstream support from the driver release date. Mainstream support is defined on the [Microsoft support lifecycle website](https://support.microsoft.com/lifecycle).

 Extended and custom support options are not available for the Microsoft PHP Drivers.

 The following Microsoft PHP Drivers are supported, until the indicated End of Support date.

|Driver Name|Driver Package Version|End of Mainstream Support|
|-|-|-|
|Microsoft PHP Drivers 5.3 for SQL Server|5.3|July 20, 2023|
|Microsoft PHP Drivers 5.2 for SQL Server|5.2|February 9, 2023|
|Microsoft PHP Drivers 4.3 for SQL Server|4.3|July 6, 2022|
|Microsoft PHP Drivers 4.0 for SQL Server|4.0|July 11, 2021|
|Microsoft PHP Drivers 3.2 for SQL Server|3.2|March 9, 2020|
|Microsoft PHP Drivers 3.1 for SQL Server|3.1|December 12, 2019|

 The following Microsoft PHP Drivers are no longer supported.

|Driver Name|Driver Package Version|End of Mainstream Support|
|-|-|-|
|Microsoft PHP Drivers 3.0 for SQL Server|3.0|March 6, 2017|
|Microsoft PHP Drivers 2.0 for SQL Server|2.0|August 10, 2015|
|Microsoft PHP Drivers 1.0 for SQL Server|1.0|April 28, 2014|

## SQL Server Version Certified Compatibility
 The following matrix lists SQL Server versions that have been tested and certified as compatible with the corresponding driver version. We strive to maintain backward compatibility with previous driver versions, but only the latest supported driver is tested and certified with new SQL Server versions as SQL Server is released.

|PHP for SQL Server driver version &#8594;<br />&#8595; SQL Server version|5.3 and 5.2<br />&nbsp;|4.3<br />&nbsp;|4.0<br />&nbsp;|3.2<br />&nbsp;|3.1<br />&nbsp;|3.0<br />&nbsp;|2.0<br />&nbsp;|
|---|---|---|---|---|---|---|---|
|Azure SQL Managed Instance<br/> (Extended Private Preview)|Y|Y| | | | | |
|Azure SQL Data Warehouse|Y|Y| | | | | |
|SQL Server 2017   |Y|Y| | | | | |
|SQL Server 2016   |Y|Y|Y| | | | |
|SQL Server 2014   |Y|Y|Y|Y|Y| | |
|SQL Server 2012   |Y|Y|Y|Y|Y|Y| |
|SQL Server 2008 R2|Y|Y|Y|Y|Y|Y|Y|
|SQL Server 2008   | | |Y|Y|Y|Y|Y|

## PHP Version Support
 The following versions of PHP are supported with the listed version of the Microsoft PHP Drivers:

|PHP for SQL Server driver version &#8594;<br />&#8595; PHP version|5.3 and 5.2<br />&nbsp;|4.3<br />&nbsp;|4.0<br />&nbsp;|3.2<br />&nbsp;|3.1<br />&nbsp;|3.0<br />&nbsp;|2.0<br />&nbsp;|
|---|---|---|---|---|---|---|---|
|7.2|7.2.1+ on Windows<br/>7.2.0+ on other platforms| | | | | | |
|7.1|7.1.0+ |7.1.0+ |       |        |        |        |        |
|7.0|7.0.0+ |7.0.0+ |7.0.0+ |        |        |        |        |
|5.6|       |       |       |5.6.4+  |        |        |        |
|5.5|       |       |       |5.5.16+ |5.5.16+ |        |        |
|5.4|       |       |       |5.4.32  |5.4.32  |5.4.32  |        |
|5.3|       |       |       |        |        |5.3.0   |5.3.0   |
|5.2|       |       |       |        |        |        |5.2.4<br />5.2.13|

## Supported Operating Systems
 The following Windows operating system versions are supported with the listed version of the Microsoft PHP Drivers:

|PHP for SQL Server driver version &#8594;<br />&#8595; Operating system|5.3 and 5.2<br />&nbsp;|4.3<br />&nbsp;|4.0<br />&nbsp;|3.2<br />&nbsp;|3.1<br />&nbsp;|3.0<br />&nbsp;|2.0<br />&nbsp;|
|---|---|---|---|---|---|---|---|
|Windows Server 2016                 |Y  |Y  |   |   |   |   |   |
|Windows Server 2012 R2              |Y  |Y  |Y  |Y  |Y  |   |   |
|Windows Server 2012                 |Y  |Y  |Y  |Y  |Y  |   |   |
|Windows Server 2008 R2 SP1          |   |   |Y  |Y  |Y  |Y  |   |
|Windows Server 2008 R2              |   |   |   |   |   |   |Y  |
|Windows Server 2008 SP2             |   |   |Y  |Y  |Y  |Y  |   |
|Windows Server 2008                 |   |   |   |   |   |   |Y  |
|Windows Server 2003 SP1             |   |   |   |   |   |   |Y  |
|Windows 10                          |Y  |Y  |Y  |   |   |   |   |
|Windows 8.1                         |Y  |Y  |Y  |Y  |Y  |   |   |
|Windows 8                           |   |Y  |Y  |Y  |Y  |   |   |
|Windows 7 SP1                       |   |   |Y  |Y  |Y  |Y  |   |
|Windows Vista SP2                   |   |   |Y  |Y  |Y  |Y  |Y  |
|Windows XP SP3                      |   |   |   |   |   |   |Y  |

 The following Linux and Mac operating system versions (64-bit only) are supported with the listed version of the Microsoft PHP Drivers:

|PHP for SQL Server driver version &#8594;<br />&#8595; Operating system|5.3<br />&nbsp;|5.2<br />&nbsp;|4.3<br />&nbsp;|4.0<br />&nbsp;|3.2<br />&nbsp;|3.1<br />&nbsp;|3.0<br />&nbsp;|2.0<br />&nbsp;|
|--|---|---|---|---|---|---|---|---|
|Ubuntu 18.04 (64-bit)               |Y  |   |   |   |   |   |   |   |
|Ubuntu 17.10 (64-bit)               |Y  |Y  |   |   |   |   |   |   |
|Ubuntu 16.04 (64-bit)               |Y  |Y  |Y  |Y  |   |   |   |   |
|Ubuntu 15.10 (64-bit)               |   |   |Y  |   |   |   |   |   |
|Ubuntu 15.04 (64-bit)               |   |   |   |Y  |   |   |   |   |
|Debian 9 (64-bit)                   |Y  |Y  |   |   |   |   |   |   |
|Debian 8 (64-bit)                   |Y  |Y  |Y  |   |   |   |   |   |
|Red Hat Enterprise Linux 7 (64-bit) |Y  |Y  |Y  |Y  |   |   |   |   |
|Suse Enterprise Linux 12 (64-bit)   |Y  |Y  |   |   |   |   |   |   |
|macOS High Sierra (64-bit)          |Y  |   |   |   |   |   |   |   |
|macOS Sierra (64-bit)               |Y  |Y  |Y  |   |   |   |   |   |
|macOS El Capitan (64-bit)           |Y  |Y  |Y  |   |   |   |   |   |

## See Also  
[Release Notes](../../connect/php/release-notes-for-the-php-sql-driver.md)

[Support Resources](../../connect/php/support-resources-for-the-php-sql-driver.md)

[System Requirements](../../connect/php/system-requirements-for-the-php-sql-driver.md)
