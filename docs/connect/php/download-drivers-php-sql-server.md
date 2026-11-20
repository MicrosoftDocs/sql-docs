---
title: Download the Microsoft Drivers for PHP for SQL Server
description: Download the Microsoft Drivers for PHP for SQL Server to develop PHP applications that connect to SQL Server and Azure SQL Database.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 11/20/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: install-set-up-deploy
ai-usage: ai-assisted
---
# Download the Microsoft Drivers for PHP for SQL Server

The Microsoft Drivers for PHP for SQL Server enable integration with SQL Server for PHP applications. The drivers are PHP extensions that you can use to read and write SQL Server data from within PHP scripts. The drivers provide interfaces for accessing the [Microsoft SQL Database Engine](../../database-engine/sql-database-engine.md) in Azure SQL Database, SQL database in Fabric, Azure SQL Managed Instance, and in [all supported versions and editions](microsoft-php-drivers-for-sql-server-support-matrix.md#sql-server-version-certified-compatibility) of SQL Server (including Express Editions). The drivers use PHP features, including PHP streams, to read and write large objects.

Starting with version 5.14.0, new driver releases are published through the PHP Installer for Extensions (PIE), not the PHP Extension Community Library (PECL). PIE supports Windows, Linux, and macOS. For installation instructions, see [Install on Windows](windows-install.md) or [Install on Linux and macOS](installation-tutorial-linux-mac.md). Prebuilt packages for manual installation remain available on the GitHub release page.

## Download

Microsoft Drivers 5.14.0 for PHP for SQL Server is the latest general availability (GA) version. This release supports PHP 8.4, 8.5, and 8.6. PHP 8.3 and earlier versions aren't supported.

:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Microsoft Drivers for PHP for SQL Server (Windows)](https://go.microsoft.com/fwlink/?LinkId=2389231)**

[GitHub Release Tag v5.14.0 (Linux and macOS packages are available here)](https://github.com/microsoft/msphpsql/releases/tag/v5.14.0)

> [!NOTE]
> Make sure you have the latest version of the ODBC driver installed to ensure optimal performance and security. For download information, see [Download ODBC Driver for SQL Server](../odbc/download-odbc-driver-for-sql-server.md).

### Version information

- Release number: 5.14.0
- Released: November 20, 2026

If you have feedback, the best way to contact the Microsoft Drivers for PHP for SQL Server team is by filing an issue on the [GitHub repository](https://github.com/Microsoft/msphpsql/issues).

## Release notes

For details about what has changed in this release, see [the release notes](release-notes-php-sql-driver.md).

## Previous releases

This page is for the latest version of the Microsoft Drivers for PHP only. To download previous versions, see [Previous Microsoft Drivers for PHP for SQL Server Releases](release-notes-php-sql-driver.md#previous-releases).

## Related content

- [Getting Started with the Microsoft Drivers for PHP for SQL Server](getting-started-with-the-php-sql-driver.md)
- [System requirements for the Microsoft Drivers for PHP for SQL Server](system-requirements-for-the-php-sql-driver.md)
- [Microsoft PHP Drivers for SQL Server Support Matrix](microsoft-php-drivers-for-sql-server-support-matrix.md)
- [Programming Guide for the Microsoft Drivers for PHP for SQL Server](programming-guide-for-php-sql-driver.md)
- [SQLSRV Driver API Reference](sqlsrv-driver-api-reference.md)
- [PDO_SQLSRV Driver Reference](pdo-sqlsrv-driver-reference.md)
