---
title: "Microsoft ODBC Driver for SQL Server on Windows | Microsoft Docs"
ms.custom: ""
ms.date: "02/14/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: b10cfc22-6a2c-4707-a456-0dcec317982b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Microsoft ODBC Driver for SQL Server on Windows
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The Microsoft ODBC Drivers for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] are stand-alone ODBC drivers which provide an application programming interface (API) implementing the standard ODBC interfaces to Microsoft [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].

The Microsoft ODBC Driver for SQL Server can be used to create new applications. You can also upgrade your older applications which currently use an older ODBC driver. The ODBC Driver for SQL Server supports connections to Azure SQL Database, Azure SQL Data Warehouse, SQL Server 2017, SQL Server 2016, SQL Server 2014, SQL Server 2012, SQL Server 2008 R2, SQL Server 2008, and SQL Server 2005.  

## Summary

| Version       | Features Supported      |
| ------------- |---------------| 
| Microsoft ODBC Driver 17 for SQL Server | <ul><li>Always Encrypted support for BCP API</li><li>New connection string attribute UseFMTONLY causes driver to use legacy metadata in special cases requiring temp tables</li>
| Microsoft ODBC Driver 13.1 for SQL Server     | <ul><li>Always Encrypted</li><li>Azure AD Authentication</li><li>AlwaysOn Availability Groups (AG)</li></ul>   | 
| Microsoft ODBC Driver 13 for SQL Server      | <ul><li>Internationalized Domain Name (IDN)</li></ul> |
| Microsoft ODBC Driver 11 for SQL Server | <ul><li>Driver-Aware Connection Pooling</li><li>Connection Resiliency</li><li>Asynchronous execution (Polling Method)</li></ul> |    

## Documentation  
This documentation for the Microsoft ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] includes:  
  
-   [Release Notes](../../../connect/odbc/windows/release-notes.md)  
-   [Features of the Microsoft ODBC Driver for SQL Server on Windows](../../../connect/odbc/windows/features-of-the-microsoft-odbc-driver-for-sql-server-on-windows.md)  
-   [System Requirements, Installation, and Driver Files](../../../connect/odbc/windows/system-requirements-installation-and-driver-files.md)  
-   [Driver-Aware Connection Pooling in the ODBC Driver for SQL Server](../../../connect/odbc/windows/driver-aware-connection-pooling-in-the-odbc-driver-for-sql-server.md)  
-   [Asynchronous Execution &#40;Notification Method&#41; Sample](../../../connect/odbc/windows/asynchronous-execution-notification-method-sample.md)  
-   [Connection Resiliency in the Windows ODBC Driver](../../../connect/odbc/windows/connection-resiliency-in-the-windows-odbc-driver.md)  
-   [Using Always Encrypted with the ODBC Driver](../../../connect/odbc/using-always-encrypted-with-the-odbc-driver.md)
-   [Using Azure Active Directory with the ODBC Driver](../../../connect/odbc/using-azure-active-directory.md) 
-   [Using Transparent Network IP Resolution](../../../connect/odbc/using-transparent-network-ip-resolution.md)   

## Community  
- [Microsoft ODBC Driver For SQL Server Team blog](https://blogs.msdn.com/sqlnativeclient/default.aspx)  
- [SQL Server Data Access Forum](https://social.technet.microsoft.com/Forums/en/sqldataaccess/threads)  
  
## See Also  
- [About SQL Server Native Client](https://msdn.microsoft.com/sqlserver/ff658532.aspx)   
- [Building Applications with SQL Server Native Client](../../../relational-databases/native-client/applications/building-applications-with-sql-server-native-client.md)   
- [SQL Server Native Client FAQ](https://msdn.microsoft.com/sqlserver/aa937707.aspx)   
- [ODBC Programmer's Reference](../../../odbc/reference/odbc-programmer-s-reference.md)   
- [SQL Server Native Client (ODBC)](../../../relational-databases/native-client/odbc/sql-server-native-client-odbc.md)  
