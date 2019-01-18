---
title: "Features of the Microsoft ODBC Driver for SQL Server on Windows | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 76326eeb-1144-4b9f-85db-50524c655d30
author: MightyPen
ms.author: genemi
manager: craigg
---
# Features of the Microsoft ODBC Driver for SQL Server on Windows
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

    
## Microsoft ODBC Driver 13.1 for SQL Server on Windows

The ODBC Driver 13.1 for SQL Server contains all the functionality of the previous version (11) and adds support for Always Encrypted and Azure Active Directory authentication when used in conjunction with Microsoft SQL Server 2016.  
  
Always Encrypted allows clients to encrypt sensitive data inside client applications and never reveal the encryption keys to SQL Server. An Always Encrypted enabled driver installed on the client computer achieves this by automatically encrypting and decrypting sensitive data in the SQL Server client application. The driver encrypts the data in sensitive columns before passing the data to SQL Server, and automatically rewrites queries so that the semantics to the application are preserved. Similarly, the driver transparently decrypts data stored in encrypted database columns that are contained in query results. For more information, see [Using Always Encrypted with the ODBC Driver](../../../connect/odbc/using-always-encrypted-with-the-odbc-driver.md).
 
Azure Active Directory allows users, DBA's, and application programmers to use Azure Active Directory authentication as a mechanism of connecting to Microsoft Azure SQL Database and Microsoft SQL Server 2016 by using identities in Azure Active Directory (Azure AD). For more information, see [Using Azure Active Directory with the ODBC Driver](../../../connect/odbc/using-azure-active-directory.md), and [Connecting to SQL Database or SQL Data Warehouse By Using Azure Active Directory Authentication](https://azure.microsoft.com/documentation/articles/sql-database-aad-authentication/).   
  
## Microsoft ODBC Driver 11 for SQL Server on Windows  

The ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] contains all the functionality of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver that shipped in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)]. For more information, see [SQL Server Native Client Programming](../../../relational-databases/native-client/sql-server-native-client-programming.md). The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver is based on the ODBC driver that ships in the Windows operating system. For more information, see [Windows Data Access Components SDK](https://msdn.microsoft.com/library/aa968814(VS.85).aspx).  
  
This release of the ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] contains the following new features:  
  
### bcp.exe -l option for specifying a login timeout
 
The -l option specifies the number of seconds before a `bcp.exe` login to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] times out when you try to connect to a server. The default login timeout is 15 seconds. The login timeout must be a number between 0 and 65534. If the value supplied is not numeric or does not fall into that range, `bcp.exe` generates an error message. A value of 0 specifies an infinite timeout. A login timeout of less than (approximately) 10 seconds is not reliable.  
  
### Driver-Aware Connection Pooling  
The ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports [Driver-Aware Connection Pooling](https://msdn.microsoft.com/library/hh405031(VS.85).aspx). For more information, see [Driver-Aware Connection Pooling in the ODBC Driver for SQL Server](../../../connect/odbc/windows/driver-aware-connection-pooling-in-the-odbc-driver-for-sql-server.md).  
  
### Asynchronous Execution (Notification Method)  
The ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports [Asynchronous Execution (Notification Method)](https://msdn.microsoft.com/library/hh405038(VS.85).aspx). For a usage sample, see [Asynchronous Execution &#40;Notification Method&#41; Sample](../../../connect/odbc/windows/asynchronous-execution-notification-method-sample.md).  
  
### Connection Resiliency
To ensure that applications remain connected to a Microsoft Azure SQL Database, the ODBC driver on Windows can restore idle connections. For more information, see [Connection Resiliency in the Windows ODBC Driver](../../../connect/odbc/windows/connection-resiliency-in-the-windows-odbc-driver.md).  
  
## Behavior Changes

In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, the `-y0` option for `sqlcmd.exe` caused output to be truncated at 1 MB if the display width was 0.
  
Beginning in the ODBC Driver 11 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], there is no limit on the amount of data that can be retrieved in a single column when `-y0` is specified. `sqlcmd.exe` now streams columns as large as 2 GB ( [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type maximum).  
  
Another difference is that specifying both `-h` and `-y0` now produces an error reporting that the options are incompatible. `-h`, which specifies the number of rows to print between the column headings and has never been compatible with `-y0`, was ignored although no headers were printed.
  
Note that `-y0` can cause performance issues on both the server and the network, depending on the size of the data returned.

## See Also  
[Microsoft ODBC Driver for SQL Server on Windows](../../../connect/odbc/windows/microsoft-odbc-driver-for-sql-server-on-windows.md)  
