---
title: Microsoft ODBC Driver for SQL Server
description: The Microsoft ODBC Driver for SQL Server provides connectivity to SQL Server and Azure SQL Database via standard ODBC APIs.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest, davidengel, sunilbs, mcimfl
ms.date: 08/17/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
---
# Microsoft ODBC Driver for SQL Server

[!INCLUDE [ODBC_Current_Version](../../includes/odbc-latest-release.md)]

[!INCLUDE [Driver_ODBC_Download](../../includes/driver_odbc_download.md)]

ODBC is the primary native data access API for applications written in C and C++ for SQL Server. There's an ODBC driver for most data sources. Other languages that can use ODBC include COBOL, Perl, PHP, and Python. ODBC is widely used in data integration scenarios.

The ODBC driver comes with tools such as [**sqlcmd**](../../tools/sqlcmd/sqlcmd-utility.md) and [**bcp**](../../tools/bcp/bcp-utility.md). The **`sqlcmd`** utility lets you run Transact-SQL statements, system procedures, and SQL scripts. The **`bcp`** utility bulk copies data between an instance of Microsoft SQL Server and a data file in a format you choose. You can use **`bcp`** to import many new rows into SQL Server tables or to export data out of tables into data files.

## Code example in C++

For a C++ example that demonstrates how to use the ODBC APIs to connect to and access a database, see [Sample ODBC Program](../../odbc/reference/sample-odbc-program.md).

## Download

:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download ODBC driver](download-odbc-driver-for-sql-server.md)**

## Documentation

### Features

- [Connection resiliency in the ODBC driver](connection-resiliency.md)
- [Custom Keystore Providers](custom-keystore-providers.md)
- [Data Classification](data-classification.md)
- [DSN and connection string keywords and attributes](dsn-connection-string-attribute.md)
- [SQL Server Native Client Features](../../relational-databases/native-client/features/sql-server-native-client-features.md) (the features available also apply, without OLEDB, to the ODBC Driver for SQL Server)
- [Using Always Encrypted with the ODBC Driver for SQL Server](using-always-encrypted-with-the-odbc-driver.md)
- [Using Microsoft Entra ID with the ODBC Driver](using-azure-active-directory.md)
- [Use transparent network IP resolution with the ODBC driver](using-transparent-network-ip-resolution.md)
- [Using XA Transactions](use-xa-with-dtc.md)
- [Vector data type (ODBC)](vector-data-type.md)
- [Connection encryption troubleshooting in the ODBC driver](connection-troubleshooting.md)

### Linux and macOS

- [Install the Microsoft ODBC driver for SQL Server (Linux)](linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md)
- [Install the Microsoft ODBC driver for SQL Server (macOS)](linux-mac/install-microsoft-odbc-driver-sql-server-macos.md)
- [Connecting from Linux or macOS](linux-mac/connection-string-keywords-and-data-source-names-dsns.md)
- [bcp utility](../../tools/bcp/bcp-utility.md)
- [Connecting with **sqlcmd**](linux-mac/connecting-with-sqlcmd.md)
- [Data access tracing on Linux and macOS](linux-mac/data-access-tracing-with-the-odbc-driver-on-linux.md)
- [Frequently Asked Questions](linux-mac/frequently-asked-questions-faq-for-odbc-linux.yml)
- [Installing the Driver Manager](linux-mac/installing-the-driver-manager.md)
- [Known issues for the ODBC driver on Linux and macOS](linux-mac/known-issues-in-this-version-of-the-driver.md)
- [Programming Guidelines](linux-mac/programming-guidelines.md)
- [Release notes for the Microsoft ODBC driver for SQL Server on Linux and macOS](linux-mac/release-notes-odbc-sql-server-linux-mac.md)
- [Release notes for the Microsoft SQL Server tools on Linux and macOS](linux-mac/release-notes-tools.md)
- [High availability and disaster recovery](odbc-driver-support-for-high-availability-disaster-recovery.md)
- [Using Integrated Authentication](linux-mac/using-integrated-authentication.md)

### Windows

- [Asynchronous Execution (Notification Method) Sample](windows/asynchronous-execution-notification-method-sample.md)
- [Driver-Aware Connection Pooling in the ODBC Driver for SQL Server](windows/driver-aware-connection-pooling-in-the-odbc-driver-for-sql-server.md)
- [Features of the Microsoft ODBC Driver for SQL Server on Windows](windows/features-of-the-microsoft-odbc-driver-for-sql-server-on-windows.md)
- [Release Notes for Microsoft ODBC Driver for SQL Server on Windows](windows/release-notes-odbc-sql-server-windows.md)
- [System requirements, installation, and driver files](windows/system-requirements-installation-and-driver-files.md)

## Community

- [SQL Server Drivers blog](https://techcommunity.microsoft.com/category/sql-server/blog/sqlserver)
