---
title: Microsoft ODBC Driver for SQL Server on Windows
description: Learn about creating applications that use the Microsoft ODBC Driver for SQL Server on Windows.
author: David-Engel
ms.author: v-davidengel
ms.date: 02/15/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Microsoft ODBC Driver for SQL Server on Windows

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The Microsoft ODBC Drivers for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] are stand-alone ODBC drivers which provide an application programming interface (API) implementing the standard ODBC interfaces to Microsoft [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].

The Microsoft ODBC Driver for SQL Server can be used to create new applications. You can also upgrade your older applications which currently use an older ODBC driver. The ODBC Driver for SQL Server supports connections to Azure SQL Database, Azure Synapse Analytics, and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].

## Summary

| Version       | Features Supported      |
| ------------- |---------------|
| Microsoft ODBC Driver 18 for SQL Server | <ul><li>Support for TDS 8.0</li><li>Extensions to SQLGetData</li><li>Option to send `SQL_LONG_*` types as `(max)`-types</li></ul>  |
| Microsoft ODBC Driver 17 for SQL Server | <ul><li>Always Encrypted support for BCP API</li><li>New connection string attribute UseFMTONLY causes driver to use legacy metadata in special cases requiring temp tables</li>
| Microsoft ODBC Driver 13.1 for SQL Server     | <ul><li>Always Encrypted</li><li>Azure AD Authentication</li><li>Always On Availability Groups (AG)</li></ul>   |
| Microsoft ODBC Driver 13 for SQL Server      | <ul><li>Internationalized Domain Name (IDN)</li></ul> |
| Microsoft ODBC Driver 11 for SQL Server | <ul><li>Driver-Aware Connection Pooling</li><li>Connection Resiliency</li><li>Asynchronous execution (Polling Method)</li></ul> |

## Documentation

This documentation for the Microsoft ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] includes:

- [Release Notes for ODBC to SQL Server on Windows](release-notes-odbc-sql-server-windows.md)
- [Features of the Microsoft ODBC Driver for SQL Server on Windows](features-of-the-microsoft-odbc-driver-for-sql-server-on-windows.md)
- [System Requirements, Installation, and Driver Files](system-requirements-installation-and-driver-files.md)
- [Driver-Aware Connection Pooling in the ODBC Driver for SQL Server](driver-aware-connection-pooling-in-the-odbc-driver-for-sql-server.md)
- [Asynchronous Execution &#40;Notification Method&#41; Sample](asynchronous-execution-notification-method-sample.md)
- [Connection Resiliency in the Windows ODBC Driver](../connection-resiliency.md)
- [Using Always Encrypted with the ODBC Driver](../using-always-encrypted-with-the-odbc-driver.md)
- [Using Azure Active Directory with the ODBC Driver](../using-azure-active-directory.md)
- [Using Transparent Network IP Resolution](../using-transparent-network-ip-resolution.md)

## Community

- [SQL Server Drivers blog](https://techcommunity.microsoft.com/t5/sql-server/bg-p/SQLServer/label-name/SQLServerDrivers)
- [SQL Server Data Access Forum](https://social.technet.microsoft.com/Forums/en/sqldataaccess/threads)

## See Also

- [Building Applications with SQL Server Native Client](../../../relational-databases/native-client/applications/building-applications-with-sql-server-native-client.md)
- [SQL Server Native Client FAQ](/previous-versions/aa937707(v=msdn.10))
- [ODBC Programmer's Reference](../../../odbc/reference/odbc-programmer-s-reference.md)
- [SQL Server Native Client (ODBC)](../../../relational-databases/native-client/odbc/sql-server-native-client-odbc.md)
