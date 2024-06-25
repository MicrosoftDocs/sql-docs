---
title: Download the Microsoft Drivers for PHP for SQL Server
description: Download the Microsoft Drivers for PHP for SQL Server to develop PHP applications that connect to SQL Server and Azure SQL Database.
author: David-Engel
ms.author: davidengel
ms.date: 01/31/2024
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Download the Microsoft Drivers for PHP for SQL Server

The Microsoft Drivers for PHP for SQL Server enable integration with SQL Server for PHP applications. The drivers are PHP extensions that allow the reading and writing of SQL Server data from within PHP scripts. The drivers provide interfaces for accessing data in Azure SQL Database and in all editions of SQL Server 2012 and later (including Express Editions). The drivers make use of PHP features, including PHP streams, to read and write large objects.

On Linux and macOS, the drivers for PHP are easily downloaded and installed using PECL. See the [Linux and macOS installation tutorial](installation-tutorial-linux-mac.md) for details. If you need to download and install the drivers for PHP on Linux and macOS manually, packages for those platforms can be found on the GitHub release tags.

## Download

Microsoft Drivers 5.12 for PHP for SQL Server is the latest general availability (GA) version.

:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft Drivers for PHP for SQL Server (Windows)](https://go.microsoft.com/fwlink/?linkid=2258816)**  
[GitHub Release Tag v5.12.0 (Linux and macOS packages are available here)](https://github.com/Microsoft/msphpsql/releases/v5.12.0)

> [!NOTE]
> Make sure you have the latest version of the ODBC driver installed to ensure optimal performance and security. For download information, see [Download ODBC Driver for SQL Server](../odbc/download-odbc-driver-for-sql-server.md).

### Version information

- Release number: 5.12.0
- Released: January 31, 2023

If you have feedback, the best way to contact the Microsoft Drivers for PHP for SQL Server team is by filing an issue on the [GitHub repository](https://github.com/Microsoft/msphpsql/issues).

## Release notes

For details about what has changed in this release, see [the release notes](release-notes-php-sql-driver.md).

## Previous releases

This page is for the latest version of the Microsoft Drivers for PHP only. To download previous versions, see [Previous Microsoft Drivers for PHP for SQL Server Releases](release-notes-php-sql-driver.md#previous-releases).

## See also

[Getting Started with the Microsoft Drivers for PHP for SQL Server](getting-started-with-the-php-sql-driver.md)  
[System Requirements for the Microsoft Drivers for PHP for SQL Server](system-requirements-for-the-php-sql-driver.md)  
[Microsoft PHP Drivers for SQL Server Support Matrix](microsoft-php-drivers-for-sql-server-support-matrix.md)  
[Programming Guide for the Microsoft Drivers for PHP for SQL Server](programming-guide-for-php-sql-driver.md)  
[SQLSRV Driver API Reference](sqlsrv-driver-api-reference.md)  
[PDO_SQLSRV Driver API Reference](pdo-sqlsrv-driver-reference.md)  
