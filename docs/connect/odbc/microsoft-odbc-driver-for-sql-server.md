---
title: "Microsoft ODBC Driver for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "02/05/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 9f2ae91b-06af-4c9a-9d24-062df7bc4662
author: MightyPen
ms.author: genemi
manager: craigg
---
# Microsoft ODBC Driver for SQL Server

[!INCLUDE[Driver_ODBC_Download](../../includes/driver_odbc_download.md)]

ODBC is the primary native data access API for applications written in C and C++ for SQL Server. There is an ODBC driver for most data sources. Other languages that can use ODBC include COBOL, Perl, PHP, and Python. ODBC is widely used in data integration scenarios.

The ODBC driver comes with tools such as [**sqlcmd**](../../tools/sqlcmd-utility.md) and [**bcp**](../../tools/bcp-utility.md). The **sqlcmd** utility lets you run Transact-SQL statements, system procedures, and SQL scripts. The **bcp** utility bulk copies data between an instance of Microsoft SQL Server and a data file in a format you choose. You can use **bcp** to import many new rows into SQL Server tables or to export data out of tables into data files.  

## Code example in C++

The following C++ sample demonstrates how to use the ODBC APIs to connect to and access a database:

- [C++ code example, using ODBC](../../odbc/reference/sample-odbc-program.md)

## Download

- ![Download-DownArrow-Circled](../../ssdt/media/download.png)[To download ODBC driver](download-odbc-driver-for-sql-server.md)

## Documentation

### Features

- [Custom Keystore Providers](../../connect/odbc/custom-keystore-providers.md)
- [DSN and Connection String Keywords and Attributes](dsn-connection-string-attribute.md)
- [SQL Server Native Client](../../relational-databases/native-client/features/sql-server-native-client-features.md) (the features available also apply, without OLEDB, to the ODBC Driver for SQL Server)
- [Using Always Encrypted](../../connect/odbc/using-always-encrypted-with-the-odbc-driver.md)
- [Using Azure Active Directory](../../connect/odbc/using-azure-active-directory.md)
- [Using Transparent Network IP Resolution](../../connect/odbc/using-transparent-network-ip-resolution.md)
- [Using XA Transactions](../../connect/odbc/use-xa-with-dtc.md)

### Linux and macOS

- [Installing the Driver](../../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md)
- [Connecting to SQL Server](../../connect/odbc/linux-mac/connection-string-keywords-and-data-source-names-dsns.md)
- [Connecting with **bcp**](../../connect/odbc/linux-mac/connecting-with-bcp.md)
- [Connecting with **sqlcmd**](../../connect/odbc/linux-mac/connecting-with-sqlcmd.md)
- [Data Access Tracing](../../connect/odbc/linux-mac/data-access-tracing-with-the-odbc-driver-on-linux.md)
- [Frequently Asked Questions](../../connect/odbc/linux-mac/frequently-asked-questions-faq-for-odbc-linux.md)
- [Installing the Driver Manager](../../connect/odbc/linux-mac/installing-the-driver-manager.md)
- [Known Issues](../../connect/odbc/linux-mac/known-issues-in-this-version-of-the-driver.md)
- [Programming Guidelines](../../connect/odbc/linux-mac/programming-guidelines.md)
- [Release Notes](../../connect/odbc/linux-mac/release-notes-odbc-sql-server-linux-mac.md)
- [Support for High Availability and Disaster Recovery](../../connect/odbc/linux-mac/odbc-driver-on-linux-support-for-high-availability-disaster-recovery.md)
- [Using Integrated Authentication (Kerberos)](../../connect/odbc/linux-mac/using-integrated-authentication.md)

### Windows

- [Asynchronous Execution (Notification Method) Sample](../../connect/odbc/windows/asynchronous-execution-notification-method-sample.md)
- [Connection Resiliency in the Windows ODBC Driver](../../connect/odbc/windows/connection-resiliency-in-the-windows-odbc-driver.md)
- [Driver-Aware Connection Pooling](../../connect/odbc/windows/driver-aware-connection-pooling-in-the-odbc-driver-for-sql-server.md)
- [Features and Behavior Changes](../../connect/odbc/windows/features-of-the-microsoft-odbc-driver-for-sql-server-on-windows.md)
- [Release Notes for ODBC to SQL Server on Windows](windows/release-notes-odbc-sql-server-windows.md)
- [System Requirements, Installation, and Driver Files](../../connect/odbc/windows/system-requirements-installation-and-driver-files.md)



## Community  
- [Microsoft ODBC Driver For SQL Server Team blog](https://blogs.msdn.com/sqlnativeclient/default.aspx)  
- [SQL Server Data Access Forum](https://social.technet.microsoft.com/Forums/en/sqldataaccess/threads)  
