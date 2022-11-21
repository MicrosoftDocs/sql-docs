---
title: "Microsoft Drivers for PHP Support Matrix"
description: "This page contains the support matrix and support lifecycle policy for the Microsoft PHP Drivers for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: 01/24/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Microsoft PHP Drivers for SQL Server Support Matrix

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This page contains the support matrix and support lifecycle policy for the Microsoft PHP Drivers for SQL Server.

## Microsoft PHP Drivers Support Lifecycle Matrix and Policy

The Microsoft Support Lifecycle (MSL) policy provides transparent, predictable information regarding the support lifecycle of Microsoft products. PHP Drivers versions 3.x, 4.x, and 5.x have five years of Mainstream support from the driver release date. Mainstream support is defined on the [Microsoft support lifecycle website](https://support.microsoft.com/lifecycle).

Extended and custom support options are not available for the Microsoft PHP Drivers.

The following Microsoft PHP Drivers are supported, until the indicated End of Support date.

|Driver Name|Driver Package Version|End of Mainstream Support|
|-|:-:|-|
|Microsoft PHP Drivers 5.10 for SQL Server|5.10|January 31, 2027|
|Microsoft PHP Drivers 5.9 for SQL Server|5.9|January 29, 2026|
|Microsoft PHP Drivers 5.8 for SQL Server|5.8|January 31, 2025|
|Microsoft PHP Drivers 5.6 for SQL Server|5.6|February 21, 2024|
|Microsoft PHP Drivers 5.3 for SQL Server|5.3|July 20, 2023|
|Microsoft PHP Drivers 5.2 for SQL Server|5.2|February 9, 2023|
|Microsoft PHP Drivers 4.3 for SQL Server|4.3|July 6, 2022|

The following Microsoft PHP Drivers are no longer supported.

|Driver Name|Driver Package Version|End of Mainstream Support|
|-|:-:|-|
|Microsoft PHP Drivers 4.0 for SQL Server|4.0|July 11, 2021|
|Microsoft PHP Drivers 3.2 for SQL Server|3.2|March 9, 2020|
|Microsoft PHP Drivers 3.1 for SQL Server|3.1|December 12, 2019|
|Microsoft PHP Drivers 3.0 for SQL Server|3.0|March 6, 2017|
|Microsoft PHP Drivers 2.0 for SQL Server|2.0|August 10, 2015|
|Microsoft PHP Drivers 1.0 for SQL Server|1.0|April 28, 2014|

## SQL Server Version Certified Compatibility
 The following matrix lists database versions that have been tested and certified as compatible with the corresponding driver version. We strive to maintain backward compatibility with previous driver versions, but only the latest supported driver is tested and certified with new SQL Server versions as SQL Server is released.

|Driver version&nbsp;&#8594;<br />&#8595; Database version|5.10|5.9|5.8|5.6|5.3|5.2|4.3|4.0|3.2|
|---|---|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
|Azure SQL Database        |Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |
|Azure SQL Managed Instance|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |
|Azure Synapse Analytics   |Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |
|SQL Server 2019           |Yes|Yes|Yes|   |   |   |   |   |   |
|SQL Server 2017           |Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |
|SQL Server 2016           |Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |
|SQL Server 2014           |Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|
|SQL Server 2012           |Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|
|SQL Server 2008 R2        |   |   |   |Yes|Yes|Yes|Yes|Yes|Yes|
|SQL Server 2008           |   |   |   |   |   |   |   |Yes|Yes|

For information about using PHP with Azure SQL Database, see [Connecting to Microsoft Azure SQL Database](connecting-to-microsoft-azure-sql-database.md).

## PHP Version Support

The following versions of PHP are supported with the listed version of the Microsoft PHP Drivers:

|Driver version&nbsp;&#8594;<br />&#8595; PHP version|5.10|5.9|5.8|5.6|5.3|5.2|4.3|4.0|3.2|
|:---:|---|---|---|---|---|---|---|---|---|
|8.1|8.1.0+ |   |   |   |   |   |   |   |   |
|8.0|8.0.0+ |8.0.0+ |   |   |   |   |   |   |   |
|7.4|7.4.0+ |7.4.0+ |7.4.0+ |   |   |   |   |   |   |
|7.3|   |7.3.0+ |7.3.0+ |7.3.0+ |   |   |   |   |   |
|7.2|   |   |7.2+<sup>1</sup>|7.2+<sup>1</sup>|7.2+<sup>1</sup>|7.2+<sup>1</sup>|   |   |   |
|7.1|   |   |   |7.1.0+ |7.1.0+ |7.1.0+ |7.1.0+ |   |   |
|7.0|   |   |   |   |7.0.0+ |7.0.0+ |7.0.0+ |7.0.0+ |   |
|5.6|   |   |   |   |   |   |   |   |5.6.4+ |
|5.5|   |   |   |   |   |   |   |   |5.5.16+|
|5.4|   |   |   |   |   |   |   |   |5.4.32 |

<sup>1</sup> Versions 7.2.1 and later are supported on Windows, while versions 7.2.0 and later are supported on Linux and macOS.

## Supported Operating Systems

The following Windows operating system versions are supported with the listed version of the Microsoft PHP Drivers:

|Driver version&nbsp;&#8594;<br />&#8595; Operating system|5.10|5.9|5.8|5.6|5.3|5.2|4.3|4.0|3.2|
|---|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
|Windows Server 2022                 |Yes|   |   |   |   |   |   |   |   |
|Windows Server 2019                 |Yes|Yes|Yes|Yes|   |   |   |   |   |
|Windows Server 2016                 |Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |
|Windows Server 2012 R2              |Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|
|Windows Server 2012                 |Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|
|Windows Server 2008 R2 SP1          |   |   |   |   |   |   |   |Yes|Yes|
|Windows Server 2008 R2              |   |   |   |   |   |   |   |   |   |
|Windows Server 2008 SP2             |   |   |   |   |   |   |   |Yes|Yes|
|Windows 11                          |Yes|   |   |   |   |   |   |   |   |
|Windows 10                          |Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |
|Windows 8.1                         |Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|
|Windows 8                           |   |   |   |   |   |   |Yes|Yes|Yes|
|Windows 7 SP1                       |   |   |   |   |   |   |   |Yes|Yes|
|Windows Vista SP2                   |   |   |   |   |   |   |   |Yes|Yes|

The following Linux and macOS operating system versions (64-bit only) are supported with the listed version of the Microsoft PHP Drivers:

|Driver version&nbsp;&#8594;<br />&#8595; Operating system|5.10|5.9|5.8|5.6|5.3|5.2|4.3|4.0|3.2|
|---|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
|Ubuntu 21.10 (64-bit)               |Yes|   |   |   |   |   |   |   |   |
|Ubuntu 21.04 (64-bit)               |Yes|   |   |   |   |   |   |   |   |
|Ubuntu 20.10 (64-bit)               |   |Yes|   |   |   |   |   |   |   |
|Ubuntu 20.04 (64-bit)               |Yes|Yes|Yes|   |   |   |   |   |   |
|Ubuntu 19.10 (64-bit)               |   |   |Yes|   |   |   |   |   |   |
|Ubuntu 18.10 (64-bit)               |   |   |   |Yes|   |   |   |   |   |
|Ubuntu 18.04 (64-bit)               |Yes|Yes|Yes|Yes|Yes|   |   |   |   |
|Ubuntu 17.10 (64-bit)               |   |   |   |   |Yes|Yes|   |   |   |
|Ubuntu 16.04 (64-bit)               |   |Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |
|Ubuntu 15.10 (64-bit)               |   |   |   |   |   |   |Yes|   |   |
|Ubuntu 15.04 (64-bit)               |   |   |   |   |   |   |   |Yes|   |
|Debian 11 (64-bit)                  |Yes|   |   |   |   |   |   |   |   |
|Debian 10 (64-bit)                  |Yes|Yes|Yes|Yes|   |   |   |   |   |
|Debian 9 (64-bit)                   |Yes|Yes|Yes|Yes|Yes|Yes|   |   |   |
|Debian 8 (64-bit)                   |   |   |Yes|Yes|Yes|Yes|Yes|   |   |
|Red Hat Enterprise Linux 8 (64-bit) |Yes|Yes|Yes|   |   |   |   |   |   |
|Red Hat Enterprise Linux 7 (64-bit) |Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |
|Suse Enterprise Linux 15 (64-bit)   |Yes|Yes|Yes|Yes|   |   |   |   |   |
|Suse Enterprise Linux 12 (64-bit)   |Yes|Yes|Yes|Yes|Yes|Yes|   |   |   |
|Alpine Linux 3.15 (64-bit)          |Yes|   |   |   |   |   |   |   |   |
|Alpine Linux 3.14 (64-bit)          |Yes|   |   |   |   |   |   |   |   |
|Alpine Linux 3.13 (64-bit)          |Yes|   |   |   |   |   |   |   |   |
|Alpine Linux 3.12 (64-bit)          |   |Yes|   |   |   |   |   |   |   |
|Alpine Linux 3.11 (64-bit)          |   |Yes|Yes<sup>1</sup>|   |   |   |
|macOS Monterey (64-bit)             |Yes|   |   |   |   |   |   |   |   |
|macOS Big Sur (64-bit)              |Yes|Yes|   |   |   |   |   |   |   |
|macOS Catalina (64-bit)             |Yes|Yes|Yes|   |   |   |   |   |   |
|macOS Mojave (64-bit)               |   |Yes|Yes|Yes|   |   |   |   |   |
|macOS High Sierra (64-bit)          |   |   |Yes|Yes|Yes|   |   |   |   |
|macOS Sierra (64-bit)               |   |   |   |Yes|Yes|Yes|Yes|   |   |
|macOS El Capitan (64-bit)           |   |   |   |   |Yes|Yes|Yes|   |   |

<sup>1</sup> Alpine Linux support is experimental for version 5.8.0. Version 5.8.1 introduces production support.

## See Also

[Release Notes](release-notes-php-sql-driver.md)

[Support Resources](support-resources-for-the-php-sql-driver.md)

[System Requirements](system-requirements-for-the-php-sql-driver.md)
